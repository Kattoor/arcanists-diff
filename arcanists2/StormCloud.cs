// Decompiled with JetBrains decompiler
// Type: StormCloud
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
