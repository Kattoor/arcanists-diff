// Decompiled with JetBrains decompiler
// Type: UnityThreading.TaskDistributor
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
  public class TaskDistributor : DispatcherBase
  {
    private TaskWorker[] workerThreads;
    private static TaskDistributor mainTaskDistributor;
    public int MaxAdditionalWorkerThreads;
    private string name;
    private bool isDisposed;
    private ThreadPriority priority = ThreadPriority.BelowNormal;

    internal WaitHandle NewDataWaitHandle => (WaitHandle) this.dataEvent;

    public static TaskDistributor Main
    {
      get
      {
        return TaskDistributor.mainTaskDistributor != null ? TaskDistributor.mainTaskDistributor : throw new InvalidOperationException("No default TaskDistributor found, please create a new TaskDistributor instance before calling this property.");
      }
    }

    public static TaskDistributor MainNoThrow => TaskDistributor.mainTaskDistributor;

    public TaskDistributor(string name)
      : this(name, 0)
    {
    }

    public override int TaskCount
    {
      get
      {
        int taskCount = base.TaskCount;
        lock (this.workerThreads)
        {
          for (int index = 0; index < this.workerThreads.Length; ++index)
            taskCount += this.workerThreads[index].Dispatcher.TaskCount;
        }
        return taskCount;
      }
    }

    public TaskDistributor(string name, int workerThreadCount)
      : this(name, workerThreadCount, true)
    {
    }

    public TaskDistributor(string name, int workerThreadCount, bool autoStart)
    {
      this.name = name;
      if (workerThreadCount <= 0)
        workerThreadCount = ThreadBase.AvailableProcessors * 2;
      this.workerThreads = new TaskWorker[workerThreadCount];
      lock (this.workerThreads)
      {
        for (int index = 0; index < workerThreadCount; ++index)
          this.workerThreads[index] = new TaskWorker(name, this);
      }
      if (TaskDistributor.mainTaskDistributor == null)
        TaskDistributor.mainTaskDistributor = this;
      if (!autoStart)
        return;
      this.Start();
    }

    public void Start()
    {
      lock (this.workerThreads)
      {
        for (int index = 0; index < this.workerThreads.Length; ++index)
        {
          if (!this.workerThreads[index].IsAlive)
            this.workerThreads[index].Start();
        }
      }
    }

    public void SpawnAdditionalWorkerThread()
    {
      lock (this.workerThreads)
      {
        Array.Resize<TaskWorker>(ref this.workerThreads, this.workerThreads.Length + 1);
        this.workerThreads[this.workerThreads.Length - 1] = new TaskWorker(this.name, this);
        this.workerThreads[this.workerThreads.Length - 1].Priority = this.priority;
        this.workerThreads[this.workerThreads.Length - 1].Start();
      }
    }

    internal void FillTasks(Dispatcher target) => target.AddTasks(this.IsolateTasks(1));

    protected override void CheckAccessLimitation()
    {
      if (this.MaxAdditionalWorkerThreads <= 0 && this.AllowAccessLimitationChecks && ThreadBase.CurrentThread != null && ThreadBase.CurrentThread is TaskWorker && ((TaskWorker) ThreadBase.CurrentThread).TaskDistributor == this)
        throw new InvalidOperationException("Access to TaskDistributor prohibited when called from inside a TaskDistributor thread. Dont dispatch new Tasks through the same TaskDistributor. If you want to distribute new tasks create a new TaskDistributor and use the new created instance. Remember to dispose the new instance to prevent thread spamming.");
    }

    internal override void TasksAdded()
    {
      if (this.MaxAdditionalWorkerThreads > 0 && (((IEnumerable<TaskWorker>) this.workerThreads).All<TaskWorker>((Func<TaskWorker, bool>) (worker => worker.Dispatcher.TaskCount > 0 || worker.IsWorking)) || this.taskList.Count > this.workerThreads.Length))
      {
        Interlocked.Decrement(ref this.MaxAdditionalWorkerThreads);
        this.SpawnAdditionalWorkerThread();
      }
      base.TasksAdded();
    }

    public override void Dispose()
    {
      if (this.isDisposed)
        return;
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
      lock (this.workerThreads)
      {
        for (int index = 0; index < this.workerThreads.Length; ++index)
          this.workerThreads[index].Dispose();
        this.workerThreads = new TaskWorker[0];
      }
      this.dataEvent.Close();
      this.dataEvent = (ManualResetEvent) null;
      if (TaskDistributor.mainTaskDistributor == this)
        TaskDistributor.mainTaskDistributor = (TaskDistributor) null;
      this.isDisposed = true;
    }

    public ThreadPriority Priority
    {
      get => this.priority;
      set
      {
        this.priority = value;
        foreach (ThreadBase workerThread in this.workerThreads)
          workerThread.Priority = value;
      }
    }
  }
}
