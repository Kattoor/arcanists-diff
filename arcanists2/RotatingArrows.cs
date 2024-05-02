// Decompiled with JetBrains decompiler
// Type: RotatingArrows
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RotatingArrows : MonoBehaviour
{
  private float ts;
  private Vector3 tScale = Vector3.zero;
  private Vector3 tRotation = Vector3.zero;
  public float speed = 60f;
  public float minScale = 1f;
  public float maxScale = 1.25f;
  public float scaleSpeed = 5f;

  private void Update()
  {
    this.tRotation.z += this.speed * Time.deltaTime;
    this.ts += this.scaleSpeed * Time.deltaTime;
    float num = Mathf.Lerp(this.minScale, this.maxScale, Mathf.Abs(Mathf.Sin(this.ts)));
    this.tScale.x = num;
    this.tScale.y = num;
    this.transform.localEulerAngles = this.tRotation;
    this.transform.localScale = this.tScale;
  }
}
