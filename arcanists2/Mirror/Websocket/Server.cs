

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
using UnityEngine;

#nullable disable
namespace Mirror.Websocket
{
  public class Server
  {
    private const int MaxMessageSize = 262144;
    private TcpListener listener;
    private readonly IWebSocketServerFactory webSocketServerFactory = (IWebSocketServerFactory) new WebSocketServerFactory();
    private CancellationTokenSource cancellation;
    private Dictionary<int, WebSocket> clients = new Dictionary<int, WebSocket>();
    public bool NoDelay = true;
    private static int counter;
    public bool _secure;
    public Server.SslConfiguration _sslConfig;

    public event Action<int> Connected;

    public event Action<int, ArraySegment<byte>> ReceivedData;

    public event Action<int> Disconnected;

    public event Action<int, Exception> ReceivedError;

    public static int NextConnectionId()
    {
      int num = Interlocked.Increment(ref Server.counter);
      return num != int.MaxValue ? num : throw new Exception("connection id limit reached: " + (object) num);
    }

    public bool Active => this.listener != null;

    public WebSocket GetClient(int connectionId) => this.clients[connectionId];

    public async void Listen(int port)
    {
      try
      {
        this.cancellation = new CancellationTokenSource();
        this.listener = TcpListener.Create(port);
        this.listener.Server.NoDelay = this.NoDelay;
        this.listener.Start();
        Debug.Log((object) string.Format("Websocket server started listening on port {0}", (object) port));
        while (true)
          this.ProcessTcpClient(await this.listener.AcceptTcpClientAsync(), this.cancellation.Token);
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

    private async void ProcessTcpClient(TcpClient tcpClient, CancellationToken token)
    {
      Server server = this;
      try
      {
        Stream stream = (Stream) tcpClient.GetStream();
        if (server._secure)
        {
          SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(server.CertVerificationCallback));
          sslStream.AuthenticateAsServer((X509Certificate) server._sslConfig.Certificate, server._sslConfig.ClientCertificateRequired, server._sslConfig.EnabledSslProtocols, server._sslConfig.CheckCertificateRevocation);
          stream = (Stream) sslStream;
        }
        WebSocketHttpContext context = await server.webSocketServerFactory.ReadHttpHeaderFromStreamAsync(stream, token);
        if (context.IsWebSocketRequest)
        {
          WebSocketServerOptions options = new WebSocketServerOptions()
          {
            KeepAliveInterval = TimeSpan.FromSeconds(30.0),
            SubProtocol = "binary"
          };
          WebSocket webSocket = await server.webSocketServerFactory.AcceptWebSocketAsync(context, options);
          await server.ReceiveLoopAsync(webSocket, token);
        }
        else
          Debug.Log((object) "Http header contains no web socket upgrade request. Ignoring");
      }
      catch (ObjectDisposedException ex)
      {
      }
      catch (Exception ex)
      {
        Action<int, Exception> receivedError = server.ReceivedError;
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
          Action<int, Exception> receivedError = server.ReceivedError;
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

    private async Task ReceiveLoopAsync(WebSocket webSocket, CancellationToken token)
    {
      int connectionId = Server.NextConnectionId();
      this.clients.Add(connectionId, webSocket);
      byte[] buffer = new byte[262144];
      try
      {
        Action<int> connected = this.Connected;
        if (connected != null)
          connected(connectionId);
        WebSocketReceiveResult async;
        while (true)
        {
          async = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);
          if (async.MessageType != WebSocketMessageType.Close)
          {
            ArraySegment<byte> arraySegment = await this.ReadFrames(connectionId, async, webSocket, buffer, token);
            if (arraySegment.Count != 0)
            {
              try
              {
                Action<int, ArraySegment<byte>> receivedData = this.ReceivedData;
                if (receivedData != null)
                  receivedData(connectionId, arraySegment);
              }
              catch (Exception ex)
              {
                Action<int, Exception> receivedError = this.ReceivedError;
                if (receivedError != null)
                  receivedError(connectionId, ex);
              }
            }
            else
              goto label_7;
          }
          else
            break;
        }
        Debug.Log((object) string.Format("Client initiated close. Status: {0} Description: {1}", (object) async.CloseStatus, (object) async.CloseStatusDescription));
        return;
label_7:;
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
        this.clients.Remove(connectionId);
        Action<int> disconnected = this.Disconnected;
        if (disconnected != null)
          disconnected(connectionId);
      }
    }

    private async Task<ArraySegment<byte>> ReadFrames(
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
          return new ArraySegment<byte>();
        }
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, count, 262144 - count), CancellationToken.None);
        count += result.Count;
      }
      return new ArraySegment<byte>(buffer, 0, count);
    }

    public void Stop()
    {
      if (!this.Active)
        return;
      Debug.Log((object) "Server: stopping...");
      this.cancellation.Cancel();
      this.listener.Stop();
      this.clients.Clear();
      this.listener = (TcpListener) null;
    }

    public async void Send(int connectionId, byte[] data)
    {
      int num;
      WebSocket webSocket;
      if (num == 0 || this.clients.TryGetValue(connectionId, out webSocket))
      {
        try
        {
          await webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, this.cancellation.Token);
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

    public string GetClientAddress(int connectionId)
    {
      return this.clients.TryGetValue(connectionId, out WebSocket _) ? "" : (string) null;
    }

    public bool Disconnect(int connectionId)
    {
      WebSocket webSocket;
      if (!this.clients.TryGetValue(connectionId, out webSocket))
        return false;
      this.clients.Remove(connectionId);
      webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
      Debug.Log((object) ("Server.Disconnect connectionId:" + (object) connectionId));
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
