

using System;
using System.Runtime.InteropServices;

#nullable disable
public class FileOperationAPIWrapper
{
  [DllImport("shell32.dll", CharSet = CharSet.Auto)]
  private static extern int SHFileOperation(ref FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp);

  public static bool Send(string path, FileOperationAPIWrapper.FileOperationFlags flags)
  {
    try
    {
      FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp = new FileOperationAPIWrapper.SHFILEOPSTRUCT()
      {
        wFunc = FileOperationAPIWrapper.FileOperationType.FO_DELETE,
        pFrom = path + "\0\0",
        fFlags = FileOperationAPIWrapper.FileOperationFlags.FOF_ALLOWUNDO | flags
      };
      FileOperationAPIWrapper.SHFileOperation(ref FileOp);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static bool Send(string path)
  {
    return FileOperationAPIWrapper.Send(path, FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_WANTNUKEWARNING);
  }

  public static bool MoveToRecycleBin(string path)
  {
    return FileOperationAPIWrapper.Send(path, FileOperationAPIWrapper.FileOperationFlags.FOF_SILENT | FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_NOERRORUI);
  }

  private static bool deleteFile(string path, FileOperationAPIWrapper.FileOperationFlags flags)
  {
    try
    {
      FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp = new FileOperationAPIWrapper.SHFILEOPSTRUCT()
      {
        wFunc = FileOperationAPIWrapper.FileOperationType.FO_DELETE,
        pFrom = path + "\0\0",
        fFlags = flags
      };
      FileOperationAPIWrapper.SHFileOperation(ref FileOp);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public static bool DeleteCompletelySilent(string path)
  {
    return FileOperationAPIWrapper.deleteFile(path, FileOperationAPIWrapper.FileOperationFlags.FOF_SILENT | FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_NOERRORUI);
  }

  [Flags]
  public enum FileOperationFlags : ushort
  {
    FOF_SILENT = 4,
    FOF_NOCONFIRMATION = 16, // 0x0010
    FOF_ALLOWUNDO = 64, // 0x0040
    FOF_SIMPLEPROGRESS = 256, // 0x0100
    FOF_NOERRORUI = 1024, // 0x0400
    FOF_WANTNUKEWARNING = 16384, // 0x4000
  }

  public enum FileOperationType : uint
  {
    FO_MOVE = 1,
    FO_COPY = 2,
    FO_DELETE = 3,
    FO_RENAME = 4,
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  private struct SHFILEOPSTRUCT
  {
    public IntPtr hwnd;
    [MarshalAs(UnmanagedType.U4)]
    public FileOperationAPIWrapper.FileOperationType wFunc;
    public string pFrom;
    public string pTo;
    public FileOperationAPIWrapper.FileOperationFlags fFlags;
    [MarshalAs(UnmanagedType.Bool)]
    public bool fAnyOperationsAborted;
    public IntPtr hNameMappings;
    public string lpszProgressTitle;
  }
}
