

using UnityEngine;

#nullable disable
public class SlideOutUI : MonoBehaviour
{
  private float cur;
  private Vector2 startPos;
  private Vector2 endPos;
  public Vector2 add;
  public UIOnHover button;
  public RectTransform rect;

  private void Start()
  {
    this.startPos = this.rect.anchoredPosition;
    this.endPos = this.startPos + this.add;
  }

  private void Update()
  {
    if (this.button.IsHovering)
    {
      this.cur += Time.deltaTime * 3f;
      if ((double) this.cur >= 1.0)
      {
        this.enabled = false;
        this.cur = 1f;
        this.rect.anchoredPosition = this.endPos;
      }
      else
        this.rect.anchoredPosition = Vector2.Lerp(this.startPos, this.endPos, Mathf.SmoothStep(0.0f, 1f, this.cur));
    }
    else
    {
      this.cur -= Time.deltaTime * 3f;
      if ((double) this.cur <= 0.0)
      {
        this.enabled = false;
        this.cur = 0.0f;
        this.rect.anchoredPosition = this.startPos;
      }
      else
        this.rect.anchoredPosition = Vector2.Lerp(this.startPos, this.endPos, Mathf.SmoothStep(0.0f, 1f, this.cur));
    }
  }
}
