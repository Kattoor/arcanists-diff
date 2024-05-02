// Decompiled with JetBrains decompiler
// Type: UnityThreading.ActionExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace UnityThreading
{
  public static class ActionExtension
  {
    public static Task RunAsync(this Action that)
    {
      return that.RunAsync(UnityThreadHelper.TaskDistributor);
    }

    public static Task RunAsync(this Action that, TaskDistributor target) => target.Dispatch(that);

    public static Task<T> RunAsync<T>(this Func<T> that)
    {
      return that.RunAsync<T>(UnityThreadHelper.TaskDistributor);
    }

    public static Task<T> RunAsync<T>(this Func<T> that, TaskDistributor target)
    {
      return target.Dispatch<T>(that);
    }

    public static Task<T> AsTask<T>(this Func<T> that) => new Task<T>(that);
  }
}
