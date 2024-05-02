// Decompiled with JetBrains decompiler
// Type: MyMessageBox
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MyMessageBox : MonoBehaviour
{
  public Action onEnd;
  public Action onCancel;
  public TMP_Text text;
  public TMP_Text txtOk;
  public TMP_Text txtCancel;
  public Toggle doNotShow;
  public Action donotshow;
  public Image image;
  public static MyMessageBox Instance;
  public const byte TypeRejoinGame = 1;
  private float autoClose;

  public static void Create(
    string msg,
    Action onEnd,
    string ok = "Ok",
    string cancel = "Cancel",
    Action onCancel = null,
    Action doNotShow = null,
    Sprite icon = null)
  {
    MyMessageBox andApply = Controller.Instance.CreateAndApply<MyMessageBox>(Controller.Instance.messageBox, Controller.Instance.transform);
    MyMessageBox.Instance = andApply;
    andApply.text.text = msg;
    andApply.txtOk.text = ok;
    andApply.txtCancel.text = cancel;
    andApply.onEnd = onEnd;
    andApply.onCancel = onCancel;
    if (string.IsNullOrEmpty(cancel))
      andApply.txtCancel.transform.parent.gameObject.SetActive(false);
    if (string.IsNullOrEmpty(ok))
      andApply.txtOk.transform.parent.gameObject.SetActive(false);
    if ((UnityEngine.Object) icon != (UnityEngine.Object) null)
    {
      andApply.image.sprite = icon;
      andApply.image.gameObject.SetActive(true);
    }
    if (doNotShow == null)
      return;
    andApply.donotshow = doNotShow;
    andApply.doNotShow.gameObject.SetActive(true);
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MyMessageBox.Instance == (UnityEngine.Object) this))
      return;
    MyMessageBox.Instance = (MyMessageBox) null;
  }

  public void ClickOk()
  {
    this.ToggleShowAgain();
    Action onEnd = this.onEnd;
    if (onEnd != null)
      onEnd();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickCancel()
  {
    this.ToggleShowAgain();
    Action onCancel = this.onCancel;
    if (onCancel != null)
      onCancel();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ToggleShowAgain()
  {
    if (!this.doNotShow.gameObject.activeSelf || !this.doNotShow.isOn)
      return;
    Action donotshow = this.donotshow;
    if (donotshow == null)
      return;
    donotshow();
  }

  public void Update()
  {
    if (!((UnityEngine.Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (UnityEngine.Object) this.transform))
      return;
    Controller instance = Controller.Instance;
    if ((instance != null ? (!instance.IsPopupAbove() ? 1 : 0) : 1) == 0)
      return;
    this.autoClose += Time.deltaTime;
    if ((double) this.autoClose <= 2.0)
      return;
    this.ClickCancel();
  }
}
