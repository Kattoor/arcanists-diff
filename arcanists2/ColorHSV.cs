// Decompiled with JetBrains decompiler
// Type: ColorHSV
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public struct ColorHSV : IEquatable<ColorHSV>
{
  public float h;
  public float s;
  public float v;
  public float a;

  public ColorHSV(float h, float s, float v)
  {
    this.h = h;
    this.s = s;
    this.v = v;
    this.a = 1f;
  }

  public ColorHSV(float h, float s, float v, float a)
  {
    this.h = h;
    this.s = s;
    this.v = v;
    this.a = a;
  }

  public ColorHSV(Color c)
  {
    float a1 = Mathf.Clamp01(c.r);
    float a2 = Mathf.Clamp01(c.g);
    float b = Mathf.Clamp01(c.b);
    float num1 = Mathf.Max(a1, Mathf.Max(a2, b));
    float num2 = Mathf.Min(a1, Mathf.Min(a2, b));
    float f = num1 - num2;
    this.h = (double) Mathf.Abs(f) >= 1.0 / 1000.0 ? ((double) a1 < (double) a2 || (double) a1 < (double) b ? ((double) a2 < (double) b ? (float) ((((double) a1 - (double) a2) / (double) f + 4.0) / 6.0) : (float) ((((double) b - (double) a1) / (double) f + 2.0) / 6.0)) : (float) (((double) a2 - (double) b) / (double) f % 6.0 / 6.0)) : 0.0f;
    this.s = (double) num1 > 0.0 ? f / num1 : 0.0f;
    this.v = num1;
    this.a = Mathf.Clamp01(c.a);
  }

  public float HueAngle => this.h * 360f;

  public ColorHSV normalized => ColorHSV.Normalize(this);

  public static Color LowerHigher(Color c, float val)
  {
    ColorHSV hsv = new ColorHSV(c);
    hsv.v += (double) hsv.v >= 0.5 ? -val : val;
    hsv.Normalize();
    return ColorHSV.ToColor(hsv);
  }

  public static Color AdjustColor(Color c, float hue, float sat, float val)
  {
    ColorHSV hsv = new ColorHSV(c);
    hsv.h += hue;
    hsv.s += sat;
    hsv.v += val;
    hsv.Normalize();
    return ColorHSV.ToColor(hsv);
  }

  public static Color AdjustColor(Color c, float hue)
  {
    ColorHSV hsv = new ColorHSV(c);
    hsv.h += hue;
    hsv.h %= 1f;
    if ((double) hsv.h < 0.0)
      ++hsv.h;
    return ColorHSV.ToColor(hsv);
  }

  public static Color AdjustColor(Color c, float hue, float sat)
  {
    ColorHSV hsv = new ColorHSV(c);
    hsv.h += hue;
    hsv.s += sat;
    hsv.h %= 1f;
    hsv.s = Mathf.Clamp01(hsv.s);
    if ((double) hsv.h < 0.0)
      ++hsv.h;
    return ColorHSV.ToColor(hsv);
  }

  public void AdjustHue(float v)
  {
    this.h += v;
    this.h %= 1f;
    if ((double) this.h >= 0.0)
      return;
    ++this.h;
  }

  public void AdjustSaturation(float v)
  {
    this.s += v;
    this.s = Mathf.Clamp01(this.s);
  }

  public void AdjustValue(float v)
  {
    this.v += v;
    this.v = Mathf.Clamp01(this.v);
  }

  public void Normalize()
  {
    this.h %= 1f;
    if ((double) this.h < 0.0)
      ++this.h;
    this.s = Mathf.Clamp01(this.s);
    this.v = Mathf.Clamp01(this.v);
    this.a = Mathf.Clamp01(this.a);
  }

  public override string ToString()
  {
    return string.Format("HSVA({0:0.000}, {1:0.000}, {2:0.000},{3:0.000})", (object) this.h, (object) this.s, (object) this.v, (object) this.a);
  }

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case ColorHSV colorHsv:
        return this == colorHsv;
      case Color color:
        return this == (ColorHSV) color;
      case Vector4 vector4:
        return this == (ColorHSV) vector4;
      default:
        return false;
    }
  }

  public bool Equals(ColorHSV b) => this == b;

  public override int GetHashCode()
  {
    return this.h.GetHashCode() ^ this.s.GetHashCode() << 2 ^ this.v.GetHashCode() >> 2 ^ this.a.GetHashCode() >> 1;
  }

  public static explicit operator ColorHSV(Color c) => new ColorHSV(c);

  public static explicit operator Color(ColorHSV hsv) => ColorHSV.ToColor(hsv);

  public static implicit operator ColorHSV(Vector4 c) => new ColorHSV(c.x, c.y, c.z, c.w);

  public static implicit operator Vector4(ColorHSV hsv) => new Vector4(hsv.h, hsv.s, hsv.v, hsv.a);

  public static ColorHSV operator +(ColorHSV a, ColorHSV b)
  {
    return new ColorHSV()
    {
      h = a.h + b.h,
      s = a.s + b.s,
      v = a.v + b.v,
      a = a.a + b.a
    };
  }

  public static ColorHSV operator -(ColorHSV a, ColorHSV b)
  {
    return new ColorHSV()
    {
      h = a.h - b.h,
      s = a.s - b.s,
      v = a.v - b.v,
      a = a.a - b.a
    };
  }

  public static ColorHSV operator *(ColorHSV a, ColorHSV b)
  {
    return new ColorHSV()
    {
      h = a.h * b.h,
      s = a.s * b.s,
      v = a.v * b.v,
      a = a.a * b.a
    };
  }

  public static ColorHSV operator *(ColorHSV a, float b)
  {
    return new ColorHSV()
    {
      h = a.h * b,
      s = a.s * b,
      v = a.v * b,
      a = a.a * b
    };
  }

  public static ColorHSV operator /(ColorHSV a, float b)
  {
    if (float.IsNaN(b) || (double) b == 0.0)
      return new ColorHSV();
    return new ColorHSV()
    {
      h = a.h / b,
      s = a.s / b,
      v = a.v / b,
      a = a.a / b
    };
  }

  public static bool operator ==(ColorHSV a, ColorHSV b)
  {
    return (double) a.h == (double) b.h && (double) a.s == (double) b.s && (double) a.v == (double) b.v && (double) a.a == (double) b.a;
  }

  public static bool operator !=(ColorHSV a, ColorHSV b)
  {
    return (double) a.h != (double) b.h || (double) a.s != (double) b.s || (double) a.v != (double) b.v || (double) a.a != (double) b.a;
  }

  public static ColorHSV Normalize(ColorHSV c)
  {
    c.Normalize();
    return c;
  }

  public static Color ToColor(ColorHSV hsv)
  {
    hsv.Normalize();
    float f = (float) ((double) hsv.h % 1.0 * 360.0 / 60.0);
    if ((double) f < 0.0)
      f += 60f;
    float num1 = hsv.v * hsv.s;
    float num2 = num1 * (1f - Mathf.Abs((float) ((double) f % 2.0 - 1.0)));
    float num3 = 0.0f;
    float num4 = 0.0f;
    float num5 = 0.0f;
    switch (Mathf.FloorToInt(f))
    {
      case 0:
        num3 = num1;
        num4 = num2;
        break;
      case 1:
        num3 = num2;
        num4 = num1;
        break;
      case 2:
        num4 = num1;
        num5 = num2;
        break;
      case 3:
        num4 = num2;
        num5 = num1;
        break;
      case 4:
        num3 = num2;
        num5 = num1;
        break;
      case 5:
        num3 = num1;
        num5 = num2;
        break;
    }
    float num6 = hsv.v - num1;
    return new Color(num3 + num6, num4 + num6, num5 + num6, hsv.a);
  }

  public static ColorHSV Lerp(ColorHSV start, ColorHSV end, float t)
  {
    start.h += (end.h - start.h) * t;
    start.s += (end.s - start.s) * t;
    start.v += (end.v - start.v) * t;
    start.a += (end.a - start.a) * t;
    return start;
  }

  public static ColorHSV Slerp(ColorHSV start, ColorHSV end, float t)
  {
    ColorHSV colorHsv = new ColorHSV();
    if ((double) start.v <= 0.0)
    {
      colorHsv.h = end.h;
      colorHsv.s = end.s;
    }
    else if ((double) end.v <= 0.0)
    {
      colorHsv.h = start.h;
      colorHsv.s = start.s;
    }
    else
    {
      if ((double) start.s <= 0.0)
        colorHsv.h = end.h;
      else if ((double) end.s <= 0.0)
      {
        colorHsv.h = start.h;
      }
      else
      {
        float num = (float) (((double) end.h - (double) start.h) % 1.0);
        colorHsv.h = start.h + num * t;
        colorHsv.h -= Mathf.Floor(colorHsv.h);
      }
      colorHsv.s = Mathf.Clamp01(start.s + (end.s - start.s) * t);
    }
    colorHsv.v = Mathf.Clamp01(start.v + (end.v - start.v) * t);
    colorHsv.a = Mathf.Clamp01(start.a + (end.a - start.a) * t);
    return colorHsv;
  }

  public static ColorHSV black => new ColorHSV(0.0f, 0.0f, 0.0f);

  public static ColorHSV white => new ColorHSV(0.0f, 0.0f, 1f);

  public static ColorHSV red => new ColorHSV(0.0f, 1f, 1f);

  public static ColorHSV green => new ColorHSV(0.333333343f, 1f, 1f);

  public static ColorHSV blue => new ColorHSV(0.6666667f, 1f, 1f);

  public static ColorHSV cyan => new ColorHSV(0.5f, 1f, 1f);

  public static ColorHSV magenta => new ColorHSV(-0.1666667f, 1f, 1f);

  public static ColorHSV gray => new ColorHSV(0.0f, 0.0f, 0.5f);

  public static ColorHSV grey => new ColorHSV(0.0f, 0.0f, 0.5f);
}
