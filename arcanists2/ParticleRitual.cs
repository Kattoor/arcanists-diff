

using UnityEngine;

#nullable disable
public class ParticleRitual : MonoBehaviour
{
  public SpriteRenderer icon;
  public SpriteRenderer lightObj;
  public SpriteRenderer bg;
  private float cur;
  public bool up = true;
  public float speed = 1f;

  private void Update()
  {
    if (this.up)
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.up = false;
      }
    }
    else
    {
      this.cur -= Time.deltaTime * this.speed;
      if ((double) this.cur <= 0.0)
      {
        Object.Destroy((Object) this.gameObject);
        return;
      }
    }
    this.icon.color = this.icon.color with { a = this.cur };
    this.lightObj.color = this.lightObj.color with
    {
      a = Mathf.Lerp(0.0f, 0.6588f, this.cur)
    };
    this.bg.color = this.bg.color with
    {
      a = Mathf.Lerp(0.0f, 0.6588f, this.cur)
    };
  }
}
