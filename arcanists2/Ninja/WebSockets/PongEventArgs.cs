

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
