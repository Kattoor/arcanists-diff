// Decompiled with JetBrains decompiler
// Type: ParticleFusion
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleFusion : MonoBehaviour
{
  internal ZCreature creature;

  private void Update()
  {
    if (!((ZComponent) this.creature == (object) null) && this.creature.fusion > 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
