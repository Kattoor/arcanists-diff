// Decompiled with JetBrains decompiler
// Type: Hazel.NetworkEndPoint
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
