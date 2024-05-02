// Decompiled with JetBrains decompiler
// Type: Hazel.ConnectionListener
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
