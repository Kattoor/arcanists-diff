// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Const
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Generator
{
  public class Const : ModuleBase
  {
    private double _value;

    public Const()
      : base(0)
    {
    }

    public Const(double value)
      : base(0)
    {
      this.Value = value;
    }

    public double Value
    {
      get => this._value;
      set => this._value = value;
    }

    public override double GetValue(double x, double y, double z) => this._value;
  }
}
