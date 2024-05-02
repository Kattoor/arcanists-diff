

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class QuickchatUI : MonoBehaviour
{
  public TMP_Text pfab;
  public TMP_Text pfabItem;
  public RectTransform containerCategory;
  public RectTransform containerExtend;
  public RectTransform containerActive;
  public RectTransform containerInactive;
  public UIOnHover buttonSend;
  public Scrollbar scroll;
  public TMP_Dropdown dropMaps;
  public TMP_Dropdown dropTime;
  public TMP_Dropdown dropStyle;
  public TMP_Dropdown dropRanked;
  public TMP_Dropdown dropCount;
  public TMP_Dropdown dropMinion;
  public TMP_Dropdown dropSpell;
  public EmojiSelector emojiSelector;
  public TMP_InputField inputPrivate;
  public TMP_Dropdown dropDestination;
  public TMP_Dropdown dropPrivate;
  private List<ChatOrigination> valueDestionation = new List<ChatOrigination>();
  internal static ChatOrigination targetDestination;
  private static int targetMap = 0;
  private static int targetTime = 0;
  private static int targetStyle = 0;
  private static int targetRanked = 0;
  private static int targetCount = 0;
  private static int targetMinion = 0;
  private static int targetSpell = 0;
  internal static string privateDM = "";
  private int selectedCategory = -1;
  private Quickchat.Command selectedCommand;
  private string selectedEmoji = "";

  public static QuickchatUI Instance { get; private set; }

  public static bool Active
  {
    get
    {
      return (UnityEngine.Object) QuickchatUI.Instance != (UnityEngine.Object) null && QuickchatUI.Instance.gameObject.activeInHierarchy;
    }
  }

  public static void Create()
  {
    if ((UnityEngine.Object) QuickchatUI.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) QuickchatUI.Instance.gameObject);
    if ((UnityEngine.Object) ChatBox.Instance == (UnityEngine.Object) null || (UnityEngine.Object) ColorSchemeUI.Instance != (UnityEngine.Object) null)
      return;
    QuickchatUI.Instance = Controller.Instance.CreateAndApply<QuickchatUI>(Controller.Instance.quickchat, ChatBox.Instance.transform);
    ((RectTransform) QuickchatUI.Instance.transform).anchoredPosition = new Vector2(0.0f, 0.0f);
    QuickchatUI.Instance.MoveInactive();
    Quickchat.Initialize();
    QuickchatUI.Instance.SetupDestinations();
    QuickchatUI.Instance.buttonSend.gameObject.SetActive(false);
  }

  private bool ActiveInputField()
  {
    GameObject selectedGameObject = EventSystem.current?.currentSelectedGameObject;
    if ((UnityEngine.Object) selectedGameObject != (UnityEngine.Object) null)
    {
      TMP_InputField component = selectedGameObject.GetComponent<TMP_InputField>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component.isFocused;
    }
    return false;
  }

  private void Update()
  {
    if (this.ActiveInputField())
      return;
    if (Input.GetKeyDown(KeyCode.Return) && this.selectedCommand != null)
    {
      this.SendCommand();
      MyToolTip.Close();
    }
    else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Tab))
    {
      this.Close();
      MyToolTip.Close();
    }
    else
    {
      if (!Input.anyKeyDown)
        return;
      for (int index = 0; index < 7; ++index)
      {
        if (Input.GetKeyDown(Quickchat.AllCommands(index).key))
        {
          this.SelectCategory(index);
          MyToolTip.Close();
          return;
        }
      }
      if (this.selectedCategory == -1)
        return;
      (string name, List<Quickchat.Command> list, KeyCode key) tuple = Quickchat.AllCommands(this.selectedCategory);
      for (int index = 0; index < tuple.list.Count; ++index)
      {
        if (Input.GetKeyDown(tuple.list[index].key))
        {
          this.SelectItem(index);
          MyToolTip.Close();
          break;
        }
      }
    }
  }

  private void Awake()
  {
    int num = 0;
    for (int i = 0; i < 7; ++i)
    {
      (string name, List<Quickchat.Command> list, KeyCode key) tuple = Quickchat.AllCommands(i);
      TMP_Text tmpText = UnityEngine.Object.Instantiate<TMP_Text>(this.pfab, (Transform) this.containerCategory);
      UIOnHover component = tmpText.GetComponent<UIOnHover>();
      tmpText.text = "<mspace=20>" + (object) (num + 1) + "</mspace>) " + tuple.name;
      int e = num;
      component.onClick.AddListener((UnityAction) (() => this.SelectCategory(e)));
      tmpText.gameObject.SetActive(true);
      ++num;
    }
    this.emojiSelector.SetCustomOnEnd((Action<string>) (s =>
    {
      int startIndex = s.IndexOf('"') + 1;
      this.selectedEmoji = s.Substring(startIndex, s.LastIndexOf('"') - startIndex);
      this.SendCommand();
    }));
    this.SetupDestinations();
    this.inputPrivate.text = QuickchatUI.privateDM;
    this.dropMaps.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetMap = z));
    this.dropTime.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetTime = z));
    this.dropStyle.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetStyle = z));
    this.dropRanked.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetRanked = z));
    this.dropCount.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetCount = z));
    this.dropMinion.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetMinion = z));
    this.dropSpell.onValueChanged.AddListener((UnityAction<int>) (z => QuickchatUI.targetSpell = z));
    this.dropDestination.onValueChanged.AddListener(new UnityAction<int>(this.OnDestinationChanged));
    if (this.dropDestination.value > -1)
      this.OnDestinationChanged(this.dropDestination.value);
    this.dropPrivate.onValueChanged.AddListener((UnityAction<int>) (z => this.inputPrivate.text = this.dropPrivate.options[z].text));
  }

  public void OnDestinationChanged(int i)
  {
    this.inputPrivate.gameObject.SetActive(this.valueDestionation[i] == ChatOrigination.Private);
    this.dropPrivate.gameObject.SetActive(this.inputPrivate.gameObject.activeSelf);
    if (!this.inputPrivate.gameObject.activeSelf || this.dropPrivate.options.Count != 0)
      return;
    List<Account> list = Client._accounts.Values.ToList<Account>();
    list.Sort((Comparison<Account>) ((a, b) => a.name.CompareTo(b.name)));
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (Account account in list)
    {
      if (account.location.Online() && !string.Equals(account.name, Client.Name))
        optionDataList.Add(new TMP_Dropdown.OptionData(account.name));
    }
    this.dropPrivate.options = optionDataList;
  }

  private void SetupDestinations()
  {
    this.valueDestionation.Clear();
    this.valueDestionation.Add(ChatOrigination.Lobby);
    if (Client._accounts.Count > 1 && !Client.offlineMode)
      this.valueDestionation.Add(ChatOrigination.Private);
    if (Client.clan != null)
      this.valueDestionation.Add(ChatOrigination.Clan);
    if (Client.game != null && Client.game.isTeam && !Client.game.isSpectator)
      this.valueDestionation.Add(ChatOrigination.Team);
    if (Client.miniGame != null)
      this.valueDestionation.Add(ChatOrigination.MiniGame);
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (ChatOrigination chatOrigination in this.valueDestionation)
      optionDataList.Add(new TMP_Dropdown.OptionData(chatOrigination.ToString()));
    this.dropDestination.options = optionDataList;
    this.dropDestination.value = Mathf.Clamp(this.valueDestionation.FindIndex((Predicate<ChatOrigination>) (z => z == QuickchatUI.targetDestination)), 0, optionDataList.Count - 1);
  }

  private void SelectCategory(int cat)
  {
    this.containerExtend.DestroyChildern();
    this.selectedCategory = cat;
    List<Quickchat.Command> list = Quickchat.AllCommands(this.selectedCategory).list;
    int num = 0;
    for (int index = 0; index < list.Count; ++index)
    {
      Quickchat.Command command = list[index];
      TMP_Text tmpText = UnityEngine.Object.Instantiate<TMP_Text>(this.pfabItem, (Transform) this.containerExtend);
      UIOnHover component = tmpText.GetComponent<UIOnHover>();
      tmpText.text = "<mspace=20>" + (object) command.key + "</mspace>) " + command.name;
      int e = num;
      component.onClick.AddListener((UnityAction) (() => this.SelectItem(e)));
      tmpText.gameObject.SetActive(true);
      ++num;
    }
    this.scroll.value = 1f;
  }

  private void SelectItem(int item)
  {
    Quickchat.Command x = Quickchat.AllCommands(this.selectedCategory).list[item];
    this.selectedCommand = x;
    if (x.options == null || x.options.Length == 0)
    {
      this.SendCommand();
    }
    else
    {
      this.SetCommandActive(x);
      QuickchatUI.Instance.buttonSend.gameObject.SetActive(true);
    }
  }

  private int[] GetOptions()
  {
    Quickchat.Command selectedCommand = this.selectedCommand;
    int[] options = new int[selectedCommand.options == null ? 0 : selectedCommand.options.Length];
    for (int index = 0; index < options.Length; ++index)
    {
      switch (selectedCommand.options[index])
      {
        case Quickchat.Options.Map:
          options[index] = this.dropMaps.value;
          break;
        case Quickchat.Options.Time:
          options[index] = this.dropTime.value;
          break;
        case Quickchat.Options.Style:
          options[index] = this.dropStyle.value;
          break;
        case Quickchat.Options.PlayerCount:
          options[index] = this.dropCount.value;
          break;
        case Quickchat.Options.Ranked:
          options[index] = this.dropRanked.value;
          break;
        case Quickchat.Options.Spell:
          options[index] = this.dropSpell.value;
          break;
        case Quickchat.Options.Minion:
          options[index] = this.dropMinion.value;
          break;
        case Quickchat.Options.Emoji:
          options[index] = EmojiInfo.allEmoji.list.FindIndex((Predicate<Emoji>) (z => string.Equals(z.realName, this.selectedEmoji)));
          break;
      }
    }
    return options;
  }

  public void SendCommand()
  {
    int[] options = this.GetOptions();
    QuickchatUI.privateDM = this.inputPrivate.text;
    QuickchatUI.targetDestination = this.valueDestionation[Mathf.Clamp(this.dropDestination.value, 0, this.valueDestionation.Count - 1)];
    Quickchat.Send(this.selectedCommand.id, options, QuickchatUI.targetDestination, QuickchatUI.privateDM);
    QuickchatUI.Instance.buttonSend.gameObject.SetActive(false);
    this.Close();
  }

  public void Close() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void InitMaps()
  {
    if (this.dropMaps.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string map in Quickchat.Maps)
      optionDataList.Add(new TMP_Dropdown.OptionData(map));
    this.dropMaps.options = optionDataList;
    this.dropMaps.value = QuickchatUI.targetMap;
  }

  public void InitTime()
  {
    if (this.dropTime.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string time in Quickchat.Times)
      optionDataList.Add(new TMP_Dropdown.OptionData(time));
    this.dropTime.options = optionDataList;
    this.dropTime.value = QuickchatUI.targetTime;
  }

  public void InitStyles()
  {
    if (this.dropStyle.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string style in Quickchat.Styles)
      optionDataList.Add(new TMP_Dropdown.OptionData(style));
    this.dropStyle.options = optionDataList;
    this.dropStyle.value = QuickchatUI.targetStyle;
  }

  public void InitPlayerCounts()
  {
    if (this.dropCount.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string playerCount in Quickchat.PlayerCounts)
      optionDataList.Add(new TMP_Dropdown.OptionData(playerCount));
    this.dropCount.options = optionDataList;
    this.dropCount.value = QuickchatUI.targetCount;
  }

  public void InitSpells()
  {
    if (this.dropSpell.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string text in Quickchat.Spell)
      optionDataList.Add(new TMP_Dropdown.OptionData(text));
    this.dropSpell.options = optionDataList;
    this.dropSpell.value = QuickchatUI.targetSpell;
  }

  public void InitMinions()
  {
    if (this.dropMinion.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string text in Quickchat.Minion)
      optionDataList.Add(new TMP_Dropdown.OptionData(text));
    this.dropMinion.options = optionDataList;
    this.dropMinion.value = QuickchatUI.targetMinion;
  }

  public void InitRanked()
  {
    if (this.dropRanked.options.Count > 0)
      return;
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (string text in Quickchat.Ranked)
      optionDataList.Add(new TMP_Dropdown.OptionData(text));
    this.dropRanked.options = optionDataList;
    this.dropRanked.value = QuickchatUI.targetRanked;
  }

  private void MoveInactive()
  {
    for (int index = this.containerActive.childCount - 1; index >= 0; --index)
      this.containerActive.GetChild(index).SetParent((Transform) this.containerInactive);
  }

  private void SetCommandActive(Quickchat.Command x)
  {
    this.MoveInactive();
    foreach (int option in x.options)
    {
      switch (option)
      {
        case 2:
          this.InitMaps();
          this.dropMaps.transform.SetParent((Transform) this.containerActive);
          break;
        case 3:
          this.InitTime();
          this.dropTime.transform.SetParent((Transform) this.containerActive);
          break;
        case 4:
          this.InitStyles();
          this.dropStyle.transform.SetParent((Transform) this.containerActive);
          break;
        case 5:
          this.InitPlayerCounts();
          this.dropCount.transform.SetParent((Transform) this.containerActive);
          break;
        case 6:
          this.InitRanked();
          this.dropRanked.transform.SetParent((Transform) this.containerActive);
          break;
        case 7:
          this.InitSpells();
          this.dropSpell.transform.SetParent((Transform) this.containerActive);
          break;
        case 8:
          this.InitMinions();
          this.dropMinion.transform.SetParent((Transform) this.containerActive);
          break;
        case 9:
          this.emojiSelector.transform.SetParent((Transform) this.containerActive);
          break;
      }
    }
  }
}
