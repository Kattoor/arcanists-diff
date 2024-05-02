// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Checker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace LibNoise.Generator
{
  public class Checker : ModuleBase
  {
    public Checker()
      : base(0)
    {
    }

    public override double GetValue(double x, double y, double z)
    {
      return ((int) Math.Floor(Utils.MakeInt32Range(x)) & 1 ^ (int) Math.Floor(Utils.MakeInt32Range(y)) & 1 ^ (int) Math.Floor(Utils.MakeInt32Range(z)) & 1) == 0 ? 1.0 : -1.0;
    }
  }
}
