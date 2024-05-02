

using Hazel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#nullable disable
public class Account
{
  public const byte AttributeIgnore = 1;
  [JsonIgnore]
  public int sessionWinningStreak;
  [JsonIgnore]
  public NewGamesPlayed gameLowTime = new NewGamesPlayed();
  [JsonIgnore]
  public NewGamesPlayed gameHighTime = new NewGamesPlayed();
  [JsonIgnore]
  public NewGamesPlayed gameFun = new NewGamesPlayed();
  [JsonIgnore]
  public NewGamesPlayed _oldGames = new NewGamesPlayed();
  [JsonIgnore]
  public bool fake;
  [JsonIgnore]
  public Connection activeConnection;
  [JsonIgnore]
  public string state = "";
  [JsonIgnore]
  internal bool saveCosmetics;
  private static int[] digitsValues = new int[13]
  {
    1,
    4,
    5,
    9,
    10,
    40,
    50,
    90,
    100,
    400,
    500,
    900,
    1000
  };
  private static string[] romanDigits = new string[13]
  {
    "I",
    "IV",
    "V",
    "IX",
    "X",
    "XL",
    "L",
    "XC",
    "C",
    "CD",
    "D",
    "CM",
    "M"
  };
  private static char[] punctuation = new char[1]{ ' ' };
  private static string[] InvalidWord = new string[3]
  {
    "mod",
    "dev",
    "m0d"
  };
  private static string[] fullWords = new string[4]
  {
    "pur3",
    "pure extreme",
    "pur extreme",
    "pure xtreme"
  };
  public const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

  [JsonIgnore]
  public short HighestRating
  {
    get
    {
      return Math.Max(Math.Max(this.gameLowTime.rating, this.gameHighTime.rating), this.gameFun.rating);
    }
  }

  [JsonIgnore]
  public short similarRating => Math.Max((short) 1000, this.HighestRating);

  [JsonIgnore]
  public string ratingString
  {
    get
    {
      return this[(int) Client.gameType].rating != (short) -1 ? this[(int) Client.gameType].rating.ToString() : "-";
    }
  }

  public void ToFile()
  {
  }

  public static async Task<Account> FromFile(string n, bool all = true) => new Account();

  public string key
  {
    get => this.name.ToLower();
    set
    {
    }
  }

  [JsonProperty("name")]
  public string name { get; set; } = "";

  [JsonProperty("date")]
  public string dateCreated { get; set; } = "";

  [JsonProperty("a")]
  [Indexed(null)]
  public string email { get; set; } = "";

  [JsonProperty("b")]
  public bool permBanned { get; set; }

  [JsonProperty("c")]
  public byte[] hash { get; set; }

  [JsonProperty("d")]
  public byte[] salt { get; set; }

  [JsonIgnore]
  public ushort iterations { get; set; } = 10000;

  [JsonProperty("e")]
  public string ispLock { get; set; } = "";

  [JsonProperty("f")]
  public string extraInfo { get; set; } = "";

  [JsonProperty("g")]
  [Indexed(null)]
  public string steamKey { get; set; } = "";

  [JsonProperty("h")]
  public int tournamentCoins { get; set; }

  [JsonIgnore]
  public int totalRatedGames
  {
    get
    {
      return this.TotalRatedGames + this.TotalRatedGames1 + this.TotalRatedGames2 + (this._oldGames != null ? this._oldGames.totalRatedGames : 0);
    }
  }

  [JsonIgnore]
  public int totalRatedGamesWon
  {
    get
    {
      return this.RatedGamesWon + this.RatedGamesWon1 + this.RatedGamesWon2 + (this._oldGames != null ? this._oldGames.ratedGamesWon : 0);
    }
  }

  [JsonProperty("i")]
  [Indexed(null)]
  public int totalUnratedGames { get; set; }

  [JsonIgnore]
  public int spellbookWinningStreak { get; set; }

  [JsonIgnore]
  public int spellbookWinningStreak_Maps { get; set; }

  [JsonIgnore]
  public byte[] lastSpellBook { get; set; } = new byte[16]
  {
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue,
    byte.MaxValue
  };

  [JsonIgnore]
  public NewGamesPlayed this[int i]
  {
    get
    {
      switch (i)
      {
        case 0:
          return this.gameLowTime;
        case 1:
          return this.gameHighTime;
        case 2:
          return this.gameFun;
        default:
          return this._oldGames;
      }
    }
  }

  [JsonProperty("ratelow")]
  [Indexed(null)]
  public short Rating
  {
    get => this.gameLowTime.rating;
    set => this.gameLowTime.rating = value;
  }

  [JsonProperty("gg2")]
  public int RatedGamesWon
  {
    get => this.gameLowTime.ratedGamesWon;
    set => this.gameLowTime.ratedGamesWon = value;
  }

  [JsonProperty("gg3")]
  public int TotalRatedGames
  {
    get => this.gameLowTime.totalRatedGames;
    set => this.gameLowTime.totalRatedGames = value;
  }

  [JsonProperty("gg4")]
  public int CurrentWinningStreak
  {
    get => this.gameLowTime.currentWinningStreak;
    set => this.gameLowTime.currentWinningStreak = value;
  }

  [JsonProperty("gg5")]
  public int LongestWinningStreak
  {
    get => this.gameLowTime.longestWinningStreak;
    set => this.gameLowTime.longestWinningStreak = value;
  }

  [JsonProperty("gg6")]
  public long DamageDealt
  {
    get => this.gameLowTime.damageDealt;
    set => this.gameLowTime.damageDealt = value;
  }

  [JsonProperty("gg7")]
  public int GamesSurvived
  {
    get => this.gameLowTime.gamesSurvived;
    set => this.gameLowTime.gamesSurvived = value;
  }

  [JsonProperty("gg8")]
  public int Kills
  {
    get => this.gameLowTime.kills;
    set => this.gameLowTime.kills = value;
  }

  [JsonProperty("gg9")]
  public int GamesWentFirst
  {
    get => this.gameLowTime.gamesWentFirst;
    set => this.gameLowTime.gamesWentFirst = value;
  }

  [JsonProperty("gg10")]
  public int GamesWentFirstWon
  {
    get => this.gameLowTime.gamesWentFirstWon;
    set => this.gameLowTime.gamesWentFirstWon = value;
  }

  [JsonProperty("gg11")]
  public int Draws
  {
    get => this.gameLowTime.draws;
    set => this.gameLowTime.draws = value;
  }

  [JsonProperty("gg12")]
  public long LastGamePlayed
  {
    get => this.gameLowTime.lastGamePlayed;
    set => this.gameLowTime.lastGamePlayed = value;
  }

  [JsonProperty("gg13")]
  public int TeamGames
  {
    get => this.gameLowTime.teamGames;
    set => this.gameLowTime.teamGames = value;
  }

  [JsonProperty("ratehigh")]
  [Indexed(null)]
  public short Rating1
  {
    get => this.gameHighTime.rating;
    set => this.gameHighTime.rating = value;
  }

  [JsonProperty("gh2")]
  public int RatedGamesWon1
  {
    get => this.gameHighTime.ratedGamesWon;
    set => this.gameHighTime.ratedGamesWon = value;
  }

  [JsonProperty("gh3")]
  public int TotalRatedGames1
  {
    get => this.gameHighTime.totalRatedGames;
    set => this.gameHighTime.totalRatedGames = value;
  }

  [JsonProperty("gh4")]
  public int CurrentWinningStreak1
  {
    get => this.gameHighTime.currentWinningStreak;
    set => this.gameHighTime.currentWinningStreak = value;
  }

  [JsonProperty("gh5")]
  public int LongestWinningStreak1
  {
    get => this.gameHighTime.longestWinningStreak;
    set => this.gameHighTime.longestWinningStreak = value;
  }

  [JsonProperty("gh6")]
  public long DamageDealt1
  {
    get => this.gameHighTime.damageDealt;
    set => this.gameHighTime.damageDealt = value;
  }

  [JsonProperty("gh7")]
  public int GamesSurvived1
  {
    get => this.gameHighTime.gamesSurvived;
    set => this.gameHighTime.gamesSurvived = value;
  }

  [JsonProperty("gh8")]
  public int Kills1
  {
    get => this.gameHighTime.kills;
    set => this.gameHighTime.kills = value;
  }

  [JsonProperty("gh9")]
  public int GamesWentFirst1
  {
    get => this.gameHighTime.gamesWentFirst;
    set => this.gameHighTime.gamesWentFirst = value;
  }

  [JsonProperty("gh10")]
  public int GamesWentFirstWon1
  {
    get => this.gameHighTime.gamesWentFirstWon;
    set => this.gameHighTime.gamesWentFirstWon = value;
  }

  [JsonProperty("gh11")]
  public int Draws1
  {
    get => this.gameHighTime.draws;
    set => this.gameHighTime.draws = value;
  }

  [JsonProperty("gh12")]
  public long LastGamePlayed1
  {
    get => this.gameHighTime.lastGamePlayed;
    set => this.gameHighTime.lastGamePlayed = value;
  }

  [JsonProperty("gh13")]
  public int TeamGames1
  {
    get => this.gameHighTime.teamGames;
    set => this.gameHighTime.teamGames = value;
  }

  [JsonProperty("rateparty")]
  [Indexed(null)]
  public short Rating2
  {
    get => this.gameFun.rating;
    set => this.gameFun.rating = value;
  }

  [JsonProperty("gi2")]
  public int RatedGamesWon2
  {
    get => this.gameFun.ratedGamesWon;
    set => this.gameFun.ratedGamesWon = value;
  }

  [JsonProperty("gi3")]
  public int TotalRatedGames2
  {
    get => this.gameFun.totalRatedGames;
    set => this.gameFun.totalRatedGames = value;
  }

  [JsonProperty("gi4")]
  public int CurrentWinningStreak2
  {
    get => this.gameFun.currentWinningStreak;
    set => this.gameFun.currentWinningStreak = value;
  }

  [JsonProperty("gi5")]
  public int LongestWinningStreak2
  {
    get => this.gameFun.longestWinningStreak;
    set => this.gameFun.longestWinningStreak = value;
  }

  [JsonProperty("gi6")]
  public long DamageDealt2
  {
    get => this.gameFun.damageDealt;
    set => this.gameFun.damageDealt = value;
  }

  [JsonProperty("gi7")]
  public int GamesSurvived2
  {
    get => this.gameFun.gamesSurvived;
    set => this.gameFun.gamesSurvived = value;
  }

  [JsonProperty("gi8")]
  public int Kills2
  {
    get => this.gameFun.kills;
    set => this.gameFun.kills = value;
  }

  [JsonProperty("gi9")]
  public int GamesWentFirst2
  {
    get => this.gameFun.gamesWentFirst;
    set => this.gameFun.gamesWentFirst = value;
  }

  [JsonProperty("gi10")]
  public int GamesWentFirstWon2
  {
    get => this.gameFun.gamesWentFirstWon;
    set => this.gameFun.gamesWentFirstWon = value;
  }

  [JsonProperty("gi11")]
  public int Draws2
  {
    get => this.gameFun.draws;
    set => this.gameFun.draws = value;
  }

  [JsonProperty("gi12")]
  public long LastGamePlayed2
  {
    get => this.gameFun.lastGamePlayed;
    set => this.gameFun.lastGamePlayed = value;
  }

  [JsonProperty("gi13")]
  public int TeamGames2
  {
    get => this.gameFun.teamGames;
    set => this.gameFun.teamGames = value;
  }

  [JsonProperty("k")]
  public string OLD_GAMES
  {
    get => this._oldGames != null ? JsonConvert.SerializeObject((object) this._oldGames) : "";
    set
    {
      this._oldGames = string.IsNullOrEmpty(value) ? (NewGamesPlayed) null : JsonConvert.DeserializeObject<NewGamesPlayed>(value);
    }
  }

  [JsonProperty("j")]
  public string oldName { get; set; } = "";

  [JsonProperty("l")]
  public Account.ExtraStuff extraStuff { get; set; } = new Account.ExtraStuff();

  [JsonProperty("m")]
  public AccountType accountType { get; set; } = AccountType.Muted;

  [JsonProperty("n")]
  public byte experience { get; set; }

  [JsonProperty("o")]
  public byte prestige { get; set; }

  [JsonProperty("p")]
  [Indexed(null)]
  public ulong discord { get; set; }

  [JsonProperty("q")]
  public int displayedIcon { get; set; }

  [JsonProperty("r")]
  public byte displayClanPrefix { get; set; }

  [JsonProperty("s")]
  public int points { get; set; }

  [JsonProperty("t")]
  public long lastLogin { get; set; }

  [JsonProperty("u")]
  public int dust { get; set; } = 100;

  [JsonProperty("ap")]
  public int poll { get; set; }

  [JsonIgnore]
  public Location location { get; set; }

  [JsonIgnore]
  public ulong p => this.discord;

  [JsonProperty("x")]
  public short version { get; set; } = 10029;

  [JsonProperty("y")]
  public BitBools tutorials { get; set; } = new BitBools();

  [JsonProperty("z")]
  public int wands { get; set; }

  [JsonProperty("aa")]
  public int totalWands { get; set; }

  [JsonProperty("ab")]
  public ChatSetting lobbyChat { get; set; }

  [JsonProperty("ac")]
  public ChatSetting gameChat { get; set; }

  [JsonProperty("ad")]
  public ChatSetting friendChat { get; set; }

  [JsonProperty("ae")]
  public ChatSetting inviteChat { get; set; }

  [JsonProperty("af")]
  public ChatSetting spectatorChat { get; set; }

  [JsonProperty("ag")]
  public ChatSetting clanChat { get; set; }

  [JsonProperty("ah")]
  public ChatSetting teamChat { get; set; }

  [JsonProperty("ai")]
  public ChatSetting miniGameChat { get; set; }

  [JsonProperty("aj")]
  public HashSet<string> friends { get; set; } = new HashSet<string>();

  [JsonProperty("ak")]
  public HashSet<string> ignored { get; set; } = new HashSet<string>();

  [JsonProperty("al")]
  public Cosmetics cosmetics { get; set; } = new Cosmetics();

  [JsonProperty("am")]
  public float bonusExperience { get; set; }

  [JsonProperty("an")]
  public string clan { get; set; } = "";

  [JsonProperty("ao")]
  public Clan.Roles clanRole { get; set; } = Clan.Roles.Member;

  [JsonProperty("aq")]
  public BitBools badges { get; set; } = new BitBools();

  public NewGamesPlayed GetGamesPlayed(ZGame.GameType f)
  {
    int i = (int) f;
    return i < 0 || i > 2 ? this.gameFun : this[i];
  }

  public NewGamesPlayed GetGamesPlayed(GameFacts f)
  {
    if (f.GetStyle().HasStyle(GameStyle.Elementals | GameStyle.Random_Spells))
      return this.gameFun;
    return f.GetTimeInSeconds() >= 30 ? this.gameHighTime : this.gameLowTime;
  }

  public int experienceIndex() => (int) this.experience;

  public static string numberplus(int i) => i.ToString() + Account.number(i);

  public static string number(int i)
  {
    switch (i)
    {
      case 1:
        return "st";
      case 2:
        return "nd";
      case 3:
        return "rd";
      default:
        return "th";
    }
  }

  public void IncreaseExperience(Connection c)
  {
    this.bonusExperience -= (float) this.RequiredPoints();
    if ((double) this.bonusExperience < 0.0)
      this.bonusExperience = 0.0f;
    ++this.experience;
    Server.ReturnServerMsg(c, "You are now level " + (object) this.experienceIndex() + "!");
    Server.UpdateAccountInfo(this);
  }

  public short RequiredRating() => (short) ((int) this.experience * 200);

  public int RequiredAchievements() => Mathf.Clamp((int) this.experience, 0, 10) * 2500 + 500;

  public int RequiredPoints() => ((int) this.experience + 1) * 156;

  public void AddWands(int x)
  {
    this.totalWands += x;
    if (this.totalWands > Prestige.MaxWands(this))
    {
      int num = this.totalWands - Prestige.MaxWands(this);
      this.totalWands -= num;
      x -= num;
    }
    this.wands += x;
  }

  public static string ToRomanNumber(int number)
  {
    StringBuilder stringBuilder = new StringBuilder();
    while (number > 0)
    {
      for (int index = ((IEnumerable<int>) Account.digitsValues).Count<int>() - 1; index >= 0; --index)
      {
        if (number / Account.digitsValues[index] >= 1)
        {
          number -= Account.digitsValues[index];
          stringBuilder.Append(Account.romanDigits[index]);
          break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public Account()
  {
  }

  public Account(string n) => this.name = n;

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.name);
    w.Write(this.Rating);
    w.Write(this.Rating1);
    w.Write(this.Rating2);
    w.Write((int) this.accountType);
    w.Write(this.experience);
    w.Write(this.discord);
    w.Write(this.displayedIcon);
    w.Write(this.displayClanPrefix);
    w.Write(this.clan);
    w.Write((byte) this.clanRole);
    w.Write(this.location.Online() ? (byte) 1 : (byte) 0);
    w.Write(this.prestige);
    w.Write(this.oldName);
  }

  public void Deserialize(myBinaryReader r)
  {
    this.name = r.ReadString();
    this.Rating = r.ReadInt16();
    this.Rating1 = r.ReadInt16();
    this.Rating2 = r.ReadInt16();
    this.accountType = (AccountType) r.ReadInt32();
    this.experience = r.ReadByte();
    this.discord = r.ReadUInt64();
    this.displayedIcon = r.ReadInt32();
    this.displayClanPrefix = r.ReadByte();
    this.clan = r.ReadString();
    this.clanRole = (Clan.Roles) r.ReadByte();
    this.location = r.ReadByte() == (byte) 1 ? Location.Ingame : Location.Disconnecting;
    this.prestige = r.ReadByte();
    this.oldName = r.ReadString();
  }

  public void Copy(Account other)
  {
    this.name = other.name;
    this.Rating = other.Rating;
    this.Rating1 = other.Rating1;
    this.Rating2 = other.Rating2;
    this.accountType = other.accountType;
    this.experience = other.experience;
    this.discord = other.discord;
    this.displayedIcon = other.displayedIcon;
    this.displayClanPrefix = other.displayClanPrefix;
    this.clan = other.clan;
    this.clanRole = other.clanRole;
    this.location = other.location;
    this.prestige = other.prestige;
    this.oldName = other.oldName;
  }

  public static string ValidName(string s)
  {
    s = s.ToLower();
    if (string.IsNullOrEmpty(s))
      return "Cannot be empty";
    if (s.StartsWith(" "))
      return "Cannot begin with a Space";
    if (s.EndsWith(" "))
      return "Cannot end with a Space";
    if (s.Length > 13)
      return "Maximum 13 Characters";
    string str = Account.ContainsInvalidWord(s);
    if (str != null)
      return "Contains invalid Phrase '" + str + "'";
    foreach (char ch in s)
    {
      if ((ch < 'a' || ch > 'z') && (ch < '0' || ch > '9') && ch != ' ')
        return "Only Alphanumeric and Space characters are allowed";
    }
    return (string) null;
  }

  public static string ValidPassword(string s)
  {
    return s.Length < 8 || s.Length > 20 ? "Password must between 8 and 20 Characters" : (string) null;
  }

  private static string ContainsInvalidWord(string source)
  {
    IEnumerable<string> source1 = ((IEnumerable<string>) source.Split()).Select<string, string>((Func<string, string>) (x => x.Trim(Account.punctuation)));
    foreach (string str in Account.InvalidWord)
    {
      if (source1.Contains<string>(str, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        return str;
    }
    foreach (string fullWord in Account.fullWords)
    {
      if (source.Contains(fullWord))
        return fullWord;
    }
    return (string) null;
  }

  public static void SerializeList(HashSet<string> s, myBinaryWriter w)
  {
    w.Write(s.Count);
    foreach (string str in s)
      w.Write(str);
  }

  public static void DeserializeList(HashSet<string> s, myBinaryReader r)
  {
    s.Clear();
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
      s.Add(r.ReadString());
  }

  [Serializable]
  public class ExtraStuff
  {
    public override bool Equals(object obj) => obj is Account.ExtraStuff || base.Equals(obj);
  }
}
