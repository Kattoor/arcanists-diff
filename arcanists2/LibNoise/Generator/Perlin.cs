// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Perlin
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace LibNoise.Generator
{
  public class Perlin : ModuleBase
  {
    private double _frequency = 1.0;
    private double _lacunarity = 2.0;
    private LibNoise.QualityMode _quality = LibNoise.QualityMode.Medium;
    private int _octaveCount = 6;
    private double _persistence = 0.5;
    private int _seed;

    public Perlin()
      : base(0)
    {
    }

    public Perlin(
      double frequency,
      double lacunarity,
      double persistence,
      int octaves,
      int seed,
      LibNoise.QualityMode quality)
      : base(0)
    {
      this.Frequency = frequency;
      this.Lacunarity = lacunarity;
      this.OctaveCount = octaves;
      this.Persistence = persistence;
      this.Seed = seed;
      this.Quality = quality;
    }

    public double Frequency
    {
      get => this._frequency;
      set => this._frequency = value;
    }

    public double Lacunarity
    {
      get => this._lacunarity;
      set => this._lacunarity = value;
    }

    public LibNoise.QualityMode Quality
    {
      get => this._quality;
      set => this._quality = value;
    }

    public int OctaveCount
    {
      get => this._octaveCount;
      set => this._octaveCount = Mathf.Clamp(value, 1, 30);
    }

    public double Persistence
    {
      get => this._persistence;
      set => this._persistence = value;
    }

    public int Seed
    {
      get => this._seed;
      set => this._seed = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      double num1 = 0.0;
      double num2 = 1.0;
      x *= this._frequency;
      y *= this._frequency;
      z *= this._frequency;
      for (int index = 0; index < this._octaveCount; ++index)
      {
        double x1 = Utils.MakeInt32Range(x);
        double num3 = Utils.MakeInt32Range(y);
        double num4 = Utils.MakeInt32Range(z);
        long num5 = (long) (this._seed + index) & (long) uint.MaxValue;
        double y1 = num3;
        double z1 = num4;
        long seed = num5;
        int quality = (int) this._quality;
        double num6 = Utils.GradientCoherentNoise3D(x1, y1, z1, seed, (LibNoise.QualityMode) quality);
        num1 += num6 * num2;
        x *= this._lacunarity;
        y *= this._lacunarity;
        z *= this._lacunarity;
        num2 *= this._persistence;
      }
      return num1;
    }
  }
}
