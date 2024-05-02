﻿// Decompiled with JetBrains decompiler
// Type: Hazel.Udp.BroadcastPacket
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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