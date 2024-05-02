

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

#nullable disable
namespace UnityThreading
{
  public abstract class Task
  {
    private object syncRoot = new object();
    private bool hasEnded;
    public string Name;
    public volatile int Priority;
    private ManualResetEvent abortEvent = new ManualResetEvent(false);
    private ManualResetEvent endedEvent = new ManualResetEvent(false);
    private ManualResetEvent endingEvent = new ManualResetEvent(false);
    private bool hasStarted;
    [ThreadStatic]
    private static Task current;
    private bool disposed;

    ~Task() => this.Dispose();

    private event TaskEndedEventHandler taskEnded;

    public event TaskEndedEventHandler TaskEnded
    {
      add
      {
        lock (this.syncRoot)
        {
          if (this.endingEvent.InterWaitOne(0))
            value(this);
          else
            this.taskEnded += value;
        }
      }
      remove
      {
        lock (this.syncRoot)
          this.taskEnded -= value;
      }
    }

    private void End()
    {
      lock (this.syncRoot)
      {
        this.endingEvent.Set();
        if (this.taskEnded != null)
          this.taskEnded(this);
        this.endedEvent.Set();
        if (Task.current == this)
          Task.current = (Task) null;
        this.hasEnded = true;
      }
    }

    protected abstract IEnumerator Do();

    public static Task Current => Task.current;

    public bool ShouldAbort => this.abortEvent.InterWaitOne(0);

    public bool HasEnded => this.hasEnded || this.endedEvent.InterWaitOne(0);

    public bool IsEnding => this.endingEvent.InterWaitOne(0);

    public bool IsSucceeded => this.endingEvent.InterWaitOne(0) && !this.abortEvent.InterWaitOne(0);

    public bool IsFailed => this.endingEvent.InterWaitOne(0) && this.abortEvent.InterWaitOne(0);

    public void Abort()
    {
      this.abortEvent.Set();
      if (this.hasStarted)
        return;
      this.End();
    }

    public void AbortWait()
    {
      this.Abort();
      if (!this.hasStarted)
        return;
      this.Wait();
    }

    public void AbortWaitForSeconds(float seconds)
    {
      this.Abort();
      if (!this.hasStarted)
        return;
      this.WaitForSeconds(seconds);
    }

    public void Wait()
    {
      if (this.hasEnded)
        return;
      --this.Priority;
      this.endedEvent.WaitOne();
    }

    public void WaitForSeconds(float seconds)
    {
      if (this.hasEnded)
        return;
      --this.Priority;
      this.endedEvent.InterWaitOne(TimeSpan.FromSeconds((double) seconds));
    }

    public abstract TResult Wait<TResult>();

    public abstract TResult WaitForSeconds<TResult>(float seconds);

    public abstract object RawResult { get; }

    public abstract TResult WaitForSeconds<TResult>(float seconds, TResult defaultReturnValue);

    internal void DoInternal()
    {
      Task.current = this;
      this.hasStarted = true;
      if (!this.ShouldAbort)
      {
        try
        {
          IEnumerator enumerator = this.Do();
          if (enumerator == null)
          {
            this.End();
            return;
          }
          this.RunEnumerator(enumerator);
        }
        catch (Exception ex)
        {
          this.Abort();
          if (string.IsNullOrEmpty(this.Name))
            Debug.LogError((object) ("Error while processing task:\n" + ex.ToString()));
          else
            Debug.LogError((object) ("Error while processing task '" + this.Name + "':\n" + ex.ToString()));
        }
      }
      this.End();
    }

    private void RunEnumerator(IEnumerator enumerator)
    {
      ThreadBase currentThread = ThreadBase.CurrentThread;
      do
      {
        if (enumerator.Current is Task)
        {
          Task current = (Task) enumerator.Current;
          currentThread.DispatchAndWait(current);
        }
        else if (enumerator.Current is SwitchTo)
        {
          SwitchTo current = (SwitchTo) enumerator.Current;
          if (current.Target == SwitchTo.TargetType.Main && currentThread != null)
          {
            Task taskBase = Task.Create((Action) (() =>
            {
              if (!enumerator.MoveNext() || this.ShouldAbort)
                return;
              this.RunEnumerator(enumerator);
            }));
            currentThread.DispatchAndWait(taskBase);
          }
          else if (current.Target == SwitchTo.TargetType.Thread && currentThread == null)
            break;
        }
      }
      while (enumerator.MoveNext() && !this.ShouldAbort);
    }

    public void Dispose()
    {
      if (this.disposed)
        return;
      this.disposed = true;
      if (this.hasStarted)
        this.Wait();
      this.endingEvent.Close();
      this.endedEvent.Close();
      this.abortEvent.Close();
    }

    public Task Run(DispatcherBase target)
    {
      if (target == null)
        return this.Run();
      target.Dispatch(this);
      return this;
    }

    public Task Run()
    {
      this.Run((DispatcherBase) UnityThreadHelper.TaskDistributor);
      return this;
    }

    public static Task Create(Action<Task> action) => (Task) new Task<Task.Unit>(action);

    public static Task Create(Action action) => (Task) new Task<Task.Unit>(action);

    public static Task<T> Create<T>(Func<Task, T> func) => new Task<T>(func);

    public static Task<T> Create<T>(Func<T> func) => new Task<T>(func);

    public static Task Create(IEnumerator enumerator) => (Task) new Task<IEnumerator>(enumerator);

    public static Task<T> Create<T>(System.Type type, string methodName, params object[] args)
    {
      return new Task<T>(type, methodName, args);
    }

    public static Task<T> Create<T>(object that, string methodName, params object[] args)
    {
      return new Task<T>(that, methodName, args);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct Unit
    {
    }
  }
}
