

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
