// Decompiled with JetBrains decompiler
// Type: Hazel.Udp.UdpServerConnection
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Hazel.Udp
{
  internal sealed class UdpServerConnection : UdpConnection
  {
    private object stateLock = new object();

    public UdpConnectionListener Listener { get; private set; }

    internal UdpServerConnection(UdpConnectionListener listener, System.Net.EndPoint endPoint, IPMode IPMode)
    {
      this.Listener = listener;
      this.RemoteEndPoint = endPoint;
      this.EndPoint = (ConnectionEndPoint) new NetworkEndPoint(endPoint);
      this.IPMode = IPMode;
      this.State = ConnectionState.Connected;
    }

    protected override void WriteBytesToConnection(byte[] bytes)
    {
      lock (this.stateLock)
      {
        if (this.State != ConnectionState.Connected)
          throw new InvalidOperationException("Could not send data as this Connection is not connected. Did you disconnect?");
        this.Listener.SendData(bytes, this.RemoteEndPoint);
      }
    }

    public override void Connect(byte[] bytes = null, int timeout = 5000)
    {
      throw new HazelException("Cannot manually connect a UdpServerConnection, did you mean to use UdpClientConnection?");
    }

    internal void InvokeDataReceived(byte[] buffer)
    {
      byte[] bytes = this.HandleReceive(buffer, buffer.Length);
      if (bytes == null)
        return;
      this.InvokeDataReceived(bytes, (SendOption) buffer[0]);
    }

    protected override void HandleDisconnect(HazelException e = null)
    {
      bool flag = false;
      lock (this.stateLock)
      {
        if (this.State == ConnectionState.Connected)
        {
          this.State = ConnectionState.Disconnecting;
          flag = true;
        }
      }
      if (!flag)
        return;
      this.InvokeDisconnected((Exception) e);
      this.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.State == ConnectionState.Connected)
          this.SendDisconnect();
        lock (this.stateLock)
        {
          this.Listener.RemoveConnectionTo(this.RemoteEndPoint);
          this.State = ConnectionState.NotConnected;
        }
      }
      base.Dispose(disposing);
    }
  }
}
