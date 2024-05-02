// Decompiled with JetBrains decompiler
// Type: AnimLance
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimLance : MonoBehaviour
{
  private IAnimator c;
  private int state;
  private float cur;

  private void Start() => this.c = this.GetComponentInParent<IAnimator>();

  private void Update()
  {
    if (!((Object) this.c != (Object) null))
      return;
    if (this.state == 0)
    {
      if (!this.Moving())
        return;
      this.state = 1;
    }
    else if (this.state == 1)
    {
      if (!this.Moving())
      {
        this.state = 2;
      }
      else
      {
        if ((double) this.cur >= 1.0)
          return;
        this.cur += Time.deltaTime * 10f;
        if ((double) this.cur > 1.0)
          this.cur = 1f;
        this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) (-(double) this.cur * 90.0));
      }
    }
    else
    {
      if (this.state != 2)
        return;
      if (this.Moving())
      {
        this.state = 1;
      }
      else
      {
        if ((double) this.cur <= 0.0)
          return;
        this.cur -= Time.deltaTime * 10f;
        if ((double) this.cur < 0.0)
        {
          this.cur = 0.0f;
          this.state = 0;
        }
        this.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) (-(double) this.cur * 90.0));
      }
    }
  }

  private bool Moving() => this.c.currentState == AnimateState.Walk;
}
