// Decompiled with JetBrains decompiler
// Type: AnimateWave
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
  public Transform _cam;

  private void Start()
  {
    this._cam = Camera.main.transform;
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
    float num = this.lastCamX - this._cam.position.x;
    this.curOffsetX += Time.deltaTime * this.scrollSpeed;
    this.curOffsetX -= num;
    while ((double) this.curOffsetX > 64.0)
      this.curOffsetX -= 64f;
    while ((double) this.curOffsetX < -64.0)
      this.curOffsetX += 64f;
    for (int index = 0; index < this.rend.Length; ++index)
      this.rend[index].transform.position = new Vector3(-this.curOffsetX + (float) (index * 32) + this._cam.position.x, (float) (index * 8) + this.curOffsetY + this.offsetY, 0.0f);
    this.lastCamX = this._cam.position.x;
  }
}
