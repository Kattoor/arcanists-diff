// Decompiled with JetBrains decompiler
// Type: ZSpellClockworkBomb
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class ZSpellClockworkBomb : ZSpell
{
  public void Activate()
  {
    this.game.forceRysncPause = true;
    ZEffector effector = this.effector;
    effector.game = this.game;
    effector.position = this.position;
    effector.collider = this.collider;
    effector.collider.effector = effector;
    effector.collider.spell = (ZSpell) this;
    this.collider.world = this.game.world;
    this.collider.Enable(this.position);
    if (!((ZComponent) effector.whoSummoned == (object) null) || !((ZComponent) this.parent != (object) null) || this.Bounces > 0)
      return;
    this.Bounces = 1;
    effector.active = false;
    effector.whoSummoned = this.parent;
    this.game.globalEffectors.Add(effector);
  }

  public MyLocation SetPosition
  {
    set
    {
      this.position = value;
      this.collider?.Move(value);
    }
  }

  public ZMyCollider toCollideCheck2()
  {
    if (this.curDuration > 3)
      return this.collider;
    return this.parent?.colliderGameObject;
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellClockworkBomb spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    spell.effector.game = spell.game;
    spell.collider.world = spell.game.world;
    if (spell.curDuration <= 3)
      spell.collider.Disable(false);
    if (gotoStatic)
      yield return 0.0f;
label_50:
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
          spell.SetPosition = new MyLocation(spell.validX, spell.validY);
          yield return 0.0f;
          goto label_50;
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
            if (!spell.map.SpellCheckPositionIgnoreSelf((int) (fixedInt3 + spell.pX) + spell.zb[index2].x, (int) (fixedInt4 + spell.pY) + spell.zb[index2].y, spell.toCollideCheck2(), Inert.mask_spell_movement))
            {
              int num2 = (int) (fixedInt3 + spell.pX);
              int num3 = (int) (fixedInt4 + spell.pY);
              if (!spell.explodeOnImpact || spell.velocity.y > 0)
              {
                spell.SetPosition = new MyLocation(spell.validX, spell.validY);
                MyLocation zero = MyLocation.zero;
                int num4 = num2 + spell.zb[index2].x;
                int num5 = num3 + spell.zb[index2].y;
                for (int index3 = -2; index3 <= 2; ++index3)
                {
                  for (int index4 = -2; index4 <= 2; ++index4)
                  {
                    if (spell.map.SpellCheckPositionIgnoreSelf(num4 + index3, num5 + index4, spell.toCollideCheck2(), Inert.mask_movement_NoEffector))
                    {
                      zero.x += index3;
                      zero.y += index4;
                    }
                  }
                }
                zero.Normalize();
                MyLocation.Reflect(spell.velocity, ref zero, out spell.velocity);
                spell.velocity = spell.velocity * spell.elasticity;
                if (MyLocation.Distance(MyLocation.zero, spell.velocity) <= 1 && !spell.ShouldSpellFall())
                {
                  spell.isMoving = false;
                  spell.moving = (IEnumerator<float>) null;
                  spell.velocity.y = (FixedInt) 0;
                  spell.velocity.x = (FixedInt) 0;
                  spell.Activate();
                  yield break;
                }
                else if (spell.game.isClient)
                {
                  AudioManager.Play(AudioManager.instance.spellBounce);
                  goto label_43;
                }
                else
                  goto label_43;
              }
              else
              {
                spell.SetPosition = new MyLocation(spell.validX, spell.validY);
                spell.moving = (IEnumerator<float>) null;
                spell.isMoving = false;
                spell.velocity.y = (FixedInt) 0;
                spell.velocity.x = (FixedInt) 0;
                spell.Activate();
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
      MyLocation position = spell.position;
      spell.SetPosition = spell.position + spell.velocity;
      spell.game.CreatureMoveSurroundings(position, spell.radius);
      if (spell.position.y < spell.radius)
      {
        spell.moving = (IEnumerator<float>) null;
        spell.isMoving = false;
        spell.velocity = MyLocation.zero;
        spell.Splash();
        spell.effector?.SetNull();
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
      else if (spell.velocity.y > -ZMap.MaxSpeed)
        spell.velocity.y += spell.map.Gravity;
      spell.Wind();
label_43:
      ++spell.curDuration;
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.Explode();
        spell.ApplyExplosionForce(spell.position + spell.addedDeathVector);
        spell.effector?.SetNull();
        spell.DestroyDelay();
        break;
      }
      if (spell.curDuration > 150)
        spell.explodeOnImpact = true;
      yield return 0.0f;
    }
  }
}
