

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
