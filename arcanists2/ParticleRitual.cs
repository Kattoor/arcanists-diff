// Decompiled with JetBrains decompiler
// Type: ParticleRitual
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleRitual : MonoBehaviour
{
  public SpriteRenderer icon;
  public SpriteRenderer lightObj;
  public SpriteRenderer bg;
  private float cur;
  public bool up = true;
  public float speed = 1f;

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
        Object.Destroy((Object) this.gameObject);
        return;
      }
    }
    this.icon.color = this.icon.color with { a = this.cur };
    this.lightObj.color = this.lightObj.color with
    {
      a = Mathf.Lerp(0.0f, 0.6588f, this.cur)
    };
    this.bg.color = this.bg.color with
    {
      a = Mathf.Lerp(0.0f, 0.6588f, this.cur)
    };
  }
}
