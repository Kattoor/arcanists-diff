

using System.Collections.Generic;

#nullable disable
namespace Telepathy
{
  public class SafeQueue<T>
  {
    private Queue<T> queue = new Queue<T>();

    public int Count
    {
      get
      {
        lock (this.queue)
          return this.queue.Count;
      }
    }

    public void Enqueue(T item)
    {
      lock (this.queue)
        this.queue.Enqueue(item);
    }

    public bool TryDequeue(out T result)
    {
      lock (this.queue)
      {
        result = default (T);
        if (this.queue.Count <= 0)
          return false;
        result = this.queue.Dequeue();
        return true;
      }
    }

    public bool TryDequeueAll(out T[] result)
    {
      lock (this.queue)
      {
        result = this.queue.ToArray();
        this.queue.Clear();
        return result.Length != 0;
      }
    }

    public void Clear()
    {
      lock (this.queue)
        this.queue.Clear();
    }
  }
}
