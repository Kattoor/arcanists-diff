// Decompiled with JetBrains decompiler
// Type: AnimateMythEffector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
