// Decompiled with JetBrains decompiler
// Type: Hazel.ConnectionListener
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
