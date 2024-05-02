// Decompiled with JetBrains decompiler
// Type: UnityThreading.TaskExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace UnityThreading
{
  public static class TaskExtension
  {
    public static Task WithName(this Task task, string name)
    {
      task.Name = name;
      return task;
    }

    public static Task<T> WithName<T>(this Task<T> task, string name)
    {
      task.Name = name;
      return task;
    }

    public static void WaitAll(this IEnumerable<Task> tasks)
    {
      foreach (Task task in tasks)
        task.Wait();
    }

    public static IEnumerable<Task> Then(
      this IEnumerable<Task> that,
      Task followingTask,
      DispatcherBase target)
    {
      int remaining = that.Count<Task>();
      object syncRoot = new object();
      foreach (Task task in that)
      {
        task.WhenFailed((Action) (() =>
        {
          if (followingTask.ShouldAbort)
            return;
          followingTask.Abort();
        }));
        task.WhenSucceeded((Action) (() =>
        {
          if (followingTask.ShouldAbort)
            return;
          lock (syncRoot)
          {
            --remaining;
            if (remaining != 0)
              return;
            if (target != null)
              followingTask.Run(target);
            else if (ThreadBase.CurrentThread is TaskWorker)
              followingTask.Run((DispatcherBase) ((TaskWorker) ThreadBase.CurrentThread).TaskDistributor);
            else
              followingTask.Run();
          }
        }));
      }
      return that;
    }

    public static IEnumerable<Task> WhenSucceeded(
      this IEnumerable<Task> that,
      Action action,
      DispatcherBase target)
    {
      int remaining = that.Count<Task>();
      object syncRoot = new object();
      foreach (Task task in that)
        task.WhenSucceeded((Action) (() =>
        {
          lock (syncRoot)
          {
            --remaining;
            if (remaining != 0)
              return;
            if (target == null)
              action();
            else
              target.Dispatch((Action) (() => action()));
          }
        }));
      return that;
    }

    public static IEnumerable<Task> WhenFailed(
      this IEnumerable<Task> that,
      Action action,
      DispatcherBase target)
    {
      bool hasFailed = false;
      object syncRoot = new object();
      foreach (Task task in that)
        task.WhenFailed((Action) (() =>
        {
          lock (syncRoot)
          {
            if (hasFailed)
              return;
            hasFailed = true;
            if (target == null)
              action();
            else
              target.Dispatch((Action) (() => action()));
          }
        }));
      return that;
    }

    public static Task OnResult(this Task task, Action<object> action)
    {
      return task.OnResult(action, (DispatcherBase) null);
    }

    public static Task OnResult(this Task task, Action<object> action, DispatcherBase target)
    {
      return task.WhenSucceeded((Action<Task>) (t => action(t.RawResult)), target);
    }

    public static Task OnResult<T>(this Task task, Action<T> action)
    {
      return task.OnResult<T>(action, (DispatcherBase) null);
    }

    public static Task OnResult<T>(this Task task, Action<T> action, DispatcherBase target)
    {
      return task.WhenSucceeded((Action<Task>) (t => action((T) t.RawResult)), target);
    }

    public static Task<T> OnResult<T>(this Task<T> task, Action<T> action)
    {
      return TaskExtension.OnResult<T>(task, action, (DispatcherBase) null);
    }

    public static Task<T> OnResult<T>(
      this Task<T> task,
      Action<T> action,
      DispatcherBase actionTarget)
    {
      return task.WhenSucceeded<T>((Action<Task<T>>) (t => action(t.Result)), actionTarget);
    }

    public static Task<T> WhenSucceeded<T>(this Task<T> task, Action action)
    {
      return task.WhenSucceeded<T>((Action<Task<T>>) (t => action()), (DispatcherBase) null);
    }

    public static Task<T> WhenSucceeded<T>(this Task<T> task, Action<Task<T>> action)
    {
      return task.WhenSucceeded<T>(action, (DispatcherBase) null);
    }

    public static Task<T> WhenSucceeded<T>(
      this Task<T> task,
      Action<Task<T>> action,
      DispatcherBase target)
    {
      Action<Task<T>> perform = (Action<Task<T>>) (t =>
      {
        if (target == null)
          action(t);
        else
          target.Dispatch((Action) (() =>
          {
            if (!t.IsSucceeded)
              return;
            action(t);
          }));
      });
      return task.WhenEnded<T>((Action<Task<T>>) (t =>
      {
        if (!t.IsSucceeded)
          return;
        perform(t);
      }), (DispatcherBase) null);
    }

    public static Task WhenSucceeded(this Task task, Action action)
    {
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsSucceeded)
          return;
        action();
      }));
    }

    public static Task WhenSucceeded(this Task task, Action<Task> action)
    {
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsSucceeded)
          return;
        action(t);
      }));
    }

    public static Task WhenSucceeded(
      this Task task,
      Action<Task> action,
      DispatcherBase actiontargetTarget)
    {
      Action<Task> perform = (Action<Task>) (t =>
      {
        if (actiontargetTarget == null)
          action(t);
        else
          actiontargetTarget.Dispatch((Action) (() =>
          {
            if (!t.IsSucceeded)
              return;
            action(t);
          }));
      });
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsSucceeded)
          return;
        perform(t);
      }), (DispatcherBase) null);
    }

    public static Task<T> WhenFailed<T>(this Task<T> task, Action action)
    {
      return task.WhenFailed<T>((Action<Task<T>>) (t => action()), (DispatcherBase) null);
    }

    public static Task<T> WhenFailed<T>(this Task<T> task, Action<Task<T>> action)
    {
      return task.WhenFailed<T>(action, (DispatcherBase) null);
    }

    public static Task<T> WhenFailed<T>(
      this Task<T> task,
      Action<Task<T>> action,
      DispatcherBase target)
    {
      return task.WhenEnded<T>((Action<Task<T>>) (t =>
      {
        if (!t.IsFailed)
          return;
        action(t);
      }), target);
    }

    public static Task WhenFailed(this Task task, Action action)
    {
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsFailed)
          return;
        action();
      }));
    }

    public static Task WhenFailed(this Task task, Action<Task> action)
    {
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsFailed)
          return;
        action(t);
      }));
    }

    public static Task WhenFailed(this Task task, Action<Task> action, DispatcherBase target)
    {
      return task.WhenEnded((Action<Task>) (t =>
      {
        if (!t.IsFailed)
          return;
        action(t);
      }), target);
    }

    public static Task<T> WhenEnded<T>(this Task<T> task, Action action)
    {
      return task.WhenEnded<T>((Action<Task<T>>) (t => action()), (DispatcherBase) null);
    }

    public static Task<T> WhenEnded<T>(this Task<T> task, Action<Task<T>> action)
    {
      return task.WhenEnded<T>(action, (DispatcherBase) null);
    }

    public static Task<T> WhenEnded<T>(
      this Task<T> task,
      Action<Task<T>> action,
      DispatcherBase target)
    {
      task.TaskEnded += (TaskEndedEventHandler) (t =>
      {
        if (target == null)
          action(task);
        else
          target.Dispatch((Action) (() => action(task)));
      });
      return task;
    }

    public static Task WhenEnded(this Task task, Action action)
    {
      return task.WhenEnded((Action<Task>) (t => action()));
    }

    public static Task WhenEnded(this Task task, Action<Task> action)
    {
      return task.WhenEnded((Action<Task>) (t => action(t)), (DispatcherBase) null);
    }

    public static Task WhenEnded(this Task task, Action<Task> action, DispatcherBase target)
    {
      task.TaskEnded += (TaskEndedEventHandler) (t =>
      {
        if (target == null)
          action(task);
        else
          target.Dispatch((Action) (() => action(task)));
      });
      return task;
    }

    public static Task Then(this Task that, Task followingTask)
    {
      TaskDistributor target = (TaskDistributor) null;
      if (ThreadBase.CurrentThread is TaskWorker)
        target = ((TaskWorker) ThreadBase.CurrentThread).TaskDistributor;
      return that.Then(followingTask, (DispatcherBase) target);
    }

    public static Task Then(this Task that, Task followingTask, DispatcherBase target)
    {
      that.WhenFailed((Action) (() => followingTask.Abort()));
      that.WhenSucceeded((Action) (() =>
      {
        if (target != null)
          followingTask.Run(target);
        else if (ThreadBase.CurrentThread is TaskWorker)
          followingTask.Run((DispatcherBase) ((TaskWorker) ThreadBase.CurrentThread).TaskDistributor);
        else
          followingTask.Run();
      }));
      return that;
    }

    public static Task Await(this Task that, Task taskToWaitFor)
    {
      taskToWaitFor.Then(that);
      return that;
    }

    public static Task Await(this Task that, Task taskToWaitFor, DispatcherBase target)
    {
      taskToWaitFor.Then(that, target);
      return that;
    }

    public static Task<T> As<T>(this Task that) => (Task<T>) that;

    public static IEnumerable<Task> ContinueWhenAnyEnded(
      this IEnumerable<Task> tasks,
      Action action)
    {
      return tasks.ContinueWhenAnyEnded((Action<Task>) (t => action()));
    }

    public static IEnumerable<Task> ContinueWhenAnyEnded(
      this IEnumerable<Task> tasks,
      Action<Task> action)
    {
      object syncRoot = new object();
      bool done = false;
      foreach (Task task in tasks)
        task.WhenEnded((Action<Task>) (t =>
        {
          lock (syncRoot)
          {
            if (done)
              return;
            done = true;
            action(t);
          }
        }));
      return tasks;
    }

    public static IEnumerable<Task> ContinueWhenAllEnded(
      this IEnumerable<Task> tasks,
      Action action)
    {
      return tasks.ContinueWhenAllEnded((Action<IEnumerable<Task>>) (t => action()));
    }

    public static IEnumerable<Task> ContinueWhenAllEnded(
      this IEnumerable<Task> tasks,
      Action<IEnumerable<Task>> action)
    {
      int count = tasks.Count<Task>();
      if (count == 0)
        action((IEnumerable<Task>) new Task[0]);
      List<Task> finishedTasks = new List<Task>();
      object syncRoot = new object();
      foreach (Task task1 in tasks)
      {
        Task task = task1;
        task.WhenEnded((Action<Task>) (t =>
        {
          lock (syncRoot)
          {
            finishedTasks.Add(task);
            if (finishedTasks.Count != count)
              return;
            action((IEnumerable<Task>) finishedTasks);
          }
        }));
      }
      return tasks;
    }
  }
}
