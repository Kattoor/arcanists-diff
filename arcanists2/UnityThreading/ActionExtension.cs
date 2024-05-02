

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
