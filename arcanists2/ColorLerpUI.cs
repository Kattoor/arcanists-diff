// Decompiled with JetBrains decompiler
// Type: ColorLerpUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ColorLerpUI : MonoBehaviour
{
  public Image rend;
  public float min = 0.2f;
  public float max = 0.5f;
  public float speed = 1f;
  private float cur;
  private bool up = true;
  private float c = 0.5f;

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
    this.c = Mathf.Lerp(this.min, this.max, this.cur);
    this.rend.color = this.rend.color with { a = this.c };
  }
}
