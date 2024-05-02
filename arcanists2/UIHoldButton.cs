// Decompiled with JetBrains decompiler
// Type: UIHoldButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MovementEffects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Image))]
public class UIHoldButton : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler
{
  internal Color normalColor;
  public Color colorDown;
  public UIHoldButton.OnClick onDown;
  public UIHoldButton.OnClick onUp;
  public UIHoldButton.OnClick onHold;
  private Image image;
  private bool isDown;
  private IEnumerator<float> looper;
  public AudioClip audioClick;

  private void Awake()
  {
    this.image = this.GetComponent<Image>();
    this.normalColor = this.image.color;
  }

  private void OnDisable()
  {
    this.isDown = false;
    this.image.color = this.normalColor;
  }

  private IEnumerator<float> Loop()
  {
    while (this.isDown)
    {
      yield return 0.0f;
      this.onHold?.Invoke();
    }
    this.looper = (IEnumerator<float>) null;
  }

  private void StartLoop()
  {
    if (this.looper != null)
      return;
    this.looper = Timing.RunCoroutine(this.Loop());
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    if (eventData.button != PointerEventData.InputButton.Left)
      return;
    this.isDown = true;
    this.image.color = this.colorDown;
    this.onDown?.Invoke();
    if (this.onHold != null)
      this.StartLoop();
    if (!(bool) (UnityEngine.Object) this.audioClick)
      return;
    AudioManager.Play(this.audioClick);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    if (eventData.button != PointerEventData.InputButton.Left)
      return;
    this.isDown = false;
    this.image.color = this.normalColor;
    Timing.KillCoroutines(this.looper);
    this.onUp?.Invoke();
  }

  [Serializable]
  public class OnClick : UnityEvent
  {
  }
}
