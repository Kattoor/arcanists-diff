

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class LobbyMenu : Catalogue
{
  public RatedTab ratedTab;
  public RectTransform _containerGames;
  public RectTransform _containerAccounts;
  public TextMeshProUGUI txtRating;
  public TextMeshProUGUI txtName;
  public TextMeshProUGUI txtRateType;
  public RecycledScrollViewGeneric recycledGames;
  public Toggle toggleLTS;
  public Toggle toggleHTS;
  public Toggle togglePMO;
  public Toggle toggleRatedGames;
  public Toggle toggleUnratedGames;
  public GameObject panelFindingOpponents;
  public GameObject headerGames;
  public GameObject headerQueue;
  public GameObject pfabNames;
  public GameObject pfabGame;
  public GameObject pfabFriend;
  public UIOnHover butViewLobby;
  public UIOnHover butViewFriends;
  public UIOnHover butViewIgnore;
  public UIOnHover butViewClan;
  public UIOnHover butAddFriend;
  internal Viewing viewing;
  [Header("Preview")]
  public SpellImageList spellImageList;
  public GameObject buttonPoll;
  public TMP_Text txtPoll;
  public GameObject buttonPollResults;
  private float[] scrollValues = new float[4]
  {
    1f,
    1f,
    1f,
    1f
  };
  [Header("Scroll")]
  public ScrollRect scrollRect;
  public TMP_FontAsset fontArc;
  public TMP_FontAsset fontDef;
  public const string viewLTS = "prefviewlts";
  public const string viewHTS = "prefviewhts";
  public const string viewPMO = "prefviewpmo";
  public const string viewRatedGames = "prefviewratedgames";
  public const string viewUnratedGames = "prefviewunratedgames";
  private List<object> gameList = new List<object>();
  private List<List<object>> sortedSearches = new List<List<object>>();

  public static LobbyMenu instance { get; private set; }

  public void UpdateScrollValue()
  {
    this.scrollValues[LobbyMenu.GetScrollIndex(this.viewing)] = this.scrollRect.verticalNormalizedPosition;
  }

  public void SetScrollValue()
  {
    this.scrollRect.verticalNormalizedPosition = this.scrollValues[LobbyMenu.GetScrollIndex(this.viewing)];
  }

  private void Update()
  {
  }

  public IEnumerator DelayAddGame(int i)
  {
    yield return (object) new WaitForSecondsRealtime(10f);
    if (Client._NEWratedSearches.ContainsKey(i))
    {
      Client._ratedSearches.Add(i, Client._NEWratedSearches[i]);
      Client._NEWratedSearches.Remove(i);
      this.RefreshGames();
    }
  }

  private void Awake()
  {
    LobbyMenu.instance = this;
    this.UpdateOutfit();
    this.recycledGames.onScrolled = (Action<IRecycledScrollViewGenericItem>) (p =>
    {
      pfabLobbyGame pfabLobbyGame = p as pfabLobbyGame;
      if (!((UnityEngine.Object) pfabLobbyGame != (UnityEngine.Object) null) || pfabLobbyGame.GetGameFacts == null && pfabLobbyGame.GetRatedFacts == null)
        return;
      this.headerGames.SetActive(pfabLobbyGame.GetGameFacts != null);
      this.headerQueue.SetActive(pfabLobbyGame.GetRatedFacts != null);
    });
  }

  private void Start()
  {
    ChatBox.Instance.Init(ColorScheme.GetColor(Global.ColorLobbyText));
    AudioManager.PlayMusicMainMenu();
    DiscordIntergration.Instance?.UpdateActivityLobby();
    this.TogglePoll();
    Client.viewLTS = Global.GetPrefBool("prefviewlts", true);
    Client.viewHTS = Global.GetPrefBool("prefviewhts", true);
    Client.viewPMO = Global.GetPrefBool("prefviewpmo", true);
    Client.viewRatedGames = Global.GetPrefBool("prefviewratedgames", true);
    Client.viewUnratedGames = Global.GetPrefBool("prefviewunratedgames", true);
    this.toggleLTS.SetIsOnWithoutNotify(Client.viewLTS);
    this.toggleHTS.SetIsOnWithoutNotify(Client.viewHTS);
    this.togglePMO.SetIsOnWithoutNotify(Client.viewPMO);
    this.toggleRatedGames.SetIsOnWithoutNotify(Client.viewRatedGames);
    this.toggleUnratedGames.SetIsOnWithoutNotify(Client.viewUnratedGames);
    this.toggleLTS.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleViewLTS));
    this.toggleHTS.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleViewHTS));
    this.togglePMO.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleViewPMO));
    this.toggleRatedGames.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleViewRatedGames));
    this.toggleUnratedGames.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleViewUnratedGames));
    this.RefreshGames();
    this.ViewLobby();
  }

  public void TogglePoll()
  {
    this.buttonPoll.SetActive(Client.pollID >= 0 && Client.MyAccount.poll != Client.pollID);
    this.buttonPollResults.SetActive(!string.IsNullOrEmpty(Client.pollName) && (Client.pollID < 0 || Client.MyAccount.poll == Client.pollID));
    this.txtPoll.text = Client.pollName;
  }

  public void ToggleViewLTS(bool v)
  {
    Client.viewLTS = v;
    Global.SetPrefBool("prefviewlts", Client.viewLTS);
    this.RefreshGames();
  }

  public void ToggleViewHTS(bool v)
  {
    Client.viewHTS = v;
    Global.SetPrefBool("prefviewhts", Client.viewHTS);
    this.RefreshGames();
  }

  public void ToggleViewPMO(bool v)
  {
    Client.viewPMO = v;
    Global.SetPrefBool("prefviewpmo", Client.viewPMO);
    this.RefreshGames();
  }

  public void ToggleViewRatedGames(bool v)
  {
    Client.viewRatedGames = v;
    Global.SetPrefBool("prefviewratedgames", Client.viewRatedGames);
    this.RefreshGames();
  }

  public void ToggleViewUnratedGames(bool v)
  {
    Client.viewUnratedGames = v;
    Global.SetPrefBool("prefviewunratedgames", Client.viewUnratedGames);
    this.RefreshGames();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) LobbyMenu.instance == (UnityEngine.Object) this))
      return;
    LobbyMenu.instance = (LobbyMenu) null;
  }

  public void ClickPoll()
  {
    if (Client.MyAccount.poll == Client.pollID)
      this.ClickPollResults();
    else
      Client.Ask((byte) 90, (byte) 1);
  }

  public void ClickPollResults() => Client.Ask((byte) 90, (byte) 4);

  public void ClickAddFriend()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator("Name of Friend");
    myContextMenu.AddInput((Action<string>) (s =>
    {
      if (string.IsNullOrEmpty(s) || s.Length > 13)
        return;
      Client.AskToAddFriend(true, true, s);
    }));
    myContextMenu.Rebuild();
  }

  public void ClickSettings() => Controller.ShowSettingsMenu();

  public void ClickPrestige() => Controller.ShowPrestigeMenu();

  public static void DestroyChildern(RectTransform p)
  {
    for (int index = p.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) p.GetChild(index).gameObject);
  }

  public static int GetScrollIndex(Viewing v)
  {
    switch (v)
    {
      case Viewing.Lobby:
        return 0;
      case Viewing.Friends:
        return 1;
      case Viewing.Ignore:
        return 2;
      case Viewing.Clan:
        return 3;
      default:
        return 0;
    }
  }

  public void RefreshFriends(bool updatePos = false)
  {
    if (this.viewing != Viewing.Friends)
      return;
    this.txtRating.text = "Friends " + (object) Client.friends.Count + " / 200";
    this.txtRateType.text = "";
    int num = (int) ((RectTransform) this.pfabFriend.transform).sizeDelta.y + 2;
    int x = 2;
    List<(string, int)> valueTupleList = new List<(string, int)>();
    foreach (string friend in Client.friends)
      valueTupleList.Add((friend, Client.GetAccount(friend).location.Online() ? 1 : 0));
    valueTupleList.Sort((Comparison<(string, int)>) ((a, b) => a.online == b.online ? a.name.CompareTo(b.name) : b.online - a.online));
    this._containerAccounts.DestroyChildern();
    this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, (float) (num * valueTupleList.Count + 35));
    for (int index = 0; index < valueTupleList.Count; ++index)
    {
      GameObject andApply = Controller.Instance.CreateAndApply(this.pfabFriend, (Transform) this._containerAccounts);
      ((RectTransform) andApply.transform).anchoredPosition = new Vector2((float) x, (float) (-index * num - 2 - 35));
      pfabName component = andApply.GetComponent<pfabName>();
      component.Setup(valueTupleList[index].Item1);
      component.ready.color = pfabName.colorStatus[valueTupleList[index].Item2];
      component.txtName.color = pfabName.colorStatus[valueTupleList[index].Item2];
    }
    if (!updatePos)
      return;
    this.SetScrollValue();
  }

  public void RefreshIgnore(bool updatePos = false)
  {
    if (this.viewing != Viewing.Ignore)
      return;
    this.txtRating.text = "Ignored " + (object) Client.ignore.Count + " / 100";
    this.txtRateType.text = "";
    int num = (int) ((RectTransform) this.pfabFriend.transform).sizeDelta.y + 2;
    int x = 2;
    List<string> stringList = new List<string>();
    foreach (string str in Client.ignore)
      stringList.Add(str);
    stringList.Sort();
    List<Client.TempIgnored.Who> whoList = new List<Client.TempIgnored.Who>();
    foreach (Client.TempIgnored.Who who in Client.tempIgnored.list)
      whoList.Add(who);
    whoList.Sort((Comparison<Client.TempIgnored.Who>) ((a, b) => a.name.CompareTo(b.name)));
    this._containerAccounts.DestroyChildern();
    this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, (float) (num * (stringList.Count + whoList.Count)));
    for (int index = 0; index < stringList.Count; ++index)
    {
      GameObject andApply = Controller.Instance.CreateAndApply(this.pfabFriend, (Transform) this._containerAccounts);
      ((RectTransform) andApply.transform).anchoredPosition = new Vector2((float) x, (float) (-index * num - 2));
      andApply.GetComponent<pfabName>().Setup(stringList[index]);
    }
    for (int index = 0; index < whoList.Count; ++index)
    {
      GameObject andApply = Controller.Instance.CreateAndApply(this.pfabFriend, (Transform) this._containerAccounts);
      ((RectTransform) andApply.transform).anchoredPosition = new Vector2((float) x, (float) ((-index - stringList.Count) * num - 2));
      andApply.GetComponent<pfabName>().SetupIgnored(whoList[index].name, whoList[index].TimeTill());
    }
    if (!updatePos)
      return;
    this.SetScrollValue();
  }

  public void RefreshClan(bool updatePos = false)
  {
    if (this.viewing != Viewing.Clan)
      return;
    this.txtRateType.text = "Role";
    if (Client.clan == null)
    {
      this.txtRating.text = "You're not in a clan :(";
      this._containerAccounts.DestroyChildern();
      this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, 20f);
    }
    else
    {
      this.txtRating.text = "Clan " + (object) Client.clan.members.Count + " / 100";
      int num = (int) ((RectTransform) this.pfabFriend.transform).sizeDelta.y + 2;
      int x = 2;
      List<Clan.MemberX> memberXList = new List<Clan.MemberX>();
      foreach (KeyValuePair<string, Clan.Member> member in Client.clan.members)
        memberXList.Add(new Clan.MemberX()
        {
          name = member.Value.name,
          role = member.Value.role,
          acc = Client.GetAccount(member.Value.name)
        });
      memberXList.Sort((Comparison<Clan.MemberX>) ((a, b) => b.acc.location.Online() == a.acc.location.Online() ? (b.role == a.role ? a.name.CompareTo(b.name) : (int) (b.role - a.role)) : (!b.acc.location.Online() ? -1 : 1)));
      this._containerAccounts.DestroyChildern();
      this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, (float) (num * memberXList.Count + 2));
      for (int index = 0; index < memberXList.Count; ++index)
      {
        GameObject andApply = Controller.Instance.CreateAndApply(this.pfabFriend, (Transform) this._containerAccounts);
        ((RectTransform) andApply.transform).anchoredPosition = new Vector2((float) x, (float) (-index * num - 2));
        pfabName component = andApply.GetComponent<pfabName>();
        component.Setup(memberXList[index].name);
        component.txtRating.text = "(" + memberXList[index].role.ToString().Replace('_', ' ') + ")";
        component.ready.color = pfabName.colorStatus[memberXList[index].acc.location.Online() ? 1 : 0];
        component.txtName.color = pfabName.colorStatus[memberXList[index].acc.location.Online() ? 1 : 0];
      }
      if (!updatePos)
        return;
      this.SetScrollValue();
    }
  }

  [ContextMenu("Add")]
  public void AddAccount()
  {
    for (int count = Client._lobby.Count; count < 13; ++count)
      Client._lobby.Add("temp" + (object) Client._lobby.Count, "temp" + (object) Client._lobby.Count);
    this.RefreshNames();
  }

  [ContextMenu("Remove")]
  public void RemoveAccount()
  {
    Client._lobby.RemoveAt(Client._lobby.Count - 1);
    this.RefreshNames();
  }

  public void RefreshActive()
  {
    this.RefreshNames();
    this.RefreshFriends();
    this.RefreshIgnore();
    this.RefreshClan();
  }

  public void RefreshNames(bool updatePos = false)
  {
    if (this.viewing != Viewing.Lobby)
      return;
    Client.GetAccount(Client.Name);
    PrestigeLobbyUI.Rating((TMP_Text) this.txtRating);
    this.txtRateType.text = "Rating";
    if ((UnityEngine.Object) this.txtName != (UnityEngine.Object) null)
      this.txtName.text = Client.Name;
    int num1 = (int) ((RectTransform) this.pfabNames.transform).sizeDelta.y + 2;
    int x = 2;
    int index1 = 0;
    this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, (float) (num1 * Client._lobby.Count));
    for (; index1 < Client._lobby.Count && index1 < this._containerAccounts.childCount; ++index1)
      this._containerAccounts.GetChild(index1).GetComponent<pfabName>().Setup(Client.GetAccount(Client._lobby.GetItem(index1).Key));
    for (; index1 < Client._lobby.Count; ++index1)
    {
      GameObject andApply = Controller.Instance.CreateAndApply(this.pfabNames, (Transform) this._containerAccounts);
      ((RectTransform) andApply.transform).anchoredPosition = new Vector2((float) x, (float) (-index1 * num1 - 2));
      andApply.GetComponent<pfabName>().Setup(Client.GetAccount(Client._lobby.GetItem(index1).Key));
    }
    int num2 = index1;
    for (int index2 = this._containerAccounts.childCount - 1; index2 >= num2; --index2)
      UnityEngine.Object.Destroy((UnityEngine.Object) this._containerAccounts.GetChild(index2).gameObject);
    if (!updatePos)
      return;
    this.SetScrollValue();
  }

  public void ShowToolTip(string s) => MyToolTip.Show(s);

  public void HideToolTip() => MyToolTip.Close();

  public void InitLists()
  {
    if (this.sortedSearches.Count == 0)
    {
      this.sortedSearches.Add(new List<object>());
      this.sortedSearches.Add(new List<object>());
      this.sortedSearches.Add(new List<object>());
    }
    this.gameList.Clear();
    foreach (List<object> sortedSearch in this.sortedSearches)
      sortedSearch.Clear();
  }

  public void ApplyLists()
  {
    switch (Client.gameType)
    {
      case ZGame.GameType.LowStandard:
        foreach (object obj in this.sortedSearches[0])
          this.gameList.Add(obj);
        foreach (object obj in this.sortedSearches[1])
          this.gameList.Add(obj);
        using (List<object>.Enumerator enumerator = this.sortedSearches[2].GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.gameList.Add(enumerator.Current);
          break;
        }
      case ZGame.GameType.HighStandard:
        foreach (object obj in this.sortedSearches[1])
          this.gameList.Add(obj);
        foreach (object obj in this.sortedSearches[0])
          this.gameList.Add(obj);
        using (List<object>.Enumerator enumerator = this.sortedSearches[2].GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.gameList.Add(enumerator.Current);
          break;
        }
      default:
        foreach (object obj in this.sortedSearches[2])
          this.gameList.Add(obj);
        foreach (object obj in this.sortedSearches[0])
          this.gameList.Add(obj);
        using (List<object>.Enumerator enumerator = this.sortedSearches[1].GetEnumerator())
        {
          while (enumerator.MoveNext())
            this.gameList.Add(enumerator.Current);
          break;
        }
    }
  }

  public void RefreshGames()
  {
    Vector2 sizeDelta = ((RectTransform) this.pfabGame.transform).sizeDelta;
    Client._games.SortValues((Comparison<GameFacts>) ((a, b) => (a.status == (byte) 0 ? a.id : a.id + 10000) - (b.status == (byte) 0 ? b.id : b.id + 10000)));
    this.InitLists();
    foreach (KeyValuePair<int, GameFacts> game in Client._games)
    {
      if ((game.Value.GetRatedMode() && Client.viewRatedGames || !game.Value.GetRatedMode() && Client.viewUnratedGames) && game.Value.status == (byte) 0)
        this.gameList.Add((object) game.Value);
    }
    if (Client.viewRatedGames)
    {
      foreach (KeyValuePair<int, RatedContainer> ratedSearch in Client._ratedSearches)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        foreach (RatedFacts ratedFacts in ratedSearch.Value.list)
        {
          RatedFacts y = ratedFacts;
          if (y.gameType == 0 && Client.viewLTS && !flag1 || y.gameType == 1 && Client.viewHTS && !flag2 || y.gameType == 2 && Client.viewPMO && !flag3)
          {
            if (y.gameType == 0)
              flag1 = true;
            else if (y.gameType == 1)
              flag2 = true;
            else
              flag3 = true;
            if (this.sortedSearches[y.gameType].Find((Predicate<object>) (z => z.Equals((object) y))) == null)
              this.sortedSearches[y.gameType].Add((object) y);
          }
        }
      }
    }
    this.ApplyLists();
    foreach (KeyValuePair<int, GameFacts> game in Client._games)
    {
      if ((game.Value.GetRatedMode() && Client.viewRatedGames || !game.Value.GetRatedMode() && Client.viewUnratedGames) && game.Value.status != (byte) 0)
        this.gameList.Add((object) game.Value);
    }
    this.recycledGames.Set((IEnumerable<object>) this.gameList);
  }

  public void AddGame(GameFacts game) => this.RefreshGames();

  public void AddName(Account a)
  {
    if (this.viewing != Viewing.Lobby)
      return;
    this.RefreshNames();
  }

  public void ViewLobby()
  {
    this.UpdateScrollValue();
    this.viewing = Viewing.Lobby;
    this.butViewLobby.AlwaysOn = true;
    this.butViewFriends.AlwaysOn = false;
    this.butViewIgnore.AlwaysOn = false;
    this.butViewClan.AlwaysOn = false;
    this.butAddFriend.gameObject.SetActive(this.butViewFriends.AlwaysOn);
    this._containerAccounts.DestroyChildern();
    this.RefreshNames(true);
  }

  public void ViewFriends()
  {
    this.UpdateScrollValue();
    this.viewing = Viewing.Friends;
    this.butViewLobby.AlwaysOn = false;
    this.butViewFriends.AlwaysOn = true;
    this.butViewIgnore.AlwaysOn = false;
    this.butViewClan.AlwaysOn = false;
    this.butAddFriend.gameObject.SetActive(this.butViewFriends.AlwaysOn);
    this.RefreshFriends(true);
  }

  public void ViewIgnore()
  {
    this.UpdateScrollValue();
    this.viewing = Viewing.Ignore;
    this.butViewLobby.AlwaysOn = false;
    this.butViewFriends.AlwaysOn = false;
    this.butViewIgnore.AlwaysOn = true;
    this.butViewClan.AlwaysOn = false;
    this.butAddFriend.gameObject.SetActive(this.butViewFriends.AlwaysOn);
    this.RefreshIgnore(true);
  }

  public void ViewClan()
  {
    this.UpdateScrollValue();
    this.viewing = Viewing.Clan;
    this.butViewLobby.AlwaysOn = false;
    this.butViewFriends.AlwaysOn = false;
    this.butViewIgnore.AlwaysOn = false;
    this.butViewClan.AlwaysOn = true;
    this.butAddFriend.gameObject.SetActive(false);
    this.RefreshClan(true);
  }

  public void UpdateOnlineUI(string n)
  {
    if (this.viewing == Viewing.Friends && Client.HasFriend(n))
    {
      this.RefreshFriends();
    }
    else
    {
      if (this.viewing != Viewing.Clan || Client.clan == null || !Client.clan.members.ContainsKey(n))
        return;
      this.RefreshClan();
    }
  }

  public void UpdateGame(GameFacts g) => this.RefreshGames();

  public void UpdateName(Account a) => this.RefreshNames();

  public void ClickMainMenu() => Client.AskToGoToMainMenu();

  public void ClickRated() => this.ratedTab.gameObject.SetActive(true);

  public void ClickRatedLobby() => Client.AskToCreateRatedLobby();

  public void ClickUnrated() => Client.AskToCreateGame();

  public void UpdateSpellIcons() => this.spellImageList.SetSpells();

  public void UpdateOutfit()
  {
    this.spellImageList.UpdateOutfit();
    PrestigeLobbyUI.Instance?.RefreshOutfit();
  }

  public void ClickJoinGame(int id)
  {
  }
}
