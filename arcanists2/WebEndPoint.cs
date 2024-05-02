// Decompiled with JetBrains decompiler
// Type: WebEndPoint
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
