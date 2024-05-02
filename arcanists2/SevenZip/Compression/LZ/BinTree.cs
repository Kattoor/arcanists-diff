

using System;
using System.IO;

#nullable disable
namespace SevenZip.Compression.LZ
{
  public class BinTree : InWindow, IMatchFinder, IInWindowStream
  {
    private uint _cyclicBufferPos;
    private uint _cyclicBufferSize;
    private uint _matchMaxLen;
    private uint[] _son;
    private uint[] _hash;
    private uint _cutValue = (uint) byte.MaxValue;
    private uint _hashMask;
    private uint _hashSizeSum;
    private bool HASH_ARRAY = true;
    private const uint kHash2Size = 1024;
    private const uint kHash3Size = 65536;
    private const uint kBT2HashSize = 65536;
    private const uint kStartMaxLen = 1;
    private const uint kHash3Offset = 1024;
    private const uint kEmptyHashValue = 0;
    private const uint kMaxValForNormalize = 2147483647;
    private uint kNumHashDirectBytes;
    private uint kMinMatchCheck = 4;
    private uint kFixHashSize = 66560;

    public void SetType(int numHashBytes)
    {
      this.HASH_ARRAY = numHashBytes > 2;
      if (this.HASH_ARRAY)
      {
        this.kNumHashDirectBytes = 0U;
        this.kMinMatchCheck = 4U;
        this.kFixHashSize = 66560U;
      }
      else
      {
        this.kNumHashDirectBytes = 2U;
        this.kMinMatchCheck = 3U;
        this.kFixHashSize = 0U;
      }
    }

    public new void SetStream(Stream stream) => base.SetStream(stream);

    public new void ReleaseStream() => base.ReleaseStream();

    public new void Init()
    {
      base.Init();
      for (uint index = 0; index < this._hashSizeSum; ++index)
        this._hash[(int) index] = 0U;
      this._cyclicBufferPos = 0U;
      this.ReduceOffsets(-1);
    }

    public new void MovePos()
    {
      if (++this._cyclicBufferPos >= this._cyclicBufferSize)
        this._cyclicBufferPos = 0U;
      base.MovePos();
      if (this._pos != (uint) int.MaxValue)
        return;
      this.Normalize();
    }

    public new byte GetIndexByte(int index) => base.GetIndexByte(index);

    public new uint GetMatchLen(int index, uint distance, uint limit)
    {
      return base.GetMatchLen(index, distance, limit);
    }

    public new uint GetNumAvailableBytes() => base.GetNumAvailableBytes();

    public void Create(
      uint historySize,
      uint keepAddBufferBefore,
      uint matchMaxLen,
      uint keepAddBufferAfter)
    {
      if (historySize > 2147483391U)
        throw new Exception();
      this._cutValue = 16U + (matchMaxLen >> 1);
      uint keepSizeReserv = (historySize + keepAddBufferBefore + matchMaxLen + keepAddBufferAfter) / 2U + 256U;
      this.Create(historySize + keepAddBufferBefore, matchMaxLen + keepAddBufferAfter, keepSizeReserv);
      this._matchMaxLen = matchMaxLen;
      uint num1 = historySize + 1U;
      if ((int) this._cyclicBufferSize != (int) num1)
        this._son = new uint[(int) (this._cyclicBufferSize = num1) * 2];
      uint num2 = 65536;
      if (this.HASH_ARRAY)
      {
        uint num3 = historySize - 1U;
        uint num4 = num3 | num3 >> 1;
        uint num5 = num4 | num4 >> 2;
        uint num6 = num5 | num5 >> 4;
        uint num7 = (num6 | num6 >> 8) >> 1 | (uint) ushort.MaxValue;
        if (num7 > 16777216U)
          num7 >>= 1;
        this._hashMask = num7;
        num2 = num7 + 1U + this.kFixHashSize;
      }
      if ((int) num2 == (int) this._hashSizeSum)
        return;
      this._hash = new uint[(int) (this._hashSizeSum = num2)];
    }

    public uint GetMatches(uint[] distances)
    {
      uint num1;
      if (this._pos + this._matchMaxLen <= this._streamPos)
      {
        num1 = this._matchMaxLen;
      }
      else
      {
        num1 = this._streamPos - this._pos;
        if (num1 < this.kMinMatchCheck)
        {
          this.MovePos();
          return 0;
        }
      }
      uint matches = 0;
      uint num2 = this._pos > this._cyclicBufferSize ? this._pos - this._cyclicBufferSize : 0U;
      uint index1 = this._bufferOffset + this._pos;
      uint num3 = 1;
      uint index2 = 0;
      uint num4 = 0;
      uint num5;
      if (this.HASH_ARRAY)
      {
        int num6 = (int) CRC.Table[(int) this._bufferBase[(int) index1]] ^ (int) this._bufferBase[(int) index1 + 1];
        index2 = (uint) (num6 & 1023);
        int num7 = num6 ^ (int) this._bufferBase[(int) index1 + 2] << 8;
        num4 = (uint) (num7 & (int) ushort.MaxValue);
        num5 = (uint) (num7 ^ (int) CRC.Table[(int) this._bufferBase[(int) index1 + 3]] << 5) & this._hashMask;
      }
      else
        num5 = (uint) this._bufferBase[(int) index1] ^ (uint) this._bufferBase[(int) index1 + 1] << 8;
      uint num8 = this._hash[(int) this.kFixHashSize + (int) num5];
      if (this.HASH_ARRAY)
      {
        uint num9 = this._hash[(int) index2];
        uint num10 = this._hash[1024 + (int) num4];
        this._hash[(int) index2] = this._pos;
        this._hash[1024 + (int) num4] = this._pos;
        if (num9 > num2 && (int) this._bufferBase[(int) this._bufferOffset + (int) num9] == (int) this._bufferBase[(int) index1])
        {
          uint[] numArray1 = distances;
          int index3 = (int) matches;
          uint num11 = (uint) (index3 + 1);
          int num12;
          num3 = (uint) (num12 = 2);
          numArray1[index3] = (uint) num12;
          uint[] numArray2 = distances;
          int index4 = (int) num11;
          matches = (uint) (index4 + 1);
          int num13 = (int) this._pos - (int) num9 - 1;
          numArray2[index4] = (uint) num13;
        }
        if (num10 > num2 && (int) this._bufferBase[(int) this._bufferOffset + (int) num10] == (int) this._bufferBase[(int) index1])
        {
          if ((int) num10 == (int) num9)
            matches -= 2U;
          uint[] numArray3 = distances;
          int index5 = (int) matches;
          uint num14 = (uint) (index5 + 1);
          int num15;
          num3 = (uint) (num15 = 3);
          numArray3[index5] = (uint) num15;
          uint[] numArray4 = distances;
          int index6 = (int) num14;
          matches = (uint) (index6 + 1);
          int num16 = (int) this._pos - (int) num10 - 1;
          numArray4[index6] = (uint) num16;
          num9 = num10;
        }
        if (matches != 0U && (int) num9 == (int) num8)
        {
          matches -= 2U;
          num3 = 1U;
        }
      }
      this._hash[(int) this.kFixHashSize + (int) num5] = this._pos;
      uint index7 = (uint) (((int) this._cyclicBufferPos << 1) + 1);
      uint index8 = this._cyclicBufferPos << 1;
      uint val2;
      uint val1 = val2 = this.kNumHashDirectBytes;
      if (this.kNumHashDirectBytes != 0U && num8 > num2 && (int) this._bufferBase[(int) this._bufferOffset + (int) num8 + (int) this.kNumHashDirectBytes] != (int) this._bufferBase[(int) index1 + (int) this.kNumHashDirectBytes])
      {
        uint[] numArray5 = distances;
        int index9 = (int) matches;
        uint num17 = (uint) (index9 + 1);
        int numHashDirectBytes;
        num3 = (uint) (numHashDirectBytes = (int) this.kNumHashDirectBytes);
        numArray5[index9] = (uint) numHashDirectBytes;
        uint[] numArray6 = distances;
        int index10 = (int) num17;
        matches = (uint) (index10 + 1);
        int num18 = (int) this._pos - (int) num8 - 1;
        numArray6[index10] = (uint) num18;
      }
      uint cutValue = this._cutValue;
      while (num8 > num2 && cutValue-- != 0U)
      {
        uint num19 = this._pos - num8;
        uint index11 = (uint) ((num19 <= this._cyclicBufferPos ? (int) this._cyclicBufferPos - (int) num19 : (int) this._cyclicBufferPos - (int) num19 + (int) this._cyclicBufferSize) << 1);
        uint num20 = this._bufferOffset + num8;
        uint num21 = Math.Min(val1, val2);
        if ((int) this._bufferBase[(int) num20 + (int) num21] == (int) this._bufferBase[(int) index1 + (int) num21])
        {
          do
            ;
          while ((int) ++num21 != (int) num1 && (int) this._bufferBase[(int) num20 + (int) num21] == (int) this._bufferBase[(int) index1 + (int) num21]);
          if (num3 < num21)
          {
            uint[] numArray7 = distances;
            int index12 = (int) matches;
            uint num22 = (uint) (index12 + 1);
            int num23;
            num3 = (uint) (num23 = (int) num21);
            numArray7[index12] = (uint) num23;
            uint[] numArray8 = distances;
            int index13 = (int) num22;
            matches = (uint) (index13 + 1);
            int num24 = (int) num19 - 1;
            numArray8[index13] = (uint) num24;
            if ((int) num21 == (int) num1)
            {
              this._son[(int) index8] = this._son[(int) index11];
              this._son[(int) index7] = this._son[(int) index11 + 1];
              goto label_29;
            }
          }
        }
        if ((int) this._bufferBase[(int) num20 + (int) num21] < (int) this._bufferBase[(int) index1 + (int) num21])
        {
          this._son[(int) index8] = num8;
          index8 = index11 + 1U;
          num8 = this._son[(int) index8];
          val2 = num21;
        }
        else
        {
          this._son[(int) index7] = num8;
          index7 = index11;
          num8 = this._son[(int) index7];
          val1 = num21;
        }
      }
      this._son[(int) index7] = this._son[(int) index8] = 0U;
label_29:
      this.MovePos();
      return matches;
    }

    public void Skip(uint num)
    {
      do
      {
        uint num1;
        if (this._pos + this._matchMaxLen <= this._streamPos)
        {
          num1 = this._matchMaxLen;
        }
        else
        {
          num1 = this._streamPos - this._pos;
          if (num1 < this.kMinMatchCheck)
          {
            this.MovePos();
            goto label_19;
          }
        }
        uint num2 = this._pos > this._cyclicBufferSize ? this._pos - this._cyclicBufferSize : 0U;
        uint index1 = this._bufferOffset + this._pos;
        uint num3;
        if (this.HASH_ARRAY)
        {
          int num4 = (int) CRC.Table[(int) this._bufferBase[(int) index1]] ^ (int) this._bufferBase[(int) index1 + 1];
          this._hash[num4 & 1023] = this._pos;
          int num5 = num4 ^ (int) this._bufferBase[(int) index1 + 2] << 8;
          this._hash[1024 + (num5 & (int) ushort.MaxValue)] = this._pos;
          num3 = (uint) (num5 ^ (int) CRC.Table[(int) this._bufferBase[(int) index1 + 3]] << 5) & this._hashMask;
        }
        else
          num3 = (uint) this._bufferBase[(int) index1] ^ (uint) this._bufferBase[(int) index1 + 1] << 8;
        uint num6 = this._hash[(int) this.kFixHashSize + (int) num3];
        this._hash[(int) this.kFixHashSize + (int) num3] = this._pos;
        uint index2 = (uint) (((int) this._cyclicBufferPos << 1) + 1);
        uint index3 = this._cyclicBufferPos << 1;
        uint val2;
        uint val1 = val2 = this.kNumHashDirectBytes;
        uint cutValue = this._cutValue;
        while (num6 > num2 && cutValue-- != 0U)
        {
          uint num7 = this._pos - num6;
          uint index4 = (uint) ((num7 <= this._cyclicBufferPos ? (int) this._cyclicBufferPos - (int) num7 : (int) this._cyclicBufferPos - (int) num7 + (int) this._cyclicBufferSize) << 1);
          uint num8 = this._bufferOffset + num6;
          uint num9 = Math.Min(val1, val2);
          if ((int) this._bufferBase[(int) num8 + (int) num9] == (int) this._bufferBase[(int) index1 + (int) num9])
          {
            do
              ;
            while ((int) ++num9 != (int) num1 && (int) this._bufferBase[(int) num8 + (int) num9] == (int) this._bufferBase[(int) index1 + (int) num9]);
            if ((int) num9 == (int) num1)
            {
              this._son[(int) index3] = this._son[(int) index4];
              this._son[(int) index2] = this._son[(int) index4 + 1];
              goto label_18;
            }
          }
          if ((int) this._bufferBase[(int) num8 + (int) num9] < (int) this._bufferBase[(int) index1 + (int) num9])
          {
            this._son[(int) index3] = num6;
            index3 = index4 + 1U;
            num6 = this._son[(int) index3];
            val2 = num9;
          }
          else
          {
            this._son[(int) index2] = num6;
            index2 = index4;
            num6 = this._son[(int) index2];
            val1 = num9;
          }
        }
        this._son[(int) index2] = this._son[(int) index3] = 0U;
label_18:
        this.MovePos();
label_19:;
      }
      while (--num != 0U);
    }

    private void NormalizeLinks(uint[] items, uint numItems, uint subValue)
    {
      for (uint index = 0; index < numItems; ++index)
      {
        uint num1 = items[(int) index];
        uint num2 = num1 > subValue ? num1 - subValue : 0U;
        items[(int) index] = num2;
      }
    }

    private void Normalize()
    {
      uint subValue = this._pos - this._cyclicBufferSize;
      this.NormalizeLinks(this._son, this._cyclicBufferSize * 2U, subValue);
      this.NormalizeLinks(this._hash, this._hashSizeSum, subValue);
      this.ReduceOffsets((int) subValue);
    }

    public void SetCutValue(uint cutValue) => this._cutValue = cutValue;
  }
}
