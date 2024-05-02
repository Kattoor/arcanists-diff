// Decompiled with JetBrains decompiler
// Type: ScrollButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class ScrollButton : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler,
  IPointerEnterHandler,
  IPointerExitHandler
{
  public Sprite hoverSprite;
  public Sprite defaultSprite;
  public Image image;
  public ScrollRect _chatScrollbar;
  public RectTransform content;
  public RecycledScrollView recycle;
  public bool scrollDown;
  private bool isDown;

  public void OnPointerDown(PointerEventData eventData)
  {
    this.isDown = true;
    this.image.sprite = this.hoverSprite;
    if ((Object) this.recycle != (Object) null)
      this.StartCoroutine(this.scrollDown ? this.OnRecycled_Down() : this.OnRecycled_Up());
    else
      this.StartCoroutine(this.scrollDown ? this.Go_Down() : this.Go_Up());
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    this.isDown = false;
    this.image.sprite = this.defaultSprite;
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (this.isDown)
      return;
    this.image.sprite = this.hoverSprite;
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    if (this.isDown)
      return;
    this.image.sprite = this.defaultSprite;
  }

  private IEnumerator OnRecycled_Down()
  {
    float cur = 0.0f;
    this.recycle.ScrollDown();
    while (this.isDown && (double) cur < 0.5)
    {
      cur += Time.deltaTime;
      yield return (object) null;
    }
    while (this.isDown)
    {
      this.recycle.ScrollDown();
      yield return (object) new WaitForSecondsRealtime(0.1f);
    }
  }

  private IEnumerator OnRecycled_Up()
  {
    float cur = 0.0f;
    this.recycle.ScrolUp();
    while (this.isDown && (double) cur < 0.5)
    {
      cur += Time.deltaTime;
      yield return (object) null;
    }
    while (this.isDown)
    {
      this.recycle.ScrolUp();
      yield return (object) new WaitForSecondsRealtime(0.1f);
    }
  }

  private IEnumerator Go_Down()
  {
    float cur = 0.0f;
    ScrollRect chatScrollbar1 = this._chatScrollbar;
    double x1 = (double) this._chatScrollbar.normalizedPosition.x;
    double y1 = (double) this._chatScrollbar.normalizedPosition.y;
    double scrollSensitivity1 = (double) this._chatScrollbar.scrollSensitivity;
    double y2 = (double) this.content.sizeDelta.y;
    Rect rect = ((RectTransform) this.content.parent).rect;
    double height1 = (double) rect.height;
    double num1 = y2 - height1;
    double num2 = scrollSensitivity1 / num1;
    double y3 = y1 - num2;
    Vector2 vector2_1 = new Vector2((float) x1, (float) y3);
    chatScrollbar1.normalizedPosition = vector2_1;
    while (this.isDown && (double) cur < 1.0)
    {
      cur += Time.deltaTime;
      yield return (object) null;
    }
    while (this.isDown)
    {
      ScrollRect chatScrollbar2 = this._chatScrollbar;
      double x2 = (double) this._chatScrollbar.normalizedPosition.x;
      double y4 = (double) this._chatScrollbar.normalizedPosition.y;
      double scrollSensitivity2 = (double) this._chatScrollbar.scrollSensitivity;
      double y5 = (double) this.content.sizeDelta.y;
      rect = ((RectTransform) this.content.parent).rect;
      double height2 = (double) rect.height;
      double num3 = y5 - height2;
      double num4 = scrollSensitivity2 / num3;
      double y6 = y4 - num4;
      Vector2 vector2_2 = new Vector2((float) x2, (float) y6);
      chatScrollbar2.normalizedPosition = vector2_2;
      yield return (object) new WaitForSecondsRealtime(0.1f);
    }
  }

  private IEnumerator Go_Up()
  {
    float cur = 0.0f;
    ScrollRect chatScrollbar1 = this._chatScrollbar;
    double x1 = (double) this._chatScrollbar.normalizedPosition.x;
    double y1 = (double) this._chatScrollbar.normalizedPosition.y;
    double scrollSensitivity1 = (double) this._chatScrollbar.scrollSensitivity;
    double y2 = (double) this.content.sizeDelta.y;
    Rect rect = ((RectTransform) this.content.parent).rect;
    double height1 = (double) rect.height;
    double num1 = y2 - height1;
    double num2 = scrollSensitivity1 / num1;
    double y3 = y1 + num2;
    Vector2 vector2_1 = new Vector2((float) x1, (float) y3);
    chatScrollbar1.normalizedPosition = vector2_1;
    while (this.isDown && (double) cur < 1.0)
    {
      cur += Time.deltaTime;
      yield return (object) null;
    }
    while (this.isDown)
    {
      ScrollRect chatScrollbar2 = this._chatScrollbar;
      double x2 = (double) this._chatScrollbar.normalizedPosition.x;
      double y4 = (double) this._chatScrollbar.normalizedPosition.y;
      double scrollSensitivity2 = (double) this._chatScrollbar.scrollSensitivity;
      double y5 = (double) this.content.sizeDelta.y;
      rect = ((RectTransform) this.content.parent).rect;
      double height2 = (double) rect.height;
      double num3 = y5 - height2;
      double num4 = scrollSensitivity2 / num3;
      double y6 = y4 + num4;
      Vector2 vector2_2 = new Vector2((float) x2, (float) y6);
      chatScrollbar2.normalizedPosition = vector2_2;
      yield return (object) new WaitForSecondsRealtime(0.1f);
    }
  }

  private void OnApplicationFocus(bool focus) => this.isDown = false;

  private void ScrollTo(Transform target)
  {
    Canvas.ForceUpdateCanvases();
    Vector2 vector2 = (Vector2) this._chatScrollbar.transform.InverseTransformPoint(this.content.position) - (Vector2) this._chatScrollbar.transform.InverseTransformPoint(target.position);
    this.content.anchoredPosition = this.content.anchoredPosition with
    {
      y = vector2.y
    };
  }
}
