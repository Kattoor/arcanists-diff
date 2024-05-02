// Decompiled with JetBrains decompiler
// Type: Educative.SpellStats
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace Educative
{
  [Serializable]
  public class SpellStats
  {
    [Header("Size")]
    public int radius = 5;
    public int maxDistance = 10;
    [Header("Spell")]
    public SpellLogic spellLogic;
    public SpellType spellType;
    public TargetType targetType;
    [Range(1f, 4f)]
    public int level = 1;
    [Header("Speed")]
    public EditorFixedInt speedMin = (EditorFixedInt) 8;
    public EditorFixedInt speedMax = (EditorFixedInt) 40;
    public EditorFixedInt speedFlying;
    public EditorFixedInt speedRotation = (EditorFixedInt) 5.5f;
    public EditorFixedInt elasticity = (EditorFixedInt) 0.8f;
    public int maxDuration = 900;
    [Header("Explosion")]
    public int damage;
    public DamageType damageType;
    public int EXORADIUS = 257;
    public EditorFixedInt explisiveForce = (EditorFixedInt) 1;
    public ExplosionCutout explosionCutout;
    public Curve damageOverDistance;
    public Curve forceOverDistance;
    public bool affectedByGravity = true;
    public bool explodeOnImpact;
    [Header("Path")]
    public int frameOfPhantom;
    public bool goToTarget;
    public int maxTargetFrames = 60;
    public bool Rotates;
    [Header("Uses")]
    public bool EndsTurn = true;
    public int MaxUses = -1;
    public int RechargeTime;
    public int TurnsTillFirstUse;
    public bool halfFirstTurn;
    [Header("Type")]
    public CastType type;
    public BookOf bookOf;
    public int amount = 1;
    public int MaxMinionCap;
    public int Bounces;
    public EditorFixedInt CustomGravity = (EditorFixedInt) -838860L;
    public bool hitPhantom;
    public bool destroyTerrainFirstBounce;
    public int destroyTerrainBounces = 3;
    public string description = "";
  }
}
