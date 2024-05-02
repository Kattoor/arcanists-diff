// Decompiled with JetBrains decompiler
// Type: RotateTransformZ
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RotateTransformZ : MonoBehaviour, IClientOnly
{
  public float speed;
  private Vector3 rot;

  private void Update()
  {
    this.rot.z += this.speed * Time.deltaTime;
    this.transform.localEulerAngles = this.rot;
  }
}
