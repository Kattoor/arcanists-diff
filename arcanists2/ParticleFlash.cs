// Decompiled with JetBrains decompiler
// Type: ParticleFlash
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleFlash : MonoBehaviour
{
  public SpriteRenderer white;
  public Transform red;
  public Transform black;
  private float cur;

  private void Start()
  {
  }

  private void Update()
  {
    this.cur += Time.deltaTime * 2f;
    this.white.color = this.white.color with
    {
      a = Mathf.Lerp(0.0f, 0.8f, this.cur)
    };
    float num1 = Mathf.Lerp(0.0f, 0.9f, this.cur * 1.5f);
    this.red.localScale = new Vector3(num1, num1, 1f);
    float num2 = Mathf.Lerp(0.0f, 0.8f, this.cur);
    this.black.localScale = new Vector3(num2, num2, 1f);
    if ((double) this.cur <= 1.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
