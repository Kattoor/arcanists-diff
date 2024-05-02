// Decompiled with JetBrains decompiler
// Type: AnimateMythEffector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimateMythEffector : MonoBehaviour
{
  public ZEffector effector;
  public GameObject color;

  private void Update()
  {
    if (ZComponent.IsNull((ZComponent) this.effector) || this.effector.dead)
      return;
    if (this.effector.active && !this.color.activeSelf)
    {
      this.color.SetActive(true);
    }
    else
    {
      if (this.effector.active || !this.color.activeSelf)
        return;
      this.color.SetActive(false);
    }
  }
}
