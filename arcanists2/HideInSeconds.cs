// Decompiled with JetBrains decompiler
// Type: HideInSeconds
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class HideInSeconds : MonoBehaviour
{
  public float seconds = 0.2f;
  private float cur;

  private void Update()
  {
    this.cur += Time.deltaTime;
    if ((double) this.cur <= (double) this.seconds)
      return;
    this.gameObject.SetActive(false);
    this.cur = 0.0f;
  }
}
