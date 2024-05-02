// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.ScaleBias
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class ScaleBias : ModuleBase
  {
    public ScaleBias()
      : base(1)
    {
      this.Scale = 1.0;
    }

    public ScaleBias(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
      this.Scale = 1.0;
    }

    public ScaleBias(double scale, double bias, ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
      this.Bias = bias;
      this.Scale = scale;
    }

    public double Bias { get; set; }

    public double Scale { get; set; }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(x, y, z) * this.Scale + this.Bias;
    }
  }
}
