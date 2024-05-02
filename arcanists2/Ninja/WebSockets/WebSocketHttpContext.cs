// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketHttpContext
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
