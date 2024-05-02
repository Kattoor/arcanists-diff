// Decompiled with JetBrains decompiler
// Type: hardInput
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using HardShellStudios.InputManager;
using UnityEngine;

#nullable disable
public class hardInput : MonoBehaviour
{
  public static bool GetKey(string keyName) => hardManager.singleton.GetKey(keyName);

  public static bool GetKeyDown(string keyName) => hardManager.singleton.GetKeyDown(keyName);

  public static bool GetKeyDownSecondaryOnly(string keyName)
  {
    return hardManager.singleton.GetKeyDownSecondaryOnly(keyName);
  }

  public static bool GetKeyDownPrimaryOnly(string keyName)
  {
    return hardManager.singleton.GetKeyDownPrimaryOnly(keyName);
  }

  public static bool GetKeyUp(string keyName) => hardManager.singleton.GetKeyUp(keyName);

  public static float GetAxis(string keyName, string keyName2, float gravity)
  {
    return hardManager.singleton.GetAxis(keyName, keyName2, gravity);
  }

  public static float GetAxis(string keyName, float gravity)
  {
    return hardManager.singleton.GetAxis(keyName, gravity);
  }

  public static string GetKeyName(string keyName, bool useSecond)
  {
    return hardManager.singleton.GetKeyName(keyName, useSecond);
  }

  public static string GetKeyName(string keyString, int keyWheel)
  {
    return hardManager.singleton.GetKeyName(keyString, keyWheel);
  }

  public static void HardStartRebind(string keyNameGET, bool wantSecond, GameObject inputFrom)
  {
    hardManager.singleton.HardStartRebind(keyNameGET, wantSecond, inputFrom);
  }

  public static void ForceRebind(string keyName, bool useSecondaryKey, KeyCode keyCode)
  {
    hardManager.singleton.HardStartRebind(keyName, useSecondaryKey, keyCode);
  }

  public static void MouseLock(bool setMouseLock) => hardManager.singleton.MouseLock(setMouseLock);

  public static void MouseVisible(bool setMouseVisible)
  {
    hardManager.singleton.MouseVisble(setMouseVisible);
  }

  public static void SetControllerType(hardInput.controllerType type)
  {
    hardManager.singleton.setControllerType(type);
  }

  public static hardInput.controllerType GetControllerType()
  {
    switch (hardManager.singleton.controllerType)
    {
      case 1:
        return hardInput.controllerType.PS4;
      case 2:
        return hardInput.controllerType.XBOX1;
      case 3:
        return hardInput.controllerType.XBOX360;
      default:
        return hardInput.controllerType.PS3;
    }
  }

  public static int GetControllerTypeIndex() => hardManager.singleton.controllerType;

  public static KeyCode GetKeyCode(string keyName, bool useSecondaryKey)
  {
    return hardManager.singleton.GetKeyCode(keyName, useSecondaryKey);
  }

  public static void ResetAllBindings() => hardManager.singleton.resetSavedKeys();

  public static void ResetBinding(string bindingName)
  {
    hardManager.singleton.resetKey(bindingName);
  }

  public enum controllerType
  {
    PS3,
    PS4,
    XBOX1,
    XBOX360,
  }
}
