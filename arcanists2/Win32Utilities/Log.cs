// Decompiled with JetBrains decompiler
// Type: Win32Utilities.Log
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
