

using Hazel;
using MovementEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class HUD : UIBehaviour
{
  public Inert inert;
  public ClientResources clientResources;
  public EmojiSelector emojiSelector;
  public RectTransform luaContainer;
  public RectTransform panelSpells;
  public Image[] spellbookDisable;
  public RectTransform containerSpells;
  public GameObject panelPause;
  public Material matStarScroll;
  public GameObject buttonChangeOutfits;
  public GameObject pfabPanelPlayer;
  public GameObject pfabPanelVS;
  public ChatFade chatFade;
  public RectTransform buttonHideChat;
  public GameObject buttonChatEaseOfAccess;
  public InputFieldPlus chatInput;
  public TextMeshProUGUI _name;
  public AnimateRepeatImage repeatChatMsg;
  public RectTransform borderLeftMap;
  public RectTransform borderTopMap;
  public GameObject LoadingPanel;
  public GameObject buttonSaveAndQuit;
  public GameObject buttonSaveGameOver;
  public GameObject gameOverConnectionLost;
  public RecycledScrollViewStandalone replayTimeline;
  public GameObject buttonReplayTimeline;
  public GameObject buttonShowSpells;
  public Image seasonIcon;
  public GameObject buttonJoinBoat;
  public GameObject buttonOverheadEmoji;
  public GameObject panelSkipTurn;
  public OptionsMenu optionsMenu;
  public UIOnHover toggleSkipWarning;
  [Header("Armageddon")]
  public Image armOverlay;
  public UnityEngine.UI.Text armText;
  public Image armBg;
  public Image armIcon;
  [Header("Spell Descriptions")]
  public GameObject panelSpellDescription;
  public TMP_Text textSpellDescription;
  public TMP_Text textSpellExtraText;
  public bool showDescriptions = true;
  public UIOnHover showDescriptionsButton;
  [Header("Full Book")]
  public UIOnHover fullBookButton;
  public TMP_Text fullBookTextLevel;
  public GameObject fullBookObj;
  public Image fullBookImg;
  public Sprite[] fullBookSprites;
  public TMP_Text textSpellCasted;
  public TMP_Text txtGameOptions;
  public RectTransform panelPlayersPanel;
  [Header("Starting Panel")]
  public List<UIPlayerCharacter> uiPlayerCharacters = new List<UIPlayerCharacter>();
  public List<HUD.ListOfCharacters> teamsOf12to9 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf11 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf10 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf9 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf8 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf7 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf6 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf5 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf4 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf3 = new List<HUD.ListOfCharacters>();
  public List<HUD.ListOfCharacters> teamsOf2 = new List<HUD.ListOfCharacters>();
  public GameObject panelStart;
  public Image panelStartColor;
  public UIOnHover button;
  public TMP_Text buttonText;
  public TMP_Text awardsText;
  [Header("Rematch")]
  public GameObject rematchObj;
  public TMP_Text rematchTxt;
  public UIOnHover toggleSave;
  public GameObject quizObject;
  public TMP_Text quizTxt;
  [Header("Time Panel")]
  public TMP_Text timeHeader;
  public Image time_bg;
  public TMP_Text time_txt;
  public TMP_Text waiting_txt;
  public ReplayTime replayTime;
  public TMP_Text txtGameID;
  public GameObject BidPanel;
  public TMP_Text bidMaxText;
  public TMP_InputField inputBid;
  public GameObject panelCountdown;
  public TMP_Text txtCountdown;
  [Header("Draw/Resign")]
  public UnityEngine.UI.Text drawOfferings;
  public GameObject resignButObj;
  public GameObject offerDrawButObj;
  public TMP_Text resignButTxt;
  public TMP_Text offerDrawButTxt;
  [Header("Reconnect")]
  public GameObject panelReconnect;
  public UnityEngine.UI.Text txtAttemptReconnect;
  [Header("Touch Screen Controls")]
  public GameObject panelControls;
  public GameObject panelAimControls;
  public GameObject panelFireControls;
  public GameObject panelMoveControls;
  public GameObject buttonSkipTurn;
  public RectTransform handleAim;
  public GameObject butFireControl;
  public UIOnHover buttonGlide;
  public UIOnHover buttonSprint;
  public UIOnHover buttonAlt;
  public UIOnHover buttonPing;
  internal static bool UseTouchControls = false;
  internal bool PressingMoveLeft;
  internal bool PressingMoveRight;
  internal bool PressingFire;
  internal bool PressingHighJump;
  internal bool PressingLongJump;
  internal bool PressingDetower;
  internal bool PressingNext;
  internal bool NoGlide;
  internal bool sprint;
  internal bool PressingBackQuote;
  internal float fireAngle;
  internal float pickup_music;
  [Header("Dummies")]
  public GameObject buttonDummies;
  public GameObject pfabDummies;
  public TMP_InputField inputDummyHp;
  public UIOnHover uiHoverToggleTeam;
  public UIOnHover uiHoverToggleOverlay;
  public GameObject familiarHowTo;
  [NonSerialized]
  public bool hideOverheadIcons;
  private float _lastResyncMsg;
  private int zzturn;
  private string[] compassRose = new string[9]
  {
    "North",
    "North-West",
    "West",
    "South-West",
    "South",
    "South-East",
    "East",
    "North-East",
    "North"
  };
  private Coroutine corCastSpell;
  private Vector2 _matOffsetStars = new Vector2(0.0f, -0.09f);
  private float lastMapPing;
  private bool pingOnNextClick;
  public static string doNotShowResign = "pref_noshowresign";
  public static string doNotShowLeave = "pref_noshowleave";
  public bool FollowSpells = true;
  private bool dummiesLoaded;
  public static bool talking;
  public byte lastPlayersTurn = byte.MaxValue;
  private bool _lockScale;
  private bool hidden = true;
  private Coroutine showHideEverything;
  private Quiz quiz = new Quiz();
  internal bool usedFourSeasons;
  [Header("Tutorial")]
  public RectTransform tutPopup;
  public TMP_Text turTextbox;
  public TMP_Text turTextButton;
  public Image tutPopupBorder;
  internal Action tutAction;
  private bool tutHidden;

  public static HUD instance { get; private set; }

  public ZGame game => Client.game;

  internal bool PressingOnScreenControl
  {
    get
    {
      return this.PressingMoveLeft || this.PressingMoveRight || this.PressingFire || this.PressingHighJump || this.PressingLongJump || this.PressingDetower;
    }
  }

  public static bool YourTurn
  {
    get
    {
      return (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person != null && Player.Instance.person.yourTurn;
    }
  }

  protected override void Awake()
  {
    HUD.UseTouchControls = Global.GetPrefBool("prefControls", HUD.UseTouchControls);
    Inert.Init(this.inert);
    ClientResources.Init(this.clientResources);
    HUD.instance = this;
    base.Awake();
    if ((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null)
    {
      this.textSpellCasted.rectTransform.SetParent(ChatBox.Instance.transform);
      this.textSpellCasted.rectTransform.SetAsFirstSibling();
      ChatBox.Instance.textSpellCasted = this.textSpellCasted;
    }
    this.buttonChangeOutfits.SetActive(Client.cosmetics.achievements[(int) SettingsPlayer.Achievement_GameOutfit]);
    this.panelStart.SetActive(true);
  }

  public void ToggleOverheadIcons()
  {
    this.hideOverheadIcons = !this.hideOverheadIcons;
    this.HideOverheadIcons(this.hideOverheadIcons);
  }

  public void HideOverheadIcons(bool v)
  {
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
    {
      if (v)
        Player.Instance.selected.selectedSpellPanel.SetActive(false);
      else if (!v && Player.Instance.selectedSpellIndex > -1)
        Player.Instance.selected.selectedSpellPanel.SetActive(true);
    }
    foreach (ZPerson zperson in this.game.players.Concat<ZPerson>((IEnumerable<ZPerson>) this.game._playersExtended))
    {
      foreach (ZCreature zcreature in zperson.controlled)
        zcreature.overheadCanvas?.gameObject.SetActive(!v);
    }
    if (this.game._uncontrolledPlayer != null)
    {
      foreach (ZCreature zcreature in this.game._uncontrolledPlayer.controlled)
        zcreature.overheadCanvas?.gameObject.SetActive(!v);
    }
    this.uiHoverToggleOverlay.AlwaysOn = v;
  }

  public void UpdateRematch(int i)
  {
    ZPerson player = this.game.players[i];
    if (player.panelPlayer.left)
      this.uiPlayerCharacters[i].SetRematchText("Left", false, Color.red);
    else if (player.offeringRematch)
      this.uiPlayerCharacters[i].SetRematchText("Offering Rematch", true, Color.white);
    else
      this.uiPlayerCharacters[i].SetRematchText("", false, Color.white);
    ZPerson zperson = this.game.players.Find((Predicate<ZPerson>) (z => string.Equals(z.name, Client.Name)));
    if (player != zperson)
      return;
    this.rematchTxt.text = player.offeringRematch ? "Cancel" : "Offer Rematch";
  }

  public void UpdateDraws()
  {
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
    {
      this.offerDrawButTxt.text = Player.Instance.person.offeringDraw ? "Cancel Draw" : "Offer Draw";
      this.offerDrawButObj.SetActive(true);
    }
    else
      this.offerDrawButObj.SetActive(false);
    StringBuilder stringBuilder = new StringBuilder("");
    bool flag = true;
    HashSet<string> stringSet = new HashSet<string>();
    foreach (ZPerson player in this.game.players)
    {
      if (player.offeringDraw && player.controlled.Count > 0 && stringSet.Add(player.name))
      {
        if (!flag)
          stringBuilder.Append("\n");
        stringBuilder.Append(player.name).Append(" wants to draw");
        flag = false;
      }
    }
    this.drawOfferings.text = stringBuilder.ToString();
  }

  public void OnToggleShowDescriptions()
  {
    this.showDescriptions = !this.showDescriptions;
    Global.SetPrefBool("prefshowdescript", this.showDescriptions);
    this.showDescriptionsButton.AlwaysOn = this.showDescriptions;
  }

  public void OnSpectatorShouldResync(string err)
  {
    if ((double) this._lastResyncMsg > (double) Time.realtimeSinceStartup)
      return;
    this._lastResyncMsg = Time.realtimeSinceStartup + 30f;
    Spectator.connection?.SendBytes(new byte[2]
    {
      (byte) 83,
      (byte) 5
    });
    ChatBox.Instance?.NewChatMsg("Attempting to resync... " + err, (Color) ColorScheme.GetColor(Global.ColorSystem));
  }

  public void AddPanelPlayer(ZCreature c)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabPanelPlayer, (Transform) this.panelPlayersPanel);
    RectTransform transform = (RectTransform) gameObject.transform;
    PanelPlayer component = gameObject.GetComponent<PanelPlayer>();
    int num = 0;
    if (this.game.isTeam)
      num = 35 * ((int) c.parent.id / Client._gameFacts.GetNumberPlayersPerTeam());
    transform.anchoredPosition = new Vector2(6f, (float) (-6 - (int) c.parent.id * 45 - num));
    if (c.isPawn)
      component.Init(c.parent.name, c.parent.settingsPlayer, c.parent.clientColor, c.clientObj);
    else
      component.Init(c.parent.name, c.parent.settingsPlayer, c.parent.clientColor, c.clientObj, true);
    c.panelPlayer = component;
    c.parent.panelPlayer = component;
  }

  public void AddPanelPlayer(ZPerson c)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabPanelPlayer, (Transform) this.panelPlayersPanel);
    RectTransform transform = (RectTransform) gameObject.transform;
    PanelPlayer component = gameObject.GetComponent<PanelPlayer>();
    int num = 0;
    if (this.game.isTeam)
      num = 35 * ((int) c.id / Client._gameFacts.GetNumberPlayersPerTeam());
    transform.anchoredPosition = new Vector2(6f, (float) (-6 - (int) c.id * 45 - num));
    component.Init(c.name, c.settingsPlayer, c.clientColor);
    c.panelPlayer = component;
  }

  public void AdjustPlayersPanel(ZPerson p)
  {
    if (!((UnityEngine.Object) p.panelPlayer != (UnityEngine.Object) null))
      return;
    ((RectTransform) p.panelPlayer.transform).anchoredPosition = new Vector2(6f, (float) (-6 - (int) p.id * 45));
  }

  public void AddVSPanel()
  {
    int num1 = this.game.gameFacts.players.Count / this.game.gameFacts.GetNumberPlayersPerTeam() - 1;
    int num2 = 0;
    for (int index = 0; index < num1; ++index)
    {
      RectTransform transform = (RectTransform) UnityEngine.Object.Instantiate<GameObject>(this.pfabPanelVS, (Transform) this.panelPlayersPanel).transform;
      transform.anchoredPosition = new Vector2(6f, (float) ((-6 - this.game.gameFacts.GetNumberPlayersPerTeam() * 45) * (index + 1) - num2));
      transform.SetAsFirstSibling();
      num2 += 30;
    }
  }

  public void NextTurn(ZPerson p, bool forceUpdate = false)
  {
    if (!this.armBg.gameObject.activeSelf || !(p.localTurn > this.zzturn | forceUpdate))
      return;
    this.UpdateArmageddonIcon(p.localTurn);
  }

  public void UpdateArmageddonIcon(int zzturn)
  {
    if (zzturn >= this.game.armageddonTurn)
    {
      if (!this.armOverlay.gameObject.activeSelf)
        return;
      this.armOverlay.gameObject.SetActive(false);
      this.armBg.color = Color.red;
    }
    else
    {
      this.armOverlay.color = new Color(1f, 1f, 1f, 0.11f * (float) (this.game.armageddonTurn - zzturn));
      this.armText.text = (this.game.armageddonTurn - zzturn).ToString();
      this.armOverlay.gameObject.SetActive(true);
      this.armBg.color = Color.white;
    }
  }

  public void UpdateSeasonIcon(GameSeason season)
  {
    if (!this.seasonIcon.gameObject.activeSelf)
      this.seasonIcon.gameObject.SetActive(true);
    this.seasonIcon.sprite = ClientResources.Instance.seasonIcons[(int) season];
  }

  public void HoverSeasons()
  {
    if ((UnityEngine.Object) this.seasonIcon.sprite == (UnityEngine.Object) ClientResources.Instance.seasonIcons[0])
      MyToolTip.Show("The Four Seasons has been cast - the seasons will begin looping on their next turn");
    else if ((UnityEngine.Object) this.seasonIcon.sprite == (UnityEngine.Object) ClientResources.Instance.seasonIcons[1])
      MyToolTip.Show("Summer");
    else if ((UnityEngine.Object) this.seasonIcon.sprite == (UnityEngine.Object) ClientResources.Instance.seasonIcons[2])
      MyToolTip.Show("Autumn");
    else if ((UnityEngine.Object) this.seasonIcon.sprite == (UnityEngine.Object) ClientResources.Instance.seasonIcons[3])
    {
      MyToolTip.Show("Winter");
    }
    else
    {
      if (!((UnityEngine.Object) this.seasonIcon.sprite == (UnityEngine.Object) ClientResources.Instance.seasonIcons[4]))
        return;
      MyToolTip.Show("Spring");
    }
  }

  public void HoverWind()
  {
    FixedInt fixedInt = Client.game.map.windSpeed / ZMap.MaxWindSpeed;
    int index = (int) ((Client.game.map.windDir + (FixedInt) 22.5f) / 45);
    while (index < 0)
      index += this.compassRose.Length;
    while (index >= this.compassRose.Length)
      index -= this.compassRose.Length;
    string str1 = fixedInt > (FixedInt) 0.8f ? "Heavy" : (fixedInt > (FixedInt) 0.6f ? "Strong" : (fixedInt > (FixedInt) 0.4f ? "Moderate" : (fixedInt > (FixedInt) 0.2f ? "Light" : "Calm")));
    string str2 = this.compassRose[index];
    if (fixedInt == 0)
      MyToolTip.Show("No Wind");
    else
      MyToolTip.Show(str1 + " " + str2 + " Wind");
  }

  public void SetupMiniCamera()
  {
    if (Client.map == null)
      return;
    float num1 = (float) Screen.width / (float) Screen.height / 1.77777779f;
    float num2 = (float) Client.map.Width / 1920f;
    double num3 = (double) Client.map.Height / (Client.map.Width > 2500 ? 960.0 : (Client.map.Height > 2000 ? 1060.0 : 1020.0));
    Camera camera = Controller.miniCameraObj;
    if ((UnityEngine.Object) camera == (UnityEngine.Object) null)
    {
      GameObject withTag = GameObject.FindWithTag("miniCamera");
      if ((UnityEngine.Object) withTag == (UnityEngine.Object) null)
      {
        Client.Disconnect(false);
        Controller.Instance.DestroyMap();
        Controller.Instance.OpenMenu(Controller.Instance.MenuLogin, false);
        return;
      }
      camera = withTag.GetComponent<Camera>();
    }
    camera.transform.position = new Vector3((float) (Client.map.Width / 2), (float) (Client.map.Height / 2), -210f);
    Rect rect = camera.rect with { width = 0.2f * num2 };
    rect.height = (float) ((double) rect.width * ((double) Client.map.Height / (double) Client.map.Width) * (double) num1 * 1.7999999523162842);
    if ((double) rect.width > 0.23999999463558197)
    {
      float num4 = 0.24f / rect.width;
      rect.width = 0.24f;
      rect.height *= num4;
    }
    rect.x = 1f - rect.width;
    camera.rect = rect;
    camera.orthographicSize = (float) Client.map.Width / 2f / camera.aspect;
    float num5 = (double) num1 > 1.0 ? num1 : 1f;
    float num6 = (double) num1 < 1.0 ? num1 : 1f;
    this.borderLeftMap.anchoredPosition = new Vector2((float) (-((double) rect.width * 1600.0 * (double) num5) - 3.7999999523162842), 0.0f);
    this.borderTopMap.anchoredPosition = new Vector2(0.0f, (float) ((double) rect.height * 900.0 / (double) num6 + 3.7999999523162842));
    this.borderLeftMap.sizeDelta = new Vector2(4f, this.borderTopMap.anchoredPosition.y);
    this.borderTopMap.sizeDelta = new Vector2(-this.borderLeftMap.anchoredPosition.x, 4f);
  }

  public void CastSpell(Spell s, ZCreature c)
  {
    this.textSpellCasted.text = "Casting " + ClickSpell.GetSpellName(s, c);
    this.StartCoroutine(this.SpellTextLerp(s.EndsTurn ? 3f : 2f));
  }

  private IEnumerator SpellTextLerp(float waitTime)
  {
    Vector2 spellCastStartPos = new Vector2(this.textSpellCasted.rectTransform.anchoredPosition.x, 0.0f);
    Vector2 spellCastEndPos = new Vector2(this.textSpellCasted.rectTransform.anchoredPosition.x, 95f);
    Vector2 current = Vector2.zero with
    {
      x = spellCastStartPos.x
    };
    float t;
    for (t = 0.0f; (double) t < 1.0; t += Time.deltaTime * 3f)
    {
      current.y = Mathf.SmoothStep(spellCastStartPos.y, spellCastEndPos.y, t);
      this.textSpellCasted.rectTransform.anchoredPosition = current;
      yield return (object) new WaitForEndOfFrame();
    }
    this.textSpellCasted.rectTransform.anchoredPosition = spellCastEndPos;
    yield return (object) new WaitForSecondsRealtime(waitTime);
    for (t = 1f; (double) t > 0.0; t -= Time.deltaTime * 3f)
    {
      current.y = Mathf.SmoothStep(spellCastStartPos.y, spellCastEndPos.y, t);
      this.textSpellCasted.rectTransform.anchoredPosition = current;
      yield return (object) new WaitForEndOfFrame();
    }
    this.textSpellCasted.rectTransform.anchoredPosition = spellCastStartPos;
    this.textSpellCasted.text = "";
  }

  private void Update()
  {
    if (this.game == null)
      return;
    if ((double) this.pickup_music > 0.0)
    {
      this.pickup_music -= Time.deltaTime;
      if ((double) this.pickup_music <= 0.0)
      {
        this.pickup_music = 0.0f;
        AudioManager.instance?.StopAllCoroutines();
        AudioManager.instance?.StartCoroutine(AudioManager.instance.FadePickupMusic(true));
      }
    }
    if (Input.GetKeyDown(KeyCode.F8))
      this.ShowSpellsPopup();
    if (Input.GetKeyDown(KeyCode.F12) && Client.MyAccount.accountType.isDev() && string.Equals(Client.MyAccount.name, "pur3 extreme"))
    {
      Client.game.AllowInput = false;
      MyContextMenu myContextMenu = MyContextMenu.Show();
      myContextMenu.AddSeperator("Just doing some debugging...");
      myContextMenu.AddInput((Action<string>) (s =>
      {
        Client.AskDebugTest(s);
        Client.game.AllowInput = true;
      }));
      myContextMenu.SetOnCancel((Action) (() => Client.game.AllowInput = true));
      myContextMenu.Rebuild();
    }
    this._matOffsetStars.x -= Time.deltaTime * 3f;
    if ((double) this._matOffsetStars.x < -1.0)
      ++this._matOffsetStars.x;
    this.matStarScroll.mainTextureOffset = this._matOffsetStars;
    if (Input.GetKey(KeyCode.LeftControl))
    {
      if (Input.GetKeyDown(KeyCode.F11))
      {
        if (!this.game.resyncing && this.game.isReplay && this.game.serverUpdate != null)
          Timing.KillCoroutines(this.game.serverUpdate);
        this.game.init_Resync(true);
      }
      else if (Input.GetKeyDown(KeyCode.F5) && Client.isConnected)
        Client.Disconnected((object) null, (DisconnectedEventArgs) null);
    }
    if (this.panelPause.activeInHierarchy && Input.GetKeyDown(KeyCode.F1))
      Controller.ShowSettingsMenu();
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button9))
    {
      if (this.chatInput.gameObject.activeInHierarchy)
        this.ToggleChat(false);
      else
        this.TogglePauseMenu(!this.panelPause.activeSelf);
    }
    if (this.game == null || this.game.AllowInput)
    {
      if (this.panelStart.activeSelf && !this.button.gameObject.activeSelf && !hardInput.GetKey("BackQuote") && !this.PressingBackQuote)
        this.HideStartPanel();
      else if (!this.Chatting())
      {
        if ((hardInput.GetKey("BackQuote") || this.PressingBackQuote) && !this.panelStart.activeSelf)
          this.ShowStartPanel();
        else if (hardInput.GetKeyDown("ToggleIcons"))
          HUD.instance.ToggleOverheadIcons();
        else if (hardInput.GetKeyDown("OverheadEmoji"))
          this.ClickOverheadEmoji();
        if ((hardInput.GetKeyDown("mapping") || this.pingOnNextClick && Input.GetMouseButtonDown(0)) && (double) this.lastMapPing < (double) Time.realtimeSinceStartup)
        {
          this.lastMapPing = (Client.game.isSpectator ? 1f : 0.1f) + Time.realtimeSinceStartup;
          this.pingOnNextClick = false;
          if (this.buttonPing.gameObject.activeSelf)
            this.buttonPing.AlwaysOn = false;
          Client.AsktoPing(0, (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
      }
      if (hardInput.GetKeyDown("Minions") && (int) this.game.serverState.playersTurn != Client.curGameID && !this.Chatting() && (int) this.game.serverState.playersTurn < this.game.players.Count)
        CameraMovement.Instance.LerpToTransform(this.game.players[(int) this.game.serverState.playersTurn].controlled[0], true);
    }
    if (this.game.isReplay || !Input.GetKeyDown(KeyCode.Tab) || this.game.isReplay || this.game.isServer && (!Client.allowtutorialDebugging || !this.game.isTutorial) && (!this.game.isSandbox || !((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null)))
      return;
    this.ToggleChatInput();
  }

  public bool Chatting()
  {
    return this.chatInput.gameObject.activeSelf || InputFieldPlus.Active || (this.game.isSpectator || this.game.isSandbox && (UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null) && ChatBox.Instance.chatInput.enabled && (ChatBox.Instance.chatInput.GetCaretPos > 0 || !Global.InputString.Contains("`")) || (UnityEngine.Object) ReportMenu.instance != (UnityEngine.Object) null || (UnityEngine.Object) ChangeSpellBookMenu.Instance != (UnityEngine.Object) null;
  }

  public void SetupStartPanel()
  {
    this.InitStartPanel();
    this.button.gameObject.SetActive(true);
    if (this.game.isReplay && !ZGame.replayShowStartPanel)
    {
      this.HideStartPanel();
      this.buttonReplayTimeline.GetComponent<UIOnHover>().onClick.Invoke();
    }
    else if (this.game.resyncing)
      this.HideStartPanel();
    else
      this.ShowStartPanel();
  }

  public void FindPlayer()
  {
    if ((UnityEngine.Object) this.game.GetMapTransform().GetComponent<Player>() != (UnityEngine.Object) null)
      return;
    if (!this.game.isSpectator)
    {
      for (int index = 0; index < this.game.players.Count; ++index)
      {
        ZPerson player1 = this.game.players[index];
        if (string.Equals(player1.name, Client.NameOrReplay, StringComparison.OrdinalIgnoreCase))
        {
          Player player2;
          Player.Instance = player2 = player1.game.GetMapTransform().gameObject.AddComponent<Player>();
          player2.gameObject.tag = "Player";
          Player.Instance.person = player1;
          Player.Instance.selectedCreaturePlayerOffset = index;
          Client.curGameID = index;
          player2.selected = player1.controlled[0];
          ClickSpell.Instance.SetBGColor(player1.clientColor);
          break;
        }
      }
    }
    if ((UnityEngine.Object) Player.Instance == (UnityEngine.Object) null && (UnityEngine.Object) CameraMovement.Instance != (UnityEngine.Object) null)
    {
      CameraMovement.Instance.GotoPosition(new Vector3((float) (this.game.map.Width / 2), (float) (this.game.map.Height / 2)));
      if ((double) CameraMovement.Instance.GetZoom() < 550.0)
        CameraMovement.Instance.SetZoom(550f);
    }
    if (this.game.isReplay && ZGame.replayShowStartPanel || this.game.resyncing || !((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || !((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null) || (this.game.gameFacts.GetStyle() & GameStyle.Random_Spells) == ~GameStyle.Dont_Mind)
      return;
    if (this.game.isMulti)
    {
      int num = 1;
      foreach (ZPerson zperson in this.game.GetTeam(Player.Instance.person.team))
      {
        StringBuilder stringBuilder = new StringBuilder("Your spells #");
        stringBuilder.Append(num).Append(": ");
        ++num;
        if ((ZComponent) zperson.first() != (object) null)
        {
          foreach (SpellSlot spell in zperson.first().spells)
            stringBuilder.Append("<sprite name=\"" + spell.spell.name + "\">");
        }
        ChatBox.Instance.NewChatMsg("", stringBuilder.ToString(), Color.white, "", ChatOrigination.System);
      }
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder("Your spells: (Press F8 to view) ");
      foreach (SpellSlot spell in Player.Instance.person.first().spells)
        stringBuilder.Append("<sprite name=\"" + spell.spell.name + "\">");
      ChatBox.Instance.NewChatMsg("", stringBuilder.ToString(), Color.white, "", ChatOrigination.System);
    }
  }

  public static void ChatSpells(byte[] spells, int extra)
  {
  }

  public void OnDeath() => this.StartCoroutine(this.DelayOnDeath());

  private IEnumerator DelayOnDeath()
  {
    yield return (object) new WaitForSecondsRealtime(5f);
    while (Client.game != null && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person != null && Player.Instance.person.yourTurn && Client.game.serverState.busy != ServerState.Busy.Ended)
      yield return (object) new WaitForSecondsRealtime(1f);
    if (Client.game != null && Client.game.serverState.busy != ServerState.Busy.Ended)
    {
      this.buttonJoinBoat.SetActive(true);
      this.buttonOverheadEmoji.SetActive(false);
    }
  }

  public void AskToJoinBoat() => Spectator.AskToJoinBoat();

  public void ShowSpellsPopup()
  {
    if (Client.game.isSandbox)
      return;
    this.ClickReturnToGame();
    if ((UnityEngine.Object) SpellLobbyChange.Instance != (UnityEngine.Object) null)
      SpellLobbyChange.Instance.ClickCancel();
    else if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person != null && Player.Instance.person.settingsPlayer != null)
      SpellLobbyChange.Create(Player.Instance.person.settingsPlayer, (Action<SettingsPlayer>) (set => { }), validate: false, transparent: true);
    else
      SpellLobbyChange.Create(Client.settingsPlayer, (Action<SettingsPlayer>) (set => Client.AskToChangeSpells(set)), transparent: true);
  }

  public void ShowOutfitPopup()
  {
    string name = Player.Instance.person.name;
    string persistentDataPath = Application.persistentDataPath;
    char directorySeparatorChar = Path.DirectorySeparatorChar;
    string str1 = directorySeparatorChar.ToString();
    directorySeparatorChar = Path.DirectorySeparatorChar;
    string str2 = directorySeparatorChar.ToString();
    string path1 = persistentDataPath + str1 + "SavedOutfits" + str2;
    Global.CheckDirectoryExists(path1, "downloads/" + name);
    string path2 = path1 + "downloads/" + name + ".outfit";
    if (string.IsNullOrEmpty(Path.GetFileNameWithoutExtension(path2)))
      return;
    using (FileStream fileStream = new FileStream(path2, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) fileStream))
        Player.Instance.person.settingsPlayer.Serialize(w);
    }
  }

  private IEnumerator ReadyRepeat()
  {
    while (this.game != null && !this.game.receivedInitialMsg)
    {
      Client.SendToGameServer(new byte[1]{ (byte) 17 });
      yield return (object) new WaitForSeconds(1f);
    }
  }

  protected override void Start()
  {
    Application.targetFrameRate = Mathf.Max(61, Mathf.Min(144, Screen.currentResolution.refreshRate));
    base.Start();
    this.panelControls.SetActive(false);
    this.buttonJoinBoat.SetActive(false);
    this.buttonOverheadEmoji.SetActive(true);
    if (Client._gameFacts.CreateGameFromGF)
    {
      this.buttonDummies.SetActive(false);
      this.LoadingPanel.SetActive(true);
      if (this.game.resyncing)
        this.LoadingPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Resyncing Game State";
      this.buttonHideChat.gameObject.SetActive(true);
      ChatBox.Instance.SetActive(Global.GetPrefBool("prefChatHIdden", true));
      this.ValidateFromChatBox();
      if (this.game.isReplay)
      {
        if (Client._gameFacts.GetTeamMode())
          HUD.instance.AddVSPanel();
        if (this.game.players.Count == 0)
          this.game.CreatePlayers(Client._gameFacts);
        Client.NameOrReplay = ReplayMenu.nameWhoRecordedReplay;
        Client.map.doneCallback = (Action) (() =>
        {
          UnityEngine.Resources.UnloadUnusedAssets();
          HUD.instance.FindPlayer();
          HUD.FindFullBooks(this.game);
          HUD.instance.SetupStartPanel();
          if ((UnityEngine.Object) CameraMovement.Instance != (UnityEngine.Object) null && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
          {
            CameraMovement.Instance.GotoPosition(Player.Instance.GetTransform.position);
            CameraMovement.Instance.target = Player.Instance.person.first();
            CameraMovement.Instance.state = CameraState.Follow;
          }
          HUD.instance.SetupMiniCamera();
        });
        this.game.serverUpdate = Timing.RunCoroutine(this.game.PushReplay(), Segment.Update);
        this.replayTime.enabled = true;
        this.timeHeader.text = "Turn:";
        this.buttonReplayTimeline.SetActive(true);
      }
      this.SetArmageddonIcon();
      this.txtGameID.text = "Game ID: #" + Client._gameFacts.id.ToString();
      this.StartCoroutine(this.ReadyRepeat());
    }
    else
    {
      Client._gameFacts = new GameFacts();
      Client.game = new ZGame();
      Client.game.gameFacts = Client._gameFacts;
      Client._gameFacts.game = Client.game;
      this.game.MaxTurnTime = Client._gameFacts.GetTimeInSeconds();
      this.game.ArcaneZero = false;
      Inert.LoadSettingsPlayer();
      this.game.isSandbox = true;
      this.game.isClient = true;
      this.game.isServer = true;
      this.game.isTutorial = Client._tutorial != null;
      SettingsPlayer settingsPlayer = this.game.isTutorial ? new SettingsPlayer() : Client.settingsPlayer;
      if (this.game.isTutorial)
      {
        settingsPlayer.CopySpells(Client._tutorial.settings, true);
        settingsPlayer.CopyOutfit(Client.settingsPlayer);
        this.buttonDummies.SetActive(false);
      }
      if (settingsPlayer.fullBook > (byte) 0)
        this.game.gameFacts.ToggleStyle(GameStyle.Elementals);
      Client._gameFacts.seed = new System.Random().Next();
      Client._gameFacts.settings.mapSeed = new System.Random().Next();
      this.txtGameID.text = this.game.isTutorial ? Client._tutorial.Name : "Sandbox";
      int e = this.game.isTutorial ? Client._tutorial.GetMapID() : PlayerPrefs.GetInt("prefsandboxmaps", 0);
      if (e < 0 || e >= ClientResources.Instance._maps.Length)
        e = 0;
      while (e == 11 && !this.game.isTutorial)
        e = UnityEngine.Random.Range(0, ClientResources.Instance._maps.Length);
      this.game.gameFacts.realMap = GameFacts.MapFromIndex(e);
      this.game.gameFacts.SetMapMode(this.game.gameFacts.realMap);
      this.game.armageddon = this.game.gameFacts.realMap;
      if (this.game.isTutorial && string.Equals("Empty", Client._tutorial.mapId))
        Client.map.SetMapSprite(this.game, new Color32[Client._tutorial.customWidth * Client._tutorial.customHeight], Client._tutorial.customHeight, Client._tutorial.customWidth);
      else
        Client.map.SetMapSprite(this.game, ClientResources.Instance._maps[e]);
      this.game.init_sandbox();
      this.game.SandBoxOrOnline();
      ZPerson player1 = Client._gameFacts.connections[0].player.player;
      player1.game = this.game;
      player1.settingsPlayer = settingsPlayer;
      Client.settingsPlayer.VerifyOutfit(Client.cosmetics);
      ZCreature character = Inert.CreateCharacter(player1, player1.settingsPlayer, new MyLocation(Client.map.Width / 2 - 50, Client.map.Height - (this.game.gameFacts.realMap == MapEnum.Jungle ? 330 : 30)), 0);
      ClickSpell.Instance.SetBGColor(character.bg.color);
      player1.controlled.Add(character);
      if (this.game.isTutorial)
        Client._tutorial.creatures.Add(-2, character);
      Player player2;
      Player.Instance = player2 = player1.game.GetMapTransform().gameObject.AddComponent<Player>();
      player2.gameObject.tag = "Player";
      Player.Instance.person = player1;
      Player.Instance.person.id = (byte) 0;
      character.team = 0;
      player1.team = 0;
      Client.curGameID = 0;
      player2.selected = player1.controlled[0];
      CameraMovement.Instance.GotoPosition((double) Player.Instance.GetTransform.position.y > 0.0 ? Player.Instance.GetTransform.position : new Vector3((float) (this.game.map.Width / 2), (float) (this.game.map.Height / 2)));
      HUD.FindFullBooks(this.game);
      this.SetupMiniCamera();
      this.SetArmageddonIcon();
      this.buttonHideChat.gameObject.SetActive(false);
      this.InitStartPanel();
      this.game.OnSetup();
      this.game.serverUpdate = Timing.RunCoroutine(this.game.FixedUpdate(), Segment.Update);
      if (character.ShouldFall())
        character.Fall();
    }
    if (this.game.isReplay || this.game.isSandbox || this.game.isSpectator)
    {
      if (this.game.isReplay || this.game.isSandbox)
        this.buttonSaveAndQuit.GetComponent<UIOnHover>().Interactable(false);
      this.offerDrawButObj.GetComponent<UIOnHover>().Interactable(false);
      this.resignButObj.GetComponent<UIOnHover>().Interactable(false);
      if (this.game.isSpectator && !this.game.isReplay)
      {
        this.buttonJoinBoat.SetActive(true);
        this.buttonOverheadEmoji.SetActive(false);
      }
    }
    else
    {
      this.offerDrawButObj.GetComponent<UIOnHover>().Interactable(true);
      this.resignButObj.GetComponent<UIOnHover>().Interactable(true);
    }
    AudioManager.PlayGameMusic(GameFacts.GetMapIndex(Client._gameFacts.GetMapMode()));
    this.showDescriptions = Global.GetPrefBool("prefshowdescript", true);
    this.showDescriptionsButton.AlwaysOn = this.showDescriptions;
    this.FollowSpells = Global.GetPrefBool("preffollowtargets", true);
    CameraMovement.refollowCamera = Global.GetPrefBool("prefrefollowcamera", true);
    CameraMovement.FOLLOWTARGETS = HUD.instance.FollowSpells;
    this.buttonPing.gameObject.SetActive(HUD.UseTouchControls && !Client.game.isSandbox && Client.game.isTeam);
    if ((UnityEngine.Object) this.toggleSave != (UnityEngine.Object) null)
    {
      this.toggleSave.onClick.AddListener(new UnityAction(this.ToggleSave));
      this.toggleSave.AlwaysOnNoCallback = Global.GetPrefBool("prefsavereplay", true);
    }
    if (this.game != null)
      this.txtGameOptions.text = this.game.gameFacts.ToString(this.game, true);
    if (!this.game.isSpectator && !this.game.isSandbox)
      return;
    HUD.instance.buttonShowSpells.GetComponent<UIOnHover>().Interactable(false);
  }

  public static string GetCustomArmageddonName(GameFacts f)
  {
    string customArmageddonName = "";
    for (int index = 0; index < f.settings.customArmageddon.Count; ++index)
    {
      Spell spell = Inert.GetSpell(f.settings.customArmageddon[index]);
      if ((UnityEngine.Object) spell != (UnityEngine.Object) null)
      {
        customArmageddonName += spell.name;
        if (index < f.settings.customArmageddon.Count - 1)
          customArmageddonName += "\n";
      }
    }
    return customArmageddonName;
  }

  public void SetArmageddonIcon()
  {
    List<SpellEnum> customArmageddon = this.game.gameFacts.settings.customArmageddon;
    // ISSUE: explicit non-virtual call
    if ((customArmageddon != null ? (__nonvirtual (customArmageddon.Count) > 0 ? 1 : 0) : 0) != 0)
    {
      this.armBg.gameObject.SetActive(true);
      this.armIcon.sprite = ClientResources.Instance.GetSpellIcon(Inert.GetSpell(this.game.gameFacts.settings.customArmageddon[0])?.name);
      string s = HUD.GetCustomArmageddonName(this.game.gameFacts);
      UIOnHover component = this.armIcon.GetComponent<UIOnHover>();
      component.onEnter.RemoveAllListeners();
      component.onExit.RemoveAllListeners();
      component.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(s)));
      component.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
      this.UpdateArmageddonIcon(this.game.players.Count > 0 ? Mathf.Max(0, this.game.players[0].localTurn) : 0);
    }
    else
    {
      int mapIndex = GameFacts.GetMapIndex(this.game.armageddon);
      if (mapIndex > 0 && Inert.Instance.ArmageddonIcons.Length > mapIndex && (UnityEngine.Object) Inert.Instance.ArmageddonIcons[mapIndex] != (UnityEngine.Object) null && !string.Equals(Inert.Instance.ArmageddonIcons[mapIndex].name, "Nothing") && !string.Equals(Inert.Instance.ArmageddonIcons[mapIndex].name, "levelstyle_icons_9"))
      {
        this.armBg.gameObject.SetActive(true);
        this.armIcon.sprite = Inert.Instance.ArmageddonIcons[mapIndex];
        if (this.game.armageddon == MapEnum.Mos_LeHarmless && this.game.gameFacts.GetStyle().HasStyle(GameStyle.Zombie_Monkey))
          this.armIcon.sprite = ClientResources.Instance.zombieMonkeyIcon;
        UIOnHover component = this.armIcon.GetComponent<UIOnHover>();
        component.onEnter.RemoveAllListeners();
        component.onExit.RemoveAllListeners();
        component.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(this.armIcon.sprite.name)));
        component.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
        this.UpdateArmageddonIcon(this.game.players.Count > 0 ? Mathf.Max(0, this.game.players[0].localTurn) : 0);
      }
      else
        this.armBg.gameObject.SetActive(false);
    }
  }

  public void SendChatMessage(string s)
  {
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      Player.Instance.lastSendTick = Time.realtimeSinceStartup + 0.4f;
    this.chatInput.gameObject.SetActive(false);
    if (!string.IsNullOrEmpty(s))
    {
      if (ChatBox.Instance.privateChat)
      {
        Client.SendPrivateChatMsg(ChatBox.Instance.privateChatTo, s);
        this.InitChat();
      }
      else
        Client.SendChatMsg(s);
    }
    else
    {
      if (!ChatBox.Instance.privateChat)
        return;
      this.InitChat();
    }
  }

  public void InitPrivateChat(string name)
  {
    if (this.game.isSpectator && ChatBox.Instance.Active)
    {
      ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), name);
      ChatBox.Instance.AllowInput(true);
    }
    else
    {
      this.chatInput.Init(ColorScheme.GetColor(Global.ColorSentPrivate));
      this._name.color = (Color) ColorScheme.GetColor(Global.ColorSentPrivate);
      this._name.text = "To " + name + ":<color=#00000000>||";
      this.chatInput.inputText.color = (Color) ColorScheme.GetColor(Global.ColorSentPrivate);
      ChatBox.Instance.privateChat = true;
      ChatBox.Instance.privateChatTo = name;
      this.chatInput.gameObject.SetActive(true);
      this.OnTouch(this.chatInput);
    }
  }

  public void InitChat()
  {
    this.chatInput.Init(ColorScheme.GetColor(Global.ColorGameText));
    this.chatInput.inputText.color = (Color) ColorScheme.GetColor(Global.ColorGameText);
    this._name.text = "";
    ChatBox.Instance.privateChat = false;
  }

  public void ConfirmResign()
  {
    if (this.game != null && this.game.players.Count > 1 && (!this.game.isMulti || this.game.TEAM_COUNT > 1) && !this.game.isSandbox && !this.game.isReplay && !this.game.isSpectator && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && !Global.GetPrefBool(HUD.doNotShowResign, false))
      MyMessageBox.Create("Are you sure you want to <b>RESIGN</b>?", (Action) (() => this.SendResign()), "Yes", "No", doNotShow: (Action) (() => Global.SetPrefBool(HUD.doNotShowResign, true)));
    else
      this.SendResign();
  }

  public void ConfirmLeave()
  {
    if (this.game != null && this.game.players.Count > 1 && (!this.game.isMulti || this.game.TEAM_COUNT > 1) && !this.game.isSandbox && !this.game.isReplay && !this.game.isSpectator && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && !Global.GetPrefBool(HUD.doNotShowLeave, false))
      MyMessageBox.Create("Are you sure you want to<br><b>LEAVE the GAME?</b>", (Action) (() => this.ClickExit()), "Yes", "No", doNotShow: (Action) (() => Global.SetPrefBool(HUD.doNotShowLeave, true)));
    else
      this.ClickExit();
  }

  public void ConfirmLeaveSave()
  {
    if (this.game != null && this.game.players.Count > 1 && (!this.game.isMulti || this.game.TEAM_COUNT > 1) && !this.game.isSandbox && !this.game.isReplay && !this.game.isSpectator && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && !Global.GetPrefBool(HUD.doNotShowLeave, false))
      MyMessageBox.Create("Are you sure you want to save and<br><b>LEAVE the GAME?</b>", (Action) (() => this.ClickSaveExit()), "Yes", "No", doNotShow: (Action) (() => Global.SetPrefBool(HUD.doNotShowLeave, true)));
    else
      this.ClickSaveExit();
  }

  public void SendResign()
  {
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 195);
          myBinaryWriter.Write(Player.Instance.person.id);
        }
        Client.connection.SendBytes(memoryStream.ToArray());
      }
    }
    this.resignButObj.GetComponent<UIOnHover>().Interactable(false);
    this.panelPause.SetActive(false);
  }

  public void SendOfferDraw()
  {
    if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null))
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 194);
        myBinaryWriter.Write(Player.Instance.person.id);
        myBinaryWriter.Write(!Player.Instance.person.offeringDraw);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  private static void OnInitSpell(ZCreature creature, SpellSlot slot)
  {
    if (slot.spell.spellEnum == SpellEnum.Glide)
      ZSpell.FireGlide(slot.spell, creature);
    else if (slot.spell.spellEnum == SpellEnum.Arcane_Gate || slot.spell.spellEnum == SpellEnum.Santas_Magic)
      creature.parent.AddGate(slot);
    if (creature.game.First_Turn_Teleport && slot.TurnsTillFirstUse > 0)
      ++slot.TurnsTillFirstUse;
    ++creature.parent.awards.spellTypesBrought[(int) slot.spell.spellType];
    if (!creature.game.AllowExpansion)
    {
      if (slot.spell.spellEnum == SpellEnum.Ice_Bomb || slot.spell.spellEnum == SpellEnum.Thorn_Bomb || slot.spell.spellEnum == SpellEnum.Ice_Shield || slot.spell.spellEnum == SpellEnum.Forest_Seed || slot.spell.spellEnum == SpellEnum.Shock_Bomb || slot.spell.spellEnum == SpellEnum.Magma_Bomb || slot.spell.spellEnum == SpellEnum.Summon_Swarm)
      {
        slot.TurnsTillFirstUse = 0;
        if (slot.spell.spellEnum != SpellEnum.Ice_Shield)
          return;
        slot.MaxUses = -1;
      }
      else
      {
        if (slot.spell.spellEnum != SpellEnum.Nature_s_Wrath)
          return;
        slot.RechargeTime = 0;
      }
    }
    else
    {
      if (slot.spell.spellEnum != SpellEnum.Forest_Seed || creature.game.MaxTurnTime >= 20)
        return;
      slot.TurnsTillFirstUse = 0;
    }
  }

  public static void FindFullBooks(ZGame game)
  {
    game.resigned.Clear();
    int index = 0;
    foreach (ZPerson player in game.players)
    {
      if ((ZComponent) player.first() != (object) null)
        HUD.FindFullBooks(game, player, index);
      ++index;
    }
  }

  public static void ClientChangeElementalStaff(ZPerson p, BookOf b, bool activateable)
  {
    try
    {
      if (b == BookOf.Nothing || b >= BookOf.The_Wilds)
        return;
      AnimateRepeat[] componentsInChildren = p.first()?.gameObject?.GetComponentsInChildren<AnimateRepeat>();
      if (componentsInChildren == null || componentsInChildren.Length == 0)
        return;
      for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
      {
        if (componentsInChildren[index1].gameObject.CompareTag("ElementalStaff"))
        {
          if (activateable || p.ActivateableFamiliar == BookOf.Nothing)
          {
            for (int index2 = 0; index2 < 4; ++index2)
              componentsInChildren[index1].sprites[index2] = ClientResources.Instance.staffPerElement[(int) (b + 1) * 4 + index2];
            break;
          }
          componentsInChildren[index1].sprites = new Sprite[8];
          for (int index3 = 0; index3 < 4; ++index3)
            componentsInChildren[index1].sprites[index3] = ClientResources.Instance.staffPerElement[(int) (p.ActivateableFamiliar + 1) * 4 + index3];
          for (int index4 = 0; index4 < 4; ++index4)
            componentsInChildren[index1].sprites[index4 + 4] = ClientResources.Instance.staffPerElement[(int) (b + 1) * 4 + index4];
          componentsInChildren[index1].UpdateFPS(12f);
          break;
        }
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) "Caught Elemental Staff Exception");
    }
  }

  public static void FindFullBooks(ZGame game, ZPerson x, int index)
  {
    ZCreature zcreature = x.controlled[0];
    int num1 = 0;
    int num2 = 0;
    HashSet<SpellEnum> spellEnumSet = new HashSet<SpellEnum>();
    int num3 = 0;
    BookOf b = zcreature.spells.Count > 9 ? zcreature.spells[9].spell.bookOf : BookOf.Nothing;
    for (int index1 = 0; index1 < zcreature.spells.Count; ++index1)
    {
      HUD.OnInitSpell(zcreature, zcreature.spells[index1]);
      if (zcreature.spells[index1].spell.bookOf == b)
        ++num3;
      if (game.AllowExpansion && zcreature.spells[index1].spell.IsMinionSpell())
      {
        ++num1;
        spellEnumSet.Add(zcreature.spells[index1].spell.spellEnum);
      }
      else if (game.AllowExpansion && zcreature.spells[index1].spell.spellType == SpellType.Bomb)
        ++num2;
    }
    if (num3 == 12 && (zcreature.game.AllowExpansion || b != BookOf.Arcane))
    {
      x.familiarBook = (FamiliarType) (1 << (int) (b & (BookOf) 31));
      x.ActivateableFamiliar = b;
      if (game.isClient)
        HUD.ClientChangeElementalStaff(x, b, true);
      if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person == x)
      {
        HUD.instance.fullBookImg.sprite = b != BookOf.Seasons || !zcreature.parent.seasonISHoliday ? HUD.instance.fullBookSprites[(int) b] : ClientResources.Instance.spellBookIconHoliday;
        HUD.instance.fullBookObj.SetActive(true);
        if (!Global.GetPrefBool("prefhasclickedfamiliar", false))
          HUD.instance.familiarHowTo.SetActive(true);
      }
    }
    if (num3 == 12 && b == BookOf.Arcane && (game.isSandbox || game.gameFacts.GetAllowArcanePowers()) && x.account.accountType.has(AccountType.Developer | AccountType.Arcane_Monster | AccountType.Game_Director) && !x.game.isRated)
      HUD.TransformArcaneMonster(x, zcreature, game, index);
    if (num1 >= 12 && zcreature.game.AllowExpansion)
    {
      x.MinionMaster = true;
      foreach (Spell minionSpell in Inert.Instance.MinionSpells)
      {
        if (minionSpell.level == 3)
        {
          int num4 = 0;
          for (int index2 = 0; index2 < zcreature.spells.Count && index2 < 16; ++index2)
          {
            SpellSlot spell = zcreature.spells[index2];
            if (spell != null && !((UnityEngine.Object) spell.spell == (UnityEngine.Object) null) && spell.spell.bookOf == minionSpell.bookOf)
              ++num4;
          }
          if (num4 > 0)
          {
            zcreature.parent.minionBookTitans.Add(new ZGame.MinionBookTitan()
            {
              spell = minionSpell.spellEnum,
              used = false
            });
            if (minionSpell.level == 3 && game.isClient)
              HUD.instance.uiPlayerCharacters[(int) zcreature.parent.id].AddLevel3(minionSpell);
          }
        }
      }
      Spell spell1 = Inert.GetSpell(SpellEnum.Summon_Titan);
      zcreature.spells.Add(new SpellSlot(spell1));
      ZFamiliar.CreateMinionMaster(x);
    }
    else if (num2 >= 9)
    {
      x.BombMaster = true;
      foreach (KeyValuePair<SpellEnum, Spell> spellsEnum in Inert.Instance.spellsEnums)
      {
        if (spellsEnum.Value.spellType == SpellType.Bomb && !zcreature.HasSpell(spellsEnum.Value.spellEnum))
          zcreature.spells.Add(new SpellSlot(spellsEnum.Value));
      }
      ZFamiliar.CreateBombMaster(x);
    }
    if (game.gameFacts.GetStyle().HasStyle(GameStyle.Elementals))
    {
      int index3 = (int) zcreature.parent.settingsPlayer.fullBook - 1;
      int num5 = game.gameFacts.GetStyle().HasStyle(GameStyle.Original_Spells_Only) ? 9 : (int) RandomExtensions.LastBook();
      if (index3 < 0 || index3 > num5)
        index3 = 0;
      int index4 = index3 * 12;
      zcreature.parent.familiarBook |= (FamiliarType) (1 << index3);
      int index5 = 0;
      int index6 = 0;
      while (index6 < 12)
      {
        Spell spell = !zcreature.parent.seasonISHoliday || index3 != 10 ? Inert.Instance._spells[index4] : Inert.Instance.holidaySpells[index6];
        int spellIndex = zcreature.GetSpellIndex(spell.spellEnum);
        if (index3 != 0 && spellIndex >= 0)
          zcreature.spells.RemoveAt(spellIndex);
        zcreature.spells.Insert(index5, new SpellSlot(spell));
        HUD.OnInitSpell(zcreature, zcreature.spells[index5]);
        ++index5;
        if ((spell.level == 3 || zcreature.parent.seasonISHoliday && spell.spellEnum == SpellEnum.Snow_Globe) && game.isClient)
          HUD.instance.uiPlayerCharacters[(int) zcreature.parent.id].AddLevel3(spell);
        ++index6;
        ++index4;
      }
      for (int index7 = 0; index7 < (game.isSandbox ? 5 : (int) game.gameFacts.elementalLevel); ++index7)
      {
        ++zcreature.parent.familiarLevels[index3];
        game.CreateFamiliar((BookOf) index3, zcreature.parent, false);
      }
      if (game.isClient)
        HUD.ClientChangeElementalStaff(x, (BookOf) index3, false);
    }
    if (!game.gameFacts.GetStyle().HasStyle(GameStyle.Watchtower) || game.gameFacts.GetStyle().HasStyle(GameStyle.First_Turn_Teleport))
      return;
    ZSpell.FireTower(Inert.Instance.spells["Watchtower"], zcreature, zcreature.position, (FixedInt) 0, (FixedInt) 0);
  }

  public static void TransformArcaneMonster(ZPerson x, ZCreature c, ZGame game, int index)
  {
    BookOf b = BookOf.Arcane;
    x.familiarBook = (FamiliarType) (1 << (int) (b & (BookOf) 31));
    x.ActivateableFamiliar = b;
    if (game.isClient)
      HUD.ClientChangeElementalStaff(x, b, true);
    game.hasArcaneMonster = true;
    c.FullArcane = true;
    c.parent.FullArcane = true;
    FixedInt fixedInt = (FixedInt) 1835008L;
    c.transform.localScale = new Vector3(fixedInt.ToFloat(), fixedInt.ToFloat(), 1f);
    c.radius = (int) (fixedInt * 18);
    c.collider.radius = c.radius;
    c.collider.Move(c.position);
    c.zb = MapGenerator.getOutlineArray(c.radius);
    c.scale = fixedInt.ToFloat();
    c.scaleFixed = fixedInt;
    c.massMulti = (FixedInt) 262144L;
    game.forceRysncPause = true;
    c.maxHealth = 500;
    c.health = 500;
    if (!c.game.originalSpellsOnly)
    {
      c.spells.Add(new SpellSlot(Inert.Instance.arcaneMist));
      c.spells.Add(new SpellSlot(Inert.Instance.arcaneDragon));
      c.spells.Add(new SpellSlot(Inert.Instance.spells["Arcane Meteor"]));
    }
    if ((UnityEngine.Object) c.overheadCanvas != (UnityEngine.Object) null)
    {
      c.overheadCanvas.transform.localScale = (Vector3) new Vector2(1f / c.scale, 1f / c.scale);
      ((RectTransform) c.overheadCanvas.transform).anchoredPosition = c.ClientOverHeadOffset.ToSinglePrecision() * (1f / fixedInt.ToFloat());
    }
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person == x)
    {
      HUD.instance.fullBookImg.sprite = HUD.instance.fullBookSprites[(int) b];
      HUD.instance.fullBookObj.SetActive(true);
    }
    HUD.ClientFullArcane(game, x, c, index);
  }

  public static void ClientFullArcane(ZGame game, ZPerson x, ZCreature c, int index)
  {
    if (!game.isClient)
      return;
    x.clientColor = ClientResources.Instance.ModColors[0];
    c.gameObject?.GetComponent<ConfigurePlayer>().ModEquipAll(x.name, x.settingsPlayer, ClientResources.Instance.ModColors);
    x.panelPlayer.TurnMod(x.settingsPlayer, x.clientColor, c.clientObj);
    HUD.instance.uiPlayerCharacters[index].Copy(x);
    c.bg.color = x.clientColor;
    c.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = x.clientColor;
    c.txtHealth.color = x.clientColor;
    ClickSpell.Instance.SetBGColor(x.clientColor);
    c.UpdateHealthTxt();
    c.ColorPrestigeHat();
    game.map.Purpalize();
  }

  public void ClickChangeOutfits() => ChangeOutfitMenu.Create(false, false);

  public void ClickUpdateFamiliar()
  {
    this.familiarHowTo?.SetActive(false);
    Global.SetPrefBool("prefhasclickedfamiliar", true);
    if (!this.game.isServer && Client.connection.State != ConnectionState.Connected || (UnityEngine.Object) Player.Instance == (UnityEngine.Object) null || !Player.Instance.person.yourTurn || this.game.serverState.busy != ServerState.Busy.No || Player.Instance.person.localTurn <= 0 && Client.game.First_Turn_Teleport)
      return;
    ZCreature zcreature = Player.Instance.person.controlled[0];
    if (zcreature.familiarLevelActivateable < 5 && zcreature.health > 20)
    {
      if (zcreature.FullArcane)
      {
        if (zcreature.health <= 100)
          return;
        zcreature.DoDamage(100);
        zcreature.UpdateHealthTxt();
        this.SendUpdateFamiliar();
        this.game.RemoveFamiliars();
        return;
      }
      Player.Instance.person.IncreaseLevel();
      zcreature.DoDamage(20);
      if (zcreature.hasDarkDefenses)
        zcreature.DarkDefenses(true);
      this.fullBookTextLevel.text = "Level " + (object) zcreature.familiarLevelActivateable;
      zcreature.UpdateHealthTxt();
      this.SendUpdateFamiliar();
      if (this.game.MoveQue.Count > 0)
        this.game.MoveQue.Enqueue((Action) (() => this.game.CreateFamiliar(Player.Instance.person.ActivateableFamiliar, Player.Instance.person)));
      else
        this.game.CreateFamiliar(Player.Instance.person.ActivateableFamiliar, Player.Instance.person);
    }
    if (zcreature.familiarLevelActivateable < 5)
      return;
    this.fullBookImg.transform.localPosition = new Vector3(-4.1f, 0.0f, 0.0f);
    this.fullBookButton.Interactable(false);
  }

  public void SendUpdateFamiliar()
  {
    if (this.game.isServer || (UnityEngine.Object) Player.Instance == (UnityEngine.Object) null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 201);
        myBinaryWriter.Write(Player.Instance.person.id);
        myBinaryWriter.Write(Client.game.gameFacts.id);
        myBinaryWriter.Write(Player.Instance.person.GetNextMoveID());
        myBinaryWriter.Write((byte) 0);
        myBinaryWriter.Write((byte) 0);
      }
      Client.SendToServer(memoryStream.ToArray());
    }
  }

  protected override void OnDestroy()
  {
    if ((UnityEngine.Object) HUD.instance == (UnityEngine.Object) this)
      HUD.instance = (HUD) null;
    base.OnDestroy();
  }

  public void Reconnect()
  {
    this.txtAttemptReconnect.gameObject.SetActive(true);
    Client.ConnectToGame();
  }

  private void DoTheDo(List<HUD.ListOfCharacters> t)
  {
    int index1 = 0;
    for (int index2 = 0; index2 < t.Count; ++index2)
    {
      for (int index3 = 0; index3 < t[index2].list.Count; ++index3)
      {
        this.uiPlayerCharacters[index1] = t[index2].list[index3];
        ++index1;
      }
    }
  }

  public void InitStartPanel()
  {
    int index = 0;
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      this.panelStartColor.color = Player.Instance.person.clientColor with
      {
        a = 0.9f
      };
    if (this.game.players.Count > 12 && this.game.isTeam)
    {
      switch (this.game.gameFacts.GetNumberPlayersPerTeam())
      {
        case 2:
          this.DoTheDo(this.teamsOf2);
          break;
        case 3:
          this.DoTheDo(this.teamsOf3);
          break;
        case 4:
          this.DoTheDo(this.teamsOf4);
          break;
        case 5:
          this.DoTheDo(this.teamsOf5);
          break;
        case 6:
          this.DoTheDo(this.teamsOf6);
          break;
        case 7:
          this.DoTheDo(this.teamsOf7);
          break;
        case 8:
          this.DoTheDo(this.teamsOf8);
          break;
        case 9:
          this.DoTheDo(this.teamsOf9);
          break;
        case 10:
          this.DoTheDo(this.teamsOf10);
          break;
        case 11:
          this.DoTheDo(this.teamsOf11);
          break;
        case 12:
          this.DoTheDo(this.teamsOf12to9);
          break;
      }
    }
    foreach (ZPerson player in this.game.players)
    {
      this.uiPlayerCharacters[index].Copy(player);
      ++index;
    }
    if (!this.game.isTeam || this.game.players.Count > 12)
      return;
    int numberPlayersPerTeam = this.game.gameFacts.GetNumberPlayersPerTeam();
    if (numberPlayersPerTeam == 2)
    {
      if (this.game.gameFacts.settingsPlayer.Count < 6)
        this.SwapPos(1, 2);
      else if (this.game.gameFacts.settingsPlayer.Count > 6)
      {
        this.SwapPos(1, 2);
        this.SwapPos(1, 6);
        this.SwapPos(3, 7);
        this.SwapPos(4, 8);
        this.SwapPos(5, 6);
        this.SwapPos(6, 9);
        this.SwapPos(9, 10);
      }
    }
    if (numberPlayersPerTeam == 3)
    {
      this.SwapPos(1, 2);
      this.SwapPos(2, 4);
      this.SwapPos(3, 4);
      this.SwapPos(7, 8);
      this.SwapPos(8, 10);
      this.SwapPos(9, 10);
    }
    else if (numberPlayersPerTeam == 4)
    {
      this.SwapPos(1, 6);
      this.SwapPos(3, 4);
      this.SwapPos(4, 6);
      this.SwapPos(6, 5);
      this.SwapPos(1, 2);
      this.SwapPos(2, 3);
      if (this.game.gameFacts.settingsPlayer.Count != 12)
        return;
      this.SwapPos(2, 8);
      this.SwapPos(6, 9);
      this.SwapPos(1, 4);
      this.SwapPos(2, 7);
    }
    else if (numberPlayersPerTeam == 5)
    {
      this.SwapPos(1, 2);
      this.SwapPos(2, 4);
      this.SwapPos(3, 4);
      this.SwapPos(3, 6);
      this.SwapPos(4, 8);
      this.SwapPos(5, 6);
      this.SwapPos(6, 8);
      this.SwapPos(7, 8);
    }
    else
    {
      if (numberPlayersPerTeam != 6)
        return;
      this.SwapPos(1, 6);
      this.SwapPos(3, 8);
      this.SwapPos(5, 10);
    }
  }

  private void SwapPos(int a, int b)
  {
    Vector2 anchoredPosition = ((RectTransform) this.uiPlayerCharacters[a].transform).anchoredPosition;
    ((RectTransform) this.uiPlayerCharacters[a].transform).anchoredPosition = ((RectTransform) this.uiPlayerCharacters[b].transform).anchoredPosition;
    ((RectTransform) this.uiPlayerCharacters[b].transform).anchoredPosition = anchoredPosition;
  }

  public void ToggleSave()
  {
    if (!Global.GetPrefBool("prefsavereplay", true))
    {
      Global.SetPrefBool("prefsavereplay", true);
      this.toggleSave.AlwaysOn = true;
    }
    else
    {
      Global.SetPrefBool("prefsavereplay", false);
      this.toggleSave.AlwaysOn = false;
    }
  }

  public void ShowStartPanel()
  {
    this.panelStart.SetActive(true);
    this.transform.SetAsLastSibling();
    if (this.game.serverState.busy != ServerState.Busy.Ended || this.game.isSpectator || this.game.isReplay || !Client.isConnected)
      return;
    this.rematchObj.SetActive(true);
  }

  public void HideStartPanel()
  {
    this.button.gameObject.SetActive(false);
    this.panelStart.SetActive(false);
    this.PressingBackQuote = false;
    if (this.panelPause.activeSelf || this.LoadingPanel.activeSelf)
      return;
    this.transform.SetAsFirstSibling();
  }

  public void ClickReturnToGame()
  {
    this.panelPause.SetActive(false);
    if (this.panelStart.activeSelf || this.LoadingPanel.activeSelf)
      return;
    this.transform.SetAsFirstSibling();
  }

  public void ClickScreenSize() => Controller.ShowScreenSizeMenu();

  public void ClickOptions() => Controller.ShowSettingsMenu();

  public void TogglePauseMenu(bool v)
  {
    this.panelPause.SetActive(v);
    if (this.panelPause.activeSelf)
    {
      if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && !this.game.isReplay && !this.game.isSandbox)
      {
        this.resignButObj.GetComponent<UIOnHover>().Interactable(true);
        this.buttonSkipTurn.GetComponent<UIOnHover>().Interactable(Player.Instance.person.yourTurn);
      }
      else
      {
        this.resignButObj.GetComponent<UIOnHover>().Interactable(false);
        this.buttonSkipTurn.GetComponent<UIOnHover>().Interactable(this.game.isSandbox);
      }
    }
    if (this.panelPause.activeSelf || this.panelStart.activeSelf || this.LoadingPanel.activeSelf)
      this.transform.SetAsLastSibling();
    else
      this.transform.SetAsFirstSibling();
  }

  public void ClickInstructions()
  {
  }

  private void OnApplicationPause(bool v)
  {
  }

  public void SafeExit() => this.StartCoroutine(this._SafeExit());

  public IEnumerator _SafeExit()
  {
    yield return (object) new WaitForSecondsRealtime(5f);
    Client.AskToJoinLobby();
    yield return (object) new WaitForSecondsRealtime(5f);
    Client.Disconnect(true);
  }

  public void ClickExit()
  {
    if (this.game == null)
      Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
    else
      this.game.QuitToMainMenu(exitToLobby: Client.JoinLocation.Lobby);
  }

  public void ClickSaveExit() => this.game.QuitToMainMenu(true, Client.JoinLocation.Lobby, true);

  public void ClickSaveExitNotForced() => this.game.QuitToMainMenu(true, Client.JoinLocation.Lobby);

  public void ClickOfferRematch()
  {
    ZPerson zperson = this.game.players.Find((Predicate<ZPerson>) (z => string.Equals(z.name, Client.Name)));
    if (zperson == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 196);
        myBinaryWriter.Write(zperson.id);
        myBinaryWriter.Write(zperson.offeringRematch ? (byte) 0 : (byte) 1);
      }
      Client.connection.SendBytes(memoryStream.ToArray());
    }
  }

  public void KillAllSummons()
  {
    for (int index = 0; index < this.game._playersExtended.Count; ++index)
      this.KillAllSummons(this.game._playersExtended[index]);
  }

  public void KillAllSummons(ZPerson p)
  {
    for (int index = p.controlled.Count - 1; index > 0; --index)
    {
      if (index < p.controlled.Count)
      {
        ZCreature zcreature = p.controlled[index];
        zcreature.health = 0;
        zcreature.OnDeath(true);
      }
    }
  }

  public void HoverExit() => MyToolTip.instance?._Close();

  public void Hover(string msg) => MyToolTip.Show(msg);

  public void HoverArmageddon()
  {
    if (this.game == null)
      return;
    int num = 0;
    foreach (ZPerson player in this.game.players)
    {
      if (player.localTurn > num)
        num = player.localTurn;
    }
    if (this.game.isCountdown)
    {
      StringBuilder stringBuilder = new StringBuilder(this.game.isCountdown ? string.Format("{0} / 200 Turns<br>", (object) (num + 1)) : string.Format("{0} / 60 Turns<br>", (object) (num + 1)));
      foreach (ZPerson player in this.game.players)
        stringBuilder.Append("<").Append(InputFieldPlus.RGBtoHEX((Color32) player.clientColor)).Append(">").Append(player.name).Append("</color> - ").Append(Global.IntToTime((int) ((double) player.countdown + 0.5), 12)).Append("<br>");
      MyToolTip.Show(stringBuilder.ToString());
    }
    else if (this.game.isSandbox || this.game.isTutorial)
      MyToolTip.Show(string.Format("Turn {0}", (object) (num + 1)));
    else
      MyToolTip.Show(string.Format("{0} / 60 Turns", (object) (num + 1)));
  }

  public void ToggleSummonOnYourTeam()
  {
    Player.Instance.summonOnYourTeam = !Player.Instance.summonOnYourTeam;
    this.StartCoroutine(this.delaySwitchSprites(Player.Instance.summonOnYourTeam));
  }

  private IEnumerator delaySwitchSprites(bool v)
  {
    while (Input.GetMouseButton(0))
      yield return (object) 0;
    if (Player.Instance.summonOnYourTeam == v)
    {
      this.uiHoverToggleTeam.NormalSprite = this.uiHoverToggleTeam.PressedSprite;
      this.uiHoverToggleTeam.HighlightedSprite = this.uiHoverToggleTeam.DisabledSprite;
      this.uiHoverToggleTeam.PressedSprite = this.uiHoverToggleTeam.DisabledSprite;
      this.uiHoverToggleTeam.DisabledSprite = this.uiHoverToggleTeam.NormalSprite;
      if (!this.uiHoverToggleTeam.IsHovering)
        this.uiHoverToggleTeam.GetImage.sprite = this.uiHoverToggleTeam.NormalSprite;
    }
  }

  public void ToggleReplayTimeline(GameObject t) => t.SetActive(!t.activeSelf);

  public void ToggleDummies(GameObject t)
  {
    bool flag = !t.activeSelf;
    Player.Instance.StopSummoningDummy();
    if (flag && !this.dummiesLoaded)
    {
      this.dummiesLoaded = true;
      float num1 = -5f;
      float y1 = 30f;
      int num2 = 0;
      float y2 = ((RectTransform) this.pfabDummies.transform).sizeDelta.y;
      List<Spell> spellList = new List<Spell>();
      foreach (Spell summonSpell in ClientResources.Instance.summonSpells)
      {
        if (summonSpell.spellEnum != SpellEnum.Summon_Tutorial_Target)
          spellList.Add(summonSpell);
      }
      spellList.Sort((Comparison<Spell>) ((a, b) => a.bookOf == b.bookOf ? a.spellEnum - b.spellEnum : a.bookOf - b.bookOf));
      spellList.Add(Inert.GetSpell(SpellEnum.Summon_Tutorial_Target));
      foreach (Spell spell in spellList)
      {
        Spell s = spell;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabDummies, this.pfabDummies.transform.parent);
        ((RectTransform) gameObject.transform).anchoredPosition = new Vector2(num1 - (float) (num2 * 46), y1);
        string str = spell.name;
        if (str.StartsWith("Summon "))
          str = str.Substring(7);
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ClientResources.Instance.icons[spell.name];
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = str;
        gameObject.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() => Player.Instance.StartSummoningDummy(s)));
        gameObject.SetActive(true);
        ++num2;
        if (num2 > 3)
        {
          num2 = 0;
          y1 += (float) ((double) y2 + 1.0 + 3.0);
        }
      }
    }
    t.SetActive(flag);
  }

  public void ClickOverheadEmoji()
  {
    Player.Instance?.UnselectSpell();
    MyContextMenu m = MyContextMenu.Show();
    m.AddEmojiSelector((Action<string>) (s =>
    {
      int startIndex = s.IndexOf('"') + 1;
      s = s.Substring(startIndex, s.LastIndexOf('"') - startIndex);
      Client.AskOverheadEmoji(EmojiInfo.GetIndex(s));
      m.Close();
      MyToolTip.Close();
    }));
    m.Rebuild();
  }

  public void HideOverheadEmoji()
  {
    this.emojiSelector.transform.parent.gameObject.SetActive(false);
  }

  public void ToggleChatInput()
  {
    if (Input.GetKey(KeyCode.LeftControl))
    {
      if ((this.game.isSpectator || this.game.isSandbox && (UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null) && ChatBox.Instance.Active)
      {
        if (string.IsNullOrEmpty(ChatBox.Instance.privateChatTo))
          ChatBox.Instance.AllowInput(!ChatBox.Instance.chatInput.enabled);
        else
          ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), ChatBox.Instance.privateChatTo);
      }
      else
      {
        this.chatInput.gameObject.SetActive(true);
        if (ChatBox.Instance.privateChat || string.IsNullOrEmpty(ChatBox.Instance.privateChatTo))
          this.InitChat();
        else
          this.InitPrivateChat(ChatBox.Instance.privateChatTo);
      }
    }
    else if ((this.game.isSpectator || this.game.isSandbox && (UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null) && ChatBox.Instance.Active)
      ChatBox.Instance.AllowInput(!ChatBox.Instance.chatInput.enabled);
    else
      this.chatInput.gameObject.SetActive(!this.chatInput.gameObject.activeSelf);
  }

  public void ToggleChat() => this.ToggleChat(!ChatBox.Instance.Active);

  public void StopAnimateChatRepaet()
  {
    this.repeatChatMsg.enabled = false;
    this.repeatChatMsg.sp.sprite = this.repeatChatMsg.sprites[0];
  }

  private void ToggleChat(bool v)
  {
    this.StopAnimateChatRepaet();
    Global.SetPrefBool("prefChatHIdden", v);
    ChatBox.Instance.SetActive(v);
    this.ValidateFromChatBox();
    if (this.game.isSpectator || this.game.isSandbox && (UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null)
      ChatBox.Instance.AllowInput(v);
    if (!v)
      return;
    this.OnTouch(this.chatInput);
  }

  public void OnTouch(InputFieldPlus i) => ClientResources.Instance.OnTouch(i);

  public void disable_talking()
  {
  }

  public void enable_talking()
  {
  }

  public void toggle_talking()
  {
  }

  public void ShowSkipTurnPanel()
  {
    this.panelSkipTurn.SetActive(true);
    this.SetupSkipWarning();
  }

  internal void SetupSkipWarning()
  {
    if (!((UnityEngine.Object) this.toggleSkipWarning != (UnityEngine.Object) null))
      return;
    this.toggleSkipWarning.onClick.RemoveAllListeners();
    this.toggleSkipWarning.onClick.AddListener((UnityAction) (() => HUD.ToggleSkipWarning(this.toggleSkipWarning, true)));
    this.toggleSkipWarning.AlwaysOn = Global.GetPrefBool("prefskipwarning", false);
  }

  public static void ToggleSkipWarning(UIOnHover toggleSkipWarning, bool same)
  {
    if (!Global.GetPrefBool("prefskipwarning", false))
    {
      Global.SetPrefBool("prefskipwarning", true);
      toggleSkipWarning.AlwaysOn = same;
    }
    else
    {
      Global.SetPrefBool("prefskipwarning", false);
      toggleSkipWarning.AlwaysOn = !same;
    }
  }

  public void ConfirmSkipTurn()
  {
    if (this.panelSkipTurn.activeInHierarchy)
      Player.Instance.SendMove((byte) 206);
    this.panelSkipTurn.SetActive(false);
  }

  public void HideSkipPanel() => this.panelSkipTurn.SetActive(false);

  public void HoverBook()
  {
    if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || !((ZComponent) Player.Instance.person.first() != (object) null) || Player.Instance.person.ActivateableFamiliar == BookOf.Nothing)
      return;
    this.textSpellDescription.text = Descriptions.GetBookDescription(Player.Instance.person.ActivateableFamiliar);
    this.textSpellExtraText.text = "";
    this.ShowPanelDescription();
  }

  public void LeaveBook()
  {
    this.panelSpellDescription.SetActive(false);
    if (!this.game.isTutorial || string.IsNullOrEmpty(this.turTextbox.text))
      return;
    this.tutPopup.gameObject.SetActive(true);
  }

  public void ShowPanelDescription()
  {
    this.panelSpellDescription.SetActive(true);
    if (!this.game.isTutorial || string.IsNullOrEmpty(this.turTextbox.text))
      return;
    this.tutPopup.gameObject.SetActive(false);
  }

  public void ToggleLockScale() => this._lockScale = !this._lockScale;

  public void HoverPlayersPanel()
  {
    if (this._lockScale)
      return;
    this.game?.ScalePlayersPanel(true);
  }

  public void LeavePlayersPanel()
  {
    if (this._lockScale)
      return;
    this.game?.ScalePlayersPanel(false);
  }

  public void UpdateTime()
  {
    if ((int) this.lastPlayersTurn != (int) this.game.serverState.playersTurn)
    {
      this.lastPlayersTurn = this.game.serverState.playersTurn;
      if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
      {
        if ((int) this.lastPlayersTurn == Player.Instance.myTurn && (this.game.serverState.busy == ServerState.Busy.No || this.game.serverState.busy == ServerState.Busy.Starting_Turn && this.game.ongoing.NumberOfSlowUpdateCoroutines == 0))
        {
          Player.Instance.Show_All();
          if ((UnityEngine.Object) ClickSpell.Instance != (UnityEngine.Object) null)
            ClickSpell.Instance.SetSpells();
        }
        else
          Player.Instance.Hide_All();
      }
      this.timeHeader.color = this.game.players[(int) this.lastPlayersTurn].clientColor;
      this.time_txt.color = this.timeHeader.color;
      this.txtCountdown.color = this.time_txt.color;
      this.waiting_txt.color = this.time_txt.color;
    }
    this.time_txt.text = string.Concat((object) Math.Max(0, (!Client.game.isSandbox || Client.game.sandboxTime != 0 ? (int) this.game.PlayersMaxTurnTime : 0) - (int) ((double) this.game.serverState.turnTime + 0.5)));
    this.waiting_txt.gameObject.SetActive(this.game.ongoing.isRunningSpell);
    this.txtCountdown.text = Global.IntToTime((int) ((double) this.game.CurrentPlayerNotNull().countdown + 0.5), 30);
  }

  public void ToggleWaitingText()
  {
    if (this.game == null)
      return;
    this.waiting_txt.color = this.time_txt.color;
    this.waiting_txt.gameObject.SetActive(this.game.ongoing.isRunningSpell);
  }

  public void DisablePressedButtons()
  {
    this.PressingHighJump = false;
    this.PressingLongJump = false;
    this.PressingDetower = false;
    this.PressingMoveLeft = false;
    this.PressingMoveRight = false;
    this.PressingFire = false;
  }

  public void PressedFlip() => Player.Instance?.FlipFlop();

  public void PressedHigh(bool v) => this.PressingHighJump = v;

  public void PressedLong(bool v) => this.PressingLongJump = v;

  public void PressedDetower(bool v) => this.PressingDetower = v;

  public void PressedLeft(bool v) => this.PressingMoveLeft = v;

  public void PressedRight(bool v) => this.PressingMoveRight = v;

  public void PressedBackQuote()
  {
    this.PressingBackQuote = true;
    this.ShowStartPanel();
    this.panelPause.SetActive(false);
  }

  public void PressedNoGlide()
  {
    this.NoGlide = !this.NoGlide;
    this.buttonGlide.AlwaysOn = this.NoGlide;
  }

  public void PressedSprint()
  {
    this.sprint = !this.sprint;
    this.buttonSprint.AlwaysOn = this.sprint;
  }

  public void PressedNext()
  {
    Player instance = Player.Instance;
    if ((instance != null ? (!instance.person.InWater() ? 1 : 0) : 1) != 0)
      Player.Instance?.NextControlled();
    else
      Player.Instance?.NextRecallDevice(false);
  }

  public void PressedPrevious()
  {
    Player instance = Player.Instance;
    if ((instance != null ? (!instance.person.InWater() ? 1 : 0) : 1) != 0)
      Player.Instance?.PreviousControlled();
    else
      Player.Instance?.PreviousRecallDevice();
  }

  public void PressedFireCancel() => Player.Instance?.StopSummoningDummy();

  public void PressedFireUp()
  {
    this.PressingFire = false;
    Player.Instance?.MouseUp();
  }

  public void PressedFireDown()
  {
    this.PressingFire = true;
    Player.Instance?.MouseDown();
  }

  public void PressedAlt() => Player.Instance.ToggleExtendedShot(!this.buttonAlt.AlwaysOn);

  public void PressedPing()
  {
    this.lastMapPing = Time.realtimeSinceStartup + 0.1f;
    this.pingOnNextClick = !this.pingOnNextClick;
    this.buttonPing.AlwaysOn = this.pingOnNextClick;
  }

  public void PressedSkip()
  {
    if (Client.game.ongoing.NumberOfSlowUpdateCoroutines == 0 && Player.Instance.person.yourTurn)
      Player.Instance.SendMove((byte) 206);
    if (this.game.isSandbox)
      return;
    this.panelPause.SetActive(false);
  }

  public void ReleaseBackQuoteMobile() => this.PressingBackQuote = false;

  public void DisableButtons()
  {
    this.PressingHighJump = false;
    this.PressingLongJump = false;
    this.PressingDetower = false;
    this.PressingMoveLeft = false;
    this.PressingMoveRight = false;
    this.NoGlide = false;
    this.buttonGlide.AlwaysOn = false;
    this.PressingFire = false;
    Player.Instance.ToggleExtendedShot(false);
  }

  public void AngleChanged(float f) => this.fireAngle = f;

  public void PressedCenterCamera()
  {
    if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
    {
      Player.Instance.CenterCamera();
    }
    else
    {
      ZCreature v = this.game.ClientCurrentCreature();
      if (!((ZComponent) v != (object) null))
        return;
      if (v.type == CreatureType.Beehive)
      {
        ZCreatureBeehive zcreatureBeehive = (ZCreatureBeehive) v;
        bool flag = (ZComponent) CameraMovement.Instance.target == (object) v;
        for (int index = 0; index < zcreatureBeehive.bees.Count; ++index)
        {
          if (flag && (ZComponent) zcreatureBeehive.bees[index] != (object) null)
          {
            CameraMovement.Instance.LerpToTransform(zcreatureBeehive.bees[index], true);
            return;
          }
          if ((ZComponent) zcreatureBeehive.bees[index] != (object) null && (ZComponent) zcreatureBeehive.bees[index] == (object) CameraMovement.Instance.target)
            flag = true;
        }
      }
      CameraMovement.Instance.LerpToTransform(v, true);
    }
  }

  public static bool Hidden => (UnityEngine.Object) HUD.instance == (UnityEngine.Object) null || HUD.instance.hidden;

  public void ShowEverything(bool instant)
  {
    if (!this.hidden && !instant || this.game.isReplay)
      return;
    if (this.showHideEverything != null)
      this.StopCoroutine(this.showHideEverything);
    this.hidden = false;
    this.showHideEverything = this.StartCoroutine(this.ieShow(instant ? 0.998f : 0.0f));
  }

  public void HideEverything(bool instant)
  {
    if (this.hidden && !instant)
      return;
    if (this.showHideEverything != null)
      this.StopCoroutine(this.showHideEverything);
    this.hidden = true;
    this.showHideEverything = this.StartCoroutine(this.ieHide(instant ? 0.998f : 0.0f));
  }

  public void ValidateFromChatBox()
  {
    if (this.hidden)
    {
      this.HideEverything(true);
    }
    else
    {
      if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null))
        return;
      Player.Instance.UpdateVisuals();
      this.ShowEverything(true);
    }
  }

  public void PlaceBid()
  {
    string text = this.inputBid.text;
    int num = 0;
    ref int local = ref num;
    if (!int.TryParse(text, out local))
      return;
    int bid = Mathf.Clamp(num, 0, (int) Client.game.gameFacts.startHealth);
    Client.AskToBid((byte) bid);
    ChatBox.Instance.NewChatMsg("You placed a bid of " + (object) bid + " lifepoints", (Color) ColorScheme.GetColor(Global.ColorSystem));
    this.BidPanel.SetActive(false);
  }

  private IEnumerator ieShow(float t)
  {
    Vector2 pos = this.containerSpells.anchoredPosition;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * 5f;
      this.containerSpells.anchoredPosition = Vector2.Lerp(pos, new Vector2(0.0f, this.getChatYOpen()), t);
      yield return (object) new WaitForEndOfFrame();
    }
    this.showHideEverything = (Coroutine) null;
  }

  private IEnumerator ieHide(float t)
  {
    Vector2 pos = this.containerSpells.anchoredPosition;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * 3f;
      this.containerSpells.anchoredPosition = Vector2.Lerp(pos, new Vector2(0.0f, this.getChatY()), t);
      yield return (object) new WaitForEndOfFrame();
    }
    this.showHideEverything = (Coroutine) null;
    ClickSpell.Instance?.MakeSureHidden();
  }

  private float getChatYOpen()
  {
    if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null)
      return 16f;
    RectTransform transform = (RectTransform) ChatBox.Instance.transform;
    return (float) ((double) transform.anchoredPosition.y + (double) transform.sizeDelta.y + 16.0);
  }

  private float getChatY()
  {
    if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null || this.game.serverState.busy == ServerState.Busy.Ended)
      return -352.460022f;
    RectTransform transform = (RectTransform) ChatBox.Instance.transform;
    return (float) ((double) transform.anchoredPosition.y + (double) transform.sizeDelta.y + 16.0 - 380.0);
  }

  public void ClickQuizYes()
  {
    this.quiz.Yes();
    this.quizObject.SetActive(false);
  }

  public void ClickQuizNo()
  {
    this.quiz.No();
    this.quizObject.SetActive(false);
  }

  public void InitQuiz()
  {
    if (!this.usedFourSeasons || PlayerPrefs.GetInt("quizcorrectcount") >= PlayerPrefs.GetInt("quiz_max", 10))
      return;
    this.quiz.Randomize(this.quizTxt);
    this.quizObject.SetActive(true);
  }

  internal void TutorialPopup(string s, Action a, bool next = false, bool pauseGame = false)
  {
    this.turTextbox.text = s;
    this.tutAction = a;
    if (next)
    {
      if (pauseGame)
        Client.game.tutorialPaused = true;
      this.turTextButton.text = "Continue";
    }
    else
      this.turTextButton.text = this.tutHidden ? "Show Tutorial" : "Hide Tutorial";
    if (next && this.tutHidden)
    {
      this.tutHidden = false;
      this.tutPopup.sizeDelta = new Vector2(this.tutPopup.sizeDelta.x, 210f);
      this.tutPopupBorder.enabled = true;
      this.turTextbox.enabled = true;
    }
    this.tutPopup.gameObject.SetActive(true);
    this.tutPopup.anchoredPosition = new Vector2(HUD.UseTouchControls ? -283f : -60f, 0.0f);
  }

  public void ToggleTutorial()
  {
    this.tutHidden = !this.tutHidden;
    this.turTextButton.text = this.tutHidden ? "Show Tutorial" : "Hide Tutorial";
    this.tutPopup.sizeDelta = new Vector2(this.tutPopup.sizeDelta.x, this.tutHidden ? 0.0f : 210f);
    this.tutPopupBorder.enabled = !this.tutHidden;
    this.turTextbox.enabled = !this.tutHidden;
  }

  public void ClickTutorialPopup()
  {
    if (this.tutAction == null)
    {
      this.ToggleTutorial();
    }
    else
    {
      Action tutAction = this.tutAction;
      if (tutAction == null)
        return;
      tutAction();
    }
  }

  [Serializable]
  public class ListOfCharacters
  {
    public List<UIPlayerCharacter> list;
  }
}
