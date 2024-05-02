

using System.Diagnostics;

#nullable disable
public static class EditorLog
{
  [Conditional("UNITY_EDITOR")]
  public static void Log(string s)
  {
  }

  [Conditional("UNITY_EDITOR")]
  public static void LogWarning(string s)
  {
  }

  [Conditional("UNITY_EDITOR")]
  public static void LogError(string s)
  {
  }
}
