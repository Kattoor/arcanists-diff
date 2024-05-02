

using UnityEngine;

#nullable disable
public class SpinningSword : MonoBehaviour
{
  public float _speed = 600f;
  private Vector3 a = Vector3.zero;

  private void Update()
  {
    this.a.z -= Time.deltaTime * this._speed;
    if ((double) this.a.z <= -360.0)
    {
      this.a.z = 0.0f;
      this.enabled = false;
    }
    this.transform.localEulerAngles = this.a;
  }
}
