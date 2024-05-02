

using UnityEngine;

#nullable disable
public class WaterFollowCamera : MonoBehaviour
{
  private Transform _cam;
  private Vector3 pos = Vector3.zero;

  private void Start() => this._cam = Camera.main.transform;

  private void LateUpdate()
  {
    this.pos.x = this._cam.position.x;
    this.transform.position = this.pos;
  }
}
