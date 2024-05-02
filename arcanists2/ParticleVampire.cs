

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

  private void Start()
  {
    this.target = this.GetComponentInParent<Creature>();
    this.x = this.target?.serverObj;
  }

  private void Update()
  {
    if (!(bool) (UnityEngine.Object) this.target || !(bool) (UnityEngine.Object) this.target.transform || !((UnityEngine.Object) CharacterCreation.Instance == (UnityEngine.Object) null) || !(this.lastPos != this.target.position))
      return;
    this.lastPos = this.target.position;
    Coords start = new Coords((int) this.x.position.x, (int) this.x.position.y);
    Coords end = new Coords((int) this.x.position.x, this.x.map.Height);
    Coords coords = Client.map.bresenhamsLineCast(start, end, this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 100, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 100, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 25, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 25, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 50, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 50, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 75, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 75, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 125, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 125, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x - 150, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
      coords = Client.map.bresenhamsLineCast(start, new Coords((int) this.x.position.x + 150, this.x.map.Height), this.x, (ZSpell) null, Inert.mask_entity_movement);
    if (coords != null)
    {
      if (this.last || (double) this.spriteRenderer.color.a <= 0.0)
        return;
      this.last = true;
      this.StopAllCoroutines();
      this.StartCoroutine(this.Fade(0.0f));
    }
    else
    {
      if (!this.last || (double) this.spriteRenderer.color.a >= 1.0)
        return;
      this.last = false;
      this.StopAllCoroutines();
      this.StartCoroutine(this.Fade(1f));
    }
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
