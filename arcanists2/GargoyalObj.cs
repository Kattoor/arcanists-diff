﻿// Decompiled with JetBrains decompiler
// Type: GargoyalObj
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GargoyalObj : MonoBehaviour
{
  public List<SpriteRenderer> sprites;
  public Transform leftWing;
  public Transform rightWing;
  private bool flying = true;
  private float f;
  private bool up = true;

  private void Awake() => this.ToggleFlying(true);

  public void SetColor(bool flying, float v)
  {
    this.StopAllCoroutines();
    this.StartCoroutine(this.ColorDelay(v));
    this.ToggleFlying(flying);
  }

  private void ToggleFlying(bool flying)
  {
    this.flying = flying;
    if (flying)
      this.StartCoroutine(this.Fly());
    else
      this.StartCoroutine(this.StopFlying());
  }

  private IEnumerator ColorDelay(float target)
  {
    Vector4 v = new Vector4(0.0f, -1f - target, 0.0f, 0.0f);
    if ((double) v.y < (double) target)
    {
      while ((double) v.y < (double) target)
      {
        v.y += Time.deltaTime * 3f;
        foreach (Renderer sprite in this.sprites)
          sprite.material.SetVector("_HSVAAdjust", v);
        yield return (object) new WaitForEndOfFrame();
      }
    }
    else
    {
      while ((double) v.y > (double) target)
      {
        v.y -= Time.deltaTime * 3f;
        foreach (Renderer sprite in this.sprites)
          sprite.material.SetVector("_HSVAAdjust", v);
        yield return (object) new WaitForEndOfFrame();
      }
    }
  }

  private IEnumerator Fly()
  {
    while (this.flying)
    {
      if (this.up)
      {
        this.f += Time.deltaTime * 3f;
        if ((double) this.f >= 1.0)
        {
          this.f = 1f;
          this.up = false;
        }
      }
      else
      {
        this.f -= Time.deltaTime * 3f;
        if ((double) this.f <= 0.0)
        {
          this.f = 0.0f;
          this.up = true;
        }
      }
      this.rightWing.localEulerAngles = new Vector3(0.0f, Mathf.SmoothStep(0.0f, -45f, this.f), Mathf.SmoothStep(0.0f, -10f, this.f));
      this.leftWing.localEulerAngles = new Vector3(0.0f, Mathf.SmoothStep(0.0f, -45f, this.f), Mathf.SmoothStep(0.0f, 10f, this.f));
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private IEnumerator StopFlying()
  {
    while ((double) this.f > 0.0)
    {
      this.f -= Time.deltaTime;
      if ((double) this.f < 0.0)
        this.f = 0.0f;
      this.rightWing.localEulerAngles = new Vector3(0.0f, Mathf.Lerp(0.0f, -30f, this.f), Mathf.Lerp(0.0f, -10f, this.f));
      this.leftWing.localEulerAngles = new Vector3(0.0f, Mathf.Lerp(0.0f, -30f, this.f), Mathf.Lerp(0.0f, 10f, this.f));
      yield return (object) new WaitForEndOfFrame();
    }
    this.rightWing.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    this.leftWing.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
  }
}