

using System;
using UnityEngine;

#nullable disable
public class StormCloud : MonoBehaviour
{
  public StormCloud.CloudUpdate[] clouds;
  private bool back;
  private float t;
  public float speed = 1f;

  private void Start()
  {
    for (int index = 0; index < this.clouds.Length; ++index)
    {
      this.clouds[index].startPos = this.clouds[index].cloud.localPosition;
      this.clouds[index].endPos = this.clouds[index].startPos;
      this.clouds[index].endPos.x = -this.clouds[index].endPos.x;
    }
  }

  private void Update()
  {
    if (this.back)
    {
      this.t -= Time.deltaTime * this.speed;
      if ((double) this.t <= 0.0)
        this.back = false;
    }
    else
    {
      this.t += Time.deltaTime * this.speed;
      if ((double) this.t > 1.0)
        this.back = true;
    }
    for (int index = 0; index < this.clouds.Length; ++index)
    {
      float x = Mathf.SmoothStep(this.clouds[index].startPos.x, this.clouds[index].endPos.x, this.t);
      this.clouds[index].cloud.localPosition = new Vector3(x, this.clouds[index].startPos.y);
    }
  }

  [Serializable]
  public class CloudUpdate
  {
    public Transform cloud;
    [NonSerialized]
    public Vector3 startPos;
    [NonSerialized]
    public Vector3 endPos;
  }
}
