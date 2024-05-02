

using Hazel;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
public class GameFacts
{
  public ZGame game;
  public bool CreateGameFromGF;
  public int id;
  public int port = -1;
  public int seed;
  public double lastChange;
  public int serverStartTime;
  public List<Connection> connections = new List<Connection>();
  public List<string> players = new List<string>();
  public List<string> originalOrder = new List<string>();
  public HashSet<string> invitedPlayers = new HashSet<string>();
  public List<SettingsPlayer> settingsPlayer = new List<SettingsPlayer>();
  public List<SpellsOnly> realSpells = new List<SpellsOnly>();
  public List<Account> accounts = new List<Account>();
  public InviteEnum rematchInvite = InviteEnum.Open;
  public bool tournamentWasSetByTO;
  public byte status;
  public const byte status_Not_Started = 0;
  public const byte status_Starting = 1;
  public const byte status_Inprogress = 2;
  public const byte status_Concluded = 3;
  public GameSettings settings = new GameSettings();
  public MapEnum realMap = MapEnum.Grassy_Hills;
  private IEnumerator<float> ieStart;

  public ZGame.GameType gameType
  {
    get => this.settings.gameType;
    set => this.settings.gameType = value;
  }

  public int gameModes
  {
    get => this.settings.gameModes;
    set => this.settings.gameModes = value;
  }

  public int gameModes2
  {
    get => this.settings.gameModes2;
    set => this.settings.gameModes2 = value;
  }

  public int gameModes3
  {
    get => this.settings.gameModes3;
    set => this.settings.gameModes3 = value;
  }

  public int gameModes4
  {
    get => this.settings.gameModes4;
    set => this.settings.gameModes4 = value;
  }

  public byte elementalLevel
  {
    get => this.settings.elementalLevel;
    set => this.settings.elementalLevel = value;
  }

  public ushort startHealth
  {
    get => this.settings.startHealth;
    set => this.settings.startHealth = value;
  }

  public byte armageddonTurn
  {
    get => this.settings.armageddonTurn;
    set => this.settings.armageddonTurn = value;
  }

  public short countdownTime
  {
    get => this.settings.countdownTime;
    set => this.settings.countdownTime = value;
  }

  public byte countdownDelay
  {
    get => this.settings.countdownDelay;
    set => this.settings.countdownDelay = value;
  }

  public ushort customTime
  {
    get => this.settings.customTime;
    set => this.settings.customTime = value;
  }

  public byte customPlayerCount
  {
    get => this.settings.customPlayerCount;
    set => this.settings.customPlayerCount = value;
  }

  public Restrictions restrictions
  {
    get => this.settings.restrictions;
    set => this.settings.restrictions = value;
  }

  public void CalculateGameType()
  {
    int timeInSeconds = this.GetTimeInSeconds();
    if (this.IsNonStandard() || timeInSeconds < 7 || timeInSeconds > 90 || this.armageddonTurn != (byte) 10 || this.startHealth != (ushort) 250 || this.restrictions != null && this.restrictions.AnyRestricted() || this.GetArmageddon() != ~MapEnum.Dont_Mind || this.settings.customArmageddon != null)
      this.gameType = ZGame.GameType.Party;
    else
      this.gameType = this.GetTimeInSeconds() < 30 ? ZGame.GameType.LowStandard : ZGame.GameType.HighStandard;
  }

  public static bool AllowCustomArmageddon(SpellEnum s)
  {
    return s == SpellEnum.Rising_Lava || s == SpellEnum.Dense_Fog;
  }

  public string ToString(ZGame g, bool linked = false)
  {
    StringBuilder stringBuilder = new StringBuilder();
    GameStyle style = this.GetStyle();
    this.CalculateGameType();
    if (this.GetRatedMode())
      stringBuilder.Append("<#FF0000>Rated Game</color><br>");
    if (g != null && g.isTutorial)
      stringBuilder.Append("Tutorial<br>");
    else if (g != null && g.isSandbox)
      stringBuilder.Append("Sandbox<br>");
    else
      stringBuilder.Append(RatedFacts.GetGameTypeAsStringLong((int) this.gameType)).Append("<br>");
    stringBuilder.Append("Map: ").Append(this.status == (byte) 0 ? GameFacts.MapName(this.GetMapMode()) : GameFacts.MapName(this.realMap)).Append("<br>");
    List<SpellEnum> customArmageddon = this.settings.customArmageddon;
    // ISSUE: explicit non-virtual call
    if ((customArmageddon != null ? (__nonvirtual (customArmageddon.Count) > 0 ? 1 : 0) : 0) != 0)
    {
      stringBuilder.Append("Armageddon: ");
      for (int index = 0; index < this.settings.customArmageddon.Count; ++index)
      {
        Spell spell = Inert.GetSpell(this.settings.customArmageddon[index]);
        if ((UnityEngine.Object) spell != (UnityEngine.Object) null && (spell.level < 4 || GameFacts.AllowCustomArmageddon(spell.spellEnum)))
          stringBuilder.Append("<sprite name=\"").Append(spell.name).Append("\">");
      }
      stringBuilder.Append("<br>");
    }
    else
      stringBuilder.Append("Armageddon: ").Append(g == null ? GameFacts.ArmageddonName(this.GetArmageddon(), style) : GameFacts.ArmageddonName(g.armageddon, style)).Append(g == null || g.armageddon != MapEnum.Grassy_Hills ? " (turn " + (object) this.armageddonTurn + ")<br>" : "<br>");
    if (g == null)
      stringBuilder.Append("Players: ").Append(this.players.Count).Append("/").Append(this.customPlayerCount.ToString()).Append("<br>");
    else
      stringBuilder.Append("Players: ").Append(this.players.Count).Append("<br>");
    stringBuilder.Append("Turn Time: ").Append(this.customTime).Append("s<br>");
    stringBuilder.Append(this.GetTeamMode() ? (this.GetMultiTeamMode() ? "Multi-Teams of " + (object) this.GetNumberPlayersPerTeam() : "Teams of " + (object) this.GetNumberPlayersPerTeam()) : "FFA").Append("<br>");
    if (this.startHealth != (ushort) 250)
      stringBuilder.Append("Health: ").Append(this.startHealth).Append("<br>");
    if (this.countdownTime != (short) 0)
      stringBuilder.Append("Countdown: ").Append(Global.IntToTime((int) this.countdownTime, 16)).Append(" +").Append(this.countdownDelay).Append("<br>");
    if (!this.GetSpectatorMode())
      stringBuilder.Append("No Spectators<br>");
    if (style.HasStyle(GameStyle.Shuffle_Players))
      stringBuilder.Append("Shuffle Players<br>");
    if (style.HasStyle(GameStyle.Random_Spells))
      stringBuilder.Append("Random Spells<br>");
    if (style.HasStyle(GameStyle.Original_Spells_Only))
      stringBuilder.Append("Original Spells Only<br>");
    if (style.HasStyle(GameStyle.No_Movement))
      stringBuilder.Append("No Movement<br>");
    if (style.HasStyle(GameStyle.Zero_Shield))
      stringBuilder.Append("Zero Shield<br>");
    if (style.HasStyle(GameStyle.Sandbox))
      stringBuilder.Append("Sandbox<br>");
    if (style.HasStyle(GameStyle.First_Turn_Teleport))
      stringBuilder.Append("First Turn Teleport<br>");
    if (style.HasStyle(GameStyle.Elementals))
      stringBuilder.Append("Elementals Level: ").Append(this.settings.elementalLevel).Append("<br>");
    if (this.GetAllowArcanePowers())
      stringBuilder.Append("Allow Arcane Monster<br>");
    if (this.restrictions != null)
      stringBuilder.Append("Restricted spells<br>");
    if (this.settings.mapWidth != (byte) 100)
      stringBuilder.Append("Width: ").Append(this.settings.mapWidth).Append("%<br>");
    if (this.settings.mapHeight != (byte) 100)
      stringBuilder.Append("Height: ").Append(this.settings.mapHeight).Append("%<br>");
    if (g != null && !g.isSandbox || this.settings.fixedMapSeed)
    {
      if (linked)
        stringBuilder.Append("Seed: <u><link=Copy: " + (object) this.settings.mapSeed + ">").Append(this.settings.mapSeed).Append(this.settings.fixedMapSeed ? "</link></u> (Fixed)<br>" : "</link></u><br>");
      else
        stringBuilder.Append("Seed: ").Append(this.settings.mapSeed).Append(this.settings.fixedMapSeed ? " (Fixed)<br>" : "<br>");
    }
    stringBuilder.Append("<br>");
    stringBuilder.Append(this.settings.FilteredDescription());
    return stringBuilder.ToString();
  }

  public void SendReadyState()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 17);
        myBinaryWriter.Write(this.connections.Count);
        bool flag = true;
        for (int index = 0; index < this.connections.Count; ++index)
        {
          myBinaryWriter.Write(this.connections[index].player.ready);
          if (!this.connections[index].player.ready)
            flag = false;
        }
        if (this.connections.Count < 2 && this.GetRatedMode() || this.GetRatedMode() && this.GetMultiTeamMode() && this.connections.Count != 2)
          flag = false;
        myBinaryWriter.Write(flag);
        if (flag)
        {
          if (this.GetRatedMode())
            this.DelayStart();
          else
            Server.StartGame(this);
        }
        else
          this.KillStart();
      }
      byte[] array = memoryStream.ToArray();
      for (int index = 0; index < this.connections.Count; ++index)
        this.connections[index].SendBytes(array);
    }
  }

  public void RecieveReadyState(myBinaryReader r)
  {
    int length = r.ReadInt32();
    UnratedMenu.readyStates = new bool[length];
    for (int index = 0; index < length; ++index)
      UnratedMenu.readyStates[index] = r.ReadBoolean();
    UnratedMenu.instance?.RefreshReadyStates(r.ReadBoolean());
  }

  public void DelayStart()
  {
    this.KillStart();
    if (!this.CheckRatingDiff())
      Server.SendUnratedMsg(this, "Shuffle players is not on and the rating difference between the teams is too high to start");
    else
      this.ieStart = Timing.RunCoroutine(this.startRatedLobby());
  }

  public bool CheckRatingDiff() => true;

  public void KillStart()
  {
    if (this.ieStart != null)
      Timing.KillCoroutines(this.ieStart);
    this.ieStart = (IEnumerator<float>) null;
  }

  private IEnumerator<float> startRatedLobby()
  {
    GameFacts gg = this;
    float f = 5.5f;
    while ((double) f > 0.0)
    {
      f -= Time.deltaTime;
      yield return 0.0f;
    }
    if (gg.connections.Count >= 4 && gg.connections.Count % gg.GetNumberPlayersPerTeam() == 0)
    {
      Server.StartGame(gg);
      gg.ieStart = (IEnumerator<float>) null;
    }
  }

  public void Apply(RatedFacts r)
  {
    this.SetMapMode(r.GetRandomMap());
    this.SetTimeMode(r.GetRandomTime());
    this.customTime = (ushort) this.GetTimeInSeconds();
    this.SetArmageddon((MapEnum) r.armageddon);
    if (r.teams != 65536)
    {
      if (this.connections.Count > 2 && this.connections.Count % 2 == 0 && (r.teams == 16777216 || Server.random.Next(0, 2) == 1))
      {
        this.SetTeamMode(TeamEnum.Yes);
        if (this.connections.Count == 4)
          this.SetPlayersPerTeam(PlayersPerTeam.Half);
      }
      else
        this.SetTeamMode(TeamEnum.No);
    }
    bool flag1 = (r.extraOptions & 8) != 0;
    int num = (r.extraOptions & 16) != 0 ? 1 : 0;
    bool flag2 = (r.extraOptions & 2) != 0;
    bool flag3 = (r.extraOptions & 8192) != 0;
    if ((r.extraOptions & 65536) != 0)
    {
      if (r.extraOptions == -1)
      {
        if (Server.random.Next(0, 1) == 0)
          this.SetStyle(GameStyle.Watchtower);
      }
      else
        this.SetStyle(GameStyle.Watchtower);
    }
    if (num != 0)
    {
      if (r.extraOptions == -1)
      {
        if (Server.random.Next(0, 1) == 0)
          this.SetStyle(GameStyle.Original_Spells_Only);
      }
      else
        this.SetStyle(GameStyle.Original_Spells_Only);
    }
    if (flag1)
    {
      if (r.extraOptions == -1)
      {
        if (Server.random.Next(0, 1) == 0)
          this.SetStyle(GameStyle.Random_Spells);
      }
      else
        this.SetStyle(GameStyle.Random_Spells);
    }
    if (flag2)
    {
      if (r.extraOptions == -1)
      {
        if (Server.random.Next(0, 1) == 0)
          this.SetStyle(GameStyle.Elementals);
      }
      else
        this.SetStyle(GameStyle.Elementals);
    }
    if (r.gameType == 2 && !this.GetStyle().HasStyle(GameStyle.Elementals | GameStyle.Random_Spells))
    {
      if (flag1)
        this.SetStyle(GameStyle.Random_Spells);
      else if (flag2)
        this.SetStyle(GameStyle.Elementals);
      else if (Server.random.Next(0, 1) == 0)
        this.SetStyle(GameStyle.Random_Spells);
      else
        this.SetStyle(GameStyle.Elementals);
    }
    if (!flag3)
      return;
    if (r.extraOptions == -1)
    {
      if (Server.random.Next(0, 1) != 0)
        return;
      this.SetStyle(GameStyle.First_Turn_Teleport);
    }
    else
      this.SetStyle(GameStyle.First_Turn_Teleport);
  }

  public void SetInviteMode(InviteEnum e)
  {
    this.gameModes &= -32;
    this.gameModes = (int) ((InviteEnum) this.gameModes | e);
  }

  public void SetSpectatorMode(bool e)
  {
    if (e)
      this.gameModes |= 64;
    else
      this.gameModes &= -65;
  }

  public void SetRatedMode(bool e)
  {
    if (e)
      this.gameModes |= 128;
    else
      this.gameModes &= -129;
  }

  public void SetTournamentMode(bool e)
  {
    if (e)
      this.gameModes |= 32768;
    else
      this.gameModes &= -32769;
  }

  public void SetTimeMode(TimeEnum e)
  {
    this.gameModes &= -1343265;
    this.gameModes = (int) ((TimeEnum) this.gameModes | e);
  }

  public void SetMapMode(MapEnum e)
  {
    this.gameModes4 &= -1207928433;
    this.gameModes4 = (int) ((MapEnum) this.gameModes4 | e);
  }

  public void SetArmageddon(MapEnum e)
  {
    this.gameModes3 &= -1207928433;
    this.gameModes3 = (int) ((MapEnum) this.gameModes3 | e);
  }

  public void ActivateAllMaps() => this.gameModes4 |= 1207927920;

  public void ActivateAlArmageddons() => this.gameModes3 |= 1207928432;

  public void SetPlayersPerTeam(PlayersPerTeam e)
  {
    this.gameModes2 &= -2117074945;
    this.gameModes2 = (int) ((PlayersPerTeam) this.gameModes2 | e);
  }

  public void SetTeamMode(TeamEnum e)
  {
    this.gameModes &= -17367041;
    this.gameModes = (int) ((TeamEnum) this.gameModes | e);
  }

  public void SetPlayerMode(PlayerEnum e)
  {
    this.gameModes &= -2113929217;
    this.gameModes = (int) ((PlayerEnum) this.gameModes | e);
  }

  public bool GetAllowArcanePowers() => (this.gameModes2 & 131072) != 0;

  public InviteEnum GetInviteMode() => (InviteEnum) (this.gameModes & 31);

  public bool GetSpectatorMode() => (this.gameModes & 64) > 0;

  public bool GetRatedMode() => (this.gameModes & 128) > 0;

  public bool GetTournamentMode() => (this.gameModes & 32768) > 0;

  public TimeEnum GetTimeMode() => (TimeEnum) (this.gameModes & 1343264);

  public static TimeEnum GetLowTimes()
  {
    return TimeEnum.Twenty | TimeEnum.Fifteen | TimeEnum.Ten | TimeEnum.Seven;
  }

  public static TimeEnum GetHighTimes()
  {
    return TimeEnum.Ninety | TimeEnum.Sixty | TimeEnum.Forty_Five | TimeEnum.Thirty;
  }

  public static TimeEnum GetPartyTimes()
  {
    return TimeEnum.Ninety | TimeEnum.Sixty | TimeEnum.Forty_Five | TimeEnum.Thirty | TimeEnum.Twenty | TimeEnum.Fifteen | TimeEnum.Ten | TimeEnum.Seven;
  }

  public MapEnum GetMapMode() => (MapEnum) (this.gameModes4 & 1207928432);

  public MapEnum GetArmageddon() => (MapEnum) (this.gameModes3 & 1207928432);

  public static int AllMaps() => 16745584;

  public PlayersPerTeam GetPlayersPerTeam() => (PlayersPerTeam) (this.gameModes2 & 2117074944);

  public int GetNumberPlayersPerTeam()
  {
    switch (this.GetPlayersPerTeam())
    {
      case PlayersPerTeam.Eight:
        return 8;
      case PlayersPerTeam.Two:
        return 2;
      case PlayersPerTeam.Three:
        return 3;
      case PlayersPerTeam.Four:
        return 4;
      case PlayersPerTeam.Five:
        return 5;
      case PlayersPerTeam.Six:
        return 6;
      case PlayersPerTeam.Seven:
        return 7;
      default:
        if (this.game == null || !this.GetMultiTeamMode() || this.game.gameFacts.status == (byte) 0 || this.game.gameFacts.players.Count <= 2)
          return Mathf.Max(2, (1 + Math.Max(this.players.Count, this.connections.Count)) / 2);
        int numberPlayersPerTeam = 1;
        string player = this.game.gameFacts.players[0];
        for (int index = 1; index < this.game.gameFacts.players.Count && string.Equals(this.game.gameFacts.players[index], this.game.gameFacts.players[0]); ++index)
          ++numberPlayersPerTeam;
        return numberPlayersPerTeam;
    }
  }

  public bool GetTeamMode() => (this.gameModes & 16777216) > 0;

  public bool GetMultiTeamMode() => (this.gameModes & 524288) > 0;

  public PlayerEnum GetPlayerMode() => (PlayerEnum) (this.gameModes & 2113929216);

  public void ToggleStyle(GameStyle e) => this.gameModes2 = (int) ((GameStyle) this.gameModes2 ^ e);

  public void SetStyle(GameStyle e) => this.gameModes2 = (int) ((GameStyle) this.gameModes2 | e);

  public void ReSetStyle(GameStyle e) => this.gameModes2 = (int) ((GameStyle) this.gameModes2 & ~e);

  public GameStyle GetStyle() => (GameStyle) (this.gameModes2 & 258459);

  public bool IsNonStandard() => (this.gameModes2 & 254362) != 0;

  public static MapEnum GetRandomMap(ZGame game, int mapStyle, MapEnum def)
  {
    List<MapEnum> mapEnumList = new List<MapEnum>();
    foreach (MapEnum mapEnum in (MapEnum[]) Enum.GetValues(typeof (MapEnum)))
    {
      if ((mapStyle != -1 || mapEnum != MapEnum.Dark_Fortress) && mapEnum != MapEnum.Random && ((MapEnum) mapStyle & mapEnum) != ~MapEnum.Dont_Mind && mapEnum != MapEnum.Dont_Mind)
        mapEnumList.Add(mapEnum);
    }
    return mapEnumList.Count > 0 ? mapEnumList[game.RandomInt(0, mapEnumList.Count)] : def;
  }

  public int GetTimeInSeconds() => GameFacts._GetTimeInSeconds(this.GetTimeMode());

  public static int _GetTimeInSeconds(TimeEnum t)
  {
    switch (t)
    {
      case TimeEnum.Five:
        return 5;
      case TimeEnum.One_Hundred_Twenty:
        return 120;
      case TimeEnum.Ninety:
        return 90;
      case TimeEnum.Sixty:
        return 60;
      case TimeEnum.Forty_Five:
        return 45;
      case TimeEnum.Thirty:
        return 30;
      case TimeEnum.Twenty:
        return 20;
      case TimeEnum.Ten:
        return 10;
      case TimeEnum.Seven:
        return 7;
      case TimeEnum.Fifteen:
        return 15;
      default:
        return 60;
    }
  }

  public TimeEnum GetClosestTimeToCustom()
  {
    if (this.customTime <= (ushort) 6)
      return TimeEnum.Five;
    if (this.customTime <= (ushort) 9)
      return TimeEnum.Seven;
    if (this.customTime <= (ushort) 12)
      return TimeEnum.Ten;
    if (this.customTime <= (ushort) 18)
      return TimeEnum.Fifteen;
    if (this.customTime <= (ushort) 25)
      return TimeEnum.Twenty;
    if (this.customTime <= (ushort) 38)
      return TimeEnum.Thirty;
    if (this.customTime <= (ushort) 53)
      return TimeEnum.Forty_Five;
    if (this.customTime <= (ushort) 72)
      return TimeEnum.Sixty;
    return this.customTime <= (ushort) 105 ? TimeEnum.Ninety : TimeEnum.One_Hundred_Twenty;
  }

  public static int GetPlayersNeeded(PlayerEnum p)
  {
    switch (p)
    {
      case PlayerEnum.Twenty_Four:
        return 24;
      case PlayerEnum.Two:
        return 2;
      case PlayerEnum.Three:
        return 3;
      case PlayerEnum.Four:
        return 4;
      case PlayerEnum.Five:
        return 5;
      case PlayerEnum.Six:
        return 6;
      default:
        return 6;
    }
  }

  public int GetMaxPlayers() => GameFacts.GetPlayersNeeded(this.GetPlayerMode());

  public static string GameStyleAsString(GameStyle e)
  {
    switch (e)
    {
      case GameStyle.Dont_Mind:
        return "All";
      case GameStyle.Shuffle_Players:
        return "Shuffle";
      case GameStyle.Elementals:
        return "Ele";
      case GameStyle.CustomHP:
        return "HP";
      case GameStyle.Random_Spells:
        return "Random";
      case GameStyle.Original_Spells_Only:
        return "OG";
      case GameStyle.No_Movement:
        return "No Move";
      case GameStyle.Zero_Shield:
        return "";
      case GameStyle.Bid:
        return "Bid";
      case GameStyle.Standard:
        return "";
      case GameStyle.First_Turn_Teleport:
        return "FTT";
      case GameStyle.Sandbox:
        return "Sandbox";
      case GameStyle.Zombie_Monkey:
        return "ZMonkey";
      case GameStyle.Watchtower:
        return "WT";
      case GameStyle.Arcane_Monster:
        return "Monster";
      case GameStyle.Wind:
        return "Wind";
      default:
        return "";
    }
  }

  public static string MapShortName(MapEnum e)
  {
    switch (e)
    {
      case MapEnum.Jungle:
        return "Jungle";
      case MapEnum.Snowy_Hills:
        return "Snowy";
      case MapEnum.Ocean_Floor:
        return "Ocean";
      case MapEnum.Dark_Fortress:
        return "Fortress";
      case MapEnum.Wasteland:
        return "Wasteland";
      case MapEnum.Grassy_Hills:
        return "Grassy";
      case MapEnum.Giants_Mountains:
        return "Cano";
      case MapEnum.Elven_Isles:
        return "Comet";
      case MapEnum.Goblin_Caves:
        return "Fissure";
      case MapEnum.Murky_Swamp:
        return "Swamp";
      case MapEnum.Graveyard:
        return "Grave";
      case MapEnum.Sky_Castles:
        return "Sky";
      case MapEnum.Mos_LeHarmless:
        return "Mos";
      case MapEnum.Arcane_Crystals:
        return "Crystals";
      case MapEnum.Random:
        return "Random";
      case MapEnum.Alien_World:
        return "Alien";
      case MapEnum.Ghostly_Halls:
        return "Halls";
      case MapEnum.Space_Nexus:
        return "Nexus";
      default:
        return "Random";
    }
  }

  public static string MapName(MapEnum e)
  {
    switch (e)
    {
      case MapEnum.Dont_Mind:
        return "Any Map";
      case MapEnum.Jungle:
        return "Kharazi Jungle";
      case MapEnum.Snowy_Hills:
        return "Snowy Hills";
      case MapEnum.Ocean_Floor:
        return "Ocean Floor";
      case MapEnum.Dark_Fortress:
        return "Dark Fortress";
      case MapEnum.Wasteland:
        return "Wasteland";
      case MapEnum.Grassy_Hills:
        return "Grassy Hills";
      case MapEnum.Giants_Mountains:
        return "Giants' Mountains";
      case MapEnum.Elven_Isles:
        return "Elven Isles";
      case MapEnum.Goblin_Caves:
        return "Goblin Caves";
      case MapEnum.Murky_Swamp:
        return "Murky Swamp";
      case MapEnum.Graveyard:
        return "Graveyard";
      case MapEnum.Sky_Castles:
        return "Sky Castles";
      case MapEnum.Mos_LeHarmless:
        return "Mos Le'Harmless";
      case MapEnum.Arcane_Crystals:
        return "Arcane Crystals";
      case MapEnum.Random:
        return "Random";
      case MapEnum.Alien_World:
        return "Alien World";
      case MapEnum.Ghostly_Halls:
        return "Ghostly Halls";
      case MapEnum.Space_Nexus:
        return "Space Nexus";
      default:
        return "Random";
    }
  }

  public static string ArmageddonName(MapEnum e, GameStyle styles)
  {
    switch (e)
    {
      case MapEnum.Jungle:
        return "Dark Totem";
      case MapEnum.Snowy_Hills:
        return "Presents!";
      case MapEnum.Ocean_Floor:
        return "Rain of Clams";
      case MapEnum.Dark_Fortress:
        return "Rising Lava";
      case MapEnum.Wasteland:
        return "Acid Rain";
      case MapEnum.Grassy_Hills:
        return "None";
      case MapEnum.Giants_Mountains:
        return "Volcano";
      case MapEnum.Elven_Isles:
        return "Comet";
      case MapEnum.Goblin_Caves:
        return "Fissure";
      case MapEnum.Murky_Swamp:
        return "Storm";
      case MapEnum.Graveyard:
        return "Rain of Chaos";
      case MapEnum.Sky_Castles:
        return "Sky Ray";
      case MapEnum.Mos_LeHarmless:
        return !styles.HasStyle(GameStyle.Zombie_Monkey) ? "Summon Monkey" : "Zombie Monkeys";
      case MapEnum.Arcane_Crystals:
        return "Arcane Arrow";
      case MapEnum.Random:
        return "Random";
      case MapEnum.Alien_World:
        return "Shooting Stars";
      case MapEnum.Ghostly_Halls:
        return "Duplication";
      case MapEnum.Space_Nexus:
        return "Arcane Meteor";
      default:
        return "Random";
    }
  }

  public static string ArmageddonShortName(MapEnum e)
  {
    switch (e)
    {
      case MapEnum.Jungle:
        return "Wolf";
      case MapEnum.Snowy_Hills:
        return "Presents!";
      case MapEnum.Ocean_Floor:
        return "Clams";
      case MapEnum.Dark_Fortress:
        return "Totem";
      case MapEnum.Wasteland:
        return "Acid";
      case MapEnum.Grassy_Hills:
        return "";
      case MapEnum.Giants_Mountains:
        return "Cano";
      case MapEnum.Elven_Isles:
        return "Comet";
      case MapEnum.Goblin_Caves:
        return "Fiss";
      case MapEnum.Murky_Swamp:
        return "Storm";
      case MapEnum.Graveyard:
        return "RoC";
      case MapEnum.Sky_Castles:
        return "Ray";
      case MapEnum.Mos_LeHarmless:
        return "Monkey";
      case MapEnum.Arcane_Crystals:
        return "Arrows";
      case MapEnum.Random:
        return "Random";
      case MapEnum.Alien_World:
        return "Stars";
      case MapEnum.Ghostly_Halls:
        return "Dupe";
      case MapEnum.Space_Nexus:
        return "Meteor";
      default:
        return "Random";
    }
  }

  public static MapEnum MapFromIndex(int e)
  {
    switch (e)
    {
      case 0:
        return MapEnum.Grassy_Hills;
      case 1:
        return MapEnum.Giants_Mountains;
      case 2:
        return MapEnum.Elven_Isles;
      case 3:
        return MapEnum.Goblin_Caves;
      case 4:
        return MapEnum.Murky_Swamp;
      case 5:
        return MapEnum.Graveyard;
      case 6:
        return MapEnum.Sky_Castles;
      case 7:
        return MapEnum.Mos_LeHarmless;
      case 8:
        return MapEnum.Arcane_Crystals;
      case 9:
        return MapEnum.Snowy_Hills;
      case 10:
        return MapEnum.Ocean_Floor;
      case 11:
        return MapEnum.Grassy_Hills;
      case 12:
        return MapEnum.Dark_Fortress;
      case 13:
        return MapEnum.Wasteland;
      case 14:
        return MapEnum.Space_Nexus;
      case 15:
        return MapEnum.Jungle;
      case 16:
        return MapEnum.Alien_World;
      case 17:
        return MapEnum.Ghostly_Halls;
      default:
        return ~MapEnum.Dont_Mind;
    }
  }

  public static int GetMapIndex(MapEnum e)
  {
    switch (e)
    {
      case MapEnum.Jungle:
        return 15;
      case MapEnum.Snowy_Hills:
        return 9;
      case MapEnum.Ocean_Floor:
        return 10;
      case MapEnum.Dark_Fortress:
        return 12;
      case MapEnum.Wasteland:
        return 13;
      case MapEnum.Grassy_Hills:
        return 0;
      case MapEnum.Giants_Mountains:
        return 1;
      case MapEnum.Elven_Isles:
        return 2;
      case MapEnum.Goblin_Caves:
        return 3;
      case MapEnum.Murky_Swamp:
        return 4;
      case MapEnum.Graveyard:
        return 5;
      case MapEnum.Sky_Castles:
        return 6;
      case MapEnum.Mos_LeHarmless:
        return 7;
      case MapEnum.Arcane_Crystals:
        return 8;
      case MapEnum.Random:
        return 11;
      case MapEnum.Alien_World:
        return 16;
      case MapEnum.Ghostly_Halls:
        return 17;
      case MapEnum.Space_Nexus:
        return 14;
      default:
        return 11;
    }
  }

  public static int GameModeType(GameFacts.Message m)
  {
    switch (m)
    {
      case GameFacts.Message.Invite_Mode:
      case GameFacts.Message.Spectators:
      case GameFacts.Message.Time:
      case GameFacts.Message.Players:
      case GameFacts.Message.PerTeam:
        return 1;
      case GameFacts.Message.Map:
      case GameFacts.Message.AddMap:
        return 4;
      case GameFacts.Message.Team:
      case GameFacts.Message.GameStyle:
        return 2;
      case GameFacts.Message.Armageddon:
      case GameFacts.Message.AddArmageddon:
        return 3;
      default:
        return -1;
    }
  }

  public enum Message : byte
  {
    Invite_Mode = 1,
    Spectators = 3,
    Time = 5,
    Map = 6,
    Team = 7,
    Players = 8,
    PerTeam = 9,
    GameStyle = 11, // 0x0B
    AddMap = 12, // 0x0C
    Armageddon = 13, // 0x0D
    AddArmageddon = 14, // 0x0E
    ElementalLevel = 15, // 0x0F
    StartHealth = 16, // 0x10
    ArmageddonTurn = 17, // 0x11
    MaxTime = 19, // 0x13
    CustomTime = 20, // 0x14
    Delay = 21, // 0x15
    CustomPlayerCount = 22, // 0x16
    Tournament = 23, // 0x17
    Map_Size = 24, // 0x18
    Map_Seed = 25, // 0x19
    Disable_Map_Seed = 26, // 0x1A
    GameType = 27, // 0x1B
    Custom_Armageddon = 28, // 0x1C
    Remove_Custom_Armageddon = 29, // 0x1D
  }
}
