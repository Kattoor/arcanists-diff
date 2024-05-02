

using System;

#nullable disable
namespace Ninja.WebSockets.Exceptions
{
  [Serializable]
  public class EntityTooLargeException : Exception
  {
    public EntityTooLargeException()
    {
    }

    public EntityTooLargeException(string message)
      : base(message)
    {
    }

    public EntityTooLargeException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
