﻿// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Max
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace LibNoise.Operator
{
  public class Max : ModuleBase
  {
    public Max()
      : base(2)
    {
    }

    public Max(ModuleBase lhs, ModuleBase rhs)
      : base(2)
    {
      this.Modules[0] = lhs;
      this.Modules[1] = rhs;
    }

    public override double GetValue(double x, double y, double z)
    {
      return Math.Max(this.Modules[0].GetValue(x, y, z), this.Modules[1].GetValue(x, y, z));
    }
  }
}
