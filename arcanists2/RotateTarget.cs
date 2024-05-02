

using UnityEngine;

#nullable disable
public class RotateTarget : MonoBehaviour
{
  private Vector3 tRotation = Vector3.zero;
  public float speed = 60f;
  public bool freezeChildern;

  private void Update()
  {
    this.tRotation.z += this.speed * Time.deltaTime;
    this.transform.localEulerAngles = this.tRotation;
    if (!this.freezeChildern || this.transform.childCount <= 0)
      return;
    for (int index = 0; index < this.transform.childCount; ++index)
      this.transform.GetChild(index).localEulerAngles = new Vector3(0.0f, 0.0f, -this.tRotation.z);
  }
}
