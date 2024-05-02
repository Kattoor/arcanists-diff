// Decompiled with JetBrains decompiler
// Type: ColorSchemeUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ColorSchemeUI : MonoBehaviour
{
  public PoolColoredImage pool;
  public Texture2D baseTexture;
  internal ColorScheme scheme;
  private ColorScheme selectedScheme;
  public GameObject pfabColor;
  public RectTransform containerColors;
  public GameObject pfabText;
  public RectTransform containerText;
  public ColorPicker colorPicker;
  public GameObject butFileLocation;
  public GameObject panelColors;
  public GameObject panelText;
  public GameObject panelLoad;
  public GameObject panelSave;
  public InputFieldPlus input;
  public TMP_Text txtLoad;
  public TMP_Text txtSave;
  public UIOnHover butSelectScheme;
  public TMP_Dropdown dropGraphics;
  public TMP_Dropdown dropFont;
  public GameObject buttonShare;
  [Header("Examples")]
  public RectTransform contextMenuPanel;
  public ChatBox chatbox;
  public GameObject buttonDefaultColor;
  public GameObject buttonStartingColor;
  public Image imgBackgroundColor;
  public Image imgHighlightColor;
  public Image imgBG;
  public Image imgBG2;
  public Image imgBG3;
  private Color32 defaultColor = (Color32) Color.black;
  private Color32 startingColor = (Color32) Color.black;
  private Dictionary<Color32, List<TMP_Text>> dText = new Dictionary<Color32, List<TMP_Text>>();
  private Dictionary<Color32, List<UIOnHoverChild>> dColorNormal = new Dictionary<Color32, List<UIOnHoverChild>>();
  private Dictionary<Color32, List<UIOnHoverChild>> dColorHighlighted = new Dictionary<Color32, List<UIOnHoverChild>>();
  private Dictionary<Color32, List<UIOnHoverChild>> dColorPressed = new Dictionary<Color32, List<UIOnHoverChild>>();
  private Dictionary<Color32, List<UIOnHoverChild>> dColorDisabled = new Dictionary<Color32, List<UIOnHoverChild>>();
  private Dictionary<Color32, List<RectTransform>> highlightRects = new Dictionary<Color32, List<RectTransform>>();
  private Dictionary<Material, List<RectTransform>> highlightMaterials = new Dictionary<Material, List<RectTransform>>();
  public static bool ForceApply;
  [Header("Load/Save")]
  public RectTransform containerFolder;
  public GameObject pfabFolder;
  public GameObject pfabBook;
  private GameObject selectedSchemeObj;
  private string subDir = "";
  private string selectedFilePath = "";

  public static ColorSchemeUI Instance { get; private set; }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) ColorSchemeUI.Instance == (UnityEngine.Object) this)
      ColorSchemeUI.Instance = (ColorSchemeUI) null;
    ColorSchemeUI.DisableHighlight();
  }

  public static void Create(ColorScheme s)
  {
    GameObject g = UnityEngine.Object.Instantiate<GameObject>(Controller.Instance.MenuColorScheme, Controller.Instance.transform);
    Controller.Instance.Push(g);
    g.GetComponent<ColorSchemeUI>().Init(s);
  }

  public void Init(ColorScheme s)
  {
    ColorSchemeUI.Instance = this;
    this.scheme = s.Copy();
    this.scheme.InitTextHash();
    MyContextMenu.BuildColorScheme((Transform) this.contextMenuPanel).GetComponent<Button>().enabled = false;
    this.chatbox.NewChatMsg("Mini-game chat color", Global.ColorMiniGameText);
    this.chatbox.NewChatMsg("Team Chat", Global.ColorTeamText);
    this.chatbox.NewChatMsg("Announcements", Global.ColorAnnoucement);
    this.chatbox.NewChatMsg("System chat color", Global.ColorSystem);
    this.chatbox.NewChatMsg("Notification chat color", Global.ColorNotification);
    this.chatbox.NewChatMsg("Lobby chat color", Global.ColorLobbyText);
    this.chatbox.NewChatMsg("Game chat color", Global.ColorGameText);
    this.chatbox.NewChatMsg("Gray chat color", Global.ColorGrayChat);
    this.chatbox.NewChatMsg("Clan chat color", Global.ColorClanText);
    this.chatbox.NewChatMsg("Private received chat color", Global.ColorReceivedPrivate);
    this.chatbox.NewChatMsg("Private sent chat color", Global.ColorSentPrivate);
    Color32 color32;
    foreach (TMP_Text componentsInChild in this.GetComponentsInChildren<TMP_Text>())
    {
      TMP_Text x = componentsInChild;
      if (this.scheme.textSet.TryGetValue(x.color.To32(), out color32))
      {
        List<TMP_Text> tmpTextList = (List<TMP_Text>) null;
        if (!this.dText.TryGetValue(x.color.To32(), out tmpTextList))
          tmpTextList = new List<TMP_Text>();
        tmpTextList.Add(x);
        this.dText[x.color.To32()] = tmpTextList;
        this.AddHighlightToList(x.color.To32(), x.rectTransform);
        int index = this.scheme._materials.FindIndex((Predicate<ColorMaterial>) (mat => (UnityEngine.Object) mat._material == (UnityEngine.Object) x.fontSharedMaterial));
        if (index >= 0)
        {
          List<RectTransform> rectTransformList;
          if (!this.highlightMaterials.TryGetValue(this.scheme._materials[index]._material, out rectTransformList))
            rectTransformList = new List<RectTransform>();
          rectTransformList.Add(x.rectTransform);
          this.highlightMaterials[this.scheme._materials[index]._material] = rectTransformList;
        }
      }
    }
    foreach (UIOnHoverChild componentsInChild in this.GetComponentsInChildren<UIOnHoverChild>())
    {
      if (componentsInChild.useTMPTextColor)
      {
        if (this.scheme.textSet.TryGetValue(componentsInChild.textNormalColor.To32(), out color32))
          this.Add(componentsInChild.textNormalColor.To32(), componentsInChild, this.dColorNormal);
        if (this.scheme.textSet.TryGetValue(componentsInChild.textHighlightedColor.To32(), out color32))
          this.Add(componentsInChild.textHighlightedColor.To32(), componentsInChild, this.dColorHighlighted);
        if (this.scheme.textSet.TryGetValue(componentsInChild.textPressedColor.To32(), out color32))
          this.Add(componentsInChild.textPressedColor.To32(), componentsInChild, this.dColorPressed);
        if (this.scheme.textSet.TryGetValue(componentsInChild.textDisabledColor.To32(), out color32))
          this.Add(componentsInChild.textDisabledColor.To32(), componentsInChild, this.dColorDisabled);
      }
    }
    this.Setup();
    if (!Directory.Exists(ColorScheme.defaultPath))
      Directory.CreateDirectory(ColorScheme.defaultPath);
    GameObject butFileLocation = this.butFileLocation;
    if (butFileLocation != null)
      butFileLocation.HideIfWebGL();
    this.dropGraphics.SetValueWithoutNotify(Mathf.Clamp(PlayerPrefs.GetInt("prefforcedgraphics", 0), 0, this.dropGraphics.options.Count - 1));
    this.dropGraphics.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      ColorScheme.forcedGraphics = (ColorScheme.ForcedGraphics) v;
      PlayerPrefs.SetInt("prefforcedgraphics", v);
      ColorScheme.Resolution(this.gameObject, true);
    }));
    this.dropFont.SetValueWithoutNotify(Mathf.Clamp((int) s.fontIndex, 0, this.dropFont.options.Count - 1));
    this.dropFont.onValueChanged.AddListener((UnityAction<int>) (v =>
    {
      if ((int) this.scheme.fontIndex == v)
        return;
      TMP_FontAsset font1 = Controller.GetFont((int) this.scheme.fontIndex);
      TMP_FontAsset font2 = Controller.GetFont(v);
      this.scheme.fontIndex = (byte) v;
      foreach (TextMeshProUGUI componentsInChild in this.GetComponentsInChildren<TextMeshProUGUI>())
      {
        if ((UnityEngine.Object) componentsInChild.font == (UnityEngine.Object) font1)
          componentsInChild.font = font2;
      }
    }));
    this.buttonShare.SetActive((UnityEngine.Object) MainMenu.Instance == (UnityEngine.Object) null);
    this.scheme.SetColors();
    this.scheme.InitTextHash();
    this.scheme.Apply(this.gameObject);
  }

  private void Start()
  {
    UIOnHoverChild component = this.butSelectScheme.transform.GetChild(0).GetComponent<UIOnHoverChild>();
    component.useTMPTextColor = true;
    component.textDisabledColor = Color.black;
  }

  public void Export()
  {
    File.WriteAllBytes("testPalette.png", ((Texture2D) this.scheme._material.GetTexture("_PaletteTex")).EncodeToPNG());
  }

  public void ClickShare()
  {
    Client.sharingWith = "";
    Client.AskToShare(this.scheme.name, ContentType.ColorScheme, (object) this.scheme);
    if ((UnityEngine.Object) OptionsMenu.Instance != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) OptionsMenu.Instance.gameObject);
    this.ClickCancel();
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.F1))
      return;
    this.Export();
  }

  private void Add(
    Color32 c,
    UIOnHoverChild x,
    Dictionary<Color32, List<UIOnHoverChild>> dictonary)
  {
    List<UIOnHoverChild> uiOnHoverChildList = (List<UIOnHoverChild>) null;
    if (!dictonary.TryGetValue(c, out uiOnHoverChildList))
      uiOnHoverChildList = new List<UIOnHoverChild>();
    if (uiOnHoverChildList.Contains(x))
      return;
    uiOnHoverChildList.Add(x);
    dictonary[c] = uiOnHoverChildList;
    this.AddHighlightToList(c, (RectTransform) x.transform);
  }

  private void AddHighlightToList(Color32 c, RectTransform t)
  {
    List<RectTransform> rectTransformList = (List<RectTransform>) null;
    if (!this.highlightRects.TryGetValue(c, out rectTransformList))
      rectTransformList = new List<RectTransform>();
    if (rectTransformList.Contains(t))
      return;
    rectTransformList.Add(t);
    this.highlightRects[c] = rectTransformList;
  }

  public void HighLight(int saturation)
  {
    ClientResources.Instance._matColorPalette.SetFloat("_HighLight", 1f);
    ClientResources.Instance._matColorPalette.SetFloat("_Saturation", (float) saturation / (float) byte.MaxValue);
    ClientResources.Instance._matColorPalette.SetColor("_SatColor", this.scheme.highlightColor);
  }

  public void HighLight(Material c)
  {
    ColorSchemeUI.DisableHighlight();
    this.pool.DeSpawn();
    List<RectTransform> rectTransformList = (List<RectTransform>) null;
    if (!this.highlightMaterials.TryGetValue(c, out rectTransformList))
      return;
    foreach (RectTransform rt in rectTransformList)
    {
      if ((UnityEngine.Object) rt != (UnityEngine.Object) null)
        this.HighLight(rt);
    }
  }

  public void HighLight(Color32 c)
  {
    ColorSchemeUI.DisableHighlight();
    this.pool.DeSpawn();
    List<RectTransform> rectTransformList = (List<RectTransform>) null;
    if (!this.highlightRects.TryGetValue(c, out rectTransformList))
      return;
    foreach (RectTransform rt in rectTransformList)
    {
      if ((UnityEngine.Object) rt != (UnityEngine.Object) null)
        this.HighLight(rt);
    }
  }

  public void HighLight(RectTransform rt)
  {
    Vector3[] fourCornersArray = new Vector3[4];
    rt.GetWorldCorners(fourCornersArray);
    for (int index = 0; index < 4; ++index)
      fourCornersArray[index] = this.transform.InverseTransformPoint(fourCornersArray[index]);
    this.pool.SpawnColorScheme(new Vector3(Mathf.Lerp(fourCornersArray[0].x, fourCornersArray[2].x, 0.5f), Mathf.Lerp(fourCornersArray[0].y, fourCornersArray[2].y, 0.5f)), new Vector2((float) ((double) fourCornersArray[2].x - (double) fourCornersArray[0].x + 20.0), (float) ((double) fourCornersArray[2].y - (double) fourCornersArray[0].y + 20.0)), this.scheme.highlightColor);
  }

  public static void DisableHighlight()
  {
    ClientResources.Instance._matColorPalette.SetFloat("_HighLight", 0.0f);
  }

  public void Unhighlight()
  {
    ColorSchemeUI.DisableHighlight();
    this.pool.DeSpawn();
  }

  public void ClickStartingColor()
  {
    this.buttonStartingColor.SetActive(false);
    this.colorPicker.CurrentColor = (Color) this.startingColor;
  }

  public void ClickDefaultColor() => this.colorPicker.CurrentColor = (Color) this.defaultColor;

  public void ClickBackgroundColor()
  {
    this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
    this.startingColor = (Color32) this.scheme.bgColor;
    this.defaultColor = (Color32) Color.black;
    this.buttonStartingColor.SetActive(false);
    this.colorPicker.CurrentColor = this.scheme.bgColor;
    this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
    {
      this.imgBackgroundColor.color = c;
      this.scheme.bgColor = c;
      this.buttonStartingColor.SetActive(true);
      this.imgBG.color = c;
    }));
  }

  public void ClickBackgroundColor2()
  {
    this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
    this.startingColor = (Color32) this.scheme.backgroundColor;
    this.defaultColor = (Color32) Color.white;
    this.buttonStartingColor.SetActive(false);
    this.colorPicker.CurrentColor = this.scheme.backgroundColor;
    this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
    {
      this.scheme.backgroundColor = c;
      this.buttonStartingColor.SetActive(true);
      this.imgBG2.color = c;
      this.imgBG3.color = c;
    }));
  }

  public void ClickHighLightColor()
  {
    this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
    this.startingColor = (Color32) this.scheme.highlightColor;
    this.defaultColor = new Color32(byte.MaxValue, (byte) 212, (byte) 0, byte.MaxValue);
    this.buttonStartingColor.SetActive(false);
    this.colorPicker.CurrentColor = this.scheme.highlightColor;
    this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
    {
      this.imgHighlightColor.color = c;
      this.scheme.highlightColor = c;
      this.buttonStartingColor.SetActive(true);
    }));
  }

  public void Setup()
  {
    float y1 = -3f;
    float num1 = ((RectTransform) this.pfabColor.transform).sizeDelta.y + 2f;
    this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
    this.buttonStartingColor.SetActive(false);
    this.containerColors.DestroyChildern();
    this.containerText.DestroyChildern();
    this.containerColors.sizeDelta = new Vector2(this.containerColors.sizeDelta.x, (float) ((double) num1 * (double) this.scheme.colors.Count + 6.0));
    this.containerText.sizeDelta = new Vector2(this.containerText.sizeDelta.x, (float) ((double) num1 * (double) (this.scheme.text.Count + this.scheme._materials.Count) + 6.0));
    int num2 = 0;
    foreach (ColorValue color in this.scheme.colors)
    {
      int e = num2;
      ColorValue z = color;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabColor, (Transform) this.containerColors);
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2(3f, y1);
      Image img = gameObject.transform.GetChild(0).GetComponent<Image>();
      img.color = (Color) z.color;
      gameObject.SetActive(true);
      UIOnHover component = gameObject.GetComponent<UIOnHover>();
      component.onEnter.AddListener((UnityAction) (() => this.HighLight(z.saturation)));
      component.onExit.AddListener((UnityAction) (() => this.Unhighlight()));
      component.onClick.AddListener((UnityAction) (() =>
      {
        this.defaultColor = Client.defaultScheme.colors[e].color;
        this.startingColor = z.color;
        this.buttonStartingColor.SetActive(false);
        this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
        this.colorPicker.CurrentColorNoNotify = img.color;
        this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
        {
          img.color = c;
          z.color = (Color32) c;
          this.scheme.SetColors();
          this.buttonStartingColor.SetActive(true);
        }));
      }));
      ++num2;
      y1 -= num1;
    }
    float y2 = -3f;
    int num3 = 0;
    foreach (ColorText colorText in this.scheme.text)
    {
      ColorText z = colorText;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabColor, (Transform) this.containerText);
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2(3f, y2);
      Image img = gameObject.transform.GetChild(0).GetComponent<Image>();
      img.color = (Color) z.color;
      gameObject.SetActive(true);
      UIOnHover component = gameObject.GetComponent<UIOnHover>();
      component.onEnter.AddListener((UnityAction) (() => this.HighLight(z._origionalColor)));
      component.onExit.AddListener((UnityAction) (() => this.Unhighlight()));
      component.onClick.AddListener((UnityAction) (() =>
      {
        this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
        this.colorPicker.CurrentColorNoNotify = img.color;
        this.buttonStartingColor.SetActive(false);
        this.startingColor = z.color;
        this.defaultColor = z._origionalColor;
        this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
        {
          this.buttonStartingColor.SetActive(true);
          img.color = c;
          z.color = (Color32) c;
          this.scheme.SetColors();
          List<UIOnHoverChild> uiOnHoverChildList;
          if (this.dColorNormal.TryGetValue(z._origionalColor, out uiOnHoverChildList))
          {
            foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
            {
              uiOnHoverChild.GetText.color = c;
              uiOnHoverChild.textNormalColor = c;
            }
          }
          if (this.dColorHighlighted.TryGetValue(z._origionalColor, out uiOnHoverChildList))
          {
            foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
              uiOnHoverChild.textHighlightedColor = c;
          }
          if (this.dColorPressed.TryGetValue(z._origionalColor, out uiOnHoverChildList))
          {
            foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
              uiOnHoverChild.textPressedColor = c;
          }
          if (this.dColorDisabled.TryGetValue(z._origionalColor, out uiOnHoverChildList))
          {
            foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
              uiOnHoverChild.textDisabledColor = c;
          }
          List<TMP_Text> tmpTextList;
          if (!this.dText.TryGetValue(z._origionalColor, out tmpTextList))
            return;
          foreach (Graphic graphic in tmpTextList)
            graphic.color = c;
        }));
      }));
      ++num3;
      y2 -= num1;
    }
    foreach (ColorMaterial material in this.scheme._materials)
    {
      ColorMaterial zz = material;
      ColorMaterial z = zz;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabColor, (Transform) this.containerText);
      ((RectTransform) gameObject.transform).anchoredPosition = new Vector2(3f, y2);
      Image img = gameObject.transform.GetChild(0).GetComponent<Image>();
      img.color = (Color) z.color;
      gameObject.SetActive(true);
      UIOnHover component = gameObject.GetComponent<UIOnHover>();
      component.onEnter.AddListener((UnityAction) (() => this.HighLight(zz._material)));
      component.onExit.AddListener((UnityAction) (() => this.Unhighlight()));
      component.onClick.AddListener((UnityAction) (() =>
      {
        this.colorPicker.onValueChangedPUBLIC.RemoveAllListeners();
        this.colorPicker.CurrentColorNoNotify = img.color;
        this.colorPicker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (c =>
        {
          img.color = c;
          z.color = (Color32) c;
          z.Apply();
        }));
      }));
      ++num3;
      y2 -= num1;
    }
    this.imgHighlightColor.color = this.scheme.highlightColor;
    this.imgBackgroundColor.color = this.scheme.bgColor;
    this.imgBG.color = this.scheme.bgColor;
    this.imgBG2.color = this.scheme.backgroundColor;
    this.imgBG3.color = this.scheme.backgroundColor;
  }

  private void Apply(ColorScheme x)
  {
    foreach (ColorText colorText in x.text)
    {
      List<UIOnHoverChild> uiOnHoverChildList;
      if (this.dColorNormal.TryGetValue(colorText._origionalColor, out uiOnHoverChildList))
      {
        foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
        {
          uiOnHoverChild.GetText.color = (Color) colorText.color;
          uiOnHoverChild.textNormalColor = (Color) colorText.color;
        }
      }
      if (this.dColorHighlighted.TryGetValue(colorText._origionalColor, out uiOnHoverChildList))
      {
        foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
          uiOnHoverChild.textHighlightedColor = (Color) colorText.color;
      }
      if (this.dColorPressed.TryGetValue(colorText._origionalColor, out uiOnHoverChildList))
      {
        foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
          uiOnHoverChild.textPressedColor = (Color) colorText.color;
      }
      if (this.dColorDisabled.TryGetValue(colorText._origionalColor, out uiOnHoverChildList))
      {
        foreach (UIOnHoverChild uiOnHoverChild in uiOnHoverChildList)
          uiOnHoverChild.textDisabledColor = (Color) colorText.color;
      }
      List<TMP_Text> tmpTextList;
      if (this.dText.TryGetValue(colorText._origionalColor, out tmpTextList))
      {
        foreach (Graphic graphic in tmpTextList)
          graphic.color = (Color) colorText.color;
      }
    }
  }

  public void ApplyColors() => this.scheme.SetColors();

  public void ClickOk()
  {
    this.scheme.SetColors();
    Client.colorScheme = this.scheme;
    this.scheme.Save((string) null);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    if ((UnityEngine.Object) OptionsMenu.Instance != (UnityEngine.Object) null)
    {
      ColorSchemeUI.ForceApply = true;
      UnityEngine.Object.Destroy((UnityEngine.Object) OptionsMenu.Instance.gameObject);
      Controller.ShowSettingsMenu(true);
    }
    else
      Controller.Instance.ReopenMenu();
  }

  public void ClickCancel()
  {
    Client.colorScheme.SetColors();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickLoad()
  {
    if (!this.panelSave.activeSelf && this.panelLoad.activeSelf)
    {
      this.scheme.SetColors();
      this.Apply(this.scheme);
      this.Unhide();
    }
    else
    {
      this.panelColors.SetActive(false);
      this.panelText.SetActive(false);
      this.panelLoad.SetActive(true);
      this.panelSave.SetActive(false);
      this.Load(this.subDir);
      this.txtLoad.text = "Cancel";
      this.txtSave.text = "Save";
    }
  }

  public void ClickSave()
  {
    if (this.panelSave.activeSelf)
    {
      this.Unhide();
    }
    else
    {
      this.panelColors.SetActive(false);
      this.panelText.SetActive(false);
      this.panelLoad.SetActive(true);
      this.panelSave.SetActive(true);
      this.input.SetText(this.scheme.name);
      this.Load(this.subDir);
      this.txtLoad.text = "Load";
      this.txtSave.text = "Cancel";
    }
  }

  private void Unhide()
  {
    this.panelColors.SetActive(true);
    this.panelText.SetActive(true);
    this.panelLoad.SetActive(false);
    this.panelSave.SetActive(false);
    this.txtLoad.text = "Load";
    this.txtSave.text = "Save";
  }

  public void ClickFileLocation() => Global.OpenFileLocation("Schemes");

  public void ConfirmSave()
  {
    if (string.IsNullOrWhiteSpace(this.input.inputText.text))
      return;
    this.scheme.name = this.input.inputText.text;
    Global.CheckDirectoryExists(ColorScheme.defaultPath, this.subDir + this.scheme.name);
    this.scheme.Save(this.subDir + this.scheme.name);
    this.Unhide();
  }

  private void Load(string subFolder = "")
  {
    this.butSelectScheme.Interactable(false);
    this.selectedSchemeObj = (GameObject) null;
    this.selectedScheme = (ColorScheme) null;
    this.subDir = subFolder;
    for (int index = this.containerFolder.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.containerFolder.GetChild(index).gameObject);
    string defaultPath = ColorScheme.defaultPath;
    int length = defaultPath.Length;
    if (!Directory.Exists(defaultPath))
      Directory.CreateDirectory(defaultPath);
    if (!string.IsNullOrEmpty(subFolder))
    {
      defaultPath += subFolder;
      if (!Directory.Exists(defaultPath))
        defaultPath = ColorScheme.defaultPath;
    }
    int num1 = 0;
    if (defaultPath.Length > length)
    {
      num1 = 64;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
      RectTransform transform = (RectTransform) gameObject1.transform;
      transform.anchoredPosition = (Vector2) new Vector3(5f, 0.0f);
      transform.localScale = new Vector3(1f, 1f, 1f);
      transform.GetComponent<TMP_Text>().text = "Top Folder";
      transform.name = "";
      UIOnHover component1 = gameObject1.GetComponent<UIOnHover>();
      component1.textNormalColor = Color.green;
      gameObject1.GetComponent<TMP_Text>().color = Color.green;
      component1.onClick.AddListener((UnityAction) (() => this.Load()));
      gameObject1.SetActive(true);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
      RectTransform rect = (RectTransform) gameObject2.transform;
      rect.anchoredPosition = (Vector2) new Vector3(0.0f, -32f);
      rect.localScale = new Vector3(1f, 1f, 1f);
      rect.GetComponent<TMP_Text>().text = "Up One Folder";
      string fullName = Directory.GetParent(Directory.GetParent(defaultPath).FullName).FullName;
      rect.name = fullName.Length > length ? fullName.Substring(length) + Path.DirectorySeparatorChar.ToString() : "";
      UIOnHover component2 = gameObject2.GetComponent<UIOnHover>();
      component2.textNormalColor = Color.green;
      gameObject2.GetComponent<TMP_Text>().color = Color.green;
      component2.onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
      gameObject2.SetActive(true);
    }
    string[] directories = Directory.GetDirectories(defaultPath);
    for (int index = 0; index < directories.Length; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabFolder, (Transform) this.containerFolder);
      RectTransform rect = (RectTransform) gameObject.transform;
      rect.anchoredPosition = (Vector2) new Vector3(0.0f, (float) (index * -32 - num1));
      rect.localScale = new Vector3(1f, 1f, 1f);
      rect.GetComponent<TMP_Text>().text = directories[index].Substring(defaultPath.Length);
      rect.name = directories[index].Substring(length) + Path.DirectorySeparatorChar.ToString();
      gameObject.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() => this.Load(rect.name)));
      gameObject.SetActive(true);
    }
    int num2 = num1 + directories.Length * 32;
    List<string> list = ((IEnumerable<string>) Directory.GetFiles(defaultPath, "*" + ColorScheme.ext)).OrderBy<string, string>((Func<string, string>) (f => f)).ToList<string>();
    float num3 = -2f;
    foreach (string str in list)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(this.pfabBook, (Transform) this.containerFolder);
      RectTransform transform = (RectTransform) g.transform;
      transform.localScale = new Vector3(1f, 1f, 1f);
      transform.anchoredPosition = new Vector2(53f, num3 - (float) num2);
      string s = str;
      g.name = str;
      g.GetComponent<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(g.name);
      g.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
      {
        if ((UnityEngine.Object) this.selectedSchemeObj == (UnityEngine.Object) g)
        {
          this.SelectScheme();
        }
        else
        {
          this.selectedSchemeObj = g;
          this.PreviewBook(s);
        }
      }));
      g.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(s, g)));
      num3 -= (float) ((double) transform.sizeDelta.y * (double) transform.localScale.y - 2.0);
      g.SetActive(true);
    }
    this.containerFolder.sizeDelta = new Vector2(this.containerFolder.sizeDelta.x, (float) (num2 + list.Count * 32));
  }

  public void ClickDefault()
  {
    ColorScheme colorScheme = Client.defaultScheme.Copy();
    colorScheme.SetColors();
    this.scheme = colorScheme;
    this.Apply(this.scheme);
    this.Setup();
    this.Unhide();
  }

  public void SelectScheme()
  {
    if (this.selectedScheme == null)
      return;
    this.selectedScheme.SetColors();
    this.scheme = this.selectedScheme;
    this.Apply(this.scheme);
    this.selectedScheme = (ColorScheme) null;
    this.Setup();
    this.Unhide();
  }

  private void PreviewBook(string s)
  {
    ColorScheme x = ColorScheme.FromFile(s);
    x.SetColors();
    this.Apply(x);
    this.selectedScheme = x;
    this.butSelectScheme.Interactable(true);
  }

  private void OnRightClick(string s, GameObject gameObject)
  {
    if (!File.Exists(s))
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Rename", (Action) (() => MyContextMenu.Show().AddInput((Action<string>) (ss =>
    {
      if (string.IsNullOrEmpty(ss))
        return;
      string dir = s.Substring(0, s.Length - Path.GetFileName(s).Length) + ss + Path.GetExtension(s);
      File.Move(s, dir);
      this.selectedFilePath = dir;
      gameObject.GetComponent<UIOnHover>().onRightClick.RemoveAllListeners();
      gameObject.name = dir;
      gameObject.GetComponent<TextMeshProUGUI>().text = ss;
      gameObject.GetComponent<UIOnHover>().onRightClick.AddListener((UnityAction) (() => this.OnRightClick(dir, gameObject)));
    }))), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Delete " + Path.GetFileNameWithoutExtension(s), (Action) (() =>
    {
      File.Delete(s);
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
    }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    myContextMenu.Rebuild();
  }
}
