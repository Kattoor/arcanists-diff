

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
