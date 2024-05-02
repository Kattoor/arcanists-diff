// Decompiled with JetBrains decompiler
// Type: ColorLabel
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
[RequireComponent(typeof (TextMeshProUGUI))]
public class ColorLabel : MonoBehaviour
{
  public ColorPicker picker;
  public ColorValues type;
  public string prefix = "R: ";
  public float minValue;
  public float maxValue = (float) byte.MaxValue;
  public int precision;
  private TextMeshProUGUI label;

  private void Awake() => this.label = this.GetComponent<TextMeshProUGUI>();

  private void OnEnable()
  {
    if (!Application.isPlaying || !((Object) this.picker != (Object) null))
      return;
    this.picker.onValueChanged.AddListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.AddListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void OnDestroy()
  {
    if (!((Object) this.picker != (Object) null))
      return;
    this.picker.onValueChanged.RemoveListener(new UnityAction<Color>(this.ColorChanged));
    this.picker.onHSVChanged.RemoveListener(new UnityAction<float, float, float>(this.HSVChanged));
  }

  private void ColorChanged(Color color) => this.UpdateValue();

  private void HSVChanged(float hue, float sateration, float value) => this.UpdateValue();

  private void UpdateValue()
  {
    if ((Object) this.picker == (Object) null)
      this.label.text = this.prefix + "-";
    else
      this.label.text = this.prefix + this.ConvertToDisplayString(this.minValue + this.picker.GetValue(this.type) * (this.maxValue - this.minValue));
  }

  private string ConvertToDisplayString(float value)
  {
    return this.precision > 0 ? value.ToString("f " + (object) this.precision) : Mathf.FloorToInt(value).ToString();
  }
}
