// Decompiled with JetBrains decompiler
// Type: Controller
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using Hazel;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

#nullable disable
public class Controller : MonoBehaviour
{
  public bool isActive = true;
  public bool useColorScheme;
  public Controller schemeController;
  public static float realtimeSinceStartup = 0.0f;
  public Inert inert;
  public ClientResources clientResources;
  public RectTransform canvasRect;
  public GameObject MenuLogin;
  public GameObject MenuMain;
  public GameObject MenuLobby;
  public GameObject MenuUnrated;
  public GameObject MenuRated;
  public GameObject MenuCharacterCreation;
  public GameObject MenuSpellSelection;
  public GameObject MenuOptions;
  public GameObject MenuHUD;
  public GameObject MenuScreenSize;
  public GameObject MenuReplay;
  public GameObject MenuControls;
  public GameObject MenuRatings;
  public GameObject MenuAchievements;
  public GameObject MenuPrestige;
  public GameObject PopupSavedSpells;
  public GameObject PopupSavedOutfits;
  public GameObject RewindingPanel;
  public GameObject MenuPatchNotes;
  public GameObject MenuCredits;
  public GameObject MenuClanList;
  public GameObject MenuTutorialEditor;
  public GameObject MenuTutorialCodeEditor;
  public GameObject MenuColorScheme;
  public ChessUI chessui;
  public GameObject mapObj;
  public GameObject mapMeterObj;
  public GameObject miniCamera;
  public GameObject chatBox;
  public GameObject notification;
  public GameObject notificationerror;
  public GameObject MenuReport;
  public GameObject MenuProcessReports;
  public GameObject MenuContextMenu;
  public MyToolTip MenuToolTip;
  public MyToolTip MenuToolTip2;
  public MyPopup MyPopupItem;
  public UIAchievement uIAchievement;
  public GameObject MenuEmoji;
  public QuickchatUI quickchat;
  public CosmeticsMenuDev cosmeticsMenu;
  public SpellLobbyChange spellLobbyChange;
  public ChooseJsonDialog dialogChooseJson;
  public PopupRestrictions dialogRestrictions;
  public MyFilePicker dialogFilePicker;
  public SpellOverridesUI spellOverrides;
  public MyPollUI mypoll;
  public EmojiSelector emojiSelector;
  public MyPollResults mypollResults;
  public Newsfeed newsfeed;
  public GameObject iconRecordVideo;
  public ColorPickerUI colorPickerUI;
  public MyMessageBox messageBox;
  public KeyBoard mobileKeyboard;
  public static Camera miniCameraObj;
  public static List<GameObject> _gameObjects = new List<GameObject>();
  public static string OperatingSystem = "None";
  public static string HWID = "None";
  private GameObject gameBG;
  private int lastScreenHeight = -1;
  private int lastScreenWidth = -1;
  public static bool ApplicationFocused = true;
  public Controller otherController;
  [Header("Fonts")]
  public TMP_FontAsset fontArc;
  public TMP_FontAsset fontDef;
  private float _keepalive;
  private float _removeTempIgnored;
  internal List<Controller.Stack> stack = new List<Controller.Stack>();
  private GameObject openedMenu;
  private GameObject openHandle;

  public static Controller Instance { get; private set; }

  public static TMP_FontAsset GetFont(int index)
  {
    return index != 0 ? Controller.Instance.fontArc : Controller.Instance.fontDef;
  }

  private void OnApplicationQuit()
  {
    if (!this.isActive)
      return;
    ClientResources.Instance?.OnApplicationQuit();
  }

  public void ActivateOther(bool mainmenu = true)
  {
  }

  private void OnApplicationFocus(bool focus)
  {
    if (!this.isActive)
      return;
    Controller.ApplicationFocused = focus;
    if (!focus)
      return;
    MapObjects.Instance?.SetWaves();
  }

  public Task<TRet> StartNew<TRet>(Func<TRet> func) => Task.FromResult<TRet>(func());

  public async void tt()
  {
    Task task = await this.StartNew<Task>((Func<Task>) (() =>
    {
      Debug.Log((object) ("Time: " + (object) Time.realtimeSinceStartup));
      return Task.CompletedTask;
    }));
  }

  private void OnEnable()
  {
    if (!this.isActive)
      return;
    Screen.sleepTimeout = -1;
  }

  public GameObject CreateAndApply(GameObject t, Transform parent)
  {
    GameObject g = UnityEngine.Object.Instantiate<GameObject>(t, parent);
    if (this.useColorScheme && t.name.Contains("2"))
      Client.colorScheme?.Apply(g);
    return g;
  }

  public T CreateAndApply<T>(T t, Transform parent) where T : MonoBehaviour
  {
    T andApply = UnityEngine.Object.Instantiate<T>(t, parent);
    if (this.useColorScheme && t.name.Contains("2"))
      Client.colorScheme?.Apply(andApply.gameObject);
    return andApply;
  }

  public void ApplyScheme(GameObject t)
  {
    if (!this.useColorScheme || !t.name.Contains("2"))
      return;
    Client.colorScheme?.Apply(t);
  }

  public static void ShowPrestigeMenu()
  {
    if ((UnityEngine.Object) PrestigeMenu.Instance == (UnityEngine.Object) null)
      Controller.Instance.Push(Controller.Instance.CreateAndApply(Controller.Instance.MenuPrestige, Controller.Instance.transform));
    else
      PrestigeMenu.Instance?.transform.SetAsLastSibling();
  }

  public static void ShowSettingsMenu(bool force = false)
  {
    if (!((UnityEngine.Object) OptionsMenu.Instance == (UnityEngine.Object) null | force))
      return;
    Controller.Instance.Push(Controller.Instance.CreateAndApply(Controller.Instance.MenuOptions, Controller.Instance.transform));
  }

  public static void ShowScreenSizeMenu()
  {
    if (!((UnityEngine.Object) ScreenSizeMenu.Instance == (UnityEngine.Object) null))
      return;
    Controller.Instance.Push(Controller.Instance.CreateAndApply(Controller.Instance.MenuScreenSize, Controller.Instance.transform));
  }

  public static void ShowControlsMenu()
  {
    if (!((UnityEngine.Object) KeyBindMenu.Instance == (UnityEngine.Object) null))
      return;
    Controller.Instance.Push(Controller.Instance.CreateAndApply(Controller.Instance.MenuControls, Controller.Instance.transform));
  }

  public static void CreateNotification(string x)
  {
    if (!((UnityEngine.Object) Controller.Instance != (UnityEngine.Object) null))
      return;
    Controller.Instance.CreateAndApply(Controller.Instance.notification, Controller.Instance.transform).GetComponent<NotificationMenu>().Init(x);
  }

  public static void CreateErrorNotification(string x)
  {
    if (!((UnityEngine.Object) Controller.Instance != (UnityEngine.Object) null))
      return;
    Controller.Instance.CreateAndApply(Controller.Instance.notificationerror, Controller.Instance.transform).GetComponent<NotificationMenu>().Init(x);
  }

  public static void CreateEmojiMenu()
  {
    if (!((UnityEngine.Object) Controller.Instance != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Instantiate<GameObject>(Controller.Instance.MenuEmoji, Controller.Instance.transform);
  }

  private void Awake()
  {
    if (!this.isActive)
      return;
    Controller.Instance = this;
    if ((UnityEngine.Object) this.inert != (UnityEngine.Object) null && this.inert.spells.Count == 0)
    {
      Application.runInBackground = true;
      if ((UnityEngine.Object) this.inert != (UnityEngine.Object) null)
        Inert.Init(this.inert);
      if ((UnityEngine.Object) this.clientResources != (UnityEngine.Object) null)
        ClientResources.Init(this.clientResources);
    }
    else
    {
      Inert.Instance = this.inert;
      ClientResources.Instance = this.clientResources;
    }
    if ((UnityEngine.Object) this.MenuLogin != (UnityEngine.Object) null)
      this.OpenMenu(this.MenuLogin, false);
    Controller.OperatingSystem = SystemInfo.operatingSystem;
    Controller.HWID = SystemInfo.deviceUniqueIdentifier;
    if (Controller.HWID.Length < 6)
    {
      byte[] numArray = new byte[20];
      using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
      {
        cryptoServiceProvider.GetBytes(numArray);
        Controller.HWID = Convert.ToBase64String(numArray);
      }
    }
    this.mobileKeyboard.Awake();
  }

  private void Start()
  {
    if (!this.isActive)
      return;
    Client.CheckBloodToggle();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) Controller.Instance == (UnityEngine.Object) this))
      return;
    Controller.Instance = (Controller) null;
  }

  public void RefreshKeepAliveTimer() => this._keepalive = Time.realtimeSinceStartup + 20f;

  private void Update()
  {
    if (!this.isActive)
      return;
    Controller.realtimeSinceStartup = Time.realtimeSinceStartup;
    if (this.lastScreenHeight != Screen.height || this.lastScreenWidth != Screen.width)
    {
      this.lastScreenWidth = Screen.width;
      this.lastScreenHeight = Screen.height;
      if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
        HUD.instance.SetupMiniCamera();
      if (this.useColorScheme)
        ColorScheme.Resolution(this.gameObject, true);
    }
    if ((double) this._removeTempIgnored < (double) Time.realtimeSinceStartup)
    {
      this._removeTempIgnored = Time.realtimeSinceStartup + 10f;
      Client.tempIgnored.RemoveByTime();
    }
    if (Client.isConnected && (double) this._keepalive < (double) Time.realtimeSinceStartup)
    {
      this._keepalive = Time.realtimeSinceStartup + 20f;
      Client.connection.SendBytes(new byte[1]{ (byte) 34 });
      UnityEngine.ClusterModule.Resources.Release();
      if (Spectator.isConnected)
        Spectator.connection.SendBytes(new byte[1]
        {
          (byte) 34
        });
    }
    if (this.stack.Count > 0 && Input.GetKeyDown(KeyCode.Escape))
      this.Pop();
    if (!Input.GetKeyDown(KeyCode.F12) || !Input.GetKey(KeyCode.LeftShift) || !Client.MyAccount.accountType.isDev() || !((UnityEngine.Object) HUD.instance == (UnityEngine.Object) null))
      return;
    SpellOverridesUI.Create();
  }

  public void ProcessReports()
  {
    if ((UnityEngine.Object) ProcessReportMenu.instance == (UnityEngine.Object) null)
      UnityEngine.Object.Instantiate<GameObject>(this.MenuProcessReports, this.transform);
    ProcessReportMenu.instance?.Init();
  }

  public void Report(string player)
  {
    if ((UnityEngine.Object) ReportMenu.instance == (UnityEngine.Object) null)
      UnityEngine.Object.Instantiate<GameObject>(this.MenuReport, this.transform);
    ReportMenu.instance?.Show(player);
  }

  public void Pop()
  {
    for (int index = this.stack.Count - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) this.stack[index].current == (UnityEngine.Object) null)
      {
        this.stack.RemoveAt(index);
      }
      else
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.stack[index].current);
        this.stack.RemoveAt(index);
        break;
      }
    }
  }

  public void Push(GameObject g)
  {
    this.stack.Add(new Controller.Stack() { current = g });
  }

  public static void ShowPopup(GameObject m)
  {
    Controller.Instance.Push(Controller.Instance.CreateAndApply(m, Controller.Instance.transform));
  }

  public static void ShowPopup(CreditsMenu.Type t, string txt = null)
  {
    GameObject andApply = Controller.Instance.CreateAndApply(Controller.Instance.MenuCredits, Controller.Instance.transform);
    andApply.GetComponent<CreditsMenu>().Init(t, txt);
    Controller.Instance.Push(andApply);
  }

  public static void CloseDialogs()
  {
    if ((UnityEngine.Object) ChangeSpellBookMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ChangeSpellBookMenu.Instance.gameObject);
    if ((UnityEngine.Object) ChangeOutfitMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ChangeOutfitMenu.Instance.gameObject);
    if ((UnityEngine.Object) ElementalSelection.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ElementalSelection.Instance.gameObject);
    if ((UnityEngine.Object) SpellLobbyChange.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) SpellLobbyChange.Instance.gameObject);
    if ((UnityEngine.Object) OptionsMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) OptionsMenu.Instance.gameObject);
    if ((UnityEngine.Object) KeyBindMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) KeyBindMenu.Instance.gameObject);
    if ((UnityEngine.Object) ScreenSizeMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ScreenSizeMenu.Instance.gameObject);
    if ((UnityEngine.Object) ChooseJsonDialog.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) ChooseJsonDialog.Instance.gameObject);
    if ((UnityEngine.Object) PopupRestrictions.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) PopupRestrictions.Instance.gameObject);
    if ((UnityEngine.Object) RatingsMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) RatingsMenu.Instance.gameObject);
    if ((UnityEngine.Object) CharacterCreation.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) CharacterCreation.Instance.gameObject);
    for (int index = 0; index < Controller.Instance.stack.Count; ++index)
    {
      if (index < Controller.Instance.stack.Count && (UnityEngine.Object) Controller.Instance.stack[index].current != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) Controller.Instance.stack[index].current);
    }
    Controller.Instance.stack.Clear();
  }

  public void ReopenMenu()
  {
    if ((UnityEngine.Object) this.openedMenu == (UnityEngine.Object) null)
      return;
    int num = (UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null ? 1 : 0;
    if ((UnityEngine.Object) this.openHandle != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.openHandle);
    this.openHandle = Controller.Instance.CreateAndApply(this.openedMenu, (Transform) this.canvasRect);
    if (num == 0)
      return;
    List<PfabChatMsg.contain> getList = ChatBox.Instance.recycled.GetList;
    this.DestroyChatBox();
    this.ShowChatBox();
    foreach (PfabChatMsg.contain contain in getList)
      ChatBox.Instance.recycled.Add(contain);
  }

  public void OpenMenu(GameObject m, bool showchatbox)
  {
    if ((UnityEngine.Object) this.openedMenu == (UnityEngine.Object) m)
      return;
    MyContextMenu.CloseInstance();
    MyToolTip.Close();
    this.DestroyMap();
    Controller.CloseDialogs();
    if ((UnityEngine.Object) this.gameBG == (UnityEngine.Object) null)
      this.gameBG = Camera.main.transform.GetChild(0).gameObject;
    this.gameBG.SetActive(false);
    this.openedMenu = m;
    if ((UnityEngine.Object) this.openHandle != (UnityEngine.Object) null)
    {
      Catalogue component = this.openHandle.GetComponent<Catalogue>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.Kill();
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) this.openHandle);
    }
    this.openHandle = Controller.Instance.CreateAndApply(m, (Transform) this.canvasRect);
    if (showchatbox)
      this.ShowChatBox();
    else
      this.DestroyChatBox();
    if ((UnityEngine.Object) ChessUI.Instance != (UnityEngine.Object) null)
      ChessUI.Instance.transform.SetAsLastSibling();
    if ((UnityEngine.Object) ReportMenu.instance != (UnityEngine.Object) null)
      ReportMenu.instance.Close();
    if ((UnityEngine.Object) ProcessReportMenu.instance != (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) m == (UnityEngine.Object) this.MenuLobby || (UnityEngine.Object) m == (UnityEngine.Object) this.MenuUnrated)
        ProcessReportMenu.instance.transform.SetAsLastSibling();
      else
        ProcessReportMenu.instance.Close();
    }
    PrestigeMenu.Instance?.ClickClose();
    MyMessageBox.Instance?.transform?.SetAsLastSibling();
  }

  public bool IsPopupAbove()
  {
    Transform child = this.transform.GetChild(this.transform.childCount - 1);
    return (UnityEngine.Object) child.GetComponent<MyToolTip>() != (UnityEngine.Object) null || (UnityEngine.Object) child.GetComponent<MyContextMenu>() != (UnityEngine.Object) null;
  }

  public void InitMap(bool sandbox, bool tutorial = false)
  {
    Controller.CloseDialogs();
    if (!tutorial)
      Client._tutorial = (Tutorial) null;
    if ((UnityEngine.Object) this.openHandle != (UnityEngine.Object) null)
    {
      Catalogue component = this.openHandle.GetComponent<Catalogue>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.Kill();
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) this.openHandle);
      this.openHandle = (GameObject) null;
    }
    Controller._gameObjects.Add(UnityEngine.Object.Instantiate<GameObject>(this.mapObj));
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mapMeterObj);
    for (int index = 0; index < gameObject.transform.childCount; ++index)
      gameObject.transform.GetChild(index).gameObject.SetActive(false);
    Controller._gameObjects.Add(gameObject);
    Controller.miniCameraObj = UnityEngine.Object.Instantiate<GameObject>(this.miniCamera).GetComponent<Camera>();
    Controller._gameObjects.Add(Controller.miniCameraObj.gameObject);
    if (!sandbox)
      this.ShowChatBox(false);
    this.openedMenu = this.MenuHUD;
    this.openHandle = UnityEngine.Object.Instantiate<GameObject>(this.MenuHUD, (Transform) this.canvasRect);
    this.openHandle.transform.SetAsFirstSibling();
    if (!((UnityEngine.Object) ProcessReportMenu.instance != (UnityEngine.Object) null))
      return;
    ProcessReportMenu.instance.Close();
  }

  public void DestroyMap(bool resyncing = false, bool disconnect = true)
  {
    if (Client.game == null)
      return;
    if (disconnect)
      Spectator.Disconnect();
    try
    {
      if (Client.game.serverUpdate != null)
        Timing.KillCoroutines(Client.game.serverUpdate);
      Client.game.serverUpdate = (IEnumerator<float>) null;
      foreach (GameObject gameObject in Controller._gameObjects)
      {
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) gameObject);
      }
      Controller._gameObjects.Clear();
      Client.game?.CleanUp(resyncing);
      Client.game = (ZGame) null;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public void ShowChatBox(bool defaultPosition = true)
  {
    if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null)
      Controller.Instance.CreateAndApply(this.chatBox, (Transform) this.canvasRect);
    ChatBox.Instance.UpdatePostion(defaultPosition);
    ChatBox.Instance.SetActive(defaultPosition || Client.game != null && Client.game.isSandbox || Global.GetPrefBool("prefChatHIdden", false));
    ChatBox.Instance.AllowInput(defaultPosition || Client.game.isSpectator && ChatBox.Instance.Active || Client.game.isSandbox && !Client.game.isTutorial);
    ChatBox.Instance.transform.SetAsLastSibling();
  }

  public void DestroyChatBox()
  {
    if (!((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) ChatBox.Instance.gameObject);
    ChatBox.Instance = (ChatBox) null;
  }

  internal class Stack
  {
    public GameObject current;
  }
}
