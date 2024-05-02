// Decompiled with JetBrains decompiler
// Type: RotateTransformZAlt
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
