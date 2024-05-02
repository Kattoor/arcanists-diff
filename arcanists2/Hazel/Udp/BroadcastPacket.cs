

using System;
using System.Net;

#nullable disable
namespace Hazel.Udp
{
  public class BroadcastPacket
  {
    public string Data;
    public DateTime ReceiveTime;
    public IPEndPoint Sender;

    public BroadcastPacket(string data, IPEndPoint sender)
    {
      this.Data = data;
      this.Sender = sender;
      this.ReceiveTime = DateTime.Now;
    }

    public string GetAddress() => this.Sender.Address.ToString();
  }
}
