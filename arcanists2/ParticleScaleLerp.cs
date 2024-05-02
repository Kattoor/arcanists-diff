

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
