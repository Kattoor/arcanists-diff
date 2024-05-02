// Decompiled with JetBrains decompiler
// Type: Hazel.NetworkEndPoint
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Net;

#nullable disable
namespace Hazel
{
  public sealed class NetworkEndPoint : ConnectionEndPoint
  {
    public EndPoint EndPoint { get; set; }

    public IPMode IPMode { get; set; }

    public NetworkEndPoint(EndPoint endPoint, IPMode mode = IPMode.IPv4)
    {
      this.EndPoint = endPoint;
      this.IPMode = mode;
    }

    public NetworkEndPoint(IPAddress address, int port, IPMode mode = IPMode.IPv4)
      : this((EndPoint) new IPEndPoint(address, port), mode)
    {
    }

    public NetworkEndPoint(string IP, int port, IPMode mode = IPMode.IPv4)
      : this(IPAddress.Parse(IP), port, mode)
    {
    }

    public override string ToString() => this.EndPoint.ToString();
  }
}
