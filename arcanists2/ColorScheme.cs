

using mattmc3.dotmore.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ColorScheme
{
  public string name = "";
  public List<ColorValue> colors = new List<ColorValue>();
  public List<ColorText> text = new List<ColorText>();
  public Color backgroundColor = (Color) ColorScheme.DefBackgroundColor;
  internal Dictionary<Color32, Color32> textSet = new Dictionary<Color32, Color32>();
  internal List<ColorMaterial> _materials = new List<ColorMaterial>();
  private const byte Version = 4;
  private Color32 temp;
  public Color highlightColor = (Color) new Color32(byte.MaxValue, (byte) 212, (byte) 0, byte.MaxValue);
  public Color bgColor = Color.black;
  public byte fontIndex;
  public static Color32 DefBackgroundColor = new Color32((byte) 188, (byte) 188, (byte) 188, byte.MaxValue);
  public static ColorScheme.ForcedGraphics forcedGraphics = ColorScheme.ForcedGraphics.Dynamic;

  public Material _material => ClientResources.Instance._matColorPalette;

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 4);
    w.Write(this.name);
    w.Write(this.colors.Count);
    for (int index = 0; index < this.colors.Count; ++index)
    {
      w.Write(this.colors[index].saturation);
      w.WriteNoAlpha(this.colors[index].color);
    }
    w.Write(this.text.Count);
    for (int index = 0; index < this.text.Count; ++index)
    {
      w.WriteNoAlpha(this.text[index]._origionalColor);
      w.WriteNoAlpha(this.text[index].color);
    }
    w.Write(this._materials.Count);
    for (int index = 0; index < this._materials.Count; ++index)
      w.WriteNoAlpha(this._materials[index].color);
    w.WriteNoAlpha((Color32) this.highlightColor);
    w.WriteNoAlpha((Color32) this.bgColor);
    w.WriteNoAlpha((Color32) this.backgroundColor);
    w.Write(this.fontIndex);
  }

  public static ColorScheme Deserialize(myBinaryReader r)
  {
    ColorScheme colorScheme = Client.defaultScheme.Copy();
    int num1 = (int) r.ReadByte();
    colorScheme.name = r.ReadString();
    int num2 = r.ReadInt32();
    if (num2 > 100)
      return colorScheme;
    for (int index = 0; index < num2; ++index)
    {
      int sat = r.ReadInt32();
      Color32 color32 = r.ReadColor32NoAlpha();
      ColorValue colorValue = colorScheme.colors.Find((Predicate<ColorValue>) (z => z.saturation == sat));
      if (colorValue != null)
        colorValue.color = color32;
    }
    if (num2 > 100)
      return colorScheme;
    int num3 = r.ReadInt32();
    for (int index = 0; index < num3; ++index)
    {
      Color32 og = r.ReadColor32NoAlpha();
      Color32 color32 = r.ReadColor32NoAlpha();
      ColorText colorText = colorScheme.text.Find((Predicate<ColorText>) (z => (int) z._origionalColor.r == (int) og.r && (int) z._origionalColor.g == (int) og.g && (int) z._origionalColor.b == (int) og.b));
      if (colorText != null)
        colorText.color = color32;
    }
    int num4 = r.ReadInt32();
    if (num4 > colorScheme._materials.Count)
      num4 = colorScheme._materials.Count;
    for (int index = 0; index < num4; ++index)
      colorScheme._materials[index].color = r.ReadColor32NoAlpha();
    if (num1 > 1)
    {
      colorScheme.highlightColor = (Color) r.ReadColor32NoAlpha();
      colorScheme.bgColor = (Color) r.ReadColor32NoAlpha();
    }
    colorScheme.backgroundColor = num1 <= 2 ? (Color) ColorScheme.DefBackgroundColor : (Color) r.ReadColor32NoAlpha();
    colorScheme.fontIndex = num1 <= 3 ? (byte) 0 : r.ReadByte();
    return colorScheme;
  }

  internal static string defaultScheme
  {
    get
    {
      return Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "scheme" + ColorScheme.ext;
    }
  }

  internal static string defaultPath
  {
    get
    {
      string persistentDataPath = Application.persistentDataPath;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str2 = directorySeparatorChar.ToString();
      return persistentDataPath + str1 + "Schemes" + str2;
    }
  }

  internal static string ext => ".arcscheme";

  public void Save(string name)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        this.Serialize(w);
      if (string.IsNullOrEmpty(name))
        File.WriteAllBytes(ColorScheme.defaultScheme, memoryStream.ToArray());
      else
        File.WriteAllBytes(ColorScheme.defaultPath + name + ColorScheme.ext, memoryStream.ToArray());
    }
  }

  public static ColorScheme FromFile(string name)
  {
    if (File.Exists(name))
    {
      using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(name)))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
        {
          try
          {
            return ColorScheme.Deserialize(r);
          }
          catch (Exception ex)
          {
            Debug.Log((object) ex.ToString());
          }
        }
      }
    }
    return (UnityEngine.Object) ClientResources.Instance != (UnityEngine.Object) null ? ColorScheme.From(ClientResources.Instance._texturePalette) : (ColorScheme) null;
  }

  public static ColorScheme From(Texture2D t)
  {
    Controller schemeController = Controller.Instance.schemeController;
    ColorScheme colorScheme = new ColorScheme();
    Color color = (Color) new Color32((byte) 10, (byte) 9, (byte) 120, (byte) 47);
    colorScheme.colors.Add(new ColorValue()
    {
      saturation = 0,
      color = (Color32) Color.black
    });
    colorScheme.colors.Add(new ColorValue()
    {
      saturation = (int) byte.MaxValue,
      color = (Color32) Color.white
    });
    for (int x = 15; x < 250; x += 5)
    {
      Color pixel = t.GetPixel(x, 0);
      if ((double) pixel.g != 1.0 || (double) pixel.r != 0.0 || (double) pixel.b != 0.0)
        colorScheme.colors.Add(new ColorValue()
        {
          saturation = x,
          color = (Color32) pixel
        });
    }
    OrderedSet<Color32> colors = new OrderedSet<Color32>();
    colorScheme.AddText(schemeController.MenuMain, colors);
    colorScheme.AddText(schemeController.MenuLobby, colors);
    colorScheme.AddText(schemeController.MenuUnrated, colors);
    colorScheme.AddText(schemeController.MenuOptions, colors);
    colorScheme.AddText(schemeController.MenuColorScheme, colors);
    colorScheme.AddText(schemeController.MenuSpellSelection, colors);
    colorScheme.AddText(schemeController.PopupSavedOutfits, colors);
    colorScheme.AddText(schemeController.PopupSavedSpells, colors);
    colorScheme.AddText(schemeController.MenuCharacterCreation, colors);
    colorScheme.AddText(schemeController.MenuSpellSelection, colors);
    foreach (Color32 contextMenuColor in ColorScheme.GetAllContextMenuColors())
      colors.Add(contextMenuColor);
    foreach (Color32 allChatColor in ColorScheme.GetAllChatColors())
      colors.Add(allChatColor);
    foreach (Color32 color32 in colors)
      colorScheme.text.Add(new ColorText()
      {
        color = color32,
        _origionalColor = color32
      });
    foreach (ColorMaterial schemematerial in ClientResources.Instance._schemematerials)
      colorScheme._materials.Add(schemematerial.Copy());
    return colorScheme;
  }

  public static List<Color32> GetAllContextMenuColors()
  {
    return new List<Color32>()
    {
      MyContextMenu.ColorRed.To32(),
      MyContextMenu.ColorGreen.To32(),
      MyContextMenu.ColorYellow.To32(),
      MyContextMenu.ColorGray.To32(),
      MyContextMenu.ColorBlue.To32(),
      MyContextMenu.ColorCyan.To32(),
      MyContextMenu.ColorPurple.To32(),
      MyContextMenu.ColorClan.To32(),
      MyContextMenu.ColorCream.To32()
    };
  }

  public static List<Color32> GetAllChatColors()
  {
    return new List<Color32>()
    {
      Global.ColorMiniGameText.To32(),
      Global.ColorLobbyText.To32(),
      Global.ColorGameText.To32(),
      Global.ColorSentPrivate.To32(),
      Global.ColorReceivedPrivate.To32(),
      Global.ColorGrayChat.To32(),
      Global.ColorClanText.To32(),
      Global.ColorSystem.To32(),
      Global.ColorNotification.To32(),
      Global.ColorAnnoucement.To32(),
      Global.ColorTeamText.To32()
    };
  }

  private void AddText(GameObject g, OrderedSet<Color32> colors)
  {
    foreach (TMP_Text componentsInChild in g.GetComponentsInChildren<TMP_Text>())
      colors.Add(componentsInChild.color.To32());
    foreach (UIOnHoverChild componentsInChild in g.GetComponentsInChildren<UIOnHoverChild>())
    {
      if (componentsInChild.useTMPTextColor)
      {
        colors.Add(componentsInChild.textNormalColor.To32());
        colors.Add(componentsInChild.textHighlightedColor.To32());
        colors.Add(componentsInChild.textPressedColor.To32());
        colors.Add(componentsInChild.textDisabledColor.To32());
      }
    }
  }

  public ColorScheme Copy()
  {
    ColorScheme colorScheme = new ColorScheme();
    foreach (ColorValue color in this.colors)
      colorScheme.colors.Add(new ColorValue()
      {
        color = color.color,
        saturation = color.saturation
      });
    foreach (ColorText colorText in this.text)
      colorScheme.text.Add(new ColorText()
      {
        color = colorText.color,
        _origionalColor = colorText._origionalColor
      });
    foreach (ColorMaterial colorMaterial in this._materials.Count == 0 ? ClientResources.Instance._schemematerials : this._materials)
      colorScheme._materials.Add(colorMaterial.Copy());
    colorScheme.highlightColor = this.highlightColor;
    colorScheme.bgColor = this.bgColor;
    colorScheme.backgroundColor = this.backgroundColor;
    colorScheme.fontIndex = this.fontIndex;
    return colorScheme;
  }

  public void SetColors()
  {
    Texture2D texture2D = new Texture2D(256, 1, TextureFormat.ARGB32, false, false);
    texture2D.filterMode = FilterMode.Point;
    texture2D.wrapMode = TextureWrapMode.Clamp;
    Color32[] pixels32 = texture2D.GetPixels32();
    foreach (ColorValue color in this.colors)
    {
      int num = Mathf.Min((int) byte.MaxValue, color.saturation + 2);
      for (int index = Mathf.Max(color.saturation - 2, 0); index <= num; ++index)
        pixels32[index] = color.color;
    }
    texture2D.SetPixels32(pixels32);
    texture2D.Apply();
    this._material.SetTexture("_PaletteTex", (Texture) texture2D);
    foreach (ColorMaterial material in this._materials)
      material.Apply();
    this.InitTextHash();
  }

  public void InitTextHash()
  {
    this.textSet.Clear();
    foreach (ColorText colorText in this.text)
      this.textSet.Add(colorText._origionalColor, colorText.color);
  }

  public static Color32 GetColor(Color32 a)
  {
    return Client.colorScheme == null ? a : Client.colorScheme.GetTextColor(a);
  }

  public static Color32 GetColor(ChatOrigination chatOrigination)
  {
    switch (chatOrigination)
    {
      case ChatOrigination.Lobby:
        return ColorScheme.GetColor(Global.ColorLobbyText);
      case ChatOrigination.Game:
        return ColorScheme.GetColor(Global.ColorGameText);
      case ChatOrigination.Private:
        return ColorScheme.GetColor(Global.ColorReceivedPrivate);
      case ChatOrigination.System:
        return ColorScheme.GetColor(Global.ColorSystem);
      case ChatOrigination.Clan:
        return ColorScheme.GetColor(Global.ColorClanText);
      case ChatOrigination.MiniGame:
        return ColorScheme.GetColor(Global.ColorMiniGameText);
      case ChatOrigination.Team:
        return ColorScheme.GetColor(Global.ColorTeamText);
      default:
        return ColorScheme.GetColor(Global.ColorLobbyText);
    }
  }

  public static Color32 GetColor(Color a)
  {
    return Client.colorScheme == null ? (Color32) a : Client.colorScheme.GetTextColor(a);
  }

  public Color32 GetTextColor(Color a)
  {
    return !this.textSet.TryGetValue(a.To32(), out this.temp) ? a.To32() : this.temp;
  }

  public Color32 GetTextColor(Color32 a)
  {
    return !this.textSet.TryGetValue(a, out this.temp) ? a : this.temp;
  }

  public void Apply(GameObject g)
  {
    Color32 color32;
    foreach (TMP_Text componentsInChild in g.GetComponentsInChildren<TMP_Text>(true))
    {
      if (this.textSet.TryGetValue(componentsInChild.color.To32(), out color32))
        componentsInChild.color = (Color) color32;
      if (this.fontIndex != (byte) 0 && (UnityEngine.Object) componentsInChild.font == (UnityEngine.Object) Controller.Instance.fontDef)
        componentsInChild.font = Controller.Instance.fontArc;
    }
    foreach (UIOnHoverChild componentsInChild in g.GetComponentsInChildren<UIOnHoverChild>(true))
    {
      if (componentsInChild.useTMPTextColor)
      {
        if (this.textSet.TryGetValue(componentsInChild.textNormalColor.To32(), out color32))
          componentsInChild.textNormalColor = (Color) color32;
        if (this.textSet.TryGetValue(componentsInChild.textHighlightedColor.To32(), out color32))
          componentsInChild.textHighlightedColor = (Color) color32;
        if (this.textSet.TryGetValue(componentsInChild.textPressedColor.To32(), out color32))
          componentsInChild.textPressedColor = (Color) color32;
        if (this.textSet.TryGetValue(componentsInChild.textDisabledColor.To32(), out color32))
          componentsInChild.textDisabledColor = (Color) color32;
        if (componentsInChild.gameObject.activeInHierarchy)
        {
          if (((object) componentsInChild).GetType() == typeof (UIOnHover))
            ((UIOnHover) componentsInChild).ForceUpdate();
          else
            componentsInChild.GetComponentInParent<UIOnHover>()?.ForceUpdate();
        }
      }
    }
    ColorScheme.Resolution(g);
  }

  public static void Resolution(GameObject g, bool forced = false)
  {
    if (ColorScheme.forcedGraphics != ColorScheme.ForcedGraphics.Dynamic)
    {
      if (!forced && ColorScheme.forcedGraphics != ColorScheme.ForcedGraphics.High)
        return;
      ColorScheme._Resolution(g, ColorScheme.forcedGraphics == ColorScheme.ForcedGraphics.High ? ClientResources.Instance.largeUI : ClientResources.Instance.smallUI);
    }
    else
    {
      bool flag = (double) Screen.width < 1000.0 * (96.0 / (double) Screen.dpi) || (double) Screen.height < 700.0 * (96.0 / (double) Screen.dpi);
      if (!flag && !forced)
        return;
      Dictionary<string, Sprite> ui = flag ? ClientResources.Instance.largeUI : ClientResources.Instance.smallUI;
      ColorScheme._Resolution(g, ui);
    }
  }

  public static void _Resolution(GameObject g, Dictionary<string, Sprite> ui)
  {
    Sprite sprite;
    foreach (Image componentsInChild in g.GetComponentsInChildren<Image>(true))
    {
      if ((UnityEngine.Object) componentsInChild.sprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.sprite.name, out sprite))
        componentsInChild.sprite = sprite;
    }
    foreach (UIOnHoverChild componentsInChild in g.GetComponentsInChildren<UIOnHoverChild>(true))
    {
      if (componentsInChild.useSpriteSwap)
      {
        if ((UnityEngine.Object) componentsInChild.NormalSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.NormalSprite.name, out sprite))
          componentsInChild.NormalSprite = sprite;
        if ((UnityEngine.Object) componentsInChild.HighlightedSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.HighlightedSprite.name, out sprite))
          componentsInChild.HighlightedSprite = sprite;
        if ((UnityEngine.Object) componentsInChild.PressedSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.PressedSprite.name, out sprite))
          componentsInChild.PressedSprite = sprite;
        if ((UnityEngine.Object) componentsInChild.DisabledSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.DisabledSprite.name, out sprite))
          componentsInChild.DisabledSprite = sprite;
      }
    }
    foreach (Scrollbar componentsInChild in g.GetComponentsInChildren<Scrollbar>(true))
    {
      if (componentsInChild.transition == Selectable.Transition.SpriteSwap)
      {
        SpriteState spriteState = componentsInChild.spriteState;
        if ((UnityEngine.Object) spriteState.highlightedSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.highlightedSprite.name, out sprite))
          spriteState.highlightedSprite = sprite;
        if ((UnityEngine.Object) spriteState.pressedSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.pressedSprite.name, out sprite))
          spriteState.pressedSprite = sprite;
        if ((UnityEngine.Object) spriteState.disabledSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.disabledSprite.name, out sprite))
          spriteState.disabledSprite = sprite;
        componentsInChild.spriteState = spriteState;
      }
    }
    foreach (Button componentsInChild in g.GetComponentsInChildren<Button>(true))
    {
      if (componentsInChild.transition == Selectable.Transition.SpriteSwap)
      {
        SpriteState spriteState = componentsInChild.spriteState;
        if ((UnityEngine.Object) spriteState.highlightedSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.highlightedSprite.name, out sprite))
          spriteState.highlightedSprite = sprite;
        if ((UnityEngine.Object) spriteState.pressedSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.pressedSprite.name, out sprite))
          spriteState.pressedSprite = sprite;
        if ((UnityEngine.Object) spriteState.disabledSprite != (UnityEngine.Object) null && ui.TryGetValue(spriteState.disabledSprite.name, out sprite))
          spriteState.disabledSprite = sprite;
        componentsInChild.spriteState = spriteState;
      }
    }
    foreach (ScrollButton componentsInChild in g.GetComponentsInChildren<ScrollButton>(true))
    {
      if ((UnityEngine.Object) componentsInChild.hoverSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.hoverSprite.name, out sprite))
        componentsInChild.hoverSprite = sprite;
      if ((UnityEngine.Object) componentsInChild.defaultSprite != (UnityEngine.Object) null && ui.TryGetValue(componentsInChild.defaultSprite.name, out sprite))
        componentsInChild.defaultSprite = sprite;
    }
  }

  public enum ForcedGraphics
  {
    Dynamic,
    Low,
    High,
  }
}
