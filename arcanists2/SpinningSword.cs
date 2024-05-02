// Decompiled with JetBrains decompiler
// Type: SpinningSword
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SpinningSword : MonoBehaviour
{
  public float _speed = 600f;
  private Vector3 a = Vector3.zero;

  private void Update()
  {
    this.a.z -= Time.deltaTime * this._speed;
    if ((double) this.a.z <= -360.0)
    {
      this.a.z = 0.0f;
      this.enabled = false;
    }
    this.transform.localEulerAngles = this.a;
  }
}
