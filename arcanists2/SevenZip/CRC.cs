﻿// Decompiled with JetBrains decompiler
// Type: SevenZip.CRC
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace SevenZip
{
  internal class CRC
  {
    public static readonly uint[] Table = new uint[256];
    private uint _value = uint.MaxValue;

    static CRC()
    {
      for (uint index1 = 0; index1 < 256U; ++index1)
      {
        uint num = index1;
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (((int) num & 1) != 0)
            num = num >> 1 ^ 3988292384U;
          else
            num >>= 1;
        }
        CRC.Table[(int) index1] = num;
      }
    }

    public void Init() => this._value = uint.MaxValue;

    public void UpdateByte(byte b)
    {
      this._value = CRC.Table[(int) (byte) this._value ^ (int) b] ^ this._value >> 8;
    }

    public void Update(byte[] data, uint offset, uint size)
    {
      for (uint index = 0; index < size; ++index)
        this._value = CRC.Table[(int) (byte) this._value ^ (int) data[(int) offset + (int) index]] ^ this._value >> 8;
    }

    public uint GetDigest() => this._value ^ uint.MaxValue;

    private static uint CalculateDigest(byte[] data, uint offset, uint size)
    {
      CRC crc = new CRC();
      crc.Update(data, offset, size);
      return crc.GetDigest();
    }

    private static bool VerifyDigest(uint digest, byte[] data, uint offset, uint size)
    {
      return (int) CRC.CalculateDigest(data, offset, size) == (int) digest;
    }
  }
}