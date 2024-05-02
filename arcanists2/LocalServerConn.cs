

using Educative;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityThreading;

#nullable disable
public class LocalServerConn : Catalogue
{
  public UnityEngine.UI.Text yourip;
  public TMP_InputField input_name;
  public TMP_InputField input_pass;
  public TMP_InputField input_ip;
  public GameObject buttonLocalIp;
  private string ipstring = "";
  public UnityEngine.UI.Text errorTxt;
  public GameObject downloadButton;
  public GameObject updateButton;
  public GameObject discordButton;
  public TextMeshProUGUI discordText;
  public Shader glowMac;
  [Header("Server stuff")]
  public RectTransform serverContainer;
  public GameObject panelServers;
  public PfabServerButton pfabServer;
  [Header("Account Creation")]
  public GameObject panelAccountCreation;
  public TMP_InputField inputNameCreation;
  public TMP_InputField inputPasswordCreation;
  public TMP_InputField inputPassVerifyCreation;
  public Button buttonCreateAccountPopup;
  public Button buttonChangePassword;
  public Button buttonPublicServer;
  [Header("Password Change")]
  public GameObject panelPasswordChange;
  public TMP_InputField inputNamePasswordChange;
  public TMP_InputField inputOrigonalPasswordChange;
  public TMP_InputField inputPasswordPasswordChange;
  public TMP_InputField inputPassVerifyPasswordChange;
  [Header("Password Reset")]
  public GameObject panelReset;
  public GameObject buttonOpenResetPanel;
  [Header("Orb")]
  public GameObject panelOrb;
  public Button toggleOrb;
  public static bool CreatingAccount = false;
  public static bool PasswordChange = false;
  public Toggle testing;
  private bool local;
  private bool localEncryption;
  public static bool UseEncryption = true;
  private bool showOrb = true;
  private List<PfabServerButton> serverButtons = new List<PfabServerButton>();
  public TMP_Text txtCurServer;
  public string filePath = "";
  private float lastPress = -3000f;
  private string lastPass = "";
  private string lastName = "";
  public List<UIElectricity> electricities;
  private float t;
  private int other;

  public static LocalServerConn Instance { get; private set; }

  public TMP_InputField temp => this.input_pass;

  private IEnumerator FindServers()
  {
    LocalServerConn localServerConn1 = this;
    using (UnityWebRequest webRequest = UnityWebRequest.Get("http://play.arcanists2.com/ServerList.json"))
    {
      yield return (object) webRequest.SendWebRequest();
      if (webRequest.isNetworkError)
      {
        UnityEngine.Debug.Log((object) ("Error: " + webRequest.error));
      }
      else
      {
        KnownServersList knownServersList = JsonConvert.DeserializeObject<KnownServersList>(webRequest.downloadHandler.text);
        Server.serverList = knownServersList;
        if (knownServersList.servers.Count > 1)
        {
          localServerConn1.panelServers.SetActive(true);
          foreach (KnownServers server in knownServersList.servers)
          {
            LocalServerConn localServerConn = localServerConn1;
            PfabServerButton g = UnityEngine.Object.Instantiate<PfabServerButton>(localServerConn1.pfabServer, (Transform) localServerConn1.serverContainer);
            g.gameObject.SetActive(true);
            g.txtName.text = server.name + " (" + server.location + ")";
            g.Ping(server.ip);
            KnownServers b = server;
            g.uibutton.onClick.AddListener((UnityAction) (() =>
            {
              foreach (PfabServerButton serverButton in localServerConn.serverButtons)
                serverButton.uibutton.AlwaysOn = false;
              g.uibutton.AlwaysOn = true;
              localServerConn.SelectServer(b);
            }));
            localServerConn1.serverButtons.Add(g);
            if (string.Equals(server.ip, PlayerPrefs.GetString("prefserver", knownServersList.servers[0].ip)))
            {
              localServerConn1.txtCurServer.text = "Server: " + server.name + " (" + server.location + ")";
              g.uibutton.AlwaysOn = true;
            }
          }
        }
        else if (knownServersList.servers.Count > 0)
        {
          KnownServers server = knownServersList.servers[0];
          localServerConn1.txtCurServer.text = "Server: " + server.name + " (" + server.location + ")";
          PlayerPrefs.SetString("prefserver", knownServersList.servers[0].ip);
        }
      }
    }
  }

  private void SelectServer(KnownServers x)
  {
    this.txtCurServer.text = "Server: " + x.name + " (" + x.location + ")";
    PlayerPrefs.SetString("prefserver", x.ip);
  }

  private void Awake()
  {
    Client.clan = (Clan) null;
    LocalServerConn.Instance = this;
    FileAssociations.EnsureAssociationsSet();
    this.showOrb = Global.GetPrefBool("prefshoworb", false);
    this.panelOrb.SetActive(this.showOrb);
    this.toggleOrb.onClick.AddListener(new UnityAction(this.ToggleOrb));
    Dispatcher dispatcher = UnityThreadHelper.Dispatcher;
    this.StartCoroutine(this.FindServers());
  }

  [ContextMenu("Play replay")]
  private void doreplay()
  {
    if (!this.filePath.EndsWith(".arcreplay"))
      return;
    ZGame.replayShowStartPanel = true;
    ZGame.targetTimelineFrame = -1;
    ZGame.init_Replay(this.filePath);
  }

  private void Server_ReceivedError(int arg1, Exception arg2)
  {
    UnityEngine.Debug.LogError((object) (arg1.ToString() + " " + arg2.ToString()));
  }

  private void ToggleOrb()
  {
    this.showOrb = !this.showOrb;
    Global.SetPrefBool("prefshoworb", this.showOrb);
    this.panelOrb.SetActive(this.showOrb);
  }

  public void ResetPassword()
  {
    Global.OpenURL("https://discord.com/api/oauth2/authorize?response_type=token&client_id=633505532753346580&redirect_uri=http%3A%2F%2F18.218.134.19%3A8080&scope=identify&state=ResetPassword");
    this.panelReset.SetActive(false);
  }

  public void ClickOpenPasswordReset() => this.panelReset.SetActive(true);

  public void CancelPasswordReset() => this.panelReset.SetActive(false);

  private void Start()
  {
    RatingsMenu.OnLogout();
    EmojiInfo.Intitialize();
    foreach (string commandLineArg in Environment.GetCommandLineArgs())
    {
      if (commandLineArg.EndsWith(".arcreplay", StringComparison.OrdinalIgnoreCase))
      {
        Client.Name = PlayerPrefs.GetString("prefName", "");
        if (string.IsNullOrEmpty(Client.Name))
          Client.Name = "Player";
        ZGame.replayShowStartPanel = true;
        ZGame.targetTimelineFrame = -1;
        ZGame.init_Replay(commandLineArg);
        return;
      }
      if (commandLineArg.EndsWith(".arcTutorial2", StringComparison.OrdinalIgnoreCase))
      {
        Tutorial tutorial = Tutorial.FromCodeOnly(System.IO.File.ReadAllText(commandLineArg));
        tutorial.debugLog = true;
        tutorial.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
        return;
      }
      if (commandLineArg.EndsWith(".arcTutorial", StringComparison.OrdinalIgnoreCase))
      {
        Tutorial tutorial = Tutorial.FromJson(System.IO.File.ReadAllText(commandLineArg));
        tutorial.debugLog = true;
        tutorial.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
        return;
      }
    }
    FixedInt fixedInt = (FixedInt) 0.1f;
    this.input_name.text = PlayerPrefs.GetString("prefName", "");
    this.input_ip.text = PlayerPrefs.GetString("localispserver", "127.0.0.1");
    this.ipstring = LocalServerConn.getIpAddress();
    this.yourip.text = !string.IsNullOrEmpty(this.ipstring) ? "Your Local IPAddress is: <color=green>" + this.ipstring + "</color>" : "";
    Inert.LoadSettingsPlayer();
    string[] commandLineArgs = Environment.GetCommandLineArgs();
    if (commandLineArgs.Length > 1 && string.Equals(commandLineArgs[1].ToLower(), "local", StringComparison.OrdinalIgnoreCase))
      this.EnableLocal();
    else if (commandLineArgs.Length > 1 && string.Equals(commandLineArgs[1].ToLower(), "localencryption", StringComparison.OrdinalIgnoreCase))
    {
      this.localEncryption = true;
      this.EnableLocal();
    }
    if (this.local && !this.localEncryption)
    {
      this.input_pass.gameObject.SetActive(false);
      this.buttonCreateAccountPopup.gameObject.SetActive(false);
      this.buttonChangePassword.gameObject.SetActive(false);
      this.buttonPublicServer.gameObject.SetActive(false);
      this.testing.gameObject.SetActive(false);
    }
    if (!LocalServerConn.UseEncryption)
    {
      this.localEncryption = false;
      this.input_pass.gameObject.SetActive(false);
      this.buttonCreateAccountPopup.gameObject.SetActive(false);
      this.buttonChangePassword.gameObject.SetActive(false);
      this.testing.gameObject.SetActive(false);
    }
    for (int index = 0; index < 8; ++index)
      this.electricities[index].currentAngle = (float) (-21.25 * (double) index - 95.0);
    for (int index = 8; index < 16; ++index)
      this.electricities[index].currentAngle = (float) (21.25 * (double) (index - 8) - 85.0);
    if (!string.IsNullOrEmpty(this.input_name.text))
    {
      if (this.input_pass.gameObject.activeSelf)
        this.input_pass.Select();
    }
    else
      this.input_name.Select();
    if (string.IsNullOrEmpty(this.input_name.text))
      return;
    Tuple<string, string> tuple = new Secure.SlashS().UnitTests(this.input_name.text);
    ZPlayerPrefs.Initialize(tuple.Item2.ToString(), tuple.Item1.ToString());
    string str = ZPlayerPrefs.GetString("test1", "");
    this.temp.text = str;
    this.testing.isOn = !string.IsNullOrEmpty(str);
  }

  private void EnableLocal()
  {
    this.local = true;
    this.yourip.gameObject.SetActive(true);
    this.input_ip.gameObject.SetActive(true);
    this.buttonLocalIp.SetActive(true);
  }

  public void clickDownload()
  {
    Global.OpenURL("https://drive.google.com/drive/folders/1FUi9IcaE8Ny2rF-wVJJiw417JoFYRwyD");
    Application.Quit();
  }

  public void clickDiscord() => Global.OpenURL(Server.discordLink);

  public void Cancel() => Application.Quit();

  public void Error(string s)
  {
    if (s == null)
      s = "";
    this.errorTxt.text = s;
    this.errorTxt.gameObject.SetActive(true);
    if (!string.Equals(s, "Invalid Name and/or Password"))
      return;
    this.buttonOpenResetPanel.SetActive(true);
  }

  public void StartLocalServer()
  {
    if ((UnityEngine.Object) Server.Instance != (UnityEngine.Object) null)
      return;
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) new GameObject("Server", new System.Type[1]
    {
      typeof (Server)
    }));
  }

  public void PlayOffline()
  {
    Client.offlineMode = true;
    Client.Name = this.input_name.text.Trim();
    if (string.IsNullOrEmpty(Client.Name) || Client.Name.Length > 13)
    {
      this.Error("Invalid Name");
    }
    else
    {
      PlayerPrefs.SetString("prefName", Client.Name);
      Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
      Account acc = new Account();
      acc.name = Client.Name;
      Client.cosmetics = Server.DefaultCosmetics;
      acc.cosmetics = Client.cosmetics;
      acc.cosmetics.spells.ResetAll();
      Prestige.Unlock(acc, BookOf.Arcane, true, true);
      Prestige.Unlock(acc, BookOf.Flame, true);
      Client._accounts[acc.name] = acc;
      Client.MyAccount = acc;
    }
  }

  public void clickUpdate()
  {
    new Process()
    {
      StartInfo = {
        WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
        FileName = "Update.exe",
        Arguments = string.Join(" ", Environment.GetCommandLineArgs())
      }
    }.Start();
    Application.Quit();
  }

  public void ConnectAndStartServer()
  {
    this.StartLocalServer();
    if (Input.GetKey(KeyCode.LeftControl))
      return;
    Controller.Instance.StartCoroutine(this.AfterStartConnect());
  }

  public IEnumerator AfterStartConnect()
  {
    while (!Server.IsReady)
      yield return (object) null;
    this.ConnectToLocalServer();
  }

  public void ConnectToLocalServer()
  {
    LocalServerConn.CreatingAccount = this.panelAccountCreation.activeSelf;
    LocalServerConn.PasswordChange = false;
    if (!LocalServerConn.CreatingAccount)
      LocalServerConn.PasswordChange = this.panelPasswordChange.activeSelf;
    Client.offlineMode = false;
    Client.Name = this.input_name.text.Trim();
    if (string.IsNullOrEmpty(Client.Name) || Client.Name.Length > 13)
    {
      this.Error("Invalid Name");
    }
    else
    {
      PlayerPrefs.SetString("prefName", Client.Name);
      string a = this.input_ip.text;
      if (a == "")
        a = "127.0.0.1";
      PlayerPrefs.SetString("localispserver", a);
      this.Error("");
      if (!this.localEncryption)
        LocalServerConn.UseEncryption = false;
      if (string.Equals(a, "127.0.0.1", StringComparison.OrdinalIgnoreCase) || string.Equals(a, this.ipstring, StringComparison.OrdinalIgnoreCase))
        Client.ConnectToLocalServer();
      else
        Client.ConnectToLocalServer();
    }
  }

  public void ConnectToMasterServer()
  {
    if ((double) this.lastPress > (double) Time.realtimeSinceStartup && string.Equals(this.lastPass, this.input_pass.text) && string.Equals(this.lastName, this.input_name.text))
      return;
    this.lastPress = Time.realtimeSinceStartup + 3f;
    this.lastPass = this.input_pass.text;
    this.lastName = this.input_name.text;
    LocalServerConn.CreatingAccount = this.panelAccountCreation.activeSelf;
    LocalServerConn.PasswordChange = false;
    if (!LocalServerConn.CreatingAccount)
      LocalServerConn.PasswordChange = this.panelPasswordChange.activeSelf;
    Client.offlineMode = false;
    Client.Name = this.input_name.text;
    PlayerPrefs.SetString("prefName", Client.Name);
    this.Error("");
    Client.ConnectToServer();
  }

  public static string getIpAddress()
  {
    string ipAddress = "<color=red>No Internet Connection could be found</color>";
    try
    {
      using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP))
      {
        socket.Connect("8.8.8.8", 65530);
        ipAddress = (socket.LocalEndPoint as IPEndPoint).Address.ToString();
      }
    }
    catch (Exception ex)
    {
      UnityEngine.Debug.LogError((object) ex);
    }
    return ipAddress;
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) LocalServerConn.Instance == (UnityEngine.Object) this)
      LocalServerConn.Instance = (LocalServerConn) null;
    Tuple<string, string> tuple = new Secure.SlashS().UnitTests(this.input_name.text);
    ZPlayerPrefs.Initialize(tuple.Item2.ToString(), tuple.Item1.ToString());
    ZPlayerPrefs.SetString("test1", this.testing.isOn ? this.temp.text : "");
  }

  public void ClickOpenPasswordChange()
  {
    this.panelPasswordChange.SetActive(true);
    this.inputNamePasswordChange.Select();
  }

  public void ClickCancelPasswordChange()
  {
    this.panelPasswordChange.SetActive(false);
    this.input_name.Select();
    this.Error("");
  }

  public void ClickOpenCreateAccountScreen()
  {
    this.panelAccountCreation.SetActive(true);
    this.inputNameCreation.Select();
  }

  public void ClickCancelCreateAccountScreen()
  {
    this.panelAccountCreation.SetActive(false);
    this.input_name.Select();
    this.Error("");
  }

  public void ClickChangePassword()
  {
    string text1 = this.inputNamePasswordChange.text;
    string text2 = this.inputOrigonalPasswordChange.text;
    string text3 = this.inputPasswordPasswordChange.text;
    string text4 = this.inputPassVerifyPasswordChange.text;
    string s = Account.ValidPassword(text3);
    if (s != null)
      this.Error(s);
    else if (!string.Equals(text3, text4))
    {
      this.Error("Passwords do not match");
    }
    else
    {
      this.Error("");
      this.input_name.text = text1;
      this.input_pass.text = text2;
      if (this.local)
        this.ConnectToLocalServer();
      else
        this.ConnectToMasterServer();
    }
  }

  public void ClickCreateAccount()
  {
    string text1 = this.inputNameCreation.text;
    string text2 = this.inputPasswordCreation.text;
    string text3 = this.inputPassVerifyCreation.text;
    string s1 = Account.ValidName(text1);
    if (s1 != null)
    {
      this.Error(s1);
    }
    else
    {
      string s2 = Account.ValidPassword(text2);
      if (s2 != null)
        this.Error(s2);
      else if (!string.Equals(text2, text3))
      {
        this.Error("Passwords do not match");
      }
      else
      {
        this.Error("");
        this.input_name.text = text1;
        this.input_pass.text = text2;
        if (this.local)
          this.ConnectToLocalServer();
        else
          this.ConnectToMasterServer();
      }
    }
  }

  public void ValidateName(string s)
  {
    string s1 = Account.ValidName(s);
    if (s1 == null)
    {
      IEnumerator enumerator = WordFilter.GetBadWords(s).GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          this.Error("Inappropriate Phrase: " + ((Capture) enumerator.Current).Value + " - if you try to circumnavigate this, you may be banned.");
          return;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    this.Error(s1);
  }

  public void ValidatePassword(string s) => this.Error(Account.ValidPassword(s));

  public void ValidateSecondPassword(string s)
  {
    if (string.Equals(s, this.inputPasswordCreation.text))
      this.Error("");
    else
      this.Error("Passwords do not match");
  }

  public void ValidateSecondChangePassword(string s)
  {
    if (string.Equals(s, this.inputPasswordPasswordChange.text))
      this.Error("");
    else
      this.Error("Passwords do not match");
  }

  private void Update()
  {
    this.t += Time.deltaTime * 30f;
    if ((double) this.t > 1.0)
    {
      this.t = 0.0f;
      Vector2 zero1 = Vector2.zero;
      Vector2 zero2 = Vector2.zero;
      if (this.other == 4)
        this.other = 0;
      int num1 = (int) ((RectTransform) this.transform.parent).sizeDelta.x / 2;
      int num2 = (int) ((RectTransform) this.transform.parent).sizeDelta.y / 2;
      float num3 = 25f * Time.deltaTime;
      for (int index = 0; index < this.electricities.Count; ++index)
      {
        UIElectricity electricity = this.electricities[index];
        float num4 = UnityEngine.Random.Range(0.0f, 1.5f) + num3;
        if ((double) electricity.currentAngle <= -90.0)
        {
          electricity.currentAngle -= num4;
          if ((double) electricity.currentAngle < -265.0)
            electricity.currentAngle += 170f;
        }
        else
        {
          electricity.currentAngle += num4;
          if ((double) electricity.currentAngle > 85.0)
            electricity.currentAngle -= 170f;
        }
        Vector2 singlePrecision = Global.VelocityRight((FixedInt) electricity.currentAngle, (FixedInt) 385).ToSinglePrecision();
        Vector2 rectEdgeRad = LocalServerConn.projectToRectEdgeRad(electricity.currentAngle, (float) (num1 * 2), (float) (num2 * 2));
        this.electricities[index].Setup(singlePrecision, rectEdgeRad, this.other == 0);
      }
      ++this.other;
    }
    if (!Input.anyKeyDown)
      return;
    if (Input.GetKeyDown(KeyCode.Return))
    {
      GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
      if (!((UnityEngine.Object) selectedGameObject != (UnityEngine.Object) null))
        return;
      selectedGameObject.GetComponent<UINavigation>()?.OnSubmit();
    }
    else if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.DownArrow))
    {
      GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
      if (!((UnityEngine.Object) selectedGameObject != (UnityEngine.Object) null))
        return;
      selectedGameObject.GetComponent<UINavigation>()?.OnSelect();
    }
    else
    {
      if (!Input.GetKeyDown(KeyCode.UpArrow))
        return;
      GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
      if (!((UnityEngine.Object) selectedGameObject != (UnityEngine.Object) null))
        return;
      selectedGameObject.GetComponent<UINavigation>()?.OnBack();
    }
  }

  public float getAngle(Vector2 me, Vector2 target)
  {
    return Mathf.Atan2(target.y - me.y, target.x - me.x) * 57.2957764f;
  }

  public float getAngle(Vector2 target) => Mathf.Atan2(target.y, target.x) * 57.2957764f;

  public static Vector2 projectToRectEdgeRad(float angle, float width, float height)
  {
    float num1 = 6.28318548f;
    float f = (float) ((double) angle * 3.1415927410125732 / 180.0);
    while ((double) f < -3.1415927410125732)
      f += num1;
    while ((double) f > 3.1415927410125732)
      f -= num1;
    float num2 = Mathf.Atan2(height, width);
    float num3 = Mathf.Tan(f);
    Vector2 rectEdgeRad;
    if ((double) f > -(double) num2 && (double) f <= (double) num2)
    {
      rectEdgeRad.x = width / 2f;
      rectEdgeRad.y = width / 2f * num3;
    }
    else if ((double) f > (double) num2 && (double) f <= 3.1415927410125732 - (double) num2)
    {
      rectEdgeRad.x = height / 2f / num3;
      rectEdgeRad.y = height / 2f;
    }
    else if (((double) f > 3.1415927410125732 - (double) num2 || (double) f <= 3.1415927410125732 + (double) num2) && (double) Mathf.Abs((float) (-(double) width / 2.0) * num3) < (double) height / 2.0)
    {
      rectEdgeRad.x = (float) (-(double) width / 2.0);
      rectEdgeRad.y = (float) (-(double) width / 2.0) * num3;
    }
    else
    {
      rectEdgeRad.x = (float) (-(double) height / 2.0) / num3;
      rectEdgeRad.y = (float) (-(double) height / 2.0);
    }
    return rectEdgeRad;
  }
}
