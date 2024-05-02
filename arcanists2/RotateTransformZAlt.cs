

using UnityEngine;

#nullable disable
public class RotateTransformZAlt : MonoBehaviour, IClientOnly
{
  public float speed;
  public AnimationCurve curve;
  private Vector3 rot;

  private void Update()
  {
    this.rot.z += this.speed * this.curve.Evaluate(this.rot.z / 360f) * Time.deltaTime;
    this.transform.localEulerAngles = this.rot;
    if ((double) this.rot.z < 360.0)
      return;
    this.rot.z -= 360f;
  }
}
