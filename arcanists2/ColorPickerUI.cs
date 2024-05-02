// Decompiled with JetBrains decompiler
// Type: ColorPickerUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class ColorPickerUI : MonoBehaviour
{
  public ColorPicker picker;
  private Action<Color> onEnd;
  private Color start;

  public static ColorPickerUI Instance { get; private set; }

  public static ColorPickerUI Create(Color c, Action<Color> onEnd, Action<Color> onChange)
  {
    if ((UnityEngine.Object) ColorPickerUI.Instance != (UnityEngine.Object) null)
      return ColorPickerUI.Instance;
    ColorPickerUI.Instance = Controller.Instance.CreateAndApply<ColorPickerUI>(Controller.Instance.colorPickerUI, Controller.Instance.transform);
    ColorPickerUI.Instance.picker.CurrentColor = c;
    ColorPickerUI.Instance.onEnd = onEnd;
    ColorPickerUI.Instance.start = c;
    if (onChange != null)
      ColorPickerUI.Instance.picker.onValueChangedPUBLIC.AddListener((UnityAction<Color>) (col =>
      {
        Action<Color> action = onChange;
        if (action == null)
          return;
        action(col);
      }));
    return ColorPickerUI.Instance;
  }

  public void ClickOk()
  {
    Action<Color> onEnd = this.onEnd;
    if (onEnd != null)
      onEnd(this.picker.CurrentColor);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickCancel()
  {
    Action<Color> onEnd = this.onEnd;
    if (onEnd != null)
      onEnd(this.start);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  private void Update()
  {
    if (!((UnityEngine.Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (UnityEngine.Object) this.transform))
      return;
    this.ClickCancel();
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) ColorPickerUI.Instance == (UnityEngine.Object) this))
      return;
    ColorPickerUI.Instance = (ColorPickerUI) null;
  }
}
