// Decompiled with JetBrains decompiler
// Type: Educative.CCreateEntity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
