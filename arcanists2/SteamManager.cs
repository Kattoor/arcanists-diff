﻿

using AOT;
using Steamworks;
using System;
using System.Text;
using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
  protected static bool s_EverInitialized;
  protected static SteamManager s_instance;
  protected bool m_bInitialized;
  protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

  protected static SteamManager Instance
  {
    get
    {
      return (UnityEngine.Object) SteamManager.s_instance == (UnityEngine.Object) null ? new GameObject(nameof (SteamManager)).AddComponent<SteamManager>() : SteamManager.s_instance;
    }
  }

  public static bool Initialized => SteamManager.Instance.m_bInitialized;

  [MonoPInvokeCallback(typeof (SteamAPIWarningMessageHook_t))]
  protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
  {
    Debug.LogWarning((object) pchDebugText);
  }

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
  private static void InitOnPlayMode()
  {
    SteamManager.s_EverInitialized = false;
    SteamManager.s_instance = (SteamManager) null;
  }

  protected virtual void Awake()
  {
    if ((UnityEngine.Object) SteamManager.s_instance != (UnityEngine.Object) null)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      SteamManager.s_instance = this;
      if (SteamManager.s_EverInitialized)
        throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      if (!Packsize.Test())
        Debug.LogError((object) "[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", (UnityEngine.Object) this);
      if (!DllCheck.Test())
        Debug.LogError((object) "[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", (UnityEngine.Object) this);
      try
      {
        if (SteamAPI.RestartAppIfNecessary((AppId_t) 2901550U))
        {
          Application.Quit();
          return;
        }
      }
      catch (DllNotFoundException ex)
      {
        Debug.LogError((object) ("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + (object) ex), (UnityEngine.Object) this);
        Application.Quit();
        return;
      }
      this.m_bInitialized = SteamAPI.Init();
      if (!this.m_bInitialized)
        Debug.LogError((object) "[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", (UnityEngine.Object) this);
      else
        SteamManager.s_EverInitialized = true;
    }
  }

  protected virtual void OnEnable()
  {
    if ((UnityEngine.Object) SteamManager.s_instance == (UnityEngine.Object) null)
      SteamManager.s_instance = this;
    if (!this.m_bInitialized || this.m_SteamAPIWarningMessageHook != null)
      return;
    this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
    SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
  }

  protected virtual void OnDestroy()
  {
    if ((UnityEngine.Object) SteamManager.s_instance != (UnityEngine.Object) this)
      return;
    SteamManager.s_instance = (SteamManager) null;
    if (!this.m_bInitialized)
      return;
    SteamAPI.Shutdown();
  }

  protected virtual void Update()
  {
    if (!this.m_bInitialized)
      return;
    SteamAPI.RunCallbacks();
  }
}
