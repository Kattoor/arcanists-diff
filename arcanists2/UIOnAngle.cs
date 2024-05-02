

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#nullable disable
public class UIOnAngle : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler,
  IPointerEnterHandler,
  IPointerExitHandler,
  IDragHandler,
  IEndDragHandler
{
  public RectTransform handle;
  public bool interactable = true;
  public UIOnAngle.OnClick onClick;
  private bool isHovering;
  private RectTransform rectTransform;

  public bool IsHovering => this.gameObject.activeInHierarchy && this.isHovering;

  private void Awake() => this.rectTransform = (RectTransform) this.transform;

  private void OnDisable() => this.isHovering = false;

  public void Interactable(bool v)
  {
    if (this.interactable == v)
      return;
    this.interactable = v;
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (!this.interactable)
      return;
    this.CallBack((Vector3) eventData.position);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
  }

  public void OnDrag(PointerEventData eventData) => this.CallBack((Vector3) eventData.position);

  public void OnEndDrag(PointerEventData eventData) => this.CallBack((Vector3) eventData.position);

  public void OnPointerEnter(PointerEventData eventData) => this.isHovering = true;

  public void OnPointerExit(PointerEventData eventData) => this.isHovering = false;

  public void SetValue(float f) => this.handle.localEulerAngles = new Vector3(0.0f, 0.0f, f);

  private void CallBack(Vector3 eventData)
  {
    Vector2 localPoint = Vector2.zero;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, (Vector2) eventData, (Camera) null, out localPoint))
      return;
    Vector2 vector2 = new Vector2(this.rectTransform.sizeDelta.x / 2f, 0.0f);
    Vector2 normalized = (localPoint - vector2).normalized;
    float z = Mathf.Atan2(normalized.y, normalized.x) * 57.29578f;
    this.handle.localEulerAngles = new Vector3(0.0f, 0.0f, z);
    if (this.onClick == null)
      return;
    this.onClick.Invoke(z);
  }

  [Serializable]
  public class OnClick : UnityEvent<float>
  {
  }

  [Serializable]
  public class OnOther : UnityEvent
  {
  }
}
