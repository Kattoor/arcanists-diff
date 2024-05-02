// Decompiled with JetBrains decompiler
// Type: Hazel.Tcp.StateObject
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Hazel.Tcp
{
  internal struct StateObject
  {
    internal byte[] buffer;
    internal int totalBytesReceived;
    internal Action<byte[]> callback;

    internal StateObject(int length, Action<byte[]> callback)
    {
      this.buffer = new byte[length];
      this.totalBytesReceived = 0;
      this.callback = callback;
    }
  }
}
