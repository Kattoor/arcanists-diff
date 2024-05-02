

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
