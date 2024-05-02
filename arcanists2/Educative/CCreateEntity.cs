// Decompiled with JetBrains decompiler
// Type: Educative.CCreateEntity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Educative
{
  public class CCreateEntity : Command
  {
    public int summon = 1;
    public int team = 1;
    public bool bool_OnPlayerPanel;
    public bool bool_playSound;
    public Point point_location = new Point();
    public SettingsPlayer settings = SettingsPlayer.DefaultSettings();

    public CCreateEntity() => this.type = Command.Type.CreateEntity;
  }
}
