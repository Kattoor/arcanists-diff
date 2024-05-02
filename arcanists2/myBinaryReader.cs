// Decompiled with JetBrains decompiler
// Type: myBinaryReader
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public class myBinaryReader : BinaryReader
{
  private int readLength;
  private int i;

  private long Length => this.BaseStream.Length;

  internal myBinaryReader(Stream stream)
    : base(stream)
  {
  }

  internal myBinaryReader(Stream stream, Encoding encoding)
    : base(stream, encoding)
  {
  }

  public virtual byte[] ReadBytes()
  {
    this.readLength = this.ReadInt32();
    byte[] numArray = new byte[this.readLength];
    for (this.i = 0; this.i < this.readLength; ++this.i)
      numArray[this.i] = this.ReadByte();
    return numArray;
  }

  public virtual MyLocation ReadMyLocation()
  {
    return new MyLocation()
    {
      x = (FixedInt) this.ReadInt64(),
      y = (FixedInt) this.ReadInt64()
    };
  }

  public virtual Vector2 ReadVector2()
  {
    return new Vector2()
    {
      x = this.ReadSingle(),
      y = this.ReadSingle()
    };
  }

  public virtual Vector3 ReadVector3()
  {
    return new Vector3()
    {
      x = this.ReadSingle(),
      y = this.ReadSingle(),
      z = this.ReadSingle()
    };
  }

  public virtual Quaternion ReadQuaternion()
  {
    return Quaternion.identity with
    {
      x = this.ReadSingle(),
      y = this.ReadSingle(),
      z = this.ReadSingle(),
      w = this.ReadSingle()
    };
  }

  public virtual Vector4 ReadVector4()
  {
    return new Vector4()
    {
      x = this.ReadSingle(),
      y = this.ReadSingle(),
      z = this.ReadSingle(),
      w = this.ReadSingle()
    };
  }

  public virtual Color32 ReadColor32()
  {
    Color32 color32 = new Color32();
    color32.a = this.ReadByte();
    if (color32.a == (byte) 0)
      return color32;
    color32.r = color32.a;
    color32.a = byte.MaxValue;
    color32.g = this.ReadByte();
    color32.b = this.ReadByte();
    return color32;
  }

  public virtual Color32 ReadColor32NoAlpha()
  {
    return new Color32()
    {
      a = byte.MaxValue,
      r = this.ReadByte(),
      g = this.ReadByte(),
      b = this.ReadByte()
    };
  }

  public virtual Color ReadColor()
  {
    return new Color()
    {
      r = this.ReadSingle(),
      g = this.ReadSingle(),
      b = this.ReadSingle(),
      a = this.ReadSingle()
    };
  }
}
