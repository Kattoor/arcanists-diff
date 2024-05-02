// Decompiled with JetBrains decompiler
// Type: JsonGamesParty
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;

#nullable disable
public class JsonGamesParty : IJsonGames
{
  [JsonIgnore]
  public NewGamesPlayed gameFun = new NewGamesPlayed();

  public string name { get; set; }

  [JsonIgnore]
  public int rank { get; set; }

  [JsonIgnore]
  public NewGamesPlayed games
  {
    get => this.gameFun;
    set => this.gameFun = value;
  }

  [JsonProperty("rateparty")]
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
}
