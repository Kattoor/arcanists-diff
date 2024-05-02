// Decompiled with JetBrains decompiler
// Type: UI_Manager
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
