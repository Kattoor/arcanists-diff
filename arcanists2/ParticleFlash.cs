

using UnityEngine;

#nullable disable
public class ParticleFlash : MonoBehaviour
{
  public SpriteRenderer white;
  public Transform red;
  public Transform black;
  private float cur;

  private void Start()
  {
  }

  private void Update()
  {
    this.cur += Time.deltaTime * 2f;
    this.white.color = this.white.color with
    {
      a = Mathf.Lerp(0.0f, 0.8f, this.cur)
    };
    float num1 = Mathf.Lerp(0.0f, 0.9f, this.cur * 1.5f);
    this.red.localScale = new Vector3(num1, num1, 1f);
    float num2 = Mathf.Lerp(0.0f, 0.8f, this.cur);
    this.black.localScale = new Vector3(num2, num2, 1f);
    if ((double) this.cur <= 1.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
