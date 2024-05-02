// Decompiled with JetBrains decompiler
// Type: MovementEffects.MECExtensionMethods
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
