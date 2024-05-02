

using UnityEngine;

#nullable disable
public interface IFollowTarget
{
  Vector3 GetTarget();

  bool Check();
}
