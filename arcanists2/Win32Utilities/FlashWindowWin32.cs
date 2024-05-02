// Decompiled with JetBrains decompiler
// Type: Win32Utilities.FlashWindowWin32
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Win32Utilities
{
  internal class FlashWindowWin32 : IFlashWindow
  {
    public const uint FLASHW_STOP = 0;
    public const uint FLASHW_CAPTION = 1;
    public const uint FLASHW_TRAY = 2;
    public const uint FLASHW_ALL = 3;
    public const uint FLASHW_TIMER = 4;
    public const uint FLASHW_TIMERNOFG = 12;

    private static Win32API.FLASHWINFO Create_FLASHWINFO(
      IntPtr handle,
      uint flags,
      uint count,
      uint timeout)
    {
      Win32API.FLASHWINFO structure = new Win32API.FLASHWINFO()
      {
        hwnd = handle,
        dwFlags = flags,
        uCount = count,
        dwTimeout = timeout
      };
      structure.cbSize = Convert.ToUInt32(Marshal.SizeOf<Win32API.FLASHWINFO>(structure));
      return structure;
    }

    public bool Flash()
    {
      try
      {
        Win32API.FLASHWINFO flashwinfo = FlashWindowWin32.Create_FLASHWINFO(Win32API.WindowHandle.handle, 15U, uint.MaxValue, 0U);
        return Win32API.FlashWindowEx(ref flashwinfo);
      }
      catch (Exception ex)
      {
        Log.d.error(nameof (Flash), ex);
      }
      return false;
    }

    public bool Flash(uint count)
    {
      try
      {
        Win32API.FLASHWINFO flashwinfo = FlashWindowWin32.Create_FLASHWINFO(Win32API.WindowHandle.handle, 3U, count, 0U);
        return Win32API.FlashWindowEx(ref flashwinfo);
      }
      catch (Exception ex)
      {
        Log.d.error(string.Format("{0}({1})", (object) nameof (Flash), (object) count), ex);
      }
      return false;
    }

    public bool Start()
    {
      try
      {
        Win32API.FLASHWINFO flashwinfo = FlashWindowWin32.Create_FLASHWINFO(Win32API.WindowHandle.handle, 3U, uint.MaxValue, 0U);
        return Win32API.FlashWindowEx(ref flashwinfo);
      }
      catch (Exception ex)
      {
        Log.d.error(nameof (Start), ex);
      }
      return false;
    }

    public bool Stop()
    {
      try
      {
        Win32API.FLASHWINFO flashwinfo = FlashWindowWin32.Create_FLASHWINFO(Win32API.WindowHandle.handle, 0U, uint.MaxValue, 0U);
        return Win32API.FlashWindowEx(ref flashwinfo);
      }
      catch (Exception ex)
      {
        Log.d.error(nameof (Stop), ex);
      }
      return false;
    }
  }
}
