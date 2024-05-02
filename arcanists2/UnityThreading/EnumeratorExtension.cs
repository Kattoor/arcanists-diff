// Decompiled with JetBrains decompiler
// Type: UnityThreading.EnumeratorExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
