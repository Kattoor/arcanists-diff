// Decompiled with JetBrains decompiler
// Type: ColorMaterial
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ColorMaterial
{
  public Material _material;
  public Color32 color;
  public ColorMaterial.Type type;

  public void Apply()
  {
    if (this.type == ColorMaterial.Type.Outline)
    {
      this._material.SetColor("_OutlineColor", (Color) this.color);
    }
    else
    {
      if (this.type != ColorMaterial.Type.Color)
        return;
      this._material.color = (Color) this.color;
    }
  }

  public ColorMaterial Copy()
  {
    return new ColorMaterial()
    {
      _material = this._material,
      color = this.color,
      type = this.type
    };
  }

  [Serializable]
  public enum Type
  {
    Outline,
    Color,
  }
}
