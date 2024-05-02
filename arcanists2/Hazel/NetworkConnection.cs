

#nullable disable
namespace Hazel
{
  public abstract class NetworkConnection : Connection
  {
    public System.Net.EndPoint RemoteEndPoint { get; protected set; }

    public IPMode IPMode { get; protected set; }
  }
}
