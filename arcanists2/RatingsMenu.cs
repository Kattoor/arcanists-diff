// Decompiled with JetBrains decompiler
// Type: RatingsMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable disable
public class RatingsMenu : MonoBehaviour
{
  public RectTransform mainBox;
  public RectTransform bg;
  public RectTransform container;
  public pfabRankingLine pfab;
  public pfabRankingLine spacers;
  public pfabRankingLine headers;
  public TMP_Text headerWhich;
  public GameObject loading;
  public GameObject infoTxt;
  public static List<RatingLineInfo> cachedFriends;
  public static List<RatingLineInfo> cachedGlobal;
  public static AllRankingsContainer cached;
  public static AllRankingsContainer selfRank;
  public static AllRankingsContainer nameRank;
  public RatingsMenu.Sorter sorter;
  public RecycledScrollViewGeneric recycled;
  public int accuracy;
  private bool obtainFriends;
  private bool obtainGlobal;
  private static DateTime nextStatGet;
  public static readonly string[] Accuracy = new string[3]
  {
    "0",
    "0.0",
    "0.00"
  };

  public static RatingsMenu Instance { get; private set; }

  private void Awake()
  {
    RatingsMenu.Instance = this;
    this.CheckCacheTime();
    this.ResizeContainer();
    this.CheckActive();
    this.accuracy = Mathf.Clamp(PlayerPrefs.GetInt("prefrateaccuracy", 0), 0, 2);
  }

  public static void OnLogout()
  {
    RatingsMenu.cached = (AllRankingsContainer) null;
    RatingsMenu.selfRank = (AllRankingsContainer) null;
    RatingsMenu.nameRank = (AllRankingsContainer) null;
    RatingsMenu.cachedFriends = (List<RatingLineInfo>) null;
    RatingsMenu.cachedGlobal = (List<RatingLineInfo>) null;
    RatingsMenu.nextStatGet = new DateTime(1, 1, 1);
  }

  private void Start() => this.ClickDefault();

  public void ClickToggleItems() => this.pfab.ShowContextMenu();

  public void ResizeContainer()
  {
    int num1 = -1;
    int num2 = PlayerPrefs.GetInt("prefshowrankings", 0);
    for (int index = 0; index <= 7; ++index)
      num1 += (num2 & 1 << index) != 0 ? 1 : 0;
    this.mainBox.sizeDelta = new Vector2((float) (1559 - (7 - num1) * 92), this.mainBox.sizeDelta.y);
    this.bg.sizeDelta = new Vector2(779.5f - (float) ((7 - num1) * 46), this.bg.sizeDelta.y);
  }

  public void CheckActive()
  {
    int x = PlayerPrefs.GetInt("prefshowrankings", 0);
    this.pfab.ToggleItems(x);
    this.spacers.ToggleItems(x);
    this.headers.ToggleItems(x);
    foreach (IRecycledScrollViewGenericItem getPfab in this.recycled.GetPfabs)
    {
      pfabRankingLine pfabRankingLine = getPfab as pfabRankingLine;
      if ((UnityEngine.Object) pfabRankingLine != (UnityEngine.Object) null)
        pfabRankingLine.ToggleItems(x);
    }
    this.ResizeContainer();
  }

  public void CheckCacheTime()
  {
    if ((RatingsMenu.nextStatGet - DateTime.Now).TotalHours > 0.0)
      return;
    RatingsMenu.cached = (AllRankingsContainer) null;
    RatingsMenu.nextStatGet = Global.GetNearestHour().AddHours(1.0);
  }

  public IEnumerator TryAgain()
  {
    yield return (object) new WaitForSecondsRealtime(5f);
    Client.AskGetRank();
  }

  public void HideToolTip() => MyToolTip.Close();

  public void ClickMyStats()
  {
    if (RatingsMenu.selfRank == null || RatingsMenu.selfRank.low.Count != 2)
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem(this.GetStats(), (Action) null, (Color) ColorScheme.GetColor(MyContextMenu.ColorCream));
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Copy to clipboard", new Action(this.ClickCopyStats), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.Rebuild();
  }

  public void ClickAccuracy()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem("0", (Action) (() => this.SetAccuracy(0)), (Color) ColorScheme.GetColor(MyContextMenu.ColorCream));
    myContextMenu.AddItem("0.0", (Action) (() => this.SetAccuracy(1)), (Color) ColorScheme.GetColor(MyContextMenu.ColorCream));
    myContextMenu.AddItem("0.00", (Action) (() => this.SetAccuracy(2)), (Color) ColorScheme.GetColor(MyContextMenu.ColorCream));
    myContextMenu.Rebuild();
  }

  private void SetAccuracy(int i)
  {
    this.accuracy = i;
    PlayerPrefs.GetInt("prefrateaccuracy", i);
    this.ClickDefault();
  }

  public void ClickCopyStats() => Global.systemCopyBuffer = this.GetStats();

  private string GetStats()
  {
    NewGamesPlayed newGamesPlayed = new NewGamesPlayed();
    newGamesPlayed.Combine(RatingsMenu.selfRank.low[0].games);
    newGamesPlayed.Combine(RatingsMenu.selfRank.low[1].games);
    newGamesPlayed.Combine(RatingsMenu.selfRank.high[0].games);
    newGamesPlayed.Combine(RatingsMenu.selfRank.party[0].games);
    return newGamesPlayed.ToString(Client.MyAccount, RatingsMenu.selfRank.low[0].games.rating.ToString() + "/" + (object) RatingsMenu.selfRank.high[0].games.rating + "/" + (object) RatingsMenu.selfRank.party[0].games.rating + "/" + (object) RatingsMenu.selfRank.low[1].games.rating) + "\nTotal Unrated Games: " + (object) RatingsMenu.selfRank.high[1].TotalRatedGames1;
  }

  public void ClickDefault()
  {
    switch (PlayerPrefs.GetInt("ranktype", 0))
    {
      case 0:
        this.ClickLow();
        break;
      case 1:
        this.ClickHigh();
        break;
      default:
        this.ClickParty();
        break;
    }
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) RatingsMenu.Instance == (UnityEngine.Object) this))
      return;
    RatingsMenu.Instance = (RatingsMenu) null;
  }

  public void ClickMainMenu() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  [EnumAction(typeof (RatingsMenu.Sorter))]
  public void ClickSorter(int i)
  {
    this.sorter = (RatingsMenu.Sorter) i;
    this.Start();
  }

  private void Sort<T>(List<T> cache) where T : IJsonGames
  {
    this.infoTxt.SetActive(false);
    switch (this.sorter)
    {
      case RatingsMenu.Sorter.Name:
        this.headerWhich.text += " (sorted alphabetically)";
        cache.Sort((Comparison<T>) ((a, b) => a.name.CompareTo(b.name)));
        break;
      case RatingsMenu.Sorter.Rating:
        this.headerWhich.text += " (by rating)";
        cache.Sort((Comparison<T>) ((a, b) => a.rank - b.rank));
        break;
      case RatingsMenu.Sorter.Played:
        this.headerWhich.text += " (by games played)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.totalRatedGames - a.games.totalRatedGames));
        break;
      case RatingsMenu.Sorter.Win:
        this.headerWhich.text += " (by games won)";
        this.infoTxt.SetActive(true);
        cache.Sort((Comparison<T>) ((a, b) => (int) ((b.games.totalRatedGames > 50 ? (double) b.games.GetWinPercent() : (double) b.games.GetWinPercent() - 66666664.0) - (a.games.totalRatedGames > 50 ? (double) a.games.GetWinPercent() : (double) a.games.GetWinPercent() - 66666664.0))));
        break;
      case RatingsMenu.Sorter.Draw:
        this.headerWhich.text += " (by games drawn)";
        this.infoTxt.SetActive(true);
        cache.Sort((Comparison<T>) ((a, b) => (int) ((b.games.totalRatedGames > 50 ? (double) b.games.GetDrawPercent() : (double) b.games.GetDrawPercent() - 66666664.0) - (a.games.totalRatedGames > 50 ? (double) a.games.GetDrawPercent() : (double) a.games.GetDrawPercent() - 66666664.0))));
        break;
      case RatingsMenu.Sorter.Lost:
        this.headerWhich.text += " (by games lost)";
        this.infoTxt.SetActive(true);
        cache.Sort((Comparison<T>) ((a, b) => (int) ((b.games.totalRatedGames > 50 ? (double) b.games.GetLossPercent() : (double) b.games.GetLossPercent() - 66666664.0) - (a.games.totalRatedGames > 50 ? (double) a.games.GetLossPercent() : (double) a.games.GetLossPercent() - 66666664.0))));
        break;
      case RatingsMenu.Sorter.Damage:
        this.headerWhich.text += " (by damage)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.damageDealt.CompareTo(a.games.damageDealt)));
        break;
      case RatingsMenu.Sorter.Kills:
        this.headerWhich.text += " (by kills)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.kills.CompareTo(a.games.kills)));
        break;
      case RatingsMenu.Sorter.WinStreak:
        this.headerWhich.text += " (by longest win streak)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.longestWinningStreak.CompareTo(a.games.longestWinningStreak)));
        break;
      case RatingsMenu.Sorter.CurWinStreak:
        this.headerWhich.text += " (by current win streak)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.currentWinningStreak.CompareTo(a.games.currentWinningStreak)));
        break;
      case RatingsMenu.Sorter.Survived:
        this.headerWhich.text += " (by games survived)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.gamesSurvived.CompareTo(a.games.gamesSurvived)));
        break;
      case RatingsMenu.Sorter.FirstTurn:
        this.headerWhich.text += " (by first turn percentage)";
        this.infoTxt.SetActive(true);
        cache.Sort((Comparison<T>) ((a, b) => (b.games.totalRatedGames > 50 ? Global.Divide((float) b.games.gamesWentFirst, (float) (b.games.totalRatedGames - b.games.teamGames)) : Global.Divide((float) b.games.gamesWentFirst, (float) (b.games.totalRatedGames - b.games.teamGames)) - 66666664f).CompareTo(a.games.totalRatedGames > 50 ? Global.Divide((float) a.games.gamesWentFirst, (float) (a.games.totalRatedGames - a.games.teamGames)) : Global.Divide((float) a.games.gamesWentFirst, (float) (a.games.totalRatedGames - a.games.teamGames)) - 66666664f)));
        break;
      case RatingsMenu.Sorter.FTW:
        this.headerWhich.text += " (by first turn win percentage)";
        this.infoTxt.SetActive(true);
        cache.Sort((Comparison<T>) ((a, b) => (b.games.totalRatedGames > 50 ? Global.Divide((float) b.games.gamesWentFirstWon, (float) b.games.gamesWentFirst) : Global.Divide((float) b.games.gamesWentFirstWon, (float) b.games.gamesWentFirst) - 66666664f).CompareTo(a.games.totalRatedGames > 50 ? Global.Divide((float) a.games.gamesWentFirstWon, (float) a.games.gamesWentFirst) : Global.Divide((float) a.games.gamesWentFirstWon, (float) a.games.gamesWentFirst) - 66666664f)));
        break;
      case RatingsMenu.Sorter.Teams:
        this.headerWhich.text += " (by team games)";
        cache.Sort((Comparison<T>) ((a, b) => b.games.teamGames.CompareTo(a.games.teamGames)));
        break;
    }
  }

  public void ClickLow()
  {
    this.headerWhich.text = "Low Time Standard";
    PlayerPrefs.SetInt("ranktype", 0);
    if (RatingsMenu.cached == null)
    {
      if (this.obtainGlobal)
        return;
      this.loading.SetActive(true);
      this.obtainGlobal = true;
      Client.AskGetRank();
    }
    else
      this.Populate<JsonGamesLow>(RatingsMenu.cached.low, RatingsMenu.selfRank?.low);
  }

  public void ClickHigh()
  {
    this.headerWhich.text = "High Time Standard";
    PlayerPrefs.SetInt("ranktype", 1);
    if (RatingsMenu.cached == null)
    {
      if (this.obtainGlobal)
        return;
      this.loading.SetActive(true);
      this.obtainGlobal = true;
      Client.AskGetRank();
    }
    else
      this.Populate<JsonGamesHigh>(RatingsMenu.cached.high, RatingsMenu.selfRank?.high);
  }

  public void ClickParty()
  {
    this.headerWhich.text = "Party Mode";
    PlayerPrefs.SetInt("ranktype", 2);
    if (RatingsMenu.cached == null)
    {
      if (this.obtainGlobal)
        return;
      this.loading.SetActive(true);
      this.obtainGlobal = true;
      Client.AskGetRank();
    }
    else
      this.Populate<JsonGamesParty>(RatingsMenu.cached.party, RatingsMenu.selfRank?.party);
  }

  public void Populate<T>(List<T> cache, List<T> self) where T : IJsonGames
  {
    this.loading.SetActive(false);
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (30 * cache.Count));
    this.container.DestroyChildern();
    this.Sort<T>(cache);
    this.recycled.Clear();
    List<object> data = new List<object>();
    foreach (T obj in cache)
    {
      IJsonGames jsonGames = (IJsonGames) obj;
      if (string.Equals(jsonGames.name, Client.Name) && self != null && self.Count > 0)
        self[0].rank = jsonGames.rank;
      data.Add((object) jsonGames);
    }
    this.recycled.Set((IEnumerable<object>) data);
    if (self != null && self.Count > 0)
      this.Set(this.pfab, (IJsonGames) self[0]);
    this.recycled.Scroll(1f);
    this.recycled._chatScrollbar.normalizedPosition = new Vector2(0.0f, 1f);
    this.CheckActive();
  }

  public void Set(pfabRankingLine g, IJsonGames c)
  {
    g.txtName.text = c.rank > 0 ? c.rank.ToString() + ". " + c.name : c.name;
    g.txtRating.text = c.games.rating < (short) 0 ? "-" : c.games.rating.ToString();
    g.txtPlayed.text = c.games.totalRatedGames.ToString();
    if (c.games.totalRatedGames == 0)
    {
      g.txtWon.text = "0%";
      g.txtLost.text = "0%";
      g.txtDraw.text = "0%";
      g.txtDamage.text = "0";
      g.txtKills.text = "0";
      g.txtWinStreak.text = "0";
      g.txtCurWinStreak.text = "0";
      g.txtSurvived.text = "0";
      g.txtFirstTurn.text = "0%";
      g.txtFTW.text = "0%";
      g.txtTeam.text = "0";
    }
    else
    {
      string format = RatingsMenu.Accuracy[this.accuracy];
      TMP_Text txtWon = g.txtWon;
      float num = (float) (c.games.ratedGamesWon * 100) / (float) c.games.totalRatedGames;
      string str1 = num.ToString(format) + "%";
      txtWon.text = str1;
      TMP_Text txtLost = g.txtLost;
      num = (float) ((c.games.totalRatedGames - c.games.ratedGamesWon - c.games.draws) * 100) / (float) c.games.totalRatedGames;
      string str2 = num.ToString(format) + "%";
      txtLost.text = str2;
      TMP_Text txtDraw = g.txtDraw;
      num = (float) (c.games.draws * 100) / (float) c.games.totalRatedGames;
      string str3 = num.ToString(format) + "%";
      txtDraw.text = str3;
      g.txtDamage.text = c.games.damageDealt.ToString();
      g.txtKills.text = c.games.kills.ToString();
      g.txtWinStreak.text = c.games.longestWinningStreak.ToString();
      g.txtCurWinStreak.text = c.games.currentWinningStreak.ToString();
      g.txtSurvived.text = c.games.gamesSurvived.ToString();
      TMP_Text txtFirstTurn = g.txtFirstTurn;
      num = Global.Divide((float) c.games.gamesWentFirst, (float) (c.games.totalRatedGames - c.games.teamGames)) * 100f;
      string str4 = num.ToString(format) + "%";
      txtFirstTurn.text = str4;
      TMP_Text txtFtw = g.txtFTW;
      num = Global.Divide((float) c.games.gamesWentFirstWon, (float) c.games.gamesWentFirst) * 100f;
      string str5 = num.ToString(format) + "%";
      txtFtw.text = str5;
      g.txtTeam.text = c.games.teamGames.ToString();
    }
  }

  public enum Sorter
  {
    Name = -1, // 0xFFFFFFFF
    Rating = 0,
    Played = 1,
    Win = 2,
    Draw = 3,
    Lost = 4,
    Damage = 5,
    Kills = 6,
    WinStreak = 7,
    CurWinStreak = 8,
    Survived = 9,
    FirstTurn = 10, // 0x0000000A
    FTW = 11, // 0x0000000B
    Teams = 12, // 0x0000000C
  }
}
