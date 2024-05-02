

using System;

#nullable disable
namespace Junk
{
  public class IsaacCipher
  {
    private int _seed;
    private int[] output;
    private int[] internalArray;
    private int nextIdx;
    private int aa;
    private int bb;
    private int cc;

    public static int LogicalShift(int i, int shift) => i >>> shift;

    public int seed => this._seed;

    private static void mix(int[] s)
    {
      s[0] ^= s[1] << 11;
      s[3] += s[0];
      s[1] += s[2];
      s[1] ^= IsaacCipher.LogicalShift(s[2], 2);
      s[4] += s[1];
      s[2] += s[3];
      s[2] ^= s[3] << 8;
      s[5] += s[2];
      s[3] += s[4];
      s[3] ^= IsaacCipher.LogicalShift(s[4], 16);
      s[6] += s[3];
      s[4] += s[5];
      s[4] ^= s[5] << 10;
      s[7] += s[4];
      s[5] += s[6];
      s[5] ^= IsaacCipher.LogicalShift(s[6], 4);
      s[0] += s[5];
      s[6] += s[7];
      s[6] ^= s[7] << 8;
      s[1] += s[6];
      s[7] += s[0];
      s[7] ^= IsaacCipher.LogicalShift(s[0], 9);
      s[2] += s[7];
      s[0] += s[1];
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this._seed);
      w.Write(this.output.Length);
      w.Write(this.internalArray.Length);
      w.Write(this.nextIdx);
      w.Write(this.aa);
      w.Write(this.bb);
      w.Write(this.cc);
      for (int index = 0; index < this.output.Length; ++index)
        w.Write(this.output[index]);
      for (int index = 0; index < this.internalArray.Length; ++index)
        w.Write(this.internalArray[index]);
    }

    public static IsaacCipher Deserialize(myBinaryReader r)
    {
      IsaacCipher isaacCipher = new IsaacCipher(false);
      isaacCipher._seed = r.ReadInt32();
      isaacCipher.output = new int[r.ReadInt32()];
      isaacCipher.internalArray = new int[r.ReadInt32()];
      isaacCipher.nextIdx = r.ReadInt32();
      isaacCipher.aa = r.ReadInt32();
      isaacCipher.bb = r.ReadInt32();
      isaacCipher.cc = r.ReadInt32();
      for (int index = 0; index < isaacCipher.output.Length; ++index)
        isaacCipher.output[index] = r.ReadInt32();
      for (int index = 0; index < isaacCipher.internalArray.Length; ++index)
        isaacCipher.internalArray[index] = r.ReadInt32();
      return isaacCipher;
    }

    public IsaacCipher(bool donotinit)
    {
    }

    public IsaacCipher(params int[] key)
    {
      this._seed = key[0];
      this.output = new int[256];
      int num = Math.Min(this.output.Length, key.Length);
      for (int index = 0; index < num; ++index)
        this.output[index] = key[index];
      this.internalArray = new int[256];
      int[] s = new int[8];
      for (int index = 0; index < s.Length; ++index)
        s[index] = -1640531527;
      IsaacCipher.mix(s);
      IsaacCipher.mix(s);
      IsaacCipher.mix(s);
      IsaacCipher.mix(s);
      for (int index1 = 0; index1 < 256; index1 += 8)
      {
        for (int index2 = 0; index2 < 8; ++index2)
          s[index2] += this.output[index1 + index2];
        IsaacCipher.mix(s);
        for (int index3 = 0; index3 < 8; ++index3)
          this.internalArray[index1 + index3] = s[index3];
      }
      for (int index4 = 0; index4 < 256; index4 += 8)
      {
        for (int index5 = 0; index5 < 8; ++index5)
          s[index5] += this.internalArray[index4 + index5];
        IsaacCipher.mix(s);
        for (int index6 = 0; index6 < 8; ++index6)
          this.internalArray[index4 + index6] = s[index6];
      }
      this.nextRound();
    }

    private void nextRound()
    {
      ++this.cc;
      this.bb += this.cc;
      for (int index = 0; index < 256; ++index)
      {
        switch (index & 3)
        {
          case 0:
            this.aa ^= this.aa << 13;
            break;
          case 1:
            this.aa ^= IsaacCipher.LogicalShift(this.aa, 6);
            break;
          case 2:
            this.aa ^= this.aa << 2;
            break;
          case 3:
            this.aa ^= IsaacCipher.LogicalShift(this.aa, 16);
            break;
        }
        int num1 = this.internalArray[index];
        this.aa += this.internalArray[index ^ 128];
        int num2;
        this.internalArray[index] = num2 = this.internalArray[num1 >> 2 & (int) byte.MaxValue] + this.aa + this.bb;
        this.output[index] = this.bb = this.internalArray[num2 >> 10 & (int) byte.MaxValue] + num1;
      }
      this.nextIdx = 256;
    }

    public int next()
    {
      if (this.nextIdx == 0)
        this.nextRound();
      return this.output[--this.nextIdx];
    }

    public int next(int bound)
    {
      if ((bound & bound - 1) == 0)
        return (int) ((long) bound * ((long) this.next() & (long) uint.MaxValue) >> 32);
      int num1 = int.MaxValue - (int) (4294967296L % (long) bound);
      int num2;
      do
      {
        num2 = this.next();
      }
      while (num2 > num1);
      return num2 >= 0 ? num2 % bound : bound - 1 + (num2 + 1) % bound;
    }

    public int next2(int bound)
    {
      int num1 = int.MaxValue - (int) (4294967296L % (long) bound);
      int num2;
      do
      {
        num2 = this.next();
      }
      while (num2 > num1);
      return num2 >= 0 ? num2 % bound : bound - 1 + (num2 + 1) % bound;
    }

    public int Next(int min, int max)
    {
      return max - min <= 0 ? min : Math.Abs(this.next()) % (max - min) + min;
    }
  }
}
