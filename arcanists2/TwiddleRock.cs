// Decompiled with JetBrains decompiler
// Type: TwiddleRock
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwiddleRock : MonoBehaviour
{
  public float speed = 1f;
  public float maxZ = 5f;
  public float minZ;
  private float curTime;
  private bool down;

  private void Update()
  {
    if (this.down)
    {
      this.curTime -= Time.deltaTime * this.speed;
      if ((double) this.curTime <= 0.0)
      {
        this.curTime = 0.0f;
        this.down = false;
      }
    }
    else
    {
      if ((double) this.curTime >= 1.0)
      {
        this.curTime = 1f;
        this.down = true;
      }
      this.curTime += Time.deltaTime * this.speed;
    }
    this.transform.localEulerAngles = this.transform.localEulerAngles with
    {
      z = Mathf.SmoothStep(this.minZ, this.maxZ, this.curTime)
    };
  }
}
