

using UnityEngine;

#nullable disable
public class RotateTransformX : MonoBehaviour
{
  public float speed;
  private Vector3 rot;

  private void Update()
  {
    this.rot.x += this.speed * Time.deltaTime;
    this.transform.localEulerAngles = this.rot;
  }
}
