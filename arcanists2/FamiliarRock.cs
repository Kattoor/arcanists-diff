// Decompiled with JetBrains decompiler
// Type: FamiliarRock
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FamiliarRock : Familiar
{
  public float maxDistance = 4f;
  public float speed = 1f;
  public float height = 18f;
  public IAnimator anim;
  private AnimateState lastState;

  private void Start()
  {
    if (ZComponent.IsNull((ZComponent) this.creature))
      return;
    this.transform.position = (Vector3) this.creature.position.ToSinglePrecision() + new Vector3(-9f, -12f);
  }

  protected override void LateUpdate()
  {
    if (ZComponent.IsNull((ZComponent) this.creature) || this.creature.isDead)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (!this.creature.game.isClient)
        return;
      Vector3 offset = this.GetOffset();
      if ((double) this.creature.transformscale > 0.0)
        offset.x = -offset.x;
      Vector3 vector3 = (Vector3) this.creature.position.ToSinglePrecision() + offset;
      Coords coords = this.creature.game.map.bresenhamsLineCastOnlyTerrain(new Coords((int) vector3.x, (int) vector3.y), new Coords((int) vector3.x, (int) vector3.y - (this.creature.GetRadius() + 30)));
      if (coords != null)
        vector3.y = (float) coords.y + this.height;
      if ((double) this.transform.localScale.x < 0.0 && (double) this.creature.transformscale > 0.0)
        this.transform.localScale = new Vector3(1f, 1f, 1f);
      else if ((double) this.transform.localScale.x > 0.0 && (double) this.creature.transformscale < 0.0)
        this.transform.localScale = new Vector3(-1f, 1f, 1f);
      bool flag = true;
      float num = Vector3.Distance(vector3, this.transform.position);
      if ((int) vector3.y != (int) this.transform.position.y || (double) num > (double) this.maxDistance)
      {
        this.transform.position = Vector3.Lerp(this.transform.position, vector3, Time.deltaTime * this.speed);
        if ((double) num > 3.0 && this.lastState == AnimateState.Walk && this.creature.animator.currentState == AnimateState.Stop)
        {
          this.anim.Play(AnimateState.Walk, 0.05f);
          flag = false;
        }
      }
      if (!flag)
        return;
      if (this.creature.animator.currentState == AnimateState.Walk)
      {
        this.anim.Play(AnimateState.Walk, 0.05f);
        this.lastState = AnimateState.Walk;
      }
      else if (this.creature.animator.currentState == AnimateState.Jump)
      {
        this.anim.Play(AnimateState.Jump);
        this.lastState = AnimateState.Jump;
      }
      else
      {
        if (this.creature.animator.currentState != AnimateState.Stop)
          return;
        this.anim.Play(AnimateState.Stop);
      }
    }
  }
}
