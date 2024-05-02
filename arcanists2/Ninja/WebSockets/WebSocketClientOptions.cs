

using System;
using System.Collections.Generic;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketClientOptions
  {
    public TimeSpan KeepAliveInterval { get; set; }

    public bool NoDelay { get; set; }

    public Dictionary<string, string> AdditionalHttpHeaders { get; set; }

    public bool IncludeExceptionInCloseResponse { get; set; }

    public string SecWebSocketExtensions { get; set; }

    public string SecWebSocketProtocol { get; set; }

    public WebSocketClientOptions()
    {
      this.KeepAliveInterval = TimeSpan.Zero;
      this.NoDelay = true;
      this.AdditionalHttpHeaders = new Dictionary<string, string>();
      this.IncludeExceptionInCloseResponse = false;
      this.SecWebSocketProtocol = (string) null;
    }
  }
}
