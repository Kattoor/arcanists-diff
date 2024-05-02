// Decompiled with JetBrains decompiler
// Type: Hazel.DisconnectedEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
