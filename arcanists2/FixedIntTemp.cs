

using System;
using UnityS.Mathematics;

#nullable disable
public struct FixedIntTemp
{
  public static FixedIntTemp OneF = FixedIntTemp.Create(1);
  public static FixedIntTemp TwoF = FixedIntTemp.Create(2);
  public static FixedIntTemp ZeroF = FixedIntTemp.Create(0);
  public static FixedIntTemp HalfF = FixedIntTemp.Create(524288L);
  public static FixedIntTemp MaxValue = FixedIntTemp.Create(int.MaxValue);
  public sfloat RawValue;
  public static FixedIntTemp ThreeSixtyBy1 = (FixedIntTemp) 0.002777f;
  public static FixedIntTemp Point_Two_Eight = (FixedIntTemp) 0.28f;
  public static FixedIntTemp Rad2Deg = (FixedIntTemp) 57.29578f;
  public static FixedIntTemp Deg2Rad = (FixedIntTemp) 0.0174533f;
  public static FixedIntTemp Pi = (FixedIntTemp) 3.14159f;
  public static FixedIntTemp PiOver2 = (FixedIntTemp) 1.570795f;
  public static FixedIntTemp PiTimes2 = (FixedIntTemp) 6.28318f;
  private const int LUT_SIZE = 205887;
  private static readonly FixedIntTemp LutInterval = (FixedIntTemp) 205886 / FixedIntTemp.PiOver2;
  private const int FRACTIONAL_PLACES = 20;

  public static FixedIntTemp MAX_VALUE => FixedIntTemp.Create(sfloat.MaxValue);

  public FixedIntTemp(long value) => this.RawValue = (sfloat) ((float) value / 1048576f);

  public static FixedIntTemp Create(sfloat StartingValue)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = StartingValue;
    return fixedIntTemp;
  }

  public static FixedIntTemp Create(int StartingValue)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = (sfloat) StartingValue;
    return fixedIntTemp;
  }

  public static FixedIntTemp Create(long StartingRawValue)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = (sfloat) ((float) StartingRawValue / 1048576f);
    return fixedIntTemp;
  }

  public static FixedIntTemp Create(float FloatValue)
  {
    FixedIntTemp fixedIntTemp = (FixedIntTemp) 0;
    if (float.IsNaN(FloatValue))
      return fixedIntTemp;
    fixedIntTemp.RawValue = (sfloat) FloatValue;
    return fixedIntTemp;
  }

  public static FixedIntTemp Create(Decimal DoubleValue)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = (sfloat) (float) DoubleValue;
    return fixedIntTemp;
  }

  public static FixedIntTemp Create(double DoubleValue)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = (sfloat) (float) DoubleValue;
    return fixedIntTemp;
  }

  public void Multiply(long OtherRawValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue * (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public void Multiply(int OtherValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue * (sfloat) (float) OtherValue;
  }

  public static FixedIntTemp operator *(FixedIntTemp one, FixedIntTemp other)
  {
    one.RawValue *= other.RawValue;
    return one;
  }

  public static FixedIntTemp operator *(FixedIntTemp one, long other)
  {
    one.RawValue *= (sfloat) ((float) other / 1048576f);
    return one;
  }

  public static FixedIntTemp operator *(FixedIntTemp one, int multi)
  {
    one.RawValue *= (sfloat) (float) multi;
    return one;
  }

  public bool BiggerThenOrEqualToZero() => this.RawValue >= (sfloat) 0;

  public bool SmallerThenOrEqualToOne() => this.RawValue <= (sfloat) 1;

  public void Divide(long OtherRawValue, out FixedIntTemp ret)
  {
    if (OtherRawValue == 0L)
      ret.RawValue = this.RawValue;
    else
      ret.RawValue = this.RawValue / (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public void Divide(int OtherValue, out FixedIntTemp ret)
  {
    if (OtherValue == 0)
      ret.RawValue = this.RawValue;
    else
      ret.RawValue = this.RawValue / (sfloat) (float) OtherValue;
  }

  public static FixedIntTemp operator /(FixedIntTemp one, FixedIntTemp other)
  {
    if (other.RawValue == (sfloat) 0)
      return one;
    one.RawValue /= other.RawValue;
    return one;
  }

  public static FixedIntTemp operator /(FixedIntTemp one, int divisor)
  {
    if (divisor == 0)
      return one;
    one.RawValue /= (sfloat) (float) divisor;
    return one;
  }

  public void Modulo(long OtherRawValue, out FixedIntTemp ret)
  {
    if (OtherRawValue == 0L)
      ret.RawValue = (sfloat) 0;
    else
      ret.RawValue = this.RawValue % (sfloat) (float) (OtherRawValue >> 20);
  }

  public void Modulo(int OtherValue, out FixedIntTemp ret)
  {
    if (OtherValue == 0)
      ret.RawValue = (sfloat) 0;
    else
      ret.RawValue = this.RawValue % (sfloat) OtherValue;
  }

  public static FixedIntTemp operator %(FixedIntTemp one, FixedIntTemp other)
  {
    one.RawValue %= other.RawValue;
    return one;
  }

  public void Add(long OtherRawValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue + (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public void Add(int OtherValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue + (sfloat) OtherValue;
  }

  public static FixedIntTemp operator +(FixedIntTemp one, FixedIntTemp other)
  {
    one.RawValue += other.RawValue;
    return one;
  }

  public static FixedIntTemp operator +(FixedIntTemp one, long other)
  {
    one.RawValue += (sfloat) ((float) other / 1048576f);
    return one;
  }

  public static bool operator ==(FixedIntTemp one, int other) => one.RawValue == (sfloat) other;

  public override bool Equals(object o) => base.Equals(o);

  public static bool operator !=(FixedIntTemp one, int other) => one.RawValue != (sfloat) other;

  public static FixedIntTemp operator +(FixedIntTemp one, int other)
  {
    one.RawValue += (sfloat) other;
    return one;
  }

  public static bool operator <(FixedIntTemp one, int other) => one.RawValue < (sfloat) other;

  public static bool operator >(FixedIntTemp one, int other) => one.RawValue > (sfloat) other;

  public static bool operator >=(FixedIntTemp one, int other) => one.RawValue >= (sfloat) other;

  public static bool operator <=(FixedIntTemp one, int other) => one.RawValue <= (sfloat) other;

  public static bool operator >=(FixedIntTemp one, FixedIntTemp other)
  {
    return one.RawValue >= other.RawValue;
  }

  public static bool operator <=(FixedIntTemp one, FixedIntTemp other)
  {
    return one.RawValue <= other.RawValue;
  }

  public static bool operator <(FixedIntTemp one, FixedIntTemp other)
  {
    return one.RawValue < other.RawValue;
  }

  public static bool operator >(FixedIntTemp one, long other)
  {
    return one.RawValue > (sfloat) ((float) other / 1048576f);
  }

  public static bool operator <(FixedIntTemp one, long other)
  {
    return one.RawValue < (sfloat) ((float) other / 1048576f);
  }

  public static bool operator >(FixedIntTemp one, FixedIntTemp other)
  {
    return one.RawValue > other.RawValue;
  }

  public void Subtract(long OtherRawValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue - (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public void Subtract(int OtherValue, out FixedIntTemp ret)
  {
    ret.RawValue = this.RawValue - (sfloat) OtherValue;
  }

  public static FixedIntTemp operator -(FixedIntTemp one, FixedIntTemp other)
  {
    one.RawValue -= other.RawValue;
    return one;
  }

  public static FixedIntTemp operator -(FixedIntTemp one, int other)
  {
    one.RawValue -= (sfloat) other;
    return one;
  }

  public bool Equals(long OtherRawValue)
  {
    return this.RawValue == (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public static bool operator ==(FixedIntTemp one, FixedIntTemp other)
  {
    return one.Equals((object) other.RawValue);
  }

  public static bool operator !=(FixedIntTemp one, FixedIntTemp other)
  {
    return !one.Equals((object) other.RawValue);
  }

  public bool MoreEquals(long OtherRawValue)
  {
    return this.RawValue >= (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public bool LessEquals(long OtherRawValue)
  {
    return this.RawValue <= (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public bool More(long OtherRawValue)
  {
    return this.RawValue > (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public bool Less(long OtherRawValue)
  {
    return this.RawValue < (sfloat) ((float) OtherRawValue / 1048576f);
  }

  public static bool InvalidAngle(FixedIntTemp x) => x.RawValue.IsNaN();

  public static FixedIntTemp Angle(MyLocation a, MyLocation b)
  {
    sfloat x1 = math.atan2((sfloat) (float) a.y.RawValue, (sfloat) (float) a.x.RawValue);
    sfloat x2 = math.atan2((sfloat) (float) b.y.RawValue, (sfloat) (float) b.x.RawValue);
    FixedIntTemp x3;
    x3.RawValue = math.abs(x1) - math.abs(x2);
    return FixedIntTemp.Abs(x3);
  }

  public static FixedIntTemp Atan(FixedIntTemp z)
  {
    if (z.RawValue == (sfloat) 0)
      return FixedIntTemp.ZeroF;
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = math.atan(z.RawValue);
    return fixedIntTemp;
  }

  public static FixedIntTemp Atan2(FixedIntTemp y, FixedIntTemp x)
  {
    sfloat rawValue = y.RawValue;
    if (x.RawValue == (sfloat) 0)
    {
      if (rawValue > (sfloat) 0)
        return FixedIntTemp.PiOver2;
      return rawValue == (sfloat) 0 ? FixedIntTemp.ZeroF : -FixedIntTemp.PiOver2;
    }
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = math.atan2(y.RawValue, x.RawValue);
    return fixedIntTemp;
  }

  public static FixedIntTemp TempFloat(float f) => FixedIntTemp.Create(f);

  public static float Float(FixedIntTemp f) => f.ToFloat();

  public static explicit operator FixedIntTemp(Decimal v) => FixedIntTemp.Create(v);

  public static explicit operator int(FixedIntTemp v) => v.ToInt();

  public static implicit operator FixedIntTemp(int v) => FixedIntTemp.Create(v);

  public static implicit operator FixedIntTemp(long v) => FixedIntTemp.Create(v);

  public static implicit operator FixedIntTemp(float v) => FixedIntTemp.Create(v);

  public bool AbsoluteValueMoreThan(long OtherRawValue)
  {
    return !(this.RawValue <= (sfloat) (float) OtherRawValue) || this.RawValue < (sfloat) (float) -((double) OtherRawValue / 1048576.0);
  }

  public void AbsoluteValue(out FixedIntTemp ret)
  {
    if (this.RawValue < (sfloat) 0)
      ret.RawValue = -this.RawValue;
    else
      ret.RawValue = this.RawValue;
  }

  public void Sign()
  {
    if (this.RawValue == (sfloat) 0)
      return;
    if (this.RawValue > (sfloat) 0)
      this.RawValue = FixedIntTemp.OneF.RawValue;
    else
      this.RawValue = -FixedIntTemp.OneF.RawValue;
  }

  public void Inverse(out FixedIntTemp ret) => ret.RawValue = this.RawValue * (sfloat) -1;

  public static FixedIntTemp operator -(FixedIntTemp src)
  {
    src.Inverse(out src);
    return src;
  }

  public static FixedIntTemp Abs(FixedIntTemp x)
  {
    if (x > 0)
      return x;
    x.RawValue = -x.RawValue;
    return x;
  }

  public float ToFloat() => (float) this.RawValue;

  public int ToInt() => (int) this.RawValue;

  public double ToDouble() => (double) (float) this.RawValue;

  public short ToRoundedShort() => (short) (int) this.RawValue;

  public override int GetHashCode() => this.RawValue.GetHashCode();

  public override string ToString() => this.RawValue.ToString();

  public static FixedIntTemp FastMul(FixedIntTemp x, FixedIntTemp y) => x * y;

  public static int Sign(FixedIntTemp x)
  {
    if (x.RawValue == (sfloat) 0)
      return 0;
    return x.RawValue > (sfloat) 0 ? 1 : -1;
  }

  public static FixedIntTemp Sin(FixedIntTemp x)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = math.sin(x.RawValue);
    return fixedIntTemp;
  }

  public static FixedIntTemp Cos(FixedIntTemp x)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = math.cos(x.RawValue);
    return fixedIntTemp;
  }

  public static FixedIntTemp Max(FixedIntTemp a, FixedIntTemp b) => !(a > b) ? b : a;

  public static FixedIntTemp Min(FixedIntTemp a, FixedIntTemp b) => !(a < b) ? b : a;

  public static FixedIntTemp Clamp(FixedIntTemp x, FixedIntTemp a, FixedIntTemp b)
  {
    if (x < a)
      return a;
    return x > b ? b : x;
  }

  public static FixedIntTemp Round(FixedIntTemp a)
  {
    FixedIntTemp fixedIntTemp;
    fixedIntTemp.RawValue = math.round(a.RawValue);
    return fixedIntTemp;
  }
}
