// Decompiled with JetBrains decompiler
// Type: myBinaryWriter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public class myBinaryWriter : BinaryWriter
{
  private long Length => this.BaseStream.Length;

  public myBinaryWriter()
    : base((Stream) new MemoryStream(256))
  {
  }

  public myBinaryWriter(Encoding encoding)
    : base((Stream) new MemoryStream(256), encoding)
  {
  }

  internal myBinaryWriter(Stream stream)
    : base(stream)
  {
  }

  internal myBinaryWriter(Stream stream, Encoding encoding)
    : base(stream, encoding)
  {
  }

  public virtual void Write(List<byte> value)
  {
    this.Write(value.Count);
    foreach (byte num in value)
      this.Write(num);
  }

  public override void Write(byte[] value)
  {
    this.Write(value.Length);
    foreach (byte num in value)
      this.Write(num);
  }

  public void WriteBytesNoLength(byte[] value)
  {
    foreach (byte num in value)
      this.Write(num);
  }

  public void WriteFixed(FixedInt value) => this.Write(value.RawValue);

  public void Write(MyLocation value)
  {
    this.WriteFixed(value.x);
    this.WriteFixed(value.y);
  }

  public void Write(Vector2 value)
  {
    this.Write(value.x);
    this.Write(value.y);
  }

  public void Write(Vector3 value)
  {
    this.Write(value.x);
    this.Write(value.y);
    this.Write(value.z);
  }

  public void Write(Quaternion value)
  {
    this.Write(value.x);
    this.Write(value.y);
    this.Write(value.z);
    this.Write(value.w);
  }

  public void Write(Vector4 value)
  {
    this.Write(value.x);
    this.Write(value.y);
    this.Write(value.z);
    this.Write(value.w);
  }

  public void Write(Color32 value)
  {
    if (value.a == (byte) 0)
    {
      this.Write(value.a);
    }
    else
    {
      this.Write(value.r > (byte) 0 ? value.r : (byte) 1);
      this.Write(value.g);
      this.Write(value.b);
    }
  }

  public void WriteNoAlpha(Color32 value)
  {
    this.Write(value.r);
    this.Write(value.g);
    this.Write(value.b);
  }

  public void Write(Color value)
  {
    this.Write(value.r);
    this.Write(value.g);
    this.Write(value.b);
    this.Write(value.a);
  }
}
