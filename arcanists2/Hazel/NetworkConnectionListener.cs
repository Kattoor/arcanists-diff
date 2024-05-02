// Decompiled with JetBrains decompiler
// Type: Hazel.NetworkConnectionListener
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Net;

#nullable disable
namespace Hazel
{
  public abstract class NetworkConnectionListener : ConnectionListener
  {
    public EndPoint EndPoint { get; protected set; }

    public IPMode IPMode { get; protected set; }
  }
}
