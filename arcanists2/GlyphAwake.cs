// Decompiled with JetBrains decompiler
// Type: GlyphAwake
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
