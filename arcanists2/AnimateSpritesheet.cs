// Decompiled with JetBrains decompiler
// Type: AnimateSpritesheet
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AnimateSpritesheet : IAnimator
{
  public SpriteRenderer rend;
  public float walkSpeed;
  public float jumpSpeed;
  public Sprite[] walkFrames;
  public Sprite[] jumpFrames;
  public Sprite idleFrame;
  private float nextWalkSound;
  public bool playAudio = true;

  public override void Play(AnimateState anim, float duration = 0.0f, bool sound = true)
  {
    this.playAudio = sound;
    this.duration = duration;
    if (anim == this.currentState || (anim == AnimateState.Fall || anim == AnimateState.Stop) && this.currentState == AnimateState.Jump)
      return;
    this.currentState = anim;
    this.StopAllCoroutines();
    switch (anim)
    {
      case AnimateState.Stop:
        this.rend.sprite = this.idleFrame;
        break;
      case AnimateState.Walk:
        this.StartCoroutine(this.Walk());
        break;
      case AnimateState.Jump:
        this.StartCoroutine(this.Jump());
        break;
    }
  }

  private IEnumerator Walk()
  {
    AnimateSpritesheet animateSpritesheet = this;
    float t = 0.0f;
    float cur = 0.0f;
    int frame = 1;
    animateSpritesheet.nextWalkSound = 0.0f;
    while ((double) animateSpritesheet.duration > 0.0)
    {
      t += Time.deltaTime;
      cur += Time.deltaTime;
      if ((double) t > (double) animateSpritesheet.nextWalkSound)
      {
        if (animateSpritesheet.playAudio && (Object) AudioManager.instance != (Object) null && (Object) CharacterCreation.Instance == (Object) null)
          AudioManager.Play(AudioManager.instance.footSteps);
        animateSpritesheet.nextWalkSound = t + 10f;
      }
      if ((double) cur >= (double) animateSpritesheet.walkSpeed)
      {
        cur = 0.0f;
        ++frame;
        if (frame >= animateSpritesheet.walkFrames.Length)
          frame = 0;
        animateSpritesheet.rend.sprite = animateSpritesheet.walkFrames[frame];
      }
      animateSpritesheet.duration -= Time.deltaTime;
      yield return (object) new WaitForEndOfFrame();
    }
    animateSpritesheet.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Jump()
  {
    AnimateSpritesheet animateSpritesheet = this;
    float cur = animateSpritesheet.jumpSpeed;
    int frame = 0;
    while (true)
    {
      cur += Time.deltaTime;
      if ((double) cur >= (double) animateSpritesheet.jumpSpeed)
      {
        cur = 0.0f;
        ++frame;
        if (frame < animateSpritesheet.jumpFrames.Length)
          animateSpritesheet.rend.sprite = animateSpritesheet.jumpFrames[frame];
        else
          break;
      }
      yield return (object) new WaitForEndOfFrame();
    }
    animateSpritesheet.rend.sprite = animateSpritesheet.idleFrame;
    animateSpritesheet.currentState = AnimateState.Stop;
  }
}
