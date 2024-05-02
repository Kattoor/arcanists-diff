

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZEntity : ZComponent
{
  public int id;
  public ZGame game;
  public int radius = 5;
  [NonSerialized]
  public ZMyCollider collider;
  private MyLocation _position;
  [NonSerialized]
  public MyLocation velocity;
  [NonSerialized]
  public MyLocation addedVelocity;
  [NonSerialized]
  public bool addVelocity;
  [NonSerialized]
  public bool isDead;
  public bool affectedByGravity = true;
  public IEnumerator<float> moving;
  public bool isMoving;
  public bool isPawn = true;
  [NonSerialized]
  public FixedInt validX = FixedInt.ZeroF;
  [NonSerialized]
  public FixedInt validY = FixedInt.ZeroF;
  [NonSerialized]
  public FixedInt pX = FixedInt.ZeroF;
  [NonSerialized]
  public FixedInt pY = FixedInt.ZeroF;
  public List<Coords> zb;

  public ZMyWorld world => this.game?.world;

  public ZMap map => this.game?.map;

  public MyLocation position
  {
    get => this._position;
    set
    {
      this._position = value;
      if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
        return;
      this.transform.position = (Vector3) value.ToSinglePrecision();
    }
  }

  public MyLocation GetPositionAI
  {
    get => new MyLocation(this._position.x, Mathd.Max(this._position.y, (FixedInt) 0));
  }

  public ZCreature asCreature
  {
    get
    {
      return !typeof (ZCreature).IsAssignableFrom(this.GetType()) ? (ZCreature) null : (ZCreature) this;
    }
  }

  public ZSpell asSpell
  {
    get => !typeof (ZSpell).IsAssignableFrom(this.GetType()) ? (ZSpell) null : (ZSpell) this;
  }

  public void SetOffsetStaticBall(MyLocation sb) => this._position -= sb;

  public void SetOffsetShockBomb(int x) => this._position.y += x;

  public int GetX() => (int) this._position.x;

  public int GetY() => (int) this._position.y;

  public virtual void SetPosition(MyLocation p) => this.position = p;

  public void KillMovement()
  {
    this.isMoving = false;
    this.velocity.y = (FixedInt) 0;
    this.velocity.x = (FixedInt) 0;
    this.addedVelocity.x = (FixedInt) 0;
    this.addedVelocity.y = (FixedInt) 0;
    this.validX = this.position.x;
    this.validY = this.position.y;
    this.pX = this.validX;
    this.pY = this.validY;
    if (this.moving != null)
      this.game?.ongoing.KillCoroutines(this.moving);
    this.moving = (IEnumerator<float>) null;
  }

  public void KillMovementSimple()
  {
    this.isMoving = false;
    this.velocity.y = (FixedInt) 0;
    this.velocity.x = (FixedInt) 0;
    this.addedVelocity.x = (FixedInt) 0;
    this.addedVelocity.y = (FixedInt) 0;
    this.moving = (IEnumerator<float>) null;
  }

  public virtual void OnDeath(bool playDeathClip = true)
  {
    this.moving = (IEnumerator<float>) null;
    this.velocity = MyLocation.zero;
    this.isMoving = false;
  }

  public virtual void StartMoving(bool fromInput = false)
  {
  }
}
