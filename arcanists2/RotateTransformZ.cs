

using UnityEngine;

#nullable disable
public class RotateTransformZ : MonoBehaviour, IClientOnly
{
  public float speed;
  private Vector3 rot;

  private void Update()
  {
    this.rot.z += this.speed * Time.deltaTime;
    this.transform.localEulerAngles = this.rot;
  }
}
