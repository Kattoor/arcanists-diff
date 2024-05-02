// Decompiled with JetBrains decompiler
// Type: CCColorButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
