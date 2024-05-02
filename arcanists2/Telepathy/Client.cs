// Decompiled with JetBrains decompiler
// Type: Telepathy.Client
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace Telepathy
{
  public class Client : Common
  {
    public TcpClient client;
    private Thread receiveThread;
    private Thread sendThread;
    private volatile bool _Connecting;
    private SafeQueue<byte[]> sendQueue = new SafeQueue<byte[]>();
    private ManualResetEvent sendPending = new ManualResetEvent(false);

    public bool Connected
    {
      get => this.client != null && this.client.Client != null && this.client.Client.Connected;
    }

    public bool Connecting => this._Connecting;

    private void ReceiveThreadFunction(string ip, int port)
    {
      try
      {
        this.client.Connect(ip, port);
        this._Connecting = false;
        this.client.NoDelay = this.NoDelay;
        this.client.SendTimeout = this.SendTimeout;
        this.sendThread = new Thread((ThreadStart) (() => Common.SendLoop(0, this.client, this.sendQueue, this.sendPending)));
        this.sendThread.IsBackground = true;
        this.sendThread.Start();
        Common.ReceiveLoop(0, this.client, this.receiveQueue, this.MaxMessageSize);
      }
      catch (SocketException ex)
      {
        Logger.Log("Client Recv: failed to connect to ip=" + ip + " port=" + (object) port + " reason=" + (object) ex);
        this.receiveQueue.Enqueue(new Message(0, EventType.Disconnected, (byte[]) null));
      }
      catch (Exception ex)
      {
        Logger.LogError("Client Recv Exception: " + (object) ex);
      }
      this.sendThread?.Interrupt();
      this._Connecting = false;
      this.client.Close();
    }

    public void Connect(string ip, int port)
    {
      if (this.Connecting || this.Connected)
        return;
      this._Connecting = true;
      this.client = new TcpClient();
      this.client.Client = (Socket) null;
      this.receiveQueue = new ConcurrentQueue<Message>();
      this.sendQueue.Clear();
      this.receiveThread = new Thread((ThreadStart) (() => this.ReceiveThreadFunction(ip, port)));
      this.receiveThread.IsBackground = true;
      this.receiveThread.Start();
    }

    public void Disconnect()
    {
      if (!this.Connecting && !this.Connected)
        return;
      this.client.Close();
      this.receiveThread?.Join();
      this.sendQueue.Clear();
      this.client = (TcpClient) null;
    }

    public bool Send(byte[] data)
    {
      if (this.Connected)
      {
        if (data.Length <= this.MaxMessageSize)
        {
          this.sendQueue.Enqueue(data);
          this.sendPending.Set();
          return true;
        }
        Logger.LogError("Client.Send: message too big: " + (object) data.Length + ". Limit: " + (object) this.MaxMessageSize);
        return false;
      }
      Logger.LogWarning("Client.Send: not connected!");
      return false;
    }
  }
}
