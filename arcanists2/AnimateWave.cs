

using UnityEngine;

#nullable disable
public class AnimateWave : MonoBehaviour
{
  public float scrollSpeed = 32f;
  public float bobSpeed = 32f;
  public float stopSpeed = 1f;
  public float minY = -32f;
  public float maxY = 32f;
  public float offsetY;
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  private float curTime;
  private float curOffsetX;
  private float curOffsetY;
  private float lastCamX;
  private float timeBetweenFrames;
  private int index;
  private bool down = true;
  private bool stop;
  private float curStop;
  public SpriteRenderer[] rend;

  private void Start()
  {
    this.minY -= 8f;
    this.maxY -= 8f;
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
  }

  private void Update()
  {
    this.curTime += Time.deltaTime;
    if ((double) this.curTime > (double) this.timeBetweenFrames)
    {
      this.curTime = 0.0f;
      ++this.index;
      if (this.index >= this.sprites.Length)
        this.index = 0;
      for (int index = 0; index < this.rend.Length; ++index)
        this.rend[index].sprite = this.sprites[this.index];
    }
    if (this.down)
    {
      this.curOffsetY -= Time.deltaTime * this.bobSpeed;
      if ((double) this.curOffsetY <= (double) this.minY)
      {
        this.curOffsetY = this.minY;
        this.down = false;
      }
    }
    else if (this.stop)
    {
      this.curStop += Time.deltaTime * this.stopSpeed;
      if ((double) this.curStop >= (double) this.maxY)
      {
        this.curStop = 0.0f;
        this.stop = false;
      }
    }
    else
    {
      this.curOffsetY += Time.deltaTime * this.bobSpeed;
      if ((double) this.curOffsetY >= (double) this.maxY)
      {
        this.curOffsetY = this.maxY;
        this.down = true;
        this.stop = true;
      }
    }
    this.curOffsetX += Time.deltaTime * this.scrollSpeed;
    while ((double) this.curOffsetX > 64.0)
      this.curOffsetX -= 64f;
    while ((double) this.curOffsetX < -64.0)
      this.curOffsetX += 64f;
    for (int index = 0; index < this.rend.Length; ++index)
      this.rend[index].transform.position = new Vector3(-this.curOffsetX + (float) (index * 32) + (float) ((Client.game?.map?.Width ?? 0) / 2), (float) (index * 8) + this.curOffsetY + this.offsetY, 0.0f);
  }
}
