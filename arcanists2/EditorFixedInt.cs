﻿

using System;

#nullable disable
[Serializable]
public class EditorFixedInt
{
  public long x;

  public override bool Equals(object obj) => this.x.Equals(obj);

  public override int GetHashCode() => this.x.GetHashCode();

  public static bool operator ==(EditorFixedInt v, EditorFixedInt x) => x.x == v.x;

  public static bool operator !=(EditorFixedInt v, EditorFixedInt x) => x.x != v.x;

  public static bool operator ==(EditorFixedInt v, int x) => (long) x == v.x >> 20;

  public static bool operator !=(EditorFixedInt v, int x) => (long) x != v.x >> 20;

  public static implicit operator FixedInt(EditorFixedInt v) => FixedInt.Create(v.x);

  public static implicit operator EditorFixedInt(int v)
  {
    return new EditorFixedInt() { x = (long) (v << 20) };
  }

  public static implicit operator EditorFixedInt(long v)
  {
    return new EditorFixedInt() { x = v };
  }

  public static implicit operator EditorFixedInt(FixedInt v)
  {
    return new EditorFixedInt() { x = v.RawValue };
  }

  public static implicit operator EditorFixedInt(float v)
  {
    return new EditorFixedInt()
    {
      x = (long) ((Decimal) v * 1048576M)
    };
  }

  public static explicit operator int(EditorFixedInt v) => (int) (v.x >> 20);

  public static bool operator >(EditorFixedInt v, int x) => (int) (v.x >> 20) > x;

  public static bool operator <(EditorFixedInt v, int x) => (int) (v.x >> 20) < x;

  public static bool operator >(EditorFixedInt v, EditorFixedInt x) => v.x > x.x;

  public static bool operator <(EditorFixedInt v, EditorFixedInt x) => v.x < x.x;
}
