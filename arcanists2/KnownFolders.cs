﻿// Decompiled with JetBrains decompiler
// Type: KnownFolders
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Microsoft.Win32;
using System;
using System.IO;

#nullable disable
public static class KnownFolders
{
  public static string GetHomePath()
  {
    return Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX ? Environment.GetEnvironmentVariable("HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
  }

  public static string GetDownloadFolderPath()
  {
    return Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX ? Path.Combine(KnownFolders.GetHomePath(), "Downloads") : Convert.ToString(Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", (object) string.Empty));
  }
}
