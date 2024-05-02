

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

#nullable disable
namespace Hazel.Udp
{
  public class UdpConnectionListener : NetworkConnectionListener
  {
    private Socket listener;
    private byte[] dataBuffer = new byte[(int) ushort.MaxValue];
    private Dictionary<EndPoint, UdpServerConnection> connections = new Dictionary<EndPoint, UdpServerConnection>();

    [Obsolete("Temporary constructor in beta only, use NetworkEndPoint constructor instead.")]
    public UdpConnectionListener(IPAddress IPAddress, int port, IPMode mode = IPMode.IPv4)
      : this(new NetworkEndPoint(IPAddress, port, mode))
    {
    }

    public UdpConnectionListener(NetworkEndPoint endPoint)
    {
      this.EndPoint = endPoint.EndPoint;
      this.IPMode = endPoint.IPMode;
      if (endPoint.IPMode == IPMode.IPv4)
      {
        this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      }
      else
      {
        if (!Socket.OSSupportsIPv6)
          throw new HazelException("IPV6 not supported!");
        this.listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
        this.listener.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
      }
    }

    public override void Start()
    {
      try
      {
        lock (this.listener)
          this.listener.Bind(this.EndPoint);
      }
      catch (SocketException ex)
      {
        throw new HazelException("Could not start listening as a SocketException occured", (Exception) ex);
      }
      this.StartListeningForData();
    }

    public override void StartNoTry()
    {
      lock (this.listener)
        this.listener.Bind(this.EndPoint);
      this.StartListeningForData();
    }

    private void StartListeningForData()
    {
      EndPoint endPoint = this.EndPoint;
      try
      {
        lock (this.listener)
          this.listener.BeginReceiveFrom(this.dataBuffer, 0, this.dataBuffer.Length, SocketFlags.None, ref endPoint, new AsyncCallback(this.ReadCallback), (object) this.dataBuffer);
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    private void ReadCallback(IAsyncResult result)
    {
      EndPoint endPoint = (EndPoint) new IPEndPoint(this.IPMode == IPMode.IPv4 ? IPAddress.Any : IPAddress.IPv6Any, 0);
      int from;
      try
      {
        lock (this.listener)
          from = this.listener.EndReceiveFrom(result, ref endPoint);
      }
      catch (ObjectDisposedException ex)
      {
        return;
      }
      catch (SocketException ex)
      {
        this.StartListeningForData();
        return;
      }
      if (from == 0)
        return;
      byte[] numArray1 = new byte[from];
      Buffer.BlockCopy((Array) result.AsyncState, 0, (Array) numArray1, 0, from);
      this.StartListeningForData();
      bool flag;
      UdpServerConnection serverConnection;
      lock (this.connections)
      {
        flag = this.connections.ContainsKey(endPoint);
        if (flag)
        {
          serverConnection = this.connections[endPoint];
        }
        else
        {
          if (numArray1[0] != (byte) 8)
            return;
          serverConnection = new UdpServerConnection(this, endPoint, this.IPMode);
          this.connections.Add(endPoint, serverConnection);
          serverConnection.SendAck(numArray1[1], numArray1[2]);
        }
      }
      if (flag)
      {
        serverConnection.InvokeDataReceived(numArray1);
      }
      else
      {
        byte[] numArray2 = new byte[numArray1.Length - 1];
        Buffer.BlockCopy((Array) numArray1, 1, (Array) numArray2, 0, numArray1.Length - 1);
        this.InvokeNewConnection(numArray2, (Hazel.Connection) serverConnection);
      }
    }

    internal void SendData(byte[] bytes, EndPoint endPoint)
    {
      try
      {
        lock (this.listener)
          this.listener.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, endPoint, (AsyncCallback) (result => this.listener.EndSendTo(result)), (object) null);
      }
      catch (SocketException ex)
      {
        throw new HazelException("Could not send data as a SocketException occured.", (Exception) ex);
      }
      catch (ObjectDisposedException ex)
      {
      }
    }

    internal void RemoveConnectionTo(EndPoint endPoint)
    {
      lock (this.connections)
        this.connections.Remove(endPoint);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        lock (this.listener)
          this.listener.Close();
      }
      base.Dispose(disposing);
    }
  }
}
