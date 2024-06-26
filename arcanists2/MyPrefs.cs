﻿

using UnityEngine;

#nullable disable
public static class MyPrefs
{
  public static void SetFloat(string n, float f)
  {
    GoogleHandler.prefs[n] = new PrefType(n, "float", f.ToString());
    GoogleHandler.Save();
    PlayerPrefs.SetFloat(n, f);
  }

  public static void SetInt(string n, int f)
  {
    GoogleHandler.prefs[n] = new PrefType(n, "int", f.ToString());
    GoogleHandler.Save();
    PlayerPrefs.SetInt(n, f);
  }

  public static void SetBool(string n, bool f)
  {
    GoogleHandler.prefs[n] = new PrefType(n, "bool", f.ToString());
    GoogleHandler.Save();
    Global.SetPrefBool(n, f);
  }

  public static void SetString(string n, string f)
  {
    GoogleHandler.prefs[n] = new PrefType(n, "string", f.ToString());
    GoogleHandler.Save();
    PlayerPrefs.SetString(n, f);
  }

  public static float GetFloat(string n, float def = 0.0f) => PlayerPrefs.GetFloat(n, def);

  public static int GetInt(string n, int def = 0) => PlayerPrefs.GetInt(n, def);

  public static string GetString(string n, string def = "") => PlayerPrefs.GetString(n, def);

  public static bool GetBool(string n, bool def = false) => Global.GetPrefBool(n, def);
}
