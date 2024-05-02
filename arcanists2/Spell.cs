

using Educative;
using System;
using UnityEngine;

#nullable disable
public class Spell : Entity, ISpellBridge
{
  public SpellEnum spellEnum;
  public SpellStats stats;
  [NonSerialized]
  internal SpellStats runTimeStats;
  internal ZSpell serverObj;
  public AudioClip castClip;
  public AudioClip explosionClip;
  public GameObject explosion;
  public GameObject toSummon;
  public Texture2D snowTexture;
  public ExplosionCutout snowCutout;
  public MyCollider collider;
  public MyCollider colliderB;
  public Effector effector;
  internal int spellIndex;
  private Renderer _renderer;

  public int radius => this.runTimeStats.radius;

  public int maxDistance => this.runTimeStats.maxDistance;

  public bool affectedByGravity => this.runTimeStats.affectedByGravity;

  public Renderer Renderer => this._renderer;

  public void FindRenderer()
  {
    if (!((UnityEngine.Object) this._renderer == (UnityEngine.Object) null))
      return;
    this._renderer = this.GetComponent<Renderer>();
  }

  public SpellLogic spellLogic => this.runTimeStats.spellLogic;

  public SpellType spellType => this.runTimeStats.spellType;

  public TargetType targetType => this.runTimeStats.targetType;

  public int level => this.runTimeStats.level;

  public EditorFixedInt speedMin => this.runTimeStats.speedMin;

  public EditorFixedInt speedMax => this.runTimeStats.speedMax;

  public EditorFixedInt speedFlying => this.runTimeStats.speedFlying;

  public EditorFixedInt speedRotation => this.runTimeStats.speedRotation;

  public int maxDuration => this.runTimeStats.maxDuration;

  public EditorFixedInt elasticity => this.runTimeStats.elasticity;

  public int damage => this.runTimeStats.damage;

  public DamageType damageType => this.runTimeStats.damageType;

  public int EXORADIUS => this.runTimeStats.EXORADIUS;

  public EditorFixedInt explisiveForce => this.runTimeStats.explisiveForce;

  public ExplosionCutout explosionCutout => this.runTimeStats.explosionCutout;

  public Curve damageOverDistance => this.runTimeStats.damageOverDistance;

  public Curve forceOverDistance => this.runTimeStats.forceOverDistance;

  public bool explodeOnImpact => this.runTimeStats.explodeOnImpact;

  public int frameOfPhantom => this.runTimeStats.frameOfPhantom;

  public bool goToTarget => this.runTimeStats.goToTarget;

  public int maxTargetFrames => this.runTimeStats.maxTargetFrames;

  public bool Rotates => this.runTimeStats.Rotates;

  public bool EndsTurn => this.runTimeStats.EndsTurn;

  public int MaxUses => this.runTimeStats.MaxUses;

  public int RechargeTime => this.runTimeStats.RechargeTime;

  public int TurnsTillFirstUse => this.runTimeStats.TurnsTillFirstUse;

  public bool halfFirstTurn => this.runTimeStats.halfFirstTurn;

  public CastType type => this.runTimeStats.type;

  public BookOf bookOf => this.runTimeStats.bookOf;

  public int amount => this.runTimeStats.amount;

  public int MaxMinionCount => this.runTimeStats.MaxMinionCap;

  public int Bounces => this.runTimeStats.Bounces;

  public EditorFixedInt CustomGravity => this.runTimeStats.CustomGravity;

  public bool hitPhantom => this.runTimeStats.hitPhantom;

  public bool destroyTerrainFirstBounce => this.runTimeStats.destroyTerrainFirstBounce;

  public int destroyTerrainBounces => this.runTimeStats.destroyTerrainBounces;

  public static bool IsMinionSpell(SpellEnum e) => ZSpell.MinionSpells.Contains(e);

  public bool IsMinionSpell() => ZSpell.MinionSpells.Contains(this.spellEnum);

  public SpellType GetSpellType => this.spellType;

  public string GetName => this.name;

  public SpellEnum GetSpellEnum => this.spellEnum;

  public BookOf book => this.bookOf;

  public bool FromArmageddon => false;

  public ZCreature GetParent => (ZCreature) null;

  public bool GetGoToTarget => this.goToTarget;

  public GameObject GetToSummon => this.toSummon;

  public Spell GetBaseSpell => this;

  public ExplosionCutout GetExplosionCutout => this.explosionCutout;
}
