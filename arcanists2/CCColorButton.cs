// Decompiled with JetBrains decompiler
// Type: CCColorButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class CCColorButton : MonoBehaviour
{
  public UIOnHover button;
  public Image imgColor;
  public TMP_Text txtName;
  public static CCColorButton active;

  public void Activate(int index)
  {
    if ((Object) CCColorButton.active != (Object) null)
      CCColorButton.active.button.AlwaysOn = false;
    CCColorButton.active = this;
    this.button.AlwaysOn = true;
    CharacterCreation.Instance.txtPickerHeader.text = this.txtName.text;
    CharacterCreation.Instance.picker.CurrentColorNoNotify = this.imgColor.color;
    CharacterCreation.Instance.colorType = (ColorType) index;
  }
}
