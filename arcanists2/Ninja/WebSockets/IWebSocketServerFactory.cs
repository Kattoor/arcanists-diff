

using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public interface IWebSocketServerFactory
  {
    Task<WebSocketHttpContext> ReadHttpHeaderFromStreamAsync(Stream stream, CancellationToken token = default (CancellationToken));

    Task<WebSocket> AcceptWebSocketAsync(WebSocketHttpContext context, CancellationToken token = default (CancellationToken));

    Task<WebSocket> AcceptWebSocketAsync(
      WebSocketHttpContext context,
      WebSocketServerOptions options,
      CancellationToken token = default (CancellationToken));
  }
}
