// Decompiled with JetBrains decompiler
// Type: Hazel.ObjectPool`1
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
