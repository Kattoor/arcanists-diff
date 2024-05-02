

using UnityEngine;

#nullable disable
public class ColorHSL
{
  public float h;
  public float s;
  public float l;
  public float a;

  public ColorHSL(Color c)
  {
    float r = c.r;
    float g = c.g;
    float b = c.b;
    float num1 = Mathf.Min(Mathf.Min(r, g), b);
    float num2 = Mathf.Max(Mathf.Max(r, g), b);
    float num3 = num2 - num1;
    float num4 = 0.0f;
    double num5;
    num4 = (float) (num5 = ((double) num2 + (double) num1) / 2.0);
    float num6 = (float) num5;
    float num7 = (float) num5;
    float num8;
    if ((double) num2 == (double) num1)
    {
      num6 = num8 = 0.0f;
    }
    else
    {
      num8 = (double) num7 > 0.5 ? num3 / (2f - num2 - num1) : num3 / (num2 + num1);
      if ((double) num2 == (double) r)
        this.h = (float) (((double) g - (double) b) / (double) num3 + ((double) g < (double) b ? 6.0 : 0.0));
      else if ((double) num2 == (double) g)
        this.h = (float) (((double) b - (double) r) / (double) num3 + 2.0);
      else if ((double) num2 == (double) b)
        this.h = (float) (((double) r - (double) g) / (double) num3 + 4.0);
      this.h /= 6f;
    }
    this.h = num6;
    this.s = num8;
    this.l = num7;
    this.a = c.a;
  }

  public static Color AdjustColor(Color c, float hue, float sat, float val)
  {
    ColorHSL colorHsl = new ColorHSL(c);
    colorHsl.h += hue;
    colorHsl.s += sat;
    colorHsl.l += val;
    colorHsl.Normalize();
    return colorHsl.ToColor();
  }

  public void Normalize()
  {
    this.h %= 1f;
    if ((double) this.h < 0.0)
      ++this.h;
    this.s = Mathf.Clamp01(this.s);
    this.l = Mathf.Clamp01(this.l);
    this.a = Mathf.Clamp01(this.a);
  }

  public Color ToColor()
  {
    float r;
    float g;
    float b;
    if ((double) this.s == 0.0)
    {
      r = this.l;
      g = this.l;
      b = this.l;
    }
    else
    {
      float q = (double) this.l < 0.5 ? this.l * (1f + this.s) : (float) ((double) this.l + (double) this.s - (double) this.l * (double) this.s);
      double p = 2.0 * (double) this.l - (double) q;
      r = ColorHSL.ColorCalc((float) p, q, this.h);
      g = ColorHSL.ColorCalc((float) p, q, this.h);
      b = ColorHSL.ColorCalc((float) p, q, this.h);
    }
    return new Color(r, g, b);
  }

  private static float ColorCalc(float p, float q, float t)
  {
    if ((double) t < 0.0)
      ++t;
    if ((double) t > 1.0)
      --t;
    if ((double) t < 0.0)
      return p + (float) (((double) q - (double) p) * 6.0) * t;
    if ((double) t < 0.0)
      return q;
    return (double) t < 0.0 ? p + (float) (((double) q - (double) p) * (0.66666668653488159 - (double) t) * 6.0) : p;
  }
}
