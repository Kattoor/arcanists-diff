// Decompiled with JetBrains decompiler
// Type: Explosion
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
