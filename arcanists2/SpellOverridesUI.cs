// Decompiled with JetBrains decompiler
// Type: SpellOverridesUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using Hazel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class SpellOverridesUI : MonoBehaviour
{
  public RectTransform editContainer;
  public RectTransform listContainer;
  public GameObject pfabListItem;
  public pfabEditJsonItem pfabItem;
  public pfabBoolJsonItem pfabBool;
  public TMP_Dropdown pfabDropdown;
  public TMP_Text txtHeaderViewing;
  public GameObject buttonUpload;
  [Header("Buttons")]
  public GameObject buttonDelete;
  public GameObject buttonAdd;
  public TMP_Text txt_editing;
  internal TMP_Text selectedListText;
  internal JObject json = new JObject();
  private JArray activeList;
  internal JToken item;
  internal SpellOverrides raw;
  private int selectedListIndex = -1;
  internal bool edited;
  internal bool changesMade;
  internal string editing = "";
  internal int viewing;
  private static string prefOpened = "prefspelloverridesopened";

  public static SpellOverridesUI Instance { get; private set; }

  public static void Create()
  {
    if ((UnityEngine.Object) SpellOverridesUI.Instance != (UnityEngine.Object) null)
      return;
    Controller.ShowPopup(Controller.Instance.spellOverrides.gameObject);
  }

  private void Awake()
  {
    SpellOverridesUI.Instance = this;
    if (Client.game == null)
      return;
    Client.game.CleanUp();
    Client.game = (ZGame) null;
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) SpellOverridesUI.Instance == (UnityEngine.Object) this))
      return;
    SpellOverridesUI.Instance = (SpellOverridesUI) null;
  }

  private void Start()
  {
    string path = SpellOverrides.PATH + PlayerPrefs.GetString(SpellOverridesUI.prefOpened, "_Temporary" + SpellOverrides.EXT);
    if (File.Exists(path))
    {
      this.raw = SpellOverrides.FromFile(path);
      this.Editing(PlayerPrefs.GetString(SpellOverridesUI.prefOpened, "_Temporary" + SpellOverrides.EXT));
    }
    else
      this.raw = new SpellOverrides();
    this.json = this.raw.ToJson();
    this.ClickView(0);
    this.ClickGlobal();
    this.buttonUpload.SetActive(Client.isConnected && Client.MyAccount.accountType.isDev());
  }

  public void ClickView(int i)
  {
    this.viewing = i;
    switch (i)
    {
      case 0:
        this.txtHeaderViewing.text = "Spells";
        break;
      case 1:
        this.txtHeaderViewing.text = "Creatures";
        break;
    }
    this.PopulateList();
  }

  public void PopulateList()
  {
    this.activeList = (JArray) null;
    JToken jtoken;
    if (this.viewing == 0 && this.json.TryGetValue("listSpells", out jtoken))
      this.activeList = (JArray) jtoken;
    else if (this.viewing == 1 && this.json.TryGetValue("listCreatures", out jtoken))
      this.activeList = (JArray) jtoken;
    if (this.activeList == null)
    {
      this.activeList = JArray.Parse("[]");
      this.json.Add("listSpells", (JToken) this.activeList);
    }
    int num = 0;
    for (int index = this.listContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.listContainer.GetChild(index).gameObject);
    this.item = (JToken) null;
    foreach (JObject child in this.activeList.Children<JObject>())
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabListItem, (Transform) this.listContainer);
      gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
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

  public void ClickDelete()
  {
    if (this.activeList == null || this.activeList.Count <= 0)
      return;
    this.activeList.RemoveAt(this.selectedListIndex);
    if (this.selectedListIndex >= this.activeList.Count)
      --this.selectedListIndex;
    this.PopulateList();
    if (this.selectedListIndex <= 0)
      return;
    this.listContainer.GetChild(this.selectedListIndex).GetComponent<UIOnHover>().onClick.Invoke();
  }

  public void ClickAdd()
  {
    MyContextMenu.Show().AddInput((Action<string>) (s =>
    {
      if (this.viewing == 0)
      {
        Spell spell = Inert.GetSpell(s);
        if ((UnityEngine.Object) spell != (UnityEngine.Object) null)
          this.activeList.Add(JToken.FromObject((object) new SpellOverrides.ASpell()
          {
            Name = s,
            stats = spell.stats
          }));
        else
          Debug.LogError((object) ("Spell Not Found: " + s));
      }
      else
      {
        IEnumerable<Creature> source = ((IEnumerable<Creature>) Inert.Instance._creatures).Where<Creature>((Func<Creature, bool>) (t => string.Equals(t.name, s)));
        Creature creature = source != null ? source.ToList<Creature>()[0] : (Creature) null;
        if ((UnityEngine.Object) creature != (UnityEngine.Object) null)
          this.activeList.Add(JToken.FromObject((object) new SpellOverrides.ACreature()
          {
            Name = s,
            stats = creature.stats
          }));
        else
          Debug.LogError((object) ("Creature Not Found: " + s));
      }
      this.Unselect();
      this.PopulateList();
    }));
  }

  public void ClickClose() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void _OnLoad(string s)
  {
    this.raw = SpellOverrides.FromFile(s);
    this.json = this.raw.ToJson();
    this.UpdateRaw();
    this.Unselect();
    this.PopulateList();
    this.Editing(s.Substring(SpellOverrides.PATH.Length));
  }

  public void _OnSave(string s)
  {
    this.UpdateRaw();
    this.raw.SaveToFile(s);
    this.Editing(s.Substring(SpellOverrides.PATH.Length));
  }

  public void _OnUpload(string s)
  {
    this._OnSave(s);
    SpellOverrides.SerializeAsServer(this.raw);
    this.Editing(s.Substring(SpellOverrides.PATH.Length));
    Client.SendToServer(SpellOverrides.ServerBytes);
  }

  public void _OnCompare(string s)
  {
    SpellOverrides b = SpellOverrides.FromFile(s);
    this.UpdateRaw();
    SpellOverrides.Compare(this.raw, b);
  }

  public void ClickLoad()
  {
    MyFilePicker.Create("Spell Overrides", "SpellOverrides", SpellOverrides.EXT, false, new Action<string>(this._OnLoad), ContentType.SpellOverrides);
  }

  public void ClickSave()
  {
    MyFilePicker.Create("Spell Overrides", "SpellOverrides", SpellOverrides.EXT, true, new Action<string>(this._OnSave), ContentType.SpellOverrides);
  }

  public void ClickUpload()
  {
    MyFilePicker.Create("Spell Overrides", "SpellOverrides", SpellOverrides.EXT, true, new Action<string>(this._OnUpload), ContentType.SpellOverrides);
  }

  public void ClickCompare()
  {
    MyFilePicker.Create("Compare Spell Overrides", "SpellOverrides", SpellOverrides.EXT, false, new Action<string>(this._OnCompare), ContentType.SpellOverrides);
  }

  public void ClickNew()
  {
    this.raw = new SpellOverrides();
    this.json = this.raw.ToJson();
    this.PopulateList();
    this.Unselect();
    this.Editing("_Temporary" + SpellOverrides.EXT);
  }

  public void ClickApply()
  {
    this.UpdateRaw();
    Inert.Instance.Apply(this.raw);
  }

  public void Unselect()
  {
    if ((UnityEngine.Object) this.selectedListText != (UnityEngine.Object) null)
      this.selectedListText.GetComponent<UIOnHover>().AlwaysOn = false;
    this.DeselectItem();
    this.item = this.json.GetValue("global");
    this.PopulateEdit();
    this.buttonDelete.SetActive(false);
  }

  private GameObject CreateHeader(ref int i, string name, bool canDelete = false, Action a = null, int offsetX = 200)
  {
    GameObject header = UnityEngine.Object.Instantiate<GameObject>(this.pfabListItem, (Transform) this.editContainer);
    header.transform.localScale = new Vector3(1f, 1f, 1f);
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

  public void DeselectItem()
  {
    this.selectedListText = (TMP_Text) null;
    this.selectedListIndex = -1;
    this.item = (JToken) null;
    for (int index = this.editContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.editContainer.GetChild(index).gameObject);
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
    }
  }

  public void ClickGlobal()
  {
    this.UpdateRaw(false);
    this.Unselect();
  }

  public void UpdateRaw(bool force = true)
  {
    if (!this.changesMade && !force)
      return;
    this.changesMade = false;
    this.raw = SpellOverrides.FromJson(this.json);
  }

  public void Edited()
  {
    this.changesMade = true;
    if (!this.edited && string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary" + SpellOverrides.EXT);
    this.edited = true;
  }

  public void Editing(string s)
  {
    PlayerPrefs.SetString(SpellOverridesUI.prefOpened, s);
    this.editing = s;
    this.txt_editing.text = s.Substring(0, s.Length - Path.GetExtension(s).Length);
  }

  public void PopulateEdit()
  {
    int i = 0;
    for (int index = this.editContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.editContainer.GetChild(index).gameObject);
    if (this.item == null)
      return;
    foreach (JProperty x in this.item.Values<JProperty>())
      this.PopulateItem(x, ref i);
    this.editContainer.sizeDelta = new Vector2(this.editContainer.sizeDelta.x, (float) (i * 50 + 16));
  }

  private void PopulateItem(JProperty x, ref int i, int arrayIndex = -1)
  {
    if (x.Value.Type == JTokenType.Object)
    {
      if (!string.Equals(x.Name, "settings"))
        this.CreateHeader(ref i, x.Name);
      foreach (JProperty jproperty in x.Value.Values<JProperty>())
      {
        if (string.Equals(x.Name, "Name"))
        {
          string name = "";
          foreach (object obj in (IEnumerable<JToken>) x.Values())
            name = obj.ToString();
          int i1 = -1;
          this.CreateHeader(ref i1, name);
        }
        else if (jproperty.Value.Type == JTokenType.Array)
        {
          this.PopulateItem(jproperty, ref i);
          int i2 = -1;
          this.CreateHeader(ref i2, "End Array");
        }
        else
          this.CreateItem(jproperty, ref i);
      }
    }
    else if (x.Value.Type == JTokenType.Array)
    {
      bool flag = !string.Equals(x.Name, "spells");
      this.CreateHeader(ref i, x.Name);
      int num = 1;
      foreach (JObject child in x.Value.Children<JObject>())
      {
        int zz = num - 1;
        int i3 = i;
        if (flag)
          this.CreateHeader(ref i3, num.ToString(), true, (Action) (() =>
          {
            ((JArray) x.Value).RemoveAt(zz);
            this.PopulateEdit();
            this.edited = true;
          }), 20);
        else
          this.CreateHeader(ref i, num.ToString());
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
      int i4 = -1;
      this.CreateItem(x, ref i4, arrayIndex);
    }
    else if (string.Equals(x.Name, "type"))
    {
      int i5 = -3;
      this.CreateItem(x, ref i5, arrayIndex);
    }
    else if (string.Equals(x.Name, "tabIndex"))
    {
      int i6 = -2;
      this.CreateItem(x, ref i6, arrayIndex);
    }
    else
      this.CreateItem(x, ref i, arrayIndex);
  }

  private void CreateItem(JProperty property, ref int i, int arrayIndex = -1)
  {
    if (property.Name.StartsWith("$"))
      return;
    switch (property.Name)
    {
      case "bookOf":
        this.CreateDropDown<BookOf>(property, ref i, BookOf.Nothing);
        break;
      case "damageOverDistance":
      case "forceOverDistance":
        this.CreateDropDown<Curve>(property, ref i, Curve.None);
        break;
      case "damageType":
        this.CreateDropDown<DamageType>(property, ref i, DamageType.None);
        break;
      case "explosionCutout":
        this.CreateDropDown<ExplosionCutout>(property, ref i, ExplosionCutout.None);
        break;
      case "race":
        this.CreateDropDown<CreatureRace>(property, ref i, CreatureRace.Unaffiliated);
        break;
      case "serializedSpellEnum":
        this.CreateDropDown<SpellEnum>(property, ref i, SpellEnum.None);
        break;
      case "spellEnum":
        this.CreateDropDown<SpellEnum>(property, ref i, SpellEnum.None);
        break;
      case "spellLogic":
        this.CreateDropDown<SpellLogic>(property, ref i, SpellLogic.Arrow);
        break;
      case "spellType":
        this.CreateDropDown<SpellType>(property, ref i, SpellType.None);
        break;
      case "targetType":
        this.CreateDropDown<TargetType>(property, ref i, TargetType.None);
        break;
      case "type":
        if (this.viewing == 0)
        {
          this.CreateDropDown<CastType>(property, ref i, CastType.Blit);
          break;
        }
        this.CreateDropDown<CreatureType>(property, ref i, CreatureType.Other);
        break;
      default:
        string b = (string) null;
        foreach (object obj in (IEnumerable<JToken>) property.Values())
          b = obj.ToString();
        if ((string.Equals("False", b, StringComparison.OrdinalIgnoreCase) ? 1 : (string.Equals("True", b, StringComparison.OrdinalIgnoreCase) ? 1 : 0)) != 0)
        {
          pfabBoolJsonItem pfabBoolJsonItem = UnityEngine.Object.Instantiate<pfabBoolJsonItem>(this.pfabBool, (Transform) this.editContainer);
          pfabBoolJsonItem.transform.localScale = new Vector3(1f, 1f, 1f);
          pfabBoolJsonItem.gameObject.SetActive(true);
          pfabBoolJsonItem.initSpellOverride(property, arrayIndex);
          ++i;
          break;
        }
        pfabEditJsonItem pfabEditJsonItem = UnityEngine.Object.Instantiate<pfabEditJsonItem>(this.pfabItem, (Transform) this.editContainer);
        pfabEditJsonItem.transform.localScale = new Vector3(1f, 1f, 1f);
        pfabEditJsonItem.gameObject.SetActive(true);
        int num = property.Name.IndexOf('_');
        pfabEditJsonItem.initSpellOverride(property, arrayIndex, num > -1 ? property.Name.Substring(num + 1) : (string) null);
        ++i;
        break;
    }
  }

  private void CreateDropDown<T>(JProperty x, ref int i, T t, Action<int> additionalAction = null)
  {
    TMP_Dropdown d = UnityEngine.Object.Instantiate<TMP_Dropdown>(this.pfabDropdown, (Transform) this.editContainer);
    d.transform.localScale = new Vector3(1f, 1f, 1f);
    d.gameObject.SetActive(true);
    this.PopulateDropDown<T>(x, d, t, additionalAction);
    d.transform.GetChild(0).GetComponent<TMP_Text>().text = x.Name;
    ++i;
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
      this.Edited();
      Action<int> action = additionalAction;
      if (action == null)
        return;
      action(v);
    }));
  }

  public void ClickDiff()
  {
    this.UpdateRaw();
    this.raw.CompareDefault();
  }

  private char get(char e)
  {
    int num1 = (int) ~e & 15;
    int num2 = 97;
    int num3 = 122 - num2;
    return (char) (num1 % num3 + num2);
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.F12))
      return;
    this.UpdateRaw();
    Global.systemCopyBuffer = this.json.ToString();
    Debug.Log((object) "Copied to Clipboard");
  }
}
