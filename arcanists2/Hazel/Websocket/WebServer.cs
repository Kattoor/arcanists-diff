// Decompiled with JetBrains decompiler
// Type: Hazel.Websocket.WebServer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Hazel.Websocket
{
  public class WebServer
  {
    private const int MaxMessageSize = 262144;
    private TcpListener listener;
    private readonly IWebSocketServerFactory webSocketServerFactory = (IWebSocketServerFactory) new WebSocketServerFactory();
    private CancellationTokenSource cancellation;
    private Dictionary<int, WebConnectionOld> clients = new Dictionary<int, WebConnectionOld>();
    public bool NoDelay = true;
    private static int counter;
    public bool _secure;
    public WebServer.SslConfiguration _sslConfig;

    public event Action<WebConnectionOld> Connected;

    public event Action<WebConnectionOld> Disconnected;

    public event Action<int, Exception> ReceivedError;

    public static int NextConnectionId()
    {
      int num = Interlocked.Increment(ref WebServer.counter);
      return num != int.MaxValue ? num : throw new Exception("connection id limit reached: " + (object) num);
    }

    public bool Active => this.listener != null;

    public WebConnectionOld GetClient(int connectionId) => this.clients[connectionId];

    public async void Listen(int port)
    {
      try
      {
        this.cancellation = new CancellationTokenSource();
        this.listener = TcpListener.Create(port);
        this.listener.Server.NoDelay = this.NoDelay;
        this.listener.Start();
        while (true)
        {
          TcpClient tcpClient = await this.listener.AcceptTcpClientAsync();
          this.ProcessTcpClient(tcpClient, this.cancellation.Token, tcpClient.Client.RemoteEndPoint.ToString());
        }
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
    }

    private async void ProcessTcpClient(TcpClient tcpClient, CancellationToken token, string ip)
    {
      WebServer webServer = this;
      try
      {
        Stream stream = (Stream) tcpClient.GetStream();
        if (webServer._secure)
        {
          SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(webServer.CertVerificationCallback));
          sslStream.AuthenticateAsServer((X509Certificate) webServer._sslConfig.Certificate, webServer._sslConfig.ClientCertificateRequired, webServer._sslConfig.EnabledSslProtocols, webServer._sslConfig.CheckCertificateRevocation);
          stream = (Stream) sslStream;
        }
        WebSocketHttpContext context = await webServer.webSocketServerFactory.ReadHttpHeaderFromStreamAsync(stream, token);
        if (!context.IsWebSocketRequest)
          return;
        WebSocketServerOptions options = new WebSocketServerOptions()
        {
          KeepAliveInterval = TimeSpan.FromSeconds(30.0),
          SubProtocol = "binary"
        };
        WebSocket webSocket = await webServer.webSocketServerFactory.AcceptWebSocketAsync(context, options);
        await webServer.ReceiveLoopAsync(webSocket, token, ip);
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (Exception ex)
      {
        Action<int, Exception> receivedError = webServer.ReceivedError;
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
          Action<int, Exception> receivedError = webServer.ReceivedError;
          if (receivedError != null)
            receivedError(0, ex);
        }
      }
    }

    private bool CertVerificationCallback(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

    private async Task ReceiveLoopAsync(WebSocket webSocket, CancellationToken token, string ip)
    {
      int connectionId = WebServer.NextConnectionId();
      WebConnectionOld con = new WebConnectionOld(webSocket, ip, connectionId);
      this.clients.Add(connectionId, con);
      byte[] buffer = new byte[262144];
      try
      {
        Action<WebConnectionOld> connected = this.Connected;
        if (connected != null)
          connected(con);
        while (true)
        {
          WebSocketReceiveResult async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
          if (async.MessageType != WebSocketMessageType.Close)
          {
            byte[] b = await this.ReadFrames(connectionId, async, webSocket, buffer, token);
            if (b.Length != 0)
            {
              try
              {
                ++con.messagesReceived;
                con.DataReceived(b);
              }
              catch (Exception ex)
              {
                Action<int, Exception> receivedError = this.ReceivedError;
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
        Action<int, Exception> receivedError = this.ReceivedError;
        if (receivedError == null)
          return;
        receivedError(connectionId, ex);
      }
      finally
      {
        con.Connected = false;
        this.clients.Remove(connectionId);
        Action<WebConnectionOld> disconnected = this.Disconnected;
        if (disconnected != null)
          disconnected(con);
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
        if (count >= 262144)
        {
          await webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, string.Format("Maximum message size: {0} bytes.", (object) 262144), CancellationToken.None);
          Action<int, Exception> receivedError = this.ReceivedError;
          if (receivedError != null)
            receivedError(connectionId, (Exception) new WebSocketException(WebSocketError.HeaderError));
          return new byte[0];
        }
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, count, 262144 - count), CancellationToken.None);
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
      WebConnectionOld webConnectionOld;
      if (num == 0 || this.clients.TryGetValue(connectionId, out webConnectionOld))
      {
        try
        {
          await webConnectionOld.socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
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

    public async void Send(WebConnectionOld client, byte[] data)
    {
      try
      {
        await client.socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
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
      WebConnectionOld webConnectionOld;
      if (!this.clients.TryGetValue(connectionId, out webConnectionOld))
        return false;
      webConnectionOld.Connected = false;
      this.clients.Remove(connectionId);
      webConnectionOld.socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
      Action<WebConnectionOld> disconnected = this.Disconnected;
      if (disconnected != null)
        disconnected(webConnectionOld);
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
      public SslProtocols EnabledSslProtocols;
      public bool CheckCertificateRevocation;
    }
  }
}
