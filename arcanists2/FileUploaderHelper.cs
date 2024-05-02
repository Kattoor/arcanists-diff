// Decompiled with JetBrains decompiler
// Type: FileUploaderHelper
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
public static class FileUploaderHelper
{
  private static Action<string> pathCallback;
  public static HashSet<string> unnotable = new HashSet<string>()
  {
    "clue_scroll"
  };

  static FileUploaderHelper()
  {
    string name = typeof (FileUploaderHelper).Name;
  }

  public static void RequestFile(Action<string> callback, string extensions = ".jpg, .jpeg, .png")
  {
    FileUploaderHelper.pathCallback = callback;
  }

  public static void SetResult(string path)
  {
    FileUploaderHelper.pathCallback(path);
    FileUploaderHelper.Dispose();
  }

  private static void Dispose() => FileUploaderHelper.pathCallback = (Action<string>) null;

  private static string MakeSafeForCode(string str)
  {
    str = Regex.Replace(str, "[^a-zA-Z0-9_ ]", "", RegexOptions.Compiled);
    if (char.IsDigit(str[0]))
      str = "_" + str;
    return str;
  }

  private static string RemoveSpaces(string str)
  {
    str = Regex.Replace(str, "[^a-zA-Z0-9_]", "_", RegexOptions.Compiled);
    if (char.IsDigit(str[0]))
      str = "_" + str;
    return str;
  }

  public static void Parse()
  {
  }

  public class noted
  {
    public bool isNoted;
    public int offset;
  }
}
