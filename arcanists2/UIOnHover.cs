

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class UIOnHover : 
  UIOnHoverChild,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler,
  IPointerEnterHandler,
  IPointerExitHandler
{
  public bool interactable = true;
  public bool OnEnterExitWhenDisabled;
  public bool onEnterIsRightClick;
  public bool rightAsLeftWhenDisabled;
  public UIOnHover.OnClick onClick;
  public UIOnHover.OnClick onRightClick;
  public UIOnHover.OnClick onEnter;
  public UIOnHover.OnClick onExit;
  private UIOnHoverChild[] uiChildern;
  private bool isHovering;
  private bool isDown;
  private bool alwaysOn;
  public AudioClip audioOver;
  public AudioClip audioClick;
  private bool _init;
  private float holdTime;

  public bool IsHovering => this.gameObject.activeInHierarchy && this.isHovering;

  public void InitializeOnClick(UnityAction call)
  {
    this.onClick = new UIOnHover.OnClick();
    this.onClick.AddListener(call);
  }

  private void Awake()
  {
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
      uiOnHoverChild.ToggleInteractable(this.interactable, this.alwaysOn || this.isHovering, this.alwaysOn);
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

  public void ForceUpdate()
  {
    if (!this.interactable)
      this.I();
    else if (this.isHovering)
      this.E();
    else
      this.X();
  }

  public void FindChildern()
  {
    this.uiChildern = this.GetComponentsInChildren<UIOnHoverChild>(true);
  }

  public void Intitialize()
  {
    if (this._init)
      return;
    this._init = true;
    this.FindChildern();
  }

  public bool AlwaysOnNoCallback
  {
    set => this.alwaysOn = value;
  }

  public bool AlwaysOn
  {
    get => this.alwaysOn;
    set
    {
      this.alwaysOn = value;
      if (value)
        this.D();
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

  private IEnumerator MobileHold()
  {
    while (this.isDown)
    {
      yield return (object) new WaitForEndOfFrame();
      this.holdTime += Time.deltaTime;
      if ((double) this.holdTime >= 0.5)
      {
        if (this.onRightClick == null)
          break;
        this.onRightClick.Invoke();
        if (!(bool) (UnityEngine.Object) this.audioClick)
          break;
        AudioManager.Play(this.audioClick);
        break;
      }
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    this.holdTime = 0.0f;
    if (!this.interactable)
    {
      if (!this.rightAsLeftWhenDisabled || this.onRightClick == null)
        return;
      this.onRightClick?.Invoke();
    }
    else if (eventData.button == PointerEventData.InputButton.Left)
    {
      this.isDown = true;
      if (!this.alwaysOn)
        this.D();
      if (Input.touchCount > 0)
        this.StartCoroutine(this.MobileHold());
      if (Input.touchSupported)
        return;
      if (this.onClick != null)
        this.onClick.Invoke();
      if (!(bool) (UnityEngine.Object) this.audioClick)
        return;
      AudioManager.Play(this.audioClick);
    }
    else
    {
      if (eventData.button != PointerEventData.InputButton.Right)
        return;
      if (this.onEnterIsRightClick && this.onEnter != null)
      {
        if (this.onEnter != null)
          this.onEnter.Invoke();
        if (!(bool) (UnityEngine.Object) this.audioClick)
          return;
        AudioManager.Play(this.audioClick);
      }
      else
      {
        if (this.onRightClick == null)
          return;
        this.onRightClick.Invoke();
        if (!(bool) (UnityEngine.Object) this.audioClick)
          return;
        AudioManager.Play(this.audioClick);
      }
    }
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (!this.interactable)
    {
      this.isDown = false;
    }
    else
    {
      if (eventData.button != PointerEventData.InputButton.Left)
        return;
      this.isDown = false;
      if (Input.touchSupported)
      {
        if ((double) this.holdTime > 1.0 && (this.onRightClick != null && !this.onRightClick.IsNull() || this.onEnterIsRightClick))
        {
          if (this.onEnterIsRightClick)
            this.onEnter?.Invoke();
          else
            this.onRightClick?.Invoke();
        }
        else
        {
          if (this.onClick != null)
            this.onClick.Invoke();
          if ((bool) (UnityEngine.Object) this.audioClick)
            AudioManager.Play(this.audioClick);
        }
      }
      int num = eventData.dragging ? 1 : 0;
      if (!this.interactable || this.alwaysOn)
        return;
      if (this.isHovering)
        this.U();
      else
        this.X();
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.isHovering = true;
    if (!this.interactable)
    {
      if (!this.OnEnterExitWhenDisabled || this.onEnterIsRightClick || this.onEnter == null)
        return;
      this.onEnter.Invoke();
    }
    else
    {
      if (this.onEnter != null && !this.onEnterIsRightClick)
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
      if (!this.OnEnterExitWhenDisabled || this.onExit == null)
        return;
      this.onExit.Invoke();
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

  private void OnApplicationFocus(bool v)
  {
    if (!this.isDown)
      return;
    this.isDown = false;
    if (!this.interactable || this.alwaysOn)
      return;
    this.X();
  }

  private void U()
  {
    this.Intitialize();
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnUp(this.alwaysOn);
    }
  }

  private void D()
  {
    this.Intitialize();
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnDown(this.alwaysOn);
    }
  }

  private void X()
  {
    this.Intitialize();
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnExit(this.alwaysOn);
    }
  }

  private void E()
  {
    this.Intitialize();
    if (this.isHovering && (bool) (UnityEngine.Object) this.audioOver)
      AudioManager.Play(this.audioOver);
    foreach (UIOnHoverChild uiOnHoverChild in this.uiChildern)
    {
      if ((UnityEngine.Object) uiOnHoverChild != (UnityEngine.Object) null)
        uiOnHoverChild.OnEnter(this.alwaysOn);
    }
  }

  private void I()
  {
    this.Intitialize();
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

  [Serializable]
  public class OnClick : UnityEvent
  {
  }
}
