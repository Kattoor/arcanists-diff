

using System.Collections.Generic;

#nullable disable
namespace Hazel
{
  public class player_info
  {
    public Account account = new Account();
    public int gameNumber;
    public int id;
    public int port;
    public bool inBoat;
    public int botPos;
    public SettingsPlayer settingsPlayer = new SettingsPlayer();
    public List<SettingsPlayer> multiSettingsPlayer;
    public bool spectator;
    public ZPerson player;
    public long lastChatMsg;
    public long lastShareMsg;
    public bool ready;

    public Connection activeConnection
    {
      get => this.account.activeConnection;
      set => this.account.activeConnection = value;
    }

    public Location location
    {
      get => this.account.location;
      set => this.account.location = value;
    }

    public static player_info Copy(player_info p)
    {
      return new player_info()
      {
        account = p.account,
        gameNumber = p.gameNumber,
        id = p.id,
        port = p.port,
        settingsPlayer = p.settingsPlayer,
        location = p.location,
        spectator = p.spectator,
        player = p.player
      };
    }
  }
}
