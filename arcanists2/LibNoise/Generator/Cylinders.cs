// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Cylinders
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace LibNoise.Generator
{
  public class Cylinders : ModuleBase
  {
    private double _frequency = 1.0;

    public Cylinders()
      : base(0)
    {
    }

    public Cylinders(double frequency)
      : base(0)
    {
      this.Frequency = frequency;
    }

    public double Frequency
    {
      get => this._frequency;
      set => this._frequency = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      x *= this._frequency;
      z *= this._frequency;
      double d = Math.Sqrt(x * x + z * z);
      double val1 = d - Math.Floor(d);
      double val2 = 1.0 - val1;
      return 1.0 - Math.Min(val1, val2) * 4.0;
    }
  }
}
