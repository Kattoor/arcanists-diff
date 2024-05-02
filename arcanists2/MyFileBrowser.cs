// Decompiled with JetBrains decompiler
// Type: MyFileBrowser
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Crosstales.Common.Util;
using Crosstales.FB;
using System;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

#nullable disable
public static class MyFileBrowser
{
  public static void GetFile(
    string name,
    string startDir,
    Action<string, byte[]> onEnd,
    string[] extensions)
  {
    string path = Singleton<FileBrowser>.Instance.OpenSingleFile(name, startDir, "", extensions);
    if (string.IsNullOrWhiteSpace(path) || onEnd == null)
      return;
    onEnd(path, File.ReadAllBytes(path));
  }

  public static void GetFileAsync(
    string name,
    string startDir,
    Action<string, byte[]> onEnd,
    string[] extensions)
  {
    Singleton<FileBrowser>.Instance.OpenSingleFileAsync(name, startDir, "", (Action<string[]>) (f =>
    {
      if (f.Length == 0 || string.IsNullOrWhiteSpace(f[0]))
        return;
      Action<string, byte[]> action = onEnd;
      if (action == null)
        return;
      action(f[0], File.ReadAllBytes(f[0]));
    }), extensions);
  }

  public static IEnumerator LoadFile(string path, Action<string, byte[]> onEnd)
  {
    using (UnityWebRequest imageWeb = new UnityWebRequest(path, "GET"))
    {
      imageWeb.downloadHandler = (DownloadHandler) new DownloadHandlerTexture();
      yield return (object) imageWeb.SendWebRequest();
      Action<string, byte[]> action = onEnd;
      if (action != null)
        action(path, imageWeb.downloadHandler.data);
    }
  }
}
