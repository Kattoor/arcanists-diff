

using System;
using System.Collections;
using System.Threading;
using UnityEngine;

#nullable disable
namespace UnityThreading
{
  public abstract class ThreadBase : IDisposable
  {
    protected Dispatcher targetDispatcher;
    protected Thread thread;
    protected ManualResetEvent exitEvent = new ManualResetEvent(false);
    [ThreadStatic]
    private static ThreadBase currentThread;
    private string threadName;
    private System.Threading.ThreadPriority priority = System.Threading.ThreadPriority.BelowNormal;

    public static int AvailableProcessors => SystemInfo.processorCount;

    public static ThreadBase CurrentThread => ThreadBase.currentThread;

    public ThreadBase(string threadName)
      : this(threadName, true)
    {
    }

    public ThreadBase(string threadName, bool autoStartThread)
      : this(threadName, Dispatcher.CurrentNoThrow, autoStartThread)
    {
    }

    public ThreadBase(string threadName, Dispatcher targetDispatcher)
      : this(threadName, targetDispatcher, true)
    {
    }

    public ThreadBase(string threadName, Dispatcher targetDispatcher, bool autoStartThread)
    {
      this.threadName = threadName;
      this.targetDispatcher = targetDispatcher;
      if (!autoStartThread)
        return;
      this.Start();
    }

    public bool IsAlive => this.thread != null && this.thread.IsAlive;

    public bool ShouldStop => this.exitEvent.InterWaitOne(0);

    public void Start()
    {
      if (this.thread != null)
        this.Abort();
      this.exitEvent.Reset();
      this.thread = new Thread(new ThreadStart(this.DoInternal));
      this.thread.Name = this.threadName;
      this.thread.Priority = this.priority;
      this.thread.Start();
    }

    public void Exit()
    {
      if (this.thread == null)
        return;
      this.exitEvent.Set();
    }

    public void Abort()
    {
      this.Exit();
      if (this.thread == null)
        return;
      this.thread.Join();
    }

    public void AbortWaitForSeconds(float seconds)
    {
      this.Exit();
      if (this.thread == null)
        return;
      this.thread.Join((int) ((double) seconds * 1000.0));
      if (!this.thread.IsAlive)
        return;
      this.thread.Abort();
    }

    public Task<T> Dispatch<T>(Func<T> function) => this.targetDispatcher.Dispatch<T>(function);

    public T DispatchAndWait<T>(Func<T> function)
    {
      Task<T> task = this.Dispatch<T>(function);
      task.Wait();
      return task.Result;
    }

    public T DispatchAndWait<T>(Func<T> function, float timeOutSeconds)
    {
      Task<T> task = this.Dispatch<T>(function);
      task.WaitForSeconds(timeOutSeconds);
      return task.Result;
    }

    public Task Dispatch(Action action) => this.targetDispatcher.Dispatch(action);

    public void DispatchAndWait(Action action) => this.Dispatch(action).Wait();

    public void DispatchAndWait(Action action, float timeOutSeconds)
    {
      this.Dispatch(action).WaitForSeconds(timeOutSeconds);
    }

    public Task Dispatch(Task taskBase) => this.targetDispatcher.Dispatch(taskBase);

    public void DispatchAndWait(Task taskBase) => this.Dispatch(taskBase).Wait();

    public void DispatchAndWait(Task taskBase, float timeOutSeconds)
    {
      this.Dispatch(taskBase).WaitForSeconds(timeOutSeconds);
    }

    protected void DoInternal()
    {
      ThreadBase.currentThread = this;
      IEnumerator enumerator = this.Do();
      if (enumerator == null)
        return;
      this.RunEnumerator(enumerator);
    }

    private void RunEnumerator(IEnumerator enumerator)
    {
      do
      {
        if (enumerator.Current is Task)
          this.DispatchAndWait((Task) enumerator.Current);
        else if (enumerator.Current is SwitchTo)
        {
          SwitchTo current = (SwitchTo) enumerator.Current;
          if (current.Target == SwitchTo.TargetType.Main && ThreadBase.CurrentThread != null)
            this.DispatchAndWait(Task.Create((Action) (() =>
            {
              if (!enumerator.MoveNext() || this.ShouldStop)
                return;
              this.RunEnumerator(enumerator);
            })));
          else if (current.Target == SwitchTo.TargetType.Thread && ThreadBase.CurrentThread == null)
            break;
        }
      }
      while (enumerator.MoveNext() && !this.ShouldStop);
    }

    protected abstract IEnumerator Do();

    public virtual void Dispose() => this.AbortWaitForSeconds(1f);

    public System.Threading.ThreadPriority Priority
    {
      get => this.priority;
      set
      {
        this.priority = value;
        if (this.thread == null)
          return;
        this.thread.Priority = this.priority;
      }
    }
  }
}
