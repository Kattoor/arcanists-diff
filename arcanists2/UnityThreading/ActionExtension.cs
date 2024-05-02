// Decompiled with JetBrains decompiler
// Type: UnityThreading.ActionExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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

    public static Task AsTask(this Action that) => Task.Create(that);

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
