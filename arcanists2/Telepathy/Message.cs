// Decompiled with JetBrains decompiler
// Type: Telepathy.Message
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
