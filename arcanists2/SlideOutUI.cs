// Decompiled with JetBrains decompiler
// Type: SlideOutUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
