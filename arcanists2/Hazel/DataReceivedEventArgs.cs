

using System;

#nullable disable
namespace Hazel
{
  public class DataReceivedEventArgs : EventArgs, IRecyclable
  {
    private static readonly ObjectPool<DataReceivedEventArgs> objectPool = new ObjectPool<DataReceivedEventArgs>((Func<DataReceivedEventArgs>) (() => new DataReceivedEventArgs()));

    internal static DataReceivedEventArgs GetObject()
    {
      return DataReceivedEventArgs.objectPool.GetObject();
    }

    public byte[] Bytes { get; private set; }

    public SendOption SendOption { get; private set; }

    private DataReceivedEventArgs()
    {
    }

    internal void Set(byte[] bytes, SendOption sendOption)
    {
      this.Bytes = bytes;
      this.SendOption = sendOption;
    }

    public void Recycle() => DataReceivedEventArgs.objectPool.PutObject(this);
  }
}
