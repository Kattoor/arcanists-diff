// Decompiled with JetBrains decompiler
// Type: ZSpellCallingBell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellCallingBell : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellCallingBell spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_47:
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
          goto label_47;
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
              int x2 = spell.zb[index2].x;
              int x3 = num2 + x2;
              int y2 = num3 + spell.zb[index2].y;
              if (!spell.map.CheckPositionOnlyMap(x3, y2))
              {
                spell.isDead = true;
                spell.isNull = true;
                if (spell.game.isClient && !spell.game.resyncing)
                  ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) x3, (float) y2), Quaternion.identity, spell.game.GetMapTransform());
                ZComponent.Destroy<GameObject>(spell.gameObject);
                yield break;
              }
              else
              {
                ZCreature c = spell.map.PhysicsCollideCreature(spell.toCollideCheck, x3, y2);
                if ((ZComponent) c != (object) null && c.race != CreatureRace.Effector && c.type != CreatureType.Recall_Device)
                {
                  while (spell.game.recallDevices.Count > 0)
                  {
                    int index3 = spell.game.RandomInt(0, spell.game.recallDevices.Count);
                    ZCreature recallDevice = spell.game.recallDevices[index3];
                    if (!ZComponent.IsNull((ZComponent) recallDevice))
                    {
                      spell.game.recallDevices.RemoveAt(index3);
                      recallDevice.OnDeath(true);
                      if ((ZComponent) c != (object) null)
                      {
                        ZSpell.FireTeleportNoDetower(spell.baseSpell, c, recallDevice.position);
                        break;
                      }
                      break;
                    }
                    spell.game.recallDevices.RemoveAt(index3);
                  }
                  spell.isDead = true;
                  spell.isNull = true;
                  if (spell.game.isClient && !spell.game.resyncing)
                    ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) x3, (float) y2), Quaternion.identity, spell.game.GetMapTransform());
                  ZComponent.Destroy<GameObject>(spell.gameObject);
                  yield break;
                }
                else
                {
                  spell.isDead = true;
                  spell.isNull = true;
                  if (spell.game.isClient && !spell.game.resyncing)
                    ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) x3, (float) y2), Quaternion.identity, spell.game.GetMapTransform());
                  ZComponent.Destroy<GameObject>(spell.gameObject);
                  yield break;
                }
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
      if (spell.Rotates)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
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
