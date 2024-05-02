// Decompiled with JetBrains decompiler
// Type: PresentColor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PresentColor : MonoBehaviour
{
  public SpriteRenderer main;
  public SpriteRenderer second;

  private void Awake()
  {
    this.main.color = ColorHSV.ToColor(new ColorHSV(Random.Range(0.0f, 1f), 1f, 1f));
    this.second.color = ColorHSV.ToColor(new ColorHSV(Random.Range(0.0f, 1f), 1f, 1f));
  }
}
