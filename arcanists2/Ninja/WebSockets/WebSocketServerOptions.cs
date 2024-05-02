﻿// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketServerOptions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets
{
  public class WebSocketServerOptions
  {
    public TimeSpan KeepAliveInterval { get; set; }

    public bool IncludeExceptionInCloseResponse { get; set; }

    public string SubProtocol { get; set; }

    public WebSocketServerOptions()
    {
      this.KeepAliveInterval = TimeSpan.FromSeconds(60.0);
      this.IncludeExceptionInCloseResponse = false;
      this.SubProtocol = (string) null;
    }
  }
}
