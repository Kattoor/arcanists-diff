

using System;

#nullable disable
namespace Hazel
{
  public class DisconnectedEventArgs : EventArgs, IRecyclable
  {
    private static readonly ObjectPool<DisconnectedEventArgs> objectPool = new ObjectPool<DisconnectedEventArgs>((Func<DisconnectedEventArgs>) (() => new DisconnectedEventArgs()));

    internal static DisconnectedEventArgs GetObject()
    {
      return DisconnectedEventArgs.objectPool.GetObject();
    }

    public Exception Exception { get; private set; }

    private DisconnectedEventArgs()
    {
    }

    internal void Set(Exception e) => this.Exception = e;

    public void Recycle() => DisconnectedEventArgs.objectPool.PutObject(this);
  }
}
