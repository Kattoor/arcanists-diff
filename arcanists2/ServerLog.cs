

using UnityEngine;

#nullable disable
public static class ServerLog
{
  public static void Log(string s) => Debug.Log((object) s);

  public static void LogWarning(string s) => Debug.LogWarning((object) s);

  public static void LogError(string s) => Debug.LogError((object) s);
}
