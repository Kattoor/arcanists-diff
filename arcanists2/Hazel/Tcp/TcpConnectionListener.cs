// Decompiled with JetBrains decompiler
// Type: Hazel.Tcp.TcpConnectionListener
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Net;
using System.Net.Sockets;

#nullable disable
namespace Hazel.Tcp
{
  public sealed class TcpConnectionListener : NetworkConnectionListener
  {
    private Socket listener;

    [Obsolete("Temporary constructor in beta only, use NetworkEndPoint constructor instead.")]
    public TcpConnectionListener(IPAddress IPAddress, int port, IPMode mode = IPMode.IPv4)
      : this(new NetworkEndPoint(IPAddress, port, mode))
    {
    }

    public TcpConnectionListener(NetworkEndPoint endPoint)
    {
      this.EndPoint = endPoint.EndPoint;
      this.IPMode = endPoint.IPMode;
      if (endPoint.IPMode == IPMode.IPv4)
      {
        this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      }
      else
      {
        if (!Socket.OSSupportsIPv6)
          throw new HazelException("IPV6 not supported!");
        this.listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
        this.listener.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
      }
    }

    public override void Start()
    {
      try
      {
        lock (this.listener)
        {
          this.listener.Bind(this.EndPoint);
          this.listener.Listen(1000);
          this.listener.BeginAccept(new AsyncCallback(this.AcceptConnection), (object) null);
        }
      }
      catch (SocketException ex)
      {
      }
    }

    public override void StartNoTry()
    {
      lock (this.listener)
      {
        this.listener.Bind(this.EndPoint);
        this.listener.Listen(1000);
        this.listener.BeginAccept(new AsyncCallback(this.AcceptConnection), (object) null);
      }
    }

    private void AcceptConnection(IAsyncResult result)
    {
      lock (this.listener)
      {
        Socket socket;
        try
        {
          socket = this.listener.EndAccept(result);
        }
        catch (ObjectDisposedException ex)
        {
          return;
        }
        this.listener.BeginAccept(new AsyncCallback(this.AcceptConnection), (object) null);
        TcpConnection tcpConnection = new TcpConnection(socket);
        tcpConnection.StartWaitingForHandshake((Action<byte[]>) (bytes =>
        {
          this.InvokeNewConnection(bytes, (Hazel.Connection) tcpConnection);
          tcpConnection.StartReceiving();
        }));
      }
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
