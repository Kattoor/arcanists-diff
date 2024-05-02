﻿

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Min : ModuleBase
  {
    public Min()
      : base(2)
    {
    }

    public Min(ModuleBase lhs, ModuleBase rhs)
      : base(2)
    {
      this.Modules[0] = lhs;
      this.Modules[1] = rhs;
    }

    public override double GetValue(double x, double y, double z)
    {
      return Math.Min(this.Modules[0].GetValue(x, y, z), this.Modules[1].GetValue(x, y, z));
    }
  }
}
