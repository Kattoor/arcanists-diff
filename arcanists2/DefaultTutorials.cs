

using System.Collections.Generic;

#nullable disable
public static class DefaultTutorials
{
  public static List<DefaultTutorials.Data> tutorials = new List<DefaultTutorials.Data>();

  public class Data
  {
    public string name = "";
    public byte[] bytes;
  }
}
