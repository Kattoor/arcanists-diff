

using System;
using System.Collections;

#nullable disable
namespace UnityThreading
{
  public sealed class ActionThread : ThreadBase
  {
    private Action<ActionThread> action;

    public ActionThread(Action<ActionThread> action)
      : this(action, true)
    {
    }

    public ActionThread(Action<ActionThread> action, bool autoStartThread)
      : base(nameof (ActionThread), Dispatcher.Current, false)
    {
      this.action = action;
      if (!autoStartThread)
        return;
      this.Start();
    }

    protected override IEnumerator Do()
    {
      this.action(this);
      return (IEnumerator) null;
    }
  }
}
