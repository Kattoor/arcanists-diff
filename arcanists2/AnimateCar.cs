// Decompiled with JetBrains decompiler
// Type: AnimateCar
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimateCar : IAnimator
{
  public Transform wheel1;
  public Transform wheel2;
  private Vector3 angles = Vector3.zero;

  public override void Play(AnimateState anim, float duration = 0.0f, bool sound = true)
  {
    if (!Client.game.isClient || Client.game.resyncing)
      return;
    this.duration = duration;
    this.currentState = anim;
  }

  private void Update()
  {
    if (Client.game == null || !Client.game.isClient || Client.game.resyncing || (double) this.duration <= 0.0)
      return;
    this.duration -= Time.deltaTime;
    this.angles.z -= Time.deltaTime * 150f;
    this.wheel1.localEulerAngles = this.angles;
    this.wheel2.localEulerAngles = this.angles;
  }
}
