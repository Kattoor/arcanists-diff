

using Educative;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class TutorialEditorMenu : MonoBehaviour
{
  internal JObject json = new JObject();
  internal JObject defaultJson = new JObject();
  internal Educative.Command.Type containerType;
  internal TMP_Text selectedListText;
  internal int selectedListIndex = -1;
  private JArray activeList;
  internal JToken item;
  internal Tutorial raw;
  public GameObject buttonCodeFile;
  public RectTransform listContainer;
  public GameObject pfabListItem;
  public RectTransform editContainer;
  public pfabEditJsonItem pfabItem;
  public pfabEditJsonItem pfabItemMultiline;
  public pfabEditJsonItem pfabCodeEditor;
  public pfabBoolJsonItem pfabBool;
  public UIOnHover pfabButton;
  public TMP_Dropdown pfabDropdown;
  public TMP_Dropdown mapDropdown;
  public UnityEngine.UI.Text txt_editing;
  public Toggle toggleInsert;
  public Image imgMap;
  public Image imgMapBg;
  public GameObject buttonDelete;
  public GameObject buttonMoveUp;
  public GameObject buttonMoveDown;
  public GameObject buttonCopy;
  public GameObject buttonPaste;
  [Header("Ref Image")]
  public Image imgRef;
  public RectTransform imgRefBehind;
  public GameObject att_builds_button;
  private static string prefOpened = "preftutopened";
  private JObject copiedCommand;
  internal bool edited;
  internal bool changesMade;
  internal string editing = "";

  public static TutorialEditorMenu Instance { get; private set; }

  public void SetImgRef(string n, JToken j)
  {
  }

  private void Awake()
  {
    TutorialEditorMenu.Instance = this;
    if (Client.game == null)
      return;
    Client.game.CleanUp();
    Client.game = (ZGame) null;
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) TutorialEditorMenu.Instance == (UnityEngine.Object) this))
      return;
    TutorialEditorMenu.Instance = (TutorialEditorMenu) null;
  }

  private void Start()
  {
    string str = Global.GetTutorialPath + PlayerPrefs.GetString(TutorialEditorMenu.prefOpened, "_Temporary.arcTutorial");
    if (File.Exists(str))
    {
      this.raw = Global.OpenTutorial(str);
      this.Editing(PlayerPrefs.GetString(TutorialEditorMenu.prefOpened, "_Temporary.arcTutorial"));
      this.ToggleCodeTest(string.Equals(".arcTutorial2", Path.GetExtension(str), StringComparison.OrdinalIgnoreCase) || File.Exists(str + "2"));
    }
    else
    {
      this.raw = new Tutorial();
      Tutorial raw = this.raw;
      CInfo c = new CInfo();
      c.message = "Welcome to this Arcanists Tutorial! Click to Continue...";
      c.Name = "Welcome";
      raw.CreateCommand((Educative.Command) c);
      this.ToggleCodeTest(false);
    }
    this.raw.ValidateIdentifiers();
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.Unselect();
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (Sprite map in ClientResources.Instance._maps)
      optionDataList.Add(new TMP_Dropdown.OptionData(map.name));
    this.mapDropdown.options = optionDataList;
    this.FindMapImage();
  }

  public void OnMapChange(int v)
  {
    foreach (JProperty jproperty in this.json.Values<JProperty>())
    {
      if (string.Equals(jproperty.Name, "mapId"))
      {
        jproperty.Value = JToken.Parse("\"" + this.mapDropdown.options[v].text + "\"");
        this.UpdateMapImage(v);
        this.Edited();
        break;
      }
    }
  }

  public void Unselect()
  {
    if ((UnityEngine.Object) this.selectedListText != (UnityEngine.Object) null)
      this.selectedListText.GetComponent<UIOnHover>().AlwaysOn = false;
    this.DeselectItem();
    int i1 = -2;
    this.CreateHeader(ref i1, "~ Basic Tutorial Settings ~");
    int i2 = i1 + 1;
    foreach (JProperty x in this.json.Values<JProperty>())
    {
      if (string.Equals(x.Name, "Name") || string.Equals(x.Name, "message") || string.Equals(x.Name, "settings"))
        this.PopulateItem(x, ref i2);
    }
    this.buttonDelete.SetActive(false);
    this.buttonMoveUp.SetActive(false);
    this.buttonMoveDown.SetActive(false);
    this.buttonCopy.SetActive(false);
    this.buttonPaste.SetActive(false);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1))
      this.CopyToClipBoard();
    if (!Input.GetKeyDown(KeyCode.F2))
      return;
    this.CreateDefaultTutorial();
  }

  public void CopyToClipBoard()
  {
    Global.systemCopyBuffer = this.json.ToString();
    Debug.Log((object) "Copied Json");
  }

  public void Populate()
  {
    Global.systemCopyBuffer = this.json.ToString();
    Debug.Log((object) "Copied Json to clipboard");
  }

  public void CreateDefaultTutorial()
  {
    byte[] bytes = Tutorial.FromJson(this.json).ToBytes();
    StringBuilder stringBuilder = new StringBuilder("new Data(){ name = \"" + Path.GetFileNameWithoutExtension(this.editing) + "\" , bytes = new byte[] { ");
    for (int index = 0; index < bytes.Length - 1; ++index)
      stringBuilder.Append(bytes[index].ToString()).Append(",");
    stringBuilder.Append(bytes[bytes.Length - 1]);
    stringBuilder.Append("} },\n");
    Global.systemCopyBuffer = stringBuilder.ToString();
    Debug.Log((object) "Copied Json as bytes (DefaultTutorials.cs) to clipboard");
  }

  public void CommandAdd()
  {
    if (this.toggleInsert.isOn)
      this.CommandInsertAfter();
    else
      this.CommandInsertBefore();
  }

  public void CopyCommand()
  {
    if (this.selectedListIndex == -1)
      return;
    this.UpdateRaw();
    this.copiedCommand = ClipboardExtension.ToJson<ContainerCommand>(new ContainerCommand()
    {
      command = this.raw.commands[this.selectedListIndex]
    });
    this.buttonPaste.SetActive(true);
  }

  public void PasteCommand()
  {
    if (this.selectedListIndex == -1 || this.copiedCommand == null)
      return;
    this.UpdateRaw();
    Educative.Command command = ClipboardExtension.FromJson<ContainerCommand>(this.copiedCommand).command;
    command.identifier = this.raw.lastID;
    ++this.raw.lastID;
    int i = this.selectedListIndex + (this.toggleInsert.isOn ? 1 : 0);
    this.raw.InsertCommand(i, command);
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.RefreshSelectedUI(i);
    this.Edited();
  }

  public void CommandInsertBefore()
  {
    int i1 = this.selectedListIndex;
    if (i1 == -1)
      i1 = 0;
    this.UpdateRaw();
    Tutorial raw = this.raw;
    int i2 = i1;
    CInfo c = new CInfo();
    c.Name = "Info";
    raw.InsertCommand(i2, (Educative.Command) c);
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.RefreshSelectedUI(i1);
    this.Edited();
  }

  public void CommandInsertAfter()
  {
    int selectedListIndex = this.selectedListIndex;
    this.UpdateRaw();
    int i1 = selectedListIndex != -1 ? selectedListIndex + 1 : this.raw.commands.Count;
    this.selectedListIndex = i1;
    Tutorial raw = this.raw;
    int i2 = i1;
    CInfo c = new CInfo();
    c.Name = "Info";
    raw.InsertCommand(i2, (Educative.Command) c);
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.RefreshSelectedUI(i1);
    this.Edited();
  }

  public void CommandDelete()
  {
    int selectedListIndex = this.selectedListIndex;
    if (selectedListIndex == -1)
      return;
    this.UpdateRaw();
    this.raw.commands.RemoveAt(selectedListIndex);
    this.json = this.raw.ToJson();
    this.PopulateList();
    if (selectedListIndex < this.raw.commands.Count)
      this.listContainer.GetChild(selectedListIndex).GetComponent<UIOnHover>().onClick.Invoke();
    this.Edited();
  }

  public void CommandChange(Educative.Command.Type t)
  {
    int selectedListIndex1 = this.selectedListIndex;
    if (selectedListIndex1 == -1)
      return;
    this.UpdateRaw();
    Educative.Command command1 = this.raw.commands[selectedListIndex1];
    Educative.Command command2 = Educative.Command.From(t);
    command2.Name = t.ToString();
    command2.identifier = command1.identifier;
    command2.tabIndex = command1.tabIndex;
    this.raw.commands[selectedListIndex1] = command2;
    this.json = this.raw.ToJson();
    int selectedListIndex2 = this.selectedListIndex;
    this.PopulateList();
    if (selectedListIndex2 >= 0)
      this.listContainer.GetChild(selectedListIndex2).GetComponent<UIOnHover>().onClick.Invoke();
    this.Edited();
  }

  public void UpdateRaw(bool force = true)
  {
    if (!this.changesMade && !force)
      return;
    this.changesMade = false;
    this.raw = Tutorial.FromJson(this.json);
  }

  public void MoveCommandUp()
  {
    int selectedListIndex = this.selectedListIndex;
    if (selectedListIndex <= 0)
      return;
    this.UpdateRaw();
    Educative.Command command = this.raw.commands[selectedListIndex - 1];
    this.raw.commands[selectedListIndex - 1] = this.raw.commands[selectedListIndex];
    this.raw.commands[selectedListIndex] = command;
    this.json = this.raw.ToJson();
    int index = selectedListIndex - 1;
    this.PopulateList();
    this.listContainer.GetChild(index).GetComponent<UIOnHover>().onClick.Invoke();
    this.Edited();
  }

  public void MoveCommandDown()
  {
    int selectedListIndex = this.selectedListIndex;
    if (selectedListIndex == -1)
      return;
    this.UpdateRaw();
    if (selectedListIndex >= this.raw.commands.Count - 1)
      return;
    Educative.Command command = this.raw.commands[selectedListIndex + 1];
    this.raw.commands[selectedListIndex + 1] = this.raw.commands[selectedListIndex];
    this.raw.commands[selectedListIndex] = command;
    this.json = this.raw.ToJson();
    int index = selectedListIndex + 1;
    this.PopulateList();
    this.listContainer.GetChild(index).GetComponent<UIOnHover>().onClick.Invoke();
    this.Edited();
  }

  public void CommandUpdated()
  {
    int selectedListIndex = this.selectedListIndex;
    this.UpdateRaw();
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.listContainer.GetChild(selectedListIndex).GetComponent<UIOnHover>().onClick.Invoke();
    this.Edited();
  }

  public void DeselectItem()
  {
    this.selectedListText = (TMP_Text) null;
    this.selectedListIndex = -1;
    this.item = (JToken) null;
    this.SetImgRef((string) null, (JToken) null);
    for (int index = this.editContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.editContainer.GetChild(index).gameObject);
    this.ResizeContainer(0);
  }

  public void ResizeContainer(int amount)
  {
    this.editContainer.sizeDelta = new Vector2(this.editContainer.sizeDelta.x, (float) (amount * 50 + 16));
  }

  public void DeselectList()
  {
    this.activeList = (JArray) null;
    for (int index = this.listContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.listContainer.GetChild(index).gameObject);
  }

  public void PopulateList()
  {
    JToken jtoken;
    if (this.json.TryGetValue("commands", out jtoken))
    {
      this.activeList = (JArray) jtoken;
    }
    else
    {
      this.activeList = JArray.Parse("[]");
      this.json.Add("commands", (JToken) this.activeList);
    }
    int num = 0;
    for (int index = this.listContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.listContainer.GetChild(index).gameObject);
    this.item = (JToken) null;
    foreach (JObject child in this.activeList.Children<JObject>())
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabListItem, (Transform) this.listContainer);
      RectTransform transform = (RectTransform) gameObject.transform;
      transform.localScale = new Vector3(1f, 1f, 1f);
      transform.anchoredPosition = new Vector2((float) (int.Parse(child["tabIndex"].ToString()) * 25), (float) (num * -50 - 8));
      gameObject.SetActive(true);
      gameObject.GetComponent<TMP_Text>().text = (1 + num).ToString() + ") " + child["Name"].ToString();
      UIOnHover component = gameObject.GetComponent<UIOnHover>();
      JObject j = child;
      int e = num;
      component.onClick.AddListener((UnityAction) (() =>
      {
        this.item = (JToken) j;
        this.OnSelectListItem(e);
        if (this.selectedListIndex < 0)
          return;
        this.PopulateEdit();
      }));
      ++num;
    }
    this.listContainer.sizeDelta = new Vector2(this.listContainer.sizeDelta.x, (float) (num * 50 + 16));
    this.DeselectItem();
  }

  internal void RefreshSelectedUI(int i)
  {
    if (i < 0 || i >= this.listContainer.childCount)
      return;
    this.listContainer.GetChild(i).GetComponent<UIOnHover>().onClick.Invoke();
  }

  public void OnSelectListItem(int t)
  {
    this.UpdateRaw(false);
    if (t == this.selectedListIndex)
    {
      this.Unselect();
    }
    else
    {
      this.selectedListIndex = t;
      if ((UnityEngine.Object) this.selectedListText != (UnityEngine.Object) null)
        this.selectedListText.GetComponent<UIOnHover>().AlwaysOn = false;
      this.selectedListText = this.listContainer.GetChild(t).gameObject.GetComponent<TMP_Text>();
      this.selectedListText.GetComponent<UIOnHover>().AlwaysOn = true;
      this.buttonDelete.SetActive(true);
      this.buttonCopy.SetActive(true);
      this.buttonPaste.SetActive(this.copiedCommand != null);
      this.buttonMoveUp.SetActive(t > 0);
      this.buttonMoveDown.SetActive(t < this.listContainer.childCount - 1);
    }
  }

  public void PopulateEdit()
  {
    int i = 0;
    for (int index = this.editContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.editContainer.GetChild(index).gameObject);
    if (this.item == null)
    {
      this.editContainer.sizeDelta = new Vector2(this.editContainer.sizeDelta.x, 100f);
    }
    else
    {
      foreach (JProperty x in this.item.Values<JProperty>())
        this.PopulateItem(x, ref i);
      this.editContainer.sizeDelta = new Vector2(this.editContainer.sizeDelta.x, (float) (i * 50 + 16));
    }
  }

  private void PopulateItem(JProperty x, ref int i, int arrayIndex = -1)
  {
    if (x.Value.Type == JTokenType.Object)
    {
      if (!string.Equals(x.Name, "settings"))
        this.CreateHeader(ref i, x.Name);
      if (x.Name.StartsWith("point_"))
      {
        int result1 = 0;
        int result2 = 0;
        foreach (JProperty source in x.Value.Values<JProperty>())
        {
          if (string.Equals(source.Name, nameof (x)))
          {
            foreach (object obj in (IEnumerable<JToken>) source.Values())
              int.TryParse(obj.ToString(), out result1);
          }
          else if (string.Equals(source.Name, "y"))
          {
            foreach (object obj in (IEnumerable<JToken>) source.Values())
              int.TryParse(obj.ToString(), out result2);
          }
        }
        MapCoordPicker.Instance?.FromPoint(new Vector2((float) result1, (float) result2), 0);
      }
      else if (string.Equals(x.Name, "settings"))
      {
        SettingsPlayer sp = x.Value.ToObject<SettingsPlayer>();
        if (this.selectedListIndex >= 0)
        {
          UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, (Transform) this.editContainer);
          RectTransform transform = (RectTransform) uiOnHover.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
          uiOnHover.gameObject.SetActive(true);
          uiOnHover.transform.GetChild(0).GetComponent<TMP_Text>().text = "Outfit";
          uiOnHover.onClick.AddListener((UnityAction) (() => CharacterCreation.Create(sp, (Action<SettingsPlayer>) (set =>
          {
            sp.CopyOutfit(set);
            x.Value = JToken.FromObject((object) sp);
            TutorialEditorMenu.Instance.Edited();
          }))));
          ++i;
        }
        UIOnHover uiOnHover1 = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, (Transform) this.editContainer);
        RectTransform transform1 = (RectTransform) uiOnHover1.transform;
        transform1.localScale = new Vector3(1f, 1f, 1f);
        transform1.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
        uiOnHover1.gameObject.SetActive(true);
        uiOnHover1.transform.GetChild(0).GetComponent<TMP_Text>().text = "Spells";
        uiOnHover1.onClick.AddListener((UnityAction) (() => SpellLobbyChange.Create(sp, (Action<SettingsPlayer>) (set =>
        {
          sp.CopySpells(set, true);
          x.Value = JToken.FromObject((object) sp);
          TutorialEditorMenu.Instance.Edited();
        }), true)));
        ++i;
        return;
      }
      foreach (JProperty property in x.Value.Values<JProperty>())
      {
        if (string.Equals(x.Name, "Name"))
        {
          string name = "";
          foreach (object obj in (IEnumerable<JToken>) x.Values())
            name = obj.ToString();
          int i1 = -1;
          this.CreateHeader(ref i1, name);
        }
        else
          this.CreateItem(property, ref i);
      }
    }
    else if (x.Value.Type == JTokenType.Array)
    {
      bool flag = !string.Equals(x.Name, "parts");
      this.CreateHeader(ref i, x.Name);
      int num = 1;
      foreach (JObject child in x.Value.Children<JObject>())
      {
        int zz = num - 1;
        int i2 = i;
        if (flag)
          this.CreateHeader(ref i2, num.ToString(), true, (Action) (() =>
          {
            ((JArray) x.Value).RemoveAt(zz);
            this.PopulateEdit();
            this.edited = true;
          }), 20);
        foreach (JProperty x1 in child.Values<JProperty>())
          this.PopulateItem(x1, ref i, zz);
        ++num;
      }
      if (!flag)
        return;
      GameObject header = this.CreateHeader(ref i, "--- Add Item ---");
      JArray jt = (JArray) x.Value;
      header.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
      {
        this.Edited();
        foreach (JProperty jproperty in this.json.Values<JProperty>())
        {
          if (string.Equals(jproperty.Name, "lastID"))
          {
            int int32 = Convert.ToInt32(jproperty.Value.ToString());
            jt.Add(JToken.FromObject((object) new CIntitializeVarible.ListVars()
            {
              identifier = int32
            }));
            jproperty.Value = JToken.Parse("\"" + (object) (int32 + 1) + "\"");
            break;
          }
        }
        this.PopulateEdit();
      }));
      ++i;
    }
    else if (string.Equals(x.Name, "Name"))
    {
      int i3 = -1;
      this.CreateItem(x, ref i3, arrayIndex);
    }
    else if (string.Equals(x.Name, "type"))
    {
      int i4 = -3;
      this.CreateItem(x, ref i4, arrayIndex);
    }
    else if (string.Equals(x.Name, "tabIndex"))
    {
      int i5 = -2;
      this.CreateItem(x, ref i5, arrayIndex);
    }
    else
      this.CreateItem(x, ref i, arrayIndex);
  }

  private void CreateDropDown<T>(JProperty x, ref int i, T t, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) d.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    d.gameObject.SetActive(true);
    this.PopulateDropDown<T>(x, d, t, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
  }

  private void CreateDropDownVariables(JProperty x, ref int i, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) d.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    d.gameObject.SetActive(true);
    this.PopulateDropDownVariables(x, d, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
  }

  private void CreateDropDownEntity(JProperty x, ref int i, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) d.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    d.gameObject.SetActive(true);
    this.PopulateDropDownEntities(x, d, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
  }

  private void CreateDropDownCommandID(JProperty x, ref int i, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) d.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    d.gameObject.SetActive(true);
    this.PopulateDropDownCommandID(x, d, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
  }

  private void CreateDropDownSummon(JProperty x, ref int i, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) d.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    d.gameObject.SetActive(true);
    this.PopulateDropDownSummon(x, d, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
  }

  private void CreateItem(JProperty property, ref int i, int arrayIndex = -1)
  {
    if (property.Name.StartsWith("$"))
      return;
    switch (property.Name)
    {
      case "algorithm":
        this.CreateDropDown<MyMaths>(property, ref i, MyMaths.Addition);
        break;
      case "armageddon":
        this.CreateDropDown<MapEnum>(property, ref i, MapEnum.Grassy_Hills);
        break;
      case "bookOf":
        this.CreateDropDown<BookOf>(property, ref i, BookOf.Arcane);
        break;
      case "castType":
        this.CreateDropDown<CastType>(property, ref i, CastType.Power);
        break;
      case "commandID":
        this.CreateDropDownCommandID(property, ref i);
        break;
      case "comparison":
        this.CreateDropDown<Comparison>(property, ref i, Comparison.Equal);
        break;
      case "entity":
        this.CreateDropDownEntity(property, ref i);
        break;
      case "identifier":
        break;
      case "kind":
        this.CreateDropDown<CCreateIndicator.Kinds>(property, ref i, CCreateIndicator.Kinds.Area);
        break;
      case "spellEnum":
        this.CreateDropDown<SpellEnum>(property, ref i, SpellEnum.None);
        break;
      case "spellType":
        this.CreateDropDown<SpellType>(property, ref i, SpellType.None);
        break;
      case "summon":
        this.CreateDropDownSummon(property, ref i);
        break;
      case "tabIndex":
        this.CreateDropDown<Educative.Command.TabIndex>(property, ref i, Educative.Command.TabIndex.Zero, (Action<int>) (v => this.CommandUpdated()));
        break;
      case "type":
        this.CreateDropDown<Educative.Command.Type>(property, ref i, Educative.Command.Type.Info, (Action<int>) (v => this.CommandChange((Educative.Command.Type) v)));
        break;
      case "which":
        this.CreateDropDownVariables(property, ref i);
        break;
      default:
        if (property.Name.StartsWith("bool_"))
        {
          pfabBoolJsonItem pfabBoolJsonItem = UnityEngine.Object.Instantiate<pfabBoolJsonItem>(this.pfabBool, (Transform) this.editContainer);
          RectTransform transform = (RectTransform) pfabBoolJsonItem.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
          pfabBoolJsonItem.gameObject.SetActive(true);
          pfabBoolJsonItem.init(property, arrayIndex);
          ++i;
          break;
        }
        if (string.Equals(property.Name, "message"))
        {
          pfabEditJsonItem pfabEditJsonItem = UnityEngine.Object.Instantiate<pfabEditJsonItem>(this.pfabItemMultiline, (Transform) this.editContainer);
          RectTransform transform = (RectTransform) pfabEditJsonItem.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
          pfabEditJsonItem.gameObject.SetActive(true);
          pfabEditJsonItem.init(property, arrayIndex);
          i += 5;
          break;
        }
        if (string.Equals(property.Name, "code"))
        {
          pfabEditJsonItem pfabEditJsonItem = UnityEngine.Object.Instantiate<pfabEditJsonItem>(this.pfabCodeEditor, (Transform) this.editContainer);
          RectTransform transform = (RectTransform) pfabEditJsonItem.transform;
          transform.localScale = new Vector3(1f, 1f, 1f);
          transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
          pfabEditJsonItem.gameObject.SetActive(true);
          pfabEditJsonItem.init(property, arrayIndex, property.Name, true);
          i += 8;
          pfabEditJsonItem.value.Select();
          this.CreateButton(ref i, "Spells (Clipboard)", (UnityAction) (() => SpellLobbyChange.Create(onEnd: (Action<SettingsPlayer>) (set =>
          {
            StringBuilder stringBuilder = new StringBuilder("sum.spells = {");
            foreach (byte spell in set.spells)
            {
              if (spell != byte.MaxValue)
              {
                if (stringBuilder.Length > 10)
                  stringBuilder.Append(", ");
                stringBuilder.Append("\"");
                stringBuilder.Append(Inert.Instance.spells.GetItem((int) spell).Key);
                stringBuilder.Append("\"");
              }
              else
                break;
            }
            stringBuilder.Append("} ");
            stringBuilder.Append("\n sum.elemental = BookOf.").Append((object) (BookOf) set.fullBook);
            Global.systemCopyBuffer = stringBuilder.ToString();
          }), center: true)));
          this.CreateButton(ref i, "Outfit (Clipboard)", (UnityAction) (() => ChangeOutfitMenu.Create(false, onEnd: (Action<SettingsPlayer>) (set =>
          {
            StringBuilder stringBuilder = new StringBuilder("sum.outfit = {");
            for (int o = 0; o < 9; ++o)
            {
              if (o > 0)
                stringBuilder.Append(", ");
              stringBuilder.Append(set.GetOutfitIndex((Outfit) o));
            }
            stringBuilder.Append("} \nsum.colors = {");
            for (int i1 = 0; i1 < 4; ++i1)
            {
              if (i1 > 0)
                stringBuilder.Append(", ");
              stringBuilder.Append("\"#").Append(ColorUtility.ToHtmlStringRGB((Color) set.coloring.colors[0].FromIndex(i1))).Append("\"");
            }
            stringBuilder.Append("}");
            Global.systemCopyBuffer = stringBuilder.ToString();
          }))));
          this.CreateButton(ref i, "API Documentation", (UnityAction) (() => Global.OpenURL("https://pur3extreme.github.io/ArcTutorialDocumentation.html")));
          if (!Global.OS.Is(OperatingSystem.Standalone))
            break;
          this.CreateButton_Code_convert(ref i);
          break;
        }
        pfabEditJsonItem pfabEditJsonItem1 = UnityEngine.Object.Instantiate<pfabEditJsonItem>(this.pfabItem, (Transform) this.editContainer);
        RectTransform transform1 = (RectTransform) pfabEditJsonItem1.transform;
        transform1.localScale = new Vector3(1f, 1f, 1f);
        transform1.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
        pfabEditJsonItem1.gameObject.SetActive(true);
        int num = property.Name.IndexOf('_');
        pfabEditJsonItem1.init(property, arrayIndex, num > -1 ? property.Name.Substring(num + 1) : (string) null);
        ++i;
        break;
    }
  }

  private void CreateButton(ref int i, string text, UnityAction onClick)
  {
    UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) uiOnHover.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    uiOnHover.gameObject.SetActive(true);
    uiOnHover.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
    uiOnHover.onClick.AddListener(onClick);
    ++i;
  }

  private void CreateButton_Code_convert(ref int i)
  {
    UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) uiOnHover.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2(200f, (float) (i * -50 - 8));
    uiOnHover.gameObject.SetActive(true);
    uiOnHover.transform.GetChild(0).GetComponent<TMP_Text>().text = "Convert to code only";
    uiOnHover.onClick.AddListener((UnityAction) (() => ChooseJsonDialog.Create(true, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) ((s, tut, _index) =>
    {
      this.UpdateRaw();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("--NAME = ").Append(this.raw.Name);
      stringBuilder.Append("\n--DESCRIPTION = ").Append(this.raw.message);
      stringBuilder.Append("\n--MAP = ").Append(this.raw.mapId);
      stringBuilder.Append("\n--SPELLS = {");
      for (int index = 0; index < 16 && this.raw.settings.spells[index] < byte.MaxValue; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(Inert.Instance._spells[(int) this.raw.settings.spells[index]].name);
      }
      stringBuilder.Append("}");
      stringBuilder.Append("\n--DEBUG = ").Append(((CScript) this.raw.commands[this.selectedListIndex]).bool_debug.ToString().ToUpper());
      stringBuilder.Append("\n\n--CODE--\n\n");
      stringBuilder.Append(((CScript) this.raw.commands[this.selectedListIndex]).code);
      this.Editing(s);
      Global.SaveTutorialCode(s, stringBuilder.ToString());
    }))));
    ++i;
  }

  private GameObject CreateHeader(ref int i, string name, bool canDelete = false, Action a = null, int offsetX = 200)
  {
    GameObject header = UnityEngine.Object.Instantiate<GameObject>(this.pfabListItem, (Transform) this.editContainer);
    RectTransform transform = (RectTransform) header.transform;
    transform.localScale = new Vector3(1f, 1f, 1f);
    transform.anchoredPosition = new Vector2((float) offsetX, (float) (i * -50 - 8));
    header.gameObject.SetActive(true);
    int num = name.IndexOf('_');
    if (num > 0)
      header.GetComponent<TMP_Text>().text = name.Substring(num + 1);
    else
      header.GetComponent<TMP_Text>().text = name;
    ++i;
    if (canDelete)
    {
      Transform child = header.transform.GetChild(0);
      child.gameObject.SetActive(true);
      child.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
      {
        Action action = a;
        if (action == null)
          return;
        action();
      }));
    }
    return header;
  }

  public bool FindInputByName(string s, string value)
  {
    for (int index = this.editContainer.childCount - 1; index >= 0; --index)
    {
      pfabEditJsonItem component = this.editContainer.GetChild(index).GetComponent<pfabEditJsonItem>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && string.Equals(s, component.text.text))
      {
        component.value.text = value;
        component.value.onEndEdit.Invoke(value);
        return true;
      }
    }
    return false;
  }

  public void ClickMainMenu()
  {
    this.CheckSave();
    Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
  }

  public void ClickTestField()
  {
    this.CheckSave();
    this.raw = Tutorial.FromJson(this.json);
    this.raw.debugLog = true;
    this.raw.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
    Client.allowtutorialDebugging = true;
  }

  public void ClickTestCode()
  {
    this.CheckSave();
    if (!this.editing.EndsWith("2"))
      this.editing += "2";
    this.raw = Global.OpenTutorial(Global.GetTutorialPath + this.editing);
    this.raw.debugLog = true;
    this.raw.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
    Client.allowtutorialDebugging = true;
  }

  public void ClickSaveAs()
  {
    ChooseJsonDialog.Create(true, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) ((s, tut, _index) =>
    {
      Global.SaveTutorial(s, this.json.ToString());
      this.Editing(s + ".arcTutorial");
    }));
  }

  public void ClickOpen()
  {
    ChooseJsonDialog.Create(false, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) ((s, tut, _index) => this.OnOpen(s, tut)), true);
  }

  public void ClickNew() => this.OnOpen("", Tutorial.Default);

  public void OnOpen(string s, Tutorial j)
  {
    j.ValidateIdentifiers();
    if (string.IsNullOrEmpty(s))
    {
      s = "_Temporary.arcTutorial";
      this.ToggleCodeTest(false);
    }
    else
    {
      if (Path.GetExtension(s).EndsWith("2"))
        this.ToggleCodeTest(true);
      else
        this.ToggleCodeTest(File.Exists(s + "2"));
      s = s.Substring(Global.GetTutorialPath.Length);
    }
    this.Editing(s);
    this.edited = false;
    this.DeselectItem();
    this.DeselectList();
    this.raw = j;
    this.json = j.ToJson();
    this.PopulateList();
    this.Unselect();
    this.FindMapImage();
  }

  public void ToggleCodeTest(bool v) => this.buttonCodeFile.SetActive(true);

  public void FindMapImage()
  {
    for (int index = 0; index < this.mapDropdown.options.Count; ++index)
    {
      if (string.Equals(this.raw.mapId, this.mapDropdown.options[index].text))
      {
        this.mapDropdown.SetValueWithoutNotify(index);
        this.UpdateMapImage(index);
        return;
      }
    }
    this.mapDropdown.SetValueWithoutNotify(0);
  }

  public void UpdateMapImage(int i)
  {
    this.imgMap.sprite = ClientResources.Instance._maps[i];
    float num1 = 1000f;
    float height = (float) this.imgMap.sprite.texture.height;
    float width = (float) this.imgMap.sprite.texture.width;
    this.imgMapBg.rectTransform.sizeDelta = new Vector2(width, height);
    this.imgMap.rectTransform.sizeDelta = new Vector2(width, height);
    float num2 = Mathf.Min((float) (240.0 / (double) height), num1 / width);
    this.imgMapBg.transform.localScale = new Vector3(num2, num2, 1f);
  }

  public void Edited()
  {
    this.changesMade = true;
    if (!this.edited && string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary.arcTutorial");
    this.edited = true;
  }

  public void Editing(string s)
  {
    PlayerPrefs.SetString(TutorialEditorMenu.prefOpened, s);
    this.editing = s;
    this.txt_editing.text = s.Substring(0, s.Length - Path.GetExtension(s).Length);
  }

  public void Hover(string s) => MyToolTip.Show(s);

  public void HoverLeave() => MyToolTip.Close();

  public void CheckSave()
  {
    if (!this.edited)
      return;
    if (string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary.arcTutorial");
    if (this.editing.EndsWith("2"))
    {
      this.editing = this.editing.Substring(0, this.editing.Length - 1);
      PlayerPrefs.SetString(TutorialEditorMenu.prefOpened, this.editing);
      TutorialEditorMenu.prefOpened = this.editing;
      this.Editing(this.editing);
    }
    Global.SaveTutorial(this.editing, this.json.ToString());
  }

  public void SelectQuickSave()
  {
    try
    {
      Global.SaveTutorial(string.IsNullOrEmpty(this.editing) ? "_Temporary" : this.editing, this.json.ToString());
      this.edited = false;
    }
    catch (Exception ex)
    {
      MyContextMenu.ShowSimple("Save Failed - An error occured:\n" + ex.Message);
    }
  }

  public void PopulateDropDown<T>(JProperty j, TMP_Dropdown d, T t, Action<int> additionalAction = null)
  {
    int result = 0;
    int.TryParse(j.Value.ToString(), out result);
    T[] e = (T[]) Enum.GetValues(typeof (T));
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int num1 = -1;
    int num2 = 0;
    foreach (T obj in e)
    {
      optionDataList.Add(new TMP_Dropdown.OptionData(obj.ToString()));
      if (Convert.ToInt32((object) obj) == result)
        num1 = num2;
      ++num2;
    }
    d.options = optionDataList;
    d.value = num1;
    d.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      j.Value = (JToken) Convert.ToInt32((object) e[v]);
      TutorialEditorMenu.Instance.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }

  public void PopulateDropDownVariables(JProperty j, TMP_Dropdown d, Action<int> additionalAction = null)
  {
    int result = 0;
    int.TryParse(j.Value.ToString(), out result);
    From[] e = (From[]) Enum.GetValues(typeof (From));
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int num1 = -1;
    int num2 = 0;
    foreach (From from in e)
    {
      optionDataList.Add(new TMP_Dropdown.OptionData(from.ToString()));
      if (Convert.ToInt32((object) from) == result)
        num1 = num2;
      ++num2;
    }
    List<Tutorial.ReturnEntity> t = this.raw.CalculateVariables();
    int index = 0;
    foreach (Tutorial.ReturnEntity returnEntity in t)
    {
      optionDataList.Add(new TMP_Dropdown.OptionData(t[index].name));
      if (Convert.ToInt32(returnEntity.identifier) == result)
        num1 = num2;
      ++num2;
      ++index;
    }
    d.options = optionDataList;
    d.value = num1;
    d.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      j.Value = (JToken) (v >= e.Length ? Convert.ToInt32(t[v - e.Length].identifier) : Convert.ToInt32((object) e[v]));
      TutorialEditorMenu.Instance.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }

  public void PopulateDropDownEntities(JProperty j, TMP_Dropdown d, Action<int> additionalAction = null)
  {
    int result = 0;
    int.TryParse(j.Value.ToString(), out result);
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int num1 = -1;
    int num2 = 0;
    List<Tutorial.ReturnEntity> t = this.raw.CalculatePossibilities();
    foreach (Tutorial.ReturnEntity returnEntity in t)
    {
      if (returnEntity.index < 0)
        optionDataList.Add(new TMP_Dropdown.OptionData(returnEntity.index == -2 ? "Player" : "Any"));
      else
        optionDataList.Add(new TMP_Dropdown.OptionData(this.raw.commands[returnEntity.index].Name));
      if (returnEntity.identifier == result)
        num1 = num2;
      ++num2;
    }
    d.options = optionDataList;
    d.value = num1;
    d.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      j.Value = (JToken) t[v].identifier;
      TutorialEditorMenu.Instance.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }

  public void PopulateDropDownCommandID(JProperty j, TMP_Dropdown d, Action<int> additionalAction = null)
  {
    int result = 0;
    int.TryParse(j.Value.ToString(), out result);
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int num1 = -1;
    int num2 = 0;
    List<Tutorial.ReturnEntity> t = this.raw.CalculateCommandID();
    foreach (Tutorial.ReturnEntity returnEntity in t)
    {
      optionDataList.Add(new TMP_Dropdown.OptionData(returnEntity.name));
      if (returnEntity.identifier == result)
        num1 = num2;
      ++num2;
    }
    d.options = optionDataList;
    d.value = num1;
    d.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      j.Value = (JToken) t[v].identifier;
      TutorialEditorMenu.Instance.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }

  public void PopulateDropDownSummon(JProperty j, TMP_Dropdown d, Action<int> additionalAction = null)
  {
    int result = 0;
    int.TryParse(j.Value.ToString(), out result);
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    int num1 = 0;
    int num2 = 1;
    optionDataList.Add(new TMP_Dropdown.OptionData("Player Character"));
    foreach (Spell summonSpell in ClientResources.Instance.summonSpells)
    {
      optionDataList.Add(new TMP_Dropdown.OptionData(summonSpell.toSummon.name));
      if (num2 == result)
        num1 = num2;
      ++num2;
    }
    d.options = optionDataList;
    d.value = num1;
    d.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      j.Value = (JToken) v;
      TutorialEditorMenu.Instance.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }
}
