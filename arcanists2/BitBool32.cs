// Decompiled with JetBrains decompiler
// Type: BitBool32
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public class BitBool32
{
  private int array;

  public bool this[int index]
  {
    get => 1 << index != 0;
    set
    {
      if (value)
        this.array |= 1 << index;
      else
        this.array &= ~(1 << index);
    }
  }

  public void Copy(BitBool32 b) => this.array = b.array;

  public void ResetAll() => this.array = 0;

  public void SetAll() => this.array = -1;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index <= 31; ++index)
      stringBuilder.Append(this[index] ? "1" : "0");
    return stringBuilder.ToString();
  }

  public void Serialize(myBinaryWriter w) => w.Write(this.array);

  public void Deserialize(myBinaryReader r) => this.array = r.ReadInt32();
}
