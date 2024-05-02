

using System;
using System.Collections.Generic;

#nullable disable
public class ZFlameWallSpell : ZSpell
{
  public ZMyCollider colliderA => this.collider;

  public override void SetPosition(MyLocation p)
  {
    this.position = p;
    if ((ZComponent) this.colliderA != (object) null)
      this.colliderA.Move(p);
    if ((ZComponent) this.colliderB != (object) null)
      this.colliderB.Move(p);
    if (this.spellEnum != SpellEnum.Prickly_Barrier || !((ZComponent) this.effector2 != (object) null) || !this.effector2.active)
      return;
    List<ZMyCollider> zmyColliderList = this.world.OverlapCircleAll((Point) p, this.effector2.collider.radius, (ZMyCollider) null, Inert.mask_movement_NoEffector | Inert.mask_Phantom);
    if (zmyColliderList.Count > 1)
      zmyColliderList.Sort((Comparison<ZMyCollider>) ((a, b) => MyLocation.FastDistance(a.position, this.position) - MyLocation.FastDistance(b.position, this.position)));
    foreach (ZMyCollider zmyCollider in zmyColliderList)
      this.effector2.EffectCreature((ZComponent) zmyCollider.tower != (object) null ? zmyCollider.tower.creature : zmyCollider.creature, true);
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZFlameWallSpell zflameWallSpell = this;
    zflameWallSpell.isMoving = true;
    if (zflameWallSpell.zb == null)
      zflameWallSpell.zb = MapGenerator.getOutlineArray(zflameWallSpell.radius);
    if (gotoStatic)
      yield return 0.0f;
    while (!zflameWallSpell.isDead)
    {
      zflameWallSpell.pX = zflameWallSpell.position.x;
      zflameWallSpell.pY = zflameWallSpell.position.y;
      FixedInt fixedInt1 = zflameWallSpell.position.x + zflameWallSpell.velocity.x;
      FixedInt fixedInt2 = zflameWallSpell.position.y + zflameWallSpell.velocity.y;
      zflameWallSpell.validX = zflameWallSpell.pX;
      zflameWallSpell.validY = zflameWallSpell.pY;
      zflameWallSpell.steps = 1;
      FixedInt x1 = zflameWallSpell.velocity.x;
      FixedInt y1 = zflameWallSpell.velocity.y;
      FixedInt fixedInt3 = zflameWallSpell.velocity.x;
      FixedInt fixedInt4 = zflameWallSpell.velocity.y;
      if (x1 > 1 || x1 < -1 || y1 > 1 || y1 < -1)
      {
        if (FixedInt.Abs(x1) > FixedInt.Abs(y1))
          zflameWallSpell.steps = (int) FixedInt.Abs(x1) + 1;
        else
          zflameWallSpell.steps = (int) FixedInt.Abs(y1) + 1;
        fixedInt3 = zflameWallSpell.velocity.x / zflameWallSpell.steps;
        fixedInt4 = zflameWallSpell.velocity.y / zflameWallSpell.steps;
      }
      while (zflameWallSpell.steps > 0)
      {
        --zflameWallSpell.steps;
        int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zflameWallSpell.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zflameWallSpell.zb.Count) - zflameWallSpell.radius;
        if (num1 < 0)
          num1 += zflameWallSpell.zb.Count;
        for (int index1 = 0; index1 < zflameWallSpell.radius * 2; ++index1)
        {
          int index2 = (index1 + num1) % zflameWallSpell.zb.Count;
          if (!zflameWallSpell.map.CheckPosition((int) (fixedInt3 + zflameWallSpell.pX) + zflameWallSpell.zb[index2].x, (int) (fixedInt4 + zflameWallSpell.pY) + zflameWallSpell.zb[index2].y, (ZCreature) null, Inert.mask_movement_NoEffector | (zflameWallSpell.hitPhantom ? Inert.mask_Phantom : 0)))
          {
            int num2 = (int) (fixedInt3 + zflameWallSpell.pX);
            int num3 = (int) (fixedInt4 + zflameWallSpell.pY);
            int x2 = num2 + zflameWallSpell.zb[index2].x;
            int y2 = num3 + zflameWallSpell.zb[index2].y;
            if (zflameWallSpell.map.CheckPositionOnlyMap(x2, y2) && !zflameWallSpell.explodeOnImpact)
            {
              zflameWallSpell.SetPosition(new MyLocation(zflameWallSpell.validX, zflameWallSpell.validY));
              MyLocation zero = MyLocation.zero;
              int num4 = num2 + zflameWallSpell.zb[index2].x;
              int num5 = num3 + zflameWallSpell.zb[index2].y;
              for (int index3 = -2; index3 <= 2; ++index3)
              {
                for (int index4 = -2; index4 <= 2; ++index4)
                {
                  if (zflameWallSpell.map.CheckPosition(num4 + index3, num5 + index4, (ZCreature) null, Inert.mask_movement_NoEffector))
                  {
                    zero.x += index3;
                    zero.y += index4;
                  }
                }
              }
              if (zflameWallSpell.spellEnum == SpellEnum.Flame_Wall)
                zflameWallSpell.effector.EffectCreature((ZCreature) null);
            }
            zflameWallSpell.velocity.y = (FixedInt) 0;
            zflameWallSpell.velocity.x = (FixedInt) 0;
            zflameWallSpell.SetPosition(new MyLocation(zflameWallSpell.validX, zflameWallSpell.validY));
            zflameWallSpell.moving = (IEnumerator<float>) null;
            zflameWallSpell.isMoving = false;
            if (!zflameWallSpell.ShouldSpellFall())
            {
              yield break;
            }
            else
            {
              zflameWallSpell.moving = zflameWallSpell.game.ongoing.RunSpell(zflameWallSpell.SpellMove(false, true));
              yield break;
            }
          }
        }
        zflameWallSpell.validX = zflameWallSpell.pX;
        zflameWallSpell.validY = zflameWallSpell.pY;
        zflameWallSpell.pX = zflameWallSpell.pX + fixedInt3;
        zflameWallSpell.pY = zflameWallSpell.pY + fixedInt4;
      }
      zflameWallSpell.SetPosition(zflameWallSpell.position + zflameWallSpell.velocity);
      if (zflameWallSpell.position.y < zflameWallSpell.radius)
      {
        if (zflameWallSpell.spellEnum == SpellEnum.Nature_s_Wrath)
        {
          zflameWallSpell.velocity.y = (FixedInt) 0;
          zflameWallSpell.velocity.x = (FixedInt) 0;
          zflameWallSpell.moving = (IEnumerator<float>) null;
          zflameWallSpell.isMoving = false;
          zflameWallSpell.SetPosition(new MyLocation(zflameWallSpell.position.x, (FixedInt) 0));
          break;
        }
        zflameWallSpell.moving = (IEnumerator<float>) null;
        zflameWallSpell.velocity = MyLocation.zero;
        zflameWallSpell.isMoving = false;
        zflameWallSpell.MyDestroyDelay();
        break;
      }
      if (zflameWallSpell.addVelocity)
      {
        zflameWallSpell.addVelocity = false;
        zflameWallSpell.velocity = zflameWallSpell.velocity + zflameWallSpell.addedVelocity;
        zflameWallSpell.velocity.x = Mathd.Clamp(zflameWallSpell.velocity.x, (FixedInt) -50, (FixedInt) 50);
        zflameWallSpell.velocity.y = Mathd.Clamp(zflameWallSpell.velocity.y, (FixedInt) -50, (FixedInt) 50);
        zflameWallSpell.addedVelocity.x = (FixedInt) 0;
        zflameWallSpell.addedVelocity.y = (FixedInt) 0;
      }
      if (zflameWallSpell.affectedByGravity && zflameWallSpell.velocity.y > -ZMap.MaxSpeed)
        zflameWallSpell.velocity.y += zflameWallSpell.map.Gravity;
      else if (!zflameWallSpell.affectedByGravity && zflameWallSpell.velocity.y > -10 && zflameWallSpell.maxDuration > 150)
        zflameWallSpell.affectedByGravity = true;
      zflameWallSpell.Wind();
      ++zflameWallSpell.curDuration;
      if (zflameWallSpell.curDuration >= zflameWallSpell.maxDuration)
      {
        zflameWallSpell.MyDestroyDelay();
        break;
      }
      yield return 0.0f;
    }
  }

  public void MyDestroyDelay()
  {
    this.isNull = true;
    this.isDead = true;
    this.colliderA?.Disable();
    this.colliderB?.Disable();
    this.DestroyDelay();
  }
}
