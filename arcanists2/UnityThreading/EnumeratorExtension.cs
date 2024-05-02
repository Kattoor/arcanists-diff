// Decompiled with JetBrains decompiler
// Type: UnityThreading.EnumeratorExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;

#nullable disable
namespace UnityThreading
{
  public static class EnumeratorExtension
  {
    public static Task RunAsync(this IEnumerator that)
    {
      return that.RunAsync(UnityThreadHelper.TaskDistributor);
    }

    public static Task RunAsync(this IEnumerator that, TaskDistributor target)
    {
      return target.Dispatch(Task.Create(that));
    }
  }
}
