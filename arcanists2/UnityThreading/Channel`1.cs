

using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace UnityThreading
{
  public class Channel<T> : IDisposable
  {
    private List<T> buffer = new List<T>();
    private object setSyncRoot = new object();
    private object getSyncRoot = new object();
    private object disposeRoot = new object();
    private ManualResetEvent setEvent = new ManualResetEvent(false);
    private ManualResetEvent getEvent = new ManualResetEvent(true);
    private ManualResetEvent exitEvent = new ManualResetEvent(false);
    private bool disposed;

    public int BufferSize { get; private set; }

    public Channel()
      : this(1)
    {
    }

    public Channel(int bufferSize)
    {
      this.BufferSize = bufferSize >= 1 ? bufferSize : throw new ArgumentOutOfRangeException(nameof (bufferSize), "Must be greater or equal to 1.");
    }

    ~Channel() => this.Dispose();

    public void Resize(int newBufferSize)
    {
      if (newBufferSize < 1)
        throw new ArgumentOutOfRangeException(nameof (newBufferSize), "Must be greater or equal to 1.");
      lock (this.setSyncRoot)
      {
        if (this.disposed)
          return;
        if (WaitHandle.WaitAny(new WaitHandle[2]
        {
          (WaitHandle) this.exitEvent,
          (WaitHandle) this.getEvent
        }) == 0)
          return;
        this.buffer.Clear();
        if (newBufferSize == this.BufferSize)
          return;
        this.BufferSize = newBufferSize;
      }
    }

    public bool Set(T value) => this.Set(value, int.MaxValue);

    public bool Set(T value, int timeoutInMilliseconds)
    {
      lock (this.setSyncRoot)
      {
        if (this.disposed)
          return false;
        switch (WaitHandle.WaitAny(new WaitHandle[2]
        {
          (WaitHandle) this.exitEvent,
          (WaitHandle) this.getEvent
        }, timeoutInMilliseconds))
        {
          case 0:
          case 258:
            return false;
          default:
            this.buffer.Add(value);
            if (this.buffer.Count == this.BufferSize)
            {
              this.setEvent.Set();
              this.getEvent.Reset();
            }
            return true;
        }
      }
    }

    public T Get() => this.Get(int.MaxValue, default (T));

    public T Get(int timeoutInMilliseconds, T defaultValue)
    {
      lock (this.getSyncRoot)
      {
        if (this.disposed)
          return defaultValue;
        switch (WaitHandle.WaitAny(new WaitHandle[2]
        {
          (WaitHandle) this.exitEvent,
          (WaitHandle) this.setEvent
        }, timeoutInMilliseconds))
        {
          case 0:
          case 258:
            return defaultValue;
          default:
            T obj = this.buffer[0];
            this.buffer.RemoveAt(0);
            if (this.buffer.Count == 0)
            {
              this.getEvent.Set();
              this.setEvent.Reset();
            }
            return obj;
        }
      }
    }

    public void Close()
    {
      lock (this.disposeRoot)
      {
        if (this.disposed)
          return;
        this.exitEvent.Set();
      }
    }

    public void Dispose()
    {
      if (this.disposed)
        return;
      lock (this.disposeRoot)
      {
        this.exitEvent.Set();
        lock (this.getSyncRoot)
        {
          lock (this.setSyncRoot)
          {
            this.setEvent.Close();
            this.setEvent = (ManualResetEvent) null;
            this.getEvent.Close();
            this.getEvent = (ManualResetEvent) null;
            this.exitEvent.Close();
            this.exitEvent = (ManualResetEvent) null;
            this.disposed = true;
          }
        }
      }
    }
  }
}
