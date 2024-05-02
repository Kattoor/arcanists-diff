

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
