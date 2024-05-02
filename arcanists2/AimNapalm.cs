// Decompiled with JetBrains decompiler
// Type: AimNapalm
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
