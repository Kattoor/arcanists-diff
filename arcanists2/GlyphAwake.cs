

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
