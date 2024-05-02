// Decompiled with JetBrains decompiler
// Type: ButtonArrayContextmenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ButtonArrayContextmenu : MonoBehaviour
{
  public UIOnHover pfabButton;
  private int count;

  public void AddItem(string n, Action a)
  {
    ++this.count;
    UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, this.transform.GetChild(0));
    uiOnHover.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = n;
    uiOnHover.onClick.AddListener((UnityAction) (() =>
    {
      Action action = a;
      if (action == null)
        return;
      action();
    }));
    uiOnHover.onClick.AddListener((UnityAction) (() => MyContextMenu.CloseInstance()));
    uiOnHover.gameObject.SetActive(true);
    ((RectTransform) this.transform).sizeDelta = new Vector2((float) (this.count * 55 + 25), 55f);
  }
}
