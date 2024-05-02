

using UnityEngine;

#nullable disable
public class TwiddleRock : MonoBehaviour
{
  public float speed = 1f;
  public float maxZ = 5f;
  public float minZ;
  private float curTime;
  private bool down;

  private void Update()
  {
    if (this.down)
    {
      this.curTime -= Time.deltaTime * this.speed;
      if ((double) this.curTime <= 0.0)
      {
        this.curTime = 0.0f;
        this.down = false;
      }
    }
    else
    {
      if ((double) this.curTime >= 1.0)
      {
        this.curTime = 1f;
        this.down = true;
      }
      this.curTime += Time.deltaTime * this.speed;
    }
    this.transform.localEulerAngles = this.transform.localEulerAngles with
    {
      z = Mathf.SmoothStep(this.minZ, this.maxZ, this.curTime)
    };
  }
}
