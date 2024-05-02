

using System;

#nullable disable
namespace Hazel
{
  public class NewConnectionEventArgs : EventArgs, IRecyclable
  {
    private static readonly ObjectPool<NewConnectionEventArgs> objectPool = new ObjectPool<NewConnectionEventArgs>((Func<NewConnectionEventArgs>) (() => new NewConnectionEventArgs()));

    internal static NewConnectionEventArgs GetObject()
    {
      return NewConnectionEventArgs.objectPool.GetObject();
    }

    public byte[] HandshakeData { get; private set; }

    public Connection Connection { get; private set; }

    private NewConnectionEventArgs()
    {
    }

    internal void Set(byte[] bytes, Connection connection)
    {
      this.HandshakeData = bytes;
      this.Connection = connection;
    }

    public void Recycle() => NewConnectionEventArgs.objectPool.PutObject(this);
  }
}
