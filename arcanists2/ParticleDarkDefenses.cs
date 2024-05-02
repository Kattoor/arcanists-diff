// Decompiled with JetBrains decompiler
// Type: ParticleDarkDefenses
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ParticleDarkDefenses : MonoBehaviour
{
  public Creature c;

  private void Update()
  {
    if (!((Object) this.c == (Object) null) && this.c.serverObj.hasDarkDefenses)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
