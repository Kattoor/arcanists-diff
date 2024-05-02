

using System.Collections.Generic;

#nullable disable
public class ZSpellClam : ZSpell
{
  private FixedInt minY = (FixedInt) 100000;

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellClam spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    bool locked = false;
    int bounces = 0;
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
      FixedInt x = spell.velocity.x;
      FixedInt y = spell.velocity.y;
      FixedInt fixedInt3 = spell.velocity.x;
      FixedInt fixedInt4 = spell.velocity.y;
      if (x > 1 || x < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x) > FixedInt.Abs(y))
          spell.steps = (int) FixedInt.Abs(x) + 1;
        else
          spell.steps = (int) FixedInt.Abs(y) + 1;
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
              if (spell.position.y < spell.minY || bounces < 5 && spell.map.SpellCheckPositionEntities(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y, spell.toCollideCheck, Inert.mask_spell_movement))
              {
                ++bounces;
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
                if (locked)
                {
                  spell.velocity.y = (FixedInt) 6;
                  spell.velocity.x = (FixedInt) 0;
                  spell.minY = spell.position.y - 15;
                }
                else
                {
                  spell.affectedByGravity = true;
                  zero.Normalize();
                  MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                  spell.velocity = spell.velocity * spell.elasticity;
                  locked = true;
                }
                spell.map.ServerBitBlt(18, (int) spell.validX, (int) spell.validY);
                spell.MoveSurroundings(25);
                if (spell.game.isClient)
                {
                  spell.gameObject.GetComponent<AnimateRepeat>().enabled = true;
                  goto label_41;
                }
                else
                  goto label_41;
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
        spell.addedVelocity.x = (FixedInt) 0;
        spell.addedVelocity.y = (FixedInt) 0;
      }
      else if (spell.affectedByGravity && spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      else if (!spell.affectedByGravity && spell.velocity.y > -1 && spell.maxDuration > 150)
        spell.affectedByGravity = true;
      spell.Wind();
label_41:
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.OnDeath(true);
        break;
      }
      yield return 0.0f;
    }
  }
}
