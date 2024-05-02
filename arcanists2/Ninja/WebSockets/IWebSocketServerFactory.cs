// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.IWebSocketServerFactory
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
