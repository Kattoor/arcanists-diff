

using Hazel;
using Hazel.Tcp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class RatedTab : MonoBehaviour
{
  public GameObject[] lowGameObjects;
  public GameObject[] highGameObjects;
  public GameObject[] neitherGameObjects;
  public GameObject panelInfoAltSettings;
  public Button buttonStartGame;
  public TMP_Text txtStartGame;
  public GameObject pfabFriend;
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
  public Toggle toggleBroadcast;
  private bool findingOpponents;
  public bool IsFirst = true;
  public List<string> partyQueue = new List<string>();
  [Header("Disco")]
  public RectTransform discoBall;
  public Image discoPfab;
  public AudioClip discoMusic;
  public PoolColoredImage pool;
  private int curRatedFacts;
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

  public static RatedTab instance { get; private set; }

  public static bool FindingOpponents
  {
    get => (UnityEngine.Object) RatedTab.instance != (UnityEngine.Object) null && RatedTab.instance.findingOpponents;
  }

  private RatedFacts _ratedFacts
  {
    get
    {
      return Client._ratedFacts.list[Mathf.Clamp(this.curRatedFacts, 0, Client._ratedFacts.list.Count - 1)];
    }
  }

  [ContextMenu("Add Current Settings to Que")]
  public void AddToQue()
  {
    Server._ratedQueue.Add((Connection) new TcpConnection(), Client._ratedFacts);
  }

  [ContextMenu("Fake Game")]
  public void AddToQue2()
  {
    GameFacts gameFacts = new GameFacts();
    gameFacts.SetRatedMode(true);
    Client._games.Add(Client._games.Count, gameFacts);
    LobbyMenu.instance.RefreshGames();
  }

  [ContextMenu("Fake Searching")]
  public void AddToQue3()
  {
    for (int index = 0; index < 20; ++index)
    {
      RatedContainer ratedContainer = new RatedContainer()
      {
        list = new List<RatedFacts>()
        {
          new RatedFacts()
          {
            armageddon = UnityEngine.Random.Range(-1, int.MaxValue),
            playerCount = UnityEngine.Random.Range(-1, int.MaxValue),
            extraOptions = UnityEngine.Random.Range(-1, int.MaxValue),
            mapStyle = UnityEngine.Random.Range(-1, int.MaxValue),
            gameType = UnityEngine.Random.Range(0, 3),
            teams = UnityEngine.Random.Range(-1, int.MaxValue),
            turnTime = UnityEngine.Random.Range(-1, int.MaxValue)
          }
        }
      };
      ratedContainer.list[0].VerifyGameType();
      Client._ratedSearches.Add(Client._ratedSearches.Count, ratedContainer);
    }
    LobbyMenu.instance.RefreshGames();
  }

  [ContextMenu("Find Matches")]
  public void SearchQue() => Server.FindRatedMatches();

  [ContextMenu("Remove All")]
  public void removeall()
  {
    Server._ratedQueue.Clear();
    Client._games.Clear();
    Client._ratedSearches.Clear();
    LobbyMenu.instance.RefreshGames();
  }

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

  private void Awake() => RatedTab.instance = this;

  private void Start()
  {
    this.RefreshGameOptions();
    this.toggleBroadcast.SetIsOnWithoutNotify(Global.GetPrefBool("prefbroadcast", true));
    this.toggleBroadcast.onValueChanged.AddListener((UnityAction<bool>) (v => Global.SetPrefBool("prefbroadcast", v)));
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) RatedTab.instance == (UnityEngine.Object) this))
      return;
    RatedTab.instance = (RatedTab) null;
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

  public void ClickHide() => this.gameObject.SetActive(false);

  public void ClickedLobbyQueue(RatedFacts r, bool add)
  {
    if (add)
    {
      if (Client._ratedFacts.list.Count >= 10)
        Client._ratedFacts.list.RemoveAt(9);
      Client._ratedFacts.list.Add(r);
    }
    else
    {
      Client._ratedFacts.list.Clear();
      Client._ratedFacts.list.Add(r);
    }
    if (this.findingOpponents)
      Client.AskToFindOpponents(false);
    this.gameObject.SetActive(true);
    this.RefreshGameOptions();
  }

  public void ToggleAll(UIOnHover[] group)
  {
    for (int index = 0; index < group.Length; ++index)
    {
      if (group[index].gameObject.activeSelf && group[index].AlwaysOn)
        group[index].AlwaysOn = false;
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
    LobbyMenu.instance?.panelFindingOpponents.SetActive(v);
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
    if (!RatedTab.FindingOpponents)
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
        int gameType = ratedFacts.gameType;
        if (ratedFacts.gameType == 2 && (ratedFacts.extraOptions & 10) == 0)
        {
          if (!((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null))
            return;
          ChatBox.Instance.NewChatMsg("", "You must search for Random Spells and/or Elementals in the Party Mode queue.", (Color) ColorScheme.GetColor(Global.ColorSystem), "", ChatOrigination.System);
          return;
        }
        if ((ratedFacts.extraOptions & 16) != 0 && (ratedFacts.spellOverrides != null ? Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, ratedFacts.spellOverrides) : Server.OriginalSpellsOnly(GameStyle.Original_Spells_Only, Client.settingsPlayer)))
        {
          ChatBox.Instance.NewChatMsg("You have original spells enabled, but have non-original spells in your settings profile { " + (object) num + " } !!!!", (Color) ColorScheme.GetColor(Global.ColorSystem));
          return;
        }
      }
    }
    Client.AskToFindOpponents(!this.findingOpponents);
  }

  public void ClickReset()
  {
    if (this.findingOpponents)
      return;
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
