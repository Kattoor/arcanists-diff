// Decompiled with JetBrains decompiler
// Type: ColorImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (Image))]
public class ColorImage : MonoBehaviour
{
  public ColorPicker picker;
  private Image image;

  private void Awake()
  {
    this.image = this.GetComponent<Image>();
    this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
  }

  private void OnDestroy()
  {
    this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
  }

  public void ColorChanged(Color newColor) => this.image.color = newColor;
}
