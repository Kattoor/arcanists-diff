// Decompiled with JetBrains decompiler
// Type: EmojiPicker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class EmojiPicker : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
  [SerializeField]
  private RectTransform container;
  private Action<int> action;

  public static EmojiPicker instance { get; private set; }

  private void Awake() => EmojiPicker.instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) EmojiPicker.instance == (UnityEngine.Object) this))
      return;
    EmojiPicker.instance = (EmojiPicker) null;
  }

  public static void Show(Action<int> a)
  {
    if ((UnityEngine.Object) EmojiPicker.instance == (UnityEngine.Object) null)
      Controller.CreateEmojiMenu();
    EmojiPicker.instance._Show(a);
  }

  private void _Show(Action<int> a)
  {
    this.action = a;
    this.transform.SetAsLastSibling();
    this.gameObject.SetActive(true);
  }

  public static void Close()
  {
    EmojiPicker.instance.action = (Action<int>) null;
    EmojiPicker.instance.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.F12))
      return;
    EmojiPicker.Close();
  }

  private void DebugPoint(PointerEventData ped)
  {
    Vector2 localPoint;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.container, ped.position, ped.pressEventCamera, out localPoint))
      return;
    if ((double) localPoint.x < -5.0 || (double) localPoint.y < -5.0 || (double) localPoint.x > 805.0 || (double) localPoint.y > 883.0)
    {
      EmojiPicker.Close();
    }
    else
    {
      int num = (int) Mathf.Clamp(localPoint.x / 15.6862745f, 0.0f, 51f) + 51 * (56 - (int) Mathf.Clamp(localPoint.y / 15.6785717f, 0.0f, 56f)) - 51;
      Action<int> action = this.action;
      if (action != null)
        action(num);
      EmojiPicker.Close();
    }
  }

  public void OnPointerDown(PointerEventData eventData) => this.DebugPoint(eventData);
}
