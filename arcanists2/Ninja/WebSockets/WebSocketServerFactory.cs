

using Ninja.WebSockets.Exceptions;
using Ninja.WebSockets.Internal;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketServerFactory : IWebSocketServerFactory
  {
    private readonly Func<MemoryStream> _bufferFactory;
    private readonly IBufferPool _bufferPool;

    public WebSocketServerFactory()
    {
      this._bufferPool = (IBufferPool) new BufferPool();
      this._bufferFactory = new Func<MemoryStream>(this._bufferPool.GetBuffer);
    }

    public WebSocketServerFactory(Func<MemoryStream> bufferFactory)
    {
      this._bufferFactory = bufferFactory;
    }

    public async Task<WebSocketHttpContext> ReadHttpHeaderFromStreamAsync(
      Stream stream,
      CancellationToken token = default (CancellationToken))
    {
      string str = await HttpHelper.ReadHttpHeaderAsync(stream, token);
      string pathFromHeader = HttpHelper.GetPathFromHeader(str);
      return new WebSocketHttpContext(HttpHelper.IsWebSocketUpgradeRequest(str), HttpHelper.GetSubProtocols(str), str, pathFromHeader, stream);
    }

    public async Task<WebSocket> AcceptWebSocketAsync(
      WebSocketHttpContext context,
      CancellationToken token = default (CancellationToken))
    {
      return await this.AcceptWebSocketAsync(context, new WebSocketServerOptions(), token);
    }

    public async Task<WebSocket> AcceptWebSocketAsync(
      WebSocketHttpContext context,
      WebSocketServerOptions options,
      CancellationToken token = default (CancellationToken))
    {
      Guid guid = Guid.NewGuid();
      Events.Log.AcceptWebSocketStarted(guid);
      await WebSocketServerFactory.PerformHandshakeAsync(guid, context.HttpHeader, options.SubProtocol, context.Stream, token);
      Events.Log.ServerHandshakeSuccess(guid);
      string secWebSocketExtensions = (string) null;
      return (WebSocket) new WebSocketImplementation(guid, this._bufferFactory, context.Stream, options.KeepAliveInterval, secWebSocketExtensions, options.IncludeExceptionInCloseResponse, false, options.SubProtocol);
    }

    private static void CheckWebSocketVersion(string httpHeader)
    {
      Match match = new Regex("Sec-WebSocket-Version: (.*)", RegexOptions.IgnoreCase).Match(httpHeader);
      if (!match.Success)
        throw new WebSocketVersionNotSupportedException("Cannot find \"Sec-WebSocket-Version\" in http header");
      int int32 = Convert.ToInt32(match.Groups[1].Value.Trim());
      if (int32 < 13)
        throw new WebSocketVersionNotSupportedException(string.Format("WebSocket Version {0} not suported. Must be {1} or above", (object) int32, (object) 13));
    }

    private static async Task PerformHandshakeAsync(
      Guid guid,
      string httpHeader,
      string subProtocol,
      Stream stream,
      CancellationToken token)
    {
      try
      {
        Regex regex = new Regex("Sec-WebSocket-Key: (.*)", RegexOptions.IgnoreCase);
        WebSocketServerFactory.CheckWebSocketVersion(httpHeader);
        string input = httpHeader;
        Match match = regex.Match(input);
        if (!match.Success)
          throw new SecWebSocketKeyMissingException("Unable to read \"Sec-WebSocket-Key\" from http header");
        string socketAcceptString = HttpHelper.ComputeSocketAcceptString(match.Groups[1].Value.Trim());
        string response = "HTTP/1.1 101 Switching Protocols\r\nConnection: Upgrade\r\nUpgrade: websocket\r\n" + (subProtocol != null ? "Sec-WebSocket-Protocol: " + subProtocol + "\r\n" : "") + "Sec-WebSocket-Accept: " + socketAcceptString;
        Events.Log.SendingHandshakeResponse(guid, response);
        await HttpHelper.WriteHttpHeaderAsync(response, stream, token);
      }
      catch (WebSocketVersionNotSupportedException ex)
      {
        Events.Log.WebSocketVersionNotSupported(guid, ex.ToString());
        await HttpHelper.WriteHttpHeaderAsync("HTTP/1.1 426 Upgrade Required\r\nSec-WebSocket-Version: 13" + ex.Message, stream, token);
        throw;
      }
      catch (Exception ex)
      {
        Events.Log.BadRequest(guid, ex.ToString());
        await HttpHelper.WriteHttpHeaderAsync("HTTP/1.1 400 Bad Request", stream, token);
        throw;
      }
    }
  }
}
