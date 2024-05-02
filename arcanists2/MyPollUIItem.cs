// Decompiled with JetBrains decompiler
// Type: MyPollUIItem
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MyPolling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MyPollUIItem : MonoBehaviour
{
  public RectTransform rect;
  public Image checkbox;
  public UIOnHover toggle;
  public TMP_Text text;
  public TMP_InputField input;
  public GameObject objCheck;
  private int myIndex;

  public void Setup(int index, MyPoll.Answer item)
  {
    this.myIndex = index;
    this.text.text = item.text;
    this.input.text = item.userInput;
    this.objCheck.SetActive(item.isChecked);
    this.toggle.gameObject.SetActive(item.checkable);
    this.input.gameObject.SetActive(item.allowUserInput);
    this.gameObject.SetActive(true);
  }

  public void OnClick() => MyPollUI.Instance.SelectItem(this.myIndex);

  public void OnEdit(string s) => MyPollUI.Instance.OnEdit(this.myIndex, s);
}
