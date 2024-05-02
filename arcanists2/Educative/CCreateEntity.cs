

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
