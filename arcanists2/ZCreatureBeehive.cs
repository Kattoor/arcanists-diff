// Decompiled with JetBrains decompiler
// Type: ZCreatureBeehive
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZCreatureBeehive : ZCreature
{
  public List<ZCreature> bees = new List<ZCreature>();

  public override void OnNextTurn()
  {
    for (int index1 = this.bees.Count - 1; index1 >= 0; --index1)
    {
      if (index1 < this.bees.Count)
      {
        if ((ZComponent) this.bees[index1] == (object) null || this.bees[index1].isDead || this.bees[index1].team != this.team)
        {
          this.bees.RemoveAt(index1);
        }
        else
        {
          ZCreature bee = this.bees[index1];
          if (bee.invulnerable > 0)
            --bee.invulnerable;
          if (bee.superStunned)
            bee.superStunned = false;
          else
            bee.stunned = false;
          for (int index2 = bee.effectors.Count - 1; index2 >= 0; --index2)
          {
            if (ZComponent.IsNull((ZComponent) bee.effectors[index2]) || bee.effectors[index2].dead)
              bee.effectors.RemoveAt(index2);
            else
              bee.effectors[index2].TurnPassed(index2, false, false);
          }
          for (int index3 = bee.destroyableEffectors.Count - 1; index3 >= 0; --index3)
          {
            if (ZComponent.IsNull((ZComponent) bee.destroyableEffectors[index3]))
              bee.destroyableEffectors.RemoveAt(index3);
            else
              bee.destroyableEffectors[index3].TurnPassed(index3, true, false);
          }
          if (bee.halfHealing > 0)
            --bee.halfHealing;
          if (bee.bleeding && bee.health > 1)
          {
            --bee.bleedCounter;
            bee.DoDamage(Mathf.Min(bee.health - 1, 10));
            ZSpell.RandomBlood(bee.game, bee.position);
          }
        }
      }
    }
  }

  public override void OnDeath(bool playDeathClip = true)
  {
    foreach (ZCreature bee in this.bees)
    {
      if ((ZComponent) bee != (object) null && !bee.isDead)
        bee.OnDeath(playDeathClip);
    }
    base.OnDeath(playDeathClip);
  }

  public void CreateBee()
  {
    MyLocation position = this.position;
    position.x += this.radius + Inert.Instance.bee.radius + 1;
    position.y -= 19;
    if (position.y < Inert.Instance.bee.radius)
      position.y = (FixedInt) Inert.Instance.bee.radius;
    if (position.x > this.game.map.Width - Inert.Instance.bee.radius)
      position.x = (FixedInt) (this.game.map.Width - Inert.Instance.bee.radius);
    if (this.bees.Count >= 3 || this.game.world.OverlapCircle(new Point((int) position.x, (int) position.y), Inert.Instance.bee.radius, this.collider, Inert.mask_movement_NoEffector))
      return;
    ZCreature creature = ZCreatureCreate.CreateCreature(this.parent, Inert.Instance.bee, position.ToSinglePrecision(), Quaternion.identity, this.game.GetMapTransform());
    this.bees.Add(creature);
    creature.spellEnum = SpellEnum.Summon_Bees;
    creature.game = this.game;
    creature.parent = this.parent;
    creature.collider.world = creature.world;
    creature.collider.creature = creature;
    creature.collider.Move(position);
    creature.position = position;
    ZEffector auraOfDecay = creature.auraOfDecay;
    auraOfDecay.game = this.game;
    auraOfDecay.whoSummoned = creature;
    auraOfDecay.followParent = true;
    auraOfDecay.collider.world = auraOfDecay.world;
    creature.followingColliders.Add(auraOfDecay.collider);
    auraOfDecay.collider.Move(creature.position);
    creature.auraOfDecay = auraOfDecay;
    creature.effectors.Add(auraOfDecay);
    auraOfDecay.active = false;
    auraOfDecay.TurnPassed(creature.effectors.Count - 1, false, false);
    if (!this.game.isClient)
      return;
    GameObject gameObject1 = ZComponent.Instantiate<GameObject>(ClientResources.Instance.bg_glow, position.ToSinglePrecision(), Quaternion.identity, creature.transform);
    GameObject gameObject2 = ZComponent.Instantiate<GameObject>(ClientResources.Instance.minimap_glow, position.ToSinglePrecision(), Quaternion.identity, creature.transform);
    creature.miniMapBg = gameObject2.GetComponent<SpriteRenderer>();
    creature.clientObj.bg = gameObject1.GetComponent<SpriteRenderer>();
    creature.clientObj.bg.color = this.parent.clientColor;
    if ((double) creature.bg.color.a < 1.0)
    {
      Color color = creature.bg.color with { a = 1f };
      creature.bg.color = color;
    }
    creature.miniMapBg.color = creature.bg.color;
    float num1 = creature.BGScale();
    gameObject1.transform.localScale = new Vector3(num1, num1, 1f);
    float num2 = creature.MiniBGScale();
    gameObject2.transform.localScale = new Vector3(num2, num2, 1f);
    ZComponent.Instantiate<GameObject>(this.deathExplosion, position.ToSinglePrecision(), Quaternion.identity);
  }

  public override void DoFlipFlop(float scaleX)
  {
    for (int index = this.bees.Count - 1; index >= 0; --index)
    {
      if (index < this.bees.Count)
      {
        ZCreature bee = this.bees[index];
        if ((ZComponent) bee == (object) null || bee.isDead)
          this.bees.RemoveAt(index);
        else
          bee.DoFlipFlop(scaleX);
      }
    }
    base.DoFlipFlop(scaleX);
  }

  public override void MoveLeft(int extraBits = 0)
  {
    for (int index = this.bees.Count - 1; index >= 0; --index)
    {
      if (index < this.bees.Count)
      {
        ZCreature bee = this.bees[index];
        if ((ZComponent) bee == (object) null || bee.isDead)
          this.bees.RemoveAt(index);
        else if (!bee.stunned)
        {
          if (bee.flying && !bee.entangledOrGravity)
          {
            bee.velocity = new MyLocation((FixedInt) ((double) bee.transformscale < 0.0 ? -11010048L : -6291456L), (FixedInt) 0);
            bee.SetScale(-1f);
            bee.moving = this.game.ongoing.RunCoroutine(bee.FlyMove(true));
          }
          else
            bee.MoveLeft();
        }
      }
    }
    this.SetScale(-1f);
  }

  public override void MoveRight(int extraBits = 0)
  {
    for (int index = this.bees.Count - 1; index >= 0; --index)
    {
      if (index < this.bees.Count)
      {
        ZCreature bee = this.bees[index];
        if ((ZComponent) bee == (object) null || bee.isDead)
          this.bees.RemoveAt(index);
        else if (!bee.stunned)
        {
          if (bee.flying && !bee.entangledOrGravity)
          {
            bee.velocity = new MyLocation((FixedInt) ((double) bee.transformscale > 0.0 ? 9437184L : 4194304L), (FixedInt) 0);
            bee.SetScale(1f);
            bee.moving = this.game.ongoing.RunCoroutine(bee.FlyMove(true));
          }
          else
            bee.MoveRight();
        }
      }
    }
    this.SetScale(1f);
  }

  public override void DoHighJump(int extraBits)
  {
    for (int index = this.bees.Count - 1; index >= 0; --index)
    {
      if (index < this.bees.Count)
      {
        ZCreature bee = this.bees[index];
        if ((ZComponent) bee == (object) null || bee.isDead)
          this.bees.RemoveAt(index);
        else if (!bee.stunned)
          bee.DoHighJump(extraBits);
      }
    }
  }

  public override void DoLongJump(int extraBits)
  {
    for (int index = this.bees.Count - 1; index >= 0; --index)
    {
      if (index < this.bees.Count)
      {
        ZCreature bee = this.bees[index];
        if ((ZComponent) bee == (object) null || bee.isDead)
          this.bees.RemoveAt(index);
        else if (!bee.stunned)
          bee.DoLongJump(extraBits);
      }
    }
  }
}
