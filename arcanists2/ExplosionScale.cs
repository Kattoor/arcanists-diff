// Decompiled with JetBrains decompiler
// Type: ExplosionScale
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
