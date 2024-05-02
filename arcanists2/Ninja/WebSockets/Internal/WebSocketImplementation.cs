// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.WebSocketImplementation
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal class WebSocketImplementation : WebSocket
  {
    private readonly Guid _guid;
    private readonly Func<MemoryStream> _recycledStreamFactory;
    private readonly Stream _stream;
    private readonly bool _includeExceptionInCloseResponse;
    private readonly bool _isClient;
    private readonly string _subProtocol;
    private CancellationTokenSource _internalReadCts;
    private WebSocketState _state;
    private bool _isContinuationFrame;
    private WebSocketMessageType _continuationFrameMessageType = WebSocketMessageType.Binary;
    private readonly bool _usePerMessageDeflate;
    private bool _tryGetBufferFailureLogged;
    private const int MAX_PING_PONG_PAYLOAD_LEN = 125;
    private WebSocketCloseStatus? _closeStatus;
    private string _closeStatusDescription;

    public event EventHandler<PongEventArgs> Pong;

    internal WebSocketImplementation(
      Guid guid,
      Func<MemoryStream> recycledStreamFactory,
      Stream stream,
      TimeSpan keepAliveInterval,
      string secWebSocketExtensions,
      bool includeExceptionInCloseResponse,
      bool isClient,
      string subProtocol)
    {
      this._guid = guid;
      this._recycledStreamFactory = recycledStreamFactory;
      this._stream = stream;
      this._isClient = isClient;
      this._subProtocol = subProtocol;
      this._internalReadCts = new CancellationTokenSource();
      this._state = WebSocketState.Open;
      if (secWebSocketExtensions != null && secWebSocketExtensions.IndexOf("permessage-deflate") >= 0)
      {
        this._usePerMessageDeflate = true;
        Events.Log.UsePerMessageDeflate(guid);
      }
      else
        Events.Log.NoMessageCompression(guid);
      this.KeepAliveInterval = keepAliveInterval;
      this._includeExceptionInCloseResponse = includeExceptionInCloseResponse;
      if (keepAliveInterval.Ticks < 0L)
        throw new InvalidOperationException("KeepAliveInterval must be Zero or positive");
      if (keepAliveInterval == TimeSpan.Zero)
      {
        Events.Log.KeepAliveIntervalZero(guid);
      }
      else
      {
        PingPongManager pingPongManager = new PingPongManager(guid, (WebSocket) this, keepAliveInterval, this._internalReadCts.Token);
      }
    }

    public override WebSocketCloseStatus? CloseStatus => this._closeStatus;

    public override string CloseStatusDescription => this._closeStatusDescription;

    public override WebSocketState State => this._state;

    public override string SubProtocol => this._subProtocol;

    public TimeSpan KeepAliveInterval { get; private set; }

    public override async Task<WebSocketReceiveResult> ReceiveAsync(
      ArraySegment<byte> buffer,
      CancellationToken cancellationToken)
    {
      WebSocketImplementation sender = this;
      object obj;
      int num;
      try
      {
        while (true)
        {
          using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(sender._internalReadCts.Token, cancellationToken))
          {
            WebSocketFrame frame = (WebSocketFrame) null;
            try
            {
              frame = await WebSocketFrameReader.ReadAsync(sender._stream, buffer, linkedCts.Token);
              Events.Log.ReceivedFrame(sender._guid, frame.OpCode, frame.IsFinBitSet, frame.Count);
            }
            catch (InternalBufferOverflowException ex1)
            {
              await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.MessageTooBig, "Frame too large to fit in buffer. Use message fragmentation", (Exception) ex1);
              throw;
            }
            catch (ArgumentOutOfRangeException ex2)
            {
              await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.ProtocolError, "Payload length out of range", (Exception) ex2);
              throw;
            }
            catch (EndOfStreamException ex3)
            {
              await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.InvalidPayloadData, "Unexpected end of stream encountered", (Exception) ex3);
              throw;
            }
            catch (OperationCanceledException ex4)
            {
              await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.EndpointUnavailable, "Operation cancelled", (Exception) ex4);
              throw;
            }
            catch (Exception ex5)
            {
              await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.InternalServerError, "Error reading WebSocket frame", ex5);
              throw;
            }
            Exception ex;
            switch (frame.OpCode)
            {
              case WebSocketOpCode.ContinuationFrame:
                return new WebSocketReceiveResult(frame.Count, sender._continuationFrameMessageType, frame.IsFinBitSet);
              case WebSocketOpCode.TextFrame:
                if (!frame.IsFinBitSet)
                  sender._continuationFrameMessageType = WebSocketMessageType.Text;
                return new WebSocketReceiveResult(frame.Count, WebSocketMessageType.Text, frame.IsFinBitSet);
              case WebSocketOpCode.BinaryFrame:
                if (!frame.IsFinBitSet)
                  sender._continuationFrameMessageType = WebSocketMessageType.Binary;
                return new WebSocketReceiveResult(frame.Count, WebSocketMessageType.Binary, frame.IsFinBitSet);
              case WebSocketOpCode.ConnectionClose:
                return await sender.RespondToCloseFrame(frame, buffer, linkedCts.Token);
              case WebSocketOpCode.Ping:
                ArraySegment<byte> payload1 = new ArraySegment<byte>(buffer.Array, buffer.Offset, frame.Count);
                await sender.SendPongAsync(payload1, linkedCts.Token);
                break;
              case WebSocketOpCode.Pong:
                ArraySegment<byte> payload2 = new ArraySegment<byte>(buffer.Array, frame.Count, buffer.Offset);
                EventHandler<PongEventArgs> pong = sender.Pong;
                if (pong != null)
                {
                  pong((object) sender, new PongEventArgs(payload2));
                  break;
                }
                break;
              default:
                ex = (Exception) new NotSupportedException(string.Format("Unknown WebSocket opcode {0}", (object) frame.OpCode));
                await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.ProtocolError, ex.Message, ex);
                throw ex;
            }
            ex = (Exception) null;
            frame = (WebSocketFrame) null;
          }
        }
      }
      catch (Exception ex)
      {
        obj = (object) ex;
        num = 1;
      }
      if (num == 1)
      {
        Exception ex = (Exception) obj;
        if (sender._state == WebSocketState.Open)
          await sender.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.InternalServerError, "Unexpected error reading from WebSocket", ex);
        if (!(obj is Exception source))
          throw obj;
        ExceptionDispatchInfo.Capture(source).Throw();
      }
      obj = (object) null;
      WebSocketReceiveResult async;
      return async;
    }

    public override async Task SendAsync(
      ArraySegment<byte> buffer,
      WebSocketMessageType messageType,
      bool endOfMessage,
      CancellationToken cancellationToken)
    {
      using (MemoryStream stream = this._recycledStreamFactory())
      {
        WebSocketOpCode oppCode = this.GetOppCode(messageType);
        if (this._usePerMessageDeflate)
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Compress);
            deflateStream.Write(buffer.Array, buffer.Offset, buffer.Count);
            deflateStream.Flush();
            ArraySegment<byte> fromPayload = new ArraySegment<byte>(memoryStream.ToArray());
            WebSocketFrameWriter.Write(oppCode, fromPayload, stream, endOfMessage, this._isClient);
            Events.Log.SendingFrame(this._guid, oppCode, endOfMessage, fromPayload.Count, true);
          }
        }
        else
        {
          WebSocketFrameWriter.Write(oppCode, buffer, stream, endOfMessage, this._isClient);
          Events.Log.SendingFrame(this._guid, oppCode, endOfMessage, buffer.Count, false);
        }
        await this.WriteStreamToNetwork(stream, cancellationToken);
        this._isContinuationFrame = !endOfMessage;
      }
    }

    public async Task SendPingAsync(ArraySegment<byte> payload, CancellationToken cancellationToken)
    {
      if (payload.Count > 125)
        throw new InvalidOperationException(string.Format("Cannot send Ping: Max ping message size {0} exceeded: {1}", (object) 125, (object) payload.Count));
      if (this._state != WebSocketState.Open)
        return;
      using (MemoryStream stream = this._recycledStreamFactory())
      {
        WebSocketFrameWriter.Write(WebSocketOpCode.Ping, payload, stream, true, this._isClient);
        Events.Log.SendingFrame(this._guid, WebSocketOpCode.Ping, true, payload.Count, false);
        await this.WriteStreamToNetwork(stream, cancellationToken);
      }
    }

    public override void Abort()
    {
      this._state = WebSocketState.Aborted;
      this._internalReadCts.Cancel();
    }

    public override async Task CloseAsync(
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      CancellationToken cancellationToken)
    {
      if (this._state == WebSocketState.Open)
      {
        using (MemoryStream stream = this._recycledStreamFactory())
        {
          ArraySegment<byte> fromPayload = this.BuildClosePayload(closeStatus, statusDescription);
          WebSocketFrameWriter.Write(WebSocketOpCode.ConnectionClose, fromPayload, stream, true, this._isClient);
          Events.Log.CloseHandshakeStarted(this._guid, new WebSocketCloseStatus?(closeStatus), statusDescription);
          Events.Log.SendingFrame(this._guid, WebSocketOpCode.ConnectionClose, true, fromPayload.Count, true);
          await this.WriteStreamToNetwork(stream, cancellationToken);
          this._state = WebSocketState.CloseSent;
        }
      }
      else
        Events.Log.InvalidStateBeforeClose(this._guid, this._state);
    }

    public override async Task CloseOutputAsync(
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      CancellationToken cancellationToken)
    {
      if (this._state == WebSocketState.Open)
      {
        this._state = WebSocketState.Closed;
        using (MemoryStream stream = this._recycledStreamFactory())
        {
          ArraySegment<byte> fromPayload = this.BuildClosePayload(closeStatus, statusDescription);
          WebSocketFrameWriter.Write(WebSocketOpCode.ConnectionClose, fromPayload, stream, true, this._isClient);
          Events.Log.CloseOutputNoHandshake(this._guid, new WebSocketCloseStatus?(closeStatus), statusDescription);
          Events.Log.SendingFrame(this._guid, WebSocketOpCode.ConnectionClose, true, fromPayload.Count, true);
          await this.WriteStreamToNetwork(stream, cancellationToken);
        }
      }
      else
        Events.Log.InvalidStateBeforeCloseOutput(this._guid, this._state);
      this._internalReadCts.Cancel();
    }

    public override void Dispose()
    {
      Events.Log.WebSocketDispose(this._guid, this._state);
      try
      {
        if (this._state == WebSocketState.Open)
        {
          CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5.0));
          try
          {
            this.CloseOutputAsync(WebSocketCloseStatus.EndpointUnavailable, "Service is Disposed", cancellationTokenSource.Token).Wait();
          }
          catch (OperationCanceledException ex)
          {
            Events.Log.WebSocketDisposeCloseTimeout(this._guid, this._state);
          }
        }
        this._internalReadCts.Cancel();
        this._stream.Close();
      }
      catch (Exception ex)
      {
        Events.Log.WebSocketDisposeError(this._guid, this._state, ex.ToString());
      }
    }

    protected virtual void OnPong(PongEventArgs e)
    {
      EventHandler<PongEventArgs> pong = this.Pong;
      if (pong == null)
        return;
      pong((object) this, e);
    }

    private ArraySegment<byte> BuildClosePayload(
      WebSocketCloseStatus closeStatus,
      string statusDescription)
    {
      byte[] bytes1 = BitConverter.GetBytes((ushort) closeStatus);
      Array.Reverse((Array) bytes1);
      if (statusDescription == null)
        return new ArraySegment<byte>(bytes1);
      byte[] bytes2 = Encoding.UTF8.GetBytes(statusDescription);
      byte[] numArray = new byte[bytes1.Length + bytes2.Length];
      System.Buffer.BlockCopy((Array) bytes1, 0, (Array) numArray, 0, bytes1.Length);
      System.Buffer.BlockCopy((Array) bytes2, 0, (Array) numArray, bytes1.Length, bytes2.Length);
      return new ArraySegment<byte>(numArray);
    }

    private async Task SendPongAsync(
      ArraySegment<byte> payload,
      CancellationToken cancellationToken)
    {
      if (payload.Count > 125)
      {
        Exception ex = (Exception) new InvalidOperationException(string.Format("Max ping message size {0} exceeded: {1}", (object) 125, (object) payload.Count));
        await this.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.ProtocolError, ex.Message, ex);
        throw ex;
      }
      try
      {
        if (this._state == WebSocketState.Open)
        {
          using (MemoryStream stream = this._recycledStreamFactory())
          {
            WebSocketFrameWriter.Write(WebSocketOpCode.Pong, payload, stream, true, this._isClient);
            Events.Log.SendingFrame(this._guid, WebSocketOpCode.Pong, true, payload.Count, false);
            await this.WriteStreamToNetwork(stream, cancellationToken);
          }
        }
      }
      catch (Exception ex)
      {
        await this.CloseOutputAutoTimeoutAsync(WebSocketCloseStatus.EndpointUnavailable, "Unable to send Pong response", ex);
        throw;
      }
    }

    private async Task<WebSocketReceiveResult> RespondToCloseFrame(
      WebSocketFrame frame,
      ArraySegment<byte> buffer,
      CancellationToken token)
    {
      this._closeStatus = frame.CloseStatus;
      this._closeStatusDescription = frame.CloseStatusDescription;
      if (this._state == WebSocketState.CloseSent)
      {
        this._state = WebSocketState.Closed;
        Events.Log.CloseHandshakeComplete(this._guid);
      }
      else if (this._state == WebSocketState.Open)
      {
        ArraySegment<byte> fromPayload = new ArraySegment<byte>(new byte[0], 0, 0);
        this._state = WebSocketState.CloseReceived;
        Events.Log.CloseHandshakeRespond(this._guid, frame.CloseStatus, frame.CloseStatusDescription);
        using (MemoryStream stream = this._recycledStreamFactory())
        {
          WebSocketFrameWriter.Write(WebSocketOpCode.ConnectionClose, fromPayload, stream, true, this._isClient);
          Events.Log.SendingFrame(this._guid, WebSocketOpCode.ConnectionClose, true, fromPayload.Count, false);
          await this.WriteStreamToNetwork(stream, token);
        }
      }
      else
        Events.Log.CloseFrameReceivedInUnexpectedState(this._guid, this._state, frame.CloseStatus, frame.CloseStatusDescription);
      return new WebSocketReceiveResult(frame.Count, WebSocketMessageType.Close, frame.IsFinBitSet, frame.CloseStatus, frame.CloseStatusDescription);
    }

    private ArraySegment<byte> GetBuffer(MemoryStream stream)
    {
      ArraySegment<byte> buffer;
      if (!stream.TryGetBuffer(out buffer))
      {
        if (!this._tryGetBufferFailureLogged)
        {
          Events.Log.TryGetBufferNotSupported(this._guid, stream?.GetType()?.ToString());
          this._tryGetBufferFailureLogged = true;
        }
        byte[] array = stream.ToArray();
        buffer = new ArraySegment<byte>(array, 0, array.Length);
      }
      return new ArraySegment<byte>(buffer.Array, buffer.Offset, (int) stream.Position);
    }

    private async Task WriteStreamToNetwork(
      MemoryStream stream,
      CancellationToken cancellationToken)
    {
      ArraySegment<byte> buffer = this.GetBuffer(stream);
      await this._stream.WriteAsync(buffer.Array, buffer.Offset, buffer.Count, cancellationToken).ConfigureAwait(false);
    }

    private WebSocketOpCode GetOppCode(WebSocketMessageType messageType)
    {
      if (this._isContinuationFrame)
        return WebSocketOpCode.ContinuationFrame;
      switch (messageType)
      {
        case WebSocketMessageType.Text:
          return WebSocketOpCode.TextFrame;
        case WebSocketMessageType.Binary:
          return WebSocketOpCode.BinaryFrame;
        case WebSocketMessageType.Close:
          throw new NotSupportedException("Cannot use Send function to send a close frame. Use Close function.");
        default:
          throw new NotSupportedException(string.Format("MessageType {0} not supported", (object) messageType));
      }
    }

    private async Task CloseOutputAutoTimeoutAsync(
      WebSocketCloseStatus closeStatus,
      string statusDescription,
      Exception ex)
    {
      WebSocketImplementation socketImplementation = this;
      TimeSpan timeSpan = TimeSpan.FromSeconds(5.0);
      Events.Log.CloseOutputAutoTimeout(socketImplementation._guid, closeStatus, statusDescription, ex.ToString());
      try
      {
        if (socketImplementation._includeExceptionInCloseResponse)
          statusDescription = statusDescription + "\r\n\r\n" + ex.ToString();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(timeSpan);
        await socketImplementation.CloseOutputAsync(closeStatus, statusDescription, cancellationTokenSource.Token);
      }
      catch (OperationCanceledException ex1)
      {
        Events.Log.CloseOutputAutoTimeoutCancelled(socketImplementation._guid, (int) timeSpan.TotalSeconds, closeStatus, statusDescription, ex.ToString());
      }
      catch (Exception ex2)
      {
        Events.Log.CloseOutputAutoTimeoutError(socketImplementation._guid, ex2.ToString(), closeStatus, statusDescription, ex.ToString());
      }
    }
  }
}
