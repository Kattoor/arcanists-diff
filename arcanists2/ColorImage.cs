

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
