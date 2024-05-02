

using Newtonsoft.Json.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

#nullable disable
public class MapCoordPicker : 
  MonoBehaviour,
  IPointerDownHandler,
  IEventSystemHandler,
  IPointerUpHandler
{
  public TMP_Text text;
  public RectTransform container;
  public RectTransform center;
  public UILineRenderer edge;
  public bool down;
  public Vector2 point_clicked = Vector2.zero;
  public int radius;
  public JProperty itemPoint;

  public static MapCoordPicker Instance { get; private set; }

  private void Awake() => MapCoordPicker.Instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MapCoordPicker.Instance == (UnityEngine.Object) this))
      return;
    MapCoordPicker.Instance = (MapCoordPicker) null;
  }

  public void FromRadius(int radius)
  {
    this.radius = radius;
    this.UpdateVisuals();
  }

  public void FromX(int x)
  {
    this.point_clicked = this.UnAdjust(this.Adjust(this.point_clicked) with
    {
      x = (float) x
    });
    this.UpdateVisuals();
  }

  public void FromY(int y)
  {
    this.point_clicked = this.UnAdjust(this.Adjust(this.point_clicked) with
    {
      y = (float) y
    });
    this.UpdateVisuals();
  }

  public void FromPoint(Vector2 z, int radius)
  {
    this.point_clicked = this.UnAdjust(z);
    this.radius = radius;
    this.UpdateVisuals();
  }

  public void ToPoint()
  {
    Vector2 vector2 = this.Adjust(this.point_clicked);
    if (!((UnityEngine.Object) TutorialEditorMenu.Instance != (UnityEngine.Object) null) || !TutorialEditorMenu.Instance.FindInputByName("x", ((int) vector2.x).ToString()))
      return;
    TutorialEditorMenu.Instance.FindInputByName("y", ((int) vector2.y).ToString());
  }

  public void UpdateVisuals()
  {
    int radius = this.radius;
    this.center.anchoredPosition = this.point_clicked;
    this.center.gameObject.SetActive(this.point_clicked != Vector2.zero);
    this.edge.rectTransform.anchoredPosition = this.point_clicked;
    if (radius <= 0)
    {
      this.edge.enabled = false;
    }
    else
    {
      float num1 = (float) (360.0 / (double) (this.edge.Points.Length - 1) * (Math.PI / 180.0));
      float f = 0.0f;
      Vector2[] points = this.edge.Points;
      int num2 = radius / 3;
      for (int index = 0; index < this.edge.Points.Length; ++index)
      {
        points[index] = new Vector2(Mathf.Sin(f) * (float) num2, Mathf.Cos(f) * (float) num2);
        f += num1;
      }
      this.edge.Points = points;
      this.edge.SetAllDirty();
      this.edge.enabled = true;
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    this.down = true;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.container, (Vector2) Input.mousePosition, (Camera) null, out this.point_clicked))
      this.point_clicked = Vector2.zero;
    this.UpdateVisuals();
    this.ToPoint();
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    this.down = false;
    Vector2 vector2 = this.Adjust(this.point_clicked);
    Global.systemCopyBuffer = "point(" + (object) (int) vector2.x + ", " + (object) (int) vector2.y + ")";
  }

  private void Update()
  {
    Vector2 localPoint;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.container, (Vector2) Input.mousePosition, (Camera) null, out localPoint))
      localPoint = Vector2.zero;
    if (this.down)
    {
      this.point_clicked = localPoint;
      this.UpdateVisuals();
      this.ToPoint();
      Vector2 vector2 = this.Adjust(localPoint);
      if (this.radius > 0)
        this.text.text = "Radius: " + (object) this.radius + " Point (" + (object) (int) vector2.x + ", " + (object) (int) vector2.y + ")";
      this.text.text = "Point (" + (object) (int) vector2.x + ", " + (object) (int) vector2.y + ")";
    }
    else
    {
      Vector2 vector2 = this.Adjust(localPoint);
      if (this.radius > 0)
        this.text.text = "Radius: " + (object) this.radius + " Point (" + (object) (int) vector2.x + ", " + (object) (int) vector2.y + ")";
      this.text.text = "Point (" + (object) (int) vector2.x + ", " + (object) (int) vector2.y + ")";
    }
  }

  public Vector2 Adjust(Vector2 a)
  {
    a.x = this.container.sizeDelta.x + a.x;
    return a;
  }

  public Vector2 UnAdjust(Vector2 a)
  {
    a.x -= this.container.sizeDelta.x;
    return a;
  }

  private void OnDisable() => this.down = false;
}
