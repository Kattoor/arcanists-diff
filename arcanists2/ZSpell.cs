

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityS.Mathematics;

#nullable disable
public class ZSpell : ZEntity, ISpellBridge
{
  internal string name;
  internal Spell clientObj;
  internal ZEffector effector;
  internal ZEffector effector2;
  internal Spell baseSpell;
  internal int steps = 1;
  internal bool wasChild;
  public int maxDuration;
  public int damage;
  public DamageType damageType;
  public int EXORADIUS;
  public FixedInt explisiveForce;
  public ExplosionCutout explosionCutout;
  public bool explodeOnImpact;
  internal ZCreature extraCheck;
  public bool goToTarget;
  public int maxTargetFrames;
  public MyLocation target = NullMyLocation.Get();
  public int amount;
  public int curDuration;
  public FixedInt curSpeed;
  internal bool fromArmageddon;
  private Renderer _renderer;
  public int Bounces;
  public ZMyCollider colliderB;
  internal MyLocation addedDeathVector = MyLocation.zero;
  internal ZCreature hitCreature;
  internal int timesBounced;
  internal int lastLivingHit = -100;
  internal int lastHit;
  internal int curDestroyTerrainBounces;
  private bool death;
  private int waitFrames;
  private int[] _circleSizes = new int[3]{ 16, 8, 4 };
  public static Color[] HolidayColors = new Color[7]
  {
    Color.red,
    Color.green,
    Color.blue,
    Color.yellow,
    Color.magenta,
    Color.cyan,
    new Color(1f, 0.3582275f, 0.0f)
  };
  public static HashSet<SpellEnum> MinionSpells = new HashSet<SpellEnum>()
  {
    SpellEnum.Summon_Imps,
    SpellEnum.Summon_Dragon_Egg,
    SpellEnum.Summon_Flame_Dragon,
    SpellEnum.Summon_Dwarf,
    SpellEnum.Summon_Rock_Golem,
    SpellEnum.Summon_Cyclops,
    SpellEnum.Summon_Storm_Spirit,
    SpellEnum.Summon_Storm_Dragon,
    SpellEnum.Summon_Sylph,
    SpellEnum.Summon_Frost_Giant,
    SpellEnum.Frost_Dragon,
    SpellEnum.Summon_Swarm,
    SpellEnum.Summon_Bees,
    SpellEnum.Summon_Dark_Knight,
    SpellEnum.Summon_Wraith,
    SpellEnum.Summon_Pegasus,
    SpellEnum.Summon_Paladin,
    SpellEnum.Summon_Pixies,
    SpellEnum.Summon_Man_Trap,
    SpellEnum.Summon_Elves,
    SpellEnum.Summon_Brine_Goblin,
    SpellEnum.Summon_Water_Trolls,
    SpellEnum.Summon_Water_Lord,
    SpellEnum.Recall_Device,
    SpellEnum.Steam_Dragon,
    SpellEnum.Summon_Snowman,
    SpellEnum.Summon_Reindeer,
    SpellEnum.Summon_Phantom,
    SpellEnum.Summon_Monarchs,
    SpellEnum.Summon_Dryad,
    SpellEnum.Summon_Kraken,
    SpellEnum.Summon_Mountain_Goat,
    SpellEnum.From_the_Depths,
    SpellEnum.Summon_Gargoyle,
    SpellEnum.Summon_Blood_Bank,
    SpellEnum.Dark_Totem,
    SpellEnum.Summon_Vampire,
    SpellEnum.Cogmobile,
    SpellEnum.Arcane_Dragon,
    SpellEnum.Monkey,
    SpellEnum.Little_Devil,
    SpellEnum.Hatch,
    SpellEnum.Stone_Form,
    SpellEnum.Acorn,
    SpellEnum.Summon_Snowman2,
    SpellEnum.Summon_Boar,
    SpellEnum.Summon_Tiger,
    SpellEnum.Summon_Will_o_the_Wisp,
    SpellEnum.Summon_Drone,
    SpellEnum.Cosmic_Horror,
    SpellEnum.Summon_King_Monarch,
    SpellEnum.Summon_Titan
  };

  public SpellEnum spellEnum => this.baseSpell.spellEnum;

  public SpellLogic spellLogic => this.baseSpell.spellLogic;

  public SpellType spellType => this.baseSpell.spellType;

  public TargetType targetType => this.baseSpell.targetType;

  public AudioClip castClip => this.baseSpell.castClip;

  public AudioClip explosionClip => this.baseSpell.explosionClip;

  public int spellIndex => this.baseSpell.spellIndex;

  public int level => this.baseSpell.level;

  public FixedInt speedMin => (FixedInt) this.baseSpell.speedMin;

  public FixedInt speedMax => (FixedInt) this.baseSpell.speedMax;

  public FixedInt speedFlying => (FixedInt) this.baseSpell.speedFlying;

  public FixedInt speedRotation => (FixedInt) this.baseSpell.speedRotation;

  public FixedInt elasticity => (FixedInt) this.baseSpell.elasticity;

  public Curve damageOverDistance => this.baseSpell.damageOverDistance;

  public Curve forceOverDistance => this.baseSpell.forceOverDistance;

  public GameObject explosion => this.baseSpell.explosion;

  public ZCreature toCollideCheck
  {
    get
    {
      if (this.curDuration >= 6 || this.wasChild)
        return (ZCreature) null;
      return (ZComponent) this.extraCheck != (object) null ? this.extraCheck : this.parent;
    }
  }

  public int frameOfPhantom => this.baseSpell.frameOfPhantom;

  public bool Rotates => this.baseSpell.Rotates;

  public bool EndsTurn => this.baseSpell.EndsTurn;

  public int MaxUses => this.baseSpell.MaxUses;

  public int RechargeTime => this.baseSpell.RechargeTime;

  public int TurnsTillFirstUse => this.baseSpell.TurnsTillFirstUse;

  public CastType type => this.baseSpell.type;

  public BookOf bookOf => this.baseSpell.bookOf;

  public GameObject toSummon => this.baseSpell.toSummon;

  public ZCreature parent { get; set; }

  public Renderer Renderer => this._renderer;

  public void FindRenderer()
  {
    if (!((UnityEngine.Object) this._renderer == (UnityEngine.Object) null))
      return;
    this._renderer = this.gameObject?.GetComponent<Renderer>();
  }

  public Texture2D snowTexture => this.baseSpell.snowTexture;

  public ExplosionCutout snowCutout => this.baseSpell.snowCutout;

  public FixedInt CustomGravity => (FixedInt) this.baseSpell.CustomGravity;

  public bool hitPhantom => this.baseSpell.hitPhantom;

  public bool destroyTerrainFirstBounce => this.baseSpell.destroyTerrainFirstBounce;

  public int destroyTerrainBounces => this.baseSpell.destroyTerrainBounces;

  internal FixedInt GetRotation
  {
    get => FixedInt.Atan2(this.velocity.y, this.velocity.x) * FixedInt.Rad2Deg;
  }

  public virtual int SummoningAmount() => 0;

  public override void OnDeath(bool playDeathClip = true)
  {
    if (ZComponent.IsNull((ZComponent) this))
      return;
    this.isNull = true;
    this.game.forceRysncPause = true;
    this.isDead = true;
    this.isNull = true;
    this.Explode();
    this.ApplyExplosionForce(this.position + this.addedDeathVector);
    this.AfterExplosion();
    this.DestroyDelay();
  }

  public void MoveSurroundings(int radius)
  {
    ZSpell.MoveSurroundings(this.game.world, this.position, radius);
  }

  public static void MoveSurroundings(ZMyWorld world, MyLocation position, int radius)
  {
    List<ZMyCollider> zmyColliderList = world.OverlapCircleAll((Point) position, radius + 5, (ZMyCollider) null);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature) && (ZComponent) creature.mount == (object) null)
        {
          if (!creature.isMoving && creature.ShouldFall())
            creature.Fall();
        }
        else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
          zmyColliderList[index].spell.SpellFall();
      }
      else if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZTower tower = zmyColliderList[index].tower;
        if (!ZComponent.IsNull((ZComponent) tower) && !tower.creature.isMoving)
          tower.ShouldFall();
      }
      else
      {
        ZSpell spell = zmyColliderList[index].spell;
        if (!ZComponent.IsNull((ZComponent) spell) && !spell.isMoving && !spell.isDead && spell.ShouldSpellFall())
          spell.SpellFall();
      }
    }
  }

  public void TryFall(bool onlyMap)
  {
    if (this.isMoving || this.isDead || !this.ShouldSpellFall(onlyMap))
      return;
    this.SpellFall();
  }

  public void OnExplosion()
  {
    if (!this.game.isClient || this.game.resyncing || !((UnityEngine.Object) this.explosion != (UnityEngine.Object) null))
      return;
    ZComponent.Instantiate<GameObject>(this.explosion, this.position.ToSinglePrecision(), Quaternion.identity);
  }

  public static ZSpell BaseFireKablam(Spell spell, ZCreature c, MyLocation pos, Quaternion rot)
  {
    ZSpell zspell = ZSpell.Create(c.game, spell, (Vector3) pos.ToSinglePrecision(), spell.Rotates ? rot : Quaternion.identity, c.game.GetMapTransform());
    zspell.game = c.game;
    zspell.position = pos;
    zspell.parent = c;
    if (c.game.ongoing.forceArmageddon)
      zspell.fromArmageddon = true;
    return zspell;
  }

  public static ZSpell BaseFire(
    Spell spell,
    ZPerson c,
    MyLocation pos,
    Quaternion rot,
    MyLocation power,
    bool setVelocity = true,
    bool isChild = false)
  {
    ZSpell e = ZSpell.Create(c.game, spell, (Vector3) pos.ToSinglePrecision(), spell.Rotates ? rot : Quaternion.identity, c.game.GetMapTransform());
    e.game = c.game;
    e.position = pos;
    e.wasChild = isChild;
    e.parent = c.first();
    if (c.game.ongoing.forceArmageddon)
      e.fromArmageddon = true;
    if (setVelocity)
      e.SetVelocity(power);
    if (e.game.isClient && !e.game.resyncing && CameraMovement.FOLLOWTARGETS)
      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) e));
    return e;
  }

  public static ZSpell BaseFire(
    Spell spell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    MyLocation power,
    bool setVelocity = true,
    bool isChild = false)
  {
    ZSpell e = ZSpell.Create(c.game, spell, (Vector3) pos.ToSinglePrecision(), spell.Rotates ? rot : Quaternion.identity, c.game.GetMapTransform());
    e.game = c.game;
    e.position = pos;
    e.wasChild = isChild;
    e.parent = c;
    if (c.game.ongoing.forceArmageddon)
      e.fromArmageddon = true;
    if (setVelocity)
      e.SetVelocity(power);
    if (e.game.isClient && !e.game.resyncing && CameraMovement.FOLLOWTARGETS)
      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) e));
    return e;
  }

  public static ZSpell BaseFire(
    Spell spell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    bool isChild = false)
  {
    ZSpell e = ZSpell.Create(c.game, spell, (Vector3) pos.ToSinglePrecision(), spell.Rotates ? rot : Quaternion.identity, c.game.GetMapTransform());
    e.game = c.game;
    e.position = pos;
    e.wasChild = isChild;
    e.parent = c;
    if (c.game.ongoing.forceArmageddon)
      e.fromArmageddon = true;
    if (e.game.isClient && !e.game.resyncing && CameraMovement.FOLLOWTARGETS)
      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) e));
    return e;
  }

  public static ZSpell BaseFireTarget(
    Spell spell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    MyLocation power,
    FixedInt speed,
    MyLocation target,
    bool setVelocity = true,
    bool isChild = false)
  {
    ZSpell e = ZSpell.Create(c.game, spell, (Vector3) pos.ToSinglePrecision(), spell.Rotates ? rot : Quaternion.identity, c.game.GetMapTransform());
    e.game = c.game;
    e.position = pos;
    e.wasChild = isChild;
    e.parent = c;
    e.curSpeed = speed;
    if (c.game.ongoing.forceArmageddon)
      e.fromArmageddon = true;
    if (setVelocity)
      e.SetVelocity(power);
    e.target = target;
    if (e.game.isClient && !e.game.resyncing && CameraMovement.FOLLOWTARGETS)
      CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowEntity((ZEntity) e));
    return e;
  }

  public void SpellFall()
  {
    this.velocity = MyLocation.down;
    if (this.isMoving)
      return;
    this.moving = this.game.ongoing.RunSpell(this.SpellMove());
  }

  public void SetVelocity(MyLocation v)
  {
    this.velocity = v;
    this.moving = this.game.ongoing.RunSpell(this.SpellMove(), false);
  }

  public virtual void Damage(ZCreature c)
  {
    if (c.FullArcane && this.damageType == DamageType.Arcane)
      c.ApplyHeal(DamageType.Arcane, (int) ((FixedInt) 524288L + (FixedInt) this.damage * Inert.GetCurve(this.damageOverDistance).Evaluate((FixedInt) (MyLocation.Distance(c.position, this.position) - c.GetRadius() - this.radius) / (FixedInt) this.EXORADIUS)), this.parent);
    else if (this.damageType == DamageType.Arcane && (ZComponent) c == (object) this.parent && ((ZComponent) c.tower == (object) null || c.tower.type != TowerType.Arcane) || this.damage == 0)
    {
      if (this.damage != 0 || c.race != CreatureRace.Effector || c.type == CreatureType.Jar)
        return;
      c.ApplyDamage(this.spellEnum, this.damageType, 1, this.parent, this.game.turn, (ISpellBridge) this);
    }
    else if (this.spellEnum == SpellEnum.Shock_Bomb && Mathd.Abs(c.position.x - this.position.x) < 10 + c.GetRadius())
      c.ApplyDamage(this.spellEnum, this.damageType, (int) ((FixedInt) 524288L + (FixedInt) this.damage * Inert.GetCurve(this.damageOverDistance).Evaluate((FixedInt) (MyLocation.Distance(c.position, this.position) - c.GetRadius() - this.radius - 30) / (FixedInt) this.EXORADIUS)), this.parent, this.game.turn, (ISpellBridge) this);
    else
      c.ApplyDamage(this.spellEnum, this.damageType, (int) ((FixedInt) 524288L + (FixedInt) this.damage * Inert.GetCurve(this.damageOverDistance).Evaluate((FixedInt) (MyLocation.Distance(c.position, this.position) - c.GetRadius() - this.radius) / (FixedInt) this.EXORADIUS)), this.parent, this.game.turn, (ISpellBridge) this);
  }

  private static void Damage(
    SpellEnum spellEnum,
    ZCreature c,
    ZCreature parent,
    DamageType damageType,
    int damage,
    int spellRadius,
    FixedInt EXORADIUS,
    MyLocation pos,
    int TurnCreated,
    Curve damageCurve = Curve.Generic,
    ISpellBridge spellRef = null)
  {
    if ((c.FullArcane || c.type == CreatureType.Imp) && damageType == DamageType.Arcane)
    {
      c.ApplyHeal(DamageType.Arcane, (int) ((FixedInt) 524288L + (FixedInt) damage * Inert.GetCurve(damageCurve).Evaluate((FixedInt) (MyLocation.Distance(c.position, pos) - c.GetRadius() - spellRadius) / EXORADIUS)), parent);
    }
    else
    {
      if (damageType == DamageType.Arcane && (ZComponent) c == (object) parent && ((ZComponent) c.tower == (object) null || c.tower.type != TowerType.Arcane))
        return;
      c.ApplyDamage(spellEnum, damageType, (int) ((FixedInt) 524288L + (FixedInt) damage * Inert.GetCurve(damageCurve).Evaluate((FixedInt) (MyLocation.Distance(c.position, pos) - c.GetRadius() - spellRadius) / EXORADIUS)), parent, TurnCreated, spellRef);
    }
  }

  public void Undie() => this.death = false;

  public bool ApplyExplosionForce(
    MyLocation pos,
    int mask = 0,
    bool die = true,
    ISpellBridge spellRef = null,
    ZCreature firstHit = null)
  {
    if (this.death)
      return false;
    ++this.game.numExplosionsAndMovement;
    if (die)
      this.death = true;
    if (this.spellEnum == SpellEnum.Whisper_Bomb && (ZComponent) this.parent != (object) null && this.parent.familiarLevelIllusion > 0)
      this.damage += Mathf.Clamp((int) FixedInt.Abs(this.position.x - this.parent.position.x) / 20 + (5 - this.parent.familiarLevelIllusion) * 2, 0, this.parent.familiarLevelIllusion * 8);
    else if (this.spellEnum == SpellEnum.Shock_Bomb || this.spellEnum == SpellEnum.Rusty_Bomb)
    {
      pos.y -= 30;
      if (this.spellEnum == SpellEnum.Shock_Bomb)
        this.SetOffsetShockBomb(-30);
    }
    List<ZMyCollider> zmyColliderList = this.game.world.OverlapCircleAll((Point) pos, this.EXORADIUS, (ZMyCollider) null, mask == 0 ? Inert.mask_all : mask);
    if ((ZComponent) firstHit != (object) null && (ZComponent) firstHit.collider != (object) null)
    {
      if (firstHit.collider.enabled && zmyColliderList.FindIndex((Predicate<ZMyCollider>) (a => (ZComponent) a == (object) firstHit.collider)) == -1)
        zmyColliderList.Add(firstHit.collider);
      else if ((ZComponent) firstHit.tower != (object) null && zmyColliderList.FindIndex((Predicate<ZMyCollider>) (a => (ZComponent) a == (object) firstHit.tower.collider)) == -1)
        zmyColliderList.Add(firstHit.tower.collider);
    }
    int count = zmyColliderList.Count;
    if (count > 1)
      zmyColliderList.Sort((IComparer<ZMyCollider>) Comparer<ZMyCollider>.Create((Comparison<ZMyCollider>) ((a, b) => a.HasNoRider - b.HasNoRider)));
    List<ZCreature> zcreatureList = new List<ZCreature>();
    bool flag = false;
    for (int index = 0; index < count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZCreature creature = zmyColliderList[index].tower.creature;
        if (!ZComponent.IsNull((ZComponent) creature))
        {
          flag = true;
          if ((ZComponent) creature != (object) this.parent || this.damageType != DamageType.Wallop && this.spellEnum != SpellEnum.Fire_Wave && this.spellEnum != SpellEnum.Thunder_Shock && (this.damageType != DamageType.Arcane || creature.tower.type == TowerType.Arcane))
          {
            if (this.damageType != DamageType.Blast_From_The_Past || this.curDuration > 0)
              this.Damage(creature);
            zcreatureList.Add(creature);
          }
          else
            zcreatureList.Add(creature);
        }
      }
      else if (zmyColliderList[index].HasLayer(65792))
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature) && !creature.isDead)
        {
          flag = true;
          if ((ZComponent) creature != (object) this.parent || this.damageType != DamageType.Wallop && this.damageType != DamageType.Sunder && this.damageType != DamageType.SunderLight && this.damageType != DamageType.Arcane && this.spellEnum != SpellEnum.Fire_Wave && this.spellEnum != SpellEnum.Thunder_Shock)
          {
            int health = creature.health;
            if (this.damageType != DamageType.Gravity && (this.damageType != DamageType.Blast_From_The_Past || this.curDuration > 0 || creature.GetType() == typeof (ZCreatureThorn)))
              this.Damage(creature);
            if (!ZComponent.IsNull((ZComponent) creature) && !creature.isDead)
            {
              if (health > creature.health || !creature.shiningPower || creature.entangledOrGravity)
              {
                creature.ApplyExplosionForce(this.radius, pos, this.spellEnum != SpellEnum.Sphere_of_Healing || creature.race != CreatureRace.Undead ? this.explisiveForce : (FixedInt) 30, this.EXORADIUS, this.forceOverDistance);
                zcreatureList.Add(creature);
              }
              else
              {
                creature.ApplyExplosionForce(this.radius, pos, this.spellEnum != SpellEnum.Sphere_of_Healing || creature.race != CreatureRace.Undead ? this.explisiveForce : (FixedInt) 30, this.EXORADIUS, this.forceOverDistance);
                creature.StartMovingShiningPower();
              }
            }
            if (this.damageType == DamageType.Gravity && !ZComponent.IsNull((ZComponent) creature))
              this.Damage(creature);
          }
          else if (this.explisiveForce > 0 && (ZComponent) creature.mount == (object) null)
          {
            if (creature.FullArcane && this.damageType == DamageType.Arcane)
              this.Damage(creature);
            if ((!creature.shiningPower || creature.entangledOrGravity) && !creature.isMoving)
            {
              creature.velocity = new MyLocation(0, -2);
              zcreatureList.Add(creature);
            }
          }
        }
        else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
          zmyColliderList[index].spell.SpellFall();
      }
      else
      {
        ZSpell spell = zmyColliderList[index].spell;
        if (!ZComponent.IsNull((ZComponent) spell))
        {
          if (spell.spellLogic == SpellLogic.Leaf)
            (spell as ZSpellLeaf).ApplyExplosionForce(this.radius, pos, this.explisiveForce, this.EXORADIUS, this.forceOverDistance, true);
          else if (spell.ShouldSpellFall())
            spell.Fall();
        }
        else
        {
          ZCreature creature = zmyColliderList[index].creature;
          if (!ZComponent.IsNull((ZComponent) creature) && creature.ShouldFall())
            creature.Fall();
        }
      }
    }
    for (int index = 0; index < zcreatureList.Count; ++index)
      zcreatureList[index].StartMoving(false);
    this.MoveSurroundingsZSpell();
    return flag;
  }

  public void MoveSurroundingsZSpell()
  {
    ZSpell.MoveSurroundings(this.game.world, this.position, this.EXORADIUS);
  }

  public static void ApplyExplosionForce(
    SpellEnum spellEnum,
    ZMyWorld world,
    MyLocation pos,
    int damage,
    Curve curve,
    int spellRadius,
    int EXORADIUS,
    FixedInt explosionForce,
    DamageType damageType,
    ZCreature parent,
    int TurnCreated,
    Curve damageCurve = Curve.Generic,
    ISpellBridge spellRef = null,
    ZCreature firstHit = null)
  {
    if ((ZComponent) parent != (object) null)
      ++parent.game.numExplosionsAndMovement;
    List<ZMyCollider> zmyColliderList = world.OverlapCircleAll((Point) pos, EXORADIUS, (ZMyCollider) null, Inert.mask_all);
    if ((ZComponent) firstHit != (object) null && (ZComponent) firstHit.collider != (object) null && firstHit.collider.enabled && zmyColliderList.FindIndex((Predicate<ZMyCollider>) (a => (ZComponent) a == (object) firstHit.collider)) == -1)
      zmyColliderList.Add(firstHit.collider);
    int count = zmyColliderList.Count;
    if (count > 1)
      zmyColliderList.Sort((IComparer<ZMyCollider>) Comparer<ZMyCollider>.Create((Comparison<ZMyCollider>) ((a, b) => a.HasNoRider - b.HasNoRider)));
    List<ZCreature> zcreatureList = new List<ZCreature>();
    for (int index = 0; index < count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZCreature creature = zmyColliderList[index].tower.creature;
        if (!ZComponent.IsNull((ZComponent) creature))
        {
          if ((ZComponent) creature != (object) parent || damageType != DamageType.Arcane || creature.tower.type == TowerType.Arcane)
          {
            if (damage != 0 || creature.race == CreatureRace.Effector)
              ZSpell.Damage(spellEnum, creature, parent, damageType, damage, spellRadius, (FixedInt) EXORADIUS, pos, TurnCreated, damageCurve, spellRef);
            zcreatureList.Add(creature);
          }
          else
            zcreatureList.Add(creature);
        }
      }
      else if (zmyColliderList[index].HasLayer(65792))
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature) && !creature.isDead)
        {
          if ((ZComponent) creature != (object) parent || damageType != DamageType.Arcane)
          {
            int health = creature.health;
            if (damage != 0 || creature.race == CreatureRace.Effector)
              ZSpell.Damage(spellEnum, creature, parent, damageType, damage, spellRadius, (FixedInt) EXORADIUS, pos, TurnCreated, damageCurve, spellRef);
            if (!creature.isDead && (health > creature.health || !creature.shiningPower || creature.entangledOrGravity))
            {
              zcreatureList.Add(creature);
              creature.ApplyExplosionForce(spellRadius, pos, explosionForce, EXORADIUS, curve);
            }
          }
          else
          {
            if ((creature.FullArcane || creature.type == CreatureType.Imp) && damageType == DamageType.Arcane && damage != 0)
              ZSpell.Damage(spellEnum, creature, parent, damageType, damage, spellRadius, (FixedInt) EXORADIUS, pos, TurnCreated, damageCurve, spellRef);
            if ((!creature.shiningPower || creature.entangledOrGravity) && !creature.isMoving)
            {
              creature.velocity = new MyLocation(0, -2);
              zcreatureList.Add(creature);
            }
          }
        }
        else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
          zmyColliderList[index].spell.SpellFall();
      }
      else
      {
        ZSpell spell = zmyColliderList[index].spell;
        if (!ZComponent.IsNull((ZComponent) spell))
        {
          if (spell.spellLogic == SpellLogic.Leaf)
            (spell as ZSpellLeaf).ApplyExplosionForce(spellRadius, pos, explosionForce, EXORADIUS, curve, true);
          else if (spell.ShouldSpellFall())
            spell.Fall();
        }
        else
        {
          ZCreature creature = zmyColliderList[index].creature;
          if (!ZComponent.IsNull((ZComponent) creature) && creature.ShouldFall())
            creature.Fall();
        }
      }
    }
    for (int index = 0; index < zcreatureList.Count; ++index)
      zcreatureList[index].StartMoving(false);
    ZSpell.MoveSurroundings(world, pos, EXORADIUS);
  }

  protected void DestroyDelay()
  {
    this.isNull = true;
    this.isDead = true;
    if (!ZComponent.IsNull((ZComponent) this.effector))
      this.effector.isNull = true;
    if (!ZComponent.IsNull((ZComponent) this.collider))
    {
      this.collider.Disable();
      ZComponent.Destroy<GameObject>(this.gameObject);
      this.game.forceRysncPause = true;
    }
    else
    {
      ZComponent.Destroy<GameObject>(this.gameObject, 3f);
      if (!this.game.isClient)
        return;
      SpriteRenderer component = this.gameObject?.GetComponent<SpriteRenderer>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.enabled = false;
      ParticleSystem[] componentsInChildren = this.gameObject?.GetComponentsInChildren<ParticleSystem>();
      if (componentsInChildren == null)
        return;
      foreach (ParticleSystem particleSystem in componentsInChildren)
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
  }

  public bool BombShouldFall(bool onlyMap = false)
  {
    return !(this.velocity.y != 0) && this.ShouldSpellFall(onlyMap);
  }

  public virtual bool ShouldSpellFall(bool onlyMap = false)
  {
    if (this.isDead)
      return false;
    if (this.spellEnum == SpellEnum.Morning_Sun)
    {
      this.MoveMorningSun((int) this.position.y, (int) this.position.y);
      return false;
    }
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num < 0)
      num += this.zb.Count;
    if (onlyMap)
    {
      for (int index1 = 0; index1 < this.radius * 2; ++index1)
      {
        int index2 = (index1 + num) % this.zb.Count;
        if (!this.map.CheckPositionOnlyMap(this.position.x.ToInt() + this.zb[index2].x, this.position.y.ToInt() - 3 + this.zb[index2].y))
          return false;
      }
    }
    else
    {
      for (int index3 = 0; index3 < this.radius * 2; ++index3)
      {
        int index4 = (index3 + num) % this.zb.Count;
        if (!this.map.SpellCheckPosition(this.position.x.ToInt() + this.zb[index4].x, this.position.y.ToInt() - 3 + this.zb[index4].y, (ZCreature) null, Inert.mask_movement_NoEffector))
          return false;
      }
    }
    this.position = new MyLocation(this.position.x, this.position.y - 1);
    return true;
  }

  public IEnumerator<float> ZSpellMoveKablam()
  {
    ZSpell zspell = this;
    zspell.isMoving = true;
    for (int i = 0; i < zspell.maxDuration; ++i)
      yield return 0.0f;
    zspell.OnExplosionGeneric();
    zspell.OnDeath(true);
  }

  public void MoveMorningSun(int startY, int fromY)
  {
    this.isMoving = true;
    int x = (int) this.position.x;
    for (int index1 = fromY; !this.isDead && index1 >= this.radius; --index1)
    {
      for (int index2 = -12; index2 < 13; ++index2)
      {
        ZCreature c = this.map.PhysicsCollideCreature((ZCreature) null, x + index2, index1, Inert.mask_Phantom);
        if ((ZComponent) c != (object) null)
        {
          if (this.collider.effector.active && this.collider.effector.MorningSunEffect(c))
          {
            this.isMoving = false;
            return;
          }
          if (startY == index1)
          {
            this.isMoving = false;
            return;
          }
          this.velocity.y = (FixedInt) 0;
          this.velocity.x = (FixedInt) 0;
          FixedInt y = c.type == CreatureType.Tree ? c.position.y + 35 : c.position.y;
          this.position = new MyLocation((FixedInt) x, y);
          this.collider.Move(this.position);
          this.moving = (IEnumerator<float>) null;
          this.isMoving = false;
          return;
        }
        if (!this.map.CheckPositionOnlyMap(x + index2, index1))
        {
          if (startY == index1)
          {
            this.isMoving = false;
            return;
          }
          this.velocity.y = (FixedInt) 0;
          this.velocity.x = (FixedInt) 0;
          this.position = new MyLocation(x, index1);
          this.collider.Move(this.position);
          this.moving = (IEnumerator<float>) null;
          this.isMoving = false;
          return;
        }
      }
    }
    this.position = new MyLocation(x, 0);
    this.collider.Move(this.position);
    this.moving = (IEnumerator<float>) null;
    this.velocity = MyLocation.zero;
    this.isMoving = false;
  }

  public virtual IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpell spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_61:
    while (!spell.isDead)
    {
      spell.pX = spell.position.x;
      spell.pY = spell.position.y;
      FixedInt fixedInt1 = spell.position.x + spell.velocity.x;
      FixedInt fixedInt2 = spell.position.y + spell.velocity.y;
      spell.validX = spell.pX;
      spell.validY = spell.pY;
      spell.steps = 1;
      FixedInt x1 = spell.velocity.x;
      FixedInt y1 = spell.velocity.y;
      FixedInt fixedInt3 = spell.velocity.x;
      FixedInt fixedInt4 = spell.velocity.y;
      if (x1 > 1 || x1 < -1 || y1 > 1 || y1 < -1)
      {
        spell.steps = !(FixedInt.Abs(x1) > FixedInt.Abs(y1)) ? FixedInt.Abs(y1).ToInt() + 1 : FixedInt.Abs(x1).ToInt() + 1;
        fixedInt3 = spell.velocity.x / spell.steps;
        fixedInt4 = spell.velocity.y / spell.steps;
      }
      while (spell.steps > 0)
      {
        if (checkEffectors && spell.map.SpellCheckEffectors(spell.toCollideCheck, spell, spell.validX.ToInt(), spell.validY.ToInt()))
        {
          spell.position = new MyLocation(spell.validX, spell.validY);
          yield return 0.0f;
          goto label_61;
        }
        else
        {
          --spell.steps;
          FixedInt fixedInt5 = (FixedInt.Create(360) - (Inert.AngleOfVelocity(spell.velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * spell.zb.Count - spell.radius;
          int num1 = fixedInt5.ToInt();
          if (num1 < 0)
            num1 += spell.zb.Count;
          for (int index1 = 0; index1 < spell.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % spell.zb.Count;
            int num2;
            if (spell.curDuration >= spell.frameOfPhantom)
            {
              ZMap map = spell.map;
              fixedInt5 = fixedInt3 + spell.pX;
              int x2 = fixedInt5.ToInt() + spell.zb[index2].x;
              fixedInt5 = fixedInt4 + spell.pY;
              int y2 = fixedInt5.ToInt() + spell.zb[index2].y;
              ZCreature toCollideCheck = spell.toCollideCheck;
              int maskSpellMovement = Inert.mask_spell_movement;
              num2 = !map.SpellCheckPosition(x2, y2, toCollideCheck, maskSpellMovement) ? 1 : 0;
            }
            else
              num2 = 0;
            if (num2 != 0)
            {
              fixedInt5 = fixedInt3 + spell.pX;
              int xInt = fixedInt5.ToInt();
              fixedInt5 = fixedInt4 + spell.pY;
              int yInt = fixedInt5.ToInt();
              if (!spell.explodeOnImpact)
              {
                spell.position = new MyLocation(spell.validX, spell.validY);
                MyLocation zero = MyLocation.zero;
                int x3 = xInt + spell.zb[index2].x;
                int y3 = yInt + spell.zb[index2].y;
                for (int index3 = -2; index3 <= 2; ++index3)
                {
                  for (int index4 = -2; index4 <= 2; ++index4)
                  {
                    if (spell.map.SpellCheckPosition(x3 + index3, y3 + index4, spell.toCollideCheck, Inert.mask_movement_NoEffector))
                    {
                      zero.x += index3;
                      zero.y += index4;
                    }
                  }
                }
                zero.Normalize();
                MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                spell.velocity.Multiply(spell.elasticity.RawValue, out spell.velocity);
                bool stop = !spell.map.CheckPositionOnlyMap(x3, y3);
                if (MyLocation.Distance(MyLocation.zero, spell.velocity) <= 1 && (stop ? (spell.ShouldSpellFall() ? 1 : 0) : (spell.BombShouldFall() ? 1 : 0)) == 0)
                {
                  while (spell.curDuration < spell.maxDuration)
                  {
                    ++spell.curDuration;
                    yield return 0.0f;
                    if ((stop ? (spell.ShouldSpellFall() ? 1 : 0) : (spell.BombShouldFall() ? 1 : 0)) != 0)
                      goto label_58;
                  }
                  spell.Explode();
                  spell.ApplyExplosionForce(spell.position + spell.addedDeathVector);
                  spell.AfterExplosion();
                  spell.DestroyDelay();
                  yield break;
                }
                else if (spell.game.isClient)
                {
                  AudioManager.Play(AudioManager.instance.spellBounce);
                  goto label_58;
                }
                else
                  goto label_58;
              }
              else
              {
                spell.addedDeathVector = Inert.Velocity(Inert.AngleOfVelocity(spell.velocity), spell.radius);
                if (spell.spellEnum != SpellEnum.Disruption)
                {
                  if (spell.spellEnum == SpellEnum.Comet)
                    spell.position = new MyLocation(xInt, yInt);
                  else
                    spell.position = new MyLocation(xInt, yInt + spell.zb[index2].y);
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.OnDeath(true);
                }
                else
                {
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.OnDeath(true);
                }
                spell.velocity.y = FixedInt.ZeroF;
                spell.velocity.x = FixedInt.ZeroF;
                yield break;
              }
            }
          }
          spell.validX = spell.pX;
          spell.validY = spell.pY;
          spell.pX = spell.pX + fixedInt3;
          spell.pY = spell.pY + fixedInt4;
        }
      }
      spell.position = spell.position + spell.velocity;
      if (spell.position.y < spell.radius)
      {
        spell.moving = (IEnumerator<float>) null;
        spell.isMoving = false;
        spell.Splash();
        if (spell.spellEnum == SpellEnum.Water_Ball)
        {
          spell.game.skimmed_on_water = true;
          spell.position = new MyLocation(spell.position.x, FixedInt.Create(spell.radius));
          spell.velocity.y = -spell.velocity.y;
          spell.amount *= 2;
          spell.OnDeath(true);
          break;
        }
        if (spell.spellEnum == SpellEnum.Brine_Bomb || spell.spellEnum == SpellEnum.Brine_Burst)
        {
          spell.Explode();
          spell.ApplyExplosionForce(spell.position + spell.addedDeathVector);
        }
        spell.velocity = MyLocation.zero;
        spell.DestroyDelay();
        break;
      }
      if (spell.addVelocity)
      {
        spell.addVelocity = false;
        spell.velocity = spell.velocity + spell.addedVelocity;
        spell.velocity.x = Mathd.Clamp(spell.velocity.x, (FixedInt) -50, (FixedInt) 50);
        spell.velocity.y = Mathd.Clamp(spell.velocity.y, (FixedInt) -50, (FixedInt) 50);
        spell.addedVelocity.x = (FixedInt) 0;
        spell.addedVelocity.y = (FixedInt) 0;
      }
      if (spell.goToTarget)
      {
        if (spell.curDuration >= spell.maxTargetFrames)
          spell.goToTarget = false;
        else
          spell.RotateToward();
      }
      else if (spell.affectedByGravity && spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      else if (!spell.affectedByGravity && spell.velocity.y > -10 && spell.maxDuration > 150)
        spell.affectedByGravity = true;
      spell.Wind();
      if (spell.Rotates && (UnityEngine.Object) spell.transform != (UnityEngine.Object) null)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
label_58:
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.Explode();
        spell.ApplyExplosionForce(spell.position + spell.addedDeathVector);
        spell.AfterExplosion();
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }

  public void Wind()
  {
    if (!this.game.isWindy)
      return;
    this.velocity.x += this.map.wind.x;
    this.velocity.y += this.map.wind.y;
  }

  protected void RotateToward()
  {
    ++this.waitFrames;
    if (this.waitFrames < 4)
      return;
    MyLocation myLocation = this.target - this.position;
    myLocation.Normalize();
    FixedInt fixedInt = FixedInt.Angle(this.velocity, MyLocation.up) * FixedInt.Rad2Deg;
    FixedInt angle = FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg - 90;
    if (this.velocity.x > 0)
    {
      if (FixedInt.Create(360) - fixedInt - angle >= 360)
      {
        if (this.velocity.y < 0)
          angle += 2;
        else
          angle -= 2;
      }
    }
    else if (fixedInt - angle >= 180)
    {
      if (this.velocity.y > 0)
        angle += 2;
      else
        angle -= 2;
    }
    MyLocation target = Global.Velocity(angle);
    this.velocity.RotateTowards(ref target, this.speedRotation * FixedInt.Deg2Rad, out this.velocity);
    this.velocity.Normalize();
    this.velocity = this.velocity * this.curSpeed;
    if (this.curSpeed < this.speedFlying)
    {
      if (FixedInt.Abs(this.curSpeed - this.speedFlying) <= (FixedInt) 1572864L)
        this.curSpeed = this.speedFlying;
      else
        this.curSpeed += this.spellEnum == SpellEnum.Arcane_Arrow ? 524288L : 1048576L;
    }
    else
    {
      if (!(this.curSpeed > this.speedFlying))
        return;
      if (FixedInt.Abs(this.curSpeed - this.speedFlying) <= (FixedInt) 1572864L)
        this.curSpeed = this.speedFlying;
      else
        this.curSpeed -= (FixedInt) (this.speedFlying > 15 ? 1048576L : 1468006L);
    }
  }

  public void Splash()
  {
    if (this.game == null || !this.game.isClient || this.game.resyncing)
      return;
    AudioManager.Play(AudioManager.instance?.spellSlash);
  }

  public virtual void Fall()
  {
    this.velocity = MyLocation.down;
    if (this.isMoving)
      return;
    this.moving = this.game.ongoing.RunSpell(this.SpellMove());
  }

  public override void StartMoving(bool fromInput = false)
  {
    if (this.isMoving)
      return;
    this.moving = this.game.ongoing.RunSpell(this.SpellMove());
  }

  public void StopAndDie()
  {
    this.velocity.y = (FixedInt) 0;
    this.velocity.x = (FixedInt) 0;
    this.position = new MyLocation(this.validX, this.validY);
    this.moving = (IEnumerator<float>) null;
    this.isMoving = false;
    this.OnDeath(true);
  }

  public SpellType GetSpellType => this.spellType;

  public string GetName => this.name;

  public SpellEnum GetSpellEnum => this.spellEnum;

  public BookOf book => this.bookOf;

  public bool FromArmageddon => this.fromArmageddon;

  public ZCreature GetParent => this.parent;

  public bool GetGoToTarget => this.goToTarget;

  public GameObject GetToSummon => this.baseSpell?.toSummon;

  public Spell GetBaseSpell => this.baseSpell;

  public ExplosionCutout GetExplosionCutout => this.explosionCutout;

  public void Copy(Spell c, ZGame game)
  {
    this.baseSpell = c;
    this.damage = c.damage;
    this.damageType = c.damageType;
    this.explisiveForce = (FixedInt) c.explisiveForce;
    this.explosionCutout = c.explosionCutout;
    this.explodeOnImpact = c.explodeOnImpact;
    this.goToTarget = c.goToTarget;
    this.maxTargetFrames = c.maxTargetFrames;
    this.amount = c.amount;
    this.maxDuration = c.maxDuration;
    this.EXORADIUS = c.EXORADIUS;
    this.radius = c.radius;
    this.Bounces = c.Bounces;
    this.affectedByGravity = c.affectedByGravity;
    if ((UnityEngine.Object) c.collider != (UnityEngine.Object) null)
    {
      this.collider = new ZMyCollider();
      this.collider.id = ++game.nextColliderID;
      this.collider.Copy(c.collider);
      this.collider.spell = this;
    }
    if ((UnityEngine.Object) c.colliderB != (UnityEngine.Object) null)
    {
      this.colliderB = new ZMyCollider();
      this.colliderB.id = ++game.nextColliderID;
      this.colliderB.Copy(c.colliderB);
      this.colliderB.spell = this;
    }
    if ((UnityEngine.Object) c.effector != (UnityEngine.Object) null)
    {
      this.effector = new ZEffector();
      this.effector.game = game;
      this.effector.id = ++game.nextEffectorID;
      this.effector.Copy(c.effector, game);
      this.effector.spell = this;
      this.effector.collider = this.collider;
      if ((ZComponent) this.collider != (object) null)
        this.collider.effector = this.effector;
    }
    this.name = c.name;
  }

  public static ZSpell Create(
    ZGame game,
    Spell c,
    Vector3 position,
    Quaternion q,
    Transform parent)
  {
    ZSpell zspell = ZSpell.Get(c, game);
    zspell.game = game;
    zspell.id = ++game.nextSpellID;
    zspell.Copy(c, game);
    zspell.SetupClient(c, position, q, parent);
    return zspell;
  }

  public static ZFlameWallSpell Create(
    ZGame game,
    FlameWallSpell c,
    Vector3 position,
    Quaternion q,
    Transform parent)
  {
    ZFlameWallSpell zflameWallSpell = new ZFlameWallSpell();
    zflameWallSpell.game = game;
    zflameWallSpell.id = ++game.nextSpellID;
    zflameWallSpell.Copy((Spell) c, game);
    if ((UnityEngine.Object) c.effector2 != (UnityEngine.Object) null)
    {
      zflameWallSpell.effector2 = new ZEffector();
      zflameWallSpell.effector2.id = ++game.nextEffectorID;
      zflameWallSpell.effector2.Copy(c.effector2, game);
      zflameWallSpell.effector2.collider = zflameWallSpell.colliderB;
      zflameWallSpell.colliderB.effector = zflameWallSpell.effector2;
      zflameWallSpell.colliderB.spell = (ZSpell) zflameWallSpell;
      zflameWallSpell.effector2.spell = (ZSpell) zflameWallSpell;
    }
    zflameWallSpell.SetupClient((Spell) c, position, q, parent);
    if ((ZComponent) zflameWallSpell.effector2 != (object) null)
    {
      zflameWallSpell.effector2.gameObject = zflameWallSpell.gameObject;
      zflameWallSpell.effector2.transform = zflameWallSpell.transform;
    }
    return zflameWallSpell;
  }

  private void SetupClient(Spell c, Vector3 position, Quaternion q, Transform parent)
  {
    Spell spell = UnityEngine.Object.Instantiate<Spell>(c, position, q, parent);
    this.clientObj = spell;
    spell.runTimeStats = c.runTimeStats;
    this.gameObject = spell.gameObject;
    this.transform = spell.transform;
    spell.serverObj = this;
    if ((ZComponent) this.collider != (object) null)
    {
      this.collider.gameObject = this.gameObject;
      this.collider.transform = this.transform;
    }
    if ((ZComponent) this.colliderB != (object) null)
    {
      this.colliderB.gameObject = this.gameObject;
      this.colliderB.transform = this.transform;
    }
    if (!((ZComponent) this.effector != (object) null))
      return;
    this.effector.gameObject = this.gameObject;
    this.effector.transform = this.transform;
  }

  public static ZSpell Get(Spell x, ZGame game)
  {
    switch (x.spellLogic)
    {
      case SpellLogic.Default:
        return new ZSpell();
      case SpellLogic.FlameWall:
        return (ZSpell) new ZFlameWallSpell();
      case SpellLogic.Napalm:
        return (ZSpell) new ZNapalmSpell();
      case SpellLogic.Pebble:
        return (ZSpell) new ZPebbleSpell();
      case SpellLogic.Arrow:
        return (ZSpell) new ZSpellArrow();
      case SpellLogic.CallingBell:
        return (ZSpell) new ZSpellCallingBell();
      case SpellLogic.ChainLightning:
        return (ZSpell) new ZSpellChainLightning();
      case SpellLogic.Clam:
        return (ZSpell) new ZSpellClam();
      case SpellLogic.ClockworkBomb:
        return (ZSpell) new ZSpellClockworkBomb();
      case SpellLogic.CogFall:
        return (ZSpell) new ZSpellCogFall();
      case SpellLogic.ConductorRod:
        return (ZSpell) new ZSpellConductorRod();
      case SpellLogic.CustomGravity:
        return (ZSpell) new ZSpellCustomGravity();
      case SpellLogic.EnduringSpirits:
        return (ZSpell) new ZSpellEnduringSpirits();
      case SpellLogic.Entangle:
        return (ZSpell) new ZSpellEntangle();
      case SpellLogic.MechanicalArrow:
        return (ZSpell) new ZSpellMechanicalArrow();
      case SpellLogic.MegaBoulder:
        return (ZSpell) new ZSpellMegaBoulder();
      case SpellLogic.Meteor:
        return (ZSpell) new ZSpellMeteor();
      case SpellLogic.Sand:
        return (ZSpell) new ZSpellSand();
      case SpellLogic.Snowball:
        return (ZSpell) new ZSpellSnowball();
      case SpellLogic.ThornBall:
        return (ZSpell) new ZSpellThornBall();
      case SpellLogic.WaterDrop:
        return (ZSpell) new ZSpellWaterDrop();
      case SpellLogic.BloodClot:
        return (ZSpell) new ZSpellBloodClot();
      case SpellLogic.Leaf:
        return (ZSpell) new ZSpellLeaf();
      case SpellLogic.Javelin:
        return (ZSpell) new ZSpellJavelin();
      default:
        Debug.LogError((object) ("Unknown spell logic :" + (object) x));
        return new ZSpell();
    }
  }

  public static void CameraFollow(Vector3 t)
  {
    CameraMovement.followTargets.Enqueue((IFollowTarget) new FollowPosition((Vector2) t));
  }

  public static void CameraFollow(MyLocation t)
  {
    ZSpell.CameraFollow((Vector3) t.ToSinglePrecision());
  }

  public static void FireTeleport(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.inWater = false;
    MyLocation position = c.position;
    if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
    {
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
      ZComponent.Instantiate<GameObject>(theSpell.explosion, target.ToSinglePrecision(), Quaternion.identity);
    }
    if ((ZComponent) c.mount != (object) null)
      c.Demount();
    if ((ZComponent) c.tower != (object) null)
    {
      c.CheckEffectorsOnMove();
      c.CreatureMoveSurroundings();
      c.game.CreatureMoveSurroundings(position, c.radius + 5);
      if ((ZComponent) c.tower != (object) null)
      {
        if (c.tower.type == TowerType.Arcane || c.tower.type == TowerType.Wooden)
        {
          c.tower.SetPosition(target);
          c.tower.ShouldFall();
          return;
        }
        c.DestroyTower();
        c.MoveToPosition = target;
        return;
      }
    }
    List<ParticleSystem> list = new List<ParticleSystem>();
    c.GetComponentsInChildren<ParticleSystem>(false, list);
    foreach (Component component in list)
      component.gameObject.SetActive(false);
    c.MoveToPosition = target;
    c.CheckEffectorsOnMove();
    if (c.flying && !c.pawn)
      c.RemoveFlight();
    else
      c.Fall();
    c.CreatureMoveSurroundings();
    c.game.CreatureMoveSurroundings(position, c.radius + 5);
    foreach (ParticleSystem particleSystem in list)
      particleSystem?.gameObject.SetActive(true);
    if (ZComponent.IsNull((ZComponent) c) || c.isDead || !c.game.First_Turn_Teleport || c.parent.localTurn > 0 || !c.game.gameFacts.GetStyle().HasStyle(GameStyle.Watchtower))
      return;
    c.KillMovement();
    ZSpell.FireTower(Inert.Instance.spells["Watchtower"], c, c.position, (FixedInt) 0, (FixedInt) 0);
  }

  public static void FireTeleportNoDetower(Spell theSpell, ZCreature c, MyLocation target)
  {
    if ((ZComponent) c == (object) null)
      return;
    target = c.MakeSureInBounds(target);
    if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) c.transform != (UnityEngine.Object) null && (UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null)
    {
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
      ZComponent.Instantiate<GameObject>(theSpell.explosion, target.ToSinglePrecision(), Quaternion.identity);
    }
    MyLocation position = c.position;
    if ((ZComponent) c.mount != (object) null)
      c.Demount();
    if ((ZComponent) c.tower != (object) null)
    {
      int radius = c.tower.radius + 20;
      c.tower.SetPosition(target);
      c.CheckEffectorsOnMove();
      if ((ZComponent) c.tower != (object) null)
      {
        if (c.tower.type == TowerType.Nature)
          c.tower.sanctuaryEffector.CheckSanctuary();
        ZSpell.CheckSurroundings(c);
        if ((ZComponent) c.tower != (object) null)
          c.tower.ShouldFall();
        c.CreatureMoveSurroundings();
        c.game.CreatureMoveSurroundings(position, radius);
        return;
      }
    }
    List<ParticleSystem> list = new List<ParticleSystem>();
    c.GetComponentsInChildren<ParticleSystem>(false, list);
    foreach (ParticleSystem particleSystem in list)
    {
      if ((UnityEngine.Object) particleSystem != (UnityEngine.Object) null && (UnityEngine.Object) particleSystem.gameObject != (UnityEngine.Object) null)
        particleSystem.gameObject.SetActive(false);
    }
    c.MoveToPosition = target;
    c.CheckEffectorsOnMove();
    ZSpell.CheckSurroundings(c);
    c.CreatureMoveSurroundings();
    c.Fall();
    c.game.CreatureMoveSurroundings(position, c.radius + 5);
    foreach (ParticleSystem particleSystem in list)
    {
      if ((UnityEngine.Object) particleSystem != (UnityEngine.Object) null && (UnityEngine.Object) particleSystem.gameObject != (UnityEngine.Object) null)
        particleSystem.gameObject.SetActive(true);
    }
  }

  public static void FireRecall(Spell theSpell, ZCreature c, MyLocation target)
  {
    target = c.position;
    ZCreature c1 = c.game.players[(int) c.game.serverState.playersTurn].controlled[0];
    if (c1.inWater)
    {
      c.game.serverState.busy = ServerState.Busy.Between_Turns;
      c1.parent.yourTurn = false;
    }
    c.OnDeath(false);
    ZSpell.FireTeleportNoDetower(theSpell, c1, target);
  }

  public static void CheckSurroundings(ZCreature c)
  {
    List<ZMyCollider> zmyColliderList = c.game.world.OverlapColliderAll((ZComponent) c.tower == (object) null ? c.collider : c.tower.collider, 43520);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      ZEffector effector = zmyColliderList[index].effector;
      if (!ZComponent.IsNull((ZComponent) effector))
        effector.EffectCreature(c);
    }
  }

  public static void FireDive(Spell theSpell, ZCreature c, MyLocation target, bool extended)
  {
    target.y = (FixedInt) (c.type == CreatureType.Kraken ? 0 : c.radius);
    ExplosionCutout explosionCutout = ExplosionCutout.Seventy_Five;
    if (c.radius > 50)
      explosionCutout = ExplosionCutout.Three_Fifty;
    else if (c.radius > 40)
      explosionCutout = ExplosionCutout.Two_Seventy;
    else if (c.radius > 20)
      explosionCutout = ExplosionCutout.One_Twenty_Eight;
    c.map.ServerBitBlt((int) explosionCutout, (int) target.x, 0);
    c.map.ServerBitBlt((int) explosionCutout, (int) c.position.x, 0);
    if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
    {
      Inert.Instance.Splash(c.transform.position, (float) c.radius);
      Inert.Instance.Splash((Vector3) target.ToSinglePrecision(), (float) c.radius);
    }
    MyLocation position = c.position;
    if (target.x > c.position.x)
      c.SetScale(1f);
    else
      c.SetScale(-1f);
    c.MoveToPosition = target;
    ZSpell.DiveForce(theSpell, c, explosionCutout, position, target, extended);
  }

  public static void DiveForce(
    Spell theSpell,
    ZCreature c,
    ExplosionCutout e,
    MyLocation oldPos,
    MyLocation target,
    bool extended)
  {
    ZCreature rider = !extended ? c.rider : (ZCreature) null;
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, new MyLocation(target.x, (FixedInt) 0), Quaternion.identity);
    zspell.explisiveForce = (FixedInt) (c.radius / 5);
    zspell.EXORADIUS = (int) ((FixedInt) ExplosionSize.GetRadius(e) * FixedInt.Create(1572864L));
    zspell.radius = c.radius;
    zspell.ApplyExplosionForce(zspell.position);
    if (oldPos.y > -1000)
    {
      zspell.Undie();
      zspell.position = new MyLocation(oldPos.x, (FixedInt) 0);
      zspell.ApplyExplosionForce(zspell.position);
    }
    if (!ZComponent.IsNull((ZComponent) rider) && !ZComponent.IsNull((ZComponent) c) && (ZComponent) c.rider == (object) rider)
      rider.KillMovement();
    ZComponent.Destroy<GameObject>(zspell.gameObject);
  }

  public static void FireSpiritLink(Spell theSpell, ZCreature c, MyLocation target)
  {
    c.map.ServerBitBlt((int) theSpell.explosionCutout, (int) c.position.x, (int) c.position.y);
    ZCreature c1 = c.map.PhysicsCollideCreature(c, (int) target.x, (int) target.y, Inert.mask_Phantom);
    if (!((ZComponent) c1 != (object) null))
      return;
    if ((ZComponent) c1.rider != (object) null)
      c1.rider.Demount();
    ZSpell.FireTeleportNoDetower(theSpell, c1, c.position);
    c.OnDeath(true);
  }

  public static void FireFairyRing(Spell theSpell, ZCreature c, MyLocation target)
  {
    c.map.ServerBitBlt((int) theSpell.explosionCutout, (int) c.position.x, (int) c.position.y);
    MyLocation myLocation = target;
    List<ZMyCollider> zmyColliderList1 = c.game.world.OverlapCircleAll((Point) c.position, 15, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    for (int index = 0; index < zmyColliderList1.Count; ++index)
    {
      ZCreature creature = zmyColliderList1[index].gameObjectLayer == 13 ? (ZCreature) null : zmyColliderList1[index].creature;
      if (!ZComponent.IsNull((ZComponent) creature) && creature.team == c.team && (ZComponent) c != (object) creature && (ZComponent) creature.mount == (object) null)
      {
        target = creature.position + new MyLocation(0, c.position.y > creature.position.y ? -15 : 15);
        target.x = Mathd.Clamp(target.x, (FixedInt) creature.radius, (FixedInt) (c.map.Width - creature.radius));
        target.y = Mathd.Clamp(target.y, (FixedInt) creature.radius, (FixedInt) (c.map.Height - creature.radius));
        if (!((ZComponent) creature.tower != (object) null))
        {
          creature.MoveToPosition = target;
          creature.CheckEffectorsOnMove();
          creature.Fall();
        }
      }
    }
    List<ZMyCollider> zmyColliderList2 = c.game.world.OverlapCircleAll((Point) myLocation, 30, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    List<ZCreature> zcreatureList = new List<ZCreature>();
    for (int index = 0; index < zmyColliderList2.Count; ++index)
    {
      ZCreature z = zmyColliderList2[index].gameObjectLayer == 13 ? zmyColliderList2[index].tower?.creature : zmyColliderList2[index].creature;
      if (!ZComponent.IsNull((ZComponent) z) && z.team == c.team && (ZComponent) c != (object) z && !zcreatureList.Contains(z) && MyLocation.Distance(z.position, myLocation) < 30)
      {
        target = z.position - myLocation + c.position;
        target.x = Mathd.Clamp(target.x, (FixedInt) z.radius, (FixedInt) (c.map.Width - z.radius));
        target.y = Mathd.Clamp(target.y, (FixedInt) z.radius, (FixedInt) (c.map.Height - z.radius));
        List<ParticleSystem> list = new List<ParticleSystem>();
        z.GetComponentsInChildren<ParticleSystem>(false, list);
        foreach (Component component in list)
          component.gameObject.SetActive(false);
        if (!ZComponent.IsNull((ZComponent) z.rider))
          zcreatureList.Add(z.rider);
        else if ((ZComponent) z.mount != (object) null)
        {
          zcreatureList.Add(z.mount);
          if (MyLocation.Distance(z.mount.position, myLocation) < 30)
          {
            z.mount.MoveToPosition = target;
            z.mount.Fall();
            continue;
          }
          z.Demount();
        }
        if ((ZComponent) z.tower != (object) null)
        {
          if (z.tower.type == TowerType.Arcane)
          {
            z.tower.SetPosition(target);
            z.tower.ShouldFall();
          }
          else
          {
            z.DestroyTower();
            z.MoveToPosition = target;
          }
        }
        else
        {
          z.MoveToPosition = target;
          z.Fall();
        }
        foreach (ParticleSystem particleSystem in list)
          particleSystem?.gameObject.SetActive(true);
      }
    }
    c.game.CreatureMoveSurroundings(myLocation, 50);
    MyLocation position = c.position;
    c.OnDeath(true);
    c.game.CreatureMoveSurroundings(position, 50, forceLeaf: true);
  }

  public static void AddPortalToOtherPlayer(ZCreature c, ZEffector portal)
  {
    c.effectors.Add(new ZEffector()
    {
      type = EffectorType.Fake_Portal,
      whoSummoned = c,
      game = portal.game,
      active = false,
      MaxTurnsAlive = 10000,
      partner = portal
    });
  }

  public static void AddPortalToSpawnedPlayer(ZCreature c)
  {
    foreach (ZEffector globalEffector in c.game.globalEffectors)
    {
      if ((ZComponent) globalEffector != (object) null && !globalEffector.dead && (globalEffector.type == EffectorType.Portal || globalEffector.type == EffectorType.Wormhole))
        ZSpell.AddPortalToOtherPlayer(c, globalEffector);
    }
  }

  public static void FireArcanePortal(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    int spellSlot,
    MyLocation target)
  {
    for (int index = c.game.globalEffectors.Count - 1; index >= 0; --index)
    {
      if (!ZComponent.IsNull((ZComponent) c.game.globalEffectors[index]) && (c.game.globalEffectors[index].type == EffectorType.Portal || c.game.globalEffectors[index].type == EffectorType.Wormhole) && (ZComponent) c.game.globalEffectors[index].whoSummoned == (object) c && (c.game.globalEffectors[index].variable == spellSlot || c.game.globalEffectors[index].type == EffectorType.Wormhole || c.game.globalEffectors[index].type != theSpell.toSummon.GetComponent<Effector>().type))
      {
        c.game.globalEffectors[index].Die(index, false, true);
        c.game.globalEffectors[index] = (ZEffector) null;
      }
    }
    if (theSpell.spellEnum == SpellEnum.Arcane_Portal)
    {
      SpellSlot spellSlot1 = c.GetSpellSlot(SpellEnum.Wormhole);
      if (spellSlot1 != null)
        spellSlot1.SetTurnFired = c.parent.localTurn;
    }
    else if (theSpell.spellEnum == SpellEnum.Wormhole)
    {
      SpellSlot spellSlot2 = c.GetSpellSlot(SpellEnum.Arcane_Portal);
      if (spellSlot2 != null)
        spellSlot2.SetTurnFired = c.parent.localTurn;
    }
    MyLocation myLocation = c.OverHeadOffset + c.position;
    if ((ZComponent) c.tower != (object) null && c.tower.type != TowerType.Nature)
      myLocation.y += 25;
    else
      myLocation.y += 42;
    myLocation.y += c.radius - 18;
    if (ZEffector.InSanctuary(c.game.world, myLocation) || ZEffector.InSanctuary(c.game.world, target))
    {
      ZEffector.SpawnVineExplosion((Vector3) pos.ToSinglePrecision());
      ZEffector.SpawnVineExplosion((Vector3) target.ToSinglePrecision());
    }
    else
    {
      if (c.game.isClient && !c.game.resyncing)
      {
        ZComponent.Instantiate<GameObject>(theSpell.explosion, myLocation.ToSinglePrecision(), Quaternion.identity);
        ZComponent.Instantiate<GameObject>(theSpell.explosion, target.ToSinglePrecision(), Quaternion.identity);
      }
      int num1 = theSpell.spellEnum == SpellEnum.Wormhole ? theSpell.radius : (c.game.originalSpellsOnly ? 20 : c.radius + 2);
      ZEffector zeffector1 = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) myLocation.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      zeffector1.variable = spellSlot;
      zeffector1.game = c.game;
      zeffector1.collider.radius = num1;
      zeffector1.collider.world = zeffector1.world;
      zeffector1.collider.Move(myLocation);
      zeffector1.whoSummoned = c;
      c.game.globalEffectors.Insert(0, zeffector1);
      foreach (ZPerson player in c.game.players)
      {
        if ((ZComponent) player.first() != (object) null && !player.first().isDead)
          ZSpell.AddPortalToOtherPlayer(player.first(), zeffector1);
      }
      if (theSpell.spellEnum != SpellEnum.Wormhole)
        zeffector1.transform.GetChild(3).GetComponent<RotateAroundCenter>().speed *= -1f;
      else
        zeffector1.transform.GetChild(4).GetComponent<RotateTarget>().speed *= -1f;
      ZEffector zeffector2 = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      if (theSpell.spellEnum == SpellEnum.Wormhole)
      {
        zeffector2.variable = 666;
        zeffector2.PortalDisable();
      }
      zeffector2.game = c.game;
      zeffector2.collider.radius = num1;
      zeffector2.collider.world = zeffector2.world;
      zeffector2.collider.Move(target);
      zeffector2.whoSummoned = c;
      zeffector1.partner = zeffector2;
      zeffector2.partner = zeffector1;
      zeffector1.active = false;
      zeffector2.active = false;
      if (c.game.isClient)
      {
        float num2 = (float) num1 / (float) theSpell.radius;
        zeffector1.transform.localScale = new Vector3(num2, num2, num2);
        zeffector2.transform.localScale = new Vector3(num2, num2, num2);
      }
      else
      {
        for (int index = 0; index < zeffector1.transform.childCount; ++index)
        {
          zeffector1.transform.GetChild(index).gameObject.SetActive(false);
          zeffector2.transform.GetChild(index).gameObject.SetActive(false);
        }
      }
      c.game.ongoing.RunSpell(ZSpell.delayPortal(zeffector1));
    }
  }

  public static void FireGlobalPortal(Spell theSpell, ZGame game, MyLocation pos, MyLocation pos2)
  {
    ZEffector e = ZEffector.Create(game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) pos2.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
    e.variable = 0;
    e.game = game;
    e.collider.radius = 34;
    e.collider.world = e.world;
    e.collider.Move(pos2);
    e.whoSummoned = (ZCreature) null;
    game.globalEffectors.Insert(0, e);
    e.transform.GetChild(3).GetComponent<RotateAroundCenter>().speed *= -1f;
    ZEffector zeffector = ZEffector.Create(game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) pos.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform());
    zeffector.game = game;
    zeffector.collider.radius = 34;
    zeffector.collider.world = zeffector.world;
    zeffector.collider.Move(pos);
    zeffector.whoSummoned = (ZCreature) null;
    e.partner = zeffector;
    zeffector.partner = e;
    e.active = false;
    zeffector.active = false;
    if (game.isClient)
    {
      float num = 1.7f;
      e.transform.localScale = new Vector3(num, num, num);
      zeffector.transform.localScale = new Vector3(num, num, num);
    }
    else
    {
      for (int index = 0; index < e.transform.childCount; ++index)
      {
        e.transform.GetChild(index).gameObject.SetActive(false);
        zeffector.transform.GetChild(index).gameObject.SetActive(false);
      }
    }
    game.ongoing.RunSpell(ZSpell.delayPortal(e));
  }

  private static IEnumerator<float> delayPortal(ZEffector e)
  {
    yield return 0.0f;
    yield return 0.0f;
    e.TurnPassed(e.game.globalEffectors.Count - 1, false, true);
  }

  public static void FireBanish(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    if (c.game.isClient && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    c.map.ServerBitBlt((int) theSpell.explosionCutout, (int) c.position.x, (int) c.position.y);
    c.parent.controlled.Remove(c);
    if (c.game.isClient)
      c.parent.panelPlayer.SetSummons(c.parent);
    c.map.OnGhosted((int) c.position.x, (int) c.position.y, 32, c.collider);
    c.OnDeath(true);
  }

  public static void FireMelt(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    if (c.game.isClient && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    ZSpell.OnExplosionMelt(theSpell.toSummon.GetComponent<Spell>(), c.game, c.position, c, MyLocation.zero, c.health / 4);
    c.parent.controlled.Remove(c);
    if (c.game.isClient)
      c.parent.panelPlayer.SetSummons(c.parent);
    c.OnDeath(true);
  }

  public static void FireSnowbolt(
    Spell theSpell,
    ZCreature c,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, c.position, Quaternion.identity, rot_z, power, target));
  }

  public static void FireSacrifice(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    if (c.game.isClient && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    foreach (ZMyCollider zmyCollider in c.world.OverlapCircleAll((Point) c.position, 100, (ZMyCollider) null, 65792))
    {
      ZCreature creature = zmyCollider.creature;
      if (!ZComponent.IsNull((ZComponent) creature) && creature.isPawn && creature.race != CreatureRace.Effector)
        creature.OnDeath(true);
    }
  }

  public static void FireSelfDestruct(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
    {
      GameObject gameObject = ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
      float num = (float) ExplosionSize.GetRadius(ExplosionSize.GetImpRadius(c.health)) / 118f;
      gameObject.transform.localScale = new Vector3(num, num, 1f);
    }
    c.game.ongoing.RunSpell(ZSpell.IEFireSelfDestruct(theSpell, c));
  }

  public static IEnumerator<float> IEFireSelfDestruct(Spell theSpell, ZCreature c)
  {
    for (int i = 0; i < 10; ++i)
      yield return 0.0f;
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, c.position, Quaternion.identity);
    ExplosionCutout impRadius = ExplosionSize.GetImpRadius(c.health);
    zspell.EXORADIUS = ((FixedInt) ExplosionSize.GetRadius(impRadius) * (FixedInt) 1363148L).ToInt();
    c.map.ServerBitBlt((int) impRadius, (int) c.position.x, (int) c.position.y);
    zspell.damage = c.health;
    zspell.explisiveForce += c.health > 50 ? (FixedInt) (c.health - 50) / 20 : (FixedInt) 0;
    zspell.ApplyExplosionForce(c.position);
    ZComponent.Destroy<GameObject>(zspell.gameObject);
    c.OnDeath(true);
  }

  public static void DuplicateMarket(ZCreature c, MinerMarket m)
  {
    if (m == null)
      return;
    c.minerMarket = new MinerMarket();
    for (int index = 0; index < c.minerMarket.bought.Length; ++index)
    {
      if (m.bought[index])
      {
        c.minerMarket.bought[index] = true;
        if (c.game.isClient)
        {
          if ((UnityEngine.Object) c.minerMarket.clientObj == (UnityEngine.Object) null)
            c.minerMarket.clientObj = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.pfabMinerMarketIcons, c.transform.localPosition + new Vector3((float) (c.radius + 16), (float) (c.radius + 3)), Quaternion.identity, c.overheadCanvas.transform);
          Transform transform1 = c.minerMarket.clientObj.transform;
          Transform transform2 = UnityEngine.Object.Instantiate<Transform>(transform1.GetChild(0), transform1);
          transform2.GetComponent<SpriteRenderer>().sprite = Inert.Instance.minerMarket[index].icon;
          transform2.gameObject.SetActive(true);
        }
      }
    }
  }

  public static void FireMinerMarket(Spell theSpell, ZCreature c, int y, bool doDamage)
  {
    MinerMarket.Types types = (MinerMarket.Types) y;
    if (types < MinerMarket.Types.Light)
      types = MinerMarket.Types.Light;
    else if (types > MinerMarket.Types.x3)
      types = MinerMarket.Types.x3;
    if (c.minerMarket == null)
      c.minerMarket = new MinerMarket();
    if (doDamage && !c.minerMarket.CanBuy(c, types))
      return;
    c.minerMarket.Buy(types);
    if (c.minerMarket.count == 15 && !c.game.isClient)
      Achievements.Set(Achievement.Livin_the_Dream, c.parent.account, c.game);
    if (c.game.isClient)
    {
      if ((UnityEngine.Object) c.minerMarket.clientObj == (UnityEngine.Object) null)
        c.minerMarket.clientObj = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.pfabMinerMarketIcons, c.transform.localPosition + new Vector3((float) (c.radius + 16), (float) (c.radius + 3)), Quaternion.identity, c.overheadCanvas.transform);
      Transform transform1 = c.minerMarket.clientObj.transform;
      Transform transform2 = UnityEngine.Object.Instantiate<Transform>(transform1.GetChild(0), transform1);
      transform2.GetComponent<SpriteRenderer>().sprite = Inert.Instance.minerMarket[(int) types].icon;
      transform2.gameObject.SetActive(true);
    }
    if (doDamage)
      c.DoDamage(Inert.Instance.minerMarket[(int) types].cost);
    if (c.game.isClient && !c.game.resyncing)
      ChatBox.Instance?.NewChatMsg(c.parent.name + "'s dwarf equipped " + types.ToString().Replace('_', ' '), (Color) ColorScheme.GetColor(Color.white));
    switch (types)
    {
      case MinerMarket.Types.Light:
        c.HighJumpData.y += 3;
        c.LongJumpData.y += 2;
        break;
      case MinerMarket.Types.H2O:
        c.waterWalking = true;
        break;
      case MinerMarket.Types.Rock_Blaster:
        if (c.GetSpellSlot(SpellEnum.Rock_Blaster) != null)
          break;
        c.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Rock_Blaster))
        {
          SetTurnFired = c.parent.localTurn
        });
        break;
      case MinerMarket.Types.Map:
        if (c.GetSpellSlot(SpellEnum.Miner_Map) != null)
          break;
        c.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Miner_Map))
        {
          SetTurnFired = c.parent.localTurn
        });
        break;
      case MinerMarket.Types.Shaft:
        if (c.GetSpellSlot(SpellEnum.Shaft) != null)
          break;
        c.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Shaft))
        {
          SetTurnFired = c.parent.localTurn
        });
        break;
      case MinerMarket.Types.x2:
        c.spells[1].SetTurnFired = c.parent.localTurn;
        if (!doDamage)
          break;
        c.OnStunned();
        break;
      case MinerMarket.Types.x3:
        c.spells[1].SetTurnFired = c.parent.localTurn;
        if (!doDamage)
          break;
        c.OnStunned();
        break;
    }
  }

  public static void FireKablam(Spell theSpell, ZCreature c, MyLocation pos, FixedInt rot_z)
  {
    MyLocation pos1 = Inert.Velocity(rot_z) * 20 + pos;
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, pos1, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()));
    if (c.minerMarket != null)
    {
      if (c.minerMarket.Has(MinerMarket.Types.Dynamite))
        zspell.maxDuration += 30;
      if (c.minerMarket.Has(MinerMarket.Types.x2))
        zspell.explosionCutout = ExplosionCutout.Two_Twenty_One;
      if (c.minerMarket.Has(MinerMarket.Types.x3))
        zspell.explosionCutout = zspell.explosionCutout != ExplosionCutout.Two_Twenty_One ? ExplosionCutout.Three_Fifty : ExplosionCutout.Six_Fifty_Three;
      if (c.minerMarket.Has(MinerMarket.Types.Depth_Charge))
        zspell.Fall();
      zspell.EXORADIUS = ExplosionSize.GetRadius(zspell.explosionCutout) + 5;
    }
    c.game.ongoing.RunSpell(zspell.ZSpellMoveKablam());
    c.game.ongoing.RunSpell(c.MoveMultipleSteps(pos1.x > c.position.x, zspell.maxDuration));
  }

  public static void FireMine(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    int addTime = 0,
    bool postStartingTurn = false)
  {
    if (c.isMoving && !postStartingTurn || (ZComponent) c.tower != (object) null || c.inWater)
      return;
    int num = c.minerMarket == null || !c.minerMarket.Has(MinerMarket.Types.Drill) ? 0 : theSpell.maxDuration / 2;
    CoroutineInstance ongoing = c.game.ongoing;
    c.moving = ongoing.RunSpell(c.DKMove(new MyLocation(target.x, target.y), theSpell.explosionCutout, theSpell.maxDuration + num + addTime, false, false, 18), !postStartingTurn);
    ongoing.RunSpell(ZSpell.IEMine(theSpell, c, target, theSpell.maxDuration + addTime), !postStartingTurn);
  }

  public static void FireCharge(Spell theSpell, ZCreature c, MyLocation target)
  {
    if ((ZComponent) c.tower != (object) null || c.inWater)
      return;
    if (c.isMoving)
      c.KillMovement();
    FixedInt x = c.position.x - target.x;
    FixedInt y = c.position.y - target.y;
    bool spotOn = c.game.AllowDKH && FixedInt.Abs(x - 1) <= (FixedInt) 524288L && FixedInt.Abs(y - 6) <= (FixedInt) 524288L;
    if (spotOn)
    {
      target.x = c.position.x;
      target.y = c.position.y - 60;
    }
    if (c.game.AllowDKH)
      c.moving = c.game.ongoing.RunSpell(c.DKMove(new MyLocation(target.x, target.y), theSpell.explosionCutout, theSpell.maxDuration, true, spotOn, 25));
    else
      c.moving = c.game.ongoing.RunSpell(c.DKMove(c.position - new MyLocation(x, y) * (spotOn ? 1 : 120), theSpell.explosionCutout, theSpell.maxDuration, true, spotOn, 25));
    c.game.ongoing.RunSpell(ZSpell.IECharge(theSpell, c));
  }

  public static IEnumerator<float> IECharge(Spell theSpell, ZCreature c)
  {
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, c.position, Quaternion.identity);
    int radius = c.radius;
    for (int i = 0; i <= theSpell.maxDuration && !ZComponent.IsNull((ZComponent) c) && !c.stunned && !c.InDarkTotem(); ++i)
    {
      if (i % 3 == 2)
      {
        kablam.position = c.position + c.velocity.normalized * (theSpell.radius + 5);
        kablam.ApplyExplosionForce(kablam.position);
        kablam.Undie();
      }
      yield return 0.0f;
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static IEnumerator<float> IEWolfJump(
    ZGame game,
    ZCreature whoSummoned,
    ZCreature cre,
    int wait,
    int damage)
  {
    for (int i = 0; i < wait && !whoSummoned.stunned; ++i)
      yield return 0.0f;
    if (!ZComponent.IsNull((ZComponent) cre) && !whoSummoned.stunned && !whoSummoned.isDead && !cre.inWater)
    {
      FixedInt fixedInt1 = (FixedInt) (whoSummoned.team == 24 ? 26 : 20);
      bool smallAngle = true;
      FixedInt d = FixedInt.Abs(whoSummoned.position.x - cre.position.x);
      FixedInt fixedInt2 = ZSpell.AngleToGoDistance(fixedInt1, -game.map.Gravity, d, cre.position.y - whoSummoned.position.y, smallAngle);
      if (d > 10 && FixedInt.InvalidAngle(fixedInt2))
      {
        MyLocation myLocation = cre.position - whoSummoned.position;
        myLocation.Normalize();
        fixedInt2 = FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
      }
      else if ((FixedInt) cre.collider.x < whoSummoned.position.x)
        fixedInt2 = (FixedInt) 180 - fixedInt2;
      if (whoSummoned.isMoving)
        whoSummoned.KillMovement();
      whoSummoned.velocity = Inert.Velocity(fixedInt2, fixedInt1);
      whoSummoned.SetScale(whoSummoned.velocity.x.ToFloat());
      whoSummoned.moving = game.ongoing.RunSpell(whoSummoned.WolfLeap(damage));
    }
  }

  public static IEnumerator<float> IEMine(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    int addTime = 0)
  {
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, c.position, Quaternion.identity);
    int radius = c.radius;
    if (c.minerMarket != null && c.minerMarket.Has(MinerMarket.Types.Punch))
      kablam.explisiveForce *= 3;
    for (int i = 0; i < addTime && !ZComponent.IsNull((ZComponent) c) && !c.stunned; ++i)
    {
      if (i % 2 == 0)
      {
        MyLocation myLocation = target - c.position;
        kablam.position = c.position + Global.VelocityRight(FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg, (FixedInt) (theSpell.radius - 5));
        kablam.ApplyExplosionForce(kablam.position);
        kablam.Undie();
      }
      yield return 0.0f;
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static void FireImpDestruction(Spell theSpell, ZCreature c, MyLocation target)
  {
    Spell component = theSpell.toSummon.GetComponent<Spell>();
    foreach (ZPerson player in c.game.players)
      ZSpell.ImpKill(theSpell, component, player, c, target);
    if (c.game?._uncontrolledPlayer != null)
      ZSpell.ImpKill(theSpell, component, c.game._uncontrolledPlayer, c, target);
    if (c.game == null || c.game._playersExtended.Count <= 0)
      return;
    foreach (ZPerson p in c.game._playersExtended)
      ZSpell.ImpKill(theSpell, component, p, c, target);
  }

  private static void ImpKill(
    Spell theSpell,
    Spell arcaneArrow,
    ZPerson p,
    ZCreature c,
    MyLocation target)
  {
    for (int index = p.controlled.Count - 1; index >= 0; --index)
    {
      if (p.controlled[index].type == CreatureType.Imp && (p.controlled[index].spellEnum == SpellEnum.Summon_Imps || p.controlled[index].spellEnum == SpellEnum.Little_Devil))
      {
        ZCreature zcreature = p.controlled[index];
        if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) zcreature.transform != (UnityEngine.Object) null)
          ZComponent.Instantiate<GameObject>(theSpell.explosion, zcreature.transform.position, Quaternion.identity);
        c.map.ServerBitBlt((int) theSpell.explosionCutout, (int) zcreature.position.x, (int) zcreature.position.y);
        int amount = zcreature.health / 10;
        if (amount <= 0)
          amount = 1;
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorImpDestruction(arcaneArrow, c, zcreature.position, Quaternion.identity, (FixedInt) 10, amount, target));
        zcreature.parent?.controlled?.RemoveAt(index);
        if (c.game.isClient)
          zcreature.parent?.panelPlayer?.SetSummons(zcreature.parent);
        zcreature.OnDeath(true);
      }
    }
  }

  public static IEnumerator<float> IEnumeratorImpDestruction(
    Spell spell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt power,
    int amount,
    MyLocation target)
  {
    for (int i = 0; i < amount; ++i)
    {
      FixedInt fixedInt = power + c.game.RandomFixedInt(-3145728L, 1048576L);
      ZSpell zspell = ZSpell.BaseFireTarget(spell, c, pos, rot, Inert.Velocity((FixedInt) c.game.RandomInt(0, 360), fixedInt), fixedInt, target);
      if (c.HasArcaneEnergizer)
        zspell.damage = (int) ((FixedInt) zspell.damage * (FixedInt) 1572864L);
      zspell.maxTargetFrames += c.game.RandomInt(-5, 5);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> FireSkyRay(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    bool armageddon = false)
  {
    int x = (int) target.x;
    int y = c.map.Height - 1;
    for (; y >= 0; --y)
    {
      int x1 = x - 4;
      int num = 0;
      while (num < 9)
      {
        if (c.map.SpellCheckPosition(x1, y, (ZCreature) null, Inert.mask_spell_movement))
        {
          ++num;
          ++x1;
        }
        else
          goto label_7;
      }
    }
label_7:
    if (c.game.isClient && !c.game.resyncing)
    {
      ParticleSkyRay component = ZComponent.Instantiate<GameObject>(theSpell.toSummon, new Vector3((float) x, (float) (c.map.Height + 800)), Quaternion.identity, c.game.GetMapTransform()).GetComponent<ParticleSkyRay>();
      component.start = new Vector3((float) x, (float) (c.map.Height + 800));
      component.target = new Vector3((float) x, (float) y);
      ZSpell.CameraFollow(component.target);
    }
    for (int i = 0; i < 30; ++i)
      yield return 0.0f;
    if (c.game.isClient && !c.game.resyncing)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, new Vector3((float) x, (float) y), Quaternion.identity);
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, new MyLocation(x, y), Quaternion.identity);
    zspell.fromArmageddon = armageddon;
    c.map.ServerBitBlt((int) theSpell.explosionCutout, x, y);
    zspell.ApplyExplosionForce(new MyLocation(x, y));
    ZComponent.Destroy<GameObject>(zspell.gameObject);
  }

  public static void FireProtectionShield(Spell theSpell, ZCreature c)
  {
    c.turnProtectionShieldCast = c.game.turn;
    if (ZEffector.InSanctuary(c.game.world, c.position))
    {
      ZEffector.SpawnVineExplosion(c.transform.position);
    }
    else
    {
      c.CreateProtectionShield();
      c.shield += 50;
      if (c.shield > 150 && !c.game.originalSpellsOnly && c.familiarLevelOverlight == 0)
        c.shield = 150;
      c.UpdateHealthTxt();
      Achievements.CheckShieldsUp(c);
    }
  }

  public static void FireVineBloom(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), c.transform.position, Quaternion.identity, c.transform);
    zeffector.game = c.game;
    zeffector.followParent = true;
    zeffector.whoSummoned = c;
    c.effectors.Add(zeffector);
    zeffector.active = false;
  }

  public static void FireStaticShield(Spell theSpell, ZCreature c)
  {
    if (ZEffector.InSanctuary(c.game.world, c.position))
    {
      ZEffector.SpawnVineExplosion(c.transform.position);
    }
    else
    {
      ZEffectorStaticBall zeffectorStaticBall = ZEffector.CreateStatic(c.game, (Effector) theSpell.toSummon.GetComponent<EffectorStaticBall>(), c.transform.position, Quaternion.identity, c.game.GetMapTransform());
      zeffectorStaticBall.followParent = true;
      zeffectorStaticBall.whoSummoned = c;
      zeffectorStaticBall.collider.world = zeffectorStaticBall.world;
      zeffectorStaticBall.collider.Move(c.position);
      c.destroyableEffectors.Add((ZEffector) zeffectorStaticBall);
      c.effectorStaticShield = zeffectorStaticBall;
      c.followingColliders.Add(zeffectorStaticBall.collider);
    }
  }

  public void OnExplosionStaticBall()
  {
    if ((ZComponent) this.parent == (object) null || this.parent.isDead)
      return;
    if (ZEffector.InSanctuary(this.parent.game.world, this.position))
    {
      ZEffector.SpawnVineExplosion(this.transform.position);
    }
    else
    {
      ZEffectorStaticBall zeffectorStaticBall = ZEffector.CreateStatic(this.game, (Effector) this.toSummon.GetComponent<EffectorStaticBall>(), this.transform.position, Quaternion.identity, this.game.GetMapTransform());
      zeffectorStaticBall.game = this.game;
      zeffectorStaticBall.collider.world = zeffectorStaticBall.world;
      zeffectorStaticBall.collider.Move(this.position);
      zeffectorStaticBall.whoSummoned = this.parent;
      this.parent.effectors.Add((ZEffector) zeffectorStaticBall);
    }
  }

  public static void FireCallOfTheVoid(Spell theSpell, ZCreature c, MyLocation target)
  {
    if (ZComponent.IsNull((ZComponent) c))
      return;
    c.game.forceRysncPause = true;
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    c.effectors.Add(zeffector);
    zeffector.game = c.game;
    zeffector.whoSummoned = c;
    zeffector.position = target;
    zeffector.active = false;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
  }

  public static void FireInfection(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    bool destroyable,
    MyLocation target,
    int radius = 1)
  {
    if (ZComponent.IsNull((ZComponent) c))
      return;
    List<ZMyCollider> zmyColliderList = c.world.OverlapCircleAll(new Point((int) target.x, (int) target.y), radius, (ZMyCollider) null, Inert.mask_Phantom | Inert.mask_movement_NoEffector);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      ZCreature creature = zmyColliderList[index].creature;
      if ((ZComponent) creature == (object) null && (ZComponent) zmyColliderList[index].tower != (object) null)
        creature = zmyColliderList[index].tower.creature;
      if ((ZComponent) creature != (object) null && creature.type != CreatureType.Tree && creature.race != CreatureRace.Effector)
      {
        ZEffector.SpreadInfection(c, creature, (ZEffector) null, true);
        break;
      }
    }
  }

  public static void FireRisingLava(Spell theSpell, ZGame game)
  {
    ZPerson zperson = game.CurrentPlayer();
    if (zperson == null || zperson.game.globalEffectors.FindIndex((Predicate<ZEffector>) (a => (ZComponent) a != (object) null && a.type == EffectorType.Rising_Lava)) != -1)
      return;
    ZEffector zeffector = ZEffector.Create(game, theSpell.toSummon.GetComponent<Effector>(), new Vector3((float) game.map.Width / 2f, 0.0f), Quaternion.identity, game.GetMapTransform());
    zeffector.game = game;
    zeffector.active = false;
    zeffector.position = new MyLocation(game.map.Width / 2, 0);
    foreach (ZEffector globalEffector in game.globalEffectors)
    {
      if (globalEffector.type == EffectorType.Dense_Fog)
      {
        globalEffector.halved = true;
        zeffector.halved = true;
        globalEffector.VisualUpdate();
        break;
      }
    }
    game.globalEffectors.Add(zeffector);
    zeffector.TurnPassed(game.globalEffectors.Count - 1, false, true);
  }

  public static void FireDenseFog(Spell theSpell, ZGame game)
  {
    ZPerson zperson = game.CurrentPlayer();
    if (zperson == null || zperson.game.globalEffectors.FindIndex((Predicate<ZEffector>) (a => (ZComponent) a != (object) null && a.type == EffectorType.Dense_Fog)) != -1)
      return;
    ZEffector zeffector = ZEffector.Create(game, theSpell.toSummon.GetComponent<Effector>(), new Vector3((float) game.map.Width / 2f, (float) game.map.Height), Quaternion.identity, game.GetMapTransform());
    zeffector.game = game;
    zeffector.active = false;
    zeffector.position = new MyLocation(game.map.Width / 2, game.map.Height);
    foreach (ZEffector globalEffector in game.globalEffectors)
    {
      if (globalEffector.type == EffectorType.Rising_Lava)
      {
        globalEffector.halved = true;
        zeffector.halved = true;
        globalEffector.VisualUpdate();
        break;
      }
    }
    game.globalEffectors.Add(zeffector);
    zeffector.TurnPassed(game.globalEffectors.Count - 1, false, true);
  }

  public static void FireEffector(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    bool destroyable)
  {
    if (destroyable && ZEffector.InSanctuary(c.game.world, c.position))
    {
      ZEffector.SpawnVineExplosion(c.transform.position);
    }
    else
    {
      c.game.forceRysncPause = true;
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), c.transform.position, Quaternion.identity, c.transform);
      zeffector.game = c.game;
      zeffector.followParent = true;
      zeffector.whoSummoned = c;
      if ((ZComponent) zeffector.collider != (object) null)
      {
        zeffector.collider.world = zeffector.world;
        c.followingColliders.Add(zeffector.collider);
        zeffector.collider.Initialize(c.position, c.game.world);
      }
      if (theSpell.spellEnum == SpellEnum.Wind_Shield)
      {
        for (int index = c.destroyableEffectors.Count - 1; index >= 0; --index)
        {
          if ((ZComponent) c.destroyableEffectors[index] != (object) null && c.destroyableEffectors[index].type == EffectorType.Spirit_Shield)
            c.destroyableEffectors[index].Die(index, true, false);
        }
        c.game.windShieldEffectors.Add(zeffector);
      }
      else if (theSpell.spellEnum == SpellEnum.Social_Distancing)
      {
        c.socialDistancing = true;
        if (c.familiarLevelIllusion == 5)
          zeffector.MaxTurnsAlive = 9000;
      }
      else if (theSpell.spellEnum == SpellEnum.Arcane_Energiser && c.familiarLevelArcane == 5)
        zeffector.MaxTurnsAlive = 9000;
      if (destroyable)
        c.destroyableEffectors.Add(zeffector);
      else
        c.effectors.Add(zeffector);
      Achievements.CheckShieldsUp(c);
    }
  }

  public static void FireEffectorStormShield(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    if ((ZComponent) c.stormShield != (object) null && c.stormShield.MaxTurnsAlive > 10)
      return;
    if (ZEffector.InSanctuary(c.game.world, c.position))
    {
      ZEffector.SpawnVineExplosion(c.transform.position);
    }
    else
    {
      c.game.forceRysncPause = true;
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), c.transform.position, Quaternion.identity, c.transform);
      zeffector.game = c.game;
      zeffector.whoSummoned = c;
      zeffector.followParent = true;
      if ((ZComponent) zeffector.collider != (object) null)
      {
        zeffector.collider.world = zeffector.world;
        zeffector.collider.radius = Math.Max(40, c.radius + 18);
        if (c.game.isClient && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
        {
          c.notScaled.Add((ZCreature.NotScaled) new ZCreature.OneScaled(zeffector.transform));
          zeffector.transform.localScale = new Vector3((float) zeffector.collider.radius / 40f / c.scale, (float) zeffector.collider.radius / 40f / c.scale, 1f);
        }
        c.followingColliders.Add(zeffector.collider);
        zeffector.collider.effector = zeffector;
        zeffector.collider.Move(c.position);
      }
      if (!ZComponent.IsNull((ZComponent) c.stormShield))
        c.stormShield.Die(c.destroyableEffectors.FindIndex((Predicate<ZEffector>) (ss => (ZComponent) ss == (object) c.stormShield)), true, false);
      c.stormShield = zeffector;
      c.destroyableEffectors.Add(zeffector);
      ++zeffector.MaxTurnsAlive;
      zeffector.TurnPassed(c.destroyableEffectors.Count - 1, true, false);
    }
  }

  public static void FireMammatusCloud(Spell theSpell, ZCreature c, MyLocation target)
  {
    if (ZEffector.InSanctuary(c.game.world, target))
    {
      ZEffector.SpawnVineExplosion((Vector3) target.ToSinglePrecision());
    }
    else
    {
      c.game.forceRysncPause = true;
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      zeffector.game = c.game;
      zeffector.collider.world = zeffector.world;
      zeffector.collider.Move(target);
      if (theSpell.spellEnum == SpellEnum.Vortex)
      {
        zeffector.variable = (double) c.transformscale > 0.0 ? 1 : -1;
        if (c.game.isClient && zeffector.variable > 0)
          zeffector.transform.localScale = new Vector3(-1f, 1f, 1f);
      }
      zeffector.whoSummoned = c;
      c.effectors.Add(zeffector);
      if (!c.game.isClient || c.game.resyncing)
        return;
      ZSpell.CameraFollow(target);
    }
  }

  public static ZEffector FireAuraOfDecay(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    bool returnWithoutActivating = false)
  {
    c.game.forceRysncPause = true;
    ZEffector e = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    e.game = c.game;
    e.collider.world = e.world;
    e.collider.Move(target);
    e.whoSummoned = c;
    c.effectors.Add(e);
    e.active = false;
    if (returnWithoutActivating)
      return e;
    ZSpell.FinishFireingAura(c, e, target);
    return e;
  }

  public static void FinishFireingAura(ZCreature c, ZEffector e, MyLocation target)
  {
    e.TurnPassed(c.effectors.Count - 1, false, false);
    if (ZEffector.InSanctuary(c.game.world, target))
    {
      e?.Die(c.effectors.Count - 1, false, false);
      if (c.game.isClient)
        ZEffector.SpawnVineExplosion((Vector3) target.ToSinglePrecision());
    }
    if (!c.game.isClient || c.game.resyncing)
      return;
    ZSpell.CameraFollow(target);
  }

  public static void FireDeathAndDecay(Spell theSpell, ZCreature c)
  {
    ZEffector effector = c.followingColliders.Find((Predicate<ZMyCollider>) (x => (ZComponent) x.effector != (object) null && x.effector.type == EffectorType.Lich_Aura_of_decay))?.effector;
    if ((ZComponent) effector != (object) null)
    {
      effector.MaxTurnsAlive = 1000000;
      effector.TurnPassed(c.effectors.Count - 1, false, false);
      if (!c.game.isClient || c.game.resyncing)
        return;
      ZSpell.CameraFollow(c.position);
    }
    else
    {
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) c.position.ToSinglePrecision(), Quaternion.identity, c.transform);
      zeffector.game = c.game;
      zeffector.whoSummoned = c;
      zeffector.followParent = true;
      zeffector.collider.world = zeffector.world;
      zeffector.collider.Move(c.position);
      c.effectors.Add(zeffector);
      c.followingColliders.Add(zeffector.collider);
      zeffector.active = false;
      c.auraOfDecay = zeffector;
      zeffector.TurnPassed(c.effectors.Count - 1, false, false);
      c.game.forceRysncPause = true;
      if (c.game.isClient && !c.game.resyncing)
        ZSpell.CameraFollow(c.position);
      if (!c.game.isClient || (double) c.scale == 1.0)
        return;
      float num = 1f / c.scale;
      zeffector.transform.localScale = new Vector3(num, num, num);
    }
  }

  public static void FireLichdom(Spell theSpell, ZCreature c)
  {
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) c.position.ToSinglePrecision(), Quaternion.identity, c.transform);
    zeffector.game = c.game;
    c.TurnUndead();
    zeffector.whoSummoned = c;
    zeffector.followParent = true;
    zeffector.collider.world = zeffector.world;
    zeffector.collider.Move(c.position);
    c.effectors.Add(zeffector);
    c.followingColliders.Add(zeffector.collider);
    zeffector.active = false;
    c.auraOfDecay = zeffector;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
    c.game.forceRysncPause = true;
    if (c.game.isClient && !c.game.resyncing)
      ZSpell.CameraFollow(c.position);
    if (!c.game.isClient || (double) c.scale == 1.0)
      return;
    float num = 1f / c.scale;
    zeffector.transform.localScale = new Vector3(num, num, num);
  }

  public static void FireWerewolf(Spell theSpell, ZCreature c)
  {
    if (c.TrueSize != -1 || c.race == CreatureRace.Undead)
      return;
    c.TrueSize = c.radius;
    c.effectors.Add(new ZEffector()
    {
      game = c.game,
      active = false,
      whoSummoned = c,
      type = EffectorType.Bear,
      MaxTurnsAlive = 10
    });
    c.race = CreatureRace.Bear;
    c.type = CreatureType.Bear;
    c.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Swipe)));
    c.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Rampage)));
    c.SetRadius(25);
    c.UpdateHealthTxt();
    c.SetPosition(new MyLocation(c.position.x, c.position.y + (c.radius - c.TrueSize)));
    c.game.map.ServerBitBlt(36, (int) c.position.x, (int) c.position.y);
    c.HighJumpData.y += 2;
    c.LongJumpData.x += 2;
    c.game.CreatureMoveSurroundings(c.position, c.radius + 5);
    ZSpell.CreateWerewolfObj(theSpell, c);
  }

  public static void CreateWerewolfObj(Spell theSpell, ZCreature c)
  {
    if (!c.game.isClient || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null))
      return;
    int childCount = c.transform.GetChild(0).childCount;
    for (int index = 2; index < childCount; ++index)
      c.transform.GetChild(0).GetChild(index).gameObject.SetActive(false);
    c.werewolfObj = ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.position.ToSinglePrecision(), Quaternion.identity, c.transform);
    c.animator = c.werewolfObj.GetComponent<IAnimator>();
    c.werewolfObj.GetComponent<WerewolfObject>().Init(c.daOriginalTrueParent ?? c.parent, c, c.daOriginalTrueParent?.settingsPlayer ?? c.parent.settingsPlayer, c.daOriginalTrueParent?.name ?? c.parent.name);
    ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    c.werewolfObj.transform.localScale = new Vector3(1f / c.scale, 1f / c.scale, 1f);
    c.werewolfObj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
  }

  public static void FireInvulnerablityShield(Spell theSpell, ZCreature c)
  {
    c.invulnerable = 3;
    if (!c.game.isClient)
      return;
    ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position, Quaternion.identity, c.transform).GetComponent<ParticleInvulnerablity>().c = c.clientObj;
  }

  public static void FireGlide(Spell theSpell, ZCreature c)
  {
    if (c.shiningPower || c.flying || c.gliding)
      return;
    c.gliding = true;
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) (c.position.ToSinglePrecision() - new Vector2(0.0f, (float) (c.radius - 2))), Quaternion.identity, c.transform);
    zeffector.game = c.game;
    zeffector.followParent = true;
    zeffector.whoSummoned = c;
    c.effectors.Add(zeffector);
  }

  public static void WhenPigsFly(Spell theSpell, ZCreature c)
  {
    if (c.shiningPower || c.phantom)
      return;
    c.flightSpell = theSpell.spellEnum;
    c.tempFlight = false;
    c.removeFlight = false;
    c.permenantFlight = true;
    if (c.flying)
    {
      c.RemoveFlight();
    }
    else
    {
      c.flying = true;
      c.flightObj = ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position + ((double) c.transformscale > 0.0 ? new Vector3(-19.1f, -2.3f) : new Vector3(19.1f, -2.3f)) * c.scale, Quaternion.identity, c.transform);
      if (c.game.isClient && !c.game.resyncing)
        ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    }
    if (c.entangledOrGravity || c.isMoving)
      return;
    c.velocity.x = (FixedInt) 1;
    c.velocity.y = (FixedInt) 6;
    c.moving = c.game.ongoing.RunCoroutine(c.FlyMove());
  }

  public static void FireFlight(Spell theSpell, ZCreature c, bool shiningPower = false)
  {
    if (c.shiningPower || c.phantom)
      return;
    c.flightSpell = theSpell.spellEnum;
    c.tempFlight = theSpell.spellEnum == SpellEnum.Whistling_Winds || theSpell.spellEnum == SpellEnum.Vampire_Bat;
    c.removeFlight = false;
    c.permenantFlight = true;
    if (c.flying)
      c.RemoveFlight();
    else if (!shiningPower && theSpell.spellEnum != SpellEnum.Vampire_Bat && ZEffector.InSanctuary(c.game.world, c.position))
    {
      ZEffector.SpawnVineExplosion(c.transform.position);
    }
    else
    {
      c.flying = true;
      if (shiningPower)
      {
        c.shiningPower = true;
        c.flightObj = ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position + ((double) c.transformscale > 0.0 ? new Vector3(-19.1f, -2.3f) : new Vector3(19.1f, -2.3f)) * c.scale, Quaternion.identity, c.transform);
        if (c.game.isClient && !c.game.resyncing)
          ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
      }
      else if (theSpell.spellEnum == SpellEnum.Vampire_Bat && c.type == CreatureType.Vampire)
      {
        c.radius = 16;
        c.collider.SetRadius = 16;
        c.zb = MapGenerator.getOutlineArray(c.radius);
        c.game.CreatureMoveSurroundings(c.position, 24);
        if (c.game.isClient)
        {
          c.animator = c.transform.GetChild(0).GetComponent<IAnimator>();
          c.transform.GetChild(0).gameObject.SetActive(true);
          float num = c.BGScale();
          c.bg.transform.localScale = new Vector3(num, num, 1f);
          for (int index = 1; index < 7; ++index)
            c.transform.GetChild(index).gameObject.SetActive(false);
          ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
        }
      }
      else if ((UnityEngine.Object) theSpell.toSummon != (UnityEngine.Object) null)
      {
        c.permenantFlight = false;
        c.flightObj = ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position - new Vector3(0.0f, (float) (c.radius - 2)), Quaternion.identity, c.transform);
      }
      if (c.entangledOrGravity || c.isMoving)
        return;
      c.velocity.x = (FixedInt) 1;
      c.velocity.y = (FixedInt) 6;
      c.moving = c.game.ongoing.RunCoroutine(c.FlyMove());
    }
  }

  public static void ClientFireFlight(Spell theSpell, ZCreature c)
  {
    if (c.shiningPower)
      c.flightObj = UnityEngine.Object.Instantiate<GameObject>(Inert.GetSpell(SpellEnum.Shining_Power).toSummon, c.transform.position + ((double) c.transformscale > 0.0 ? new Vector3(-19.1f, -2.3f) : new Vector3(19.1f, -2.3f)) * c.scale, Quaternion.identity, c.transform);
    else if (c.phantom)
    {
      if (!c.game.isClient)
        return;
      ColorLerp colorLerp = c.gameObject.AddComponent<ColorLerp>();
      colorLerp.rends = ((IEnumerable<SpriteRenderer>) c.gameObject.GetComponentsInChildren<SpriteRenderer>()).ToList<SpriteRenderer>();
      for (int index = colorLerp.rends.Count - 1; index >= 0; --index)
      {
        if ((UnityEngine.Object) colorLerp.rends[index].transform?.parent?.GetComponent<Effector>() != (UnityEngine.Object) null)
          colorLerp.rends.RemoveAt(index);
      }
      for (int index = colorLerp.rends.Count - 1; index >= 0; --index)
      {
        if (colorLerp.rends[index].gameObject.layer == 14)
          colorLerp.rends.RemoveAt(index);
      }
    }
    else if (c.type == CreatureType.Vampire && c.flightSpell == SpellEnum.Vampire_Bat)
    {
      if (!c.game.isClient)
        return;
      c.animator = c.transform.GetChild(0).GetComponent<IAnimator>();
      c.transform.GetChild(0).gameObject.SetActive(true);
      float num = c.BGScale();
      c.bg.transform.localScale = new Vector3(num, num, 1f);
      for (int index = 1; index < 7; ++index)
        c.transform.GetChild(index).gameObject.SetActive(false);
    }
    else
    {
      if (!((UnityEngine.Object) c.flightObj == (UnityEngine.Object) null) || !((UnityEngine.Object) theSpell != (UnityEngine.Object) null) || !((UnityEngine.Object) theSpell.toSummon != (UnityEngine.Object) null))
        return;
      c.flightObj = UnityEngine.Object.Instantiate<GameObject>(theSpell.toSummon, c.transform.position - new Vector3(0.0f, (float) (c.radius - 2)), Quaternion.identity, c.transform);
    }
  }

  public static void FireTower(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    if ((ZComponent) c.tower != (object) null)
      c.DestroyTower();
    if (c.game.isClient && !c.game.resyncing)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
    c.game.forceRysncPause = true;
    MyLocation myLocation = new MyLocation(0, 18 - c.radius);
    Tower component = theSpell.toSummon.GetComponent<Tower>();
    if (component.type == TowerType.Cosmos)
    {
      c.SetPosition(new MyLocation(c.position.x, c.position.y - 5));
      pos.y -= 5;
    }
    ZTower t = ZTower.Create(c.game, component, (Vector3) (c.position.ToSinglePrecision() - myLocation.ToSinglePrecision()), Quaternion.identity, c.game.GetMapTransform());
    t.firstCast = c.parent.GetSpellCast(theSpell.spellEnum) < 2;
    t.creature = c;
    c.tower = t;
    t.collider.world = t.world;
    t.position = c.position + myLocation;
    t.collider.Initialize(pos, c.game.world);
    t.originX = (int) pos.x;
    if (c.parent.towerHealth[(int) t.type] != 0)
    {
      t.MaxHealth = c.parent.towerHealth[(int) t.type];
      if (t.Health > t.MaxHealth)
        t.Health = t.MaxHealth;
    }
    c.map.ServerBitBlt((int) t.cutout, (int) c.position.x, (int) c.position.y + t.cutoutOffsetY - (int) myLocation.y);
    c.collider.Disable(false);
    t.SetPosition(t.position);
    ZSpell.TowerApplyEffectors(false, t, c);
    List<ZMyCollider> zmyColliderList1 = c.game.world.OverlapCircleAll((Point) (c.position + new MyLocation(0, t.cutoutOffsetY)), ExplosionSize.GetRadius(t.cutout), (ZMyCollider) null, 256);
    for (int index = 0; index < zmyColliderList1.Count; ++index)
    {
      ZCreature creature = zmyColliderList1[index].creature;
      if (!ZComponent.IsNull((ZComponent) creature) && creature.GetType() == typeof (ZCreatureThorn))
        creature.ApplyDamage(theSpell.spellEnum, DamageType.None, 1, c, c.game.turn);
    }
    List<ZMyCollider> zmyColliderList2 = c.game.world.OverlapColliderAll(t.collider, 43520);
    for (int index = 0; index < zmyColliderList2.Count; ++index)
    {
      ZEffector effector = zmyColliderList2[index].effector;
      if (!ZComponent.IsNull((ZComponent) effector))
        effector.EffectCreature(c);
    }
    c.game.CreatureMoveSurroundings(c.position, t.radius + 20, forceLeaf: true);
  }

  public static void TowerApplyEffectors(bool OnDeserialization, ZTower t, ZCreature c)
  {
    if (c.game.isClient)
    {
      if ((UnityEngine.Object) c.beard != (UnityEngine.Object) null)
      {
        c.head.sortingOrder += 9;
        c.beard.sortingOrder += 9;
        c.beard2.sortingOrder += 9;
        c.beard3.sortingOrder += 9;
        c.hat.sortingOrder += 9;
        c.mouth.sortingOrder += 9;
      }
      c.UpdateHealthTxt();
    }
    if ((UnityEngine.Object) c.overheadCanvas != (UnityEngine.Object) null)
      ((RectTransform) c.overheadCanvas.transform).anchoredPosition = c.OverHeadOffset.ToSinglePrecision() * (1f / c.scale);
    if (t.type == TowerType.Darkness)
    {
      ZEffector effector = t.effector;
      effector.game = c.game;
      effector.whoSummoned = c;
      effector.doNotCreateObjectOnResync = true;
      effector.followParent = true;
      t.secondCollider = effector.collider;
      effector.collider.world = effector.world;
      c.followingColliders.Add(effector.collider);
      effector.collider.Move(c.position);
      effector.active = false;
      c.effectors.Add(effector);
      c.auraOfDecay = effector;
      if (OnDeserialization)
        return;
      effector.TurnPassed(c.destroyableEffectors.Count - 1, false, false);
    }
    else if (t.type == TowerType.Nature)
    {
      ZEffector effector = t.effector;
      effector.game = c.game;
      effector.whoSummoned = c;
      effector.doNotCreateObjectOnResync = true;
      effector.followParent = true;
      t.secondCollider = effector.collider;
      effector.collider.world = effector.world;
      c.followingColliders.Add(effector.collider);
      effector.collider.Move(c.position);
      t.sanctuaryEffector = effector;
      c.parent.sactuaryShieldsRemoved = 0;
      if (OnDeserialization)
        return;
      effector.CheckSanctuary();
      if (c.parent.sactuaryShieldsRemoved < 5 || c.game.isClient)
        return;
      Achievements.Set(Achievement.Sanctify_Yourself, c.parent.account, c.game);
    }
    else if (t.type == TowerType.Holiday)
    {
      ZEffector zeffector = ZEffector.Create(c.game, Inert.Instance.frostGiantBlizzard.GetComponent<Effector>(), new Vector3((float) c.position.x.ToInt(), (float) (c.map.Height + 300)), Quaternion.identity, t.transform);
      zeffector.game = c.game;
      zeffector.whoSummoned = c;
      zeffector.doNotCreateObjectOnResync = true;
      c.effectors.Add(zeffector);
      zeffector.active = false;
      t.effector = zeffector;
    }
    else
    {
      if (t.type != TowerType.Arcane || c.familiarLevelArcane < 3 || OnDeserialization)
        return;
      if (c.familiarLevelArcane == 5)
        t.Health += 50;
      else
        t.Health += 25;
      c.UpdateHealthTxt();
    }
  }

  public static void Offset(ZCreature c, Transform t, FixedInt y)
  {
    MyLocation position = c.position;
    position.y += y;
    if ((UnityEngine.Object) t != (UnityEngine.Object) null)
      t.position = (Vector3) position.ToSinglePrecision();
    c.position = position;
  }

  public static void SyncSpellsWithParent(ZCreature c, ZCreature sum, bool reSaveZSpells = false)
  {
    if (reSaveZSpells)
    {
      for (int index = 0; index < sum.spells.Count; ++index)
      {
        if (sum.spells[index] != null && sum.spells[index].syncWithParent)
          sum.spells[index] = new SpellSlot(sum.spells[index]);
      }
    }
    bool flag1 = c.game.gameFacts.GetStyle().HasStyle(GameStyle.Random_Spells) || c.parent != null && c.parent.MinionMaster;
    for (int index = sum.spells.Count - 1; index >= 0; --index)
    {
      if (sum.spells[index].syncWithParent)
      {
        if (sum.spells[index].disabledturn == 1000000)
          sum.spells[index].disabledturn = -1;
        SpellEnum spellEnum1 = sum.spells[index].spell.spellEnum;
        bool flag2 = false;
        if (flag1)
          flag2 = true;
        switch (spellEnum1)
        {
          case SpellEnum.Protection_Shield:
            if (!flag1)
            {
              foreach (SpellSlot spell in c.spells)
              {
                if (spell.spell.spellEnum == spellEnum1)
                {
                  flag2 = true;
                  break;
                }
              }
            }
            if (!flag2)
            {
              sum.spells[index].disabledturn = 1000000;
              continue;
            }
            continue;
          case SpellEnum.Shock_Shield:
            SpellEnum spellEnum2 = SpellEnum.Storm_Shield;
            if (!flag1)
            {
              foreach (SpellSlot spell in c.spells)
              {
                if (spell.spell.spellEnum == spellEnum2)
                {
                  flag2 = true;
                  break;
                }
              }
            }
            if (!flag2)
            {
              sum.spells[index].disabledturn = 1000000;
              continue;
            }
            continue;
          case SpellEnum.Spirit_Hurricane:
            spellEnum1 = SpellEnum.Hurricane;
            break;
        }
        bool flag3 = false;
        foreach (SpellSlot spell in c.spells)
        {
          if (spell.spell.spellEnum == spellEnum1)
          {
            flag3 = true;
            if (spellEnum1 != SpellEnum.Hurricane && spellEnum1 != SpellEnum.Sphere_of_Healing && (spell.MaxUses > 0 || spell.RechargeTime > 1))
            {
              sum.spells[index] = spell;
              sum.spells[index].syncWithParent = true;
              break;
            }
            sum.spells[index].SetTurnFired = spell.LastTurnFired;
            break;
          }
        }
        if (!flag3)
        {
          if (flag1)
          {
            foreach (SpellSlot randomSyncedZspell in c.parent.randomSyncedZSpells)
            {
              if (randomSyncedZspell.spell.spellEnum == spellEnum1)
              {
                flag3 = true;
                if (spellEnum1 != SpellEnum.Hurricane && (randomSyncedZspell.MaxUses > 0 || randomSyncedZspell.RechargeTime > 1))
                {
                  sum.spells[index] = randomSyncedZspell;
                  sum.spells[index].syncWithParent = true;
                  break;
                }
                sum.spells[index].SetTurnFired = randomSyncedZspell.LastTurnFired;
                break;
              }
            }
            if (!flag3)
            {
              SpellSlot spellSlot = new SpellSlot(sum.spells[index].spell);
              spellSlot.MaxUses = sum.spells[index].spell.MaxUses;
              spellSlot.RechargeTime = sum.spells[index].spell.RechargeTime;
              spellSlot.EndsTurn = sum.spells[index].spell.EndsTurn;
              spellSlot.SetTurnFired = sum.spells[index].LastTurnFired;
              sum.spells[index] = spellSlot;
              sum.spells[index].syncWithParent = true;
              c.parent.randomSyncedZSpells.Add(spellSlot);
            }
          }
          else
            sum.spells[index].disabledturn = 1000000;
        }
      }
    }
  }

  public static void HatchSteamDragon(ZCreature c, MyLocation target)
  {
    Spell spell = Inert.Instance.spells["Steam Dragon"];
    target.y += 20;
    ZGame game = c.game;
    ZCreature c1 = c;
    MyLocation target1 = target;
    ZCreature zcreature = ZSpell.FireSummon(spell, game, c1, target1, 1);
    zcreature.health = 50;
    zcreature.UpdateHealthTxt();
  }

  public static void FireHatch(Spell theSpell, ZCreature c)
  {
    if (!c.isPawn)
      return;
    Spell theSpell1 = theSpell;
    if (c.health >= 150)
    {
      switch (c.game.RandomInt(0, 3))
      {
        case 0:
          theSpell1 = Inert.Instance.spells["Summon Flame Dragon"];
          break;
        case 1:
          theSpell1 = Inert.Instance.spells["Summon Storm Dragon"];
          break;
        case 2:
          theSpell1 = Inert.Instance.spells["Frost Dragon"];
          break;
      }
    }
    if (c.health < 25)
      theSpell1 = Inert.Instance.spells["Dragon Hatchling"];
    MyLocation target = c.position + new MyLocation(0, 20);
    ZCreature zcreature = ZSpell.FireSummon(theSpell1, c.game, c, target, 1);
    zcreature.health = c.health;
    zcreature.maxHealth = zcreature.health;
    zcreature.UpdateHealthTxt();
    int index1 = c.parent.controlled.FindIndex((Predicate<ZCreature>) (m => (ZComponent) m == (object) c));
    if (index1 > -1)
    {
      int index2 = c.parent.controlled.Count - 1;
      c.parent.controlled[index1] = zcreature;
      c.parent.controlled[index2] = c;
      if (c.game.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && Player.Instance.person.yourTurn)
        Player.Instance.selected = zcreature;
    }
    if (c.parent.controlled.Count > 30 && !c.game.isClient)
      Achievements.Set(Achievement.Turing_Machine, c.parent.connection.player.account, c.game);
    c.OnDeath(true);
  }

  public static void TryMountOnSummon(ZCreature sum, ZCreature c)
  {
    if (sum.spellEnum != SpellEnum.Summon_Flame_Dragon && sum.spellEnum != SpellEnum.Frost_Dragon && sum.spellEnum != SpellEnum.Summon_Storm_Dragon && sum.spellEnum != SpellEnum.Steam_Dragon && sum.spellEnum != SpellEnum.Arcane_Dragon && sum.spellEnum != SpellEnum.Corrupt_Dragon || !((ZComponent) sum != (object) null) || sum.isDead || !((ZComponent) c != (object) null) || c.isDead || !c.canMount || !sum.mountable || !c.map.TryMount(sum, c, true))
      return;
    MyLocation position = c.position;
    c.Demount();
    c.OnMount(sum);
    c.RiderMoveToPosition = sum.position + Global.GetMountOffset(sum.transformscale, sum.type);
    c.game.CreatureMoveSurroundings(position, c.radius);
  }

  public static ZCreature FireSummon(
    Spell theSpell,
    ZGame game,
    ZCreature c,
    MyLocation target,
    int amountOverrideIndex = -1,
    bool stillSync = false,
    ZPerson overrideZPerson = null)
  {
    int c1 = amountOverrideIndex > -1 ? 1 : theSpell.amount;
    if ((ZComponent) c != (object) null && amountOverrideIndex == -1 && c.parent.GetMinionCount() > c.parent.GetMaxMinions() - c1)
      return (ZCreature) null;
    if ((UnityEngine.Object) theSpell.toSummon == (UnityEngine.Object) null)
      return (ZCreature) null;
    game.forceRysncPause = true;
    if (amountOverrideIndex == -1 && c != null)
      c.parent?.awards.Summoned(c1);
    List<ZCreature> zcreatureList = new List<ZCreature>();
    MyLocation zero = MyLocation.zero;
    for (int index = 0; index < c1; ++index)
    {
      MyLocation pos = target;
      switch (c1)
      {
        case 1:
          if ((theSpell.spellEnum == SpellEnum.Summon_Flame_Dragon || theSpell.spellEnum == SpellEnum.Summon_Storm_Dragon || theSpell.spellEnum == SpellEnum.Frost_Dragon || theSpell.spellEnum == SpellEnum.Steam_Dragon) && (ZComponent) c != (object) null)
            ++c.parent.awards.dragonsSummoned;
          Vector3 singlePrecision = (Vector3) pos.ToSinglePrecision();
          if ((UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null && game.isClient && !game.resyncing)
          {
            if (theSpell.spellEnum == SpellEnum.Summon_Flame_Dragon || theSpell.spellEnum == SpellEnum.Summon_Storm_Dragon || theSpell.spellEnum == SpellEnum.Frost_Dragon || theSpell.spellEnum == SpellEnum.Steam_Dragon || theSpell.spellEnum == SpellEnum.Summon_Water_Lord)
            {
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision - new Vector3((float) theSpell.radius, 0.0f, 0.0f), Quaternion.identity);
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision + new Vector3((float) theSpell.radius, 0.0f, 0.0f), Quaternion.identity);
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision - new Vector3(0.0f, (float) theSpell.radius, 0.0f), Quaternion.identity);
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision + new Vector3(0.0f, (float) theSpell.radius, 0.0f), Quaternion.identity);
            }
            else if (theSpell.spellEnum == SpellEnum.Summon_Dark_Knight || theSpell.spellEnum == SpellEnum.Summon_Paladin || theSpell.spellEnum == SpellEnum.Summon_Man_Trap)
            {
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision - new Vector3((float) theSpell.radius * 0.66f, 0.0f, 0.0f), Quaternion.identity);
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision + new Vector3((float) theSpell.radius * 0.66f, 0.0f, 0.0f), Quaternion.identity);
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision + new Vector3(0.0f, (float) theSpell.radius * 0.66f, 0.0f), Quaternion.identity);
            }
            else
              ZComponent.Instantiate<GameObject>(theSpell.explosion, singlePrecision, Quaternion.identity);
          }
          ZCreature creature = ZCreatureCreate.CreateCreature(c?.parent, theSpell.toSummon.GetComponent<Creature>(), (Vector2) singlePrecision, Quaternion.identity, game.GetMapTransform());
          if ((ZComponent) c == (object) null)
          {
            c = creature;
            c.parent = overrideZPerson ?? game.players[game.players.Count - 1];
          }
          creature.game = game;
          creature.parent = c.parent;
          creature.spellEnum = theSpell.spellEnum;
          creature.collider.world = creature.world;
          creature.collider.creature = creature;
          creature.collider.Move(pos);
          creature.position = pos;
          if ((ZComponent) c != (object) null)
          {
            creature.maxHealth += 2 * c.familiarLevelDruidism;
            creature.health += 2 * c.familiarLevelDruidism;
            if (theSpell.spellEnum == SpellEnum.Summon_Monarchs)
            {
              creature.health -= 10;
              creature.maxHealth -= 10;
            }
          }
          if ((ZComponent) c != (object) null && amountOverrideIndex == -1 | stillSync)
            ZSpell.SyncSpellsWithParent(c.parent.first(), creature);
          ZSpell.ApplyEffectors(game, creature, target, amountOverrideIndex, theSpell, c);
          if ((ZComponent) c != (object) null && c.waterWalking)
            creature.waterWalking = true;
          creature.team = c.team;
          if (theSpell.spellEnum != SpellEnum.Summon_Tutorial_Target && (creature.spellEnum != SpellEnum.Summon_Bees || c.GetType() != typeof (ZCreatureBeehive)))
            c.parent.controlled.Add(creature);
          else if (creature.spellEnum == SpellEnum.Summon_Bees && c.GetType() == typeof (ZCreatureBeehive))
            ((ZCreatureBeehive) c).bees.Add(creature);
          else
            c.game._uncontrolledPlayer.controlled.Add(creature);
          zcreatureList.Add(creature);
          if (creature.FullArcane && (ZComponent) c != (object) null && !c.FullArcane)
          {
            creature.FullArcane = false;
            creature.TurnUndead();
            continue;
          }
          continue;
        case 2:
          pos.x += index == 0 ? (FixedInt) -theSpell.radius / FixedInt.Create(1572864L) : (FixedInt) theSpell.radius / FixedInt.Create(1572864L);
          goto case 1;
        case 3:
          if (index != 1)
          {
            pos.x += index == 0 ? (FixedInt) -theSpell.radius / FixedInt.Create(1572864L) : (FixedInt) theSpell.radius / FixedInt.Create(1572864L);
            goto case 1;
          }
          else
          {
            pos.y += theSpell.radius / 4;
            goto case 1;
          }
        default:
          Debug.LogError((object) ("Invalid summon amount: " + (object) c1));
          goto case 1;
      }
    }
    for (int index1 = 0; index1 < zcreatureList.Count; ++index1)
    {
      ZCreature zcreature = zcreatureList[index1];
      if (zcreature.game.isClient)
        zcreature.UpdateHealthTxt();
      zcreature.CheckEffectorsOnMove();
      if (zcreature.flying)
      {
        List<ZMyCollider> list1 = game.world.OverlapCircleAll((Point) zcreature.position, zcreature.radius, (ZMyCollider) null, 2560);
        for (int index2 = 0; index2 < list1.Count; ++index2)
        {
          ZEffector effector = list1[index2].effector;
          if (!ZComponent.IsNull((ZComponent) effector))
          {
            effector.game = game;
            effector.EffectCreature(zcreature);
          }
        }
        game.world.listPool.ReturnList(list1);
        if (zcreature.mountable && ZComponent.IsNull((ZComponent) zcreature.rider))
        {
          List<ZMyCollider> list2 = game.world.OverlapCircleAll((Point) zcreature.position, zcreature.radius + 1, (ZMyCollider) null, 256);
          for (int index3 = 0; index3 < list2.Count; ++index3)
          {
            if (!ZComponent.IsNull((ZComponent) list2[index3].creature) && !list2[index3].creature.isDead)
              game.map.TryMount(zcreature, list2[index3].creature);
          }
          game.world.listPool.ReturnList(list2);
        }
      }
      else
        zcreature.Fall();
      if (amountOverrideIndex > -1)
      {
        if (game.isClient)
          c.parent.panelPlayer?.SetSummons(c.parent);
        return zcreature;
      }
    }
    if (game.isClient)
      c.parent?.panelPlayer?.SetSummons(c.parent);
    return zcreatureList.Count <= 0 ? (ZCreature) null : zcreatureList[0];
  }

  public static void ApplyEffectors(
    ZGame game,
    ZCreature sum,
    MyLocation target,
    int amountOverrideIndex = -1,
    Spell theSpell = null,
    ZCreature cre = null,
    bool isDeserialization = false)
  {
    if (sum.phantom)
    {
      sum.effectors.Add(new ZEffector()
      {
        game = sum.game,
        whoSummoned = sum,
        active = false,
        type = EffectorType.Phantom,
        MaxTurnsAlive = 100000
      });
      if (sum.game.isClient)
      {
        ColorLerp component = sum.gameObject.GetComponent<ColorLerp>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component._creature = sum;
      }
      sum.collider.gameObjectLayer = 8;
      sum.collider.layer = 256;
    }
    if (sum.spellEnum == SpellEnum.Summon_Frost_Giant)
    {
      ZEffector zeffector = ZEffector.Create(game, Inert.Instance.frostGiantBlizzard.GetComponent<Effector>(), new Vector3((float) target.x.ToInt(), (float) (sum.map.Height + 300)), Quaternion.identity, game.GetMapTransform());
      zeffector.game = game;
      zeffector.whoSummoned = sum;
      zeffector.doNotCreateObjectOnResync = true;
      sum.effectors.Add(zeffector);
      zeffector.active = false;
      if (!game.isClient)
        ZComponent.Destroy<GameObject>(zeffector.transform.GetChild(0).gameObject);
    }
    else if (sum.spellEnum == SpellEnum.Summon_Swarm || sum.spellEnum == SpellEnum.Summon_Bees)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      auraOfDecay.game = game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      auraOfDecay.followParent = true;
      auraOfDecay.collider.world = auraOfDecay.world;
      sum.followingColliders.Add(auraOfDecay.collider);
      auraOfDecay.collider.Move(sum.position);
      sum.effectors.Add(auraOfDecay);
      auraOfDecay.active = false;
      if ((UnityEngine.Object) theSpell != (UnityEngine.Object) null)
        auraOfDecay.TurnPassed(sum.effectors.Count - 1, false, false);
    }
    else if (sum.spellEnum == SpellEnum.Summon_Boar || sum.spellEnum == SpellEnum.Summon_Alpha_Wolf || sum.spellEnum == SpellEnum.Pack_Mentality || sum.spellEnum == SpellEnum.Pack_Leader)
    {
      sum.effectors.Add(new ZEffector()
      {
        game = game,
        whoSummoned = sum,
        doNotCreateObjectOnResync = true,
        MaxTurnsAlive = 100000,
        type = EffectorType.Boar_Charge,
        active = false,
        followParent = true
      });
      SpellSlot spellSlot = sum.GetSpellSlot(SpellEnum.Pack_Mentality);
      if (spellSlot != null)
        spellSlot.SetTurnFired = sum.parent.localTurn;
    }
    else if (sum.spellEnum == SpellEnum.Summon_Tiger)
    {
      if (sum.familiarLevelDruidism > 0)
        sum.spells.Add(new SpellSlot(Inert.GetSpell(SpellEnum.Compete)));
    }
    else if (sum.spellEnum == SpellEnum.Summon_Elves)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      sum.auraOfDecay = (ZEffector) null;
      auraOfDecay.game = game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      sum.effectors.Add(auraOfDecay);
      game.elfEffectors.Add(auraOfDecay);
      auraOfDecay.active = false;
    }
    else if (sum.spellEnum == SpellEnum.Summon_Dryad)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      sum.auraOfDecay = (ZEffector) null;
      auraOfDecay.game = sum.game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      auraOfDecay.followParent = true;
      auraOfDecay.collider.world = auraOfDecay.world;
      auraOfDecay.collider.effector = auraOfDecay;
      sum.followingColliders.Add(auraOfDecay.collider);
      auraOfDecay.collider.Move(sum.position);
      sum.parent.sactuaryShieldsRemoved = 0;
      sum.effectors.Add(auraOfDecay);
      if ((UnityEngine.Object) theSpell != (UnityEngine.Object) null)
      {
        auraOfDecay.CheckSanctuary();
        if (sum.parent.sactuaryShieldsRemoved >= 5 && !sum.game.isClient)
          Achievements.Set(Achievement.Sanctify_Yourself, sum.parent.account, sum.game);
      }
      if (sum.game.isClient)
        sum.gameObject.GetComponent<DryadTextureChange>().creature = sum.clientObj;
    }
    else if (sum.spellEnum == SpellEnum.Summon_Water_Trolls || sum.spellEnum == SpellEnum.Dark_Totem || sum.spellEnum == SpellEnum.Summon_Myth)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      sum.auraOfDecay = (ZEffector) null;
      auraOfDecay.game = game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      auraOfDecay.followParent = true;
      auraOfDecay.collider.world = auraOfDecay.world;
      sum.followingColliders.Add(auraOfDecay.collider);
      auraOfDecay.collider.Move(sum.position);
      sum.effectors.Add(auraOfDecay);
      if (sum.spellEnum == SpellEnum.Dark_Totem)
        auraOfDecay.active = false;
      else
        sum.stormShield = auraOfDecay;
      if (game.isClient)
      {
        AnimateTrollHead componentInChildren = sum.clientObj.GetComponentInChildren<AnimateTrollHead>();
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        {
          componentInChildren.effector = auraOfDecay;
        }
        else
        {
          AnimateMythEffector component = sum.clientObj.GetComponent<AnimateMythEffector>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.effector = auraOfDecay;
        }
      }
    }
    else if (sum.spellEnum == SpellEnum.Recall_Device)
    {
      if ((UnityEngine.Object) theSpell != (UnityEngine.Object) null)
        game.recallDevices.Add(sum);
    }
    else if (sum.spellEnum == SpellEnum.Summon_Dragon_Egg)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      sum.auraOfDecay = (ZEffector) null;
      auraOfDecay.game = game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      auraOfDecay.active = false;
      sum.effectors.Add(auraOfDecay);
    }
    else if (sum.spellEnum == SpellEnum.Summon_Blood_Bank)
      sum.parent.bloodBank = sum;
    else if (sum.spellEnum == SpellEnum.Monkey)
    {
      ZEffector auraOfDecay = sum.auraOfDecay;
      sum.auraOfDecay = (ZEffector) null;
      auraOfDecay.game = game;
      auraOfDecay.whoSummoned = sum;
      auraOfDecay.doNotCreateObjectOnResync = true;
      sum.effectors.Add(auraOfDecay);
      auraOfDecay.active = false;
      if ((UnityEngine.Object) theSpell != (UnityEngine.Object) null)
      {
        if (game.gameFacts.GetStyle().HasStyle(GameStyle.Zombie_Monkey))
          sum.TurnUndead();
        auraOfDecay.TurnPassed(0, false, false);
      }
    }
    else if (sum.spellEnum == SpellEnum.Summon_Imps && (UnityEngine.Object) theSpell != (UnityEngine.Object) null)
    {
      if ((ZComponent) sum.parent.first() != (object) null && sum.parent.first().FullArcane)
        sum.health = 100;
    }
    else if (sum.spellEnum == SpellEnum.Arcane_Dragon && (UnityEngine.Object) theSpell != (UnityEngine.Object) null)
      sum.FullArcane = true;
    else if (sum.spellEnum == SpellEnum.Summon_Reindeer && (UnityEngine.Object) theSpell != (UnityEngine.Object) null)
    {
      if (sum.familiarLevelSeasons == 5)
        sum.spells.Add(new SpellSlot(Inert.Instance.spells["Santa's Magic"])
        {
          MaxUses = 1
        });
    }
    else if ((sum.spellEnum == SpellEnum.Summon_Snowman || sum.spellEnum == SpellEnum.Summon_Snowman2) && (UnityEngine.Object) theSpell != (UnityEngine.Object) null)
    {
      if (sum.familiarLevelSeasons > 0)
      {
        sum.maxHealth += sum.familiarLevelSeasons * 5;
        sum.health = sum.maxHealth;
        sum.UpdateHealthTxt();
      }
    }
    else if (sum.spellEnum == SpellEnum.Summon_Kraken && (UnityEngine.Object) theSpell != (UnityEngine.Object) null)
    {
      sum.map.ServerBitBlt(22, (int) target.x, 0);
      ZSpell.DiveForce(theSpell, sum, ExplosionCutout.Three_Seventy_Five, new MyLocation(0, -1000), target, false);
    }
    else if (sum.spellEnum == SpellEnum.Summon_Gargoyle)
    {
      if (game.isClient)
        ((AnimatePlayer) sum.animator).creature = sum;
    }
    else if (sum.spellEnum == SpellEnum.Summon_Dwarf && game.originalSpellsOnly && sum.spells.Count > 2)
      sum.spells.RemoveAt(2);
    if (game.isClient)
    {
      if (!isDeserialization)
        ZSpell.Recolor(game, sum);
      if (sum.spellEnum != SpellEnum.Summon_Tutorial_Target)
        Inert.AddBG(sum);
    }
    if (game.isClient && sum.spellEnum != SpellEnum.Summon_Tutorial_Target && (sum.spellEnum != SpellEnum.Summon_Bees || (ZComponent) cre != (object) null && cre.type != CreatureType.Beehive))
      Inert.AddOverheadCanvas(sum.clientObj);
    if (!((UnityEngine.Object) HUD.instance != (UnityEngine.Object) null) || !HUD.instance.hideOverheadIcons)
      return;
    sum.overheadCanvas?.gameObject.SetActive(false);
  }

  public static void Recolor(ZGame game, ZCreature sum)
  {
    if (sum.spellEnum != SpellEnum.Summon_Imps && sum.spellEnum != SpellEnum.Little_Devil && sum.spellEnum != SpellEnum.Summon_Dwarf && sum.spellEnum != SpellEnum.Summon_Rock_Golem && sum.spellEnum != SpellEnum.Summon_Frost_Giant && sum.spellEnum != SpellEnum.Summon_Dark_Knight && sum.spellEnum != SpellEnum.Summon_Wraith && sum.spellEnum != SpellEnum.Summon_Elves && sum.spellEnum != SpellEnum.Summon_Man_Trap && sum.spellEnum != SpellEnum.Summon_Water_Trolls && sum.spellEnum != SpellEnum.Recall_Device && sum.spellEnum != SpellEnum.Monkey && sum.spellEnum != SpellEnum.Summon_Brine_Goblin && sum.spellEnum != SpellEnum.From_the_Depths && sum.spellEnum != SpellEnum.Summon_Mountain_Goat && sum.spellEnum != SpellEnum.Summon_Vampire && sum.spellEnum != SpellEnum.Summon_Tiger && sum.spellEnum != SpellEnum.Summon_Boar && sum.spellEnum != SpellEnum.Summon_Alpha_Wolf && sum.spellEnum != SpellEnum.Pack_Mentality && sum.spellEnum != SpellEnum.Pack_Leader && sum.spellEnum != SpellEnum.Summon_Will_o_the_Wisp)
      return;
    foreach (SpriteRenderer componentsInChild in sum.gameObject.GetComponentsInChildren<SpriteRenderer>())
    {
      if ((UnityEngine.Object) componentsInChild.sprite != (UnityEngine.Object) null && componentsInChild.sprite.texture.isReadable)
        ConfigurePlayer.ApplyOutfit(componentsInChild, sum.daOriginalTrueParent != null ? sum.daOriginalTrueParent.settingsPlayer : sum.parent.settingsPlayer);
    }
  }

  public static void ChangeSprites(ZCreature c, Sprite[] s)
  {
    AnimateDragon component = c.gameObject?.GetComponent<AnimateDragon>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    for (int index = 0; index < component.sprites.Length && index < s.Length; ++index)
      component.sprites[index] = s[index];
  }

  public static void FireRedo(Spell theSpell, ZCreature c, bool force = false)
  {
    foreach (ZPerson player in c.game.players)
    {
      if (player != null)
      {
        for (int index = 0; index < player.towerHealth.Length; ++index)
          player.towerHealth[index] = 0;
        for (int index = 0; index < player.minionBookTitans.Count; ++index)
          player.minionBookTitans[index].used = false;
        foreach (ZCreature z in player.controlled)
        {
          if (!ZComponent.IsNull((ZComponent) z))
          {
            foreach (SpellSlot spell in z.spells)
            {
              if (spell != null && (UnityEngine.Object) spell.spell != (UnityEngine.Object) null)
              {
                if (spell.spell.spellEnum != SpellEnum.Redo | force)
                {
                  spell.ResetUses();
                  spell.SetTurnFired = -1;
                }
                else if (spell.UsedUses == 0)
                  spell.IncreaseUses();
              }
            }
          }
        }
      }
    }
  }

  public static void FireBlackHole(Spell theSpell, ZCreature c, MyLocation target)
  {
    c.game.forceRysncPause = true;
    c.map.ServerBitBlt(9, (int) target.x, (int) target.y);
    if (!ZComponent.IsNull((ZComponent) c.game.blackhole))
    {
      c.game.blackhole.position = target;
      c.game.blackhole.whoSummoned = c;
      if ((UnityEngine.Object) c.game.blackhole.transform != (UnityEngine.Object) null)
        c.game.blackhole.transform.position = (Vector3) target.ToSinglePrecision();
    }
    else
    {
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      zeffector.game = c.game;
      c.game.blackhole = zeffector;
      c.game.blackhole.position = target;
      zeffector.whoSummoned = c;
    }
    if (!((ZComponent) c != (object) null) || c.game == null)
      return;
    c.game.CreatureMoveSurroundings(target, 120, forceLeaf: true);
  }

  public static void FireNaturesWrath(Spell theSpell, ZCreature c, MyLocation target)
  {
    for (int index = c.game.thorns.Count - 1; index >= 0; --index)
    {
      if (!ZComponent.IsNull((ZComponent) c.game.thorns[index]))
        c.game.thorns[index].ApplyDamage(SpellEnum.Nature_s_Wrath, DamageType.None, 1, (ZCreature) null, 0, (ISpellBridge) null, false);
    }
    c.game.forceRysncPause = true;
    c.game.thorns.Clear();
    MyLocation myLocation = new MyLocation(target.x, (FixedInt) c.map.Height);
    if (!ZComponent.IsNull((ZComponent) c.game.naturesWrath))
    {
      c.game.naturesWrath.position = myLocation;
      c.game.naturesWrath.spell.position = myLocation;
      c.game.naturesWrath.whoSummoned = c;
      c.game.naturesWrath.spell.Fall();
    }
    else
    {
      ZFlameWallSpell zflameWallSpell1 = ZSpell.Create(c.game, theSpell.toSummon.GetComponent<FlameWallSpell>(), new Vector3((float) target.x.ToInt(), (float) c.map.Height), Quaternion.identity, c.game.GetMapTransform());
      ZEffector effector = zflameWallSpell1.effector;
      effector.game = c.game;
      c.game.naturesWrath = effector;
      c.game.naturesWrath.position = new MyLocation(target.x, (FixedInt) c.map.Height);
      effector.whoSummoned = c;
      ZFlameWallSpell zflameWallSpell2 = zflameWallSpell1;
      zflameWallSpell2.colliderA.world = effector.world;
      effector.collider = zflameWallSpell2.colliderA;
      zflameWallSpell2.colliderA.Initialize(myLocation, c.game.world);
      zflameWallSpell2.SetPosition(myLocation);
      effector.spell.position = myLocation;
      effector.spell.game = c.game;
      effector.spell.Fall();
      if (c.game.isClient)
        return;
      ZComponent.Destroy<GameObject>(effector.transform.GetChild(1).gameObject);
      ZComponent.Destroy<GameObject>(effector.transform.GetChild(0).gameObject);
    }
  }

  public static void FireMinerMap(Spell theSpell, ZCreature c, MyLocation target)
  {
    ZGame game = c.game;
    if (!ZComponent.IsNull((ZComponent) c.game.dwarfMapEffector))
    {
      c.game.dwarfMapEffector.position = target;
      c.game.dwarfMapEffector.spell.position = target;
      c.game.dwarfMapEffector.collider.Move(target);
      if (!c.game.dwarfMapEffector.spell.ShouldSpellFall(true))
        return;
      c.game.dwarfMapEffector.spell.Fall();
    }
    else
    {
      ZFlameWallSpell zflameWallSpell = ZSpell.Create(c.game, theSpell.GetComponent<FlameWallSpell>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      ZEffector effector = zflameWallSpell.effector;
      effector.game = game;
      c.game.dwarfMapEffector = effector;
      effector.active = false;
      effector.collider.world = effector.world;
      effector.collider.Move(target);
      zflameWallSpell.colliderA.world = effector.world;
      zflameWallSpell.SetPosition(target);
      effector.spell.game = c.game;
      if (!effector.spell.ShouldSpellFall(true))
        return;
      effector.spell.Fall();
    }
  }

  public static void FireVolley(Spell theSpell, ZCreature c, MyLocation target)
  {
    ZGame game = c.game;
    if (!ZComponent.IsNull((ZComponent) c.game.targetEffector))
    {
      c.game.targetEffector.position = target;
      c.game.targetEffector.spell.position = target;
      c.game.targetEffector.collider.Move(target);
      if (!c.game.targetEffector.spell.ShouldSpellFall(true))
        return;
      c.game.targetEffector.spell.Fall();
    }
    else
    {
      ZFlameWallSpell zflameWallSpell = ZSpell.Create(c.game, theSpell.GetComponent<FlameWallSpell>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      ZEffector effector = zflameWallSpell.effector;
      effector.game = game;
      c.game.targetEffector = effector;
      effector.active = false;
      effector.collider.world = effector.world;
      effector.collider.Move(target);
      zflameWallSpell.colliderA.world = effector.world;
      zflameWallSpell.SetPosition(target);
      effector.spell.game = c.game;
      if (!effector.spell.ShouldSpellFall(true))
        return;
      effector.spell.Fall();
    }
  }

  public static void FireFlurry(Spell theSpell, ZCreature c, MyLocation target)
  {
    if (!ZComponent.IsNull((ZComponent) c.game.targetEffector))
    {
      c.game.targetEffector.DestroyNoExplosion();
      c.game.targetEffector = (ZEffector) null;
    }
    for (int index = c.game.elfEffectors.Count - 1; index >= 0; --index)
    {
      if (!ZComponent.IsNull((ZComponent) c.game.elfEffectors[index]))
        c.game.ongoing.RunSpell(ZSpell.IEArrows(c.game.elfEffectors[index].whoSummoned, Inert.Instance.Arrow, target, false));
      else
        c.game.elfEffectors.RemoveAt(index);
    }
    if (!((ZComponent) c != (object) null) || c.parent == null || c.parent.GetLevel(BookOf.Nature) != 5 || c.game.elfEffectors.Count < 4)
      return;
    c.parent.awards.Black_out_the_sun = true;
    if (!c.game.isRated || c.game.isClient)
      return;
    Achievements.Set(Achievement.Block_out_the_sun, c.parent.account, c.game);
  }

  public static sfloat AngleToGoDistance(sfloat v, sfloat g, sfloat d, sfloat y, bool smallAngle)
  {
    return smallAngle ? math.atan((v * v - math.sqrt(v * v * v * v - g * (g * d * d + (sfloat) 2 * y * v * v))) / (g * d)) * (sfloat) 57.29578f : math.atan((v * v + math.sqrt(v * v * v * v - g * (g * d * d + (sfloat) 2 * y * v * v))) / (g * d)) * (sfloat) 57.29578f;
  }

  public static float AngleToGoDistance(
    float velocity,
    float gravity,
    float distance,
    float height,
    bool smallAngle)
  {
    return smallAngle ? Mathf.Atan((float) (((double) velocity * (double) velocity - (double) Mathf.Sqrt((float) ((double) velocity * (double) velocity * (double) velocity * (double) velocity - (double) gravity * ((double) gravity * (double) distance * (double) distance + 2.0 * (double) height * (double) velocity * (double) velocity)))) / ((double) gravity * (double) distance))) * 57.29578f : Mathf.Atan((float) (((double) velocity * (double) velocity + (double) Mathf.Sqrt((float) ((double) velocity * (double) velocity * (double) velocity * (double) velocity - (double) gravity * ((double) gravity * (double) distance * (double) distance + 2.0 * (double) height * (double) velocity * (double) velocity)))) / ((double) gravity * (double) distance))) * 57.29578f;
  }

  public static FixedInt AngleToGoDistance(
    FixedInt v,
    FixedInt g,
    FixedInt d,
    FixedInt y,
    bool smallAngle)
  {
    return (FixedInt) ZSpell.AngleToGoDistance((sfloat) v, (sfloat) g, (sfloat) d, (sfloat) y, smallAngle);
  }

  public static FixedInt AngleToGoDistanceBAD(
    FixedInt v,
    FixedInt g,
    FixedInt d,
    FixedInt y,
    bool smallAngle)
  {
    return smallAngle ? FixedInt.Atan((v * v - Mathd.Sqrt(v * v * v * v - g * (g * d * d + (FixedInt) 2 * y * v * v))) / (g * d)) * FixedInt.Rad2Deg : FixedInt.Atan((v * v + Mathd.Sqrt(v * v * v * v - g * (g * d * d + (FixedInt) 2 * y * v * v))) / (g * d)) * FixedInt.Rad2Deg;
  }

  public static sfloat CalculateStartingVelocity(
    sfloat gravity,
    sfloat distance,
    sfloat height,
    sfloat angle)
  {
    sfloat x = angle / (sfloat) 57.29578f;
    return math.sqrt((gravity * distance * math.tan(x) + gravity * distance * distance / ((sfloat) 2 * math.cos(x) * math.cos(x))) / (height - distance * math.tan(x)));
  }

  public static FixedInt CalculateStartingVelocity(
    FixedInt gravity,
    FixedInt distance,
    FixedInt height,
    FixedInt angle)
  {
    return (FixedInt) ZSpell.CalculateStartingVelocity((sfloat) gravity, (sfloat) distance, (sfloat) height, (sfloat) angle);
  }

  public static FixedInt VelocityToGoDistance(
    FixedInt angle,
    FixedInt gravity,
    FixedInt distance,
    FixedInt height,
    bool smallAngle)
  {
    return (FixedInt) ZSpell.VelocityToGoDistance((sfloat) angle, (sfloat) gravity, (sfloat) distance, (sfloat) height, smallAngle);
  }

  public static sfloat VelocityToGoDistance(
    sfloat angle,
    sfloat gravity,
    sfloat distance,
    sfloat height,
    bool smallAngle)
  {
    sfloat x = angle / (sfloat) 57.29578f;
    sfloat sfloat1 = distance * distance;
    sfloat sfloat2 = math.sin((sfloat) 2 * x) * distance;
    sfloat sfloat3 = (sfloat) 2 * (height - distance * math.tan(x));
    return smallAngle ? math.sqrt(gravity * sfloat1 / (sfloat2 + math.sqrt(sfloat2 * sfloat2 + (sfloat) 4 * sfloat1 * sfloat3))) : math.sqrt(gravity * sfloat1 / (sfloat2 - math.sqrt(sfloat2 * sfloat2 + (sfloat) 4 * sfloat1 * sfloat3)));
  }

  public static FixedInt Range(MyLocation u, FixedInt g) => (FixedInt) 2 * u.x * u.y / g + u.x;

  public static FixedInt MaximumHeight(MyLocation u, FixedInt g)
  {
    return u.y * u.y / ((FixedInt) 2 * -g) + u.y / 2;
  }

  public static void ApparitionArmageddon(ZCreature c)
  {
    List<SpellSlot> spellSlotList = new List<SpellSlot>();
    int num1 = c.parent.localTurn - c.game.armageddonTurn;
    foreach (SpellSlot spell in c.spells)
    {
      if (c.SpellUseable(spell) && (spell.spell.level == 1 || spell.spell.level == 2 && num1 >= 10 || num1 >= 20) && (!spell.isPresent || !Armageddon.ApparitionExcludedSpells.Contains(spell.spell.spellEnum)))
        spellSlotList.Add(spell);
    }
    SpellSlot spellSlot = (SpellSlot) null;
    Spell spell1 = (Spell) null;
    if (spellSlotList.Count > 0)
    {
      spellSlot = spellSlotList[c.game.RandomInt(0, spellSlotList.Count)];
      spell1 = spellSlot.spell;
    }
    if ((UnityEngine.Object) spell1 == (UnityEngine.Object) null)
      spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
    ZCreature zcreature;
    FixedInt power;
    FixedInt fixedInt1;
    int x1;
    int y1;
    while (true)
    {
      zcreature = (ZCreature) null;
      FixedInt fixedInt2 = (FixedInt) 10000;
      bool flag = Armageddon.ApparitionFriendlySpells.Contains(spell1.spellEnum);
      foreach (ZPerson player in c.game.players)
      {
        if (flag == (player.team == c.parent.team) && !ZComponent.IsNull((ZComponent) player.first()))
        {
          FixedInt fixedInt3 = (FixedInt) MyLocation.Distance(c.position, player.first().position);
          if (fixedInt3 < fixedInt2)
          {
            fixedInt2 = fixedInt3;
            zcreature = player.first();
          }
        }
      }
      if ((ZComponent) zcreature == (object) null)
        zcreature = c.parent.first();
      if (!((ZComponent) zcreature == (object) null))
      {
        MyLocation myLocation = zcreature.GetPositionAI - c.position;
        myLocation.Normalize();
        FixedInt d = FixedInt.Abs(c.position.x - (FixedInt) (float) (int) zcreature.position.x);
        FixedInt v = (FixedInt) spell1.speedMax;
        if (v <= 0)
          v = (FixedInt) 1000;
        power = (FixedInt) (spell1.spellType == SpellType.Bolt ? 0 : 1);
        FixedInt fixedInt4 = spell1.affectedByGravity ? ZSpell.AngleToGoDistance(v, -(FixedInt) spell1.CustomGravity, d, zcreature.position.y - c.position.y, false) : FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
        FixedInt fixedInt5 = spell1.affectedByGravity ? ZSpell.AngleToGoDistance(v, -(FixedInt) spell1.CustomGravity, d, zcreature.position.y - c.position.y, true) : FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
        fixedInt1 = MyLocation.Distance(zcreature.GetPositionAI, ZSpell.SimulatePath(c.game, c.position, Inert.Velocity(zcreature.position.x < c.position.x ? (FixedInt) 180 - fixedInt4 : fixedInt4, (FixedInt) spell1.speedMax), (FixedInt) spell1.CustomGravity, c, spell1)) < MyLocation.Distance(zcreature.GetPositionAI, ZSpell.SimulatePath(c.game, c.position, Inert.Velocity(zcreature.position.x < c.position.x ? (FixedInt) 180 - fixedInt5 : fixedInt5, (FixedInt) spell1.speedMax), (FixedInt) spell1.CustomGravity, c, spell1)) ? fixedInt4 : fixedInt5;
        if (FixedInt.InvalidAngle(fixedInt1))
        {
          power = (FixedInt) 1.5f;
          fixedInt1 = (FixedInt) 47185920L;
        }
        if (spell1.affectedByGravity && zcreature.position.x < c.position.x)
          fixedInt1 = (FixedInt) 180 - fixedInt1;
        if (fixedInt1 > 90 || fixedInt1 < -90)
        {
          if ((double) c.transformscale > 0.0)
            c.SetScale(-1f);
        }
        else if ((double) c.transformscale < 0.0)
          c.SetScale(1f);
        if (spell1.spellEnum == SpellEnum.Arcane_Gate || spell1.spellEnum == SpellEnum.Santas_Magic)
        {
          int max = 300;
          for (int index = 0; index < 50; ++index)
          {
            int num2 = (int) c.position.x + c.game.RandomInt(-max, max);
            int num3 = (int) c.position.y + c.game.RandomInt(-max, max);
            if (ZSpell.ServerCanFire(spell1, num2, num3, 0, 0, c, fixedInt1, power, new MyLocation(num2, num3)))
            {
              spellSlot?.Fired(c);
              ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(num2, num3), NullMyLocation.Get());
              return;
            }
          }
          spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
          spellSlot = (SpellSlot) null;
        }
        else if (spell1.spellEnum == SpellEnum.Curse_of_Disabling || spell1.spellEnum == SpellEnum.Curse_of_Loneliness)
        {
          x1 = (int) c.position.x;
          y1 = (int) c.position.y;
          if (!ZSpell.ServerCanFire(spell1, x1, y1, 0, 0, c, fixedInt1, power, new MyLocation(x1, y1)))
          {
            spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
            spellSlot = (SpellSlot) null;
          }
          else
            goto label_43;
        }
        else if (spell1.maxDistance > 10)
        {
          int max = spell1.maxDistance - spell1.radius;
          if (spell1.spellEnum == SpellEnum.Duplication)
          {
            int x2 = (int) zcreature.GetPositionAI.x;
            int y2 = (int) zcreature.GetPositionAI.y;
            for (int index = 0; index < 50; ++index)
            {
              int num4 = (int) c.position.x + c.game.RandomInt(-max, max);
              int num5 = (int) c.position.y + c.game.RandomInt(-max, max);
              if (ZSpell.ServerCanFire(spell1, x2, y2, num4, num5, c, fixedInt1, power, new MyLocation(x2, y2)))
              {
                spellSlot?.Fired(c);
                ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(x2, y2), new MyLocation(num4, num5));
                return;
              }
            }
          }
          else if (spell1.spellEnum == SpellEnum.Resurrection)
          {
            FixedInt rot_z = (FixedInt) c.game.RandomInt(0, c.game.lastMinionToDie.Count);
            for (int index = 0; index < 50; ++index)
            {
              int num6 = (int) c.position.x + c.game.RandomInt(-max, max);
              int num7 = (int) c.position.y + c.game.RandomInt(-max, max);
              if (ZSpell.ServerCanFire(spell1, num6, num7, 0, 0, c, rot_z, power, new MyLocation(num6, num7)))
              {
                spellSlot?.Fired(c);
                ZSpell.FireWhich(spell1, c, c.position, rot_z, power, new MyLocation(num6, num7), NullMyLocation.Get());
                return;
              }
            }
          }
          else if (spell1.spellEnum == SpellEnum.Summon_Titan)
          {
            FixedInt rot_z = (FixedInt) c.game.RandomInt(0, c.parent.minionBookTitans.Count);
            for (int index = 0; index < 50; ++index)
            {
              int num8 = (int) c.position.x + c.game.RandomInt(-max, max);
              int num9 = (int) c.position.y + c.game.RandomInt(-max, max);
              if (ZSpell.ServerCanFire(spell1, num8, num9, 0, 0, c, rot_z, power, new MyLocation(num8, num9)))
              {
                spellSlot?.Fired(c);
                ZSpell.FireWhich(spell1, c, c.position, rot_z, power, new MyLocation(num8, num9), NullMyLocation.Get());
                return;
              }
            }
          }
          else
          {
            for (int index = 0; index < 50; ++index)
            {
              int num10 = (int) c.position.x + c.game.RandomInt(-max, max);
              int num11 = (int) c.position.y + c.game.RandomInt(-max, max);
              if (ZSpell.ServerCanFire(spell1, num10, num11, 0, 0, c, fixedInt1, power, new MyLocation(num10, num11)))
              {
                spellSlot?.Fired(c);
                ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(num10, num11), NullMyLocation.Get());
                return;
              }
            }
          }
          spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
          spellSlot = (SpellSlot) null;
        }
        else if (spell1.spellType == SpellType.Blit)
        {
          int max = 100;
          for (int index = 0; index < 50; ++index)
          {
            int num12 = (int) zcreature.position.x + (index == 0 ? 0 : c.game.RandomInt(-max, max));
            int num13 = (int) zcreature.position.y + (index == 0 ? 0 : c.game.RandomInt(-max, max));
            if (ZSpell.ServerCanFire(spell1, num12, num13, 0, 0, c, fixedInt1, power, new MyLocation(num12, num13)))
            {
              spellSlot?.Fired(c);
              ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(num12, num13), NullMyLocation.Get());
              return;
            }
          }
          spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
          spellSlot = (SpellSlot) null;
        }
        else if (spell1.spellEnum != SpellEnum.Enchanted_Axes)
        {
          if (!ZSpell.ServerCanFire(spell1, (int) zcreature.position.x, (int) zcreature.position.y, 0, 0, c, fixedInt1, power, new MyLocation((int) zcreature.position.x, (int) zcreature.position.y)))
          {
            int max = 100 + spell1.radius;
            for (int index = 0; index < 50; ++index)
            {
              int num14 = (int) zcreature.position.x + (index == 0 ? 0 : c.game.RandomInt(-max, max));
              int num15 = (int) zcreature.position.y + (index == 0 ? 0 : c.game.RandomInt(-max, max));
              if (ZSpell.ServerCanFire(spell1, num14, num15, 0, 0, c, fixedInt1, power, new MyLocation(num14, num15)))
              {
                spellSlot?.Fired(c);
                ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(num14, num15), NullMyLocation.Get());
                return;
              }
            }
            spell1 = Inert.GetSpell(SpellEnum.Color_Spray);
            spellSlot = (SpellSlot) null;
          }
          else
            goto label_103;
        }
        else
          goto label_91;
      }
      else
        break;
    }
    return;
label_43:
    spellSlot?.Fired(c);
    ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(x1, y1), NullMyLocation.Get());
    return;
label_91:
    spellSlot?.Fired(c);
    ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, new MyLocation(fixedInt1, (FixedInt) 0), NullMyLocation.Get());
    return;
label_103:
    spellSlot?.Fired(c);
    ZSpell.FireWhich(spell1, c, c.position, fixedInt1, power, zcreature.GetPositionAI, NullMyLocation.Get());
    if (spell1.EndsTurn)
      return;
    c.game.ongoing.RunSpell(ZSpell.LazyDo((Action) (() =>
    {
      if (ZComponent.IsNull((ZComponent) c) || c.isDead)
        return;
      ZSpell.ApparitionArmageddon(c);
    })));
  }

  public static IEnumerator<float> LazyDo(Action a, int delay = 30)
  {
    while (delay >= 0)
    {
      --delay;
      yield return 0.0f;
    }
    Action action = a;
    if (action != null)
      action();
  }

  public static MyLocation SimulatePath(
    ZGame game,
    MyLocation position,
    MyLocation velocity,
    FixedInt gravity,
    ZCreature toCollideCheck)
  {
    int num = 0;
    while (num < 5000 && position.y > 0 && (gravity < 0 || position.x > 0 && position.x < game.map.Width && position.y < game.map.Height))
    {
      ++num;
      position += velocity;
      velocity.y += gravity;
      if (!game.map.SpellCheckPosition(position.x.ToInt(), position.y.ToInt(), toCollideCheck, Inert.mask_spell_movement))
        return position;
    }
    return position;
  }

  public static MyLocation SimulatePath(
    ZGame game,
    MyLocation position,
    MyLocation velocity,
    FixedInt gravity,
    ZCreature toCollideCheck,
    Spell spell)
  {
    List<Coords> outlineArray = MapGenerator.getOutlineArray(spell.radius);
    int num1 = 0;
    while (num1 < 1000 && position.y > 0 && (gravity < 0 || position.x > 0 && position.x < game.map.Width && position.y < game.map.Height))
    {
      FixedInt x1 = position.x;
      FixedInt y1 = position.y;
      FixedInt fixedInt1 = position.x + velocity.x;
      FixedInt fixedInt2 = position.y + velocity.y;
      FixedInt x2 = x1;
      FixedInt y2 = y1;
      int num2 = 1;
      FixedInt x3 = velocity.x;
      FixedInt y3 = velocity.y;
      FixedInt fixedInt3 = velocity.x;
      FixedInt fixedInt4 = velocity.y;
      FixedInt fixedInt5;
      if (x3 > 1 || x3 < -1 || y3 > 1 || y3 < -1)
      {
        if (FixedInt.Abs(x3) > FixedInt.Abs(y3))
        {
          fixedInt5 = FixedInt.Abs(x3);
          num2 = fixedInt5.ToInt() + 1;
        }
        else
        {
          fixedInt5 = FixedInt.Abs(y3);
          num2 = fixedInt5.ToInt() + 1;
        }
        fixedInt3 = velocity.x / num2;
        fixedInt4 = velocity.y / num2;
      }
      while (num2 > 0)
      {
        --num2;
        fixedInt5 = (FixedInt.Create(360) - (Inert.AngleOfVelocity(velocity) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * outlineArray.Count - spell.radius;
        int num3 = fixedInt5.ToInt();
        if (num3 < 0)
          num3 += outlineArray.Count;
        for (int index1 = 0; index1 < spell.radius * 2; ++index1)
        {
          int index2 = (index1 + num3) % outlineArray.Count;
          ZMap map = game.map;
          fixedInt5 = fixedInt3 + x1;
          int x4 = fixedInt5.ToInt() + outlineArray[index2].x;
          fixedInt5 = fixedInt4 + y1;
          int y4 = fixedInt5.ToInt() + outlineArray[index2].y;
          ZCreature creature = toCollideCheck;
          int maskSpellMovement = Inert.mask_spell_movement;
          if (!map.SpellCheckPosition(x4, y4, creature, maskSpellMovement))
          {
            if (spell.explodeOnImpact)
              return new MyLocation(x1, y1);
            position = new MyLocation(x2, y2);
            MyLocation zero = MyLocation.zero;
            fixedInt5 = fixedInt3 + x1;
            int num4 = fixedInt5.ToInt();
            fixedInt5 = fixedInt4 + y1;
            int num5 = fixedInt5.ToInt();
            int num6 = num4 + outlineArray[index2].x;
            int num7 = num5 + outlineArray[index2].y;
            for (int index3 = -2; index3 <= 2; ++index3)
            {
              for (int index4 = -2; index4 <= 2; ++index4)
              {
                if (game.map.SpellCheckPosition(num6 + index3, num7 + index4, toCollideCheck, Inert.mask_movement_NoEffector))
                {
                  zero.x += index3;
                  zero.y += index4;
                }
              }
            }
            zero.Normalize();
            MyLocation.Reflect(velocity, ref zero, out velocity);
            velocity.Multiply(spell.elasticity.x, out velocity);
            if (MyLocation.Distance(MyLocation.zero, velocity) <= 1)
              return new MyLocation(x1, y1);
            goto label_27;
          }
        }
        x2 = x1;
        y2 = y1;
        x1 += fixedInt3;
        y1 += fixedInt4;
      }
      position += velocity;
      velocity.y += gravity;
label_27:
      ++num1;
      if (num1 > spell.maxDuration)
        return position;
    }
    return position;
  }

  public static IEnumerator<float> IEArrows(
    ZCreature c,
    Spell spell,
    MyLocation pos,
    bool volley,
    int amount = 0)
  {
    ZCreature parent = c.parent.controlled.Count > 0 ? c.parent.controlled[0] : (ZCreature) null;
    if ((ZComponent) parent != (object) null)
      parent.turnFriendlyDmg = -2;
    if (amount == 0)
    {
      amount = volley ? 3 : 3;
      if (c.familiar.Has(FamiliarType.Nature) && c.familiarLevelNature > 0)
      {
        if (volley)
          amount += c.familiarLevelNature;
        else
          amount += c.familiarLevelNature * 2;
      }
    }
    FixedInt d = FixedInt.Abs(pos.x - c.GetPositionAI.x);
    if (d == 0)
      d = (FixedInt) 1;
    FixedInt.Abs(pos.y - c.GetPositionAI.y);
    FixedInt fixedInt = (FixedInt) (MyLocation.Distance(pos, c.GetPositionAI) * 2);
    FixedInt speed = (FixedInt) 8;
    if (fixedInt > 70)
      speed = !(fixedInt < 300) ? (!(fixedInt < 500) ? (!(fixedInt < 720) ? (!(fixedInt < 980) ? (!(fixedInt < 1280) ? (!(fixedInt < 1625) ? (!(fixedInt < 2010) ? (!(fixedInt < 2540) ? (!(fixedInt < 3130) ? (FixedInt) 60 : (FixedInt) 50) : (FixedInt) 45) : (FixedInt) 40) : (FixedInt) 36) : (FixedInt) 32) : (FixedInt) 28) : (FixedInt) 24) : (FixedInt) 20) : (FixedInt) 16;
    FixedInt angle = ZSpell.AngleToGoDistance(speed, -c.map.Gravity, d, pos.y - c.GetPositionAI.y, true);
    for (int i = 0; i < amount && !ZComponent.IsNull((ZComponent) c) && (!((ZComponent) parent != (object) null) || parent.turnFriendlyDmg != parent.game.turn); ++i)
    {
      MyLocation power = Inert.Velocity(angle + c.game.RandomFixedInt(-10485760L, 10485760L), speed);
      if (pos.x < c.GetPositionAI.x)
        power.x = -power.x;
      ZSpell.BaseFire(spell, c, c.GetPositionAI, Quaternion.identity, power);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEFireworkShow(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    MyLocation target)
  {
    MyLocation start = new MyLocation(target.x - 500, (FixedInt) c.map.Height);
    for (int z = 0; z < 2; ++z)
    {
      FixedInt d = FixedInt.Abs(target.x - start.x);
      if (d == 0)
        d = (FixedInt) 1;
      FixedInt.Abs(target.y - start.y);
      FixedInt fixedInt = (FixedInt) (MyLocation.Distance(target, start) * 2);
      FixedInt speed = (FixedInt) 8;
      if (fixedInt > 70)
        speed = !(fixedInt < 300) ? (!(fixedInt < 500) ? (!(fixedInt < 720) ? (!(fixedInt < 980) ? (!(fixedInt < 1280) ? (!(fixedInt < 1625) ? (!(fixedInt < 2010) ? (!(fixedInt < 2540) ? (!(fixedInt < 3130) ? (FixedInt) 60 : (FixedInt) 50) : (FixedInt) 45) : (FixedInt) 40) : (FixedInt) 36) : (FixedInt) 32) : (FixedInt) 28) : (FixedInt) 24) : (FixedInt) 20) : (FixedInt) 16;
      FixedInt angle = ZSpell.AngleToGoDistanceBAD(speed, -c.map.Gravity, d, target.y - start.y, false);
      if (FixedInt.InvalidAngle(angle))
        angle = (FixedInt) 47185920L;
      for (int i = 0; i < theSpell.amount; ++i)
      {
        MyLocation power = Inert.Velocity(angle + c.game.RandomFixedInt(-5242880L, 5242880L), speed);
        if (target.x < start.x)
          power.x = -power.x;
        ZSpell.BaseFire(theSpell, c, start, Quaternion.identity, power).maxDuration = 90;
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
      }
      start = new MyLocation(target.x + 500, (FixedInt) c.map.Height);
    }
  }

  public static void FireDarkDefenses(Spell theSpell, ZCreature c)
  {
    if (ZEffector.InSanctuary(c.game.world, c.position))
    {
      if (!c.game.isClient)
        return;
      ZEffector.SpawnVineExplosion((Vector3) c.position.ToSinglePrecision());
    }
    else
    {
      c.hasDarkDefenses = true;
      c.DarkDefenses(false);
      if (!c.game.isClient || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null))
        return;
      ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position, Quaternion.identity, c.transform).GetComponent<ParticleDarkDefenses>().c = c.clientObj;
    }
  }

  public static void FireSwallowingPit(Spell theSpell, ZCreature c)
  {
    if (c.isPawn && (ZComponent) c.parent?.first() != (object) null)
      c = c.parent.first();
    List<MyLocation> myLocationList = new List<MyLocation>();
    Spell spell = Inert.Instance.spells["Aura of Decay"];
    for (int index1 = 0; index1 < c.game._playersExtended.Count; ++index1)
    {
      for (int index2 = c.game._playersExtended[index1].controlled.Count - 1; index2 >= 0; --index2)
      {
        if (c.game._playersExtended[index1].controlled.Count > index2 && c.game._playersExtended[index1].controlled[index2].isPawn && c.game._playersExtended[index1].controlled[index2].race != CreatureRace.Undead)
          myLocationList.Add(c.game._playersExtended[index1].controlled[index2].position);
      }
    }
    List<ZEffector> zeffectorList = new List<ZEffector>();
    foreach (MyLocation pos in myLocationList)
    {
      MyLocation target = pos;
      zeffectorList.Add(ZSpell.FireAuraOfDecay(spell, c, pos, (FixedInt) 0, (FixedInt) 0, target, true));
    }
    for (int index3 = 0; index3 < c.game._playersExtended.Count; ++index3)
    {
      for (int index4 = c.game._playersExtended[index3].controlled.Count - 1; index4 >= 0; --index4)
      {
        if (c.game._playersExtended[index3].controlled.Count > index4 && c.game._playersExtended[index3].controlled[index4].isPawn && c.game._playersExtended[index3].controlled[index4].race != CreatureRace.Undead)
          c.game._playersExtended[index3].controlled[index4].OnDeath(true);
      }
    }
    for (int index = 0; index < myLocationList.Count; ++index)
      ZSpell.FinishFireingAura(c, zeffectorList[index], myLocationList[index]);
  }

  public static void FireBloodSiphon(Spell theSpell, ZCreature c)
  {
    c.game.ongoing.RunSpell(ZSpell.IEBloodSiphon(theSpell, c));
  }

  public static IEnumerator<float> IEBloodSiphon(Spell theSpell, ZCreature c)
  {
    for (int index1 = 0; index1 < c.game._playersExtended.Count; ++index1)
    {
      for (int index2 = c.game._playersExtended[index1].controlled.Count - 1; index2 >= 0; --index2)
      {
        if (c.game._playersExtended[index1].controlled.Count > index2 && (ZComponent) c.game._playersExtended[index1].controlled[index2] != (object) null && c.game._playersExtended[index1].controlled[index2].isPawn && (ZComponent) c.game._playersExtended[index1].controlled[index2] != (object) c && !((UnityEngine.Object) c.transform == (UnityEngine.Object) null) && !((UnityEngine.Object) c.game._playersExtended[index1].controlled[index2].transform == (UnityEngine.Object) null))
        {
          ParticleBloodSyphon particleBloodSyphon = ZComponent.Instantiate<ParticleBloodSyphon>(ClientResources.Instance.bloodSyphon, c.transform.position, Quaternion.identity, c.game.GetMapTransform());
          particleBloodSyphon.start = c.transform.position;
          particleBloodSyphon.end = c.game._playersExtended[index1].controlled[index2].transform.position;
          particleBloodSyphon.velocity = particleBloodSyphon.end - particleBloodSyphon.start;
        }
      }
    }
    for (int i = 0; i < 30; ++i)
      yield return 0.0f;
    for (int index3 = 0; index3 < c.game._playersExtended.Count; ++index3)
    {
      for (int index4 = c.game._playersExtended[index3].controlled.Count - 1; index4 >= 0; --index4)
      {
        if (c.game._playersExtended[index3].controlled.Count > index4 && (ZComponent) c.game._playersExtended[index3].controlled[index4] != (object) null && c.game._playersExtended[index3].controlled[index4].isPawn && (ZComponent) c.game._playersExtended[index3].controlled[index4] != (object) c)
          c.game._playersExtended[index3].controlled[index4].ApplyDamage(theSpell.spellEnum, theSpell.damageType, 25, c, c.game.turn, (ISpellBridge) theSpell);
      }
    }
  }

  public static void FireCommune(Spell theSpell, ZCreature c)
  {
    c.parent.communeWithNature = 1;
    foreach (ZCreature zcreature in c.parent.controlled)
      zcreature.UpdateHealthTxt();
  }

  public static void FireArcaneSigil(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    int amount = 3)
  {
    ZGame game = c.game;
    if (amount == 1)
    {
      ZSpell.SpawnSigil(theSpell, game, c, target);
      c.game.forceRysncPause = true;
    }
    else
    {
      ZSpell.SpawnSigil(theSpell, game, c, target + new MyLocation(0, 10));
      ZSpell.SpawnSigil(theSpell, game, c, target + new MyLocation(-8, 0));
      ZSpell.SpawnSigil(theSpell, game, c, target + new MyLocation(8, 0));
      c.game.forceRysncPause = true;
      if (!c.game.isClient || c.game.resyncing)
        return;
      ZSpell.CameraFollow(target);
    }
  }

  private static void SpawnSigil(Spell theSpell, ZGame game, ZCreature c, MyLocation pos)
  {
    if (ZEffector.InSanctuary(c.game.world, pos))
    {
      ZEffector.SpawnVineExplosion((Vector3) pos.ToSinglePrecision());
    }
    else
    {
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) pos.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      zeffector.game = game;
      zeffector.whoSummoned = c;
      zeffector.collider.world = zeffector.world;
      zeffector.collider.Move(pos);
      game.globalEffectors.Add(zeffector);
      if (!game.world.OverlapCircle((Point) zeffector.position, 16, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom))
        return;
      zeffector.EffectCreature((ZCreature) null);
    }
  }

  public static void FireLifeDiew(Spell theSpell, ZCreature c, MyLocation pos)
  {
    if (ZEffector.InSanctuary(c.game.world, pos))
    {
      ZEffector.SpawnVineExplosion((Vector3) pos.ToSinglePrecision());
    }
    else
    {
      ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) pos.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      zeffector.game = c.game;
      zeffector.whoSummoned = c;
      zeffector.collider.world = zeffector.world;
      zeffector.collider.Move(pos);
      c.effectors.Add(zeffector);
    }
  }

  public static void FireRainOfFire(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    bool extended,
    bool nono = false)
  {
    FixedInt fixedInt = (FixedInt) (!ZSpell.Controllable(theSpell, c.game) ? (c.game.RandomInt(0, 2) == 1 ? -1 : 1) : ((double) c.transformscale > 0.0 ? 1 : -1));
    pos.y = (FixedInt) (c.map.Height + 800);
    pos.x = target.x + -fixedInt * (pos.y - target.y) / 2;
    MyLocation power1 = (target - pos).normalized * (FixedInt) theSpell.speedMax;
    int num1 = 5;
    if (c.familiar.Has(FamiliarType.Flame) && c.familiarLevelFlame > 0)
    {
      int num2 = ZSpell.ClampedFlameLevel(c);
      num1 += num2 / 2;
    }
    pos.x -= (FixedInt) (80 * (num1 / 2)) * fixedInt;
    for (int index = 0; index < num1; ++index)
    {
      ZSpell.BaseFire(theSpell, c, pos, Quaternion.identity, power1);
      pos.x += (FixedInt) 80 * fixedInt;
    }
  }

  public static bool Controllable(Spell theSpell, ZGame game)
  {
    if (theSpell.targetType != TargetType.Directional && theSpell.targetType != TargetType.Directional_Controlled && theSpell.targetType != TargetType.Directional_Controlled_Always || game.originalSpellsOnly)
      return false;
    return theSpell.targetType == TargetType.Directional_Controlled && game.MaxTurnTime > 10 || game.gameFacts.GetStyle().HasStyle(GameStyle.Random_Spells) || theSpell.targetType == TargetType.Directional_Controlled_Always;
  }

  public static IEnumerator<float> AltRainOfFire(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    bool extended)
  {
    int i;
    for (i = 0; i < 10; ++i)
      yield return 0.0f;
    ZSpell.FireRainOfFire(theSpell, c, pos, rot_z, power, target + new MyLocation(25, 0), extended, true);
    for (i = 0; i < 10; ++i)
      yield return 0.0f;
    ZSpell.FireRainOfFire(theSpell, c, pos, rot_z, power, target + new MyLocation(-25, 0), extended, true);
  }

  public static void FireRainOfClams(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    int amount = 5,
    bool randomDirection = false,
    bool armageddon = false)
  {
    MyLocation zero = MyLocation.zero;
    FixedInt fixedInt = (FixedInt) (randomDirection || !ZSpell.Controllable(theSpell, c.game) ? (c.game.RandomInt(0, 2) == 1 ? -1 : 1) : ((double) c.transformscale > 0.0 ? 1 : -1));
    zero.y = (FixedInt) (c.map.Height + 800);
    zero.x = target.x + -fixedInt * (zero.y - target.y) / 2;
    MyLocation power = (target - zero).normalized * (FixedInt) theSpell.speedMax;
    zero.x -= (FixedInt) (100 * (amount / 2)) * fixedInt;
    for (int index = 0; index < amount; ++index)
    {
      ZSpell zspell = ZSpell.BaseFire(theSpell, c, zero, Quaternion.identity, power);
      zspell.fromArmageddon = armageddon;
      zspell.affectedByGravity = false;
      zero.x += (FixedInt) 100 * fixedInt;
    }
  }

  public static IEnumerator<float> FireBarrageOfBones(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    int amount = 5,
    bool randomDirection = false)
  {
    MyLocation pos = MyLocation.zero;
    FixedInt fixedInt = (FixedInt) (randomDirection || !ZSpell.Controllable(theSpell, c.game) ? (c.game.RandomInt(0, 2) == 1 ? -1 : 1) : ((double) c.transformscale > 0.0 ? 1 : -1));
    pos.y = (FixedInt) (c.map.Height + 800);
    pos.x = target.x + -fixedInt * (pos.y - target.y) / 2;
    MyLocation dir = (target - pos).normalized;
    dir *= (FixedInt) theSpell.speedMax;
    for (int i = 0; i < amount; ++i)
    {
      ZSpell.BaseFire(theSpell, c, new MyLocation(pos.x + c.game.RandomFixedInt(-100, 100), pos.y + c.game.RandomFixedInt(0, 25)), Quaternion.identity, dir).affectedByGravity = false;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static void FireRainOfChaos(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    bool armageddon = false)
  {
    MyLocation zero1 = MyLocation.zero with
    {
      y = (FixedInt) (c.map.Height + 800),
      x = target.x - 350
    };
    int num = 7;
    MyLocation zero2 = MyLocation.zero;
    if (c.familiarLevelUnderdark == 5 && c.game.AllowExpansion)
    {
      MyLocation myLocation = target;
      for (int index = 0; index < num; ++index)
      {
        FixedInt fixedInt = c.game.RandomFixedInt((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax) * 2;
        MyLocation power = (myLocation - zero1).normalized * fixedInt;
        ZSpell zspell = ZSpell.BaseFire(theSpell, c, zero1, Quaternion.identity, power);
        zspell.fromArmageddon = armageddon;
        zspell.curSpeed = fixedInt;
        zspell.target.y = myLocation.y;
        zspell.target.x = myLocation.x + c.game.RandomFixedInt(-100, 100);
        zspell.damage = 13;
        zspell.maxTargetFrames = 0;
        zspell.affectedByGravity = false;
        zero1.x += 100;
      }
    }
    else
    {
      for (int index = 0; index < num; ++index)
      {
        zero2.y = (FixedInt) (c.map.Height - c.game.RandomInt(-25, 25));
        zero2.x = target.x + c.game.RandomInt(-25, 25);
        FixedInt fixedInt = c.game.RandomFixedInt((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax);
        MyLocation power = (zero2 - zero1).normalized * fixedInt;
        ZSpell zspell = ZSpell.BaseFire(theSpell, c, zero1, Quaternion.identity, power);
        zspell.fromArmageddon = armageddon;
        zspell.curSpeed = fixedInt;
        zspell.target.y = zero2.y;
        zspell.target.x = zero2.x;
        zspell.maxTargetFrames = c.game.RandomInt(100, 150);
        zero1.x += 100;
      }
    }
  }

  public static void FireAutumnLeaves(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    bool extended)
  {
    c.game.ongoing.RunSpell(ZSpell.IEnumeratorAutumnLeaves(c, Inert.Instance.spells["Autumn Leaves"], target, -125, 125, theSpell.amount, extended));
  }

  public static void FireCometArmageddon(Spell theSpell, ZPerson c, MyLocation target)
  {
    MyLocation zero = MyLocation.zero with
    {
      y = (FixedInt) (c.map.Height + 800),
      x = target.x + c.game.RandomInt(-600, 600)
    };
    MyLocation power = (target - zero).normalized * (FixedInt) theSpell.speedMax;
    ZSpell.BaseFire(theSpell, c, zero, Quaternion.identity, power).fromArmageddon = true;
  }

  public static void FireMeteor(Spell theSpell, ZCreature c, FixedInt way, MyLocation target)
  {
    if (!ZSpell.Controllable(theSpell, c.game))
      way = (FixedInt) (c.game.RandomInt(0, 2) == 1 ? -1 : 1);
    MyLocation zero = MyLocation.zero with
    {
      y = (FixedInt) (c.map.Height + 800)
    };
    zero.x = target.x + -way * (zero.y - target.y - 300) / 2;
    MyLocation power = (target - zero).normalized * (FixedInt) theSpell.speedMax;
    if (power.y > -20)
      power.y = (FixedInt) -20;
    ZSpell.BaseFire(theSpell, c, zero, Quaternion.identity, power);
  }

  public static IEnumerator<float> IEOceansFury(Spell theSpell, ZCreature c)
  {
    MyLocation left = new MyLocation(-500, 0);
    MyLocation right = new MyLocation(c.map.Width + 500, 0);
    if (c.game.isClient && !c.game.resyncing)
    {
      GameObject t1 = ZComponent.Instantiate<GameObject>(Inert.Instance.oceanWaves, left.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      GameObject t2 = ZComponent.Instantiate<GameObject>(Inert.Instance.oceanWaves, right.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
      t2.transform.localScale = new Vector3(-1f, 1f, 1f);
      ZComponent.Destroy<GameObject>(t1, 7f);
      ZComponent.Destroy<GameObject>(t2, 7f);
    }
    ZGame game = c.game;
    for (int i = 0; i < 10; ++i)
    {
      for (int index = 0; index < 10; ++index)
      {
        MyLocation power1 = Inert.Velocity(c.game.RandomFixedInt(-10, 10) + 20 + index * 6, c.game.RandomFixedInt(-5, 5) + 40);
        (game.firstOceanFury ? ZSpell.BaseFire(Inert.Instance.oceanDrop1, c, left, Quaternion.identity, power1) : ZSpell.BaseFire(Inert.Instance.oceanDrop2, c, left, Quaternion.identity, power1)).name = theSpell.name;
        MyLocation power2 = Inert.Velocity(c.game.RandomFixedInt(-10, 10) + 110 + index * 6, c.game.RandomFixedInt(-5, 5) + 40);
        (game.firstOceanFury ? ZSpell.BaseFire(Inert.Instance.oceanDrop1, c, right, Quaternion.identity, power2) : ZSpell.BaseFire(Inert.Instance.oceanDrop2, c, right, Quaternion.identity, power2)).name = theSpell.name;
      }
      for (int z = 0; z < 20; ++z)
        yield return 0.0f;
      if (ZComponent.IsNull((ZComponent) c) || c.health <= 0)
      {
        game.firstOceanFury = false;
        yield break;
      }
    }
    game.firstOceanFury = false;
  }

  public static IEnumerator<float> IEnumeratorRainOfArrows(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    FixedInt fixedInt = (FixedInt) (!ZSpell.Controllable(theSpell, c.game) ? (c.game.RandomInt(0, 2) == 1 ? -1 : 1) : ((double) c.transformscale > 0.0 ? 1 : -1));
    pos.y = (FixedInt) (c.map.Height + 800);
    pos.x = target.x + -fixedInt * (pos.y - target.y);
    pos.x -= (FixedInt) 300 * fixedInt;
    int num1 = 5;
    if (c.familiar.Has(FamiliarType.Flame) && c.familiarLevelFlame > 0)
    {
      int num2 = ZSpell.ClampedFlameLevel(c);
      num1 += num2 / 2;
    }
    for (int index = 0; index < num1; ++index)
    {
      MyLocation myLocation = target - pos;
      rot_z = FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
      MyLocation power1 = Inert.Velocity(rot_z, (FixedInt) theSpell.speedMax);
      ZSpell.BaseFireTarget(theSpell, c, pos + new MyLocation(c.game.RandomFixedInt(-40, 40), c.game.RandomFixedInt(-40, 40)), rot, power1, (FixedInt) theSpell.speedMax, target);
      pos.x += (FixedInt) 150 * fixedInt;
    }
    yield break;
  }

  public static IEnumerator<float> IEnumeratorMeteorShower(Spell theSpell, ZCreature c)
  {
    for (int i = c.game._playersExtended.Count - 1; i >= 0; --i)
    {
      ZPerson p = c.game._playersExtended[i];
      if (p.team != c.parent.team || p == c.game._uncontrolledPlayer)
      {
        for (int e = p.controlled.Count - 1; e >= 0; --e)
        {
          if (e < p.controlled.Count)
          {
            MyLocation position = p.controlled[e].position;
            p.game.RandomInt(-50, 50);
            MyLocation pos = new MyLocation();
            pos.y = (FixedInt) (p.map.Height + 800);
            pos.x = position.x + p.game.RandomInt(-300, 300);
            MyLocation power = (position - pos).normalized * (FixedInt) theSpell.speedMax;
            ZSpell.BaseFire(theSpell, c, pos, Quaternion.identity, power);
            yield return 0.0f;
            yield return 0.0f;
            yield return 0.0f;
          }
        }
        p = (ZPerson) null;
      }
    }
  }

  public static void FireFireWave(Spell theSpell, ZCreature c)
  {
    if (c.game.isClient && !c.game.resyncing)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, c.position.ToSinglePrecision(), Quaternion.identity);
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, c.position, Quaternion.identity);
    c.map.ServerBitBlt((int) theSpell.explosionCutout, (int) c.position.x, (int) c.position.y);
    zspell.ApplyExplosionForce(c.position, die: false);
    ZComponent.Destroy<GameObject>(zspell.gameObject);
  }

  public static void FireArcaneFlash(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    int delayFrames)
  {
    if (c.game.isClient && !c.game.resyncing)
    {
      if (theSpell.spellEnum == SpellEnum.Flutter)
        ZComponent.Instantiate<GameObject>(theSpell.explosion, c.position.ToSinglePrecision(), Inert.RotationOfVelocity(Inert.Velocity(rot_z)));
      else
        ZComponent.Instantiate<GameObject>(theSpell.explosion, pos.ToSinglePrecision(), Quaternion.identity);
    }
    c.game.ongoing.RunSpell(ZSpell.IEFlash(theSpell, c, pos, rot_z, power, delayFrames));
  }

  public static IEnumerator<float> IEFlash(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    int delayFrames)
  {
    for (int i = 0; i < delayFrames; ++i)
      yield return 0.0f;
    ZSpell zspell = ZSpell.BaseFireKablam(theSpell, c, pos, Quaternion.identity);
    pos += Inert.Velocity(rot_z) * 12;
    if (theSpell.spellEnum == SpellEnum.Arcane_Flash)
      zspell.damage = !c.HasArcaneEnergizer || theSpell.bookOf != BookOf.Arcane ? 15 : 20;
    else if (theSpell.spellEnum == SpellEnum.Sunder && c.type == CreatureType.Paladin && zspell.damage == 75 && c.familiarLevelOverlight > 0 && c.game.AllowExpansion)
      zspell.damage += c.familiarLevelOverlight * 5;
    c.map.ServerBitBlt((int) zspell.explosionCutout, (int) pos.x, (int) pos.y);
    zspell.ApplyExplosionForce(pos);
    ZComponent.Destroy<GameObject>(zspell.gameObject);
    if (c.game.isClient && (theSpell.spellEnum == SpellEnum.Arcane_Flash || theSpell.spellEnum == SpellEnum.Sunder || theSpell.spellEnum == SpellEnum.Smash) && (UnityEngine.Object) theSpell.explosionClip != (UnityEngine.Object) null)
      AudioManager.Play(theSpell.explosionClip);
  }

  public static IEnumerator<float> IEFireVineWhip(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation v = Inert.Velocity(rot_z) * (theSpell.radius - c.radius + 5);
    power = !(power < 1) ? (FixedInt) 1 + power / 20 : (FixedInt) 1;
    pos += v;
    v = Inert.Velocity(rot_z) * 25 * power;
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, pos, Quaternion.identity);
    MyLocation myLocation = pos;
    pos = myLocation;
    kablam.position = myLocation;
    kablam.explisiveForce = (FixedInt) 9;
    for (int i = 0; i < 6; ++i)
    {
      if (c.game.isClient && !c.game.resyncing)
        ZComponent.Instantiate<GameObject>(theSpell.toSummon, pos.ToSinglePrecision(), Quaternion.identity);
      c.map.ServerBitBlt(18, (int) pos.x, (int) pos.y);
      kablam.position = pos;
      kablam.ApplyExplosionForce(new MyLocation(pos.x, pos.y - 10));
      pos += v;
      kablam.explisiveForce += (FixedInt) 2097152L;
      yield return 0.0f;
      yield return 0.0f;
      kablam.Undie();
    }
    AudioManager.Play(theSpell.explosionClip);
    if (c.game.isClient && !c.game.resyncing)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, pos.ToSinglePrecision(), Quaternion.identity);
    c.map.ServerBitBlt(5, (int) pos.x, (int) pos.y);
    kablam.radius = 38;
    kablam.EXORADIUS = 45;
    kablam.ApplyExplosionForce(pos);
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static void FireMorningSun(Spell theSpell, ZCreature c, MyLocation target)
  {
    target.y = (FixedInt) c.map.Height;
    target.x -= 18;
    for (int index = 0; index < 3; ++index)
    {
      ZFlameWallSpell zflameWallSpell1 = ZSpell.Create(c.game, theSpell.toSummon.GetComponent<FlameWallSpell>(), new Vector3((float) target.x.ToInt(), (float) c.map.Height), Quaternion.identity, c.game.GetMapTransform());
      ZEffector effector = zflameWallSpell1.effector;
      c.effectors.Add(effector);
      effector.game = c.game;
      effector.whoSummoned = c;
      ZFlameWallSpell zflameWallSpell2 = zflameWallSpell1;
      zflameWallSpell2.colliderA.world = effector.world;
      effector.collider = zflameWallSpell2.colliderA;
      effector.collider.effector = effector;
      zflameWallSpell2.colliderA.Initialize(target, c.game.world);
      zflameWallSpell2.SetPosition(target);
      effector.spell.position = target;
      effector.spell.game = c.game;
      effector.spell.MoveMorningSun(c.map.Height, c.map.Height);
      if (!c.game.isClient)
        ZComponent.Destroy<GameObject>(effector.transform.GetChild(0).gameObject);
      target.x += 18;
    }
  }

  public static ZFlameWallSpell FireFlameWall(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    FixedInt rot_z,
    FixedInt power,
    bool isActive = true)
  {
    ZFlameWallSpell s = ZSpell.Create(c.game, theSpell.gameObject.GetComponent<FlameWallSpell>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    s.game = c.game;
    if (theSpell.bookOf == BookOf.Flame)
      ZSpell.UpgradeFullFire(c, (ZSpell) s);
    s.parent = c;
    s.position = target;
    ZEffector effector = s.effector;
    effector.game = c.game;
    c.effectors.Add(effector);
    effector.whoSummoned = c;
    effector.collider.world = effector.world;
    ZFlameWallSpell zflameWallSpell = s;
    ZEffector effector2 = s.effector2;
    effector2.game = c.game;
    effector2.collider.Initialize(target, c.game.world);
    zflameWallSpell.colliderB = effector2.collider;
    c.effectors.Add(effector2);
    effector2.whoSummoned = c;
    Mathf.Abs(30);
    if (theSpell.spellEnum == SpellEnum.Prickly_Barrier)
    {
      effector2.clientSpriteIndex = effector2.game.RandomInt(0, 5);
      effector2.VisualUpdate();
    }
    if (!isActive)
    {
      s.effector2.active = false;
      s.effector2.VisualUpdate();
    }
    zflameWallSpell.SetPosition(target);
    s.SetVelocity(new MyLocation(0, -10));
    if (theSpell.spellEnum == SpellEnum.Prickly_Barrier && c.game.isClient)
    {
      Color clientColor = c.parent.clientColor with
      {
        a = 0.2f
      };
      s.transform.GetChild(1).GetComponent<SpriteRenderer>().color = clientColor;
    }
    return s;
  }

  public static void FireFireball(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation myLocation = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell s = ZSpell.BaseFire(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), myLocation, false);
    ZSpell.UpgradeFullFire(c, s);
    s.SetVelocity(myLocation);
  }

  public static void FireGeneric(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell.BaseFire(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1);
  }

  public static ZSpell FireGenericReturn(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    return ZSpell.BaseFire(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1);
  }

  public static void FireShiningBolt(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell zspell = ZSpell.BaseFire(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1);
    if (!c.game.AllowExpansion)
      return;
    zspell.damage += c.familiarLevelOverlight * 2;
  }

  public static void FireConductorRod(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell.BaseFire(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1).target = pos;
  }

  public static void FireFireArrow(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    MyLocation myLocation = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell s = ZSpell.BaseFireTarget(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), myLocation, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power), target, false);
    ZSpell.UpgradeFullFire(c, s);
    s.SetVelocity(myLocation);
  }

  public static void FireGenericArrow(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell.BaseFireTarget(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power), target);
  }

  public static IEnumerator<float> FireSylphArrow(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    MyLocation target)
  {
    for (int i = 0; i < 3; ++i)
    {
      MyLocation power = Inert.Velocity(rot_z + c.game.RandomFixedInt(-5, 5), (FixedInt) theSpell.speedMax);
      ZSpell.BaseFireTarget(theSpell, c, pos, Inert.RotationOfVelocity(power.ToSinglePrecision()), power, (FixedInt) theSpell.speedMax, target);
      for (int e = 0; e < 5; ++e)
        yield return 0.0f;
    }
  }

  public static void FireArcaneBomb(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    MyLocation power1 = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power));
    ZSpell zspell = ZSpell.BaseFireTarget(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), power1, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power), target);
    if (!c.HasArcaneEnergizer)
      return;
    zspell.damage += 5;
  }

  public static void FireArcaneArrows(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    int amount = c.FullArcane ? 10 : 4;
    c.game.ongoing.RunSpell(ZSpell.IEnumeratorArcaneArrows(theSpell, c, pos, Inert.RotationOfVelocity(Inert.Velocity(rot_z).ToSinglePrecision()), rot_z, power, amount, target, c.HasArcaneEnergizer ? (int) ((FixedInt) theSpell.damage * (FixedInt) 1572864L) : 0));
  }

  public static void FireSuperNova(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    bool armageddon = false)
  {
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    ZComponent.Instantiate<GameObject>(ClientResources.Instance.spaceshipObj, new Vector3(-1000f, 1000f, 0.0f), Quaternion.identity, c.game.GetMapTransform());
    zeffector.game = c.game;
    zeffector.fromArmageddon = armageddon;
    zeffector.whoSummoned = c;
    zeffector.position = target;
    c.effectors.Add(zeffector);
    zeffector.active = false;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
  }

  public static void FireVolcano(Spell theSpell, ZCreature c, MyLocation target, bool armageddon = false)
  {
    target.y = (FixedInt) -32;
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    zeffector.game = c.game;
    zeffector.fromArmageddon = armageddon;
    zeffector.whoSummoned = c;
    zeffector.position = target;
    c.effectors.Add(zeffector);
    zeffector.active = false;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
  }

  public static void FireShootingStars(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    bool armageddon = false)
  {
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    zeffector.variable = !ZSpell.Controllable(theSpell, c.game) ? (c.game.RandomInt(0, 2) == 1 ? -1 : 1) : ((double) c.transformscale > 0.0 ? 1 : -1);
    zeffector.game = c.game;
    zeffector.fromArmageddon = armageddon;
    zeffector.whoSummoned = c;
    zeffector.position = target;
    c.effectors.Add(zeffector);
    zeffector.active = false;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
  }

  public static void FireStorm(
    Spell theSpell,
    ZCreature c,
    MyLocation target,
    int offset = 300,
    bool armageddon = false)
  {
    target.y = (FixedInt) (c.map.Height + offset);
    ZEffector zeffector = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    zeffector.fromArmageddon = armageddon;
    zeffector.game = c.game;
    if ((ZComponent) zeffector.collider != (object) null)
    {
      zeffector.collider.world = c.world;
      zeffector.collider.Initialize(target, c.world);
    }
    zeffector.whoSummoned = c;
    c.effectors.Add(zeffector);
    zeffector.active = false;
    zeffector.position = target;
    zeffector.TurnPassed(c.effectors.Count - 1, false, false);
    if (!c.familiar.Has(FamiliarType.Storm) || c.familiarLevelStorm <= 0)
      return;
    foreach (SpellSlot spell in c.spells)
    {
      if (spell.spell.spellEnum == SpellEnum.Storm)
      {
        spell.SetTurnFired = spell.LastTurnFired - c.familiarLevelStorm;
        break;
      }
    }
  }

  public static IEnumerator<float> IEnumeratorArcaneArrows(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    int amount,
    MyLocation target,
    int dmgOverride = 0,
    bool armageddon = false)
  {
    FixedInt speed = Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax, power);
    for (int i = 0; i < amount; ++i)
    {
      FixedInt fixedInt = speed + c.game.RandomFixedInt(-3145728L, 1048576L);
      ZSpell zspell = ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-94371840L, 94371840L) / (power * 90), fixedInt), fixedInt, target);
      zspell.fromArmageddon = armageddon;
      if (dmgOverride > 0)
        zspell.damage = dmgOverride;
      zspell.maxTargetFrames += c.game.RandomInt(-5, 5);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
      if (amount < 7)
      {
        yield return 0.0f;
        yield return 0.0f;
        yield return 0.0f;
      }
    }
  }

  public static void FireQuake(Spell theSpell, ZCreature cre)
  {
    cre.game.ongoing.RunSpell(ZSpell.IEQuake(theSpell, cre));
  }

  public static IEnumerator<float> IEQuake(Spell theSpell, ZCreature cre)
  {
    bool right = false;
    ZGame game = cre.game;
    if (game.isClient)
      CameraShake.instance?.Shake(5f, 5f);
    for (int i = 0; i < 17; ++i)
    {
      try
      {
        for (int index1 = game._playersExtended.Count - 1; index1 >= 0; --index1)
        {
          if (index1 < game._playersExtended.Count)
          {
            ZPerson zperson = game._playersExtended[index1];
            for (int index2 = zperson.controlled.Count - 1; index2 >= 0 && index2 < zperson.controlled.Count; --index2)
            {
              ZCreature zcreature1 = zperson.controlled[index2];
              if (!ZComponent.IsNull((ZComponent) zcreature1) && !((ZComponent) zcreature1.mount != (object) null) && !zcreature1.inWater)
              {
                if ((ZComponent) zcreature1.tower != (object) null)
                {
                  if (game.isClient && !game.resyncing && (UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null)
                    ZComponent.Instantiate<GameObject>(theSpell.explosion, zcreature1.position.ToSinglePrecision() + new Vector2(right ? (float) zcreature1.radius : (float) -zcreature1.radius, (float) (-zcreature1.radius - zcreature1.tower.playerOffsetY)), Quaternion.identity);
                  if (zcreature1.tower.type == TowerType.Seasons)
                  {
                    Spell fromSpell = zcreature1.tower.baseTower.FromSpell;
                    MyLocation position = zcreature1.position;
                    FixedInt fixedInt = (FixedInt) (right ? 45 : 135);
                    for (int index3 = 0; index3 < 2; ++index3)
                    {
                      MyLocation power = right ? Inert.Velocity(game.RandomFixedInt(fixedInt - 20, fixedInt + 45), game.RandomInt(8, 20)) : Inert.Velocity(game.RandomFixedInt(fixedInt - 45, fixedInt + 20), game.RandomInt(8, 20));
                      ZSpell.BaseFire(fromSpell, zcreature1, position + new MyLocation(game.RandomFixedInt(-15, 15), game.RandomFixedInt(-15, 15)), Quaternion.identity, power);
                    }
                  }
                  else if (zcreature1.tower.type == TowerType.Holiday)
                  {
                    Spell component = theSpell.toSummon.GetComponent<Spell>();
                    MyLocation position = zcreature1.position;
                    FixedInt fixedInt = (FixedInt) (right ? 45 : 135);
                    for (int index4 = 0; index4 < 5; ++index4)
                    {
                      MyLocation power = right ? Inert.Velocity(game.RandomFixedInt(fixedInt - 20, fixedInt + 45), game.RandomInt(8, 20)) : Inert.Velocity(game.RandomFixedInt(fixedInt - 45, fixedInt + 20), game.RandomInt(8, 20));
                      ZSpell zspell = ZSpell.BaseFire(component, zcreature1, position + new MyLocation(game.RandomFixedInt(-15, 15), game.RandomFixedInt(-15, 15)), Quaternion.identity, power);
                      if (game.isClient)
                        zspell.name = "Snow Globe";
                    }
                  }
                  if (zcreature1.tower.type != TowerType.Cogs && zcreature1.tower.type != TowerType.Cosmos)
                    zcreature1.ApplyDamage(theSpell.spellEnum, DamageType.None, 4, cre, zcreature1.game.turn);
                }
                else if (zcreature1.flying && !zcreature1.isMoving && !zcreature1.entangledOrGravity)
                  zcreature1.SetVelocity(right || zcreature1.game.RandomInt(0, 10) >= 5 ? zcreature1.game.RandomInt(4, 9) : -zcreature1.game.RandomInt(4, 9), zcreature1.game.RandomInt(2, 5));
                else if ((ZComponent) zcreature1.tower == (object) null && !zcreature1.isMoving)
                {
                  if (game.isClient && !game.resyncing && (UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null)
                    ZComponent.Instantiate<GameObject>(theSpell.explosion, zcreature1.position.ToSinglePrecision() + new Vector2(right ? (float) (zcreature1.radius / 3) : (float) (-zcreature1.radius / 3), (float) -zcreature1.radius), Quaternion.identity);
                  zcreature1.SetVelocity(right ? -zcreature1.game.RandomInt(5, 10) : zcreature1.game.RandomInt(5, 10), zcreature1.game.RandomInt(3, 8));
                }
                if (zcreature1.GetType() == typeof (ZCreatureBeehive))
                {
                  List<ZCreature> bees = ((ZCreatureBeehive) zcreature1).bees;
                  for (int index5 = bees.Count - 1; index5 >= 0 && !ZComponent.IsNull((ZComponent) zcreature1); --index5)
                  {
                    if (!ZComponent.IsNull((ZComponent) bees[index5]))
                    {
                      if (bees[index5].isMoving)
                      {
                        bees[index5].addVelocity = true;
                        ZCreature zcreature2 = bees[index5];
                        zcreature2.addedVelocity = zcreature2.addedVelocity + new MyLocation(right ? zcreature1.game.RandomInt(1, 2) : -zcreature1.game.RandomInt(1, 2), zcreature1.game.RandomInt(0, 2));
                      }
                      else
                        bees[index5].SetVelocity(right || zcreature1.game.RandomInt(0, 10) >= 5 ? zcreature1.game.RandomInt(4, 9) : -zcreature1.game.RandomInt(4, 9), zcreature1.game.RandomInt(2, 5));
                    }
                  }
                }
              }
            }
          }
          else
            break;
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
      right = !right;
      for (int e = 0; e < 7; ++e)
        yield return 0.0f;
    }
  }

  public static void FireBreeze(Spell theSpell, ZCreature cre)
  {
    cre.game.ongoing.RunSpell(ZSpell.IEBreeze(theSpell, cre));
  }

  public static IEnumerator<float> IEBreeze(Spell theSpell, ZCreature cre)
  {
    bool right = (double) cre.transformscale > 0.0;
    int max = 45 + cre.familiarLevelSeasons * 5;
    List<ZSpell> napalm = new List<ZSpell>();
    for (int index1 = cre.game._playersExtended.Count - 1; index1 >= 0 && index1 < cre.game._playersExtended.Count; --index1)
    {
      ZPerson zperson = cre.game._playersExtended[index1];
      for (int index2 = zperson.controlled.Count - 1; index2 >= 0 && index2 < zperson.controlled.Count; --index2)
      {
        ZCreature zcreature = zperson.controlled[index2];
        for (int index3 = 0; index3 < zcreature.effectors.Count; ++index3)
        {
          if ((ZComponent) zcreature.effectors[index3] != (object) null && (ZComponent) zcreature.effectors[index3].collider != (object) null && (ZComponent) zcreature.effectors[index3].collider.spell != (object) null && zcreature.effectors[index3].collider.gameObjectLayer == 9)
          {
            ZSpell spell = zcreature.effectors[index3].spell;
            if (!ZComponent.IsNull((ZComponent) spell) && spell.spellEnum == SpellEnum.Autumn_Leaves)
              napalm.Add(spell);
          }
        }
      }
    }
    if (cre.game.isClient)
    {
      GameObject t = ZComponent.Instantiate<GameObject>(theSpell.explosion, new Vector3((float) (cre.map.Width / 2), (float) (cre.map.Height / 2)), Quaternion.Euler(0.0f, 0.0f, (double) cre.transformscale > 0.0 ? 0.0f : 180f), cre.game.GetMapTransform());
      float x = (float) cre.map.Width / 30f;
      float y = (float) cre.map.Height / 30f;
      float num = (float) (max * 3) / 30f;
      ParticleSystem component1 = t.GetComponent<ParticleSystem>();
      ParticleSystem.ShapeModule shape1 = component1.shape;
      ParticleSystem.MainModule main = component1.main with
      {
        duration = num - 3f
      };
      shape1.scale = new Vector3(x, y);
      ParticleSystem component2 = t.transform.GetChild(0).GetComponent<ParticleSystem>();
      ParticleSystem.ShapeModule shape2 = component2.shape;
      main = component2.main with { duration = num - 3f };
      shape2.scale = new Vector3(x, y);
      component1.Play();
      ZComponent.Destroy<GameObject>(t, num + 2f);
    }
    ZGame game = cre.game;
    for (int i = 0; i < max; ++i)
    {
      try
      {
        for (int index4 = cre.game._playersExtended.Count - 1; index4 >= 0 && index4 < cre.game._playersExtended.Count; --index4)
        {
          ZPerson zperson = cre.game._playersExtended[index4];
          for (int index5 = zperson.controlled.Count - 1; index5 >= 0 && index5 < zperson.controlled.Count; --index5)
          {
            ZCreature z = zperson.controlled[index5];
            if (!ZComponent.IsNull((ZComponent) z) && !((ZComponent) z.mount != (object) null) && !z.inWater && !((ZComponent) z.tower != (object) null))
            {
              if (z.flying && !z.isMoving && !z.entangledOrGravity)
              {
                if ((z.position.x < z.radius + 3 || z.position.x > z.map.Width - z.radius - 3) && z.shield <= 0 && z.invulnerable <= 0 && !z.shiningPower && !z.permenantFlight && !z.isPawn && !z.phantom)
                  z.RemoveFlight();
                if (z.shield == 0 && z.invulnerable <= 0)
                {
                  if (z.isMoving)
                  {
                    z.addVelocity = true;
                    ZCreature zcreature = z;
                    zcreature.addedVelocity = zcreature.addedVelocity + new MyLocation(right ? z.game.RandomInt(1, 2) : -z.game.RandomInt(1, 2), z.game.RandomInt(0, 2));
                  }
                  else
                    z.SetVelocityEitherDir(right ? z.game.RandomInt(4, 9) : -z.game.RandomInt(4, 9), z.game.RandomInt(-3, 5));
                }
                else
                  z.SetVelocityEitherDir(right ? z.game.RandomInt(4, 9) : -z.game.RandomInt(4, 9), z.game.RandomInt(-3, 5));
              }
              else if (z.isMoving)
              {
                z.addVelocity = true;
                ZCreature zcreature = z;
                zcreature.addedVelocity = zcreature.addedVelocity + new MyLocation(right ? z.game.RandomInt(1, 2) : -z.game.RandomInt(1, 2), z.game.RandomInt(0, 2));
              }
              else
                z.SetVelocity(right ? z.game.RandomInt(2, 5) : -z.game.RandomInt(2, 5), z.game.RandomInt(2, 10));
              if (z.GetType() == typeof (ZCreatureBeehive))
              {
                List<ZCreature> bees = ((ZCreatureBeehive) z).bees;
                for (int index6 = bees.Count - 1; index6 >= 0 && !ZComponent.IsNull((ZComponent) z); --index6)
                {
                  if (!ZComponent.IsNull((ZComponent) bees[index6]))
                  {
                    if (bees[index6].isMoving)
                    {
                      bees[index6].addVelocity = true;
                      ZCreature zcreature = bees[index6];
                      zcreature.addedVelocity = zcreature.addedVelocity + new MyLocation(right ? z.game.RandomInt(1, 2) : -z.game.RandomInt(1, 2), z.game.RandomInt(0, 2));
                    }
                    else
                      bees[index6].SetVelocityEitherDir(right ? z.game.RandomInt(4, 9) : -z.game.RandomInt(4, 9), z.game.RandomInt(-3, 5));
                  }
                }
              }
            }
          }
        }
        foreach (ZSpell z in napalm)
        {
          if (!ZComponent.IsNull((ZComponent) z) && !z.isDead)
          {
            MyLocation myLocation;
            if (z.spellEnum == SpellEnum.Autumn_Leaves)
            {
              myLocation = new MyLocation(right ? game.RandomInt(4, 9) : -game.RandomInt(4, 9), game.RandomInt(-3, 5));
            }
            else
            {
              z.explodeOnImpact = false;
              myLocation = new MyLocation(right ? game.RandomInt(4, 9) : -game.RandomInt(4, 9), game.RandomInt(-3, 5));
            }
            z.velocity = myLocation;
            z.StartMoving(false);
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex);
      }
      for (int e = 0; e < 3; ++e)
        yield return 0.0f;
    }
  }

  public static void FireVineBridge(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    int spellSlot)
  {
    pos = c.position;
    int force = (int) theSpell.speedMax + (int) theSpell.speedMax / 5 * c.familiarLevelNature;
    MyLocation myLocation = Inert.Velocity(rot_z, force);
    Coords start = new Coords((int) pos.x, (int) pos.y);
    Coords end = new Coords((int) (pos.x + myLocation.x), (int) (pos.y + myLocation.y));
    Coords coords = c.map.bresenhamsLineCast(start, end, c, (ZSpell) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    if (coords != null)
      c.game.ongoing.RunSpell(ZSpell.paintBridge(theSpell, c, c.game.world, spellSlot, start.x, coords.x, start.y, coords.y));
    else
      ZSpell.RefundCast(c, spellSlot, theSpell.spellEnum);
  }

  public static void RefundCast(ZCreature c, int spellSlot, SpellEnum spellEnum)
  {
    if (ZComponent.IsNull((ZComponent) c))
      return;
    --c.parent.spellsCastTHISTurn;
    if (spellSlot >= 0 && spellSlot < c.spells.Count && (UnityEngine.Object) c.spells[spellSlot].spell != (UnityEngine.Object) null && c.spells[spellSlot].spell.spellEnum == spellEnum)
      c.spells[spellSlot].RefundAUse();
    else
      ZSpell.GiveZSpell(c, spellEnum);
  }

  public static void AddUseIfInBook(ZCreature c, SpellEnum spellEnum)
  {
    if (ZComponent.IsNull((ZComponent) c))
      return;
    for (int index = 0; index < c.spells.Count; ++index)
    {
      if (c.spells[index].spell.spellEnum == spellEnum && !c.spells[index].isPresent && c.spells[index].UsedUses > 0)
      {
        c.spells[index].RefundAUse();
        break;
      }
    }
  }

  public static void GiveZSpell(ZCreature c, SpellEnum spellEnum)
  {
    int index = spellEnum.Index();
    if (index < 0)
      return;
    SpellSlot spellSlot = new SpellSlot(Inert.Instance.spells[index]);
    spellSlot.MaxUses = 1;
    spellSlot.isPresent = true;
    if (spellEnum == SpellEnum.Enchanted_Axes)
      spellSlot.bonusDmg = c.parent.yourTurn ? c.parent.localTurn : c.parent.localTurn + 1;
    c.spells.Add(spellSlot);
  }

  public static IEnumerator<float> paintBridge(
    Spell theSpell,
    ZCreature c,
    ZMyWorld world,
    int spellSlot,
    int startX,
    int endX,
    int startY,
    int endY)
  {
    bool paintedAtleastOne = false;
    startY -= 17;
    if (startX > endX)
      endX -= 6;
    else
      endX += 6;
    int dx = endX - startX;
    int dy = endY - startY;
    int i = 0;
    if (dx < 0)
    {
      dx = -dx;
      dy = -dy;
    }
    int px;
    if (FixedInt.Abs((FixedInt) dy) < dx)
    {
      if (startX > endX)
      {
        for (px = dx; px > 0; px -= 11)
        {
          int num1 = (int) FixedInt.Abs((FixedInt) (dx / 2 - px));
          int num2 = dy * px / dx;
          int num3 = startX + px - dx - 8;
          int num4 = startY + (endY - (startY - num2 - 8)) - (dx * dx / 4 - num1 * num1) / (4 * dx) * 2;
          if (!world.OverlapCircle((Point) new MyLocation(num3, num4), 8, (ZMyCollider) null, Inert.mask_movement_NoEffector))
          {
            world.map.ServerBitBlt(42 + i, num3, num4, false);
            ZSpell.RemoveItemsOnBitBlt(c.game, num3, num4, 8);
            paintedAtleastOne = true;
          }
          ++i;
          if (i > 3)
            i = 0;
          yield return 0.0f;
        }
      }
      else
      {
        for (px = 0; px < dx; px += 11)
        {
          int num5 = (int) FixedInt.Abs((FixedInt) (dx / 2 - px));
          int num6 = dy * px / dx;
          int num7 = startX + px - 8;
          int num8 = startY + num6 - 8 - (dx * dx / 4 - num5 * num5) / (4 * dx) * 2;
          if (!world.OverlapCircle((Point) new MyLocation(num7, num8), 8, (ZMyCollider) null, Inert.mask_movement_NoEffector))
          {
            world.map.ServerBitBlt(42 + i, num7, num8, false);
            ZSpell.RemoveItemsOnBitBlt(c.game, num7, num8, 8);
            paintedAtleastOne = true;
          }
          ++i;
          if (i > 3)
            i = 0;
          yield return 0.0f;
        }
      }
    }
    else
    {
      if (dy < 0)
        dy = -dy;
      FixedInt multiplier = (FixedInt) (FixedInt.Abs((FixedInt) dy) / 2 < FixedInt.Abs((FixedInt) dx) ? 2097152L : 524288L);
      if (startY > endY)
      {
        for (px = dy; px > 0; px -= 11)
        {
          int num9 = (int) FixedInt.Abs((FixedInt) (dy / 2 - px));
          int num10 = dx * px / dy;
          int num11 = endX + num10 - 8;
          int num12 = startY + (endY - startY + px - 8);
          int num13 = startX <= endX ? endX - num10 - 8 - (int) ((FixedInt) ((dy * dy / 4 - num9 * num9) / (4 * dy)) * multiplier) : num11 + (int) ((FixedInt) ((dy * dy / 4 - num9 * num9) / (4 * dy)) * multiplier);
          if (!world.OverlapCircle((Point) new MyLocation(num13, num12), 8, (ZMyCollider) null, Inert.mask_movement_NoEffector))
          {
            world.map.ServerBitBlt(42 + i, num13, num12, false);
            ZSpell.RemoveItemsOnBitBlt(c.game, num13, num12, 8);
            paintedAtleastOne = true;
          }
          ++i;
          if (i > 3)
            i = 0;
          yield return 0.0f;
        }
      }
      else
      {
        for (px = 0; px < dy; px += 11)
        {
          int num14 = (int) FixedInt.Abs((FixedInt) (dy / 2 - px));
          int num15 = dx * px / dy;
          int num16 = startX - num15 - 8;
          int num17 = startY + px - 8;
          int num18 = startX <= endX ? startX + num15 - 8 + (int) ((FixedInt) ((dy * dy / 4 - num14 * num14) / (4 * dy)) * multiplier) : num16 - (int) ((FixedInt) ((dy * dy / 4 - num14 * num14) / (4 * dy)) * multiplier);
          if (!world.OverlapCircle((Point) new MyLocation(num18, num17), 8, (ZMyCollider) null, Inert.mask_movement_NoEffector))
          {
            world.map.ServerBitBlt(42 + i, num18, num17, false);
            ZSpell.RemoveItemsOnBitBlt(c.game, num18, num17, 8);
            paintedAtleastOne = true;
          }
          ++i;
          if (i > 3)
            i = 0;
          yield return 0.0f;
        }
      }
    }
    if (!paintedAtleastOne)
      ZSpell.RefundCast(c, spellSlot, theSpell.spellEnum);
  }

  public static IEnumerator<float> IEnumeratorFireHurricane(
    Spell theSpell,
    ZCreature cre,
    bool destroyCreature)
  {
    ZGame game = cre.game;
    GameObject exo = cre.game.isClient ? ZComponent.Instantiate<GameObject>(theSpell.explosion, cre.position.ToSinglePrecision(), Quaternion.identity, cre.game.GetMapTransform()) : (GameObject) null;
    List<ZMyCollider> zmyColliderList = cre.game.world.OverlapCircleAll((Point) cre.position, 300, (ZMyCollider) null, 66304);
    List<ZCreature> listCreatures = new List<ZCreature>();
    List<ZSpell> napalm = new List<ZSpell>();
    if (zmyColliderList.Count > 0)
    {
      for (int index = zmyColliderList.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) zmyColliderList[index] != (object) cre.colliderGameObject)
        {
          if (zmyColliderList[index].gameObjectLayer == 9)
          {
            ZSpell spell = zmyColliderList[index].spell;
            if (!ZComponent.IsNull((ZComponent) spell) && (spell.spellEnum == SpellEnum.Napalm || spell.spellEnum == SpellEnum.Napalm_Bomb || spell.spellEnum == SpellEnum.Autumn_Leaves))
              napalm.Add(spell);
          }
          else if (zmyColliderList[index].gameObjectLayer != 13)
            listCreatures.Add(zmyColliderList[index].creature);
        }
      }
    }
    MyLocation vel;
    int i;
    int e;
    for (i = 0; i < 6 && !ZComponent.IsNull((ZComponent) cre) && !cre.isDead; ++i)
    {
      foreach (ZCreature z in listCreatures)
      {
        if (!ZComponent.IsNull((ZComponent) z))
        {
          vel = z.entangledOrGravity ? new MyLocation(0, 3) : new MyLocation(cre.position.x - z.position.x > 0 ? (z.velocity.x < 2 ? 1 : 0) : (z.velocity.x > -2 ? -1 : 0), 10);
          if (z.isMoving)
          {
            ZCreature zcreature = z;
            zcreature.addedVelocity = zcreature.addedVelocity + vel;
            z.addVelocity = true;
          }
          else
          {
            z.velocity = vel;
            z.StartMoving(false);
          }
        }
      }
      foreach (ZSpell z in napalm)
      {
        if (!ZComponent.IsNull((ZComponent) z) && !z.isDead)
        {
          if (z.spellEnum == SpellEnum.Autumn_Leaves)
          {
            vel = new MyLocation(game.RandomFixedInt(-10, 10), game.RandomFixedInt(0, 15));
          }
          else
          {
            z.explodeOnImpact = false;
            vel = new MyLocation(game.RandomFixedInt(-30, 30), game.RandomFixedInt(0, 30));
          }
          z.velocity = vel;
          z.StartMoving(false);
        }
      }
      for (e = 0; e < 10; ++e)
        yield return 0.0f;
    }
    if (cre.game.isClient)
    {
      exo.transform.GetChild(0).gameObject.SetActive(false);
      exo.transform.GetChild(1).gameObject.SetActive(true);
    }
    vel = new MyLocation(0, 10);
    for (i = 0; i < 6 && !ZComponent.IsNull((ZComponent) cre) && !cre.isDead; ++i)
    {
      foreach (ZCreature z in listCreatures)
      {
        if (!ZComponent.IsNull((ZComponent) z) && !z.entangledOrGravity)
        {
          vel.x = -z.velocity.x;
          if (z.isMoving)
          {
            ZCreature zcreature = z;
            zcreature.addedVelocity = zcreature.addedVelocity + vel;
            z.addVelocity = true;
          }
          else
          {
            z.velocity = vel;
            z.StartMoving(false);
          }
        }
      }
      foreach (ZSpell z in napalm)
      {
        if (!ZComponent.IsNull((ZComponent) z) && !z.isDead)
        {
          if (z.spellEnum == SpellEnum.Autumn_Leaves)
          {
            vel = new MyLocation(game.RandomFixedInt(-10, 10), game.RandomFixedInt(0, 15));
          }
          else
          {
            z.explodeOnImpact = false;
            vel = new MyLocation(game.RandomFixedInt(-30, 30), game.RandomFixedInt(0, 30));
          }
          z.velocity = vel;
          z.StartMoving(false);
        }
      }
      for (e = 0; e < 10; ++e)
        yield return 0.0f;
    }
    if (ZComponent.IsNull((ZComponent) cre) || cre.isDead)
    {
      if ((UnityEngine.Object) exo != (UnityEngine.Object) null)
        ZComponent.Destroy<GameObject>(exo, 1f);
    }
    else
    {
      if (cre.game.isClient)
      {
        exo.transform.GetChild(1).gameObject.SetActive(false);
        exo.transform.GetChild(2).gameObject.SetActive(true);
      }
      foreach (ZCreature z in listCreatures)
      {
        if (!ZComponent.IsNull((ZComponent) z))
        {
          vel = z.entangledOrGravity ? new MyLocation(0, 3) : (z.position - cre.position).normalized * 30;
          if (FixedInt.Abs(vel.x) < 12 && !z.entangledOrGravity)
            vel.x = (FixedInt) (vel.x > 0 ? 9 : -9);
          if (z.isMoving)
          {
            ZCreature zcreature = z;
            zcreature.addedVelocity = zcreature.addedVelocity + vel;
            z.addVelocity = true;
          }
          else
          {
            z.velocity = vel;
            z.StartMoving(false);
          }
        }
      }
      foreach (ZSpell z in napalm)
      {
        if (!ZComponent.IsNull((ZComponent) z) && !z.isDead)
        {
          if (z.spellEnum != SpellEnum.Autumn_Leaves)
            z.explodeOnImpact = false;
          vel = new MyLocation((FixedInt) (cre.position.x - z.position.x > 0 ? -6 : 6), game.RandomFixedInt(6, 11));
          z.velocity = vel;
          z.StartMoving(false);
        }
      }
      if ((UnityEngine.Object) exo != (UnityEngine.Object) null)
        ZComponent.Destroy<GameObject>(exo, 1f);
      if (destroyCreature)
      {
        for (i = 0; i < 30; ++i)
          yield return 0.0f;
        cre.OnDeath(true);
      }
    }
  }

  public static IEnumerator<float> IEnumeratorBrineBolt(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    for (int i = 0; i < theSpell.amount; ++i)
    {
      FixedInt fixedInt = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-2097152L, 2097152L);
      ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-8388608L, 8388608L), fixedInt), fixedInt, target);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorSandBlast(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.turnFriendlyDmg = -2;
    if (c.game.isClient)
      SandPool.Create();
    for (int i = 0; i < theSpell.amount; ++i)
    {
      for (int index = 0; index < 100; ++index)
      {
        if (ZComponent.IsNull((ZComponent) c) || c.turnFriendlyDmg == c.game.turn)
        {
          yield break;
        }
        else
        {
          FixedInt force = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-2097152L, 2097152L);
          c.game.ongoing.RunSpell(ZSpellSand.SandMove(theSpell, c.game, c, pos, Inert.Velocity(rot_z + c.game.RandomFixedInt(-2097152L, 2097152L), force)));
        }
      }
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorImbuedAxes(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    SpellSlot slot)
  {
    c.turnFriendlyDmg = -2;
    MyLocation power1 = Inert.Velocity(target.x, (FixedInt) theSpell.speedMax);
    ZSpell zspell1 = ZSpell.BaseFire(theSpell, c, pos, rot, power1, false);
    zspell1.velocity = power1;
    zspell1.moving = c.game.ongoing.RunSpell(zspell1.SpellMove(checkEffectors: false));
    if (slot != null && slot.bonusDmg > 0)
      zspell1.damage += Mathf.Min((c.parent.localTurn - slot.bonusDmg) * 2, 30 + c.familiarLevelDruidism * 2);
    if ((UnityEngine.Object) zspell1.transform != (UnityEngine.Object) null && zspell1.velocity.x > 0)
    {
      zspell1.transform.localScale = new Vector3(-1f, 1f, 1f);
      zspell1.gameObject.GetComponent<RotateTransformZ>().speed = -500f;
    }
    for (int i = 0; i < 15; ++i)
      yield return 0.0f;
    if (!ZComponent.IsNull((ZComponent) c) && !c.isDead && c.turnFriendlyDmg != c.game.turn)
    {
      MyLocation power2 = Inert.Velocity(rot_z, (FixedInt) theSpell.speedMax);
      ZSpell zspell2 = ZSpell.BaseFire(theSpell, c, pos, rot, power2, false);
      zspell2.velocity = power2;
      zspell2.moving = c.game.ongoing.RunSpell(zspell2.SpellMove(checkEffectors: false));
      if (slot != null && slot.bonusDmg > 0)
        zspell2.damage += Mathf.Min((c.parent.localTurn - slot.bonusDmg) * 2, 30 + c.familiarLevelDruidism * 2);
      if ((UnityEngine.Object) zspell2.transform != (UnityEngine.Object) null && zspell2.velocity.x > 0)
      {
        zspell2.transform.localScale = new Vector3(-1f, 1f, 1f);
        zspell2.gameObject.GetComponent<RotateTransformZ>().speed = -500f;
      }
    }
    if (slot != null)
      slot.bonusDmg = c.parent.localTurn + 1;
  }

  public static IEnumerator<float> IEnumeratorRockBlaster(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    FixedInt? accuracy = null)
  {
    FixedInt acc = accuracy.HasValue ? accuracy.Value : (FixedInt) 2097152L;
    c.turnFriendlyDmg = -2;
    FixedInt force1 = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-acc, acc);
    ZSpell.BaseFire(theSpell, c, pos, rot, Inert.Velocity(target.x + c.game.RandomFixedInt(-acc, acc), force1));
    for (int i = 0; i < 15; ++i)
      yield return 0.0f;
    if (!ZComponent.IsNull((ZComponent) c) && !c.isDead && c.turnFriendlyDmg != c.game.turn)
    {
      FixedInt force2 = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-acc, acc);
      ZSpell.BaseFire(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-acc, acc), force2));
    }
  }

  public static IEnumerator<float> IEnumeratorPebbleShot(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.turnFriendlyDmg = -2;
    for (int i = 0; i < theSpell.amount && !ZComponent.IsNull((ZComponent) c) && c.turnFriendlyDmg != c.game.turn; ++i)
    {
      FixedInt fixedInt = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-2097152L, 2097152L);
      ZSpell zspell = ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-2097152L, 2097152L), fixedInt), fixedInt, target);
      if (c.game.isClient)
        zspell.name = theSpell.name;
      if (c.game.isClient && theSpell.spellEnum == SpellEnum.Color_Spray && (UnityEngine.Object) zspell.gameObject != (UnityEngine.Object) null)
        zspell.gameObject.GetComponent<SpriteRenderer>().color = ColorHSV.ToColor(new ColorHSV(UnityEngine.Random.Range(0.0f, 1f), 1f, 1f));
      if (c.game.isClient && !c.game.resyncing && theSpell.spellEnum == SpellEnum.Pebble_Shot)
        AudioManager.Play(theSpell.castClip);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static void FireApparition(ZCreature c, bool unlimited = false, bool stayVisible = false)
  {
    c.phantom = true;
    c.flying = true;
    c.collider.gameObjectLayer = 16;
    c.collider.layer = 65536;
    if (!unlimited)
      c.effectors.Add(new ZEffector()
      {
        game = c.game,
        whoSummoned = c,
        type = EffectorType.Apparition,
        MaxTurnsAlive = 5
      });
    else
      c.RemoveEffector(EffectorType.Apparition);
    c.effectors.Add(new ZEffector()
    {
      game = c.game,
      whoSummoned = c,
      active = false,
      type = EffectorType.Phantom,
      MaxTurnsAlive = 5
    });
    if (c.game.isClient)
    {
      ColorLerp colorLerp = c.gameObject.GetComponent<ColorLerp>();
      if ((UnityEngine.Object) colorLerp == (UnityEngine.Object) null)
        colorLerp = c.gameObject.AddComponent<ColorLerp>();
      colorLerp.rends = ((IEnumerable<SpriteRenderer>) c.gameObject.GetComponentsInChildren<SpriteRenderer>()).ToList<SpriteRenderer>();
      colorLerp._creature = c;
      if (unlimited && !stayVisible)
      {
        colorLerp.min = 0.0f;
        colorLerp.max = 0.2f;
      }
      else
      {
        colorLerp.min = 0.2f;
        colorLerp.max = 0.5f;
      }
      for (int index = colorLerp.rends.Count - 1; index >= 0; --index)
      {
        if ((UnityEngine.Object) colorLerp.rends[index].transform?.parent?.GetComponent<Effector>() != (UnityEngine.Object) null)
          colorLerp.rends.RemoveAt(index);
      }
      for (int index = colorLerp.rends.Count - 1; index >= 0; --index)
      {
        if (colorLerp.rends[index].gameObject.layer == 14)
          colorLerp.rends.RemoveAt(index);
      }
    }
    c.CreatureMoveSurroundings();
  }

  public static ZCreature CreateDuplicate(
    ZCreature c,
    ZCreature col,
    MyLocation pos,
    bool armageddon = false)
  {
    ZCreature c1 = ZCreatureCreate.Clone(c.game, col, c.game.GetMapTransform(), pos);
    c1.spellEnum = col.spellEnum;
    c1.game = c.game;
    c1.daOriginalTrueParent = col.daOriginalTrueParent ?? col.parent;
    SpellSlot spellSlot = c1.GetSpellSlot(SpellEnum.Duplication);
    if (spellSlot != null && spellSlot.UsedUses == 0)
      spellSlot.IncreaseUses();
    if (col.phantom && !col.baseCreature.phantom)
    {
      ZEffector zeffector1 = col.effectors.Find((Predicate<ZEffector>) (et => et.type == EffectorType.Apparition));
      int turnsAlive = (ZComponent) zeffector1 != (object) null ? zeffector1.GetTurnsAlive() : 0;
      if ((ZComponent) zeffector1 != (object) null)
      {
        ZEffector zeffector2 = new ZEffector();
        zeffector2.game = c.game;
        zeffector2.whoSummoned = c1;
        zeffector2.type = EffectorType.Apparition;
        zeffector2.MaxTurnsAlive = zeffector1.MaxTurnsAlive;
        zeffector2.SetTurnsAlive(turnsAlive);
        c1.effectors.Add(zeffector2);
      }
    }
    if (col.race == CreatureRace.Bear)
    {
      ZEffector zeffector3 = col.effectors.Find((Predicate<ZEffector>) (et => et.type == EffectorType.Bear));
      int turnsAlive = (ZComponent) zeffector3 != (object) null ? zeffector3.GetTurnsAlive() : 0;
      if ((ZComponent) zeffector3 != (object) null)
      {
        ZEffector zeffector4 = new ZEffector();
        zeffector4.game = c.game;
        zeffector4.whoSummoned = c1;
        zeffector4.type = EffectorType.Bear;
        zeffector4.MaxTurnsAlive = zeffector3.MaxTurnsAlive;
        zeffector4.SetTurnsAlive(turnsAlive);
        c1.effectors.Add(zeffector4);
        zeffector4.active = false;
        ZSpell.CreateWerewolfObj(Inert.GetSpell(SpellEnum.Bear_Form), c1);
      }
    }
    c1.team = c.team;
    c1.parent = c.parent;
    c1.isPawn = true;
    if (c.game.isClient)
    {
      Player component = c1.gameObject?.GetComponent<Player>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        ZComponent.Destroy<Player>(component);
      if ((UnityEngine.Object) c1.bg != (UnityEngine.Object) null && (UnityEngine.Object) c.bg != (UnityEngine.Object) null)
      {
        Color color = c.bg.color with { a = 1f };
        c1.bg.color = color;
      }
      if ((UnityEngine.Object) c1.miniMapBg != (UnityEngine.Object) null && (UnityEngine.Object) c.miniMapBg != (UnityEngine.Object) null)
        c1.miniMapBg.color = c.miniMapBg.color;
      if ((UnityEngine.Object) c1.txtHealth != (UnityEngine.Object) null && (UnityEngine.Object) c.txtHealth != (UnityEngine.Object) null)
        c1.txtHealth.color = c.txtHealth.color;
      c1.prestigeIndex = col.prestigeIndex;
      if ((UnityEngine.Object) c1.imgPrestige != (UnityEngine.Object) null && (UnityEngine.Object) col.imgPrestige != (UnityEngine.Object) null && col.imgPrestige.gameObject.activeSelf)
        c1.ColorPrestigeHat(col.parent, c1.parent.clientColor);
    }
    if (col.FullArcane && col.parent != null && !col.isPawn)
    {
      for (int index = c1.spells.Count - 1; index >= 0; --index)
      {
        if (c1.spells[index].spell.spellEnum == SpellEnum.Arcane_Dragon)
        {
          c1.spells[index].spell = Inert.Instance.corruptDragon;
          break;
        }
      }
      if (c.game.isClient)
        c1.gameObject.GetComponent<ConfigurePlayer>()?.ModEquipAll(col.parent.name, col.parent.settingsPlayer, ClientResources.Instance.DarkArcane, false);
    }
    else if (col.FullArcane && c.game.isClient)
    {
      AnimateDragon component = c1.gameObject.GetComponent<AnimateDragon>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      {
        component.sprites = Inert.Instance.corruptDragon.toSummon.GetComponent<AnimateDragon>().sprites;
        component.attackSprites = Inert.Instance.corruptDragon.toSummon.GetComponent<AnimateDragon>().attackSprites;
      }
    }
    if ((UnityEngine.Object) c.flightObj != (UnityEngine.Object) null && (UnityEngine.Object) c1.transform != (UnityEngine.Object) null)
    {
      for (int index = 0; index < c1.transform.childCount; ++index)
      {
        if (string.Equals("FlightObj", c1.transform.GetChild(index).tag))
        {
          c1.flightObj = c1.transform.GetChild(index).gameObject;
          break;
        }
      }
    }
    ZSpell.DuplicateMarket(c1, col.minerMarket);
    c.parent.controlled.Add(c1);
    if (c.familiarLevelIllusion == 5)
      c1.health = (int) ((FixedInt) c1.health * (FixedInt) 786432L);
    else
      c1.health /= 2;
    if (c1.health < 1)
      c1.health = 1;
    c1.Start();
    c1.UpdateHealthTxt();
    if (c.familiarLevelIllusion != 5 && !armageddon)
      c1.effectors.Add(new ZEffector()
      {
        game = c.game,
        whoSummoned = c1,
        type = EffectorType.Duplicate,
        MaxTurnsAlive = 5
      });
    c1.collider.enabled = true;
    c1.collider.world = c.world;
    c1.MoveToPosition = pos;
    c.parent.panelPlayer?.SetSummons(c.parent);
    c.CheckEffectorsOnMove();
    if (!armageddon && c1.ShouldFall())
      c1.Fall();
    if (!c1.game.isClient)
      c1.game.SendCreatureSpells(c1, false, false);
    return c1;
  }

  public static IEnumerator<float> IEnumeratorDeluge(
    ZCreature c,
    Spell spell,
    MyLocation pos,
    int minX,
    int maxX,
    int amount)
  {
    for (int i = 0; i < amount; ++i)
    {
      ZSpell.BaseFire(spell, c, new MyLocation(pos.x + c.game.RandomInt(minX, maxX), pos.y), Quaternion.identity, new MyLocation(0, -5));
      if (c.game.isClient)
        AudioManager.Play(AudioManager.instance.waterExplosion);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorBlizzard(
    ZCreature c,
    Spell spell,
    MyLocation pos,
    int minX,
    int maxX,
    int amount,
    bool setGravity = false)
  {
    for (int i = 0; i < amount; ++i)
    {
      ZSpell.BaseFire(spell, c, new MyLocation(pos.x + c.game.RandomInt(minX, maxX), pos.y), Quaternion.identity, new MyLocation((FixedInt) 0, (FixedInt) -10485760L)).velocity.y += c.game.RandomFixedInt(-1, 1);
      if (i % 3 == 0)
        yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorAutumnLeaves(
    ZCreature c,
    Spell spell,
    MyLocation pos,
    int minX,
    int maxX,
    int amount,
    bool extended)
  {
    pos.y = (FixedInt) (c.game.map.Height + 50);
    if (amount > 40)
      amount += c.familiarLevelSeasons * 5;
    else
      amount += c.familiarLevelSeasons;
    for (int i = 0; i < amount; ++i)
    {
      ZSpell zspell = ZSpell.BaseFire(spell, c, new MyLocation(pos.x + c.game.RandomInt(minX, maxX), pos.y), Quaternion.identity);
      zspell.velocity = new MyLocation(0, 5);
      if (extended)
      {
        zspell.Bounces = ZSpellLeaf.colorIndex[c.game.RandomInt(0, ZSpellLeaf.colorIndex.Length)];
        if (c.game.isClient)
        {
          zspell.gameObject.GetComponent<SpriteRenderer>().color = (Color) new Color32((byte) zspell.Bounces, byte.MaxValue, byte.MaxValue, byte.MaxValue);
          ZComponent.Destroy<GameObject>(zspell.transform.GetChild(0).gameObject);
        }
      }
      zspell.moving = zspell.game.ongoing.RunSpell(zspell.SpellMove(), false);
      yield return 0.0f;
    }
  }

  public static void TreeHouse(ZCreature c, Spell spell)
  {
    int num = 10;
    for (int index = 0; index < num; ++index)
    {
      ZSpell zspell = ZSpell.BaseFire(spell, c, c.position, Quaternion.identity);
      zspell.velocity = new MyLocation(c.game.RandomFixedInt(-5, 5), (FixedInt) 5);
      zspell.moving = zspell.game.ongoing.RunSpell(zspell.SpellMove(), false);
      if ((ZComponent) zspell.effector != (object) null)
        zspell.effector.MaxTurnsAlive = 3;
    }
  }

  public static IEnumerator<float> IEnumeratorRisingStar(
    ZCreature c,
    Spell spell,
    MyLocation pos,
    int minX,
    int maxX,
    FixedInt angleMargin,
    int amount)
  {
    for (int i = 0; i < amount; ++i)
    {
      if (c.game.isClient)
        AudioManager.Play(spell.castClip);
      ZSpell.BaseFire(spell, c, new MyLocation(pos.x + (minX < 0 ? c.game.RandomInt(minX, maxX) : 0), pos.y), Quaternion.identity, Inert.Velocity2((FixedInt) 180 + (angleMargin > 0 ? c.game.RandomFixedInt(-angleMargin, angleMargin) : (FixedInt) 0), (FixedInt) 10));
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorStorm(
    ZCreature c,
    MyLocation pos,
    int minX,
    int maxX,
    int amount)
  {
    ZSpell kablam = ZSpell.BaseFireKablam(Inert.Instance.spells["Thunder Shock"], c, pos, Quaternion.identity);
    Spell lightningStrike = Inert.Instance.LightningStrike;
    int startX = (int) pos.x;
    int y = (int) pos.y;
    ZGame game = c.game;
    kablam.damage += (ZComponent.IsNull((ZComponent) c.parent.first()) ? c : c.parent.first()).EffectorCount(EffectorType.Electrostatic_Charge) > 0 ? 1 : 0;
    for (int i = 0; i < amount; ++i)
    {
      int x = startX + c.game.RandomInt(minX, maxX);
      Coords coords = c.map.downCastLightning(x, y, (ZCreature) null, kablam, Inert.mask_spell_movement);
      if (coords != null)
      {
        c.map.ServerBitBlt((int) kablam.explosionCutout, coords.x, coords.y);
        ZSpell.BaseFire(Inert.Instance.LightningStrike, c, coords.ToMyLocation(), Quaternion.identity, Inert.Velocity((FixedInt) c.game.RandomInt(0, 360), 8), isChild: true);
        if (c.game.isClient && !c.game.resyncing)
        {
          c.game.electricityPool.Spawn(new Vector3((float) x, (float) y), coords.ToVector());
          c.game.electricityPool.Spawn(new Vector3((float) x, (float) y), coords.ToVector());
        }
      }
      else if (c.game.isClient && !c.game.resyncing)
      {
        c.game.electricityPool.Spawn(new Vector3((float) x, (float) y), new Vector3((float) x, 0.0f));
        c.game.electricityPool.Spawn(new Vector3((float) x, (float) y), new Vector3((float) x, 0.0f));
      }
      AudioManager.Play(kablam.explosionClip);
      for (int e = 0; e < 6; ++e)
        yield return 0.0f;
      kablam.Undie();
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static IEnumerator<float> IEnumeratorLightningStrike(
    ZCreature c,
    MyLocation pos,
    int minX,
    int maxX,
    int amount,
    bool crisscross = false,
    int botX = 0,
    int botY = 0,
    ZCreature t = null)
  {
    int i;
    for (i = 0; i < 30; ++i)
      yield return 0.0f;
    ZSpell kablam = ZSpell.BaseFireKablam(Inert.Instance.spells["Thunder Shock"], c, pos, Quaternion.identity);
    Spell lightningStrike = Inert.Instance.LightningStrike;
    int startX = (int) pos.x;
    int y = (int) pos.y;
    ZGame game = c.game;
    kablam.damage += (ZComponent.IsNull((ZComponent) c.parent.first()) ? c : c.parent.first()).EffectorCount(EffectorType.Electrostatic_Charge) > 0 ? 1 : 0;
    for (i = 0; i < amount; ++i)
    {
      int num = startX + c.game.RandomInt(minX, maxX);
      Coords coords;
      if (crisscross)
      {
        MyLocation myLocation = Inert.Velocity(Inert.AngleOfVectors(new MyLocation(num, y), ZComponent.IsNull((ZComponent) t) || t.isDead ? new MyLocation(botX, botY) : t.position), c.map.Height + c.map.Width);
        Coords end = new Coords((int) ((FixedInt) num - myLocation.x), (int) ((FixedInt) y - myLocation.y));
        coords = c.map.bresenhamsLineCastLightningStrike(new Coords(num, y), end, (ZCreature) null, kablam, Inert.mask_spell_movement) ?? c.map.downCastLightning(num, y, c, kablam, Inert.mask_spell_movement);
      }
      else
        coords = c.map.downCastLightning(num, y, c, kablam, Inert.mask_spell_movement);
      if (coords != null)
      {
        c.map.ServerBitBlt((int) kablam.explosionCutout, coords.x, coords.y);
        ZSpell.BaseFire(Inert.Instance.LightningStrike, c, coords.ToMyLocation(), Quaternion.identity, Inert.Velocity((FixedInt) c.game.RandomInt(0, 360), 8), isChild: true);
        if (c.game.isClient && !c.game.resyncing)
        {
          c.game.electricityPool.Spawn(new Vector3((float) num, (float) (y + 1000)), coords.ToVector());
          c.game.electricityPool.Spawn(new Vector3((float) (num - 10), (float) (y + 1000)), coords.ToVector());
          c.game.electricityPool.Spawn(new Vector3((float) (num + 10), (float) (y + 1000)), coords.ToVector());
        }
      }
      else if (c.game.isClient && !c.game.resyncing)
      {
        c.game.electricityPool.Spawn(new Vector3((float) num, (float) (y + 1000)), new Vector3((float) num, 0.0f));
        c.game.electricityPool.Spawn(new Vector3((float) (num - 10), (float) (y + 1000)), new Vector3((float) num, 0.0f));
        c.game.electricityPool.Spawn(new Vector3((float) (num + 10), (float) (y + 1000)), new Vector3((float) num, 0.0f));
      }
      int time = c.game.RandomInt(5, 10);
      if (!c.isDead)
      {
        for (int e = 0; e < time; ++e)
          yield return 0.0f;
        if (!ZComponent.IsNull((ZComponent) c) && !c.isDead)
          kablam.Undie();
        else
          break;
      }
      else
        break;
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static IEnumerator<float> IEnumeratorStormDragonBreath(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z)
  {
    MyLocation myLocation = Inert.Velocity(rot_z, c.map.Width + c.map.Height);
    Coords start = new Coords((int) pos.x, (int) pos.y);
    Coords end = new Coords((int) (pos.x + myLocation.x), (int) (pos.y + myLocation.y));
    ZSpell spell = ZSpell.BaseFireKablam(theSpell, c, pos, Quaternion.identity);
    Coords coords = c.map.bresenhamsLineCast(start, end, c, spell, Inert.mask_spell_movement);
    int num = (ZComponent.IsNull((ZComponent) c.parent.first()) ? c : c.parent.first()).EffectorCount(EffectorType.Electrostatic_Charge);
    spell.damage += num > 0 ? 1 : 0;
    if (coords != null)
    {
      c.map.ServerBitBlt((int) theSpell.explosionCutout, coords.x, coords.y);
      if (c.game.isClient && !c.game.resyncing)
      {
        ZComponent.Instantiate<GameObject>(theSpell.explosion, coords.ToVector(), Quaternion.identity);
        c.game.electricityPool.Spawn(start.ToVector(), coords.ToVector());
        c.game.electricityPool.Spawn(start.ToVector(), coords.ToVector());
        c.game.electricityPool.Spawn(start.ToVector(), coords.ToVector());
      }
    }
    else if (c.game.isClient && !c.game.resyncing)
    {
      c.game.electricityPool.Spawn(start.ToVector(), (Vector3) (myLocation + pos).ToSinglePrecision());
      c.game.electricityPool.Spawn(start.ToVector(), (Vector3) (myLocation + pos).ToSinglePrecision());
      c.game.electricityPool.Spawn(start.ToVector(), (Vector3) (myLocation + pos).ToSinglePrecision());
    }
    ZComponent.Destroy<GameObject>(spell.gameObject);
    for (int i = 0; i < 30; ++i)
      yield return 0.0f;
  }

  public static IEnumerator<float> IEnumeratorAbduction(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    bool extended,
    MyLocation target,
    bool armageddon)
  {
    MyLocation myLocation = Inert.Velocity(rot_z, (ZComponent) c.tower != (object) null && c.tower.type == TowerType.Cosmos || c.type == CreatureType.Cosmic_Horror ? 300 : 200);
    Coords start = new Coords((int) pos.x, (int) pos.y);
    Coords end = new Coords((int) (pos.x + myLocation.x), (int) (pos.y + myLocation.y));
    Coords coord = c.map.bresenhamsCircleCast(start, end, armageddon ? (ZCreature) null : c, Inert.mask_spell_movement, theSpell.radius);
    GameObject gameObject = c.game.resyncing ? (GameObject) null : ZComponent.Instantiate<GameObject>(theSpell.explosion, pos.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
    float y1 = start.DistanceSq(coord == null ? end : coord) / 142f;
    if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
    {
      gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0.0f, 0.0f, rot_z.ToFloat() + 90f);
      gameObject.transform.GetChild(0).localScale = new Vector3(1f, y1, 1f);
      Transform ufoTransform = gameObject.GetComponent<ParticleAbduction>().ufoTransform;
      ufoTransform.transform.parent.rotation = Quaternion.Euler(0.0f, 0.0f, rot_z.ToFloat() + 90f);
      ufoTransform.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }
    int abductionLength = 30;
    if ((ZComponent) c.tower != (object) null && c.tower.type == TowerType.Cosmos)
    {
      abductionLength = 60;
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        gameObject.GetComponent<ParticleAbduction>().stage2 = 2f;
    }
    int i;
    for (i = 0; i < 30; ++i)
      yield return 0.0f;
    if (coord != null)
    {
      ZCreature cre = c.game.map.PhysicsCollideCreature(armageddon ? (ZCreature) null : c, coord.x, coord.y);
      if ((ZComponent) cre != (object) null && (ZComponent) cre.tower == (object) null)
      {
        cre.moving = cre.CreAbductionMove();
        cre.isMoving = true;
        if (cre.type != CreatureType.Cosmic_Horror && cre.familiarLevelCosmos < 5 && cre.appliedGravity < cre.parent.localTurn + 1 && !cre.flying && cre.type != CreatureType.Tree && cre.race != CreatureRace.Effector)
        {
          cre.CreateGravityObj();
          cre.appliedGravity = cre.parent.localTurn + 1;
        }
        MyLocation d = -Inert.Velocity(rot_z, 2);
        if (cre.type == CreatureType.Tree)
        {
          d.x = (FixedInt) 0;
          if (d.y == 0)
            yield break;
        }
        List<ZMyCollider> num = cre.type == CreatureType.Jar ? cre.game.world.OverlapCircleAll((Point) cre.position, 14, cre.collider, Inert.mask_ButterflyJar) : (List<ZMyCollider>) null;
        for (i = 0; i < abductionLength && (ZComponent) cre != (object) null && (ZComponent) c != (object) null && !ZComponent.IsNull((ZComponent) cre); ++i)
        {
          MyLocation position = cre.position;
          if (MyLocation.Distance(cre.position, pos) >= cre.radius || cre.type == CreatureType.Tree)
          {
            cre.MoveToPosition = cre.position + d;
            cre.game.CreatureMoveSurroundings(position, cre.radius + 5, cre.collider);
            cre.CreatureMoveSurroundings();
            cre.CheckEffectorsOnMove();
            cre.moving.MoveNext();
            if (num != null && num.Count > 0)
            {
              foreach (ZMyCollider z in num)
              {
                if (!ZComponent.IsNull((ZComponent) z) && (ZComponent) z.creature != (object) null)
                  z.creature.MoveToPosition = z.creature.position + d;
              }
            }
            if (cre.type == CreatureType.Tree)
            {
              Coords[] coordsArray = d.y > 0 ? ZCreatureTree.top_coords : ZCreatureTree.coords;
              int x = (int) cre.position.x;
              int y2 = (int) cre.position.y;
              for (int index = 0; index < coordsArray.Length; ++index)
              {
                if (!cre.map.CheckPositionOnlyEntities(x + coordsArray[index].x, y2 + coordsArray[index].y, cre, Inert.mask_entity_movement))
                  goto label_36;
              }
            }
            else
            {
              List<Coords> outlineArray = MapGenerator.getOutlineArray(cre.radius);
              int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(d) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * outlineArray.Count - cre.radius).ToInt();
              for (int index1 = 0; index1 < cre.radius * 2; ++index1)
              {
                int index2 = (index1 + num1) % outlineArray.Count;
                if (!cre.map.CheckPositionOnlyEntities((int) cre.position.x + outlineArray[index2].x, (int) cre.position.y + outlineArray[index2].y, cre, Inert.mask_entity_movement))
                  goto label_36;
              }
            }
            yield return 0.0f;
          }
          else
            break;
        }
label_36:
        if ((ZComponent) cre != (object) null)
        {
          cre.moving = (IEnumerator<float>) null;
          cre.isMoving = false;
          cre.velocity = d;
          if (!cre.isMoving)
            cre.StartMoving(false);
        }
        num = (List<ZMyCollider>) null;
      }
      cre = (ZCreature) null;
    }
  }

  public static IEnumerator<float> IEnumeratorTentacleSmash(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    bool extended)
  {
    MyLocation myLocation = Inert.Velocity(rot_z, theSpell.maxDuration);
    int amount = theSpell.amount;
    Coords startCoord = new Coords((int) pos.x, (int) pos.y);
    Coords endCoord = new Coords((int) (pos.x + myLocation.x), (int) (pos.y + myLocation.y));
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, pos, Quaternion.identity);
    int offset = extended ? Player.LightningExtend(Player.GetPowerLevel(rot_z)) : 4;
    for (int i = 0; i < amount; ++i)
    {
      Coords start = c.map.bresenhamsLineCast(startCoord, endCoord, c, (ZSpell) null, Inert.mask_spell_movement);
      if (start != null)
      {
        Coords lineByLength = c.map.GetLineByLength(start, endCoord, offset);
        if (lineByLength != null)
        {
          start = lineByLength;
          c.map.ServerBitBlt((int) theSpell.explosionCutout, lineByLength.x, lineByLength.y);
        }
        else
          c.map.ServerBitBlt((int) theSpell.explosionCutout, start.x, start.y);
        kablam.ApplyExplosionForce(start.ToMyLocation());
        if (c.game.isClient && !c.game.resyncing)
          ZComponent.Instantiate<GameObject>(kablam.explosion, start.ToVector(), Quaternion.identity);
      }
      else if (c.game.isClient && !c.game.resyncing)
        ZComponent.Instantiate<GameObject>(kablam.explosion, endCoord.ToVector(), Quaternion.identity);
      yield return 0.0f;
      kablam.Undie();
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static IEnumerator<float> IEnumeratorThunderJolt(Spell theSpell, ZCreature c)
  {
    MyLocation pos = c.position;
    Coords startCoord = new Coords((int) c.position.x, (int) c.position.y);
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, c.position, Quaternion.identity);
    FixedInt fixedInt = (FixedInt) 66;
    int count = 25;
    FixedInt angle = (FixedInt) 360 / count;
    FixedInt offset = angle / 8;
    for (int z = 0; z < 8; ++z)
    {
      for (int index = 0; index < count; ++index)
      {
        FixedInt angle1 = angle * index + offset * z;
        while (angle1 > 360)
          angle1 -= 360;
        MyLocation myLocation = Inert.Velocity(angle1, 100 + c.radius);
        Coords coords = c.map.bresenhamsLineCast(startCoord, new Coords((int) (pos.x + myLocation.x), (int) (pos.y + myLocation.y)), c, (ZSpell) null, Inert.mask_spell_movement);
        if (coords != null)
        {
          c.map.ServerBitBlt((int) theSpell.explosionCutout, coords.x, coords.y);
          kablam.ApplyExplosionForce(coords.ToMyLocation());
          if (c.game.isClient && !c.game.resyncing)
            c.game.electricityPool.Spawn(startCoord.ToVector(), coords.ToVector());
        }
        else if (c.game.isClient && !c.game.resyncing)
          c.game.electricityPool.Spawn(startCoord.ToVector(), (Vector3) (myLocation + pos).ToSinglePrecision());
        kablam.Undie();
      }
      AudioManager.Play(theSpell.explosionClip);
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorThunderShock(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    bool extended)
  {
    MyLocation dir = Inert.Velocity(rot_z, c.map.Width + c.map.Height);
    int amount = theSpell.amount;
    if (c.familiar.Has(FamiliarType.Storm))
      amount += c.familiarLevelStorm;
    Coords startCoord = new Coords((int) pos.x, (int) pos.y);
    Coords endCoord = new Coords((int) (pos.x + dir.x), (int) (pos.y + dir.y));
    ZSpell kablam = ZSpell.BaseFireKablam(theSpell, c, pos, Quaternion.identity);
    kablam.damage += (ZComponent.IsNull((ZComponent) c.parent.first()) ? c : c.parent.first()).EffectorCount(EffectorType.Electrostatic_Charge) > 0 ? 1 : 0;
    Coords lineByLength1 = c.map.GetLineByLength(startCoord, endCoord, 25);
    if (lineByLength1 != null)
    {
      c.map.ServerBitBlt(4, lineByLength1.x, lineByLength1.y);
      startCoord = lineByLength1;
    }
    int offset = extended ? Player.LightningExtend(Player.GetPowerLevel(rot_z)) : 4;
    for (int i = 0; i < amount; ++i)
    {
      Coords start = c.map.bresenhamsLineCast(startCoord, endCoord, c, (ZSpell) null, Inert.mask_spell_movement);
      if (start != null)
      {
        Coords lineByLength2 = c.map.GetLineByLength(start, endCoord, offset);
        if (lineByLength2 != null)
        {
          start = lineByLength2;
          c.map.ServerBitBlt((int) theSpell.explosionCutout, lineByLength2.x, lineByLength2.y);
        }
        else
          c.map.ServerBitBlt((int) theSpell.explosionCutout, start.x, start.y);
        kablam.ApplyExplosionForce(start.ToMyLocation());
        if (c.game.isClient && !c.game.resyncing)
          c.game.electricityPool.Spawn(startCoord.ToVector(), start.ToVector());
      }
      else if (c.game.isClient && !c.game.resyncing)
        c.game.electricityPool.Spawn(startCoord.ToVector(), (Vector3) (dir + pos).ToSinglePrecision());
      if (i % 2 == 0)
        AudioManager.Play(theSpell.explosionClip);
      yield return 0.0f;
      yield return 0.0f;
      kablam.Undie();
    }
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public static IEnumerator<float> IEnumeratorNapalm(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    int amount = theSpell.amount;
    if (amount > 1 && c.familiar.Has(FamiliarType.Flame) && c.familiarLevelFlame > 0)
      amount += (int) ((FixedInt) ZSpell.ClampedFlameLevel(c) * (FixedInt) 1258291L);
    c.turnFriendlyDmg = -2;
    for (int i = 0; i < amount && !ZComponent.IsNull((ZComponent) c) && c.turnFriendlyDmg != c.game.turn; ++i)
    {
      FixedInt fixedInt = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-2097152L, 2097152L);
      ZEffector effector = ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-2097152L, 2097152L), fixedInt), fixedInt, target).effector;
      effector.game = c.game;
      ZCreature z = (ZCreature) null;
      if (!ZComponent.IsNull((ZComponent) c) && c.parent != null)
        z = c.parent.first();
      effector.whoSummoned = z;
      if (ZComponent.IsNull((ZComponent) z))
        c.game.globalEffectors.Add(effector);
      else
        z.effectors.Add(effector);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorFireCrackers(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.turnFriendlyDmg = -2;
    for (int i = 0; i < theSpell.amount && !ZComponent.IsNull((ZComponent) c) && c.turnFriendlyDmg != c.game.turn; ++i)
    {
      FixedInt fixedInt = (FixedInt) theSpell.speedMax + power + c.game.RandomFixedInt(-2097152L, 2097152L);
      ZSpell zspell = ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-2097152L, 2097152L), fixedInt), fixedInt, target);
      if (c.game.isClient && theSpell.spellEnum == SpellEnum.Jingle_Boom && (UnityEngine.Object) zspell.gameObject != (UnityEngine.Object) null)
        zspell.gameObject.GetComponent<SpriteRenderer>().color = ZSpell.GetHolidayColor();
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorFireCoal(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    Quaternion rot,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target)
  {
    c.turnFriendlyDmg = -2;
    for (int i = 0; i < theSpell.amount && !ZComponent.IsNull((ZComponent) c) && c.turnFriendlyDmg != c.game.turn; ++i)
    {
      FixedInt fixedInt = Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, (FixedInt) theSpell.speedMax + c.game.RandomFixedInt(-2097152L, 2097152L), power);
      ZSpell.BaseFireTarget(theSpell, c, pos, rot, Inert.Velocity(rot_z + c.game.RandomFixedInt(-4, 4), fixedInt), fixedInt, target);
      yield return 0.0f;
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorIceShield(Spell theSpell, ZCreature c)
  {
    FixedInt radius = (FixedInt) 66;
    FixedInt angle = (FixedInt) 360 / 25 * FixedInt.Deg2Rad;
    Texture2D tex = theSpell.toSummon.GetComponent<SpriteRenderer>().sprite.texture;
    for (int i = 0; i < 13; ++i)
    {
      MyLocation myLocation1 = c.position + new MyLocation(FixedInt.Sin(angle * i) * radius, FixedInt.Cos(angle * i) * radius);
      if (c.map.CheckCircle((int) myLocation1.x, (int) myLocation1.y, 7, (ZCreature) null, Inert.mask_movement_NoEffector))
      {
        c.map.ServerBitBlt((int) theSpell.snowCutout, (int) myLocation1.x, (int) myLocation1.y, false);
        ZSpell.RemoveItemsOnBitBlt(c.game, (int) myLocation1.x, (int) myLocation1.y, tex.width / 2 - 1);
      }
      if (c.game.isClient && !c.game.resyncing)
        ZComponent.Instantiate<GameObject>(theSpell.explosion, myLocation1.ToSinglePrecision(), Quaternion.identity);
      if (i > 0)
      {
        MyLocation myLocation2 = c.position + new MyLocation(FixedInt.Sin(angle * (25 - i)) * radius, FixedInt.Cos(angle * (25 - i)) * radius);
        if (c.map.CheckCircle((int) myLocation2.x, (int) myLocation2.y, 7, (ZCreature) null, Inert.mask_movement_NoEffector))
        {
          c.map.ServerBitBlt((int) theSpell.snowCutout, (int) myLocation2.x, (int) myLocation2.y, false);
          ZSpell.RemoveItemsOnBitBlt(c.game, (int) myLocation2.x, (int) myLocation2.y, tex.width / 2 - 1);
        }
        if (c.game.isClient && !c.game.resyncing)
          ZComponent.Instantiate<GameObject>(theSpell.explosion, myLocation2.ToSinglePrecision(), Quaternion.identity);
      }
      yield return 0.0f;
      yield return 0.0f;
    }
  }

  public static IEnumerator<float> IEnumeratorArcaneGlyph(
    Spell theSpell,
    ZCreature c,
    MyLocation pos)
  {
    ZMyWorld ZMyWorld = c.game.world;
    FixedInt radius = (FixedInt) 70;
    int count = 10;
    if (!ZComponent.IsNull((ZComponent) c) && c.FullArcane)
    {
      radius = (FixedInt) 140;
      count = 20;
    }
    FixedInt angle = (FixedInt) 360 / count;
    ZEffector[] array = new ZEffector[count];
    MyLocation[] loc = new MyLocation[count];
    int i;
    for (i = 0; i < count; ++i)
    {
      loc[i] = pos + new MyLocation(FixedInt.Sin(angle * FixedInt.Deg2Rad * i) * radius, FixedInt.Cos(angle * FixedInt.Deg2Rad * i) * radius);
      if (ZEffector.InSanctuary(c.game.world, loc[i]))
      {
        ZEffector.SpawnVineExplosion((Vector3) loc[i].ToSinglePrecision());
      }
      else
      {
        array[i] = ZEffector.Create(c.game, theSpell.toSummon.GetComponent<Effector>(), (Vector3) loc[i].ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform());
        ZEffector zeffector = array[i];
        zeffector.game = c.game;
        zeffector.whoSummoned = c;
        zeffector.collider.world = zeffector.world;
        c.game.globalEffectors.Add(zeffector);
        zeffector.collider.Move(loc[i]);
        yield return 0.0f;
        yield return 0.0f;
      }
    }
    for (i = 0; i < count; ++i)
    {
      if (!ZComponent.IsNull((ZComponent) array[i]))
      {
        MyLocation myLocation = loc[i];
        if (myLocation.x < 0 || myLocation.x > c.map.Width || myLocation.y < 0 || myLocation.y > c.map.Height || !c.map.CheckCircle((int) myLocation.x, (int) myLocation.y, 16, c, Inert.mask_movement_NoEffector | Inert.mask_Phantom))
        {
          if (ZMyWorld.OverlapCircle((Point) new MyLocation((int) myLocation.x, (int) myLocation.y), 16, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom))
          {
            array[i].EffectCreature((ZCreature) null);
          }
          else
          {
            array[i].collider.Disable();
            array[i].isNull = true;
            ZComponent.Destroy<GameObject>(array[i].gameObject);
          }
        }
        yield return 0.0f;
        yield return 0.0f;
      }
    }
    c.game.forceRysncPause = true;
  }

  public void OnExplosionGeneric()
  {
    this.OnExplosion();
    this.map.ServerBitBlt((int) this.explosionCutout, (int) this.position.x, (int) this.position.y);
  }

  public void OnExplosionPresents()
  {
    this.OnExplosion();
    List<ZMyCollider> zmyColliderList = this.game.world.OverlapCircleAll((Point) new MyLocation((int) this.position.x, (int) this.position.y), this.EXORADIUS, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
      {
        ZCreature creature = zmyColliderList[index].creature;
        if (!ZComponent.IsNull((ZComponent) creature))
          ZSpell.AddPresents(this.explosion, creature);
      }
      else if (zmyColliderList[index].gameObjectLayer == 13)
      {
        ZTower tower = zmyColliderList[index].tower;
        if (!ZComponent.IsNull((ZComponent) tower) && (ZComponent) tower.creature != (object) null)
          ZSpell.AddPresents(this.explosion, tower.creature);
      }
    }
    if (!this.game.isClient)
      return;
    foreach (Renderer componentsInChild in this.clientObj.GetComponentsInChildren<SpriteRenderer>())
      componentsInChild.enabled = false;
  }

  public static void AddPresents(GameObject explosion, ZCreature c, int amount = 2)
  {
    bool flag = true;
    for (int index = c.spells.Count - 1; index >= 0; --index)
    {
      if (c.spells[index].isPresent)
      {
        c.spells.RemoveAt(index);
        if (flag)
          flag = false;
        else
          break;
      }
    }
    for (int index = 0; index < amount; ++index)
    {
      Spell randomZspello = ZSpell.GetRandomZSpello(c, c.isPawn);
      SpellSlot s = new SpellSlot(randomZspello);
      s.MaxUses = 1;
      s.isPresent = true;
      if (randomZspello.spellEnum == SpellEnum.Enchanted_Axes)
        s.bonusDmg = c.parent.yourTurn ? c.parent.localTurn : c.parent.localTurn + 1;
      c.spells.Add(s);
      if (!c.isPawn && randomZspello.spellEnum == SpellEnum.Arcane_Gate || randomZspello.spellEnum == SpellEnum.Santas_Magic)
        c.parent.AddGate(s);
    }
    if (!c.game.isClient || c.game.resyncing || !((UnityEngine.Object) explosion != (UnityEngine.Object) null) || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null))
      return;
    ZComponent.Instantiate<GameObject>(explosion, c.transform.position, Quaternion.identity);
  }

  public static Spell GetRandomZSpello(ZCreature c, bool isPawn)
  {
    Spell spell = Inert.Instance.spells[c.game.RandomInt(0, Inert.Instance.presentIndex - 1)];
    while (spell.level == 4 || isPawn && (spell.level == 3 || spell.type == CastType.Tower || spell.spellEnum == SpellEnum.Dark_Defences || spell.spellEnum == SpellEnum.Whistling_Winds || spell.spellEnum == SpellEnum.Flight && c.flying) || spell.spellEnum == SpellEnum.Redo || spell.spellEnum == SpellEnum.Lichdom || spell.spellEnum == SpellEnum.Vine_Bloom || spell.spellEnum == SpellEnum.Flurry || spell.spellEnum == SpellEnum.Lichdom || spell.spellEnum == SpellEnum.Shining_Power || spell.spellEnum == SpellEnum.Dark_Defences)
      spell = Inert.Instance.spells[c.game.RandomInt(0, Inert.Instance.spells.Count - Inert.Instance._otherspells.Count)];
    return spell;
  }

  public void OnExplosionDeathBomb() => this.OnExplosionGeneric();

  public void OnExplosionVineBomb()
  {
    this.OnExplosionGeneric();
    int x1 = this.GetX();
    int y1 = this.GetY();
    FixedInt b = (FixedInt) 0;
    FlowerBallObject flowerBallObject = Inert.Instance.flower0;
    if (this.amount == 66)
      flowerBallObject = Inert.Instance.flower2;
    else if ((ZComponent) this.parent != (object) null && this.parent.familiar.Has(FamiliarType.Nature))
    {
      if (this.parent.familiarLevelNature == 5)
        flowerBallObject = Inert.Instance.flower4;
      else if (this.parent.familiarLevelNature == 4)
        flowerBallObject = Inert.Instance.flower3;
      else if (this.parent.familiarLevelNature == 3)
        flowerBallObject = Inert.Instance.flower2;
      else if (this.parent.familiarLevelNature > 0)
        flowerBallObject = Inert.Instance.flower1;
    }
    for (int index = 0; index < flowerBallObject.sprites.Length; ++index)
    {
      int x2 = (int) flowerBallObject.sprites[index].transform.localPosition.x;
      int y2 = (int) flowerBallObject.sprites[index].transform.localPosition.y;
      b = Mathd.Max(Mathd.Abs((FixedInt) x2), b);
      if (this.map.CheckCircle(x1 + x2, y1 + y2, 6, (ZCreature) null, Inert.mask_movement_NoEffector))
        this.map.ServerBitBlt(42 + Mathf.Min(3, flowerBallObject.sprites[index].gameObject.layer), x1 + x2, y1 + y2, false);
    }
    if (!(b > 0))
      return;
    ZSpell.RemoveItemsOnBitBlt(this.game, x1, y1, b.ToInt());
  }

  public void OnExplosionThornBomb()
  {
    this.OnExplosionGeneric();
    this.game.ongoing.RunSpell(ZSpell.IEThornBomb(this.game, this.parent?.parent, this));
  }

  public static IEnumerator<float> IEThornBomb(ZGame game, ZPerson p, ZSpell theSpell)
  {
    ZPerson person = theSpell.parent.parent;
    int amount = 1;
    if (theSpell.amount == 66)
      amount += 3;
    else if (theSpell.parent.familiar.Has(FamiliarType.Nature))
      amount += theSpell.parent.familiarLevelNature;
    FixedInt angle = (FixedInt) 360 / 15 * FixedInt.Deg2Rad;
    FixedInt radius = (FixedInt) 48;
    MyLocation cPos = theSpell.position;
    FixedInt offset = angle * 7864320L;
    for (int i = 0; i < 15; ++i)
    {
      for (int index = 0; index < amount; ++index)
      {
        FixedInt fixedInt = radius + index * 16;
        MyLocation myLocation = cPos + new MyLocation(FixedInt.Sin(angle * -i + offset) * fixedInt, FixedInt.Cos(angle * -i + offset) * fixedInt);
        if (!game.map.CheckPositionOnlyMap((int) myLocation.x, (int) myLocation.y) && !ZEffector.InSanctuary(theSpell.game.world, myLocation))
        {
          ZCreatureThorn thorn = ZCreatureCreate.CreateThorn(game, p, theSpell.toSummon.GetComponent<CreatureThorn>(), myLocation.ToSinglePrecision(), Quaternion.identity, theSpell.game.GetMapTransform());
          thorn.game = person.game;
          thorn.spell.parent = person.first();
          thorn.position = myLocation;
          thorn.collider.world = thorn.world;
          thorn.collider.Move(myLocation);
          thorn.spell.game = person.game;
          thorn.spell.position = myLocation;
          thorn.parent = person;
          game.thorns.Add(thorn);
          if (game.isClient && !game.resyncing)
            ZComponent.Instantiate<GameObject>(theSpell.explosion, myLocation.ToSinglePrecision(), Quaternion.identity);
        }
      }
      yield return 0.0f;
      yield return 0.0f;
      AudioManager.Play(theSpell.explosionClip);
    }
  }

  public static void OnExplosionBrineGoblin(ZCreature c)
  {
    c.game.ongoing.RunSpell(ZSpell.IEBrineGoblin(c.parent, c.game, c.position));
  }

  public static IEnumerator<float> IEBrineGoblin(ZPerson p, ZGame game, MyLocation c)
  {
    yield return 0.0f;
    p.map.ServerBitBlt(15, (int) c.x, (int) c.y);
    Spell brineBolt = Inert.Instance.brineBolt;
    ZSpell.ApplyExplosionForce(SpellEnum.Summon_Brine_Goblin, p.world, c, 0, Curve.Generic, 8, 190, (FixedInt) 1, DamageType.Brine, p.first(), p.game.turn);
    ZCreature z = game.world.map.PhysicsCollideCreature((ZCreature) null, (int) c.x, (int) c.y);
    for (int index = 0; index < 15; ++index)
    {
      MyLocation power = Inert.Velocity((FixedInt) (24 * index), (FixedInt) brineBolt.speedMax);
      ZSpell zspell = ZSpell.BaseFire(brineBolt, p, c, Quaternion.identity, power);
      zspell.name = "Brine Goblin";
      zspell.extraCheck = z;
      if (ZComponent.IsNull((ZComponent) z))
        zspell.curDuration += 4;
    }
  }

  public void OnExplosionBrineBomb()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    FixedInt fixedInt = (FixedInt) 360 / this.amount;
    if (!ZComponent.IsNull((ZComponent) this.parent) && this.game.AllowExpansion && this.spellEnum != SpellEnum.Brine_Burst)
      this.parent.game.ongoing.RunSpell(ZSpell.IEBrineBomb(this, this.parent, this.parent.game, this.position));
    for (int index = 0; index < this.amount; ++index)
    {
      MyLocation power = Inert.Velocity(fixedInt * index, (FixedInt) component.speedMax);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
      if (this.game.isClient)
        zspell.name = this.name;
      zspell.extraCheck = z;
      if (ZComponent.IsNull((ZComponent) z))
        zspell.curDuration += 4;
    }
  }

  public static IEnumerator<float> IEBrineBomb(
    ZSpell spell,
    ZCreature parent,
    ZGame game,
    MyLocation position)
  {
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    spell.OnExplosionGeneric();
    Spell child = spell.toSummon.GetComponent<Spell>();
    ZCreature ExtraCheck = game.map.PhysicsCollideCreature((ZCreature) null, (int) position.x, (int) position.y);
    FixedInt angle = (FixedInt) 360 / spell.amount;
    for (int index = 0; index < spell.amount; ++index)
    {
      ZSpell zspell = ZSpell.BaseFire(child, parent, position, Quaternion.identity, Inert.Velocity(angle * index, (FixedInt) child.speedMax), isChild: true);
      if (game.isClient)
        zspell.name = spell.name;
      zspell.extraCheck = ExtraCheck;
      if (ZComponent.IsNull((ZComponent) ExtraCheck))
        zspell.curDuration += 4;
    }
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    yield return 0.0f;
    for (int index = 0; index < spell.amount; ++index)
    {
      ZSpell zspell = ZSpell.BaseFire(child, parent, position, Quaternion.identity, Inert.Velocity(angle * index, (FixedInt) child.speedMax), isChild: true);
      if (game.isClient)
        zspell.name = spell.name;
      zspell.extraCheck = ExtraCheck;
      if (ZComponent.IsNull((ZComponent) ExtraCheck))
        zspell.curDuration += 4;
    }
  }

  public void OnExplosionCuckooClock()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    for (int index = 0; index < 4; ++index)
    {
      MyLocation power = Inert.Velocity((FixedInt) (90 * index), 9);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
      if (this.game.isClient)
        zspell.name = this.name;
    }
  }

  public void OnExplosionMagmaBomb()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    for (int index = 0; index < this.amount; ++index)
    {
      MyLocation myLocation = Inert.Velocity(this.game.RandomFixedInt(0, 360), (FixedInt) component.speedMax);
      ZSpell s = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, myLocation, false);
      if (this.game.isClient)
        s.name = this.name;
      s.extraCheck = z;
      if (this.bookOf == BookOf.Flame)
        ZSpell.UpgradeFullFireShrapnel(this.parent, s);
      if (!ZComponent.IsNull((ZComponent) z))
        s.damage /= 2;
      else
        s.curDuration += 4;
      s.SetVelocity(myLocation);
    }
  }

  public void OnExplosionIceBomb()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    int amount = this.amount;
    FixedInt fixedInt1 = (FixedInt) -360;
    FixedInt fixedInt2 = (FixedInt) (360 / amount);
    if (!ZComponent.IsNull((ZComponent) z))
      amount -= 2;
    for (int index = 0; index < amount; ++index)
    {
      FixedInt angle = (FixedInt) index * fixedInt2;
      if (Mathd.Abs(angle - fixedInt1) < 20)
      {
        if (angle > fixedInt1)
        {
          angle += 45;
          if (angle > 360)
            angle -= 360;
        }
        else
        {
          angle -= 45;
          if (angle < 0)
            angle += 360;
        }
      }
      fixedInt1 = angle;
      MyLocation power = Inert.Velocity(angle, (FixedInt) component.speedMax);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power);
      if (this.game.isClient)
        zspell.name = this.name;
      zspell.extraCheck = z;
    }
  }

  public void OnExplosionComet()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature zcreature = this.map.PhysicsCollideCreatureCircle((ZCreature) null, (int) this.position.x, (int) this.position.y, this.radius + 10);
    if ((ZComponent) zcreature != (object) null && zcreature.flying)
    {
      FixedInt fixedInt1 = this.game.RandomFixedInt(0, 360);
      FixedInt fixedInt2 = FixedInt.Create(360) / this.amount;
      FixedInt fixedInt3 = (FixedInt) (360 / this.amount / 2);
      FixedInt fixedInt4 = fixedInt3 / 2;
      for (int index = 0; index < this.amount; ++index)
      {
        FixedInt angle = (FixedInt) index * fixedInt3 + fixedInt4;
        if (angle > 360)
          angle -= 360;
        MyLocation power = Inert.Velocity(angle, (FixedInt) component.speedMax);
        ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
        if (this.game.isClient)
          zspell.name = this.name;
        fixedInt1 += fixedInt2;
      }
    }
    else
    {
      FixedInt fixedInt5 = this.game.RandomFixedInt(0, 360);
      FixedInt fixedInt6 = FixedInt.Create(360) / this.amount;
      FixedInt fixedInt7 = (FixedInt) (360 / this.amount);
      FixedInt fixedInt8 = this.game.RandomFixedInt(0, 360);
      for (int index = 0; index < this.amount; ++index)
      {
        FixedInt angle = (FixedInt) index * fixedInt7 + fixedInt8;
        if (angle > 360)
          angle -= 360;
        MyLocation power = Inert.Velocity(angle, (FixedInt) component.speedMax);
        ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
        if (this.game.isClient)
          zspell.name = this.name;
        fixedInt5 += fixedInt6;
      }
    }
  }

  public void OnExplosionFrostArrow()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    int amount = this.amount;
    if (this.spellEnum == SpellEnum.Frost_Arrow && (ZComponent) this.parent != (object) null)
      amount += this.parent.familiarLevelFrost;
    for (int index = 0; index < amount; ++index)
    {
      MyLocation power = Inert.Velocity(this.game.RandomFixedInt(0, 360), 10);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
      if (this.game.isClient)
        zspell.name = this.name;
      if (ZComponent.IsNull((ZComponent) z))
        zspell.curDuration += 4;
      zspell.extraCheck = z;
    }
  }

  public static IEnumerator<float> OnExplosionIceBall(ZSpell theSpell)
  {
    theSpell.OnExplosionGeneric();
    Spell child = theSpell.toSummon.GetComponent<Spell>();
    MyLocation pos = theSpell.position;
    ZCreature ExtraCheck = theSpell.map.PhysicsCollideCreature((ZCreature) null, (int) theSpell.position.x, (int) theSpell.position.y);
    for (int i = 0; i < theSpell.amount; ++i)
    {
      ZSpell zspell = ZSpell.BaseFire(child, theSpell.parent, pos, Quaternion.identity, Inert.Velocity(theSpell.game.RandomFixedInt(0, 360), 3), isChild: true);
      if (theSpell.game.isClient)
        zspell.name = theSpell.name;
      if (ZComponent.IsNull((ZComponent) ExtraCheck))
        zspell.curDuration += 4;
      zspell.extraCheck = ExtraCheck;
      if (i % 3 == 0)
        yield return 0.0f;
    }
  }

  public static void OnExplosionMelt(
    Spell child,
    ZGame game,
    MyLocation position,
    ZCreature parent,
    MyLocation velocity,
    int amount)
  {
    MyLocation pos = position;
    for (int index = 0; index < amount; ++index)
    {
      MyLocation diff = velocity;
      diff.x += game.RandomFixedInt(524288L, 1572864L);
      diff.y += game.RandomFixedInt(524288L, 1572864L);
      MyLocation power = Inert.Velocity(Inert.AngleOfVelocity(diff) + game.RandomFixedInt(-10, 10), diff.magnitude + game.RandomFixedInt(-5, 5));
      ZSpell zspell = ZSpell.BaseFire(child, parent, pos, Quaternion.identity, power);
      if (game.isClient)
        zspell.name = "Melted Snowman";
    }
  }

  public static void OnExplosionSnowbolt(
    Spell child,
    ZGame game,
    MyLocation position,
    ZCreature parent,
    MyLocation velocity,
    int amount)
  {
    MyLocation pos = position;
    for (int index = 0; index < amount; ++index)
    {
      MyLocation diff = velocity;
      diff.x += game.RandomFixedInt(524288L, 1572864L);
      diff.y += game.RandomFixedInt(524288L, 1572864L);
      MyLocation power = Inert.Velocity(Inert.AngleOfVelocity(diff) + game.RandomFixedInt(-30, 30), diff.magnitude + game.RandomFixedInt(-5, 5));
      ZSpell zspell = ZSpell.BaseFire(child, parent, pos, Quaternion.identity, power);
      if (game.isClient)
        zspell.name = "Melted Snowman";
    }
  }

  public void OnExplosionStarBolt()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    MyLocation position = this.position;
    MyLocation velocity1 = this.velocity;
    for (int index = 0; index < this.amount; ++index)
    {
      MyLocation velocity2 = this.velocity;
      velocity2.x *= this.game.RandomFixedInt(524288L, 1572864L);
      velocity2.y *= this.game.RandomFixedInt(524288L, 1572864L);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, position, Quaternion.identity, velocity2, isChild: true);
      zspell.curDuration = 5;
      if (this.game.isClient)
        zspell.name = this.name;
    }
  }

  public void OnExplosionWaterBall()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    MyLocation position = this.position;
    MyLocation myLocation = this.velocity;
    for (int index = 0; index < this.amount; ++index)
    {
      if (this.game.skimmed_on_water)
      {
        myLocation = Inert.Velocity(Inert.AngleOfVelocity(myLocation) + this.game.RandomFixedInt(-2, 2), this.velocity.magnitude + this.game.RandomFixedInt(0, 5));
      }
      else
      {
        FixedInt fixedInt = Inert.AngleOfVelocity(this.velocity);
        myLocation = !(Mathd.Abs(this.velocity.x) > Mathd.Abs(this.velocity.y)) ? Inert.Velocity(fixedInt + this.game.RandomFixedInt(-12, 12), this.velocity.magnitude * this.game.RandomFixedInt((FixedInt) 524288L, (FixedInt) 1572864L)) : Inert.Velocity(fixedInt + this.game.RandomFixedInt(-10, 10), this.velocity.magnitude * this.game.RandomFixedInt((FixedInt) 1048576L, (FixedInt) 1572864L));
      }
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, position, Quaternion.identity, myLocation);
      if (this.game.skimmed_on_water)
        zspell.damage = 4;
      if (this.game.isClient)
        zspell.name = this.name;
    }
  }

  public static void OnExplosionWaterBall(
    Spell theSpell,
    ZGame game,
    ZCreature parent,
    MyLocation position,
    int amount2 = -1)
  {
    if (game.isClient && !game.resyncing && (UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null)
      ZComponent.Instantiate<GameObject>(theSpell.explosion, position.ToSinglePrecision(), Quaternion.identity);
    int num = amount2 > 0 ? amount2 : theSpell.amount;
    Spell component = theSpell.toSummon.GetComponent<Spell>();
    MyLocation pos = position;
    for (int index = 0; index < num; ++index)
    {
      ZSpell zspell = ZSpell.BaseFire(component, parent, pos, Quaternion.identity, new MyLocation(0, 0)
      {
        x = game.RandomFixedInt(-5242880L, 5242880L),
        y = game.RandomFixedInt(-5242880L, 5242880L)
      });
      if (game.isClient)
        zspell.name = theSpell.name;
    }
  }

  public static IEnumerator<float> OnExplosionSnowbolt(ZSpell theSpell)
  {
    theSpell.OnExplosionGeneric();
    Spell component = theSpell.toSummon.GetComponent<Spell>();
    MyLocation position = theSpell.position;
    FixedInt fixedInt = Inert.AngleOfVelocity(theSpell.velocity);
    for (int index = 0; index < 5; ++index)
    {
      MyLocation power = Inert.Velocity(theSpell.game.RandomFixedInt(fixedInt - 30, fixedInt + 30), theSpell.game.RandomInt(1, 17));
      ZSpell zspell = ZSpell.BaseFire(component, theSpell.parent, position + new MyLocation(theSpell.game.RandomFixedInt(-15, 15), theSpell.game.RandomFixedInt(-15, 15)), Quaternion.identity, power);
      if (theSpell.game.isClient)
        zspell.name = theSpell.name;
    }
    yield break;
  }

  public static IEnumerator<float> OnExplosionSnowBall(ZSpell theSpell)
  {
    theSpell.OnExplosionGeneric();
    Spell component = theSpell.toSummon.GetComponent<Spell>();
    MyLocation position = theSpell.position;
    FixedInt fixedInt = Inert.AngleOfVelocity(theSpell.velocity);
    for (int index = 0; index < 50; ++index)
    {
      MyLocation power = Inert.Velocity(theSpell.game.RandomFixedInt(fixedInt - 30, fixedInt + 30), theSpell.game.RandomInt(1, 17));
      ZSpell zspell = ZSpell.BaseFire(component, theSpell.parent, position + new MyLocation(theSpell.game.RandomFixedInt(-15, 15), theSpell.game.RandomFixedInt(-15, 15)), Quaternion.identity, power);
      if (theSpell.game.isClient)
        zspell.name = theSpell.name;
    }
    if (theSpell.curDuration < 2 && !ZComponent.IsNull((ZComponent) theSpell.parent) && !theSpell.parent.isDead)
    {
      theSpell.parent.ApplyDamage(theSpell.spellEnum, theSpell.damageType, theSpell.game.RandomInt(16, 30) - theSpell.curDuration, theSpell.parent, theSpell.parent.game.turn, (ISpellBridge) theSpell);
      yield break;
    }
  }

  public void OnExplosionDisruption()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature zcreature = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    for (int index = 0; index < 4; ++index)
    {
      MyLocation myLocation = Inert.Velocity(this.game.RandomFixedInt(0, 360), (FixedInt) component.speedMax);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, myLocation, false, true);
      zspell.extraCheck = zcreature;
      if ((ZComponent) this.parent != (object) null && this.parent.parent != null && this.parent.parent.localTurn <= 0)
        zspell.damage /= 2;
      if (this.game.isClient)
        zspell.name = this.name;
      zspell.SetVelocity(myLocation);
    }
  }

  public void OnExplosionScatterRock()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    for (int index = 0; index < 6; ++index)
    {
      MyLocation power = Inert.Velocity(this.game.RandomFixedInt(0, 360), 12);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
      if (this.game.isClient)
        zspell.name = this.name;
    }
  }

  public static void FireShaft(Spell theSpell, ZCreature cre, MyLocation target)
  {
    Texture2D texture = theSpell.GetComponent<SpriteRenderer>().sprite.texture;
    int x = (int) target.x;
    int height = cre.map.Height;
    ZMap map = cre.map;
    for (; height > texture.height / 2; height -= texture.height)
    {
      cre.map.ServerBitBlt((int) theSpell.snowCutout, x, height);
      if (cre.game.isClient)
        ZComponent.Instantiate<GameObject>(theSpell.explosion, new Vector3((float) x, (float) height), Quaternion.identity);
    }
    ZMyWorld world = cre.world;
    MyCollider.Bounds bounds = new MyCollider.Bounds()
    {
      x1 = x - 38,
      x2 = x + 38,
      y1 = 0,
      y2 = cre.map.Height
    };
    int layer = Inert.mask_entity_movement | Inert.mask_Phantom;
    foreach (ZMyCollider zmyCollider in world.OverlapRectangleAll(bounds, (ZMyCollider) null, layer))
    {
      if (!ZComponent.IsNull((ZComponent) zmyCollider.creature))
        zmyCollider.creature.Fall();
      else if (!ZComponent.IsNull((ZComponent) zmyCollider.spell))
        zmyCollider.spell.Fall();
    }
  }

  public static void FireBlit(Spell theSpell, ZCreature cre, MyLocation target)
  {
    if (theSpell.spellEnum == SpellEnum.Forest_Seed || theSpell.spellEnum == SpellEnum.Butterfly_Jar || theSpell.spellEnum == SpellEnum.Grove_Renewal)
    {
      if (cre.isPawn)
        cre = cre.parent.first();
      ZCreature creature1 = ZCreatureCreate.CreateCreature(cre.parent, theSpell.toSummon.GetComponent<Creature>(), target.ToSinglePrecision(), Quaternion.identity, cre.game.GetMapTransform());
      creature1.game = cre.game;
      creature1.position = target;
      creature1.team = cre.team;
      ZEffector auraOfDecay = creature1.auraOfDecay;
      auraOfDecay.game = creature1.game;
      auraOfDecay.active = false;
      auraOfDecay.whoSummoned = cre;
      auraOfDecay.collider = creature1.collider;
      auraOfDecay.collider.world = auraOfDecay.world;
      auraOfDecay.collider.Initialize(target, cre.game.world);
      auraOfDecay.collider.creature = creature1;
      creature1.parent = cre.parent;
      auraOfDecay.variable = cre.parent.localTurn;
      creature1.game.globalEffectors.Add(auraOfDecay);
      if (theSpell.spellEnum == SpellEnum.Butterfly_Jar)
      {
        if ((UnityEngine.Object) creature1.transform != (UnityEngine.Object) null)
          creature1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = cre.parent.clientColor;
        foreach (ZMyCollider zmyCollider in cre.game.world.OverlapCircleAll((Point) creature1.position, 14, creature1.collider, 256))
        {
          if ((ZComponent) zmyCollider != (object) null && (ZComponent) zmyCollider.creature != (object) null && zmyCollider.creature.isPawn && creature1.parent.controlled.Count > 0 && zmyCollider.creature.radius < 18 && zmyCollider.creature.race != CreatureRace.Effector)
          {
            zmyCollider.creature.collider.layer = Inert.mask_ButterflyJar;
            zmyCollider.creature.collider.gameObjectLayer = 21;
            if (!zmyCollider.creature.isMoving)
            {
              zmyCollider.creature.velocity = new MyLocation(0, 1);
              zmyCollider.creature.StartMoving(false);
            }
          }
        }
      }
      List<ZMyCollider> zmyColliderList = cre.game.world.OverlapColliderAll(auraOfDecay.collider, Inert.mask_entity_movement | Inert.mask_Phantom);
      if (zmyColliderList.Count > 0)
      {
        for (int index = 0; index < zmyColliderList.Count; ++index)
        {
          if (zmyColliderList[index].gameObjectLayer == 9)
          {
            ZEffector effector = zmyColliderList[index].effector;
            effector.game = creature1.game;
            if (!ZComponent.IsNull((ZComponent) effector) && effector.active)
              effector.EffectCreature(creature1);
          }
          else if (zmyColliderList[index].gameObjectLayer == 8 || zmyColliderList[index].gameObjectLayer == 16)
          {
            ZCreature creature2 = zmyColliderList[index].creature;
            if (!ZComponent.IsNull((ZComponent) creature2))
            {
              if (!ZComponent.IsNull((ZComponent) creature2.stormShield) && creature2.stormShield.active)
                creature2.stormShield.EffectCreature(creature2);
              if (!ZComponent.IsNull((ZComponent) creature2.auraOfDecay) && creature2.auraOfDecay.active)
                creature2.auraOfDecay.EffectCreature(creature2);
              if (!creature2.isMoving && creature2.ShouldFall())
                creature2.Fall();
            }
            else if ((ZComponent) zmyColliderList[index].spell != (object) null && !zmyColliderList[index].spell.isMoving && zmyColliderList[index].spell.ShouldSpellFall())
              zmyColliderList[index].spell.SpellFall();
          }
          else if (zmyColliderList[index].gameObjectLayer == 13)
          {
            ZTower tower = zmyColliderList[index].tower;
            if (!ZComponent.IsNull((ZComponent) tower))
            {
              if (!ZComponent.IsNull((ZComponent) tower.creature.stormShield) && tower.creature.stormShield.active)
                tower.creature.stormShield.EffectCreature(tower.creature);
              if (!ZComponent.IsNull((ZComponent) tower.creature.auraOfDecay) && tower.creature.auraOfDecay.active)
                tower.creature.auraOfDecay.EffectCreature(tower.creature);
              if (!tower.creature.isMoving)
                tower.ShouldFall();
            }
          }
          else
          {
            ZSpell spell = zmyColliderList[index].spell;
            if (!ZComponent.IsNull((ZComponent) spell) && !spell.isMoving && !spell.isDead && spell.ShouldSpellFall())
              spell.SpellFall();
          }
        }
      }
      creature1.game.CreatureMoveSurroundings(creature1.position, creature1.radius, creature1.collider);
      creature1.StartMoving(false);
      creature1.game.forceRysncPause = true;
    }
    else
    {
      Texture2D texture = theSpell.GetComponent<SpriteRenderer>().sprite.texture;
      cre.map.ServerBitBlt((int) theSpell.snowCutout, (int) target.x, (int) target.y, false);
      ZSpell.RemoveItemsOnBitBlt(cre.game, theSpell.spellEnum, (int) target.x, (int) target.y);
    }
  }

  public static void RemoveItemsOnBitBlt(
    ZGame game,
    SpellEnum spellEnum,
    int x,
    int y,
    int index = 0)
  {
    if (Inert.Instance.cachedBlitCollidersZ == null)
    {
      Inert.Instance.cachedBlitCollidersZ = new List<ZMyCollider>();
      for (int index1 = 0; index1 < Inert.Instance.cachedBlitColliders.Count; ++index1)
      {
        ZMyCollider zmyCollider = new ZMyCollider();
        zmyCollider.Copy(Inert.Instance.cachedBlitColliders[index1]);
        Inert.Instance.cachedBlitCollidersZ.Add(zmyCollider);
      }
    }
    ZMyCollider poly;
    switch (spellEnum)
    {
      case SpellEnum.Volcano:
        poly = Inert.Instance.cachedBlitCollidersZ[2 + index];
        break;
      case SpellEnum.Rock_Slab:
        poly = Inert.Instance.cachedBlitCollidersZ[0];
        break;
      case SpellEnum.Christmas_Tree:
        poly = Inert.Instance.cachedBlitCollidersZ[1];
        break;
      case SpellEnum.Stepping_Stone:
        poly = Inert.Instance.cachedBlitCollidersZ[8];
        break;
      default:
        return;
    }
    poly.ArbritaryInit();
    poly.x = x + poly.offsetX;
    poly.y = y + poly.offsetY;
    List<ZMyCollider> zmyColliderList = game.world.OverlapArbritaryPolygonAll(poly, 512);
    for (int index2 = 0; index2 < zmyColliderList.Count; ++index2)
    {
      ZEffector effector = zmyColliderList[index2]?.effector;
      if (!ZComponent.IsNull((ZComponent) effector) && (effector.type == EffectorType.Glyph || effector.type == EffectorType.Naplem || effector.type == EffectorType.AutumnLeaves))
      {
        if (effector.type != EffectorType.Naplem && effector.type != EffectorType.AutumnLeaves || !effector.spell.isMoving)
          effector.DestroyNoExplosion();
      }
      else if (!ZComponent.IsNull((ZComponent) effector) && effector.type == EffectorType.Morning_Sun)
        effector.spell.MoveMorningSun((int) effector.position.y, y);
    }
  }

  public static void RemoveItemsOnBitBlt(ZGame game, int x, int y, int radius)
  {
    List<ZMyCollider> zmyColliderList = game.world.OverlapCircleAll(new Point(x, y), radius, (ZMyCollider) null, 512);
    for (int index = 0; index < zmyColliderList.Count; ++index)
    {
      ZEffector effector = zmyColliderList[index]?.effector;
      if (!ZComponent.IsNull((ZComponent) effector) && (effector.type == EffectorType.Glyph || effector.type == EffectorType.Naplem))
      {
        if (effector.type != EffectorType.Naplem || !effector.spell.isMoving)
          effector.DestroyNoExplosion();
      }
      else if (!ZComponent.IsNull((ZComponent) effector) && effector.type == EffectorType.Morning_Sun)
        effector.spell.MoveMorningSun((int) effector.position.y, y);
    }
  }

  public static void FireSeasonal(Spell theSpell, ZCreature c, MyLocation target, bool extended)
  {
    target.y = (FixedInt) (c.map.Height + 300);
    switch (c.game.currentSeason)
    {
      case GameSeason.Summer:
        Spell rain = Inert.Instance.rain;
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(c, rain, target, -75, 75, 20));
        if (!c.game.isClient || c.game.resyncing)
          break;
        AudioManager.Play(Inert.Instance.spells["English Summer"].castClip);
        UnityEngine.Object.Destroy((UnityEngine.Object) ZComponent.Instantiate<GameObject>(Inert.Instance.spells["English Summer"].toSummon, target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform()), 5f);
        break;
      case GameSeason.Autumn:
        if (c.game.isClient && !c.game.resyncing)
          AudioManager.Play(Inert.Instance.spells["Autumn Leaves"].castClip);
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorAutumnLeaves(c, Inert.Instance.spells["Autumn Leaves"], target, -75, 75, 30, extended));
        break;
      case GameSeason.Winter:
        Spell snow = Inert.Instance.Snow;
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(c, snow, target, -75, 75, 20));
        if (!c.game.isClient || c.game.resyncing)
          break;
        AudioManager.Play(Inert.Instance.spells["Blizzard"].castClip);
        UnityEngine.Object.Destroy((UnityEngine.Object) ZComponent.Instantiate<GameObject>(Inert.Instance.spells["Blizzard"].toSummon, target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform()), 5f);
        break;
      case GameSeason.Spring:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorStorm(c, target, -75, 75, 4));
        if (!c.game.isClient || c.game.resyncing)
          break;
        AudioManager.Play(Inert.Instance.spells["Storm"].castClip);
        UnityEngine.Object.Destroy((UnityEngine.Object) ZComponent.Instantiate<GameObject>(Inert.Instance.spells["Storm"].toSummon, target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform()), 5f);
        break;
    }
  }

  public static void FireCogFall(Spell theSpell, ZCreature c, MyLocation target)
  {
    target.y = (FixedInt) (c.map.Height + 1000);
    ZSpell.BaseFire(theSpell, c, target, Quaternion.identity, new MyLocation(0, -1));
  }

  public void OnExplosionMechanicalArrow()
  {
    Spell component = this.toSummon.GetComponent<Spell>();
    for (int index = 0; index < 15; ++index)
    {
      MyLocation power = Inert.Velocity((FixedInt) (24 * index), 8);
      power.x += power.x / 4;
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power, isChild: true);
      if (this.game.isClient)
        zspell.name = this.name;
    }
  }

  public static void OnExplosionSteamDragon(ZCreature parent, MyLocation pos)
  {
    parent.game.ongoing.RunSpell(ZSpell.IESteamDragon(parent, pos));
  }

  public static IEnumerator<float> IESteamDragon(ZCreature parent, MyLocation pos)
  {
    yield return 0.0f;
    for (int index = 0; index < 4; ++index)
      ZSpell.BaseFire(Inert.Instance.cog, parent, pos, Quaternion.identity, Inert.Velocity((FixedInt) (90 * index), 6)).name = "Steam Dragon";
  }

  public void OnExplosionRainOfClams()
  {
    this.map.ServerBitBlt((int) this.explosionCutout, (int) this.position.x, (int) this.position.y);
    if (!this.game.isClient || this.game.resyncing || !((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    ZComponent.Instantiate<GameObject>(this.explosion, this.transform.position + new Vector3(0.0f, 45f), Quaternion.identity);
    ZComponent.Instantiate<GameObject>(this.explosion, this.transform.position + new Vector3(-45f, 0.0f), Quaternion.identity);
    ZComponent.Instantiate<GameObject>(this.explosion, this.transform.position + new Vector3(45f, 0.0f), Quaternion.identity);
  }

  public void OnExplosionMudBall()
  {
    this.OnExplosion();
    this.game.ongoing.RunSpell(this.IEMudBall());
  }

  private bool CanPlaceCircle(int x, int y, int radius)
  {
    return this.map.CheckCircleAllowOutOfBounds(x, y, radius, (ZCreature) null, Inert.mask_movement_NoEffector);
  }

  public void PlaceCircles(int centerX, int centerY, int maxRadius)
  {
    for (int index1 = 0; index1 < 5; ++index1)
    {
      int index2 = 0;
      int x = this.game.RandomInt(-maxRadius + this._circleSizes[index2], maxRadius - this._circleSizes[index2]) / 2 + centerX;
      int y = this.game.RandomInt(-maxRadius + this._circleSizes[index2], maxRadius - this._circleSizes[index2]) / 2 + centerY;
      if (this.CanPlaceCircle(x, y, this._circleSizes[index2] - 2) && y < this.game.map.Height + this._circleSizes[index2])
        this.map.ServerBitBlt(41, x, y, false);
    }
    for (int radius = 3; radius < maxRadius; radius += this.game.RandomInt(1, 12))
      this._doCircle(centerX, centerY, radius, this._circleSizes[0], ExplosionCutout.Mud_Large, 0);
    for (int radius = 3; radius < maxRadius; radius += this.game.RandomInt(1, 6))
      this._doCircle(centerX, centerY, radius, this._circleSizes[1], ExplosionCutout.Mud_Medium, 0);
    for (int radius = 3; radius < maxRadius; radius += this.game.RandomInt(1, 6))
      this._doCircle(centerX, centerY, radius, this._circleSizes[2], ExplosionCutout.Mud_Small, 0);
  }

  private void _doCircle(
    int x,
    int y,
    int radius,
    int whichRad,
    ExplosionCutout which,
    int distanceAdjust)
  {
    List<Coords> outlineArray = MapGenerator.getOutlineArray(radius);
    for (int index1 = 0; index1 < 20; ++index1)
    {
      int index2 = this.game.RandomInt(0, outlineArray.Count);
      if (this.CanPlaceCircle(x + outlineArray[index2].x, y + outlineArray[index2].y, whichRad + distanceAdjust + (y + outlineArray[index2].y > this.map.Height ? 5 : 0)) && y + outlineArray[index2].y < this.game.map.Height + whichRad)
        this.map.ServerBitBlt((int) which, x + outlineArray[index2].x, y + outlineArray[index2].y, false);
    }
    for (int index = 0; index < outlineArray.Count; index += this.game.RandomInt(1, 6))
    {
      if (this.CanPlaceCircle(x + outlineArray[index].x, y + outlineArray[index].y, whichRad + distanceAdjust + (y + outlineArray[index].y > this.map.Height ? 5 : 0)) && y + outlineArray[index].y < this.game.map.Height + whichRad)
        this.map.ServerBitBlt((int) which, x + outlineArray[index].x, y + outlineArray[index].y, false);
    }
  }

  private IEnumerator<float> IEMudBall()
  {
    ZSpell zspell = this;
    ZMyWorld world = zspell.game.world;
    int x = zspell.GetX();
    int y = zspell.GetY();
    if (zspell.spellEnum == SpellEnum.Mud_Ball)
    {
      x += (int) zspell.velocity.x / 2;
      y += (int) zspell.velocity.y / 2;
    }
    FixedInt fixedInt = (FixedInt) zspell.game.RandomInt(0, 360) * FixedInt.Deg2Rad;
    MudBallObject mud = Inert.Instance.mudBallObject;
    if (zspell.curDuration < 10 && zspell.spellEnum == SpellEnum.Mudruption)
    {
      while (zspell.curDuration < 10)
      {
        ++zspell.curDuration;
        yield return 0.0f;
      }
    }
    if (zspell.spellEnum != SpellEnum.Old_Mud_Ball)
    {
      zspell.PlaceCircles(x, y, 68);
      ZSpell.RemoveItemsOnBitBlt(zspell.game, x, y, 68);
    }
    else
    {
      for (int index = 0; index < mud.spritesBig.Length; ++index)
      {
        int x1 = (int) mud.spritesBig[index].transform.localPosition.x;
        int y1 = (int) mud.spritesBig[index].transform.localPosition.y;
        if (zspell.map.CheckCircleAllowOutOfBounds(x + x1, y + y1, 16, (ZCreature) null, Inert.mask_movement_NoEffector))
          zspell.map.ServerBitBlt(41, x + x1, y + y1, false);
      }
      for (int index = 0; index < mud.spritesMedium.Length; ++index)
      {
        int x2 = (int) mud.spritesMedium[index].transform.localPosition.x;
        int y2 = (int) mud.spritesMedium[index].transform.localPosition.y;
        if (zspell.map.CheckCircleAllowOutOfBounds(x + x2, y + y2, 8, (ZCreature) null, Inert.mask_movement_NoEffector))
          zspell.map.ServerBitBlt(40, x + x2, y + y2, false);
      }
      for (int index = 0; index < mud.spritesSmall.Length; ++index)
      {
        int x3 = (int) mud.spritesSmall[index].transform.localPosition.x;
        int y3 = (int) mud.spritesSmall[index].transform.localPosition.y;
        if (zspell.map.CheckCircleAllowOutOfBounds(x + x3, y + y3, 4, (ZCreature) null, Inert.mask_movement_NoEffector))
          zspell.map.ServerBitBlt(39, x + x3, y + y3, false);
      }
      ZSpell.RemoveItemsOnBitBlt(zspell.game, x, y, 75);
    }
  }

  public void OnExplosionArcaneBomb()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    int num = this.damage == 15 ? 4 : 3;
    if (!ZComponent.IsNull((ZComponent) this.parent) && this.parent.FullArcane)
    {
      if (!this.parent.game.originalSpellsOnly)
        ZSpell.FireAuraOfDecay(Inert.Instance.arcaneMist, this.parent, this.position, (FixedInt) 0, (FixedInt) 0, this.position);
      num += this.damage == 15 ? 8 : 6;
    }
    for (int index = 0; index < num; ++index)
    {
      MyLocation power = Inert.Velocity(this.game.RandomFixedInt(0, 360), 7);
      ZSpell zspell = ZSpell.BaseFireTarget(component, this.parent, this.position, Quaternion.identity, power, (FixedInt) 7, this.target);
      if (this.game.isClient)
        zspell.name = this.name;
    }
    if (!this.game.isClient)
      return;
    AudioManager.Play(AudioManager.instance.flashEnd);
  }

  public void OnExplosionAcorn()
  {
    this.OnExplosionGeneric();
    if (ZComponent.IsNull((ZComponent) this.parent) || this.parent.isDead)
      return;
    ZEffector zeffector = ZEffector.Create(this.game, this.toSummon.GetComponent<Effector>(), (Vector3) this.position.ToSinglePrecision(), Quaternion.identity, this.game.GetMapTransform());
    zeffector.active = false;
    zeffector.game = this.game;
    zeffector.position = this.position;
    zeffector.whoSummoned = this.parent;
    this.parent.effectors.Add(zeffector);
  }

  public void OnExplosionRocket()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    for (int index = 0; index < 12; ++index)
    {
      MyLocation power = Inert.Velocity((FixedInt) (index * 30), this.explodeOnImpact ? (FixedInt) component.speedMax / (FixedInt) 3145728L : (FixedInt) component.speedMax * 838860L);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power);
      if (this.game.isClient)
        zspell.name = this.name;
      if (ZComponent.IsNull((ZComponent) z))
        zspell.curDuration += 6;
      zspell.extraCheck = z;
      if (this.game.isClient && this.spellEnum == SpellEnum.New_Years_Rocket && (UnityEngine.Object) zspell.gameObject != (UnityEngine.Object) null)
        zspell.gameObject.GetComponent<SpriteRenderer>().color = ZSpell.GetHolidayColor();
    }
  }

  public void OnExplosionNapalmBomb()
  {
    this.OnExplosionGeneric();
    Spell component = this.toSummon.GetComponent<Spell>();
    ZCreature z1 = this.map.PhysicsCollideCreature((ZCreature) null, (int) this.position.x, (int) this.position.y);
    for (int index = 0; index < 15; ++index)
    {
      MyLocation power = Inert.Velocity((FixedInt) (index * 24), this.explodeOnImpact ? (FixedInt) component.speedMax / (FixedInt) 3145728L : (FixedInt) component.speedMax * 838860L);
      ZSpell zspell = ZSpell.BaseFire(component, this.parent, this.position, Quaternion.identity, power);
      if (this.game.isClient)
        zspell.name = this.name;
      if (ZComponent.IsNull((ZComponent) z1))
        zspell.curDuration += 4;
      zspell.extraCheck = z1;
      ZEffector effector = zspell.effector;
      effector.game = this.game;
      ZCreature z2 = (ZCreature) null;
      if (!ZComponent.IsNull((ZComponent) this.parent) && this.parent.parent != null)
        z2 = this.parent.parent.first();
      effector.whoSummoned = z2;
      if (ZComponent.IsNull((ZComponent) z2))
        this.game.globalEffectors.Add(effector);
      else
        z2.effectors.Add(effector);
    }
  }

  public static Color GetHolidayColor()
  {
    return ZSpell.HolidayColors[UnityEngine.Random.Range(0, ZSpell.HolidayColors.Length)];
  }

  public static MyLocation PowerToPosition(ZCreature c, FixedInt rot_z, FixedInt power)
  {
    return Global.Velocity(rot_z, power) + c.position;
  }

  public static int GetSpellMaxDistance(Spell theSpell, ZCreature c)
  {
    return (ZComponent) c == (object) null || c.familiarLevelDruidism == 0 || theSpell.bookOf != BookOf.Druidism && theSpell.spellEnum != SpellEnum.Duplication && !theSpell.IsMinionSpell() ? theSpell.maxDistance : theSpell.maxDistance + (int) ((FixedInt) (theSpell.maxDistance * c.familiarLevelDruidism) * (FixedInt) 157286L);
  }

  public static bool ServerCanFire(
    Spell spell,
    int x,
    int y,
    int secX,
    int secY,
    ZCreature c,
    FixedInt rot_z,
    FixedInt power,
    MyLocation targetPreFire)
  {
    switch (spell.spellEnum)
    {
      case SpellEnum.Arcane_Gate:
      case SpellEnum.Santas_Magic:
        return c.map.CheckCircle(x, y, !((ZComponent) c.tower != (object) null) || c.tower.type != TowerType.Arcane ? c.radius : 32, (ZCreature) null, Inert.mask_movement_NoEffector);
      case SpellEnum.Arcane_Portal:
        if (!c.map.CheckCircle(x, y, c.game.originalSpellsOnly ? 19 : c.radius + 3, (ZCreature) null, Inert.mask_movement_NoEffector))
          return false;
        int num1 = c.radius - 18;
        y = (int) c.OverHeadOffset.y + (int) c.position.y + (!((ZComponent) c.tower != (object) null) || c.tower.type == TowerType.Nature ? 42 : 24) + num1;
        return c.map.CheckCircle((int) c.position.x, y, c.game.originalSpellsOnly ? 19 : c.radius + 3, (ZCreature) null, Inert.mask_movement_NoEffector);
      case SpellEnum.Arcane_Sigil:
        return c.map.CheckCircle(x, y, spell.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
      case SpellEnum.Duplication:
        ZCreature zcreature1 = c.map.PhysicsCollideCreature((ZCreature) null, secY == -1000 ? x : secX, secY == -1000 ? y : secY, Inert.mask_Phantom);
        bool flag1 = (ZComponent) zcreature1 != (object) null && zcreature1.type != CreatureType.Tree && zcreature1.race != CreatureRace.Effector;
        if (!flag1 || secY == -1000)
          return flag1;
        int num2 = ZSpell.GetSpellMaxDistance(spell, c) + zcreature1.radius;
        MyLocation position1 = c.position;
        return MyLocation.Distance(new MyLocation(x, y), position1) + zcreature1.radius < num2 && c.map.CheckCircle(x, y, zcreature1.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
      case SpellEnum.The_ol_swaparoo:
        if (!c.map.InBounds(x, y, c.radius))
          return false;
        return c.parent.GetLevel(BookOf.Illusion) == 5 ? MyLocation.Distance(c.position, new MyLocation(x, y)) < (int) ((FixedInt) spell.maxDistance * (FixedInt) 1572864L) : MyLocation.Distance(c.position, new MyLocation(x, y)) < spell.maxDistance;
      case SpellEnum.Curse_of_Loneliness:
      case SpellEnum.Curse_of_Disabling:
        ZCreature zcreature2 = c.map.PhysicsCollideCreature((ZCreature) null, x, y, Inert.mask_Phantom);
        return (ZComponent) zcreature2 != (object) null && zcreature2.type != CreatureType.Tree && zcreature2.race != CreatureRace.Effector && zcreature2.spells.Count > 0 && zcreature2.parent.team != c.parent.team;
      case SpellEnum.Blood_Pact:
        ZCreature zcreature3 = c.map.PhysicsCollideCreature((ZCreature) null, x, y, Inert.mask_Phantom);
        return (ZComponent) zcreature3 != (object) null && zcreature3.type != CreatureType.Tree && zcreature3.type != CreatureType.Kraken && zcreature3.race != CreatureRace.Effector && zcreature3.health > 25 && c.health > 25;
      case SpellEnum.Blink:
        return MyLocation.Distance(c.position, new MyLocation(x, y)) < spell.maxDistance && c.map.CheckCircle(x, y, !((ZComponent) c.tower != (object) null) || c.tower.type != TowerType.Arcane ? c.radius : 32, (ZCreature) null, Inert.mask_movement_NoEffector);
      case SpellEnum.Healing_Spores:
      case SpellEnum.Spirit_Link:
        MyLocation position2 = c.position;
        int yInt1 = 0;
        if (MyLocation.Distance(new MyLocation(x, y), position2 + new MyLocation(0, yInt1)) + spell.radius >= ZSpell.GetSpellMaxDistance(spell, c))
          return false;
        ZCreature zcreature4 = c.map.PhysicsCollideCreature((ZCreature) null, x, y, Inert.mask_Phantom);
        bool flag2 = (ZComponent) zcreature4 != (object) null && zcreature4.type != CreatureType.Tree && zcreature4.race != CreatureRace.Effector && (spell.spellEnum != SpellEnum.Spirit_Link || zcreature4.parent.team == c.parent.team);
        if (spell.spellEnum == SpellEnum.Spirit_Link && (ZComponent) zcreature4 == (object) c)
          return false;
        return flag2 && spell.spellEnum == SpellEnum.Spirit_Link ? (ZComponent) c.map.PhysicsCollideCreatureCircle(c, (int) c.position.x, (int) c.position.y, zcreature4.radius, Inert.mask_movement_NoEffector) == (object) null : flag2;
      case SpellEnum.Wormhole:
        if (!c.map.CheckCircle(x, y, spell.radius + 1, (ZCreature) null, Inert.mask_movement_NoEffector))
          return false;
        int num3 = c.radius - 18;
        y = (int) c.OverHeadOffset.y + (int) c.position.y + (!((ZComponent) c.tower != (object) null) || c.tower.type == TowerType.Nature ? 42 : 24) + num3;
        return c.map.CheckCircle((int) c.position.x, y, spell.radius + 1, (ZCreature) null, Inert.mask_movement_NoEffector);
      default:
        if (spell.type == CastType.Placement)
        {
          if (spell.spellEnum == SpellEnum.Dive)
            return y <= c.radius + 1 && (int) c.position.y <= c.radius + 1 && x > c.radius - 1 && x < c.map.Width - c.radius && !c.game.world.OverlapCircle((Point) new MyLocation(x, y), c.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector);
          if (spell.spellEnum == SpellEnum.Summon_Kraken)
            return y == 0 && x > spell.radius - 1 && x < c.map.Width - spell.radius && (spell.maxDistance <= 0 || MyLocation.Distance(c.position, new MyLocation(x, y)) <= ZSpell.GetSpellMaxDistance(spell, c) - spell.radius) && !c.game.world.OverlapCircle((Point) new MyLocation(x, y), spell.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector);
          if (spell.spellEnum == SpellEnum.Resurrection)
          {
            int index = (int) rot_z;
            if (c.game.lastMinionToDie.Count <= 0 || index >= c.game.lastMinionToDie.Count || index < 0)
              return false;
            Spell spell1 = Inert.GetSpell(c.game.lastMinionToDie[index].spell);
            Creature component = spell1.toSummon?.GetComponent<Creature>();
            int yInt2 = !spell1.affectedByGravity || spell1.maxDistance <= 150 ? 0 : spell1.maxDistance - 150;
            return MyLocation.Distance(new MyLocation(x, y), c.position + new MyLocation(0, yInt2)) + component.radius < ZSpell.GetSpellMaxDistance(spell1, c) && c.map.CheckCircle(x, y, component.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
          }
          if (spell.spellEnum == SpellEnum.Summon_Titan)
          {
            int index = (int) rot_z;
            if (c.parent.minionBookTitans.Count <= 0 || index >= c.parent.minionBookTitans.Count || index < 0 || c.parent.minionBookTitans[index].used)
              return false;
            Spell spell2 = Inert.GetSpell(c.parent.minionBookTitans[index].spell);
            Creature component = spell2.toSummon?.GetComponent<Creature>();
            int yInt3 = !spell2.affectedByGravity || spell2.maxDistance <= 150 ? 0 : spell2.maxDistance - 150;
            return MyLocation.Distance(new MyLocation(x, y), c.position + new MyLocation(0, yInt3)) + component.radius < ZSpell.GetSpellMaxDistance(spell2, c) && c.map.CheckCircle(x, y, component.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
          }
          MyLocation position3 = c.position;
          if (spell.maxDistance > 0)
          {
            int yInt4 = !spell.affectedByGravity || spell.maxDistance <= 150 ? 0 : spell.maxDistance - 150;
            return MyLocation.Distance(new MyLocation(x, y), position3 + new MyLocation(0, yInt4)) + spell.radius < ZSpell.GetSpellMaxDistance(spell, c) && c.map.CheckCircle(x, y, spell.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
          }
          return spell.spellEnum == SpellEnum.Black_Hole ? !c.game.world.OverlapCircle((Point) new MyLocation(x, y), spell.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector) : c.map.CheckCircle(x, y, spell.radius, (ZCreature) null, Inert.mask_movement_NoEffector);
        }
        if (spell.type == CastType.Tower)
        {
          if ((ZComponent) c.tower != (object) null || c.flying)
            return false;
          Tower component = spell.toSummon.GetComponent<Tower>();
          if (spell.spellEnum == SpellEnum.Sanctuary && c.game.world.OverlapCircle((Point) new MyLocation((int) c.position.x, (int) (c.position.y + component.playerOffsetY + ZTower.AddedOffsetY)), c.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector))
            return false;
          Sprite sprite = spell.toSummon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
          int num4 = (int) c.position.y;
          if (num4 < c.radius && num4 > 0)
            num4 = c.radius;
          if (spell.spellEnum == SpellEnum.Floating_Castle)
            num4 += 3;
          return c.map.CheckTexuture((int) c.position.x, num4 + (sprite.texture.height / 2 - c.radius) - (num4 > c.radius ? 1 : 0), sprite.texture, false, c, false, true);
        }
        if (spell.type == CastType.Blit)
        {
          if (spell.maxDistance > 0 && MyLocation.Distance(c.position, new MyLocation(x, y)) > ZSpell.GetSpellMaxDistance(spell, c) - spell.radius)
            return false;
          Sprite sprite = spell.GetComponent<SpriteRenderer>().sprite;
          return c.map.CheckTexuture(x, y, sprite.texture, spell.spellEnum == SpellEnum.Rock_Slab || spell.spellEnum == SpellEnum.Stepping_Stone || spell.spellEnum == SpellEnum.Christmas_Tree, (ZCreature) null);
        }
        if (spell.spellEnum == SpellEnum.Abduction)
        {
          MyLocation myLocation = Inert.Velocity(rot_z, (ZComponent) c.tower != (object) null && c.tower.type == TowerType.Cosmos || c.type == CreatureType.Cosmic_Horror ? 300 : 200);
          Coords start = new Coords((int) c.position.x, (int) c.position.y);
          Coords end = new Coords((int) (c.position.x + myLocation.x), (int) (c.position.y + myLocation.y));
          Coords coords = c.map.bresenhamsCircleCast(start, end, c, Inert.mask_spell_movement, spell.radius);
          if (coords == null)
            return false;
          ZCreature zcreature5 = c.game.map.PhysicsCollideCreature(c, coords.x, coords.y);
          return (ZComponent) zcreature5 != (object) null && (ZComponent) zcreature5.tower == (object) null;
        }
        if (spell.type == CastType.Naplem)
        {
          if (spell.spellEnum != SpellEnum.Vine_Bridge)
            return true;
          int force = (int) spell.speedMax + (int) spell.speedMax / 5 * c.familiarLevelNature;
          MyLocation myLocation = Inert.Velocity(rot_z, force);
          Coords start = new Coords((int) c.position.x, (int) c.position.y);
          Coords end = new Coords((int) (c.position.x + myLocation.x), (int) (c.position.y + myLocation.y));
          return c.map.bresenhamsLineCast(start, end, c, (ZSpell) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom) != null;
        }
        if (spell.spellEnum.IsFlight())
          return (ZComponent) c.tower == (object) null;
        return spell.type != CastType.TargetOnly || spell.maxDistance <= 10 || MyLocation.Distance(c.position, new MyLocation(x, y)) < spell.maxDistance;
    }
  }

  public static bool CanFire(Spell theSpell)
  {
    if ((ZComponent) Player.Instance.selected == (object) null)
      return false;
    bool flag = ZSpell.ServerCanFire(theSpell, (int) Player.Instance.mouseWorldPos.x, (int) Player.Instance.mouseWorldPos.y, (int) Player.Instance.secTarget.x, (int) Player.Instance.secTarget.y, Player.Instance.selected, Player.Instance.rot_z, Player.Instance.power, Player.Instance.target);
    if (Player.Instance.meter_subs[6].activeInHierarchy)
    {
      if (Player.Instance.selectedSpell.spellEnum == SpellEnum.The_ol_swaparoo && flag)
      {
        ZCreature selected = Player.Instance.selected;
        ZCreature zcreature = selected.map.PhysicsCollideCreature((ZCreature) null, (int) Player.Instance.mouseWorldPos.x, (int) Player.Instance.mouseWorldPos.y, Inert.mask_Phantom);
        if ((ZComponent) zcreature != (object) null && (ZComponent) zcreature != (object) selected && zcreature.type != CreatureType.Tree && zcreature.race != CreatureRace.Effector)
          Player.Instance.meter_subs[6].GetComponent<Image>().color = Color.blue;
        else
          Player.Instance.meter_subs[6].GetComponent<Image>().color = Color.red;
      }
      else
        Player.Instance.meter_subs[6].GetComponent<Image>().color = flag ? Color.red : Color.white;
    }
    return flag;
  }

  public static void FireWhich(
    Spell theSpell,
    ZCreature c,
    MyLocation pos,
    FixedInt rot_z,
    FixedInt power,
    MyLocation target,
    MyLocation secTarget,
    int spellSlot = 0,
    bool extended = false,
    SpellSlot slot = null,
    bool fromArmageddon = false)
  {
    c.animator?.Play(AnimateState.Attack);
    if (c.game.isClient && !c.game.resyncing && (!theSpell.spellEnum.IsFlight() || !c.flying))
      AudioManager.PlayFromSource(theSpell.castClip, AudioManager.instance.sourceCastSpell);
    switch (theSpell.spellEnum)
    {
      case SpellEnum.Arcane_Arrow:
        ZSpell.FireArcaneArrows(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Arcane_Bomb:
        ZSpell.FireArcaneBomb(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Arcane_Tower:
      case SpellEnum.Watchtower:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Arcane_Energiser:
        ZSpell.FireEffector(theSpell, c, pos, rot_z, power, false);
        break;
      case SpellEnum.Arcane_Gate:
        ZSpell.FireTeleport(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Arcane_Portal:
        ZSpell.FireArcanePortal(theSpell, c, pos, rot_z, power, spellSlot, target);
        break;
      case SpellEnum.Summon_Imps:
      case SpellEnum.Monkey:
      case SpellEnum.Little_Devil:
      case SpellEnum.Summon_Tutorial_Target:
      case SpellEnum.Summon_Dryad:
      case SpellEnum.Summon_Beehive:
      case SpellEnum.Summon_Myth:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Imp_Destruction:
        ZSpell.FireImpDestruction(theSpell, c, target);
        break;
      case SpellEnum.Arcane_Glyph:
        if (theSpell.type == CastType.Flight)
          target = pos;
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorArcaneGlyph(theSpell, c, target));
        break;
      case SpellEnum.Arcane_Sigil:
        ZSpell.FireArcaneSigil(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Arcane_Flash:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 10);
        break;
      case SpellEnum.Summon_Dragon_Egg:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Fire_Ball:
        ZSpell.FireFireball(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Fire_Arrow:
        ZSpell.FireFireArrow(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Lava_Bomb:
        ZSpell.FireFireball(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Magma_Bomb:
        ZSpell.FireFireball(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Flame_Shield:
        ZSpell.FireEffector(theSpell, c, pos, rot_z, power, true);
        break;
      case SpellEnum.Flame_Wall:
        ZSpell.FireFlameWall(theSpell, c, target, (FixedInt) 0, (FixedInt) 0);
        break;
      case SpellEnum.Napalm:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorNapalm(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Napalm_Bomb:
        ZSpell.FireFireball(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Rain_of_Fire:
        ZSpell.FireRainOfFire(theSpell, c, pos, rot_z, power, target, extended);
        break;
      case SpellEnum.Rain_of_Arrows:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorRainOfArrows(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Volcano:
        ZSpell.FireVolcano(theSpell, c, target);
        break;
      case SpellEnum.Summon_Flame_Dragon:
      case SpellEnum.Summon_Storm_Dragon:
      case SpellEnum.Frost_Dragon:
      case SpellEnum.Steam_Dragon:
      case SpellEnum.Arcane_Dragon:
      case SpellEnum.Corrupt_Dragon:
        ZCreature sum1 = ZSpell.FireSummon(theSpell, c.game, c, target);
        if (!extended)
          break;
        ZSpell.TryMountOnSummon(sum1, c);
        break;
      case SpellEnum.Pebble_Shot:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Scatter_Rock:
      case SpellEnum.Pinecone:
      case SpellEnum.Acorn:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Quake:
        ZSpell.FireQuake(theSpell, c);
        break;
      case SpellEnum.Disruption:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Mud_Ball:
      case SpellEnum.Old_Mud_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Mega_Boulder:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Rock_Slab:
      case SpellEnum.Stepping_Stone:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.Fortress:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Dwarf:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Rock_Golem:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Meteor:
        ZSpell.FireMeteor(theSpell, c, (FixedInt) ((double) c.transformscale > 0.0 ? 1 : -1), target);
        break;
      case SpellEnum.Fissure:
        ZSpell.FireVolcano(theSpell, c, target);
        break;
      case SpellEnum.Thunder_Shock:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorThunderShock(theSpell, c, pos, rot_z, extended));
        if (ZComponent.IsNull((ZComponent) c))
          break;
        ZEffector.RechargeElectrostaticCharges(c.game);
        break;
      case SpellEnum.Chain_Lightning:
        ZSpell.BaseFire(theSpell, c, pos, Quaternion.identity, Inert.Velocity(rot_z, (FixedInt) theSpell.speedMax + power));
        if (ZComponent.IsNull((ZComponent) c))
          break;
        ZEffector.RechargeElectrostaticCharges(c.game);
        break;
      case SpellEnum.Wind_Shield:
        ZSpell.FireEffector(theSpell, c, pos, rot_z, power, true);
        break;
      case SpellEnum.Hurricane:
      case SpellEnum.Spirit_Hurricane:
        foreach (ZCreature zcreature in c.parent.controlled)
        {
          foreach (SpellSlot spell in zcreature.spells)
          {
            if (spell.spell.spellEnum == SpellEnum.Hurricane || spell.spell.spellEnum == SpellEnum.Spirit_Hurricane)
            {
              spell.SetTurnFired = c.parent.localTurn;
              break;
            }
          }
        }
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorFireHurricane(theSpell, c, theSpell.spellEnum == SpellEnum.Spirit_Hurricane));
        break;
      case SpellEnum.Shock_Bomb:
      case SpellEnum.Rusty_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Storm_Shield:
        ZSpell.FireEffectorStormShield(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Cyclops:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Conductor_Rod:
      case SpellEnum.Electrostatic_Charge:
        ZSpell.FireConductorRod(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Storm_Spirit:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Flight:
        ZSpell.FireFlight(theSpell, c);
        break;
      case SpellEnum.Storm:
        ZSpell.FireStorm(theSpell, c, target);
        break;
      case SpellEnum.Ice_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Ice_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Frost_Shards:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Frost_Arrow:
        ZSpell.FireGenericArrow(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Snowball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Blizzard:
        ZSpell.FireStorm(theSpell, c, target);
        break;
      case SpellEnum.Ice_Shield:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorIceShield(theSpell, c));
        break;
      case SpellEnum.Ice_Castle:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Sylph:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Frost_Giant:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Comet:
      case SpellEnum.Arcane_Meteor:
        ZSpell.FireMeteor(theSpell, c, (FixedInt) ((double) c.transformscale > 0.0 ? 1 : -1), target);
        break;
      case SpellEnum.Den_of_Darkness:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Rain_of_Chaos:
        ZSpell.FireRainOfChaos(theSpell, c, target);
        break;
      case SpellEnum.Drain_Bolt:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Death_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Swarm:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Dark_Knight:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Raise_Dead:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Wraith:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Aura_of_Decay:
      case SpellEnum.Arcane_Mist:
        ZSpell.FireAuraOfDecay(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Dark_Defences:
        ZSpell.FireDarkDefenses(theSpell, c);
        break;
      case SpellEnum.Swallowing_Pit:
        ZSpell.FireSwallowingPit(theSpell, c);
        break;
      case SpellEnum.Lichdom:
        ZSpell.FireLichdom(theSpell, c);
        break;
      case SpellEnum.Protection_Shield:
        ZSpell.FireProtectionShield(theSpell, c);
        break;
      case SpellEnum.Sky_Ray:
        c.game.ongoing.RunSpell(ZSpell.FireSkyRay(theSpell, c, target));
        break;
      case SpellEnum.Shining_Bolt:
        ZSpell.FireShiningBolt(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Rising_Star:
        ZSpell.FireStorm(theSpell, c, target, 700);
        break;
      case SpellEnum.Summon_Pegasus:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Paladin:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Forest_Seed:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.Summon_Pixies:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Sphere_of_Healing:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Castle_of_Light:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Invulnerability_Shield:
        ZSpell.FireInvulnerablityShield(theSpell, c);
        break;
      case SpellEnum.Shining_Power:
        ZSpell.FireFlight(theSpell, c, true);
        break;
      case SpellEnum.Thorn_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Thorn_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Vine_Whip:
      case SpellEnum.Ent_Whip:
        c.game.ongoing.RunSpell(ZSpell.IEFireVineWhip(theSpell, c, pos, rot_z, power));
        break;
      case SpellEnum.Vine_Bridge:
        ZSpell.FireVineBridge(theSpell, c, pos, rot_z, spellSlot);
        break;
      case SpellEnum.Entangle:
      case SpellEnum.Entangle_Kraken:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Vine_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Man_Trap:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Sanctuary:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Elves:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Flurry:
        ZSpell.FireFlurry(theSpell, c, target);
        break;
      case SpellEnum.Nature_s_Wrath:
        ZSpell.FireNaturesWrath(theSpell, c, target);
        break;
      case SpellEnum.Vine_Bloom:
        ZSpell.FireVineBloom(theSpell, c);
        break;
      case SpellEnum.Water_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Maelstrom:
        ZSpell.FireVolcano(theSpell, c, target);
        break;
      case SpellEnum.Summon_Water_Trolls:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Hydration:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Deluge:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorDeluge(c, theSpell, new MyLocation(target.x, (FixedInt) (c.map.Height + 1000)), -20, 20, 30));
        break;
      case SpellEnum.English_Summer:
      case SpellEnum.Acid_Rain:
        ZSpell.FireStorm(theSpell, c, target);
        break;
      case SpellEnum.Brine_Bolt:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorBrineBolt(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Brine_Bomb:
      case SpellEnum.Brine_Burst:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Brine_Goblin:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Rain_of_Clams:
        ZSpell.FireRainOfClams(theSpell, c, target);
        break;
      case SpellEnum.Ocean_s_Fury:
        c.game.ongoing.RunSpell(ZSpell.IEOceansFury(theSpell, c));
        break;
      case SpellEnum.Summon_Water_Lord:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Static_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Static_Shield:
        ZSpell.FireStaticShield(theSpell, c);
        break;
      case SpellEnum.Mechanical_Arrow:
        ZSpell.FireGenericArrow(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Cog_Fall:
        ZSpell.FireCogFall(theSpell, c, target);
        break;
      case SpellEnum.Recall_Device:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Calling_Bell:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Clock_Tower:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Cuckoo_Clock:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Blast_From_The_Past:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, (FixedInt) 1 + (power > 0 ? power * 139806L : (FixedInt) 0));
        break;
      case SpellEnum.Clockwork_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Redo:
        ZSpell.FireRedo(theSpell, c);
        break;
      case SpellEnum.Banish:
        ZSpell.FireBanish(theSpell, c);
        break;
      case SpellEnum.Self_Destruct:
        ZSpell.FireSelfDestruct(theSpell, c);
        break;
      case SpellEnum.Mine:
        ZSpell.FireMine(theSpell, c, target);
        break;
      case SpellEnum.Kablam:
        ZSpell.FireKablam(theSpell, c, pos, rot_z);
        break;
      case SpellEnum.Wallop:
      case SpellEnum.Chomp:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 0);
        break;
      case SpellEnum.Storm_Dragon_Breath:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorStormDragonBreath(theSpell, c, pos, rot_z));
        break;
      case SpellEnum.Shock_Shield:
        ZSpell.FireEffectorStormShield(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Sylph_Arrow:
        c.game.ongoing.RunSpell(ZSpell.FireSylphArrow(theSpell, c, pos, rot_z, target));
        break;
      case SpellEnum.Charge:
      case SpellEnum.Rampage:
        ZSpell.FireCharge(theSpell, c, target);
        break;
      case SpellEnum.Zombie_Breath:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Sunder:
      case SpellEnum.Smash:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 2);
        if (!c.game.isClient || !((UnityEngine.Object) c.leftArm != (UnityEngine.Object) null))
          break;
        SpinningSword component1 = c.leftArm.transform.parent.GetComponent<SpinningSword>();
        if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
          break;
        component1.enabled = true;
        break;
      case SpellEnum.Fairy_Ring:
        ZSpell.FireFairyRing(theSpell, c, target);
        break;
      case SpellEnum.Volley:
        ZSpell.FireVolley(theSpell, c, target);
        break;
      case SpellEnum.Dive:
        ZSpell.FireDive(theSpell, c, target, extended);
        break;
      case SpellEnum.Recall:
        ZSpell.FireRecall(theSpell, c, target);
        break;
      case SpellEnum.New_Years_Rocket:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Snowman:
      case SpellEnum.Summon_Snowman2:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Reindeer:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Presents:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Christmas_Tree:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.Thanksgiving_Dinner:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Firecrackers:
      case SpellEnum.Erosion:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorFireCrackers(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Santas_Magic:
        ZSpell.FireTeleport(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Jingle_Boom:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Coal:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorFireCoal(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Tree_House:
        c.parent.hasCastTheFourSeasons = true;
        if (c.game.isClient)
          HUD.instance?.UpdateSeasonIcon(c.game.currentSeason);
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Firework_Show:
        c.game.ongoing.RunSpell(ZSpell.IEFireworkShow(theSpell, c, pos, target));
        break;
      case SpellEnum.Air_Surge:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, (FixedInt) 1);
        if (ZComponent.IsNull((ZComponent) c) || c.isDead || c.phantom && c.pawn || !((ZComponent) c.tower == (object) null))
          break;
        if (c.isMoving)
        {
          ZCreature zcreature = c;
          zcreature.addedVelocity = zcreature.addedVelocity + Inert.Velocity(rot_z + 180, (FixedInt) theSpell.explisiveForce);
          c.addVelocity = true;
          break;
        }
        c.velocity = Inert.Velocity(rot_z + 180, (FixedInt) theSpell.explisiveForce);
        c.StartMoving(false);
        break;
      case SpellEnum.Apparition:
        ZSpell.FireApparition(c);
        break;
      case SpellEnum.Duplication:
        ZCreature col = c.map.PhysicsCollideCreature((ZCreature) null, (int) secTarget.x, (int) secTarget.y, Inert.mask_Phantom);
        if (!((ZComponent) col != (object) null) || col.type == CreatureType.Tree || col.race == CreatureRace.Effector)
          break;
        ZSpell.CreateDuplicate(c, col, target);
        break;
      case SpellEnum.Color_Spray:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Floating_Castle:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Glide:
        ZSpell.FireGlide(theSpell, c);
        break;
      case SpellEnum.Magical_Barrier:
        ZSpell.FireMammatusCloud(theSpell, c, target);
        break;
      case SpellEnum.Summon_Phantom:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Vortex:
        ZSpell.FireMammatusCloud(theSpell, c, target);
        break;
      case SpellEnum.Whisper_Arrow:
        ZSpell.FireGenericArrow(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Whisper_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Social_Distancing:
        ZSpell.FireEffector(theSpell, c, pos, rot_z, power, true);
        break;
      case SpellEnum.Frost_Leap:
        MyLocation position1 = c.position;
        if (c.game.isClient)
          ZComponent.Instantiate<GameObject>(theSpell.explosion, position1.ToSinglePrecision(), Quaternion.identity);
        c.velocity = Inert.Velocity(rot_z, Mathd.LerpUnclamped((FixedInt) theSpell.speedMin, ((FixedInt) theSpell.speedMax + (FixedInt) (c.familiarLevelFrost * (c.familiarLevelFrost < 4 ? 3 : 4))) * (c.GetAppliedMass < 1 ? (FixedInt) 838860L : (FixedInt) 1), power));
        c.StartMoving(false);
        break;
      case SpellEnum.Hatch:
        ZSpell.FireHatch(theSpell, c);
        break;
      case SpellEnum.Meteor_Shower:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorMeteorShower(theSpell, c));
        break;
      case SpellEnum.Time_Dilation:
        c.game.PlayersMaxTurnTime += (float) Mathf.Max(5, 5 * c.familiarLevelCogs);
        HUD.instance?.UpdateTime();
        break;
      case SpellEnum.Summon_Kraken:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.From_the_Depths:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Fire_Cannon:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        ZSpell.FireGeneric(theSpell, c, pos, rot_z - 15, power);
        ZSpell.FireGeneric(theSpell, c, pos, rot_z + 15, power);
        break;
      case SpellEnum.Summon_Mountain_Goat:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Fire_Wave:
        ZSpell.FireFireWave(theSpell, c);
        break;
      case SpellEnum.Gift_of_Giving:
        ZSpell.AddPresents(theSpell.explosion, c);
        if (!c.game.isClient || c.game.resyncing || !((UnityEngine.Object) theSpell.explosion != (UnityEngine.Object) null))
          break;
        ZComponent.Instantiate<GameObject>(theSpell.explosion, c.position.ToSinglePrecision(), Quaternion.identity);
        break;
      case SpellEnum.Forestation:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.The_ol_swaparoo:
        ZCreature c1 = c.map.PhysicsCollideCreature((ZCreature) null, (int) target.x, (int) target.y, Inert.mask_Phantom);
        if ((ZComponent) c1 != (object) null && (ZComponent) c1 != (object) c && c1.type != CreatureType.Tree && c1.race != CreatureRace.Effector)
        {
          MyLocation position2 = c.position;
          MyLocation position3 = c1.position;
          if ((ZComponent) c1.tower != (object) null)
            c1.tower.SetPositionNoTrigger(position2);
          else
            c1.SetPositionNoTrigger(position2);
          if ((ZComponent) c.tower != (object) null)
            c.tower.SetPositionNoTrigger(position3);
          else
            c.SetPositionNoTrigger(position3);
          ZSpell.FireTeleportNoDetower(theSpell, c1, position2);
          ZSpell.FireTeleportNoDetower(theSpell, c, position3);
          break;
        }
        ZSpell.FireTeleportNoDetower(theSpell, c, target);
        break;
      case SpellEnum.Whistling_Winds:
      case SpellEnum.Vampire_Bat:
        ZSpell.FireFlight(theSpell, c);
        break;
      case SpellEnum.Blood_Lust:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 0);
        ZSpell.RandomBlood(c.game, c.position + Inert.Velocity(rot_z) * 12);
        if (c.game.isClient && (UnityEngine.Object) c.leftArm != (UnityEngine.Object) null && c.type == CreatureType.Gargoyle)
        {
          SpinningSword component2 = c.leftArm.transform.parent.GetComponent<SpinningSword>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
            component2.enabled = true;
        }
        if (ZComponent.IsNull((ZComponent) c) || c.isDead || c.isPawn)
          break;
        c.ApplyDamage(SpellEnum.Blood_Lust, DamageType.None, 15, c, c.game.turn);
        break;
      case SpellEnum.Curse_of_Loneliness:
        ZCreature zcreature1 = c.map.PhysicsCollideCreature((ZCreature) null, (int) target.x, (int) target.y, Inert.mask_Phantom);
        if ((ZComponent) zcreature1 != (object) null && (ZComponent) zcreature1 != (object) c && zcreature1.type != CreatureType.Tree && zcreature1.race != CreatureRace.Effector)
        {
          foreach (SpellSlot spell in zcreature1.spells)
          {
            if ((UnityEngine.Object) spell.spell != (UnityEngine.Object) null && spell.spell.IsMinionSpell())
              spell.disabledturn = zcreature1.parent.localTurn + 1;
          }
        }
        using (List<SpellSlot>.Enumerator enumerator = c.spells.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SpellSlot current = enumerator.Current;
            if ((UnityEngine.Object) current.spell != (UnityEngine.Object) null && current.spell.IsMinionSpell())
              current.disabledturn = c.parent.localTurn;
          }
          break;
        }
      case SpellEnum.Blood_Mist:
        ZSpell.FireAuraOfDecay(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Barrage_of_Bones:
        c.game.ongoing.RunSpell(ZSpell.FireBarrageOfBones(theSpell, c, target, theSpell.amount));
        break;
      case SpellEnum.Flesh_Wound:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Blood_Bath:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Curse_of_Disabling:
        ZCreature zcreature2 = c.map.PhysicsCollideCreature((ZCreature) null, (int) target.x, (int) target.y, Inert.mask_Phantom);
        string str = "";
        if ((ZComponent) zcreature2 != (object) null && zcreature2.type != CreatureType.Tree && zcreature2.race != CreatureRace.Effector && zcreature2.spells.Count > 0)
        {
          int index1 = 0;
          for (int index2 = 0; index2 < 20; ++index2)
          {
            index1 = c.game.RandomInt(0, zcreature2.spells.Count);
            if (zcreature2.spells[index1].disabledturn <= zcreature2.parent.localTurn && !zcreature2.spells[index1].MaxUsesReached())
            {
              zcreature2.spells[index1].disabledturn = zcreature2.parent.localTurn + 1;
              if (zcreature2.game.isClient)
              {
                str = zcreature2.spells[index1].spell.name;
                break;
              }
              break;
            }
          }
          if (zcreature2.spells.Count > 1)
          {
            for (int index3 = 0; index3 < 20; ++index3)
            {
              int index4 = c.game.RandomInt(0, zcreature2.spells.Count);
              if (index4 != index1 && zcreature2.spells[index4].disabledturn <= zcreature2.parent.localTurn && !zcreature2.spells[index4].MaxUsesReached())
              {
                index1 = index4;
                break;
              }
            }
          }
          if (zcreature2.spells[index1].disabledturn <= zcreature2.parent.localTurn)
          {
            zcreature2.spells[index1].disabledturn = zcreature2.parent.localTurn + 1;
            if (zcreature2.game.isClient)
              str += str.Length > 0 ? "/" + zcreature2.spells[index1].spell.name : zcreature2.spells[index1].spell.name;
          }
          if (!zcreature2.game.isClient || zcreature2.game.resyncing)
            break;
          if (str.Length > 0)
          {
            ChatBox instance = ChatBox.Instance;
            if (instance == null)
              break;
            instance.NewChatMsg("", c.parent.name + " disabled " + (zcreature2.isPawn ? zcreature2.parent.name + "'s " + zcreature2.name + "'s \"" : zcreature2.parent.name + "'s \"") + str + "\"", (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
            break;
          }
          ChatBox.Instance?.NewChatMsg("", c.parent.name + " cast Curse of Disabling but did not disable anything...", (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
          break;
        }
        if (!c.game.isClient || c.game.resyncing)
          break;
        ChatBox.Instance?.NewChatMsg("", c.parent.name + " cast Curse of Disabling but did not disable anything...", (Color) ColorScheme.GetColor(Global.ColorWhiteText), "", ChatOrigination.System);
        break;
      case SpellEnum.Summon_Blood_Bank:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Gargoyle:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Blood_Craze:
      case SpellEnum.Blood_Clot:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorPebbleShot(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Summon_Vampire:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Infection:
        ZSpell.FireGenericArrow(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Blood_Pact:
        ZCreature zcreature3 = c.map.PhysicsCollideCreature((ZCreature) null, (int) target.x, (int) target.y, Inert.mask_Phantom);
        if (!((ZComponent) zcreature3 != (object) null) || !((ZComponent) zcreature3 != (object) c) || zcreature3.type == CreatureType.Tree || zcreature3.type == CreatureType.Kraken || zcreature3.race == CreatureRace.Effector || !((ZComponent) zcreature3 != (object) c) || c.health <= 25 || zcreature3.health <= 25)
          break;
        if ((ZComponent) zcreature3.pact != (object) null)
          zcreature3.pact.pact = (ZCreature) null;
        if ((ZComponent) c.pact != (object) null)
          c.pact.pact = (ZCreature) null;
        int num1 = zcreature3.health + c.health;
        int num2 = num1 / 2;
        int num3 = num1 - num2;
        zcreature3.health = num3;
        c.health = num2;
        if (c.health > c.maxHealth)
        {
          c.health = c.maxHealth;
          zcreature3.health = c.health;
        }
        if (zcreature3.health > zcreature3.maxHealth)
          zcreature3.maxHealth = zcreature3.health;
        if (zcreature3.maxHealth < c.maxHealth)
          zcreature3.maxHealth = c.maxHealth;
        zcreature3.UpdateHealthTxt();
        c.UpdateHealthTxt();
        if (zcreature3.team == c.team)
        {
          zcreature3.pact = c;
          c.pact = zcreature3;
        }
        else
        {
          zcreature3.pact = (ZCreature) null;
          c.pact = (ZCreature) null;
        }
        if (!((UnityEngine.Object) c.transform != (UnityEngine.Object) null) || !((UnityEngine.Object) zcreature3.transform != (UnityEngine.Object) null))
          break;
        ParticleBloodSyphon particleBloodSyphon = ZComponent.Instantiate<ParticleBloodSyphon>(ClientResources.Instance.bloodSyphon, c.transform.position, Quaternion.identity, c.game.GetMapTransform());
        particleBloodSyphon.start = c.transform.position;
        particleBloodSyphon.end = zcreature3.transform.position;
        particleBloodSyphon.velocity = particleBloodSyphon.end - particleBloodSyphon.start;
        break;
      case SpellEnum.Dark_Totem:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Stone_Form:
        c.GargoyleMorph(c.flying);
        break;
      case SpellEnum.Resurrection:
        int index5 = (int) rot_z;
        if (c.game.lastMinionToDie.Count <= 0 || index5 >= c.game.lastMinionToDie.Count || index5 < 0)
          break;
        ZGame.Resurection resurection = c.game.lastMinionToDie[index5];
        c.game.lastMinionToDie.RemoveAt(index5);
        Spell spell1 = Inert.GetSpell(resurection.spell);
        MyLocation target1 = target;
        if (!((UnityEngine.Object) spell1 != (UnityEngine.Object) null))
          break;
        ZCreature zcreature4 = ZSpell.FireSummon(spell1, c.game, c, target1, 1, true);
        if (zcreature4.isDead || c.familiarLevelBlood >= 5)
          break;
        zcreature4.DoDamage((int) ((FixedInt) zcreature4.health * (FixedInt) 105906L * (5 - c.familiarLevelBlood)));
        break;
      case SpellEnum.Blink:
        ZSpell.FireTeleport(theSpell, c, pos, rot_z, power, target);
        break;
      case SpellEnum.Call_of_the_Void:
        ZSpell.FireCallOfTheVoid(theSpell, c, target);
        break;
      case SpellEnum.Sand_Blast:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorSandBlast(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Summon_King_Monarch:
      case SpellEnum.Summon_Monarchs:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Life_Dew:
        ZSpell.FireLifeDiew(theSpell, c, target);
        break;
      case SpellEnum.Breeze:
        ZSpell.FireBreeze(theSpell, c);
        break;
      case SpellEnum.Morning_Sun:
        ZSpell.FireMorningSun(theSpell, c, target);
        break;
      case SpellEnum.Autumn_Leaves:
        ZSpell.FireAutumnLeaves(theSpell, c, target, extended);
        break;
      case SpellEnum.The_Four_Seasons:
        c.parent.hasCastTheFourSeasons = true;
        if (c.game.isClient && (UnityEngine.Object) Player.Instance != (UnityEngine.Object) null && c.parent == Player.Instance.person)
          HUD.instance.usedFourSeasons = true;
        ZEffector zeffector1 = new ZEffector();
        zeffector1.type = EffectorType.Remove_Four_Seasons;
        zeffector1.whoSummoned = c;
        zeffector1.MaxTurnsAlive = int.MaxValue;
        zeffector1.active = false;
        zeffector1.game = c.game;
        bool flag1 = true;
        for (int index6 = 0; index6 < c.spells.Count; ++index6)
        {
          if (c.spells[index6].spell.spellEnum == SpellEnum.Seasonal)
          {
            flag1 = false;
            break;
          }
        }
        if (flag1)
          c.spells.Add(new SpellSlot(Inert.Instance.spells["Seasonal"]));
        c.effectors.Add(zeffector1);
        if (c.game.isClient)
          HUD.instance?.UpdateSeasonIcon(c.game.currentSeason);
        target.y = (FixedInt) (c.map.Height + 300);
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(c, Inert.Instance.rain, target, -75, 75, 7));
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorAutumnLeaves(c, Inert.Instance.spells["Autumn Leaves"], target, -75, 75, 7, extended));
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorBlizzard(c, Inert.Instance.Snow, target, -75, 75, 7));
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorStorm(c, target, -75, 75, 4));
        if (!c.game.isClient || c.game.resyncing)
          break;
        UnityEngine.Object.Destroy((UnityEngine.Object) ZComponent.Instantiate<GameObject>(Inert.Instance.spells["Storm"].toSummon, target.ToSinglePrecision(), Quaternion.identity, c.game.GetMapTransform()), 5f);
        break;
      case SpellEnum.Seasonal:
        c._FourSeasonsCastAtEndOfTurn = true;
        c._FourSeasonsLocation = target;
        break;
      case SpellEnum.Flutter:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 0);
        break;
      case SpellEnum.Sacrifice:
        ZSpell.FireSacrifice(theSpell, c);
        break;
      case SpellEnum.Melt:
        ZSpell.FireMelt(theSpell, c);
        break;
      case SpellEnum.Snowbolt:
        ZSpell.FireSnowbolt(theSpell, c, rot_z, power, target);
        break;
      case SpellEnum.Cogmobile:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Blood_Siphon:
        ZSpell.FireBloodSiphon(theSpell, c);
        break;
      case SpellEnum.Miner_Market:
        ZSpell.FireMinerMarket(theSpell, c, (int) target.y, true);
        break;
      case SpellEnum.Rock_Blaster:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorRockBlaster(theSpell, c, pos, Quaternion.identity, rot_z, power, target));
        break;
      case SpellEnum.Shaft:
        ZSpell.FireShaft(theSpell, c, target);
        break;
      case SpellEnum.Miner_Map:
        ZSpell.FireMinerMap(theSpell, c, target);
        break;
      case SpellEnum.Summon_Bees:
        ZSpell.FireSummon(theSpell, c.game, c, target, 1);
        break;
      case SpellEnum.Snow_Globe2:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Summon_Will_o_the_Wisp:
      case SpellEnum.Summon_Tiger:
      case SpellEnum.Compete:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Summon_Boar:
      case SpellEnum.Summon_Alpha_Wolf:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Bear_Form:
        ZSpell.FireWerewolf(theSpell, c);
        break;
      case SpellEnum.Enchanted_Axes:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorImbuedAxes(theSpell, c, pos, Quaternion.identity, rot_z, power, target, slot));
        break;
      case SpellEnum.Harmony:
        ZSpell.FireCommune(theSpell, c);
        break;
      case SpellEnum.Verdant_Javelin:
        ZSpell zspell = ZSpell.FireGenericReturn(theSpell, c, pos, rot_z, power);
        if (slot == null)
          break;
        zspell.damage += slot.bonusDmg;
        slot.bonusDmg += 7;
        break;
      case SpellEnum.Faiere_Jump:
      case SpellEnum.Pounce:
        if ((ZComponent) c.tower != (object) null)
          break;
        MyLocation position4 = c.position;
        bool flag2 = false;
        if (c.game.world.OverlapCircle(new Point((int) pos.x, (int) pos.y), c.radius, c.collider, Inert.mask_movement_NoEffector))
        {
          flag2 = true;
          pos = position4;
        }
        FixedInt fixedInt1 = rot_z > 90 ? rot_z - 90 : (FixedInt) 90 - rot_z;
        if (fixedInt1 > 90)
          fixedInt1 = (FixedInt) 180 - fixedInt1;
        else if (fixedInt1 < 0)
          fixedInt1 = (FixedInt) 90 + fixedInt1;
        FixedInt fixedInt2 = Mathd.Lerp((FixedInt) theSpell.speedMax, (FixedInt) theSpell.speedMin, fixedInt1 / 90);
        MyLocation myLocation = Inert.Velocity(rot_z, c.GetAppliedMass < 1 ? fixedInt2 * (FixedInt) 838860L : fixedInt2);
        if (!flag2)
          c.SetPosition(pos);
        c.velocity = myLocation;
        if (theSpell.spellEnum == SpellEnum.Pounce)
        {
          c.moving = c.game.ongoing.RunCoroutine(c.WolfLeap(50));
          c.game.CreatureMoveSurroundings(position4, c.radius, c.collider);
          break;
        }
        c.StartMoving(true);
        c.game.CreatureMoveSurroundings(position4, c.radius, c.collider);
        if (!((UnityEngine.Object) theSpell.toSummon != (UnityEngine.Object) null) || !c.game.isClient || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null) || c.game.resyncing)
          break;
        ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position, Quaternion.identity, c.transform);
        break;
      case SpellEnum.Bear_Claw:
      case SpellEnum.Swipe:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 2);
        break;
      case SpellEnum.Grove_Renewal:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.Prickly_Barrier:
        ZSpell.FireFlameWall(theSpell, c, target, rot_z, power);
        break;
      case SpellEnum.Healing_Spores:
        ZCreature zcreature5 = c.map.PhysicsCollideCreature((ZCreature) null, (int) target.x, (int) target.y, Inert.mask_Phantom);
        if (!((ZComponent) zcreature5 != (object) null))
          break;
        zcreature5.DoHeal(theSpell.damage * (zcreature5.isPawn ? 2 : 1), enemy: c);
        if (!((UnityEngine.Object) zcreature5.transform != (UnityEngine.Object) null) || c.game.resyncing)
          break;
        ZComponent.Instantiate<GameObject>(theSpell.explosion, zcreature5.transform.position, Quaternion.identity, c.game.GetMapTransform());
        ZComponent.Instantiate<GameObject>(theSpell.toSummon, zcreature5.transform.position, Quaternion.identity, c.game.GetMapTransform());
        break;
      case SpellEnum.Bite:
      case SpellEnum.Large_Bite:
        ZSpell.FireArcaneFlash(theSpell, c, pos, rot_z, power, 2);
        break;
      case SpellEnum.Spirit_Link:
        ZSpell.FireSpiritLink(theSpell, c, target);
        break;
      case SpellEnum.Star_Bolt:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorRockBlaster(theSpell, c, pos, Quaternion.identity, rot_z, power, target, new FixedInt?((FixedInt) 0)));
        break;
      case SpellEnum.Shooting_Stars:
        ZSpell.FireShootingStars(theSpell, c, target);
        break;
      case SpellEnum.Gravity_Pulse:
      case SpellEnum.Gravity_Well:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Dark_Matter_Bomb:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Wormhole:
        ZSpell.FireArcanePortal(theSpell, c, pos, rot_z, power, spellSlot, target);
        break;
      case SpellEnum.Collision_Course:
      case SpellEnum.Drone_Strike:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Fusion:
        c.fusion = c.parent.localTurn + 5;
        c.ApplyHeal(theSpell.damageType, theSpell.damage, c);
        c.UpdateHealthTxt();
        if (!((UnityEngine.Object) c.transform != (UnityEngine.Object) null))
          break;
        ZComponent.Instantiate<GameObject>(theSpell.toSummon, c.transform.position, Quaternion.identity, c.transform).GetComponent<ParticleFusion>().creature = c;
        break;
      case SpellEnum.MACAIR:
        ZSpell.FireTower(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Abduction:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorAbduction(theSpell, c, pos, rot_z, extended, target, fromArmageddon));
        break;
      case SpellEnum.Cosmic_Horror:
      case SpellEnum.Summon_Drone:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Black_Hole:
        ZSpell.FireBlackHole(theSpell, c, target);
        break;
      case SpellEnum.Supernova:
        ZSpell.FireSuperNova(theSpell, c, target);
        break;
      case SpellEnum.Starfire:
      case SpellEnum.Starfire_Shard:
      case SpellEnum.Star_Dust:
      case SpellEnum.Star_Ball:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
      case SpellEnum.Tentacle_Smash:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorTentacleSmash(theSpell, c, pos, rot_z, extended));
        break;
      case SpellEnum.Butterfly_Jar:
        ZSpell.FireBlit(theSpell, c, target);
        break;
      case SpellEnum.Death_and_Decay:
        ZSpell.FireDeathAndDecay(theSpell, c);
        break;
      case SpellEnum.Spirit_Walk:
        if (c.type == CreatureType.Wisp)
          ZSpell.ChangeSprites(c, ClientResources.Instance.wispPhantomSprites);
        ZSpell.FireApparition(c, true, true);
        break;
      case SpellEnum.Retribution:
        c.retribution = 50;
        ZSpell.FireEffector(theSpell, c, pos, rot_z, power, true);
        break;
      case SpellEnum.Pack_Mentality:
      case SpellEnum.Pack_Leader:
        ZSpell.FireSummon(theSpell, c.game, c, target);
        break;
      case SpellEnum.Storm_Jolt:
        c.game.ongoing.RunSpell(ZSpell.IEnumeratorThunderJolt(theSpell, c));
        if (ZComponent.IsNull((ZComponent) c))
          break;
        ZEffector.RechargeElectrostaticCharges(c.game);
        break;
      case SpellEnum.Summon_Titan:
        int index7 = (int) rot_z;
        if (c.parent.minionBookTitans.Count <= 0 || index7 >= c.parent.minionBookTitans.Count || index7 < 0 || c.parent.minionBookTitans[index7].used)
          break;
        ZGame.MinionBookTitan minionBookTitan = c.parent.minionBookTitans[index7];
        c.parent.minionBookTitans[index7].used = true;
        Spell spell2 = Inert.GetSpell(minionBookTitan.spell);
        MyLocation target2 = target;
        if (!((UnityEngine.Object) spell2 != (UnityEngine.Object) null))
          break;
        ZCreature sum2 = ZSpell.FireSummon(spell2, c.game, c, target2, 1, true);
        if (!extended)
          break;
        ZSpell.TryMountOnSummon(sum2, c);
        break;
      case SpellEnum.Stalk:
        if (c.game.isClient && !c.game.resyncing && (UnityEngine.Object) c.transform != (UnityEngine.Object) null)
          ZComponent.Instantiate<GameObject>(theSpell.explosion, c.transform.position, Quaternion.identity);
        c.MoveToPosition = new MyLocation(c.position.x, (FixedInt) -1000);
        c.inWater = true;
        ZEffector zeffector2 = new ZEffector()
        {
          whoSummoned = c,
          position = target,
          MaxTurnsAlive = 2,
          active = false,
          type = EffectorType.Stalk,
          game = c.game
        };
        c.effectors.Add(zeffector2);
        c.controllable = false;
        if (!((UnityEngine.Object) Player.Instance != (UnityEngine.Object) null) || !((ZComponent) Player.Instance.selected == (object) c))
          break;
        Player.Instance.NextControlled();
        break;
      case SpellEnum.Rising_Lava:
        ZSpell.FireRisingLava(theSpell, c.game);
        break;
      case SpellEnum.When_Pigs_Fly:
        ZSpell.WhenPigsFly(theSpell, c);
        break;
      case SpellEnum.Dense_Fog:
        ZSpell.FireDenseFog(theSpell, c.game);
        break;
      default:
        ZSpell.FireGeneric(theSpell, c, pos, rot_z, power);
        break;
    }
  }

  public void AfterExplosion()
  {
    if (this.spellEnum != SpellEnum.Death_Bomb)
      return;
    ZSpell.FireWhich(Inert.Instance.spells["Aura of Decay"], this.parent, this.position, (FixedInt) 0, (FixedInt) 0, this.position, NullMyLocation.Get());
  }

  public void Explode()
  {
    AudioManager.Play(this.explosionClip);
    switch (this.spellEnum)
    {
      case SpellEnum.Arcane_Arrow:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Arcane_Bomb:
        this.OnExplosionArcaneBomb();
        break;
      case SpellEnum.Fire_Ball:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Fire_Arrow:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Lava_Bomb:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Magma_Bomb:
        if (this.amount == 0)
        {
          this.OnExplosionGeneric();
          break;
        }
        this.OnExplosionMagmaBomb();
        break;
      case SpellEnum.Napalm_Bomb:
        this.OnExplosionNapalmBomb();
        break;
      case SpellEnum.Rain_of_Fire:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Rain_of_Arrows:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Scatter_Rock:
        this.OnExplosionScatterRock();
        break;
      case SpellEnum.Disruption:
        this.OnExplosionDisruption();
        break;
      case SpellEnum.Mud_Ball:
      case SpellEnum.Mudruption:
      case SpellEnum.Old_Mud_Ball:
        this.OnExplosionMudBall();
        break;
      case SpellEnum.Shock_Bomb:
      case SpellEnum.Rusty_Bomb:
        this.OnExplosion();
        break;
      case SpellEnum.Ice_Ball:
        this.game.ongoing.RunSpell(ZSpell.OnExplosionIceBall(this));
        break;
      case SpellEnum.Ice_Bomb:
        this.OnExplosionIceBomb();
        break;
      case SpellEnum.Frost_Shards:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Frost_Arrow:
        this.OnExplosionFrostArrow();
        break;
      case SpellEnum.Snowball:
        this.game.ongoing.RunSpell(ZSpell.OnExplosionSnowBall(this));
        break;
      case SpellEnum.Comet:
        this.OnExplosionComet();
        break;
      case SpellEnum.Rain_of_Chaos:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Drain_Bolt:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Death_Bomb:
        this.OnExplosionDeathBomb();
        break;
      case SpellEnum.Raise_Dead:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Shining_Bolt:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Sphere_of_Healing:
        this.OnExplosion();
        break;
      case SpellEnum.Thorn_Bomb:
        this.OnExplosionThornBomb();
        break;
      case SpellEnum.Vine_Bomb:
        this.OnExplosionVineBomb();
        break;
      case SpellEnum.Water_Ball:
        this.OnExplosionWaterBall();
        break;
      case SpellEnum.Hydration:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Brine_Bomb:
      case SpellEnum.Brine_Burst:
        this.OnExplosionBrineBomb();
        break;
      case SpellEnum.Rain_of_Clams:
        this.OnExplosionRainOfClams();
        break;
      case SpellEnum.Static_Ball:
        this.OnExplosionStaticBall();
        break;
      case SpellEnum.Mechanical_Arrow:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Cuckoo_Clock:
        this.OnExplosionCuckooClock();
        break;
      case SpellEnum.Blast_From_The_Past:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Clockwork_Bomb:
        this.OnExplosionCuckooClock();
        break;
      case SpellEnum.Sylph_Arrow:
        this.OnExplosionFrostArrow();
        break;
      case SpellEnum.Zombie_Breath:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.New_Years_Rocket:
      case SpellEnum.Pinecone:
        if ((UnityEngine.Object) this.toSummon != (UnityEngine.Object) null)
        {
          this.OnExplosionRocket();
          break;
        }
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Presents:
        this.OnExplosionPresents();
        break;
      case SpellEnum.Thanksgiving_Dinner:
        this.OnExplosion();
        break;
      case SpellEnum.Jingle_Boom:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Firework_Show:
        this.OnExplosionRocket();
        break;
      case SpellEnum.Air_Surge:
        this.OnExplosion();
        break;
      case SpellEnum.Color_Spray:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Whisper_Arrow:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Whisper_Bomb:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Meteor_Shower:
        this.OnExplosionNapalmBomb();
        break;
      case SpellEnum.Flesh_Wound:
        this.OnBloodExplosion();
        break;
      case SpellEnum.Blood_Bath:
        if (this.amount > 0)
        {
          this.OnExplosionMagmaBomb();
          break;
        }
        this.OnBloodExplosion();
        break;
      case SpellEnum.Blood_Craze:
        this.OnBloodExplosion();
        break;
      case SpellEnum.Summon_Vampire:
        this.OnBloodExplosion();
        this.position = this.position + this.velocity.normalized * 20;
        this.RandomBlood();
        break;
      case SpellEnum.Infection:
        ZSpell.FireInfection(this.baseSpell, this.parent, this.position, (FixedInt) 0, (FixedInt) 0, false, this.position, this.baseSpell.EXORADIUS);
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Water_Drop:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Arcane_Meteor:
        this.OnExplosionComet();
        if (ZComponent.IsNull((ZComponent) this.parent))
          break;
        ZEffector.CheckSanctuary(this.game, this.position, force: true);
        ZSpell.FireWhich(Inert.Instance.spells["Arcane Flash"], this.parent, this.position, (FixedInt) 0, (FixedInt) 0, this.position, NullMyLocation.Get());
        this.game.RandomInt(200, this.map.Height - 200);
        break;
      case SpellEnum.Arcane_Meteor_Shard:
        this.OnExplosionGeneric();
        this.target = this.position;
        ZSpell.FireArcaneSigil(Inert.Instance.spells["Arcane Meteor"].toSummon.GetComponent<Spell>(), this.parent, this.position, (FixedInt) 0, (FixedInt) 0, this.target, 1);
        break;
      case SpellEnum.Acorn:
        this.OnExplosionAcorn();
        break;
      case SpellEnum.Snowbolt:
        this.game.ongoing.RunSpell(ZSpell.OnExplosionSnowbolt(this));
        break;
      case SpellEnum.Rock_Blaster:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Enchanted_Axes:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Faiere_Jump:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Star_Bolt:
      case SpellEnum.Gravity_Pulse:
      case SpellEnum.Gravity_Well:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Dark_Matter_Bomb:
        this.OnExplosionGeneric();
        if (this.amount == 666 || !((ZComponent) this.parent != (object) null))
          break;
        ZEffector zeffector = ZEffector.Create(this.game, this.GetToSummon.GetComponent<Effector>(), (Vector3) this.target.ToSinglePrecision(), Quaternion.identity, this.game.GetMapTransform());
        zeffector.game = this.game;
        zeffector.whoSummoned = this.parent;
        zeffector.position = this.position;
        zeffector.MaxTurnsAlive = 2;
        zeffector.active = false;
        this.parent.effectors.Add(zeffector);
        break;
      case SpellEnum.Starfire:
        this.OnExplosionStarBolt();
        break;
      case SpellEnum.Starfire_Shard:
      case SpellEnum.Star_Dust:
      case SpellEnum.Star_Ball:
        this.OnExplosionGeneric();
        break;
      case SpellEnum.Drone_Strike:
        this.OnExplosion();
        break;
    }
  }

  public void OnBloodExplosion()
  {
    this.RandomBlood();
    this.OnExplosionGeneric();
  }

  public void RandomBlood()
  {
    int index = this.game.RandomInt(0, 5);
    if (!this.game.isClient || !Client.showBlood)
      return;
    this.map.BitBltBlood(ClientResources.Instance.bloodTextures[index], (int) this.position.x, (int) this.position.y);
  }

  public static void RandomBlood(ZGame game, MyLocation position)
  {
    int index = game.RandomInt(0, 5);
    if (!game.isClient || !Client.showBlood)
      return;
    game.map.BitBltBlood(ClientResources.Instance.bloodTextures[index], (int) position.x, (int) position.y);
  }

  public static ExplosionCutout NextCutout(ExplosionCutout s)
  {
    switch (s)
    {
      case ExplosionCutout.Ninty:
        return ExplosionCutout.One_O_Eight;
      case ExplosionCutout.Seventy_Five:
        return ExplosionCutout.Ninty;
      case ExplosionCutout.One_Twenty_Eight:
        return ExplosionCutout.One_Forty_Four;
      case ExplosionCutout.One_Fifteen:
        return ExplosionCutout.One_Twenty_Eight;
      case ExplosionCutout.Two_Hundred:
        return ExplosionCutout.Two_Twenty_One;
      case ExplosionCutout.One_O_Eight:
        return ExplosionCutout.One_Twenty_Eight;
      case ExplosionCutout.One_Forty_Four:
        return ExplosionCutout.One_Sixty_Two;
      case ExplosionCutout.One_Sixty_Two:
        return ExplosionCutout.One_Eighty;
      case ExplosionCutout.One_Eighty:
        return ExplosionCutout.Two_Hundred;
      default:
        Debug.Log((object) ("Unknown Cutout: " + (object) s + " Full Fire"));
        return s;
    }
  }

  public static int CutoutRadius(ExplosionCutout s)
  {
    switch (s)
    {
      case ExplosionCutout.Ninty:
        return 50;
      case ExplosionCutout.One_Twenty_Eight:
        return 69;
      case ExplosionCutout.One_Fifteen:
        return 62;
      case ExplosionCutout.Two_Hundred:
        return 105;
      case ExplosionCutout.One_O_Eight:
        return 59;
      case ExplosionCutout.One_Forty_Four:
        return 77;
      case ExplosionCutout.One_Sixty_Two:
        return 86;
      case ExplosionCutout.One_Eighty:
        return 95;
      default:
        Debug.Log((object) ("Unknown Cutout: " + (object) s + " Full Fire"));
        return 30;
    }
  }

  public static int ClampedFlameLevel(ZCreature c) => c.familiarLevelFlame;

  public static void UpgradeFullFire(ZCreature c, ZSpell s)
  {
    if (!((ZComponent) c != (object) null) || !c.familiar.Has(FamiliarType.Flame) || c.familiarLevelFlame <= 0)
      return;
    int num = ZSpell.ClampedFlameLevel(c);
    FixedInt fixedInt = (FixedInt) 1 + (FixedInt) num / FixedInt.Create(10);
    s.damage = (int) ((FixedInt) s.damage * fixedInt);
    if (s.spellEnum == SpellEnum.Flame_Wall)
      return;
    for (int index = 0; index < num; ++index)
      s.explosionCutout = ZSpell.NextCutout(s.explosionCutout);
    s.EXORADIUS = (int) ((FixedInt) ExplosionSize.GetRadius(s.explosionCutout) * (FixedInt) 1386217L);
  }

  public static void UpgradeFullFireShrapnel(ZCreature c, ZSpell s)
  {
    if (!((ZComponent) c != (object) null) || !c.familiar.Has(FamiliarType.Flame) || c.familiarLevelFlame <= 0)
      return;
    int num = ZSpell.ClampedFlameLevel(c);
    FixedInt fixedInt = (FixedInt) 1 + (FixedInt) num / FixedInt.Create(10);
    s.damage = (int) ((FixedInt) s.damage * fixedInt);
    for (int index = 0; index < num - 1; ++index)
      s.explosionCutout = ZSpell.NextCutout(s.explosionCutout);
    s.EXORADIUS = (int) ((FixedInt) ExplosionSize.GetRadius(s.explosionCutout) * (FixedInt) 1386217L);
  }

  public static bool IsMinionSpell(SpellEnum e) => ZSpell.MinionSpells.Contains(e);

  public bool IsMinionSpell() => ZSpell.MinionSpells.Contains(this.spellEnum);
}
