

using UnityEngine;

#nullable disable
public class RotatingArrows : MonoBehaviour
{
  private float ts;
  private Vector3 tScale = Vector3.zero;
  private Vector3 tRotation = Vector3.zero;
  public float speed = 60f;
  public float minScale = 1f;
  public float maxScale = 1.25f;
  public float scaleSpeed = 5f;

  private void Update()
  {
    this.tRotation.z += this.speed * Time.deltaTime;
    this.ts += this.scaleSpeed * Time.deltaTime;
    float num = Mathf.Lerp(this.minScale, this.maxScale, Mathf.Abs(Mathf.Sin(this.ts)));
    this.tScale.x = num;
    this.tScale.y = num;
    this.transform.localEulerAngles = this.tRotation;
    this.transform.localScale = this.tScale;
  }
}
