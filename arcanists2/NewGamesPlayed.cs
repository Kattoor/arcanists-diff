// Decompiled with JetBrains decompiler
// Type: NewGamesPlayed
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using System;
using System.Text;
using UnityEngine;

#nullable disable
[Serializable]
public class NewGamesPlayed
{
  public const byte version = 1;
  public short rating = 1000;
  public int ratedGamesWon;
  public int totalRatedGames;
  [JsonIgnore]
  public int currentWinningStreak;
  public int longestWinningStreak;
  public long damageDealt;
  public int gamesSurvived;
  public int kills;
  public int gamesWentFirst;
  public int gamesWentFirstWon;
  public int draws;
  public int teamGames;
  public long lastGamePlayed;

  [JsonIgnore]
  public string ratingString => this.rating != (short) -1 ? this.rating.ToString() : "-";

  public void Serialize(myBinaryWriter w)
  {
    w.Write(this.rating);
    w.Write(this.ratedGamesWon);
    w.Write(this.totalRatedGames);
    w.Write(this.currentWinningStreak);
    w.Write(this.longestWinningStreak);
    w.Write(this.damageDealt);
    w.Write(this.gamesSurvived);
    w.Write(this.kills);
    w.Write(this.gamesWentFirst);
    w.Write(this.gamesWentFirstWon);
    w.Write(this.draws);
    w.Write(this.teamGames);
    w.Write(this.lastGamePlayed);
  }

  public static NewGamesPlayed Deserialize(myBinaryReader r)
  {
    NewGamesPlayed newGamesPlayed = new NewGamesPlayed();
    newGamesPlayed._Deserialize(r);
    return newGamesPlayed;
  }

  private void _Deserialize(myBinaryReader r)
  {
    this.rating = r.ReadInt16();
    this.ratedGamesWon = r.ReadInt32();
    this.totalRatedGames = r.ReadInt32();
    this.currentWinningStreak = r.ReadInt32();
    this.longestWinningStreak = r.ReadInt32();
    this.damageDealt = r.ReadInt64();
    this.gamesSurvived = r.ReadInt32();
    this.kills = r.ReadInt32();
    this.gamesWentFirst = r.ReadInt32();
    this.gamesWentFirstWon = r.ReadInt32();
    this.draws = r.ReadInt32();
    this.teamGames = r.ReadInt32();
    this.lastGamePlayed = r.ReadInt64();
  }

  public void Combine(NewGamesPlayed b)
  {
    this.ratedGamesWon += b.ratedGamesWon;
    this.totalRatedGames += b.totalRatedGames;
    this.currentWinningStreak = Mathf.Max(this.currentWinningStreak, b.currentWinningStreak);
    this.longestWinningStreak = Mathf.Max(this.longestWinningStreak, b.longestWinningStreak);
    this.damageDealt += b.damageDealt;
    this.gamesSurvived += b.gamesSurvived;
    this.kills += b.kills;
    this.gamesWentFirst += b.gamesWentFirst;
    this.gamesWentFirstWon += b.gamesWentFirstWon;
    this.draws += b.draws;
    this.teamGames += b.teamGames;
    this.lastGamePlayed = Math.Max(this.lastGamePlayed, b.lastGamePlayed);
  }

  public string ToString(Account a, string ratingOverride = null)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Rating: ").Append(ratingOverride != null ? ratingOverride : this.rating.ToString()).Append("\n");
    stringBuilder.Append("Rated: ").Append(this.totalRatedGames).Append("\n");
    stringBuilder.Append("Won: ").Append(this.ratedGamesWon).Append(" (").Append((int) ((double) Global.Divide((float) this.ratedGamesWon, (float) this.totalRatedGames) * 100.0)).Append("%)\n");
    stringBuilder.Append("Draws: ").Append(this.draws).Append(" (").Append((int) ((double) Global.Divide((float) this.draws, (float) this.totalRatedGames) * 100.0)).Append("%)\n");
    stringBuilder.Append("Winning Streak: ").Append(this.currentWinningStreak).Append("\n");
    stringBuilder.Append("Longest WS: ").Append(this.longestWinningStreak).Append("\n");
    stringBuilder.Append("Damage: ").Append(this.damageDealt).Append("\n");
    stringBuilder.Append("Survived: ").Append(this.gamesSurvived).Append(" (").Append((int) ((double) Global.Divide((float) this.gamesSurvived, (float) this.totalRatedGames) * 100.0)).Append("%)\n");
    stringBuilder.Append("Kills: ").Append(this.kills).Append("\n");
    stringBuilder.Append("Went First: ").Append(this.gamesWentFirst).Append(" (").Append((int) ((double) Global.Divide((float) this.gamesWentFirst, (float) (this.totalRatedGames - this.teamGames)) * 100.0)).Append("%)\n");
    stringBuilder.Append("Went First Wins: ").Append(this.gamesWentFirstWon).Append(" (").Append((int) ((double) Global.Divide((float) this.gamesWentFirstWon, (float) this.gamesWentFirst) * 100.0)).Append("%)\n");
    stringBuilder.Append("Team Games: ").Append(this.teamGames).Append(" (").Append((int) ((double) Global.Divide((float) this.teamGames, (float) this.totalRatedGames) * 100.0)).Append("%)\n");
    stringBuilder.Append("Wands: ").Append(a.wands).Append("/").Append(a.totalWands).Append("/").Append(Prestige.MaxWands(a)).Append("\n");
    stringBuilder.Append("Last Time Played (UTC): ").Append(this.lastGamePlayed > 0L ? DateTime.FromBinary(this.lastGamePlayed).ToString("f") : "Before patch v4.9");
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
    Account acc,
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
        ++acc.sessionWinningStreak;
        if (this.currentWinningStreak > this.longestWinningStreak)
          ++this.longestWinningStreak;
        if (first && !team)
          ++this.gamesWentFirstWon;
        if (acc.spellbookWinningStreak == 0 || Global.combareByteArrays(acc.lastSpellBook, sp.spells))
        {
          ++acc.spellbookWinningStreak;
          acc.spellbookWinningStreak_Maps |= (int) game.gameFacts.realMap;
        }
        else
        {
          acc.spellbookWinningStreak = 0;
          acc.spellbookWinningStreak_Maps = (int) game.gameFacts.realMap;
        }
      }
      else
      {
        this.currentWinningStreak = 0;
        acc.spellbookWinningStreak = 0;
        acc.sessionWinningStreak = 0;
      }
      for (int index = 0; index < 16; ++index)
        acc.lastSpellBook[index] = sp.spells[index];
    }
    else
    {
      if (game.players.Count <= 1 || game.isMulti && game.TEAM_COUNT <= 1)
        return;
      ++acc.totalUnratedGames;
    }
  }
}
