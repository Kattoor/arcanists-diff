// Decompiled with JetBrains decompiler
// Type: Mathd
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class Mathd
{
  public static FixedInt PI = FixedInt.Create(3.1415M);
  private const int HalfShift = 10;

  public static bool IsNull(this MyLocation v) => v.x == -1000 && v.y == -1000;

  public static void Sqrt(long RawValue, out FixedInt ret)
  {
    if (RawValue > 0L)
    {
      long num = Mathd.IntSqrt(RawValue) << 10;
      ret.RawValue = num;
    }
    else
      ret.RawValue = RawValue;
  }

  public static FixedInt Sqrt(FixedInt ret)
  {
    if (ret.RawValue > 0L)
      ret.RawValue = Mathd.IntSqrt(ret.RawValue) << 10;
    return ret;
  }

  public static FixedInt Max(FixedInt a, FixedInt b) => a > b ? a : b;

  public static FixedInt Min(FixedInt a, FixedInt b) => a < b ? a : b;

  public static FixedInt Clamp(FixedInt x, FixedInt a, FixedInt b)
  {
    if (x < a)
      return a;
    return x > b ? b : x;
  }

  public static FixedInt Clamp01(FixedInt x)
  {
    if (x.RawValue < 0L)
      return (FixedInt) 0;
    return x > 1 ? FixedInt.OneF : x;
  }

  public static FixedInt LerpUnclamped(FixedInt v0, FixedInt v1, FixedInt t) => v0 + t * (v1 - v0);

  public static FixedInt Lerp(FixedInt v0, FixedInt v1, FixedInt t)
  {
    if (v0 > v1)
    {
      t = v1 + ((FixedInt) 1 - t) * (v0 - v1);
      return Mathd.Clamp(t, v1, v0);
    }
    t = v0 + t * (v1 - v0);
    return Mathd.Clamp(t, v0, v1);
  }

  public static FixedInt LerpPreciseUnclamped(FixedInt v0, FixedInt v1, FixedInt t)
  {
    return ((FixedInt) 1 - t) * v0 + t * v1;
  }

  public static FixedInt Abs(FixedInt x)
  {
    if (x > 0)
      return x;
    x.RawValue = -x.RawValue;
    return x;
  }

  public static long IntSqrt(long d)
  {
    if (d <= 0L)
      return d;
    long num1 = (d >> 1) + 1L;
    long num2;
    for (num2 = num1 + d / num1 >> 1; num2 < num1; num2 = num1 + d / num1 >> 1)
      num1 = num2;
    return num2;
  }
}
