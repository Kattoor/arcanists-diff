// Decompiled with JetBrains decompiler
// Type: UITournamentBracket
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UITournamentBracket : MonoBehaviour
{
  [NonSerialized]
  public TournamentBracket tc = new TournamentBracket();
  public int activeRound;
  public int activeBracket;

  public static UITournamentBracket Instance { get; private set; }

  public TournamentBracket.Round _activeRound => this.tc.rounds[this.activeRound];

  public TournamentBracket.Bracket _activeBracket
  {
    get => this.tc.rounds[this.activeRound].bracket[this.activeBracket];
  }

  private void Awake() => UITournamentBracket.Instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) UITournamentBracket.Instance == (UnityEngine.Object) this))
      return;
    UITournamentBracket.Instance = (UITournamentBracket) null;
  }

  public void Start() => this.RefreshUI();

  public void RefreshUI()
  {
    if (this.activeRound >= this.tc.rounds.Count)
      this.activeRound = this.tc.rounds.Count - 1;
    if (this.tc.rounds.Count == 0)
      return;
    if (this.activeBracket >= this.tc.rounds[this.activeRound].bracket.Count)
      this.activeBracket = this.tc.rounds[this.activeRound].bracket.Count - 1;
    int count = this.tc.rounds[this.activeRound].bracket.Count;
  }
}
