

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellThornBall : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellThornBall spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (gotoStatic)
      yield return 0.0f;
label_42:
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
          goto label_42;
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
              int num4 = num2 + spell.zb[index2].x;
              int num5 = num3 + spell.zb[index2].y;
              if (!spell.map.CheckPositionOnlyMap(num4, num5))
              {
                spell.velocity.y = (FixedInt) 0;
                spell.velocity.x = (FixedInt) 0;
                spell.moving = (IEnumerator<float>) null;
                spell.isMoving = false;
                if (!ZEffector.InSanctuary(spell.game.world, new MyLocation(num4, num5)))
                {
                  ZCreatureThorn thorn = ZCreatureCreate.CreateThorn(spell.game, spell.parent?.parent, spell.toSummon.GetComponent<CreatureThorn>(), (Vector2) new Vector3((float) num4, (float) num5), Quaternion.identity, spell.game.GetMapTransform());
                  thorn.game = spell.game;
                  thorn.spell.game = spell.game;
                  thorn.spell.parent = (ZComponent) spell.parent != (object) null ? spell.parent.parent?.first() : spell.parent;
                  thorn.collider.world = spell.world;
                  thorn.collider.Move(new MyLocation(num4, num5));
                  thorn.spell.position = new MyLocation(num4, num5);
                  thorn.position = new MyLocation(num4, num5);
                  thorn.parent = spell.parent?.parent;
                  spell.game.thorns.Add(thorn);
                  if (spell.game.isClient)
                    AudioManager.Play(AudioManager.instance.pop);
                }
                else
                  ZEffector.SpawnVineExplosion(new Vector3((float) num4, (float) num5));
                spell.isDead = true;
                spell.isNull = true;
                ZComponent.Destroy<GameObject>(spell.gameObject);
                yield break;
              }
              else
              {
                ZCreature zcreature = spell.map.PhysicsCollideCreature(spell.toCollideCheck, num4, num5);
                if ((ZComponent) zcreature != (object) null && zcreature.GetType() != typeof (ZCreatureThorn))
                {
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.position = new MyLocation(num2 + spell.zb[index2].x, num3 + spell.zb[index2].y);
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  spell.map.ServerBitBlt((int) spell.explosionCutout, num4, num5);
                  spell.OnExplosion();
                  spell.OnDeath(true);
                  yield break;
                }
                else
                {
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.moving = (IEnumerator<float>) null;
                  spell.isMoving = false;
                  if (!ZEffector.InSanctuary(spell.game.world, new MyLocation(num4, num5)))
                  {
                    ZCreatureThorn thorn = ZCreatureCreate.CreateThorn(spell.game, spell.parent?.parent, spell.toSummon.GetComponent<CreatureThorn>(), (Vector2) new Vector3((float) num4, (float) num5), Quaternion.identity, spell.game.GetMapTransform());
                    thorn.game = spell.game;
                    thorn.spell.game = spell.game;
                    thorn.spell.parent = spell.parent;
                    thorn.collider.world = spell.world;
                    thorn.collider.Move(new MyLocation(num4, num5));
                    thorn.spell.position = new MyLocation(num4, num5);
                    thorn.position = new MyLocation(num4, num5);
                    thorn.parent = spell.parent?.parent;
                    spell.game.thorns.Add(thorn);
                    if (spell.game.isClient)
                      AudioManager.Play(AudioManager.instance.pop);
                  }
                  else
                    ZEffector.SpawnVineExplosion(new Vector3((float) num4, (float) num5));
                  spell.isDead = true;
                  spell.isNull = true;
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
