// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Exceptions.SecWebSocketKeyMissingException
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets.Exceptions
{
  [Serializable]
  public class SecWebSocketKeyMissingException : Exception
  {
    public SecWebSocketKeyMissingException()
    {
    }

    public SecWebSocketKeyMissingException(string message)
      : base(message)
    {
    }

    public SecWebSocketKeyMissingException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
