// Decompiled with JetBrains decompiler
// Type: ParticleSkyRay
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleSkyRay : MonoBehaviour
{
  public ParticleSystem ps;
  public Vector3 target;
  public Vector3 start;
  private float t;
  private bool active;

  private void Update()
  {
    this.t += Time.deltaTime;
    if (this.active)
      return;
    this.transform.position = Vector3.Lerp(this.start, this.target, this.t);
    if ((double) this.t < 1.0)
      return;
    this.active = true;
    this.ps.Stop();
  }
}
