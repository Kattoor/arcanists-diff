

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
