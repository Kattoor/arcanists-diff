// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Exceptions.WebSocketVersionNotSupportedException
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets.Exceptions
{
  [Serializable]
  public class WebSocketVersionNotSupportedException : Exception
  {
    public WebSocketVersionNotSupportedException()
    {
    }

    public WebSocketVersionNotSupportedException(string message)
      : base(message)
    {
    }

    public WebSocketVersionNotSupportedException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
