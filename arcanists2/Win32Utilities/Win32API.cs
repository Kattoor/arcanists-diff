// Decompiled with JetBrains decompiler
// Type: Win32Utilities.Win32API
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Win32Utilities
{
  public static class Win32API
  {
    [DllImport("User32.dll")]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool FlashWindowEx(ref Win32API.FLASHWINFO pwfi);

    public struct FLASHWINFO
    {
      public uint cbSize;
      public IntPtr hwnd;
      public uint dwFlags;
      public uint uCount;
      public uint dwTimeout;
    }

    public static class WindowHandle
    {
      private const string UnityWindowClassName = "UnityWndClass";
      public static readonly IntPtr handle;

      [DllImport("kernel32.dll")]
      private static extern uint GetCurrentThreadId();

      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool EnumThreadWindows(
        uint dwThreadId,
        Win32API.WindowHandle.EnumWindowsProc lpEnumFunc,
        IntPtr lParam);

      static WindowHandle()
      {
        if (Log.d.isDebug())
          Log.d.debug("Finding WIN32 window handle...");
        IntPtr h = Win32API.GetActiveWindow();
        if (h == IntPtr.Zero)
          Win32API.WindowHandle.EnumThreadWindows(Win32API.WindowHandle.GetCurrentThreadId(), (Win32API.WindowHandle.EnumWindowsProc) ((hWnd, lParam) =>
          {
            StringBuilder lpString = new StringBuilder(1000);
            Win32API.WindowHandle.GetClassName(hWnd, lpString, lpString.Capacity);
            string str = lpString.ToString();
            if (Log.d.isDebug())
              Log.d.debug(string.Format("Found WIN32 window {0} at {1}", (object) str, (object) hWnd));
            if (!(str == "UnityWndClass"))
              return true;
            h = hWnd;
            return false;
          }), IntPtr.Zero);
        else if (Log.d.isDebug())
          Log.d.debug("WIN32 window handle found from active window.");
        if (Log.d.isDebug())
          Log.d.debug(string.Format("WIN32 window handle: {0}", (object) h));
        Win32API.WindowHandle.handle = h;
      }

      public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    }
  }
}
