// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.IPingPongManager
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  internal interface IPingPongManager
  {
    event EventHandler<PongEventArgs> Pong;

    Task SendPing(ArraySegment<byte> payload, CancellationToken cancellation);
  }
}
