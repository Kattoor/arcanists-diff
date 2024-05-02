// Decompiled with JetBrains decompiler
// Type: ZSpellEntangle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellEntangle : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellEntangle spell = this;
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
      FixedInt y = spell.velocity.y;
      FixedInt fixedInt3 = spell.velocity.x;
      FixedInt fixedInt4 = spell.velocity.y;
      if (x1 > 1 || x1 < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x1) > FixedInt.Abs(y))
          spell.steps = (int) FixedInt.Abs(x1) + 1;
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
              AudioManager.Play(spell.explosionClip);
              int x2 = spell.zb[index2].x;
              int num4 = num2 + x2;
              int num5 = num3 + spell.zb[index2].y;
              if (!spell.map.CheckPositionOnlyMap(num4, num5))
              {
                spell.velocity.y = (FixedInt) 0;
                spell.velocity.x = (FixedInt) 0;
                spell.moving = (IEnumerator<float>) null;
                spell.isMoving = false;
                spell.map.ServerBitBlt((int) spell.explosionCutout, num4, num5);
                spell.ApplyExplosionForce(new MyLocation(num4, num5));
                spell.isDead = true;
                spell.isNull = true;
                if (spell.game.isClient && !spell.game.resyncing)
                  ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) num4, (float) num5), Quaternion.identity, spell.game.GetMapTransform());
                ZComponent.Destroy<GameObject>(spell.gameObject);
                yield break;
              }
              else
              {
                ZCreature zcreature = spell.map.PhysicsCollideCreature(spell.toCollideCheck, num4, num5, Inert.mask_spell_movement);
                if ((ZComponent) zcreature != (object) null && (ZComponent) zcreature.mount != (object) null)
                  zcreature = zcreature.mount;
                if ((ZComponent) zcreature != (object) null && zcreature.race != CreatureRace.Effector && !zcreature.entangled && !ZEffector.InSanctuary(zcreature.game.world, zcreature.position))
                {
                  zcreature.entangledShield = zcreature.shield;
                  zcreature.entangled = true;
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.position = new MyLocation(spell.validX, spell.validY);
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.map.ServerBitBlt((int) spell.explosionCutout, num4, num5);
                  spell.ApplyExplosionForce(new MyLocation(num4, num5));
                  spell.isDead = true;
                  spell.isNull = true;
                  if (spell.game.isClient && (Object) zcreature.transform != (Object) null)
                  {
                    ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) num4, (float) num5), Quaternion.identity, spell.game.GetMapTransform());
                    ZComponent.Instantiate<GameObject>(spell.toSummon, zcreature.transform.position, Quaternion.identity, zcreature.transform).GetComponent<ParticleEntangle>().SetCreature(zcreature.clientObj, spell.spellEnum != SpellEnum.Entangle_Kraken);
                  }
                  ZComponent.Destroy<GameObject>(spell.gameObject);
                  yield break;
                }
                else
                {
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.map.ServerBitBlt((int) spell.explosionCutout, num4, num5);
                  spell.ApplyExplosionForce(new MyLocation(num4, num5));
                  spell.isDead = true;
                  spell.isNull = true;
                  if (spell.game.isClient && !spell.game.resyncing)
                    ZComponent.Instantiate<GameObject>(spell.explosion, new Vector3((float) num4, (float) num5), Quaternion.identity, spell.game.GetMapTransform());
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
        spell.addedVelocity.x = (FixedInt) 0;
        spell.addedVelocity.y = (FixedInt) 0;
      }
      else if (spell.affectedByGravity && spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      else if (!spell.affectedByGravity && spell.velocity.y > -1 && spell.maxDuration > 150)
        spell.affectedByGravity = true;
      spell.Wind();
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.DestroyDelay();
        break;
      }
      if (spell.game.isClient && spell.Rotates && (Object) spell.transform != (Object) null)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
      yield return 0.0f;
    }
  }
}
