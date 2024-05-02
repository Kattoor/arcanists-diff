

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
