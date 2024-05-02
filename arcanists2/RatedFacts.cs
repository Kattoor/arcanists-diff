

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class RatedFacts
{
  public const int DONT_MIND = -1;
  public int playerCount = -1;
  public int turnTime = -1;
  public int mapStyle = -1;
  public int extraOptions = 4096;
  public int teams = -1;
  public int armageddon;
  public int gameType;
  public const byte Version = 3;
  public short rating;
  public SpellsOnly spellOverrides;

  public override bool Equals(object obj) => this.EqualsRatedFacts(obj as RatedFacts);

  private bool EqualsRatedFacts(RatedFacts b)
  {
    return b != null && this.gameType == b.gameType && this.turnTime == b.turnTime && this.mapStyle == b.mapStyle && this.extraOptions == b.extraOptions && this.playerCount == b.playerCount && this.armageddon == b.armageddon && this.teams == b.teams;
  }

  public override int GetHashCode()
  {
    return ((((((17 * 23 + this.playerCount.GetHashCode()) * 23 + this.turnTime.GetHashCode()) * 23 + this.mapStyle.GetHashCode()) * 23 + this.extraOptions.GetHashCode()) * 23 + this.teams.GetHashCode()) * 23 + this.armageddon.GetHashCode()) * 23 + this.gameType.GetHashCode();
  }

  public void VerifyGameType(bool forced = false)
  {
    this.gameType = Mathf.Clamp(this.gameType, 0, 2);
    if (this.teams != -1 | forced)
      this.teams &= 16842752;
    if (this.teams == 0)
      this.teams = forced ? 65536 : -1;
    if (this.gameType == 0)
    {
      this.armageddon = 0;
      this.extraOptions = 4096;
      this.playerCount = (int) ((TimeEnum) this.teams & GameFacts.GetLowTimes());
      if ((this.teams & 65536) != 0)
        this.playerCount |= 67108864;
      if (this.turnTime != -1 | forced)
        this.turnTime &= 1335296;
      if (this.turnTime != 0)
        return;
      this.turnTime = forced ? 16384 : -1;
    }
    else if (this.gameType == 1)
    {
      this.armageddon = 0;
      this.extraOptions = 4096;
      this.playerCount = (this.teams & 16777216) != 0 ? 1375731712 : 67108864;
      if ((this.teams & 65536) != 0)
        this.playerCount |= 67108864;
      if (this.turnTime != -1 | forced)
        this.turnTime = (int) ((TimeEnum) this.turnTime & GameFacts.GetHighTimes());
      if (this.turnTime != 0)
        return;
      this.turnTime = forced ? 1024 : -1;
    }
    else
    {
      if (this.turnTime != -1 | forced)
        this.turnTime = (int) ((TimeEnum) this.turnTime & GameFacts.GetPartyTimes());
      if (this.turnTime != 0)
        return;
      this.turnTime = -1;
    }
  }

  public bool MatchExtraOption(GameStyle t, RatedFacts b)
  {
    return this.extraOptions == -1 || b.extraOptions == -1 || ((GameStyle) b.extraOptions & t) == ((GameStyle) this.extraOptions & t);
  }

  public RatedFacts Copy()
  {
    return new RatedFacts()
    {
      gameType = this.gameType,
      playerCount = this.playerCount,
      turnTime = this.turnTime,
      mapStyle = this.mapStyle,
      extraOptions = this.extraOptions,
      teams = this.teams,
      armageddon = this.armageddon,
      rating = this.rating
    };
  }

  public static string GetGameTypeAsString(int gameType)
  {
    if (gameType == 0)
      return "LTS";
    return gameType == 1 ? "HTS" : "PMO";
  }

  public static string GetGameTypeAsStringLong(int gameType)
  {
    if (gameType == 0)
      return "Low Time Standard";
    return gameType == 1 ? "High Time Standard" : "Party Mode On";
  }

  public string GetGameTypeAsString() => RatedFacts.GetGameTypeAsString(this.gameType);

  public string GetGameTypeAsStringLong() => RatedFacts.GetGameTypeAsStringLong(this.gameType);

  public string ToString(bool breaks = false, pfabLobbyGame p = null)
  {
    string str = breaks ? "\n" : "";
    StringBuilderPlus stringBuilderPlus = new StringBuilderPlus((breaks ? "<#FF0000>Rated Queue <br>" + this.GetGameTypeAsStringLong() + "</color>" : "<sprite name=\"" + this.GetGameTypeAsString() + "\">") + " " + str);
    bool flag1 = false;
    object obj = (object) null;
    int num1 = 0;
    if ((UnityEngine.Object) p != (UnityEngine.Object) null)
    {
      p.txtRatedFacts.text = stringBuilderPlus.ToString();
      stringBuilderPlus.Clear();
    }
    if (this.turnTime == -1 || this.gameType == 0 && (TimeEnum) this.turnTime == GameFacts.GetLowTimes() || this.gameType == 1 && (TimeEnum) this.turnTime == GameFacts.GetHighTimes() || this.gameType == 2 && (TimeEnum) this.turnTime == GameFacts.GetPartyTimes())
    {
      stringBuilderPlus.Append("Time DM");
    }
    else
    {
      foreach (TimeEnum timeEnum in SortedEnum.timeEnums)
      {
        if (timeEnum != TimeEnum.Dont_Mind && (timeEnum & (TimeEnum) this.turnTime) != ~TimeEnum.Dont_Mind)
        {
          if (!breaks)
            ++num1;
          if (num1 == 5)
            obj = (object) timeEnum;
          else if (num1 < 5)
            stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts._GetTimeInSeconds(timeEnum)).Append("\">");
        }
      }
    }
    if (num1 == 5)
      stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts._GetTimeInSeconds((TimeEnum) obj)).Append("\">");
    else if (num1 > 5)
      stringBuilderPlus.Append("+").Append(num1 - 4);
    int num2 = 0;
    if ((UnityEngine.Object) p != (UnityEngine.Object) null)
    {
      p.ratedTime.text = stringBuilderPlus.ToString();
      stringBuilderPlus.Clear();
    }
    if (breaks)
      stringBuilderPlus.Append("\n");
    flag1 = false;
    if (this.mapStyle == -1)
    {
      stringBuilderPlus.Append("Map DM");
    }
    else
    {
      foreach (MapEnum mapEnum in SortedEnum.mapEnums)
      {
        if (mapEnum != MapEnum.Dont_Mind && (mapEnum & (MapEnum) this.mapStyle) != ~MapEnum.Dont_Mind)
        {
          if (!breaks)
            ++num2;
          if (num2 == 5)
            obj = (object) mapEnum;
          else if (num2 < 5)
            stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts.MapName(mapEnum)).Append("\">");
        }
      }
    }
    if (num2 == 5)
      stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts.MapName((MapEnum) obj)).Append("\">");
    else if (num2 > 5)
      stringBuilderPlus.Append("+").Append(num2 - 4);
    int num3 = 0;
    if ((UnityEngine.Object) p != (UnityEngine.Object) null)
    {
      p.ratedMaps.text = stringBuilderPlus.ToString();
      stringBuilderPlus.Clear();
    }
    if (breaks)
      stringBuilderPlus.Append("\n");
    if (this.teams == -1)
      stringBuilderPlus.Append("<sprite name=\"ffa\"><sprite name=\"teams\">");
    else if (this.teams == 16777216)
      stringBuilderPlus.Append("<sprite name=\"teams\">");
    else
      stringBuilderPlus.Append("<sprite name=\"ffa\">");
    bool flag2 = false;
    if (this.gameType == 2 && this.playerCount != -1)
    {
      stringBuilderPlus.Append(" ");
      foreach (PlayerEnum playerEnum in SortedEnum.playerEnums)
      {
        if (playerEnum != PlayerEnum.Dont_Mind && (playerEnum & (PlayerEnum) this.playerCount) != ~PlayerEnum.Dont_Mind)
        {
          if (flag2)
            stringBuilderPlus.Append("/");
          int num4 = GameFacts.GetPlayersNeeded(playerEnum);
          if (num4 > 7)
            num4 = (this.teams & 65536) != 0 ? 7 : 8;
          stringBuilderPlus.Append(num4 > 6 ? num4.ToString() + "+" : num4.ToString());
          flag2 = true;
        }
      }
    }
    if ((UnityEngine.Object) p != (UnityEngine.Object) null)
    {
      p.ratedTeams.text = stringBuilderPlus.ToString();
      stringBuilderPlus.Clear();
    }
    flag1 = false;
    if (this.gameType == 2)
    {
      if (breaks)
        stringBuilderPlus.Append("\n");
      if (this.extraOptions == -1)
      {
        stringBuilderPlus.Append("Modes DM");
      }
      else
      {
        int extraOptions = this.extraOptions;
        if (((GameStyle) extraOptions).HasStyle(GameStyle.Elementals))
          stringBuilderPlus.Append("<sprite name=\"Elementals\">");
        if (((GameStyle) extraOptions).HasStyle(GameStyle.Random_Spells))
          stringBuilderPlus.Append("<sprite name=\"Emoji2_1576\">");
        if (((GameStyle) extraOptions).HasStyle(GameStyle.Original_Spells_Only))
          stringBuilderPlus.Append("<sprite name=\"Emoji2_22\">");
        if (((GameStyle) extraOptions).HasStyle(GameStyle.Zero_Shield))
          stringBuilderPlus.Append("<sprite name=\"Arcane Zero Shield\">");
        if (((GameStyle) extraOptions).HasStyle(GameStyle.First_Turn_Teleport))
          stringBuilderPlus.Append("<sprite name=\"Arcane Gate\">");
        if (((GameStyle) extraOptions).HasStyle(GameStyle.Watchtower))
          stringBuilderPlus.Append("<sprite name=\"Watchtower\">");
      }
      if (breaks)
        stringBuilderPlus.Append("\n");
      if ((UnityEngine.Object) p != (UnityEngine.Object) null)
      {
        p.ratedModes.text = stringBuilderPlus.ToString();
        stringBuilderPlus.Clear();
      }
      flag1 = false;
      if (this.armageddon == -1)
      {
        stringBuilderPlus.Append("Arma DM");
      }
      else
      {
        foreach (MapEnum mapEnum in SortedEnum.mapEnums)
        {
          if (mapEnum != MapEnum.Dont_Mind && (mapEnum & (MapEnum) this.armageddon) != ~MapEnum.Dont_Mind)
          {
            if (!breaks)
              ++num3;
            if (num3 == 5)
              obj = (object) mapEnum;
            else if (num3 < 5)
              stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts.ArmageddonName(mapEnum, GameStyle.Standard)).Append("\">");
          }
        }
      }
      if (num3 == 5)
        stringBuilderPlus.Append("<sprite name=\"").Append(GameFacts.ArmageddonName((MapEnum) obj, GameStyle.Standard)).Append("\">");
      else if (num3 > 5)
        stringBuilderPlus.Append("+").Append(num3 - 4);
      if ((UnityEngine.Object) p != (UnityEngine.Object) null)
      {
        p.ratedArma.text = stringBuilderPlus.ToString();
        stringBuilderPlus.Clear();
      }
    }
    else if ((UnityEngine.Object) p != (UnityEngine.Object) null)
    {
      p.ratedModes.text = "";
      p.ratedArma.text = "";
    }
    return stringBuilderPlus.ToString();
  }

  public void Copy(RatedFacts g)
  {
    this.playerCount = g.playerCount;
    this.turnTime = g.turnTime;
    this.mapStyle = g.mapStyle;
    this.extraOptions = g.extraOptions;
    this.teams = g.teams;
    this.armageddon = g.armageddon;
    this.gameType = g.gameType;
  }

  public TimeEnum GetRandomTime()
  {
    List<TimeEnum> timeEnumList = new List<TimeEnum>();
    foreach (TimeEnum timeEnum in (TimeEnum[]) Enum.GetValues(typeof (TimeEnum)))
    {
      if (((TimeEnum) this.turnTime & timeEnum) != ~TimeEnum.Dont_Mind && timeEnum != TimeEnum.Dont_Mind)
        timeEnumList.Add(timeEnum);
    }
    return timeEnumList.Count > 0 ? timeEnumList[Server.random.Next(0, timeEnumList.Count)] : TimeEnum.Sixty;
  }

  public MapEnum GetRandomMap()
  {
    List<MapEnum> mapEnumList = new List<MapEnum>();
    foreach (MapEnum mapEnum in (MapEnum[]) Enum.GetValues(typeof (MapEnum)))
    {
      if ((this.mapStyle != -1 || mapEnum != MapEnum.Dark_Fortress) && ((MapEnum) this.mapStyle & mapEnum) != ~MapEnum.Dont_Mind && mapEnum != MapEnum.Dont_Mind)
        mapEnumList.Add(mapEnum);
    }
    return mapEnumList.Count > 0 ? mapEnumList[Server.random.Next(0, mapEnumList.Count)] : MapEnum.Grassy_Hills;
  }

  public int MinPlayersNeeded()
  {
    if (this.gameType < 2)
      return (this.teams & 65536) != 0 ? 2 : 4;
    if ((this.playerCount & 67108864) != 0)
      return 2;
    if ((this.playerCount & 134217728) != 0)
      return 3;
    if ((this.playerCount & 268435456) != 0)
      return 4;
    if ((this.playerCount & 536870912) != 0)
      return 5;
    if ((this.playerCount & 1073741824) != 0)
      return 6;
    return (this.playerCount & 33554432) != 0 ? 7 : 2;
  }

  public int MaxPlayersNeeded()
  {
    if (this.gameType < 2)
      return (this.teams & 65536) != 0 ? 2 : 8;
    if ((this.playerCount & 33554432) != 0)
      return 24;
    if ((this.playerCount & 1073741824) != 0)
      return 6;
    if ((this.playerCount & 536870912) != 0)
      return 5;
    if ((this.playerCount & 268435456) != 0)
      return 4;
    if ((this.playerCount & 134217728) != 0)
      return 3;
    int num = this.playerCount & 67108864;
    return 2;
  }

  public bool PlayerCountIsActive(int players)
  {
    switch (players)
    {
      case 1:
        return false;
      case 2:
        return (67108864 & this.playerCount) != 0;
      case 3:
        return (134217728 & this.playerCount) != 0;
      case 4:
        return (268435456 & this.playerCount) != 0;
      case 5:
        return (536870912 & this.playerCount) != 0;
      case 6:
        return (1073741824 & this.playerCount) != 0;
      default:
        return (33554432 & this.playerCount) != 0;
    }
  }

  public static bool Match(RatedFacts a, RatedFacts b)
  {
    if (a.gameType != b.gameType || a.gameType > 1 && (a.playerCount & b.playerCount) == 0 || (a.turnTime & b.turnTime) == 0 || (a.mapStyle & b.mapStyle) == 0 || !a.MatchExtraOption(GameStyle.Random_Spells, b) || !a.MatchExtraOption(GameStyle.Original_Spells_Only, b) || !a.MatchExtraOption(GameStyle.Elementals, b) || !a.MatchExtraOption(GameStyle.First_Turn_Teleport, b) || !a.MatchExtraOption(GameStyle.Bid, b) || !a.MatchExtraOption(GameStyle.Watchtower, b) || (a.teams & b.teams) == 0)
      return false;
    if ((a.armageddon & b.armageddon) != 0 || a.armageddon == b.armageddon || b.armageddon == -1 || a.armageddon == -1)
      return true;
    if (a.armageddon == 0 && (a.mapStyle & b.mapStyle & b.armageddon) != 0)
    {
      a.mapStyle = a.mapStyle & b.mapStyle & b.armageddon;
      return true;
    }
    if (b.armageddon != 0 || (a.mapStyle & b.mapStyle & a.armageddon) == 0)
      return false;
    a.mapStyle = a.mapStyle & b.mapStyle & a.armageddon;
    return true;
  }

  public void Apply(RatedFacts a)
  {
    this.playerCount &= a.playerCount;
    this.turnTime &= a.turnTime;
    this.mapStyle &= a.mapStyle;
    this.extraOptions &= a.extraOptions;
    this.teams &= a.teams;
    this.armageddon &= a.armageddon;
  }

  public void Share(myBinaryWriter w)
  {
    w.Write(this.playerCount);
    w.Write(this.turnTime);
    w.Write(this.mapStyle);
    w.Write(this.extraOptions);
    w.Write(this.teams);
    w.Write(this.armageddon);
    w.Write(this.gameType);
  }

  public void Serialize(myBinaryWriter w, bool includeBook = true)
  {
    w.Write(this.playerCount);
    w.Write(this.turnTime);
    w.Write(this.mapStyle);
    w.Write(this.extraOptions);
    w.Write(this.teams);
    w.Write(this.armageddon);
    w.Write(this.gameType);
    w.Write(this.spellOverrides == null || !includeBook ? (byte) 0 : (byte) 1);
    if (!(this.spellOverrides != null & includeBook))
      return;
    this.spellOverrides.Serialize(w);
  }

  public static RatedFacts Deserialize(int version, myBinaryReader r)
  {
    RatedFacts ratedFacts = new RatedFacts();
    ratedFacts._Deserialize(version, r);
    return ratedFacts;
  }

  public void _Deserialize(int version, myBinaryReader r)
  {
    this.playerCount = r.ReadInt32();
    this.turnTime = r.ReadInt32();
    this.mapStyle = r.ReadInt32();
    this.extraOptions = r.ReadInt32();
    this.teams = r.ReadInt32();
    this.armageddon = r.ReadInt32();
    if (version <= 1)
      return;
    if (version > 2)
      this.gameType = r.ReadInt32();
    if (r.ReadByte() == (byte) 1)
    {
      if (version > 2)
        this.spellOverrides = SpellsOnly.DeserializeExtended(r);
      else
        this.spellOverrides = SpellsOnly.Deserialize(r);
    }
    else
      this.spellOverrides = (SpellsOnly) null;
  }

  public void DeserializeShare(int version, myBinaryReader r)
  {
    this.playerCount = r.ReadInt32();
    this.turnTime = r.ReadInt32();
    this.mapStyle = r.ReadInt32();
    this.extraOptions = r.ReadInt32();
    this.teams = r.ReadInt32();
    this.armageddon = r.ReadInt32();
    this.gameType = r.ReadInt32();
  }

  public void SerializeToFile(string s)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 3);
        this.Serialize(w);
      }
      File.WriteAllBytes(s, memoryStream.ToArray());
    }
  }

  public static RatedFacts DeserializeFromFile(string s)
  {
    using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(s)))
    {
      using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
        return RatedFacts.Deserialize((int) r.ReadByte(), r);
    }
  }
}
