// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketClientFactory
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Ninja.WebSockets.Exceptions;
using Ninja.WebSockets.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketClientFactory : IWebSocketClientFactory
  {
    private readonly Func<MemoryStream> _bufferFactory;
    private readonly IBufferPool _bufferPool;

    public WebSocketClientFactory()
    {
      this._bufferPool = (IBufferPool) new BufferPool();
      this._bufferFactory = new Func<MemoryStream>(this._bufferPool.GetBuffer);
    }

    public WebSocketClientFactory(Func<MemoryStream> bufferFactory)
    {
      this._bufferFactory = bufferFactory;
    }

    public async Task<WebSocket> ConnectAsync(Uri uri, CancellationToken token = default (CancellationToken))
    {
      return await this.ConnectAsync(uri, new WebSocketClientOptions(), token);
    }

    public async Task<WebSocket> ConnectAsync(
      Uri uri,
      WebSocketClientOptions options,
      CancellationToken token = default (CancellationToken))
    {
      Guid guid = Guid.NewGuid();
      string host = uri.Host;
      int port = uri.Port;
      TcpClient tcpClient = new TcpClient(AddressFamily.InterNetworkV6);
      tcpClient.NoDelay = options.NoDelay;
      tcpClient.Client.DualMode = true;
      string lower = uri.Scheme.ToLower();
      bool useSsl = lower == "wss" || lower == "https";
      IPAddress address;
      if (IPAddress.TryParse(host, out address))
      {
        Events.Log.ClientConnectingToIpAddress(guid, address.ToString(), port);
        await tcpClient.ConnectAsync(address, port);
      }
      else
      {
        Events.Log.ClientConnectingToHost(guid, host, port);
        await tcpClient.ConnectAsync(host, port);
      }
      token.ThrowIfCancellationRequested();
      Stream stream = this.GetStream(guid, tcpClient, useSsl, host);
      return await this.PerformHandshake(guid, uri, stream, options, token);
    }

    public async Task<WebSocket> ConnectAsync(
      Stream responseStream,
      string secWebSocketKey,
      WebSocketClientOptions options,
      CancellationToken token = default (CancellationToken))
    {
      return await this.ConnectAsync(Guid.NewGuid(), responseStream, secWebSocketKey, options.KeepAliveInterval, options.SecWebSocketExtensions, options.IncludeExceptionInCloseResponse, token);
    }

    private async Task<WebSocket> ConnectAsync(
      Guid guid,
      Stream responseStream,
      string secWebSocketKey,
      TimeSpan keepAliveInterval,
      string secWebSocketExtensions,
      bool includeExceptionInCloseResponse,
      CancellationToken token)
    {
      Events.Log.ReadingHttpResponse(guid);
      string empty = string.Empty;
      string str;
      try
      {
        str = await HttpHelper.ReadHttpHeaderAsync(responseStream, token);
      }
      catch (Exception ex)
      {
        Events.Log.ReadHttpResponseError(guid, ex.ToString());
        throw new WebSocketHandshakeFailedException("Handshake unexpected failure", ex);
      }
      this.ThrowIfInvalidResponseCode(str);
      this.ThrowIfInvalidAcceptString(guid, str, secWebSocketKey);
      string protocolFromHeader = this.GetSubProtocolFromHeader(str);
      return (WebSocket) new WebSocketImplementation(guid, this._bufferFactory, responseStream, keepAliveInterval, secWebSocketExtensions, includeExceptionInCloseResponse, true, protocolFromHeader);
    }

    private string GetSubProtocolFromHeader(string response)
    {
      Match match = new Regex("Sec-WebSocket-Protocol: (.*)", RegexOptions.IgnoreCase).Match(response);
      return match.Success ? match.Groups[1].Value.Trim() : (string) null;
    }

    private void ThrowIfInvalidAcceptString(Guid guid, string response, string secWebSocketKey)
    {
      string str = new Regex("Sec-WebSocket-Accept: (.*)", RegexOptions.IgnoreCase).Match(response).Groups[1].Value.Trim();
      string socketAcceptString = HttpHelper.ComputeSocketAcceptString(secWebSocketKey);
      if (socketAcceptString != str)
      {
        string message = string.Format("Handshake failed because the accept string from the server '" + socketAcceptString + "' was not the expected string '" + str + "'");
        Events.Log.HandshakeFailure(guid, message);
        throw new WebSocketHandshakeFailedException(message);
      }
      Events.Log.ClientHandshakeSuccess(guid);
    }

    private void ThrowIfInvalidResponseCode(string responseHeader)
    {
      string str = HttpHelper.ReadHttpResponseCode(responseHeader);
      if (string.Equals(str, "101 Switching Protocols", StringComparison.InvariantCultureIgnoreCase))
        return;
      string[] strArray = responseHeader.Split(new string[1]
      {
        "\r\n"
      }, StringSplitOptions.None);
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        if (string.IsNullOrWhiteSpace(strArray[index1]))
        {
          StringBuilder stringBuilder = new StringBuilder();
          for (int index2 = index1 + 1; index2 < strArray.Length - 1; ++index2)
            stringBuilder.AppendLine(strArray[index2]);
          string responseDetails = stringBuilder.ToString();
          throw new InvalidHttpResponseCodeException(str, responseDetails, responseHeader);
        }
      }
    }

    private Stream GetStream(Guid guid, TcpClient tcpClient, bool isSecure, string host)
    {
      Stream stream1 = (Stream) tcpClient.GetStream();
      if (isSecure)
      {
        SslStream stream2 = new SslStream(stream1, false, new RemoteCertificateValidationCallback(WebSocketClientFactory.ValidateServerCertificate), (LocalCertificateSelectionCallback) null);
        Events.Log.AttemtingToSecureSslConnection(guid);
        stream2.AuthenticateAsClient(host);
        Events.Log.ConnectionSecured(guid);
        return (Stream) stream2;
      }
      Events.Log.ConnectionNotSecure(guid);
      return stream1;
    }

    private static bool ValidateServerCertificate(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      if (sslPolicyErrors == SslPolicyErrors.None)
        return true;
      Events.Log.SslCertificateError(sslPolicyErrors);
      return false;
    }

    private static string GetAdditionalHeaders(Dictionary<string, string> additionalHeaders)
    {
      if (additionalHeaders == null || additionalHeaders.Count == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> additionalHeader in additionalHeaders)
        stringBuilder.Append(additionalHeader.Key + ": " + additionalHeader.Value + "\r\n");
      return stringBuilder.ToString();
    }

    private async Task<WebSocket> PerformHandshake(
      Guid guid,
      Uri uri,
      Stream stream,
      WebSocketClientOptions options,
      CancellationToken token)
    {
      Random random = new Random();
      byte[] inArray = new byte[16];
      byte[] buffer = inArray;
      random.NextBytes(buffer);
      string base64String = Convert.ToBase64String(inArray);
      string additionalHeaders = WebSocketClientFactory.GetAdditionalHeaders(options.AdditionalHttpHeaders);
      string str = "GET " + uri.PathAndQuery + " HTTP/1.1\r\n" + string.Format("Host: {0}:{1}\r\n", (object) uri.Host, (object) uri.Port) + "Upgrade: websocket\r\nConnection: Upgrade\r\nSec-WebSocket-Key: " + base64String + "\r\n" + string.Format("Origin: http://{0}:{1}\r\n", (object) uri.Host, (object) uri.Port) + "Sec-WebSocket-Protocol: " + options.SecWebSocketProtocol + "\r\n" + additionalHeaders + "Sec-WebSocket-Version: 13\r\n\r\n";
      byte[] bytes = Encoding.UTF8.GetBytes(str);
      stream.Write(bytes, 0, bytes.Length);
      Events.Log.HandshakeSent(guid, str);
      return await this.ConnectAsync(stream, base64String, options, token);
    }
  }
}
