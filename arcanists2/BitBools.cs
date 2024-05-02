// Decompiled with JetBrains decompiler
// Type: BitBools
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

#nullable disable
[Serializable]
public class BitBools
{
  public int[] array = new int[8];

  public override bool Equals(object obj)
  {
    if (!(obj is BitBools))
      return base.Equals(obj);
    BitBools bitBools = obj as BitBools;
    for (int index = 0; index < this.array.Length; ++index)
    {
      if (this.array[index] != bitBools.array[index])
        return false;
    }
    return true;
  }

  public bool this[int index]
  {
    get => (this.array[index >> 5] & 1 << index) != 0;
    set
    {
      if (value)
        this.array[index >> 5] |= 1 << index;
      else
        this.array[index >> 5] &= ~(1 << index);
    }
  }

  public void Copy(BitBools b)
  {
    for (int index = 0; index < this.array.Length; ++index)
      this.array[index] = b.array[index];
  }

  public bool AllOn()
  {
    for (int index = 0; index < this.array.Length; ++index)
    {
      if (this.array[index] != -1)
        return false;
    }
    return true;
  }

  public void ResetAll()
  {
    for (int index = 0; index < this.array.Length; ++index)
      this.array[index] = 0;
  }

  public void SetAll()
  {
    for (int index = 0; index < this.array.Length; ++index)
      this.array[index] = -1;
  }

  public bool UsedAllSpells()
  {
    return this.GetChunk(0) == -1 && this.GetChunk(1) == -1 && this.GetChunk(2) == -1 && this.GetChunk(3) == -1 && this.GetChunk(4) == -1 && this.GetChunk(5) == 1048575;
  }

  public bool AllOGSpellsMinusCogs()
  {
    return this.GetChunk(0) == -1 && this.GetChunk(1) == -1 && this.GetChunk(2) == -1 && this.GetChunk(3) == 4095;
  }

  public int GetChunk(int i) => this.array[i];

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index <= (int) byte.MaxValue; ++index)
    {
      stringBuilder.Append(this[index] ? "1" : "0");
      if ((index & 31) == 31)
        stringBuilder.Append(" ");
    }
    return stringBuilder.ToString();
  }

  public void Serialize(myBinaryWriter w)
  {
    for (int index = 0; index < 8; ++index)
      w.Write(this.array[index]);
  }

  public void Deserialize(myBinaryReader r)
  {
    for (int index = 0; index < 8; ++index)
      this.array[index] = r.ReadInt32();
  }
}
