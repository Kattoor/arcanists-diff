

using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  public class Dispatcher : DispatcherBase
  {
    [ThreadStatic]
    private static Task currentTask;
    [ThreadStatic]
    internal static Dispatcher currentDispatcher;
    protected static Dispatcher mainDispatcher;

    public static Task CurrentTask
    {
      get
      {
        return Dispatcher.currentTask != null ? Dispatcher.currentTask : throw new InvalidOperationException("No task is currently running.");
      }
    }

    public static Dispatcher Current
    {
      get
      {
        return Dispatcher.currentDispatcher != null ? Dispatcher.currentDispatcher : throw new InvalidOperationException("No Dispatcher found for the current thread, please create a new Dispatcher instance before calling this property.");
      }
      set
      {
        if (Dispatcher.currentDispatcher != null)
          Dispatcher.currentDispatcher.Dispose();
        Dispatcher.currentDispatcher = value;
      }
    }

    public static Dispatcher CurrentNoThrow => Dispatcher.currentDispatcher;

    public static Dispatcher Main
    {
      get
      {
        return Dispatcher.mainDispatcher != null ? Dispatcher.mainDispatcher : throw new InvalidOperationException("No Dispatcher found for the main thread, please create a new Dispatcher instance before calling this property.");
      }
    }

    public static Dispatcher MainNoThrow => Dispatcher.mainDispatcher;

    public static Func<T> CreateSafeFunction<T>(Func<T> function)
    {
      return (Func<T>) (() =>
      {
        try
        {
          return function();
        }
        catch
        {
          Dispatcher.CurrentTask.Abort();
          return default (T);
        }
      });
    }

    public static Action CreateSafeAction<T>(Action action)
    {
      return (Action) (() =>
      {
        try
        {
          action();
        }
        catch
        {
          Dispatcher.CurrentTask.Abort();
        }
      });
    }

    public Dispatcher()
      : this(true)
    {
    }

    public Dispatcher(bool setThreadDefaults)
    {
      if (!setThreadDefaults)
        return;
      Dispatcher.currentDispatcher = Dispatcher.currentDispatcher == null ? this : throw new InvalidOperationException("Only one Dispatcher instance allowed per thread.");
      if (Dispatcher.mainDispatcher != null)
        return;
      Dispatcher.mainDispatcher = this;
    }

    public void ProcessTasks()
    {
      if (!this.dataEvent.InterWaitOne(0))
        return;
      this.ProcessTasksInternal();
    }

    public bool ProcessTasks(WaitHandle exitHandle)
    {
      if (WaitHandle.WaitAny(new WaitHandle[2]
      {
        exitHandle,
        (WaitHandle) this.dataEvent
      }) == 0)
        return false;
      this.ProcessTasksInternal();
      return true;
    }

    public bool ProcessNextTask()
    {
      Task task;
      lock (this.taskListSyncRoot)
      {
        if (this.taskList.Count == 0)
          return false;
        task = this.taskList.Dequeue();
      }
      this.ProcessSingleTask(task);
      if (this.TaskCount == 0)
        this.dataEvent.Reset();
      return true;
    }

    public bool ProcessNextTask(WaitHandle exitHandle)
    {
      if (WaitHandle.WaitAny(new WaitHandle[2]
      {
        exitHandle,
        (WaitHandle) this.dataEvent
      }) == 0)
        return false;
      Task task;
      lock (this.taskListSyncRoot)
      {
        if (this.taskList.Count == 0)
          return false;
        task = this.taskList.Dequeue();
      }
      this.ProcessSingleTask(task);
      if (this.TaskCount == 0)
        this.dataEvent.Reset();
      return true;
    }

    private void ProcessTasksInternal()
    {
      List<Task> taskList;
      lock (this.taskListSyncRoot)
      {
        taskList = new List<Task>((IEnumerable<Task>) this.taskList);
        this.taskList.Clear();
      }
      while (taskList.Count != 0)
      {
        Task task = taskList[0];
        taskList.RemoveAt(0);
        this.ProcessSingleTask(task);
      }
      if (this.TaskCount != 0)
        return;
      this.dataEvent.Reset();
    }

    private void ProcessSingleTask(Task task)
    {
      this.RunTask(task);
      if (this.TaskSortingSystem != TaskSortingSystem.ReorderWhenExecuted)
        return;
      lock (this.taskListSyncRoot)
        this.ReorderTasks();
    }

    internal void RunTask(Task task)
    {
      Task currentTask = Dispatcher.currentTask;
      Dispatcher.currentTask = task;
      Dispatcher.currentTask.DoInternal();
      Dispatcher.currentTask = currentTask;
    }

    protected override void CheckAccessLimitation()
    {
      if (this.AllowAccessLimitationChecks && Dispatcher.currentDispatcher == this)
        throw new InvalidOperationException("Dispatching a Task with the Dispatcher associated to the current thread is prohibited. You can run these Tasks without the need of a Dispatcher.");
    }

    public override void Dispose()
    {
      while (true)
      {
        lock (this.taskListSyncRoot)
        {
          if (this.taskList.Count != 0)
            Dispatcher.currentTask = this.taskList.Dequeue();
          else
            break;
        }
        Dispatcher.currentTask.Dispose();
      }
      this.dataEvent.Close();
      this.dataEvent = (ManualResetEvent) null;
      if (Dispatcher.currentDispatcher == this)
        Dispatcher.currentDispatcher = (Dispatcher) null;
      if (Dispatcher.mainDispatcher != this)
        return;
      Dispatcher.mainDispatcher = (Dispatcher) null;
    }
  }
}
