// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Blend
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class Blend : ModuleBase
  {
    public Blend()
      : base(3)
    {
    }

    public Blend(ModuleBase lhs, ModuleBase rhs, ModuleBase controller)
      : base(3)
    {
      this.Modules[0] = lhs;
      this.Modules[1] = rhs;
      this.Modules[2] = controller;
    }

    public ModuleBase Controller
    {
      get => this.Modules[2];
      set => this.Modules[2] = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      double a = this.Modules[0].GetValue(x, y, z);
      double num1 = this.Modules[1].GetValue(x, y, z);
      double num2 = (this.Modules[2].GetValue(x, y, z) + 1.0) / 2.0;
      double b = num1;
      double position = num2;
      return Utils.InterpolateLinear(a, b, position);
    }
  }
}
