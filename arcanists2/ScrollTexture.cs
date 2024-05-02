// Decompiled with JetBrains decompiler
// Type: ScrollTexture
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ScrollTexture : MonoBehaviour
{
  public float uvAnimationRate;
  public Material m;
  private Vector2 uvOffset = Vector2.zero;

  private void Awake()
  {
  }

  private void LateUpdate()
  {
    this.uvOffset.x -= this.uvAnimationRate * Time.deltaTime;
    if ((double) this.uvOffset.x < -1.0)
      ++this.uvOffset.x;
    this.m.SetTextureOffset("_MainTex", this.uvOffset);
  }
}
