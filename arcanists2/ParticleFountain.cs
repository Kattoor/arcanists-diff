

using UnityEngine;

#nullable disable
public class ParticleFountain : MonoBehaviour
{
  public SpriteRenderer ray;
  public SpriteRenderer circle1;
  private Vector2 raySize;
  private Vector2 circleSize;
  private Color white = Color.white;
  private float f;

  private void Start()
  {
    this.raySize = new Vector2(1f, (float) (Client.map.Height + 1000 - (int) this.transform.position.y));
    this.circleSize = (Vector2) this.circle1.transform.localScale;
  }

  private void Update()
  {
    this.f += Time.deltaTime * 0.66f;
    if ((double) this.f < 0.5)
    {
      this.raySize.x = this.f * 150f;
      this.circleSize.x = this.f * 5f;
      this.circleSize.y = this.circleSize.x;
      this.ray.size = this.raySize;
      this.circle1.transform.localScale = (Vector3) this.circleSize;
    }
    else if ((double) this.f < 1.0)
    {
      this.white.a = (float) (1.0 - ((double) this.f - 0.5) * 2.0);
      this.circle1.color = this.white;
      this.white.a -= 0.2f;
      this.ray.color = this.white;
    }
    else
      Object.Destroy((Object) this.gameObject);
  }
}
