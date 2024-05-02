// Decompiled with JetBrains decompiler
// Type: IAnimator
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class IAnimator : MonoBehaviour
{
  public AnimateState currentState;
  [NonSerialized]
  public float duration = float.MaxValue;

  public virtual void Play(AnimateState anim, float duration = 0.0f, bool sound = true)
  {
    throw new Exception("Play not implemented in IAnimator.cs");
  }

  public virtual void ResetLeftHand()
  {
  }

  public virtual void ResetRightHand()
  {
  }

  public virtual void ResetRightFoot()
  {
  }

  public virtual void ResetLeftFoot()
  {
  }
}
