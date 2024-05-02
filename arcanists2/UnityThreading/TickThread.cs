﻿// Decompiled with JetBrains decompiler
// Type: UnityThreading.TickThread
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  public sealed class TickThread : ThreadBase
  {
    private Action action;
    private int tickLengthInMilliseconds;
    private ManualResetEvent tickEvent = new ManualResetEvent(false);

    public TickThread(Action action, int tickLengthInMilliseconds)
      : this(action, tickLengthInMilliseconds, true)
    {
    }

    public TickThread(Action action, int tickLengthInMilliseconds, bool autoStartThread)
      : base(nameof (TickThread), Dispatcher.CurrentNoThrow, false)
    {
      this.tickLengthInMilliseconds = tickLengthInMilliseconds;
      this.action = action;
      if (!autoStartThread)
        return;
      this.Start();
    }

    protected override IEnumerator Do()
    {
      while (!this.exitEvent.InterWaitOne(0))
      {
        this.action();
        if (WaitHandle.WaitAny(new WaitHandle[2]
        {
          (WaitHandle) this.exitEvent,
          (WaitHandle) this.tickEvent
        }, this.tickLengthInMilliseconds) == 0)
          return (IEnumerator) null;
      }
      return (IEnumerator) null;
    }
  }
}