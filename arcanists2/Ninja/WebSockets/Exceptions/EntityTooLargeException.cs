// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Exceptions.EntityTooLargeException
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets.Exceptions
{
  [Serializable]
  public class EntityTooLargeException : Exception
  {
    public EntityTooLargeException()
    {
    }

    public EntityTooLargeException(string message)
      : base(message)
    {
    }

    public EntityTooLargeException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
