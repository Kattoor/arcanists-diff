

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
