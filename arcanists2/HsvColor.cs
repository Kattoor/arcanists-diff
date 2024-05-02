// Decompiled with JetBrains decompiler
// Type: HsvColor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
