

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class GlowingMiddle : MonoBehaviour
{
  private float ts;
  private Color tColor = Color.white;
  public float speed = 1f;
  public float minScale = 0.5f;
  public float maxScale = 1f;
  public Image image;

  private void Update()
  {
    this.ts += this.speed * Time.deltaTime;
    this.tColor.a = Mathf.Lerp(this.maxScale, this.minScale, Mathf.Abs(Mathf.Sin(this.ts)));
    this.image.color = this.tColor;
  }
}
