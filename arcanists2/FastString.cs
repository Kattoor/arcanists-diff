// Decompiled with JetBrains decompiler
// Type: FastString
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

#nullable disable
public class FastString
{
  public StringBuilder stringBuilder = new StringBuilder();

  public static string HowTo()
  {
    FastString fastString1 = new FastString();
    FastString fastString2 = fastString1 + "It makes switching from " + "string concatenation much easier";
    return fastString1.ToString();
  }

  public int Length
  {
    get => this.stringBuilder.Length;
    set => this.stringBuilder.Length = value;
  }

  public FastString Append(string c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(int c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(char c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(float c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(Decimal c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(object c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(bool c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(ushort c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(long c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(ulong c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(uint c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(byte c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(short c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public FastString Append(double c)
  {
    this.stringBuilder.Append(c);
    return this;
  }

  public static FastString operator +(FastString a, string c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, int c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, char c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, float c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, Decimal c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, object c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, bool c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, ushort c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, long c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, ulong c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, uint c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, byte c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, short c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public static FastString operator +(FastString a, double c)
  {
    a.stringBuilder.Append(c);
    return a;
  }

  public override string ToString() => this.stringBuilder.ToString();

  public char this[int x]
  {
    get => this.stringBuilder[x];
    set => this.stringBuilder[x] = value;
  }
}
