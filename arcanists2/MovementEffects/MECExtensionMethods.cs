

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace MovementEffects
{
  public static class MECExtensionMethods
  {
    public static IEnumerator<float> CancelWith(
      this IEnumerator<float> coroutine,
      GameObject gameObject)
    {
      while ((bool) (Object) gameObject && gameObject.activeInHierarchy && coroutine.MoveNext())
        yield return coroutine.Current;
    }

    public static IEnumerator<float> CancelWith(
      this IEnumerator<float> coroutine,
      GameObject gameObject1,
      GameObject gameObject2)
    {
      while ((bool) (Object) gameObject1 && gameObject1.activeInHierarchy && (bool) (Object) gameObject2 && gameObject2.activeInHierarchy && coroutine.MoveNext())
        yield return coroutine.Current;
    }

    public static IEnumerator<float> CancelWith(
      this IEnumerator<float> coroutine,
      GameObject gameObject1,
      GameObject gameObject2,
      GameObject gameObject3)
    {
      while ((bool) (Object) gameObject1 && gameObject1.activeInHierarchy && (bool) (Object) gameObject2 && gameObject2.activeInHierarchy && (bool) (Object) gameObject3 && gameObject3.activeInHierarchy && coroutine.MoveNext())
        yield return coroutine.Current;
    }
  }
}
