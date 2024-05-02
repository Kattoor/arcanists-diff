// Decompiled with JetBrains decompiler
// Type: CanvasMain
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Canvas))]
public class CanvasMain : MonoBehaviour
{
  private static Canvas canvas;
  private static Camera _camera;
  private static RectTransform rectTransform;

  public static Camera camera
  {
    get
    {
      if ((Object) CanvasMain._camera == (Object) null)
        CanvasMain._camera = Camera.main;
      return CanvasMain._camera;
    }
  }

  public static Transform GetTransform => (Transform) CanvasMain.rectTransform;

  private void Awake()
  {
    CanvasMain.canvas = this.GetComponent<Canvas>();
    CanvasMain.rectTransform = (RectTransform) this.transform;
  }

  public static int CanvasOrder
  {
    set => CanvasMain.canvas.sortingOrder = value;
  }

  public static Vector2 WorldToCanvasPositionWithCameraOffset(Vector3 position)
  {
    Vector2 viewportPoint = (Vector2) CanvasMain.camera.WorldToViewportPoint(position + CanvasMain.camera.transform.position);
    viewportPoint.x *= CanvasMain.rectTransform.sizeDelta.x;
    viewportPoint.y *= CanvasMain.rectTransform.sizeDelta.y;
    viewportPoint.x -= CanvasMain.rectTransform.sizeDelta.x * CanvasMain.rectTransform.pivot.x;
    viewportPoint.y -= CanvasMain.rectTransform.sizeDelta.y * CanvasMain.rectTransform.pivot.y;
    return viewportPoint;
  }

  public static Vector2 WorldToCanvasPosition(Vector3 position)
  {
    Vector2 viewportPoint = (Vector2) CanvasMain.camera.WorldToViewportPoint(position);
    viewportPoint.x *= CanvasMain.rectTransform.sizeDelta.x;
    viewportPoint.y *= CanvasMain.rectTransform.sizeDelta.y;
    viewportPoint.x -= CanvasMain.rectTransform.sizeDelta.x * CanvasMain.rectTransform.pivot.x;
    viewportPoint.y -= CanvasMain.rectTransform.sizeDelta.y * CanvasMain.rectTransform.pivot.y;
    return viewportPoint;
  }

  public static Vector3 CanvasToViewport(Vector2 position)
  {
    position.x += CanvasMain.rectTransform.sizeDelta.x * CanvasMain.rectTransform.pivot.x;
    position.y += CanvasMain.rectTransform.sizeDelta.y * CanvasMain.rectTransform.pivot.y;
    position.x /= CanvasMain.rectTransform.sizeDelta.x;
    position.y /= CanvasMain.rectTransform.sizeDelta.y;
    return (Vector3) position;
  }

  public static Vector3 CanvasToWorldPosition(Vector2 position)
  {
    position.x += CanvasMain.rectTransform.sizeDelta.x * CanvasMain.rectTransform.pivot.x;
    position.y += CanvasMain.rectTransform.sizeDelta.y * CanvasMain.rectTransform.pivot.y;
    position.x /= CanvasMain.rectTransform.sizeDelta.x;
    position.y /= CanvasMain.rectTransform.sizeDelta.y;
    return CanvasMain.camera.ViewportToWorldPoint((Vector3) position);
  }
}
