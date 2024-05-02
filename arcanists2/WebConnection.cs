

using Hazel;
using Ninja.WebSockets;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
public class WebConnection : Connection
{
  public int id;
  private const int MaxMessageSize = 2097152;
  internal WebSocket webSocket;
  internal CancellationTokenSource cancellation;
  public bool NoDelay = true;
  private Uri uri;

  public override void Connect(byte[] bytes = null, int timeout = 5000)
  {
    throw new NotImplementedException();
  }

  public override void SendBytes(byte[] bytes, SendOption sendOption = SendOption.None)
  {
    this._SendBytes(this.Encrypt(bytes));
  }

  public override void SendBytesAndClose(byte[] bytes, SendOption sendOption = SendOption.FragmentedReliable)
  {
    this._SendBytes(this.Encrypt(bytes));
  }

  public override void SendBytesUnencrypted(byte[] bytes) => this._SendBytes(bytes);

  public WebConnection()
  {
  }

  public event Action<Exception> ReceivedError;

  public void SetDisconnected() => this.State = ConnectionState.Disconnecting;

  public async void Connect(Uri uri)
  {
    WebConnection webConnection = this;
    if (webConnection.webSocket != null)
    {
      webConnection.InvokeError(new Exception("Client already connected"));
    }
    else
    {
      webConnection.uri = uri;
      webConnection.State = ConnectionState.Connecting;
      webConnection.stopwatch = new Stopwatch();
      webConnection.stopwatch.Start();
      // ISSUE: reference to a compiler-generated method
      await Task.Run(new System.Action(webConnection.\u003CConnect\u003Eb__15_0));
    }
  }

  public WebConnection(WebSocket ws, string e, int id)
  {
    this.webSocket = ws;
    this.EndPoint = (ConnectionEndPoint) new WebEndPoint(e);
    this.id = id;
    this.State = ConnectionState.Connected;
    this.stopwatch = new Stopwatch();
    this.stopwatch.Start();
  }

  private async void RunThread()
  {
    WebConnection webConnection = this;
    WebSocketClientOptions options = new WebSocketClientOptions()
    {
      NoDelay = true,
      KeepAliveInterval = TimeSpan.Zero,
      SecWebSocketProtocol = "binary"
    };
    WebSocketClientFactory socketClientFactory = new WebSocketClientFactory();
    webConnection.cancellation = new CancellationTokenSource();
    try
    {
      WebSocket webSocket = await socketClientFactory.ConnectAsync(webConnection.uri, options, webConnection.cancellation.Token);
      using (webConnection.webSocket = webSocket)
      {
        CancellationToken token = webConnection.cancellation.Token;
        webConnection.State = ConnectionState.Connected;
        UnityThreadHelper.Dispatcher.Dispatch2((System.Action) (() => { }));
        await webConnection.ReceiveLoop(webConnection.webSocket, token);
      }
    }
    catch (ObjectDisposedException ex)
    {
    }
    catch (Exception ex)
    {
      webConnection.InvokeError(ex);
    }
    finally
    {
      webConnection.Disconnect();
      // ISSUE: reference to a compiler-generated method
      UnityThreadHelper.Dispatcher.Dispatch2(new System.Action(webConnection.\u003CRunThread\u003Eb__17_0));
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
    WebConnection webConnection1 = this;
    byte[] buffer = new byte[2097152];
    while (true)
    {
      WebConnection webConnection = webConnection1;
      WebSocketReceiveResult async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
      if (async != null && async.MessageType != WebSocketMessageType.Close)
      {
        byte[] data = await webConnection1.ReadFrames(async, webSocket, buffer);
        if (data.Length != 0)
        {
          try
          {
            UnityThreadHelper.Dispatcher.Dispatch2((System.Action) (() => webConnection.InvokeDataReceived(data, SendOption.Reliable)));
          }
          catch (Exception ex)
          {
            webConnection1.InvokeError(ex);
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
    this.State = ConnectionState.Disconnecting;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.State = ConnectionState.NotConnected;
      this.Disconnect();
    }
    base.Dispose(disposing);
  }

  public async void _SendBytes(byte[] data)
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

  public async void _SendBytesAndClose(byte[] data)
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
        this.InvokeError(ex);
      }
      finally
      {
        this.Disconnect();
      }
    }
  }

  public override string ToString()
  {
    if (this.State == ConnectionState.Connected)
      return string.Format("Websocket connected to {0}", (object) this.uri);
    return this.State == ConnectionState.Connecting ? string.Format("Websocket connecting to {0}", (object) this.uri) : "";
  }
}
