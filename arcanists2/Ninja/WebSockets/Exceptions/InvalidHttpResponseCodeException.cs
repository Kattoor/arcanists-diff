// Decompiled with JetBrains decompiler
// Type: Ninja.WebSockets.Exceptions.InvalidHttpResponseCodeException
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Ninja.WebSockets.Exceptions
{
  [Serializable]
  public class InvalidHttpResponseCodeException : Exception
  {
    public string ResponseCode { get; private set; }

    public string ResponseHeader { get; private set; }

    public string ResponseDetails { get; private set; }

    public InvalidHttpResponseCodeException()
    {
    }

    public InvalidHttpResponseCodeException(string message)
      : base(message)
    {
    }

    public InvalidHttpResponseCodeException(
      string responseCode,
      string responseDetails,
      string responseHeader)
      : base(responseCode)
    {
      this.ResponseCode = responseCode;
      this.ResponseDetails = responseDetails;
      this.ResponseHeader = responseHeader;
    }

    public InvalidHttpResponseCodeException(string message, Exception inner)
      : base(message, inner)
    {
    }
  }
}
