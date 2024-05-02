// Decompiled with JetBrains decompiler
// Type: StandaloneInputModuleV2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
