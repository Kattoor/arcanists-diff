// Decompiled with JetBrains decompiler
// Type: ZSpellSoulJar
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZSpellSoulJar : ZCreature
{
  public MyLocation SetPosition
  {
    set
    {
      this.position = value;
      this.collider.Move(value);
      for (int index = this.followingColliders.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) this.followingColliders[index] == (object) null)
        {
          this.followingColliders.RemoveAt(index);
        }
        else
        {
          this.followingColliders[index].Move(this.position);
          this.followingColliders[index].effector?.Moved();
        }
      }
    }
  }

  public override void OnDeath(bool playDeathClip = true)
  {
  }

  public override int ApplyDamage(
    SpellEnum spellEnum,
    DamageType dt,
    int damage,
    ZCreature enemy,
    int TurnCreated,
    ISpellBridge spellRef = null,
    bool isLoop = false)
  {
    return 0;
  }

  public override void ApplyExplosionForce(
    int spellRadius,
    MyLocation origin,
    FixedInt force,
    int EXORADIUS,
    Curve factor,
    bool startMoving = false)
  {
    this.ShouldFall(true, false);
  }

  internal override void Awake()
  {
  }

  public override void Start()
  {
  }

  public override void StartMoving(bool fromInput = false) => this.ShouldFall(true, false);

  public override void SetVelocity(int x, int y) => this.ShouldFall(true, false);

  public override void Fall(bool ignoreFlight = false) => this.ShouldFall(true, false);

  public override void ApplyHeal(DamageType dt, int damage, ZCreature enemy)
  {
  }

  public override bool ShouldFall(bool gliding = true, bool ignoreFlight = false)
  {
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(MyLocation.down) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * this.zb.Count - this.radius).ToInt();
    if (num1 < 0)
      num1 += this.zb.Count;
    if (this.flying)
    {
      for (int index1 = 0; index1 < this.radius * 2; ++index1)
      {
        int index2 = (index1 + num1) % this.zb.Count;
        int x = (int) this.position.x;
        int num2 = (int) this.position.y - 3;
        if (!this.map.CheckPosition(x + this.zb[index2].x, num2 + this.zb[index2].y, (ZCreature) this, Inert.mask_movement_NoEffector))
          return false;
      }
    }
    else
    {
      for (int index3 = 0; index3 < this.radius * 2; ++index3)
      {
        int index4 = (index3 + num1) % this.zb.Count;
        int x = (int) this.position.x;
        int num3 = (int) this.position.y - 3;
        if (!this.map.CheckPositionOnlyMap(x + this.zb[index4].x, num3 + this.zb[index4].y))
          return false;
      }
    }
    this.SetPosition(new MyLocation(this.position.x, this.position.y - 1));
    if (this.flying)
      this.MoveMorningSun();
    else
      this.GODown();
    return false;
  }

  public override IEnumerator<float> Move(bool fromSerialization = false)
  {
    yield break;
  }

  public void GODown()
  {
    this.isMoving = true;
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    this.velocity.y = (FixedInt) -10;
    this.velocity.x = (FixedInt) 0;
    int num1 = 0;
    int num2 = 900;
    while (!this.isDead)
    {
      this.pX = this.position.x;
      this.pY = this.position.y;
      FixedInt x1 = this.position.x + this.velocity.x;
      FixedInt x2 = this.position.y + this.velocity.y;
      this.validX = this.pX;
      this.validY = this.pY;
      int num3 = 1;
      FixedInt x3 = this.velocity.x;
      FixedInt y = this.velocity.y;
      FixedInt fixedInt1 = this.velocity.x;
      FixedInt fixedInt2 = this.velocity.y;
      if (x3 > 1 || x3 < -1 || y > 1 || y < -1)
      {
        num3 = !(FixedInt.Abs(x3) > FixedInt.Abs(y)) ? (int) FixedInt.Abs(y) + 1 : (int) FixedInt.Abs(x3) + 1;
        fixedInt1 = this.velocity.x / num3;
        fixedInt2 = this.velocity.y / num3;
      }
      while (num3 > 0)
      {
        --num3;
        int xInt;
        if (FixedInt.Abs(this.pX) > FixedInt.Abs(x1))
        {
          this.pX = x1;
          xInt = (int) this.pX;
        }
        else
          xInt = (int) (fixedInt1 + this.pX);
        int num4;
        if (FixedInt.Abs(this.pY) > FixedInt.Abs(x2))
        {
          this.pY = x2;
          num4 = (int) this.pY;
        }
        else
          num4 = (int) (fixedInt2 + this.pY);
        int num5 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(this.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * this.zb.Count) - this.radius;
        if (num5 < 0)
          num5 += this.zb.Count;
        for (int index1 = 0; index1 < this.radius * 2; ++index1)
        {
          int index2 = (index1 + num5) % this.zb.Count;
          if (!this.map.CheckPositionOnlyMap(xInt + this.zb[index2].x, num4 + this.zb[index2].y))
          {
            int x4 = this.zb[index2].x;
            int num6 = num4 + this.zb[index2].y;
            this.velocity.y = (FixedInt) 0;
            this.velocity.x = (FixedInt) 0;
            this.SetPosition = new MyLocation(xInt, num6 + this.radius);
            this.moving = (IEnumerator<float>) null;
            this.isMoving = false;
            return;
          }
        }
        this.validX = this.pX;
        this.validY = this.pY;
        this.pX = this.pX + fixedInt1;
        this.pY = this.pY + fixedInt2;
      }
      this.SetPosition = this.position + this.velocity;
      if (this.position.y < this.radius)
      {
        this.moving = (IEnumerator<float>) null;
        this.velocity = MyLocation.zero;
        this.isMoving = false;
        this.collider?.Disable();
        this.isDead = true;
        this.isNull = true;
        this.parent?.RemoveFamiliar(BookOf.Underdark);
        this.CheckAchievement();
        ZComponent.Destroy<GameObject>(this.gameObject);
        break;
      }
      ++num1;
      if (num1 >= num2)
      {
        this.collider?.Disable();
        this.isDead = true;
        this.isNull = true;
        this.parent?.RemoveFamiliar(BookOf.Underdark);
        this.CheckAchievement();
        ZComponent.Destroy<GameObject>(this.gameObject);
        break;
      }
    }
  }

  public void MoveMorningSun()
  {
    this.isMoving = true;
    if (this.zb == null)
      this.zb = MapGenerator.getOutlineArray(this.radius);
    int num1 = (int) (((FixedInt) 360 - (Inert.AngleOfVelocity(this.velocity) - 90)) * FixedInt.ThreeSixtyBy1 * this.zb.Count) - this.radius;
    if (num1 < 0)
      num1 += this.zb.Count;
    int x1 = (int) this.position.x;
    for (int y = (int) this.position.y; !this.isDead && y >= this.radius; --y)
    {
      for (int index1 = 0; index1 < this.radius * 2; ++index1)
      {
        int index2 = (index1 + num1) % this.zb.Count;
        if (!this.map.CheckPosition(x1 + this.zb[index2].x, y + this.zb[index2].y, (ZCreature) this, Inert.mask_movement_NoEffector))
        {
          int x2 = this.zb[index2].x;
          int num2 = y + this.zb[index2].y;
          this.velocity.y = (FixedInt) 0;
          this.velocity.x = (FixedInt) 0;
          this.SetPosition = new MyLocation(x1, num2 + this.radius);
          this.moving = (IEnumerator<float>) null;
          this.isMoving = false;
          return;
        }
      }
    }
    this.SetPosition = new MyLocation(x1, 0);
    this.moving = (IEnumerator<float>) null;
    this.velocity = MyLocation.zero;
    this.isMoving = false;
  }

  private void CheckAchievement()
  {
    if (this.parent == null)
      return;
    ZCreature zcreature = this.game.CurrentCreature();
    if (!((ZComponent) zcreature != (object) null) || zcreature.team == this.parent.team || this.game.isClient)
      return;
    Achievements.Set(Achievement.Guardian_of_Souls, zcreature.parent.account, zcreature.game);
  }
}
