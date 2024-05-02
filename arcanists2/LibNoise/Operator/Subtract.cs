// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Subtract
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class Subtract : ModuleBase
  {
    public Subtract()
      : base(2)
    {
    }

    public Subtract(ModuleBase lhs, ModuleBase rhs)
      : base(2)
    {
      this.Modules[0] = lhs;
      this.Modules[1] = rhs;
    }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(x, y, z) - this.Modules[1].GetValue(x, y, z);
    }
  }
}
