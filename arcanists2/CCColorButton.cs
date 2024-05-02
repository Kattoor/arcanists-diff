

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
