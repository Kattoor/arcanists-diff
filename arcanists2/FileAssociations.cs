// Decompiled with JetBrains decompiler
// Type: FileAssociations
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
public class FileAssociations
{
  private const int SHCNE_ASSOCCHANGED = 134217728;
  private const int SHCNF_FLUSH = 4096;

  [DllImport("Shell32.dll")]
  private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

  public static void EnsureAssociationsSet()
  {
  }

  public static void EnsureAssociationsSet(params FileAssociation[] associations)
  {
    bool flag = false;
    foreach (FileAssociation association in associations)
      flag |= FileAssociations.SetAssociation(association.Extension, association.ProgId, association.FileTypeDescription, association.ExecutableFilePath);
    if (!flag)
      return;
    FileAssociations.SHChangeNotify(134217728, 4096, IntPtr.Zero, IntPtr.Zero);
  }

  public static bool SetAssociation(
    string extension,
    string progId,
    string fileTypeDescription,
    string applicationFilePath)
  {
    return (0 | (FileAssociations.SetKeyDefaultValue("Software\\Classes\\" + extension, progId) ? 1 : 0) | (FileAssociations.SetKeyDefaultValue("Software\\Classes\\" + progId, fileTypeDescription) ? 1 : 0) | (FileAssociations.SetKeyDefaultValue("Software\\Classes\\" + progId + "\\shell\\open\\command", "\"" + applicationFilePath + "\" \"%1\"") ? 1 : 0)) != 0;
  }

  private static bool SetKeyDefaultValue(string keyPath, string value) => false;
}
