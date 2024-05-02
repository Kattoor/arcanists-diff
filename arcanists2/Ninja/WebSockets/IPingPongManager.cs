// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.IPingPongManager
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
