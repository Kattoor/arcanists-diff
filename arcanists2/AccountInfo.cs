// Decompiled with JetBrains decompiler
// Type: AccountInfo
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
public class AccountInfo
{
  public const byte AttributeIgnore = 1;
  [NonSerialized]
  public bool fake;
  public string name = "";
  public string oldName = "";
  public AccountType accountType = AccountType.Muted;
  public byte experience;
  public byte prestige;
  public ulong discord;
  public byte displayedIcon;
  public byte displayClanPrefix;
  public int points;
  public long lastLogin;
  public int dust = 100;
  public int poll;
  public Location location;
  public Connection activeConnection;
  public short version = 10028;
  public BitBools tutorials = new BitBools();
  public int wands;
  public int totalWands;
  public ChatSetting lobbyChat;
  public ChatSetting gameChat;
  public ChatSetting friendChat;
  public ChatSetting inviteChat;
  public ChatSetting spectatorChat;
  public ChatSetting clanChat;
  public ChatSetting teamChat;
  public ChatSetting miniGameChat;
  public HashSet<string> friends = new HashSet<string>();
  public HashSet<string> ignored = new HashSet<string>();
  public GamesPlayed games = new GamesPlayed();
  public Cosmetics cosmetics;
  public float bonusExperience;
  public string clan = "";
  public Clan.Roles clanRole = Clan.Roles.Member;
  public string state = "";
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

  public short rating
  {
    get => this.games.rating;
    set => this.games.rating = value;
  }

  public int similiarRating => Mathf.Max((int) this.games.rating, 1000);

  public string ratingString => this.games.rating >= (short) 0 ? this.games.rating.ToString() : "-";

  public int experienceIndex() => (int) this.experience;

  public static string numberplus(int i) => i.ToString() + AccountInfo.number(i);

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
  }

  public short RequiredRating() => (short) ((int) this.experience * 200);

  public int RequiredAchievements() => Mathf.Clamp((int) this.experience, 0, 10) * 2500 + 500;

  public int RequiredPoints() => ((int) this.experience + 1) * 156;

  public void AddWands(int x)
  {
  }

  public static string ToRomanNumber(int number)
  {
    StringBuilder stringBuilder = new StringBuilder();
    while (number > 0)
    {
      for (int index = ((IEnumerable<int>) AccountInfo.digitsValues).Count<int>() - 1; index >= 0; --index)
      {
        if (number / AccountInfo.digitsValues[index] >= 1)
        {
          number -= AccountInfo.digitsValues[index];
          stringBuilder.Append(AccountInfo.romanDigits[index]);
          break;
        }
      }
    }
    return stringBuilder.ToString();
  }

  public AccountInfo()
  {
  }

  public AccountInfo(string n) => this.name = n;

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.name);
    w.Write(this.games == null ? (short) 1000 : this.rating);
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
    this.rating = r.ReadInt16();
    this.accountType = (AccountType) r.ReadInt32();
    this.experience = r.ReadByte();
    this.discord = r.ReadUInt64();
    this.displayedIcon = r.ReadByte();
    this.displayClanPrefix = r.ReadByte();
    this.clan = r.ReadString();
    this.clanRole = (Clan.Roles) r.ReadByte();
    this.location = r.ReadByte() == (byte) 1 ? Location.Ingame : Location.Disconnecting;
    this.prestige = r.ReadByte();
    this.oldName = r.ReadString();
  }

  public void Copy(AccountInfo other)
  {
    this.name = other.name;
    this.rating = other.rating;
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
    string str = AccountInfo.ContainsInvalidWord(s);
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
    IEnumerable<string> source1 = ((IEnumerable<string>) source.Split()).Select<string, string>((Func<string, string>) (x => x.Trim(AccountInfo.punctuation)));
    foreach (string str in AccountInfo.InvalidWord)
    {
      if (source1.Contains<string>(str, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
        return str;
    }
    foreach (string fullWord in AccountInfo.fullWords)
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

  public void ToFile(string fileName)
  {
  }
}
