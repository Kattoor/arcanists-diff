

using UnityEngine;

#nullable disable
public class AnimateExplosioin : MonoBehaviour
{
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  private float curTime;
  private float timeBetweenFrames;
  private int index;
  private SpriteRenderer sp;

  private void Start()
  {
    this.sp = this.GetComponent<SpriteRenderer>();
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
  }

  private void Update()
  {
    if ((Object) this.sp == (Object) null)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      this.curTime += Time.deltaTime;
      if ((double) this.curTime <= (double) this.timeBetweenFrames)
        return;
      this.curTime = 0.0f;
      ++this.index;
      if (this.index >= this.sprites.Length)
        Object.Destroy((Object) this.gameObject);
      else
        this.sp.sprite = this.sprites[this.index];
    }
  }
}
