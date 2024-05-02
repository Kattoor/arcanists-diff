// Decompiled with JetBrains decompiler
// Type: AnimateLava
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimateLava : MonoBehaviour
{
  public float scrollSpeed = 32f;
  public float stopSpeed = 1f;
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  private float curTime;
  private float curOffsetX;
  private float lastCamX;
  private float timeBetweenFrames;
  private int index;
  public SpriteRenderer rend;

  private void Start() => this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;

  private void Update()
  {
    this.curTime += Time.deltaTime;
    if ((double) this.curTime > (double) this.timeBetweenFrames)
    {
      this.curTime = 0.0f;
      ++this.index;
      if (this.index >= this.sprites.Length)
        this.index = 0;
      this.rend.sprite = this.sprites[this.index];
    }
    this.curOffsetX += Time.deltaTime * this.scrollSpeed;
    while ((double) this.curOffsetX > 64.0)
      this.curOffsetX -= 64f;
    while ((double) this.curOffsetX < -64.0)
      this.curOffsetX += 64f;
    this.rend.transform.position = new Vector3(-this.curOffsetX + (float) ((Client.game?.map?.Width ?? 0) / 2), this.rend.transform.position.y);
  }
}
