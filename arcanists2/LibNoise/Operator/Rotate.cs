

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Rotate : ModuleBase
  {
    private double _x;
    private double _x1Matrix;
    private double _x2Matrix;
    private double _x3Matrix;
    private double _y;
    private double _y1Matrix;
    private double _y2Matrix;
    private double _y3Matrix;
    private double _z;
    private double _z1Matrix;
    private double _z2Matrix;
    private double _z3Matrix;

    public Rotate()
      : base(1)
    {
      this.SetAngles(0.0, 0.0, 0.0);
    }

    public Rotate(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public Rotate(double x, double y, double z, ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
      this.SetAngles(x, y, z);
    }

    public double X
    {
      get => this._x;
      set => this.SetAngles(value, this._y, this._z);
    }

    public double Y
    {
      get => this._y;
      set => this.SetAngles(this._x, value, this._z);
    }

    public double Z
    {
      get => this._x;
      set => this.SetAngles(this._x, this._y, value);
    }

    private void SetAngles(double x, double y, double z)
    {
      double num1 = Math.Cos(x * (Math.PI / 180.0));
      double num2 = Math.Cos(y * (Math.PI / 180.0));
      double num3 = Math.Cos(z * (Math.PI / 180.0));
      double num4 = Math.Sin(x * (Math.PI / 180.0));
      double num5 = Math.Sin(y * (Math.PI / 180.0));
      double num6 = Math.Sin(z * (Math.PI / 180.0));
      this._x1Matrix = num5 * num4 * num6 + num2 * num3;
      this._y1Matrix = num1 * num6;
      this._z1Matrix = num5 * num3 - num2 * num4 * num6;
      this._x2Matrix = num5 * num4 * num3 - num2 * num6;
      this._y2Matrix = num1 * num3;
      this._z2Matrix = -num2 * num4 * num3 - num5 * num6;
      this._x3Matrix = -num5 * num1;
      this._y3Matrix = num4;
      this._z3Matrix = num2 * num1;
      this._x = x;
      this._y = y;
      this._z = z;
    }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(this._x1Matrix * x + this._y1Matrix * y + this._z1Matrix * z, this._x2Matrix * x + this._y2Matrix * y + this._z2Matrix * z, this._x3Matrix * x + this._y3Matrix * y + this._z3Matrix * z);
    }
  }
}
