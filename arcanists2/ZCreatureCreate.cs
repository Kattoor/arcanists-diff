// Decompiled with JetBrains decompiler
// Type: ZCreatureCreate
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public static class ZCreatureCreate
{
  public static ZCreature CreateCreature(
    ZPerson parent,
    Creature c,
    Vector2 l,
    Quaternion q,
    Transform p,
    bool copy = true)
  {
    ZCreature z = c.Get();
    if (parent?.game != null)
    {
      z.id = ++parent.game.nextCreatureID;
      z.game = parent.game;
    }
    else
      z.game = Client.game;
    z.Copy(c);
    z.daOriginalTrueParent = parent;
    z.ClientSetup(c, l, q, p);
    return z;
  }

  public static ZCreature Deserialize(ZPerson parent, myBinaryReader reader, int index)
  {
    int num1 = reader.ReadBoolean() ? 1 : 0;
    int id = reader.ReadInt32();
    if (num1 != 0)
      return parent.game.helper.GetCreature(id);
    Creature creature = Inert.GetCreature(reader.ReadString());
    bool flag = reader.ReadBoolean();
    MyLocation pos = reader.ReadMyLocation();
    ZCreature z = !flag ? Inert.CreateCharacter(parent, parent.settingsPlayer, pos, index, noSpells: true) : (typeof (CreatureThorn) == ((object) creature).GetType() ? (ZCreature) ZCreatureCreate.CreateThorn(parent.game, (CreatureThorn) creature, pos.ToSinglePrecision(), Quaternion.identity, parent.game.GetMapTransform()) : ZCreatureCreate.CreateCreature(parent, creature, pos.ToSinglePrecision(), Quaternion.identity, parent.game.GetMapTransform(), false));
    z.id = id;
    z.position = pos;
    z.parent = parent;
    z.health = reader.ReadInt32();
    z.maxHealth = reader.ReadInt32();
    z.isPawn = reader.ReadBoolean();
    z.flying = reader.ReadBoolean();
    z.mountable = reader.ReadBoolean();
    z.canMount = reader.ReadBoolean();
    z.phantom = reader.ReadBoolean();
    z.canMove = reader.ReadBoolean();
    z.race = (CreatureRace) reader.ReadInt32();
    z.type = (CreatureType) reader.ReadInt32();
    z.radius = reader.ReadInt32();
    z.affectedByGravity = reader.ReadBoolean();
    z.massMulti = (FixedInt) reader.ReadInt64();
    z.TrueSize = reader.ReadInt32();
    z.controllable = reader.ReadBoolean();
    z.movementFromInput = reader.ReadBoolean();
    z.waterWalking = reader.ReadBoolean();
    z.frostWalking = reader.ReadBoolean();
    z.LongJumpData = reader.ReadMyLocation();
    z.HighJumpData = reader.ReadMyLocation();
    parent.game.helper.id_daOgParent.Add(new ZGame.ID2(z, reader.ReadInt32()));
    parent.game.helper.id_oldParents.Add(new ZGame.ID2(z, reader.ReadInt32()));
    z.spells.Clear();
    int num2 = reader.ReadInt32();
    for (int index1 = 0; index1 < num2; ++index1)
    {
      SpellSlot s = SpellSlot.Deserialize(parent, reader);
      z.spells.Add(s);
      if (!flag && s.spell.spellEnum == SpellEnum.Arcane_Gate || s.spell.spellEnum == SpellEnum.Santas_Magic)
        z.parent?.AddGate(s);
    }
    z.moneyBags = reader.ReadBoolean();
    z.audioBags = reader.ReadBoolean();
    parent.game.helper.id_pacts.Add(new ZGame.ID2(z, reader.ReadInt32()));
    parent.game.helper.id_mounts.Add(new ZGame.ID2(z, reader.ReadInt32()));
    z.scale = reader.ReadSingle();
    z.scaleFixed = (FixedInt) reader.ReadInt64();
    z.FullArcane = reader.ReadBoolean();
    z.spellEnum = (SpellEnum) reader.ReadInt32();
    z.gliding = reader.ReadBoolean();
    z.moneyFrames = reader.ReadInt32();
    z.removeFlight = reader.ReadBoolean();
    z.UnaffectedByNaturesWraith = reader.ReadBoolean();
    z.shield = reader.ReadInt32();
    z.invulnerable = reader.ReadInt32();
    z.socialDistancing = reader.ReadBoolean();
    z.stunned = reader.ReadBoolean();
    z.superStunned = reader.ReadBoolean();
    z.entangled = reader.ReadBoolean();
    z.hasDarkDefenses = reader.ReadBoolean();
    z.inWater = reader.ReadBoolean();
    z.bleedCounter = reader.ReadInt32();
    z.turnProtectionShieldCast = reader.ReadInt32();
    z.entangledShield = reader.ReadInt32();
    z.vine_bloom_active = reader.ReadBoolean();
    z.additionalBleedOutDamage = reader.ReadInt32();
    z.loopCount = reader.ReadInt32();
    z.glideIsActive = reader.ReadBoolean();
    z.climbingHooksIsActive = reader.ReadBoolean();
    z.tries = reader.ReadInt32();
    z.retribution = reader.ReadInt32();
    z.transformscale = (float) reader.ReadInt32();
    if (Inert._Version > 57)
    {
      z.appliedGravity = reader.ReadInt32();
      z.fusion = reader.ReadInt32();
      z.halfHealing = reader.ReadInt32();
      z.loopCount = reader.ReadInt32();
      z.glideIsActive = reader.ReadBoolean();
      z.climbingHooksIsActive = reader.ReadBoolean();
      z.tries = reader.ReadInt32();
      z.turnUndeadTurn = reader.ReadInt32();
      z.turnFriendlyDmg = reader.ReadInt32();
      parent.game.helper.id_effectorStaticShield.Add(new ZGame.ID2(z, reader.ReadInt32()));
      z.usingGlide = reader.ReadBoolean();
      z._FourSeasonsCastAtEndOfTurn = reader.ReadBoolean();
      z._FourSeasonsLocation = reader.ReadMyLocation();
    }
    z.diesInWater = reader.ReadBoolean();
    parent.game.helper.id_stormShield.Add(new ZGame.ID2(z, reader.ReadInt32()));
    parent.game.helper.id_Aura.Add(new ZGame.ID2(z, reader.ReadInt32()));
    z.shiningPower = reader.ReadBoolean();
    z.prestigeIndex = reader.ReadInt32();
    z.permenantFlight = reader.ReadBoolean();
    z.tempFlight = reader.ReadBoolean();
    z.flightSpell = (SpellEnum) reader.ReadInt32();
    z.team = reader.ReadInt32();
    if ((ZComponent) z.collider == (object) null)
      z.collider = new ZMyCollider();
    ZMyCollider.Deserialize(ref z.collider, z.game, reader);
    z.isPawn = flag;
    z.game.helper.creatureID.Add(z.id, z);
    return z;
  }

  public static void Serialize(ZCreature c, myBinaryWriter writer)
  {
    bool flag = c.game.xCreature.Contains(c);
    writer.Write(flag);
    writer.Write(c.id);
    if (flag)
      return;
    c.game.xCreature.Add(c);
    writer.Write(c.baseCreature.name);
    writer.Write(c.isPawn);
    writer.Write(c.position);
    writer.Write(c.health);
    writer.Write(c.maxHealth);
    writer.Write(c.isPawn);
    writer.Write(c.flying);
    writer.Write(c.mountable);
    writer.Write(c.canMount);
    writer.Write(c.phantom);
    writer.Write(c.canMove);
    writer.Write((int) c.race);
    writer.Write((int) c.type);
    writer.Write(c.radius);
    writer.Write(c.affectedByGravity);
    writer.Write(c.massMulti.RawValue);
    writer.Write(c.TrueSize);
    writer.Write(c.controllable);
    writer.Write(c.movementFromInput);
    writer.Write(c.waterWalking);
    writer.Write(c.frostWalking);
    writer.Write(c.LongJumpData);
    writer.Write(c.HighJumpData);
    writer.Write(ZGame.GetID(c.daOriginalTrueParent));
    writer.Write(ZGame.GetID(c.originalParent));
    writer.Write(c.spells.Count);
    for (int index = 0; index < c.spells.Count; ++index)
      c.spells[index].Serialize(c.game, writer);
    writer.Write(c.moneyBags);
    writer.Write(c.audioBags);
    writer.Write(ZGame.GetID(c.pact));
    writer.Write(ZGame.GetID(c.mount));
    writer.Write(c.scale);
    writer.Write(c.scaleFixed.RawValue);
    writer.Write(c.FullArcane);
    writer.Write((int) c.spellEnum);
    writer.Write(c.gliding);
    writer.Write(c.moneyFrames);
    writer.Write(c.removeFlight);
    writer.Write(c.UnaffectedByNaturesWraith);
    writer.Write(c.shield);
    writer.Write(c.invulnerable);
    writer.Write(c.socialDistancing);
    writer.Write(c.stunned);
    writer.Write(c.superStunned);
    writer.Write(c.entangled);
    writer.Write(c.hasDarkDefenses);
    writer.Write(c.inWater);
    writer.Write(c.bleedCounter);
    writer.Write(c.turnProtectionShieldCast);
    writer.Write(c.entangledShield);
    writer.Write(c.vine_bloom_active);
    writer.Write(c.additionalBleedOutDamage);
    writer.Write(c.loopCount);
    writer.Write(c.glideIsActive);
    writer.Write(c.climbingHooksIsActive);
    writer.Write(c.tries);
    writer.Write(c.retribution);
    writer.Write(c.transformscale);
    if (Inert._Version > 57)
    {
      writer.Write(c.appliedGravity);
      writer.Write(c.fusion);
      writer.Write(c.halfHealing);
      writer.Write(c.loopCount);
      writer.Write(c.glideIsActive);
      writer.Write(c.climbingHooksIsActive);
      writer.Write(c.tries);
      writer.Write(c.turnUndeadTurn);
      writer.Write(c.turnFriendlyDmg);
      writer.Write(ZGame.GetID((ZEffector) c.effectorStaticShield));
      writer.Write(c.usingGlide);
      writer.Write(c._FourSeasonsCastAtEndOfTurn);
      writer.Write(c._FourSeasonsLocation);
    }
    writer.Write(c.diesInWater);
    writer.Write(ZGame.GetID(c.stormShield));
    writer.Write(ZGame.GetID(c.auraOfDecay));
    writer.Write(c.shiningPower);
    writer.Write(c.prestigeIndex);
    writer.Write(c.permenantFlight);
    writer.Write(c.tempFlight);
    writer.Write((int) c.flightSpell);
    writer.Write(c.team);
    c.collider.Serialize(c.game, writer);
  }

  public static ZCreature Clone(ZGame game, ZCreature old, Transform p, MyLocation position)
  {
    ZCreature zcreature = old.baseCreature.Get();
    zcreature.id = ++game.nextCreatureID;
    zcreature.game = game;
    zcreature.Clone(old);
    zcreature.ClientSetup(old.baseCreature, position.ToSinglePrecision(), Quaternion.identity, p);
    zcreature.gliding = false;
    zcreature.invulnerable = -1;
    zcreature.game = game;
    zcreature.collider.world = game.world;
    if (zcreature.spellEnum != SpellEnum.None)
      ZSpell.ApplyEffectors(game, zcreature, zcreature.position);
    zcreature.spells.Clear();
    foreach (SpellSlot spell in old.spells)
      zcreature.spells.Add(new SpellSlot(spell));
    if (zcreature.type != old.baseCreature.type)
    {
      if (zcreature.type == CreatureType.Fresh_Water_Troll)
        zcreature.ClientTransformWaterTroll();
      else if (zcreature.type == CreatureType.Hard_Water_Troll)
        zcreature.ClientTransformFreshWaterTroll();
    }
    if (zcreature.flying)
    {
      if (zcreature.shiningPower)
        zcreature.flightObj = Object.Instantiate<GameObject>(Inert.Instance.spells["Shining Power"].toSummon, zcreature.transform.position + ((double) zcreature.transformscale > 0.0 ? new Vector3(-19.1f, -2.3f) : new Vector3(19.1f, -2.3f)) * zcreature.scale, Quaternion.identity, zcreature.transform);
      else if (zcreature.phantom && !zcreature.baseCreature.phantom)
      {
        if (zcreature.game.isClient)
        {
          ColorLerp colorLerp = zcreature.gameObject.AddComponent<ColorLerp>();
          colorLerp.rends = ((IEnumerable<SpriteRenderer>) zcreature.gameObject.GetComponentsInChildren<SpriteRenderer>()).ToList<SpriteRenderer>();
          for (int index = colorLerp.rends.Count - 1; index >= 0; --index)
          {
            if (colorLerp.rends[index].gameObject.layer == 14)
              colorLerp.rends.RemoveAt(index);
          }
        }
      }
      else if (!zcreature.permenantFlight)
        zcreature.flightObj = Object.Instantiate<GameObject>(Inert.Instance.spells["Flight"].toSummon, zcreature.transform.position - new Vector3(0.0f, (float) (zcreature.radius - 2)), Quaternion.identity, zcreature.transform);
    }
    if (zcreature.stunned)
    {
      zcreature.ClientStunned();
      if (game.isClient && zcreature.superStunned && (Object) zcreature.transform != (Object) null)
        zcreature.transform.GetComponentInChildren<ParticleDucks>()?.SetRed();
    }
    if (zcreature.type == CreatureType.Gargoyle && !zcreature.flying && (Object) zcreature.gameObject != (Object) null)
      zcreature.gameObject.GetComponent<GargoyalObj>()?.SetColor(zcreature.flying, zcreature.flying ? 0.0f : -1f);
    if (zcreature.spellEnum == SpellEnum.None)
      Inert.CreateStuff(zcreature, old.daOriginalTrueParent, old.daOriginalTrueParent.settingsPlayer, false);
    if (zcreature.race != old.baseCreature.race && zcreature.race == CreatureRace.Undead && old.baseCreature.race != CreatureRace.Undead)
      zcreature.ClientTurnUndead();
    foreach (ZEffector effector in zcreature.effectors)
      effector?.VisualUpdate();
    if ((Object) zcreature.overheadCanvas != (Object) null)
      ((RectTransform) zcreature.overheadCanvas.transform).anchoredPosition = zcreature.ClientOverHeadOffset.ToSinglePrecision() * (1f / zcreature.scale);
    zcreature.SetScale(zcreature.scale);
    return zcreature;
  }

  public static ZCreatureThorn CreateThorn(
    ZGame game,
    CreatureThorn c,
    Vector2 l,
    Quaternion q,
    Transform p)
  {
    ZCreatureThorn z = new ZCreatureThorn();
    z.id = ++game.nextCreatureID;
    z.game = game;
    z.Copy((Creature) c);
    z.ClientSetup((Creature) c, l, q, p);
    z.spell = (ZSpell) new ZSpellThornBall();
    z.spell.game = game;
    z.spell.id = ++game.nextSpellID;
    z.spell.Copy(c.spell, game);
    z.spell.collider = z.collider;
    z.collider.spell = z.spell;
    z.spell.gameObject = z.gameObject;
    z.spell.transform = z.transform;
    return z;
  }

  private static void ClientSetup(
    this ZCreature z,
    Creature c,
    Vector2 l,
    Quaternion q,
    Transform p)
  {
    Creature creature = Object.Instantiate<Creature>(c, (Vector3) l, q, p);
    z.clientObj = creature;
    creature.runTimeStats = z.baseCreature.runTimeStats;
    z.gameObject = creature.gameObject;
    z.transform = creature.transform;
    creature.serverObj = z;
    if ((ZComponent) z.auraOfDecay != (object) null)
    {
      if ((Object) creature.auraOfDecay == (Object) null)
        creature.auraOfDecay = Object.Instantiate<Effector>(z.auraOfDecay.baseEffector, creature.transform.position, Quaternion.identity, creature.transform);
      z.auraOfDecay.gameObject = creature.auraOfDecay.gameObject;
      z.auraOfDecay.transform = creature.auraOfDecay.transform;
    }
    z.Awake();
    z.Start();
  }

  public static void Copy(this ZCreature z, Creature c)
  {
    z.baseCreature = c;
    z.health = c.health;
    z.maxHealth = c.maxHealth;
    z.isPawn = c.isPawn;
    z.flying = c.flying;
    z.massMulti = (FixedInt) c.massMulti;
    z.mountable = c.mountable;
    z.canMount = c.canMount;
    z.phantom = c.phantom;
    z.gliding = c.gliding;
    z.canMove = c.canMove;
    z.race = c.race;
    z.type = c.type;
    z.radius = c.radius;
    z.affectedByGravity = c.affectedByGravity;
    z.waterWalking = c.waterWalking;
    z.frostWalking = c.frostWalking;
    z.LongJumpData = c.LongJumpData.To();
    z.HighJumpData = c.HighJumpData.To();
    if ((Object) c.collider != (Object) null)
    {
      z.collider = new ZMyCollider();
      if (z.game != null)
        z.collider.id = ++z.game.nextColliderID;
      z.collider.Copy(c.collider);
      z.collider.radius = z.radius;
      z.collider.creature = z;
    }
    if ((Object) c.auraOfDecay != (Object) null)
    {
      z.auraOfDecay = new ZEffector();
      z.auraOfDecay.game = z.game;
      z.auraOfDecay.Copy(c.auraOfDecay, z.game);
      if (c.runTimeStats.effectorMaxTurnsOverride > 0)
        z.auraOfDecay.MaxTurnsAlive = c.runTimeStats.effectorMaxTurnsOverride;
    }
    for (int index = 0; index < c.spells.Count; ++index)
      z.spells.Add(new SpellSlot(c.spells[index]));
    z.undeadHead = c.undeadHead;
  }

  public static void Clone(this ZCreature z, ZCreature c, bool deepClone = false)
  {
    z.game = c.game;
    z.baseCreature = c.baseCreature;
    z.health = c.health;
    z.maxHealth = c.maxHealth;
    z.isPawn = c.isPawn;
    z.flying = c.flying;
    z.mountable = c.mountable;
    z.canMount = c.canMount;
    z.phantom = c.phantom;
    z.canMove = c.canMove;
    z.race = c.race;
    z.type = c.type;
    z.radius = c.radius;
    z.affectedByGravity = c.affectedByGravity;
    z.massMulti = c.massMulti;
    z.waterWalking = c.waterWalking;
    z.frostWalking = c.frostWalking;
    z.LongJumpData = c.LongJumpData;
    z.HighJumpData = c.HighJumpData;
    z.daOriginalTrueParent = c.daOriginalTrueParent;
    z.TrueSize = c.TrueSize;
    for (int index = 0; index < c.spells.Count; ++index)
      z.spells.Add(new SpellSlot(c.spells[index]));
    z.moneyBags = c.moneyBags;
    z.audioBags = c.audioBags;
    z.scale = c.scale;
    z.scaleFixed = c.scaleFixed;
    z.FullArcane = c.FullArcane;
    z.spellEnum = c.spellEnum;
    if (deepClone)
    {
      z.gliding = c.gliding;
      z.moneyFrames = c.moneyFrames;
      z.removeFlight = c.removeFlight;
      z.UnaffectedByNaturesWraith = c.UnaffectedByNaturesWraith;
      z.shield = c.shield;
      z.invulnerable = c.invulnerable;
      z.socialDistancing = c.socialDistancing;
      z.stunned = c.stunned;
      z.superStunned = c.superStunned;
      z.entangled = c.entangled;
      z.hasDarkDefenses = c.hasDarkDefenses;
      z.inWater = c.inWater;
      z.bleedCounter = c.bleedCounter;
      z.turnProtectionShieldCast = c.turnProtectionShieldCast;
      z.entangledShield = c.entangledShield;
      z.vine_bloom_active = c.vine_bloom_active;
      z.additionalBleedOutDamage = c.additionalBleedOutDamage;
      z.loopCount = c.loopCount;
      z.glideIsActive = c.glideIsActive;
      z.climbingHooksIsActive = c.climbingHooksIsActive;
      z.tries = c.tries;
      z.retribution = c.retribution;
    }
    z.diesInWater = c.diesInWater;
    z.shiningPower = c.shiningPower;
    z.prestigeIndex = c.prestigeIndex;
    z.permenantFlight = c.permenantFlight;
    z.tempFlight = c.tempFlight;
    z.team = c.team;
    z.parent = c.parent;
    z.isPawn = c.isPawn;
    z.collider = new ZMyCollider();
    z.collider.Clone(c.collider, z.game);
    z.collider.creature = z;
    foreach (ZMyCollider followingCollider in c.followingColliders)
    {
      if ((ZComponent) followingCollider.effector != (object) null && followingCollider.effector.MaxTurnsAlive > 60 && ((ZComponent) c.tower == (object) null || c.tower.type != TowerType.Darkness))
      {
        z.auraOfDecay = new ZEffector();
        z.auraOfDecay.Clone(c.game, followingCollider.effector, z);
      }
    }
    if ((ZComponent) z.auraOfDecay == (object) null && (Object) z.baseCreature.auraOfDecay != (Object) null)
    {
      z.auraOfDecay = new ZEffector();
      z.auraOfDecay.Copy(z.baseCreature.auraOfDecay, c.game);
    }
    z.undeadHead = c.undeadHead;
  }
}
