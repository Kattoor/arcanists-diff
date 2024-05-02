

#nullable disable
namespace LibNoise.Operator
{
  public class Translate : ModuleBase
  {
    private double _x = 1.0;
    private double _y = 1.0;
    private double _z = 1.0;

    public Translate()
      : base(1)
    {
    }

    public Translate(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public Translate(double x, double y, double z, ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public double X
    {
      get => this._x;
      set => this._x = value;
    }

    public double Y
    {
      get => this._y;
      set => this._y = value;
    }

    public double Z
    {
      get => this._z;
      set => this._z = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(x + this._x, y + this._y, z + this._z);
    }
  }
}
