

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
