// Decompiled with JetBrains decompiler
// Type: ArchWorkout
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ArchWorkout : MonoBehaviour
{
  public Vector3 startPos;
  private Vector3 endPos;
  private Vector3 small = new Vector3(0.9f, 0.9f, 1f);
  internal IAnimator animator;
  internal ZCreature creature;
  private float cur;
  private float pause;
  private bool down;

  private void Start()
  {
    this.startPos = this.transform.localPosition;
    this.endPos = this.startPos + new Vector3(0.0f, 7f);
  }

  private void Update()
  {
    if ((Object) this.animator == (Object) null || this.animator.currentState != AnimateState.Stop || this.creature.isMoving)
    {
      if ((double) this.pause >= 1.0)
        return;
      this.down = false;
      this.cur = 0.0f;
      this.pause = 1f;
    }
    else if ((double) this.pause > 0.0)
    {
      this.pause -= Time.deltaTime;
    }
    else
    {
      if (this.down)
      {
        this.cur -= Time.deltaTime * 2f;
        if ((double) this.cur <= 0.0)
        {
          this.cur = 0.0f;
          this.down = false;
        }
      }
      else
      {
        this.cur += Time.deltaTime * 2f;
        if ((double) this.cur >= 1.0)
        {
          this.cur = 1f;
          this.down = true;
        }
      }
      this.transform.localPosition = Vector3.Lerp(this.startPos, this.endPos, Mathf.SmoothStep(0.0f, 1f, this.cur));
      this.transform.localScale = Vector3.Lerp(Vector3.one, this.small, Mathf.SmoothStep(0.0f, 1f, this.cur));
    }
  }
}
