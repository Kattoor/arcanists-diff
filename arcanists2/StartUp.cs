

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
