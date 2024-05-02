// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Abs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Abs : ModuleBase
  {
    public Abs()
      : base(1)
    {
    }

    public Abs(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public override double GetValue(double x, double y, double z)
    {
      return Math.Abs(this.Modules[0].GetValue(x, y, z));
    }
  }
}
