// Decompiled with JetBrains decompiler
// Type: ColorPickerTester
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
