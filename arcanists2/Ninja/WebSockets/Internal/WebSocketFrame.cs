// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Internal.WebSocketFrame
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Net.WebSockets;

#nullable disable
namespace Ninja.WebSockets.Internal
{
  internal class WebSocketFrame
  {
    public bool IsFinBitSet { get; private set; }

    public WebSocketOpCode OpCode { get; private set; }

    public int Count { get; private set; }

    public WebSocketCloseStatus? CloseStatus { get; private set; }

    public string CloseStatusDescription { get; private set; }

    public WebSocketFrame(bool isFinBitSet, WebSocketOpCode webSocketOpCode, int count)
    {
      this.IsFinBitSet = isFinBitSet;
      this.OpCode = webSocketOpCode;
      this.Count = count;
    }

    public WebSocketFrame(
      bool isFinBitSet,
      WebSocketOpCode webSocketOpCode,
      int count,
      WebSocketCloseStatus closeStatus,
      string closeStatusDescription)
      : this(isFinBitSet, webSocketOpCode, count)
    {
      this.CloseStatus = new WebSocketCloseStatus?(closeStatus);
      this.CloseStatusDescription = closeStatusDescription;
    }
  }
}
