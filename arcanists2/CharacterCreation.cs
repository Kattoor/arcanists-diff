// Decompiled with JetBrains decompiler
// Type: CharacterCreation
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Crosstales.Common.Util;
using Crosstales.FB;
using Hazel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class CharacterCreation : Catalogue
{
  private const int amount = 75;
  public Inert inert;
  public ClientResources clientResources;
  public GameObject buttonShare;
  public TMP_Text txtMainMenu;
  public TMP_Text txtCancel;
  public GameObject pfab_PlayerCharacter;
  public RectTransform parentOutfitBox;
  [NonSerialized]
  public GameObject playerObj;
  [NonSerialized]
  public ConfigurePlayer configPlayer;
  public RectTransform outfitHoverObj;
  public RectTransform selectedOutfit;
  public TMP_Text txtPage;
  public TMP_Text txtCoins;
  public GameObject pfabOutfit;
  public RectTransform container_outfits;
  public GameObject container_creator_controls;
  [NonSerialized]
  public List<OutfitButton> outfitButtons = new List<OutfitButton>();
  public Outfit viewing;
  internal ColorType colorType;
  public UIOnHover[] buttonsOutfit;
  public GameObject[] lockedOutfit;
  public UIOnHover buttonLeft;
  public UIOnHover buttonRight;
  [Header("Color Picker")]
  public GameObject panelAdvanced;
  public ColorPicker picker;
  public CCColorButton[] colorButtons;
  public TMP_Text txtPickerHeader;
  public UIColorBox pickerPresets;
  public UIOnHover[] buttonPresets;
  public Toggle toggleColorAll;
  public Toggle toggleColorSelected;
  public Toggle[] toggleColorOutfit;
  [Header("Basic")]
  public UIColorBox boxPrimary;
  public UIColorBox boxSecondary;
  public UIColorBox boxHair;
  public UIColorBox boxSkin;
  public GameObject panelBasic;
  [Header("Options")]
  public Toggle optionNoTooltips;
  public Toggle optionRevealPrestige;
  public Toggle optionRevealExperience;
  public Toggle optionTournament;
  public Toggle optionRevealAchievements;
  public Toggle optionRevealSeasonal;
  public Toggle optionRevealAccountType;
  public Toggle optionClanOutfit;
  public UIOnHover optionAdvanced;
  public UIOnHover optionBasic;
  [Header("Undo")]
  public UIOnHover buttonUndo;
  public UIOnHover buttonRedo;
  public TMP_Text txtUndo;
  public TMP_Text txtRedo;
  private Stack<SettingsPlayer> stackUndo = new Stack<SettingsPlayer>();
  private SettingsPlayer stackCurrent;
  private Stack<SettingsPlayer> stackRedo = new Stack<SettingsPlayer>();
  private int _curOffset;
  private Sprite[] curGroup;
  private UIOnHover activeButtonOutfit;
  private UIOnHover activeButtonPreset;
  private ZPerson person = new ZPerson();
  private ZCreature creature;
  private HashSet<Outfit> recoloring = new HashSet<Outfit>();
  private bool changeAllColors = true;
  private List<List<int>> sortedGroups = new List<List<int>>();
  private List<int> filteredGroup = new List<int>();
  internal SettingsPlayer settingsPlayer = new SettingsPlayer();
  private Action<SettingsPlayer> onEnd;
  private KeySequence controls = new KeySequence(new KeyCode[10]
  {
    KeyCode.UpArrow,
    KeyCode.UpArrow,
    KeyCode.DownArrow,
    KeyCode.DownArrow,
    KeyCode.LeftArrow,
    KeyCode.RightArrow,
    KeyCode.LeftArrow,
    KeyCode.RightArrow,
    KeyCode.B,
    KeyCode.A
  });
  private int lastBody = -1;
  private const float defHeight = 105f;
  private float click_counter;
  private int curPage;

  public static CharacterCreation Instance { get; private set; }

  private List<int> curSorted => this.filteredGroup;

  private void Awake()
  {
    Inert.Init(this.inert);
    this.settingsPlayer.CopyOutfit(Client.settingsPlayer);
    ClientResources.Init(this.clientResources);
    this.CreatedSortedGroups();
    this.container_creator_controls.SetActive(Global.GetPrefBool("prefassetcreator", false));
    if (Global.OS.Is(OperatingSystem.Standalone) && OneTimePopups.Set(OneTimePopups.Tip.ClickPreviewExportPNG))
      MyPopup.Show("You can click the preview image to export it as a PNG", new Vector2(163f, -342f));
    CharacterCreation.Instance = this;
    this.buttonShare.SetActive(false);
    this.txtCoins.text = Client.MyAccount.tournamentCoins.ToString();
    this.lockedOutfit[0].SetActive(!Client.MyAccount.accountType.has(AccountType.Donator));
    this.lockedOutfit[1].SetActive(!Client.MyAccount.accountType.has(AccountType.Arch_Donator | AccountType.Lifetime));
  }

  public static void Create(SettingsPlayer sp = null, Action<SettingsPlayer> onEnd = null)
  {
    if ((UnityEngine.Object) CharacterCreation.Instance != (UnityEngine.Object) null)
      return;
    CharacterCreation.Instance = Controller.Instance.CreateAndApply(Controller.Instance.MenuCharacterCreation, Controller.Instance.transform).GetComponent<CharacterCreation>();
    Controller.Instance.Push(CharacterCreation.Instance.gameObject);
    if (sp != null)
      CharacterCreation.Instance.settingsPlayer.CopyOutfit(sp);
    CharacterCreation.Instance.onEnd = onEnd;
    CharacterCreation.Instance.txtMainMenu.text = "Ok";
    CharacterCreation.Instance.txtCancel.text = "Cancel";
    CharacterCreation.Instance.buttonShare.SetActive((UnityEngine.Object) LobbyMenu.instance != (UnityEngine.Object) null || (UnityEngine.Object) UnratedMenu.instance != (UnityEngine.Object) null || (UnityEngine.Object) RatedMenu.instance != (UnityEngine.Object) null && !string.IsNullOrEmpty(Client.sharingWith));
  }

  public void resetanim() => this.creature?.animator.Play(AnimateState.Stop);

  public void SavePNG2(string n, bool url = false)
  {
    ZCreature creature = this.creature;
    Texture2D texture2D1 = new Texture2D(128, 128);
    int num1 = texture2D1.width / 2;
    int num2 = texture2D1.height / 4;
    Color32[] pixels32_1 = texture2D1.GetPixels32();
    for (int index = 0; index < pixels32_1.Length; ++index)
      pixels32_1[index] = (Color32) Color.clear;
    texture2D1.SetPixels32(pixels32_1);
    List<SpriteRenderer> list = ((IEnumerable<SpriteRenderer>) creature.gameObject.GetComponentsInChildren<SpriteRenderer>()).ToList<SpriteRenderer>();
    list.Sort((Comparison<SpriteRenderer>) ((a, b) => a.sortingOrder - b.sortingOrder));
    foreach (SpriteRenderer spriteRenderer in list)
    {
      Texture2D t = texture2D1;
      SpriteRenderer s = spriteRenderer;
      int num3 = num1;
      Rect rect = spriteRenderer.sprite.rect;
      int num4 = (int) ((double) rect.width / 2.0 - (double) spriteRenderer.sprite.pivot.x);
      int centerX = num3 + num4;
      int num5 = num2;
      rect = spriteRenderer.sprite.rect;
      int num6 = (int) ((double) rect.height / 2.0 - (double) spriteRenderer.sprite.pivot.y);
      int num7 = num5 + num6;
      rect = spriteRenderer.sprite.rect;
      int num8 = (64 - (int) rect.height) / 2;
      int centerY = num7 + num8;
      Vector3 position1 = creature.transform.position;
      Vector3 position2 = spriteRenderer.transform.position;
      Transform transform = spriteRenderer.transform;
      this.paint(t, s, centerX, centerY, position1, position2, transform);
    }
    Rect rect1 = new Rect();
    Color32[] pixels32_2 = texture2D1.GetPixels32();
    bool flag1 = false;
    for (int index1 = 0; index1 < texture2D1.width; ++index1)
    {
      for (int index2 = 0; index2 < texture2D1.height; ++index2)
      {
        if (pixels32_2[index1 + index2 * texture2D1.width].a != (byte) 0)
        {
          rect1.x = (float) index1;
          flag1 = true;
          break;
        }
      }
      if (flag1)
        break;
    }
    bool flag2 = false;
    for (int index3 = texture2D1.width - 1; index3 >= 0; --index3)
    {
      for (int index4 = 0; index4 < texture2D1.height; ++index4)
      {
        if (pixels32_2[index3 + index4 * texture2D1.width].a != (byte) 0)
        {
          rect1.width = (float) index3 - rect1.x;
          flag2 = true;
          break;
        }
      }
      if (flag2)
        break;
    }
    bool flag3 = false;
    for (int index5 = 0; index5 < texture2D1.height; ++index5)
    {
      for (int index6 = texture2D1.width - 1; index6 >= 0; --index6)
      {
        if (pixels32_2[index6 + index5 * texture2D1.width].a != (byte) 0)
        {
          rect1.y = (float) index5;
          flag3 = true;
          break;
        }
      }
      if (flag3)
        break;
    }
    bool flag4 = false;
    for (int index7 = texture2D1.height - 1; index7 >= 0; --index7)
    {
      for (int index8 = texture2D1.width - 1; index8 >= 0; --index8)
      {
        if (pixels32_2[index8 + index7 * texture2D1.width].a != (byte) 0)
        {
          rect1.height = (float) index7 - rect1.y;
          flag4 = true;
          break;
        }
      }
      if (flag4)
        break;
    }
    ++rect1.width;
    ++rect1.height;
    Color32[] colors = new Color32[(int) ((double) rect1.width * (double) rect1.height)];
    int index9 = 0;
    for (int y = (int) rect1.y; y < (int) ((double) rect1.y + (double) rect1.height); ++y)
    {
      for (int x = (int) rect1.x; x < (int) ((double) rect1.x + (double) rect1.width); ++x)
      {
        colors[index9] = pixels32_2[x + y * texture2D1.width];
        ++index9;
      }
    }
    Texture2D texture2D2 = new Texture2D((int) rect1.width, (int) rect1.height);
    texture2D2.SetPixels32(colors);
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "Previews" + Path.DirectorySeparatorChar.ToString();
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    if (url)
    {
      Global.OpenURL(Global.systemCopyBuffer = this.ImageToBase64(texture2D2));
    }
    else
    {
      File.WriteAllBytes(path + n + ".png", texture2D2.EncodeToPNG());
      Global.OpenFileLocation("Previews");
    }
  }

  private void paint(
    Texture2D t,
    SpriteRenderer s,
    int centerX,
    int centerY,
    Vector3 parent,
    Vector3 me,
    Transform child)
  {
    if (!s.gameObject.activeSelf || !s.enabled || !s.sprite.texture.isReadable)
      return;
    Texture2D texture = s.sprite.texture;
    int width1 = (int) s.sprite.rect.width;
    Rect rect = s.sprite.rect;
    int height1 = (int) rect.height;
    Color[] pixels = texture.GetPixels(0, 0, width1, height1);
    Color32[] color32Array = new Color32[pixels.Length];
    for (int index = 0; index < color32Array.Length; ++index)
      color32Array[index] = (Color32) pixels[index];
    if (!s.gameObject.CompareTag("RawImage"))
    {
      Recolor.Colors colors = !((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.leftArm.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.rightArm.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.beard.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.beard2.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.beard3.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.head.transform) ? (!((UnityEngine.Object) child.parent == (UnityEngine.Object) this.creature.hat.transform) ? this.settingsPlayer.coloring.colors[0] : this.settingsPlayer.coloring.colors[4]) : this.settingsPlayer.coloring.colors[1]) : this.settingsPlayer.coloring.colors[10]) : this.settingsPlayer.coloring.colors[9]) : this.settingsPlayer.coloring.colors[5]) : this.settingsPlayer.coloring.colors[3]) : this.settingsPlayer.coloring.colors[2];
      Sprite s1 = Recolor.Texture(s.sprite, (Color) colors.red, (Color) colors.green, (Color) colors.blue, (Color) colors.gray);
      Surface dst = new Surface(t);
      Color32[] pixels32 = s1.texture.GetPixels32();
      rect = s.sprite.rect;
      int width2 = (int) rect.width;
      rect = s.sprite.rect;
      int height2 = (int) rect.height;
      Surface src = new Surface(pixels32, width2, height2);
      int x = centerX + (int) (((double) me.x - (double) parent.x + 0.5) * 2.0);
      int y = centerY + (int) (((double) me.y - (double) parent.y + 0.5) * 2.0);
      RotateImage.RenderOverlay(dst, src, x, y, 0.0f, 0, true, false);
      Global.DestroySprite(s1);
      UnityEngine.Object.Destroy((UnityEngine.Object) s1);
    }
    else
    {
      Surface dst = new Surface(t);
      Color32[] c = color32Array;
      rect = s.sprite.rect;
      int width3 = (int) rect.width;
      rect = s.sprite.rect;
      int height3 = (int) rect.height;
      Surface src = new Surface(c, width3, height3);
      int x = centerX + (int) (((double) me.x - (double) parent.x + 0.5) * 2.0);
      int y = centerY + (int) (((double) me.y - (double) parent.y + 0.5) * 2.0);
      RotateImage.RenderOverlay(dst, src, x, y, 0.0f, 0, true, false);
    }
  }

  private void paint(Texture2D t, SpriteRenderer s, Texture2D tex, int centerX, int centerY)
  {
    if (!s.gameObject.activeSelf || !s.enabled)
      return;
    RotateImage.RenderOverlay(new Surface(t), new Surface(tex), centerX + (int) (((double) s.transform.localPosition.x + 0.5) * 2.0), centerY + (int) (((double) s.transform.localPosition.y + 0.5) * 2.0), 0.0f, 0, true, false);
  }

  public string ImageToBase64(Texture2D image)
  {
    return "data:image/png;base64," + Convert.ToBase64String(image.EncodeToPNG());
  }

  public Texture2D Base64ToImage(string base64String)
  {
    byte[] data = Convert.FromBase64String(base64String);
    Texture2D tex = new Texture2D(2, 2);
    tex.LoadImage(data);
    return tex;
  }

  private void Start()
  {
    if (this.onEnd == null)
      AudioManager.PlayMusicCharacterCreation();
    this.playerObj = ZCreatureCreate.CreateCreature((ZPerson) null, this.pfab_PlayerCharacter.GetComponent<Creature>(), (Vector2) Vector3.zero, Quaternion.identity, (Transform) null).gameObject;
    this.configPlayer = this.playerObj.GetComponent<ConfigurePlayer>();
    this.configPlayer._sp = this.settingsPlayer;
    Creature component1 = this.playerObj.GetComponent<Creature>();
    this.creature = component1.serverObj;
    component1.enabled = false;
    component1.bg.gameObject.SetActive(false);
    if ((UnityEngine.Object) component1.overheadCanvas != (UnityEngine.Object) null)
      component1.overheadCanvas.gameObject.SetActive(false);
    CharacterCreation.Instance = this;
    float x = 26f;
    float y = -24.4f;
    for (int i = 0; i < 75; ++i)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pfabOutfit, (Transform) this.container_outfits);
      RectTransform transform = (RectTransform) gameObject.transform;
      OutfitButton component2 = gameObject.GetComponent<OutfitButton>();
      transform.anchoredPosition = new Vector2(x, y);
      component2.SetCallBack(i);
      if (i % 3 == 2)
      {
        x += 33f;
        y = -24.4f;
      }
      else
        y -= 33f;
      component2.gameObject.SetActive(true);
      this.outfitButtons.Add(component2);
    }
    this.settingsPlayer.VerifyOutfit(Client.cosmetics);
    this.outfitHoverObj.SetAsLastSibling();
    this.SelectBody();
    this.LoadAll();
    Client.game = new ZGame();
    Client.game.isClient = true;
    component1.GetComponent<AnimatePlayer>().Play(AnimateState.Walk, float.MaxValue, true);
    this.playerObj.transform.position = Vector3.zero;
    for (int t = 0; t < this.colorButtons.Length; ++t)
      this.colorButtons[t].imgColor.color = (Color) this.settingsPlayer.coloring.Get(this.changeAllColors ? Outfit.None : this.viewing, (ColorType) t);
    this.colorButtons[0].Activate(0);
    this.picker.onValueChangedPUBLIC.AddListener(new UnityAction<Color>(this.OnPickColor));
    this.ClickPresetIndex(PlayerPrefs.GetInt("prefselectedpreset", 0));
    this.boxPrimary.Set(ClientResources.Instance.GetColors(0));
    this.boxSecondary.Set(ClientResources.Instance.GetColors(0));
    this.boxHair.Set(ClientResources.Instance.GetColors(1));
    this.boxSkin.Set(ClientResources.Instance.GetColors(2));
    if (Global.GetPrefBool("ccbasic", true))
      this.ClickBasic();
    else
      this.ClickAdvanced();
    this.optionNoTooltips.SetIsOnWithoutNotify(Global.GetPrefBool("cctooltip", true));
    if (this.settingsPlayer.coloring.HasCustom())
      this.toggleColorSelected.isOn = true;
    DiscordIntergration.Instance?.UpdateActivityCharacterCreation();
  }

  public void ToggleToolTips(bool v) => Global.SetPrefBool("cctooltip", v);

  internal void TestClan()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator("Test Clan outfit");
    myContextMenu.AddInput((Action<string>) (s =>
    {
      Debug.Log((object) (Client.MyAccount.clan + " to " + s));
      Client.GetAccount(Client.Name).clan = s;
      Client.MyAccount.clan = s;
      this.EquipAll();
    }));
  }

  internal void EquipAll()
  {
    this.configPlayer.EquipAll(Client.Name, this.settingsPlayer);
    ClientResources.GetArchMageStaff(this.settingsPlayer, this.creature, this.person, Client.MyAccount);
    if (this.creature.animator.currentState == AnimateState.Walk && (this.lastBody == SettingsPlayer.body_skate && (int) this.settingsPlayer.indexBody != SettingsPlayer.body_skate || this.lastBody != SettingsPlayer.body_skate && (int) this.settingsPlayer.indexBody == SettingsPlayer.body_skate))
    {
      this.creature.animator.Play(AnimateState.Stop);
      this.creature.animator.Play(AnimateState.Walk, float.MaxValue);
    }
    this.lastBody = (int) this.settingsPlayer.indexBody;
  }

  public void ClickRefresh() => this.LoadAll();

  public void ClickShare()
  {
    Client.AskToShare("", ContentType.Outfit, (object) this.settingsPlayer);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickMainMenu()
  {
    if (this.onEnd != null)
    {
      Action<SettingsPlayer> onEnd = this.onEnd;
      if (onEnd != null)
        onEnd(this.settingsPlayer);
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      if (Client.game == null)
        return;
      Client.game.CleanUp();
      Client.game = (ZGame) null;
    }
    else
    {
      Client.settingsPlayer.CopyOutfit(this.settingsPlayer);
      Controller.Instance.OpenMenu(Controller.Instance.MenuMain, false);
    }
  }

  public void ClickSpellSelection()
  {
    if (this.onEnd != null)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      Client.settingsPlayer.CopyOutfit(this.settingsPlayer);
      if ((UnityEngine.Object) Controller.Instance == (UnityEngine.Object) null)
        SceneManager.LoadScene("SpellBook");
      else
        Controller.Instance.OpenMenu(Controller.Instance.MenuSpellSelection, false);
    }
  }

  public void SelectRandomize()
  {
    System.Random r = new System.Random();
    this.configPlayer.settingsPlayer.indexBody = (byte) r.Next(0, ClientResources.Instance._characterBody.Length);
    this.configPlayer.settingsPlayer.indexHead = (byte) r.Next(0, ClientResources.Instance._characterHeads.Length);
    this.configPlayer.settingsPlayer.indexLeftHand = (byte) r.Next(0, ClientResources.Instance._characterLeftHand.Length);
    this.configPlayer.settingsPlayer.indexRightHand = (byte) r.Next(0, ClientResources.Instance._characterRightHand.Length);
    this.configPlayer.settingsPlayer.indexHat = (byte) r.Next(0, ClientResources.Instance._characterHats.Length);
    this.configPlayer.settingsPlayer.indexBeard = (byte) r.Next(0, ClientResources.Instance._characterBeards.Length);
    this.configPlayer.settingsPlayer.indexBeard2 = (byte) r.Next(0, ClientResources.Instance._characterBeards.Length);
    this.configPlayer.settingsPlayer.indexBeard3 = (byte) r.Next(0, ClientResources.Instance._characterBeards.Length);
    this.configPlayer.settingsPlayer.coloring.Set(Outfit.None, ColorType.Red, RandomExtensions.RandomColor(r));
    this.configPlayer.settingsPlayer.coloring.Set(Outfit.None, ColorType.Green, RandomExtensions.RandomColor(r));
    this.configPlayer.settingsPlayer.coloring.Set(Outfit.None, ColorType.Blue, RandomExtensions.RandomColor(r));
    this.configPlayer.settingsPlayer.coloring.Set(Outfit.None, ColorType.Gray, RandomExtensions.RandomColor(r));
    this.configPlayer.settingsPlayer.VerifyOutfit(Client.cosmetics);
    this.UpdateColors();
    this.EquipAll();
    this.Changed();
    this.FindEquipped();
  }

  public void UpdateColors()
  {
    this.picker.CurrentColorNoNotify = (Color) this.settingsPlayer.coloring.Get(this.changeAllColors ? Outfit.None : this.viewing, this.colorType);
    for (int t = 0; t < this.colorButtons.Length; ++t)
      this.colorButtons[t].imgColor.color = (Color) this.settingsPlayer.coloring.Get(this.changeAllColors ? Outfit.None : this.viewing, (ColorType) t);
    if (!this.panelBasic.activeInHierarchy)
      return;
    this.boxPrimary.FindClosest();
    this.boxSecondary.FindClosest();
    this.boxHair.FindClosest();
    this.boxSkin.FindClosest();
  }

  public void OnPickColor(Color c)
  {
    if (this.recoloring.Count > 0)
    {
      foreach (Outfit o in this.recoloring)
        this.settingsPlayer.coloring.Set(o, this.colorType, (Color32) c);
      this.colorButtons[(int) this.colorType].imgColor.color = c;
      this.EquipAll();
      this.StopAllCoroutines();
      this.StartCoroutine(this.WaitForEndOfColor());
    }
    else
    {
      this.settingsPlayer.coloring.Set(this.changeAllColors ? Outfit.None : this.viewing, this.colorType, (Color32) c);
      this.colorButtons[(int) this.colorType].imgColor.color = c;
      this.EquipAll();
      this.StopAllCoroutines();
      this.StartCoroutine(this.WaitForEndOfColor());
    }
  }

  private IEnumerator WaitForEndOfColor()
  {
    while (Input.GetMouseButton(0) || Input.touchCount > 0)
      yield return (object) null;
    this.Changed();
  }

  public void Hover(string s) => MyToolTip.Show(s);

  public void HoverInfo()
  {
    MyToolTip.Show("<b>Asset Creator Info</b>\r\nImport - Select an *.png file which will be used in the currently selected body part slot\r\nExport PKG - Exports a *.outfitPKG file that can be shared\r\nSave Preview - Creates an image from the character preview\r\n\r\n<b>Controls</b>\r\nArrow Keys - Adjust the currently selected custom outfit piece by adjusting its pivot point\r\nHold Ctrl for incremental movement with the arrow keys\r\nF8 - Hide this menu\r\n\r\nClick the preview image to toggle the animation and save the preview (non-asset creators can also do this)\r\n");
  }

  public void LeaveToolTip() => MyToolTip.Close();

  public void CanvasContextMenu()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem("Toggle Walk Animation", new Action(this.ClickToggleWalk), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Save Image", new Action(this.ClickPreview), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.Rebuild();
  }

  public void ExtraContextMenu()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    this.settingsPlayer.MakeSureIntitialized();
    myContextMenu.AddItem("Export as URL (Experimental)", (Action) (() => this.SavePNG2("Hi", true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
    myContextMenu.AddSeperator("-------------");
    bool flag = false;
    if (this.settingsPlayer.textures != null)
    {
      if ((UnityEngine.Object) this.settingsPlayer.textures[(int) this.viewing] != (UnityEngine.Object) null)
      {
        int viewing = (int) this.viewing;
        myContextMenu.AddItem(((Outfit) viewing).ToString() + " pivot: " + this.settingsPlayer.metaData[viewing].pivot.ToString("0.##"), new Action(this.VectorPopupPivot), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
      }
      for (int index = 0; index < this.settingsPlayer.textures.Length; ++index)
      {
        if ((UnityEngine.Object) this.settingsPlayer.textures[index] != (UnityEngine.Object) null && this.settingsPlayer.metaData[index].hasEffect)
        {
          flag = true;
          int e = index;
          myContextMenu.AddItem(((Outfit) e).ToString() + " anim FPS (" + this.settingsPlayer.metaData[e].fps.ToString("0.##") + ")", (Action) (() => this.FloatPopup(this.settingsPlayer.metaData[e].fps, ((Outfit) e).ToString() + " anim FPS", (Action<string>) (s =>
          {
            if (string.IsNullOrEmpty(s))
              return;
            float f = float.Parse(s);
            this.settingsPlayer.metaData[e].fps = f;
            this.configPlayer.Get((Outfit) e).transform.GetChild(0).GetComponent<AnimateRepeat>().UpdateFPS(f);
            this.settingsPlayer.metaData[e].SerializeToFile(this.settingsPlayer.customPieces[e]);
          }))), (Color) ColorScheme.GetColor(MyContextMenu.ColorCyan));
        }
      }
    }
    if (flag)
      myContextMenu.AddSeperator("-------------");
    myContextMenu.AddItem("View Left Feet", new Action(this.SelectLeftFoot), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("View Right Feet", new Action(this.SelectRightFoot), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("View Mouths", new Action(this.SelectMouth), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.Rebuild();
  }

  private void VectorPopupPivot()
  {
    MyContextMenu.CloseInstance();
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator("Pivot");
    myContextMenu.AddVectorField((Action<Vector2>) (v => this.settingsPlayer.UpdatePivot(this.viewing, v, this.configPlayer.Get(this.viewing))), this.settingsPlayer.metaData[(int) this.viewing].pivot);
    myContextMenu.Rebuild();
  }

  private void FloatPopup(float f, string n, Action<string> a)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator(n + " current: " + f.ToString("0.##"));
    myContextMenu.AddInput(a);
    myContextMenu.Rebuild();
  }

  public void click3()
  {
    if ((double) this.click_counter < (double) Time.realtimeSinceStartup)
      this.click_counter = Time.realtimeSinceStartup;
    this.click_counter += 3f;
    if ((double) this.click_counter <= (double) Time.realtimeSinceStartup + 7.0)
      return;
    this.click_counter = 0.0f;
    this.container_creator_controls.SetActive(!this.container_creator_controls.activeSelf);
    Global.SetPrefBool("prefassetcreator", this.container_creator_controls.activeSelf);
  }

  private void Update()
  {
    if (!this.container_creator_controls.activeSelf)
    {
      if (!this.controls.Complete())
        return;
      this.container_creator_controls.SetActive(true);
      Global.SetPrefBool("prefassetcreator", true);
    }
    else if (Input.GetKeyDown(KeyCode.F8))
    {
      this.container_creator_controls.SetActive(!this.container_creator_controls.activeSelf);
      Global.SetPrefBool("prefassetcreator", this.container_creator_controls.activeSelf);
    }
    else if (Input.GetKeyDown(KeyCode.F11) && Client.MyAccount.accountType.has(AccountType.Developer))
      this.TestClan();
    else if (Input.GetKeyDown(KeyCode.F12) && Client.MyAccount.accountType.isDev())
    {
      MyContextMenu myContextMenu = MyContextMenu.Show();
      myContextMenu.AddSeperator("Upload Clan Outfit - " + this.viewing.ToString());
      myContextMenu.AddSeperator("Clan Name?");
      myContextMenu.AddInput((Action<string>) (n =>
      {
        if (string.IsNullOrEmpty(n))
          return;
        Client.AskToUploadClanOutfit(n, this.viewing, this.settingsPlayer.textures[(int) this.viewing].texture, this.settingsPlayer.metaData[(int) this.viewing].pivot, this.viewing == Outfit.Head ? (this.settingsPlayer.indexMouth == (byte) 24 ? (byte) 1 : (byte) 0) : (byte) 0);
      }));
      myContextMenu.Rebuild();
    }
    else
    {
      if (!this.container_creator_controls.activeSelf || this.settingsPlayer.textures == null || !((UnityEngine.Object) this.settingsPlayer.textures[(int) this.viewing] != (UnityEngine.Object) null) || !Input.anyKey)
        return;
      SpriteRenderer sr = this.configPlayer.Get(this.viewing);
      Texture2D texture = this.settingsPlayer.textures[(int) this.viewing].texture;
      SettingsPlayer.MetaData metaData1 = this.settingsPlayer.metaData[(int) this.viewing];
      float x = metaData1.pivot.x;
      float y1 = metaData1.pivot.y;
      if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftArrow))
      {
        x += Mathf.Clamp(Input.GetKey(KeyCode.LeftControl) ? 0.01f : Time.deltaTime, 0.0f, 0.01f);
        if ((double) x > 2.0)
          x = 2f;
        this.settingsPlayer.UpdatePivot(this.viewing, new Vector2(x, y1), sr);
      }
      if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightArrow))
      {
        x -= Mathf.Clamp(Input.GetKey(KeyCode.LeftControl) ? 0.01f : Time.deltaTime, 0.0f, 0.01f);
        if ((double) x < -2.0)
          x = -2f;
        this.settingsPlayer.UpdatePivot(this.viewing, new Vector2(x, y1), sr);
      }
      if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.UpArrow))
      {
        y1 -= Mathf.Clamp(Input.GetKey(KeyCode.LeftControl) ? 0.01f : Time.deltaTime, 0.0f, 0.01f);
        if ((double) y1 < -2.0)
          y1 = -2f;
        this.settingsPlayer.UpdatePivot(this.viewing, new Vector2(x, y1), sr);
      }
      if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.DownArrow))
      {
        float y2 = y1 + Mathf.Clamp(Input.GetKey(KeyCode.LeftControl) ? 0.01f : Time.deltaTime, 0.0f, 0.01f);
        if ((double) y2 > 2.0)
          y2 = 2f;
        this.settingsPlayer.UpdatePivot(this.viewing, new Vector2(x, y2), sr);
      }
      if (!Input.GetKeyDown(KeyCode.F12))
        return;
      int num = 0;
      foreach (SettingsPlayer.MetaData metaData2 in this.settingsPlayer.metaData)
        ++num;
    }
  }

  public void TryBuyTournamentOutfit(int i)
  {
    int num = SettingsPlayer.CheckTournament(CharacterCreation.GetViewing(this.viewing), i);
    if (num <= 0 || Client.MyAccount.cosmetics.array[(int) CharacterCreation.GetViewing(this.viewing)][i])
      return;
    if (Client.MyAccount.tournamentCoins >= num)
      MyMessageBox.Create("Buy this outfit piece for <sprite name=\"tcoin\"> <#FF0000>" + (object) num + "</color> tournament coins?", (Action) (() => Prestige.Ask((byte) 10, (int) CharacterCreation.GetViewing(this.viewing), i)), icon: this.curGroup[i]);
    else
      MyToolTip.Show("You need <sprite name=\"tcoin\"> " + (object) (num - Client.MyAccount.tournamentCoins) + " more tournament coins to buy this outfit - Better sign-up for the next one!");
  }

  public void SelectIndex(int i)
  {
    switch (this.viewing)
    {
      case Outfit.Body:
        this.configPlayer.settingsPlayer.indexBody = (byte) i;
        break;
      case Outfit.Head:
        this.configPlayer.settingsPlayer.indexHead = (byte) i;
        break;
      case Outfit.LeftHand:
        this.configPlayer.settingsPlayer.indexLeftHand = (byte) i;
        break;
      case Outfit.RightHand:
        this.configPlayer.settingsPlayer.indexRightHand = (byte) i;
        break;
      case Outfit.Hair:
        this.configPlayer.settingsPlayer.indexHat = (byte) i;
        break;
      case Outfit.Beard:
        this.configPlayer.settingsPlayer.indexBeard = (byte) i;
        break;
      case Outfit.LeftFoot:
        this.settingsPlayer.custom = (byte) 1;
        this.configPlayer.settingsPlayer.indexLeftFoot = (byte) i;
        break;
      case Outfit.RightFoot:
        this.settingsPlayer.custom = (byte) 1;
        this.configPlayer.settingsPlayer.indexRightFoot = (byte) i;
        break;
      case Outfit.Mouth:
        this.settingsPlayer.custom = (byte) 1;
        this.configPlayer.settingsPlayer.indexMouth = (byte) i;
        break;
      case Outfit.Beard2:
        this.configPlayer.settingsPlayer.indexBeard2 = (byte) i;
        break;
      case Outfit.Beard3:
        this.configPlayer.settingsPlayer.indexBeard3 = (byte) i;
        break;
    }
    this.configPlayer.settingsPlayer.ResetCustom(this.viewing);
    this.configPlayer.settingsPlayer.custom = this.configPlayer.settingsPlayer.CustomContainsSomething() ? (byte) 1 : (byte) 0;
    this.EquipAll();
    this.Changed();
    this.FindEquipped();
  }

  private void SizeToFit(int f)
  {
    this.parentOutfitBox.sizeDelta = new Vector2(this.parentOutfitBox.sizeDelta.x, f > 81 ? (float) ((double) Mathf.Ceil((float) (f - 81) / 27f) * 33.0 + 105.0) : 105f);
  }

  private void HighlightButtonOutfit(int i)
  {
    this.UpdateColors();
    if ((UnityEngine.Object) this.activeButtonOutfit != (UnityEngine.Object) null)
      this.activeButtonOutfit.AlwaysOn = false;
    if (i >= this.buttonsOutfit.Length)
      return;
    this.buttonsOutfit[i].AlwaysOn = true;
    this.activeButtonOutfit = this.buttonsOutfit[i];
  }

  public void ClickPresetIndex(int i)
  {
    if (i >= this.buttonPresets.Length)
      i = 0;
    if ((UnityEngine.Object) this.activeButtonPreset != (UnityEngine.Object) null)
      this.activeButtonPreset.AlwaysOn = false;
    this.buttonPresets[i].AlwaysOn = true;
    this.activeButtonPreset = this.buttonPresets[i];
    this.pickerPresets.Set(ClientResources.Instance.GetColors(i));
    PlayerPrefs.SetInt("prefselectedpreset", i);
  }

  public void ClickAdvanced()
  {
    this.panelAdvanced.SetActive(true);
    this.panelBasic.SetActive(false);
    this.optionAdvanced.AlwaysOn = true;
    this.optionBasic.AlwaysOn = false;
    this.UpdateColors();
    Global.SetPrefBool("ccbasic", false);
  }

  public void ClickBasic()
  {
    this.panelAdvanced.SetActive(false);
    this.panelBasic.SetActive(true);
    this.optionAdvanced.AlwaysOn = false;
    this.optionBasic.AlwaysOn = true;
    this.UpdateColors();
    Global.SetPrefBool("ccbasic", true);
  }

  public void ClickToggleAll()
  {
    this.toggleColorSelected.SetIsOnWithoutNotify(false);
    for (int index = 0; index < this.toggleColorOutfit.Length; ++index)
      this.toggleColorOutfit[index].SetIsOnWithoutNotify(false);
    this.recoloring.Clear();
    this.changeAllColors = true;
    if (this.toggleColorAll.isOn)
      return;
    this.toggleColorAll.SetIsOnWithoutNotify(true);
  }

  public void ClickToggleSelected()
  {
    this.toggleColorAll.SetIsOnWithoutNotify(false);
    for (int index = 0; index < this.toggleColorOutfit.Length; ++index)
      this.toggleColorOutfit[index].SetIsOnWithoutNotify(false);
    this.recoloring.Clear();
    this.changeAllColors = false;
    if (this.toggleColorSelected.isOn)
      return;
    this.toggleColorSelected.SetIsOnWithoutNotify(true);
  }

  public void ClickTogggleColor(int i)
  {
    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Global.OS == OperatingSystem.Android)
    {
      this.ClickAddTogggleColor(i);
    }
    else
    {
      bool isOn = this.toggleColorOutfit[i].isOn;
      this.toggleColorAll.SetIsOnWithoutNotify(false);
      this.toggleColorSelected.SetIsOnWithoutNotify(false);
      for (int index = 0; index < this.toggleColorOutfit.Length; ++index)
        this.toggleColorOutfit[index].SetIsOnWithoutNotify(false);
      this.toggleColorOutfit[i].SetIsOnWithoutNotify(isOn);
      this.recoloring.Clear();
      this.changeAllColors = false;
      if (this.recoloring.Contains((Outfit) i))
      {
        this.recoloring.Remove((Outfit) i);
        if (this.recoloring.Count != 0)
          return;
        this.changeAllColors = true;
        this.toggleColorAll.SetIsOnWithoutNotify(true);
      }
      else
        this.recoloring.Add((Outfit) i);
    }
  }

  public void ClickAddTogggleColor(int i)
  {
    this.toggleColorAll.SetIsOnWithoutNotify(false);
    this.toggleColorSelected.SetIsOnWithoutNotify(false);
    this.changeAllColors = false;
    if (this.recoloring.Contains((Outfit) i))
    {
      this.recoloring.Remove((Outfit) i);
      if (this.recoloring.Count != 0)
        return;
      this.changeAllColors = true;
      this.toggleColorAll.SetIsOnWithoutNotify(true);
    }
    else
      this.recoloring.Add((Outfit) i);
  }

  public void ClickLeft() => this.Populate(this._curOffset / 75 - 1);

  public void ClickRight() => this.Populate(this._curOffset / 75 + 1);

  public bool HasLeft() => this._curOffset > 0;

  public int Offset => this._curOffset;

  public bool HasRight() => this.curSorted.Count > this._curOffset + 75;

  public void FindEquipped()
  {
    int index = (int) this.configPlayer.settingsPlayer.GetOutfitIndex(this.viewing);
    int index1 = this.outfitButtons.FindIndex((Predicate<OutfitButton>) (z => z.Index == index && z.gameObject.activeSelf));
    if (index1 >= 0)
    {
      this.selectedOutfit.anchoredPosition = ((RectTransform) this.outfitButtons[index1].transform).anchoredPosition;
      this.selectedOutfit.gameObject.SetActive(true);
    }
    else
      this.selectedOutfit.gameObject.SetActive(false);
  }

  public void ClickUndo()
  {
    if (this.stackUndo.Count > 0)
    {
      SettingsPlayer b = this.stackUndo.Pop();
      this.stackRedo.Push(this.stackCurrent);
      this.settingsPlayer.CopyOutfit(b);
      this.stackCurrent = b;
      this.UpdateColors();
      this.EquipAll();
      this.FindEquipped();
    }
    this.buttonRedo.Interactable(this.stackRedo.Count > 0);
    this.buttonUndo.Interactable(this.stackUndo.Count > 0);
    TMP_Text txtRedo = this.txtRedo;
    int count = this.stackRedo.Count;
    string str1 = count.ToString();
    txtRedo.text = str1;
    TMP_Text txtUndo = this.txtUndo;
    count = this.stackUndo.Count;
    string str2 = count.ToString();
    txtUndo.text = str2;
  }

  public void ClickRedo()
  {
    if (this.stackRedo.Count > 0)
    {
      SettingsPlayer b = this.stackRedo.Pop();
      this.stackUndo.Push(this.stackCurrent);
      this.settingsPlayer.CopyOutfit(b);
      this.stackCurrent = b;
      this.UpdateColors();
      this.EquipAll();
      this.FindEquipped();
    }
    this.buttonRedo.Interactable(this.stackRedo.Count > 0);
    this.buttonUndo.Interactable(this.stackUndo.Count > 0);
    TMP_Text txtRedo = this.txtRedo;
    int count = this.stackRedo.Count;
    string str1 = count.ToString();
    txtRedo.text = str1;
    TMP_Text txtUndo = this.txtUndo;
    count = this.stackUndo.Count;
    string str2 = count.ToString();
    txtUndo.text = str2;
  }

  public void Changed()
  {
    SettingsPlayer settingsPlayer = new SettingsPlayer();
    settingsPlayer.CopyOutfit(this.settingsPlayer);
    if (this.stackCurrent != null)
    {
      this.stackUndo.Push(this.stackCurrent);
      this.stackCurrent = settingsPlayer;
      this.stackRedo.Clear();
    }
    else
      this.stackCurrent = settingsPlayer;
    this.buttonRedo.Interactable(this.stackRedo.Count > 0);
    this.buttonUndo.Interactable(this.stackUndo.Count > 0);
    this.txtRedo.text = this.stackRedo.Count.ToString();
    this.txtUndo.text = this.stackUndo.Count.ToString();
  }

  public void Populate(int page)
  {
    if (page < 0)
      page = 0;
    for (this._curOffset = page * 75; this.curSorted.Count <= this._curOffset && page > 0; this._curOffset = page * 75)
      --page;
    this.curPage = page;
    int count = this.curSorted.Count;
    int index1 = 0;
    int index2 = 0;
    Outfit viewing = CharacterCreation.GetViewing(this.viewing);
    int index3 = (int) viewing;
    for (; index1 < count - this._curOffset && index1 < 75; ++index1)
    {
      bool disabled = false;
      int index4 = this.curSorted[index1 + this._curOffset];
      if (index3 < 6 && !Client.cosmetics.array[index3][index4])
      {
        int num = SettingsPlayer.ClientHasAchievement(viewing, index4);
        if (num <= 0)
        {
          if (num >= 0)
            disabled = true;
          else
            continue;
        }
      }
      this.outfitButtons[index2].SetSprite(this.curGroup[index4], viewing, index4, disabled);
      this.outfitButtons[index2].gameObject.SetActive(true);
      ++index2;
    }
    for (; index1 < 75; ++index1)
      this.outfitButtons[index1].gameObject.SetActive(false);
    this.buttonLeft.Interactable(this.HasLeft());
    this.buttonRight.Interactable(this.HasRight());
    this.txtPage.text = "Page " + (object) (page + 1) + " of " + (object) ((this.curSorted.Count + 75 - 1) / 75);
    this.FindEquipped();
  }

  public void RefreshViewing()
  {
    this.Populate(this.curPage);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectBody()
  {
    this.viewing = Outfit.Body;
    this.curGroup = ClientResources.Instance._characterBody;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectBeard()
  {
    this.viewing = Outfit.Beard;
    this.curGroup = ClientResources.Instance._characterBeards;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectBeard2()
  {
    if (this.lockedOutfit[0].activeInHierarchy)
      return;
    this.viewing = Outfit.Beard2;
    this.curGroup = ClientResources.Instance._characterBeards;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectBeard3()
  {
    if (this.lockedOutfit[1].activeInHierarchy)
      return;
    this.viewing = Outfit.Beard3;
    this.curGroup = ClientResources.Instance._characterBeards;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectHair()
  {
    this.viewing = Outfit.Hair;
    this.curGroup = ClientResources.Instance._characterHats;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectHead()
  {
    this.viewing = Outfit.Head;
    this.curGroup = ClientResources.Instance._characterHeads;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectLeftHand()
  {
    this.viewing = Outfit.LeftHand;
    this.curGroup = ClientResources.Instance._characterLeftHand;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectRightHand()
  {
    this.viewing = Outfit.RightHand;
    this.curGroup = ClientResources.Instance._characterRightHand;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectLeftFoot()
  {
    this.viewing = Outfit.LeftFoot;
    this.curGroup = ClientResources.Instance._characterLeftFoot;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectRightFoot()
  {
    this.viewing = Outfit.RightFoot;
    this.curGroup = ClientResources.Instance._characterRightFoot;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void SelectMouth()
  {
    this.viewing = Outfit.Mouth;
    this.curGroup = ClientResources.Instance._characterMouths;
    this.FilterOutfits();
    this.Populate(0);
    this.HighlightButtonOutfit((int) this.viewing);
  }

  public void CreatedSortedGroups()
  {
    this.AddGroup(Outfit.Body, ClientResources.Instance._characterBody);
    this.AddGroup(Outfit.Head, ClientResources.Instance._characterHeads);
    this.AddGroup(Outfit.LeftHand, ClientResources.Instance._characterLeftHand);
    this.AddGroup(Outfit.RightHand, ClientResources.Instance._characterRightHand);
    this.AddGroup(Outfit.Hair, ClientResources.Instance._characterHats);
    this.AddGroup(Outfit.Beard, ClientResources.Instance._characterBeards);
    this.AddGroup(Outfit.LeftFoot, ClientResources.Instance._characterLeftFoot);
    this.AddGroup(Outfit.RightFoot, ClientResources.Instance._characterRightFoot);
    this.AddGroup(Outfit.Mouth, ClientResources.Instance._characterMouths);
    this.sortedGroups.Add(this.sortedGroups[5]);
    this.sortedGroups.Add(this.sortedGroups[5]);
  }

  public void ToggleClan(bool v)
  {
    this.FilterOutfits();
    this.Populate(this.curPage);
  }

  public static Outfit GetViewing(Outfit viewing)
  {
    return viewing != Outfit.Beard2 && viewing != Outfit.Beard3 ? viewing : Outfit.Beard;
  }

  private void FilterOutfits()
  {
    List<int> intList = new List<int>();
    Outfit viewing = CharacterCreation.GetViewing(this.viewing);
    List<int> sortedGroup = this.sortedGroups[(int) viewing];
    if (this.optionClanOutfit.isOn)
      intList.Add((int) SettingsPlayer.clanOutfit[(int) viewing]);
    if (this.optionRevealSeasonal.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.IsSeasonal(viewing, sortedGroup[index]))
          intList.Add(sortedGroup[index]);
      }
    }
    if (this.optionRevealAchievements.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.CheckAchievements(viewing, sortedGroup[index]) != Achievement.None)
          intList.Add(sortedGroup[index]);
      }
    }
    if (this.optionRevealAccountType.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.CheckAccountType(viewing, sortedGroup[index]) != AccountType.None)
          intList.Add(sortedGroup[index]);
      }
    }
    if (this.optionRevealPrestige.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.CheckPrestige(viewing, sortedGroup[index]) > 0)
        {
          intList.Add(sortedGroup[index]);
        }
        else
        {
          switch (SettingsPlayer.CheckAchievements(viewing, sortedGroup[index]))
          {
            case Achievement.High_Mage:
            case Achievement.Arch_Mage:
              intList.Add(sortedGroup[index]);
              continue;
            default:
              continue;
          }
        }
      }
    }
    if (this.optionRevealExperience.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.CheckExperience(viewing, sortedGroup[index]) > 0)
          intList.Add(sortedGroup[index]);
      }
    }
    if (this.optionTournament.isOn)
    {
      for (int index = 0; index < sortedGroup.Count; ++index)
      {
        if (SettingsPlayer.CheckTournament(viewing, sortedGroup[index]) > 0)
          intList.Add(sortedGroup[index]);
      }
    }
    this.filteredGroup = intList.Count == 0 ? this.sortedGroups[(int) viewing] : intList;
  }

  private void AddGroup(Outfit viewing, Sprite[] g)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < g.Length; ++index)
    {
      if (viewing < Outfit.LeftFoot && !Client.cosmetics.array[(int) viewing][index])
      {
        int num = SettingsPlayer.ClientHasAchievement(viewing, index);
        if (num <= 0 && num < 0)
          continue;
      }
      intList.Add(index);
    }
    this.sortedGroups.Add(intList);
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) this)
      CharacterCreation.Instance = (CharacterCreation) null;
    if ((UnityEngine.Object) this.playerObj != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.playerObj);
    Inert.SaveSettingsPlayer();
  }

  public void ClickSavedOutfits(bool v) => ChangeOutfitMenu.Create(v, sp: this.settingsPlayer);

  private void OnSelectCustomOutfit(string s, Outfit o)
  {
    PlayerPrefs.SetString("preffilebrowser", s.Substring(0, s.Length - Path.GetFileName(s).Length));
    if (s.EndsWith(".outfitPKG", StringComparison.OrdinalIgnoreCase))
    {
      this.settingsPlayer.Import(s);
      this.EquipAll();
      this.Changed();
    }
    else
      this.OnSelectCustomImage(s, o);
  }

  public void PickOutfit(Outfit o)
  {
    string[] strArray = new string[3]
    {
      "*png;*outfitPKG",
      "png",
      "outfitPKG"
    };
    this.onpickFile(Singleton<FileBrowser>.Instance.OpenSingleFile("Picking - " + o.ToString(), PlayerPrefs.GetString("preffilebrowser", Client.DownloadsFolder), "", strArray), o);
  }

  private void onpickFile(string s, Outfit o)
  {
    switch (s)
    {
      case null:
        break;
      case "":
        break;
      default:
        this.OnSelectCustomOutfit(s, o);
        break;
    }
  }

  public void ClickCustomOutfit() => this.PickOutfit(this.viewing);

  public void ClickToggleWalk()
  {
    if (this.creature.animator.currentState == AnimateState.Stop)
      this.creature.animator.Play(AnimateState.Walk, float.MaxValue);
    else
      this.creature.animator.Play(AnimateState.Stop);
  }

  public void ClickPreview()
  {
    if (Global.OS.Is(OperatingSystem.WebGL))
    {
      this.SavePNG2("", true);
    }
    else
    {
      MyContextMenu myContextMenu = MyContextMenu.Show();
      myContextMenu.AddSeperator("Save a preview image - Leave empty to cancel");
      myContextMenu.AddInput((Action<string>) (s =>
      {
        if (string.IsNullOrEmpty(s))
          return;
        this.SavePNG2(s);
      }));
      myContextMenu.Rebuild();
    }
  }

  public void ClickExport()
  {
    MyContextMenu.Show().AddInput((Action<string>) (s =>
    {
      if (string.IsNullOrEmpty(s))
        return;
      this.settingsPlayer.Export(s);
    }));
  }

  private void CheckForEffect(
    string url,
    Outfit o,
    Texture2D orig,
    SettingsPlayer.MetaData metaData)
  {
    metaData.hasEffect = false;
    this.settingsPlayer.ResetAnimation(o);
    string path = url.Substring(0, url.Length - Path.GetExtension(url).Length) + "_effect.png";
    if (!File.Exists(path))
      return;
    SettingsPlayer.CustomAnim anim = CharacterCreation.GenerateAnim(File.ReadAllBytes(path), orig, metaData);
    this.settingsPlayer.animations[(int) o] = anim;
    metaData.hasEffect = true;
  }

  internal static SettingsPlayer.CustomAnim GenerateAnim(
    byte[] bytes,
    Texture2D orig,
    SettingsPlayer.MetaData metaData)
  {
    Texture2D texture2D = new Texture2D(2, 2);
    texture2D.LoadImage(bytes);
    int num1 = texture2D.width / orig.width;
    int num2 = texture2D.height / orig.height;
    if (num1 == 0 || num2 == 0)
      return (SettingsPlayer.CustomAnim) null;
    int width = orig.width;
    int height = orig.height;
    SettingsPlayer.CustomAnim anim = new SettingsPlayer.CustomAnim();
    for (int index1 = 0; index1 < num1; ++index1)
    {
      for (int index2 = 0; index2 < num2; ++index2)
        anim.sprites.Add(Global.AddSprite(Sprite.Create(texture2D, new Rect((float) (index1 * width), (float) ((num2 - index2 - 1) * height), (float) width, (float) height), metaData.pivot, 2f)));
    }
    return anim;
  }

  private void OnSelectCustomImage(string url, Outfit o)
  {
    this.LoadImage(url, o);
    this.EquipAll();
    this.Changed();
  }

  private void LoadImage(string url, Outfit o)
  {
    Texture2D texture2D = new Texture2D(2, 2);
    texture2D.LoadImage(File.ReadAllBytes(url));
    int o1 = (int) o;
    SettingsPlayer.MetaData meta = SettingsPlayer.MetaData.Deserialize(url + ".meta").meta;
    this.settingsPlayer.MakeSureIntitialized();
    this.settingsPlayer.customPieces[o1] = url;
    this.settingsPlayer.metaData[o1] = meta;
    if ((UnityEngine.Object) texture2D == (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) this.settingsPlayer.textures[o1] != (UnityEngine.Object) null)
      {
        Global.DestroySprite(this.settingsPlayer.textures[o1]);
        this.settingsPlayer.textures[o1] = (Sprite) null;
      }
    }
    else
    {
      this.settingsPlayer.textures[o1] = Global.AddSprite(Sprite.Create(texture2D, new Rect(0.0f, 0.0f, (float) texture2D.width, (float) texture2D.height), this.settingsPlayer.metaData[o1].pivot, 2f));
      this.settingsPlayer.Reset((Outfit) o1);
    }
    this.CheckForEffect(url, o, texture2D, meta);
  }

  public void LoadAll()
  {
    if (!this.settingsPlayer.CustomContainsSomething())
    {
      this.EquipAll();
      this.Changed();
    }
    else
    {
      this.settingsPlayer.custom = (byte) 1;
      this.settingsPlayer.MakeSureIntitialized();
      for (int o = 0; o < 11; ++o)
      {
        if (string.IsNullOrEmpty(this.settingsPlayer.customPieces[o]) || !File.Exists(this.settingsPlayer.customPieces[o]))
        {
          if ((UnityEngine.Object) this.settingsPlayer.textures[o] != (UnityEngine.Object) null)
          {
            Global.DestroySprite(this.settingsPlayer.textures[o]);
            this.settingsPlayer.textures[o] = (Sprite) null;
          }
        }
        else
          this.LoadImage(this.settingsPlayer.customPieces[o], (Outfit) o);
      }
      this.EquipAll();
      this.Changed();
    }
  }
}
