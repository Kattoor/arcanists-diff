

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketHttpContext
  {
    public bool IsWebSocketRequest { get; private set; }

    public IList<string> WebSocketRequestedProtocols { get; private set; }

    public string HttpHeader { get; private set; }

    public string Path { get; private set; }

    public Stream Stream { get; private set; }

    public WebSocketHttpContext(
      bool isWebSocketRequest,
      IList<string> webSocketRequestedProtocols,
      string httpHeader,
      string path,
      Stream stream)
    {
      this.IsWebSocketRequest = isWebSocketRequest;
      this.WebSocketRequestedProtocols = webSocketRequestedProtocols;
      this.HttpHeader = httpHeader;
      this.Path = path;
      this.Stream = stream;
    }
  }
}
