// Decompiled with JetBrains decompiler
// Type: Mirror.Websocket.Client
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Ninja.WebSockets;
using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Mirror.Websocket
{
  public class Client
  {
    private const int MaxMessageSize = 2097152;
    private WebSocket webSocket;
    private CancellationTokenSource cancellation;
    public bool NoDelay = true;
    private Uri uri;

    public event System.Action Connected;

    public event Action<byte[]> ReceivedData;

    public event System.Action Disconnected;

    public event Action<Exception> ReceivedError;

    public bool Connecting { get; set; }

    public bool IsConnected { get; set; }

    public async void Connect(Uri uri)
    {
      if (this.webSocket != null)
      {
        Action<Exception> receivedError = this.ReceivedError;
        if (receivedError == null)
          return;
        receivedError(new Exception("Client already connected"));
      }
      else
      {
        this.uri = uri;
        this.Connecting = true;
        WebSocketClientOptions options = new WebSocketClientOptions()
        {
          NoDelay = true,
          KeepAliveInterval = TimeSpan.Zero,
          SecWebSocketProtocol = "binary"
        };
        this.cancellation = new CancellationTokenSource();
        WebSocketClientFactory socketClientFactory = new WebSocketClientFactory();
        try
        {
          using (this.webSocket = await socketClientFactory.ConnectAsync(uri, options, this.cancellation.Token))
          {
            CancellationToken token = this.cancellation.Token;
            this.IsConnected = true;
            this.Connecting = false;
            System.Action connected = this.Connected;
            if (connected != null)
              connected();
            await this.ReceiveLoop(this.webSocket, token);
          }
        }
        catch (ObjectDisposedException ex)
        {
        }
        catch (Exception ex)
        {
          Action<Exception> receivedError = this.ReceivedError;
          if (receivedError == null)
            return;
          receivedError(ex);
        }
        finally
        {
          this.Disconnect();
          System.Action disconnected = this.Disconnected;
          if (disconnected != null)
            disconnected();
        }
      }
    }

    private async Task ReceiveLoop(WebSocket webSocket, CancellationToken token)
    {
      byte[] buffer = new byte[2097152];
      while (true)
      {
        WebSocketReceiveResult async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
        if (async != null && async.MessageType != WebSocketMessageType.Close)
        {
          byte[] numArray = await this.ReadFrames(async, webSocket, buffer);
          if (numArray.Length != 0)
          {
            try
            {
              Action<byte[]> receivedData = this.ReceivedData;
              if (receivedData != null)
                receivedData(numArray);
            }
            catch (Exception ex)
            {
              Action<Exception> receivedError = this.ReceivedError;
              if (receivedError != null)
                receivedError(ex);
            }
          }
          else
            goto label_9;
        }
        else
          break;
      }
      return;
label_9:;
    }

    private async Task<byte[]> ReadFrames(
      WebSocketReceiveResult result,
      WebSocket webSocket,
      byte[] buffer)
    {
      int count = result.Count;
      while (!result.EndOfMessage)
      {
        if (count >= 2097152)
        {
          await webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, string.Format("Maximum message size: {0} bytes.", (object) 2097152), CancellationToken.None);
          Action<Exception> receivedError = this.ReceivedError;
          if (receivedError != null)
            receivedError((Exception) new WebSocketException(WebSocketError.HeaderError));
          return new byte[0];
        }
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, count, 2097152 - count), CancellationToken.None);
        count += result.Count;
      }
      byte[] dst = new byte[count];
      System.Buffer.BlockCopy((Array) buffer, 0, (Array) dst, 0, count);
      return dst;
    }

    public void Disconnect()
    {
      this.cancellation?.Cancel();
      if (this.webSocket == null)
        return;
      this.webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
      this.webSocket = (WebSocket) null;
      this.Connecting = false;
      this.IsConnected = false;
    }

    public async void SendBytes(byte[] data)
    {
      int num;
      if (num != 0 && this.webSocket == null)
      {
        Action<Exception> receivedError = this.ReceivedError;
        if (receivedError == null)
          return;
        receivedError((Exception) new SocketException(10057));
      }
      else
      {
        try
        {
          await this.webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
        }
        catch (Exception ex)
        {
          this.Disconnect();
          Action<Exception> receivedError = this.ReceivedError;
          if (receivedError == null)
            return;
          receivedError(ex);
        }
      }
    }

    public override string ToString()
    {
      if (this.IsConnected)
        return string.Format("Websocket connected to {0}", (object) this.uri);
      return this.Connecting ? string.Format("Websocket connecting to {0}", (object) this.uri) : "";
    }
  }
}
