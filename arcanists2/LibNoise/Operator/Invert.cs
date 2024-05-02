

#nullable disable
namespace LibNoise.Operator
{
  public class Invert : ModuleBase
  {
    public Invert()
      : base(1)
    {
    }

    public Invert(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public override double GetValue(double x, double y, double z)
    {
      return -this.Modules[0].GetValue(x, y, z);
    }
  }
}
