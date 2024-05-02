// Decompiled with JetBrains decompiler
// Type: AnimateUIExplosion
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class AnimateUIExplosion : MonoBehaviour
{
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  private float curTime;
  private float timeBetweenFrames;
  private int index;
  public Image sp;

  private void Start() => this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;

  private void Update()
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
