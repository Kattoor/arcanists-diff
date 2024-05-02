// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Turbulence
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using LibNoise.Generator;

#nullable disable
namespace LibNoise.Operator
{
  public class Turbulence : ModuleBase
  {
    private const double X0 = 0.189422607421875;
    private const double Y0 = 0.99371337890625;
    private const double Z0 = 0.4781646728515625;
    private const double X1 = 0.4046478271484375;
    private const double Y1 = 0.276611328125;
    private const double Z1 = 0.9230499267578125;
    private const double X2 = 0.82122802734375;
    private const double Y2 = 0.1710968017578125;
    private const double Z2 = 0.6842803955078125;
    private double _power = 1.0;
    private readonly Perlin _xDistort;
    private readonly Perlin _yDistort;
    private readonly Perlin _zDistort;

    public Turbulence()
      : base(1)
    {
      this._xDistort = new Perlin();
      this._yDistort = new Perlin();
      this._zDistort = new Perlin();
    }

    public Turbulence(ModuleBase input)
      : base(1)
    {
      this._xDistort = new Perlin();
      this._yDistort = new Perlin();
      this._zDistort = new Perlin();
      this.Modules[0] = input;
    }

    public Turbulence(double power, ModuleBase input)
      : this(new Perlin(), new Perlin(), new Perlin(), power, input)
    {
    }

    public Turbulence(Perlin x, Perlin y, Perlin z, double power, ModuleBase input)
      : base(1)
    {
      this._xDistort = x;
      this._yDistort = y;
      this._zDistort = z;
      this.Modules[0] = input;
      this.Power = power;
    }

    public double Frequency
    {
      get => this._xDistort.Frequency;
      set
      {
        this._xDistort.Frequency = value;
        this._yDistort.Frequency = value;
        this._zDistort.Frequency = value;
      }
    }

    public double Power
    {
      get => this._power;
      set => this._power = value;
    }

    public int Roughness
    {
      get => this._xDistort.OctaveCount;
      set
      {
        this._xDistort.OctaveCount = value;
        this._yDistort.OctaveCount = value;
        this._zDistort.OctaveCount = value;
      }
    }

    public int Seed
    {
      get => this._xDistort.Seed;
      set
      {
        this._xDistort.Seed = value;
        this._yDistort.Seed = value + 1;
        this._zDistort.Seed = value + 2;
      }
    }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(x + this._xDistort.GetValue(x + 0.189422607421875, y + 0.99371337890625, z + 0.4781646728515625) * this._power, y + this._yDistort.GetValue(x + 0.4046478271484375, y + 0.276611328125, z + 0.9230499267578125) * this._power, z + this._zDistort.GetValue(x + 0.82122802734375, y + 0.1710968017578125, z + 0.6842803955078125) * this._power);
    }
  }
}
