// Decompiled with JetBrains decompiler
// Type: ParticleRotate
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleRotate : MonoBehaviour
{
  public float speed = 10f;
  private Vector3 a = Vector3.zero;

  private void Update()
  {
    this.a.z += Time.deltaTime * this.speed;
    this.transform.localEulerAngles = this.a;
  }
}
