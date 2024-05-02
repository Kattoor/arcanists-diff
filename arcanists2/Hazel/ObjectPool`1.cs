// Decompiled with JetBrains decompiler
// Type: Hazel.ObjectPool`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Hazel
{
  internal sealed class ObjectPool<T> where T : IRecyclable
  {
    private Queue<T> pool = new Queue<T>();
    private Func<T> objectFactory;

    internal ObjectPool(Func<T> objectFactory) => this.objectFactory = objectFactory;

    internal T GetObject()
    {
      lock (this.pool)
      {
        if (this.pool.Count > 0)
          return this.pool.Dequeue();
      }
      return this.objectFactory();
    }

    internal void PutObject(T item)
    {
      lock (this.pool)
        this.pool.Enqueue(item);
    }
  }
}
