

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
