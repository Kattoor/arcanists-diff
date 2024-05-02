// Decompiled with JetBrains decompiler
// Type: Hazel.DataReceivedEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
