﻿// Decompiled with JetBrains decompiler
// Type: TwiddleScale
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwiddleScale : MonoBehaviour
{
  public float minScale = 0.5f;
  public float maxScale = 0.5f;
  public float speed = 2f;
  private float curT;
  private bool v;

  private void Update()
  {
    if (this.v)
    {
      this.curT += Time.deltaTime * this.speed;
      if ((double) this.curT >= 1.0)
      {
        this.v = false;
        this.curT = 1f;
      }
    }
    else
    {
      this.curT -= Time.deltaTime * this.speed;
      if ((double) this.curT <= 0.0)
      {
        this.v = true;
        this.curT = 0.0f;
      }
    }
    float num = Mathf.SmoothStep(this.minScale, this.maxScale, this.curT);
    this.transform.localScale = new Vector3(num, num, num);
  }
}
