// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.RidgedMultifractal
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace LibNoise.Generator
{
  public class RidgedMultifractal : ModuleBase
  {
    private double _frequency = 1.0;
    private double _lacunarity = 2.0;
    private LibNoise.QualityMode _quality = LibNoise.QualityMode.Medium;
    private int _octaveCount = 6;
    private double _exponent = 1.0;
    private double _gain = 2.0;
    private double _offset = 1.0;
    private int _seed;
    private readonly double[] _weights = new double[30];

    public RidgedMultifractal()
      : base(0)
    {
      this.UpdateWeights();
    }

    public RidgedMultifractal(
      double frequency,
      double lacunarity,
      int octaves,
      int seed,
      LibNoise.QualityMode quality)
      : base(0)
    {
      this.Frequency = frequency;
      this.Lacunarity = lacunarity;
      this.OctaveCount = octaves;
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
      set
      {
        this._lacunarity = value;
        this.UpdateWeights();
      }
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

    public double SpectralWeightsExponent
    {
      get => this._exponent;
      set => this._exponent = value;
    }

    public double Gain
    {
      get => this._gain;
      set => this._gain = value;
    }

    public double Offset
    {
      get => this._offset;
      set => this._offset = value;
    }

    public int Seed
    {
      get => this._seed;
      set => this._seed = value;
    }

    private void UpdateWeights()
    {
      double x = 1.0;
      for (int index = 0; index < 30; ++index)
      {
        this._weights[index] = Math.Pow(x, -this._exponent);
        x *= this._lacunarity;
      }
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
        long num5 = (long) (this._seed + index & int.MaxValue);
        double y1 = num3;
        double z1 = num4;
        long seed = num5;
        int quality = (int) this._quality;
        double num6 = this._offset - Math.Abs(Utils.GradientCoherentNoise3D(x1, y1, z1, seed, (LibNoise.QualityMode) quality));
        double num7 = num6 * num6 * num2;
        num2 = (double) Mathf.Clamp01((float) (num7 * this._gain));
        num1 += num7 * this._weights[index];
        x *= this._lacunarity;
        y *= this._lacunarity;
        z *= this._lacunarity;
      }
      return num1 * 1.25 - 1.0;
    }
  }
}
