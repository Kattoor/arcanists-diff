// Decompiled with JetBrains decompiler
// Type: SpinningSword
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
