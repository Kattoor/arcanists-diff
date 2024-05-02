

using System;
using System.Collections;

#nullable disable
namespace UnityThreading
{
  public sealed class EnumeratableActionThread : ThreadBase
  {
    private Func<ThreadBase, IEnumerator> enumeratableAction;

    public EnumeratableActionThread(Func<ThreadBase, IEnumerator> enumeratableAction)
      : this(enumeratableAction, true)
    {
    }

    public EnumeratableActionThread(
      Func<ThreadBase, IEnumerator> enumeratableAction,
      bool autoStartThread)
      : base(nameof (EnumeratableActionThread), Dispatcher.Current, false)
    {
      this.enumeratableAction = enumeratableAction;
      if (!autoStartThread)
        return;
      this.Start();
    }

    protected override IEnumerator Do() => this.enumeratableAction((ThreadBase) this);
  }
}
