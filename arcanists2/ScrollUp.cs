// Decompiled with JetBrains decompiler
// Type: ScrollUp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ScrollUp : MonoBehaviour
{
  public RectTransform parent;
  public RectTransform rect;
  public float speed = 30f;

  private void Update()
  {
    float num = this.parent.sizeDelta.y + this.rect.sizeDelta.y * this.rect.localScale.y;
    float y = this.rect.anchoredPosition.y + Time.deltaTime * this.speed;
    if ((double) y > (double) num)
      y = -10f;
    this.rect.anchoredPosition = new Vector2(0.0f, y);
  }
}
