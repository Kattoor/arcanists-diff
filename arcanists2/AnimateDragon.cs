// Decompiled with JetBrains decompiler
// Type: AnimateDragon
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimateDragon : IAnimator
{
  public SpriteRenderer sp;
  public float timeToFinish = 1f;
  public Sprite[] sprites;
  public Sprite[] attackSprites;
  public float boost = 2f;
  private float curTime;
  private float timeBetweenFrames;
  private int index;
  private float boosting;
  public Sprite[] choose;

  public override void Play(AnimateState anim, float duration = 0.0f, bool sound = true)
  {
    if (!Client.game.isClient || Client.game.resyncing)
      return;
    if (anim == AnimateState.Attack)
    {
      if (this.attackSprites.Length == 0)
        return;
      this.curTime = 0.0f;
      this.boosting = 0.0f;
      this.choose = this.attackSprites;
      this.index = 0;
    }
    else
    {
      this.boosting = 0.5f;
      this.choose = this.sprites;
    }
    this.duration = duration;
    this.currentState = anim;
  }

  private void Start()
  {
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
    this.choose = this.sprites;
  }

  private void Update()
  {
    if (Client.game == null || !Client.game.isClient || Client.game.resyncing)
      return;
    if (this.currentState == AnimateState.Stop)
      this.curTime += Time.deltaTime;
    else if (this.currentState != AnimateState.Attack)
    {
      this.curTime += Time.deltaTime * this.boost;
      this.boosting -= Time.deltaTime;
      if ((double) this.boosting <= 0.0)
      {
        this.currentState = AnimateState.Stop;
        this.choose = this.sprites;
      }
    }
    else
    {
      this.curTime += Time.deltaTime * 2f;
      this.boosting += Time.deltaTime * 2f;
      if ((double) this.boosting >= 0.89999997615814209)
      {
        this.currentState = AnimateState.Stop;
        this.choose = this.sprites;
        this.index = 0;
      }
    }
    if ((double) this.curTime <= (double) this.timeBetweenFrames)
      return;
    this.curTime = 0.0f;
    ++this.index;
    if (this.index >= this.choose.Length)
      this.index = 0;
    this.sp.sprite = this.choose[this.index];
  }
}
