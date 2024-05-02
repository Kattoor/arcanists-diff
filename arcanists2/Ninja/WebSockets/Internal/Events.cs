// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.Events
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Net.WebSockets;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  [EventSource(Name = "Ninja-WebSockets", Guid = "7DE1A071-4F85-4DBD-8FB1-EE8D3845E087")]
  internal sealed class Events : EventSource
  {
    public static Events Log = new Events();

    [Event(1, Level = EventLevel.Informational)]
    public void ClientConnectingToIpAddress(Guid guid, string ipAddress, int port)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(1, (object) guid, (object) ipAddress, (object) port);
    }

    [Event(2, Level = EventLevel.Informational)]
    public void ClientConnectingToHost(Guid guid, string host, int port)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(2, (object) guid, (object) host, (object) port);
    }

    [Event(3, Level = EventLevel.Informational)]
    public void AttemtingToSecureSslConnection(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(3, (object) guid);
    }

    [Event(4, Level = EventLevel.Informational)]
    public void ConnectionSecured(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(4, (object) guid);
    }

    [Event(5, Level = EventLevel.Informational)]
    public void ConnectionNotSecure(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(5, (object) guid);
    }

    [Event(6, Level = EventLevel.Error)]
    public void SslCertificateError(SslPolicyErrors sslPolicyErrors)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(6, (object) sslPolicyErrors);
    }

    [Event(7, Level = EventLevel.Informational)]
    public void HandshakeSent(Guid guid, string httpHeader)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(7, (object) guid, (object) (httpHeader ?? string.Empty));
    }

    [Event(8, Level = EventLevel.Informational)]
    public void ReadingHttpResponse(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(8, (object) guid);
    }

    [Event(9, Level = EventLevel.Error)]
    public void ReadHttpResponseError(Guid guid, string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(9, (object) guid, (object) (exception ?? string.Empty));
    }

    [Event(10, Level = EventLevel.Warning)]
    public void InvalidHttpResponseCode(Guid guid, string response)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(10, (object) guid, (object) (response ?? string.Empty));
    }

    [Event(11, Level = EventLevel.Error)]
    public void HandshakeFailure(Guid guid, string message)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(11, (object) guid, (object) (message ?? string.Empty));
    }

    [Event(12, Level = EventLevel.Informational)]
    public void ClientHandshakeSuccess(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(12, (object) guid);
    }

    [Event(13, Level = EventLevel.Informational)]
    public void ServerHandshakeSuccess(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(13, (object) guid);
    }

    [Event(14, Level = EventLevel.Informational)]
    public void AcceptWebSocketStarted(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(14, (object) guid);
    }

    [Event(15, Level = EventLevel.Informational)]
    public void SendingHandshakeResponse(Guid guid, string response)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(15, (object) guid, (object) (response ?? string.Empty));
    }

    [Event(16, Level = EventLevel.Error)]
    public void WebSocketVersionNotSupported(Guid guid, string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(16, (object) guid, (object) (exception ?? string.Empty));
    }

    [Event(17, Level = EventLevel.Error)]
    public void BadRequest(Guid guid, string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(17, (object) guid, (object) (exception ?? string.Empty));
    }

    [Event(18, Level = EventLevel.Informational)]
    public void UsePerMessageDeflate(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(18, (object) guid);
    }

    [Event(19, Level = EventLevel.Informational)]
    public void NoMessageCompression(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(19, (object) guid);
    }

    [Event(20, Level = EventLevel.Informational)]
    public void KeepAliveIntervalZero(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(20, (object) guid);
    }

    [Event(21, Level = EventLevel.Informational)]
    public void PingPongManagerStarted(Guid guid, int keepAliveIntervalSeconds)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(21, (object) guid, (object) keepAliveIntervalSeconds);
    }

    [Event(22, Level = EventLevel.Informational)]
    public void PingPongManagerEnded(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(22, (object) guid);
    }

    [Event(23, Level = EventLevel.Warning)]
    public void KeepAliveIntervalExpired(Guid guid, int keepAliveIntervalSeconds)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(23, (object) guid, (object) keepAliveIntervalSeconds);
    }

    [Event(24, Level = EventLevel.Warning)]
    public void CloseOutputAutoTimeout(
      Guid guid,
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(24, (object) guid, (object) closeStatus, (object) (statusDescription ?? string.Empty), (object) (exception ?? string.Empty));
    }

    [Event(25, Level = EventLevel.Error)]
    public void CloseOutputAutoTimeoutCancelled(
      Guid guid,
      int timeoutSeconds,
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(25, (object) guid, (object) timeoutSeconds, (object) closeStatus, (object) (statusDescription ?? string.Empty), (object) (exception ?? string.Empty));
    }

    [Event(26, Level = EventLevel.Error)]
    public void CloseOutputAutoTimeoutError(
      Guid guid,
      string closeException,
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(26, (object) guid, (object) (closeException ?? string.Empty), (object) closeStatus, (object) (statusDescription ?? string.Empty), (object) (exception ?? string.Empty));
    }

    [Event(27, Level = EventLevel.Warning)]
    public void TryGetBufferNotSupported(Guid guid, string streamType)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(27, (object) guid, (object) (streamType ?? string.Empty));
    }

    [Event(28, Level = EventLevel.Verbose)]
    public void SendingFrame(
      Guid guid,
      WebSocketOpCode webSocketOpCode,
      bool isFinBitSet,
      int numBytes,
      bool isPayloadCompressed)
    {
      if (!this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
        return;
      this.WriteEvent(28, (object) guid, (object) webSocketOpCode, (object) isFinBitSet, (object) numBytes, (object) isPayloadCompressed);
    }

    [Event(29, Level = EventLevel.Verbose)]
    public void ReceivedFrame(
      Guid guid,
      WebSocketOpCode webSocketOpCode,
      bool isFinBitSet,
      int numBytes)
    {
      if (!this.IsEnabled(EventLevel.Verbose, EventKeywords.None))
        return;
      this.WriteEvent(29, (object) guid, (object) webSocketOpCode, (object) isFinBitSet, (object) numBytes);
    }

    [Event(30, Level = EventLevel.Informational)]
    public void CloseOutputNoHandshake(
      Guid guid,
      WebSocketCloseStatus? closeStatus,
      string statusDescription)
    {
      if (!this.IsEnabled())
        return;
      string str = string.Format("{0}", (object) closeStatus);
      this.WriteEvent(30, (object) guid, (object) str, (object) (statusDescription ?? string.Empty));
    }

    [Event(31, Level = EventLevel.Informational)]
    public void CloseHandshakeStarted(
      Guid guid,
      WebSocketCloseStatus? closeStatus,
      string statusDescription)
    {
      if (!this.IsEnabled())
        return;
      string str = string.Format("{0}", (object) closeStatus);
      this.WriteEvent(31, (object) guid, (object) str, (object) (statusDescription ?? string.Empty));
    }

    [Event(32, Level = EventLevel.Informational)]
    public void CloseHandshakeRespond(
      Guid guid,
      WebSocketCloseStatus? closeStatus,
      string statusDescription)
    {
      if (!this.IsEnabled())
        return;
      string str = string.Format("{0}", (object) closeStatus);
      this.WriteEvent(32, (object) guid, (object) str, (object) (statusDescription ?? string.Empty));
    }

    [Event(33, Level = EventLevel.Informational)]
    public void CloseHandshakeComplete(Guid guid)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(33, (object) guid);
    }

    [Event(34, Level = EventLevel.Warning)]
    public void CloseFrameReceivedInUnexpectedState(
      Guid guid,
      WebSocketState webSocketState,
      WebSocketCloseStatus? closeStatus,
      string statusDescription)
    {
      if (!this.IsEnabled())
        return;
      string str = string.Format("{0}", (object) closeStatus);
      this.WriteEvent(34, (object) guid, (object) webSocketState, (object) str, (object) (statusDescription ?? string.Empty));
    }

    [Event(35, Level = EventLevel.Informational)]
    public void WebSocketDispose(Guid guid, WebSocketState webSocketState)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(35, (object) guid, (object) webSocketState);
    }

    [Event(36, Level = EventLevel.Warning)]
    public void WebSocketDisposeCloseTimeout(Guid guid, WebSocketState webSocketState)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(36, (object) guid, (object) webSocketState);
    }

    [Event(37, Level = EventLevel.Error)]
    public void WebSocketDisposeError(Guid guid, WebSocketState webSocketState, string exception)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(37, (object) guid, (object) webSocketState, (object) (exception ?? string.Empty));
    }

    [Event(38, Level = EventLevel.Warning)]
    public void InvalidStateBeforeClose(Guid guid, WebSocketState webSocketState)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(38, (object) guid, (object) webSocketState);
    }

    [Event(39, Level = EventLevel.Warning)]
    public void InvalidStateBeforeCloseOutput(Guid guid, WebSocketState webSocketState)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(39, (object) guid, (object) webSocketState);
    }
  }
}
