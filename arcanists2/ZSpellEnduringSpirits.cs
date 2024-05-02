

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellEnduringSpirits : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellEnduringSpirits spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_44:
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
          goto label_44;
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
              int x2 = num2 + spell.zb[index2].x;
              int y2 = num3 + spell.zb[index2].y;
              if ((ZComponent) spell.map.PhysicsCollideCreature((ZCreature) null, x2, y2) == (object) null)
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
                zero.Normalize();
                MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                spell.velocity = spell.velocity * 838860L;
                if (spell.timesBounced == 1)
                  spell.velocity = spell.velocity * 524288L;
                if (spell.game.isClient && (Object) spell.gameObject != (Object) null)
                {
                  spell.gameObject.GetComponent<SpriteRenderer>().color = ColorHSV.ToColor(new ColorHSV(Random.Range(0.0f, 1f), 1f, 1f));
                  goto label_41;
                }
                else
                  goto label_41;
              }
              else
              {
                spell.position = new MyLocation(spell.validX + fixedInt3 * spell.steps / (FixedInt) 2621440L, spell.validY + fixedInt4 * spell.steps / (FixedInt) 2621440L);
                spell.OnColoredExplosion();
                spell.damage += 2 * (spell.timesBounced > 3 ? 3 : spell.timesBounced);
                spell.MoveSurroundings(spell.EXORADIUS + 5);
                spell.ApplyExplosionForce(spell.position);
                spell.isDead = true;
                spell.isNull = true;
                AudioManager.Play(spell.explosionClip);
                ZComponent.Destroy<GameObject>(spell.gameObject);
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
        spell.isDead = true;
        spell.isNull = true;
        spell.Splash();
        ZComponent.Destroy<GameObject>(spell.gameObject);
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
label_41:
      ++spell.curDuration;
      spell.Undie();
      if (spell.curDuration >= spell.maxDuration || spell.timesBounced >= spell.Bounces)
      {
        spell.OnColoredExplosion();
        spell.ApplyExplosionForce(spell.position);
        spell.isDead = true;
        spell.isNull = true;
        AudioManager.Play(spell.explosionClip);
        ZComponent.Destroy<GameObject>(spell.gameObject);
        break;
      }
      yield return 0.0f;
    }
  }

  public void OnColoredExplosion()
  {
    this.map.ServerBitBlt((int) this.explosionCutout, (int) this.position.x, (int) this.position.y);
    if (!this.game.isClient || this.game.resyncing || !((Object) this.explosion != (Object) null) || !((Object) this.gameObject != (Object) null))
      return;
    ZComponent.Instantiate<GameObject>(this.explosion, this.position.ToSinglePrecision(), Quaternion.identity).GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;
  }
}
