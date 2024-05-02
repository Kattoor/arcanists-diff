

using System;

#nullable disable
namespace Hazel
{
  public abstract class ConnectionListener : IDisposable
  {
    public event EventHandler<NewConnectionEventArgs> NewConnection;

    public abstract void Start();

    public abstract void StartNoTry();

    protected void InvokeNewConnection(byte[] bytes, Connection connection)
    {
      NewConnectionEventArgs e = NewConnectionEventArgs.GetObject();
      e.Set(bytes, connection);
      EventHandler<NewConnectionEventArgs> newConnection = this.NewConnection;
      if (newConnection == null)
        return;
      newConnection((object) this, e);
    }

    public virtual void Close() => this.Dispose();

    public void Dispose() => this.Dispose(true);

    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
