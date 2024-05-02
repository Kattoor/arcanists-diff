// Decompiled with JetBrains decompiler
// Type: FixedInt
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

#nullable disable
public struct FixedInt
{
  public static FixedInt OneF = FixedInt.Create(1);
  public static FixedInt TwoF = FixedInt.Create(2);
  public static FixedInt ZeroF = FixedInt.Create(0);
  public static FixedInt HalfF = FixedInt.Create(524288L);
  public static FixedInt MaxValue = FixedInt.Create(int.MaxValue);
  public const int SHIFT_AMOUNT = 20;
  public const long MAX_VALUE = 8796093022207;
  public const long OneRaw = 1048576;
  public long RawValue;
  public static FixedInt ThreeSixtyBy1 = (FixedInt) 2911L;
  public static FixedInt Point_Two_Eight = (FixedInt) 293601L;
  public static FixedInt Rad2Deg = (FixedInt) 60078979L;
  public static FixedInt Deg2Rad = (FixedInt) 18301L;
  public static FixedInt Pi = (FixedInt) 3294195L;
  public static FixedInt PiOver2 = (FixedInt) 1647097L;
  public static FixedInt PiTimes2 = (FixedInt) 6588391L;
  private const int LUT_SIZE = 205887;
  private static readonly FixedInt LutInterval = (FixedInt) 205886 / FixedInt.PiOver2;
  private const int FRACTIONAL_PLACES = 20;

  public FixedInt(long value) => this.RawValue = value;

  public static FixedInt Create(int StartingValue)
  {
    FixedInt fixedInt;
    fixedInt.RawValue = (long) StartingValue;
    fixedInt.RawValue <<= 20;
    return fixedInt;
  }

  public static FixedInt Create(long StartingRawValue)
  {
    FixedInt fixedInt;
    fixedInt.RawValue = StartingRawValue;
    return fixedInt;
  }

  public static FixedInt Create(float FloatValue)
  {
    return (FixedInt) 0 with
    {
      RawValue = !float.IsNaN(FloatValue) ? (long) ((sfloat) FloatValue * (sfloat) 1048576) : 1L
    };
  }

  public static FixedInt Create(Decimal DoubleValue)
  {
    FixedInt fixedInt;
    fixedInt.RawValue = (long) (DoubleValue * 1048576M);
    return fixedInt;
  }

  public static FixedInt Create(double DoubleValue)
  {
    FixedInt fixedInt;
    fixedInt.RawValue = (long) ((Decimal) DoubleValue * 1048576M);
    return fixedInt;
  }

  public static FixedInt FromParts(long PreDecimal, long PostDecimal)
  {
    FixedInt fixedInt = FixedInt.Create(PreDecimal);
    if (PostDecimal != 0L)
      fixedInt.RawValue += (FixedInt.Create((double) PostDecimal) / 1000).RawValue;
    return fixedInt;
  }

  public void Multiply(long OtherRawValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue * OtherRawValue >> 20;
  }

  public void Multiply(int OtherValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue * (long) OtherValue;
  }

  public static FixedInt operator *(FixedInt one, FixedInt other)
  {
    one.RawValue = one.RawValue * other.RawValue >> 20;
    return one;
  }

  public static FixedInt operator *(FixedInt one, long other)
  {
    one.RawValue = one.RawValue * other >> 20;
    return one;
  }

  public static FixedInt operator *(FixedInt one, int multi)
  {
    one.RawValue *= (long) multi;
    return one;
  }

  public bool BiggerThenOrEqualToZero() => this.RawValue >= 0L;

  public bool SmallerThenOrEqualToOne() => this.RawValue <= 1048576L;

  public void Divide(long OtherRawValue, out FixedInt ret)
  {
    if (OtherRawValue == 0L)
      ret.RawValue = this.RawValue;
    else
      ret.RawValue = (this.RawValue << 20) / OtherRawValue;
  }

  public void Divide(int OtherValue, out FixedInt ret)
  {
    if (OtherValue == 0)
      ret.RawValue = this.RawValue;
    else
      ret.RawValue = this.RawValue / (long) OtherValue;
  }

  public static FixedInt operator /(FixedInt one, FixedInt other)
  {
    if (other.RawValue == 0L)
      return one;
    one.RawValue = (one.RawValue << 20) / other.RawValue;
    return one;
  }

  public static FixedInt operator /(FixedInt one, int divisor)
  {
    if (divisor == 0)
      return one;
    one.RawValue /= (long) divisor;
    return one;
  }

  public void Modulo(long OtherRawValue, out FixedInt ret)
  {
    if (OtherRawValue == 0L)
      ret.RawValue = 0L;
    else
      ret.RawValue = this.RawValue % OtherRawValue;
  }

  public void Modulo(int OtherValue, out FixedInt ret)
  {
    if (OtherValue == 0)
      ret.RawValue = 0L;
    else
      ret.RawValue = this.RawValue % ((long) OtherValue << 20);
  }

  public static FixedInt operator %(FixedInt one, FixedInt other)
  {
    one.Modulo(other.RawValue, out one);
    return one;
  }

  public void Add(long OtherRawValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue + OtherRawValue;
  }

  public void Add(int OtherValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue + ((long) OtherValue << 20);
  }

  public static FixedInt operator ++(FixedInt a) => a + 1;

  public static FixedInt operator --(FixedInt a) => a - 1;

  public static FixedInt operator +(FixedInt one, FixedInt other)
  {
    one.RawValue += other.RawValue;
    return one;
  }

  public static FixedInt operator +(FixedInt one, long other)
  {
    one.RawValue += other;
    return one;
  }

  public static bool operator ==(FixedInt one, int other) => one.RawValue == (long) other << 20;

  public override bool Equals(object o) => base.Equals(o);

  public static bool operator !=(FixedInt one, int other) => one.RawValue != (long) other << 20;

  public static FixedInt operator +(FixedInt one, int other)
  {
    one.RawValue += (long) other << 20;
    return one;
  }

  public static bool operator <(FixedInt one, int other) => one.RawValue < (long) other << 20;

  public static bool operator >(FixedInt one, int other) => one.RawValue > (long) other << 20;

  public static bool operator >=(FixedInt one, int other) => one.RawValue >= (long) other << 20;

  public static bool operator <=(FixedInt one, int other) => one.RawValue <= (long) other << 20;

  public static bool operator >=(FixedInt one, FixedInt other) => one.RawValue >= other.RawValue;

  public static bool operator <=(FixedInt one, FixedInt other) => one.RawValue <= other.RawValue;

  public static bool operator <(FixedInt one, FixedInt other) => one.RawValue < other.RawValue;

  public static bool operator >(FixedInt one, long other) => one.RawValue > other;

  public static bool operator <(FixedInt one, long other) => one.RawValue < other;

  public static bool operator >(FixedInt one, FixedInt other) => one.RawValue > other.RawValue;

  public void Subtract(long OtherRawValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue - OtherRawValue;
  }

  public void Subtract(int OtherValue, out FixedInt ret)
  {
    ret.RawValue = this.RawValue - ((long) OtherValue << 20);
  }

  public static FixedInt operator -(FixedInt one, FixedInt other)
  {
    one.RawValue -= other.RawValue;
    return one;
  }

  public static FixedInt operator -(FixedInt one, int other)
  {
    one.RawValue -= (long) other << 20;
    return one;
  }

  public bool Equals(long OtherRawValue) => this.RawValue == OtherRawValue;

  public static bool operator ==(FixedInt one, FixedInt other) => one.Equals(other.RawValue);

  public static bool operator !=(FixedInt one, FixedInt other) => !one.Equals(other.RawValue);

  public bool MoreEquals(long OtherRawValue) => this.RawValue >= OtherRawValue;

  public bool LessEquals(long OtherRawValue) => this.RawValue <= OtherRawValue;

  public bool More(long OtherRawValue) => this.RawValue > OtherRawValue;

  public bool Less(long OtherRawValue) => this.RawValue < OtherRawValue;

  public static bool InvalidAngle(FixedInt x) => x.RawValue == 1L;

  public static FixedInt Angle(MyLocation a, MyLocation b)
  {
    FixedInt x1 = FixedInt.Atan2(a.y, a.x);
    FixedInt x2 = FixedInt.Atan2(b.y, b.x);
    return FixedInt.Abs(FixedInt.Abs(x1) - FixedInt.Abs(x2));
  }

  public static FixedInt Atan(FixedInt z)
  {
    if (z.RawValue == 0L)
      return FixedInt.ZeroF;
    bool flag1 = z.RawValue < 0L;
    if (flag1)
      z = -z;
    FixedInt fixedInt1 = (FixedInt) 2;
    FixedInt fixedInt2 = (FixedInt) 3;
    bool flag2 = z > FixedInt.OneF;
    if (flag2)
      z = FixedInt.OneF / z;
    FixedInt oneF1 = FixedInt.OneF;
    FixedInt oneF2 = FixedInt.OneF;
    FixedInt fixedInt3 = z * z;
    FixedInt fixedInt4 = fixedInt3 * fixedInt1;
    FixedInt fixedInt5 = fixedInt3 + FixedInt.OneF;
    FixedInt fixedInt6 = fixedInt5 * fixedInt1;
    FixedInt fixedInt7 = fixedInt4;
    FixedInt fixedInt8 = fixedInt5 * fixedInt2;
    for (int index = 2; index < 30; ++index)
    {
      oneF2 *= fixedInt7 / fixedInt8;
      oneF1 += oneF2;
      fixedInt7 += fixedInt4;
      fixedInt8 += fixedInt6;
      if (oneF2.RawValue == 0L)
        break;
    }
    FixedInt fixedInt9 = oneF1 * z / fixedInt5;
    if (flag2)
      fixedInt9 = FixedInt.PiOver2 - fixedInt9;
    if (flag1)
      fixedInt9 = -fixedInt9;
    return fixedInt9;
  }

  public static FixedInt Atan2(FixedInt y, FixedInt x)
  {
    long rawValue1 = y.RawValue;
    long rawValue2 = x.RawValue;
    if (rawValue2 == 0L)
    {
      if (rawValue1 > 0L)
        return FixedInt.PiOver2;
      return rawValue1 == 0L ? FixedInt.ZeroF : -FixedInt.PiOver2;
    }
    FixedInt x1 = y / x;
    if (FixedInt.Abs(x1) > 1000)
      return !(y < FixedInt.ZeroF) ? FixedInt.PiOver2 : -FixedInt.PiOver2;
    FixedInt fixedInt;
    if (FixedInt.Abs(x1) < FixedInt.OneF)
    {
      fixedInt = x1 / (FixedInt.OneF + (FixedInt) 293601L * x1 * x1);
      if (rawValue2 < 0L)
        return rawValue1 < 0L ? fixedInt - FixedInt.Pi : fixedInt + FixedInt.Pi;
    }
    else
    {
      fixedInt = FixedInt.PiOver2 - x1 / (x1 * x1 + 293601L);
      if (rawValue1 < 0L)
        return fixedInt - FixedInt.Pi;
    }
    return fixedInt;
  }

  public static FixedInt TempFloat(float f) => FixedInt.Create(f);

  public static float Float(FixedInt f) => f.ToFloat();

  public static void GenerateSinLut()
  {
    long[] numArray = new long[205887];
    using (FileStream fileStream = new FileStream("SinLut.cs", FileMode.Create))
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream))
      {
        int num = 0;
        streamWriter.WriteLine("public static class SinLut");
        streamWriter.WriteLine("{");
        streamWriter.Write("public const long[] lut = new long[]{");
        for (int index = 0; index < 205887; ++index)
        {
          double a = (double) index * Math.PI * 0.5 / 205886.0;
          numArray[index] = FixedInt.Create(Math.Sin(a)).RawValue;
          streamWriter.Write(numArray[index]);
          streamWriter.Write(", ");
          ++num;
          if (num == 100)
          {
            num = 0;
            streamWriter.WriteLine("");
          }
        }
        streamWriter.WriteLine("");
        streamWriter.WriteLine("}");
        streamWriter.WriteLine("}");
      }
    }
  }

  public static explicit operator FixedInt(Decimal v) => FixedInt.Create(v);

  public static explicit operator int(FixedInt v) => v.ToInt();

  public static explicit operator sfloat(FixedInt v) => (sfloat) v.ToFloat();

  public static explicit operator FixedInt(sfloat v) => (FixedInt) (float) v;

  public static implicit operator FixedInt(int v) => FixedInt.Create(v);

  public static implicit operator FixedInt(long v) => FixedInt.Create(v);

  public static implicit operator FixedInt(float v) => FixedInt.Create(v);

  public bool AbsoluteValueMoreThan(long OtherRawValue)
  {
    return this.RawValue > OtherRawValue || this.RawValue < -OtherRawValue;
  }

  public void AbsoluteValue(out FixedInt ret)
  {
    if (this.RawValue < 0L)
      ret.RawValue = -this.RawValue;
    else
      ret.RawValue = this.RawValue;
  }

  public void Sign()
  {
    if (this.RawValue == 0L)
      return;
    if (this.RawValue > 0L)
      this.RawValue = FixedInt.OneF.RawValue;
    else
      this.RawValue = -FixedInt.OneF.RawValue;
  }

  public void Inverse(out FixedInt ret) => ret.RawValue = this.RawValue * -1L;

  public static FixedInt operator -(FixedInt src)
  {
    src.Inverse(out src);
    return src;
  }

  public static FixedInt Abs(FixedInt x)
  {
    if (x > 0)
      return x;
    x.RawValue = -x.RawValue;
    return x;
  }

  public float ToFloat() => (float) this.RawValue / 1048576f;

  public int ToInt() => (int) (this.RawValue >> 20);

  public double ToDouble() => (double) this.RawValue / 1048576.0;

  public short ToRoundedShort() => (short) (this.RawValue >> 20);

  public override int GetHashCode() => this.RawValue.GetHashCode();

  public override string ToString() => this.ToDouble().ToString();

  public static FixedInt FastMul(FixedInt x, FixedInt y)
  {
    long rawValue1 = x.RawValue;
    long rawValue2 = y.RawValue;
    ulong num1 = (ulong) rawValue1 & (ulong) uint.MaxValue;
    long num2 = rawValue1 >> 20;
    ulong num3 = (ulong) rawValue2 & (ulong) uint.MaxValue;
    long num4 = rawValue2 >> 20;
    return FixedInt.Create((long) (num1 * num3 >> 20) + (long) num1 * num4 + num2 * (long) num3 + (num2 * num4 << 20));
  }

  public static int Sign(FixedInt x)
  {
    if (x.RawValue == 0L)
      return 0;
    return x.RawValue > 0L ? 1 : -1;
  }

  public static FixedInt Sin(FixedInt x)
  {
    bool flipHorizontal;
    bool flipVertical;
    uint num1 = (uint) (FixedInt.ClampSinValue(x.RawValue, out flipHorizontal, out flipVertical) >> 3);
    if (num1 >= 205887U)
      num1 = 205886U;
    long num2 = SinLut.lut[flipHorizontal ? SinLut.lut.Length - 1 - (int) num1 : (int) num1];
    return FixedInt.Create(flipVertical ? -num2 : num2);
  }

  private static long ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
  {
    long num1 = 1768559400000000;
    long num2 = angle;
    for (int index = 0; index < 17; ++index)
      num2 %= num1 >> index;
    if (angle < 0L)
      num2 += FixedInt.PiTimes2.RawValue;
    flipVertical = num2 >= FixedInt.Pi.RawValue;
    long num3 = num2;
    while (num3 >= FixedInt.Pi.RawValue)
      num3 -= FixedInt.Pi.RawValue;
    flipHorizontal = num3 >= FixedInt.PiOver2.RawValue;
    long num4 = num3;
    if (num4 >= FixedInt.PiOver2.RawValue)
      num4 -= FixedInt.PiOver2.RawValue;
    return num4;
  }

  public static FixedInt Cos(FixedInt x)
  {
    long rawValue = x.RawValue;
    return FixedInt.Sin(x + (rawValue > 0L ? -FixedInt.Pi - FixedInt.PiOver2 : FixedInt.PiOver2));
  }

  public static FixedInt Max(FixedInt a, FixedInt b) => !(a > b) ? b : a;

  public static FixedInt Min(FixedInt a, FixedInt b) => !(a < b) ? b : a;

  public static FixedInt Clamp(FixedInt x, FixedInt a, FixedInt b)
  {
    if (x < a)
      return a;
    return x > b ? b : x;
  }

  public static FixedInt Round(FixedInt a)
  {
    if (a > 0)
    {
      int num = (int) a;
      return (FixedInt) (a - num > 524288L ? (int) (a + 1) : (int) a);
    }
    if (a == 0)
      return a;
    int num1 = (int) a;
    return (FixedInt) (a + num1 < -524288L ? (int) (a - 1) : (int) a);
  }
}
