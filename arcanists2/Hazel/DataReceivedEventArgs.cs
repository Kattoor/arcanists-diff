// Decompiled with JetBrains decompiler
// Type: Hazel.DataReceivedEventArgs
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
