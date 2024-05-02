// Decompiled with JetBrains decompiler
// Type: HsvColor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
public struct HsvColor
{
  public double H;
  public double S;
  public double V;

  public float normalizedH
  {
    get => (float) this.H / 360f;
    set => this.H = (double) value * 360.0;
  }

  public float normalizedS
  {
    get => (float) this.S;
    set => this.S = (double) value;
  }

  public float normalizedV
  {
    get => (float) this.V;
    set => this.V = (double) value;
  }

  public HsvColor(double h, double s, double v)
  {
    this.H = h;
    this.S = s;
    this.V = v;
  }

  public override string ToString()
  {
    return "{" + this.H.ToString("f2") + "," + this.S.ToString("f2") + "," + this.V.ToString("f2") + "}";
  }
}
