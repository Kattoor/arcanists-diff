

using UnityEngine;

#nullable disable
public class BigBubble : MonoBehaviour
{
  public ZCreature creature;
  private Vector3 _target;
  private Vector3 _start;
  private float cur = 1f;
  public float speed = 1f;

  private void Update()
  {
    if (!((ZComponent) this.creature == (object) null) && this.creature.parent.localTurn < 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
