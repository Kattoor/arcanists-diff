

using Educative;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ZCreature : ZEntity
{
  internal Creature baseCreature;
  internal Creature clientObj;
  public float transformscale = 1f;
  public int health;
  public int maxHealth;
  public FixedInt massMulti = (FixedInt) 1f;
  internal bool tempFlight;
  public bool flying;
  public bool mountable;
  public bool canMount;
  public bool phantom;
  public bool gliding;
  public bool canMove = true;
  public bool usingGlide = true;
  public MyLocation LongJumpData;
  public MyLocation HighJumpData;
  [NonSerialized]
  public bool controllable = true;
  public ZPerson parent;
  public ZPerson originalParent;
  public ZPerson daOriginalTrueParent;
  public Sprite undeadHead;
  [HideInInspector]
  public GameObject flightObj;
  internal GameObject werewolfObj;
  internal SpellEnum flightSpell;
  public CreatureType type;
  public CreatureRace race;
  public bool waterWalking;
  public bool frostWalking;
  public List<SpellSlot> spells = new List<SpellSlot>();
  [NonSerialized]
  public bool moneyBags;
  [NonSerialized]
  public bool audioBags;
  [NonSerialized]
  public int moneyFrames;
  [NonSerialized]
  public ZCreature pact;
  [NonSerialized]
  public bool removeFlight;
  [NonSerialized]
  public float scale = 1f;
  [NonSerialized]
  public FixedInt scaleFixed = (FixedInt) 1;
  [NonSerialized]
  public bool FullArcane;
  [NonSerialized]
  public bool UnaffectedByNaturesWraith;
  [NonSerialized]
  public SpellEnum spellEnum = SpellEnum.None;
  [NonSerialized]
  public int shield;
  [NonSerialized]
  public int team = -1;
  [NonSerialized]
  public ZCreature mount;
  [NonSerialized]
  public ZCreature rider;
  [NonSerialized]
  public ZTower tower;
  [NonSerialized]
  public int invulnerable;
  [NonSerialized]
  public bool socialDistancing;
  [NonSerialized]
  public bool stunned;
  [NonSerialized]
  public bool superStunned;
  [NonSerialized]
  public bool entangled;
  [NonSerialized]
  public bool hasDarkDefenses;
  [NonSerialized]
  public bool inWater;
  [NonSerialized]
  public bool diesInWater = true;
  [NonSerialized]
  public int bleedCounter;
  [NonSerialized]
  public int halfHealing;
  [NonSerialized]
  public ZEffector stormShield;
  [NonSerialized]
  public ZEffector auraOfDecay;
  [NonSerialized]
  public bool shiningPower;
  [NonSerialized]
  public int turnProtectionShieldCast = -1;
  [NonSerialized]
  public ZEffectorStaticBall effectorStaticShield;
  [NonSerialized]
  public GameObject indicatorOfDecay;
  [NonSerialized]
  public int entangledShield = -1;
  [NonSerialized]
  public bool vine_bloom_active;
  [NonSerialized]
  public int additionalBleedOutDamage;
  [NonSerialized]
  public int prestigeIndex = -1;
  [NonSerialized]
  public bool permenantFlight = true;
  [NonSerialized]
  internal MinerMarket minerMarket;
  [NonSerialized]
  internal int appliedGravity = -100;
  [NonSerialized]
  internal int fusion = -1;
  [NonSerialized]
  internal int sprinting;
  [NonSerialized]
  internal int retribution;
  [NonSerialized]
  public bool _FourSeasonsCastAtEndOfTurn;
  [NonSerialized]
  public MyLocation _FourSeasonsLocation;
  [NonSerialized]
  public bool movementFromInput;
  internal int loopCount;
  internal bool glideIsActive;
  internal bool climbingHooksIsActive;
  internal int tries;
  [NonSerialized]
  public List<ZCreature.NotScaled> notScaled = new List<ZCreature.NotScaled>();
  public List<ZEffector> effectors = new List<ZEffector>();
  public List<ZEffector> destroyableEffectors = new List<ZEffector>();
  public List<ZMyCollider> followingColliders = new List<ZMyCollider>();
  internal int turnUndeadTurn = -1;
  public int turnFriendlyDmg = -1;
  public ZCreature enemyWhoKilled;
  internal SpellEnum killedBy = SpellEnum.None;
  internal ZPerson killer;
  public int TrueSize = -1;
  public EventHandler<MyLocation> onSetPosition;
  private MyLocation ExplosionOrigin = MyLocation.zero;
  private int fallLoop;
  private MyLocation fallloopVec = MyLocation.zero;

  internal string name => this.baseCreature.name;

  public FixedInt speed => this.baseCreature.speed;

  public FixedInt GetAppliedMass
  {
    get => !this.gravitionalPull ? this.massMulti : Mathd.Min((FixedInt) 524288L, this.massMulti);
  }

  public FixedInt minAngle => (FixedInt) this.baseCreature.minAngle;

  public FixedInt maxAngle => (FixedInt) this.baseCreature.maxAngle;

  public bool pawn => this.isPawn && this.spells.Count < 12;

  public ZPerson achievementParent
  {
    get
    {
      return this.parent.team != 24 || this.daOriginalTrueParent == null ? this.parent : this.daOriginalTrueParent;
    }
  }

  public override string ToString()
  {
    return "Creature: " + this.baseCreature.name + " Team: " + (object) this.parent.team + " Health: " + (object) this.health + " Pos: " + (object) this.position + " Effectors: " + (object) (this.effectors.Count + this.destroyableEffectors.Count) + " Velocity: " + (object) this.velocity;
  }

  public Sprite undeadBody => this.clientObj.undeadBody;

  public Sprite undeadLeftArm => this.clientObj.undeadLeftArm;

  public Sprite undeadRightArm => this.clientObj.undeadRightArm;

  public Sprite undeadLeftFoot => this.clientObj.undeadLeftFoot;

  public Sprite undeadRightFoot => this.clientObj.undeadRightFoot;

  public GameObject deathExplosion
  {
    get
    {
      if (this.race != CreatureRace.Undead)
        return this.clientObj.deathExplosion;
      return ClientResources.Instance?.deathExplosion;
    }
  }

  public SpriteRenderer miniMapBg
  {
    get => this.clientObj.miniMapBg;
    set => this.clientObj.miniMapBg = value;
  }

  public SpriteRenderer bg => this.clientObj.bg;

  public SpriteRenderer head => this.clientObj.head;

  public SpriteRenderer body => this.clientObj.body;

  public SpriteRenderer leftArm => this.clientObj.leftArm;

  public SpriteRenderer rightArm => this.clientObj.rightArm;

  public SpriteRenderer leftFoot => this.clientObj.leftFoot;

  public SpriteRenderer rightFoot => this.clientObj.rightFoot;

  public SpriteRenderer hat => this.clientObj.hat;

  public SpriteRenderer beard => this.clientObj.beard;

  public SpriteRenderer beard2 => this.clientObj.beard2;

  public SpriteRenderer beard3 => this.clientObj.beard3;

  public SpriteRenderer mouth => this.clientObj.mouth;

  public GameObject christmasHat => this.clientObj.christmasHat;

  public TMP_Text txtHealth
  {
    get => this.clientObj.txtHealth;
    set => this.clientObj.txtHealth = value;
  }

  public TMP_Text txtShield
  {
    get => this.clientObj.txtShield;
    set => this.clientObj.txtShield = value;
  }

  public Image imgPrestige
  {
    get => this.clientObj.imgPrestige;
    set => this.clientObj.imgPrestige = value;
  }

  public GameObject selectedSpellPanel
  {
    get => this.clientObj.selectedSpellPanel;
    set => this.clientObj.selectedSpellPanel = value;
  }

  public Image selectedSpellIcon
  {
    get => this.clientObj.selectedSpellIcon;
    set => this.clientObj.selectedSpellIcon = value;
  }

  public int StoneDamageReduction => this.familiarLevelStone;

  public FamiliarType familiar
  {
    get => this.parent == null ? FamiliarType.Nothing : this.parent.familiarBook;
  }

  public int familiarLevelActivateable
  {
    get => this.parent == null ? 0 : this.parent.GetLevel(this.parent.ActivateableFamiliar);
  }

  public int familiarLevelArcane => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Arcane);

  public int familiarLevelFlame => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Flame);

  public int familiarLevelStone => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Stone);

  public int familiarLevelStorm => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Storm);

  public int familiarLevelFrost => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Frost);

  public int familiarLevelUnderdark
  {
    get => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Underdark);
  }

  public int familiarLevelOverlight
  {
    get => this.parent == null ? 0 : this.parent.GetLevel(BookOf.OverLight);
  }

  public int familiarLevelNature => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Nature);

  public int familiarLevelSeas => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Seas);

  public int familiarLevelCogs => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Cogs);

  public int familiarLevelSeasons => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Seasons);

  public int familiarLevelIllusion
  {
    get => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Illusion);
  }

  public int familiarLevelBlood => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Blood);

  public int familiarLevelWild => this.parent == null ? 0 : this.parent.GetLevel(BookOf.The_Wilds);

  public int familiarLevelCosmos => this.parent == null ? 0 : this.parent.GetLevel(BookOf.Cosmos);

  public ZMyCollider colliderGameObject
  {
    get => ZComponent.IsNull((ZComponent) this.tower) ? this.collider : this.tower.collider;
  }

  public bool bleeding
  {
    get => this.bleedCounter > 0;
    set => this.bleedCounter = value ? 5 : 0;
  }

  public bool gravitionalPull => this.appliedGravity > this.parent.localTurn;

  public bool entangledOrGravity => this.entangled || this.gravitionalPull;

  public void RemoveEntangle()
  {
    if (!this.entangled)
      return;
    this.entangled = false;
    if (this.isMoving || !((ZComponent) this.tower != (object) null))
      return;
    this.tower.ShouldFall();
  }

  public void RemoveGravity()
  {
    if (this.appliedGravity <= 0)
      return;
    this.appliedGravity = -1;
    if (this.isMoving || !((ZComponent) this.tower != (object) null))
      return;
    this.tower.ShouldFall();
  }

  public Canvas overheadCanvas
  {
    get => this.clientObj.overheadCanvas;
    set => this.clientObj.overheadCanvas = value;
  }

  public OverheadEmoji overheadEmoji => this.clientObj?.overheadEmoji;

  public IAnimator animator
  {
    get => this.clientObj.animator;
    set => this.clientObj.animator = value;
  }

  public PanelPlayer panelPlayer
  {
    get => this.clientObj.panelPlayer;
    set => this.clientObj.panelPlayer = value;
  }

  public bool drainable => this.parent == null || this.parent.drainable;

  public MyLocation OverHeadOffset
  {
    get
    {
      return new MyLocation(0, this.GetRadius() + (!ZComponent.IsNull((ZComponent) this.tower) ? 18 - this.radius : 0) + 15);
    }
  }

  public MyLocation ClientOverHeadOffset => this.OverHeadOffset + new MyLocation(0, 15);

  public int GetCurrentHealth()
  {
    return !ZComponent.IsNull((ZComponent) this.tower) ? this.tower.Health + this.health : this.health;
  }

  public bool HasArcaneEnergizer
  {
    get
    {
      foreach (ZEffector effector in this.effectors)
      {
        if (effector.type == EffectorType.Arcane_Energizer)
          return true;
      }
      return false;
    }
  }

  public FixedInt GetLongJumpX(bool usingGlide = true)
  {
    if (this.entangled && this.LongJumpData.y != 0)
      return (FixedInt) 524288L;
    if (this.familiarLevelFrost > 0 && (this.pawn || this.game.originalSpellsOnly || !usingGlide))
      return this.LongJumpData.x + (this.familiarLevelFrost / 2 + 1);
    return (ZComponent) this.mount != (object) null ? (FixedInt) 8388608L : this.LongJumpData.x;
  }

  public FixedInt GetLongJumpY(bool usingGlide = true)
  {
    if (this.entangled && this.LongJumpData.y != 0)
      return (FixedInt) 3145728L;
    if (this.familiarLevelFrost > 0 && (this.pawn || this.game.originalSpellsOnly || !usingGlide))
      return this.LongJumpData.y + this.familiarLevelFrost;
    return (ZComponent) this.mount != (object) null ? (FixedInt) 9437184L : this.LongJumpData.y;
  }

  public FixedInt GetHighJumpX(bool usingGlide = true)
  {
    if (this.entangled && this.HighJumpData.y != 0)
      return (FixedInt) 524288L;
    if (this.familiarLevelFrost > 3 && (this.pawn || this.game.originalSpellsOnly || !usingGlide))
      return this.HighJumpData.x + 1;
    return (ZComponent) this.mount != (object) null ? (FixedInt) 2097152L : this.HighJumpData.x;
  }

  public FixedInt GetHighJumpY(bool usingGlide = true)
  {
    if (this.entangled && this.HighJumpData.y != 0)
      return (FixedInt) 3145728L;
    if (this.familiarLevelFrost > 0 && (this.pawn || this.game.originalSpellsOnly || !usingGlide))
      return this.HighJumpData.y + this.familiarLevelFrost + 1;
    return (ZComponent) this.mount != (object) null ? (FixedInt) 12582912L : this.HighJumpData.y;
  }

  public SpellSlot AddLevel5Spell(SpellSlot z, BookOf b)
  {
    if (b != this.parent.ActivateableFamiliar)
      z.isLevel5 = true;
    this.spells.Add(z);
    return z;
  }

  public int EffectorCount(EffectorType t)
  {
    int num = 0;
    for (int index = 0; index < this.effectors.Count; ++index)
    {
      if (!ZComponent.IsNull((ZComponent) this.effectors[index]) && this.effectors[index].type == t)
        ++num;
    }
    return num;
  }

  public int GetRadius()
  {
    return ZComponent.IsNull((ZComponent) this.tower) ? this.radius : this.tower.radius;
  }

  public FixedInt ClampAngle(FixedInt a)
  {
    if (a > 90)
      return Mathd.Max(a, (FixedInt) (180 - (int) this.maxAngle));
    return a < -90 ? Mathd.Min(a, (FixedInt) (-180 - (int) this.minAngle)) : Mathd.Clamp(a, this.minAngle, this.maxAngle);
  }

  public MyLocation MakeSureInBounds(MyLocation target)
  {
    if (target.x + this.radius >= this.map.Width)
      target.x = (FixedInt) (this.map.Width - this.radius);
    else if (target.x - this.radius < 0)
      target.x = (FixedInt) this.radius;
    if (target.y - this.radius < 0)
      target.y = (FixedInt) this.radius;
    else if (target.y + this.radius >= this.map.Height)
      target.y = (FixedInt) (this.map.Height - this.radius);
    return target;
  }

  public void RemoveEffector(EffectorType t)
  {
    for (int index = this.effectors.Count - 1; index >= 0; --index)
    {
      if (index < this.effectors.Count && (ZComponent) this.effectors[index] != (object) null && this.effectors[index].type == t)
        this.effectors[index].Die(index, false, false);
    }
  }

  public void RemoveDestroyableEffector(EffectorType t)
  {
    for (int index = this.destroyableEffectors.Count - 1; index >= 0; --index)
    {
      if (index < this.destroyableEffectors.Count && (ZComponent) this.destroyableEffectors[index] != (object) null && this.destroyableEffectors[index].type == t)
        this.destroyableEffectors[index].Die(index, true, false);
    }
  }

  public void SetTowerCooldowns(int turns)
  {
    if (this.game.originalSpellsOnly)
      return;
    int num1 = this.parent.localTurn - (5 - turns);
    if (!this.parent.yourTurn)
    {
      int num2 = num1 + 1;
    }
    if (turns >= 2)
      return;
    foreach (SpellSlot spell in this.spells)
    {
      if (spell.spell.type == CastType.Tower)
        spell.disabledturn = this.parent.localTurn + (this.parent.yourTurn ? 0 : 1);
    }
  }

  public void DestroyTower(bool fromDeath = false)
  {
    if (ZComponent.IsNull((ZComponent) this.tower))
      return;
    this.game.forceRysncPause = true;
    this.tower.dead = true;
    this.tower.isNull = true;
    this.tower.collider?.Disable();
    this.tower.secondCollider?.Disable();
    if (!ZComponent.IsNull((ZComponent) this.tower.effector))
      this.tower.effector.isNull = true;
    this.collider.Enable();
    if (this.game.isClient && (UnityEngine.Object) this.beard != (UnityEngine.Object) null)
    {
      this.head.sortingOrder -= 9;
      this.beard.sortingOrder -= 9;
      this.beard2.sortingOrder -= 9;
      this.beard3.sortingOrder -= 9;
      this.hat.sortingOrder -= 9;
      this.mouth.sortingOrder -= 9;
    }
    ZComponent.Destroy<GameObject>(this.tower.gameObject);
    if ((UnityEngine.Object) this.overheadCanvas != (UnityEngine.Object) null)
      ((RectTransform) this.overheadCanvas.transform).anchoredPosition = this.ClientOverHeadOffset.ToSinglePrecision() * (1f / this.scale);
    this.UpdateHealthTxt();
    this.tower = (ZTower) null;
    this.isMoving = false;
    if (this.moving != null)
      this.game.ongoing.KillCoroutines(this.moving);
    this.moving = (IEnumerator<float>) null;
    this.velocity.x = FixedInt.ZeroF;
    this.velocity.y = FixedInt.ZeroF;
    this.addedVelocity.x = FixedInt.ZeroF;
    this.addedVelocity.y = FixedInt.ZeroF;
    this.game.CreatureMoveSurroundings(this.position, 100);
    if (this.health <= 0)
      this.OnDeath(true);
    else
      this.Fall();
  }

  public void OnMount(ZCreature c)
  {
    this.mount = c;
    c.rider = this;
    if (c.type != CreatureType.Cogmobile)
      return;
    c.canMove = true;
  }

  public void TutorialArcaneMonster()
  {
    HUD.TransformArcaneMonster(this.parent, this, this.game, this.team);
  }

  public IEnumerator<float> DeathBrine()
  {
    ZCreature zcreature = this;
    yield return 0.0f;
    if (zcreature.isMoving)
      zcreature.KillMovement();
    zcreature.OnDeath(true);
  }

  public IEnumerator<float> DeathOnStopMoving()
  {
    ZCreature zcreature = this;
    if (zcreature.type == CreatureType.Gargoyle)
      zcreature.flying = false;
    bool isUndead = zcreature.race == CreatureRace.Undead;
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    while (!zcreature.isDead && zcreature.isMoving)
    {
      if (isUndead != (zcreature.race == CreatureRace.Undead))
      {
        if (zcreature.type != CreatureType.Gargoyle || !zcreature.canMove)
        {
          yield break;
        }
        else
        {
          zcreature.flying = true;
          yield break;
        }
      }
      else
        yield return 0.0f;
    }
    if (!zcreature.isDead && zcreature.health > 0 && isUndead != (zcreature.race == CreatureRace.Undead))
    {
      if (zcreature.type == CreatureType.Gargoyle && zcreature.canMove)
        zcreature.flying = true;
    }
    else
      zcreature.OnDeath(true);
  }

  public override void OnDeath(bool playDeathClip = true)
  {
    this.game.forceRysncPause = true;
    base.OnDeath();
    if (this.isDead || this.isNull)
      return;
    this.isDead = true;
    this.isNull = true;
    this.Demount();
    if (this.game.isTutorial && Client._tutorial.onDeath != null)
      Client._tutorial.onDeath(new ContainerCreature(this));
    if (!ZComponent.IsNull((ZComponent) this.enemyWhoKilled) && !this.enemyWhoKilled.isDead && this.enemyWhoKilled.health > 0 && this.enemyWhoKilled.achievementParent != null && !this.isPawn && !this.game.isClient)
      Achievements.Set(Achievement.Explosives_Expert, this.enemyWhoKilled.achievementParent.account, this.game);
    if (this.isPawn && this.spellEnum != SpellEnum.None)
    {
      this.game.lastMinionToDie.Add(new ZGame.Resurection()
      {
        spell = this.spellEnum,
        position = this.position
      });
      if (this.game.lastMinionToDie.Count > 20)
        this.game.lastMinionToDie.RemoveAt(0);
    }
    this.collider?.Disable();
    if (playDeathClip)
      AudioManager.Play(this.clientObj.clipDeath);
    MyLocation position = this.position;
    int radius = this.radius + 5;
    ZCreature rider1 = this.rider;
    if (!ZComponent.IsNull((ZComponent) this.rider))
    {
      ZCreature rider2 = this.rider;
      this.rider.Demount();
      if (rider2.position.x < 0 || rider2.position.x > rider2.map.Width)
        rider2.Fall();
    }
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.collider?.Disable();
      this.tower.secondCollider?.Disable();
      this.tower.isNull = true;
      ZComponent.Destroy<GameObject>(this.tower.gameObject);
    }
    for (int index = this.effectors.Count - 1; index >= 0; --index)
    {
      ZEffector effector = this.effectors[index];
      if (!ZComponent.IsNull((ZComponent) effector))
      {
        if (effector.type == EffectorType.Naplem)
        {
          this.game.globalEffectors.Add(effector);
        }
        else
        {
          effector.collider?.Disable();
          effector.active = false;
          if ((ZComponent) effector.partner != (object) null)
          {
            effector.partner.active = false;
            effector.partner.collider?.Disable();
            effector.partner.isNull = true;
            ZComponent.Destroy<GameObject>(effector.partner.gameObject);
          }
          else if (effector.type == EffectorType.Static_Ball)
          {
            ((ZEffectorStaticBall) effector).ReleaseSpells();
            if ((UnityEngine.Object) effector.gameObject != (UnityEngine.Object) null)
            {
              ZComponent.Destroy<GameObject>(effector.gameObject);
              continue;
            }
            continue;
          }
          effector.isNull = true;
          if ((UnityEngine.Object) effector.gameObject != (UnityEngine.Object) null)
            ZComponent.Destroy<GameObject>(effector.gameObject);
        }
      }
    }
    for (int index = this.destroyableEffectors.Count - 1; index >= 0; --index)
    {
      ZEffector destroyableEffector = this.destroyableEffectors[index];
      if (!ZComponent.IsNull((ZComponent) destroyableEffector))
      {
        destroyableEffector.active = false;
        destroyableEffector.collider?.Disable();
        if (!ZComponent.IsNull((ZComponent) destroyableEffector.partner))
        {
          destroyableEffector.partner.active = false;
          destroyableEffector.partner.collider?.Disable();
          destroyableEffector.partner.isNull = true;
          ZComponent.Destroy<GameObject>(destroyableEffector.partner.gameObject);
        }
        else if (destroyableEffector.type == EffectorType.Static_Ball)
        {
          ((ZEffectorStaticBall) destroyableEffector).ReleaseSpells();
          if ((UnityEngine.Object) destroyableEffector.gameObject != (UnityEngine.Object) null)
          {
            ZComponent.Destroy<GameObject>(destroyableEffector.gameObject);
            continue;
          }
          continue;
        }
        destroyableEffector.isNull = true;
        if ((UnityEngine.Object) destroyableEffector.gameObject != (UnityEngine.Object) null)
          ZComponent.Destroy<GameObject>(destroyableEffector.gameObject);
      }
    }
    if (!this.pawn)
    {
      for (int index = this.game.globalEffectors.Count - 1; index >= 0; --index)
      {
        if (!ZComponent.IsNull((ZComponent) this.game.globalEffectors[index]) && (this.game.globalEffectors[index].type == EffectorType.Portal || this.game.globalEffectors[index].type == EffectorType.Wormhole) && (ZComponent) this.game.globalEffectors[index].whoSummoned == (object) this)
        {
          this.game.globalEffectors[index].Die(index, false, true);
          this.game.globalEffectors[index] = (ZEffector) null;
        }
      }
    }
    if (this.game.isClient && (UnityEngine.Object) this.deathExplosion != (UnityEngine.Object) null)
    {
      if (!this.isPawn && this.parent != null)
      {
        if ((long) this.parent.account.discord == (long) SettingsPlayer.id_sno)
          ZComponent.Instantiate<GameObject>(ClientResources.Instance.lightExplosion, this.position.ToSinglePrecision(), Quaternion.identity);
        else if (this.parent.settingsPlayer.indexRightHand == (byte) 84)
          ZComponent.Instantiate<GameObject>(ClientResources.Instance.lavaBombExplosion, this.position.ToSinglePrecision(), Quaternion.identity);
        else
          ZComponent.Instantiate<GameObject>(this.deathExplosion, this.position.ToSinglePrecision(), Quaternion.identity);
      }
      else
        ZComponent.Instantiate<GameObject>(this.deathExplosion, this.position.ToSinglePrecision(), Quaternion.identity);
    }
    this.game._uncontrolledPlayer.controlled.Remove(this);
    if (this.parent == null || this.parent.controlled.Count == 0)
    {
      ZComponent.Destroy<GameObject>(this.gameObject);
      this.game.CreatureMoveSurroundings(position, radius);
    }
    else if ((ZComponent) this.parent.controlled[0] == (object) this)
    {
      ZPerson yourTurn = this.parent;
      for (int index = this.parent.controlled.Count - 1; index > 0; --index)
      {
        if (index < this.parent.controlled.Count)
          this.parent.controlled[index].OnDeath(true);
      }
      this.parent.controlled.Clear();
      if (this.game.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && this.parent == Player.Instance.person)
        HUD.instance?.OnDeath();
      if (this.game.isSandbox && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && this.parent == Player.Instance.person)
        this.game.DelayKill();
      else if (!this.isPawn)
      {
        if (this.parent.familiars != null)
        {
          foreach (ZFamiliar familiar in this.parent.familiars)
          {
            if ((ZComponent) familiar != (object) null)
            {
              familiar.soulJar?.collider?.Disable();
              familiar.soulJar?.collider?.SetNull();
              familiar.soulJar?.SetNull();
              familiar.effector?.SetNull();
              familiar.creature = (ZCreature) null;
              familiar.effector = (ZEffector) null;
              familiar.soulJar = (ZSpellSoulJar) null;
            }
          }
          this.parent.familiars.Clear();
        }
        if (this.game.isServer)
        {
          if (this.FullArcane && this.game.hasArcaneMonster && this.game.players.Count <= 5)
          {
            bool flag = true;
            foreach (ZPerson player in this.game.players)
            {
              if (player.FullArcane && !player.didResign && player.controlled.Count > 0 && (ZComponent) player.first() != (object) null && !player.first().isDead)
                flag = false;
            }
            if (flag)
            {
              foreach (ZPerson player in this.game.players)
              {
                if (!player.FullArcane)
                  Achievements.Set(Achievement.Mark_of_Fame, player.account, this.game);
              }
            }
          }
          if (this.game.IsGameOver() && this.game.winOnDeath)
          {
            if (this.killer != null && this.game.isRated && !this.game.isClient && this.killedBy == SpellEnum.Blast_From_The_Past)
              Achievements.Set(Achievement.Paper_Bag, this.killer.account, this.game);
            this.game.DelayKill();
          }
        }
        if (this.game.isClient)
        {
          HUD.instance?.UpdateDraws();
          if ((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person == this.parent && (!this.game.isMulti || !this.game.TeamAlive(this.game.GetTeam(this.parent.team), this.parent)))
            HUD.instance?.containerSpells?.gameObject.SetActive(false);
        }
      }
      if (!ZComponent.IsNull((ZComponent) this.game.naturesWrath) && (ZComponent) this.game.naturesWrath.whoSummoned == (object) this)
      {
        this.game.naturesWrath.collider?.Disable();
        this.game.naturesWrath.DestroyNoExplosion();
        this.game.naturesWrath = (ZEffector) null;
      }
      if (!ZComponent.IsNull((ZComponent) this.game.blackhole) && (ZComponent) this.game.blackhole.whoSummoned == (object) this)
      {
        this.game.blackhole.collider?.Disable();
        this.game.blackhole.DestroyNoExplosion();
        this.game.blackhole = (ZEffector) null;
      }
      if ((UnityEngine.Object) this.clientObj != (UnityEngine.Object) null)
        ZComponent.Destroy<GameObject>(this.gameObject);
      this.game.CreatureMoveSurroundings(position, radius);
      if (this.game.isClient || !yourTurn.yourTurn)
        return;
      this.game.MoveQue.Enqueue((Action) (() =>
      {
        if (yourTurn == null || !yourTurn.yourTurn)
          return;
        this.game.NextTurn();
      }));
    }
    else
    {
      if (!this.parent.controlled.Remove(this))
        this.game._uncontrolledPlayer.controlled.Remove(this);
      if (this.originalParent != null)
      {
        this.originalParent.stolenMinions.Remove(this);
        if (this.game.isClient)
          this.originalParent.panelPlayer?.SetSummons(this.originalParent);
      }
      if (this.parent.takenMinions.Count > 0)
        this.parent.ReleaseTakenMinion();
      if (this.spellEnum == SpellEnum.Summon_Blood_Bank && ((ZComponent) this.parent.bloodBank == (object) null || this.parent.bloodBank.isDead))
      {
        for (int index = 1; index < this.parent.controlled.Count; ++index)
        {
          if ((ZComponent) this.parent.controlled[index] != (object) null && !this.parent.controlled[index].isDead && this.parent.controlled[index].spellEnum == SpellEnum.Summon_Blood_Bank)
          {
            this.parent.bloodBank = this.parent.controlled[index];
            break;
          }
        }
      }
      if (this.game.isClient && (UnityEngine.Object) this.parent.panelPlayer != (UnityEngine.Object) null)
        this.parent.panelPlayer.SetSummons(this.parent);
      if (this.game.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && (int) this.game.serverState.playersTurn == Player.Instance.myTurn && Player.Instance.person == this.parent && Player.Instance.person.yourTurn && (this.spellEnum != SpellEnum.Summon_Dragon_Egg || this.health <= 0 || Player.Instance.selectedCreatureIndex >= this.parent.controlled.Count))
      {
        if (this.type == CreatureType.Recall_Device && this.health > 0)
          Player.Instance.selectedCreatureIndex = 0;
        this.game.ongoing.RunSpell(this.game.castWaitOnDeath(30f));
      }
      if (this.type == CreatureType.Brine_Goblin)
      {
        ZSpell.OnExplosionBrineGoblin(this);
        ZComponent.Destroy<GameObject>(this.gameObject);
        ZSpell.ApplyExplosionForce(SpellEnum.Summon_Brine_Goblin, this.world, position, 25, Curve.Generic, 20, 95, (FixedInt) 10, DamageType.Brine, this.parent?.first(), this.game.turn);
      }
      else
      {
        if (this.type == CreatureType.Dragon && this.name.StartsWith("Steam"))
          ZSpell.OnExplosionSteamDragon(this.parent.controlled[0], this.position);
        else if (this.type == CreatureType.Gargoyle && this.race != CreatureRace.Undead)
          this.map.ServerBitBlt((double) this.transformscale > 0.0 ? 52 : 53, (int) this.position.x - 4, (int) this.position.y + 2, false);
        ZComponent.Destroy<GameObject>(this.gameObject);
        this.game.CreatureMoveSurroundings(position, radius);
        if (ZComponent.IsNull((ZComponent) rider1) || rider1.isMoving || rider1.isDead)
          return;
        rider1.Fall();
      }
    }
  }

  public void ColorPrestigeHat()
  {
    if (!this.game.isClient || this.prestigeIndex <= 0 || this.isPawn)
      return;
    ConfigurePlayer.PrestigeHat(this.imgPrestige, (byte) (this.prestigeIndex - 1), this.parent.clientColor, true);
    this.imgPrestige.gameObject.SetActive(true);
  }

  public void ColorPrestigeHat(ZPerson other, Color color)
  {
    if (!this.game.isClient || this.prestigeIndex <= 0)
      return;
    ConfigurePlayer.PrestigeHat(this.imgPrestige, (byte) (this.prestigeIndex - 1), this.parent.clientColor, false);
    this.imgPrestige.gameObject.SetActive(true);
  }

  public virtual void Start() => this.Start2();

  protected void Start2()
  {
    if (!((UnityEngine.Object) this.animator != (UnityEngine.Object) null) || !(((object) this.animator).GetType() == typeof (AnimatePlayer)))
      return;
    ((AnimatePlayer) this.animator).creature = this;
  }

  internal virtual void Awake() => this.animator = this.gameObject.GetComponent<IAnimator>();

  public void OnStop()
  {
    if (!((UnityEngine.Object) this.animator != (UnityEngine.Object) null))
      return;
    this.animator.Play(AnimateState.Stop);
    if (!this.game.isClient)
      return;
    AudioManager.Play(AudioManager.instance.land);
  }

  public void Demount()
  {
    if (!((ZComponent) this.mount != (object) null))
      return;
    if (this.mount.type == CreatureType.Cogmobile)
      this.mount.canMove = false;
    this.mount.rider = (ZCreature) null;
    this.mount = (ZCreature) null;
  }

  public void CreateProtectionShield(bool force = false)
  {
    if (!this.game.isClient || !(this.shield == 0 | force) || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    GameObject gameObject = ZComponent.Instantiate<GameObject>(Inert.Instance.protectionShieldObj, this.transform.position, Quaternion.identity, this.transform);
    gameObject.GetComponent<ParticleProtectionShield>().c = this.clientObj;
    float num = (float) this.radius / 72f / this.scale;
    gameObject.transform.localScale = new Vector3(num, num, 1f);
  }

  public void CreateGravityObj(bool force = false)
  {
    if (!this.game.isClient || !force && this.gravitionalPull || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null) || !((UnityEngine.Object) this.clientObj.gravityObj == (UnityEngine.Object) null))
      return;
    this.clientObj.gravityObj = ZComponent.Instantiate<GameObject>(ClientResources.Instance.gravityObj, this.transform.position, Quaternion.identity, this.transform);
    this.clientObj.gravityObj.GetComponent<ParticleEntangle>().SetCreature(this.clientObj, true);
  }

  public void CreateEntangleObj(bool force = false)
  {
    if (!this.game.isClient || !(!this.entangled | force) || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    ZComponent.Instantiate<GameObject>(Inert.GetSpell(SpellEnum.Entangle).toSummon, this.transform.position, Quaternion.identity, this.transform).GetComponent<ParticleEntangle>().SetCreature(this.clientObj, this.spellEnum != SpellEnum.Entangle_Kraken);
  }

  public bool RemoveSpell(SpellEnum s)
  {
    for (int index = this.spells.Count - 1; index >= 0; --index)
    {
      if (this.spells[index].spell.spellEnum == s)
      {
        this.spells.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  public int GetSpellIndex(SpellEnum s)
  {
    int spellIndex = 0;
    foreach (SpellSlot spell in this.spells)
    {
      int num;
      if (spell == null)
      {
        num = 0;
      }
      else
      {
        SpellEnum? spellEnum1 = spell.spell?.spellEnum;
        SpellEnum spellEnum2 = s;
        num = spellEnum1.GetValueOrDefault() == spellEnum2 & spellEnum1.HasValue ? 1 : 0;
      }
      if (num != 0)
        return spellIndex;
      ++spellIndex;
    }
    return -1;
  }

  public bool HasSpell(SpellEnum s)
  {
    foreach (SpellSlot spell in this.spells)
    {
      int num;
      if (spell == null)
      {
        num = 0;
      }
      else
      {
        SpellEnum? spellEnum1 = spell.spell?.spellEnum;
        SpellEnum spellEnum2 = s;
        num = spellEnum1.GetValueOrDefault() == spellEnum2 & spellEnum1.HasValue ? 1 : 0;
      }
      if (num != 0)
        return true;
    }
    return false;
  }

  public SpellSlot GetSpellSlot(SpellEnum s)
  {
    foreach (SpellSlot spell in this.spells)
    {
      if (spell != null)
      {
        SpellEnum? spellEnum1 = spell.spell?.spellEnum;
        SpellEnum spellEnum2 = s;
        if (spellEnum1.GetValueOrDefault() == spellEnum2 & spellEnum1.HasValue)
          return spell;
      }
    }
    return (SpellSlot) null;
  }

  public SpellSlot GetAvailableGate(ref byte spellIndex, int turnOffset = 0)
  {
    if (this.parent.arcaneGateSpellSlot == null)
      return (SpellSlot) null;
    SpellSlot availableGate = (SpellSlot) null;
    int num = this.parent.localTurn + turnOffset;
    for (int ag = 0; ag < this.parent.arcaneGateSpellSlot.Count; ++ag)
    {
      if (this.parent.arcaneGateSpellSlot[ag] == null || this.parent.arcaneGateSpellSlot[ag].isPresent && this.parent.arcaneGateSpellSlot[ag].UsedUses > 0)
      {
        this.parent.arcaneGateSpellSlot.RemoveAt(ag);
        --ag;
      }
      else if (this.parent.arcaneGateSpellSlot[ag].LastTurnFired < 0 || this.parent.arcaneGateSpellSlot[ag].LastTurnFired <= this.parent.localTurn - 5 - turnOffset)
      {
        int index = this.spells.FindIndex((Predicate<SpellSlot>) (x => x == this.parent.arcaneGateSpellSlot[ag]));
        if (index >= 0)
        {
          spellIndex = (byte) index;
          return this.parent.arcaneGateSpellSlot[ag];
        }
      }
      else if (this.parent.arcaneGateSpellSlot[ag].LastTurnFired < num)
      {
        availableGate = this.parent.arcaneGateSpellSlot[ag];
        num = this.parent.arcaneGateSpellSlot[ag].LastTurnFired;
      }
    }
    if (availableGate != null)
      return availableGate;
    return this.parent.arcaneGateSpellSlot.Count > 0 ? this.parent.arcaneGateSpellSlot[0] : (SpellSlot) null;
  }

  public bool SpellUseable(SpellEnum s)
  {
    foreach (SpellSlot spell in this.spells)
    {
      if (spell != null)
      {
        SpellEnum? spellEnum1 = spell.spell?.spellEnum;
        SpellEnum spellEnum2 = s;
        if (spellEnum1.GetValueOrDefault() == spellEnum2 & spellEnum1.HasValue)
          return this.SpellUseable(spell);
      }
    }
    return false;
  }

  public bool SpellUseable(SpellSlot x)
  {
    if ((x.TurnsTillFirstUse <= this.parent.localTurn || x.LastTurnFired > -1) && (x.LastTurnFired == -1 || x.LastTurnFired + x.RechargeTime < this.parent.localTurn) && (x.MaxUses < 0 || x.UsedUses < x.MaxUses))
    {
      if (x.spell.spellEnum == SpellEnum.Resurrection)
        return this.game.lastMinionToDie.Count > 0;
      if (x.spell.spellEnum == SpellEnum.Summon_Titan)
        return this.parent.minionBookTitans.Count > 0 && this.parent.minionBookTitans.Where<ZGame.MinionBookTitan>((Func<ZGame.MinionBookTitan, bool>) (m => !m.used)).Count<ZGame.MinionBookTitan>() > 0;
      if (!ZSpell.IsMinionSpell(x.spell.spellEnum) || x.spell.amount + this.parent.GetMinionCount() <= this.parent.GetMaxMinions())
        return true;
    }
    return false;
  }

  public void TurnUndead(bool forceSpellSync = true)
  {
    this.turnUndeadTurn = this.game.totalTurnsCombined;
    if (this.pawn & forceSpellSync)
      ZSpell.SyncSpellsWithParent(this.parent.first(), this, true);
    if (this.isPawn & forceSpellSync)
    {
      if (this.game.originalSpellsOnly)
        this.maxHealth = 250;
      else if (this.maxHealth < 125)
        this.maxHealth += 25;
      else
        this.maxHealth = 250;
    }
    this.team = this.parent.team;
    this.race = CreatureRace.Undead;
    if (this.type == CreatureType.Dragon)
    {
      this.spells[0].spell = Inert.Instance.ZombieBreath;
      if ((ZComponent) this.auraOfDecay == (object) null)
      {
        ZEffector zeffector = ZEffector.Create(this.game, Inert.Instance.lichAuraOfDecay.GetComponent<Effector>(), this.transform.position, Quaternion.identity, this.transform);
        zeffector.game = this.game;
        zeffector.type = EffectorType.Dragon_Aura_of_Decay;
        zeffector.collider.world = this.world;
        zeffector.collider.Move(this.position);
        zeffector.whoSummoned = this;
        this.effectors.Add(zeffector);
        this.followingColliders.Add(zeffector.collider);
        zeffector.followParent = true;
        zeffector.active = false;
        zeffector.MaxTurnsAlive = 1000000000;
        zeffector.gameObjectActive = false;
        zeffector.gameObject?.SetActive(false);
        this.auraOfDecay = zeffector;
        this.game.forceRysncPause = true;
        if (!ZComponent.IsNull((ZComponent) this.rider))
          this.rider.ApplyDamage(this.spellEnum, DamageType.Death, 15, this, 0);
      }
      if (this.GetType() == typeof (ZCreatureFlameDragon))
        ((ZCreatureFlameDragon) this).ImmuneTo = DamageType.Not_Immune;
    }
    else if (this.type == CreatureType.Pegasus)
    {
      if ((ZComponent) this.auraOfDecay == (object) null)
      {
        ZEffector zeffector = ZEffector.Create(this.game, Inert.Instance.lichAuraOfDecay.GetComponent<Effector>(), this.transform.position, Quaternion.identity, this.transform);
        zeffector.active = false;
        zeffector.game = this.game;
        zeffector.type = EffectorType.Dragon_Aura_of_Decay;
        zeffector.collider.world = this.world;
        zeffector.collider.Move(this.position);
        zeffector.whoSummoned = this;
        this.effectors.Add(zeffector);
        this.followingColliders.Add(zeffector.collider);
        zeffector.followParent = true;
        zeffector.MaxTurnsAlive = 1000000000;
        zeffector.gameObjectActive = false;
        zeffector.gameObject?.SetActive(false);
        this.auraOfDecay = zeffector;
        this.game.forceRysncPause = true;
        this.spells.Clear();
        this.spells.Add(new SpellSlot(Inert.Instance.spells["Drain Bolt"]));
        if (!ZComponent.IsNull((ZComponent) this.rider))
          this.rider.ApplyDamage(this.spellEnum, DamageType.Death, 15, this, 0);
      }
      if (this.game.isRated && !this.game.isClient)
      {
        this.achievementParent.awards.Subversion |= 2;
        if (this.achievementParent.awards.Subversion == 3)
          Achievements.Set(Achievement.Subversion, this.achievementParent.account, this.game);
      }
    }
    else if (this.type == CreatureType.Monkey)
    {
      this.LongJumpData.x = (FixedInt) ((FixedInt) 8).RawValue;
      this.LongJumpData.y = (FixedInt) ((FixedInt) 9).RawValue;
      this.HighJumpData.x = (FixedInt) ((FixedInt) 2).RawValue;
      this.HighJumpData.y = (FixedInt) ((FixedInt) 13).RawValue;
    }
    else if (this.type == CreatureType.Paladin & forceSpellSync)
    {
      this.spells.Clear();
      this.spells.Add(new SpellSlot(Inert.Instance.chargePaladin));
      this.LongJumpData = new MyLocation(7, 4);
      this.HighJumpData = new MyLocation(2, 6);
      if (this.parent.controlled[0].HasSpell(SpellEnum.Drain_Bolt))
        this.spells.Add(new SpellSlot(Inert.Instance.spells["Drain Bolt"]));
      if (this.game.isRated && !this.game.isClient)
      {
        this.achievementParent.awards.Subversion |= 1;
        if (this.achievementParent.awards.Subversion == 3)
          Achievements.Set(Achievement.Subversion, this.achievementParent.account, this.game);
      }
    }
    else if (this.type == CreatureType.Pixie & forceSpellSync)
    {
      this.spells.Clear();
      this.spells.Add(new SpellSlot(Inert.Instance.spells["Drain Bolt"]));
    }
    if (this.minerMarket != null && this.isPawn)
    {
      ZCreature zcreature = ZSpell.FireSummon(Inert.GetSpell(SpellEnum.Summon_Myth), this.game, this.parent.first(), this.position, 1);
      zcreature.health = zcreature.maxHealth / 4;
      if (zcreature.health > 37)
        zcreature.health = 37;
      zcreature.UpdateHealthTxt();
      int index1 = this.parent.controlled.FindIndex((Predicate<ZCreature>) (m => (ZComponent) m == (object) this));
      if (index1 > -1)
      {
        int index2 = this.parent.controlled.Count - 1;
        this.parent.controlled[index1] = zcreature;
        this.parent.controlled[index2] = this;
        if (this.game.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person.yourTurn)
          Player.Instance.selected = zcreature;
      }
      this.OnDeath(true);
    }
    else
      this.ClientTurnUndead();
  }

  public void ClientTurnUndead()
  {
    if (!this.game.isClient)
      return;
    if (this.type == CreatureType.Dragon)
    {
      AnimateDragon animator = (AnimateDragon) this.animator;
      if (this.spells.Count > 7)
      {
        animator.choose = ClientResources.Instance.corruptZombieDragonSprites;
        animator.sprites = ClientResources.Instance.corruptZombieDragonSprites;
        animator.attackSprites = ClientResources.Instance.corruptZombieDragonAttackSprites;
      }
      else
      {
        animator.choose = ClientResources.Instance.zombieDragonSprites;
        animator.sprites = ClientResources.Instance.zombieDragonSprites;
        animator.attackSprites = ClientResources.Instance.zombieDragonAttackSprites;
      }
    }
    else if (this.type == CreatureType.Pegasus)
    {
      AnimateDragon animator = (AnimateDragon) this.animator;
      animator.choose = ClientResources.Instance.zombiePegasusSprites;
      animator.sprites = ClientResources.Instance.zombiePegasusSprites;
    }
    else if (this.spellEnum == SpellEnum.Summon_King_Monarch)
    {
      AnimateDragon animator = (AnimateDragon) this.animator;
      animator.choose = ClientResources.Instance.monarchSprites;
      animator.sprites = ClientResources.Instance.monarchSprites;
    }
    else
    {
      if (!((UnityEngine.Object) this.head != (UnityEngine.Object) null) || !((UnityEngine.Object) this.undeadHead != (UnityEngine.Object) null))
        return;
      this.ApplyUndeadPiece(this.undeadHead, this.head);
      this.ApplyUndeadPiece(this.undeadBody, this.body);
      this.ApplyUndeadPiece(this.undeadLeftArm, this.leftArm);
      this.ApplyUndeadPiece(this.undeadRightArm, this.rightArm);
      this.ApplyUndeadPiece(this.undeadLeftFoot, this.leftFoot);
      this.ApplyUndeadPiece(this.undeadRightFoot, this.rightFoot);
      if (this.pawn)
        return;
      if ((UnityEngine.Object) this.beard != (UnityEngine.Object) null)
        this.beard.enabled = false;
      if ((UnityEngine.Object) this.beard2 != (UnityEngine.Object) null)
        this.beard2.enabled = false;
      if ((UnityEngine.Object) this.beard3 != (UnityEngine.Object) null)
        this.beard3.enabled = false;
      if ((UnityEngine.Object) this.hat != (UnityEngine.Object) null)
        this.hat.enabled = false;
      if ((UnityEngine.Object) this.mouth != (UnityEngine.Object) null)
        this.mouth.enabled = false;
      if (this.isPawn || !((UnityEngine.Object) this.parent.panelPlayer != (UnityEngine.Object) null))
        return;
      this.parent.panelPlayer.TurnUndead(this.undeadHead, this.parent.settingsPlayer);
    }
  }

  private void ApplyUndeadPiece(Sprite s, SpriteRenderer sr)
  {
    if ((UnityEngine.Object) sr == (UnityEngine.Object) null || (UnityEngine.Object) s == (UnityEngine.Object) null)
      return;
    this.TryDelete(sr);
    sr.sprite = s;
    ConfigurePlayer.ApplyOutfit(sr, this.parent.settingsPlayer);
  }

  public void TryDelete(SpriteRenderer sr)
  {
    try
    {
      if (!sr.sprite.texture.isReadable || !sr.gameObject.CompareTag("RawImage"))
        return;
      Global.DestroySprite(sr.sprite);
    }
    catch (Exception ex)
    {
    }
  }

  public IEnumerator<float> IEDarkDefenses()
  {
    ZCreature zcreature = this;
    ZCreature cre = zcreature.parent.controlled[0];
    int z = 0;
    int safeExit = 0;
    int krackenCount = 0;
label_22:
    while (zcreature.parent.controlled.Count - krackenCount > 1 && cre.health < cre.maxHealth)
    {
      ++safeExit;
      krackenCount = 0;
      if (safeExit > 500)
      {
        yield break;
      }
      else
      {
        int multiplier = zcreature.halfHealing > 0 ? 2 : 1;
        int i = zcreature.parent.controlled.Count - 1;
        while (true)
        {
          if (i > 0 && cre.health < cre.maxHealth && i < zcreature.parent.controlled.Count)
          {
            if (zcreature.parent.controlled[i].type == CreatureType.Kraken)
            {
              ++krackenCount;
            }
            else
            {
              if (zcreature.parent.controlled[i].type == CreatureType.Water_Lord)
                zcreature.parent.controlled[i].health -= multiplier;
              zcreature.parent.controlled[i].health -= multiplier;
              ++cre.health;
              if (cre.game.isClient)
              {
                zcreature.parent.controlled[i].UpdateHealthTxt();
                cre.UpdateHealthTxt();
                if (z % 5 == 0)
                  AudioManager.Play(zcreature.parent.controlled[i].clientObj.clipHit);
              }
              if (zcreature.parent.controlled[i].health <= 0)
                zcreature.parent.controlled[i].OnDeath(true);
              ++z;
              if (z == 200 && zcreature.game.isRated && !zcreature.game.isClient)
                Achievements.Set(Achievement.Soul_Drain, zcreature.parent.account, zcreature.game);
              if (z <= cre.maxHealth)
              {
                if (z % 5 == 0)
                  yield return 0.0f;
              }
              else
                break;
            }
            --i;
          }
          else
            goto label_22;
        }
        Debug.LogError((object) "Infinite Dark Defenses?");
        yield break;
      }
    }
    for (int index = zcreature.parent.controlled.Count - 1; index >= 0; --index)
      zcreature.parent.controlled[index].UpdateHealthTxt();
    if (zcreature.parent.controlled.Count - krackenCount == 1)
      cre.hasDarkDefenses = false;
  }

  public void DarkDefenses(bool instant)
  {
    if (instant)
    {
      ZCreature zcreature = this.parent.controlled[0];
      int num1 = 0;
      int num2 = this.halfHealing > 0 ? 2 : 1;
      int num3 = 0;
label_16:
      while (this.parent.controlled.Count - num3 > 1 && zcreature.health < zcreature.maxHealth)
      {
        num3 = 0;
        int index = this.parent.controlled.Count - 1;
        while (true)
        {
          if (index > 0 && zcreature.health < zcreature.maxHealth)
          {
            if (this.parent.controlled[index].type == CreatureType.Kraken)
            {
              ++num3;
            }
            else
            {
              if (this.parent.controlled[index].type == CreatureType.Water_Lord)
                this.parent.controlled[index].health -= num2;
              ++zcreature.health;
              this.parent.controlled[index].health -= num2;
              if (zcreature.game.isClient)
              {
                this.parent.controlled[index].UpdateHealthTxt();
                zcreature.UpdateHealthTxt();
                if (num1 == 0)
                  AudioManager.Play(this.parent.controlled[index].clientObj.clipHit);
              }
              if (this.parent.controlled[index].health <= 0)
                this.parent.controlled[index].OnDeath(true);
              ++num1;
              if (num1 > zcreature.maxHealth)
                break;
            }
            --index;
          }
          else
            goto label_16;
        }
        Debug.LogError((object) "Infinite Dark Defenses?");
        return;
      }
      for (int index = this.parent.controlled.Count - 1; index >= 0; --index)
        this.parent.controlled[index].UpdateHealthTxt();
      if (this.parent.controlled.Count - num3 != 1)
        return;
      zcreature.hasDarkDefenses = false;
    }
    else
      this.parent.game.ongoing.RunSpell(this.IEDarkDefenses());
  }

  private IEnumerator<float> delayRemoveFlight()
  {
    ZCreature zcreature = this;
    yield return 0.0f;
    yield return 0.0f;
    if (zcreature.flying && !zcreature.isMoving || zcreature.entangledOrGravity)
      zcreature.RemoveFlight();
  }

  public virtual void DoHeal(int damage, DamageType dt = DamageType.None, ZCreature enemy = null, bool fromDoHeal = false)
  {
    if (this.isDead || this.health <= 0 && !this.isPawn)
      return;
    if (this.halfHealing > 0 && !fromDoHeal && dt != DamageType.Heal)
      damage /= 2;
    if (!fromDoHeal && !ZComponent.IsNull((ZComponent) this.pact) && !this.pact.isDead)
    {
      int damage1 = damage / 2;
      damage -= damage1;
      if (damage > damage1 && this.health > this.pact.health)
      {
        int num = damage;
        damage = damage1;
        damage1 = num;
      }
      this.pact.DoHeal(damage1, dt, enemy, true);
    }
    if (damage > 0 && this.health >= this.maxHealth && dt != DamageType.Drain)
      return;
    this.health += damage;
    if (this.health > this.maxHealth && dt != DamageType.Drain)
      this.health = this.maxHealth;
    this.UpdateHealthTxt();
  }

  public virtual void DoDamage(int damage, DamageType dt = DamageType.None, ZCreature enemy = null, bool fromDoDmage = false)
  {
    if (!fromDoDmage && !ZComponent.IsNull((ZComponent) this.pact) && !this.pact.isDead)
    {
      int damage1 = damage / 2;
      damage -= damage1;
      if (damage1 <= 0)
        damage1 = 1;
      if (damage <= 0)
        damage = 1;
      if (this.pact.health > this.health && damage1 < damage)
      {
        int num = damage1;
        damage1 = damage;
        damage = num;
      }
      this.pact.DoDamage(damage1, dt, enemy, true);
      if (!ZComponent.IsNull((ZComponent) this.pact) && this.pact.health <= 0 && !this.pact.SoulJarSave(dt))
        this.pact.OnDeath(true);
    }
    else if (!fromDoDmage && this.parent.communeWithNature > 0 && this.game.CurrentPlayer() != this.parent)
    {
      List<ZCreature> zcreatureList = new List<ZCreature>();
      foreach (ZCreature zcreature in this.parent.controlled)
      {
        if ((ZComponent) zcreature != (object) null && !zcreature.isDead && (ZComponent) zcreature != (object) this)
          zcreatureList.Add(zcreature);
      }
      if (zcreatureList.Count > 0)
      {
        if (damage < zcreatureList.Count + 1)
        {
          zcreatureList.Add(this);
          zcreatureList.Sort((Comparison<ZCreature>) ((a, b) => b.health * 100 / b.maxHealth - a.health * 100 / a.maxHealth));
          int index = 0;
          int num1 = zcreatureList.Count * damage;
          int num2 = 0;
          while (damage > 0)
          {
            ++num1;
            if ((ZComponent) zcreatureList[index] != (object) null)
            {
              --damage;
              zcreatureList[index].DoDamage(1, dt, enemy, true);
            }
            ++index;
            if (index >= zcreatureList.Count)
              index = 0;
            if (num2 >= num1)
              break;
          }
          return;
        }
        int damage2 = damage / (zcreatureList.Count + 1);
        damage -= damage2 * zcreatureList.Count;
        if (damage2 <= 0)
          damage2 = 1;
        if (damage <= 0)
          damage = 1;
        foreach (ZCreature zcreature in zcreatureList)
        {
          if ((ZComponent) zcreature != (object) null)
            zcreature.DoDamage(damage2, dt, enemy, true);
          if ((ZComponent) zcreature != (object) null && zcreature.health <= 0 && !zcreature.SoulJarSave(dt))
            zcreature.OnDeath(true);
        }
      }
    }
    if ((ZComponent) enemy != (object) null && !enemy.isDead && (ZComponent) this != (object) enemy && damage > 1)
    {
      switch (dt)
      {
        case DamageType.Heal20:
          break;
        case DamageType.Heal50:
          int num3 = 0;
          if (dt == DamageType.Heal50)
            num3 = 5;
          if (!ZComponent.IsNull((ZComponent) enemy.parent.bloodBank) && !enemy.parent.bloodBank.isDead)
            num3 += enemy.type == CreatureType.Vampire ? 3 : 1;
          int damage3 = Mathf.Min(damage, this.health) * num3 / 10;
          if (damage3 <= 0)
            damage3 = 1;
          if (enemy.health > 0)
            enemy.DoHeal(damage3, !ZComponent.IsNull((ZComponent) enemy.parent.bloodBank) ? DamageType.Heal20 : dt);
          enemy.UpdateHealthTxt();
          break;
        default:
          if (ZComponent.IsNull((ZComponent) enemy.parent.bloodBank) || enemy.parent.bloodBank.isDead)
            break;
          goto case DamageType.Heal50;
      }
    }
    if (this.spellEnum == SpellEnum.Summon_Blood_Bank && (ZComponent) this.pact != (object) this.parent.first() && damage > 0 && (ZComponent) this.parent.first() != (object) null && !this.parent.first().isDead && this.parent.first().health > 0)
      this.parent.first().DoHeal(Mathf.Min(damage, this.health), DamageType.Heal20);
    this.health -= damage;
    if (this.health > this.maxHealth && damage < 0)
      this.health = this.maxHealth;
    this.UpdateHealthTxt();
    if (this.isPawn && !ZComponent.IsNull((ZComponent) this.rider) && this.rider.team == this.team && !this.rider.isPawn && this.parent != null)
      this.parent.awards.minionMountedDamageSaved += damage;
    if (!fromDoDmage || this.health > 0 || this.SoulJarSave(dt))
      return;
    this.OnDeath(true);
  }

  public bool SoulJarSave(DamageType dt)
  {
    if (this.isPawn || !this.familiar.Has(FamiliarType.Underdark) || ZComponent.IsNull((ZComponent) this.parent.GetFamiliar(BookOf.Underdark)) || dt == DamageType.Light || dt == DamageType.Heal)
      return false;
    ZSpellSoulJar soulJar = this.parent.GetFamiliar(BookOf.Underdark)?.soulJar;
    if (ZComponent.IsNull((ZComponent) soulJar) || soulJar.isDead)
      return false;
    if (!ZComponent.IsNull((ZComponent) this.pact))
    {
      this.pact.pact = (ZCreature) null;
      this.pact = (ZCreature) null;
    }
    ZFamiliar familiar = this.parent.GetFamiliar(BookOf.Underdark);
    familiar.bookOf = BookOf.Nothing;
    familiar.isNull = true;
    this.health = familiar.effector.variable;
    this.SetPosition(familiar.position);
    this.KillMovement();
    ZComponent.Destroy<GameObject>(familiar.gameObject);
    this.UpdateHealthTxt();
    this.CheckEffectorsOnMove();
    this.Fall();
    this.game.forceRysncPause = true;
    return true;
  }

  public virtual int ApplyDamage(
    SpellEnum hitBySpell,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    if (this.invulnerable > 0 && dt != DamageType.Heal || dt == DamageType.NoAllies && (ZComponent) enemy != (object) null && enemy.parent.team == this.parent.team || dt == DamageType.Ignore || this.isNull)
      return 0;
    if ((ZComponent) enemy != (object) null && !enemy.isPawn && enemy.parent != null && (spellRef == null || !spellRef.FromArmageddon))
      enemy.parent.awards.OnlyMinionDamage = false;
    if (this.health <= 0)
    {
      if (dt == DamageType.Drain && (ZComponent) enemy != (object) null)
      {
        if (!((ZComponent) enemy == (object) this) || enemy.race != CreatureRace.Undead)
        {
          if ((ZComponent) enemy == (object) this)
            return 0;
          if (this.race != CreatureRace.Undead && this.race != CreatureRace.Arcane && this.type != CreatureType.Golem && (ZComponent) enemy != (object) this)
          {
            int damage1 = this.game.AllowExpansion ? Mathf.Min(this.health, Mathf.Min(enemy.health, damage)) : Mathf.Min(enemy.health, damage);
            if (!this.drainable)
              enemy.DoHeal(damage1);
            else
              enemy.DoHeal(damage1, dt, enemy);
            if ((FixedInt) enemy.health > Mathd.Max((FixedInt) enemy.maxHealth, (FixedInt) this.game.MaxHealth(enemy)))
            {
              damage1 -= enemy.health - Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy));
              enemy.health = Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy));
            }
            enemy.UpdateHealthTxt();
            enemy?.parent?.awards.Heal(enemy, this, damage1);
          }
        }
      }
      else
        this.CheckToTunUndead(dt, enemy);
      return damage;
    }
    if ((ZComponent) enemy != (object) null && enemy.type == CreatureType.Bear)
      damage += Math.Min(damage, (enemy.maxHealth - enemy.health) / 2);
    if ((dt == DamageType.Sunder || dt == DamageType.Rake) && (ZComponent) enemy != (object) null && this.team == enemy.team)
    {
      if (dt != DamageType.Rake || (ZComponent) enemy == (object) this || !this.isPawn && this.parent == enemy.parent)
        return 0;
      if ((ZComponent) this.mount == (object) enemy)
        goto label_46;
    }
    if (this.FullArcane)
    {
      switch (dt)
      {
        case DamageType.Arcane:
          this.ApplyHeal(dt, damage, enemy);
          return 0;
        case DamageType.Heal:
        case DamageType.Heal20:
          break;
        default:
          if (damage > 6)
            damage = (damage -= 4);
          else if (damage > 4)
            damage -= 2;
          else
            --damage;
          if (damage <= 0)
          {
            damage = 1;
            break;
          }
          break;
      }
    }
    if ((ZComponent) this.mount != (object) null)
    {
      switch (dt)
      {
        case DamageType.Heal:
          goto label_44;
        case DamageType.Drain:
          if (!((ZComponent) enemy != (object) this))
            goto label_44;
          else
            break;
      }
      if (((ZComponent) enemy != (object) this.mount || dt != DamageType.Death) && (this.mount.race != CreatureRace.Undead && this.race != CreatureRace.Undead || this.race == CreatureRace.Undead && this.mount.race == CreatureRace.Undead))
      {
        if ((ZComponent) enemy == (object) this.mount && dt == DamageType.Wallop)
          return 0;
        damage = this.mount.ApplyDamage(hitBySpell, dt, damage, enemy, TurnCreated, spellRef);
        if (damage <= 0)
          return 0;
        goto label_46;
      }
    }
label_44:
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ApplyDamage(hitBySpell, dt, damage, enemy, TurnCreated, spellRef);
      return 0;
    }
label_46:
    if (this.familiar.Has(FamiliarType.Stone) && this.familiarLevelStone > 0 && dt != DamageType.Percentage50)
    {
      if (damage < 0)
      {
        damage += 5;
        if (damage >= 0)
          damage = -1;
      }
      else
      {
        damage -= this.StoneDamageReduction;
        if (damage <= 0)
          damage = 1;
      }
    }
    if (this.minerMarket != null)
    {
      if (this.minerMarket.Has(MinerMarket.Types.Gas_Mask) && (dt == DamageType.Death || dt == DamageType.Death_Bomb || hitBySpell == SpellEnum.Arcane_Mist || hitBySpell == SpellEnum.Blood_Mist))
        return 0;
      if (this.minerMarket.Has(MinerMarket.Types.Hard_Hat))
      {
        damage -= 5;
        if (damage <= 0)
          damage = 1;
      }
    }
    if (this.type == CreatureType.Golem)
    {
      damage -= 5;
      if (damage <= 0)
        damage = 1;
    }
    if (dt == DamageType.Death || dt == DamageType.Death_Bomb && this.turnUndeadTurn == this.game.totalTurnsCombined)
    {
      if (dt == DamageType.Death_Bomb)
        return 0;
      if (this.race == CreatureRace.Undead)
      {
        if (damage > 0 && this.health >= this.maxHealth)
          return 0;
        this.DoHeal(damage, dt, enemy);
        if (this.health > Mathf.Max(this.maxHealth, this.game.MaxHealth(this)))
        {
          damage -= this.health - Mathf.Max(this.maxHealth, this.game.MaxHealth(this));
          this.health = Mathf.Max(this.maxHealth, this.game.MaxHealth(this));
        }
        this.UpdateHealthTxt();
        enemy?.parent?.awards.Heal(enemy, this, damage);
        return 0;
      }
    }
    else if (dt == DamageType.Drain && (ZComponent) enemy != (object) null)
    {
      if (!((ZComponent) enemy == (object) this) || enemy.race != CreatureRace.Undead)
      {
        if ((ZComponent) enemy == (object) this)
          return 0;
        if (this.race != CreatureRace.Undead && this.race != CreatureRace.Arcane && this.type != CreatureType.Golem && (ZComponent) enemy != (object) this)
        {
          int damage2 = this.game.AllowExpansion ? Mathf.Min(this.health, Mathf.Min(enemy.health, damage)) : Mathf.Min(enemy.health, damage);
          if (!this.drainable)
            enemy.DoHeal(damage2);
          else
            enemy.DoHeal(damage2, dt, enemy);
          if ((FixedInt) enemy.health > Mathd.Max((FixedInt) enemy.maxHealth, (FixedInt) this.game.MaxHealth(enemy)))
          {
            damage2 -= enemy.health - Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy));
            enemy.health = Mathf.Max(enemy.maxHealth, this.game.MaxHealth(enemy));
          }
          enemy.UpdateHealthTxt();
          enemy?.parent?.awards.Heal(enemy, this, damage2);
        }
      }
    }
    else if ((dt == DamageType.Light || dt == DamageType.Heal) && this.race == CreatureRace.Undead)
    {
      damage <<= 1;
    }
    else
    {
      if (dt == DamageType.Heal || dt == DamageType.Heal20 && this.race != CreatureRace.Undead)
      {
        if (this.race != CreatureRace.Arcane && this.type != CreatureType.Golem && this.health < this.maxHealth)
        {
          this.DoHeal(damage, dt, enemy);
          if (this.health > this.maxHealth)
          {
            damage -= this.health - this.maxHealth;
            this.health = this.maxHealth;
          }
          this.UpdateHealthTxt();
          enemy?.parent?.awards.Heal(enemy, this, damage);
        }
        return 0;
      }
      if (dt == DamageType.Water)
      {
        if (this.familiar.Has(FamiliarType.Seas))
        {
          damage -= this.familiarLevelSeas;
          if (this.familiarLevelSeas >= 4)
            --damage;
          if (damage <= 0)
            return 0;
        }
      }
      else
      {
        if (dt == DamageType.Brine && this.type == CreatureType.Brine_Goblin)
        {
          this.health = 0;
          this.UpdateHealthTxt();
          enemy?.parent?.awards.Kill(enemy, this);
          this.game.ongoing.RunSpell(this.DeathBrine());
          return 0;
        }
        if (dt == DamageType.Bleeding)
        {
          this.bleedCounter = Mathf.Max(this.bleedCounter, 5 + enemy.familiarLevelBlood);
          this.halfHealing = 3;
        }
        else if (spellRef != null && spellRef.book == BookOf.Blood && (ZComponent) enemy != (object) null && enemy.familiarLevelBlood > 0 && (spellRef.GetSpellType == SpellType.Ball || spellRef.GetSpellType == SpellType.Bolt || spellRef.GetSpellType == SpellType.Bomb || spellRef.GetSpellType == SpellType.Flash))
          this.bleedCounter = Mathf.Max(this.bleedCounter, enemy.familiarLevelBlood);
        else if (dt == DamageType.Percentage50)
        {
          damage = this.health * damage / 100;
          int num = 15;
          if ((ZComponent) enemy != (object) null)
          {
            num += enemy.additionalBleedOutDamage;
            if (!this.isPawn)
              enemy.additionalBleedOutDamage += 15;
          }
          damage += num;
        }
      }
    }
    if ((ZComponent) enemy != (object) null && enemy.race == CreatureRace.Undead && !enemy.isPawn && dt != DamageType.Drain && this.race != CreatureRace.Arcane && this.type != CreatureType.Golem && dt != DamageType.Death_Bomb && ((ZComponent) enemy != (object) this || dt == DamageType.Death))
    {
      if ((ZComponent) enemy == (object) this)
        enemy.DoHeal(damage, dt, enemy);
      else
        enemy.DoHeal(damage / 2, dt, enemy);
      if (enemy.health > enemy.maxHealth)
        enemy.health = enemy.maxHealth;
      enemy.UpdateHealthTxt();
    }
    if (dt == DamageType.Gravity)
    {
      if (ZEffector.InSanctuary(this.world, this.position))
      {
        if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
          ZEffector.SpawnVineExplosion(this.transform.position);
      }
      else if (hitBySpell == SpellEnum.Gravity_Pulse)
      {
        if (this.type != CreatureType.Cosmic_Horror && this.familiarLevelCosmos < 5)
        {
          this.CreateGravityObj();
          this.appliedGravity = this.parent.localTurn + 3;
        }
      }
      else if (this.type != CreatureType.Cosmic_Horror && this.familiarLevelCosmos < 5)
      {
        this.CreateGravityObj();
        this.appliedGravity = this.parent.localTurn + 2;
      }
    }
    else if (hitBySpell == SpellEnum.Gravity_Well && spellRef != null)
      ZSpell.FireEffector(spellRef.GetBaseSpell, this, this.position, (FixedInt) 0, (FixedInt) 0, true);
    if (this.retribution > 0 && (ZComponent) enemy != (object) null && enemy.parent.team != this.parent.team && !isLoop && hitBySpell != SpellEnum.Retribution)
    {
      int damage3 = damage / 2;
      if (damage3 < 1)
        damage3 = 1;
      if (damage3 > this.retribution)
        damage3 = this.retribution;
      this.retribution -= damage3;
      if (this.retribution <= 0)
        this.RemoveDestroyableEffector(EffectorType.Retribution);
      enemy.ApplyDamage(SpellEnum.Retribution, DamageType.None, damage3, this, TurnCreated, isLoop: true);
    }
    if (this.shield > 0 && dt != DamageType.IgnoreShield && dt != DamageType.Percentage50 && hitBySpell != SpellEnum.Blood_Craze)
    {
      if (this.shield >= damage)
      {
        this.entangledShield -= damage;
        if (this.entangled && this.entangledShield < 0)
          this.RemoveEntangle();
        this.shield -= damage;
        enemy?.achievementParent?.awards.DealtDamge(enemy, this, damage, hitBySpell, spellRef);
        if (dt == DamageType.SuperStun)
          this.superStunned = true;
        if (!this.superStunned)
        {
          if (damage > 0)
          {
            ZPerson parent = this.parent;
            if ((parent != null ? (!parent.yourTurn ? 1 : 0) : 0) == 0 && this.game.serverState.busy != ServerState.Busy.No && this.game.serverState.busy != ServerState.Busy.Moving && this.game.serverState.busy != ServerState.Busy.Moving_NoCountdown && this.game.serverState.busy != ServerState.Busy.Between_Turns || TurnCreated < this.turnProtectionShieldCast)
              goto label_155;
          }
          else
            goto label_155;
        }
        this.OnStunned();
label_155:
        this.UpdateHealthTxt();
        enemy?.achievementParent?.awards.DealtDamge(enemy, this, damage, hitBySpell, spellRef);
        return 0;
      }
      damage -= this.shield;
      enemy?.achievementParent?.awards.DealtDamge(enemy, this, this.shield, hitBySpell, spellRef);
      this.shield = 0;
    }
    if (!this.permenantFlight && !this.removeFlight)
    {
      this.removeFlight = true;
      this.game.ongoing.RunSpell(this.delayRemoveFlight());
    }
    if (dt == DamageType.Hydration)
    {
      if (this.type == CreatureType.Water_Troll)
      {
        this.type = CreatureType.Fresh_Water_Troll;
        this.health = 100 * this.health / this.maxHealth;
        this.maxHealth = 100;
        this.radius = 22;
        this.zb = MapGenerator.getOutlineArray(this.radius);
        this.collider.SetRadius = 22;
        this.collider.Moved();
        this.effectors[0].collider.SetRadius = 40;
        this.stormShield.variable = 15;
        this.map.ServerBitBlt(13, (int) this.position.x, (int) this.position.y);
        this.game.CreatureMoveSurroundings(this.position, 75);
        ZSpell.ApplyExplosionForce(SpellEnum.Hydration, this.world, this.position, 0, Curve.Generic, 1, 80, (FixedInt) 1, DamageType.None, this, this.game.turn);
        this.ClientTransformWaterTroll();
        return 0;
      }
      if (this.type == CreatureType.Fresh_Water_Troll)
      {
        this.type = CreatureType.Hard_Water_Troll;
        this.health = 200 * this.health / this.maxHealth;
        this.maxHealth = 200;
        this.radius = 44;
        this.zb = MapGenerator.getOutlineArray(this.radius);
        this.collider.SetRadius = 44;
        this.collider.Moved();
        this.effectors[0].collider.SetRadius = 65;
        this.stormShield.variable = 30;
        this.map.ServerBitBlt(2, (int) this.position.x, (int) this.position.y);
        this.game.CreatureMoveSurroundings(this.position, 140);
        ZSpell.ApplyExplosionForce(SpellEnum.Hydration, this.world, this.position, 0, Curve.Generic, 1, 140, (FixedInt) 1, DamageType.None, this, this.game.turn);
        this.ClientTransformFreshWaterTroll();
        return 0;
      }
      if (this.type == CreatureType.Hard_Water_Troll)
        return 0;
    }
    else if (this.type == CreatureType.Kraken && (dt == DamageType.Fire || dt == DamageType.Napalm))
      damage *= 2;
    this.DoDamage(damage, dt, enemy);
    if ((ZComponent) enemy != (object) null)
    {
      if (enemy.parent == this.parent)
        this.turnFriendlyDmg = this.game.turn;
      ZCreature zcreature = enemy.parent?.first();
      if ((ZComponent) zcreature != (object) this && (ZComponent) zcreature != (object) null)
      {
        enemy.achievementParent?.awards.DealtDamge(enemy, this, damage, hitBySpell, spellRef);
        if (this.race != CreatureRace.Arcane && this.type != CreatureType.Golem && enemy.isPawn && (ZComponent) enemy.parent.first() != (object) null && enemy.parent.first().race == CreatureRace.Undead && (this.race != CreatureRace.Undead || dt != DamageType.Death && dt != DamageType.Death_Bomb))
        {
          zcreature.DoHeal(damage / (this.game.originalSpellsOnly ? 2 : 4));
          if (zcreature.health > Mathf.Max(zcreature.maxHealth, (int) this.game.gameFacts.startHealth))
            zcreature.health = Mathf.Max(zcreature.maxHealth, (int) this.game.gameFacts.startHealth);
          zcreature.UpdateHealthTxt();
        }
      }
    }
    if (this.game.isClient)
      AudioManager.Play(this.clientObj.clipHit);
    if (this.entangled)
      this.RemoveEntangle();
    if (this.fusion > 0)
      this.fusion = 0;
    if (this.hasDarkDefenses)
      this.DarkDefenses(true);
    if (this.health <= 0)
    {
      if (!this.CheckToTunUndead(dt, enemy))
      {
        if (hitBySpell == SpellEnum.Clockwork_Bomb && this.spellEnum == SpellEnum.Summon_Dragon_Egg && (ZComponent) enemy != (object) null)
          ZSpell.HatchSteamDragon(enemy, this.position);
        else if (!this.SoulJarSave(dt))
        {
          int num = -this.health;
          this.health = 0;
          this.UpdateHealthTxt();
          enemy?.parent?.awards.Kill(enemy, this);
          if (!this.isPawn)
          {
            ++this.game.playersKilledThisTurn;
            if (spellRef != null && spellRef.FromArmageddon)
              this.game.playerKilledByArmageddon = true;
            if (this.game.isClient && !this.game.resyncing && Global.GetPrefBool("prefdeathmsg", true))
              ChatBox.Instance?.NewChatMsg("", Descriptions.GetDeathMessage(hitBySpell, spellRef, this, enemy), (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
            else if ((ZComponent) enemy != (object) null && enemy.parent != null && enemy.team != this.team && !this.game.isClient)
            {
              if (this.game.isRated)
              {
                if (hitBySpell == SpellEnum.Sunder && enemy.type == CreatureType.Paladin)
                  Achievements.Set(Achievement.Blade_Master, enemy.parent.account, this.game);
                else if (hitBySpell == SpellEnum.Charge && enemy.parent.curDamageDealtInOneAttack >= 200)
                  Achievements.Set(Achievement.Axing_for_Trouble, enemy.parent.account, this.game);
                if (this.game.isTeam && enemy.isPawn && enemy.parent != this.game.CurrentPlayer())
                  Achievements.Set(Achievement.Team_Worker, this.game.CurrentPlayer()?.account, this.game);
                if (this.game.lastSpellCast == SpellEnum.Nature_s_Wrath)
                  Achievements.Set(Achievement.Cyclone_of_Pain, enemy.parent.account, this.game);
                if (!ZComponent.IsNull((ZComponent) enemy.effectorStaticShield) && enemy.effectorStaticShield.HasPlayersOceansFurry(this))
                  Achievements.Set(Achievement.Circle_of_Death, enemy.parent.account, this.game);
              }
              if (this.game.lastSpellCast == SpellEnum.Entangle)
                Achievements.Set(Achievement.Icarus, enemy.parent.account, this.game);
              if (this.game.portalUsedThisSpellTurn)
                Achievements.Set(Achievement._3rd_Dimension, enemy.parent.account, this.game);
              if (hitBySpell == SpellEnum.Water_Drop && this.game.skimmed_on_water)
                Achievements.Set(Achievement.Skimmer, enemy.parent.account, this.game);
              if (spellRef != null)
              {
                if (spellRef.GetSpellType == SpellType.Arrow && !spellRef.GetGoToTarget)
                  Achievements.Set(Achievement.Drop_Shot, enemy.parent.account, this.game);
                else if (spellRef.GetSpellEnum == SpellEnum.Summon_Myth)
                  Achievements.Set(Achievement.Man_Myth_Legend, enemy.parent.account, this.game);
                else if (spellRef.GetSpellType == SpellType.Bomb && !enemy.flying)
                {
                  ++enemy.parent.bombKillsFromGround;
                  if (enemy.parent.bombKillsFromGround == 2)
                    Achievements.Set(Achievement.Throwing_Arm, enemy.parent.account, this.game);
                }
                if (this.game.isRated && (ZComponent) spellRef.GetParent != (object) null && Mathd.Abs(spellRef.GetParent.position.x - this.position.x) > 1000 && (spellRef.GetSpellType == SpellType.Bomb || spellRef.GetSpellType == SpellType.Ball || spellRef.GetSpellType == SpellType.Bolt) && !spellRef.FromArmageddon)
                  enemy.parent.awards.Sniper = true;
              }
              if (hitBySpell == SpellEnum.Kablam)
                this.enemyWhoKilled = enemy;
            }
          }
          this.killer = enemy?.parent;
          this.killedBy = hitBySpell;
          this.game.ongoing.RunSpell(this.DeathOnStopMoving());
          return num;
        }
      }
    }
    else
    {
      this.UpdateHealthTxt();
      if (dt == DamageType.SuperStun)
        this.superStunned = true;
      if (!this.superStunned)
      {
        if (damage > 0)
        {
          ZPerson parent = this.parent;
          if ((parent != null ? (!parent.yourTurn ? 1 : 0) : 0) == 0 && this.game.serverState.busy != ServerState.Busy.No && this.game.serverState.busy != ServerState.Busy.Moving && this.game.serverState.busy != ServerState.Busy.Moving_NoCountdown && this.game.serverState.busy != ServerState.Busy.Between_Turns)
            goto label_243;
        }
        else
          goto label_243;
      }
      this.OnStunned();
    }
label_243:
    return 0;
  }

  public void ClientTransformWaterTroll()
  {
    if (!this.game.isClient || this.isDead || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.clientObj.clipSelect = AudioManager.instance.trollMedium;
    this.UpdateHealthTxt();
    this.txtHealth.transform.parent.localPosition = new Vector3(0.0f, (float) (this.radius + 15), 0.0f);
    this.TryDelete(this.head);
    this.head.sprite = ClientResources.Instance.freshWaterSprites[0];
    for (int index = 1; index < 6; ++index)
      this.transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = ClientResources.Instance.freshWaterSprites[index];
    this.undeadHead = ClientResources.Instance.freshWaterSprites[6];
    if (this.race == CreatureRace.Undead)
      this.head.sprite = this.undeadHead;
    ConfigurePlayer.ApplyOutfit(this.head, this.parent.settingsPlayer);
    for (int index = 1; index < 7; ++index)
      ConfigurePlayer.ApplyOutfit(this.transform.GetChild(index).GetComponent<SpriteRenderer>(), this.parent.settingsPlayer);
    float num1 = this.BGScale();
    this.bg.transform.localScale = new Vector3(num1, num1, 1f);
    float num2 = this.MiniBGScale();
    this.miniMapBg.transform.localScale = new Vector3(num2, num2, 1f);
  }

  public void ClientTransformFreshWaterTroll()
  {
    if (!this.game.isClient || this.isDead || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.clientObj.clipSelect = AudioManager.instance.trollBig;
    this.UpdateHealthTxt();
    this.txtHealth.transform.parent.localPosition = new Vector3(0.0f, (float) (this.radius + 15), 0.0f);
    this.TryDelete(this.head);
    this.head.sprite = ClientResources.Instance.hardWaterSprites[0];
    for (int index = 1; index < 6; ++index)
      this.transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = ClientResources.Instance.hardWaterSprites[index];
    this.undeadHead = ClientResources.Instance.hardWaterSprites[6];
    if (this.race == CreatureRace.Undead)
      this.head.sprite = this.undeadHead;
    ConfigurePlayer.ApplyOutfit(this.head, this.parent.settingsPlayer);
    for (int index = 1; index < 7; ++index)
      ConfigurePlayer.ApplyOutfit(this.transform.GetChild(index).GetComponent<SpriteRenderer>(), this.parent.settingsPlayer);
    float num1 = this.BGScale();
    this.bg.transform.localScale = new Vector3(num1, num1, 1f);
    float num2 = this.MiniBGScale();
    this.miniMapBg.transform.localScale = new Vector3(num2, num2, 1f);
  }

  internal ParticleDucks myducks
  {
    get => this.clientObj.myducks;
    set => this.clientObj.myducks = value;
  }

  public void OnStunned()
  {
    if (!this.stunned)
    {
      this.stunned = true;
      this.ClientStunned();
    }
    if (!this.game.isClient || !this.superStunned || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.transform.GetComponentInChildren<ParticleDucks>()?.SetRed();
  }

  public void ClientStunned()
  {
    if (!this.game.isClient || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    if ((UnityEngine.Object) this.myducks != (UnityEngine.Object) null)
      ZComponent.Destroy<GameObject>(this.myducks.gameObject);
    this.myducks = ZComponent.Instantiate<GameObject>(Inert.Instance.ducks, this.transform.position + new Vector3(0.0f, (float) (this.radius + 15)), Quaternion.Euler(-55f, 0.0f, 0.0f), this.transform).GetComponent<ParticleDucks>();
    this.myducks.c = this.clientObj;
  }

  public bool CheckToTunUndead(DamageType dt, ZCreature enemy)
  {
    if (!this.isDead && this.isPawn && (dt == DamageType.Death || dt == DamageType.Death_Bomb) && this.race != CreatureRace.Undead && (this.type != CreatureType.Gargoyle || this.canMove) && this.race != CreatureRace.Arcane && this.race != CreatureRace.Soulbound && this.type != CreatureType.Golem && (ZComponent) enemy != (object) null)
    {
      int? count = enemy.parent?.controlled.Count;
      int num = 3 + this.parent.GetMaxMinions();
      if (count.GetValueOrDefault() < num & count.HasValue)
      {
        int maxHealth = this.maxHealth;
        this.SwitchTeams(enemy?.parent);
        if (this.parent != null)
        {
          this.TurnUndead();
        }
        else
        {
          this.health = 0;
          this.OnDeath(true);
        }
        this.health = maxHealth / 4;
        if (this.health > 37)
          this.health = 37;
        this.UpdateHealthTxt();
        return true;
      }
    }
    return false;
  }

  public void SwitchTeams(ZPerson newParent)
  {
    if (newParent == null || newParent == this.parent)
      return;
    ZPerson parent = this.parent;
    this.parent?.controlled.Remove(this);
    newParent?.controlled.Add(this);
    if (newParent.controlled.Count > 5)
    {
      this.parent?.stolenMinions.Add(this);
      newParent.takenMinions.Add(this);
    }
    this.originalParent = this.parent;
    this.parent = newParent;
    this.team = (int) newParent.id;
    Inert.AddOverheadCanvas(this.clientObj);
    if ((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null && HUD.instance.hideOverheadIcons)
      this.overheadCanvas?.gameObject.SetActive(false);
    if (this.type == CreatureType.Bee)
    {
      foreach (ZCreature zcreature in this.originalParent.controlled)
      {
        if (zcreature.type == CreatureType.Beehive)
        {
          if (((ZCreatureBeehive) zcreature).bees.Remove(this))
            break;
        }
      }
    }
    if (this.game.isClient)
    {
      parent?.panelPlayer?.SetSummons(parent);
      this.parent?.panelPlayer?.SetSummons(this.parent);
      if ((UnityEngine.Object) this.txtHealth != (UnityEngine.Object) null)
        this.txtHealth.color = this.parent.clientColor;
      if ((UnityEngine.Object) this.bg != (UnityEngine.Object) null)
        this.bg.color = this.parent.clientColor;
      if ((UnityEngine.Object) this.miniMapBg != (UnityEngine.Object) null)
        this.miniMapBg.color = this.parent.clientColor;
      if ((UnityEngine.Object) this.imgPrestige != (UnityEngine.Object) null && this.imgPrestige.gameObject.activeSelf)
        this.ColorPrestigeHat(this.parent, this.parent.clientColor);
    }
    else if (this.game.isRated && !ZComponent.IsNull((ZComponent) this.rider) && !this.rider.isPawn && this.parent != null)
      Achievements.Set(Achievement.Come_With_Me, this.parent.account, this.game);
    if ((ZComponent) this.mount != (object) null && this.mount.parent.team != this.parent.team)
      this.Demount();
    else if (!ZComponent.IsNull((ZComponent) this.rider) && this.rider.parent.team != this.parent.team)
      this.rider.Demount();
    this.UpdateHealthTxt();
  }

  public float BGScale() => (float) ((double) this.radius / 18.0 * 0.20499999821186066);

  public float MiniBGScale() => (float) this.radius / 36f;

  public void CheckEffectorsOnMove()
  {
    List<ZMyCollider> zmyColliderList = !ZComponent.IsNull((ZComponent) this.tower) ? this.game.world.OverlapColliderAll(this.tower.collider, 2560) : this.game.world.OverlapCircleAll((Point) this.position, this.radius, this.collider, 2560);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      ZEffector effector = zmyColliderList[index]?.effector;
      if (!ZComponent.IsNull((ZComponent) effector))
      {
        if (effector.game == null)
          effector.game = this.game;
        effector.EffectCreature(this);
      }
    }
    ZEffector.CheckSanctuary(this.world, this);
  }

  public void UpdateEffectorPositionsOnPortal()
  {
  }

  public void CreatureMoveSurroundings()
  {
    List<ZMyCollider> zmyColliderList = this.game.world.OverlapCircleAll((Point) (this.position - this.velocity), !ZComponent.IsNull((ZComponent) this.stormShield) ? this.stormShield.collider.radius : this.radius + 10, this.collider);
    if (!ZComponent.IsNull((ZComponent) this.auraOfDecay) && this.auraOfDecay.active)
      this.auraOfDecay.EffectCreature((ZCreature) null);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature))
        {
          if ((ZComponent) creature != (object) this)
          {
            if (!ZComponent.IsNull((ZComponent) this.stormShield) && this.stormShield.active)
              this.stormShield.EffectCreature(creature);
            if (!creature.isMoving && creature.ShouldFall())
              creature.Fall();
          }
        }
        else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
          zmyColliderList[index].spell.SpellFall();
      }
      else if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZTower tower = zmyColliderList[index].tower;
        if (!ZComponent.IsNull((ZComponent) tower) && (ZComponent) tower.creature != (object) this)
        {
          if (!ZComponent.IsNull((ZComponent) this.stormShield) && this.stormShield.active)
            this.stormShield.EffectCreature(tower.creature);
          if (!tower.creature.isMoving)
            tower.ShouldFall();
        }
      }
      else
      {
        ZSpell spell = zmyColliderList[index].spell;
        if (!ZComponent.IsNull((ZComponent) spell))
        {
          if (!spell.isMoving && !spell.isDead && spell.ShouldSpellFall())
            spell.SpellFall();
        }
        else if (zmyColliderList[index].gameObjectLayer == 11 || zmyColliderList[index].gameObjectLayer == 9)
        {
          ZEffector effector = zmyColliderList[index].effector;
          if (!ZComponent.IsNull((ZComponent) effector) && effector.active && effector.collider.OverlapCircle(new Point((int) this.position.x, (int) this.position.y), this.GetRadius()))
            effector.EffectCreature(this);
        }
      }
    }
  }

  public void SetScaleBasedOnVelocity(float v)
  {
    if ((double) v >= 0.0)
    {
      if ((double) this.transformscale >= 0.0)
        return;
      this.SetScale(1f);
    }
    else
    {
      if ((double) this.transformscale <= 0.0)
        return;
      this.SetScale(-1f);
    }
  }

  public void SetScale(float scale)
  {
    scale = (double) scale <= 0.0 ? -this.scale : this.scale;
    this.transformscale = scale;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.localScale = new Vector3(scale, this.scale, 1f);
    if (!ZComponent.IsNull((ZComponent) this.rider))
    {
      this.rider.RiderMoveToPosition = this.position + Global.GetMountOffset(this.transformscale, this.type);
      this.rider.SetScale(this.transformscale);
    }
    if ((UnityEngine.Object) this.overheadCanvas != (UnityEngine.Object) null)
    {
      Vector3 scale1 = new Vector3((double) scale > 0.0 ? 1f : -1f, 1f, 1f);
      Vector3 normalScale = new Vector3(1f / scale, 1f / this.scale, 1f);
      for (int index = this.notScaled.Count - 1; index >= 0; --index)
      {
        if (this.notScaled[index] == null || (UnityEngine.Object) this.notScaled[index].transform == (UnityEngine.Object) null)
          this.notScaled.RemoveAt(index);
        else
          this.notScaled[index].Scale(scale1, normalScale);
      }
      for (int index = this.clientObj.notScaled.Count - 1; index >= 0; --index)
      {
        if (this.clientObj.notScaled[index] == null || (UnityEngine.Object) this.clientObj.notScaled[index].transform == (UnityEngine.Object) null)
          this.clientObj.notScaled.RemoveAt(index);
        else
          this.clientObj.notScaled[index].Scale(scale1);
      }
      this.overheadCanvas.transform.localScale = normalScale;
    }
    if (!((UnityEngine.Object) this.overheadEmoji != (UnityEngine.Object) null))
      return;
    this.overheadEmoji.transform.localScale = new Vector3((double) scale > 0.0 ? this.overheadEmoji.transform.localScale.x : -this.overheadEmoji.transform.localScale.x, this.overheadEmoji.transform.localScale.y, 1f);
  }

  public void SetScaleOnResync(float scale)
  {
    scale = (double) scale <= 0.0 ? -this.scale : this.scale;
    this.transformscale = scale;
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.localScale = new Vector3(scale, this.scale, 1f);
    if (!((UnityEngine.Object) this.overheadCanvas != (UnityEngine.Object) null))
      return;
    Vector3 scale1 = new Vector3((double) scale > 0.0 ? 1f : -1f, 1f, 1f);
    Vector3 normalScale = new Vector3(1f / scale, 1f / this.scale, 1f);
    for (int index = this.notScaled.Count - 1; index >= 0; --index)
    {
      if (this.notScaled[index] == null || (UnityEngine.Object) this.notScaled[index].transform == (UnityEngine.Object) null)
        this.notScaled.RemoveAt(index);
      else
        this.notScaled[index].Scale(scale1, normalScale);
    }
    for (int index = this.clientObj.notScaled.Count - 1; index >= 0; --index)
    {
      if (this.clientObj.notScaled[index] == null || (UnityEngine.Object) this.clientObj.notScaled[index].transform == (UnityEngine.Object) null)
        this.clientObj.notScaled.RemoveAt(index);
      else
        this.clientObj.notScaled[index].Scale(scale1);
    }
    this.overheadCanvas.transform.localScale = normalScale;
  }

  public MyLocation RiderMoveToPosition
  {
    set
    {
      MyLocation pos = value;
      pos.y -= 18 - this.radius;
      this.position = pos;
      if (!ZComponent.IsNull((ZComponent) this.effectorStaticShield))
        this.effectorStaticShield.position = pos;
      this.collider?.Move(pos);
      for (int index = this.followingColliders.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) this.followingColliders[index] == (object) null)
        {
          this.followingColliders.RemoveAt(index);
        }
        else
        {
          this.followingColliders[index].Move(pos);
          this.followingColliders[index].effector?.Moved();
        }
      }
    }
  }

  public MyLocation RiderMoveToPositionNoTrigger
  {
    set
    {
      MyLocation pos = value;
      pos.y -= 18 - this.radius;
      this.position = pos;
      if (!ZComponent.IsNull((ZComponent) this.effectorStaticShield))
        this.effectorStaticShield.position = pos;
      this.collider?.Move(pos);
      for (int index = this.followingColliders.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) this.followingColliders[index] == (object) null)
          this.followingColliders.RemoveAt(index);
        else
          this.followingColliders[index].Move(pos);
      }
    }
  }

  public override void SetPosition(MyLocation pos)
  {
    this.position = pos;
    if (!ZComponent.IsNull((ZComponent) this.rider))
    {
      this.rider.RiderMoveToPosition = pos + Global.GetMountOffset(this.transformscale, this.type);
      this.rider.SetScale(this.transformscale);
    }
    if (!ZComponent.IsNull((ZComponent) this.effectorStaticShield))
      this.effectorStaticShield.position = pos;
    this.collider?.Move(pos);
    for (int index = this.followingColliders.Count - 1; index >= 0; --index)
    {
      if ((ZComponent) this.followingColliders[index] == (object) null)
      {
        this.followingColliders.RemoveAt(index);
      }
      else
      {
        this.followingColliders[index].Move(pos);
        this.followingColliders[index].effector?.Moved();
      }
    }
  }

  public void SetPositionNoTrigger(MyLocation pos)
  {
    this.position = pos;
    if (!ZComponent.IsNull((ZComponent) this.rider))
    {
      this.rider.RiderMoveToPositionNoTrigger = pos + Global.GetMountOffset(this.transformscale, this.type);
      this.rider.SetScale(this.transformscale);
    }
    if (!ZComponent.IsNull((ZComponent) this.effectorStaticShield))
      this.effectorStaticShield.position = pos;
    this.collider?.Move(pos);
    for (int index = this.followingColliders.Count - 1; index >= 0; --index)
    {
      if ((ZComponent) this.followingColliders[index] == (object) null)
        this.followingColliders.RemoveAt(index);
      else
        this.followingColliders[index].Move(pos);
    }
  }

  public void OnMoved()
  {
  }

  public void UpdateHealthTxt()
  {
    if (!this.game.isClient)
      return;
    int i = ZComponent.IsNull((ZComponent) this.tower) ? this.health : this.health + this.tower.Health;
    if ((UnityEngine.Object) this.txtHealth != (UnityEngine.Object) null)
      this.txtHealth.text = i.ToString();
    if ((UnityEngine.Object) this.panelPlayer != (UnityEngine.Object) null)
      this.panelPlayer.SetHP(i);
    if (!((UnityEngine.Object) this.txtShield != (UnityEngine.Object) null))
      return;
    if (this.shield > 0 || this.bleeding || this.parent.communeWithNature > 0 || this.fusion > this.parent.localTurn || this.parent.ritualEnum.Count > 0 && !this.isPawn)
    {
      string str = this.parent.communeWithNature > 0 ? "<sprite name=\"zCommune With Nature\">" : "";
      if (this.fusion > this.parent.localTurn)
        str += "<sprite name=\"zFusion\">";
      if (this.parent.ritualEnum.Count > 0 && !this.isPawn)
      {
        foreach (SpellEnum s in this.parent.ritualEnum)
          str = str + "<sprite name=\"z" + Inert.GetSpell(s).name + "\">";
      }
      this.txtShield.text = (this.shield <= 0 || !this.bleeding ? (this.bleeding ? "<sprite name=\"blooddrop\">" : (this.shield > 0 ? "+" + this.shield.ToString() : "")) : "+" + this.shield.ToString() + "<sprite name=\"blooddrop\">") + str;
    }
    else
      this.txtShield.text = " ";
    if (!((ZComponent) this.tower != (object) null) || !this.tower.IsResistant())
      return;
    this.txtShield.text += "<sprite name=\"resistance\">";
  }

  public virtual void ApplyHeal(DamageType dt, int damage, ZCreature enemy)
  {
    if ((dt == DamageType.Heal || dt == DamageType.Light) && this.race == CreatureRace.Undead && ZComponent.IsNull((ZComponent) this.tower))
    {
      this.ApplyDamage(SpellEnum.PowerOfLight, dt, damage, enemy, this.game.turn);
    }
    else
    {
      if (dt == DamageType.Heal && (this.race == CreatureRace.Arcane || this.type == CreatureType.Golem || this.health >= this.maxHealth))
        return;
      this.DoHeal(damage, dt, enemy);
      if (this.health > this.maxHealth)
      {
        damage -= this.health - this.maxHealth;
        this.health = this.maxHealth;
      }
      enemy?.parent?.awards.Heal(enemy, this, damage);
    }
    this.UpdateHealthTxt();
  }

  public virtual void ApplyExplosionForce(
    int spellRadius,
    MyLocation origin,
    FixedInt force,
    int EXORADIUS,
    Curve factor,
    bool startMoving = false)
  {
    if (!ZComponent.IsNull((ZComponent) this.tower))
      return;
    FixedInt fixedInt1 = (FixedInt) (MyLocation.Distance(origin, this.position) - this.radius - spellRadius);
    FixedInt fixedInt2 = this.position.x - origin.x;
    if (fixedInt2 < -3)
    {
      if (fixedInt2 > -10)
        origin.x -= fixedInt2 * 2;
    }
    else if (fixedInt2 > 3 && fixedInt2 < 10)
      origin.x -= fixedInt2 * 2;
    FixedInt angle = Inert.AngleOfVectors(this.position, origin);
    if (fixedInt1 < 0)
      fixedInt1 = (FixedInt) 0;
    FixedInt force1 = force * Inert.GetCurve(factor).Evaluate(fixedInt1 / (FixedInt) EXORADIUS);
    MyLocation myLocation = Inert.Velocity(angle, force1) * this.GetAppliedMass;
    if (this.flying)
    {
      if (this.type == CreatureType.Dragon)
        myLocation *= 419430L;
      else if (origin.y < this.position.y)
      {
        if (force > 5)
          myLocation *= 524288L;
      }
      else
      {
        myLocation.x *= 943718L;
        myLocation.y *= 524288L;
      }
      if (myLocation.y < 0)
        myLocation.y = (FixedInt) 0;
    }
    this.ExplosionOrigin = origin;
    if (this.isMoving)
    {
      this.addVelocity = true;
      this.addedVelocity = this.addedVelocity + myLocation;
    }
    else
    {
      this.velocity = myLocation;
      if (this.game.isClient && !this.game.resyncing && CameraMovement.FOLLOWTARGETS)
        CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) this));
    }
    if (!startMoving)
      return;
    this.StartMoving(false);
  }

  public virtual void SetVelocityBreeze(int x, int y)
  {
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
    }
    else
    {
      this.velocity.x = (FixedInt) x;
      this.velocity.y = (FixedInt) y;
      if (this.isMoving)
        return;
      this.moving = this.game.ongoing.RunCoroutine(this.Move());
    }
  }

  public virtual void SetVelocity(int x, int y)
  {
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
    }
    else
    {
      this.velocity.x = (FixedInt) x;
      this.velocity.y = (FixedInt) y;
      if (!this.isMoving)
      {
        if (this.flying && !this.entangledOrGravity)
        {
          if (this.velocity.y < 0)
            this.velocity.y = (FixedInt) 0;
          this.moving = this.game.ongoing.RunCoroutine(this.FlyMove());
        }
        else
          this.moving = this.game.ongoing.RunCoroutine(this.Move());
      }
      else
      {
        if (!this.flying || this.entangledOrGravity || !(this.velocity.y < 0))
          return;
        this.velocity.y = (FixedInt) 0;
      }
    }
  }

  public virtual void SetVelocityEitherDir(int x, int y)
  {
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
    }
    else
    {
      this.velocity.x = (FixedInt) x;
      this.velocity.y = (FixedInt) y;
      if (this.isMoving)
        return;
      if (this.flying && !this.entangledOrGravity)
        this.moving = this.game.ongoing.RunCoroutine(this.FlyMove());
      else
        this.moving = this.game.ongoing.RunCoroutine(this.Move());
    }
  }

  public IEnumerator<float> DKMove(
    MyLocation target,
    ExplosionCutout explosionCutout,
    int MaxDuration,
    bool isCharge,
    bool spotOn,
    int zRadius)
  {
    ZCreature zcreature = this;
    zcreature.isMoving = true;
    zcreature.UnaffectedByNaturesWraith = true;
    int rad = zcreature.radius;
    if (zcreature.radius != zRadius && (ZComponent) zcreature.collider != (object) null)
    {
      zcreature.radius = zRadius;
      zcreature.collider.radius = zRadius;
      zcreature.zb = MapGenerator.getOutlineArray(zRadius);
    }
    if (zcreature.zb == null)
      zcreature.zb = MapGenerator.getOutlineArray(zcreature.radius);
    int a = 0;
    FixedInt walkSpeed = (FixedInt) (isCharge ? 6815744L : 2621440L);
    FixedInt jumpDist = (FixedInt) (isCharge ? 8388608L : 4194304L);
    ZCreature.DKMoveState state = ZCreature.DKMoveState.Walk;
    int curDuration = 0;
    MyLocation myLocation = target - zcreature.position;
    MyLocation walkvelocity = myLocation.normalized * walkSpeed;
    if (walkvelocity.x == 0 && walkvelocity.y == 0)
      walkvelocity = new MyLocation(4, 0);
    zcreature.velocity = walkvelocity;
    SpinningAxe axe = (SpinningAxe) null;
    GameObject particleEffect = (GameObject) null;
    if (zcreature.game.isClient)
    {
      if (isCharge && (UnityEngine.Object) zcreature.leftArm != (UnityEngine.Object) null)
      {
        axe = zcreature.rightArm.GetComponent<SpinningAxe>();
        SpinningAxe spinningAxe = axe;
        if (spinningAxe != null)
        {
          myLocation = walkvelocity.normalized;
          spinningAxe.StartSpinning((Vector3) (myLocation.ToSinglePrecision() * 25f));
        }
      }
      GameObject t = isCharge ? ClientResources.Instance.particleCharge : ClientResources.Instance.particleMine;
      myLocation = zcreature.position;
      Vector2 singlePrecision = myLocation.ToSinglePrecision();
      Quaternion identity = Quaternion.identity;
      Transform transform = zcreature.transform;
      particleEffect = ZComponent.Instantiate<GameObject>(t, singlePrecision, identity, transform);
      if (!zcreature.isPawn)
        zcreature.animator?.Play(AnimateState.Walk, (float) MaxDuration / 30f);
    }
    while (curDuration < MaxDuration && !zcreature.isDead && !zcreature.stunned && !ZComponent.IsNull((ZComponent) zcreature) && !zcreature.InDarkTotem())
    {
      if (zcreature.game.isClient && curDuration % 10 == 0 && zcreature.isPawn)
        AudioManager.Play(AudioManager.instance.mineCharge);
      zcreature.SetScaleBasedOnVelocity(walkvelocity.x.ToFloat());
      ++curDuration;
      if (!isCharge | spotOn && state == ZCreature.DKMoveState.Walk)
      {
        if (spotOn && curDuration == 10)
          target = new MyLocation(target.x, target.y + 50);
        myLocation = target - zcreature.position;
        walkvelocity = myLocation.normalized * walkSpeed;
        if (FixedInt.Abs(walkvelocity.x) < (FixedInt) 0.05M && walkvelocity.y < -2)
          walkvelocity.y -= 1;
      }
      if (state == ZCreature.DKMoveState.Walk)
      {
        a = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * zcreature.zb.Count - zcreature.radius).ToInt();
        if (a < 0)
          a += zcreature.zb.Count;
        state = ZCreature.DKMoveState.Jump;
        for (int index1 = 0; index1 < zcreature.radius * 2; ++index1)
        {
          int index2 = (index1 + a) % zcreature.zb.Count;
          int x = (int) zcreature.position.x;
          int num = (int) zcreature.position.y - 3;
          if (!zcreature.map.CheckPosition(x + zcreature.zb[index2].x, num + zcreature.zb[index2].y, zcreature, Inert.mask_movement_NoEffector))
          {
            if (isCharge || (FixedInt) MyLocation.Distance(target, zcreature.position) > walkSpeed)
              zcreature.MoveToPosition = zcreature.position + walkvelocity;
            else
              walkvelocity = MyLocation.down;
            state = ZCreature.DKMoveState.Walk;
            if (zcreature.game.isClient && !isCharge)
            {
              IAnimator animator = zcreature.animator;
              if (animator != null)
              {
                animator.Play(AnimateState.Walk, 0.05f);
                break;
              }
              break;
            }
            break;
          }
        }
        if (state == ZCreature.DKMoveState.Jump)
        {
          if (target.y > zcreature.position.y || !isCharge)
          {
            zcreature.velocity = walkvelocity.normalized * jumpDist;
            if (isCharge)
            {
              zcreature.velocity.x = Mathd.Clamp(zcreature.velocity.x, (FixedInt) -3, (FixedInt) 3);
              zcreature.velocity.y = Mathd.Clamp(zcreature.velocity.y, (FixedInt) 3, (FixedInt) 6);
            }
          }
          else
            zcreature.velocity = walkvelocity.normalized * walkSpeed;
          zcreature.SetScaleBasedOnVelocity(zcreature.velocity.x.ToFloat());
          zcreature.MoveToPosition = zcreature.position + zcreature.velocity;
          SpinningAxe spinningAxe = axe;
          if (spinningAxe != null)
          {
            myLocation = zcreature.velocity.normalized;
            spinningAxe.StartSpinning((Vector3) (myLocation.ToSinglePrecision() * 25f));
          }
          if (zcreature.game.isClient && (UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null)
            zcreature.animator.Play(AnimateState.Jump);
        }
      }
      else
      {
        zcreature.pX = zcreature.position.x;
        zcreature.pY = zcreature.position.y;
        FixedInt fixedInt1 = zcreature.position.x + zcreature.velocity.x;
        FixedInt fixedInt2 = zcreature.position.y + zcreature.velocity.y;
        zcreature.validX = zcreature.pX;
        zcreature.validY = zcreature.pY;
        int num = 1;
        FixedInt x = zcreature.velocity.x;
        FixedInt y1 = zcreature.velocity.y;
        FixedInt fixedInt3 = zcreature.velocity.x;
        FixedInt y2 = zcreature.velocity.y;
        FixedInt fixedInt4;
        if (x > 1 || x < -1 || y1 > 1 || y1 < -1)
        {
          num = !(Mathd.Abs(x) > Mathd.Abs(y1)) ? (int) Mathd.Abs(y1) + 1 : (int) Mathd.Abs(x) + 1;
          fixedInt3 = zcreature.velocity.x / num;
          fixedInt4 = zcreature.velocity.y / num;
        }
        else
        {
          zcreature.velocity.y = (FixedInt) -1;
          fixedInt4 = (FixedInt) -1;
        }
        while (num > 0)
        {
          --num;
          a = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(zcreature.velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * zcreature.zb.Count - zcreature.radius).ToInt();
          if (a < 0)
            a += zcreature.zb.Count;
          for (int index3 = 0; index3 < zcreature.radius * 2; ++index3)
          {
            int index4 = (index3 + a) % zcreature.zb.Count;
            if (!zcreature.map.CheckPositionOnlyMap((int) (fixedInt3 + zcreature.pX) + zcreature.zb[index4].x, (int) (fixedInt4 + zcreature.pY) + zcreature.zb[index4].y))
            {
              int xInt = (int) (fixedInt3 + zcreature.pX);
              int yInt = (int) (fixedInt4 + zcreature.pY);
              zcreature.MoveToPosition = new MyLocation(xInt, yInt);
              state = ZCreature.DKMoveState.Walk;
              zcreature.velocity = walkvelocity;
              zcreature.SetScaleBasedOnVelocity(walkvelocity.x.ToFloat());
              SpinningAxe spinningAxe = axe;
              if (spinningAxe != null)
              {
                myLocation = walkvelocity.normalized;
                spinningAxe.StartSpinning((Vector3) (myLocation.ToSinglePrecision() * 25f));
                break;
              }
              break;
            }
          }
        }
        if (state == ZCreature.DKMoveState.Jump)
        {
          zcreature.MoveToPosition = zcreature.position + zcreature.velocity;
          zcreature.velocity.y += zcreature.map.Gravity;
        }
      }
      if (zcreature.map.ServerBitBltAndReturnIfChanged((int) explosionCutout, (int) zcreature.position.x, (int) zcreature.position.y + (isCharge ? 5 : 3)) > 200)
        zcreature.minerMarket?.IncreaseHolesDug(zcreature);
      zcreature.game.CreatureMoveSurroundings(zcreature.position, zcreature.radius + 10);
      MinerMarket minerMarket = zcreature.minerMarket;
      zcreature.pX = zcreature.position.x;
      zcreature.pY = zcreature.position.y;
      for (int index5 = 0; index5 < zcreature.radius * 2; ++index5)
      {
        int index6 = (index5 + a) % zcreature.zb.Count;
        if (!zcreature.map.CheckPosition((int) zcreature.pX + zcreature.zb[index6].x, (int) zcreature.pY + zcreature.zb[index6].y, zcreature, 2560) && ZComponent.IsNull((ZComponent) zcreature))
          break;
      }
      yield return 0.0f;
    }
    if (!zcreature.isDead && !zcreature.isNull)
    {
      if (!zcreature.stunned & isCharge)
      {
        if (target.y > zcreature.position.y)
        {
          zcreature.velocity = walkvelocity.normalized * jumpDist;
          zcreature.velocity.x = Mathd.Clamp(zcreature.velocity.x, (FixedInt) -3, (FixedInt) 3);
          zcreature.velocity.y = Mathd.Clamp(zcreature.velocity.y, (FixedInt) 3, (FixedInt) 6);
          if (zcreature.game.isClient && (UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null)
            zcreature.animator.Play(AnimateState.Jump);
        }
        else
          zcreature.velocity = walkvelocity.normalized;
      }
      if (zcreature.velocity.x >= 0)
      {
        if ((double) zcreature.transformscale < 0.0)
          zcreature.SetScale(1f);
      }
      else if ((double) zcreature.transformscale > 0.0)
        zcreature.SetScale(-1f);
      axe?.StopSpinning();
      zcreature.UnaffectedByNaturesWraith = false;
      zcreature.moving = zcreature.game.ongoing.RunCoroutine(zcreature.Move());
      if (zcreature.stunned && (UnityEngine.Object) particleEffect != (UnityEngine.Object) null)
        ZComponent.Destroy<GameObject>(particleEffect);
    }
    if (rad != zcreature.radius && (ZComponent) zcreature.collider != (object) null)
    {
      zcreature.radius = rad;
      zcreature.collider.radius = rad;
      zcreature.zb = MapGenerator.getOutlineArray(rad);
    }
  }

  public IEnumerator<float> MoveMultipleSteps(bool left, int steps)
  {
    ZCreature zcreature = this;
    for (int i = 0; i < steps && !zcreature.isDead && !zcreature.InDarkTotem(); ++i)
    {
      if (!zcreature.isMoving)
      {
        if ((UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null)
          zcreature.animator.Play(AnimateState.Walk, 0.05f);
        if (left)
          zcreature.MoveLeft();
        else
          zcreature.MoveRight();
      }
      yield return 0.0f;
    }
    if (!zcreature.isDead && (UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null && !zcreature.isMoving)
      zcreature.animator?.Play(AnimateState.Stop);
  }

  public bool MoveLeftUnused(bool left)
  {
    MyLocation diff = MyLocation.zero;
    int x1 = (int) this.position.x;
    int num1 = (int) this.position.y - this.radius - 1;
    int num2 = 5;
    int num3 = 2;
    for (int index1 = -num2; index1 <= num2; ++index1)
    {
      for (int index2 = -num3; index2 <= num3; ++index2)
      {
        if (this.map.CheckPosition(x1 + index1, num1 + index2, this, Inert.mask_movement_NoEffector))
        {
          diff.x += index1;
          diff.y += index2;
        }
      }
    }
    diff.Normalize();
    FixedInt fixedInt = (FixedInt) 0;
    diff = Global.Velocity(!left ? FixedInt.Atan2(diff.y, diff.x) * FixedInt.Rad2Deg + 180 : FixedInt.Atan2(diff.y, diff.x) * FixedInt.Rad2Deg, this.speed);
    if (diff.y > 262144L)
    {
      diff.x = (FixedInt) (left ? -1 : 1);
      diff.y = (FixedInt) 0;
    }
    int num4 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(diff) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num4 < 0)
      num4 += this.zb.Count;
    int y1 = (int) this.position.y;
    if (left && num4 % this.zb.Count < this.zb.Count / 2 + 5)
      num4 = this.zb.Count / 2 + 5;
    for (int index3 = 0; index3 < this.radius * 2; ++index3)
    {
      int index4 = (index3 + num4) % this.zb.Count;
      int x2 = x1 + this.zb[index4].x;
      int y2 = y1 + this.zb[index4].y;
      if (!this.map.CheckPosition(x2, y2, this, Inert.mask_entity_movement) || x2 >= this.map.Width || x2 < 0 || y2 < 0 || y2 >= this.map.Height)
        return false;
    }
    this.SetPosition(this.position + diff);
    this.CreatureMoveSurroundings();
    if (this.ShouldFall())
      this.Fall();
    return true;
  }

  public void MoveLeft2()
  {
    if ((double) this.transformscale > 0.0)
      this.SetScale(-1f);
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.TowerMoveLeft();
    }
    else
    {
      if (this.game.isClient && !this.game.resyncing && this.parent != null && (UnityEngine.Object) this.parent.torquing != (UnityEngine.Object) null && (ZComponent) this.parent.torquing.creature == (object) this)
        this.parent.torquing.Moved();
      int x = this.GetX();
      int y = this.GetY();
      if (this.map.CheckTopLeft(x, y, this.radius, this, Inert.mask_entity_movement) != null || this.map.CheckLeftTop(x, y, this.radius, this, Inert.mask_entity_movement) != null)
        return;
      Coords coords1 = this.map.CheckBottomLeft_FromTop(x, y, this.radius, this, Inert.mask_entity_movement);
      if (coords1 != null && coords1.y - y >= -(this.radius >> 2) - 3 && coords1.y - y <= -5)
        return;
      Coords coords2 = this.map.CheckBottomLeft(x, y, this.radius, this, Inert.mask_entity_movement);
      if (coords2 == null)
      {
        int num = 0;
        for (int index = 1; index < 6; ++index)
        {
          coords2 = this.map.CheckBottomRight(x, y - index, this.radius, this, Inert.mask_entity_movement) ?? this.map.CheckBottomLeft(x, y - index, this.radius, this, Inert.mask_entity_movement);
          if (coords2 != null)
          {
            num = index;
            break;
          }
        }
        if (coords2 == null && (!this.waterWalking || this.position.y > this.radius || this.position.x <= 0 || this.position.x >= this.map.Width))
        {
          if (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead)
            return;
          this.velocity.y = (FixedInt) 1;
          this.velocity.x = (FixedInt) -1;
          if (this.isMoving)
            return;
          this.movementFromInput = true;
          this.moving = this.game.ongoing.RunCoroutine(this.Move());
          return;
        }
        if (num > 1)
        {
          MyLocation myLocation = this.position + new MyLocation(-this.speed, (FixedInt) (num * -1));
          if (myLocation.y < this.map.Height - this.radius)
            this.WalkToPosition = myLocation;
        }
        else
          this.WalkToPosition = this.position + new MyLocation(-this.speed, (FixedInt) 0);
      }
      else
      {
        if (!this.ValidateRight(x - (int) this.speed, y + (int) this.speed))
          return;
        MyLocation myLocation = this.position + new MyLocation(-this.speed, this.speed);
        if (myLocation.y < this.map.Height - this.radius)
          this.WalkToPosition = myLocation;
      }
      this.CreatureMoveSurroundings();
    }
  }

  public virtual void MoveLeft(int extraBits = 0)
  {
    if ((double) this.transformscale > 0.0)
      this.SetScale(-1f);
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.TowerMoveLeft();
      if (ExtraMoveBits.HighJump(extraBits) && (ZComponent) this.tower != (object) null)
      {
        this.tower.TowerMoveUp();
      }
      else
      {
        if (!ExtraMoveBits.LongJump(extraBits) || !((ZComponent) this.tower != (object) null))
          return;
        this.tower.TowerMoveDown();
      }
    }
    else
    {
      if (this.game.isClient && !this.game.resyncing && this.parent != null && (UnityEngine.Object) this.parent.torquing != (UnityEngine.Object) null && (ZComponent) this.parent.torquing.creature == (object) this)
        this.parent.torquing.Moved();
      this.validX = (FixedInt) this.GetX();
      this.validY = (FixedInt) this.GetY();
      if (this.CheckWalkAngleLeft((int) this.validX, (int) this.validY, new MyLocation(0, -1)))
      {
        int num = 0;
        bool flag = false;
        for (int index = 1; index < 6; ++index)
        {
          if (!this.CheckWalkAngleLeft((int) this.validX, (int) this.validY - index, new MyLocation(0, -1)) || this.map.CheckBottomRight((int) this.validX, (int) this.validY - index, this.radius, this, Inert.mask_entity_movement) != null)
          {
            num = index;
            flag = true;
            break;
          }
        }
        if (!flag && (!this.waterWalking || this.position.y > this.radius || this.position.x <= 0 || this.position.x >= this.map.Width) && (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead || this.ShouldFall()))
        {
          if (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead)
            return;
          this.velocity.y = (FixedInt) 1;
          this.velocity.x = (FixedInt) -1;
          if (this.isMoving)
            return;
          this.movementFromInput = true;
          this.moving = this.game.ongoing.RunCoroutine(this.Move());
          return;
        }
        if (num > 1)
        {
          MyLocation myLocation = this.position + new MyLocation(-this.speed, (FixedInt) (num * -1));
          if (myLocation.y < this.map.Height - this.radius)
          {
            if (!this.CheckWalkAngle((int) myLocation.x, (int) myLocation.y, new MyLocation((FixedInt) -1, (FixedInt) 734003L)))
              return;
            this.WalkToPosition = myLocation;
          }
        }
        else if (this.CheckWalkAngle((int) this.validX, (int) this.validY, new MyLocation((FixedInt) -1, (FixedInt) 734003L)))
          this.WalkToPosition = this.position + new MyLocation(-this.speed, (FixedInt) 0);
      }
      else
      {
        this.validX = this.position.x - this.speed;
        this.validY = this.position.y + this.speed;
        if (this.validY < this.map.Height - this.radius)
        {
          if (!this.CheckWalkAngle((int) this.validX, (int) this.validY, new MyLocation((FixedInt) -1, (FixedInt) 734003L)))
            return;
          this.WalkToPosition = new MyLocation(this.validX, this.validY);
        }
      }
      this.CreatureMoveSurroundings();
    }
  }

  public virtual void MoveRight(int extraBits = 0)
  {
    if ((double) this.transformscale < 0.0)
      this.SetScale(1f);
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.TowerMoveRight();
      if (ExtraMoveBits.HighJump(extraBits) && (ZComponent) this.tower != (object) null)
      {
        this.tower.TowerMoveUp();
      }
      else
      {
        if (!ExtraMoveBits.LongJump(extraBits) || !((ZComponent) this.tower != (object) null))
          return;
        this.tower.TowerMoveDown();
      }
    }
    else
    {
      if (this.game.isClient && !this.game.resyncing && this.parent != null && (UnityEngine.Object) this.parent.torquing != (UnityEngine.Object) null && (ZComponent) this.parent.torquing.creature == (object) this)
        this.parent.torquing.Moved();
      this.validX = (FixedInt) this.GetX();
      this.validY = (FixedInt) this.GetY();
      if (this.CheckWalkAngleRight((int) this.validX, (int) this.validY, new MyLocation(0, -1)))
      {
        int num = 0;
        bool flag = false;
        for (int index = 1; index < 6; ++index)
        {
          if (!this.CheckWalkAngleRight((int) this.validX, (int) this.validY - index, new MyLocation(0, -1)) || this.map.CheckBottomLeft((int) this.validX, (int) this.validY - index, this.radius, this, Inert.mask_entity_movement) != null)
          {
            num = index;
            flag = true;
            break;
          }
        }
        if (!flag && (!this.waterWalking || this.position.y > this.radius || this.position.x <= 0 || this.position.x >= this.map.Width) && (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead || this.ShouldFall()))
        {
          if (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead)
            return;
          this.velocity.y = (FixedInt) 1;
          this.velocity.x = (FixedInt) 1;
          if (this.isMoving)
            return;
          this.movementFromInput = true;
          this.moving = this.game.ongoing.RunCoroutine(this.Move());
          return;
        }
        if (num > 1)
        {
          MyLocation myLocation = this.position + new MyLocation(this.speed, (FixedInt) (num * -1));
          if (myLocation.y < this.map.Height - this.radius)
          {
            if (!this.CheckWalkAngle((int) myLocation.x, (int) myLocation.y, new MyLocation((FixedInt) 1, (FixedInt) 734003L)))
              return;
            this.WalkToPosition = myLocation;
          }
        }
        else if (this.CheckWalkAngle((int) this.validX, (int) this.validY, new MyLocation((FixedInt) 1, (FixedInt) 734003L)))
          this.WalkToPosition = this.position + new MyLocation(this.speed, (FixedInt) 0);
      }
      else
      {
        this.validX = this.position.x + this.speed;
        this.validY = this.position.y + this.speed;
        if (this.validY < this.map.Height - this.radius)
        {
          if (!this.CheckWalkAngle((int) this.validX, (int) this.validY, new MyLocation((FixedInt) 1, (FixedInt) 734003L)))
            return;
          this.WalkToPosition = new MyLocation(this.validX, this.validY);
        }
      }
      this.CreatureMoveSurroundings();
    }
  }

  public bool CheckWalkAngleLeft(int x, int y, MyLocation velocity)
  {
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num < 0)
      num += this.zb.Count;
    if (num < 0 || this.zb.Count == 0)
    {
      Debug.LogError((object) ("Error a: " + (object) num + " zb: " + (object) this.zb.Count + " vel: " + (object) velocity + " aov: " + (object) Inert.AngleOfVelocity(velocity)));
      if (num < 0)
      {
        if (this.zb.Count == 0)
          this.zb = MapGenerator.getOutlineArray(this.radius);
        while (num < 0 && this.zb.Count > 0)
          num += this.zb.Count;
      }
    }
    for (int index1 = 0; index1 < this.radius * 2; ++index1)
    {
      int index2 = (index1 + num) % this.zb.Count;
      if (this.zb[index2].x <= 0 && !this.map.CheckPosition(x + this.zb[index2].x, y + this.zb[index2].y, this, Inert.mask_entity_movement) && (this.position.y <= this.map.Height - this.radius || velocity.y <= 0))
        return false;
    }
    return true;
  }

  public bool CheckWalkAngleRight(int x, int y, MyLocation velocity)
  {
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num < 0)
      num += this.zb.Count;
    if (num < 0 || this.zb.Count == 0)
    {
      Debug.LogError((object) ("Error a: " + (object) num + " zb: " + (object) this.zb.Count + " vel: " + (object) velocity + " aov: " + (object) Inert.AngleOfVelocity(velocity)));
      if (num < 0)
      {
        if (this.zb.Count == 0)
          this.zb = MapGenerator.getOutlineArray(this.radius);
        while (num < 0 && this.zb.Count > 0)
          num += this.zb.Count;
      }
    }
    for (int index1 = 0; index1 < this.radius * 2; ++index1)
    {
      int index2 = (index1 + num) % this.zb.Count;
      if (this.zb[index2].x >= 0 && !this.map.CheckPosition(x + this.zb[index2].x, y + this.zb[index2].y, this, Inert.mask_entity_movement) && (this.position.y <= this.map.Height - this.radius || velocity.y <= 0))
        return false;
    }
    return true;
  }

  public bool CheckWalkAngle(int x, int y, MyLocation velocity)
  {
    int num = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num < 0)
      num += this.zb.Count;
    if (num < 0 || this.zb.Count == 0)
    {
      Debug.LogError((object) ("Error a: " + (object) num + " zb: " + (object) this.zb.Count + " vel: " + (object) velocity + " aov: " + (object) Inert.AngleOfVelocity(velocity)));
      if (num < 0)
      {
        if (this.zb.Count == 0)
          this.zb = MapGenerator.getOutlineArray(this.radius);
        while (num < 0 && this.zb.Count > 0)
          num += this.zb.Count;
      }
    }
    for (int index1 = 0; index1 < this.radius * 2; ++index1)
    {
      int index2 = (index1 + num) % this.zb.Count;
      if (!this.map.CheckPosition(x + this.zb[index2].x, y + this.zb[index2].y, this, Inert.mask_entity_movement) && (this.position.y <= this.map.Height - this.radius || velocity.y <= 0))
        return false;
    }
    return !this.isMoving;
  }

  public void MoveRight2()
  {
    if ((double) this.transformscale < 0.0)
      this.SetScale(1f);
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.TowerMoveRight();
    }
    else
    {
      if (this.game.isClient && !this.game.resyncing && this.parent != null && (UnityEngine.Object) this.parent.torquing != (UnityEngine.Object) null && (ZComponent) this.parent.torquing.creature == (object) this)
        this.parent.torquing.Moved();
      int x = this.GetX();
      int y = this.GetY();
      if (this.map.CheckTopRight(x, y, this.radius, this, Inert.mask_entity_movement) != null || this.map.CheckRightTop(x, y, this.radius, this, Inert.mask_entity_movement) != null)
        return;
      Coords coords1 = this.map.CheckRightBottom_FromTop(x, y, this.radius, this, Inert.mask_entity_movement);
      if (coords1 != null && coords1.y - y >= -(this.radius >> 1) - 3 && coords1.y - y <= -5)
        return;
      Coords coords2 = this.map.CheckBottomRight(x, y, this.radius, this, Inert.mask_entity_movement);
      if (coords2 == null)
      {
        int num = 0;
        for (int index = 1; index < 6; ++index)
        {
          coords2 = this.map.CheckBottomLeft(x, y - index, this.radius, this, Inert.mask_entity_movement) ?? this.map.CheckBottomRight(x, y - index, this.radius, this, Inert.mask_entity_movement);
          if (coords2 != null)
          {
            num = index;
            break;
          }
        }
        if (coords2 == null && (!this.waterWalking || this.position.y > this.radius || this.position.x <= 0 || this.position.x >= this.map.Width))
        {
          if (this.type == CreatureType.Monkey && this.race != CreatureRace.Undead)
            return;
          this.velocity.y = (FixedInt) 1;
          this.velocity.x = (FixedInt) 1;
          if (this.isMoving)
            return;
          this.movementFromInput = true;
          this.moving = this.game.ongoing.RunCoroutine(this.Move());
          return;
        }
        if (num > 1)
        {
          MyLocation myLocation = this.position + new MyLocation(this.speed, (FixedInt) (num * -1));
          if (myLocation.y < this.map.Height - this.radius)
            this.WalkToPosition = myLocation;
        }
        else
          this.WalkToPosition = this.position + new MyLocation(this.speed, (FixedInt) 0);
      }
      else
      {
        if (!this.ValidateLeft(x + (int) this.speed, y + (int) this.speed))
          return;
        MyLocation myLocation = this.position + new MyLocation(this.speed, this.speed);
        if (myLocation.y < this.map.Height - this.radius)
          this.WalkToPosition = myLocation;
      }
      this.CreatureMoveSurroundings();
    }
  }

  public bool ValidateLeft(int x, int y)
  {
    return this.map.CheckTopLeft(x, y, this.radius, this, 0) == null && this.map.CheckLeftTop(x, y, this.radius, this, 0) == null;
  }

  public bool ValidateRight(int x, int y)
  {
    return this.map.CheckTopRight(x, y, this.radius, this, 0) == null && this.map.CheckRightTop(x, y, this.radius, this, 0) == null;
  }

  public virtual void OnNextTurn()
  {
  }

  public virtual void DoFlipFlop(float scaleX)
  {
    if ((double) this.transformscale == (double) scaleX)
      return;
    this.SetScale(scaleX);
  }

  public virtual void DoHighJump(int extraBits)
  {
    if (this.isMoving)
      return;
    if (this.flying && !this.entangledOrGravity)
    {
      this.velocity = new MyLocation(((double) this.transformscale > 0.0 ? 1 : -1) * 4, 10);
      this.moving = this.game.ongoing.RunCoroutine(this.FlyMove(true));
    }
    else
    {
      this.usingGlide = !ExtraMoveBits.NoGlide(extraBits);
      this.HighJump(extraBits);
      if (!this.game.isClient)
        return;
      AudioManager.Play(!this.moneyBags || this.audioBags ? AudioManager.instance.jumpHigh : AudioManager.instance.moneyJump);
    }
  }

  public virtual void DoLongJump(int extraBits)
  {
    if (this.isMoving)
      return;
    if (this.flying && !this.entangledOrGravity)
    {
      this.velocity = new MyLocation((FixedInt) ((double) this.transformscale > 0.0 ? 1 : -1) * (FixedInt) 4456448L, (FixedInt) -9);
      this.moving = this.game.ongoing.RunCoroutine(this.FlyMove(true));
    }
    else
    {
      this.usingGlide = !ExtraMoveBits.NoGlide(extraBits);
      this.LongJump(extraBits);
      if (!this.game.isClient)
        return;
      AudioManager.Play(!this.moneyBags || this.audioBags ? AudioManager.instance.jumpLong : AudioManager.instance.moneyJump);
    }
  }

  public virtual void DoControlledJump(int extraBits)
  {
    if (this.isMoving)
      return;
    this.movementFromInput = true;
    if (this.flying && !this.entangledOrGravity)
    {
      this.moving = this.game.ongoing.RunCoroutine(this.FlyMove(true));
    }
    else
    {
      if ((ZComponent) this.mount != (object) null)
        this.Demount();
      this.moving = this.game.ongoing.RunCoroutine(this.Move());
    }
  }

  public FixedInt GravityPull()
  {
    return this.gravitionalPull && this.familiarLevelCosmos < 5 ? (FixedInt) 1 - (FixedInt) (5 - this.familiarLevelCosmos) * (FixedInt) 41943L : (FixedInt) 1;
  }

  public bool HighJump(int extraBits)
  {
    if (this.isMoving)
      return false;
    this.movementFromInput = true;
    this.velocity.x = this.GetHighJumpX(!ExtraMoveBits.NoIceJump(extraBits)) * (this.IsSprinting() ? (FixedInt) 1153433L : (FixedInt) 1) * ((double) this.transformscale > 0.0 ? 1 : -1);
    this.velocity.y = this.GetHighJumpY(!ExtraMoveBits.NoIceJump(extraBits)) * (this.IsSprinting() ? (FixedInt) 1153433L : (FixedInt) 1) * this.GravityPull();
    if (this.gliding && !this.entangledOrGravity)
      this.velocity.y *= 1258291L;
    if ((ZComponent) this.mount != (object) null)
    {
      this.Demount();
      this.velocity.x *= 1572864L;
    }
    this.moving = this.game.ongoing.RunCoroutine(this.Move());
    return true;
  }

  public bool LongJump(int extraBits)
  {
    if (this.isMoving)
      return false;
    this.movementFromInput = true;
    this.velocity.x = this.GetLongJumpX(!ExtraMoveBits.NoIceJump(extraBits)) * (this.IsSprinting() ? (FixedInt) 1310720L : (FixedInt) 1) * ((double) this.transformscale > 0.0 ? 1 : -1);
    this.velocity.y = this.GetLongJumpY(!ExtraMoveBits.NoIceJump(extraBits)) * this.GravityPull();
    if ((ZComponent) this.mount != (object) null)
      this.Demount();
    if (!this.isMoving)
      this.moving = this.game.ongoing.RunCoroutine(this.Move());
    return true;
  }

  public bool IsSprinting()
  {
    return this.sprinting != 0 && (this.sprinting > 3 && (double) this.transformscale > 0.0 || this.sprinting < 3 && (double) this.transformscale < 0.0) && this.game.AllowExpansion;
  }

  public void MoneyBags()
  {
    ++this.moneyFrames;
    if (this.moneyFrames <= 30)
      return;
    this.moneyFrames = 0;
    ZEffector zeffector = ZEffector.Create(this.game, this.audioBags ? Inert.Instance.audioBags : Inert.Instance.moneyBags, Vector3.zero, Quaternion.identity, this.game.GetMapTransform());
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
    {
      zeffector.transform.position = this.transform.position;
      if (zeffector.variable == 1)
        zeffector.gameObject.GetComponent<SpriteRenderer>().color = ColorHSV.ToColor(new ColorHSV(UnityEngine.Random.Range(0.0f, 1f), 1f, 1f));
    }
    zeffector.position = this.position;
    zeffector.game = this.game;
    zeffector.whoSummoned = this;
    zeffector.followParent = false;
    zeffector.collider.world = zeffector.world;
    this.effectors.Add(zeffector);
    zeffector.collider.Move(this.position);
  }

  public MyLocation WalkToPosition
  {
    set
    {
      if (this.moneyBags)
        this.MoneyBags();
      this.SetPosition(value);
      if (this.isPawn)
        return;
      this.parent?.awards.Moved();
    }
  }

  public MyLocation MoveToPosition
  {
    set => this.SetPosition(value);
  }

  public MyLocation GetStartPosition(MyLocation position)
  {
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num1 < 0)
      num1 += this.zb.Count;
    int num2 = (int) position.x;
    int num3 = (int) position.y - 3;
    int num4 = 0;
    int num5 = 0;
    int num6 = this.radius * 2;
    if (num3 > this.map.Height - this.radius)
      num3 = this.map.Height - this.radius;
    int num7 = 0;
    while (true)
    {
      do
      {
        for (int index1 = num3; index1 >= num7; --index1)
        {
          for (int index2 = 0; index2 < this.radius * 2; ++index2)
          {
            int index3 = (index2 + num1) % this.zb.Count;
            if (!this.map.CheckPosition(num2 + this.zb[index3].x, index1 + this.zb[index3].y, this, Inert.mask_movement_NoEffector))
            {
              if (num5 < num6)
              {
                num5 = 0;
                break;
              }
              if (this.map.CheckCircle(num2, index1 + 1, this.radius - 1, this, Inert.mask_movement_NoEffector))
                return new MyLocation(num2, index1 + 1);
              num5 = 0;
              break;
            }
          }
          ++num5;
        }
        ++num4;
        if (num4 < 10)
        {
          num3 = this.map.Height - this.radius;
          num2 += 100;
        }
        else
          goto label_22;
      }
      while (num2 < this.map.Width);
      num2 = 800;
    }
label_22:
    return position;
  }

  public void SetStartPosition() => this.SetPosition(this.GetStartPosition(this.position));

  public void InstantFall()
  {
    if (this.inWater || this.flying || this.isDead)
      return;
    int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num1 < 0)
      num1 += this.zb.Count;
    for (int index1 = this.position.y.ToInt(); index1 >= this.radius; --index1)
    {
      for (int index2 = 0; index2 < this.radius * 2; ++index2)
      {
        int index3 = (index2 + num1) % this.zb.Count;
        int x = (int) this.position.x;
        int num2 = (int) this.position.y - 1;
        if (!this.map.CheckPosition(x + this.zb[index3].x, num2 + this.zb[index3].y, this, Inert.mask_movement_NoEffector))
          return;
      }
      this.SetPosition(new MyLocation(this.position.x, this.position.y - 1));
    }
    if (this.waterWalking || this.familiarLevelFrost > 0)
      return;
    this.EnteredWater();
  }

  public bool LandUnder(int posX, int posY)
  {
    int num = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num < 0)
      num += this.zb.Count;
    if ((this.waterWalking || this.familiarLevelFrost > 0 || this.frostWalking) && posX > 0 && posX < this.map.Width)
      return true;
    for (int index1 = posY - 1; index1 >= 0; --index1)
    {
      for (int index2 = 0; index2 < this.radius * 2; ++index2)
      {
        int index3 = (index2 + num) % this.zb.Count;
        if (!this.map.CheckPosition(posX + this.zb[index3].x, index1 + this.zb[index3].y, this, Inert.mask_movement_NoEffector))
          return true;
      }
    }
    return false;
  }

  public virtual bool ShouldFall(bool gliding = true, bool ignoreFlight = false)
  {
    if (this.inWater || this.isDead)
      return false;
    if (this.position.y < this.radius)
    {
      this.Drown();
      return false;
    }
    if (this.flying && !this.entangledOrGravity && !ignoreFlight || (ZComponent) this.mount != (object) null || this.waterWalking && this.position.y <= this.radius && this.position.x > 0 && this.position.x < this.map.Width)
    {
      if (!((ZComponent) this.mount == (object) null) || !(this.position.x < 0) && !(this.position.x >= this.map.Width))
        return false;
      this.SetPosition(new MyLocation(this.position.x, this.position.y - 1));
      if (this.flying)
        this.RemoveFlight();
      return true;
    }
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
      return false;
    }
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    if (this.velocity.y != 0)
      return false;
    int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num1 < 0)
      num1 += this.zb.Count;
    for (int index1 = 0; index1 < this.radius * 2; ++index1)
    {
      int index2 = (index1 + num1) % this.zb.Count;
      int x = (int) this.position.x;
      int num2 = (int) this.position.y - 3;
      if (!this.map.CheckPosition(x + this.zb[index2].x, num2 + this.zb[index2].y, this, Inert.mask_movement_NoEffector))
        return false;
    }
    if (gliding && this.CheckClimbingHooks())
      return false;
    this.SetPosition(new MyLocation(this.position.x, this.position.y - 1));
    return true;
  }

  public bool CheckClimbingHooks()
  {
    if (this.minerMarket != null && this.minerMarket.Has(MinerMarket.Types.Platinum_Climbing_Hooks))
    {
      int x = (int) this.position.x;
      int y = (int) this.position.y;
      if (this.velocity.y <= 0)
      {
        if (this.velocity.x == 0)
        {
          for (int index = -5; index <= 5; ++index)
          {
            if (!this.map.CheckPosition(x + this.radius + 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x + this.radius + 2, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 2, y + index, this, Inert.mask_movement_NoEffector))
              return true;
          }
        }
        else if (this.velocity.x > 0)
        {
          for (int index = -5; index <= 5; ++index)
          {
            if (!this.map.CheckPosition(x + this.radius + 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x + this.radius + 2, y + index, this, Inert.mask_movement_NoEffector))
              return true;
          }
        }
        else
        {
          for (int index = -5; index <= 5; ++index)
          {
            if (!this.map.CheckPosition(x - this.radius - 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 2, y + index, this, Inert.mask_movement_NoEffector))
              return true;
          }
        }
      }
      else if (this.velocity.x == 0)
      {
        for (int index = 5; index >= -5; --index)
        {
          if (!this.map.CheckPosition(x + this.radius + 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x + this.radius + 2, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 2, y + index, this, Inert.mask_movement_NoEffector))
            return true;
        }
      }
      else if (this.velocity.x > 0)
      {
        for (int index = 5; index >= -5; --index)
        {
          if (!this.map.CheckPosition(x + this.radius + 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x + this.radius + 2, y + index, this, Inert.mask_movement_NoEffector))
            return true;
        }
      }
      else
      {
        for (int index = 5; index >= -5; --index)
        {
          if (!this.map.CheckPosition(x - this.radius - 1, y + index, this, Inert.mask_movement_NoEffector) || !this.map.CheckPosition(x - this.radius - 2, y + index, this, Inert.mask_movement_NoEffector))
            return true;
        }
      }
    }
    return false;
  }

  public void SetRadius(int i)
  {
    int num1 = 18;
    if (this.spellEnum != SpellEnum.None)
      num1 = this.baseCreature.radius;
    i = Mathf.Clamp(i, 1, 8192);
    FixedInt fixedInt = (FixedInt) i / num1;
    float num2 = fixedInt.ToFloat();
    if ((UnityEngine.Object) this.transform != (UnityEngine.Object) null)
      this.transform.localScale = new Vector3(num2, num2, 1f);
    this.radius = i;
    this.zb = MapGenerator.getOutlineArray(this.radius);
    this.scale = num2;
    this.scaleFixed = fixedInt;
    this.collider.radius = this.radius;
    this.collider.Moved();
    this.SetScale(1f);
  }

  public virtual IEnumerator<float> FlyMove(bool fromInput = false)
  {
    ZCreature zcreature = this;
    if (zcreature.invulnerable > 1000 && zcreature.game.isSandbox && !zcreature.game.AllowMovement && Client._tutorial != null)
    {
      zcreature.KillMovement();
    }
    else
    {
      zcreature.isMoving = true;
      zcreature.loopCount = 0;
      zcreature.inWater = false;
      if (zcreature.game.serverState.busy == ServerState.Busy.No)
        zcreature.game.serverState.busy = ServerState.Busy.Moving;
      yield return 0.0f;
      int size = zcreature.radius;
      if (zcreature.zb == null)
        zcreature.zb = MapGenerator.getOutlineArray(zcreature.radius);
      if ((ZComponent) zcreature.mount != (object) null)
        zcreature.Demount();
      while (!zcreature.isDead)
      {
        ++zcreature.loopCount;
label_9:
        if (zcreature.velocity.y == 0 && zcreature.velocity.x == 0)
        {
          zcreature.moving = (IEnumerator<float>) null;
          zcreature.isMoving = false;
          if (zcreature.removeFlight)
          {
            zcreature.RemoveFlight();
            yield break;
          }
          else if (!zcreature.gravitionalPull || ZComponent.IsNull((ZComponent) zcreature) || !zcreature.ShouldFall())
          {
            yield break;
          }
          else
          {
            zcreature.Fall();
            yield break;
          }
        }
        else
        {
          zcreature.pX = zcreature.position.x;
          zcreature.pY = zcreature.position.y;
          FixedInt fixedInt1 = zcreature.position.x + zcreature.velocity.x;
          FixedInt fixedInt2 = zcreature.position.y + zcreature.velocity.y;
          zcreature.validX = zcreature.pX;
          zcreature.validY = zcreature.pY;
          int num1 = 1;
          FixedInt x1 = zcreature.velocity.x;
          FixedInt y1 = zcreature.velocity.y;
          FixedInt fixedInt3 = zcreature.velocity.x;
          FixedInt fixedInt4 = zcreature.velocity.y;
          if (x1 > 1 || x1 < -1 || y1 > 1 || y1 < -1)
          {
            num1 = !(Mathd.Abs(x1) > Mathd.Abs(y1)) ? (int) Mathd.Abs(y1) + 1 : (int) Mathd.Abs(x1) + 1;
            fixedInt3 = zcreature.velocity.x / num1;
            fixedInt4 = zcreature.velocity.y / num1;
          }
          while (num1 > 0)
          {
            --num1;
            int num2 = (int) (fixedInt3 + zcreature.pX);
            int num3 = (int) (fixedInt4 + zcreature.pY);
            int num4 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(zcreature.velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * zcreature.zb.Count - size).ToInt();
            if (num4 < 0)
              num4 += zcreature.zb.Count;
            int mask = Inert.mask_entity_movement;
            if (zcreature.phantom)
              mask = 35328;
            for (int index1 = 0; index1 < size * 2; ++index1)
            {
              int index2 = (index1 + num4) % zcreature.zb.Count;
              int x2 = num2 + zcreature.zb[index2].x;
              int y2 = num3 + zcreature.zb[index2].y;
              if ((zcreature.phantom ? (!zcreature.map.CheckPositionPhantom(x2, y2, zcreature, mask) ? 1 : 0) : (!zcreature.map.CheckPosition(x2, y2, zcreature, mask) ? 1 : 0)) != 0)
              {
                FixedInt y3 = zcreature.velocity.y;
                if (zcreature.velocity.y > 0 && zcreature.canMount)
                {
                  zcreature.velocity.y = (FixedInt) -1;
                  zcreature.map.CheckPositionOnlyEntities((int) zcreature.validX, (int) zcreature.validY + size + 30, zcreature, Inert.mask_movement_NoEffector);
                  if ((ZComponent) zcreature.mount != (object) null)
                  {
                    zcreature.moving = (IEnumerator<float>) null;
                    zcreature.isMoving = false;
                    zcreature.velocity.x = (FixedInt) 0;
                    zcreature.velocity.y = (FixedInt) 0;
                    zcreature.addedVelocity.x = (FixedInt) 0;
                    zcreature.addedVelocity.y = (FixedInt) 0;
                    if (zcreature.removeFlight)
                    {
                      zcreature.RemoveFlight();
                      yield break;
                    }
                    else if (!zcreature.gravitionalPull || ZComponent.IsNull((ZComponent) zcreature) || !zcreature.ShouldFall())
                    {
                      yield break;
                    }
                    else
                    {
                      zcreature.Fall();
                      yield break;
                    }
                  }
                }
                zcreature.velocity.y = y3;
                if (!fromInput && (zcreature.velocity.y > 0 || zcreature.velocity.y < 0 && zcreature.loopCount > 2 && zcreature.loopCount < 6) && zcreature.position.y < zcreature.map.Height - zcreature.radius - 10 && zcreature.velocity.magnitude > 1)
                {
                  MyLocation zero = MyLocation.zero;
                  for (int index3 = -8; index3 <= 8; ++index3)
                  {
                    for (int index4 = -8; index4 <= 8; ++index4)
                    {
                      if (zcreature.game.map.SpellCheckPosition(x2 + index3, y2 + index4, (ZCreature) null, Inert.mask_movement_NoEffector))
                      {
                        zero.x += index3;
                        zero.y += index4;
                      }
                    }
                  }
                  zero.Normalize();
                  MyLocation.Reflect(zcreature.velocity, ref zero, out zcreature.velocity);
                  zcreature.velocity.Multiply(((FixedInt) 524288L).RawValue, out zcreature.velocity);
                  zcreature.velocity.x = -zcreature.velocity.x;
                  yield return 0.0f;
                  goto label_9;
                }
                else
                {
                  zcreature.velocity.y = (FixedInt) 0;
                  zcreature.velocity.x = (FixedInt) 0;
                  zcreature.addedVelocity.x = (FixedInt) 0;
                  zcreature.addedVelocity.y = (FixedInt) 0;
                  if ((ZComponent) zcreature.mount == (object) null && (Mathd.Abs(zcreature.validX - zcreature.position.x) > 104857L || Mathd.Abs(zcreature.validY - zcreature.position.y) > 104857L))
                    zcreature.SetPosition(new MyLocation(zcreature.validX, zcreature.validY));
                  zcreature.moving = (IEnumerator<float>) null;
                  zcreature.isMoving = false;
                  zcreature.CreatureMoveSurroundings();
                  if (zcreature.removeFlight)
                  {
                    zcreature.RemoveFlight();
                    yield break;
                  }
                  else if (!zcreature.gravitionalPull || ZComponent.IsNull((ZComponent) zcreature) || !zcreature.ShouldFall())
                  {
                    yield break;
                  }
                  else
                  {
                    zcreature.Fall();
                    yield break;
                  }
                }
              }
              else if ((x2 >= zcreature.map.Width || x2 < 0 || y2 < 0 || y2 >= zcreature.map.Height) && zcreature.flying && !zcreature.entangledOrGravity)
              {
                if (x2 >= zcreature.map.Width && zcreature.position.x < zcreature.map.Width && zcreature.velocity.x > 0 || x2 < 0 && zcreature.position.x > 0 && zcreature.velocity.x < 0)
                {
                  zcreature.velocity.x = (FixedInt) 0;
                  zcreature.validX = Mathd.Clamp(zcreature.validX, (FixedInt) size, (FixedInt) (zcreature.map.Width - size));
                  zcreature.validY = zcreature.position.y;
                  zcreature.SetPosition(new MyLocation(zcreature.validX, zcreature.validY));
                  zcreature.CreatureMoveSurroundings();
                  yield return 0.0f;
                  goto label_9;
                }
                else if (y2 >= zcreature.map.Height)
                {
                  zcreature.velocity.y = (FixedInt) 0;
                  zcreature.validX = zcreature.position.x;
                  zcreature.validY = Mathd.Clamp(zcreature.validY, (FixedInt) size, (FixedInt) (zcreature.map.Height - size));
                  zcreature.SetPosition(new MyLocation(zcreature.validX, zcreature.validY));
                  zcreature.CreatureMoveSurroundings();
                  yield return 0.0f;
                  goto label_9;
                }
              }
            }
            zcreature.validX = zcreature.pX;
            zcreature.validY = zcreature.pY;
            zcreature.pX = zcreature.pX + fixedInt3;
            zcreature.pY = zcreature.pY + fixedInt4;
          }
          zcreature.SetPosition(zcreature.position + zcreature.velocity);
          zcreature.CreatureMoveSurroundings();
          zcreature.game.CreatureMoveSurroundings(zcreature.position - zcreature.velocity, size, zcreature.collider);
          if (zcreature.addVelocity)
          {
            zcreature.addVelocity = false;
            zcreature.velocity = zcreature.velocity + zcreature.addedVelocity;
            zcreature.addedVelocity.x = (FixedInt) 0;
            zcreature.addedVelocity.y = (FixedInt) 0;
          }
          if (zcreature.position.x < 0 || zcreature.velocity.x > 0 && zcreature.position.x < 0 || zcreature.position.x >= zcreature.map.Width || zcreature.velocity.x < 0 && zcreature.position.x >= zcreature.map.Width || zcreature.position.y >= zcreature.map.Height)
          {
            zcreature.velocity.y += zcreature.map.Gravity;
          }
          else
          {
            if (zcreature.velocity.y > 0)
            {
              zcreature.velocity.y += zcreature.map.Gravity;
              if (zcreature.velocity.y < 0)
                zcreature.velocity.y = (FixedInt) 0;
            }
            else if (zcreature.velocity.y < 0)
            {
              zcreature.velocity.y -= zcreature.map.Gravity;
              if (zcreature.velocity.y > 0)
                zcreature.velocity.y = (FixedInt) 0;
            }
            if (zcreature.velocity.x > 0)
            {
              zcreature.velocity.x += zcreature.map.Gravity;
              if (zcreature.velocity.x < 0)
                zcreature.velocity.x = (FixedInt) 0;
            }
            else if (zcreature.velocity.x < 0)
            {
              zcreature.velocity.x -= zcreature.map.Gravity;
              if (zcreature.velocity.x > 0)
                zcreature.velocity.x = (FixedInt) 0;
            }
          }
          if (zcreature.position.y < zcreature.radius)
          {
            zcreature.Drown();
            yield break;
          }
          else
            yield return 0.0f;
        }
      }
      if (zcreature.removeFlight && !ZComponent.IsNull((ZComponent) zcreature))
        zcreature.RemoveFlight();
      else if (zcreature.gravitionalPull && !ZComponent.IsNull((ZComponent) zcreature) && zcreature.ShouldFall())
        zcreature.Fall();
    }
  }

  public bool ShouldBounce(int targetX, int targetY)
  {
    if (!this.game.AllowBounce || this.movementFromInput || this.glideIsActive && this.gliding || (int) this.position.y > this.map.Height - this.radius + 2 || this.velocity.y >= -20 || !ZComponent.IsNull((ZComponent) this.tower))
      return false;
    MyLocation normal = MyLocation.zero;
    targetY -= 2;
    for (int index1 = -this.radius; index1 <= this.radius; ++index1)
    {
      for (int index2 = -2; index2 <= 2; ++index2)
      {
        if (this.map.CheckPosition(targetX + index1, targetY + index2, this, Inert.mask_movement_NoEffector))
        {
          normal.x += index1;
          normal.y += index2;
        }
      }
    }
    normal = !(normal.x == 0) ? (!(normal.x < 0) ? new MyLocation(5, 10) : new MyLocation(-5, 10)) : new MyLocation(1, 10);
    normal = normal.normalized;
    MyLocation.Reflect(this.velocity, ref normal, out this.velocity);
    this.velocity.x = -this.velocity.x * 943718L;
    this.velocity = this.velocity * 262144L;
    this.loopCount = 0;
    return true;
  }

  public bool InDarkTotem() => false;

  public virtual IEnumerator<float> WolfLeap()
  {
    ZCreature zcreature = this;
    if (zcreature.zb == null)
      zcreature.zb = MapGenerator.getOutlineArray(zcreature.radius);
    zcreature.isMoving = true;
    yield return 0.0f;
    if (!zcreature.isDead)
    {
      zcreature.Demount();
      zcreature.loopCount = 0;
      bool animateState = !(zcreature.velocity.y < 0);
      while (!zcreature.isDead)
      {
        ++zcreature.loopCount;
        zcreature.pX = zcreature.position.x;
        zcreature.pY = zcreature.position.y;
        FixedInt fixedInt1 = zcreature.position.x + zcreature.velocity.x;
        FixedInt fixedInt2 = zcreature.position.y + zcreature.velocity.y;
        zcreature.validX = zcreature.pX;
        zcreature.validY = zcreature.pY;
        int num1 = 1;
        FixedInt x = zcreature.velocity.x;
        FixedInt y1 = zcreature.velocity.y;
        FixedInt fixedInt3 = zcreature.velocity.x;
        FixedInt y2 = zcreature.velocity.y;
        if (animateState)
        {
          if (y1 < 0)
          {
            animateState = false;
            if ((UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null)
              zcreature.animator.Play(AnimateState.Fall);
          }
        }
        else if (y1 > 0)
        {
          animateState = true;
          if ((UnityEngine.Object) zcreature.animator != (UnityEngine.Object) null)
            zcreature.animator.Play(AnimateState.Jump);
        }
        FixedInt fixedInt4;
        if (x > 1 || x < -1 || y1 > 1 || y1 < -1)
        {
          num1 = !(Mathd.Abs(x) > Mathd.Abs(y1)) ? (int) Mathd.Abs(y1) + 1 : (int) Mathd.Abs(x) + 1;
          fixedInt3 = zcreature.velocity.x / num1;
          fixedInt4 = zcreature.velocity.y / num1;
        }
        else
        {
          zcreature.velocity.y = (FixedInt) -1;
          fixedInt4 = (FixedInt) -1;
        }
        if (zcreature.velocity.y < 0 && zcreature.climbingHooksIsActive && zcreature.CheckClimbingHooks())
        {
          zcreature.moving = (IEnumerator<float>) null;
          zcreature.isMoving = false;
          zcreature.velocity.x = (FixedInt) 0;
          zcreature.velocity.y = (FixedInt) 0;
          zcreature.addedVelocity.x = (FixedInt) 0;
          zcreature.addedVelocity.y = (FixedInt) 0;
          break;
        }
        while (num1 > 0)
        {
          --num1;
          int num2 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(zcreature.velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * zcreature.zb.Count - zcreature.radius).ToInt();
          if (num2 < 0)
            num2 += zcreature.zb.Count;
          if (num2 < 0 || zcreature.zb.Count == 0)
          {
            Debug.LogError((object) ("Error a: " + (object) num2 + " zb: " + (object) zcreature.zb.Count + " vel: " + (object) zcreature.velocity + " aov: " + (object) Inert.AngleOfVelocity(zcreature.velocity)));
            if (num2 < 0)
            {
              if (zcreature.zb.Count == 0)
                zcreature.zb = MapGenerator.getOutlineArray(zcreature.radius);
              while (num2 < 0 && zcreature.zb.Count > 0)
                num2 += zcreature.zb.Count;
            }
          }
          for (int index1 = 0; index1 < zcreature.radius * 2; ++index1)
          {
            int index2 = (index1 + num2) % zcreature.zb.Count;
            if (!zcreature.map.CheckPosition((int) (fixedInt3 + zcreature.pX) + zcreature.zb[index2].x, (int) (fixedInt4 + zcreature.pY) + zcreature.zb[index2].y, zcreature, Inert.mask_entity_movement) && (zcreature.position.y <= zcreature.map.Height - zcreature.radius + 2 || zcreature.velocity.y <= 0))
            {
              int num3 = (int) (fixedInt3 + zcreature.pX) + zcreature.zb[index2].x;
              int num4 = (int) (fixedInt4 + zcreature.pY) + zcreature.zb[index2].y;
              zcreature.map.ServerBitBlt(zcreature.spellEnum == SpellEnum.Summon_Alpha_Wolf ? 5 : 36, num3, num4);
              ZSpell.ApplyExplosionForce(zcreature.spellEnum, zcreature.world, new MyLocation(num3, num4), zcreature.spellEnum == SpellEnum.Summon_Alpha_Wolf ? 20 : 15, Curve.None, zcreature.radius, 15, (FixedInt) 2, DamageType.Sunder, zcreature, zcreature.game.turn, Curve.None);
              ZComponent.Instantiate<GameObject>(Inert.GetSpell(SpellEnum.Summon_Wolf).explosion, new Vector3((float) num3, (float) num4), Quaternion.identity, zcreature.game.GetMapTransform());
              zcreature.KillMovement();
              if (!zcreature.ShouldFall())
              {
                yield break;
              }
              else
              {
                zcreature.Fall();
                yield break;
              }
            }
          }
          zcreature.validX = zcreature.pX;
          zcreature.validY = zcreature.pY;
          zcreature.pX = zcreature.pX + fixedInt3;
          zcreature.pY = zcreature.pY + fixedInt4;
        }
        MyLocation position = zcreature.position;
        zcreature.MoveToPosition = zcreature.position + zcreature.velocity;
        zcreature.game.CreatureMoveSurroundings(position, zcreature.radius, zcreature.collider);
        zcreature.CreatureMoveSurroundings();
        if ((ZComponent) zcreature.mount != (object) null)
        {
          zcreature.KillMovement();
          break;
        }
        if (zcreature.position.y < zcreature.radius || zcreature.loopCount > 900)
        {
          zcreature.Drown();
          break;
        }
        if (zcreature.addVelocity)
        {
          zcreature.addVelocity = false;
          zcreature.velocity = zcreature.velocity + zcreature.addedVelocity;
          zcreature.addedVelocity.x = (FixedInt) 0;
          zcreature.addedVelocity.y = (FixedInt) 0;
        }
        if (zcreature.velocity.y > -ZMap.MaxSpeed)
          zcreature.velocity.y += zcreature.map.Gravity;
        yield return 0.0f;
      }
    }
  }

  public IEnumerator<float> CreAbductionMove()
  {
    ZCreature zcreature = this;
    while (true)
    {
      if (zcreature.addVelocity)
      {
        zcreature.addVelocity = false;
        zcreature.addedVelocity.x.RawValue = 0L;
        zcreature.addedVelocity.y.RawValue = 0L;
      }
      yield return 0.0f;
    }
  }

  public virtual IEnumerator<float> Move(bool fromSerialization = false)
  {
    ZCreature zcreature1 = this;
    bool animateState = !(zcreature1.velocity.y < 0);
    if (fromSerialization)
    {
      if (zcreature1.zb == null)
      {
        zcreature1.zb = MapGenerator.getOutlineArray(zcreature1.radius);
        goto label_19;
      }
      else
        goto label_19;
    }
    else if (zcreature1.invulnerable > 1000 && zcreature1.game.isSandbox && !zcreature1.game.AllowMovement && (zcreature1.parent.localTurn > 0 || zcreature1.game.isServer && zcreature1.game.isClient) && Client._tutorial != null)
    {
      zcreature1.KillMovement();
      yield break;
    }
    else
    {
      zcreature1.isMoving = true;
      zcreature1.inWater = false;
      zcreature1.addedVelocity.x = (FixedInt) 0;
      zcreature1.addedVelocity.y = (FixedInt) 0;
      zcreature1.loopCount = 0;
      if (zcreature1.game.serverState.busy == ServerState.Busy.No)
        zcreature1.game.serverState.busy = ServerState.Busy.Moving;
      yield return 0.0f;
      if (zcreature1.isDead)
      {
        yield break;
      }
      else
      {
        zcreature1.Demount();
        zcreature1.tries = 0;
        if (zcreature1.zb == null)
          zcreature1.zb = MapGenerator.getOutlineArray(zcreature1.radius);
      }
    }
label_11:
    ++zcreature1.tries;
    zcreature1.glideIsActive = zcreature1.gliding && zcreature1.usingGlide && (zcreature1.velocity.magnitude > 6 || !zcreature1.LandUnder((int) zcreature1.position.x, (int) zcreature1.position.y));
    zcreature1.climbingHooksIsActive = zcreature1.minerMarket != null && zcreature1.usingGlide && zcreature1.minerMarket.Has(MinerMarket.Types.Platinum_Climbing_Hooks);
    if (zcreature1.glideIsActive && zcreature1.velocity.x == 0)
      zcreature1.velocity.x = (double) zcreature1.transformscale > 0.0 ? (FixedInt) 1 : -(FixedInt) 1;
    zcreature1.usingGlide = true;
    if (zcreature1.velocity.y < 0 && zcreature1.canMount)
    {
      zcreature1.map.CheckPositionOnlyEntities((int) zcreature1.position.x, (int) zcreature1.position.y + zcreature1.radius + 30, zcreature1, Inert.mask_movement_NoEffector);
      if ((ZComponent) zcreature1.mount != (object) null)
      {
        zcreature1.moving = (IEnumerator<float>) null;
        zcreature1.isMoving = false;
        zcreature1.velocity.x = (FixedInt) 0;
        zcreature1.velocity.y = (FixedInt) 0;
        zcreature1.addedVelocity.x = (FixedInt) 0;
        zcreature1.addedVelocity.y = (FixedInt) 0;
        yield break;
      }
    }
label_19:
    if (zcreature1.tries > 5)
    {
      zcreature1.isMoving = false;
      zcreature1.velocity.x = (FixedInt) 0;
      zcreature1.velocity.y = (FixedInt) 0;
      zcreature1.addedVelocity.x = (FixedInt) 0;
      zcreature1.addedVelocity.y = (FixedInt) 0;
      zcreature1.moving = (IEnumerator<float>) null;
    }
    else
    {
      while (!zcreature1.isDead)
      {
        ++zcreature1.loopCount;
        zcreature1.pX = zcreature1.position.x;
        zcreature1.pY = zcreature1.position.y;
        FixedInt fixedInt1 = zcreature1.position.x + zcreature1.velocity.x;
        FixedInt fixedInt2 = zcreature1.position.y + zcreature1.velocity.y;
        zcreature1.validX = zcreature1.pX;
        zcreature1.validY = zcreature1.pY;
        int num1 = 1;
        FixedInt x1 = zcreature1.velocity.x;
        FixedInt y1 = zcreature1.velocity.y;
        FixedInt fixedInt3 = zcreature1.velocity.x;
        FixedInt y2 = zcreature1.velocity.y;
        if (animateState)
        {
          if (y1 < 0)
          {
            animateState = false;
            if ((UnityEngine.Object) zcreature1.animator != (UnityEngine.Object) null)
              zcreature1.animator.Play(AnimateState.Fall);
          }
        }
        else if (y1 > 0)
        {
          animateState = true;
          if ((UnityEngine.Object) zcreature1.animator != (UnityEngine.Object) null)
            zcreature1.animator.Play(AnimateState.Jump);
        }
        FixedInt fixedInt4;
        if (x1 > 1 || x1 < -1 || y1 > 1 || y1 < -1)
        {
          num1 = !(Mathd.Abs(x1) > Mathd.Abs(y1)) ? (int) Mathd.Abs(y1) + 1 : (int) Mathd.Abs(x1) + 1;
          fixedInt3 = zcreature1.velocity.x / num1;
          fixedInt4 = zcreature1.velocity.y / num1;
        }
        else
        {
          zcreature1.velocity.y = (FixedInt) -1;
          fixedInt4 = (FixedInt) -1;
        }
        int num2 = num1 - 3;
        if (zcreature1.velocity.y < 0 && zcreature1.climbingHooksIsActive && zcreature1.CheckClimbingHooks())
        {
          zcreature1.moving = (IEnumerator<float>) null;
          zcreature1.isMoving = false;
          zcreature1.velocity.x = (FixedInt) 0;
          zcreature1.velocity.y = (FixedInt) 0;
          zcreature1.addedVelocity.x = (FixedInt) 0;
          zcreature1.addedVelocity.y = (FixedInt) 0;
          yield break;
        }
        else
        {
          while (num1 > 0)
          {
            --num1;
            int num3 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(zcreature1.velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * zcreature1.zb.Count - zcreature1.radius).ToInt();
            if (num3 < 0)
              num3 += zcreature1.zb.Count;
            if (num3 < 0 || zcreature1.zb.Count == 0)
            {
              Debug.LogError((object) ("Error a: " + (object) num3 + " zb: " + (object) zcreature1.zb.Count + " vel: " + (object) zcreature1.velocity + " aov: " + (object) Inert.AngleOfVelocity(zcreature1.velocity)));
              if (num3 < 0)
              {
                if (zcreature1.zb.Count == 0)
                  zcreature1.zb = MapGenerator.getOutlineArray(zcreature1.radius);
                while (num3 < 0 && zcreature1.zb.Count > 0)
                  num3 += zcreature1.zb.Count;
              }
            }
            for (int index1 = 0; index1 < zcreature1.radius * 2; ++index1)
            {
              int index2 = (index1 + num3) % zcreature1.zb.Count;
              if (!zcreature1.map.CheckPosition((int) (fixedInt3 + zcreature1.pX) + zcreature1.zb[index2].x, (int) (fixedInt4 + zcreature1.pY) + zcreature1.zb[index2].y, zcreature1, Inert.mask_entity_movement) && (zcreature1.position.y <= zcreature1.map.Height - zcreature1.radius + 2 || zcreature1.velocity.y <= 0))
              {
                int targetX = (int) (fixedInt3 + zcreature1.pX);
                int targetY = (int) (fixedInt4 + zcreature1.pY);
                if (zcreature1.glideIsActive)
                {
                  FixedInt y3 = zcreature1.velocity.y;
                  zcreature1.velocity.y = (FixedInt) 0;
                  if (zcreature1.loopCount < 1000 && zcreature1.loopCount > 5 && !zcreature1.LandUnder((int) zcreature1.validX, (int) zcreature1.validY))
                  {
                    zcreature1.MoveToPosition = new MyLocation(zcreature1.validX, zcreature1.validY);
                    zcreature1.velocity.y = y3;
                    zcreature1.velocity.x = -zcreature1.velocity.x;
                    zcreature1.velocity.y = (FixedInt) -1;
                    zcreature1.moving = (IEnumerator<float>) null;
                    zcreature1.isMoving = false;
                    zcreature1.StartMoving(zcreature1.movementFromInput);
                    yield break;
                  }
                  else
                    zcreature1.velocity.y = y3;
                }
                if ((ZComponent) zcreature1.mount != (object) null)
                {
                  zcreature1.moving = (IEnumerator<float>) null;
                  zcreature1.isMoving = false;
                  zcreature1.velocity.x = (FixedInt) 0;
                  zcreature1.velocity.y = (FixedInt) 0;
                  zcreature1.addedVelocity.x = (FixedInt) 0;
                  zcreature1.addedVelocity.y = (FixedInt) 0;
                  yield break;
                }
                else
                {
                  ZCreature zcreature2 = zcreature1.map.PhysicsCollideCreature(zcreature1, targetX + zcreature1.zb[index2].x, targetY + zcreature1.zb[index2].y);
                  if ((ZComponent) zcreature2 != (object) null && (zcreature2.velocity.x != 0 || zcreature2.velocity.y != 0) && zcreature2.isMoving)
                  {
                    if (zcreature2.velocity.y > 0)
                    {
                      if (zcreature1.velocity.y > 0)
                      {
                        FixedInt fixedInt5 = Mathd.Min(zcreature1.velocity.y, zcreature2.velocity.y) * 786432L;
                        if (fixedInt5 > 524288L)
                          zcreature1.velocity.y = fixedInt5;
                        else
                          zcreature1.velocity.y = (FixedInt) -1;
                      }
                      else
                        zcreature1.velocity.y = (FixedInt) -1;
                    }
                    else if (zcreature2.velocity.y < 0)
                    {
                      if (zcreature1.velocity.y > 0)
                        zcreature1.velocity.y = (FixedInt) -1;
                      else
                        zcreature1.velocity.y = (FixedInt) -1;
                    }
                    else if (zcreature2.position.y > zcreature1.position.y)
                      zcreature1.velocity.y = (FixedInt) -1;
                    else
                      zcreature1.velocity.y = (FixedInt) 1;
                    if (zcreature2.velocity.x > 0)
                    {
                      if (zcreature1.velocity.x > 0)
                      {
                        FixedInt fixedInt6 = Mathd.Min(zcreature1.velocity.x, zcreature2.velocity.x) * 786432L;
                        if (fixedInt6 > 524288L)
                          zcreature1.velocity.x = fixedInt6;
                        else
                          zcreature1.velocity.x = (FixedInt) -1;
                      }
                      else
                        zcreature1.velocity.x = (FixedInt) 0;
                    }
                    else if (zcreature2.velocity.x < 0)
                    {
                      if (zcreature1.velocity.x > 0)
                        zcreature1.velocity.x = (FixedInt) -1;
                      else
                        zcreature1.velocity.x = (FixedInt) 0;
                    }
                    else if (zcreature2.position.x > zcreature1.position.x)
                      zcreature1.velocity.x = (FixedInt) 0;
                    else
                      zcreature1.velocity.x = (FixedInt) 0;
                    yield return 0.0f;
                    goto label_11;
                  }
                  else if (zcreature1.velocity.x != 0)
                  {
                    if (zcreature1.loopCount == 1 && zcreature1.radius < 20 && num1 > num2 && zcreature1.velocity.y < -2 && !zcreature1.addVelocity && zcreature1.ExplosionOrigin.y - 10 < zcreature1.position.y + zcreature1.radius)
                    {
                      zcreature1.velocity.y = -zcreature1.velocity.y;
                      if (MyLocation.Distance(MyLocation.zero, zcreature1.velocity) < 21)
                        zcreature1.velocity.x *= 524288L;
                      yield return 0.0f;
                      goto label_11;
                    }
                    else
                    {
                      FixedInt x2 = zcreature1.velocity.x;
                      MyLocation position = zcreature1.position;
                      zcreature1.MoveToPosition = new MyLocation(zcreature1.pX, zcreature1.pY);
                      zcreature1.game.CreatureMoveSurroundings(position, zcreature1.radius, zcreature1.collider);
                      if (zcreature1.ShouldBounce(targetX, targetY))
                      {
                        yield return 0.0f;
                        goto label_11;
                      }
                      else
                      {
                        zcreature1.velocity.y = (FixedInt) 0;
                        zcreature1.velocity.x = (FixedInt) 0;
                        zcreature1.addedVelocity.x = (FixedInt) 0;
                        zcreature1.addedVelocity.y = (FixedInt) 0;
                        zcreature1.moving = (IEnumerator<float>) null;
                        zcreature1.isMoving = false;
                        zcreature1.CreatureMoveSurroundings();
                        if (zcreature1.health <= 0 || !zcreature1.CheckIfTooHigh(x2))
                        {
                          if (zcreature1.ShouldFall(zcreature1.climbingHooksIsActive, true))
                          {
                            zcreature1.usingGlide = zcreature1.climbingHooksIsActive;
                            zcreature1.Fall(true);
                          }
                          else
                          {
                            zcreature1.fallLoop = 0;
                            zcreature1.OnStop();
                            if (zcreature1.health <= 0)
                              zcreature1.OnDeath(true);
                          }
                        }
                        if (zcreature1.loopCount <= 15 || zcreature1.isPawn)
                        {
                          yield break;
                        }
                        else
                        {
                          ZPerson parent = zcreature1.parent;
                          if (parent == null)
                          {
                            yield break;
                          }
                          else
                          {
                            parent.awards.Bouncy();
                            yield break;
                          }
                        }
                      }
                    }
                  }
                  else if (zcreature1.velocity.y > 0 && (FixedInt) zcreature1.zb[index2].y > -((FixedInt) zcreature1.radius * (FixedInt) 786432L))
                  {
                    if (zcreature1.velocity.y > 0)
                    {
                      num1 = 0;
                      zcreature1.velocity.y = (FixedInt) -1;
                      zcreature1.pX = zcreature1.position.x;
                      zcreature1.pY = zcreature1.position.y;
                    }
                  }
                  else if (zcreature1.loopCount == 1 && zcreature1.radius < 20 && num1 > num2 && zcreature1.velocity.y < -2 && !zcreature1.addVelocity && zcreature1.ExplosionOrigin.y - 10 < zcreature1.position.y + zcreature1.radius)
                  {
                    zcreature1.velocity.y = -zcreature1.velocity.y;
                    if (MyLocation.Distance(MyLocation.zero, zcreature1.velocity) < 21)
                      zcreature1.velocity.x *= 524288L;
                    yield return 0.0f;
                    goto label_11;
                  }
                  else if (zcreature1.ShouldBounce(targetX, targetY))
                  {
                    MyLocation position = zcreature1.position;
                    zcreature1.MoveToPosition = new MyLocation(zcreature1.validX, zcreature1.validY);
                    zcreature1.game.CreatureMoveSurroundings(position, zcreature1.radius, zcreature1.collider);
                    yield return 0.0f;
                    goto label_11;
                  }
                  else
                  {
                    FixedInt pastVelocityX = zcreature1.velocity.x;
                    zcreature1.velocity.y = (FixedInt) 0;
                    zcreature1.velocity.x = (FixedInt) 0;
                    zcreature1.addedVelocity.x = (FixedInt) 0;
                    zcreature1.addedVelocity.y = (FixedInt) 0;
                    MyLocation position = zcreature1.position;
                    zcreature1.MoveToPosition = new MyLocation(zcreature1.validX, zcreature1.validY);
                    zcreature1.game.CreatureMoveSurroundings(position, zcreature1.radius, zcreature1.collider);
                    yield return 0.0f;
                    if (ZComponent.IsNull((ZComponent) zcreature1))
                    {
                      yield break;
                    }
                    else
                    {
                      zcreature1.moving = (IEnumerator<float>) null;
                      zcreature1.isMoving = false;
                      zcreature1.CreatureMoveSurroundings();
                      if (zcreature1.health > 0 && zcreature1.CheckIfTooHigh(pastVelocityX))
                        yield break;
                      else if (zcreature1.ShouldFall(zcreature1.climbingHooksIsActive, true))
                      {
                        zcreature1.usingGlide = zcreature1.climbingHooksIsActive;
                        zcreature1.Fall(true);
                        yield break;
                      }
                      else
                      {
                        zcreature1.fallLoop = 0;
                        zcreature1.OnStop();
                        if (zcreature1.loopCount > 15 && !zcreature1.isPawn)
                          zcreature1.parent?.awards.Bouncy();
                        if (zcreature1.health > 0)
                        {
                          yield break;
                        }
                        else
                        {
                          zcreature1.OnDeath(true);
                          yield break;
                        }
                      }
                    }
                  }
                }
              }
            }
            zcreature1.validX = zcreature1.pX;
            zcreature1.validY = zcreature1.pY;
            zcreature1.pX = zcreature1.pX + fixedInt3;
            zcreature1.pY = zcreature1.pY + fixedInt4;
          }
          zcreature1.tries = 0;
          MyLocation position1 = zcreature1.position;
          zcreature1.MoveToPosition = zcreature1.position + zcreature1.velocity;
          zcreature1.game.CreatureMoveSurroundings(position1, zcreature1.radius, zcreature1.collider);
          zcreature1.CreatureMoveSurroundings();
          if (zcreature1.glideIsActive)
          {
            if ((int) zcreature1.position.x <= zcreature1.radius)
            {
              if (zcreature1.velocity.x < 1)
              {
                zcreature1.velocity.x += 1;
                if (zcreature1.velocity.x < -10)
                  zcreature1.velocity.x += 2;
                zcreature1.SetScaleBasedOnVelocity(zcreature1.velocity.x.ToFloat());
              }
            }
            else if ((int) zcreature1.position.x >= zcreature1.map.Width - zcreature1.radius && zcreature1.velocity.x > -1)
            {
              zcreature1.velocity.x -= 1;
              if (zcreature1.velocity.x > 10)
                zcreature1.velocity.x -= 2;
              zcreature1.SetScaleBasedOnVelocity(zcreature1.velocity.x.ToFloat());
            }
          }
          if ((ZComponent) zcreature1.mount != (object) null)
          {
            zcreature1.moving = (IEnumerator<float>) null;
            zcreature1.isMoving = false;
            zcreature1.velocity.x = (FixedInt) 0;
            zcreature1.velocity.y = (FixedInt) 0;
            zcreature1.addedVelocity.x = (FixedInt) 0;
            zcreature1.addedVelocity.y = (FixedInt) 0;
            yield break;
          }
          else if (zcreature1.position.y < zcreature1.radius)
          {
            zcreature1.Drown();
            yield break;
          }
          else
          {
            if (zcreature1.parent.yourTurn && !zcreature1.isPawn && zcreature1.position.y >= zcreature1.game.map.Height + 3000 && !zcreature1.game.isClient)
              Achievements.Set(Achievement.Up__Up_and_Up_some_more, zcreature1.parent.account, zcreature1.game);
            if (zcreature1.addVelocity)
            {
              zcreature1.addVelocity = false;
              zcreature1.velocity = zcreature1.velocity + zcreature1.addedVelocity;
              zcreature1.addedVelocity.x = (FixedInt) 0;
              zcreature1.addedVelocity.y = (FixedInt) 0;
            }
            if ((zcreature1.affectedByGravity || zcreature1.entangledOrGravity) && zcreature1.velocity.y > -ZMap.MaxSpeed)
            {
              if (!zcreature1.glideIsActive || zcreature1.velocity.y >= 0 || zcreature1.velocity.x == 0)
              {
                zcreature1.velocity.y += zcreature1.map.Gravity;
              }
              else
              {
                if (Mathd.Abs(zcreature1.velocity.x) > 5)
                  zcreature1.velocity.y += zcreature1.map.Gravity / 4;
                if (Mathd.Abs(zcreature1.velocity.x) < 20)
                  zcreature1.velocity.x += zcreature1.velocity.x > 0 ? 314572L : -314572L;
              }
            }
            yield return 0.0f;
          }
        }
      }
      zcreature1.fallLoop = 0;
      zcreature1.velocity = MyLocation.zero;
      zcreature1.isMoving = false;
      if (zcreature1.health <= 0)
        zcreature1.OnDeath(true);
    }
  }

  private void Drown()
  {
    if (this.waterWalking && this.position.x > 0 && this.position.x < this.map.Width && !this.inWater)
    {
      this.velocity.x = (FixedInt) 0;
      this.velocity.y = (FixedInt) 0;
      this.addedVelocity.x = (FixedInt) 0;
      this.addedVelocity.y = (FixedInt) 0;
      this.isMoving = false;
      this.moving = (IEnumerator<float>) null;
      this.MoveToPosition = new MyLocation(this.position.x, (FixedInt) (this.type == CreatureType.Kraken ? 0 : this.radius));
      if (this.loopCount <= 15 || this.isPawn)
        return;
      this.parent?.awards.Bouncy();
    }
    else
    {
      int x = (int) this.position.x;
      if (this.isPawn && this.race != CreatureRace.Frost_Walker && !this.frostWalking && (!this.familiar.Has(FamiliarType.Frost) || this.familiarLevelFrost == 0) && this.diesInWater)
      {
        Inert.Instance.Splash((Vector3) this.position.ToSinglePrecision(), (float) this.radius);
        if (this.spellEnum == SpellEnum.Summon_Snowman || this.spellEnum == SpellEnum.Summon_Snowman2)
        {
          MyLocation power1 = Inert.Velocity((FixedInt) -90, 1);
          ZSpell zspell1 = ZSpell.BaseFire(Inert.Instance.spells["Snowball"], this.parent.first(), this.position, Quaternion.identity, power1);
          zspell1.curDuration = zspell1.maxDuration;
          MyLocation power2 = Inert.Velocity((FixedInt) -45, 1);
          ZSpell zspell2 = ZSpell.BaseFire(Inert.Instance.spells["Snowball"], this.parent.first(), this.position, Quaternion.identity, power2);
          zspell2.curDuration = zspell2.maxDuration;
          MyLocation power3 = Inert.Velocity((FixedInt) -135, 1);
          ZSpell zspell3 = ZSpell.BaseFire(Inert.Instance.spells["Snowball"], this.parent.first(), this.position, Quaternion.identity, power3);
          zspell3.curDuration = zspell3.maxDuration;
        }
        this.OnDeath(true);
      }
      else
      {
        this.velocity.x = (FixedInt) 0;
        this.velocity.y = (FixedInt) 0;
        this.addedVelocity.x = (FixedInt) 0;
        this.addedVelocity.y = (FixedInt) 0;
        this.isMoving = false;
        this.moving = (IEnumerator<float>) null;
        if ((this.race == CreatureRace.Frost_Walker || this.frostWalking || this.familiar.Has(FamiliarType.Frost) && this.familiarLevelFrost > 0) && x > 0 && x < this.map.Width && this.health > Mathf.Clamp(6 - this.familiarLevelFrost, 1, 5))
        {
          this.map.ServerBitBlt(54, x, 4, false);
          this.MoveToPosition = new MyLocation(this.position.x, (FixedInt) (8 + this.radius));
          this.DoDamage(Mathf.Clamp(6 - this.familiarLevelFrost, 1, 5));
          this.UpdateHealthTxt();
        }
        else if (this.isPawn && this.diesInWater)
        {
          Inert.Instance.Splash((Vector3) this.position.ToSinglePrecision(), (float) this.radius);
          this.OnDeath(true);
        }
        else
          this.EnteredWater();
      }
    }
  }

  public void EnteredWater()
  {
    if (this.parent.localTurn == -1 && this.health > 0 && !this.game.originalSpellsOnly)
      this.health = this.maxHealth;
    if (this.game.isSandbox && this.team != 0 && Client._tutorial != null && this.diesInWater)
    {
      if (this.position.y < 100)
        Inert.Instance.Splash((Vector3) this.position.ToSinglePrecision(), (float) this.radius);
      this.health = 0;
      this.UpdateHealthTxt();
      this.OnDeath(true);
    }
    else
    {
      int num = this.shield / 30;
      if (this.invulnerable > 0)
        num += 3;
      if (this.position.y < 100)
        Inert.Instance.Splash((Vector3) this.position.ToSinglePrecision(), (float) this.radius);
      this.MoveToPosition = new MyLocation(this.position.x, (FixedInt) -1000);
      this.inWater = true;
      if (this.game.isClient)
        AudioManager.Play(AudioManager.instance.splash);
      this.parent?.awards.FellInWater();
      this.RemoveEntangle();
      this.RemoveGravity();
      this.shield = 0;
      this.entangledShield = -1;
      this.invulnerable -= 3;
      this.hasDarkDefenses = false;
      this.bleeding = false;
      this.halfHealing = 0;
      this.retribution = 0;
      this.UpdateHealthTxt();
      if (this.flying && !this.shiningPower && !this.phantom)
      {
        this.removeFlight = false;
        this.flying = false;
        ++num;
        if ((UnityEngine.Object) this.flightObj != (UnityEngine.Object) null)
        {
          ZComponent.Destroy<GameObject>(this.flightObj);
          this.flightObj = (GameObject) null;
        }
      }
      ZEffector z = (ZEffector) null;
      for (int index = this.destroyableEffectors.Count - 1; index >= 0; --index)
      {
        if (!ZComponent.IsNull((ZComponent) this.destroyableEffectors[index]))
        {
          if (this.destroyableEffectors[index].type == EffectorType.Static_Ball)
          {
            z = this.destroyableEffectors[index];
          }
          else
          {
            if (this.destroyableEffectors[index].type == EffectorType.Wind_Shield || this.destroyableEffectors[index].type == EffectorType.Fire_Shield)
              ++num;
            this.destroyableEffectors[index].collider?.Disable();
            this.destroyableEffectors[index].isNull = true;
            ZComponent.Destroy<GameObject>(this.destroyableEffectors[index].gameObject);
          }
        }
      }
      if (num >= 2 && this.game.isRated && !this.parent.yourTurn && !this.game.isClient)
      {
        ZPerson zperson = this.game.CurrentPlayer();
        if (zperson != null && zperson.team != this.team)
          Achievements.Set(Achievement.Whiter_than_White, zperson.account, this.game);
      }
      this.game.forceRysncPause = true;
      this.destroyableEffectors.Clear();
      if (!ZComponent.IsNull((ZComponent) z))
        this.destroyableEffectors.Add(z);
      ++this.game.sinksThisTurn;
      if (this.game.isClient || this.game.CurrentPlayer() == null)
        return;
      if (this.game.lastSpellCast == SpellEnum.Spirit_Hurricane)
        Achievements.Set(Achievement.Didn_t_see_that_coming, this.game.CurrentPlayer().account, this.game);
      else if (this.game.lastSpellCast == SpellEnum.Wallop || this.game.lastSpellCast == SpellEnum.Ent_Whip || this.game.lastSpellCast == SpellEnum.Sunder || this.game.lastSpellCast == SpellEnum.Smash)
        Achievements.Set(Achievement.SMASH_, this.game.CurrentPlayer().account, this.game);
      else if (this.game.lastSpellCast == SpellEnum.Calling_Bell)
        Achievements.Set(Achievement.Ring_Ring_Splash, this.game.CurrentPlayer().account, this.game);
      if (this.game.sinksThisTurn == 2)
      {
        if (this.game.lastSpellCast != SpellEnum.Arcane_Flash && this.game.lastSpellCast != SpellEnum.Vine_Whip && this.game.lastSpellCast != SpellEnum.Ent_Whip)
          return;
        Achievements.Set(Achievement.Multi_Dunk_, this.game.CurrentPlayer().account, this.game);
      }
      else
      {
        if (this.game.sinksThisTurn != 3 || this.game.lastSpellCast != SpellEnum.Quake)
          return;
        Achievements.Set(Achievement.Shake_n__Quake, this.game.CurrentPlayer().account, this.game);
      }
    }
  }

  public bool CheckIfTooHigh(FixedInt pastVelocity)
  {
    if ((int) this.position.y <= this.map.Height - this.radius + 2)
      return false;
    ExplosionCutout impRadius = ExplosionSize.GetImpRadius(60 + (this.radius - 18) * 4);
    this.map.ServerBitBlt((int) impRadius, (int) this.position.x, (int) this.position.y);
    ZSpell.ApplyExplosionForce(SpellEnum.HighHigh, this.world, this.position, 0, Curve.Generic, 1, ExplosionSize.GetRadius(impRadius), (FixedInt) 1, DamageType.Ignore, this, 0);
    this.ApplyDamage(SpellEnum.HighHigh, DamageType.None, 15, (ZCreature) null, -1);
    if (this.health <= 0)
    {
      this.OnDeath(true);
      return false;
    }
    this.velocity.y = (FixedInt) 8;
    this.velocity.x = pastVelocity + (pastVelocity >= 0 ? 1 : -1);
    if (this.game.isClient && !this.game.resyncing && CameraMovement.FOLLOWTARGETS)
      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) this));
    if (this.game.isClient && (UnityEngine.Object) this.transform != (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) this.deathExplosion == (UnityEngine.Object) null)
        Debug.Log((object) this.name);
      ZComponent.Instantiate<GameObject>(this.deathExplosion, this.transform.position, Quaternion.identity, Client.GetMapTransform());
      this.UpdateHealthTxt();
    }
    this.StartMoving(this.movementFromInput);
    this.map.OnGhosted((int) this.position.x, Mathf.Min(this.map.Height, (int) this.position.y), this.radius * 4, this.collider);
    return true;
  }

  public void RemoveFlight(bool fromBreeze = false)
  {
    if (this.shiningPower || this.phantom && (!fromBreeze || !this.isPawn))
      return;
    this.removeFlight = false;
    this.flying = false;
    this.tempFlight = false;
    if ((UnityEngine.Object) this.flightObj != (UnityEngine.Object) null)
    {
      ZComponent.Destroy<GameObject>(this.flightObj);
      this.flightObj = (GameObject) null;
    }
    if (this.type == CreatureType.Vampire)
    {
      this.radius = 21;
      this.collider.SetRadius = 21;
      this.zb = MapGenerator.getOutlineArray(this.radius);
      if (this.game.isClient && (UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
      {
        this.animator = this.gameObject.GetComponent<IAnimator>();
        this.transform.GetChild(0).gameObject.SetActive(false);
        float num = this.BGScale();
        this.bg.transform.localScale = new Vector3(num, num, 1f);
        for (int index = 1; index < 7; ++index)
          this.transform.GetChild(index).gameObject.SetActive(true);
      }
    }
    this.affectedByGravity = true;
    if (this.entangledOrGravity)
      return;
    if (this.isMoving)
      this.KillMovement();
    this.Fall();
  }

  public virtual void Fall(bool ignoreFlight = false)
  {
    this.inWater = false;
    if (this.isDead || ZComponent.IsNull((ZComponent) this) || this.flying && !this.entangledOrGravity && !ignoreFlight && this.position.x > 0 && this.position.x < this.map.Width)
      return;
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
    }
    else
    {
      if (this.position != this.fallloopVec)
      {
        this.fallLoop = 0;
        this.fallloopVec = this.position;
      }
      else
        ++this.fallLoop;
      this.velocity = MyLocation.down;
      this.addedVelocity.x = (FixedInt) 0;
      this.addedVelocity.y = (FixedInt) 0;
      if (this.isMoving)
        return;
      this.moving = this.game.ongoing.RunCoroutine(this.Move());
    }
  }

  public override void StartMoving(bool fromInput = false)
  {
    this.movementFromInput = fromInput;
    if (!ZComponent.IsNull((ZComponent) this.tower))
    {
      this.tower.ShouldFall();
    }
    else
    {
      if (this.isMoving)
        return;
      if (this.flying && !this.entangledOrGravity && (!this.shiningPower || this.shield > 0))
        this.moving = this.game.ongoing.RunCoroutine(this.FlyMove());
      else
        this.moving = this.game.ongoing.RunCoroutine(this.Move());
    }
  }

  public void StartMovingShiningPower()
  {
    this.movementFromInput = false;
    if (this.isMoving)
      return;
    this.moving = this.game.ongoing.RunCoroutine(this.FlyMove());
  }

  public void GargoyleMorph(bool stone)
  {
    if (stone)
    {
      this.flying = false;
      this.canMove = false;
      if (this.ShouldFall())
        this.Fall();
      if (!this.game.isClient || !((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null))
        return;
      this.gameObject.GetComponent<GargoyalObj>()?.SetColor(false, -1f);
    }
    else
    {
      this.flying = true;
      this.canMove = true;
      if (!this.game.isClient || !((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null))
        return;
      this.gameObject.GetComponent<GargoyalObj>()?.SetColor(true, 0.0f);
    }
  }

  public class NotScaled
  {
    public Transform transform;
    public Vector3 normalPositoin;
    public Vector3 leftPosition;
    public IAnimator animator;
    public Outfit outfit = Outfit.None;

    public NotScaled()
    {
    }

    public NotScaled(Transform t) => this.transform = t;

    public NotScaled(IAnimator c, SpriteRenderer s, float offset, Outfit o)
    {
      this.transform = s.transform;
      this.normalPositoin = this.transform.localPosition;
      this.leftPosition = this.transform.localPosition + new Vector3(offset, 0.0f);
      this.animator = c;
      this.outfit = o;
    }

    public virtual void Scale(Vector3 scale, Vector3 normalScale)
    {
      this.transform.localScale = scale;
      this.transform.localPosition = (double) scale.x < 0.0 ? this.leftPosition : this.normalPositoin;
      if (this.outfit == Outfit.LeftHand)
      {
        this.animator.ResetLeftHand();
      }
      else
      {
        if (this.outfit != Outfit.RightHand)
          return;
        this.animator.ResetRightHand();
      }
    }
  }

  public class OneScaled : ZCreature.NotScaled
  {
    public OneScaled(Transform t) => this.transform = t;

    public override void Scale(Vector3 scale, Vector3 normalScale)
    {
      this.transform.localScale = normalScale;
    }
  }

  private enum DKMoveState
  {
    Walk,
    Jump,
  }
}
