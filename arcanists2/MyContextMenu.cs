// Decompiled with JetBrains decompiler
// Type: MyContextMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using Hazel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class MyContextMenu : MonoBehaviour
{
  public GameObject item;
  public GameObject itemWithImage;
  public RectTransform container;
  private Action onCancel;
  public static Color ColorRed = (Color) new Color32((byte) 194, (byte) 80, (byte) 65, byte.MaxValue);
  public static Color ColorGreen = (Color) new Color32((byte) 85, (byte) 223, (byte) 85, byte.MaxValue);
  public static Color ColorYellow = (Color) new Color32(byte.MaxValue, (byte) 210, (byte) 83, byte.MaxValue);
  public static Color ColorGray = new Color(0.5f, 0.5f, 0.5f);
  public static Color ColorBlue = (Color) new Color32((byte) 115, (byte) 100, byte.MaxValue, byte.MaxValue);
  public static Color ColorCyan = Color.cyan;
  public static Color ColorPurple = new Color(1f, 0.0f, 0.71373f);
  public static Color ColorClan = (Color) new Color32(byte.MaxValue, (byte) 142, (byte) 0, byte.MaxValue);
  public static Color ColorCream = new Color(0.9647059f, 0.8470589f, 0.6431373f);

  public static MyContextMenu instance { get; private set; }

  private void Awake()
  {
    MyContextMenu.instance = this;
    this.Position();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MyContextMenu.instance == (UnityEngine.Object) this))
      return;
    MyContextMenu.instance = (MyContextMenu) null;
  }

  public void Position()
  {
    Vector3 mousePosition = Input.mousePosition;
    float x = (double) mousePosition.x > (double) (Screen.width / 2) ? 1f : 0.0f;
    this.container.pivot = (double) mousePosition.y <= (double) (Screen.height / 2) ? new Vector2(x, 0.0f) : new Vector2(x, 1f);
    this.container.position = mousePosition;
  }

  public void AddSeperator(string n = "--------------------------")
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.item, (Transform) this.container);
    gameObject.GetComponent<TextMeshProUGUI>().text = n;
    UIOnHover component = gameObject.GetComponent<UIOnHover>();
    component.textNormalColor = (Color) ColorScheme.GetColor(MyContextMenu.ColorGray);
    component.textPressedColor = component.textNormalColor;
    component.textHighlightedColor = component.textNormalColor;
    component.DisabledColor = component.textNormalColor;
    component.ToggleInteractable(false, false, false);
    gameObject.SetActive(true);
  }

  public void AddFakeItem(string n, Color c)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.item, (Transform) this.container);
    gameObject.GetComponent<TextMeshProUGUI>().text = n;
    UIOnHover component = gameObject.GetComponent<UIOnHover>();
    component.textNormalColor = c;
    component.textPressedColor = (Color) ColorScheme.GetColor(Color.white);
    component.textHighlightedColor = component.textPressedColor;
    gameObject.SetActive(true);
  }

  public void AddItem(string n, Action a, Color c)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.item, (Transform) this.container);
    gameObject.GetComponent<TextMeshProUGUI>().text = n;
    UIOnHover component = gameObject.GetComponent<UIOnHover>();
    component.textNormalColor = c;
    component.textPressedColor = (Color) ColorScheme.GetColor(Color.white);
    component.textHighlightedColor = component.textPressedColor;
    component.onClick.AddListener((UnityAction) (() =>
    {
      Action action = a;
      if (action == null)
        return;
      action();
    }));
    component.onClick.AddListener((UnityAction) (() =>
    {
      this.onCancel = (Action) null;
      this.Close();
    }));
    gameObject.SetActive(true);
  }

  public void AddItem(string n, Action a, Color c, string tooltip)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.item, (Transform) this.container);
    gameObject.GetComponent<TextMeshProUGUI>().text = n;
    UIOnHover component = gameObject.GetComponent<UIOnHover>();
    component.textNormalColor = c;
    component.textPressedColor = (Color) ColorScheme.GetColor(Color.white);
    component.textHighlightedColor = component.textPressedColor;
    component.onClick.AddListener((UnityAction) (() =>
    {
      Action action = a;
      if (action == null)
        return;
      action();
    }));
    component.onClick.AddListener((UnityAction) (() =>
    {
      this.onCancel = (Action) null;
      this.Close();
    }));
    component.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(tooltip)));
    component.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
    gameObject.SetActive(true);
  }

  public void AddItemWithImage(string n, Sprite s, Action a, Color c, string tooltip = "")
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemWithImage, (Transform) this.container);
    gameObject.GetComponent<TextMeshProUGUI>().text = "    " + n;
    UIOnHover component = gameObject.GetComponent<UIOnHover>();
    gameObject.transform.GetChild(0).GetComponent<Image>().sprite = s;
    component.textNormalColor = c;
    component.textPressedColor = (Color) ColorScheme.GetColor(Color.white);
    component.textHighlightedColor = component.textPressedColor;
    if (a != null)
    {
      component.onClick.AddListener((UnityAction) (() =>
      {
        Action action = a;
        if (action == null)
          return;
        action();
      }));
      component.onClick.AddListener((UnityAction) (() =>
      {
        this.onCancel = (Action) null;
        this.Close();
      }));
    }
    if (!string.IsNullOrEmpty(tooltip))
    {
      component.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(tooltip)));
      component.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
    }
    gameObject.SetActive(true);
  }

  public ButtonArrayContextmenu AddArray()
  {
    return UnityEngine.Object.Instantiate<ButtonArrayContextmenu>(ClientResources.Instance.contextButtonArray, (Transform) this.container);
  }

  public void AddEmojiSelector(Action<string> a)
  {
    EmojiSelector.Create(a, (Transform) this.container);
  }

  public GameObject AddSpellSelector(UnityAction<SpellEnum> a)
  {
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.contextSpells, (Transform) this.container);
    gameObject.GetComponent<ContextMenuSpells>().Setup(a);
    return gameObject;
  }

  public Toggle AddToggle(UnityAction<bool> a, string txt, bool isOn, Color c)
  {
    Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(ClientResources.Instance.contextToggle, (Transform) this.container);
    toggle.SetIsOnWithoutNotify(isOn);
    toggle.onValueChanged.AddListener(a);
    toggle.GetComponent<TMP_Text>().text = txt;
    toggle.GetComponent<TMP_Text>().color = c;
    return toggle;
  }

  public InputFieldPlus AddInput(Action<string> a, string defTxt = null, bool big = false)
  {
    InputFieldPlus inputFieldPlus = UnityEngine.Object.Instantiate<InputFieldPlus>(big ? ClientResources.Instance.contextMenuInputFieldLong : ClientResources.Instance.contextMenuInputField, (Transform) this.container);
    inputFieldPlus.IsReportInput = true;
    if (a != null)
      inputFieldPlus.onEnd.AddListener((UnityAction<string>) (s =>
      {
        Action<string> action = a;
        if (action != null)
          action(s);
        this.onCancel = (Action) null;
        this.Close();
      }));
    else
      inputFieldPlus.NOCALLBACK = true;
    if (defTxt != null)
      inputFieldPlus.SetText(defTxt);
    inputFieldPlus.gameObject.SetActive(true);
    inputFieldPlus.SetAsActive();
    return inputFieldPlus;
  }

  public UIOnSlider AddSlider(UnityAction<float> a, string defTxt = "", float defValue = 0.0f)
  {
    UIOnSlider uiOnSlider = UnityEngine.Object.Instantiate<UIOnSlider>(ClientResources.Instance.contextMenuSlider, (Transform) this.container);
    uiOnSlider.SetValue(defValue);
    uiOnSlider.onClick.AddListener(a);
    uiOnSlider.GetComponentInChildren<TMP_Text>().text = defTxt;
    uiOnSlider.gameObject.SetActive(true);
    return uiOnSlider;
  }

  public void AddVectorField(Action<Vector2> a, Vector2 def)
  {
    VectorFieldContextMenu fieldContextMenu = UnityEngine.Object.Instantiate<VectorFieldContextMenu>(ClientResources.Instance.contextMenuVectorField, (Transform) this.container);
    fieldContextMenu.Init(a, def);
    fieldContextMenu.gameObject.SetActive(true);
  }

  public static void ShowSimple(string s, Action a = null, Color? color = null)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem(s, a, color ?? (Color) ColorScheme.GetColor(MyContextMenu.ColorGray));
    myContextMenu.Rebuild();
  }

  public static void ShowSimple(string s, Color color)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem(s, (Action) null, color);
    myContextMenu.Rebuild();
  }

  public static MyContextMenu BuildColorScheme(Transform parent)
  {
    MyContextMenu component = Controller.Instance.CreateAndApply(Controller.Instance.MenuContextMenu, parent).GetComponent<MyContextMenu>();
    int num = 1;
    foreach (Color32 contextMenuColor in ColorScheme.GetAllContextMenuColors())
    {
      component.AddFakeItem(num.ToString() + ". Context Menu", (Color) ColorScheme.GetColor(contextMenuColor));
      ++num;
    }
    component.Rebuild();
    MyContextMenu.instance = (MyContextMenu) null;
    component.enabled = false;
    return component;
  }

  public static MyContextMenu Show()
  {
    if ((UnityEngine.Object) MyContextMenu.instance != (UnityEngine.Object) null)
      MyContextMenu.instance.Close();
    Controller.Instance.CreateAndApply(Controller.Instance.MenuContextMenu, Controller.Instance.transform).GetComponent<MyContextMenu>().Position();
    return MyContextMenu.instance;
  }

  public void SetOnCancel(Action a) => this.onCancel = a;

  public void Rebuild(bool quickReturn = false)
  {
    Canvas.ForceUpdateCanvases();
    VerticalLayoutGroup component1 = this.transform.GetChild(0).GetComponent<VerticalLayoutGroup>();
    component1.enabled = false;
    component1.enabled = true;
    RectTransform child1 = (RectTransform) this.transform.GetChild(0);
    float num = 0.0f;
    if (quickReturn)
    {
      component1.padding = new RectOffset(5, 5, 5, 5);
    }
    else
    {
      for (int index = 2; index < child1.childCount; ++index)
      {
        TextMeshProUGUI component2 = child1.GetChild(index).GetComponent<TextMeshProUGUI>();
        if ((UnityEngine.Object) component2 == (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) child1.GetChild(index).GetComponent<EmojiSelector>() != (UnityEngine.Object) null)
          {
            component1.padding = new RectOffset(5, 5, 5, 5);
            return;
          }
        }
        else
        {
          component2.ForceMeshUpdate(false, false);
          float renderedWidth = component2.renderedWidth;
          if ((double) renderedWidth > (double) num)
            num = renderedWidth + (child1.GetChild(index).childCount > 0 ? 30f : 0.0f);
          RectTransform child2 = (RectTransform) child1.GetChild(index);
          child2.sizeDelta = new Vector2(child1.sizeDelta.x + 20f, child2.sizeDelta.y);
        }
      }
      float x = num + 20f;
      for (int index = 2; index < child1.childCount; ++index)
      {
        ContentSizeFitter component3 = child1.GetChild(index).GetComponent<ContentSizeFitter>();
        if (!((UnityEngine.Object) component3 == (UnityEngine.Object) null))
        {
          component3.enabled = false;
          RectTransform child3 = (RectTransform) child1.GetChild(index);
          child3.sizeDelta = new Vector2(x, child3.sizeDelta.y);
        }
      }
      component1.enabled = false;
      component1.enabled = true;
    }
  }

  public static void Show(string s, string chatMsg = null)
  {
    if (Client.connection == null || Client.connection.State != ConnectionState.Connected)
    {
      MyContextMenu myContextMenu = MyContextMenu.Show();
      myContextMenu.AddSeperator("Not Connected");
      myContextMenu.AddCopy(chatMsg);
      myContextMenu.Rebuild();
    }
    else
    {
      if ((UnityEngine.Object) MyContextMenu.instance != (UnityEngine.Object) null)
        MyContextMenu.instance.Close();
      MyContextMenu component = Controller.Instance.CreateAndApply(Controller.Instance.MenuContextMenu, Controller.Instance.transform).GetComponent<MyContextMenu>();
      component.Position();
      component.ItemFromName(s, chatMsg);
      component.Rebuild();
    }
  }

  public void DisplayIconMenu()
  {
    Account acc = Client.GetAccount(Client.Name);
    int accountType = (int) acc.accountType;
    if (Client.clan != null)
    {
      this.AddItem("Leave clan", (Action) (() => MyContextMenu.ShowSimple("Confirm Leave clan?", (Action) (() => Client.AskClanCommand((byte) 3, "")), new Color?((Color) ColorScheme.GetColor(MyContextMenu.ColorRed)))), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
      this.AddSeperator();
      if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null)
        this.AddSeperator("clan icon cannot be changed in-game");
      else
        this.AddItem(acc.displayClanPrefix == (byte) 0 ? "Hide Clan Prefix" : "Show Clan Prefix", (Action) (() => Client.AskToChangeDisplayIcon(acc.displayClanPrefix == (byte) 0 ? 253 : 254)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
      this.AddSeperator();
    }
    this.AddSeperator("Change your displayed icon");
    this.AddSeperator();
    this.AddItem("Display Default", (Action) (() => Client.AskToChangeDisplayIcon(0)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    this.AddItem("Display Experience", (Action) (() => Client.AskToChangeDisplayIcon(252)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    this.AddItem("Display Prestige", (Action) (() => Client.AskToChangeDisplayIcon((int) byte.MaxValue)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (!Client.MyAccount.badges.AllOff())
      this.AddItem("Display Badge...", (Action) (() => this.SelectBadge(acc)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (accountType == 0)
    {
      this.AddItem(Client.Name + " is the best", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
    }
    else
    {
      if (Client.clan == null || Client.clan.members[Client.Name] < Clan.Roles.Officer || !string.Equals(acc.clan, Client.clan.name) || acc.clanRole > Client.clan.members[Client.Name])
        return;
      this.AddSeperator();
      this.AddItem("Change clan rank...", (Action) (() => MyContextMenu.RankContextMenu(acc)), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
    }
  }

  private void SelectBadge(Account acc)
  {
    int accountType = (int) acc.accountType;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    BitBools badges = Client.MyAccount.badges;
    ButtonArrayContextmenu arrayContextmenu = myContextMenu.AddArray();
    for (int index = 0; index < AccountExtension._IconOrder.Length; ++index)
    {
      if (((AccountType) accountType & AccountExtension._IconOrder[index]) != AccountType.None)
      {
        byte b = AccountExtension._IconOrder[index].GetIndex();
        if (AccountExtension._IconOrder[index] != AccountType.Muted || acc.discord != 0UL)
          arrayContextmenu.AddItem(ChatBox.IconString(acc, AccountExtension._IconOrder[index]), (Action) (() =>
          {
            if (acc.accountType.IsMuted())
              return;
            Client.AskToChangeDisplayIcon((int) b);
          }), AccountExtension._IconOrder[index].ToSpacelessString());
        if (arrayContextmenu.Count >= 24)
          arrayContextmenu = myContextMenu.AddArray();
      }
    }
    for (int index = 0; index < (int) byte.MaxValue; ++index)
    {
      if (badges[index])
      {
        int e = index;
        arrayContextmenu.AddImage(ClientResources.Instance.badges[index], ClientResources.Instance.badges[index].name, (Action) (() => Client.AskToChangeDisplayIcon(256 + e)));
        if (arrayContextmenu.Count >= 24)
          arrayContextmenu = myContextMenu.AddArray();
      }
    }
    if (arrayContextmenu.Count == 0)
      UnityEngine.Object.Destroy((UnityEngine.Object) arrayContextmenu.gameObject);
    myContextMenu.Rebuild();
  }

  private void AddCopy(string chatMsg)
  {
    if (string.IsNullOrEmpty(chatMsg))
      return;
    if (chatMsg.StartsWith("Your spells: (Press F8 to view) "))
      chatMsg = chatMsg.Substring("Your spells: (Press F8 to view) ".Length);
    this.AddSeperator();
    this.AddItem("Copy Contents to Clipboard", (Action) (() =>
    {
      Global.systemCopyBuffer = chatMsg;
      ChatBox.Instance?.NewChatMsg("Copied to Clipboard!", (Color) ColorScheme.GetColor(Global.ColorSystem));
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
  }

  public void IgnoreFor(string s)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator("Ignore " + s + " for");
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("5 Minutes", (Action) (() =>
    {
      Client.TempIgnored tempIgnored = Client.tempIgnored;
      string n = s;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddMinutes(5.0);
      long binary = dateTime.ToBinary();
      tempIgnored.Add(n, binary);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("15 Minutes", (Action) (() =>
    {
      Client.TempIgnored tempIgnored = Client.tempIgnored;
      string n = s;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddMinutes(15.0);
      long binary = dateTime.ToBinary();
      tempIgnored.Add(n, binary);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("1 Hour", (Action) (() =>
    {
      Client.TempIgnored tempIgnored = Client.tempIgnored;
      string n = s;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddHours(1.0);
      long binary = dateTime.ToBinary();
      tempIgnored.Add(n, binary);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("24 Hours", (Action) (() =>
    {
      Client.TempIgnored tempIgnored = Client.tempIgnored;
      string n = s;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddHours(24.0);
      long binary = dateTime.ToBinary();
      tempIgnored.Add(n, binary);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Till the client is closed", (Action) (() => Client.tempIgnored.Add(s, long.MaxValue)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (Client.ignore.Count < 100)
      myContextMenu.AddItem("Ignore Indefinitely", (Action) (() => Client.AskToAddFriend(false, true, s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    else
      myContextMenu.AddSeperator("Ignore list full - Please use one of the above options");
    myContextMenu.Rebuild();
  }

  public void ItemFromName(string s, string chatMsg = null)
  {
    if (string.IsNullOrEmpty(s))
    {
      this.AddItem("Do nothing", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGray));
      this.AddCopy(chatMsg);
    }
    else if (string.Equals(s, Client.Name, StringComparison.OrdinalIgnoreCase))
    {
      this.DisplayIconMenu();
      if (Client.clan != null)
      {
        this.AddSeperator();
        this.AddItem("My Clan Log...", (Action) (() => Client.AskClanCommand((byte) 12, "")), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      }
      this.AddCopy(chatMsg);
    }
    else if (string.Equals(s, "[Annoucement]", StringComparison.OrdinalIgnoreCase))
    {
      this.AddItem("Join the discord!", (Action) (() => Global.OpenURL(Server.discordLink)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
      this.AddCopy(chatMsg);
    }
    else if (string.Equals(s, "[Discord]", StringComparison.OrdinalIgnoreCase))
    {
      this.AddItem("Verify!", (Action) (() => DiscordController._VerifyBrowser()), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
      this.AddItem("Join the discord!", (Action) (() => Global.OpenURL(Server.discordLink)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
      this.AddCopy(chatMsg);
    }
    else
    {
      Account acc = Client.GetAccount(s);
      if (acc.accountType.has(AccountType.Arcane_Monster) && !acc.accountType.isMod())
      {
        this.AddItem("<color=purple>Arcane Monsters</color> are <color=red>not</color> Moderators", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGray));
        this.AddSeperator("-----------------------------------");
      }
      if (Client.miniGame != null)
        this.AddItem("Invite " + s + " to the " + Client.miniGame.GetGameType() + " match", (Action) (() =>
        {
          Client.sharingWith = s;
          Client.AskToShare(Client.miniGame.GetGameType(), ContentType.MiniGameInvite, (object) new MinigameInvite()
          {
            from = Client.Name,
            minigameID = Client.miniGame.id,
            miniGameType = (int) Client.miniGame.gameType,
            spectator = (Client.miniGame.players.Count > 1)
          });
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      if ((UnityEngine.Object) LobbyMenu.instance != (UnityEngine.Object) null && !Client._lobby.ContainsKey(acc.name) && acc.location.Online())
      {
        bool flag = false;
        for (int index = Client._games.Count - 1; index >= 0; --index)
        {
          KeyValuePair<int, GameFacts> x = Client._games.GetItem(index);
          if (x.Value.status == (byte) 2)
          {
            foreach (string player in x.Value.players)
            {
              if (string.Equals(player, acc.name))
              {
                this.AddItem("Spectate " + s + " game", (Action) (() => Client.AskToSpectate(x.Key)), (Color) ColorScheme.GetColor(MyContextMenu.ColorCyan));
                this.AddSeperator();
                flag = true;
                break;
              }
            }
          }
          if (flag)
            break;
        }
      }
      if ((UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null && UnratedMenu.instance.IsFirst)
      {
        if (!Client._gameFacts.players.Contains(s))
          this.AddItem("Invite " + s + " to game", (Action) (() => Client.AskToInvite(s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
        else
          this.AddItem("Kick " + s + " from game", (Action) (() => Client.AskToKick(s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
      }
      if (Client.friends.Contains(s))
      {
        this.AddItem("Message " + s, (Action) (() =>
        {
          if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
            HUD.instance.InitPrivateChat(s);
          else
            ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), s);
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        this.AddItem("Remove friend " + s, (Action) (() => Client.AskToAddFriend(true, false, s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      }
      else if (Client.ignore.Contains(s))
        this.AddItem("Remove ignore " + s, (Action) (() => Client.AskToAddFriend(false, false, s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      else if (Client.tempIgnored.Ignored(s))
      {
        this.AddItem("Cancel ignore " + s, (Action) (() => Client.tempIgnored.Remove(s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      }
      else
      {
        this.AddItem("Message " + s, (Action) (() =>
        {
          if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null)
            HUD.instance.InitPrivateChat(s);
          else
            ChatBox.Instance.InitPrivate(ColorScheme.GetColor(Global.ColorSentPrivate), s);
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        if (Client.friends.Count < 200)
          this.AddItem("Add friend", (Action) (() => Client.AskToAddFriend(true, true, s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
        this.AddItem("Ignore for... ", (Action) (() => this.IgnoreFor(s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      }
      if (Client.clan != null && Client.clan.members[Client.Name] >= Clan.Roles.Officer)
      {
        if (string.IsNullOrEmpty(acc.clan))
          this.AddItem("Invite " + s + " to the clan ", (Action) (() => Client.AskClanCommand((byte) 7, acc.name)), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
        else if (string.Equals(acc.clan, Client.clan.name) && acc.clanRole < Client.clan.members[Client.Name])
        {
          this.AddItem("Kick " + s + " from clan", (Action) (() => MyContextMenu.ShowSimple("Confirm kick " + s + " from clan?", (Action) (() => Client.AskClanCommand((byte) 10, acc.name)), new Color?(MyContextMenu.ColorRed))), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
          this.AddItem("Change " + s + "'s clan rank...", (Action) (() => MyContextMenu.RankContextMenu(acc)), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
        }
      }
      this.AddSeperator();
      if (!string.IsNullOrEmpty(acc.clan))
        this.AddItem("Clan: " + acc.clan + " (" + (object) acc.clanRole + ")", (Action) null, (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      else
        this.AddItem("This player is not in a clan", (Action) null, (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      this.AddSeperator();
      this.AddItem("Share...", (Action) (() => MyContextMenu.ShareContextMenu(s, acc)), MyContextMenu.ColorYellow);
      this.AddSeperator();
      if (!string.IsNullOrEmpty(acc.oldName))
        this.AddItem("Previous Name: " + acc.oldName, (Action) (() =>
        {
          Global.systemCopyBuffer = acc.oldName;
          ChatBox.Instance.NewChatMsg("", "Copied " + s + "'s previous name to Clipboard", (Color) ColorScheme.GetColor(Global.ColorSystem), s, ChatOrigination.System);
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
      Account account = Client.GetAccount(Client.Name);
      if (acc.discord != 0UL && account.accountType.IsModPlus())
        this.AddItem("Discord ID: " + acc.discord.ToString(), (Action) (() =>
        {
          Global.systemCopyBuffer = acc.discord.ToString();
          ChatBox.Instance.NewChatMsg("", "Copied " + s + "'s discord ID to Clipboard", (Color) ColorScheme.GetColor(Global.ColorSystem), s, ChatOrigination.System);
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
      if (account.accountType.IsModPlus())
      {
        this.AddSeperator();
        if (acc.accountType.IsMuted() && !acc.fake)
          this.AddItem("Unmute " + s, (Action) (() => Client.SendChatMsg("?unmute:" + s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
        else
          this.AddItem("Mute " + s, (Action) (() => Client.SendChatMsg("?mute:" + s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      }
      this.AddSeperator();
      this.AddItem("Report " + s, (Action) (() => Controller.Instance.Report(s)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      if ((acc.accountType == AccountType.None || acc.fake) && acc.experience == (byte) 0 && acc.prestige == (byte) 0)
      {
        this.AddCopy(chatMsg);
      }
      else
      {
        if (Client.MyAccount.accountType.IsModPlus())
        {
          this.AddSeperator("Account Tags");
          this.AddSeperator();
          int accountType = (int) acc.accountType;
          for (int index1 = 0; index1 < AccountExtension._IconOrder.Length; ++index1)
          {
            if (((AccountType) accountType & AccountExtension._IconOrder[index1]) != AccountType.None)
            {
              int index2 = (int) AccountExtension._IconOrder[index1].GetIndex();
              if (AccountExtension._IconOrder[index1] == AccountType.Muted && acc.discord == 0UL)
                this.AddItem(ChatBox.IconString(acc, AccountExtension._IconOrder[index1]) + "Unverified", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
              else
                this.AddItem(ChatBox.IconString(acc, AccountExtension._IconOrder[index1]) + AccountExtension._IconOrder[index1].ToSpacelessString(), (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
            }
          }
          if (acc.prestige > (byte) 0)
            this.AddItem(ChatBox.PrestigeString((int) acc.prestige) + (object) acc.prestige + Account.number((int) acc.prestige) + " prestige", (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
          if (acc.experience > (byte) 0)
            this.AddItem("Level " + (object) acc.experience, (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
        }
        this.AddCopy(chatMsg);
      }
    }
  }

  public void AdditionalInformation(string s, string chatMsg = null)
  {
  }

  public static void ShareContextMenu(string s, Account acc)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem("Share an outfit with " + s, (Action) (() =>
    {
      ChangeOutfitMenu.Create(false, onEnd: (Action<SettingsPlayer>) (sp => { }));
      Client.sharingWith = s;
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Share current outfit with " + s, (Action) (() =>
    {
      Client.sharingWith = s;
      Client.AskToShare("Current", ContentType.Outfit, (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null ? (object) Player.Instance.person.settingsPlayer : (object) Client.settingsPlayer);
      Client.sharingWith = "";
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Share a spellbook with " + s, (Action) (() =>
    {
      ChangeSpellBookMenu.Create(false, onEnd: (Action<SettingsPlayer>) (sp => { }));
      Client.sharingWith = s;
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Share current spellbook with " + s, (Action) (() =>
    {
      Client.sharingWith = s;
      Client.AskToShare("Current", ContentType.SpellBook, (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null ? (object) Player.Instance.person.settingsPlayer : (object) Client.settingsPlayer);
      Client.sharingWith = "";
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (Client.colorScheme != null)
      myContextMenu.AddItem("Share current color scheme with " + s, (Action) (() =>
      {
        Client.sharingWith = s;
        Client.AskToShare(Client.colorScheme.name, ContentType.ColorScheme, (object) Client.colorScheme);
        Client.sharingWith = "";
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (ChooseJsonDialog.HasCustomTutorials())
      myContextMenu.AddItem("Share a tutorial with " + s, (Action) (() =>
      {
        ChooseJsonDialog.Create(false, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) ((name, sp, _index) =>
        {
          Client.AskToShare(Path.GetFileNameWithoutExtension(name), ContentType.ArcTutorial, (object) sp);
          Client.sharingWith = "";
        }));
        Client.sharingWith = s;
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    if (!string.IsNullOrEmpty(acc.clan) && Client.clan != null && string.Equals(Client.clan.name, acc.clan))
    {
      myContextMenu.AddSeperator();
      myContextMenu.AddItem("Share an outfit with your clan", (Action) (() =>
      {
        ChangeOutfitMenu.Create(false, onEnd: (Action<SettingsPlayer>) (sp => { }));
        Client.sharingWith = "[Clan]";
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      myContextMenu.AddItem("Share current outfit with your clan", (Action) (() =>
      {
        Client.sharingWith = "[Clan]";
        Client.AskToShare("Current", ContentType.Outfit, (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null ? (object) Player.Instance.person.settingsPlayer : (object) Client.settingsPlayer);
        Client.sharingWith = "";
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      myContextMenu.AddItem("Share a spellbook with your clan", (Action) (() =>
      {
        ChangeSpellBookMenu.Create(false, onEnd: (Action<SettingsPlayer>) (sp => { }));
        Client.sharingWith = "[Clan]";
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      myContextMenu.AddItem("Share current spellbook with your clan", (Action) (() =>
      {
        Client.sharingWith = "[Clan]";
        Client.AskToShare("Current", ContentType.SpellBook, (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null ? (object) Player.Instance.person.settingsPlayer : (object) Client.settingsPlayer);
        Client.sharingWith = "";
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
      if (Client.colorScheme != null)
        myContextMenu.AddItem("Share current color scheme with " + s, (Action) (() =>
        {
          Client.sharingWith = "[Clan]";
          Client.AskToShare(Client.colorScheme.name, ContentType.ColorScheme, (object) Client.colorScheme);
          Client.sharingWith = "";
        }), (Color) ColorScheme.GetColor(MyContextMenu.ColorClan));
    }
    myContextMenu.Rebuild();
  }

  public static void RankContextMenu(Account acc)
  {
    Clan.Roles member = Client.clan.members[Client.Name];
    MyContextMenu myContextMenu = MyContextMenu.Show();
    List<Clan.Roles> list = ((IEnumerable<Clan.Roles>) Enum.GetValues(typeof (Clan.Roles))).ToList<Clan.Roles>();
    for (int index = 0; index < list.Count; ++index)
    {
      Clan.Roles x = list[index];
      if (list[index] == member)
        myContextMenu.AddSeperator(list[index].ToString());
      else if (list[index] < member)
        myContextMenu.AddItem(list[index].ToString(), (Action) (() => Client.AskClanRank(acc.name, (int) x)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
      else
        myContextMenu.AddItem(list[index].ToString(), (Action) (() => { }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    }
    myContextMenu.Rebuild();
  }

  public void Close()
  {
    MyToolTip.Close();
    if ((UnityEngine.Object) MyContextMenu.instance == (UnityEngine.Object) this)
      MyContextMenu.instance = (MyContextMenu) null;
    Action onCancel = this.onCancel;
    if (onCancel != null)
      onCancel();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public static void CloseInstance() => MyContextMenu.instance?.Close();
}
