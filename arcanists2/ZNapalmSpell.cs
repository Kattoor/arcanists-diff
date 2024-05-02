

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZNapalmSpell : ZSpell
{
  public override void OnDeath(bool playDeathClip = true)
  {
    this.game.forceRysncPause = true;
    if (ZComponent.IsNull((ZComponent) this))
      return;
    this.isDead = true;
    this.isNull = true;
    AudioManager.PlayNapalm(this.explosionClip);
    ZMyCollider collider = this.collider;
    if ((ZComponent) collider != (object) null && collider.enabled)
      collider.Disable();
    this.DestroyDelay();
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZNapalmSpell spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    ZMyCollider col = spell.collider;
    col.Disable(false);
    col.world = spell.world;
    col.spell = (ZSpell) spell;
    ZEffector effector = spell.effector;
    effector.position = spell.position;
    effector.collider = col;
    col.effector = effector;
    if (gotoStatic)
      yield return 0.0f;
label_49:
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
          goto label_49;
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
            if (!spell.map.SpellCheckPosition(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y, spell.toCollideCheck, Inert.mask_spell_movement))
            {
              int x2 = num2 + spell.zb[index2].x;
              int y2 = num3 + spell.zb[index2].y;
              if (!spell.map.CheckPositionOnlyMap(x2, y2))
              {
                if (!spell.explodeOnImpact)
                {
                  AudioManager.PlayNapalm(spell.explosionClip);
                  spell.explodeOnImpact = true;
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
                  spell.map.ServerBitBlt((int) spell.explosionCutout, x2, y2);
                  spell.MoveSurroundings(spell.EXORADIUS + 5);
                  if (spell.game.isClient && (Object) spell.transform != (Object) null)
                  {
                    spell.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    spell.gameObject.GetComponent<ParticleSystem>().Play();
                  }
                  spell.EXORADIUS += 12;
                  if (spell.ApplyExplosionForce(spell.position))
                  {
                    spell.velocity.y = (FixedInt) 0;
                    spell.velocity.x = (FixedInt) 0;
                    spell.position = new MyLocation(spell.validX, spell.validY);
                    spell.moving = (IEnumerator<float>) null;
                    spell.isMoving = false;
                    spell.OnDeath(true);
                    yield break;
                  }
                  else
                  {
                    spell.EXORADIUS -= 12;
                    spell.Undie();
                    goto label_46;
                  }
                }
                else if (spell.ShouldSpellFall())
                {
                  spell.velocity.y = (FixedInt) -1;
                  spell.velocity.x = (FixedInt) 0;
                }
                else
                {
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.position = new MyLocation(spell.validX, spell.validY);
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  col.Enable(spell.position);
                  spell.explisiveForce = (FixedInt) 5;
                  spell.game.forceRysncPause = true;
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
                spell.map.ServerBitBlt(0, x2, y2);
                spell.ApplyExplosionForce(spell.position);
                spell.OnDeath(true);
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
label_46:
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
