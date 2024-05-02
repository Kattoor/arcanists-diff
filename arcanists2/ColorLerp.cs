// Decompiled with JetBrains decompiler
// Type: ColorLerp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ColorLerp : MonoBehaviour
{
  public List<SpriteRenderer> rends = new List<SpriteRenderer>();
  public float min = 0.2f;
  public float max = 0.5f;
  public float speed = 1f;
  private float cur;
  private bool up = true;
  private float c = 0.5f;

  public void Kill()
  {
    for (int index = 0; index < this.rends.Count; ++index)
    {
      Color color = this.rends[index].color with { a = 1f };
      this.rends[index].color = color;
    }
    this.enabled = false;
    Object.Destroy((Object) this);
  }

  private void Start()
  {
    if (!Application.isBatchMode)
      return;
    this.enabled = false;
  }

  private void Update()
  {
    if (this.up)
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.up = false;
      }
    }
    else
    {
      this.cur -= Time.deltaTime * this.speed;
      if ((double) this.cur <= 0.0)
      {
        this.cur = 0.0f;
        this.up = true;
      }
    }
    this.c = Mathf.SmoothStep(this.min, this.max, this.cur);
    for (int index = 0; index < this.rends.Count; ++index)
    {
      if ((Object) this.rends[index] == (Object) null)
      {
        this.rends.RemoveAt(index);
      }
      else
      {
        Color color = this.rends[index].color with
        {
          a = this.c
        };
        this.rends[index].color = color;
      }
    }
  }
}
