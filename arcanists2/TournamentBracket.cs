// Decompiled with JetBrains decompiler
// Type: TournamentBracket
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class TournamentBracket
{
  public HashSet<string> players = new HashSet<string>();
  public List<TournamentBracket.Participant> participants = new List<TournamentBracket.Participant>();
  public GameSettings settings = new GameSettings();
  public List<TournamentBracket.Round> rounds = new List<TournamentBracket.Round>();
  private int nextID;

  public TournamentBracket()
  {
  }

  public TournamentBracket(List<string> p, bool randomSeeds)
  {
    int num = 0;
    foreach (string str in p)
    {
      this.participants.Add(new TournamentBracket.Participant()
      {
        name = str,
        seed = num
      });
      this.players.Add(str);
      ++num;
    }
    if (!randomSeeds)
      return;
    for (int index1 = 0; index1 < 7; ++index1)
    {
      for (int index2 = 0; index2 < this.participants.Count; ++index2)
      {
        int index3 = Server.random.Next(0, this.participants.Count);
        if (index3 != index2)
        {
          TournamentBracket.Participant participant = this.participants[index2];
          this.participants[index2] = this.participants[index3];
          this.participants[index3] = participant;
        }
      }
    }
    for (int index = 0; index < this.participants.Count; ++index)
      this.participants[index].seed = index;
  }

  public void AddRound() => this.rounds.Add(new TournamentBracket.Round());

  public void GenerateNextRound(
    int r,
    TournamentBracket.Bracket.Mode mode,
    int bestOf,
    int advances)
  {
    if (this.rounds[r].bracket.Count == 0)
      return;
    this.AddRound();
    this.rounds[r + 1].bracket.Add(new TournamentBracket.Bracket());
    List<string> p = new List<string>();
    foreach (TournamentBracket.Bracket bracket in this.rounds[r].bracket)
    {
      for (int index = 0; index < bracket.topAdvances; ++index)
        p.Add("");
    }
    this.GenerateSeries(r + 1, 0, p, mode, bestOf, advances);
  }

  public void GenerateSeries(
    int r,
    int b,
    List<string> p,
    TournamentBracket.Bracket.Mode mode,
    int bestOf,
    int advances)
  {
    TournamentBracket.Bracket bracket = this.rounds[r].bracket[b];
    bracket.mode = mode;
    bracket.bestOf = bestOf;
    bracket.topAdvances = advances;
    if (mode == TournamentBracket.Bracket.Mode.Default)
    {
      int num = 2;
      while (num < p.Count)
        num <<= 1;
      int count = p.Count;
      while (p.Count < num)
        p.Add("<bye>");
      List<TournamentBracket.Bracket.Series> seriesList1 = new List<TournamentBracket.Bracket.Series>();
      for (int index = 0; index < p.Count / 2; ++index)
      {
        TournamentBracket.Bracket.Series series = new TournamentBracket.Bracket.Series()
        {
          id = this.nextID++,
          player1 = p[index],
          player2 = p[p.Count - index - 1],
          winner = p[p.Count - index - 1][0] == '<' ? 1 : 0
        };
        bracket.series.Add(series);
        seriesList1.Add(series);
      }
      for (int index1 = num >> 1; index1 > 1; index1 >>= 1)
      {
        List<TournamentBracket.Bracket.Series> seriesList2 = new List<TournamentBracket.Bracket.Series>();
        for (int index2 = 0; index2 < seriesList1.Count / 2; ++index2)
        {
          TournamentBracket.Bracket.Series series = new TournamentBracket.Bracket.Series()
          {
            id = this.nextID++,
            from1 = seriesList1[index2],
            from2 = seriesList1[p.Count - index2 - 1]
          };
          seriesList1[index2].next1 = series;
          seriesList1[p.Count - index2 - 1] = series;
          seriesList2.Add(series);
        }
        seriesList1.Clear();
        seriesList1 = seriesList2;
      }
    }
    else
    {
      for (int index3 = 0; index3 < p.Count; ++index3)
      {
        for (int index4 = index3 + 1; index4 < p.Count; ++index4)
        {
          TournamentBracket.Bracket.Series series = new TournamentBracket.Bracket.Series()
          {
            id = this.nextID++,
            player1 = p[index3],
            player2 = p[index4]
          };
          bracket.series.Add(series);
        }
      }
    }
  }

  public class Participant
  {
    public string name;
    public int seed;
  }

  public class Bracket
  {
    public List<TournamentBracket.Bracket.Series> series = new List<TournamentBracket.Bracket.Series>();
    public TournamentBracket.Bracket.Mode mode;
    public int topAdvances = 1;
    public int bestOf = 1;

    public class Series
    {
      public int id;
      public TournamentBracket.Bracket.Series from1;
      public TournamentBracket.Bracket.Series from2;
      public TournamentBracket.Bracket.Series next1;
      public TournamentBracket.Bracket.Series next2;
      public string player1;
      public string player2;
      public int wins1;
      public int wins2;
      public int draws;
      public int winner;
    }

    public enum Mode
    {
      Default,
      Round_Robin,
    }
  }

  public class Round
  {
    public List<TournamentBracket.Bracket> bracket = new List<TournamentBracket.Bracket>();
  }
}
