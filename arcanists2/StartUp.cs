// Decompiled with JetBrains decompiler
// Type: StartUp
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class StartUp : MonoBehaviour
{
  private void Start()
  {
    string[] commandLineArgs = Environment.GetCommandLineArgs();
    if (commandLineArgs.Length > 1 && commandLineArgs[1] == "Server")
    {
      Application.targetFrameRate = 60;
      QualitySettings.vSyncCount = 0;
      SceneManager.LoadScene("ServerLobby");
    }
    else
    {
      Application.targetFrameRate = Mathf.Max(61, Mathf.Min(144, Screen.currentResolution.refreshRate));
      SceneManager.LoadScene("mainmenu");
    }
  }

  public static void CheckDirectorySwitch()
  {
  }
}
