

using UnityEngine;

#nullable disable
public class ParticleRotate : MonoBehaviour
{
  public float speed = 10f;
  private Vector3 a = Vector3.zero;

  private void Update()
  {
    this.a.z += Time.deltaTime * this.speed;
    this.transform.localEulerAngles = this.a;
  }
}
