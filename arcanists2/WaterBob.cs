

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WaterBob : MonoBehaviour
{
  public float dist = -5f;
  public float max;
  public float speed = 0.5f;
  public List<Transform> _t;
  private List<Vector3> _start;
  private bool down = true;
  private float cur;
  private bool isActive;
  private Creature c;

  private void Start()
  {
    this.c = this.GetComponent<Creature>();
    this._start = new List<Vector3>(this._t.Count);
    for (int index = 0; index < this._t.Count; ++index)
      this._start.Add(this._t[index].localPosition);
  }

  private void LateUpdate()
  {
    if ((Object) this.c == (Object) null || this.c.serverObj.isDead || (Object) this.c.animator == (Object) null || this.c.animator.currentState != AnimateState.Stop)
      return;
    if (this.isActive)
    {
      if (this.c.position.y > this.c.radius + 1)
      {
        this.isActive = false;
        for (int index = 0; index < this._t.Count; ++index)
          this._t[index].localPosition = this._start[index];
        this.cur = 0.0f;
        this.down = true;
        return;
      }
    }
    else
    {
      if (!(this.c.position.y <= this.c.radius + 1))
        return;
      this.isActive = true;
    }
    if (this.down)
    {
      this.cur += Time.deltaTime * this.speed;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.down = false;
      }
    }
    else
    {
      this.cur -= Time.deltaTime * this.speed;
      if ((double) this.cur <= 0.0)
      {
        this.cur = 0.0f;
        this.down = true;
      }
    }
    Vector3 zero = Vector3.zero with
    {
      y = Mathf.Lerp(this.max, this.dist, Mathf.SmoothStep(0.0f, 1f, this.cur))
    };
    for (int index = 0; index < this._t.Count; ++index)
      this._t[index].localPosition = this._start[index] + zero;
  }
}
