// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.PingPongManager
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Ninja.WebSockets.Internal;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets
{
  public class PingPongManager : IPingPongManager
  {
    private readonly WebSocketImplementation _webSocket;
    private readonly Guid _guid;
    private readonly TimeSpan _keepAliveInterval;
    private readonly Task _pingTask;
    private readonly CancellationToken _cancellationToken;
    private Stopwatch _stopwatch;
    private long _pingSentTicks;

    public event EventHandler<PongEventArgs> Pong;

    public PingPongManager(
      Guid guid,
      WebSocket webSocket,
      TimeSpan keepAliveInterval,
      CancellationToken cancellationToken)
    {
      WebSocketImplementation socketImplementation = webSocket as WebSocketImplementation;
      this._webSocket = socketImplementation;
      if (this._webSocket == null)
        throw new InvalidCastException("Cannot cast WebSocket to an instance of WebSocketImplementation. Please use the web socket factories to create a web socket");
      this._guid = guid;
      this._keepAliveInterval = keepAliveInterval;
      this._cancellationToken = cancellationToken;
      socketImplementation.Pong += new EventHandler<PongEventArgs>(this.WebSocketImpl_Pong);
      this._stopwatch = Stopwatch.StartNew();
      if (!(keepAliveInterval != TimeSpan.Zero))
        return;
      Task.Run(new Func<Task>(this.PingForever), cancellationToken);
    }

    public async Task SendPing(ArraySegment<byte> payload, CancellationToken cancellation)
    {
      await this._webSocket.SendPingAsync(payload, cancellation);
    }

    protected virtual void OnPong(PongEventArgs e)
    {
      EventHandler<PongEventArgs> pong = this.Pong;
      if (pong == null)
        return;
      pong((object) this, e);
    }

    private async Task PingForever()
    {
      PingPongManager pingPongManager1 = this;
      Events.Log.PingPongManagerStarted(pingPongManager1._guid, (int) pingPongManager1._keepAliveInterval.TotalSeconds);
      try
      {
        while (!pingPongManager1._cancellationToken.IsCancellationRequested)
        {
          await Task.Delay(pingPongManager1._keepAliveInterval, pingPongManager1._cancellationToken);
          if (pingPongManager1._webSocket.State == WebSocketState.Open)
          {
            TimeSpan timeSpan;
            if (pingPongManager1._pingSentTicks != 0L)
            {
              Events log = Events.Log;
              Guid guid = pingPongManager1._guid;
              timeSpan = pingPongManager1._keepAliveInterval;
              int totalSeconds = (int) timeSpan.TotalSeconds;
              log.KeepAliveIntervalExpired(guid, totalSeconds);
              await pingPongManager1._webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Format("No Pong message received in response to a Ping after KeepAliveInterval {0}", (object) pingPongManager1._keepAliveInterval), pingPongManager1._cancellationToken);
              break;
            }
            if (!pingPongManager1._cancellationToken.IsCancellationRequested)
            {
              PingPongManager pingPongManager2 = pingPongManager1;
              timeSpan = pingPongManager1._stopwatch.Elapsed;
              long ticks = timeSpan.Ticks;
              pingPongManager2._pingSentTicks = ticks;
              ArraySegment<byte> payload = new ArraySegment<byte>(BitConverter.GetBytes(pingPongManager1._pingSentTicks));
              // ISSUE: explicit non-virtual call
              await __nonvirtual (pingPongManager1.SendPing(payload, pingPongManager1._cancellationToken));
            }
          }
          else
            break;
        }
      }
      catch (OperationCanceledException ex)
      {
      }
      Events.Log.PingPongManagerEnded(pingPongManager1._guid);
    }

    private void WebSocketImpl_Pong(object sender, PongEventArgs e)
    {
      this._pingSentTicks = 0L;
      this.OnPong(e);
    }
  }
}
