// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Cache
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class Cache : ModuleBase
  {
    private double _value;
    private bool _cached;
    private double _x;
    private double _y;
    private double _z;

    public Cache()
      : base(1)
    {
    }

    public Cache(ModuleBase input)
      : base(1)
    {
      this.Modules[0] = input;
    }

    public override ModuleBase this[int index]
    {
      get => base[index];
      set
      {
        base[index] = value;
        this._cached = false;
      }
    }

    public override double GetValue(double x, double y, double z)
    {
      if (!this._cached || this._x != x || this._y != y || this._z != z)
      {
        this._value = this.Modules[0].GetValue(x, y, z);
        this._x = x;
        this._y = y;
        this._z = z;
      }
      this._cached = true;
      return this._value;
    }
  }
}
