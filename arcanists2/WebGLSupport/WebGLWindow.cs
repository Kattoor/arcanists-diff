// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WebGLWindow
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using AOT;
using System;

#nullable disable
namespace WebGLSupport
{
  public static class WebGLWindow
  {
    private static string ViewportContent;

    public static bool Focus { get; private set; }

    public static event Action OnFocusEvent = () => { };

    public static event Action OnBlurEvent = () => { };

    public static event Action OnResizeEvent = () => { };

    private static void Init()
    {
      WebGLWindow.Focus = true;
      WebGLWindowPlugin.WebGLWindowOnFocus(new Action(WebGLWindow.OnWindowFocus));
      WebGLWindowPlugin.WebGLWindowOnBlur(new Action(WebGLWindow.OnWindowBlur));
      WebGLWindowPlugin.WebGLWindowOnResize(new Action(WebGLWindow.OnWindowResize));
      WebGLWindowPlugin.WebGLWindowInjectFullscreen();
    }

    [MonoPInvokeCallback(typeof (Action))]
    private static void OnWindowFocus()
    {
      WebGLWindow.Focus = true;
      WebGLWindow.OnFocusEvent();
    }

    [MonoPInvokeCallback(typeof (Action))]
    private static void OnWindowBlur()
    {
      WebGLWindow.Focus = false;
      WebGLWindow.OnBlurEvent();
    }

    [MonoPInvokeCallback(typeof (Action))]
    private static void OnWindowResize() => WebGLWindow.OnResizeEvent();

    [UnityEngine.RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitializeOnLoadMethod() => WebGLWindow.Init();
  }
}
