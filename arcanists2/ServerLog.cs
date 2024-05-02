// Decompiled with JetBrains decompiler
// Type: ServerLog
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public static class ServerLog
{
  public static void Log(string s) => Debug.Log((object) s);

  public static void LogWarning(string s) => Debug.LogWarning((object) s);

  public static void LogError(string s) => Debug.LogError((object) s);
}
