

using System;

#nullable disable
public static class BuildConstants
{
  public static readonly DateTime buildDate = new DateTime(638502103922686763L);
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
