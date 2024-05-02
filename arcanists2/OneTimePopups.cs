

using UnityEngine;

#nullable disable
public static class OneTimePopups
{
  public static string prefName = "prefpopupbools";

  public static bool IsSet(OneTimePopups.Tip x)
  {
    return ((OneTimePopups.Tip) PlayerPrefs.GetInt(OneTimePopups.prefName, 0) & x) != 0;
  }

  public static bool Set(OneTimePopups.Tip x)
  {
    int num1 = PlayerPrefs.GetInt(OneTimePopups.prefName, 0);
    if (((OneTimePopups.Tip) num1 & x) != (OneTimePopups.Tip) 0)
      return false;
    int num2 = (int) ((OneTimePopups.Tip) num1 | x);
    PlayerPrefs.SetInt(OneTimePopups.prefName, num2);
    return true;
  }

  public enum Tip
  {
    ClickPreviewExportPNG = 1,
  }
}
