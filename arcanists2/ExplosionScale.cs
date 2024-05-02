// Decompiled with JetBrains decompiler
// Type: ExplosionScale
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
