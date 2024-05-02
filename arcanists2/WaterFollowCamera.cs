

using UnityEngine;

#nullable disable
public class WaterFollowCamera : MonoBehaviour
{
  private Vector3 pos = Vector3.zero;

  private void Start()
  {
  }

  private void LateUpdate()
  {
    if (Client.game?.map != null)
      this.pos.x = (float) (Client.game.map.Width / 2);
    this.transform.position = this.pos;
  }
}
