// Decompiled with JetBrains decompiler
// Type: ZSpellJavelin
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellJavelin : ZSpell
{
  public void RemoveFake(ZCreature fake) => fake.collider.Disable();

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellJavelin zspellJavelin = this;
    if (!gotoStatic)
      zspellJavelin.target = new MyLocation(0, 0);
    ZCreatureJavelin fake = new ZCreatureJavelin();
    fake.health = 1;
    fake.baseCreature = Inert.GetCreature("Verdant Javelin Obj");
    fake.game = zspellJavelin.game;
    fake.parent = zspellJavelin.parent.parent;
    fake.collider = new ZMyCollider();
    fake.collider.baseCollider = Inert.GetBaseCollider("Verdant Javelin Obj");
    fake.collider.radius = 30;
    fake.collider.gameObjectLayer = 16;
    fake.collider.layer = 65536;
    fake.collider.creature = (ZCreature) fake;
    fake.collider.world = zspellJavelin.world;
    zspellJavelin.isMoving = true;
    zspellJavelin.zb = MapGenerator.getOutlineArray(zspellJavelin.radius);
    if (gotoStatic)
      yield return 0.0f;
label_60:
    while (!zspellJavelin.isDead)
    {
      zspellJavelin.pX = zspellJavelin.position.x;
      zspellJavelin.pY = zspellJavelin.position.y;
      FixedInt fixedInt1 = zspellJavelin.position.x + zspellJavelin.velocity.x;
      FixedInt fixedInt2 = zspellJavelin.position.y + zspellJavelin.velocity.y;
      zspellJavelin.validX = zspellJavelin.pX;
      zspellJavelin.validY = zspellJavelin.pY;
      zspellJavelin.steps = 1;
      FixedInt x = zspellJavelin.velocity.x;
      FixedInt y = zspellJavelin.velocity.y;
      FixedInt fixedInt3 = zspellJavelin.velocity.x;
      FixedInt fixedInt4 = zspellJavelin.velocity.y;
      if (x > 1 || x < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x) > FixedInt.Abs(y))
          zspellJavelin.steps = (int) FixedInt.Abs(x) + 1;
        else
          zspellJavelin.steps = (int) FixedInt.Abs(y) + 1;
        fixedInt3 = zspellJavelin.velocity.x / zspellJavelin.steps;
        fixedInt4 = zspellJavelin.velocity.y / zspellJavelin.steps;
      }
      while (zspellJavelin.steps > 0)
      {
        if (fake.isDead)
        {
          zspellJavelin.velocity.y = (FixedInt) 0;
          zspellJavelin.velocity.x = (FixedInt) 0;
          zspellJavelin.moving = (IEnumerator<float>) null;
          zspellJavelin.isMoving = false;
          zspellJavelin.isDead = true;
          zspellJavelin.isNull = true;
          zspellJavelin.RemoveFake((ZCreature) fake);
          zspellJavelin.OnExplosion();
          ZComponent.Destroy<GameObject>(zspellJavelin.gameObject);
        }
        if (zspellJavelin.map.SpellCheckEffectors(zspellJavelin.toCollideCheck, (ZSpell) zspellJavelin, (int) zspellJavelin.validX, (int) zspellJavelin.validY))
        {
          zspellJavelin.position = new MyLocation(zspellJavelin.validX, zspellJavelin.validY);
          fake.collider.Disable(false);
          yield return 0.0f;
          goto label_60;
        }
        else
        {
          --zspellJavelin.steps;
          int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zspellJavelin.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zspellJavelin.zb.Count) - zspellJavelin.radius;
          if (num1 < 0)
            num1 += zspellJavelin.zb.Count;
          for (int index1 = 0; index1 < zspellJavelin.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % zspellJavelin.zb.Count;
            int num2 = (int) (fixedInt3 + zspellJavelin.pX);
            int num3 = (int) (fixedInt4 + zspellJavelin.pY);
            if (!zspellJavelin.map.SpellCheckPosition(num2 + zspellJavelin.zb[index2].x, num3 + zspellJavelin.zb[index2].y, zspellJavelin.toCollideCheck, Inert.mask_spell_movement))
            {
              int num4 = num2 + zspellJavelin.zb[index2].x;
              int num5 = num3 + zspellJavelin.zb[index2].y;
              zspellJavelin.RemoveFake((ZCreature) fake);
              if (!zspellJavelin.map.CheckPositionOnlyMap(num4, num5))
              {
                if (MyLocation.Distance(zspellJavelin.target, MyLocation.zero) < 45)
                {
                  SpellSlot spellSlot = zspellJavelin.parent.GetSpellSlot(SpellEnum.Verdant_Javelin);
                  if (spellSlot != null)
                    spellSlot.bonusDmg -= 7;
                }
                ZCreatureJavelin javalin = ZCreatureCreate.CreateJavalin(zspellJavelin.game, zspellJavelin.toSummon.GetComponent<CreatureJavelin>(), (Vector2) new Vector3((float) num4, (float) num5), Quaternion.identity, zspellJavelin.game.GetMapTransform());
                javalin.game = zspellJavelin.game;
                javalin.collider.world = zspellJavelin.world;
                javalin.collider.Move(new MyLocation(num4, num5));
                javalin.position = new MyLocation(num4, num5);
                javalin.parent = zspellJavelin.parent?.parent;
                javalin.auraOfDecay.rotation = zspellJavelin.GetRotation;
                javalin.auraOfDecay.collider = javalin.collider;
                javalin.collider.effector = javalin.auraOfDecay;
                javalin.auraOfDecay.whoSummoned = zspellJavelin.parent;
                zspellJavelin.parent?.effectors?.Add(javalin.auraOfDecay);
                fake.collider.Move(new MyLocation(num4, num5));
                zspellJavelin.velocity.y = (FixedInt) 0;
                zspellJavelin.velocity.x = (FixedInt) 0;
                zspellJavelin.moving = (IEnumerator<float>) null;
                zspellJavelin.isMoving = false;
                zspellJavelin.isDead = true;
                zspellJavelin.isNull = true;
                if (zspellJavelin.game.isClient)
                {
                  javalin.auraOfDecay.SetRotation();
                  AudioManager.Play(AudioManager.instance.pop);
                }
                ZComponent.Destroy<GameObject>(zspellJavelin.gameObject);
                javalin.CreatureMoveSurroundings();
                yield break;
              }
              else
              {
                zspellJavelin.hitCreature = zspellJavelin.map.PhysicsCollideCreature(zspellJavelin.toCollideCheck, num4, num5);
                if ((ZComponent) zspellJavelin.hitCreature != (object) null && zspellJavelin.hitCreature.GetType() != typeof (ZCreatureJavelin))
                {
                  int v1 = zspellJavelin.damage / 2;
                  int num6 = MyLocation.Distance(zspellJavelin.target, MyLocation.zero);
                  if (num6 < 45)
                    zspellJavelin.damage -= (int) Mathd.Lerp((FixedInt) 0, (FixedInt) v1, (FixedInt) (45 - num6) / (FixedInt) 45);
                  zspellJavelin.velocity.y = (FixedInt) 0;
                  zspellJavelin.velocity.x = (FixedInt) 0;
                  zspellJavelin.position = new MyLocation(num2 + zspellJavelin.zb[index2].x, num3 + zspellJavelin.zb[index2].y);
                  zspellJavelin.moving = (IEnumerator<float>) null;
                  zspellJavelin.isMoving = false;
                  zspellJavelin.hitCreature.ApplyDamage(zspellJavelin.spellEnum, zspellJavelin.damageType, zspellJavelin.damage, zspellJavelin.parent, zspellJavelin.game.turn, (ISpellBridge) zspellJavelin);
                  zspellJavelin.isDead = true;
                  zspellJavelin.isNull = true;
                  if ((Object) zspellJavelin.explosion != (Object) null)
                    zspellJavelin.OnExplosion();
                  ZComponent.Destroy<GameObject>(zspellJavelin.gameObject);
                  yield break;
                }
                else
                {
                  if (MyLocation.Distance(zspellJavelin.target, MyLocation.zero) < 45)
                  {
                    SpellSlot spellSlot = zspellJavelin.parent.GetSpellSlot(SpellEnum.Verdant_Javelin);
                    if (spellSlot != null)
                      spellSlot.bonusDmg -= 7;
                  }
                  ZCreatureJavelin javalin = ZCreatureCreate.CreateJavalin(zspellJavelin.game, zspellJavelin.toSummon.GetComponent<CreatureJavelin>(), (Vector2) new Vector3((float) num4, (float) num5), Quaternion.identity, zspellJavelin.game.GetMapTransform());
                  javalin.game = zspellJavelin.game;
                  javalin.collider.world = zspellJavelin.world;
                  javalin.collider.Move(new MyLocation(num4, num5));
                  javalin.position = new MyLocation(num4, num5);
                  javalin.parent = zspellJavelin.parent?.parent;
                  javalin.auraOfDecay.rotation = zspellJavelin.GetRotation;
                  javalin.auraOfDecay.collider = javalin.collider;
                  javalin.collider.effector = javalin.auraOfDecay;
                  javalin.auraOfDecay.whoSummoned = zspellJavelin.parent;
                  zspellJavelin.parent?.effectors?.Add(javalin.auraOfDecay);
                  fake.collider.Move(new MyLocation(num4, num5));
                  zspellJavelin.velocity.y = (FixedInt) 0;
                  zspellJavelin.velocity.x = (FixedInt) 0;
                  zspellJavelin.moving = (IEnumerator<float>) null;
                  zspellJavelin.isMoving = false;
                  zspellJavelin.isDead = true;
                  zspellJavelin.isNull = true;
                  if (zspellJavelin.game.isClient)
                  {
                    javalin.auraOfDecay.SetRotation();
                    AudioManager.Play(AudioManager.instance.pop);
                  }
                  ZComponent.Destroy<GameObject>(zspellJavelin.gameObject);
                  javalin.CreatureMoveSurroundings();
                  yield break;
                }
              }
            }
          }
          zspellJavelin.validX = zspellJavelin.pX;
          zspellJavelin.validY = zspellJavelin.pY;
          zspellJavelin.pX = zspellJavelin.pX + fixedInt3;
          zspellJavelin.pY = zspellJavelin.pY + fixedInt4;
        }
      }
      zspellJavelin.position = zspellJavelin.position + zspellJavelin.velocity;
      zspellJavelin.target.x += Mathd.Abs(zspellJavelin.velocity.x);
      zspellJavelin.target.y += Mathd.Abs(zspellJavelin.velocity.y);
      if (zspellJavelin.position.y < zspellJavelin.radius)
      {
        zspellJavelin.moving = (IEnumerator<float>) null;
        zspellJavelin.velocity = MyLocation.zero;
        zspellJavelin.isMoving = false;
        zspellJavelin.Splash();
        zspellJavelin.RemoveFake((ZCreature) fake);
        zspellJavelin.DestroyDelay();
        break;
      }
      if (zspellJavelin.addVelocity)
      {
        zspellJavelin.addVelocity = false;
        zspellJavelin.velocity = zspellJavelin.velocity + zspellJavelin.addedVelocity;
        zspellJavelin.velocity.x = Mathd.Clamp(zspellJavelin.velocity.x, (FixedInt) -50, (FixedInt) 50);
        zspellJavelin.velocity.y = Mathd.Clamp(zspellJavelin.velocity.y, (FixedInt) -50, (FixedInt) 50);
        zspellJavelin.addedVelocity.x = (FixedInt) 0;
        zspellJavelin.addedVelocity.y = (FixedInt) 0;
      }
      else if (zspellJavelin.affectedByGravity && zspellJavelin.velocity.y > -ZMap.MaxSpeed)
        zspellJavelin.velocity.y += zspellJavelin.map.Gravity;
      else if (!zspellJavelin.affectedByGravity && zspellJavelin.velocity.y > -10 && zspellJavelin.maxDuration > 150)
        zspellJavelin.affectedByGravity = true;
      zspellJavelin.Wind();
      if (zspellJavelin.Rotates && (Object) zspellJavelin.transform != (Object) null)
        zspellJavelin.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(zspellJavelin.velocity.y.ToFloat(), zspellJavelin.velocity.x.ToFloat()) * 57.29578f);
      ++zspellJavelin.curDuration;
      if (zspellJavelin.curDuration >= zspellJavelin.maxDuration)
      {
        zspellJavelin.RemoveFake((ZCreature) fake);
        zspellJavelin.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
