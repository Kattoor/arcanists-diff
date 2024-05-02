

using Hazel;
using Hazel.Tcp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class RatedMenu : MonoBehaviour
{
  public GameObject[] lowGameObjects;
  public GameObject[] highGameObjects;
  public GameObject[] neitherGameObjects;
  public RectTransform _containerAccounts;
  public TMP_Text txtRating;
  public TextMeshProUGUI txtRateType;
  public GameObject panelInfoAltSettings;
  public Button buttonStartGame;
  public TMP_Text txtStartGame;
  public GameObject pfabFriend;
  public UIOnHover butViewLobby;
  public UIOnHover butViewFriends;
  public UIOnHover butViewIgnore;
  public UIOnHover butViewClan;
  public UIOnHover butAddFriend;
  public GameObject tipsObj;
  internal Viewing viewing;
  public UIOnHover[] groupLadder;
  public UIOnHover[] groupPlayers;
  public UIOnHover[] groupTurnTime;
  public UIOnHover[] groupMapStyle;
  public UIOnHover[] groupArmageddon;
  public UIOnHover[] groupTeams;
  public UIOnHover[] groupGameStyles;
  [Header("Alternate settings")]
  public SpellImageList spellOverrides;
  public GameObject panelSpellOverrides;
  public UIOnHover[] alternateSettings;
  [Header("Preview")]
  public SpellImageList spellImageList;
  private bool findingOpponents;
  [Header("Disco")]
  public RectTransform discoBall;
  public Image discoPfab;
  public AudioClip discoMusic;
  public PoolColoredImage pool;
  private int curRatedFacts;
  private float[] scrollValues = new float[4]
  {
    1f,
    1f,
    1f,
    1f
  };
  [Header("Scroll")]
  public ScrollRect scrollRect;
  private bool closeDisco;
  private int periods;
  private float _time;
  private string[] period = new string[4]
  {
    "Searching",
    "Searching.",
    "Searching..",
    "Searching..."
  };

  public static RatedMenu instance { get; private set; }

  public static bool FindingOpponents
  {
    get => (UnityEngine.Object) RatedMenu.instance != (UnityEngine.Object) null && RatedMenu.instance.findingOpponents;
  }

  private RatedFacts _ratedFacts
  {
    get
    {
      return Client._ratedFacts.list[Mathf.Clamp(this.curRatedFacts, 0, Client._ratedFacts.list.Count - 1)];
    }
  }

  public void UpdateScrollValue()
  {
    this.scrollValues[LobbyMenu.GetScrollIndex(this.viewing)] = this.scrollRect.verticalNormalizedPosition;
  }

  public void SetScrollValue()
  {
    this.scrollRect.verticalNormalizedPosition = this.scrollValues[LobbyMenu.GetScrollIndex(this.viewing)];
  }

  [ContextMenu("Add Current Settings to Que")]
  public void AddToQue()
  {
    Server._ratedQueue.Add((Connection) new TcpConnection(), Client._ratedFacts);
  }

  [ContextMenu("Find Matches")]
  public void SearchQue() => Server.FindRatedMatches();

  [ContextMenu("Remove All")]
  public void removeall() => Server._ratedQueue.Clear();

  private void Update()
  {
    this._time += Time.deltaTime;
    if ((double) this._time <= 1.0)
      return;
    this._time = 0.0f;
    if (string.Equals("FIND OPPONENT", this.txtStartGame.text))
      return;
    ++this.periods;
    if (this.periods > 3)
      this.periods = 0;
    this.txtStartGame.text = this.period[this.periods];
  }

  private void Awake()
  {
    RatedMenu.instance = this;
    this.UpdateOutfit();
    this.UpdateSpellIcons();
  }

  private void Start()
  {
    this.ViewLobby();
    this.RefreshGameOptions();
    ChatBox.Instance.Init(ColorScheme.GetColor(Global.ColorGameText));
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) RatedMenu.instance == (UnityEngine.Object) this))
      return;
    RatedMenu.instance = (RatedMenu) null;
  }

  public void ClickDiscoClose() => this.closeDisco = true;

  private IEnumerator IEDisco()
  {
    this.closeDisco = false;
    float cur = 0.0f;
    while ((double) cur < 60.0)
    {
      cur += Time.deltaTime;
      if (!this.findingOpponents)
        yield break;
      else
        yield return (object) 0;
    }
    this.discoBall.gameObject.SetActive(true);
    AudioManager.PlayMusic(this.discoMusic);
    while (this.findingOpponents && (double) this.discoBall.anchoredPosition.y > -100.0)
    {
      Vector2 anchoredPosition = this.discoBall.anchoredPosition;
      anchoredPosition.y -= Time.deltaTime * 100f;
      if ((double) anchoredPosition.y < -100.0)
        anchoredPosition.y = -100f;
      this.discoBall.anchoredPosition = anchoredPosition;
      yield return (object) null;
      yield return (object) null;
    }
    int i = 0;
    while (this.findingOpponents && !this.closeDisco)
    {
      ++i;
      if (i >= 1)
      {
        i = 0;
        Image image = this.pool.Spawn();
        image.rectTransform.anchoredPosition = new Vector2((float) UnityEngine.Random.Range(-20, 20), (float) UnityEngine.Random.Range(-20, 20));
        image.rectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) UnityEngine.Random.Range(0, 360));
        image.color = new Color(UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f), UnityEngine.Random.Range(0.5f, 1f), 0.25f);
        image.gameObject.SetActive(true);
      }
      if (Input.GetMouseButtonDown(0))
        this.closeDisco = true;
      yield return (object) null;
    }
    while ((double) this.discoBall.anchoredPosition.y < 0.0)
    {
      Vector2 anchoredPosition = this.discoBall.anchoredPosition;
      anchoredPosition.y += Time.deltaTime * 100f;
      this.discoBall.anchoredPosition = anchoredPosition;
      yield return (object) null;
    }
    this.discoBall.gameObject.SetActive(false);
    AudioManager.PlayMusicMainMenu();
  }

  public void ShowRestrictions()
  {
    PopupRestrictions.Create(Server._restrictions ?? new Restrictions(), string.Equals(Client.Name, "pur3 extreme"));
  }

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

  public void ToggleAll(UIOnHover[] group)
  {
    for (int index = 0; index < group.Length; ++index)
    {
      if (group[index].gameObject.activeSelf && group[index].AlwaysOn)
        group[index].AlwaysOn = false;
    }
  }

  public void RefreshActive()
  {
    this.RefreshFriends();
    this.RefreshIgnore();
    this.RefreshClan();
  }

  public void RefreshFriends(bool updatePos = false)
  {
    if (this.viewing != Viewing.Friends)
      return;
    this.txtRateType.text = "";
    this.txtRating.text = "Friends " + (object) Client.friends.Count + " / 200";
    int num = (int) ((RectTransform) this.pfabFriend.transform).sizeDelta.y + 2;
    int x = 0;
    List<(string, int)> valueTupleList = new List<(string, int)>();
    foreach (string friend in Client.friends)
      valueTupleList.Add((friend, Client.GetAccount(friend).location.Online() ? 1 : 0));
    valueTupleList.Sort((Comparison<(string, int)>) ((a, b) => a.online == b.online ? a.name.CompareTo(b.name) : b.online - a.online));
    this._containerAccounts.DestroyChildern();
    this._containerAccounts.sizeDelta = new Vector2(this._containerAccounts.sizeDelta.x, (float) (num * valueTupleList.Count + 35));
    for (int index = 0; index < valueTupleList.Count; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabFriend, (Transform) this._containerAccounts);
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2((float) x, (float) (-index * num - 2 - 35));
      pfabName component = gameObject.GetComponent<pfabName>();
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
    this.txtRateType.text = "";
    this.txtRating.text = "Ignored " + (object) Client.ignore.Count + " / 100";
    int num = (int) ((RectTransform) this.pfabFriend.transform).sizeDelta.y + 2;
    int x = 0;
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
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabFriend, (Transform) this._containerAccounts);
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2((float) x, (float) (-index * num - 2));
      gameObject.GetComponent<pfabName>().Setup(stringList[index]);
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
      int x = 0;
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

  [EnumAction(typeof (ZGame.GameType))]
  public void AskToChangeGameType(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.GameType, x);
  }

  [EnumAction(typeof (GameStyle))]
  public void AskToChangeGameStyle(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.GameStyle, x);
  }

  [EnumAction(typeof (PlayerEnum))]
  public void AskToChangeGameModePlayers(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.Players, x);
  }

  [EnumAction(typeof (TimeEnum))]
  public void AskToChangeGameModeTime(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.Time, x);
  }

  [EnumAction(typeof (MapEnum))]
  public void AskToChangeGameModeMap(int x) => Client.AskToChangeGameMode(GameFacts.Message.Map, x);

  [EnumAction(typeof (MapEnum))]
  public void AskToChangeArmageddon(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.Armageddon, x);
  }

  [EnumAction(typeof (TeamEnum))]
  public void AskToChangeGameModeTeams(int x)
  {
    Client.AskToChangeGameMode(GameFacts.Message.Team, x);
  }

  public void FindOpponents(bool v)
  {
    this.periods = 0;
    this.txtStartGame.text = v ? "Searching" : "FIND OPPONENT";
    this.findingOpponents = v;
    if (!v)
      return;
    this.StopAllCoroutines();
    this.StartCoroutine(this.IEDisco());
  }

  public void VerifyGameMode(GameFacts.Message whichMode, int mode)
  {
    if (!this.CanChangeSettings())
      return;
    RatedFacts ratedFacts = this._ratedFacts;
    switch (whichMode)
    {
      case GameFacts.Message.Time:
        if (!Enum.IsDefined(typeof (TimeEnum), (object) mode))
          return;
        if (ratedFacts.turnTime == -1 || mode == -1)
          ratedFacts.turnTime = mode;
        else
          ratedFacts.turnTime ^= mode;
        if (ratedFacts.turnTime == 0)
        {
          ratedFacts.turnTime = -1;
          break;
        }
        break;
      case GameFacts.Message.Map:
        if (!Enum.IsDefined(typeof (MapEnum), (object) mode))
          return;
        if (ratedFacts.mapStyle == -1 || mode == -1)
          ratedFacts.mapStyle = mode;
        else
          ratedFacts.mapStyle ^= mode;
        if (ratedFacts.mapStyle == 0)
        {
          ratedFacts.mapStyle = -1;
          break;
        }
        break;
      case GameFacts.Message.Team:
        if (!Enum.IsDefined(typeof (TeamEnum), (object) mode))
          return;
        if (ratedFacts.teams == -1 || mode == -1)
          ratedFacts.teams = mode;
        else
          ratedFacts.teams ^= mode;
        if (ratedFacts.teams == 0)
        {
          ratedFacts.teams = -1;
          break;
        }
        break;
      case GameFacts.Message.Players:
        if (!Enum.IsDefined(typeof (PlayerEnum), (object) mode))
          return;
        if (ratedFacts.playerCount == -1 || mode == -1)
          ratedFacts.playerCount = mode;
        else
          ratedFacts.playerCount ^= mode;
        if (ratedFacts.playerCount == 0)
        {
          ratedFacts.playerCount = -1;
          break;
        }
        break;
      case GameFacts.Message.PerTeam:
        return;
      case (GameFacts.Message) 10:
        return;
      case GameFacts.Message.GameStyle:
        if (!Enum.IsDefined(typeof (GameStyle), (object) mode) || mode == 128)
          return;
        if (ratedFacts.extraOptions == -1 || mode == -1)
          ratedFacts.extraOptions = mode;
        else
          ratedFacts.extraOptions ^= mode;
        if (ratedFacts.extraOptions == 0)
        {
          ratedFacts.extraOptions = -1;
          break;
        }
        break;
      case GameFacts.Message.AddMap:
        return;
      case GameFacts.Message.Armageddon:
        if (mode != 0 && !Enum.IsDefined(typeof (MapEnum), (object) mode))
          return;
        if (ratedFacts.armageddon == -1 || mode == -1 || mode == 0)
        {
          ratedFacts.armageddon = mode;
          break;
        }
        ratedFacts.armageddon ^= mode;
        break;
      case GameFacts.Message.GameType:
        if (!Enum.IsDefined(typeof (ZGame.GameType), (object) mode))
          return;
        ratedFacts.gameType = mode;
        break;
      default:
        return;
    }
    ratedFacts.VerifyGameType();
    this.RefreshGameOptions();
    Inert.SaveSettingsPlayer();
  }

  public void RefreshGameOptions()
  {
    for (int index = 0; index < this.lowGameObjects.Length; ++index)
      this.lowGameObjects[index].SetActive(this._ratedFacts.gameType != 0);
    for (int index = 0; index < this.highGameObjects.Length; ++index)
      this.highGameObjects[index].SetActive(this._ratedFacts.gameType != 1);
    for (int index = 0; index < this.neitherGameObjects.Length; ++index)
      this.neitherGameObjects[index].SetActive(this._ratedFacts.gameType > 1);
    this.ToggleAll(this.groupLadder);
    this.ToggleAll(this.groupPlayers);
    this.ToggleAll(this.groupTurnTime);
    this.ToggleAll(this.groupMapStyle);
    this.ToggleAll(this.groupArmageddon);
    this.ToggleAll(this.groupTeams);
    this.ToggleAll(this.groupGameStyles);
    this.ToggleAll(this.alternateSettings);
    this.panelInfoAltSettings.SetActive(Client._ratedFacts.list.Count > 1);
    if (this._ratedFacts.turnTime == -1)
    {
      this.groupTurnTime[0].AlwaysOn = true;
    }
    else
    {
      int turnTime = this._ratedFacts.turnTime;
      if ((turnTime & 512) != 0)
        this.groupTurnTime[1].AlwaysOn = true;
      if ((turnTime & 1024) != 0)
        this.groupTurnTime[2].AlwaysOn = true;
      if ((turnTime & 2048) != 0)
        this.groupTurnTime[3].AlwaysOn = true;
      if ((turnTime & 4096) != 0)
        this.groupTurnTime[4].AlwaysOn = true;
      if ((turnTime & 8192) != 0)
        this.groupTurnTime[5].AlwaysOn = true;
      if ((turnTime & 1048576) != 0)
        this.groupTurnTime[6].AlwaysOn = true;
      if ((turnTime & 16384) != 0)
        this.groupTurnTime[7].AlwaysOn = true;
      if ((turnTime & 262144) != 0)
        this.groupTurnTime[8].AlwaysOn = true;
    }
    int mapStyle = this._ratedFacts.mapStyle;
    int armageddon = this._ratedFacts.armageddon;
    if (this._ratedFacts.mapStyle == -1)
      this.groupMapStyle[0].AlwaysOn = true;
    if (this._ratedFacts.armageddon == -1)
      this.groupArmageddon[0].AlwaysOn = true;
    else if (this._ratedFacts.armageddon == 0)
      this.groupArmageddon[this.groupArmageddon.Length - 1].AlwaysOn = true;
    foreach (MapEnum e in (MapEnum[]) Enum.GetValues(typeof (MapEnum)))
    {
      if (e != MapEnum.Dont_Mind)
      {
        if (this._ratedFacts.mapStyle != -1 && ((MapEnum) mapStyle & e) != ~MapEnum.Dont_Mind)
          this.groupMapStyle[GameFacts.GetMapIndex(e) + 1].AlwaysOn = true;
        if (this._ratedFacts.armageddon != -1 && ((MapEnum) armageddon & e) != ~MapEnum.Dont_Mind)
          this.groupArmageddon[GameFacts.GetMapIndex(e) + 1].AlwaysOn = true;
      }
    }
    if (this._ratedFacts.teams == -1)
    {
      this.groupTeams[0].AlwaysOn = true;
    }
    else
    {
      int teams = this._ratedFacts.teams;
      if ((teams & 65536) != 0)
        this.groupTeams[1].AlwaysOn = true;
      if ((teams & 16777216) != 0)
        this.groupTeams[2].AlwaysOn = true;
    }
    if (this._ratedFacts.extraOptions == -1)
    {
      this.groupGameStyles[0].AlwaysOn = true;
    }
    else
    {
      int extraOptions = this._ratedFacts.extraOptions;
      if ((extraOptions & 4096) != 0)
        this.groupGameStyles[1].AlwaysOn = true;
      if ((extraOptions & 8) != 0)
        this.groupGameStyles[2].AlwaysOn = true;
      if ((extraOptions & 16) != 0)
        this.groupGameStyles[3].AlwaysOn = true;
      if ((extraOptions & 256) != 0)
        this.groupGameStyles[4].AlwaysOn = true;
      if ((extraOptions & 8192) != 0)
        this.groupGameStyles[5].AlwaysOn = true;
      if ((extraOptions & 2) != 0)
        this.groupGameStyles[6].AlwaysOn = true;
      if ((extraOptions & 2048) != 0)
        this.groupGameStyles[7].AlwaysOn = true;
      if ((extraOptions & 65536) != 0)
        this.groupGameStyles[8].AlwaysOn = true;
    }
    this.groupLadder[Mathf.Clamp(this._ratedFacts.gameType, 0, 2)].AlwaysOn = true;
    if (this._ratedFacts.playerCount == -1)
    {
      this.groupPlayers[0].AlwaysOn = true;
    }
    else
    {
      int playerCount = this._ratedFacts.playerCount;
      if ((playerCount & 33554432) != 0)
        this.groupPlayers[6].AlwaysOn = true;
      if ((playerCount & 1073741824) != 0)
        this.groupPlayers[5].AlwaysOn = true;
      if ((playerCount & 536870912) != 0)
        this.groupPlayers[4].AlwaysOn = true;
      if ((playerCount & 268435456) != 0)
        this.groupPlayers[3].AlwaysOn = true;
      if ((playerCount & 134217728) != 0)
        this.groupPlayers[2].AlwaysOn = true;
      if ((playerCount & 67108864) != 0)
        this.groupPlayers[1].AlwaysOn = true;
    }
    if (this._ratedFacts.spellOverrides == null)
    {
      this.panelSpellOverrides.SetActive(false);
    }
    else
    {
      this.panelSpellOverrides.SetActive(true);
      this.spellOverrides.SetSpells(this._ratedFacts.spellOverrides.spells, this._ratedFacts.spellOverrides.fullBook, this._ratedFacts.spellOverrides.SeasonsIsHoliday);
    }
    this.alternateSettings[this.curRatedFacts].AlwaysOn = true;
    int index1;
    for (index1 = 0; index1 < Client._ratedFacts.list.Count; ++index1)
      this.alternateSettings[index1].gameObject.SetActive(true);
    for (; index1 < this.alternateSettings.Length; ++index1)
      this.alternateSettings[index1].gameObject.SetActive(false);
  }

  public void ViewLobby()
  {
    this.UpdateScrollValue();
    PrestigeLobbyUI.Rating(this.txtRating);
    this.viewing = Viewing.Lobby;
    this.butViewLobby.AlwaysOn = true;
    this.butViewFriends.AlwaysOn = false;
    this.butViewIgnore.AlwaysOn = false;
    this.butViewClan.AlwaysOn = false;
    this.butAddFriend.gameObject.SetActive(this.butViewFriends.AlwaysOn);
    this._containerAccounts.DestroyChildern();
    this.tipsObj.SetActive(true);
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
    this.tipsObj.SetActive(false);
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
    this.tipsObj.SetActive(false);
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
    this.tipsObj.SetActive(false);
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

  public void Tooltip(string s) => MyToolTip.Show(s);

  public void HideTooltip() => MyToolTip.Close();

  public void ClickLobby() => Client.AskToJoinLobbyFromeQue();

  public void ClickInvitePlayers()
  {
  }

  public void ClickSpellOverrides()
  {
    if (!this.CanChangeSettings())
      return;
    SettingsPlayer sp = new SettingsPlayer();
    if (this._ratedFacts.spellOverrides != null)
      sp.CopySpells(this._ratedFacts.spellOverrides);
    else
      sp.CopySpells(Client.settingsPlayer);
    SpellLobbyChange.Create(sp, new Action<SettingsPlayer>(this.OnPickSpellOverrides));
  }

  public void ClickAddAlternative()
  {
    if (!this.CanChangeSettings() || Client._ratedFacts.list.Count >= 10)
      return;
    Client._ratedFacts.AddDefault();
    this.curRatedFacts = Client._ratedFacts.list.Count - 1;
    this.RefreshGameOptions();
  }

  public void ClickRemoveAlternative()
  {
    if (!this.CanChangeSettings())
      return;
    if (Client._ratedFacts.list.Count > 1)
    {
      Client._ratedFacts.list.RemoveAt(this.curRatedFacts);
      if (this.curRatedFacts >= Client._ratedFacts.list.Count)
        --this.curRatedFacts;
      this.RefreshGameOptions();
    }
    this.HideTooltip();
  }

  public void ClickAlternativeIndex(int i)
  {
    this.curRatedFacts = i;
    this.RefreshGameOptions();
  }

  private void OnPickSpellOverrides(SettingsPlayer sp)
  {
    if (!this.CanChangeSettings())
      return;
    this._ratedFacts.spellOverrides = new SpellsOnly();
    this._ratedFacts.spellOverrides.Copy(sp);
    this.RefreshGameOptions();
    this.HideTooltip();
  }

  public void RemoveSpellOverrides()
  {
    if (!this.CanChangeSettings())
      return;
    this._ratedFacts.spellOverrides = (SpellsOnly) null;
    this.RefreshGameOptions();
    this.HideTooltip();
  }

  private bool CanChangeSettings()
  {
    if (!RatedMenu.FindingOpponents)
      return true;
    ChatBox.Instance.NewChatMsg("You cannot change settings while finding opponents!!!!!", (Color) ColorScheme.GetColor(Global.ColorSystem));
    return false;
  }

  public void ClickStartGame()
  {
    if (!this.findingOpponents)
    {
      int num = 0;
      foreach (RatedFacts ratedFacts in Client._ratedFacts.list)
      {
        ++num;
        if ((ratedFacts.extraOptions & 16) != 0 && (ratedFacts.spellOverrides != null ? Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, ratedFacts.spellOverrides) : Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, Client.settingsPlayer)))
        {
          ChatBox.Instance.NewChatMsg("You have original spells enabled, but have non-original spells in your settings profile { " + (object) num + " } !!!!", (Color) ColorScheme.GetColor(Global.ColorSystem));
          return;
        }
      }
    }
    Client.AskToFindOpponents(!this.findingOpponents);
  }

  public void UpdateSpellIcons() => this.spellImageList.SetSpells();

  public void UpdateOutfit() => this.spellImageList.UpdateOutfit();

  public void ClickReset()
  {
    Client._ratedFacts.list.Clear();
    Client._ratedFacts.AddDefault();
    this.RefreshGameOptions();
  }

  public void LoadSettings()
  {
    MyFilePicker.Create("Load Settings", "RatedSettings", ".ratedSettings", false, (Action<string>) (s =>
    {
      Client._ratedFacts = RatedContainer.DeserializeFromFile(s);
      this.RefreshGameOptions();
    }));
  }

  public void SaveSettings()
  {
    RatedContainer x = Global.Copy<RatedContainer>(Client._ratedFacts);
    MyFilePicker.Create("Save Settings", "RatedSettings", ".ratedSettings", true, (Action<string>) (s => x.SerializeToFile(s)));
  }
}
