

using Educative;
using InGameCodeEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class TutorialCodeEditor : MonoBehaviour
{
  public CodeEditor codeEditor;
  public GameObject panelPoint;
  public TMP_Text txt_editing;
  public TMP_Dropdown mapDropdown;
  public Image imgMap;
  public Image imgMapBg;
  private string editing = "";
  private bool edited;
  internal static string prefOpened = "preftutopened";
  internal const string DefaultTutorial = "--NAME = Unnamed\r\n--DESCRIPTION = No Description\r\n--MAP = 0\r\n--SPELLS = {}\r\n--DEBUG = TRUE\r\n\r\n--CODE--\r\n\r\nfunction main()\r\n    while true do\r\n        --Some lua logic goes here\r\n        --Open the API github page for basic documentation and usage\r\n        coroutine.yield(0)\r\n    end\r\nend\r\n";

  public static TutorialCodeEditor Instance { get; private set; }

  private int MapID
  {
    get
    {
      string n = this.Get("--MAP =");
      return string.IsNullOrEmpty(n) ? 0 : this.mapDropdown.options.FindIndex((Predicate<TMP_Dropdown.OptionData>) (x => string.Equals(x.text, n)));
    }
  }

  private string MapName
  {
    get => this.Get("--MAP =");
    set => this.Set("--MAP =", value.ToString());
  }

  private void Awake()
  {
    TutorialCodeEditor.Instance = this;
    if (Client.game == null)
      return;
    Client.game.CleanUp();
    Client.game = (ZGame) null;
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) TutorialCodeEditor.Instance == (UnityEngine.Object) this))
      return;
    TutorialCodeEditor.Instance = (TutorialCodeEditor) null;
  }

  private int ToInt(string s)
  {
    int result = 0;
    int.TryParse(s, out result);
    return result;
  }

  private string Get(string search)
  {
    string text = this.codeEditor.Text;
    int num1 = text.IndexOf(search, StringComparison.OrdinalIgnoreCase);
    if (num1 >= 0)
    {
      int num2 = text.IndexOfAny(new char[2]{ '\n', '\r' }, num1 + search.Length);
      if (num2 > num1)
        return text.Substring(num1 + search.Length, num2 - num1 - search.Length).Trim();
    }
    return "";
  }

  private void Set(string search, string value, bool force = true)
  {
    string text = this.codeEditor.Text;
    int length = text.IndexOf(search, StringComparison.OrdinalIgnoreCase);
    if (length >= 0)
    {
      int startIndex = text.IndexOfAny(new char[2]
      {
        '\n',
        '\r'
      }, length + search.Length);
      if (startIndex > length)
      {
        this.codeEditor.Text = text.Substring(0, length) + search + " " + value + text.Substring(startIndex, text.Length - startIndex);
      }
      else
      {
        if (!force)
          return;
        this.codeEditor.Text = text.Substring(0, length) + search + " " + value + "\n";
      }
    }
    else
    {
      if (force)
        this.codeEditor.Text = search + value + "\n" + text;
      this.Edited();
    }
  }

  public void OnMapValueChanged(int x)
  {
    this.MapName = this.mapDropdown.options[x].text;
    this.FindMapImage();
  }

  private void Start()
  {
    string s = PlayerPrefs.GetString(TutorialCodeEditor.prefOpened, "_Temporary.arcTutorial2");
    string path = Global.GetTutorialPath + s;
    if (File.Exists(path))
    {
      if (string.Equals(Path.GetExtension(path), ".arcTutorial2", StringComparison.OrdinalIgnoreCase))
      {
        this.codeEditor.Text = File.ReadAllText(path);
      }
      else
      {
        Tutorial tutorial = Tutorial.FromJson(File.ReadAllText(path));
        tutorial.ToCodeOnly();
        string str = path + "2";
        s += "2";
        this.codeEditor.Text = tutorial.ToCodeOnly();
      }
    }
    if (string.IsNullOrEmpty(this.codeEditor.Text))
    {
      s = "_Temporary.arcTutorial2";
      this.codeEditor.Text = "--NAME = Unnamed\r\n--DESCRIPTION = No Description\r\n--MAP = 0\r\n--SPELLS = {}\r\n--DEBUG = TRUE\r\n\r\n--CODE--\r\n\r\nfunction main()\r\n    while true do\r\n        --Some lua logic goes here\r\n        --Open the API github page for basic documentation and usage\r\n        coroutine.yield(0)\r\n    end\r\nend\r\n";
    }
    List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
    foreach (Sprite map in ClientResources.Instance._maps)
      optionDataList.Add(new TMP_Dropdown.OptionData(map.name));
    this.mapDropdown.options = optionDataList;
    this.mapDropdown.SetValueWithoutNotify(this.MapID);
    this.mapDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnMapValueChanged));
    this.FindMapImage();
    this.Editing(s);
    this.edited = false;
  }

  public void ClickClose()
  {
    this.CheckSave();
    Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
  }

  public void ClickCloseNoSave()
  {
    Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
  }

  public void ClickOpen()
  {
    ChooseJsonDialog.Create(false, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) null, true, (Action<string, string, int>) ((s, text, index) =>
    {
      if (Global.GetTutorialPath.Length < s.Length)
        s = s.Substring(Global.GetTutorialPath.Length);
      if (s.EndsWith(".arcTutorial", StringComparison.OrdinalIgnoreCase))
      {
        string codeOnly = Tutorial.FromJson(text).ToCodeOnly();
        s += "2";
        text = codeOnly;
      }
      this.Editing(s);
      this.OnOpen(text);
      this.edited = false;
    }));
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.F1))
      return;
    this.CopyToClipBoard();
  }

  public void CopyToClipBoard()
  {
    Global.systemCopyBuffer = this.codeEditor.Text;
    Debug.Log((object) "Copied Code");
  }

  public void CreateDefaultTutorial()
  {
    byte[] bytes = Tutorial.FromCodeOnly(this.codeEditor.Text).ToBytes();
    StringBuilder stringBuilder = new StringBuilder("new Data(){ name = \"" + Path.GetFileNameWithoutExtension(this.editing) + "\" , bytes = new byte[] { ");
    for (int index = 0; index < bytes.Length - 1; ++index)
      stringBuilder.Append(bytes[index].ToString()).Append(",");
    stringBuilder.Append(bytes[bytes.Length - 1]);
    stringBuilder.Append("} },\n");
    Global.systemCopyBuffer = stringBuilder.ToString();
    Debug.Log((object) "Copied Json as bytes (DefaultTutorials.cs) to clipboard");
  }

  private void OnOpen(string s)
  {
    this.codeEditor.Text = s;
    this.mapDropdown.SetValueWithoutNotify(this.MapID);
    this.FindMapImage();
  }

  public void ClickSaveAs()
  {
    ChooseJsonDialog.Create(true, ChooseJsonDialog.Viewing.Custom, (Action<string, Tutorial, int>) ((s, tut, index) =>
    {
      Global.SaveTutorialCode(s, this.codeEditor.Text);
      this.Editing(s + ".arcTutorial2");
    }));
  }

  public void ClickTest()
  {
    if (!this.edited)
    {
      if (!this.editing.EndsWith("2"))
        this.editing += "2";
      if (File.Exists(Global.GetTutorialPath + this.editing))
      {
        Tutorial tutorial = Global.OpenTutorial(Global.GetTutorialPath + this.editing);
        tutorial.debugLog = true;
        tutorial.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
        return;
      }
    }
    this.CheckSave();
    if (!this.editing.EndsWith("2"))
      this.editing += "2";
    Tutorial tutorial1 = Tutorial.FromCodeOnly(this.codeEditor.Text);
    tutorial1.debugLog = true;
    tutorial1.ClickSandbox(true, ChooseJsonDialog.Viewing.Custom);
    Client.allowtutorialDebugging = true;
  }

  public void ClickAPI()
  {
    Global.OpenURL("https://pur3extreme.github.io/ArcTutorialDocumentation.html");
  }

  public void ClickOutfit()
  {
    ChangeOutfitMenu.Create(false, onEnd: (Action<SettingsPlayer>) (set =>
    {
      StringBuilder stringBuilder = new StringBuilder("sum.outfit = {");
      for (int o = 0; o < 9; ++o)
      {
        if (o > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(set.GetOutfitIndex((Outfit) o));
      }
      stringBuilder.Append("} \nsum.colors = {");
      for (int i = 0; i < 4; ++i)
      {
        if (i > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append("\"#").Append(ColorUtility.ToHtmlStringRGB((Color) set.coloring.colors[0].FromIndex(i))).Append("\"");
      }
      stringBuilder.Append("}");
      Global.systemCopyBuffer = stringBuilder.ToString();
    }));
  }

  public void ClickSpells()
  {
    SpellLobbyChange.Create(onEnd: (Action<SettingsPlayer>) (set =>
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
      if (set.Elemental != BookOf.Nothing)
        stringBuilder.Append("\n sum.elemental = BookOf.").Append((object) set.Elemental);
      Global.systemCopyBuffer = stringBuilder.ToString();
    }), center: true);
  }

  public void ClickPoint() => this.panelPoint.SetActive(!this.panelPoint.activeSelf);

  public void Editing(string s)
  {
    PlayerPrefs.SetString(TutorialCodeEditor.prefOpened, s);
    this.editing = s;
    this.txt_editing.text = s.Substring(0, s.Length - Path.GetExtension(s).Length);
  }

  public void CheckSave()
  {
    if (!this.edited)
      return;
    if (string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary.arcTutorial2");
    if (!this.editing.EndsWith("2"))
      this.editing += 2.ToString();
    this.Editing(this.editing);
    Global.SaveTutorialCode(this.editing, this.codeEditor.Text, false);
  }

  public void ClickOpenExternalEditor()
  {
    if (string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary.arcTutorial2");
    if (!this.editing.EndsWith("2"))
      this.editing += 2.ToString();
    this.Editing(this.editing);
    Global.SaveTutorialCode(this.editing, this.codeEditor.Text);
  }

  public void Edited()
  {
    if (!this.edited && string.IsNullOrEmpty(this.editing))
      this.Editing("_Temporary.arcTutorial2");
    this.edited = true;
  }

  public void FindMapImage()
  {
    string mapName = this.MapName;
    for (int index = 0; index < this.mapDropdown.options.Count; ++index)
    {
      if (string.Equals(mapName, this.mapDropdown.options[index].text))
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
}
