// Decompiled with JetBrains decompiler
// Type: ParticleFusion
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
