// Decompiled with JetBrains decompiler
// Type: GlyphAwake
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GlyphAwake : MonoBehaviour
{
  [SerializeField]
  private Sprite[] images;
  [SerializeField]
  private SpriteRenderer sprite;

  private void Awake()
  {
    int index = Client.ranomdStuff.Next(0, this.images.Length);
    if (!((Object) this.sprite != (Object) null))
      return;
    this.sprite.sprite = this.images[index];
  }
}
