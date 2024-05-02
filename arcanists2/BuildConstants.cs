// Decompiled with JetBrains decompiler
// Type: BuildConstants
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public static class BuildConstants
{
  public static readonly DateTime buildDate = new DateTime(638384671596677109L);
  public const string version = "1.0.0.3";
  public const BuildConstants.ReleaseType releaseType = BuildConstants.ReleaseType.NewReleaseType;
  public const BuildConstants.Platform platform = BuildConstants.Platform.PC;
  public const BuildConstants.Architecture architecture = BuildConstants.Architecture.Windows_x64;
  public const BuildConstants.Distribution distribution = BuildConstants.Distribution.None;

  public enum ReleaseType
  {
    None,
    NewReleaseType,
  }

  public enum Platform
  {
    None,
    Android,
    Linux,
    macOS,
    WebGL,
    PC,
  }

  public enum Architecture
  {
    None,
    Android,
    Linux_x64,
    macOS,
    WebGL,
    Windows_x64,
  }

  public enum Distribution
  {
    None,
  }
}
