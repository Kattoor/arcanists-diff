// Decompiled with JetBrains decompiler
// Type: Hazel.DisconnectedEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
