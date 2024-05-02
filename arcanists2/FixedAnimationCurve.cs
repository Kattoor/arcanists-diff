// Decompiled with JetBrains decompiler
// Type: FixedAnimationCurve
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
