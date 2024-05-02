

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellMechanicalArrow : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellMechanicalArrow spell = this;
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
              int xInt = (int) (fixedInt3 + spell.pX);
              int num2 = (int) (fixedInt4 + spell.pY);
              spell.position = new MyLocation(xInt, num2 + spell.zb[index2].y);
              spell.moving = (IEnumerator<float>) null;
              spell.isMoving = false;
              if (spell.spellEnum == SpellEnum.Mechanical_Arrow && spell.curDuration > 120)
                spell.damage -= spell.curDuration - 120;
              spell.OnDeath(true);
              spell.velocity.y = (FixedInt) 0;
              spell.velocity.x = (FixedInt) 0;
              yield break;
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
        spell.isMoving = false;
        if (spell.spellEnum == SpellEnum.Water_Ball)
        {
          spell.position = new MyLocation(spell.position.x, (FixedInt) spell.radius);
          spell.velocity.y = -spell.velocity.y;
          spell.OnDeath(true);
          break;
        }
        spell.velocity = MyLocation.zero;
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
      if (spell.goToTarget)
      {
        if (spell.curDuration >= spell.maxTargetFrames)
          spell.goToTarget = false;
        else
          spell.RotateToward();
      }
      else if (spell.affectedByGravity && spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      else if (!spell.affectedByGravity && spell.velocity.y > -1 && spell.maxDuration > 150)
        spell.affectedByGravity = true;
      spell.Wind();
      if (spell.Rotates && (Object) spell.transform != (Object) null)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        if (spell.game.isClient)
          AudioManager.Play(AudioManager.instance.mechArrowExplode);
        spell.OnExplosionMechanicalArrow();
        spell.DestroyDelay();
        break;
      }
      if (spell.curDuration % 30 == 0)
        ZSpell.BaseFire(spell.toSummon.GetComponent<Spell>(), spell.parent, spell.position, Quaternion.identity, new MyLocation(0, -5));
      yield return 0.0f;
    }
  }
}
