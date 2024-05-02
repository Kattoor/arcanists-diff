

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
