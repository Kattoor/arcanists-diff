// Decompiled with JetBrains decompiler
// Type: ZSpellMeteor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellMeteor : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellMeteor spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    MyLocation velocity = spell.velocity;
    if (gotoStatic)
      yield return 0.0f;
label_43:
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
      bool flag1 = false;
      bool flag2 = false;
      while (spell.steps > 0)
      {
        if (spell.map.SpellCheckEffectors(spell.toCollideCheck, (ZSpell) spell, (int) spell.validX, (int) spell.validY))
        {
          spell.position = new MyLocation(spell.validX, spell.validY);
          yield return 0.0f;
          goto label_43;
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
              if (!flag1 || !spell.map.CheckPositionOnlyMap(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y))
              {
                int x2 = num2 + spell.zb[index2].x;
                int y2 = num3 + spell.zb[index2].y;
                ++spell.timesBounced;
                spell.map.ServerBitBlt((int) spell.explosionCutout, (int) spell.validX, (int) spell.validY);
                spell.OnExplosion();
                spell.MoveSurroundings(spell.EXORADIUS + 5);
                spell.ApplyExplosionForce(spell.position);
                if ((Object) spell.toSummon != (Object) null && spell.spellEnum != SpellEnum.Arcane_Meteor)
                {
                  Spell component = spell.toSummon.GetComponent<Spell>();
                  for (int index3 = 0; index3 < 4; ++index3)
                  {
                    MyLocation power = Inert.Velocity(spell.game.RandomFixedInt(0, 360), 15);
                    ZSpell.BaseFire(component, spell.parent, spell.position, Quaternion.identity, power);
                  }
                }
                if (spell.spellEnum == SpellEnum.Fire_Cannon)
                {
                  ZCreature zcreature = spell.map.PhysicsCollideCreature((ZCreature) null, x2, y2);
                  if ((ZComponent) zcreature != (object) null && (ZComponent) zcreature.tower != (object) null)
                    flag2 = true;
                }
                AudioManager.Play(spell.explosionClip);
              }
              flag1 = true;
            }
          }
          spell.validX = spell.pX;
          spell.validY = spell.pY;
          spell.pX = spell.pX + fixedInt3;
          spell.pY = spell.pY + fixedInt4;
        }
      }
      if (flag2)
        spell.position = spell.position + spell.velocity * 419430L;
      else
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
      ++spell.curDuration;
      spell.Undie();
      if (spell.curDuration >= spell.maxDuration || spell.timesBounced >= spell.amount)
      {
        spell.Explode();
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }
}
