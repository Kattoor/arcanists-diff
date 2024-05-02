

using System;
using System.Collections.Generic;

#nullable disable
namespace UnityThreading
{
  public static class EnumerableExtension
  {
    public static IEnumerable<Task> ParallelForEach<T>(this IEnumerable<T> that, Action<T> action)
    {
      return that.ParallelForEach<T>(action, (TaskDistributor) null);
    }

    public static IEnumerable<Task> ParallelForEach<T>(
      this IEnumerable<T> that,
      Action<T> action,
      TaskDistributor target)
    {
      return (IEnumerable<Task>) that.ParallelForEach<Task.Unit, T>((Func<T, Task.Unit>) (element =>
      {
        action(element);
        return new Task.Unit();
      }), target);
    }

    public static IEnumerable<Task<TResult>> ParallelForEach<TResult, T>(
      this IEnumerable<T> that,
      Func<T, TResult> action)
    {
      return that.ParallelForEach<TResult, T>(action);
    }

    public static IEnumerable<Task<TResult>> ParallelForEach<TResult, T>(
      this IEnumerable<T> that,
      Func<T, TResult> action,
      TaskDistributor target)
    {
      List<Task<TResult>> taskList = new List<Task<TResult>>();
      foreach (T obj in that)
      {
        T tmp = obj;
        Task<TResult> task = Task.Create<TResult>((Func<TResult>) (() => action(tmp))).Run((DispatcherBase) target);
        taskList.Add(task);
      }
      return (IEnumerable<Task<TResult>>) taskList;
    }

    public static IEnumerable<Task> SequentialForEach<T>(this IEnumerable<T> that, Action<T> action)
    {
      return that.SequentialForEach<T>(action, (TaskDistributor) null);
    }

    public static IEnumerable<Task> SequentialForEach<T>(
      this IEnumerable<T> that,
      Action<T> action,
      TaskDistributor target)
    {
      return (IEnumerable<Task>) that.SequentialForEach<Task.Unit, T>((Func<T, Task.Unit>) (element =>
      {
        action(element);
        return new Task.Unit();
      }), target);
    }

    public static IEnumerable<Task<TResult>> SequentialForEach<TResult, T>(
      this IEnumerable<T> that,
      Func<T, TResult> action)
    {
      return that.SequentialForEach<TResult, T>(action);
    }

    public static IEnumerable<Task<TResult>> SequentialForEach<TResult, T>(
      this IEnumerable<T> that,
      Func<T, TResult> action,
      TaskDistributor target)
    {
      List<Task<TResult>> taskList = new List<Task<TResult>>();
      Task task1 = (Task) null;
      foreach (T obj in that)
      {
        T tmp = obj;
        Task<TResult> task = Task.Create<TResult>((Func<TResult>) (() => action(tmp)));
        if (task1 == null)
          task.Run((DispatcherBase) target);
        else
          task1.WhenEnded((Action) (() => task.Run((DispatcherBase) target)));
        task1 = (Task) task;
        taskList.Add(task);
      }
      return (IEnumerable<Task<TResult>>) taskList;
    }
  }
}
