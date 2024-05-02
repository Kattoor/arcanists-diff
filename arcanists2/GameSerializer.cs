

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class GameSerializer
{
  public static void Serialize(ZGame game, ZPerson x, myBinaryWriter writer)
  {
    writer.Write(x.team);
    writer.Write(x.id);
    writer.Write(x.localTurn);
    writer.Write(x.yourTurn);
    writer.Write(x.ready);
    writer.Write(x.sendResync);
    writer.Write(x.offeringDraw);
    writer.Write(x.offeringRematch);
    writer.Write(x.winner);
    writer.Write(x.gainedWands);
    writer.Write(x.wasFirst);
    writer.Write(x.movedThisTurn);
    writer.Write(x.communeWithNature);
    writer.Write(x.countdown);
    writer.Write(x.lastArmageddon);
    writer.Write(x.name);
    x.settingsPlayer.Serialize(writer);
    x.account.Serialize(writer);
    writer.WriteNoAlpha((Color32) x.clientColor);
    writer.Write(x.timesOutfitChanged);
    writer.Write((int) x.familiarBook);
    writer.Write((int) x.ActivateableFamiliar);
    writer.Write(x.drainable);
    writer.Write(x.waterMultipler);
    writer.Write(x.hasCastTheFourSeasons);
    writer.Write(x.seasonISHoliday);
    writer.Write(x.FullArcane);
    writer.Write(x.didResign);
    writer.Write(x.didLeave);
    writer.Write(x.MinionMaster);
    writer.Write(x.BombMaster);
    writer.Write(JsonConvert.SerializeObject((object) x.spellsCast));
    for (int index = 0; index < x.familiarLevels.Length; ++index)
      writer.Write(x.familiarLevels[index]);
  }

  public static void PostSerialize(this ZGame game, ZPerson x, myBinaryWriter writer)
  {
    game.SerializeList(writer, x.stolenMinions);
    game.SerializeList(writer, x.takenMinions);
    writer.Write(ZGame.GetID(x.bloodBank));
    if (x.familiars == null)
    {
      writer.Write(0);
    }
    else
    {
      writer.Write(x.familiars.Count);
      for (int index = 0; index < x.familiars.Count; ++index)
      {
        writer.Write((ZComponent) x.familiars[index] != (object) null);
        if ((ZComponent) x.familiars[index] != (object) null)
          x.familiars[index].Serialize(writer);
      }
    }
  }

  public static void PostDeserialize(this ZGame game, ZPerson x, myBinaryReader reader)
  {
    game.DeserializeList(reader, x.stolenMinions);
    game.DeserializeList(reader, x.takenMinions);
    x.bloodBank = game.helper.GetCreature(reader.ReadInt32());
    int num = reader.ReadInt32();
    for (int index = 0; index < num; ++index)
    {
      if (reader.ReadBoolean())
        ZFamiliar.Deserialize(reader, x.first());
    }
    if (!((ZComponent) x.first() != (object) null))
      return;
    for (int book = 0; book < x.familiarLevels.Length; ++book)
    {
      if (x.familiarLevels[book] > 0 && book != 5 && (ZComponent) x.GetFamiliar((BookOf) book) == (object) null && (book != 10 || !x.seasonISHoliday))
      {
        Familiar component = Inert.Instance.familiars[book].GetComponent<Familiar>();
        ZFamiliar zfamiliar = ZFamiliar.Create(x.first(), component);
        if (x.familiars == null)
          x.familiars = new List<ZFamiliar>();
        x.familiars.Add(zfamiliar);
        zfamiliar.clientObj.transform.position = (Vector3) x.first().position.ToSinglePrecision() + zfamiliar.clientObj.GetOffset();
      }
    }
    if (x.MinionMaster)
      ZFamiliar.CreateMinionMaster(x);
    if (!x.BombMaster)
      return;
    ZFamiliar.CreateBombMaster(x);
  }

  public static void Deserialize(this ZGame game, ZPerson x, myBinaryReader reader)
  {
    x.team = reader.ReadInt32();
    x.id = reader.ReadByte();
    x.localTurn = reader.ReadInt32();
    x.yourTurn = reader.ReadBoolean();
    x.ready = reader.ReadBoolean();
    x.sendResync = reader.ReadBoolean();
    x.offeringDraw = reader.ReadBoolean();
    x.offeringRematch = reader.ReadBoolean();
    x.winner = reader.ReadBoolean();
    x.gainedWands = reader.ReadInt32();
    x.wasFirst = reader.ReadBoolean();
    x.movedThisTurn = reader.ReadBoolean();
    x.communeWithNature = reader.ReadInt32();
    x.countdown = reader.ReadSingle();
    x.lastArmageddon = reader.ReadInt32();
    x.name = reader.ReadString();
    x.settingsPlayer = new SettingsPlayer();
    x.settingsPlayer.Deserialize(reader);
    x.account.Deserialize(reader);
    x.clientColor = (Color) reader.ReadColor32NoAlpha();
    x.timesOutfitChanged = reader.ReadInt32();
    x.familiarBook = (FamiliarType) reader.ReadInt32();
    x.ActivateableFamiliar = (BookOf) reader.ReadInt32();
    x.drainable = reader.ReadBoolean();
    x.waterMultipler = reader.ReadInt32();
    x.hasCastTheFourSeasons = reader.ReadBoolean();
    x.seasonISHoliday = reader.ReadBoolean();
    x.FullArcane = reader.ReadBoolean();
    x.didResign = reader.ReadBoolean();
    x.didLeave = reader.ReadBoolean();
    x.MinionMaster = reader.ReadBoolean();
    x.BombMaster = reader.ReadBoolean();
    x.spellsCast = JsonConvert.DeserializeObject<Dictionary<SpellEnum, SpellsCast>>(reader.ReadString());
    if (x.FullArcane)
      x.clientColor = ClientResources.Instance.ModColors[0];
    for (int index = 0; index < x.familiarLevels.Length; ++index)
      x.familiarLevels[index] = reader.ReadInt32();
  }

  public static void Serialize(ZSpell x, myBinaryWriter writer, bool fromEffector)
  {
    bool flag = x.game.xSpell.Contains(x);
    writer.Write(flag);
    writer.Write(x.id);
    if (flag)
      return;
    x.game.xSpell.Add(x);
    writer.Write(x.baseSpell.name);
    writer.Write(x.position);
    writer.Write(x.maxDuration);
    writer.Write(x.damage);
    writer.Write((int) x.damageType);
    writer.Write(x.EXORADIUS);
    writer.Write(x.explisiveForce.RawValue);
    writer.Write((int) x.explosionCutout);
    writer.Write(x.explodeOnImpact);
    writer.Write(ZGame.GetID(x.extraCheck));
    writer.Write(x.goToTarget);
    writer.Write(x.maxTargetFrames);
    writer.Write(x.target);
    writer.Write(x.amount);
    writer.Write(x.curDuration);
    writer.Write(ZGame.GetID(x.parent));
    writer.Write(x.curSpeed.RawValue);
    writer.Write(x.fromArmageddon);
    writer.Write(x.velocity);
    writer.Write(x.Bounces);
    writer.Write(x.addedDeathVector);
    writer.Write(x.timesBounced);
    writer.Write(ZGame.GetID(x.hitCreature));
    writer.Write(x.lastLivingHit);
    writer.Write(x.lastHit);
    writer.Write(x.curDestroyTerrainBounces);
    writer.Write(x.moving != null);
    writer.Write((ZComponent) x.colliderB != (object) null);
    writer.Write((ZComponent) x.collider != (object) null);
    writer.Write((ZComponent) x.effector != (object) null && !fromEffector);
    writer.Write((ZComponent) x.effector2 != (object) null);
    writer.Write(ZGame.GetID(x.effector));
    x.colliderB?.Serialize(x.game, writer);
    x.collider?.Serialize(x.game, writer);
    if (!fromEffector)
      x.effector?.Serialize(writer);
    x.effector2?.Serialize(writer);
  }

  public static ZSpell DeserializeSpell(ZGame game, myBinaryReader reader)
  {
    int num1 = reader.ReadBoolean() ? 1 : 0;
    int num2 = reader.ReadInt32();
    if (num1 != 0)
      return game.helper.Getspell(num2);
    Spell spell = Inert.GetSpell(reader.ReadString());
    MyLocation myLocation = reader.ReadMyLocation();
    ZSpell z = !((Object) spell != (Object) null) ? new ZSpell() : (!(typeof (FlameWallSpell) == ((object) spell).GetType()) ? ZSpell.Create(game, spell, (Vector3) myLocation.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform()) : (ZSpell) ZSpell.Create(game, (FlameWallSpell) spell, (Vector3) myLocation.ToSinglePrecision(), Quaternion.identity, game.GetMapTransform()));
    game.helper.spellID.Add(num2, z);
    z.id = num2;
    z.position = myLocation;
    z.maxDuration = reader.ReadInt32();
    z.damage = reader.ReadInt32();
    z.damageType = (DamageType) reader.ReadInt32();
    z.EXORADIUS = reader.ReadInt32();
    z.explisiveForce = (FixedInt) reader.ReadInt64();
    z.explosionCutout = (ExplosionCutout) reader.ReadInt32();
    z.explodeOnImpact = reader.ReadBoolean();
    game.helper.spell_extraCheck.Add(new ZGame.IDSpell(z, reader.ReadInt32()));
    z.goToTarget = reader.ReadBoolean();
    z.maxTargetFrames = reader.ReadInt32();
    z.target = reader.ReadMyLocation();
    z.amount = reader.ReadInt32();
    z.curDuration = reader.ReadInt32();
    int num3 = reader.ReadInt32();
    game.helper.spell_parent.Add(new ZGame.IDSpell(z, num3));
    z.curSpeed = (FixedInt) reader.ReadInt64();
    z.fromArmageddon = reader.ReadBoolean();
    z.velocity = reader.ReadMyLocation();
    z.Bounces = reader.ReadInt32();
    z.addedDeathVector = reader.ReadMyLocation();
    z.timesBounced = reader.ReadInt32();
    game.helper.spell_hit.Add(new ZGame.IDSpell(z, reader.ReadInt32()));
    z.lastLivingHit = reader.ReadInt32();
    z.lastHit = reader.ReadInt32();
    z.curDestroyTerrainBounces = reader.ReadInt32();
    int num4 = reader.ReadBoolean() ? 1 : 0;
    int num5 = reader.ReadBoolean() ? 1 : 0;
    bool flag1 = reader.ReadBoolean();
    bool flag2 = reader.ReadBoolean();
    bool flag3 = reader.ReadBoolean();
    game.helper.spell_effector.Add(new ZGame.IDSpell(z, reader.ReadInt32()));
    if (num5 != 0)
      ZMyCollider.Deserialize(ref z.colliderB, game, reader);
    if (flag1)
      ZMyCollider.Deserialize(ref z.collider, game, reader);
    if (flag2)
      z.effector = ZEffector.Deserialze(game, game.helper.GetCreature(num3), reader, z.effector);
    if (flag3)
      z.effector2 = ZEffector.Deserialze(game, game.helper.GetCreature(num3), reader, z.effector2);
    if (num4 != 0)
    {
      IEnumerator<float> enumerator = z.SpellMove(true);
      enumerator.MoveNext();
      z.moving = enumerator;
    }
    return z;
  }

  public static void SerializePerson(
    ZGame game,
    ZPerson p,
    myBinaryWriter writer,
    bool includePlayerSpells = true)
  {
    GameSerializer.Serialize(game, p, writer);
    writer.Write(p.minionBookTitans.Count);
    foreach (ZGame.MinionBookTitan minionBookTitan in p.minionBookTitans)
    {
      writer.Write((int) minionBookTitan.spell);
      writer.Write(minionBookTitan.used);
    }
    writer.Write(p.controlled.Count);
    foreach (ZCreature c in p.controlled)
    {
      if ((ZComponent) c == (object) null)
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        ZCreatureCreate.Serialize(c, writer, includePlayerSpells);
        if (c.type == CreatureType.Beehive)
        {
          ZCreatureBeehive zcreatureBeehive = (ZCreatureBeehive) c;
          writer.Write(zcreatureBeehive.bees.Count);
          for (int index = 0; index < zcreatureBeehive.bees.Count; ++index)
          {
            if ((ZComponent) zcreatureBeehive.bees[index] == (object) null)
            {
              writer.Write(false);
            }
            else
            {
              writer.Write(true);
              ZCreatureCreate.Serialize(zcreatureBeehive.bees[index], writer);
            }
          }
        }
        writer.Write((ZComponent) c.tower != (object) null);
        c.tower?.Serialzie(writer);
        writer.Write(c.minerMarket != null);
        c.minerMarket?.Serialize(writer);
        writer.Write(c.effectors.Count);
        for (int index = 0; index < c.effectors.Count; ++index)
        {
          if ((ZComponent) c.effectors[index] == (object) null)
          {
            writer.Write(false);
          }
          else
          {
            writer.Write(true);
            c.effectors[index].Serialize(writer);
          }
        }
        writer.Write(c.destroyableEffectors.Count);
        for (int index = 0; index < c.destroyableEffectors.Count; ++index)
        {
          if ((ZComponent) c.destroyableEffectors[index] == (object) null)
          {
            writer.Write(false);
          }
          else
          {
            writer.Write(true);
            c.destroyableEffectors[index].Serialize(writer);
          }
        }
        writer.Write(c.followingColliders.Count);
        for (int index = 0; index < c.followingColliders.Count; ++index)
          writer.Write(ZGame.GetID(c.followingColliders[index]));
      }
    }
  }

  public static ZPerson DeserializePerson(
    ZGame game,
    int i,
    myBinaryReader reader,
    bool addToPlayers)
  {
    ZPerson zperson = new ZPerson();
    zperson.game = game;
    game.Deserialize(zperson, reader);
    if (addToPlayers)
      game.players.Add(zperson);
    game.helper.playerID.Add((int) zperson.id, zperson);
    int num1 = reader.ReadInt32();
    for (int index = 0; index < num1; ++index)
      zperson.minionBookTitans.Add(new ZGame.MinionBookTitan()
      {
        spell = (SpellEnum) reader.ReadInt32(),
        used = reader.ReadBoolean()
      });
    int num2 = reader.ReadInt32();
    for (int index1 = 0; index1 < num2; ++index1)
    {
      if (reader.ReadBoolean())
      {
        ZCreature zcreature = ZCreatureCreate.Deserialize(zperson, reader, i);
        zcreature.parent = zperson;
        if (!zcreature.isPawn)
        {
          if (zcreature.FullArcane)
            HUD.ClientFullArcane(game, zperson, zcreature, i);
        }
        else
          ZSpell.ApplyEffectors(game, zcreature, zcreature.position, cre: zperson.first(), isDeserialization: true);
        if (zcreature.spellEnum != SpellEnum.Summon_Tutorial_Target)
          zperson.controlled.Add(zcreature);
        zcreature.SetScaleOnResync(zcreature.transformscale);
        if ((Object) HUD.instance != (Object) null && HUD.instance.hideOverheadIcons)
          zcreature.overheadCanvas?.gameObject.SetActive(false);
        if (zcreature.type == CreatureType.Fresh_Water_Troll)
          zcreature.ClientTransformWaterTroll();
        else if (zcreature.type == CreatureType.Hard_Water_Troll)
          zcreature.ClientTransformFreshWaterTroll();
        if (zcreature.race == CreatureRace.Undead && zcreature.baseCreature.race != CreatureRace.Undead)
          zcreature.ClientTurnUndead();
        else if (zcreature.race == CreatureRace.Bear)
          ZSpell.CreateWerewolfObj(Inert.GetSpell(SpellEnum.Bear_Form), zcreature);
        if (zcreature.flying && !zcreature.baseCreature.flying)
          ZSpell.ClientFireFlight(Inert.GetSpell(zcreature.flightSpell), zcreature);
        if (zcreature.hasDarkDefenses && zcreature.game.isClient && (Object) zcreature.transform != (Object) null)
          Object.Instantiate<GameObject>(Inert.GetSpell(SpellEnum.Dark_Defences).toSummon, zcreature.transform.position, Quaternion.identity, zcreature.transform).GetComponent<ParticleDarkDefenses>().c = zcreature.clientObj;
        if (zcreature.shield > 0)
          zcreature.CreateProtectionShield(true);
        if (zcreature.gravitionalPull)
          zcreature.CreateGravityObj(true);
        if (zcreature.entangled)
          zcreature.CreateEntangleObj(true);
        if (zcreature.stunned)
        {
          zcreature.ClientStunned();
          zcreature.OnStunned();
        }
        if (zcreature.type == CreatureType.Beehive)
        {
          ZCreatureBeehive cre = (ZCreatureBeehive) zcreature;
          int num3 = reader.ReadInt32();
          for (int index2 = 0; index2 < num3; ++index2)
          {
            if (reader.ReadBoolean())
            {
              ZCreature sum = ZCreatureCreate.Deserialize(zperson, reader, i);
              cre.bees.Add(sum);
              sum.parent = zperson;
              ZSpell.ApplyEffectors(game, sum, sum.position, cre: (ZCreature) cre);
            }
          }
        }
        if (reader.ReadBoolean())
          ZTower.Deserialize(reader, game, zcreature);
        if (reader.ReadBoolean())
          MinerMarket.Deserialize(reader, zcreature);
        if (zcreature.type == CreatureType.Gargoyle && !zcreature.flying && (Object) zcreature.gameObject != (Object) null)
          zcreature.gameObject.GetComponent<GargoyalObj>()?.SetColor(zcreature.flying, zcreature.flying ? 0.0f : -1f);
        if (zcreature.type == CreatureType.Wisp && zcreature.phantom && (Object) zcreature.gameObject != (Object) null)
          ZSpell.ChangeSprites(zcreature, ClientResources.Instance.wispPhantomSprites);
        zcreature.UpdateHealthTxt();
        int num4 = reader.ReadInt32();
        for (int index3 = 0; index3 < num4; ++index3)
        {
          if (reader.ReadBoolean())
          {
            ZEffector zeffector = ZEffector.Deserialze(game, zcreature, reader, (ZEffector) null);
            if ((ZComponent) zeffector != (object) null && !zeffector.doNotCreateObjectOnResync)
              zcreature.effectors.Add(zeffector);
          }
        }
        int num5 = reader.ReadInt32();
        for (int index4 = 0; index4 < num5; ++index4)
        {
          if (reader.ReadBoolean())
          {
            ZEffector zeffector = ZEffector.Deserialze(game, zcreature, reader, (ZEffector) null);
            if ((ZComponent) zeffector != (object) null && !zeffector.doNotCreateObjectOnResync)
              zcreature.destroyableEffectors.Add(zeffector);
          }
        }
        int num6 = reader.ReadInt32();
        for (int index5 = 0; index5 < num6; ++index5)
          game.helper.id_followColliders.Add(new ZGame.ID2(zcreature, reader.ReadInt32()));
      }
    }
    return zperson;
  }
}
