// Decompiled with JetBrains decompiler
// Type: FollowEntity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FollowEntity : IFollowTarget
{
  private ZEntity s;
  private float time = -1f;

  public FollowEntity(ZEntity e) => this.s = e;

  public Vector3 GetTarget()
  {
    return ZComponent.IsNull((ZComponent) this.s) || (Object) this.s.transform == (Object) null ? new Vector3(0.0f, -1f, 1f) : this.s.transform.position;
  }

  public bool Check()
  {
    if (Client.game.serverState.busy == ServerState.Busy.No)
      return false;
    if ((double) this.time < 0.0)
    {
      if (ZComponent.IsNull((ZComponent) this.s) || !this.s.isMoving)
        return false;
      this.time = Time.realtimeSinceStartup + (Client.game.isReplay ? 0.1f : 1f);
      return true;
    }
    if ((double) this.time < (double) Time.realtimeSinceStartup)
      return false;
    if (!ZComponent.IsNull((ZComponent) this.s) && this.s.isMoving)
      this.time = Time.realtimeSinceStartup + (Client.game.isReplay ? 0.1f : 1f);
    return true;
  }
}
