

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
