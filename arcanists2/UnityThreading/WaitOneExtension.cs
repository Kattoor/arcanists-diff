// Decompiled with JetBrains decompiler
// Type: UnityThreading.WaitOneExtension
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  public static class WaitOneExtension
  {
    public static bool InterWaitOne(this ManualResetEvent that, int ms) => that.WaitOne(ms, false);

    public static bool InterWaitOne(this ManualResetEvent that, TimeSpan duration)
    {
      return that.WaitOne(duration, false);
    }
  }
}
