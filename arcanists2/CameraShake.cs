// Decompiled with JetBrains decompiler
// Type: CameraShake
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CameraShake : MonoBehaviour
{
  public Vector3 Amount = new Vector3(1f, 1f, 0.0f);
  public float Duration = 1f;
  public float Speed = 10f;
  public AnimationCurve Curve = AnimationCurve.EaseInOut(0.0f, 1f, 1f, 0.0f);
  public bool DeltaMovement = true;
  protected Camera Camera;
  public float time;
  protected Vector3 lastPos;
  protected Vector3 nextPos;
  protected bool destroyAfterPlay;
  public static CameraShake instance;

  private void Awake()
  {
    this.Camera = this.GetComponent<Camera>();
    CameraShake.instance = this;
  }

  public static void ShakeOnce(
    float duration = 1f,
    float speed = 10f,
    Vector3? amount = null,
    Camera camera = null,
    bool deltaMovement = true,
    AnimationCurve curve = null)
  {
    CameraShake cameraShake = ((Object) camera != (Object) null ? (Component) camera : (Component) Camera.main).gameObject.AddComponent<CameraShake>();
    cameraShake.Duration = duration;
    cameraShake.Speed = speed;
    if (amount.HasValue)
      cameraShake.Amount = amount.Value;
    if (curve != null)
      cameraShake.Curve = curve;
    cameraShake.DeltaMovement = deltaMovement;
    cameraShake.destroyAfterPlay = true;
    cameraShake.Shake();
  }

  public void Shake(float intensity = 0.7f, float dur = 1f)
  {
    this.Amount = new Vector3(intensity, intensity, 0.0f);
    this.Duration = dur;
    this.ResetCam();
    this.time = this.Duration;
  }

  private void LateUpdate()
  {
    if ((double) this.time <= 0.0)
      return;
    this.time -= Time.deltaTime;
    if ((double) this.time > 0.0)
    {
      this.nextPos = (Mathf.PerlinNoise(this.time * this.Speed, (float) ((double) this.time * (double) this.Speed * 2.0)) - 0.5f) * this.Amount.x * this.transform.right * this.Curve.Evaluate((float) (1.0 - (double) this.time / (double) this.Duration)) + (Mathf.PerlinNoise((float) ((double) this.time * (double) this.Speed * 2.0), this.time * this.Speed) - 0.5f) * this.Amount.y * this.transform.up * this.Curve.Evaluate((float) (1.0 - (double) this.time / (double) this.Duration));
      this.Camera.transform.parent.Translate(this.DeltaMovement ? this.nextPos - this.lastPos : this.nextPos);
      this.lastPos = this.nextPos;
    }
    else
    {
      this.ResetCam();
      if (!this.destroyAfterPlay)
        return;
      Object.Destroy((Object) this);
    }
  }

  private void ResetCam()
  {
    this.Camera.transform.parent.position = Vector3.zero;
    this.lastPos = this.nextPos = Vector3.zero;
  }
}
