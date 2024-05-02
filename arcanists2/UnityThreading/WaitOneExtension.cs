

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
