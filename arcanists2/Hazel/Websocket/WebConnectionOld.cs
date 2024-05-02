// Decompiled with JetBrains decompiler
// Type: Hazel.Websocket.WebConnectionOld
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Net.WebSockets;

#nullable disable
namespace Hazel.Websocket
{
  public class WebConnectionOld
  {
    public WebSocket socket;
    public player_info player = new player_info();
    public bool Connected;
    public string EndPoint = "";
    public int id;
    public int messagesReceived;
    public int checkMessagesReceived = -1;

    public event Action<WebConnectionOld, byte[]> ReceivedData;

    public WebConnectionOld(WebSocket ws, string e, int id)
    {
      this.socket = ws;
      this.EndPoint = e.Split(':')[0];
      this.id = id;
      this.Connected = true;
    }

    public void DataReceived(byte[] b)
    {
      Action<WebConnectionOld, byte[]> receivedData = this.ReceivedData;
      if (receivedData == null)
        return;
      receivedData(this, b);
    }

    public WebConnectionOld()
    {
    }

    public string name
    {
      get => this.player.account.name;
      set => this.player.account.name = value;
    }

    public Account account
    {
      get => this.player.account;
      set => this.player.account = value;
    }
  }
}
