

using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ColorPickerTester : MonoBehaviour
{
  public Renderer renderer;
  public ColorPicker picker;
  private MaterialPropertyBlock mpb;

  private void Start()
  {
    this.picker.onValueChanged.AddListener((UnityAction<Color>) (color => this.renderer.material.color = color));
    this.renderer.material.color = this.picker.CurrentColor;
  }
}
