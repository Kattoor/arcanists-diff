

using UnityEngine;

#nullable disable
public class TwiddleLerpZ : MonoBehaviour
{
  public float minScale = -10f;
  public float maxScale = 10f;
  public float speed = 2f;
  private float curT;
  private bool v;

  private void Update()
  {
    if (this.v)
    {
      this.curT += Time.deltaTime * this.speed;
      if ((double) this.curT >= 1.0)
      {
        this.v = false;
        this.curT = 1f;
      }
    }
    else
    {
      this.curT -= Time.deltaTime * this.speed;
      if ((double) this.curT <= 0.0)
      {
        this.v = true;
        this.curT = 0.0f;
      }
    }
    this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.SmoothStep(this.minScale, this.maxScale, this.curT));
  }
}
