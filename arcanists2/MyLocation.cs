// Decompiled with JetBrains decompiler
// Type: MyLocation
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public struct MyLocation
{
  public static MyLocation zero = new MyLocation(0, 0);
  public static MyLocation one = new MyLocation(1, 1);
  public static MyLocation up = new MyLocation(0, 1);
  public static MyLocation down = new MyLocation(0, -1);
  public static MyLocation left = new MyLocation(-1, 0);
  public static MyLocation right = new MyLocation(1, 0);
  public FixedInt x;
  public FixedInt y;

  public FixedInt this[int index]
  {
    get
    {
      if (index == 0)
        return this.x;
      if (index == 1)
        return this.y;
      throw new IndexOutOfRangeException("Invalid MyLocation index!");
    }
    set
    {
      if (index != 0)
      {
        if (index != 1)
          throw new IndexOutOfRangeException("Invalid MyLocation index!");
        this.y = value;
      }
      else
        this.x = value;
    }
  }

  public FixedInt magnitude
  {
    get
    {
      FixedInt ret1;
      this.x.AbsoluteValue(out ret1);
      FixedInt ret2;
      if (ret1.RawValue == 0L)
      {
        if (this.y.RawValue == -1L || this.y.RawValue == 1L)
          ret2.RawValue = 1048576L;
      }
      else if (ret1.RawValue == 1L && this.y.RawValue == 0L)
        ret2.RawValue = 1048576L;
      this.x.Multiply(this.x.RawValue, out ret2);
      FixedInt ret3;
      this.y.Multiply(this.y.RawValue, out ret3);
      ret2.Add(ret3.RawValue, out ret2);
      Mathd.Sqrt(ret2.RawValue, out ret2);
      return ret2;
    }
  }

  public FixedInt Magnitude(out FixedInt ret)
  {
    FixedInt ret1;
    this.x.AbsoluteValue(out ret1);
    if (ret1.RawValue == 0L)
    {
      if (this.y.RawValue == -1L || this.y.RawValue == 1L)
        ret.RawValue = 1048576L;
    }
    else if (ret1.RawValue == 1L && this.y.RawValue == 0L)
      ret.RawValue = 1048576L;
    this.x.Multiply(this.x.RawValue, out ret);
    FixedInt ret2;
    this.y.Multiply(this.y.RawValue, out ret2);
    ret.Add(ret2.RawValue, out ret);
    Mathd.Sqrt(ret.RawValue, out ret);
    return ret;
  }

  public FixedInt SqrMagnitude(out FixedInt ret)
  {
    this.x.Multiply(this.x.RawValue, out ret);
    FixedInt ret1;
    this.y.Multiply(this.y.RawValue, out ret1);
    ret.Add(ret1.RawValue, out ret);
    return ret;
  }

  public MyLocation(FixedInt x, FixedInt y)
  {
    this.x = x;
    this.y = y;
  }

  public MyLocation(int xInt, int yInt)
  {
    this.x.RawValue = (long) xInt << 20;
    this.y.RawValue = (long) yInt << 20;
  }

  public static explicit operator MyLocation(Point v)
  {
    return new MyLocation(FixedInt.Create(v.x), FixedInt.Create(v.y));
  }

  public override bool Equals(object o) => base.Equals(o);

  public static bool operator !=(MyLocation x, MyLocation b)
  {
    return x.x.RawValue != b.x.RawValue || x.y.RawValue != b.y.RawValue;
  }

  public static bool operator ==(MyLocation x, MyLocation b)
  {
    return x.x.RawValue == b.x.RawValue && x.y.RawValue == b.y.RawValue;
  }

  public static explicit operator Point(MyLocation v) => new Point(v.x.ToInt(), v.y.ToInt());

  public static explicit operator MyLocation(Vector3 v)
  {
    return new MyLocation(FixedInt.Create(v.x), FixedInt.Create(v.y));
  }

  public static explicit operator Vector3(MyLocation v)
  {
    return new Vector3(v.x.ToFloat(), v.y.ToFloat());
  }

  public void Add(ref MyLocation Other, out MyLocation ret)
  {
    this.x.Add(Other.x.RawValue, out ret.x);
    this.y.Add(Other.y.RawValue, out ret.y);
  }

  public void Subtract(ref MyLocation Other, out MyLocation ret)
  {
    this.x.Subtract(Other.x.RawValue, out ret.x);
    this.y.Subtract(Other.y.RawValue, out ret.y);
  }

  public void Multiply(long OtherRawValue, out MyLocation ret)
  {
    this.x.Multiply(OtherRawValue, out ret.x);
    this.y.Multiply(OtherRawValue, out ret.y);
  }

  public void Multiply(int OtherValue, out MyLocation ret)
  {
    this.x.Multiply(OtherValue, out ret.x);
    this.y.Multiply(OtherValue, out ret.y);
  }

  public void Divide(long OtherRawValue, out MyLocation ret)
  {
    this.x.Divide(OtherRawValue, out ret.x);
    this.y.Divide(OtherRawValue, out ret.y);
  }

  public void Divide(int OtherValue, out MyLocation ret)
  {
    this.x.Divide(OtherValue, out ret.x);
    this.y.Divide(OtherValue, out ret.y);
  }

  public void Invert()
  {
    this.x.RawValue = -this.x.RawValue;
    this.y.RawValue = -this.y.RawValue;
  }

  public static MyLocation operator +(MyLocation a, MyLocation b)
  {
    a.Add(ref b, out a);
    return a;
  }

  public static MyLocation operator -(MyLocation a, MyLocation b)
  {
    a.Subtract(ref b, out a);
    return a;
  }

  public static MyLocation operator -(MyLocation a) => new MyLocation(-a.x, -a.y);

  public static MyLocation operator *(MyLocation a, long d)
  {
    a.x.RawValue = a.x.RawValue * d >> 20;
    a.y.RawValue = a.y.RawValue * d >> 20;
    return a;
  }

  public static MyLocation operator *(MyLocation a, FixedInt d)
  {
    a.Multiply(d.RawValue, out a);
    return a;
  }

  public static MyLocation operator *(MyLocation a, int d)
  {
    a.Multiply(d, out a);
    return a;
  }

  public static MyLocation operator /(MyLocation a, FixedInt d)
  {
    a.Divide(d.RawValue, out a);
    return a;
  }

  public static MyLocation operator /(MyLocation a, int d)
  {
    a.Divide(d, out a);
    return a;
  }

  public MyLocation normalized
  {
    get
    {
      MyLocation ret1 = this;
      if (ret1.x.RawValue == 0L && ret1.y.RawValue == 0L)
        return ret1;
      FixedInt ret2;
      ret1.x.AbsoluteValue(out ret2);
      FixedInt ret3;
      ret1.y.AbsoluteValue(out ret3);
      if (ret2.RawValue == 0L)
      {
        if (ret3.RawValue == 1048576L)
          return ret1;
      }
      else if (ret3.RawValue == 0L && ret2.RawValue == 1048576L)
        return ret1;
      if (ret2.RawValue > ret3.RawValue)
        ret1.Divide(ret2.RawValue + ret3.RawValue / 2L, out ret1);
      else
        ret1.Divide(ret3.RawValue + ret2.RawValue / 2L, out ret1);
      FixedInt ret4;
      ret1.Magnitude(out ret4);
      if (ret4.RawValue <= 0L || ret4.RawValue == 1048576L)
        return ret1;
      ret1.Divide(ret4.RawValue, out ret1);
      return ret1;
    }
  }

  public void Normalize()
  {
    if (this.x.RawValue == 0L && this.y.RawValue == 0L)
      return;
    FixedInt ret1;
    this.x.AbsoluteValue(out ret1);
    FixedInt ret2;
    this.y.AbsoluteValue(out ret2);
    if (ret1.RawValue == 0L)
    {
      if (ret2.RawValue == 1048576L)
        return;
    }
    else if (ret2.RawValue == 0L && ret1.RawValue == 1048576L)
      return;
    if (ret1.RawValue > ret2.RawValue)
      this.Divide(ret1.RawValue + ret2.RawValue / 2L, out this);
    else
      this.Divide(ret2.RawValue + ret1.RawValue / 2L, out this);
    FixedInt ret3;
    this.Magnitude(out ret3);
    if (ret3.RawValue <= 0L || ret3.RawValue == 1048576L)
      return;
    this.Divide(ret3.RawValue, out this);
  }

  public Vector2 ToSinglePrecision() => new Vector2(this.x.ToFloat(), this.y.ToFloat());

  public override string ToString()
  {
    return "(" + this.x.ToDouble().ToString() + ", " + this.y.ToDouble().ToString() + ")";
  }

  public string ToIntString()
  {
    return "(" + this.x.ToInt().ToString() + ", " + this.y.ToInt().ToString() + ")";
  }

  public string ToString(string format)
  {
    return "(" + this.x.ToDouble().ToString(format) + ", " + this.y.ToDouble().ToString(format) + ")";
  }

  public override int GetHashCode() => this.x.GetHashCode() ^ this.y.GetHashCode() << 2;

  public bool Equals(ref MyLocation other)
  {
    return this.x.RawValue == other.x.RawValue && this.y.RawValue == other.y.RawValue;
  }

  public static void Dot(MyLocation lhs, MyLocation rhs, out FixedInt ret)
  {
    lhs.x.Multiply(rhs.x.RawValue, out ret);
    FixedInt ret1;
    lhs.y.Multiply(rhs.y.RawValue, out ret1);
    ret.Add(ret1.RawValue, out ret);
  }

  public static void Cross(ref MyLocation U, ref MyLocation B, out FixedInt ret)
  {
    U.x.Multiply(B.y.RawValue, out ret);
    FixedInt ret1;
    U.y.Multiply(B.x.RawValue, out ret1);
    ret.Subtract(ret1.RawValue, out ret);
  }

  public static int Distance(MyLocation a, MyLocation b)
  {
    FixedInt ret;
    Mathd.Sqrt(((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)).RawValue, out ret);
    return ret.ToInt();
  }

  public static int DistanceDirty(MyLocation a, MyLocation b)
  {
    int a1 = (int) Mathd.Abs(a.x - b.x);
    int b1 = (int) Mathd.Abs(a.y - b.y);
    int num1 = (int) Mathd.Min((FixedInt) a1, (FixedInt) b1);
    int num2 = (int) Mathd.Max((FixedInt) a1, (FixedInt) b1);
    return num2 != 0 ? num1 * num1 / num2 + num2 : 0;
  }

  public static int FastDistance(MyLocation a, MyLocation b)
  {
    return Mathf.Abs((int) a.x - (int) b.x) + Mathf.Abs((int) a.y - (int) b.y);
  }

  public static void Reflect(MyLocation vector, ref MyLocation normal, out MyLocation ret)
  {
    FixedInt ret1;
    MyLocation.Dot(vector, normal, out ret1);
    normal.Multiply(ret1.RawValue, out ret);
    ret.Multiply(2, out ret);
    vector.Subtract(ref ret, out ret);
  }

  public void Rotate(long CosRaw, long SinRaw, out MyLocation ret)
  {
    FixedInt ret1;
    this.x.Multiply(CosRaw, out ret1);
    FixedInt ret2;
    this.y.Multiply(-SinRaw, out ret2);
    ret1.Add(ret2.RawValue, out ret1);
    FixedInt ret3;
    this.x.Multiply(SinRaw, out ret3);
    this.y.Multiply(CosRaw, out ret2);
    ret3.Add(ret2.RawValue, out ret3);
    ret.x = -ret3;
    ret.y = ret1;
  }

  public void RotateTowards(ref MyLocation target, FixedInt amount, out MyLocation ret)
  {
    ret = this;
    MyLocation localright = this.localright;
    FixedInt ret1;
    MyLocation.Dot(localright, target, out ret1);
    int OtherValue = ret1.RawValue != 0L ? (ret1.RawValue <= 0L ? -1 : 1) : 0;
    if (OtherValue.Equals(0))
      return;
    MyLocation ret2;
    localright.Multiply(OtherValue, out ret2);
    ret2.Multiply(amount.RawValue, out ret2);
    MyLocation ret3;
    this.Add(ref ret2, out ret3);
    FixedInt ret4;
    MyLocation.Dot(ret3.localright, target, out ret4);
    ret4.Sign();
    if (ret4.RawValue == (long) OtherValue || ret4.RawValue > 0L == OtherValue > 0)
    {
      ret3.Normalize();
      ret = ret3;
    }
    else
      ret = target;
  }

  public MyLocation localright => new MyLocation(this.y, -this.x);
}
