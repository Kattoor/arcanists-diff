

using Hazel;
using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

#nullable disable
public class WebListener : ConnectionListener
{
  private const int MaxMessageSize = 1048576;
  private TcpListener listener;
  private readonly IWebSocketServerFactory webSocketServerFactory = (IWebSocketServerFactory) new WebSocketServerFactory();
  private CancellationTokenSource cancellation;
  private Dictionary<int, WebConnection> clients = new Dictionary<int, WebConnection>();
  public bool NoDelay = true;
  private static int counter;
  public bool _secure = true;
  public WebListener.SslConfiguration _sslConfig = new WebListener.SslConfiguration();

  public override void StartNoTry() => throw new NotImplementedException();

  public override void Start() => throw new NotImplementedException();

  public event Action<int, Exception> ReceivedError;

  public static int NextConnectionId()
  {
    int num = Interlocked.Increment(ref WebListener.counter);
    return num != int.MaxValue ? num : throw new Exception("connection id limit reached: " + (object) num);
  }

  public bool Active => this.listener != null;

  public WebConnection GetClient(int connectionId) => this.clients[connectionId];

  public async void Listen(int port)
  {
    WebListener w = this;
    try
    {
      if (w._secure)
        w._sslConfig.SetKey(w);
      w.cancellation = new CancellationTokenSource();
      w.listener = TcpListener.Create(port);
      w.listener.Server.NoDelay = w.NoDelay;
      w.listener.Start();
      while (true)
      {
        TcpClient tcpClient = await w.listener.AcceptTcpClientAsync();
        w.ProcessTcpClient(tcpClient, w.cancellation.Token, tcpClient.Client.RemoteEndPoint.ToString());
      }
    }
    catch (ObjectDisposedException ex)
    {
    }
    catch (Exception ex)
    {
      Action<int, Exception> receivedError = w.ReceivedError;
      if (receivedError == null)
        return;
      receivedError(0, ex);
    }
  }

  private async void ProcessTcpClient(TcpClient tcpClient, CancellationToken token, string ip)
  {
    try
    {
      WebSocketHttpContext context = await this.webSocketServerFactory.ReadHttpHeaderFromStreamAsync((Stream) tcpClient.GetStream(), token);
      if (context.IsWebSocketRequest)
      {
        WebSocketServerOptions options = new WebSocketServerOptions()
        {
          KeepAliveInterval = TimeSpan.FromSeconds(30.0),
          SubProtocol = "binary"
        };
        await this.ReceiveLoopAsync(await this.webSocketServerFactory.AcceptWebSocketAsync(context, options), token, ip);
      }
      else
        Debug.Log((object) "Http header contains no web socket upgrade request. Ignoring");
    }
    catch (ObjectDisposedException ex)
    {
    }
    catch (Exception ex)
    {
      Action<int, Exception> receivedError = this.ReceivedError;
      if (receivedError == null)
        return;
      receivedError(0, ex);
    }
    finally
    {
      try
      {
        tcpClient.Client.Close();
        tcpClient.Close();
      }
      catch (Exception ex)
      {
        Action<int, Exception> receivedError = this.ReceivedError;
        if (receivedError != null)
          receivedError(0, ex);
      }
    }
  }

  private async Task ReceiveLoopAsync(WebSocket webSocket, CancellationToken token, string ip)
  {
    WebListener webListener = this;
    int connectionId = WebListener.NextConnectionId();
    WebConnection con = new WebConnection(webSocket, ip, connectionId);
    con.cancellation = new CancellationTokenSource();
    webListener.clients.Add(connectionId, con);
    byte[] buffer = new byte[1048576];
    try
    {
      webListener.InvokeNewConnection(new byte[0], (Connection) con);
      while (true)
      {
        WebSocketReceiveResult async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
        if (async.MessageType != WebSocketMessageType.Close)
        {
          byte[] bytes = await webListener.ReadFrames(connectionId, async, webSocket, buffer, token);
          if (bytes.Length != 0)
          {
            try
            {
              con.Statistics.LogReceive(bytes.Length, bytes.Length + 4);
              con.InvokeDataReceived(bytes, SendOption.Reliable);
            }
            catch (Exception ex)
            {
              Action<int, Exception> receivedError = webListener.ReceivedError;
              if (receivedError != null)
                receivedError(connectionId, ex);
            }
          }
          else
            goto label_2;
        }
        else
          break;
      }
      return;
label_2:;
    }
    catch (Exception ex)
    {
      Action<int, Exception> receivedError = webListener.ReceivedError;
      if (receivedError == null)
        return;
      receivedError(connectionId, ex);
    }
    finally
    {
      con.SetDisconnected();
      webListener.clients.Remove(connectionId);
      con.InvokeDisconnected();
      con.Dispose();
    }
  }

  private async Task<byte[]> ReadFrames(
    int connectionId,
    WebSocketReceiveResult result,
    WebSocket webSocket,
    byte[] buffer,
    CancellationToken token)
  {
    int count = result.Count;
    while (!result.EndOfMessage)
    {
      if (count >= 1048576)
      {
        await webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, string.Format("Maximum message size: {0} bytes.", (object) 1048576), CancellationToken.None);
        Action<int, Exception> receivedError = this.ReceivedError;
        if (receivedError != null)
          receivedError(connectionId, (Exception) new WebSocketException(WebSocketError.HeaderError));
        return new byte[0];
      }
      result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, count, 1048576 - count), CancellationToken.None);
      count += result.Count;
    }
    byte[] dst = new byte[count];
    System.Buffer.BlockCopy((Array) buffer, 0, (Array) dst, 0, count);
    return dst;
  }

  public void Stop()
  {
    if (!this.Active)
      return;
    this.cancellation.Cancel();
    this.listener.Stop();
    this.clients.Clear();
    this.listener = (TcpListener) null;
  }

  public async void Send(int connectionId, byte[] data)
  {
    int num;
    WebConnection webConnection;
    if (num == 0 || this.clients.TryGetValue(connectionId, out webConnection))
    {
      try
      {
        await webConnection.webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
      }
      catch (ObjectDisposedException ex)
      {
        this.Disconnect(connectionId);
      }
      catch (Exception ex)
      {
        if (this.clients.ContainsKey(connectionId))
        {
          Action<int, Exception> receivedError = this.ReceivedError;
          if (receivedError != null)
            receivedError(connectionId, ex);
        }
        this.Disconnect(connectionId);
      }
    }
    else
    {
      Action<int, Exception> receivedError = this.ReceivedError;
      if (receivedError == null)
        return;
      receivedError(connectionId, (Exception) new SocketException(10057));
    }
  }

  public async void Send(WebConnection client, byte[] data)
  {
    try
    {
      await client.webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
    }
    catch (ObjectDisposedException ex)
    {
      this.Disconnect(client.id);
    }
    catch (Exception ex)
    {
      if (this.clients.ContainsKey(client.id))
      {
        Action<int, Exception> receivedError = this.ReceivedError;
        if (receivedError != null)
          receivedError(client.id, ex);
      }
      this.Disconnect(client.id);
    }
  }

  public bool Disconnect(int connectionId)
  {
    WebConnection webConnection;
    if (!this.clients.TryGetValue(connectionId, out webConnection))
      return false;
    webConnection.SetDisconnected();
    this.clients.Remove(connectionId);
    webConnection.webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
    webConnection.InvokeDisconnected();
    return true;
  }

  public override string ToString()
  {
    return this.Active ? string.Format("Websocket server {0}", (object) this.listener.LocalEndpoint) : "";
  }

  public class SslConfiguration
  {
    public X509Certificate2 Certificate;
    public bool ClientCertificateRequired;
    public SslProtocols EnabledSslProtocols = SslProtocols.Tls12;
    public bool CheckCertificateRevocation;

    public void SetKey(WebListener w)
    {
    }
  }
}
