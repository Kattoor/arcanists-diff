// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WebGLInputPlugin
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace WebGLSupport
{
  internal class WebGLInputPlugin
  {
    public static void WebGLInputInit()
    {
    }

    public static int WebGLInputCreate(
      string canvasId,
      int x,
      int y,
      int width,
      int height,
      int fontsize,
      string text,
      string placeholder,
      bool isMultiLine,
      bool isPassword,
      bool isHidden,
      bool isMobile)
    {
      return 0;
    }

    public static void WebGLInputEnterSubmit(int id, bool flag)
    {
    }

    public static void WebGLInputTab(int id, Action<int, int> cb)
    {
    }

    public static void WebGLInputFocus(int id)
    {
    }

    public static void WebGLInputOnFocus(int id, Action<int> cb)
    {
    }

    public static void WebGLInputOnBlur(int id, Action<int> cb)
    {
    }

    public static void WebGLInputOnValueChange(int id, Action<int, string> cb)
    {
    }

    public static void WebGLInputOnEditEnd(int id, Action<int, string> cb)
    {
    }

    public static int WebGLInputSelectionStart(int id) => 0;

    public static int WebGLInputSelectionEnd(int id) => 0;

    public static int WebGLInputSelectionDirection(int id) => 0;

    public static void WebGLInputSetSelectionRange(int id, int start, int end)
    {
    }

    public static void WebGLInputMaxLength(int id, int maxlength)
    {
    }

    public static void WebGLInputText(int id, string text)
    {
    }

    public static bool WebGLInputIsFocus(int id) => false;

    public static void WebGLInputDelete(int id)
    {
    }

    public static void WebGLInputForceBlur(int id)
    {
    }
  }
}
