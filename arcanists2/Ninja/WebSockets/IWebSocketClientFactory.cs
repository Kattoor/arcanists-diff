// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.IWebSocketClientFactory
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public interface IWebSocketClientFactory
  {
    Task<WebSocket> ConnectAsync(Uri uri, CancellationToken token = default (CancellationToken));

    Task<WebSocket> ConnectAsync(Uri uri, WebSocketClientOptions options, CancellationToken token = default (CancellationToken));

    Task<WebSocket> ConnectAsync(
      Stream responseStream,
      string secWebSocketKey,
      WebSocketClientOptions options,
      CancellationToken token = default (CancellationToken));
  }
}
