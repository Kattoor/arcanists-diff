

using System;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketServerOptions
  {
    public TimeSpan KeepAliveInterval { get; set; }

    public bool IncludeExceptionInCloseResponse { get; set; }

    public string SubProtocol { get; set; }

    public WebSocketServerOptions()
    {
      this.KeepAliveInterval = TimeSpan.FromSeconds(60.0);
      this.IncludeExceptionInCloseResponse = false;
      this.SubProtocol = (string) null;
    }
  }
}
