// Decompiled with JetBrains decompiler
// Type: GlowingMiddle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class GlowingMiddle : MonoBehaviour
{
  private float ts;
  private Color tColor = Color.white;
  public float speed = 1f;
  public float minScale = 0.5f;
  public float maxScale = 1f;
  public Image image;

  private void Update()
  {
    this.ts += this.speed * Time.deltaTime;
    this.tColor.a = Mathf.Lerp(this.maxScale, this.minScale, Mathf.Abs(Mathf.Sin(this.ts)));
    this.image.color = this.tColor;
  }
}
