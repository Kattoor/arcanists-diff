// Decompiled with JetBrains decompiler
// Type: ParticleScaleLerp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleScaleLerp : MonoBehaviour
{
  private bool back;
  private float t;
  public float speed = 1f;
  public float maxScale = 1f;
  public float minScale = 1f;

  private void Update()
  {
    if (this.back)
    {
      this.t -= Time.deltaTime * this.speed;
      if ((double) this.t <= 0.0)
        this.back = false;
    }
    else
    {
      this.t += Time.deltaTime * this.speed;
      if ((double) this.t > 1.0)
        this.back = true;
    }
    float num = Mathf.SmoothStep(this.minScale, this.maxScale, this.t);
    this.transform.localScale = new Vector3(num, num);
  }
}
