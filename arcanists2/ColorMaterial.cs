// Decompiled with JetBrains decompiler
// Type: ColorMaterial
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
