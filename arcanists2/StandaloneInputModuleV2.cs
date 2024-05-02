// Decompiled with JetBrains decompiler
// Type: StandaloneInputModuleV2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class StandaloneInputModuleV2 : StandaloneInputModule
{
  private static StandaloneInputModuleV2 currentInput;

  public GameObject GameObjectUnderPointer(int pointerId)
  {
    return this.GetLastPointerEventData(pointerId)?.pointerCurrentRaycast.gameObject;
  }

  public GameObject GameObjectUnderPointer() => this.GameObjectUnderPointer(-1);

  public static StandaloneInputModuleV2 CurrentInput
  {
    get
    {
      if ((Object) StandaloneInputModuleV2.currentInput == (Object) null)
      {
        StandaloneInputModuleV2.currentInput = EventSystem.current.currentInputModule as StandaloneInputModuleV2;
        if ((Object) StandaloneInputModuleV2.currentInput == (Object) null)
          Debug.LogError((object) "Missing StandaloneInputModuleV2.");
      }
      return StandaloneInputModuleV2.currentInput;
    }
  }
}
