// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketClientOptions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketClientOptions
  {
    public TimeSpan KeepAliveInterval { get; set; }

    public bool NoDelay { get; set; }

    public Dictionary<string, string> AdditionalHttpHeaders { get; set; }

    public bool IncludeExceptionInCloseResponse { get; set; }

    public string SecWebSocketExtensions { get; set; }

    public string SecWebSocketProtocol { get; set; }

    public WebSocketClientOptions()
    {
      this.KeepAliveInterval = TimeSpan.Zero;
      this.NoDelay = true;
      this.AdditionalHttpHeaders = new Dictionary<string, string>();
      this.IncludeExceptionInCloseResponse = false;
      this.SecWebSocketProtocol = (string) null;
    }
  }
}
