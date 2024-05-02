// Decompiled with JetBrains decompiler
// Type: Hazel.Udp.BroadcastPacket
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
