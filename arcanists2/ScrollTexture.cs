// Decompiled with JetBrains decompiler
// Type: ScrollTexture
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
