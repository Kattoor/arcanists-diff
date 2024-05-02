// Decompiled with JetBrains decompiler
// Type: UnityThreading.EnumeratableActionThread
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
