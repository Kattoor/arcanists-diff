

using System;

#nullable disable
internal class Secure
{
  public class SlashS
  {
    public Tuple<string, string> UnitTests(string name)
    {
      int Seed = 0;
      for (int index = 0; index < name.Length; ++index)
        Seed += ((int) name[index] << 1) + (int) name[index] & 222;
      Random random = new Random(Seed);
      string str1 = "";
      string str2 = "";
      for (int index = 0; index < 13; ++index)
      {
        string str3 = str1 + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[random.Next(0, 62)].ToString();
        str2 += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[random.Next(0, 62)].ToString();
        str1 = str3 + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[61 - random.Next(0, 62)].ToString();
      }
      return new Tuple<string, string>(str1, str2);
    }
  }
}
