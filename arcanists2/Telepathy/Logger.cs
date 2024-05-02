// Decompiled with JetBrains decompiler
// Type: Telepathy.Logger
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Telepathy
{
  public static class Logger
  {
    public static Action<string> Log = new Action<string>(Console.WriteLine);
    public static Action<string> LogWarning = new Action<string>(Console.WriteLine);
    public static Action<string> LogError = new Action<string>(Console.Error.WriteLine);
  }
}
