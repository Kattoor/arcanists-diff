// Decompiled with JetBrains decompiler
// Type: TwiddleBob
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwiddleBob : MonoBehaviour
{
  public float speed = 1f;
  public float maxY = 5f;
  public float minY;
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
    this.transform.localPosition = this.transform.localPosition with
    {
      y = Mathf.Lerp(this.minY, this.maxY, this.curTime)
    };
  }
}
