// Decompiled with JetBrains decompiler
// Type: LerpUpDown
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class LerpUpDown : MonoBehaviour
{
  private float cur = 0.5f;
  private bool down = true;
  public Vector3 top = new Vector3(0.0f, 40f, 0.0f);
  public Vector3 bottom = new Vector3(0.0f, -40f, 0.0f);

  public void Update()
  {
    if (this.down)
    {
      this.cur += Time.deltaTime * 1.25f;
      if ((double) this.cur >= 1.0)
      {
        this.cur = 1f;
        this.down = false;
      }
    }
    else
    {
      this.cur -= Time.deltaTime * 1.25f;
      if ((double) this.cur <= 0.0)
      {
        this.cur = 0.0f;
        this.down = true;
      }
    }
    this.transform.localPosition = Vector3.Slerp(this.top, this.bottom, this.cur);
  }
}
