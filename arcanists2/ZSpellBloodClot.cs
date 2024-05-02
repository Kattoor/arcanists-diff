

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellBloodClot : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellBloodClot spell = this;
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
              AudioManager.Play(spell.explosionClip);
              int x2 = num2 + spell.zb[index2].x;
              int y2 = num3 + spell.zb[index2].y;
              ++spell.timesBounced;
              spell.target = new MyLocation((FixedInt) x2 + spell.velocity.x, (FixedInt) y2 + spell.velocity.y);
              ZCreature zcreature = spell.map.PhysicsCollideCreature((ZCreature) null, x2, y2);
              if (((ZComponent) zcreature == (object) null || (ZComponent) spell.parent != (object) null && zcreature.parent.team == spell.parent.parent.team) && spell.timesBounced < spell.Bounces)
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
                if ((ZComponent) zcreature != (object) null)
                {
                  spell.velocity.Normalize();
                  spell.velocity = spell.velocity * 20;
                  goto label_44;
                }
                else
                {
                  spell.velocity = spell.velocity * spell.elasticity;
                  goto label_44;
                }
              }
              else
              {
                if ((ZComponent) zcreature == (object) null || (ZComponent) spell.parent != (object) null && zcreature.parent.team != spell.parent.parent.team)
                {
                  spell.position = new MyLocation(spell.validX + fixedInt3 * spell.steps / (FixedInt) 2621440L, spell.validY + fixedInt4 * spell.steps / (FixedInt) 2621440L);
                  spell.map.ServerBitBltBrightness(46, (int) spell.position.x, (int) spell.position.y, (FixedInt) (spell.game.RandomFixedInt(50, 150) / 100).ToFloat(), false);
                  spell.OnExplosion();
                  spell.ApplyExplosionForce(spell.position);
                }
                spell.DestroyDelay();
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
      if (spell.timesBounced > 0)
        spell.RotateToward();
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
label_44:
      ++spell.curDuration;
      spell.Undie();
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }

  public void ElectrostaticInterference()
  {
    List<ZMyCollider> list = this.game.world.OverlapCircleAll((Point) this.position, 512, (ZMyCollider) null, 512);
    ZSpell kablam = (ZSpell) null;
    for (int index = 0; index < list.Count; ++index)
    {
      if ((ZComponent) list[index] != (object) null && (ZComponent) list[index].effector != (object) null && list[index].effector.type == EffectorType.Electrostatic_Charge)
      {
        if ((ZComponent) kablam == (object) null)
          kablam = ZSpell.BaseFireKablam(Inert.Instance.spells["Thunder Shock"], this.parent, this.position, Quaternion.identity);
        Coords start = new Coords((int) this.position.x, (int) this.position.y);
        Coords end = new Coords((int) list[index].position.x, (int) list[index].position.y);
        Coords coords = this.parent.map.bresenhamsLineCast(start, end, this.parent, (ZSpell) null, Inert.mask_spell_movement);
        if (coords != null)
        {
          this.parent.map.ServerBitBlt((int) this.explosionCutout, coords.x, coords.y);
          kablam.ApplyExplosionForce(coords.ToMyLocation());
          if (this.parent.game.isClient && !this.parent.game.resyncing)
            this.parent.game.electricityPool.Spawn(start.ToVector(), coords.ToVector());
        }
        else
        {
          this.ChainLink(list[index].position, kablam);
          if (this.parent.game.isClient && !this.parent.game.resyncing)
            this.parent.game.electricityPool.Spawn(start.ToVector(), (Vector3) list[index].position.ToSinglePrecision());
        }
        kablam.Undie();
      }
    }
    this.game.world.listPool.ReturnList(list);
    if (!((Object) kablam?.gameObject != (Object) null))
      return;
    ZComponent.Destroy<GameObject>(kablam.gameObject);
  }

  public void ChainLink(MyLocation position, ZSpell kablam)
  {
    List<ZMyCollider> list = this.game.world.OverlapCircleAll((Point) position, 256, (ZMyCollider) null, 256);
    for (int index = 0; index < list.Count; ++index)
    {
      if (!ZComponent.IsNull((ZComponent) list[index]) && !ZComponent.IsNull((ZComponent) list[index].creature) && list[index].creature.race != CreatureRace.Effector)
      {
        Coords start = new Coords((int) position.x, (int) position.y);
        Coords end = new Coords((int) list[index].position.x, (int) list[index].position.y);
        Coords coords = this.parent.map.bresenhamsLineCast(start, end, this.parent, (ZSpell) null, Inert.mask_spell_movement);
        if (coords != null)
        {
          this.parent.map.ServerBitBlt((int) this.explosionCutout, coords.x, coords.y);
          kablam.ApplyExplosionForce(coords.ToMyLocation());
          if (this.parent.game.isClient && !this.parent.game.resyncing)
            this.parent.game.electricityPool.Spawn(start.ToVector(), coords.ToVector());
        }
        else if (this.parent.game.isClient && !this.parent.game.resyncing)
          this.parent.game.electricityPool.Spawn(start.ToVector(), (Vector3) list[index].position.ToSinglePrecision());
        kablam.Undie();
      }
    }
    this.game.world.listPool.ReturnList(list);
  }
}
