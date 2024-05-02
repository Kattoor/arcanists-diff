// Decompiled with JetBrains decompiler
// Type: Telepathy.Logger
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
