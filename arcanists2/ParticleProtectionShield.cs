// Decompiled with JetBrains decompiler
// Type: ParticleProtectionShield
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
