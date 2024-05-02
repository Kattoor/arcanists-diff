// Decompiled with JetBrains decompiler
// Type: LibNoise.Generator.Const
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
