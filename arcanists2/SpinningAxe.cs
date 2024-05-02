

using UnityEngine;

#nullable disable
public class SpinningAxe : MonoBehaviour
{
  public Transform parentT;
  public Transform realAxe;
  public float _speed = 1000f;
  private Vector3 startPos = Vector3.zero;
  private Vector3 target = Vector3.zero;
  private Vector3 angle = Vector3.zero;
  private float lerpT;

  public void StartSpinning(Vector3 target)
  {
    this.target = target;
    this.lerpT = 0.0f;
    this.startPos = this.transform.localPosition;
    if (this.gameObject.activeSelf)
      return;
    this.angle.z = 0.0f;
    this.transform.localPosition = this.realAxe.localPosition;
    this.realAxe.localEulerAngles = Vector3.zero;
    this.realAxe.gameObject.SetActive(false);
    this.gameObject.SetActive(true);
  }

  public void StopSpinning()
  {
    this.realAxe.gameObject.SetActive(true);
    this.gameObject.SetActive(false);
  }

  private void LateUpdate()
  {
    this.angle.z += this._speed * Time.deltaTime * this.parentT.localScale.x;
    while ((double) this.angle.z < -360.0)
      this.angle.z += 360f;
    while ((double) this.angle.z > 360.0)
      this.angle.z -= 360f;
    this.transform.localEulerAngles = this.angle;
    if ((double) this.lerpT < 1.0)
    {
      this.lerpT += Time.deltaTime * 4f;
      if ((double) this.lerpT > 1.0)
        this.lerpT = 1f;
      this.transform.position = Vector3.Lerp(this.startPos, this.target, this.lerpT) + this.parentT.position;
    }
    else
      this.transform.position = this.target + this.parentT.position;
  }
}
