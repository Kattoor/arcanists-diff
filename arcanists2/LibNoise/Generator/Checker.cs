// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Checker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
