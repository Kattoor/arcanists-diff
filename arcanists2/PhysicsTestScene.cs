

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PhysicsTestScene : MonoBehaviour
{
  public List<GameObject> towers = new List<GameObject>();
  public CircleCollider2D circle;
  public ZMyWorld world = new ZMyWorld();
}
