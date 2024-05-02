// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.PongEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets
{
  public class PongEventArgs : EventArgs
  {
    public ArraySegment<byte> Payload { get; private set; }

    public PongEventArgs(ArraySegment<byte> payload) => this.Payload = payload;
  }
}
