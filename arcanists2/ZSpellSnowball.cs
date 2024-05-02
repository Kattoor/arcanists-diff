// Decompiled with JetBrains decompiler
// Type: ZSpellSnowball
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellSnowball : ZSpell
{
  public void Blit()
  {
    if (this.spellEnum != SpellEnum.Brine_Bolt && this.spellEnum != SpellEnum.Verdant_Javelin && this.position.y >= this.map.Height - 150)
    {
      this.OnExplosion();
    }
    else
    {
      if (!((ZComponent) this.map.PhysicsCollideCreatureCircle((ZCreature) null, (int) this.position.x, (int) this.position.y, 2, 256) == (object) null))
        return;
      if (this.spellEnum == SpellEnum.Verdant_Javelin)
      {
        Color32[] pixels32;
        if (!ZMap.cachedPixels.TryGetValue(this.snowTexture, out pixels32))
        {
          pixels32 = this.snowTexture.GetPixels32();
          ZMap.cachedPixels.Add(this.snowTexture, pixels32);
        }
        Surface surface = new Surface(pixels32, this.snowTexture.width, this.snowTexture.height);
        if (this.game.isClient && !this.game.resyncing && (Object) this.explosion != (Object) null)
          ZComponent.Instantiate<GameObject>(this.explosion, this.position.ToSinglePrecision(), Quaternion.identity).transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        this.map.ServerBitBltRotate((int) this.snowCutout, (int) this.position.x, (int) this.position.y - surface.height / 2, (FixedInt) 360 - FixedInt.Atan2(this.velocity.y, this.velocity.x) * FixedInt.Rad2Deg, false);
      }
      else
      {
        this.map.ServerBitBlt((int) this.snowCutout, (int) this.position.x, (int) this.position.y, false);
        ZSpell.RemoveItemsOnBitBlt(this.game, (int) this.position.x, (int) this.position.y, this.snowTexture.width / 2 - 1);
      }
    }
  }

  public override void OnDeath(bool playDeathClip = true)
  {
    if (ZComponent.IsNull((ZComponent) this))
      return;
    this.Blit();
    this.isDead = true;
    this.isNull = true;
    if ((Object) this.transform == (Object) null)
      return;
    AudioManager.Play(this.explosionClip);
    ZComponent.Destroy<GameObject>(this.gameObject);
  }

  public override IEnumerator<float> SpellMove(bool gotoStatic = false, bool checkEffectors = true)
  {
    ZSpellSnowball zspellSnowball = this;
    zspellSnowball.isMoving = true;
    zspellSnowball.zb = MapGenerator.getOutlineArray(zspellSnowball.radius);
    if (gotoStatic)
      yield return 0.0f;
label_2:
    int collisionFrame = zspellSnowball.damageType != DamageType.Snow ? (zspellSnowball.spellEnum == SpellEnum.Verdant_Javelin ? -1 : 0) : 2;
    while (!zspellSnowball.isDead)
    {
      if (zspellSnowball.curDuration == 10 && !zspellSnowball.affectedByGravity)
        zspellSnowball.velocity.x = (FixedInt) 0;
      zspellSnowball.pX = zspellSnowball.position.x;
      zspellSnowball.pY = zspellSnowball.position.y;
      FixedInt fixedInt1 = zspellSnowball.position.x + zspellSnowball.velocity.x;
      FixedInt fixedInt2 = zspellSnowball.position.y + zspellSnowball.velocity.y;
      zspellSnowball.validX = zspellSnowball.pX;
      zspellSnowball.validY = zspellSnowball.pY;
      zspellSnowball.steps = 1;
      FixedInt x = zspellSnowball.velocity.x;
      FixedInt y = zspellSnowball.velocity.y;
      FixedInt fixedInt3 = zspellSnowball.velocity.x;
      FixedInt fixedInt4 = zspellSnowball.velocity.y;
      if (x > 1 || x < -1 || y > 1 || y < -1)
      {
        if (FixedInt.Abs(x) > FixedInt.Abs(y))
          zspellSnowball.steps = (int) FixedInt.Abs(x) + 1;
        else
          zspellSnowball.steps = (int) FixedInt.Abs(y) + 1;
        fixedInt3 = zspellSnowball.velocity.x / zspellSnowball.steps;
        fixedInt4 = zspellSnowball.velocity.y / zspellSnowball.steps;
      }
      while (zspellSnowball.steps > 0)
      {
        if (zspellSnowball.map.SpellCheckEffectors(zspellSnowball.toCollideCheck, (ZSpell) zspellSnowball, (int) zspellSnowball.validX, (int) zspellSnowball.validY))
        {
          zspellSnowball.position = new MyLocation(zspellSnowball.validX, zspellSnowball.validY);
          yield return 0.0f;
          goto label_2;
        }
        else
        {
          --zspellSnowball.steps;
          int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(zspellSnowball.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * zspellSnowball.zb.Count) - zspellSnowball.radius;
          if (num1 < 0)
            num1 += zspellSnowball.zb.Count;
          for (int index1 = 0; index1 < zspellSnowball.radius * 2; ++index1)
          {
            int index2 = (index1 + num1) % zspellSnowball.zb.Count;
            int num2 = (int) (fixedInt3 + zspellSnowball.pX);
            int num3 = (int) (fixedInt4 + zspellSnowball.pY);
            if ((zspellSnowball.curDuration > collisionFrame ? (!zspellSnowball.map.SpellCheckPosition(num2 + zspellSnowball.zb[index2].x, num3 + zspellSnowball.zb[index2].y, zspellSnowball.toCollideCheck, Inert.mask_spell_movement) ? 1 : 0) : (!zspellSnowball.map.CheckPositionOnlyMap(num2 + zspellSnowball.zb[index2].x, num3 + zspellSnowball.zb[index2].y) ? 1 : 0)) != 0)
            {
              if (!zspellSnowball.map.CheckPositionOnlyMap(num2 + zspellSnowball.zb[index2].x, num3 + zspellSnowball.zb[index2].y))
              {
                zspellSnowball.position = new MyLocation(zspellSnowball.validX, (FixedInt) (num3 + zspellSnowball.zb[index2].y + zspellSnowball.radius));
                yield return 0.0f;
                yield return 0.0f;
                zspellSnowball.OnDeath(true);
                yield break;
              }
              else
              {
                ZCreature zcreature = zspellSnowball.map.PhysicsCollideCreature(zspellSnowball.toCollideCheck, num2 + zspellSnowball.zb[index2].x, num3 + zspellSnowball.zb[index2].y);
                if (zspellSnowball.spellEnum == SpellEnum.Verdant_Javelin && (ZComponent) zcreature == (object) null)
                  zcreature = zspellSnowball.map.PhysicsCollideCreatureCircle(zspellSnowball.toCollideCheck, num2 + zspellSnowball.zb[index2].x, num3 + zspellSnowball.zb[index2].y, 3);
                if ((ZComponent) zcreature != (object) null && typeof (ZCreatureThorn) != zcreature.GetType())
                {
                  zspellSnowball.velocity.y = (FixedInt) 0;
                  zspellSnowball.velocity.x = (FixedInt) 0;
                  zspellSnowball.addedDeathVector = Inert.Velocity(Inert.AngleOfVelocity(zspellSnowball.velocity), zspellSnowball.radius);
                  zspellSnowball.position = new MyLocation(zspellSnowball.validX, zspellSnowball.validY);
                  zspellSnowball.moving = (IEnumerator<float>) null;
                  zspellSnowball.isMoving = false;
                  zcreature.ApplyDamage(zspellSnowball.spellEnum, zspellSnowball.damageType, zspellSnowball.damage, zspellSnowball.parent, zspellSnowball.game.turn, (ISpellBridge) zspellSnowball);
                  if ((Object) zspellSnowball.explosion != (Object) null)
                    zspellSnowball.OnExplosion();
                  zspellSnowball.isDead = true;
                  zspellSnowball.isNull = true;
                  ZComponent.Destroy<GameObject>(zspellSnowball.gameObject);
                  yield break;
                }
                else
                {
                  zspellSnowball.position = new MyLocation(zspellSnowball.validX, zspellSnowball.validY);
                  zspellSnowball.OnDeath(true);
                  yield break;
                }
              }
            }
          }
          zspellSnowball.validX = zspellSnowball.pX;
          zspellSnowball.validY = zspellSnowball.pY;
          zspellSnowball.pX = zspellSnowball.pX + fixedInt3;
          zspellSnowball.pY = zspellSnowball.pY + fixedInt4;
        }
      }
      zspellSnowball.position = zspellSnowball.position + zspellSnowball.velocity;
      if (zspellSnowball.position.y < zspellSnowball.radius && zspellSnowball.velocity.y <= 0)
      {
        zspellSnowball.moving = (IEnumerator<float>) null;
        zspellSnowball.velocity = MyLocation.zero;
        zspellSnowball.isMoving = false;
        zspellSnowball.Splash();
        if (zspellSnowball.spellEnum != SpellEnum.Brine_Bolt)
          zspellSnowball.OnDeath(true);
        else
          ZComponent.Destroy<GameObject>(zspellSnowball.gameObject);
        zspellSnowball.isDead = true;
        zspellSnowball.isNull = true;
        break;
      }
      if (zspellSnowball.addVelocity)
      {
        zspellSnowball.addVelocity = false;
        zspellSnowball.velocity = zspellSnowball.velocity + zspellSnowball.addedVelocity;
        zspellSnowball.velocity.x = Mathd.Clamp(zspellSnowball.velocity.x, (FixedInt) -50, (FixedInt) 50);
        zspellSnowball.velocity.y = Mathd.Clamp(zspellSnowball.velocity.y, (FixedInt) -50, (FixedInt) 50);
        zspellSnowball.addedVelocity.x = (FixedInt) 0;
        zspellSnowball.addedVelocity.y = (FixedInt) 0;
      }
      else if ((zspellSnowball.affectedByGravity || zspellSnowball.curDuration > 150) && zspellSnowball.velocity.y > -ZMap.MaxSpeed)
        zspellSnowball.velocity.y += zspellSnowball.map.Gravity;
      else if (!zspellSnowball.affectedByGravity && zspellSnowball.velocity.y > -ZMap.SnowSpeed)
        zspellSnowball.velocity.y += zspellSnowball.map.Gravity;
      else if (!zspellSnowball.affectedByGravity && zspellSnowball.velocity.y > -10 && zspellSnowball.maxDuration > 150 && zspellSnowball.curDuration > 10)
        zspellSnowball.affectedByGravity = true;
      zspellSnowball.Wind();
      if (zspellSnowball.Rotates && (Object) zspellSnowball.transform != (Object) null)
        zspellSnowball.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(zspellSnowball.velocity.y.ToFloat(), zspellSnowball.velocity.x.ToFloat()) * 57.29578f);
      ++zspellSnowball.curDuration;
      if (zspellSnowball.curDuration >= zspellSnowball.maxDuration)
      {
        if ((Object) zspellSnowball.explosion != (Object) null)
          zspellSnowball.OnExplosion();
        ZComponent.Destroy<GameObject>(zspellSnowball.gameObject);
        zspellSnowball.isDead = true;
        zspellSnowball.isNull = true;
        break;
      }
      yield return 0.0f;
    }
  }
}
