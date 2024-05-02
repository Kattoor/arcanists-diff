// Decompiled with JetBrains decompiler
// Type: WebEndPoint
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;

#nullable disable
public class WebEndPoint : ConnectionEndPoint
{
  private string e = "";

  public WebEndPoint(string e)
  {
    string[] strArray = e.Split(':');
    if (strArray.Length == 5 && strArray[3].Length > 1)
      this.e = strArray[3].Substring(0, strArray[3].Length - 1) + ":" + strArray[4];
    else
      this.e = e;
  }

  public override string ToString() => this.e;
}
