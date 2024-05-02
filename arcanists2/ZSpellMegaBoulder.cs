

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellMegaBoulder : ZSpell
{
  private const int lifetime = 30;

  public void Expand()
  {
    AudioManager.Play(this.explosionClip);
    if (this.game.isClient && (Object) this.gameObject != (Object) null)
      this.gameObject.GetComponent<SpriteRenderer>().sprite = Inert.Instance.megaBoulderSprite;
    this.radius = 28;
    this.velocity.x *= 786432L;
    this.zb = MapGenerator.getOutlineArray(this.radius);
    this.lastHit = this.curDuration;
    this.OnExplosion();
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellMegaBoulder spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_59:
    while (!spell.isDead)
    {
      if (spell.curDuration == 30 && spell.radius != 28)
        spell.Expand();
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
          goto label_59;
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
            if (!spell.map.SpellCheckPosition((int) (fixedInt3 + spell.pX) + spell.zb[index2].x, (int) (fixedInt4 + spell.pY) + spell.zb[index2].y, spell.toCollideCheck, Inert.mask_spell_movement))
            {
              int num2 = (int) (fixedInt3 + spell.pX);
              int num3 = (int) (fixedInt4 + spell.pY);
              if (spell.game.isClient)
              {
                if (spell.curDuration >= 30)
                  AudioManager.Play(spell.explosionClip);
                else
                  AudioManager.Play(AudioManager.instance.spellBounce);
              }
              int x2 = num2 + spell.zb[index2].x;
              int y2 = num3 + spell.zb[index2].y;
              if (spell.curDuration < 30)
              {
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
                zero.Normalize();
                MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                spell.velocity = spell.velocity * spell.elasticity;
                goto label_56;
              }
              else
              {
                ++spell.timesBounced;
                spell.position = new MyLocation(spell.validX, spell.validY);
                ZCreature zcreature = spell.map.PhysicsCollideCreature((ZCreature) null, x2, y2);
                spell.damage = 25 + Mathf.Clamp(spell.curDuration - spell.lastHit, 0, 7);
                if ((ZComponent) zcreature == (object) null)
                {
                  spell.velocity.x = -spell.velocity.x;
                  spell.velocity.x *= spell.elasticity;
                  spell.velocity.y = (FixedInt) (spell.zb[index2].y > 5 ? 2 : 7);
                }
                else
                {
                  MyLocation zero = MyLocation.zero;
                  for (int index5 = -2; index5 <= 2; ++index5)
                  {
                    for (int index6 = -2; index6 <= 2; ++index6)
                    {
                      if (spell.map.SpellCheckPosition(x2 + index5, y2 + index6, spell.toCollideCheck, Inert.mask_movement_NoEffector))
                      {
                        zero.x += index5;
                        zero.y += index6;
                      }
                    }
                  }
                  zero.Normalize();
                  MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                  spell.velocity.x *= spell.timesBounced % 2 == 1 ? -2 : 2;
                  spell.velocity.x = Mathd.Clamp(spell.velocity.x, (FixedInt) -9, (FixedInt) 9);
                  spell.velocity.y = (FixedInt) (spell.curDuration - spell.lastHit > 2 ? -5 : 5);
                }
                spell.lastHit = spell.curDuration;
                spell.map.ServerBitBlt((int) spell.explosionCutout, (int) spell.validX, spell.map.CheckPositionOnlyMap(x2, y2) ? y2 : (int) spell.validY);
                spell.OnExplosion();
                spell.ApplyExplosionForce(spell.position);
                if ((ZComponent) zcreature != (object) null && spell.timesBounced < 4 && spell.velocity.magnitude < 7)
                {
                  spell.Undie();
                  goto label_59;
                }
                else
                  goto label_56;
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
label_56:
      ++spell.curDuration;
      spell.Undie();
      if (spell.curDuration >= spell.maxDuration || spell.timesBounced >= 4)
      {
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
