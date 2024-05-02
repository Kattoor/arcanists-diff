// Decompiled with JetBrains decompiler
// Type: UnityThreading.Task`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Reflection;

#nullable disable
namespace UnityThreading
{
  public class Task<T> : Task
  {
    private Func<Task, T> function;
    private T result;

    public Task(Func<Task, T> function) => this.function = function;

    public Task(Func<T> function) => this.function = (Func<Task, T>) (t => function());

    public Task(Action<Task> action)
    {
      this.function = (Func<Task, T>) (t =>
      {
        action(t);
        return default (T);
      });
    }

    public Task(Action action)
    {
      this.function = (Func<Task, T>) (t =>
      {
        action();
        return default (T);
      });
    }

    public Task(IEnumerator enumerator) => this.function = (Func<Task, T>) (t => (T) enumerator);

    public Task(Type type, string methodName, params object[] args)
    {
      MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
      this.function = !(methodInfo == (MethodInfo) null) ? (Func<Task, T>) (t => (T) methodInfo.Invoke((object) null, args)) : throw new ArgumentException(nameof (methodName), "Fitting method with the given name was not found.");
    }

    public Task(object that, string methodName, params object[] args)
    {
      MethodInfo methodInfo = that.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentException(nameof (methodName), "Fitting method with the given name was not found.");
      this.function = (Func<Task, T>) (t => (T) methodInfo.Invoke(that, args));
    }

    protected override IEnumerator Do()
    {
      this.result = this.function((Task) this);
      return (object) this.result is IEnumerator ? (IEnumerator) (object) this.result : (IEnumerator) null;
    }

    public override TResult Wait<TResult>()
    {
      --this.Priority;
      return (TResult) (object) this.Result;
    }

    public override TResult WaitForSeconds<TResult>(float seconds)
    {
      --this.Priority;
      return this.WaitForSeconds<TResult>(seconds, default (TResult));
    }

    public override TResult WaitForSeconds<TResult>(float seconds, TResult defaultReturnValue)
    {
      if (!this.HasEnded)
        this.WaitForSeconds(seconds);
      return this.IsSucceeded ? (TResult) (object) this.result : defaultReturnValue;
    }

    public override object RawResult
    {
      get
      {
        if (!this.IsEnding)
          this.Wait();
        return (object) this.result;
      }
    }

    public T Result
    {
      get
      {
        if (!this.IsEnding)
          this.Wait();
        return this.result;
      }
    }

    public Task<T> Run(DispatcherBase target)
    {
      base.Run(target);
      return this;
    }

    public Task<T> Run()
    {
      base.Run();
      return this;
    }
  }
}
