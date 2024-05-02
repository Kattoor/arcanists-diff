// Decompiled with JetBrains decompiler
// Type: AimNapalm
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AimNapalm : MonoBehaviour
{
  public List<Transform> childern;

  private void Update()
  {
    Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.localEulerAngles.z * -1f);
    for (int index = 0; index < this.childern.Count; ++index)
      this.childern[index].rotation = quaternion;
  }
}
