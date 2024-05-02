

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class FixedAnimationCurve
{
  public AnimationCurve curve;
  public EditorFixedInt[] fixedValues = new EditorFixedInt[100];

  public void Apply()
  {
    for (int index = 0; index < 100; ++index)
      this.fixedValues[index] = (EditorFixedInt) this.curve.Evaluate((float) index / 100f);
  }

  public FixedInt Evaluate(FixedInt x)
  {
    int index = (int) (x * 104857600L);
    if (index >= this.fixedValues.Length)
      return (FixedInt) this.fixedValues[this.fixedValues.Length - 1];
    return index < 0 ? (FixedInt) this.fixedValues[0] : (FixedInt) this.fixedValues[index];
  }
}
