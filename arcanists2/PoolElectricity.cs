// Decompiled with JetBrains decompiler
// Type: PoolElectricity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class PoolElectricity
{
  public GameObject o;
  private List<ParticleElectricity> pool2 = new List<ParticleElectricity>();

  public void Spawn(Vector3 start, Vector3 end)
  {
    for (int index = 0; index < this.pool2.Count; ++index)
    {
      if (!this.pool2[index].gameObject.activeInHierarchy)
      {
        this.pool2[index].Setup(start, end);
        return;
      }
    }
    ParticleElectricity component = UnityEngine.Object.Instantiate<GameObject>(this.o).GetComponent<ParticleElectricity>();
    this.pool2.Add(component);
    component.Setup(start, end);
  }

  public void Clear()
  {
    foreach (Component component in this.pool2)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.pool2.Clear();
  }
}
