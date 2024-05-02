// Decompiled with JetBrains decompiler
// Type: AnimatePlayer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class AnimatePlayer : IAnimator
{
  public bool playAudio = true;
  public Transform leftHand;
  public Transform rightHand;
  public Transform leftFoot;
  public Transform rightFoot;
  public float walkSpeed = 20f;
  public float jumpSpeed = 8f;
  public float stopSpeed = 16f;
  public float distUpHand = 2f;
  public float distUpFoot = 4f;
  public float distDownHand = 2f;
  public float distDownFoot = 6f;
  public float footRadiusX = 7f;
  public float footRadiusY = 4f;
  public float handRadius = 2f;
  private Vector3 startLeftHand;
  private Vector3 startRightHand;
  private Vector3 startLeftFoot;
  private Vector3 startRightFoot;
  private Vector3 jointLeftHand;
  private Vector3 jointRightHand;
  private Vector3 jointLeftFoot;
  private Vector3 jointRightFoot;
  private Vector3 jumpUpLeftHand;
  private Vector3 jumpUpLeftFoot;
  private Vector3 jumpUpRightHand;
  private Vector3 jumpUpRightFoot;
  private Vector3 jumpDownLeftHand;
  private Vector3 jumpDownLeftFoot;
  private Vector3 jumpDownRightHand;
  private Vector3 jumpDownRightFoot;
  private Coroutine coroutine;
  [NonSerialized]
  public ZCreature creature;
  private float nextWalkSound;

  public override void ResetLeftHand()
  {
    this.startLeftHand = this.leftHand.localPosition;
    this.jointLeftHand = this.leftHand.localPosition;
    this.jumpUpLeftHand = this.leftHand.localPosition + new Vector3(0.0f, this.distUpHand, 0.0f);
    this.jumpDownLeftHand = this.leftHand.localPosition - new Vector3(0.0f, this.distDownHand, 0.0f);
  }

  public override void ResetRightHand()
  {
    this.startRightHand = this.rightHand.localPosition;
    this.jointRightHand = this.rightHand.localPosition;
    this.jumpUpRightHand = this.rightHand.localPosition + new Vector3(0.0f, this.distUpHand, 0.0f);
    this.jumpDownRightHand = this.rightHand.localPosition - new Vector3(0.0f, this.distDownHand, 0.0f);
  }

  public override void ResetRightFoot()
  {
  }

  public override void ResetLeftFoot()
  {
  }

  private void Awake()
  {
    this.startLeftHand = this.leftHand.localPosition;
    this.startRightHand = this.rightHand.localPosition;
    this.startLeftFoot = this.leftFoot.localPosition;
    this.startRightFoot = this.rightFoot.localPosition;
    this.jointLeftHand = this.leftHand.localPosition;
    this.jointRightHand = this.rightHand.localPosition;
    this.jointLeftFoot = this.leftFoot.localPosition;
    this.jointRightFoot = this.rightFoot.localPosition;
    this.jumpUpLeftHand = this.leftHand.localPosition + new Vector3(0.0f, this.distUpHand, 0.0f);
    this.jumpUpRightHand = this.rightHand.localPosition + new Vector3(0.0f, this.distUpHand, 0.0f);
    this.jumpUpLeftFoot = this.leftFoot.localPosition + new Vector3(0.0f, this.distUpFoot, 0.0f);
    this.jumpUpRightFoot = this.rightFoot.localPosition + new Vector3(0.0f, this.distUpFoot, 0.0f);
    this.jumpDownLeftHand = this.leftHand.localPosition - new Vector3(0.0f, this.distDownHand, 0.0f);
    this.jumpDownRightHand = this.rightHand.localPosition - new Vector3(0.0f, this.distDownHand, 0.0f);
    this.jumpDownLeftFoot = this.leftFoot.localPosition - new Vector3(0.0f, this.distDownFoot, 0.0f);
    this.jumpDownRightFoot = this.rightFoot.localPosition - new Vector3(0.0f, this.distDownFoot, 0.0f);
    this.jointLeftHand.x += this.handRadius;
    this.jointRightHand.x -= this.handRadius;
  }

  public override void Play(AnimateState anim, float duration = 0.0f, bool sound = true)
  {
    if (Client.game == null || !Client.game.isClient || Client.game.resyncing || !ZComponent.IsNull((ZComponent) this.creature) && this.creature.parent != null && (UnityEngine.Object) this.creature.parent.torquing != (UnityEngine.Object) null && (ZComponent) this.creature.parent.torquing.creature == (object) this.creature || (ZComponent) this.creature != (object) null && this.creature.flying && !this.creature.entangledOrGravity)
      return;
    if (anim == AnimateState.Swing)
    {
      this.StartCoroutine(this.Swing());
    }
    else
    {
      this.duration = duration;
      if (this.currentState == anim)
        return;
      if (this.playAudio)
        this.playAudio = sound;
      this.currentState = anim;
      if (this.coroutine != null)
        this.StopCoroutine(this.coroutine);
      this.nextWalkSound = 0.0f;
      switch (anim)
      {
        case AnimateState.Walk:
          byte? indexBody = this.creature?.parent?.settingsPlayer.indexBody;
          int? nullable = indexBody.HasValue ? new int?((int) indexBody.GetValueOrDefault()) : new int?();
          int num = 142;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          {
            ZCreature creature = this.creature;
            if ((creature != null ? (!creature.isPawn ? 1 : 0) : 0) != 0)
              goto label_13;
          }
          if (!((UnityEngine.Object) CharacterCreation.Instance != (UnityEngine.Object) null) || CharacterCreation.Instance.settingsPlayer.indexBody != (byte) 142)
          {
            this.coroutine = this.StartCoroutine(this.Walk());
            break;
          }
label_13:
          this.coroutine = this.StartCoroutine(this.Skate());
          break;
        case AnimateState.Jump:
          this.coroutine = this.StartCoroutine(this.Jump());
          break;
        case AnimateState.Fall:
          this.coroutine = this.StartCoroutine(this.Fall());
          break;
        default:
          this.coroutine = this.StartCoroutine(this.Stop());
          break;
      }
    }
  }

  private IEnumerator Swing()
  {
    AnimatePlayer animatePlayer = this;
    float t = 0.0f;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * 2f;
      animatePlayer.leftHand.localEulerAngles = new Vector3(0.0f, 0.0f, -360f * t);
      yield return (object) new WaitForEndOfFrame();
    }
    animatePlayer.leftHand.localEulerAngles = Vector3.zero;
    animatePlayer.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Walk()
  {
    AnimatePlayer animatePlayer = this;
    float t = 0.0f;
    while ((double) animatePlayer.duration > 0.0)
    {
      t += Time.deltaTime * animatePlayer.walkSpeed;
      if ((double) t > (double) animatePlayer.nextWalkSound)
      {
        if (animatePlayer.playAudio && (UnityEngine.Object) AudioManager.instance != (UnityEngine.Object) null && (UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null)
          AudioManager.Play(ZComponent.IsNull((ZComponent) animatePlayer.creature) || !animatePlayer.creature.moneyBags || animatePlayer.creature.audioBags ? (!((ZComponent) animatePlayer.creature != (object) null) || animatePlayer.creature.sprinting == 0 ? AudioManager.instance.footSteps : AudioManager.instance.runSteps) : AudioManager.instance.moneySteps);
        animatePlayer.nextWalkSound = t + 10f;
      }
      animatePlayer.leftFoot.localPosition = new Vector3(animatePlayer.jointLeftFoot.x + Mathf.Sin(t) * animatePlayer.footRadiusX, animatePlayer.jointLeftFoot.y + Mathf.Max(0.0f, Mathf.Cos(t) * animatePlayer.footRadiusY));
      animatePlayer.rightFoot.localPosition = new Vector3(animatePlayer.jointRightFoot.x + Mathf.Sin(t + 180f) * animatePlayer.footRadiusX, animatePlayer.jointRightFoot.y + Mathf.Max(0.0f, Mathf.Cos(t + 180f) * animatePlayer.footRadiusY));
      animatePlayer.leftHand.localPosition = new Vector3(animatePlayer.jointLeftHand.x + Mathf.Sin(t + 180f) * animatePlayer.handRadius, animatePlayer.jointLeftHand.y + Mathf.Cos(t + 180f) * animatePlayer.handRadius);
      animatePlayer.rightHand.localPosition = new Vector3(animatePlayer.jointRightHand.x + Mathf.Sin(t) * animatePlayer.handRadius, animatePlayer.jointRightHand.y + Mathf.Cos(t) * animatePlayer.handRadius);
      animatePlayer.duration -= Time.deltaTime;
      yield return (object) new WaitForEndOfFrame();
    }
    animatePlayer.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Skate()
  {
    AnimatePlayer animatePlayer = this;
    float t = 0.0f;
    while ((double) animatePlayer.duration > 0.0)
    {
      t += (float) ((double) Time.deltaTime * (double) animatePlayer.walkSpeed / 2.0);
      if ((double) t > (double) animatePlayer.nextWalkSound)
      {
        if (animatePlayer.playAudio && (UnityEngine.Object) AudioManager.instance != (UnityEngine.Object) null && (UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null)
          AudioManager.Play(ZComponent.IsNull((ZComponent) animatePlayer.creature) || !animatePlayer.creature.moneyBags || animatePlayer.creature.audioBags ? (!((ZComponent) animatePlayer.creature != (object) null) || animatePlayer.creature.sprinting == 0 ? AudioManager.instance.footSteps : AudioManager.instance.runSteps) : AudioManager.instance.moneySteps);
        animatePlayer.nextWalkSound = t + 10f;
      }
      animatePlayer.leftFoot.localPosition = new Vector3((float) ((double) Mathf.Sin(t) * (double) animatePlayer.footRadiusX - (double) animatePlayer.footRadiusX / 2.0), animatePlayer.startLeftFoot.y);
      animatePlayer.rightFoot.localPosition = new Vector3((float) (-((double) Mathf.Sin(t) * (double) animatePlayer.footRadiusX) + (double) animatePlayer.footRadiusX / 2.0), animatePlayer.startRightFoot.y);
      animatePlayer.leftHand.localPosition = new Vector3(animatePlayer.startLeftHand.x + Mathf.Sin(t) * animatePlayer.handRadius, animatePlayer.startLeftHand.y);
      animatePlayer.rightHand.localPosition = new Vector3(animatePlayer.startRightHand.x - Mathf.Sin(t) * animatePlayer.handRadius, animatePlayer.startRightHand.y);
      animatePlayer.duration -= Time.deltaTime;
      yield return (object) new WaitForEndOfFrame();
    }
    animatePlayer.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Jump()
  {
    AnimatePlayer animatePlayer = this;
    float t = 0.0f;
    Vector3 slh = animatePlayer.leftHand.localPosition;
    Vector3 srh = animatePlayer.rightHand.localPosition;
    Vector3 slf = animatePlayer.leftFoot.localPosition;
    Vector3 srf = animatePlayer.rightFoot.localPosition;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * animatePlayer.jumpSpeed;
      animatePlayer.leftHand.localPosition = Vector3.Lerp(slh, animatePlayer.jumpDownLeftHand, t);
      animatePlayer.rightHand.localPosition = Vector3.Lerp(srh, animatePlayer.jumpDownRightHand, t);
      animatePlayer.leftFoot.localPosition = Vector3.Lerp(slf, animatePlayer.jumpDownLeftFoot, t);
      animatePlayer.rightFoot.localPosition = Vector3.Lerp(srf, animatePlayer.jumpDownRightFoot, t);
      yield return (object) new WaitForEndOfFrame();
    }
    animatePlayer.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Fall()
  {
    AnimatePlayer animatePlayer = this;
    float t = 0.0f;
    Vector3 slh = animatePlayer.leftHand.localPosition;
    Vector3 srh = animatePlayer.rightHand.localPosition;
    Vector3 slf = animatePlayer.leftFoot.localPosition;
    Vector3 srf = animatePlayer.rightFoot.localPosition;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * animatePlayer.jumpSpeed;
      animatePlayer.leftHand.localPosition = Vector3.Lerp(slh, animatePlayer.jumpUpLeftHand, t);
      animatePlayer.rightHand.localPosition = Vector3.Lerp(srh, animatePlayer.jumpUpRightHand, t);
      animatePlayer.leftFoot.localPosition = Vector3.Lerp(slf, animatePlayer.jumpUpLeftFoot, t);
      animatePlayer.rightFoot.localPosition = Vector3.Lerp(srf, animatePlayer.jumpUpRightFoot, t);
      yield return (object) new WaitForEndOfFrame();
    }
    animatePlayer.Play(AnimateState.Stop, 0.0f, true);
  }

  private IEnumerator Stop()
  {
    float t = 0.0f;
    Vector3 slh = this.leftHand.localPosition;
    Vector3 srh = this.rightHand.localPosition;
    Vector3 slf = this.leftFoot.localPosition;
    Vector3 srf = this.rightFoot.localPosition;
    while ((double) t < 1.0)
    {
      t += Time.deltaTime * this.stopSpeed;
      this.leftHand.localPosition = Vector3.Lerp(slh, this.startLeftHand, t);
      this.rightHand.localPosition = Vector3.Lerp(srh, this.startRightHand, t);
      this.leftFoot.localPosition = Vector3.Lerp(slf, this.startLeftFoot, t);
      this.rightFoot.localPosition = Vector3.Lerp(srf, this.startRightFoot, t);
      yield return (object) new WaitForEndOfFrame();
    }
    this.leftHand.localPosition = this.startLeftHand;
    this.rightHand.localPosition = this.startRightHand;
    this.leftFoot.localPosition = this.startLeftFoot;
    this.rightFoot.localPosition = this.startRightFoot;
  }
}
