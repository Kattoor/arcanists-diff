

using UnityEngine;

#nullable disable
public class LerpUpDown : MonoBehaviour
{
  private float cur = 0.5f;
  private bool down = true;
  public Vector3 top = new Vector3(0.0f, 40f, 0.0f);
  public Vector3 bottom = new Vector3(0.0f, -40f, 0.0f);

  public void Update()
  {
    if (this.down)
    {
      this.cur += Time.deltaTime * 1.25f;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.down = false;
      }
    }
    else
    {
      this.cur -= Time.deltaTime * 1.25f;
      if ((double) this.cur <= 0.0)
      {
        this.cur = 0.0f;
        this.down = true;
      }
    }
    this.transform.localPosition = Vector3.Slerp(this.top, this.bottom, this.cur);
  }
}
