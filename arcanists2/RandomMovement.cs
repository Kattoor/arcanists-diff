﻿// Decompiled with JetBrains decompiler
// Type: RandomMovement
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RandomMovement : MonoBehaviour
{
  public Vector3 Amount = new Vector3(5f, 5f, 0.0f);
  public float Speed = 0.2f;
  public bool DeltaMovement = true;
  public float time;
  protected Vector3 lastPos;
  protected Vector3 nextPos;

  private void LateUpdate()
  {
    this.time += Time.deltaTime * this.Speed;
    this.nextPos.x = (float) ((double) Mathf.PerlinNoise(this.time, this.time * 2f) * (double) this.Amount.x - (double) this.Amount.x / 2.0);
    this.nextPos.y = (float) ((double) Mathf.PerlinNoise(this.time * 2f, this.time) * (double) this.Amount.y - (double) this.Amount.y / 2.0);
    this.transform.Translate(this.DeltaMovement ? this.nextPos - this.lastPos : this.nextPos);
    this.lastPos = this.nextPos;
  }
}
