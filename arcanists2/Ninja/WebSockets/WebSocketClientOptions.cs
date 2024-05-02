// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketClientOptions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
