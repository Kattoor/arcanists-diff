

using UnityEngine;

#nullable disable
public class ScrollTexture : MonoBehaviour
{
  public float uvAnimationRate;
  public Material m;
  private Vector2 uvOffset = Vector2.zero;

  private void Awake()
  {
  }

  private void LateUpdate()
  {
    this.uvOffset.x -= this.uvAnimationRate * Time.deltaTime;
    if ((double) this.uvOffset.x < -1.0)
      ++this.uvOffset.x;
    this.m.SetTextureOffset("_MainTex", this.uvOffset);
  }
}
