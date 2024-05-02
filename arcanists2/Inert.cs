// Decompiled with JetBrains decompiler
// Type: Inert
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using mattmc3.dotmore.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class Inert : MonoBehaviour
{
  public static string Version = "v6.6";
  public static int _Version = 61;
  public Spell[] _spells;
  public Spell[] holidaySpells;
  public Spell[] MinionSpells;
  [Tooltip("Spells that can be added by name, but not in a spellbook")]
  public Spell[] _Additionalspells;
  public List<Spell> _otherspells;
  public Creature[] _creatures;
  public Tower[] _towers;
  public GameObject[] _otherObjects;
  internal Dictionary<string, Creature> Creatures = new Dictionary<string, Creature>();
  internal Dictionary<string, Tower> Towers = new Dictionary<string, Tower>();
  public Spell[] ArmageddonObjects;
  public Sprite[] ArmageddonIcons;
  public MyCollider basicCircleCollider;
  public Creature baseLeafCreature;
  public List<MyCollider> cachedBlitColliders;
  internal List<ZMyCollider> cachedBlitCollidersZ;
  public GameObject Player_Character_GO;
  public Texture2D[] cutouts;
  public Texture2D acornSappling;
  public Texture2D leafTexture;
  public Texture2D leafTextureReverse;
  public GameObject overheadCanvas;
  public Texture2D[] _volcanoTex;
  public Creature bee;
  public SpellSlot waterGate;
  public GameObject electricityPool;
  public Effector moneyBags;
  public Effector audioBags;
  public Spell Seasonal;
  public Spell LightningStrike;
  public Spell Snow;
  public Spell Snow2;
  public Spell rain;
  public Spell acidRain;
  public Spell ZombieBreath;
  public Spell chargePaladin;
  public Spell risingStarNormal;
  public Spell risingStarHuge;
  public Spell Arrow;
  public Spell VineBloomThorn;
  public Spell VineBloomVine;
  public Spell maelstromDrop;
  public Spell brineBolt;
  public Spell oceanDrop1;
  public Spell oceanDrop2;
  public Spell thornBall;
  public Spell clamOceanFloor;
  public Spell seaSalt;
  public Spell cog;
  public Effector infectedBloodAura;
  public GameObject frostGiantBlizzard;
  public GameObject lichAuraOfDecay;
  public Spell arcaneMist;
  public Spell arcaneDragon;
  public Spell corruptDragon;
  public Texture2D iceBlockTex;
  public Texture2D gargoyleStoneTex;
  public Texture2D gargoyleStoneTexReversed;
  public MudBallObject mudBallObject;
  public Texture2D[] flowerTextures;
  public FlowerBallObject flower0;
  public FlowerBallObject flower1;
  public FlowerBallObject flower2;
  public FlowerBallObject flower3;
  public FlowerBallObject flower4;
  public GameObject ducks;
  public GameObject indicatorOfDecay;
  public GameObject auraOfDecayExplosion;
  public GameObject auraOfArcaneExplosion;
  public GameObject auraOfBloodExplosion;
  public GameObject auraOfSeasonsExplosion;
  public GameObject oceanWaves;
  public GameObject oceanSplash;
  public GameObject vineExplosion;
  public GameObject fountain;
  public GameObject protectionShieldObj;
  public Sprite megaBoulderSprite;
  public GameObject[] familiars;
  public FixedAnimationCurve genericCurveFixed;
  public FixedAnimationCurve genericCurveMin50;
  public FixedAnimationCurve NoneCurveFixed;
  public FixedAnimationCurve LinearCurveFixed;
  public FixedAnimationCurve ShockbombCurveFixed;
  public FixedAnimationCurve DropOffFixed;
  public FixedAnimationCurve ZeroDamage;
  internal int presentIndex;
  public OrderedDictionary<string, Spell> spells = new OrderedDictionary<string, Spell>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  public OrderedDictionary<SpellEnum, Spell> spellsEnums = new OrderedDictionary<SpellEnum, Spell>();
  public OrderedDictionary<string, Effector> baseEffectors = new OrderedDictionary<string, Effector>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  public OrderedDictionary<string, MyCollider> baseColliders = new OrderedDictionary<string, MyCollider>((IEqualityComparer<string>) Server._caseInsensitiveComparer);
  public static byte[] Version_As_Bytes;
  public List<MinerMarket.Item> minerMarket;
  public static int mask_Jar = 262144;
  public static int mask_ButterflyJar = 2097152;
  public static int mask_Phantom = 65536;
  public static int mask_entity_movement = 43776;
  public static int mask_spell_movement = 8448;
  public static int mask_movement_NoEffector = 8448;
  public static int mask_all = Inert.mask_movement_NoEffector | 1024 | 2048 | 512 | 65536 | Inert.mask_Jar;

  [ContextMenu("Apply Curves")]
  private void curves()
  {
    this.genericCurveFixed.Apply();
    this.genericCurveMin50.Apply();
    this.NoneCurveFixed.Apply();
    this.LinearCurveFixed.Apply();
    this.ShockbombCurveFixed.Apply();
    this.DropOffFixed.Apply();
  }

  public static Inert Instance { get; set; }

  public static Spell GetSpell(string s)
  {
    Spell spell1 = (Spell) null;
    if (Inert.Instance.spells.TryGetValue(s, out spell1))
      return spell1;
    Spell spell2 = Inert.Instance._otherspells.Find((Predicate<Spell>) (x => string.Equals(x.name, s, StringComparison.OrdinalIgnoreCase)));
    return (UnityEngine.Object) spell2 != (UnityEngine.Object) null ? spell2 : (Spell) null;
  }

  public static Spell GetSpell(SpellEnum s)
  {
    Spell spell = (Spell) null;
    return Inert.Instance.spellsEnums.TryGetValue(s, out spell) ? spell : (Spell) null;
  }

  public static MyCollider GetBaseCollider(string n)
  {
    MyCollider baseCollider;
    Inert.Instance.baseColliders.TryGetValue(n, out baseCollider);
    return baseCollider;
  }

  public static Effector GetBaseEffector(string n)
  {
    if (string.IsNullOrEmpty(n))
      return (Effector) null;
    Effector baseEffector;
    Inert.Instance.baseEffectors.TryGetValue(n, out baseEffector);
    return baseEffector;
  }

  public static Creature GetCreature(string n)
  {
    Creature creature;
    Inert.Instance.Creatures.TryGetValue(n, out creature);
    return creature;
  }

  public bool TryGetSpell(string n, out Spell s)
  {
    s = this._otherspells.Find((Predicate<Spell>) (x => string.Equals(x.name, n, StringComparison.OrdinalIgnoreCase)));
    return (UnityEngine.Object) s != (UnityEngine.Object) null;
  }

  public void Splash(Vector3 pos, float radius)
  {
    float num = (float) (((double) radius - 13.0) / 94.0 + 0.5);
    UnityEngine.Object.Instantiate<GameObject>(Inert.Instance.oceanSplash, pos, Quaternion.identity).transform.localScale = new Vector3(num, num, num);
  }

  public static void Init(Inert x)
  {
    if ((UnityEngine.Object) Inert.Instance != (UnityEngine.Object) null)
      return;
    Inert.Instance = x;
    for (int index = 0; index < x._spells.Length; ++index)
    {
      x._spells[index].spellIndex = index;
      x.spells.Add(x._spells[index].name, x._spells[index]);
      x.spellsEnums.Add(x._spells[index].spellEnum, x._spells[index]);
    }
    for (int index = 0; index < x._Additionalspells.Length; ++index)
    {
      x._Additionalspells[index].spellIndex = index + x._spells.Length;
      x.spells.Add(x._Additionalspells[index].name, x._Additionalspells[index]);
      x.spellsEnums.Add(x._Additionalspells[index].spellEnum, x._Additionalspells[index]);
    }
    for (int index = 0; index < x.holidaySpells.Length; ++index)
    {
      if (!x.spells.ContainsKey(x.holidaySpells[index].name))
      {
        x.holidaySpells[index].spellIndex = index + x._spells.Length + x._Additionalspells.Length;
        x.spells.Add(x.holidaySpells[index].name, x.holidaySpells[index]);
        x.spellsEnums.Add(x.holidaySpells[index].spellEnum, x.holidaySpells[index]);
      }
    }
    x.presentIndex = x.spells.Count;
    for (int index = 0; index < x._otherspells.Count; ++index)
    {
      if (!x.spells.ContainsKey(x._otherspells[index].name))
      {
        x.spells.Add(x._otherspells[index].name, x._otherspells[index]);
        if (!x.spellsEnums.ContainsKey(x._otherspells[index].spellEnum))
          x.spellsEnums.Add(x._otherspells[index].spellEnum, x._otherspells[index]);
      }
    }
    foreach (Creature creature in x._creatures)
    {
      x.Creatures.Add(creature.name, creature);
      foreach (SpellSlot spell in creature.stats.spells)
      {
        if (!x.spells.ContainsKey(spell.spell.name))
          x.spells.Add(spell.spell.name, spell.spell);
        if (!x.spellsEnums.ContainsKey(spell.spell.spellEnum))
          x.spellsEnums.Add(spell.spell.spellEnum, spell.spell);
      }
    }
    foreach (GameObject otherObject in x._otherObjects)
    {
      foreach (Effector componentsInChild in otherObject.GetComponentsInChildren<Effector>())
      {
        if (!Inert.Instance.baseEffectors.ContainsKey(componentsInChild.name))
          Inert.Instance.baseEffectors.Add(componentsInChild.name, componentsInChild);
      }
      foreach (MyCollider componentsInChild in otherObject.GetComponentsInChildren<MyCollider>())
      {
        if (!Inert.Instance.baseColliders.ContainsKey(componentsInChild.name))
          Inert.Instance.baseColliders.Add(componentsInChild.name, componentsInChild);
      }
    }
    foreach (Tower tower in x._towers)
      x.Towers.Add(tower.name, tower);
    x.SetDefaultStats();
  }

  public void SetDefaultStats()
  {
    foreach (KeyValuePair<string, Spell> spell in this.spells)
      Inert.InitSpell(spell.Value);
    foreach (Creature creature in this._creatures)
      Inert.InitCreature(creature);
  }

  public void Apply(SpellOverrides o)
  {
    if (SpellOverrides.activeObj == o)
      return;
    SpellOverrides.activeObj = o;
    this.SetDefaultStats();
    if (o == null)
      return;
    foreach (SpellOverrides.ASpell listSpell in o.listSpells)
    {
      Spell spell;
      if (this.spells.TryGetValue(listSpell.Name, out spell))
      {
        string json = JsonUtility.ToJson((object) listSpell.stats);
        spell.runTimeStats = JsonUtility.FromJson<SpellStats>(json);
        if (string.IsNullOrWhiteSpace(spell.runTimeStats.description))
          spell.runTimeStats.description = Descriptions._getSpellDescriptionRaw(spell.name);
      }
    }
    foreach (SpellOverrides.ACreature listCreature in o.listCreatures)
    {
      SpellOverrides.ACreature x = listCreature;
      Creature creature = ((IEnumerable<Creature>) this._creatures).FirstOrDefault<Creature>((Func<Creature, bool>) (z => string.Equals(x.Name, z.name)));
      if ((UnityEngine.Object) creature != (UnityEngine.Object) null)
      {
        string json = JsonUtility.ToJson((object) x.stats);
        creature.runTimeStats = JsonUtility.FromJson<CreatureStats>(json);
        creature.runTimeStats.spells = new List<SpellSlot>();
      }
    }
  }

  public static void InitCreature(Creature c)
  {
    string json = JsonUtility.ToJson((object) c.stats);
    c.runTimeStats = JsonUtility.FromJson<CreatureStats>(json);
    c.runTimeStats.spells = new List<SpellSlot>();
    foreach (SpellSlot spell in c.stats.spells)
      c.runTimeStats.spells.Add(new SpellSlot(spell));
    foreach (Effector componentsInChild in c.GetComponentsInChildren<Effector>())
    {
      if (!Inert.Instance.baseEffectors.ContainsKey(componentsInChild.name))
        Inert.Instance.baseEffectors.Add(componentsInChild.name, componentsInChild);
    }
    foreach (MyCollider componentsInChild in c.GetComponentsInChildren<MyCollider>())
    {
      if (!Inert.Instance.baseColliders.ContainsKey(componentsInChild.name))
        Inert.Instance.baseColliders.Add(componentsInChild.name, componentsInChild);
    }
  }

  public static void InitSpell(Spell s)
  {
    string json = JsonUtility.ToJson((object) s.stats);
    s.runTimeStats = JsonUtility.FromJson<SpellStats>(json);
    s.runTimeStats.description = Descriptions._getSpellDescriptionRaw(s.name);
    foreach (Effector componentsInChild in s.GetComponentsInChildren<Effector>())
    {
      if (!Inert.Instance.baseEffectors.ContainsKey(componentsInChild.name))
        Inert.Instance.baseEffectors.Add(componentsInChild.name, componentsInChild);
    }
    foreach (MyCollider componentsInChild in s.GetComponentsInChildren<MyCollider>())
    {
      if (!Inert.Instance.baseColliders.ContainsKey(componentsInChild.name))
        Inert.Instance.baseColliders.Add(componentsInChild.name, componentsInChild);
    }
    if (!((UnityEngine.Object) s.toSummon != (UnityEngine.Object) null))
      return;
    foreach (Effector componentsInChild in s.toSummon.GetComponentsInChildren<Effector>())
    {
      if (!Inert.Instance.baseEffectors.ContainsKey(componentsInChild.name))
        Inert.Instance.baseEffectors.Add(componentsInChild.name, componentsInChild);
    }
    foreach (MyCollider componentsInChild in s.toSummon.GetComponentsInChildren<MyCollider>())
    {
      if (!Inert.Instance.baseColliders.ContainsKey(componentsInChild.name))
        Inert.Instance.baseColliders.Add(componentsInChild.name, componentsInChild);
    }
  }

  private void DestroyAllSprites()
  {
  }

  private void DestroyThings(Creature x)
  {
    if ((UnityEngine.Object) x == (UnityEngine.Object) null)
      return;
    IAnimator component = x.GetComponent<IAnimator>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) component);
    SpriteRenderer[] componentsInChildren = x.gameObject.GetComponentsInChildren<SpriteRenderer>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if ((UnityEngine.Object) componentsInChildren[index].sprite != (UnityEngine.Object) null && (UnityEngine.Object) componentsInChildren[index].sprite.texture != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) componentsInChildren[index].sprite.texture);
      componentsInChildren[index].DestroyThis();
    }
  }

  private void Awake()
  {
    Inert.Init(this);
    if (!Application.isBatchMode)
      return;
    this.DestroyAllSprites();
  }

  private void OnDestroy() => Inert.Instance = (Inert) null;

  public static FixedAnimationCurve GetCurve(Curve v)
  {
    switch (v)
    {
      case Curve.Generic:
        return Inert.Instance.genericCurveFixed;
      case Curve.None:
        return Inert.Instance.NoneCurveFixed;
      case Curve.Linear:
        return Inert.Instance.LinearCurveFixed;
      case Curve.Shockbomb:
        return Inert.Instance.ShockbombCurveFixed;
      case Curve.DropOff:
        return Inert.Instance.DropOffFixed;
      case Curve.Generic50:
        return Inert.Instance.genericCurveMin50;
      case Curve.Zero:
        return Inert.Instance.ZeroDamage;
      default:
        return (FixedAnimationCurve) null;
    }
  }

  public static void AddBG(ZCreature sum)
  {
    if ((UnityEngine.Object) sum.bg != (UnityEngine.Object) null)
    {
      if (!Global.CompareColors((Color32) sum.bg.color, (Color32) Color.white))
        return;
    }
    else
    {
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.bg_glow, sum.transform.position, Quaternion.identity, sum.transform);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.minimap_glow, sum.transform.position, Quaternion.identity, sum.transform);
      float num1 = sum.BGScale();
      gameObject1.transform.localScale = new Vector3(num1, num1, 1f);
      float num2 = sum.MiniBGScale();
      gameObject2.transform.localScale = new Vector3(num2, num2, 1f);
      sum.clientObj.miniMapBg = gameObject2.GetComponent<SpriteRenderer>();
      sum.clientObj.bg = gameObject1.GetComponent<SpriteRenderer>();
    }
    sum.bg.color = sum.parent.clientColor;
    if ((double) sum.bg.color.a < 1.0)
    {
      Color color = sum.bg.color with { a = 1f };
      sum.bg.color = color;
    }
    if ((UnityEngine.Object) sum.miniMapBg != (UnityEngine.Object) null)
      sum.miniMapBg.color = sum.bg.color;
    if (!((UnityEngine.Object) sum.christmasHat != (UnityEngine.Object) null))
      return;
    sum.christmasHat.SetActive(DateTime.Now.Month == 12 || sum.parent != null && (sum.parent.ActivateableFamiliar == BookOf.Frost || sum.familiarLevelFrost > 0) && (sum.parent.settingsPlayer.indexHat == (byte) 69 || sum.parent.settingsPlayer.indexHat == (byte) 70));
  }

  public static void AddOverheadCanvas(Creature c)
  {
    if (!c.serverObj.game.isClient || (UnityEngine.Object) c.overheadCanvas != (UnityEngine.Object) null)
      return;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Inert.Instance.overheadCanvas, (Vector3) c.serverObj.ClientOverHeadOffset.ToSinglePrecision() + c.transform.position, Quaternion.identity, c.transform);
    c.overheadCanvas = gameObject.GetComponent<Canvas>();
    c.selectedSpellPanel = gameObject.transform.GetChild(0).gameObject;
    c.selectedSpellIcon = c.selectedSpellPanel.transform.GetChild(0).GetComponent<Image>();
    c.txtHealth = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
    c.imgPrestige = gameObject.transform.GetChild(2).GetComponent<Image>();
    c.txtShield = gameObject.transform.GetChild(3).GetComponent<TMP_Text>();
    if (!c.serverObj.isPawn && c.parent != null)
      c.serverObj.prestigeIndex = (int) c.parent.account.prestige;
    c.serverObj.ColorPrestigeHat();
    if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null && HUD.instance.hideOverheadIcons)
      c.overheadCanvas?.gameObject.SetActive(false);
    if (c.parent != null)
      c.txtHealth.color = c.parent.clientColor;
    Vector3 vector3 = new Vector3((double) c.serverObj.scale > 0.0 ? 1f : -1f, 1f, 1f);
    c.overheadCanvas.transform.localScale = new Vector3(((double) c.serverObj.scale > 0.0 ? 1f : -1f) / c.serverObj.scale, 1f / c.serverObj.scale, 1f);
  }

  public static Quaternion RotationOfVelocity(MyLocation velocity)
  {
    return Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(velocity.y.ToFloat(), velocity.x.ToFloat()) * 57.29578f);
  }

  public static Quaternion RotationOfVelocity(Vector2 diff)
  {
    return Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(diff.y, diff.x) * 57.29578f);
  }

  public static FixedInt AngleOfVectors(MyLocation a, MyLocation b)
  {
    return FixedInt.Atan2(a.y - b.y, a.x - b.x) * FixedInt.Rad2Deg;
  }

  public static FixedInt AngleOfVelocity(MyLocation diff)
  {
    return FixedInt.Atan2(diff.y, diff.x) * FixedInt.Rad2Deg;
  }

  public static MyLocation Velocity(FixedInt angle)
  {
    FixedInt x = angle * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x), FixedInt.Sin(x));
  }

  public static MyLocation Velocity(FixedInt angle, int force)
  {
    FixedInt x = angle * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force);
  }

  public static MyLocation Velocity(FixedInt angle, FixedInt force)
  {
    FixedInt x = angle * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force);
  }

  public static MyLocation Velocity2(FixedInt angle, FixedInt force)
  {
    FixedInt x = (angle + 90) * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force);
  }

  public static MyLocation Velocity(FixedInt angle, FixedInt force, MyLocation already)
  {
    FixedInt x = (angle + 90) * FixedInt.Deg2Rad;
    return new MyLocation(FixedInt.Cos(x) * force, FixedInt.Sin(x) * force) + already;
  }

  public static SettingsPlayer DefaultCharacterSetup()
  {
    SettingsPlayer settingsPlayer = new SettingsPlayer();
    for (byte index = 0; index < (byte) 16; ++index)
      settingsPlayer.spells[(int) index] = index;
    return settingsPlayer;
  }

  public static void AddTorquingAndOrArchStaffs(
    ZPerson p,
    ZCreature creature,
    SettingsPlayer sp,
    bool first,
    bool destroyOld = true)
  {
    if ((int) sp.indexBody == SettingsPlayer.sno_body2 || sp.indexRightHand == (byte) 54 && p.account.discord == 247247946557423616UL || (int) sp.indexRightHand == SettingsPlayer.sno_pHand || sp.indexLeftHand == (byte) 67 || sp.indexRightHand == (byte) 103 || sp.indexRightHand == (byte) 104)
    {
      GameObject gameObject = creature.transform.GetChild(0).gameObject;
      gameObject.transform.rotation = Quaternion.identity;
      Torquing torquing = gameObject.GetComponent<Torquing>();
      if ((UnityEngine.Object) torquing == (UnityEngine.Object) null)
        torquing = gameObject.AddComponent<Torquing>();
      torquing.goback = true;
      torquing.creature = creature;
      p.torquing = torquing;
    }
    else if (!first)
    {
      creature.transform.GetChild(0).rotation = Quaternion.identity;
      if ((UnityEngine.Object) p.torquing != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) p.torquing);
        p.torquing = (Torquing) null;
      }
    }
    ClientResources.GetArchMageStaff(sp, creature, p, p.account, destroyOld);
  }

  public static void CreateStuff(
    ZCreature creature,
    ZPerson p,
    SettingsPlayer sp,
    bool destroyOld = true)
  {
    if (p.game.isClient)
    {
      ConfigurePlayer component = creature.gameObject.GetComponent<ConfigurePlayer>();
      creature.bg.color = p.clientColor;
      creature.miniMapBg = creature.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
      creature.miniMapBg.color = p.clientColor;
      string name = p.name;
      SettingsPlayer settingsPlayer = sp;
      component.EquipAll(name, settingsPlayer);
      if (!p.game.isSandbox)
        creature.mouth.sprite = ClientResources.Instance._characterMouths[(int) sp.indexMouth];
      Inert.AddTorquingAndOrArchStaffs(p, creature, sp, true, destroyOld);
      Inert.AddOverheadCanvas(creature.clientObj);
    }
    if (sp.indexRightHand == (byte) 105)
    {
      creature.moneyBags = true;
    }
    else
    {
      if (sp.indexRightHand != (byte) 155)
        return;
      creature.moneyBags = true;
      creature.audioBags = true;
    }
  }

  public static ZCreature CreateCharacter(
    ZPerson p,
    SettingsPlayer sp,
    MyLocation pos,
    int index,
    bool onPlayerPanel = true,
    bool noSpells = false)
  {
    if (p.game.isClient)
      p.clientColor = (int) sp.indexBody != SettingsPlayer.sno_body2 ? TeamColors.GetColor(index) : new Color(0.5f, 0.5f, 0.5f);
    ZCreature creature = ZCreatureCreate.CreateCreature(p, Inert.Instance.Player_Character_GO.GetComponent<Creature>(), pos.ToSinglePrecision(), Quaternion.identity, p.game.GetMapTransform());
    creature.game = p.game;
    creature.parent = p;
    creature.collider.Initialize(pos, p.game.world);
    creature.collider.creature = creature;
    creature.position = pos;
    sp.indexMouth = (byte) p.game.RandomInt(0, 24);
    if (sp._spells.SeasonsIsHoliday)
      p.seasonISHoliday = true;
    Inert.CreateStuff(creature, p, sp);
    if (p.game.isClient && onPlayerPanel)
      HUD.instance.AddPanelPlayer(creature);
    if (noSpells)
      return creature;
    for (int index1 = 0; index1 < 16; ++index1)
    {
      if (sp.spells[index1] < byte.MaxValue && Inert.Instance._spells.Length > (int) sp.spells[index1])
      {
        SpellSlot spellSlot = new SpellSlot(!p.seasonISHoliday || sp.spells[index1] < (byte) 120 || sp.spells[index1] > (byte) 131 ? Inert.Instance._spells[(int) sp.spells[index1]] : Inert.Instance.holidaySpells[(int) sp.spells[index1] - 120]);
        creature.spells.Add(spellSlot);
      }
    }
    if (p.game.gameFacts.GetStyle().HasStyle(GameStyle.Random_Spells))
    {
      for (int index2 = creature.spells.Count - 1; index2 >= 0; --index2)
      {
        if (creature.spells[index2].spell.spellEnum == SpellEnum.Flurry)
        {
          if (!creature.HasSpell(SpellEnum.Summon_Elves))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Summon Elves"]);
            creature.spells.Insert(index2, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Vine_Bloom)
        {
          if (!creature.HasSpell(SpellEnum.Summon_Man_Trap))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Summon Man-Trap"]);
            creature.spells.Insert(index2, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Calling_Bell)
        {
          if (!creature.HasSpell(SpellEnum.Recall_Device))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Recall Device"]);
            creature.spells.Insert(index2, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Summon_Elves)
        {
          if (!creature.HasSpell(SpellEnum.Flurry))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Flurry"]);
            creature.spells.Insert(index2 + 1, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Summon_Man_Trap)
        {
          if (!creature.HasSpell(SpellEnum.Vine_Bloom))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Vine Bloom"]);
            creature.spells.Insert(index2 + 1, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Recall_Device)
        {
          if (!creature.HasSpell(SpellEnum.Calling_Bell))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Calling Bell"]);
            creature.spells.Insert(index2 + 1, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Imp_Destruction)
        {
          if (!creature.HasSpell(SpellEnum.Summon_Imps))
          {
            SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Summon Imps"]);
            creature.spells.Insert(index2, spellSlot);
          }
        }
        else if (creature.spells[index2].spell.spellEnum == SpellEnum.Arcane_Energiser && !creature.HasSpell(SpellEnum.Arcane_Arrow))
        {
          SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells["Arcane Arrow"]);
          creature.spells.Insert(index2, spellSlot);
        }
      }
    }
    return creature;
  }

  public static void LoadSettingsPlayer()
  {
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "Settings.settings";
    bool flag = false;
    if (File.Exists(path))
    {
      using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(path)))
      {
        using (myBinaryReader r = new myBinaryReader((Stream) memoryStream))
        {
          try
          {
            Client.settingsPlayer.Deserialize(r);
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ex);
            Client.settingsPlayer = new SettingsPlayer();
            flag = true;
          }
          try
          {
            Client._ratedFacts = RatedContainer.Deserialize(r);
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ex);
            Client._ratedFacts = new RatedContainer();
            Client._ratedFacts.AddDefault();
            flag = true;
          }
          try
          {
            byte version = r.ReadByte();
            Client._gameSettings.Deserialize(r, version);
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ex);
            Client._gameSettings = new GameSettings();
            flag = true;
          }
        }
      }
    }
    Client.TempIgnored.Load();
    Inert.LoadSpellOverrides();
    ColorScheme.forcedGraphics = (ColorScheme.ForcedGraphics) PlayerPrefs.GetInt("prefforcedgraphics", 0);
    Client.colorScheme = ColorScheme.FromFile(ColorScheme.defaultScheme);
    Client.colorScheme?.SetColors();
    if (!flag)
      return;
    Inert.SaveSettingsPlayer();
  }

  public static void SaveSettingsPlayer()
  {
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "Settings.settings";
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        Client.settingsPlayer.Serialize(w);
        Client._ratedFacts.Serialize(w);
        w.Write((byte) 4);
        Client._gameSettings.Serialize(w);
      }
      File.WriteAllBytes(path, memoryStream.ToArray());
    }
  }

  public static void LoadSpellOverrides()
  {
    try
    {
      SpellOverrides o = SpellOverrides.FromFile(SpellOverrides.SERVER_PATH);
      if (o == null)
        return;
      SpellOverrides.serverObj = o;
      Inert.Instance.Apply(o);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  [ContextMenu("Gen Polygons")]
  private void genpolygons()
  {
    StringBuilder stringBuilder1 = new StringBuilder("[System.Serializable]\npublic enum CachedPolygonEnums {\n");
    using (FileStream fileStream = new FileStream("CachedPolygons.txt", FileMode.Create))
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream))
      {
        streamWriter.WriteLine("public static class StaticPolygons");
        streamWriter.WriteLine("{");
        streamWriter.WriteLine("public static bool Inside(CachedPolygonEnums e, int x, int y)");
        streamWriter.WriteLine("{");
        streamWriter.WriteLine("    return polygons[(int)e].Inside(x, y);");
        streamWriter.WriteLine("}");
        streamWriter.WriteLine(" public static CachedPolygon[] polygons = new CachedPolygon[] {");
        int num = 0;
        foreach (Spell spell in ((IEnumerable<Spell>) this._spells).Concat<Spell>((IEnumerable<Spell>) this._Additionalspells).Concat<Spell>((IEnumerable<Spell>) this._otherspells))
        {
          if (spell.type == CastType.Tower || spell.spellEnum == SpellEnum.Forest_Seed)
          {
            Debug.Log((object) spell.toSummon.name);
            Texture2D texture2D = spell.type == CastType.Tower ? spell.toSummon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture : spell.toSummon.GetComponent<SpriteRenderer>().sprite.texture;
            Color32[] pixels32 = texture2D.GetPixels32();
            bool[,] flagArray = new bool[texture2D.width, texture2D.height];
            int index1 = 0;
            StringBuilder stringBuilder2 = new StringBuilder();
            int y = spell.type == CastType.Tower ? (int) spell.toSummon.transform.GetChild(0).localPosition.y : 0;
            stringBuilder1.Append(spell.name.Replace(' ', '_')).Append(",\n");
            ++num;
            stringBuilder2.Append("new CachedPolygon(){ offset = ").Append(y).Append(", grid = new bool[,] {//").Append(spell.name.Replace(' ', '_')).Append(" - ").Append(texture2D.width).Append(" x " + (object) texture2D.height);
            stringBuilder2.Append("\n");
            for (int index2 = 0; index2 < texture2D.height; ++index2)
            {
              stringBuilder2.Append("{");
              for (int index3 = 0; index3 < texture2D.width; ++index3)
              {
                flagArray[index3, index2] = pixels32[index1].a > (byte) 0;
                stringBuilder2.Append(flagArray[index3, index2].ToString().ToLower()).Append(", ");
                ++index1;
              }
              stringBuilder2.Append("},\n");
            }
            stringBuilder2.Append("}},");
            if (num == 10)
            {
              stringBuilder2.Append("\nnull");
              stringBuilder1.Append("None,\n");
            }
            streamWriter.WriteLine(stringBuilder2.ToString());
          }
        }
        stringBuilder1.Append("}");
        streamWriter.WriteLine("new CachedPolygon()};");
        streamWriter.WriteLine(stringBuilder1.ToString());
        streamWriter.WriteLine("");
        streamWriter.WriteLine("}");
      }
    }
    Global.OpenURL("CachedPolygons.txt");
  }
}
