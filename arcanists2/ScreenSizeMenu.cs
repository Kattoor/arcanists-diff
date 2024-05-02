// Decompiled with JetBrains decompiler
// Type: ScreenSizeMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ScreenSizeMenu : MonoBehaviour
{
  public List<GameObject> androidHide = new List<GameObject>();
  public GameObject androidSupport;

  public static ScreenSizeMenu Instance { get; private set; }

  private void Awake() => ScreenSizeMenu.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) ScreenSizeMenu.Instance == (Object) this))
      return;
    ScreenSizeMenu.Instance = (ScreenSizeMenu) null;
  }

  public void Resizeable()
  {
    Resolution currentResolution1 = Screen.currentResolution;
    int width1 = currentResolution1.width;
    currentResolution1 = Screen.currentResolution;
    int height1 = currentResolution1.height;
    Screen.SetResolution(width1, height1, FullScreenMode.FullScreenWindow);
    Resolution currentResolution2 = Screen.currentResolution;
    int width2 = currentResolution2.width / 2;
    currentResolution2 = Screen.currentResolution;
    int height2 = currentResolution2.height / 2;
    Screen.SetResolution(width2, height2, false);
  }

  public void Fullscreen()
  {
    Resolution currentResolution = Screen.currentResolution;
    int width = currentResolution.width;
    currentResolution = Screen.currentResolution;
    int height = currentResolution.height;
    Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
  }

  public void FullscreenMaximized()
  {
    Resolution currentResolution = Screen.currentResolution;
    int width = currentResolution.width;
    currentResolution = Screen.currentResolution;
    int height = currentResolution.height;
    Screen.SetResolution(width, height, FullScreenMode.MaximizedWindow);
  }

  public void Back() => Object.Destroy((Object) this.gameObject);
}
