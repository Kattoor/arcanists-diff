

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Abs : ModuleBase
  {
    public Abs()
      : base(1)
    {
    }

    public Abs(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public override double GetValue(double x, double y, double z)
    {
      return Math.Abs(this.Modules[0].GetValue(x, y, z));
    }
  }
}
