// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Voronoi
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace LibNoise.Generator
{
  public class Voronoi : ModuleBase
  {
    private double _displacement = 1.0;
    private double _frequency = 1.0;
    private int _seed;
    private bool _distance;

    public Voronoi()
      : base(0)
    {
    }

    public Voronoi(double frequency, double displacement, int seed, bool distance)
      : base(0)
    {
      this.Frequency = frequency;
      this.Displacement = displacement;
      this.Seed = seed;
      this.UseDistance = distance;
    }

    public double Displacement
    {
      get => this._displacement;
      set => this._displacement = value;
    }

    public double Frequency
    {
      get => this._frequency;
      set => this._frequency = value;
    }

    public int Seed
    {
      get => this._seed;
      set => this._seed = value;
    }

    public bool UseDistance
    {
      get => this._distance;
      set => this._distance = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      x *= this._frequency;
      y *= this._frequency;
      z *= this._frequency;
      int num1 = x > 0.0 ? (int) x : (int) x - 1;
      int num2 = y > 0.0 ? (int) y : (int) y - 1;
      int num3 = z > 0.0 ? (int) z : (int) z - 1;
      double num4 = (double) int.MaxValue;
      double d1 = 0.0;
      double d2 = 0.0;
      double d3 = 0.0;
      for (int z1 = num3 - 2; z1 <= num3 + 2; ++z1)
      {
        for (int y1 = num2 - 2; y1 <= num2 + 2; ++y1)
        {
          for (int x1 = num1 - 2; x1 <= num1 + 2; ++x1)
          {
            double num5 = (double) x1 + Utils.ValueNoise3D(x1, y1, z1, this._seed);
            double num6 = (double) y1 + Utils.ValueNoise3D(x1, y1, z1, this._seed + 1);
            double num7 = (double) z1 + Utils.ValueNoise3D(x1, y1, z1, this._seed + 2);
            double num8 = num5 - x;
            double num9 = num6 - y;
            double num10 = num7 - z;
            double num11 = num8 * num8 + num9 * num9 + num10 * num10;
            if (num11 < num4)
            {
              num4 = num11;
              d1 = num5;
              d2 = num6;
              d3 = num7;
            }
          }
        }
      }
      double num12;
      if (this._distance)
      {
        double num13 = d1 - x;
        double num14 = d2 - y;
        double num15 = d3 - z;
        num12 = Math.Sqrt(num13 * num13 + num14 * num14 + num15 * num15) * 1.7320508075688772 - 1.0;
      }
      else
        num12 = 0.0;
      return num12 + this._displacement * Utils.ValueNoise3D((int) Math.Floor(d1), (int) Math.Floor(d2), (int) Math.Floor(d3), 0);
    }
  }
}
