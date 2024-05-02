// Decompiled with JetBrains decompiler
// Type: Telepathy.Message
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Telepathy
{
  public struct Message
  {
    public readonly int connectionId;
    public readonly EventType eventType;
    public readonly byte[] data;

    public Message(int connectionId, EventType eventType, byte[] data)
    {
      this.connectionId = connectionId;
      this.eventType = eventType;
      this.data = data;
    }
  }
}
