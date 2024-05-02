// Decompiled with JetBrains decompiler
// Type: GamesPlayed
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
[Serializable]
public class GamesPlayed
{
  public const byte version = 3;
  public short rating = 1000;
  public int totalUnratedGames;
  public int totalRatedGames;
  public int ratedGamesWon;
  public int teamGames;
  [JsonIgnore]
  public int currentWinningStreak;
  public int longestWinningStreak;
  public long damageDealt;
  public int gamesSurvived;
  public int kills;
  public int gamesWentFirst;
  public int gamesWentFirstWon;
  public int draws;
  public long lastGamePlayed;
  [JsonIgnore]
  public int wands;
  [JsonIgnore]
  public int spellbookWinningStreak;
  [JsonIgnore]
  public int spellbookWinningStreak_Maps;
  [JsonIgnore]
  public byte[] lastSpellBook = new byte[16]
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
  [NonSerialized]
  public int sessionWinningStreak;

  public string ToString(Account a)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("\nRating: ").Append(this.rating).Append("\n");
    stringBuilder.Append("Rated: ").Append(this.totalRatedGames).Append("\n");
    stringBuilder.Append("Won: ").Append(this.ratedGamesWon).Append(" (").Append((int) ((double) this.ratedGamesWon / (double) this.totalRatedGames * 100.0)).Append("%)\n");
    stringBuilder.Append("Draws: ").Append(this.draws).Append(" (").Append((int) ((double) this.draws / (double) this.totalRatedGames * 100.0)).Append("%)\n");
    stringBuilder.Append("Winning Streak: ").Append(this.currentWinningStreak).Append("\n");
    stringBuilder.Append("Longest WS: ").Append(this.longestWinningStreak).Append("\n");
    stringBuilder.Append("Spellbook WS: ").Append(this.spellbookWinningStreak).Append("\n");
    stringBuilder.Append("Damage: ").Append(this.damageDealt).Append("\n");
    stringBuilder.Append("Survived: ").Append(this.gamesSurvived).Append(" (").Append((int) ((double) this.gamesSurvived / (double) this.totalRatedGames * 100.0)).Append("%)\n");
    stringBuilder.Append("Kills: ").Append(this.kills).Append("\n");
    stringBuilder.Append("Went First: ").Append(this.gamesWentFirst).Append(" (").Append((int) ((double) this.gamesWentFirst / (double) (this.totalRatedGames - this.teamGames) * 100.0)).Append("%)\n");
    stringBuilder.Append("Went First Wins: ").Append(this.gamesWentFirstWon).Append(" (").Append((int) ((double) this.gamesWentFirstWon / (double) this.gamesWentFirst * 100.0)).Append("%)\n");
    stringBuilder.Append("Team Games: ").Append(this.teamGames).Append(" (").Append((int) ((double) this.teamGames / (double) this.totalRatedGames * 100.0)).Append("%)\n");
    stringBuilder.Append("Wands: ").Append(a.wands).Append("/").Append(a.totalWands).Append("/").Append(Prestige.MaxWands(a)).Append("\n");
    stringBuilder.Append("Unrated: ").Append(this.totalUnratedGames).Append("\n");
    stringBuilder.Append("Last Time Played (UTC): ").Append(this.lastGamePlayed > 0L ? DateTime.FromBinary(this.lastGamePlayed).ToString("f") + "\n" : "Before patch v4.9\n");
    return stringBuilder.ToString();
  }

  public float GetWinPercent()
  {
    return this.totalRatedGames <= 0 ? 0.0f : (float) ((double) this.ratedGamesWon / (double) this.totalRatedGames * 100.0);
  }

  public float GetDrawPercent()
  {
    return this.totalRatedGames <= 0 ? 0.0f : (float) ((double) this.draws / (double) this.totalRatedGames * 100.0);
  }

  public float GetLossPercent()
  {
    return this.totalRatedGames <= 0 ? 0.0f : (float) ((double) (this.totalRatedGames - this.ratedGamesWon - this.draws) / (double) this.totalRatedGames * 100.0);
  }

  public int GetFirstTurnWinPercentage()
  {
    return (int) ((double) this.gamesWentFirstWon / (double) this.gamesWentFirst * 100.0);
  }

  public void Update(
    ZGame game,
    Account name,
    bool rated,
    short rating,
    bool won,
    bool first,
    bool survived,
    int dmg,
    int kills,
    bool team,
    SettingsPlayer sp)
  {
    if (rated)
    {
      ++this.totalRatedGames;
      if (this.rating >= (short) 0)
      {
        this.rating += rating;
        if (this.rating < (short) 1000)
          this.rating = (short) 1000;
      }
      this.lastGamePlayed = DateTime.UtcNow.ToBinary();
      if (first && !team)
        ++this.gamesWentFirst;
      if (survived)
        ++this.gamesSurvived;
      if (rating == (short) 0)
        ++this.draws;
      if (team)
        ++this.teamGames;
      this.kills += kills;
      this.damageDealt += (long) dmg;
      if (won)
      {
        ++this.ratedGamesWon;
        ++this.currentWinningStreak;
        ++this.sessionWinningStreak;
        if (this.currentWinningStreak > this.longestWinningStreak)
          ++this.longestWinningStreak;
        if (first && !team)
          ++this.gamesWentFirstWon;
        if (this.spellbookWinningStreak == 0 || Global.combareByteArrays(this.lastSpellBook, sp.spells))
        {
          ++this.spellbookWinningStreak;
          this.spellbookWinningStreak_Maps = (int) ((MapEnum) this.spellbookWinningStreak_Maps | game.gameFacts.realMap);
        }
        else
        {
          this.spellbookWinningStreak = 0;
          this.spellbookWinningStreak_Maps = (int) game.gameFacts.realMap;
        }
      }
      else
      {
        this.currentWinningStreak = 0;
        this.spellbookWinningStreak = 0;
        this.sessionWinningStreak = 0;
      }
      for (int index = 0; index < 16; ++index)
        this.lastSpellBook[index] = sp.spells[index];
    }
    else
      ++this.totalUnratedGames;
    name.ToFile();
  }

  public void SerializeToClient(myBinaryWriter w)
  {
    w.Write(this.rating);
    w.Write(this.totalRatedGames);
    w.Write(this.ratedGamesWon);
    w.Write(this.draws);
  }

  public static GamesPlayed DeserializeToClient(myBinaryReader r)
  {
    return new GamesPlayed()
    {
      rating = r.ReadInt16(),
      totalRatedGames = r.ReadInt32(),
      ratedGamesWon = r.ReadInt32(),
      draws = r.ReadInt32()
    };
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 3);
    w.Write(this.rating);
    w.Write(this.totalUnratedGames);
    w.Write(this.totalRatedGames);
    w.Write(this.ratedGamesWon);
    w.Write(this.currentWinningStreak);
    w.Write(this.longestWinningStreak);
    w.Write(this.damageDealt);
    w.Write(this.gamesSurvived);
    w.Write(this.kills);
    w.Write(this.gamesWentFirst);
    w.Write(this.gamesWentFirstWon);
    w.Write(this.spellbookWinningStreak);
    w.Write(this.lastSpellBook);
    w.Write(this.draws);
    w.Write(this.wands);
    w.Write(this.teamGames);
    w.Write(this.spellbookWinningStreak_Maps);
    w.Write(this.lastGamePlayed);
  }

  public static GamesPlayed Deserialize(myBinaryReader r)
  {
    GamesPlayed gamesPlayed = new GamesPlayed();
    byte num = r.ReadByte();
    gamesPlayed.rating = r.ReadInt16();
    gamesPlayed.totalUnratedGames = r.ReadInt32();
    gamesPlayed.totalRatedGames = r.ReadInt32();
    gamesPlayed.ratedGamesWon = r.ReadInt32();
    gamesPlayed.currentWinningStreak = r.ReadInt32();
    gamesPlayed.longestWinningStreak = r.ReadInt32();
    gamesPlayed.damageDealt = r.ReadInt64();
    gamesPlayed.gamesSurvived = r.ReadInt32();
    gamesPlayed.kills = r.ReadInt32();
    gamesPlayed.gamesWentFirst = r.ReadInt32();
    gamesPlayed.gamesWentFirstWon = r.ReadInt32();
    gamesPlayed.spellbookWinningStreak = r.ReadInt32();
    gamesPlayed.lastSpellBook = r.ReadBytes();
    gamesPlayed.draws = r.ReadInt32();
    gamesPlayed.wands = r.ReadInt32();
    if (num > (byte) 0)
      gamesPlayed.teamGames = r.ReadInt32();
    if (num > (byte) 1)
      gamesPlayed.spellbookWinningStreak_Maps = r.ReadInt32();
    if (num > (byte) 2)
      gamesPlayed.lastGamePlayed = r.ReadInt64();
    return gamesPlayed;
  }

  public static void SetGames(string fileName, GamesPlayed g)
  {
    string path1 = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "Accounts" + Path.DirectorySeparatorChar.ToString() + fileName + Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(path1))
      return;
    string path2 = path1 + "games.games";
    try
    {
      using (FileStream fileStream = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.None))
      {
        using (myBinaryWriter w = new myBinaryWriter((Stream) fileStream))
          g.Serialize(w);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    Connection connection;
    if (!Server._names.TryGetValue(fileName, out connection) || connection.State != ConnectionState.Connected)
      return;
    Server.UpdateAccountInfo(connection.player.account, false);
  }

  public static GamesPlayed GetGamesGraph(string path2, string fileName)
  {
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + path2 + Path.DirectorySeparatorChar.ToString() + fileName + Path.DirectorySeparatorChar.ToString() + "games.games";
    if (!File.Exists(path))
      return new GamesPlayed();
    try
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) fileStream))
          return GamesPlayed.Deserialize(r);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return new GamesPlayed();
  }

  public static GamesPlayed GetGames(string fileName)
  {
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "Accounts" + Path.DirectorySeparatorChar.ToString() + fileName + Path.DirectorySeparatorChar.ToString() + "games.games";
    if (!File.Exists(path))
      return new GamesPlayed();
    try
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) fileStream))
          return GamesPlayed.Deserialize(r);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return new GamesPlayed();
  }

  public static GamesPlayed GetGamesFullPath(string path)
  {
    if (!File.Exists(path))
      return new GamesPlayed();
    try
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) fileStream))
          return GamesPlayed.Deserialize(r);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return new GamesPlayed();
  }
}
