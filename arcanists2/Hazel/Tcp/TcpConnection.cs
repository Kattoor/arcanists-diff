

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

#nullable disable
namespace Hazel.Tcp
{
  public sealed class TcpConnection : NetworkConnection
  {
    private Socket socket;
    private object socketLock = new object();

    internal TcpConnection(Socket socket)
    {
      if (socket.ProtocolType != ProtocolType.Tcp)
        throw new ArgumentException("A TcpConnection requires a TCP socket.");
      lock (this.socketLock)
      {
        this.EndPoint = (ConnectionEndPoint) new NetworkEndPoint(socket.RemoteEndPoint);
        this.RemoteEndPoint = socket.RemoteEndPoint;
        this.socket = socket;
        this.socket.NoDelay = true;
        this.State = ConnectionState.Connected;
        this.stopwatch = new Stopwatch();
        this.stopwatch.Start();
      }
    }

    public static IPAddress GetHostAddress(string hostname)
    {
      foreach (IPAddress hostAddress in Dns.GetHostAddresses(hostname))
      {
        if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
          return hostAddress;
      }
      return (IPAddress) null;
    }

    public TcpConnection(NetworkEndPoint remoteEndPoint)
    {
      lock (this.socketLock)
      {
        if (this.State != ConnectionState.NotConnected)
          throw new InvalidOperationException("Cannot connect as the Connection is already connected.");
        this.EndPoint = (ConnectionEndPoint) remoteEndPoint;
        this.RemoteEndPoint = remoteEndPoint.EndPoint;
        this.IPMode = remoteEndPoint.IPMode;
        if (remoteEndPoint.IPMode == IPMode.IPv4)
        {
          this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        else
        {
          if (!Socket.OSSupportsIPv6)
            throw new HazelException("IPV6 not supported!");
          this.socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
          this.socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
        }
        this.socket.NoDelay = true;
      }
    }

    public TcpConnection()
    {
    }

    public static void Log(string logMessage)
    {
      using (StreamWriter streamWriter1 = System.IO.File.AppendText("log.txt"))
      {
        streamWriter1.Write("\r\nLog Entry : ");
        StreamWriter streamWriter2 = streamWriter1;
        DateTime now = DateTime.Now;
        string longTimeString = now.ToLongTimeString();
        now = DateTime.Now;
        string longDateString = now.ToLongDateString();
        streamWriter2.WriteLine("{0} {1}", (object) longTimeString, (object) longDateString);
        streamWriter1.WriteLine("  :");
        streamWriter1.WriteLine("  :{0}", (object) logMessage);
        streamWriter1.WriteLine("-------------------------------");
      }
    }

    public override void Connect(byte[] bytes = null, int timeout = 5000)
    {
      lock (this.socketLock)
      {
        this.State = ConnectionState.Connecting;
        try
        {
          IAsyncResult asyncResult = this.socket.BeginConnect(this.RemoteEndPoint, (AsyncCallback) null, (object) null);
          asyncResult.AsyncWaitHandle.WaitOne(timeout);
          this.socket.EndConnect(asyncResult);
        }
        catch (Exception ex)
        {
          throw new HazelException("Could not connect as an exception occured.", ex);
        }
        try
        {
          this.StartWaitingForHeader(new Action<byte[]>(this.BodyReadCallback));
        }
        catch (Exception ex)
        {
          throw new HazelException("An exception occured while initiating the first receive operation.", ex);
        }
        byte[] numArray;
        if (bytes == null)
        {
          numArray = new byte[1];
        }
        else
        {
          numArray = new byte[bytes.Length + 1];
          Buffer.BlockCopy((Array) bytes, 0, (Array) numArray, 1, bytes.Length);
        }
        this.State = ConnectionState.Connected;
        this.stopwatch = new Stopwatch();
        this.stopwatch.Start();
        this.SendBytesUnencrypted(numArray);
      }
    }

    public override void SendBytesUnencrypted(byte[] bytes)
    {
      byte[] buffer = TcpConnection.AppendLengthHeader(bytes);
      lock (this.socketLock)
      {
        if (this.State != ConnectionState.Connected)
        {
          this.HandleDisconnect();
          return;
        }
        try
        {
          this.socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, (AsyncCallback) null, (object) null);
        }
        catch (Exception ex)
        {
          HazelException e = new HazelException("Could not send data as an occured.", ex);
          this.HandleDisconnect(e);
          throw e;
        }
      }
      this.Statistics.LogSend(bytes.Length, buffer.Length);
    }

    private void ccc(IAsyncResult a) => UnityEngine.Debug.Log((object) a);

    public override void SendBytes(byte[] bytes, SendOption sendOption = SendOption.FragmentedReliable)
    {
      byte[] numArray = (byte[]) null;
      if (this.encryptionType == EncryptionType.S && bytes.Length > 1)
        numArray = this.Encrypt(bytes);
      byte[] buffer = TcpConnection.AppendLengthHeader(numArray ?? bytes);
      lock (this.socketLock)
      {
        if (this.State != ConnectionState.Connected)
        {
          this.HandleDisconnect();
          return;
        }
        try
        {
          this.socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, (AsyncCallback) null, (object) null);
        }
        catch (Exception ex)
        {
          HazelException e = new HazelException("Could not send data as an occured.", ex);
          this.HandleDisconnect(e);
          throw e;
        }
      }
      this.Statistics.LogSend(bytes.Length, buffer.Length);
    }

    public override void SendBytesAndClose(byte[] bytes, SendOption sendOption = SendOption.FragmentedReliable)
    {
      byte[] numArray = (byte[]) null;
      if (this.encryptionType == EncryptionType.S && bytes.Length > 1)
        numArray = this.Encrypt(bytes);
      byte[] buffer = TcpConnection.AppendLengthHeader(numArray ?? bytes);
      lock (this.socketLock)
      {
        if (this.State != ConnectionState.Connected)
        {
          this.HandleDisconnect();
          return;
        }
        try
        {
          this.socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, (AsyncCallback) (result =>
          {
            try
            {
              this.HandleDisconnect();
            }
            catch (Exception ex)
            {
            }
          }), (object) null);
        }
        catch (Exception ex)
        {
          HazelException e = new HazelException("Could not send data as an occured.", ex);
          this.HandleDisconnect(e);
          throw e;
        }
      }
      this.Statistics.LogSend(bytes.Length, buffer.Length);
    }

    private void HeaderReadCallback(byte[] bytes, Action<byte[]> callback)
    {
      int lengthFromBytes = TcpConnection.GetLengthFromBytes(bytes);
      try
      {
        this.StartWaitingForBytes(lengthFromBytes, callback);
      }
      catch (Exception ex)
      {
        this.HandleDisconnect(new HazelException("An exception occured while initiating a body receive operation.", ex));
      }
    }

    private void BodyReadCallback(byte[] bytes)
    {
      this.InvokeDataReceived(bytes, SendOption.FragmentedReliable);
      try
      {
        this.StartWaitingForHeader(new Action<byte[]>(this.BodyReadCallback));
      }
      catch (Exception ex)
      {
        this.HandleDisconnect(new HazelException("An exception occured while initiating a header receive operation.", ex));
      }
      this.Statistics.LogReceive(bytes.Length, bytes.Length + 4);
    }

    internal void StartReceiving()
    {
      try
      {
        this.StartWaitingForHeader(new Action<byte[]>(this.BodyReadCallback));
      }
      catch (Exception ex)
      {
        this.HandleDisconnect(new HazelException("An exception occured while initiating the first receive operation.", ex));
      }
    }

    internal void StartWaitingForHandshake(Action<byte[]> callback)
    {
      try
      {
        this.StartWaitingForHeader((Action<byte[]>) (bytes =>
        {
          byte[] dst = new byte[bytes.Length - 1];
          Buffer.BlockCopy((Array) bytes, 1, (Array) dst, 0, bytes.Length - 1);
          callback(dst);
        }));
      }
      catch (Exception ex)
      {
        this.HandleDisconnect(new HazelException("An exception occured while initiating the first receive operation.", ex));
      }
    }

    private void StartWaitingForHeader(Action<byte[]> callback)
    {
      this.StartWaitingForBytes(4, (Action<byte[]>) (bytes => this.HeaderReadCallback(bytes, callback)));
    }

    private void StartWaitingForBytes(int length, Action<byte[]> callback)
    {
      this.StartWaitingForChunk(new StateObject(length, callback));
    }

    private void StartWaitingForChunk(StateObject state)
    {
      lock (this.socketLock)
      {
        if (this.State != ConnectionState.Connected && this.State != ConnectionState.Connecting)
          return;
        this.socket.BeginReceive(state.buffer, state.totalBytesReceived, state.buffer.Length - state.totalBytesReceived, SocketFlags.None, new AsyncCallback(this.ChunkReadCallback), (object) state);
      }
    }

    private void ChunkReadCallback(IAsyncResult result)
    {
      int num;
      try
      {
        lock (this.socketLock)
          num = this.socket.EndReceive(result);
      }
      catch (ObjectDisposedException ex)
      {
        return;
      }
      catch (Exception ex)
      {
        this.HandleDisconnect(new HazelException("An exception occured while completing a chunk read operation.", ex));
        return;
      }
      StateObject asyncState = (StateObject) result.AsyncState;
      asyncState.totalBytesReceived += num;
      if (num == 0)
        this.HandleDisconnect();
      else if (asyncState.totalBytesReceived < asyncState.buffer.Length)
      {
        try
        {
          this.StartWaitingForChunk(asyncState);
        }
        catch (Exception ex)
        {
          this.HandleDisconnect(new HazelException("An exception occured while initiating a chunk receive operation.", ex));
        }
      }
      else
        asyncState.callback(asyncState.buffer);
    }

    private void HandleDisconnect(HazelException e = null)
    {
      bool flag = false;
      lock (this.socketLock)
      {
        if (this.State == ConnectionState.Connected)
        {
          this.State = ConnectionState.Disconnecting;
          flag = true;
        }
      }
      if (!flag)
        return;
      this.InvokeDisconnected((Exception) e);
      this.Dispose();
    }

    private static byte[] AppendLengthHeader(byte[] bytes)
    {
      byte[] dst = new byte[bytes.Length + 4];
      dst[0] = (byte) (bytes.Length >>> 24 & (int) byte.MaxValue);
      dst[1] = (byte) (bytes.Length >>> 16 & (int) byte.MaxValue);
      dst[2] = (byte) (bytes.Length >>> 8 & (int) byte.MaxValue);
      dst[3] = (byte) bytes.Length;
      Buffer.BlockCopy((Array) bytes, 0, (Array) dst, 4, bytes.Length);
      return dst;
    }

    private static int GetLengthFromBytes(byte[] bytes)
    {
      if (bytes.Length < 4)
        throw new IndexOutOfRangeException("Not enough bytes passed to calculate length.");
      return (int) bytes[0] << 24 | (int) bytes[1] << 16 | (int) bytes[2] << 8 | (int) bytes[3];
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        lock (this.socketLock)
        {
          this.State = ConnectionState.NotConnected;
          if (this.socket.Connected)
            this.socket.Shutdown(SocketShutdown.Send);
          this.socket.Close();
        }
      }
      base.Dispose(disposing);
    }
  }
}
