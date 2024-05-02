// Decompiled with JetBrains decompiler
// Type: PresentColor
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
