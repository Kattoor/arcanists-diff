

using System.Threading;

#nullable disable
namespace Hazel
{
  public class ConnectionStatistics
  {
    public long CompareMessagesReceived = -1;
    private long messagesSent;
    private long dataBytesSent;
    private long totalBytesSent;
    private long counter = 300;
    public bool checkRate;
    private long messagesReceived;
    private long dataBytesReceived;
    private long totalBytesReceived;

    public long MessagesSent => Interlocked.Read(ref this.messagesSent);

    public long DataBytesSent => Interlocked.Read(ref this.dataBytesSent);

    public long TotalBytesSent => Interlocked.Read(ref this.totalBytesSent);

    public long MessagesReceived => Interlocked.Read(ref this.messagesReceived);

    public long DataBytesReceived => Interlocked.Read(ref this.dataBytesReceived);

    public long TotalBytesReceived => Interlocked.Read(ref this.totalBytesReceived);

    internal void LogSend(int dataLength, int totalLength)
    {
      Interlocked.Increment(ref this.messagesSent);
      Interlocked.Add(ref this.dataBytesSent, (long) dataLength);
      Interlocked.Add(ref this.totalBytesSent, (long) totalLength);
    }

    internal void LogReceive(int dataLength, int totalLength)
    {
      Interlocked.Increment(ref this.messagesReceived);
      Interlocked.Add(ref this.dataBytesReceived, (long) dataLength);
      Interlocked.Add(ref this.totalBytesReceived, (long) totalLength);
    }
  }
}
