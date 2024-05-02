

using UnityEngine;

#nullable disable
public class AnimateRepeat : MonoBehaviour
{
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  private float curTime;
  private float timeBetweenFrames;
  private int index;
  private SpriteRenderer sp;
  internal bool deleteOnDestroy;

  public SpriteRenderer GetSpriteRenderer => this.sp ?? this.GetComponent<SpriteRenderer>();

  private void Start()
  {
    this.sp = this.GetComponent<SpriteRenderer>();
    if ((Object) this.sp == (Object) null)
      this.enabled = false;
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
  }

  private void OnDestroy()
  {
    if (!this.deleteOnDestroy)
      return;
    foreach (Sprite sprite in this.sprites)
    {
      if ((Object) sprite != (Object) null)
        Global.DestroySprite(sprite);
    }
  }

  internal void UpdateFPS(float f)
  {
    this.timeToFinish = (float) this.sprites.Length / f;
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
  }

  private void Update()
  {
    this.curTime += Time.deltaTime;
    if ((double) this.curTime <= (double) this.timeBetweenFrames)
      return;
    this.curTime = 0.0f;
    ++this.index;
    if (this.index >= this.sprites.Length)
      this.index = 0;
    this.sp.sprite = this.sprites[this.index];
  }
}
