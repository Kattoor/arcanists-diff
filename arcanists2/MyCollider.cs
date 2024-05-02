

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MyCollider : MonoBehaviour
{
  internal ZMyCollider serverObj;
  public int layer = 256;
  public int offsetX;
  public int offsetY;
  public int x;
  public int y;
  public int radius = 1;
  public int radiusY;
  public List<MyCollider.Vertex> polygon;
  public List<MyCollider.XY> points = new List<MyCollider.XY>();
  public MyLocation Center;
  public MyCollider.Shape shape;
  public StaticPolygons.CachedPolygonEnums cachedPolygon;
  internal Coords boundsMin;
  internal Coords boundsMax;

  public int GetRectMinX() => this.boundsMin.x;

  public int GetRectMaxX() => this.boundsMax.x;

  public int GetRectMinY() => this.boundsMin.y;

  public int GetRectMaxY() => this.boundsMax.y;

  [ContextMenu("Setup")]
  private void setup()
  {
    Collider2D component = this.GetComponent<Collider2D>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    if (typeof (CircleCollider2D) == ((object) component).GetType())
    {
      this.radius = (int) ((CircleCollider2D) component).radius;
    }
    else
    {
      this.shape = MyCollider.Shape.Polygon;
      PolygonCollider2D polygonCollider2D = (PolygonCollider2D) component;
      this.points = new List<MyCollider.XY>();
      foreach (Vector2 point in polygonCollider2D.points)
        this.points.Add(new MyCollider.XY()
        {
          x = (int) ((double) point.x + (double) polygonCollider2D.offset.x),
          y = (int) ((double) point.y + (double) polygonCollider2D.offset.y)
        });
    }
  }

  [ContextMenu("Sift")]
  private void shift()
  {
    for (int index = 0; index < this.points.Count; ++index)
    {
      this.points[index].x += this.offsetX;
      this.points[index].y += this.offsetY;
    }
    Debug.Log((object) "Finished");
  }

  public void ArbritaryInit()
  {
    if (this.polygon != null)
      return;
    this.polygon = new List<MyCollider.Vertex>(this.points.Count);
    foreach (MyCollider.XY point in this.points)
      this.polygon.Add(new MyCollider.Vertex(point.x, point.y));
    this.boundsMin = new Coords(0, 0);
    this.boundsMax = new Coords(0, 0);
    int num1 = 0;
    int num2 = 0;
    foreach (MyCollider.Vertex vertex in this.polygon)
    {
      num1 += vertex.X;
      num2 += vertex.Y;
      if (vertex.X < this.boundsMin.x)
        this.boundsMin.x = vertex.X;
      else if (vertex.X > this.boundsMax.x)
        this.boundsMax.x = vertex.X;
      if (vertex.Y < this.boundsMin.y)
        this.boundsMin.y = vertex.Y;
      else if (vertex.Y > this.boundsMax.y)
        this.boundsMax.y = vertex.Y;
    }
    this.Center = new MyLocation((FixedInt) num1 / (FixedInt) this.polygon.Count, (FixedInt) num2 / (FixedInt) this.polygon.Count);
    int index1 = this.polygon.Count - 1;
    for (int index2 = 0; index2 < this.polygon.Count; ++index2)
    {
      MyCollider.Vertex vertex1 = this.polygon[index2];
      MyCollider.Vertex vertex2 = this.polygon[index1];
      vertex1.LAB = (int) Mathd.IntSqrt((long) ((vertex2.X - vertex1.X) * (vertex2.X - vertex1.X) + (vertex2.Y - vertex1.Y) * (vertex2.Y - vertex1.Y)));
      vertex1.d = new MyLocation(vertex2.X - vertex1.X, vertex2.Y - vertex1.Y);
      MyLocation.Dot(vertex1.d, vertex1.d, out vertex1.dotD);
      index1 = index2;
    }
  }

  public void InitializeRectangle()
  {
    if (this.boundsMin != null)
      return;
    int x = this.radius / 2;
    int y = this.radiusY / 2;
    this.boundsMin = new Coords(-x, -y);
    this.boundsMax = new Coords(x, y);
  }

  [Serializable]
  public class XY
  {
    public int x;
    public int y;
  }

  public class Vertex
  {
    public int X;
    public int Y;
    public FixedInt fX;
    public FixedInt fY;
    public int LAB;
    public MyLocation d;
    public FixedInt dotD;

    public Vertex(int x, int y)
    {
      this.X = x;
      this.Y = y;
      this.fX = (FixedInt) this.X;
      this.fY = (FixedInt) this.Y;
    }
  }

  public class ParentNode
  {
    public MyCollider.ParentNode next;
    public ZMyWorld.Node node;
  }

  public enum Shape
  {
    Circle,
    Rectangle,
    Polygon,
    DELETE,
  }

  public struct Bounds
  {
    public int x1;
    public int y1;
    public int x2;
    public int y2;
  }
}
