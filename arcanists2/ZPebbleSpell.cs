

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZPebbleSpell : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZPebbleSpell zpebbleSpell = this;
    zpebbleSpell.isMoving = true;
    zpebbleSpell.zb = MapGenerator.getOutlineArray(zpebbleSpell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_53:
    while (!zpebbleSpell.isDead)
    {
      zpebbleSpell.pX = zpebbleSpell.position.x;
      zpebbleSpell.pY = zpebbleSpell.position.y;
      FixedInt fixedInt1 = zpebbleSpell.position.x + zpebbleSpell.velocity.x;
      FixedInt fixedInt2 = zpebbleSpell.position.y + zpebbleSpell.velocity.y;
      zpebbleSpell.validX = zpebbleSpell.pX;
      zpebbleSpell.validY = zpebbleSpell.pY;
      zpebbleSpell.steps = 1;
      FixedInt x1 = zpebbleSpell.velocity.x;
      FixedInt y1 = zpebbleSpell.velocity.y;
      FixedInt fixedInt3 = zpebbleSpell.velocity.x;
      FixedInt fixedInt4 = zpebbleSpell.velocity.y;
      if (x1 > 1 || x1 < -1 || y1 > 1 || y1 < -1)
      {
        if (FixedInt.Abs(x1) > FixedInt.Abs(y1))
          zpebbleSpell.steps = (int) FixedInt.Abs(x1) + 1;
        else
          zpebbleSpell.steps = (int) FixedInt.Abs(y1) + 1;
        fixedInt3 = zpebbleSpell.velocity.x / zpebbleSpell.steps;
        fixedInt4 = zpebbleSpell.velocity.y / zpebbleSpell.steps;
      }
      while (zpebbleSpell.steps > 0)
      {
        --zpebbleSpell.steps;
        int pX = (int) zpebbleSpell.pX;
        int pY = (int) zpebbleSpell.pY;
        int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zpebbleSpell.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zpebbleSpell.zb.Count) - zpebbleSpell.radius;
        if (num1 < 0)
          num1 += zpebbleSpell.zb.Count;
        for (int index1 = 0; index1 < zpebbleSpell.radius * 2; ++index1)
        {
          int index2 = (index1 + num1) % zpebbleSpell.zb.Count;
          if (!zpebbleSpell.map.SpellCheckPosition(pX + zpebbleSpell.zb[index2].x, pY + zpebbleSpell.zb[index2].y, zpebbleSpell.toCollideCheck, Inert.mask_spell_movement))
          {
            int x2 = pX + zpebbleSpell.zb[index2].x;
            int y2 = pY + zpebbleSpell.zb[index2].y;
            if (!zpebbleSpell.map.CheckPositionOnlyMap(x2, y2))
            {
              if (zpebbleSpell.game.isClient && !zpebbleSpell.game.resyncing)
                AudioManager.PlayNapalm(AudioManager.instance.spellBounce);
              if (zpebbleSpell.timesBounced < 10)
              {
                ++zpebbleSpell.timesBounced;
                zpebbleSpell.position = new MyLocation(zpebbleSpell.validX, zpebbleSpell.validY);
                MyLocation zero = MyLocation.zero;
                int num2 = pX + zpebbleSpell.zb[index2].x;
                int num3 = pY + zpebbleSpell.zb[index2].y;
                for (int index3 = -2; index3 <= 2; ++index3)
                {
                  for (int index4 = -2; index4 <= 2; ++index4)
                  {
                    if (zpebbleSpell.map.SpellCheckPosition(num2 + index3, num3 + index4, zpebbleSpell.toCollideCheck, Inert.mask_movement_NoEffector))
                    {
                      zero.x += index3;
                      zero.y += index4;
                    }
                  }
                }
                zero.Normalize();
                MyLocation.Reflect(zpebbleSpell.velocity, ref zero, out zpebbleSpell.velocity);
                zpebbleSpell.velocity = zpebbleSpell.velocity * zpebbleSpell.elasticity;
                zpebbleSpell.map.ServerBitBlt((int) zpebbleSpell.explosionCutout, x2, y2);
                FixedInt explisiveForce = zpebbleSpell.explisiveForce;
                zpebbleSpell.explisiveForce = (FixedInt) 2;
                if (zpebbleSpell.ApplyExplosionForce(zpebbleSpell.position))
                {
                  zpebbleSpell.StopAndDie();
                  yield break;
                }
                else
                {
                  List<ZMyCollider> zmyColliderList = zpebbleSpell.game.world.OverlapCircleAll((Point) zpebbleSpell.position, 50, (ZMyCollider) null, 256);
                  for (int index5 = 0; index5 < zmyColliderList.Count; ++index5)
                  {
                    if ((ZComponent) zmyColliderList[index5] != (object) null && !ZComponent.IsNull((ZComponent) zmyColliderList[index5].creature) && !zmyColliderList[index5].creature.isDead && zmyColliderList[index5].creature.type == CreatureType.Tree)
                      zmyColliderList[index5].creature.ApplyDamage(zpebbleSpell.spellEnum, zpebbleSpell.damageType, zpebbleSpell.damage, zpebbleSpell.parent, zpebbleSpell.game.turn, (ISpellBridge) zpebbleSpell);
                  }
                  zpebbleSpell.explisiveForce = (FixedInt) (float) (int) explisiveForce;
                  zpebbleSpell.Undie();
                  if (zpebbleSpell.game.isClient)
                  {
                    GameObject gameObject = zpebbleSpell.gameObject;
                    if (gameObject != null)
                    {
                      gameObject.GetComponent<ParticleSystem>().Play();
                      goto label_50;
                    }
                    else
                      goto label_50;
                  }
                  else
                    goto label_50;
                }
              }
              else
              {
                zpebbleSpell.StopAndDie();
                yield break;
              }
            }
            else
            {
              zpebbleSpell.map.ServerBitBlt((int) zpebbleSpell.explosionCutout, x2, y2);
              zpebbleSpell.StopAndDie();
              yield break;
            }
          }
        }
        zpebbleSpell.validX = zpebbleSpell.pX;
        zpebbleSpell.validY = zpebbleSpell.pY;
        zpebbleSpell.pX = zpebbleSpell.pX + fixedInt3;
        zpebbleSpell.pY = zpebbleSpell.pY + fixedInt4;
        if (zpebbleSpell.map.SpellCheckEffectors(zpebbleSpell.toCollideCheck, (ZSpell) zpebbleSpell, (int) zpebbleSpell.validX, (int) zpebbleSpell.validY))
        {
          zpebbleSpell.position = new MyLocation(zpebbleSpell.validX, zpebbleSpell.validY);
          yield return 0.0f;
          goto label_53;
        }
      }
      zpebbleSpell.position = zpebbleSpell.position + zpebbleSpell.velocity;
      if (zpebbleSpell.position.y < zpebbleSpell.radius)
      {
        zpebbleSpell.moving = (IEnumerator<float>) null;
        zpebbleSpell.velocity = MyLocation.zero;
        zpebbleSpell.isMoving = false;
        zpebbleSpell.Splash();
        zpebbleSpell.DestroyDelay();
        break;
      }
      if (zpebbleSpell.addVelocity)
      {
        zpebbleSpell.addVelocity = false;
        zpebbleSpell.velocity = zpebbleSpell.velocity + zpebbleSpell.addedVelocity;
        zpebbleSpell.addedVelocity.x = (FixedInt) 0;
        zpebbleSpell.addedVelocity.y = (FixedInt) 0;
      }
      else if (zpebbleSpell.affectedByGravity && zpebbleSpell.velocity.y > -ZMap.MaxSpeed)
        zpebbleSpell.velocity.y += zpebbleSpell.map.Gravity;
      else if (!zpebbleSpell.affectedByGravity && zpebbleSpell.velocity.y > -1 && zpebbleSpell.maxDuration > 150)
        zpebbleSpell.affectedByGravity = true;
      zpebbleSpell.Wind();
label_50:
      ++zpebbleSpell.curDuration;
      if (zpebbleSpell.curDuration >= zpebbleSpell.maxDuration)
      {
        zpebbleSpell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
