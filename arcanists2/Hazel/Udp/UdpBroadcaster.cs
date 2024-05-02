

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

#nullable disable
namespace Hazel.Udp
{
  public class UdpBroadcaster : IDisposable
  {
    private Socket socket;
    private byte[] data;
    private EndPoint endpoint;
    private Action<string> logger;

    public UdpBroadcaster(int port, Action<string> logger = null)
    {
      this.logger = logger;
      this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      this.socket.EnableBroadcast = true;
      this.socket.MulticastLoopback = false;
      this.endpoint = (EndPoint) new IPEndPoint(IPAddress.Broadcast, port);
    }

    public void SetData(string data)
    {
      this.data = new byte[Encoding.UTF8.GetByteCount(data) + 2];
      this.data[0] = (byte) 4;
      this.data[1] = (byte) 2;
      Encoding.UTF8.GetBytes(data, 0, data.Length, this.data, 2);
    }

    public void Broadcast()
    {
      if (this.data == null)
        return;
      try
      {
        this.socket.BeginSendTo(this.data, 0, this.data.Length, SocketFlags.None, this.endpoint, new AsyncCallback(this.FinishSendTo), (object) null);
      }
      catch (Exception ex)
      {
        Action<string> logger = this.logger;
        if (logger == null)
          return;
        logger("BroadcastListener: " + (object) ex);
      }
    }

    private void FinishSendTo(IAsyncResult evt)
    {
      try
      {
        this.socket.EndSendTo(evt);
      }
      catch (Exception ex)
      {
        Action<string> logger = this.logger;
        if (logger == null)
          return;
        logger("BroadcastListener: " + (object) ex);
      }
    }

    public void Dispose()
    {
      if (this.socket == null)
        return;
      try
      {
        this.socket.Shutdown(SocketShutdown.Both);
      }
      catch
      {
      }
      try
      {
        this.socket.Close();
      }
      catch
      {
      }
      try
      {
        this.socket.Dispose();
      }
      catch
      {
      }
      this.socket = (Socket) null;
    }
  }
}
