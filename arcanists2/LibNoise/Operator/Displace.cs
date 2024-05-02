// Decompiled with JetBrains decompiler
// Type: LibNoise.Operator.Displace
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace LibNoise.Operator
{
  public class Displace : ModuleBase
  {
    public Displace()
      : base(4)
    {
    }

    public Displace(ModuleBase input, ModuleBase x, ModuleBase y, ModuleBase z)
      : base(4)
    {
      this.Modules[0] = input;
      this.Modules[1] = x;
      this.Modules[2] = y;
      this.Modules[3] = z;
    }

    public ModuleBase X
    {
      get => this.Modules[1];
      set => this.Modules[1] = value;
    }

    public ModuleBase Y
    {
      get => this.Modules[2];
      set => this.Modules[2] = value;
    }

    public ModuleBase Z
    {
      get => this.Modules[3];
      set => this.Modules[3] = value;
    }

    public override double GetValue(double x, double y, double z)
    {
      return this.Modules[0].GetValue(x + this.Modules[1].GetValue(x, y, z), y + this.Modules[2].GetValue(x, y, z), z + this.Modules[3].GetValue(x, y, z));
    }
  }
}
