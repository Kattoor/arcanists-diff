

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

#nullable disable
namespace Hazel.Udp
{
  public class UdpBroadcastListener : IDisposable
  {
    private Socket socket;
    private EndPoint endpoint;
    private Action<string> logger;
    private byte[] buffer = new byte[1024];
    private List<BroadcastPacket> packets = new List<BroadcastPacket>();

    public bool Running { get; private set; }

    public UdpBroadcastListener(int port, Action<string> logger = null)
    {
      this.logger = logger;
      this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      this.socket.EnableBroadcast = true;
      this.socket.MulticastLoopback = false;
      this.endpoint = (EndPoint) new IPEndPoint(IPAddress.Any, port);
      this.socket.Bind(this.endpoint);
    }

    public void StartListen()
    {
      if (this.Running)
        return;
      this.Running = true;
      try
      {
        EndPoint remoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 0);
        IAsyncResult result = this.socket.BeginReceiveFrom(this.buffer, 0, this.buffer.Length, SocketFlags.None, ref remoteEP, new AsyncCallback(this.HandleData), (object) null);
        if (!result.CompletedSynchronously)
          return;
        ThreadPool.QueueUserWorkItem((WaitCallback) (_ => this.HandleData(result)));
      }
      catch (NullReferenceException ex)
      {
      }
      catch (Exception ex)
      {
        Action<string> logger = this.logger;
        if (logger != null)
          logger("BroadcastListener: " + (object) ex);
        this.Dispose();
      }
    }

    private void HandleData(IAsyncResult result)
    {
      this.Running = false;
      EndPoint endPoint = (EndPoint) new IPEndPoint(IPAddress.Any, 0);
      int from;
      try
      {
        from = this.socket.EndReceiveFrom(result, ref endPoint);
      }
      catch (NullReferenceException ex)
      {
        return;
      }
      catch (Exception ex)
      {
        Action<string> logger = this.logger;
        if (logger != null)
          logger("BroadcastListener: " + (object) ex);
        this.Dispose();
        return;
      }
      if (from < 3 || this.buffer[0] != (byte) 4 || this.buffer[1] != (byte) 2)
      {
        this.StartListen();
      }
      else
      {
        IPEndPoint sender = (IPEndPoint) endPoint;
        string data = Encoding.UTF8.GetString(this.buffer, 2, from - 2);
        int hashCode = data.GetHashCode();
        lock (this.packets)
        {
          bool flag = false;
          for (int index = 0; index < this.packets.Count; ++index)
          {
            BroadcastPacket packet = this.packets[index];
            if (packet == null || packet.Data == null)
            {
              this.packets.RemoveAt(index);
              --index;
            }
            else if (packet.Data.GetHashCode() == hashCode && packet.Sender.Equals((object) sender))
            {
              this.packets[index].ReceiveTime = DateTime.Now;
              break;
            }
          }
          if (!flag)
            this.packets.Add(new BroadcastPacket(data, sender));
        }
        this.StartListen();
      }
    }

    public BroadcastPacket[] GetPackets()
    {
      lock (this.packets)
      {
        BroadcastPacket[] array = this.packets.ToArray();
        this.packets.Clear();
        return array;
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
