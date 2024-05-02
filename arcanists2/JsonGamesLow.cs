

using Newtonsoft.Json;

#nullable disable
public class JsonGamesLow : IJsonGames
{
  [JsonIgnore]
  public NewGamesPlayed gameLowTime = new NewGamesPlayed();

  public string name { get; set; }

  [JsonIgnore]
  public int rank { get; set; }

  [JsonIgnore]
  public NewGamesPlayed games
  {
    get => this.gameLowTime;
    set => this.gameLowTime = value;
  }

  [JsonProperty("ratelow")]
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
}
