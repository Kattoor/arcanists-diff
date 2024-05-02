// Decompiled with JetBrains decompiler
// Type: RotatingArrows
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
