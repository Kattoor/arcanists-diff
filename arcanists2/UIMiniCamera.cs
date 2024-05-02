// Decompiled with JetBrains decompiler
// Type: UIMiniCamera
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class UIMiniCamera : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
{
  public Camera miniCam;
  private Camera _main;

  private Camera main
  {
    get
    {
      if ((Object) this._main == (Object) null)
        this._main = Camera.main;
      return this._main;
    }
  }

  public void OnDrag(PointerEventData eventData)
  {
    Vector3 viewportPoint = this.miniCam.ScreenToViewportPoint((Vector3) eventData.position);
    this.main.transform.position = new Vector3(viewportPoint.x * (float) Client.map.Width, viewportPoint.y * (float) Client.map.Height);
    CameraMovement.Instance.KillMovement();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    Vector3 viewportPoint = this.miniCam.ScreenToViewportPoint((Vector3) eventData.position);
    this.main.transform.position = new Vector3(viewportPoint.x * (float) Client.map.Width, viewportPoint.y * (float) Client.map.Height);
    CameraMovement.Instance.KillMovement();
  }
}
