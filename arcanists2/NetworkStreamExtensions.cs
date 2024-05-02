// Decompiled with JetBrains decompiler
// Type: NetworkStreamExtensions
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Net.Sockets;

#nullable disable
public static class NetworkStreamExtensions
{
  public static int ReadSafely(this NetworkStream stream, byte[] buffer, int offset, int size)
  {
    try
    {
      return stream.Read(buffer, offset, size);
    }
    catch (IOException ex)
    {
      return 0;
    }
  }

  public static bool ReadExactly(this NetworkStream stream, byte[] buffer, int amount)
  {
    int num;
    for (int offset = 0; offset < amount; offset += num)
    {
      int size = amount - offset;
      num = stream.ReadSafely(buffer, offset, size);
      if (num == 0)
        return false;
    }
    return true;
  }
}
