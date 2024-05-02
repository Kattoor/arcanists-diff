// Decompiled with JetBrains decompiler
// Type: ParticleAbduction
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ParticleAbduction : MonoBehaviour
{
  public SpriteRenderer zLight;
  public List<SpriteRenderer> ship;
  public ParticleSystem part;
  public AnimationCurve curve;
  public Transform ufoTransform;
  private int state;
  private float cur;
  public float stage2 = 1f;

  private void Update()
  {
    this.cur += Time.deltaTime;
    if (this.state == 0)
    {
      Color color = this.zLight.color with
      {
        a = this.curve.Evaluate(this.cur)
      };
      this.zLight.color = color;
      this.SetColor(new Color(1f, 1f, 1f, color.a));
      if ((double) this.cur < 1.0)
        return;
      this.cur = 0.0f;
      ++this.state;
    }
    else if (this.state == 1)
    {
      if ((double) this.cur < (double) this.stage2)
        return;
      this.cur = 0.0f;
      ++this.state;
      this.part.Stop();
    }
    else
    {
      Color color = this.zLight.color with
      {
        a = this.curve.Evaluate(1f - this.cur)
      };
      this.zLight.color = color;
      this.SetColor(new Color(1f, 1f, 1f, color.a));
      if ((double) this.cur < 1.0)
        return;
      Object.Destroy((Object) this.gameObject);
    }
  }

  private void SetColor(Color c)
  {
    foreach (SpriteRenderer spriteRenderer in this.ship)
      spriteRenderer.color = c;
  }
}
