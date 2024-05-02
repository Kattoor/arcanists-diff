

using UnityEngine;

#nullable disable
public class SpaceshipLogic : MonoBehaviour
{
  public AnimationCurve curve;
  private float min = -1000f;
  private float max;
  private float cur;
  public float speed = 0.01f;
  private Vector3 pos;
  private float seed;

  private void Start()
  {
    this.max = (float) (Client.game.map.Width + 1000);
    this.seed = (float) Random.Range(-100, 100);
  }

  private void Update()
  {
    if (Client.game == null || Client.game.map == null)
      return;
    this.cur += Time.deltaTime * this.speed;
    this.pos.x = Mathf.Lerp(this.min, this.max, this.cur);
    this.pos.y = (float) ((double) Mathf.PerlinNoise(this.pos.x / 1000f + this.seed, this.pos.x / 500f + this.seed) * 500.0 + (double) Client.game.map.Height + 300.0);
    this.transform.position = this.pos;
    if ((double) this.cur < 1.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
