﻿// Decompiled with JetBrains decompiler
// Type: SevenZip.Compression.RangeCoder.BitTreeDecoder
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace SevenZip.Compression.RangeCoder
{
  internal struct BitTreeDecoder
  {
    private BitDecoder[] Models;
    private int NumBitLevels;

    public BitTreeDecoder(int numBitLevels)
    {
      this.NumBitLevels = numBitLevels;
      this.Models = new BitDecoder[1 << numBitLevels];
    }

    public void Init()
    {
      for (uint index = 1; (long) index < (long) (1 << this.NumBitLevels); ++index)
        this.Models[(int) index].Init();
    }

    public uint Decode(Decoder rangeDecoder)
    {
      uint index = 1;
      for (int numBitLevels = this.NumBitLevels; numBitLevels > 0; --numBitLevels)
        index = (index << 1) + this.Models[(int) index].Decode(rangeDecoder);
      return index - (uint) (1 << this.NumBitLevels);
    }

    public uint ReverseDecode(Decoder rangeDecoder)
    {
      uint index1 = 1;
      uint num1 = 0;
      for (int index2 = 0; index2 < this.NumBitLevels; ++index2)
      {
        uint num2 = this.Models[(int) index1].Decode(rangeDecoder);
        index1 = (index1 << 1) + num2;
        num1 |= num2 << index2;
      }
      return num1;
    }

    public static uint ReverseDecode(
      BitDecoder[] Models,
      uint startIndex,
      Decoder rangeDecoder,
      int NumBitLevels)
    {
      uint num1 = 1;
      uint num2 = 0;
      for (int index = 0; index < NumBitLevels; ++index)
      {
        uint num3 = Models[(int) startIndex + (int) num1].Decode(rangeDecoder);
        num1 = (num1 << 1) + num3;
        num2 |= num3 << index;
      }
      return num2;
    }
  }
}
