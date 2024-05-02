

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellConductorRod : ZSpell
{
  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellConductorRod spell = this;
    spell.isMoving = true;
    spell.zb = MapGenerator.getOutlineArray(spell.radius);
    if (spell.spellEnum == SpellEnum.Conductor_Rod && (ZComponent) spell.parent != (object) null && spell.parent.familiarLevelStorm > 0)
      spell.clientObj.GetComponent<SpriteRenderer>().sprite = ClientResources.Instance.paperClip;
    if (gotoStatic)
      yield return 0.0f;
label_54:
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
          goto label_54;
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
              ++spell.timesBounced;
              spell.position = new MyLocation(spell.validX, spell.validY);
              int num4 = num2 + spell.zb[index2].x;
              int num5 = num3 + spell.zb[index2].y;
              AudioManager.Play(spell.explosionClip);
              spell.hitCreature = spell.map.PhysicsCollideCreature((ZCreature) null, x2, y2);
              goto label_29;
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
label_29:
      if (spell.Rotates && (Object) spell.transform != (Object) null)
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(spell.velocity.y.ToFloat(), spell.velocity.x.ToFloat()) * 57.29578f);
      ++spell.curDuration;
      spell.Undie();
      if (spell.timesBounced >= 1)
      {
        if (spell.spellEnum == SpellEnum.Conductor_Rod)
        {
          spell.transform?.GetChild(1).gameObject.SetActive(true);
          int max = spell.parent.familiar.Has(FamiliarType.Storm) ? (1 + spell.parent.familiarLevelStorm) / 2 + 5 : 5;
          int num = 0;
          int amount = Mathf.Clamp(((int) FixedInt.Abs(spell.position.x - spell.target.x) + num) / 52, 3, max);
          spell.game.ongoing.RunSpell(ZSpell.IEnumeratorLightningStrike(spell.parent, new MyLocation(spell.position.x, (FixedInt) spell.map.Height), -30, 30, amount, true, (int) spell.position.x, (int) spell.position.y, spell.hitCreature));
        }
        else if (spell.spellEnum == SpellEnum.Collision_Course)
          ZSpell.BaseFire(Inert.GetSpell(SpellEnum.Starfire), spell.parent, new MyLocation(spell.position.x, (FixedInt) (spell.parent.game.map.Height + 1000)), Quaternion.identity, new MyLocation(0, -40));
        if (spell.game.AllowExpansion && (ZComponent) spell.parent != (object) null || spell.spellEnum == SpellEnum.Collision_Course)
        {
          ZEffector effector = spell.effector;
          effector.game = spell.parent.game;
          effector.whoSummoned = spell.parent;
          effector.followParent = false;
          effector.position = spell.position;
          effector.active = false;
          spell.parent.effectors.Add(effector);
          spell.game.forceRysncPause = true;
          if (spell.spellEnum == SpellEnum.Electrostatic_Charge)
          {
            if (spell.game.isClient)
              spell.transform?.GetChild(0).gameObject.SetActive(true);
            spell.game.staticCharge.Add(effector);
            effector.active = true;
            effector.collider.effector = effector;
            effector.collider.world = effector.world;
            effector.collider.Move(spell.position);
            effector.CheckInitialSpawn_Creatures(effector.collider.radius);
            break;
          }
          spell.parent = spell.hitCreature;
          if (!((ZComponent) spell.hitCreature != (object) null))
            break;
          Transform transform = spell.transform;
          if (transform == null)
            break;
          transform.SetParent(spell.hitCreature.transform);
          break;
        }
        spell.DestroyConductorRodDelay();
        break;
      }
      if (spell.curDuration >= spell.maxDuration)
      {
        spell.DestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }

  protected void DestroyConductorRodDelay()
  {
    this.isDead = true;
    this.isNull = true;
    ZComponent.Destroy<GameObject>(this.gameObject, 2f);
    if (this.game.isClient)
    {
      ParticleSystem[] componentsInChildren = this.gameObject?.GetComponentsInChildren<ParticleSystem>();
      if (componentsInChildren != null)
      {
        foreach (ParticleSystem particleSystem in componentsInChildren)
          particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
      }
    }
    this.effector.isNull = true;
  }
}
