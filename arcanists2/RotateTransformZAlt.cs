// Decompiled with JetBrains decompiler
// Type: RotateTransformZAlt
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RotateTransformZAlt : MonoBehaviour, IClientOnly
{
  public float speed;
  public AnimationCurve curve;
  private Vector3 rot;

  private void Update()
  {
    this.rot.z += this.speed * this.curve.Evaluate(this.rot.z / 360f) * Time.deltaTime;
    this.transform.localEulerAngles = this.rot;
    if ((double) this.rot.z < 360.0)
      return;
    this.rot.z -= 360f;
  }
}
