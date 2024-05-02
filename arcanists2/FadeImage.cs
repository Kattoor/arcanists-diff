

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class FadeImage : MonoBehaviour
{
  public float speed = 0.3f;
  public float delay = 1f;
  public Image img;
  private float cur;

  private void Update()
  {
    if ((double) this.delay > 0.0)
    {
      this.delay -= Time.deltaTime;
    }
    else
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
        this.gameObject.SetActive(false);
      else
        this.img.color = this.img.color with
        {
          a = 1f - this.cur
        };
    }
  }
}
