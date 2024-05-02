// Decompiled with JetBrains decompiler
// Type: UnityThreading.DispatcherBase
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  public abstract class DispatcherBase : IDisposable
  {
    protected int lockCount;
    protected object taskListSyncRoot = new object();
    protected Queue<Task> taskList = new Queue<Task>();
    protected Queue<Task> delayedTaskList = new Queue<Task>();
    protected ManualResetEvent dataEvent = new ManualResetEvent(false);
    public bool AllowAccessLimitationChecks;
    public TaskSortingSystem TaskSortingSystem;

    public bool IsWorking => this.dataEvent.InterWaitOne(0);

    public virtual int TaskCount
    {
      get
      {
        lock (this.taskListSyncRoot)
          return this.taskList.Count;
      }
    }

    public void Lock()
    {
      lock (this.taskListSyncRoot)
        ++this.lockCount;
    }

    public void Unlock()
    {
      lock (this.taskListSyncRoot)
      {
        --this.lockCount;
        if (this.lockCount != 0 || this.delayedTaskList.Count <= 0)
          return;
        while (this.delayedTaskList.Count > 0)
          this.taskList.Enqueue(this.delayedTaskList.Dequeue());
        if (this.TaskSortingSystem == TaskSortingSystem.ReorderWhenAdded || this.TaskSortingSystem == TaskSortingSystem.ReorderWhenExecuted)
          this.ReorderTasks();
        this.TasksAdded();
      }
    }

    public Task<T> Dispatch<T>(Func<T> function)
    {
      this.CheckAccessLimitation();
      Task<T> task = new Task<T>(function);
      this.AddTask((Task) task);
      return task;
    }

    public Task Dispatch(Action action)
    {
      this.CheckAccessLimitation();
      Task task = Task.Create(action);
      this.AddTask(task);
      return task;
    }

    public void Dispatch2(Action action)
    {
      if (UnityThreadHelper.OnMainThread())
      {
        if (action == null)
          return;
        action();
      }
      else
      {
        this.CheckAccessLimitation();
        this.AddTask(Task.Create(action));
      }
    }

    public Task Dispatch(Task task)
    {
      this.CheckAccessLimitation();
      this.AddTask(task);
      return task;
    }

    internal virtual void AddTask(Task task)
    {
      lock (this.taskListSyncRoot)
      {
        if (this.lockCount > 0)
        {
          this.delayedTaskList.Enqueue(task);
          return;
        }
        this.taskList.Enqueue(task);
        if (this.TaskSortingSystem != TaskSortingSystem.ReorderWhenAdded)
        {
          if (this.TaskSortingSystem != TaskSortingSystem.ReorderWhenExecuted)
            goto label_9;
        }
        this.ReorderTasks();
      }
label_9:
      this.TasksAdded();
    }

    internal void AddTasks(IEnumerable<Task> tasks)
    {
      lock (this.taskListSyncRoot)
      {
        if (this.lockCount > 0)
        {
          using (IEnumerator<Task> enumerator = tasks.GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.delayedTaskList.Enqueue(enumerator.Current);
            return;
          }
        }
        else
        {
          foreach (Task task in tasks)
            this.taskList.Enqueue(task);
          if (this.TaskSortingSystem != TaskSortingSystem.ReorderWhenAdded)
          {
            if (this.TaskSortingSystem != TaskSortingSystem.ReorderWhenExecuted)
              goto label_23;
          }
          this.ReorderTasks();
        }
      }
label_23:
      this.TasksAdded();
    }

    internal virtual void TasksAdded() => this.dataEvent.Set();

    protected void ReorderTasks()
    {
      this.taskList = new Queue<Task>((IEnumerable<Task>) this.taskList.OrderBy<Task, int>((Func<Task, int>) (t => t.Priority)));
    }

    internal IEnumerable<Task> SplitTasks(int divisor)
    {
      if (divisor == 0)
        divisor = 2;
      return this.IsolateTasks(this.TaskCount / divisor);
    }

    internal IEnumerable<Task> IsolateTasks(int count)
    {
      Queue<Task> taskQueue = new Queue<Task>();
      if (count == 0)
        count = this.taskList.Count;
      lock (this.taskListSyncRoot)
      {
        for (int index = 0; index < count && index < this.taskList.Count; ++index)
          taskQueue.Enqueue(this.taskList.Dequeue());
        if (this.TaskCount == 0)
          this.dataEvent.Reset();
      }
      return (IEnumerable<Task>) taskQueue;
    }

    protected abstract void CheckAccessLimitation();

    public virtual void Dispose()
    {
      while (true)
      {
        Task task;
        lock (this.taskListSyncRoot)
        {
          if (this.taskList.Count != 0)
            task = this.taskList.Dequeue();
          else
            break;
        }
        task.Dispose();
      }
      this.dataEvent.Close();
      this.dataEvent = (ManualResetEvent) null;
    }
  }
}
