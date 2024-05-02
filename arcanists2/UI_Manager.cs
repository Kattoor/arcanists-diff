

using UnityEngine;

#nullable disable
[AddComponentMenu("Hard Shell Studios/Examples/UI Manager")]
public class UI_Manager : MonoBehaviour
{
  public GameObject menu;
  private int num;

  private void Start() => this.num = hardInput.GetControllerTypeIndex();

  private void Update()
  {
    if (hardInput.GetKeyDown("Menu"))
    {
      if (this.menu.activeSelf)
      {
        this.menu.SetActive(false);
        hardInput.MouseLock(true);
        hardInput.MouseVisible(false);
      }
      else
      {
        this.menu.SetActive(true);
        hardInput.MouseLock(false);
        hardInput.MouseVisible(true);
      }
    }
    if (!hardInput.GetKeyDown("CycleModes"))
      return;
    ++this.num;
    if (this.num == 4)
      this.num = 0;
    if (this.num == 0)
      hardInput.SetControllerType(hardInput.controllerType.PS3);
    else if (this.num == 1)
      hardInput.SetControllerType(hardInput.controllerType.PS4);
    else if (this.num == 2)
    {
      hardInput.SetControllerType(hardInput.controllerType.XBOX1);
    }
    else
    {
      if (this.num != 3)
        return;
      hardInput.SetControllerType(hardInput.controllerType.XBOX360);
    }
  }
}
