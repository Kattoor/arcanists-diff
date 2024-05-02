// Decompiled with JetBrains decompiler
// Type: GamePlayedStats
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using System;
using UnityEngine;

#nullable disable
[Serializable]
public class GamePlayedStats
{
  public string name = "";
  public short rating = 1000;
  public int totalRatedGames;
  public int wins;
  public int losses;
  public int draws;
  public int teamGames;
  [JsonIgnore]
  public long lastGamePlayed;
  public int longestWinningStreak;
  public int gamesWentFirst;
  public int gamesWentFirstWon;
  public string ftwPercent;
  public string winPercent;
  public string lossPercent;
  public string drawPercent;
  public long damageDealt;
  public int gamesSurvived;
  public int kills;
  public int totalUnratedGames;

  public GamePlayedStats(string n, GamesPlayed g)
  {
    this.name = n;
    this.rating = g.rating;
    this.totalUnratedGames = g.totalUnratedGames;
    this.totalRatedGames = g.totalRatedGames;
    this.wins = g.ratedGamesWon;
    this.teamGames = g.teamGames;
    this.longestWinningStreak = g.longestWinningStreak;
    this.damageDealt = g.damageDealt;
    this.gamesSurvived = g.gamesSurvived;
    this.kills = g.kills;
    this.gamesWentFirst = g.gamesWentFirst;
    this.gamesWentFirstWon = g.gamesWentFirstWon;
    this.draws = g.draws;
    this.losses = this.totalRatedGames - this.wins - this.draws;
    this.winPercent = (this.wins * 100 / this.totalRatedGames).ToString() + "%";
    this.lossPercent = (this.losses * 100 / this.totalRatedGames).ToString() + "%";
    this.drawPercent = (this.draws * 100 / this.totalRatedGames).ToString() + "%";
    if (this.gamesWentFirst > 0)
      this.ftwPercent = (this.gamesWentFirstWon * 100 / this.gamesWentFirst).ToString() + "%";
    else
      this.ftwPercent = "0%";
  }

  public void Minus(GamePlayedStats g)
  {
    this.rating -= g.rating;
    this.totalUnratedGames -= g.totalUnratedGames;
    this.totalRatedGames -= g.totalRatedGames;
    this.wins -= g.wins;
    this.teamGames -= g.teamGames;
    this.longestWinningStreak = Mathf.Max(this.longestWinningStreak, g.longestWinningStreak);
    this.damageDealt -= g.damageDealt;
    this.gamesSurvived -= g.gamesSurvived;
    this.kills -= g.kills;
    this.gamesWentFirst -= g.gamesWentFirst;
    this.gamesWentFirstWon -= g.gamesWentFirstWon;
    this.draws -= g.draws;
    this.losses = this.totalRatedGames - this.wins - this.draws;
    int num;
    string str1;
    if (this.wins <= 0)
    {
      str1 = "0%";
    }
    else
    {
      num = this.wins * 100 / this.totalRatedGames;
      str1 = num.ToString() + "%";
    }
    this.winPercent = str1;
    string str2;
    if (this.losses <= 0)
    {
      str2 = "0%";
    }
    else
    {
      num = this.losses * 100 / this.totalRatedGames;
      str2 = num.ToString() + "%";
    }
    this.lossPercent = str2;
    string str3;
    if (this.draws <= 0)
    {
      str3 = "0%";
    }
    else
    {
      num = this.draws * 100 / this.totalRatedGames;
      str3 = num.ToString() + "%";
    }
    this.drawPercent = str3;
    if (this.gamesWentFirst > 0)
    {
      num = this.gamesWentFirstWon * 100 / this.gamesWentFirst;
      this.ftwPercent = num.ToString() + "%";
    }
    else
      this.ftwPercent = "0%";
  }
}
