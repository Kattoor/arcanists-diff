// Decompiled with JetBrains decompiler
// Type: JsonGamesHigh
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;

#nullable disable
public class JsonGamesHigh : IJsonGames
{
  [JsonIgnore]
  public NewGamesPlayed gameHighTime = new NewGamesPlayed();

  public string name { get; set; }

  [JsonIgnore]
  public int rank { get; set; }

  [JsonIgnore]
  public NewGamesPlayed games
  {
    get => this.gameHighTime;
    set => this.gameHighTime = value;
  }

  [JsonProperty("ratehigh")]
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
}
