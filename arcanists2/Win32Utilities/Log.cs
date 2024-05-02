

using System;
using UnityEngine;

#nullable disable
namespace Win32Utilities
{
  public class Log
  {
    public static Log.Inner d = new Log.Inner();

    public class Inner
    {
      public void error(string v, Exception e) => Debug.Log((object) (v + " " + (object) e));

      internal void debug(string v) => Debug.Log((object) v);

      internal bool isDebug() => false;
    }
  }
}
