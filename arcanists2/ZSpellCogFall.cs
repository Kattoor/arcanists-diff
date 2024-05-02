// Decompiled with JetBrains decompiler
// Type: ZSpellCogFall
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellCogFall : ZSpell
{
  public void Blit()
  {
    this.map.ServerBitBlt((int) this.snowCutout, (int) this.position.x, (int) this.position.y, false);
    ZSpell.RemoveItemsOnBitBlt(this.game, (int) this.position.x, (int) this.position.y, this.snowTexture.width / 2 - 1);
  }

  public override void OnDeath(bool playDeathClip = true)
  {
    this.game.forceRysncPause = true;
    if (ZComponent.IsNull((ZComponent) this))
      return;
    this.isDead = true;
    this.isNull = true;
    this.Blit();
    if (!((Object) this.gameObject != (Object) null))
      return;
    ZComponent.Destroy<GameObject>(this.gameObject);
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellCogFall zspellCogFall = this;
    zspellCogFall.isMoving = true;
    zspellCogFall.zb = MapGenerator.getOutlineArray(zspellCogFall.radius);
    if (gotoStatic)
      yield return 0.0f;
label_55:
    while (!zspellCogFall.isDead)
    {
      if (zspellCogFall.curDuration > 10 && !zspellCogFall.affectedByGravity && zspellCogFall.spellEnum != SpellEnum.Barrage_of_Bones)
      {
        zspellCogFall.velocity.x = (FixedInt) 0;
        zspellCogFall.velocity.y = (FixedInt) -5;
      }
      zspellCogFall.pX = zspellCogFall.position.x;
      zspellCogFall.pY = zspellCogFall.position.y;
      FixedInt fixedInt1 = zspellCogFall.position.x + zspellCogFall.velocity.x;
      FixedInt fixedInt2 = zspellCogFall.position.y + zspellCogFall.velocity.y;
      zspellCogFall.validX = zspellCogFall.pX;
      zspellCogFall.validY = zspellCogFall.pY;
      zspellCogFall.steps = 1;
      FixedInt x = zspellCogFall.velocity.x;
      FixedInt y = zspellCogFall.velocity.y;
      FixedInt fixedInt3 = zspellCogFall.velocity.x;
      FixedInt fixedInt4 = zspellCogFall.velocity.y;
      if (x > 1 || x < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x) > FixedInt.Abs(y))
          zspellCogFall.steps = (int) FixedInt.Abs(x) + 1;
        else
          zspellCogFall.steps = (int) FixedInt.Abs(y) + 1;
        fixedInt3 = zspellCogFall.velocity.x / zspellCogFall.steps;
        fixedInt4 = zspellCogFall.velocity.y / zspellCogFall.steps;
      }
      while (zspellCogFall.steps > 0)
      {
        if (zspellCogFall.map.SpellCheckEffectors(zspellCogFall.toCollideCheck, (ZSpell) zspellCogFall, (int) zspellCogFall.validX, (int) zspellCogFall.validY))
        {
          zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY);
          yield return 0.0f;
          goto label_55;
        }
        else
        {
          --zspellCogFall.steps;
          int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zspellCogFall.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zspellCogFall.zb.Count) - zspellCogFall.radius;
          if (num1 < 0)
            num1 += zspellCogFall.zb.Count;
          for (int index1 = 0; index1 < zspellCogFall.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % zspellCogFall.zb.Count;
            if (!zspellCogFall.map.SpellCheckPosition((int) (fixedInt3 + zspellCogFall.pX) + zspellCogFall.zb[index2].x, (int) (fixedInt4 + zspellCogFall.pY) + zspellCogFall.zb[index2].y, zspellCogFall.toCollideCheck, Inert.mask_spell_movement))
            {
              int num2 = (int) (fixedInt3 + zspellCogFall.pX);
              int num3 = (int) (fixedInt4 + zspellCogFall.pY);
              if (!zspellCogFall.map.CheckPositionOnlyMap(num2 + zspellCogFall.zb[index2].x, num3 + zspellCogFall.zb[index2].y))
              {
                if (zspellCogFall.timesBounced < 10)
                {
                  ++zspellCogFall.timesBounced;
                  zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY);
                  MyLocation zero = MyLocation.zero;
                  int num4 = num2 + zspellCogFall.zb[index2].x;
                  int num5 = num3 + zspellCogFall.zb[index2].y;
                  for (int index3 = -2; index3 <= 2; ++index3)
                  {
                    for (int index4 = -2; index4 <= 2; ++index4)
                    {
                      if (zspellCogFall.map.SpellCheckPosition(num4 + index3, num5 + index4, zspellCogFall.toCollideCheck, Inert.mask_movement_NoEffector))
                      {
                        zero.x += index3;
                        zero.y += index4;
                      }
                    }
                  }
                  zero.Normalize();
                  MyLocation.Reflect(zspellCogFall.velocity, ref zero, out zspellCogFall.velocity);
                  zspellCogFall.velocity = zspellCogFall.velocity * zspellCogFall.elasticity;
                  if (zspellCogFall.game.isClient)
                  {
                    AudioManager.Play(zspellCogFall.spellEnum == SpellEnum.Barrage_of_Bones ? AudioManager.instance.spellBounceBones : AudioManager.instance.spellBounce);
                    goto label_50;
                  }
                  else
                    goto label_50;
                }
                else
                {
                  zspellCogFall.velocity.y = (FixedInt) 0;
                  zspellCogFall.velocity.x = (FixedInt) 0;
                  zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY);
                  zspellCogFall.moving = (IEnumerator<float>) null;
                  zspellCogFall.isMoving = false;
                  zspellCogFall.OnDeath(true);
                  yield break;
                }
              }
              else
              {
                zspellCogFall.hitCreature = zspellCogFall.map.PhysicsCollideCreature(zspellCogFall.toCollideCheck, num2 + zspellCogFall.zb[index2].x, num3 + zspellCogFall.zb[index2].y);
                if ((ZComponent) zspellCogFall.hitCreature != (object) null)
                {
                  zspellCogFall.addedDeathVector = Inert.Velocity(Inert.AngleOfVelocity(zspellCogFall.velocity), zspellCogFall.radius);
                  if (zspellCogFall.spellEnum == SpellEnum.Cog_Fall)
                    zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY) + Inert.Velocity(Inert.AngleOfVelocity(zspellCogFall.velocity), 9);
                  else
                    zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY);
                  zspellCogFall.velocity.y = (FixedInt) 0;
                  zspellCogFall.velocity.x = (FixedInt) 0;
                  zspellCogFall.moving = (IEnumerator<float>) null;
                  zspellCogFall.isMoving = false;
                  zspellCogFall.hitCreature.ApplyDamage(zspellCogFall.spellEnum, zspellCogFall.damageType, zspellCogFall.damage, zspellCogFall.parent, zspellCogFall.game.turn, (ISpellBridge) zspellCogFall);
                  if ((Object) zspellCogFall.explosion != (Object) null)
                    zspellCogFall.OnExplosion();
                  zspellCogFall.map.ServerBitBlt(zspellCogFall.spellEnum == SpellEnum.Barrage_of_Bones ? (int) zspellCogFall.explosionCutout : 11, (int) zspellCogFall.position.x, (int) zspellCogFall.position.y);
                  zspellCogFall.map.ServerBitBlt((int) zspellCogFall.snowCutout, (int) zspellCogFall.position.x, (int) zspellCogFall.position.y, false);
                  ZSpell.RemoveItemsOnBitBlt(zspellCogFall.game, (int) zspellCogFall.position.x, (int) zspellCogFall.position.y, zspellCogFall.snowTexture.width / 2 - 1);
                  zspellCogFall.game.CreatureMoveSurroundings(zspellCogFall.position, zspellCogFall.EXORADIUS);
                  zspellCogFall.OnDeath(true);
                  yield break;
                }
                else
                {
                  zspellCogFall.position = new MyLocation(zspellCogFall.validX, zspellCogFall.validY);
                  zspellCogFall.OnDeath(true);
                  yield break;
                }
              }
            }
          }
          zspellCogFall.validX = zspellCogFall.pX;
          zspellCogFall.validY = zspellCogFall.pY;
          zspellCogFall.pX = zspellCogFall.pX + fixedInt3;
          zspellCogFall.pY = zspellCogFall.pY + fixedInt4;
        }
      }
      zspellCogFall.position = zspellCogFall.position + zspellCogFall.velocity;
      if (zspellCogFall.position.y < zspellCogFall.radius)
      {
        zspellCogFall.moving = (IEnumerator<float>) null;
        zspellCogFall.velocity = MyLocation.zero;
        zspellCogFall.isMoving = false;
        zspellCogFall.isDead = true;
        zspellCogFall.isNull = true;
        zspellCogFall.Splash();
        ZComponent.Destroy<GameObject>(zspellCogFall.gameObject);
        break;
      }
      if (zspellCogFall.addVelocity)
      {
        zspellCogFall.addVelocity = false;
        zspellCogFall.velocity = zspellCogFall.velocity + zspellCogFall.addedVelocity;
        zspellCogFall.velocity.x = Mathd.Clamp(zspellCogFall.velocity.x, (FixedInt) -50, (FixedInt) 50);
        zspellCogFall.velocity.y = Mathd.Clamp(zspellCogFall.velocity.y, (FixedInt) -50, (FixedInt) 50);
        zspellCogFall.addedVelocity.x = (FixedInt) 0;
        zspellCogFall.addedVelocity.y = (FixedInt) 0;
      }
      else if (zspellCogFall.affectedByGravity && zspellCogFall.velocity.y > -ZMap.MaxSpeed)
        zspellCogFall.velocity.y += zspellCogFall.map.Gravity;
      else if (!zspellCogFall.affectedByGravity && zspellCogFall.velocity.y > -10 && zspellCogFall.maxDuration > 150)
        zspellCogFall.affectedByGravity = true;
      zspellCogFall.Wind();
label_50:
      ++zspellCogFall.curDuration;
      if (zspellCogFall.curDuration >= zspellCogFall.maxDuration)
      {
        zspellCogFall.isDead = true;
        zspellCogFall.isNull = true;
        if ((Object) zspellCogFall.explosion != (Object) null)
          zspellCogFall.OnExplosion();
        ZComponent.Destroy<GameObject>(zspellCogFall.gameObject);
        break;
      }
      yield return 0.0f;
    }
  }
}
