

using System;
using UnityEngine;

#nullable disable
public class ParticleProtectionShield : MonoBehaviour
{
  [NonSerialized]
  public Creature c;
  public SpriteRenderer sp;
  private int lastshield;

  private void Update()
  {
    if ((UnityEngine.Object) this.c == (UnityEngine.Object) null || this.c.shield == 0)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      if (this.lastshield == this.c.shield)
        return;
      this.lastshield = this.c.shield;
      this.sp.color = this.sp.color with
      {
        a = this.c.shield > 50 ? (float) Mathf.Clamp(175 + this.c.shield / 2, 175, (int) byte.MaxValue) / (float) byte.MaxValue : (float) (175 - (50 - this.c.shield)) / (float) byte.MaxValue
      };
    }
  }
}
