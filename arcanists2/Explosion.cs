

using System;
using UnityEngine;

#nullable disable
public class Explosion : MonoBehaviour
{
  public float Duration = 3f;
  [NonSerialized]
  public float curDuration;

  public virtual void Update()
  {
    this.curDuration += Time.deltaTime;
    if ((double) this.curDuration < (double) this.Duration)
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }
}
