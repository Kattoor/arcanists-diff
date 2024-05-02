﻿// Decompiled with JetBrains decompiler
// Type: FadeImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class FadeImage : MonoBehaviour
{
  public float speed = 0.3f;
  public float delay = 1f;
  public Image img;
  private float cur;

  private void Update()
  {
    if ((double) this.delay > 0.0)
    {
      this.delay -= Time.deltaTime;
    }
    else
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
        this.gameObject.SetActive(false);
      else
        this.img.color = this.img.color with
        {
          a = 1f - this.cur
        };
    }
  }
}
