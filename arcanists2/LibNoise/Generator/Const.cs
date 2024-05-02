

#nullable disable
namespace LibNoise.Generator
{
  public class Const : ModuleBase
  {
    private double _value;

    public Const()
      : base(0)
    {
    }

    public Const(double value)
      : base(0)
    {
      this.Value = value;
    }

    public double Value
    {
      get => this._value;
      set => this._value = value;
    }

    public override double GetValue(double x, double y, double z) => this._value;
  }
}
