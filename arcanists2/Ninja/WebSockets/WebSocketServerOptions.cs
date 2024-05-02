// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.WebSocketServerOptions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
