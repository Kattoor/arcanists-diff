// Decompiled with JetBrains decompiler
// Type: ZSpellLeaf
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellLeaf : ZSpell
{
  internal static int[] colorIndex = new int[28]
  {
    244,
    243,
    242,
    241,
    240,
    239,
    238,
    52,
    51,
    50,
    49,
    48,
    47,
    46,
    32,
    31,
    30,
    29,
    28,
    27,
    26,
    17,
    16,
    15,
    14,
    13,
    12,
    11
  };

  public void BiltLeaf(bool left)
  {
    if (this.spellEnum != SpellEnum.Brine_Bolt && this.position.y >= this.map.Height - 150)
    {
      this.OnExplosion();
    }
    else
    {
      if (this.map.CheckPositionOnlyEntities((int) this.position.x, (int) this.position.y, (ZCreature) null, 256))
        return;
      this.map.ServerBitBltHue(left ? 56 : 57, (int) this.position.x, (int) this.position.y - 8, (float) this.Bounces / (float) byte.MaxValue, false);
      ZSpell.RemoveItemsOnBitBlt(this.game, (int) this.position.x, (int) this.position.y, Inert.Instance.leafTexture.width / 2 - 1);
    }
  }

  public void OnDeathLeaf(bool left)
  {
    if (ZComponent.IsNull((ZComponent) this))
      return;
    this.isDead = true;
    this.isNull = true;
    AudioManager.Play(this.explosionClip);
    this.BiltLeaf(left);
    if ((Object) this.gameObject == (Object) null)
      return;
    ZComponent.Destroy<GameObject>(this.gameObject);
  }

  private void ActivateLeaf()
  {
    if (this.explodeOnImpact)
      return;
    this.explodeOnImpact = true;
    this.parent.effectors.Add(this.effector);
  }

  private bool left
  {
    get => this.goToTarget;
    set => this.goToTarget = value;
  }

  public void ApplyExplosionForce(
    int spellRadius,
    MyLocation origin,
    FixedInt force,
    int EXORADIUS,
    Curve factor,
    bool startMoving = false)
  {
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
    MyLocation myLocation = Inert.Velocity(angle, force1);
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
    if (startMoving)
      this.StartMoving(false);
    this.affectedByGravity = true;
  }

  public override void Fall()
  {
    this.velocity = new MyLocation(0, 5);
    if (this.isMoving)
      return;
    this.moving = this.game.ongoing.RunSpell(this.SpellMove(false, true));
  }

  public override bool ShouldSpellFall(bool onlyMap = false)
  {
    if (this.velocity.y != 0 || this.isDead)
      return false;
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
        ZMap map1 = this.map;
        MyLocation position = this.position;
        int x1 = position.x.ToInt() + this.zb[index2].x;
        position = this.position;
        int y1 = position.y.ToInt() - 3 + this.zb[index2].y;
        if (map1.CheckPositionOnlyMap(x1, y1))
        {
          ZMap map2 = this.map;
          position = this.position;
          int x2 = position.x.ToInt() + this.zb[index2].x;
          position = this.position;
          int y2 = position.y.ToInt() - 3 + this.zb[index2].y;
          ZCreature toCollideCheck = this.toCollideCheck;
          if (map2.LeafCheckPosition(x2, y2, toCollideCheck, 512))
            continue;
        }
        return false;
      }
    }
    else
    {
      for (int index3 = 0; index3 < this.radius * 2; ++index3)
      {
        int index4 = (index3 + num) % this.zb.Count;
        ZMap map3 = this.map;
        MyLocation position = this.position;
        int x3 = position.x.ToInt() + this.zb[index4].x;
        position = this.position;
        int y3 = position.y.ToInt() - 3 + this.zb[index4].y;
        int movementNoEffector = Inert.mask_movement_NoEffector;
        if (map3.LeafCheckPosition(x3, y3, (ZCreature) null, movementNoEffector))
        {
          ZMap map4 = this.map;
          position = this.position;
          int x4 = position.x.ToInt() + this.zb[index4].x;
          position = this.position;
          int y4 = position.y.ToInt() - 3 + this.zb[index4].y;
          ZCreature toCollideCheck = this.toCollideCheck;
          if (map4.LeafCheckPosition(x4, y4, toCollideCheck, 512))
            continue;
        }
        return false;
      }
    }
    this.position = new MyLocation(this.position.x, this.position.y - 1);
    return true;
  }

  public void MoveOtherLeaves2()
  {
    if ((ZComponent) this.effector == (object) null)
      return;
    List<ZMyCollider> list = this.world.OverlapCircleAll((Point) this.position, this.radius + 5, this.collider, 512);
    for (int index = 0; index < list.Count; ++index)
    {
      if ((ZComponent) list[index] != (object) null && (ZComponent) list[index].spell != (object) null && !list[index].spell.isMoving && list[index].spell.spellEnum == SpellEnum.Autumn_Leaves && (ZComponent) list[index].effector != (object) null && (ZComponent) list[index].effector.whoSummoned != (object) null && (list[index].effector.TurnCreated > this.effector.TurnCreated || list[index].effector.whoSummoned.team != this.parent.team))
        list[index].spell.Fall();
    }
    this.world.listPool.ReturnList(list);
  }

  public int MoveOtherLeaves()
  {
    int num = 0;
    List<ZMyCollider> list = this.world.OverlapCircleAll((Point) this.position, this.radius + 5, this.collider, 512);
    for (int index = 0; index < list.Count; ++index)
    {
      if ((ZComponent) list[index] != (object) null && (ZComponent) list[index].spell != (object) null && !list[index].spell.isMoving && list[index].spell.spellEnum == SpellEnum.Autumn_Leaves)
      {
        list[index].spell.Fall();
        ++num;
      }
    }
    this.world.listPool.ReturnList(list);
    return num;
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellLeaf zspellLeaf = this;
    zspellLeaf.collider.Disable(false);
    zspellLeaf.collider.world = zspellLeaf.world;
    zspellLeaf.collider.layer |= 262144;
    zspellLeaf.effector.whoSummoned = zspellLeaf.parent;
    zspellLeaf.effector.spell = (ZSpell) zspellLeaf;
    zspellLeaf.collider.spell = (ZSpell) zspellLeaf;
    zspellLeaf.isMoving = true;
    zspellLeaf.zb = MapGenerator.getOutlineArray(zspellLeaf.radius);
    if (gotoStatic)
    {
      zspellLeaf.transform.localScale = new Vector3(zspellLeaf.left ? 1f : -1f, 1f, 1f);
      yield return 0.0f;
    }
label_2:
    int collisionFrame = 0;
    if (!gotoStatic)
      zspellLeaf.goToTarget = zspellLeaf.game.RandomInt(0, 2) == 0;
    if ((Object) zspellLeaf.transform != (Object) null)
      zspellLeaf.transform.localScale = new Vector3(zspellLeaf.left ? 1f : -1f, 1f, 1f);
    if (!gotoStatic && zspellLeaf.Bounces == 0 && zspellLeaf.curDuration > 0)
    {
      yield return 0.0f;
      if (zspellLeaf.maxTargetFrames != zspellLeaf.game.timeline.Count)
      {
        zspellLeaf.maxTargetFrames = zspellLeaf.game.timeline.Count;
        zspellLeaf.MoveOtherLeaves();
        yield return 0.0f;
      }
    }
    while (!zspellLeaf.isDead)
    {
      zspellLeaf.pX = zspellLeaf.position.x;
      zspellLeaf.pY = zspellLeaf.position.y;
      FixedInt fixedInt1 = zspellLeaf.position.x + zspellLeaf.velocity.x;
      FixedInt fixedInt2 = zspellLeaf.position.y + zspellLeaf.velocity.y;
      zspellLeaf.validX = zspellLeaf.pX;
      zspellLeaf.validY = zspellLeaf.pY;
      zspellLeaf.steps = 1;
      FixedInt x = zspellLeaf.velocity.x;
      FixedInt y = zspellLeaf.velocity.y;
      FixedInt fixedInt3 = zspellLeaf.velocity.x;
      FixedInt fixedInt4 = zspellLeaf.velocity.y;
      if (x > 1 || x < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x) > FixedInt.Abs(y))
          zspellLeaf.steps = (int) FixedInt.Abs(x) + 1;
        else
          zspellLeaf.steps = (int) FixedInt.Abs(y) + 1;
        fixedInt3 = zspellLeaf.velocity.x / zspellLeaf.steps;
        fixedInt4 = zspellLeaf.velocity.y / zspellLeaf.steps;
      }
      while (zspellLeaf.steps > 0)
      {
        if (zspellLeaf.map.SpellCheckEffectors(zspellLeaf.toCollideCheck, (ZSpell) zspellLeaf, (int) zspellLeaf.validX, (int) zspellLeaf.validY))
        {
          zspellLeaf.position = new MyLocation(zspellLeaf.validX, zspellLeaf.validY);
          yield return 0.0f;
          goto label_2;
        }
        else
        {
          --zspellLeaf.steps;
          int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zspellLeaf.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zspellLeaf.zb.Count) - zspellLeaf.radius;
          if (num1 < 0)
            num1 += zspellLeaf.zb.Count;
          for (int index1 = 0; index1 < zspellLeaf.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % zspellLeaf.zb.Count;
            int num2 = (int) (fixedInt3 + zspellLeaf.pX);
            int num3 = (int) (fixedInt4 + zspellLeaf.pY);
            if ((zspellLeaf.curDuration > collisionFrame ? (!zspellLeaf.map.LeafCheckPosition(num2 + zspellLeaf.zb[index2].x, num3 + zspellLeaf.zb[index2].y, zspellLeaf.toCollideCheck, Inert.mask_spell_movement) ? 1 : 0) : (!zspellLeaf.map.CheckPositionOnlyMap(num2 + zspellLeaf.zb[index2].x, num3 + zspellLeaf.zb[index2].y) ? 1 : 0)) != 0 || !zspellLeaf.map.LeafCheckPosition(num2 + zspellLeaf.zb[index2].x, num3 + zspellLeaf.zb[index2].y, zspellLeaf.toCollideCheck, 512))
            {
              if (!zspellLeaf.map.CheckPositionOnlyMap(num2 + zspellLeaf.zb[index2].x, num3 + zspellLeaf.zb[index2].y))
              {
                if (zspellLeaf.velocity.y > 0)
                {
                  zspellLeaf.velocity.y = (FixedInt) -5;
                  goto label_51;
                }
                else
                {
                  zspellLeaf.position = new MyLocation(zspellLeaf.validX, zspellLeaf.validY);
                  if (zspellLeaf.Bounces > 0)
                  {
                    yield return 0.0f;
                    yield return 0.0f;
                  }
                  if (zspellLeaf.position.y >= zspellLeaf.map.Height)
                  {
                    if ((Object) zspellLeaf.explosion != (Object) null)
                      zspellLeaf.OnExplosion();
                    zspellLeaf.DestroyDelay();
                    yield break;
                  }
                  else if (zspellLeaf.Bounces <= 0)
                  {
                    zspellLeaf.KillMovementSimple();
                    zspellLeaf.collider.Enable(zspellLeaf.position);
                    zspellLeaf.ActivateLeaf();
                    yield break;
                  }
                  else
                  {
                    zspellLeaf.OnDeathLeaf(zspellLeaf.left);
                    yield break;
                  }
                }
              }
              else
              {
                ZCreature zcreature = zspellLeaf.map.PhysicsCollideCreature(zspellLeaf.toCollideCheck, num2 + zspellLeaf.zb[index2].x, num3 + zspellLeaf.zb[index2].y);
                if ((ZComponent) zcreature != (object) null && typeof (ZCreatureThorn) != zcreature.GetType() && zcreature.type != CreatureType.Tree)
                {
                  if (zspellLeaf.Bounces != 0 || !((ZComponent) zspellLeaf.parent != (object) null) || zcreature.team != zspellLeaf.parent.team)
                  {
                    zspellLeaf.velocity.y = (FixedInt) 0;
                    zspellLeaf.velocity.x = (FixedInt) 0;
                    zspellLeaf.addedDeathVector = Inert.Velocity(Inert.AngleOfVelocity(zspellLeaf.velocity), zspellLeaf.radius);
                    zspellLeaf.position = new MyLocation(zspellLeaf.validX, zspellLeaf.validY);
                    zspellLeaf.moving = (IEnumerator<float>) null;
                    zspellLeaf.isMoving = false;
                    zspellLeaf.isNull = true;
                    zspellLeaf.isDead = true;
                    zcreature.ApplyDamage(zspellLeaf.spellEnum, zspellLeaf.damageType, zspellLeaf.damage, zspellLeaf.parent, zspellLeaf.game.turn, (ISpellBridge) zspellLeaf);
                    if ((Object) zspellLeaf.explosion != (Object) null)
                      zspellLeaf.OnExplosion();
                    ZComponent.Destroy<GameObject>(zspellLeaf.gameObject);
                    yield break;
                  }
                }
                else if (zspellLeaf.velocity.y > 0)
                {
                  zspellLeaf.velocity.y = (FixedInt) -5;
                  goto label_51;
                }
                else
                {
                  zspellLeaf.position = new MyLocation(zspellLeaf.validX, zspellLeaf.validY);
                  if (zspellLeaf.position.y >= zspellLeaf.map.Height)
                  {
                    if ((Object) zspellLeaf.explosion != (Object) null)
                      zspellLeaf.OnExplosion();
                    zspellLeaf.DestroyDelay();
                    yield break;
                  }
                  else if (zspellLeaf.Bounces <= 0)
                  {
                    zspellLeaf.KillMovementSimple();
                    zspellLeaf.collider.Enable(zspellLeaf.position);
                    zspellLeaf.ActivateLeaf();
                    yield break;
                  }
                  else
                  {
                    zspellLeaf.OnDeathLeaf(zspellLeaf.left);
                    yield break;
                  }
                }
              }
            }
          }
          zspellLeaf.validX = zspellLeaf.pX;
          zspellLeaf.validY = zspellLeaf.pY;
          zspellLeaf.pX = zspellLeaf.pX + fixedInt3;
          zspellLeaf.pY = zspellLeaf.pY + fixedInt4;
        }
      }
      zspellLeaf.position = zspellLeaf.position + zspellLeaf.velocity;
label_51:
      if (zspellLeaf.position.y < zspellLeaf.radius)
      {
        zspellLeaf.moving = (IEnumerator<float>) null;
        zspellLeaf.velocity = MyLocation.zero;
        zspellLeaf.isMoving = false;
        zspellLeaf.Splash();
        if (zspellLeaf.Bounces == 0)
        {
          zspellLeaf.OnDeath(true);
          break;
        }
        zspellLeaf.OnDeathLeaf(zspellLeaf.left);
        break;
      }
      if (zspellLeaf.addVelocity)
      {
        zspellLeaf.addVelocity = false;
        zspellLeaf.velocity = zspellLeaf.velocity + zspellLeaf.addedVelocity;
        zspellLeaf.addedVelocity.x = (FixedInt) 0;
        zspellLeaf.addedVelocity.y = (FixedInt) 0;
      }
      else if (zspellLeaf.affectedByGravity && zspellLeaf.velocity.y > -ZMap.MaxSpeed)
      {
        zspellLeaf.velocity.y += zspellLeaf.map.Gravity;
        int num = 0;
        if (zspellLeaf.velocity.y != -5)
        {
          if (zspellLeaf.velocity.y < -5)
            zspellLeaf.velocity.y -= zspellLeaf.map.Gravity;
          else
            zspellLeaf.velocity.y += zspellLeaf.map.Gravity;
          if (Mathd.Abs(zspellLeaf.velocity.y) <= (FixedInt) 5 - zspellLeaf.map.Gravity)
          {
            zspellLeaf.velocity.y = (FixedInt) -5;
            ++num;
          }
        }
        else
          ++num;
        if (zspellLeaf.velocity.x != 0)
        {
          zspellLeaf.velocity.x += zspellLeaf.velocity.x > 0 ? zspellLeaf.map.Gravity : -zspellLeaf.map.Gravity;
          if (Mathd.Abs(zspellLeaf.velocity.x) <= -zspellLeaf.map.Gravity)
          {
            zspellLeaf.velocity.x = (FixedInt) 0;
            ++num;
          }
        }
        else
          ++num;
        if (num >= 2)
          zspellLeaf.affectedByGravity = false;
      }
      else if (!zspellLeaf.affectedByGravity && zspellLeaf.velocity.y > -3 && zspellLeaf.maxDuration > 150 && zspellLeaf.curDuration > 10)
      {
        zspellLeaf.velocity.y += zspellLeaf.map.Gravity;
      }
      else
      {
        FixedInt fixedInt5 = (FixedInt) 524288L;
        if (zspellLeaf.left)
        {
          if (zspellLeaf.velocity.x > 0)
          {
            zspellLeaf.velocity.y -= fixedInt5;
            zspellLeaf.velocity.x -= 1;
          }
          else if (zspellLeaf.velocity.x > -7)
          {
            zspellLeaf.velocity.x -= 1;
            zspellLeaf.velocity.y += fixedInt5;
          }
          else
          {
            zspellLeaf.left = false;
            zspellLeaf.transform.localScale = new Vector3(-1f, 1f, 1f);
          }
        }
        else if (zspellLeaf.velocity.x < 0)
        {
          zspellLeaf.velocity.y -= fixedInt5;
          zspellLeaf.velocity.x += 1;
        }
        else if (zspellLeaf.velocity.x < 7)
        {
          zspellLeaf.velocity.x += 1;
          zspellLeaf.velocity.y += fixedInt5;
        }
        else
        {
          zspellLeaf.left = true;
          zspellLeaf.transform.localScale = new Vector3(1f, 1f, 1f);
        }
      }
      zspellLeaf.Wind();
      ++zspellLeaf.curDuration;
      if (zspellLeaf.curDuration >= zspellLeaf.maxDuration)
      {
        if ((Object) zspellLeaf.explosion != (Object) null)
          zspellLeaf.OnExplosion();
        ZComponent.Destroy<GameObject>(zspellLeaf.gameObject);
        zspellLeaf.isDead = true;
        zspellLeaf.isNull = true;
        break;
      }
      yield return 0.0f;
    }
  }
}
