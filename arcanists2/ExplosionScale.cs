

using UnityEngine;

#nullable disable
public class ExplosionScale : Explosion
{
  public override void Update()
  {
    float num = (float) (1.0 - (double) this.curDuration / (double) this.Duration);
    if ((double) num < 0.0099999997764825821)
      num = 0.01f;
    this.transform.localScale = Vector3.one * num;
    base.Update();
  }
}
