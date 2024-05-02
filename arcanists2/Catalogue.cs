// Decompiled with JetBrains decompiler
// Type: Catalogue
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
