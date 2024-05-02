

using UnityEngine;

#nullable disable
public class PresentColor : MonoBehaviour
{
  public SpriteRenderer main;
  public SpriteRenderer second;

  private void Awake()
  {
    this.main.color = ColorHSV.ToColor(new ColorHSV(Random.Range(0.0f, 1f), 1f, 1f));
    this.second.color = ColorHSV.ToColor(new ColorHSV(Random.Range(0.0f, 1f), 1f, 1f));
  }
}
