// Decompiled with JetBrains decompiler
// Type: UnityThreading.ActionThread
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
