

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class ForceInput : MonoBehaviour
{
  public Selectable input;

  private void Update()
  {
    if (!((Object) EventSystem.current.currentSelectedGameObject != (Object) this.input.gameObject))
      return;
    this.input.Select();
  }
}
