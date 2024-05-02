// Decompiled with JetBrains decompiler
// Type: AnimateTrollHead
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimateTrollHead : MonoBehaviour
{
  internal ZEffector effector;
  public Transform head;
  private Vector3 curRot = Vector3.zero;
  private bool animating;
  private float t;
  private bool down = true;

  private void Start()
  {
  }

  private void Update()
  {
    if ((ZComponent) this.effector == (object) null)
      return;
    if (this.animating)
    {
      if (!this.effector.active)
      {
        this.Stop();
      }
      else
      {
        if (this.down)
        {
          this.t += Time.deltaTime * 3f;
          if ((double) this.t >= 1.0)
          {
            this.t = 1f;
            this.down = false;
          }
        }
        else
        {
          this.t -= Time.deltaTime * 3f;
          if ((double) this.t <= 0.0)
          {
            this.t = 0.0f;
            this.down = true;
          }
        }
        this.curRot.z = Mathf.SmoothStep(5f, -10f, this.t);
        this.head.localEulerAngles = this.curRot;
      }
    }
    else
    {
      if (!this.effector.active)
        return;
      this.animating = true;
    }
  }

  private void Stop()
  {
    this.animating = false;
    this.curRot.z = 0.0f;
    this.head.localEulerAngles = this.curRot;
  }
}
