// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Select
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class Select : ModuleBase
  {
    private double _fallOff;
    private double _raw;
    private double _min = -1.0;
    private double _max = 1.0;

    public Select()
      : base(3)
    {
    }

    public Select(ModuleBase inputA, ModuleBase inputB, ModuleBase controller)
      : base(3)
    {
      this.Modules[0] = inputA;
      this.Modules[1] = inputB;
      this.Modules[2] = controller;
    }

    public Select(double min, double max, double fallOff, ModuleBase inputA, ModuleBase inputB)
      : this(inputA, inputB, (ModuleBase) null)
    {
      this._min = min;
      this._max = max;
      this.FallOff = fallOff;
    }

    public ModuleBase Controller
    {
      get => this.Modules[2];
      set => this.Modules[2] = value;
    }

    public double FallOff
    {
      get => this._fallOff;
      set
      {
        double num = this._max - this._min;
        this._raw = value;
        this._fallOff = value > num / 2.0 ? num / 2.0 : value;
      }
    }

    public double Maximum
    {
      get => this._max;
      set
      {
        this._max = value;
        this.FallOff = this._raw;
      }
    }

    public double Minimum
    {
      get => this._min;
      set
      {
        this._min = value;
        this.FallOff = this._raw;
      }
    }

    public void SetBounds(double min, double max)
    {
      this._min = min;
      this._max = max;
      this.FallOff = this._fallOff;
    }

    public override double GetValue(double x, double y, double z)
    {
      double num1 = this.Modules[2].GetValue(x, y, z);
      if (this._fallOff > 0.0)
      {
        if (num1 < this._min - this._fallOff)
          return this.Modules[0].GetValue(x, y, z);
        if (num1 < this._min + this._fallOff)
        {
          double num2 = this._min - this._fallOff;
          double num3 = this._min + this._fallOff;
          double position = Utils.MapCubicSCurve((num1 - num2) / (num3 - num2));
          return Utils.InterpolateLinear(this.Modules[0].GetValue(x, y, z), this.Modules[1].GetValue(x, y, z), position);
        }
        if (num1 < this._max - this._fallOff)
          return this.Modules[1].GetValue(x, y, z);
        if (num1 >= this._max + this._fallOff)
          return this.Modules[0].GetValue(x, y, z);
        double num4 = this._max - this._fallOff;
        double num5 = this._max + this._fallOff;
        double position1 = Utils.MapCubicSCurve((num1 - num4) / (num5 - num4));
        return Utils.InterpolateLinear(this.Modules[1].GetValue(x, y, z), this.Modules[0].GetValue(x, y, z), position1);
      }
      return num1 < this._min || num1 > this._max ? this.Modules[0].GetValue(x, y, z) : this.Modules[1].GetValue(x, y, z);
    }
  }
}
