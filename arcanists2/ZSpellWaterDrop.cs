// Decompiled with JetBrains decompiler
// Type: ZSpellWaterDrop
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellWaterDrop : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellWaterDrop spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (!gotoStatic)
      spell.curDestroyTerrainBounces = spell.destroyTerrainBounces;
    if (spell.spellEnum == SpellEnum.Starfire_Shard && (ZComponent) spell.parent != (object) null && spell.parent.familiarLevelCosmos > 0 && !gotoStatic)
    {
      if (spell.parent.familiarLevelCosmos == 1)
        spell.explosionCutout = ExplosionCutout.Forty_Five;
      else if (spell.parent.familiarLevelCosmos == 2)
        spell.explosionCutout = ExplosionCutout.Fifty;
      else if (spell.parent.familiarLevelCosmos == 3)
        spell.explosionCutout = ExplosionCutout.Sixty;
      else if (spell.parent.familiarLevelCosmos == 4)
        spell.explosionCutout = ExplosionCutout.Sixty;
      else
        spell.explosionCutout = ExplosionCutout.Seventy_Five;
    }
    if (gotoStatic)
      yield return 0.0f;
label_69:
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
        if (FixedInt.Abs(x1) > FixedInt.Abs(y1))
          spell.steps = (int) FixedInt.Abs(x1) + 1;
        else
          spell.steps = (int) FixedInt.Abs(y1) + 1;
        fixedInt3 = spell.velocity.x / spell.steps;
        fixedInt4 = spell.velocity.y / spell.steps;
      }
      while (spell.steps > 0)
      {
        if (spell.map.SpellCheckEffectors(spell.toCollideCheck, (ZSpell) spell, (int) spell.validX, (int) spell.validY))
        {
          spell.position = new MyLocation(spell.validX, spell.validY);
          yield return 0.0f;
          goto label_69;
        }
        else
        {
          --spell.steps;
          int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(spell.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * spell.zb.Count) - spell.radius;
          if (num1 < 0)
            num1 += spell.zb.Count;
          for (int index1 = 0; index1 < spell.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % spell.zb.Count;
            int num2 = (int) (fixedInt3 + spell.pX);
            int num3 = (int) (fixedInt4 + spell.pY);
            if ((spell.curDuration < 3 ? (!spell.map.CheckPositionOnlyMap(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y) ? 1 : 0) : (!spell.map.SpellCheckPosition(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y, spell.toCollideCheck, Inert.mask_spell_movement) ? 1 : 0)) != 0)
            {
              int x2 = num2 + spell.zb[index2].x;
              int y2 = num3 + spell.zb[index2].y;
              if (spell.spellEnum == SpellEnum.Firecrackers || !spell.map.CheckPositionOnlyMap(x2, y2))
              {
                if (spell.timesBounced < spell.Bounces)
                {
                  ++spell.timesBounced;
                  spell.position = new MyLocation(spell.validX, spell.validY);
                  MyLocation zero = MyLocation.zero;
                  int num4 = num2 + spell.zb[index2].x;
                  int num5 = num3 + spell.zb[index2].y;
                  for (int index3 = -2; index3 <= 2; ++index3)
                  {
                    for (int index4 = -2; index4 <= 2; ++index4)
                    {
                      if (spell.map.SpellCheckPosition(num4 + index3, num5 + index4, spell.toCollideCheck, Inert.mask_movement_NoEffector))
                      {
                        zero.x += index3;
                        zero.y += index4;
                      }
                    }
                  }
                  bool flag = false;
                  if (spell.destroyTerrainFirstBounce && spell.curDestroyTerrainBounces > 0)
                  {
                    --spell.curDestroyTerrainBounces;
                    spell.map.ServerBitBlt((int) spell.explosionCutout, x2, y2);
                    spell.MoveSurroundings(spell.EXORADIUS + 5);
                    if (MyLocation.Distance(MyLocation.zero, spell.velocity) > 40)
                      flag = true;
                  }
                  if (!flag)
                  {
                    zero.Normalize();
                    MyLocation ret;
                    MyLocation.Reflect(spell.velocity, ref zero, out ret);
                    spell.velocity = ret;
                    spell.velocity = spell.velocity * spell.elasticity;
                  }
                  if (spell.game.isClient && !spell.game.resyncing && (Object) spell.toSummon != (Object) null)
                    ZComponent.Instantiate<GameObject>(spell.toSummon, new Vector3((float) x2, (float) y2), Quaternion.identity, spell.game.GetMapTransform());
                  if (spell.ApplyExplosionForce(spell.position, Inert.mask_Phantom))
                  {
                    spell.StopAndDie();
                    spell.OnExplosionGeneric();
                    yield break;
                  }
                  else
                  {
                    spell.Undie();
                    if (spell.damageType == DamageType.Water && spell.game.isClient)
                    {
                      AudioManager.Play(AudioManager.instance.waterBounce);
                      goto label_62;
                    }
                    else
                      goto label_62;
                  }
                }
                else
                {
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.position = new MyLocation(spell.validX, spell.validY);
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.OnDeath(true);
                  spell.OnExplosionGeneric();
                  yield break;
                }
              }
              else
              {
                spell.velocity.y = (FixedInt) 0;
                spell.velocity.x = (FixedInt) 0;
                spell.position = new MyLocation(spell.validX, spell.validY);
                spell.moving = (IEnumerator<float>) null;
                spell.isMoving = false;
                spell.map.ServerBitBlt((int) spell.explosionCutout, x2, y2);
                spell.OnDeath(true);
                spell.OnExplosionGeneric();
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
        spell.velocity = MyLocation.zero;
        spell.isMoving = false;
        spell.Splash();
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
      else if (spell.affectedByGravity && spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      else if (!spell.affectedByGravity && spell.velocity.y > -10 && spell.maxDuration > 150)
        spell.affectedByGravity = true;
      spell.Wind();
label_62:
      if (spell.Rotates && (Object) spell.transform != (Object) null)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        if (spell.spellEnum == SpellEnum.Firecrackers)
        {
          spell.OnDeath(true);
          spell.OnExplosionGeneric();
          break;
        }
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
