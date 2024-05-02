

using mattmc3.dotmore.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ClientResources : MonoBehaviour
{
  internal TutorialFileRetriever tutorialFileRetriever = new TutorialFileRetriever("pur3extreme.github.io/ArcanistsTutorials/index.html");
  public Sprite[] _spellicons;
  public Sprite[] _characterHeads;
  public Sprite[] _characterBody;
  public Sprite[] _characterHats;
  public Sprite[] _characterLeftHand;
  public Sprite[] _characterRightHand;
  public Sprite[] _characterLeftFoot;
  public Sprite[] _characterRightFoot;
  public Sprite[] _characterBeards;
  public Sprite[] _characterMouths;
  public Sprite[] _previewOverrides;
  public Spell[] summonSpells;
  public Spell[] familiarSpells;
  public List<Sprite> _discordEmoji;
  public List<Sprite> badges;
  public Dictionary<string, Sprite> _overridePreviews = new Dictionary<string, Sprite>();
  public OverheadEmoji overheadEmoji;
  public Sprite[] ratingTypes;
  public Sprite headMage;
  public Sprite wandDisabled;
  public BigBubble big_bubble;
  public GameObject deathExplosion;
  public ParticleBloodSyphon bloodSyphon;
  public GameObject spaceshipObj;
  public Sprite grizzlyHead;
  public Sprite grizzlyUndeadHead;
  public ClientResources.TutorialData[] _tutorials;
  public Color[] pollColors;
  public LuaPanel luaPanel;
  public TMP_InputField luaInput;
  public Sprite[] luaGraphic;
  public Canvas overheadCanvasSpectator;
  public PfabMapPing pfabMapPing;
  public static int ColorBlindness;
  public Material _matColorPalette;
  public Texture2D _texturePalette;
  public List<ColorMaterial> _schemematerials;
  public Sprite[] joinDefault;
  public Sprite[] joinHover;
  public Sprite[] joinPressed;
  public Sprite[] ogjoinDefault;
  public Sprite[] ogjoinHover;
  public Sprite[] ogjoinPressed;
  public Sprite[] zombieDucks;
  public Sprite[] ducks;
  [Header("Prickly")]
  public Sprite[] prickly_sprites;
  [SerializeField]
  private Sprite[] _largeUI;
  [SerializeField]
  private Sprite[] _smallUI;
  internal Dictionary<string, Sprite> largeUI = new Dictionary<string, Sprite>();
  internal Dictionary<string, Sprite> smallUI = new Dictionary<string, Sprite>();
  public GameObject pfabMinerMarketIcons;
  public SandPool sandPool;
  public BoatSpectators boatSpectators;
  [Header("Colors")]
  public TeamColors colorsRegular;
  public Sprite zombieMonkeyIcon;
  public Sprite imgRestricted;
  public Sprite imgLocked;
  public Sprite imgBuyable;
  public Sprite imgBuyable2;
  public Sprite imgBuyable3;
  [Header("Context Menu Items")]
  public InputFieldPlus contextMenuInputField;
  public InputFieldPlus contextMenuInputFieldLong;
  public VectorFieldContextMenu contextMenuVectorField;
  public ButtonArrayContextmenu contextButtonArray;
  public UIOnSlider contextMenuSlider;
  public GameObject contextSpells;
  public Toggle contextToggle;
  public SelectionArrow _selectionArrow;
  public List<Texture2D> bloodTextures;
  public List<GameObject> tutIndicators;
  public GameObject folderPfab;
  public UIPlayerCharacter uiPlayerCharacter;
  public Sprite[] staffPerElement;
  public AudioClip clipChangeSpellBook;
  [Header("Achievement Icons")]
  public List<Sprite> _achievementIcons;
  public Sprite _achievementNotObtained;
  public GameObject woodenExplosion;
  public GameObject minionMasterFamiliar;
  public GameObject bombMasterFamiliar;
  public GameObject gravityObj;
  public ElementalSelection elementalSelection;
  public Sprite empty;
  public Sprite[] spellBookIcons;
  public Sprite spellBookIconHoliday;
  public Texture2D mouthTex;
  public Sprite[] MainMenuSprites;
  public GameObject bg_glow;
  public GameObject minimap_glow;
  public Sprite megaBoulderSprite;
  public GameObject particleCharge;
  public GameObject particleMine;
  public GameObject lightExplosion;
  public GameObject lavaBombExplosion;
  public Sprite[] seasonIcons;
  public Sprite[] wispPhantomSprites;
  [Header("Undead Sprites")]
  public Sprite[] zombieDragonSprites;
  public Sprite[] zombieDragonAttackSprites;
  public Sprite[] corruptZombieDragonSprites;
  public Sprite[] corruptZombieDragonAttackSprites;
  public Sprite[] zombiePegasusSprites;
  public Sprite[] freshWaterSprites;
  public Sprite[] hardWaterSprites;
  public Sprite[] monarchSprites;
  [Header("Maps")]
  public Sprite[] _scaled_maps;
  public Sprite[] _maps;
  public Sprite[] _scaled_mapBgs;
  public Sprite[] mapBgs;
  public Sprite[] mapBgBig;
  [Header("Colors")]
  public Color[] DarkArcane;
  public Color[] ModColors;
  public Color[] colorsMain;
  public Color[] colorsSecondary;
  public Color[] colorsHair;
  public Color[] colorSkin;
  public Color[] colorOSRS;
  [Header("Extra")]
  public Sprite missingIcon;
  public Sprite[] _iconsPrestige;
  public Sprite[] _iconsAccountType;
  public Sprite[] _iconsIvite;
  public Sprite[] _iconsCanJoin;
  public Sprite[] _iconsSpectate;
  public Sprite[] _iconsRated;
  public Sprite[] _iconsTime;
  public Sprite[] _iconsMap;
  public Sprite[] _iconsMapBig;
  public Sprite[] _iconsTeam;
  public Sprite[] _iconsArcaneMod;
  public Sprite[] _iconsRandom;
  public Sprite[] _iconsForced;
  public Sprite[] _iconsElementals;
  public Sprite[] _iconsZeroShield;
  public Sprite[] _iconsChatFilter;
  public Sprite[] _gameTypes;
  public AnimateUIExplosion uiexplosion;
  public Sprite paperClip;
  public Sprite spriteBlack;
  public Sprite spellBGIconOld;
  public Sprite spellBGIconNew;
  public Sprite spellOverheadBGIconOld;
  public Sprite spellOverheadBGIconNew;
  public float[] lowTimePalette = new float[256]
  {
    0.0f,
    1f,
    2f,
    3f,
    4f,
    5f,
    6f,
    7f,
    8f,
    9f,
    10f,
    11f,
    12f,
    13f,
    14f,
    15f,
    16f,
    17f,
    18f,
    19f,
    20f,
    21f,
    22f,
    23f,
    24f,
    25f,
    26f,
    27f,
    28f,
    29f,
    30f,
    31f,
    32f,
    33f,
    34f,
    35f,
    36f,
    37f,
    38f,
    39f,
    40f,
    41f,
    42f,
    43f,
    44f,
    45f,
    46f,
    47f,
    48f,
    49f,
    50f,
    51f,
    52f,
    53f,
    54f,
    55f,
    56f,
    57f,
    58f,
    59f,
    60f,
    61f,
    62f,
    63f,
    64f,
    65f,
    66f,
    67f,
    68f,
    69f,
    70f,
    71f,
    72f,
    73f,
    74f,
    75f,
    76f,
    77f,
    78f,
    79f,
    80f,
    81f,
    82f,
    83f,
    84f,
    85f,
    86f,
    87f,
    88f,
    89f,
    90f,
    91f,
    92f,
    93f,
    94f,
    95f,
    96f,
    97f,
    98f,
    99f,
    100f,
    101f,
    102f,
    103f,
    104f,
    105f,
    106f,
    107f,
    108f,
    109f,
    110f,
    111f,
    112f,
    113f,
    114f,
    115f,
    116f,
    117f,
    118f,
    119f,
    120f,
    121f,
    122f,
    123f,
    124f,
    125f,
    126f,
    (float) sbyte.MaxValue,
    128f,
    129f,
    130f,
    131f,
    132f,
    133f,
    134f,
    135f,
    136f,
    137f,
    138f,
    139f,
    140f,
    141f,
    142f,
    143f,
    144f,
    145f,
    146f,
    147f,
    148f,
    149f,
    150f,
    151f,
    152f,
    153f,
    154f,
    155f,
    156f,
    157f,
    158f,
    159f,
    160f,
    161f,
    162f,
    163f,
    164f,
    165f,
    166f,
    167f,
    168f,
    169f,
    170f,
    171f,
    172f,
    173f,
    174f,
    175f,
    176f,
    177f,
    178f,
    179f,
    180f,
    181f,
    182f,
    183f,
    184f,
    185f,
    186f,
    187f,
    188f,
    189f,
    190f,
    191f,
    192f,
    193f,
    194f,
    195f,
    196f,
    197f,
    198f,
    199f,
    200f,
    201f,
    202f,
    203f,
    204f,
    205f,
    206f,
    207f,
    208f,
    209f,
    210f,
    211f,
    212f,
    213f,
    214f,
    215f,
    216f,
    217f,
    218f,
    219f,
    220f,
    221f,
    222f,
    223f,
    224f,
    225f,
    226f,
    227f,
    228f,
    229f,
    230f,
    231f,
    232f,
    233f,
    234f,
    235f,
    236f,
    237f,
    238f,
    239f,
    240f,
    241f,
    242f,
    243f,
    244f,
    245f,
    246f,
    247f,
    248f,
    249f,
    250f,
    251f,
    252f,
    253f,
    254f,
    (float) byte.MaxValue
  };
  public Material lowTimeMaterial;
  public GameObject[] ArchmageStaffs;
  public TMP_SpriteAsset spriteAsset;
  public OrderedDictionary<string, Sprite> icons = new OrderedDictionary<string, Sprite>();
  private List<GameObject> activePings = new List<GameObject>();
  private Dictionary<string, ClanOutfit> clanOutfits = new Dictionary<string, ClanOutfit>();
  private ClanOutfit tempOutfit;

  public static ClientResources Instance { get; set; }

  public Color[] GetColors(int i)
  {
    switch (i)
    {
      case 0:
        return this.colorsMain;
      case 1:
        return this.colorsHair;
      case 2:
        return this.colorSkin;
      case 3:
        return this.colorOSRS;
      default:
        return this.colorsMain;
    }
  }

  public void SpawnUIExplosion(int book, Vector3 pos)
  {
    int num = book * 12;
    for (int index = 0; index < 12; ++index)
    {
      if ((UnityEngine.Object) Inert.Instance.spells.GetItem(index + num + 1).Value.explosion != (UnityEngine.Object) null)
      {
        AnimateExplosioin component = Inert.Instance.spells.GetItem(index + num + 1).Value.explosion.GetComponent<AnimateExplosioin>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          AnimateUIExplosion animateUiExplosion = UnityEngine.Object.Instantiate<AnimateUIExplosion>(this.uiexplosion, Controller.Instance.transform);
          animateUiExplosion.transform.position = pos;
          animateUiExplosion.sp.sprite = component.GetComponent<SpriteRenderer>().sprite;
          animateUiExplosion.sprites = component.sprites;
          animateUiExplosion.timeToFinish = component.timeToFinish;
          break;
        }
      }
    }
  }

  internal void OnApplicationQuit()
  {
    this._matColorPalette.SetTexture("_PaletteTex", (Texture) this._texturePalette);
  }

  private void InitSpriteAsset()
  {
    if (this.spriteAsset.spriteInfoList.Count > 1)
      this.spriteAsset.spriteInfoList.RemoveRange(1, this.spriteAsset.spriteInfoList.Count - 1);
    foreach (KeyValuePair<string, Sprite> icon in this.icons)
    {
      if (this.spriteAsset.GetSpriteIndexFromName(icon.Key) == -1)
        this.add(this.spriteAsset, icon.Value);
    }
    this.spriteAsset.UpdateLookupTables();
  }

  private void add(TMP_SpriteAsset spriteAsset, Sprite sprite)
  {
    TMP_Sprite tmpSprite = new TMP_Sprite();
    Rect rect = sprite.rect;
    tmpSprite.name = sprite.name;
    tmpSprite.hashCode = TMP_TextUtilities.GetSimpleHashCode(tmpSprite.name);
    tmpSprite.x = rect.x;
    tmpSprite.y = rect.y;
    tmpSprite.width = rect.width;
    tmpSprite.height = rect.height;
    Vector2 vector2;
    ref Vector2 local = ref vector2;
    UnityEngine.Bounds bounds1 = sprite.bounds;
    double x1 = (double) bounds1.min.x;
    bounds1 = sprite.bounds;
    double num1 = (double) bounds1.extents.x * 2.0;
    double x2 = 0.0 - x1 / num1;
    UnityEngine.Bounds bounds2 = sprite.bounds;
    double y1 = (double) bounds2.min.y;
    bounds2 = sprite.bounds;
    double num2 = (double) bounds2.extents.y * 2.0;
    double y2 = 0.0 - y1 / num2;
    local = new Vector2((float) x2, (float) y2);
    tmpSprite.pivot = new Vector2((float) (0.0 - (double) vector2.x * (double) rect.width), rect.height - vector2.y * rect.height);
    int[] array = spriteAsset.spriteInfoList.Select<TMP_Sprite, int>((Func<TMP_Sprite, int>) (item => item.id)).ToArray<int>();
    int num3 = 0;
    for (int index = 0; index < array.Length && array[0] == 0; ++index)
    {
      if (index > 0 && array[index] - array[index - 1] > 1)
      {
        num3 = array[index - 1] + 1;
        break;
      }
      num3 = index + 1;
    }
    tmpSprite.id = num3;
    tmpSprite.xAdvance = rect.width;
    tmpSprite.scale = 1.2f;
    tmpSprite.xOffset = 0.0f;
    tmpSprite.yOffset = 32f;
    spriteAsset.spriteInfoList.Add(tmpSprite);
  }

  [ContextMenu("do")]
  public void doit()
  {
    int index1 = 187;
    int index2 = 144;
    while (index2 < 156)
    {
      Sprite spellicon = this._spellicons[index2];
      this._spellicons[index2] = this._spellicons[index1];
      this._spellicons[index1] = spellicon;
      ++index2;
      ++index1;
    }
  }

  private static void ArcColor(GameObject g, Recolor.Colors s)
  {
    Material material = g.GetComponent<SpriteRenderer>().material;
    material.SetColor("_ColorBlue", (Color) s.blue);
    material.SetColor("_ColorGreen", (Color) s.green);
    material.SetColor("_ColorRed", (Color) s.red);
    material.SetColor("_ColorGray", (Color) s.gray);
  }

  private static void Erase(SpriteRenderer disable)
  {
    Global.DestroySprite(disable.sprite);
    Texture2D texture = new Texture2D(2, 2);
    Color32[] pixels32 = texture.GetPixels32();
    for (int index = 0; index < pixels32.Length; ++index)
      pixels32[index] = (Color32) Color.clear;
    texture.SetPixels32(pixels32);
    texture.Apply();
    disable.sprite = Global.AddSprite(Sprite.Create(texture, new Rect(0.0f, 0.0f, 2f, 2f), new Vector2(0.5f, 0.5f)));
  }

  internal static void ChangeSprites(
    SpriteRenderer disable,
    AnimateRepeat anim,
    Sprite[] list,
    SettingsPlayer s,
    Outfit o)
  {
    if ((UnityEngine.Object) disable != (UnityEngine.Object) null)
      ClientResources.Erase(disable);
    for (int index = 0; index < list.Length; ++index)
      list[index] = s.coloring.Texture(list[index], o);
    anim.GetSpriteRenderer.sprite = list[0];
  }

  internal static void ChangeSprites(
    SpriteRenderer disable,
    AnimateRepeat anim,
    Sprite[] list,
    Sprite[] origList,
    Color[] colors)
  {
    if ((UnityEngine.Object) disable.sprite == (UnityEngine.Object) null || !disable.sprite.texture.isReadable)
      return;
    ClientResources.Erase(disable);
    if (list != null)
    {
      foreach (Sprite s in list)
        Global.DestroySprite(s);
    }
    if ((UnityEngine.Object) anim != (UnityEngine.Object) null)
      anim.deleteOnDestroy = true;
    for (int index = 0; index < list.Length; ++index)
      list[index] = Recolor.Texture(origList[index], colors[0], colors[1], colors[2], colors[3]);
    anim.GetSpriteRenderer.sprite = list[0];
  }

  public static void GetArchMageStaff(
    SettingsPlayer s,
    ZCreature c,
    ZPerson p,
    Account acc,
    bool destoryOld = true)
  {
    if (p.archMageStaffs != null & destoryOld)
    {
      foreach (GameObject archMageStaff in p.archMageStaffs)
      {
        if ((UnityEngine.Object) archMageStaff != (UnityEngine.Object) null)
        {
          foreach (SpriteRenderer componentsInChild in archMageStaff.GetComponentsInChildren<SpriteRenderer>())
            Global.DestroySprite(componentsInChild.sprite);
          UnityEngine.Object.Destroy((UnityEngine.Object) archMageStaff);
        }
      }
    }
    p.archMageStaffs = new List<GameObject>();
    if (acc.discord == 247247946557423616UL)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[33], c.body.transform.position + new Vector3(-1.45f, -4f), Quaternion.identity, c.body.transform);
      p.archMageStaffs.Add(gameObject);
    }
    if (s.indexRightHand == (byte) 27 || s.indexRightHand == (byte) 78)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[0], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-5.4f, 9f);
      gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) s.coloring.colors[0].green;
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 28)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[1], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-5.5f, 16.5f);
      p.archMageStaffs.Add(gameObject);
      ParticleSystem.MainModule main = gameObject.GetComponent<ParticleSystem>().main with
      {
        startColor = new ParticleSystem.MinMaxGradient((Color) s.coloring.colors[3].red, Color.white)
      };
      main = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main with
      {
        startColor = new ParticleSystem.MinMaxGradient((Color) s.coloring.colors[3].red, Color.white)
      };
    }
    else if ((int) s.indexBody == SettingsPlayer.sno_body2)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[1], c.body.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 22.5f);
      p.archMageStaffs.Add(gameObject);
      gameObject.GetComponent<ParticleSystem>().StartColor((Color) s.coloring.colors[0].green);
      gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().StartColor((Color) s.coloring.colors[0].green);
    }
    else if (s.indexRightHand == (byte) 87)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[3], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-1.86f, 10.59f);
      Material material = gameObject.GetComponent<SpriteRenderer>().material;
      material.SetColor("_ColorBlue", (Color) s.coloring.colors[3].blue);
      material.SetColor("_ColorGreen", (Color) s.coloring.colors[3].green);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 90)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[5], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-3.6f, -1.8f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 91)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[8], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-2.9f, 2.1f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 94)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[6], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-2f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 95)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[9], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-1.54f, 0.56f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 105)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[11], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(-2.8f, 15.5f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 111)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[14], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 112)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[15], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 113)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[18], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 114)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[17], c.rightArm.transform);
      g.transform.localPosition = new Vector3(0.0f, 0.0f);
      ClientResources.ArcColor(g, s.coloring.colors[3]);
      p.archMageStaffs.Add(g);
    }
    else if (s.indexRightHand == (byte) 96)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[23], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand == (byte) 98)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[21], c.rightArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexRightHand != (byte) 99)
    {
      if (s.indexRightHand == (byte) 118)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[24], c.rightArm.transform);
        gameObject.transform.localPosition = new Vector3(7.1f, -3.6f);
        gameObject.GetComponent<ParticleFlashLight>().target = c.clientObj;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 122)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[25], c.rightArm.transform);
        gameObject.transform.localPosition = new Vector3(-0.15f, -0.53f);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 130)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[26], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
        component.sprite = s.coloring.Texture(component.sprite, Outfit.RightHand);
        component.gameObject.tag = "RawImage";
        Sprite sprite = c.rightArm.sprite;
        c.rightArm.sprite = component.sprite;
        component.sprite = sprite;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand >= (byte) 137 && s.indexRightHand <= (byte) 141)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[27 + (int) s.indexRightHand - 137], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          ClientResources.ChangeSprites(c.rightArm, component, component.sprites, s, Outfit.RightHand);
          component.deleteOnDestroy = true;
        }
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 144)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[34], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites(c.rightArm, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 146)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[36], c.rightArm.transform.position + new Vector3(-4.69f, 15.5f), Quaternion.identity, c.rightArm.transform);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 151)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[40], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if ((int) s.indexRightHand == SettingsPlayer.cosmos_helper)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[43], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 155)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[45], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites(c.rightArm, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 156)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[42], c.rightArm.transform);
        gameObject.transform.localPosition = new Vector3(-3.07f, -3.51f);
        ClientResources.Erase(c.rightArm);
        c.rightArm.gameObject.tag = "Untagged";
        gameObject.GetComponent<SpriteRenderer>().sprite = s.coloring.Texture(gameObject.GetComponent<SpriteRenderer>().sprite, Outfit.RightHand);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 157)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[46], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 160)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[47], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 161)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[48], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 159)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[49], c.rightArm.transform);
        gameObject.transform.localPosition = new Vector3(-3.2f, -11f);
        gameObject.tag = "RawImage";
        gameObject.GetComponent<SpriteRenderer>().sprite = s.coloring.Texture(gameObject.GetComponent<SpriteRenderer>().sprite, Outfit.RightHand);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 163)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[50], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites(c.rightArm, component, component.sprites, s, Outfit.RightHand);
        component.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 170)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[53], c.rightArm.transform.position + new Vector3(-4.68f, -8.39f), Quaternion.identity, c.rightArm.transform);
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 174)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[51], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        SpriteRenderer component1 = gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        float a = component1.color.a;
        component1.color = (Color) s.coloring.colors[3].blue with
        {
          a = a
        };
        AnimateRepeat component2 = gameObject.GetComponent<AnimateRepeat>();
        ClientResources.ChangeSprites((SpriteRenderer) null, component2, component2.sprites, s, Outfit.RightHand);
        component2.deleteOnDestroy = true;
        p.archMageStaffs.Add(gameObject);
      }
      else if (s.indexRightHand == (byte) 180)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[54], c.rightArm.transform.position, Quaternion.identity, c.rightArm.transform);
        p.archMageStaffs.Add(gameObject);
      }
    }
    if (s.indexBeard == (byte) 98 || s.indexBeard2 == (byte) 98 || s.indexBeard3 == (byte) 98)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[52], c.body.transform.position + new Vector3(0.0f, -6.06f, 0.0f), Quaternion.identity, c.body.transform);
      AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
      Outfit o = s.indexBeard == (byte) 98 ? Outfit.Beard : (s.indexBeard2 == (byte) 98 ? Outfit.Beard2 : Outfit.Beard3);
      ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, o);
      gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = (Color) s.coloring.colors[(int) o].red;
      component.deleteOnDestroy = true;
      p.archMageStaffs.Add(gameObject);
    }
    if (s.indexLeftHand == (byte) 84)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[4], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(7f, 1f);
      Material material = gameObject.GetComponent<SpriteRenderer>().material;
      material.SetColor("_ColorBlue", (Color) s.coloring.colors[2].blue);
      material.SetColor("_ColorGreen", (Color) s.coloring.colors[2].green);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 85)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[7], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(10.9f, -3.6f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 87)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[10], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(-0.8f, 0.24f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 92)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[13], c.leftArm.transform);
      g.transform.localPosition = new Vector3(0.0f, 0.0f);
      ClientResources.ArcColor(g, s.coloring.colors[2]);
      c.leftArm.enabled = false;
      p.archMageStaffs.Add(g);
    }
    else if (s.indexLeftHand == (byte) 94)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[16], c.leftArm.transform);
      g.transform.localPosition = new Vector3(0.0f, 0.0f);
      ClientResources.ArcColor(g, s.coloring.colors[2]);
      p.archMageStaffs.Add(g);
    }
    else if (s.indexLeftHand == (byte) 96)
    {
      GameObject g = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[19], c.leftArm.transform);
      g.transform.localPosition = new Vector3(0.0f, 0.0f);
      ClientResources.ArcColor(g, s.coloring.colors[2]);
      c.leftArm.enabled = false;
      p.archMageStaffs.Add(g);
    }
    else if (s.indexLeftHand == (byte) 100)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[22], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 117)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[32], c.leftArm.transform.position, Quaternion.identity, c.leftArm.transform);
      AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
      ClientResources.ChangeSprites(c.leftArm, component, component.sprites, s, Outfit.LeftHand);
      component.deleteOnDestroy = true;
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 118)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[39], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
      AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
      ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.LeftHand);
      component.deleteOnDestroy = true;
    }
    else if (s.indexLeftHand == (byte) 119)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[35], c.leftArm.transform);
      gameObject.transform.localPosition = new Vector3(0.0f, 0.0f);
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 123)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[41], c.leftArm.transform.position, Quaternion.identity, c.leftArm.transform);
      AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
      ClientResources.ChangeSprites((SpriteRenderer) null, component, component.sprites, s, Outfit.LeftHand);
      component.deleteOnDestroy = true;
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 125)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[44], c.leftArm.transform.position, Quaternion.identity, c.leftArm.transform);
      AnimateRepeat component = gameObject.GetComponent<AnimateRepeat>();
      ClientResources.ChangeSprites(c.leftArm, component, component.sprites, s, Outfit.LeftHand);
      component.deleteOnDestroy = true;
      p.archMageStaffs.Add(gameObject);
    }
    else if (s.indexLeftHand == (byte) 132)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[55], c.leftArm.transform.position + new Vector3(-6.6f, 9.6f), Quaternion.identity, c.leftArm.transform);
      p.archMageStaffs.Add(gameObject);
    }
    if (s.animations != null)
    {
      for (int o = 0; o < 11; ++o)
      {
        if (s.animations[o] != null)
        {
          GameObject g = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[12], c.gameObject.GetComponent<ConfigurePlayer>().Get((Outfit) o).transform);
          g.transform.localPosition = Vector3.zero;
          AnimateRepeat component = g.GetComponent<AnimateRepeat>();
          component.sprites = s.animations[o].sprites.ToArray();
          g.GetComponent<SpriteRenderer>().sprite = component.sprites[0];
          ClientResources.ArcColor(g, s.coloring.colors[o]);
          component.UpdateFPS(s.metaData[o].fps);
          p.archMageStaffs.Add(g);
        }
      }
    }
    if (Client.game == null)
      return;
    if (Client.game?.gameFacts != null && (s.indexRightHand == (byte) 29 || s.indexRightHand == (byte) 67 || s.indexRightHand == (byte) 70))
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[2], Client.GetMapTransform());
      gameObject.transform.position = Client.game.gameFacts.settingsPlayer.Count != 1 ? new Vector3((float) (Client.map.Width / (Client.game.gameFacts.settingsPlayer.Count - 1) * (int) p.id), (float) (Client.map.Height + 1000)) : new Vector3(0.0f, 1000f);
      ArchmageStaff component = gameObject.GetComponent<ArchmageStaff>();
      component.target = c.clientObj;
      component.id = (int) p.id;
      Color green = (Color) s.coloring.colors[0].green with
      {
        a = 0.5f
      };
      gameObject.transform.GetComponent<SpriteRenderer>().color = green;
      p.archMageStaffs.Add(gameObject);
    }
    else if (Client.game?.gameFacts != null && s.indexRightHand == (byte) 66)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[37], c.transform.position, Quaternion.identity, c.transform);
      ArchmageStaffSiggy component = gameObject.GetComponent<ArchmageStaffSiggy>();
      component.target = c.clientObj;
      component.id = (int) p.id;
      p.archMageStaffs.Add(gameObject);
    }
    else if (Client.game?.gameFacts != null && s.indexRightHand == (byte) 173)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[38], new Vector3(c.transform.position.x, 1000f), Quaternion.identity, c.game.GetMapTransform());
      ArchmageStaffSiggy component = gameObject.GetComponent<ArchmageStaffSiggy>();
      component.target = c.clientObj;
      component.id = (int) p.id;
      component.p1.main.startColor = new ParticleSystem.MinMaxGradient((Color) s.coloring.colors[0].red, (Color) s.coloring.colors[0].green);
      p.archMageStaffs.Add(gameObject);
    }
    else if ((int) s.indexRightHand == SettingsPlayer.sno_pHand2)
    {
      float x = 0.0f;
      for (int index = 0; index < 3; index += 2)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.ArchmageStaffs[2], Client.GetMapTransform());
        gameObject.transform.position = new Vector3(x, 1000f);
        ArchmageStaff component = gameObject.GetComponent<ArchmageStaff>();
        component.target = c.clientObj;
        component.id = (int) p.id;
        component.triStaff = true;
        component.triIndex = index;
        if (index == 0)
        {
          Color red = (Color) s.coloring.colors[0].red with
          {
            a = 0.5f
          };
          gameObject.transform.GetComponent<SpriteRenderer>().color = red;
        }
        else if (index == 2)
        {
          Color blue = (Color) s.coloring.colors[0].blue with
          {
            a = 0.5f
          };
          gameObject.transform.GetComponent<SpriteRenderer>().color = blue;
        }
        x += 1920f;
        p.archMageStaffs.Add(gameObject);
      }
    }
    else if (s.indexRightHand == (byte) 100 && (UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null)
    {
      ArchWorkout archWorkout = c.rightArm.gameObject.AddComponent<ArchWorkout>();
      archWorkout.animator = c.animator;
      archWorkout.creature = c;
    }
    ClientResources.CheckNotScaled(s, c.clientObj);
  }

  public static void CheckNotScaled(SettingsPlayer s, Creature c)
  {
    if ((s.indexRightHand == (byte) 70 || s.indexRightHand == (byte) 77 || s.indexRightHand == (byte) 67 || s.indexRightHand == (byte) 97 || s.indexRightHand == (byte) 98 || s.indexRightHand == (byte) 120) && c.rightArm.gameObject.layer != 20)
      c.notScaled.Add(new Creature.NotScaled(c.animator, c.rightArm, s.indexRightHand == (byte) 120 ? -10f : 0.0f, Outfit.RightHand));
    if (s.indexLeftHand == (byte) 106 && c.leftArm.gameObject.layer != 20)
      c.notScaled.Add(new Creature.NotScaled(c.animator, c.leftArm, 12f, Outfit.LeftHand));
    if (s.indexBody != (byte) 100 || c.body.gameObject.layer == 20)
      return;
    c.notScaled.Add(new Creature.NotScaled(c.animator, c.body, 0.0f, Outfit.Body));
  }

  public Sprite GetSpellIcon(string s)
  {
    Sprite spellIcon = (Sprite) null;
    if (this.icons.TryGetValue(s, out spellIcon))
      return spellIcon;
    Debug.LogError((object) ("Missing Spell Icon: " + s));
    return this.missingIcon;
  }

  public static void Init(ClientResources x)
  {
    if ((UnityEngine.Object) ClientResources.Instance != (UnityEngine.Object) null)
      return;
    ClientResources.Instance = x;
    x._matColorPalette.SetTexture("_PaletteTex", (Texture) x._texturePalette);
    for (int index = 0; index < x._spellicons.Length; ++index)
      x.icons.Add(x._spellicons[index].name, x._spellicons[index]);
    for (int index = 0; index < x._previewOverrides.Length; ++index)
      x._overridePreviews.Add(x._previewOverrides[index].name.Substring("preview_".Length), x._previewOverrides[index]);
    foreach (Sprite sprite in x._largeUI)
      x.largeUI.Add(sprite.name, sprite);
    foreach (Sprite sprite in x._smallUI)
      x.smallUI.Add(sprite.name, sprite);
  }

  public void OnTouch(InputFieldPlus i)
  {
  }

  public void CreatePing(int type, string who, Color color, Vector2 pos)
  {
    if (!Global.GetPrefBool("prefmapping", true))
      return;
    for (int index = 0; index < this.activePings.Count; ++index)
    {
      if ((UnityEngine.Object) this.activePings[index] == (UnityEngine.Object) null)
        this.activePings.RemoveAt(index);
    }
    if (this.activePings.Count >= 4)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.activePings[0]);
      this.activePings.RemoveAt(0);
    }
    PfabMapPing pfabMapPing = UnityEngine.Object.Instantiate<PfabMapPing>(this.pfabMapPing, (Vector3) pos, Quaternion.identity, Client.game?.GetMapTransform());
    pfabMapPing.txt.text = who;
    pfabMapPing.txt.color = color;
    pfabMapPing.img.color = color;
    pfabMapPing.line.startColor = color;
    pfabMapPing.line.endColor = color;
    pfabMapPing.imgMiniMap.color = color;
    pfabMapPing.lineMiniMap.startColor = color;
    pfabMapPing.lineMiniMap.endColor = color;
    this.activePings.Add(pfabMapPing.gameObject);
  }

  public void SetClanOutfit(string clan, ClanOutfit outfit)
  {
    ClanOutfit clanOutfit;
    if (this.clanOutfits.TryGetValue(clan, out clanOutfit))
      clanOutfit.Dispose();
    this.clanOutfits[clan] = outfit;
  }

  public ClanOutfit GetClanOutfit(string clan)
  {
    return !string.IsNullOrEmpty(clan) && this.clanOutfits.TryGetValue(clan, out this.tempOutfit) ? this.tempOutfit : (ClanOutfit) null;
  }

  public void RecieveAllClanOutfits(myBinaryReader r)
  {
    foreach (KeyValuePair<string, ClanOutfit> clanOutfit in this.clanOutfits)
      clanOutfit.Value.Dispose();
    this.clanOutfits.Clear();
    int num = r.ReadInt32();
    for (int index = 0; index < num; ++index)
    {
      if (r.ReadByte() == (byte) 1)
      {
        string key = r.ReadString();
        ClanOutfit clanOutfit = ClanOutfit.Deserialize(r);
        this.clanOutfits[key] = clanOutfit;
        clanOutfit.ClientCreateSprites();
      }
    }
  }

  public static void ServerCompressOutfits()
  {
    if (Server._clans.Count == 0)
    {
      Server.compressedClanOutfits = (byte[]) null;
    }
    else
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        {
          w.Write((byte) 84);
          w.Write((byte) 5);
          w.Write(Server._clans.Count);
          foreach (KeyValuePair<string, Clan> clan in Server._clans)
          {
            w.Write(clan.Value.outfit == null ? (byte) 0 : (byte) 1);
            if (clan.Value.outfit != null)
            {
              w.Write(clan.Key);
              clan.Value.outfit.Serialize(w);
            }
          }
        }
        Server.compressedClanOutfits = memoryStream.ToArray();
      }
    }
  }

  public static void ServerCompressRatedRestrictions()
  {
    Server.compressedRatedRestrictions = ClientResources.SerializeRestricitons();
  }

  public static byte[] SerializeRestricitons()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 95);
        w.Write(Server._restrictions == null ? 0 : 1);
        w.Write((byte) 3);
        w.Write(Server._defaultRatedFacts == null ? 0 : 1);
        if (Server._restrictions != null)
          Server._restrictions.Serialize(w);
        if (Server._defaultRatedFacts != null)
          Server._defaultRatedFacts.Serialize(w);
      }
      return memoryStream.ToArray();
    }
  }

  public static void DeserializeRestrictions(myBinaryReader r)
  {
    int version1 = r.ReadInt32();
    byte version2 = r.ReadByte();
    int num = r.ReadInt32() == 1 ? 1 : 0;
    if (version1 > 0)
      Server._restrictions = Restrictions.Deserialize(r, (byte) version1);
    if (num == 0)
      return;
    Server._defaultRatedFacts = RatedFacts.Deserialize((int) version2, r);
  }

  public static void SaveRestrictions()
  {
    if (Server.compressedRatedRestrictions == null)
      return;
    File.WriteAllBytes(Application.persistentDataPath + "/restritions.res", Server.compressedRatedRestrictions);
  }

  public static void LoadRestrictions()
  {
    try
    {
      if (File.Exists(Application.persistentDataPath + "/restritions.res"))
      {
        using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(Application.persistentDataPath + "/restritions.res")))
        {
          using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
          {
            int num1 = (int) myBinaryReader.ReadByte();
            int version1 = myBinaryReader.ReadInt32();
            byte version2 = myBinaryReader.ReadByte();
            int num2 = myBinaryReader.ReadInt32() == 1 ? 1 : 0;
            Server._restrictions = version1 == 0 ? (Restrictions) null : Restrictions.Deserialize(myBinaryReader, (byte) version1);
            Server._defaultRatedFacts = num2 == 0 ? (RatedFacts) null : RatedFacts.Deserialize((int) version2, myBinaryReader);
          }
        }
      }
    }
    catch (Exception ex)
    {
      Server._restrictions = (Restrictions) null;
      Server._defaultRatedFacts = (RatedFacts) null;
      Debug.LogError((object) ex);
    }
    ClientResources.ServerCompressRatedRestrictions();
  }

  public static void LoadRandomRestrictions()
  {
    try
    {
      if (!File.Exists(Application.persistentDataPath + "/randomSpellRestrictions.resrng"))
        return;
      using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(Application.persistentDataPath + "/randomSpellRestrictions.resrng")))
      {
        using (myBinaryReader w = new myBinaryReader((Stream) memoryStream))
        {
          int num1 = (int) w.ReadByte();
          int version1 = w.ReadInt32();
          int version2 = w.ReadInt32();
          if (version1 > 0)
          {
            int num2 = (int) w.ReadByte();
            Server._casinoRestrictions = Restrictions.Deserialize(w, (byte) version1);
          }
          if (version2 <= 0)
            return;
          int num3 = (int) w.ReadByte();
          Server._randomSpellRestrictions = Restrictions.Deserialize(w, (byte) version2);
        }
      }
    }
    catch (Exception ex)
    {
      Server._casinoRestrictions = (Restrictions) null;
      Server._randomSpellRestrictions = (Restrictions) null;
      Debug.LogError((object) ex);
    }
  }

  [Serializable]
  public class TutorialData
  {
    public string name;
    public TextAsset data;
    public bool scriptOnly;
  }
}
