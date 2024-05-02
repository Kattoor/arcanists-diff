// Decompiled with JetBrains decompiler
// Type: Hazel.NewConnectionEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
