

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Exponent : ModuleBase
  {
    private double _exponent = 1.0;

    public Exponent()
      : base(1)
    {
    }

    public Exponent(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public Exponent(double exponent, ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
      this.Value = exponent;
    }

    public double Value
    {
      get => this._exponent;
      set => this._exponent = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      return Math.Pow(Math.Abs((this.Modules[0].GetValue(x, y, z) + 1.0) / 2.0), this._exponent) * 2.0 - 1.0;
    }
  }
}
