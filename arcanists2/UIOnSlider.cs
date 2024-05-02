// Decompiled with JetBrains decompiler
// Type: UIOnSlider
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class UIOnSlider : 
  UIOnHoverChild,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler,
  IPointerEnterHandler,
  IPointerExitHandler,
  IDragHandler,
  IEndDragHandler
{
  public RectTransform handle;
  public float minHandleX;
  public float maxHandleX;
  public bool interactable = true;
  public bool OnEnterExitWhenDisabled;
  public UIOnSlider.OnClick onClick;
  public UIOnSlider.OnClick onPointerUp;
  public UIOnSlider.OnOther onEnter;
  public UIOnSlider.OnOther onExit;
  private UIOnHoverChild[] uiChildern;
  private bool isHovering;
  private bool isDown;
  private bool alwaysOn;
  public AudioClip audioOver;
  public AudioClip audioClick;
  private RectTransform rectTransform;

  public bool IsHovering => this.gameObject.activeInHierarchy && this.isHovering;

  private void Awake()
  {
    this.rectTransform = (RectTransform) this.transform;
    if (this.useColorTint || this.useSpriteSwap)
      this.targetRenderer = this.GetComponent<Image>();
    if (this.useTextColor || this.useTextResize)
      this.text = this.GetComponent<Text>();
    if (!this.useTMPTextColor)
      return;
    this.tmpText = this.GetComponent<TMP_Text>();
  }

  private void OnEnable()
  {
    this.FindChildern();
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      uiOnHoverChild.Init();
      uiOnHoverChild.ToggleInteractable(this.interactable, this.AlwaysOn || this.isHovering, this.alwaysOn);
    }
  }

  private void OnDisable()
  {
    this.isHovering = false;
    this.isDown = false;
  }

  public void MakeSureUpdated()
  {
    if (!this.interactable)
      return;
    if (this.isHovering)
      this.E();
    else
      this.X();
  }

  public void FindChildern() => this.uiChildern = this.GetComponentsInChildren<UIOnHoverChild>();

  public bool AlwaysOn
  {
    get => this.alwaysOn;
    set
    {
      this.alwaysOn = value;
      if (value)
        this.E();
      else if (!this.interactable)
        this.I();
      else if (this.isHovering)
        this.E();
      else
        this.X();
    }
  }

  public void Interactable(bool v)
  {
    if (this.interactable == v)
      return;
    this.interactable = v;
    if (this.uiChildern == null)
      this.FindChildern();
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.ToggleInteractable(v, this.isHovering, this.alwaysOn);
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (!this.interactable || eventData.button != PointerEventData.InputButton.Left)
      return;
    this.isDown = true;
    this.CallBack((Vector3) eventData.position);
    if (this.alwaysOn)
      return;
    this.D();
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (!this.interactable || eventData.button != PointerEventData.InputButton.Left)
      return;
    this.isDown = false;
    this.onPointerUp?.Invoke(0.0f);
    if (!eventData.dragging)
      this.CallBack((Vector3) eventData.position);
    if (!this.interactable || this.alwaysOn)
      return;
    if (this.isHovering)
      this.U();
    else
      this.X();
  }

  public void OnDrag(PointerEventData eventData) => this.CallBack((Vector3) eventData.position);

  public void OnEndDrag(PointerEventData eventData) => this.CallBack((Vector3) eventData.position);

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.isHovering = true;
    if (!this.interactable)
    {
      if (!this.OnEnterExitWhenDisabled)
        return;
      if (this.onEnter != null)
        this.onEnter.Invoke();
      if (this.alwaysOn)
        return;
      this.E();
    }
    else
    {
      if (this.onEnter != null)
        this.onEnter.Invoke();
      if (this.isDown || this.alwaysOn)
        return;
      this.E();
    }
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.isHovering = false;
    if (!this.interactable)
    {
      if (!this.OnEnterExitWhenDisabled)
        return;
      if (this.onExit != null)
        this.onExit.Invoke();
      if (this.alwaysOn)
        return;
      this.X();
    }
    else
    {
      if (this.onExit != null)
        this.onExit.Invoke();
      if (this.isDown || this.alwaysOn)
        return;
      this.X();
    }
  }

  private void U()
  {
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnUp();
    }
  }

  private void D()
  {
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnDown();
    }
  }

  private void X()
  {
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnExit();
    }
  }

  private void E()
  {
    if (this.isHovering && (bool) (UnityEngine.Object) this.audioOver)
      AudioManager.Play(this.audioOver);
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnEnter();
    }
  }

  private void I()
  {
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.ToggleInteractable(this.interactable, this.isHovering, this.alwaysOn);
    }
  }

  public void EditorUpdate()
  {
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern = this.GetComponentsInChildren<UIOnHoverChild>())
      uiOnHoverChild.ToggleInteractable(this.interactable, false, this.alwaysOn);
  }

  public void SetValue(float f)
  {
    this.handle.anchoredPosition = Vector2.zero with
    {
      x = Mathf.Lerp(this.minHandleX, this.maxHandleX, f)
    };
  }

  private void CallBack(Vector3 eventData)
  {
    Vector2 localPoint = Vector2.zero;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, (Vector2) eventData, (Camera) null, out localPoint))
    {
      localPoint.x = Mathf.Clamp(localPoint.x, this.minHandleX, this.maxHandleX);
      localPoint.y = 0.0f;
      this.handle.anchoredPosition = localPoint;
      if (this.onClick == null)
        return;
      this.onClick.Invoke(Mathf.InverseLerp(this.minHandleX, this.maxHandleX, localPoint.x));
    }
    else
    {
      localPoint.x = Mathf.Clamp(localPoint.x, this.minHandleX, this.maxHandleX);
      localPoint.y = 0.0f;
      this.handle.anchoredPosition = localPoint;
      if (this.onClick == null)
        return;
      this.onClick.Invoke(0.0f);
    }
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
