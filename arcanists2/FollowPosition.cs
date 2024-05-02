// Decompiled with JetBrains decompiler
// Type: FollowPosition
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FollowPosition : IFollowTarget
{
  private Vector3 s;
  public float time = -1f;

  public FollowPosition(Vector2 p)
  {
    this.s = (Vector3) p;
    this.time = Time.realtimeSinceStartup + (Client.game.isReplay ? 1f : 3f);
  }

  public Vector3 GetTarget() => this.s;

  public bool Check() => (double) this.time >= (double) Time.realtimeSinceStartup;
}
