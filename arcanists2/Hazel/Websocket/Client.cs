// Decompiled with JetBrains decompiler
// Type: Hazel.Websocket.Client
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Ninja.WebSockets;
using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Hazel.Websocket
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
      Client client = this;
      if (client.webSocket != null)
      {
        client.InvokeError(new Exception("Client already connected"));
      }
      else
      {
        client.uri = uri;
        client.Connecting = true;
        // ISSUE: reference to a compiler-generated method
        await Task.Run(new System.Action(client.\u003CConnect\u003Eb__25_0));
      }
    }

    private async void RunThread()
    {
      Client client = this;
      WebSocketClientOptions options = new WebSocketClientOptions()
      {
        NoDelay = true,
        KeepAliveInterval = TimeSpan.Zero,
        SecWebSocketProtocol = "binary"
      };
      WebSocketClientFactory socketClientFactory = new WebSocketClientFactory();
      client.cancellation = new CancellationTokenSource();
      try
      {
        WebSocket webSocket = await socketClientFactory.ConnectAsync(client.uri, options, client.cancellation.Token);
        using (client.webSocket = webSocket)
        {
          CancellationToken token = client.cancellation.Token;
          client.IsConnected = true;
          client.Connecting = false;
          // ISSUE: reference to a compiler-generated method
          UnityThreadHelper.Dispatcher.Dispatch2(new System.Action(client.\u003CRunThread\u003Eb__26_1));
          await client.ReceiveLoop(client.webSocket, token);
        }
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (Exception ex)
      {
        client.InvokeError(ex);
      }
      finally
      {
        client.Disconnect();
        // ISSUE: reference to a compiler-generated method
        UnityThreadHelper.Dispatcher.Dispatch2(new System.Action(client.\u003CRunThread\u003Eb__26_0));
      }
    }

    private void InvokeError(Exception ex)
    {
      if (this.ReceivedError == null)
        return;
      UnityThreadHelper.Dispatcher.Dispatch2((System.Action) (() =>
      {
        Action<Exception> receivedError = this.ReceivedError;
        if (receivedError == null)
          return;
        receivedError(ex);
      }));
    }

    private async Task ReceiveLoop(WebSocket webSocket, CancellationToken token)
    {
      Client client1 = this;
      byte[] buffer = new byte[2097152];
      while (true)
      {
        Client client = client1;
        WebSocketReceiveResult async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
        if (async != null && async.MessageType != WebSocketMessageType.Close)
        {
          byte[] data = await client1.ReadFrames(async, webSocket, buffer);
          if (data.Length != 0)
          {
            try
            {
              UnityThreadHelper.Dispatcher.Dispatch2((System.Action) (() =>
              {
                Action<byte[]> receivedData = client.ReceivedData;
                if (receivedData == null)
                  return;
                receivedData(data);
              }));
            }
            catch (Exception ex)
            {
              client1.InvokeError(ex);
            }
          }
          else
            goto label_8;
        }
        else
          break;
      }
      return;
label_8:;
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
          this.InvokeError((Exception) new WebSocketException(WebSocketError.HeaderError));
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
        this.InvokeError((Exception) new SocketException(10057));
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
          this.InvokeError(ex);
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
