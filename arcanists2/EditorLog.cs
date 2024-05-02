// Decompiled with JetBrains decompiler
// Type: EditorLog
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
