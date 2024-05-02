

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ParticleVampire : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  [NonSerialized]
  public Creature target;
  public ZCreature x;
  private MyLocation lastPos;
  private bool last = true;
  private int last2;
  private float cur;

  private void Start()
  {
    this.target = this.GetComponentInParent<Creature>();
    this.x = this.target?.serverObj;
  }

  private void Update()
  {
    if ((bool) (UnityEngine.Object) this.target && (ZComponent) this.x != (object) null && !this.x.isDead && (bool) (UnityEngine.Object) this.target.transform && (UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null && (this.lastPos != this.target.position || this.last2 != this.x.game.numExplosionsAndMovement || (double) this.cur > 3.0))
    {
      this.cur = 0.0f;
      this.lastPos = this.target.position;
      this.last2 = this.x.game.numExplosionsAndMovement;
      if (!this.x.InSunlight())
      {
        if (!this.last && (double) this.spriteRenderer.color.a > 0.0)
        {
          this.last = true;
          this.StopAllCoroutines();
          this.StartCoroutine(this.Fade(0.0f));
        }
      }
      else if (this.last && (double) this.spriteRenderer.color.a < 1.0)
      {
        this.last = false;
        this.StopAllCoroutines();
        this.StartCoroutine(this.Fade(1f));
      }
    }
    this.cur += Time.deltaTime;
  }

  private IEnumerator Fade(float f)
  {
    Color color = this.spriteRenderer.color;
    if ((double) color.a > (double) f)
    {
      while ((double) color.a > (double) f)
      {
        color.a -= Time.deltaTime;
        if ((double) color.a < 0.0)
          color.a = 0.0f;
        this.spriteRenderer.color = color;
        yield return (object) null;
      }
    }
    else
    {
      while ((double) color.a < (double) f)
      {
        color.a += Time.deltaTime;
        if ((double) color.a > 1.0)
          color.a = 1f;
        this.spriteRenderer.color = color;
        yield return (object) null;
      }
    }
    color.a = f;
    this.spriteRenderer.color = color;
  }
}
