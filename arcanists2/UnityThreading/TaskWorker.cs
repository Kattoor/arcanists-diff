// Decompiled with JetBrains decompiler
// Type: UnityThreading.TaskWorker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  internal sealed class TaskWorker : ThreadBase
  {
    public Dispatcher Dispatcher;

    public TaskDistributor TaskDistributor { get; private set; }

    public bool IsWorking => this.Dispatcher.IsWorking;

    public TaskWorker(string name, TaskDistributor taskDistributor)
      : base(name, false)
    {
      this.TaskDistributor = taskDistributor;
      this.Dispatcher = new Dispatcher(false);
    }

    protected override IEnumerator Do()
    {
      while (!this.exitEvent.InterWaitOne(0))
      {
        if (!this.Dispatcher.ProcessNextTask())
        {
          this.TaskDistributor.FillTasks(this.Dispatcher);
          if (this.Dispatcher.TaskCount == 0)
          {
            if (WaitHandle.WaitAny(new WaitHandle[2]
            {
              (WaitHandle) this.exitEvent,
              this.TaskDistributor.NewDataWaitHandle
            }) == 0)
              return (IEnumerator) null;
            this.TaskDistributor.FillTasks(this.Dispatcher);
          }
        }
      }
      return (IEnumerator) null;
    }

    public override void Dispose()
    {
      base.Dispose();
      if (this.Dispatcher != null)
        this.Dispatcher.Dispose();
      this.Dispatcher = (Dispatcher) null;
    }
  }
}
