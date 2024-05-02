// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WebGLWindow
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
