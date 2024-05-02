// Decompiled with JetBrains decompiler
// Type: Catalogue
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public abstract class Catalogue : MonoBehaviour
{
  public virtual void Kill()
  {
    this.StopAllCoroutines();
    Object.Destroy((Object) this.gameObject);
  }

  public IEnumerator SlowKill()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    Catalogue catalogue = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    Object.Destroy((Object) catalogue.gameObject);
    return false;
  }
}
