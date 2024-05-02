

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
