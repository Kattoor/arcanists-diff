

using System;

#nullable disable
namespace LibNoise.Generator
{
  public class Checker : ModuleBase
  {
    public Checker()
      : base(0)
    {
    }

    public override double GetValue(double x, double y, double z)
    {
      return ((int) Math.Floor(Utils.MakeInt32Range(x)) & 1 ^ (int) Math.Floor(Utils.MakeInt32Range(y)) & 1 ^ (int) Math.Floor(Utils.MakeInt32Range(z)) & 1) == 0 ? 1.0 : -1.0;
    }
  }
}
