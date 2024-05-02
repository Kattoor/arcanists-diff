// Decompiled with JetBrains decompiler
// Type: ZMyCollider
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZMyCollider : ZComponent
{
  public int id;
  public MyCollider baseCollider;
  internal bool ghosted;
  internal ZMap.GhostTerrain ghosting;
  internal bool enabled = true;
  public ZMyWorld world;
  public int layer = 256;
  public int gameObjectLayer = 8;
  public int x;
  public int y;
  public int radius = 1;
  public int radiusY;
  [NonSerialized]
  internal MyCollider.ParentNode firstParent;
  [NonSerialized]
  internal ZCreature creature;
  [NonSerialized]
  internal ZSpell spell;
  [NonSerialized]
  internal ZEffector effector;
  [NonSerialized]
  internal ZTower tower;

  public void Serialize(ZGame game, myBinaryWriter writer)
  {
    bool flag = game.xCollider.Contains(this);
    writer.Write(flag);
    writer.Write(this.id);
    if (this.id == 0)
      Debug.Log((object) this.baseCollider.name);
    if (flag)
      return;
    game.xCollider.Add(this);
    writer.Write(this.baseCollider.name);
    writer.Write(this.layer);
    writer.Write(this.gameObjectLayer);
    writer.Write(this.radius);
    writer.Write(this.radiusY);
    writer.Write(this.enabled);
    writer.Write(this.x - this.offsetX);
    writer.Write(this.y - this.offsetY);
    writer.Write(ZGame.GetID(this.creature));
    writer.Write(ZGame.GetID(this.effector));
    writer.Write(ZGame.GetID(this.spell));
    writer.Write(this.ghosted);
    if (!this.ghosted)
      return;
    for (ZMap.GhostTerrain ghostTerrain = this.ghosting; ghostTerrain != null; ghostTerrain = ghostTerrain.next)
    {
      writer.Write(ghostTerrain.x);
      writer.Write(ghostTerrain.y);
      writer.Write(ghostTerrain.radius2);
      writer.Write(ghostTerrain.next != null);
    }
  }

  public static void Deserialize(ref ZMyCollider c, ZGame game, myBinaryReader reader)
  {
    int num = reader.ReadBoolean() ? 1 : 0;
    int id = reader.ReadInt32();
    if (num != 0)
    {
      c = game.helper.GetCollider(id);
    }
    else
    {
      c.baseCollider = Inert.GetBaseCollider(reader.ReadString());
      c.id = id;
      c.layer = reader.ReadInt32();
      c.gameObjectLayer = reader.ReadInt32();
      c.radius = reader.ReadInt32();
      c.radiusY = reader.ReadInt32();
      c.enabled = reader.ReadBoolean();
      c.x = reader.ReadInt32();
      c.y = reader.ReadInt32();
      game.helper.col_ref_creatures.Add(new ZGame.IDCollider(c, reader.ReadInt32()));
      game.helper.col_ref_effectors.Add(new ZGame.IDCollider(c, reader.ReadInt32()));
      game.helper.col_ref_spells.Add(new ZGame.IDCollider(c, reader.ReadInt32()));
      if (c.shape == MyCollider.Shape.Polygon)
        c.ArbritaryInit();
      c.world = game.world;
      game.helper.colliderID.Add(c.id, c);
      c.ghosted = reader.ReadBoolean();
      if (!c.ghosted)
        return;
      c.ghosting = new ZMap.GhostTerrain();
      for (ZMap.GhostTerrain ghostTerrain = c.ghosting; ghostTerrain != null; ghostTerrain = ghostTerrain.next)
      {
        ghostTerrain.x = reader.ReadInt32();
        ghostTerrain.y = reader.ReadInt32();
        ghostTerrain.radius2 = reader.ReadInt32();
        ghostTerrain.next = reader.ReadBoolean() ? new ZMap.GhostTerrain() : (ZMap.GhostTerrain) null;
      }
    }
  }

  public static ZMyCollider Create(
    ZGame game,
    MyCollider c,
    Vector3 position,
    Quaternion q,
    Transform parent)
  {
    ZMyCollider zmyCollider = new ZMyCollider();
    zmyCollider.id = ++game.nextColliderID;
    zmyCollider.Copy(c);
    zmyCollider.SetupClient(c, position, q, parent);
    return zmyCollider;
  }

  private void SetupClient(MyCollider c, Vector3 position, Quaternion q, Transform parent)
  {
    MyCollider myCollider = UnityEngine.Object.Instantiate<MyCollider>(c, position, q, parent);
    this.gameObject = myCollider.gameObject;
    this.transform = myCollider.transform;
    myCollider.serverObj = this;
  }

  public void Copy(MyCollider c)
  {
    this.baseCollider = c;
    this.layer = 1 << c.gameObject.layer;
    this.gameObjectLayer = c.gameObject.layer;
    this.radius = c.radius;
    this.radiusY = c.radiusY;
    if (c.shape != MyCollider.Shape.Polygon)
      return;
    this.ArbritaryInit();
  }

  public void Clone(ZMyCollider c, ZGame game)
  {
    this.id = ++game.nextColliderID;
    this.baseCollider = c.baseCollider;
    this.layer = c.layer;
    this.gameObjectLayer = c.gameObjectLayer;
    this.radius = c.radius;
    this.radiusY = c.radiusY;
    this.enabled = c.enabled;
    this.x = c.x;
    this.y = c.y;
    this.world = c.world;
    if (c.shape != MyCollider.Shape.Polygon)
      return;
    this.ArbritaryInit();
  }

  public int offsetX => this.baseCollider.offsetX;

  public int offsetY => this.baseCollider.offsetY;

  public MyLocation position => new MyLocation(this.x, this.y);

  public Point positionAsPoint => new Point(this.x, this.y);

  public int SetRadius
  {
    set
    {
      this.radius = value;
      this.Moved();
    }
  }

  public List<MyCollider.Vertex> polygon => this.baseCollider.polygon;

  public MyLocation Center => this.baseCollider.Center;

  public List<MyCollider.XY> points => this.baseCollider.points;

  public MyCollider.Shape shape => this.baseCollider.shape;

  public StaticPolygons.CachedPolygonEnums cachedPolygon => this.baseCollider.cachedPolygon;

  private Coords boundsMin => this.baseCollider.boundsMin;

  private Coords boundsMax => this.baseCollider.boundsMax;

  public MyCollider.Bounds GetBounds()
  {
    return new MyCollider.Bounds()
    {
      x1 = this.boundsMin.x + this.x,
      x2 = this.boundsMax.x + this.x,
      y1 = this.boundsMin.y + this.y,
      y2 = this.boundsMax.y + this.y
    };
  }

  public int GetRectMinX() => this.boundsMin.x;

  public int GetRectMaxX() => this.boundsMax.x;

  public int GetRectMinY() => this.boundsMin.y;

  public int GetRectMaxY() => this.boundsMax.y;

  internal ZEntity entity
  {
    get
    {
      if (!ZComponent.IsNull((ZComponent) this.creature))
        return (ZEntity) this.creature;
      return ZComponent.IsNull((ZComponent) this.spell) ? (ZEntity) null : (ZEntity) this.spell;
    }
  }

  internal int HasNoRider => !((ZComponent) this.creature?.rider != (object) null) ? 1 : 0;

  public void ArbritaryInit()
  {
    if (this.polygon != null)
      return;
    this.baseCollider.ArbritaryInit();
  }

  public void Initialize(MyLocation pos, ZMyWorld world)
  {
    this.world = world;
    this._Init();
    this.x = pos.x.ToInt() + this.offsetX;
    this.y = pos.y.ToInt() + this.offsetY;
    if (!this.enabled)
      return;
    world.AddCollider(this);
  }

  public void InitializeResync(ZMyWorld world)
  {
    this.world = world;
    this._Init();
    if (!this.enabled)
      return;
    world.AddCollider(this);
  }

  private void _Init()
  {
    if (this.shape == MyCollider.Shape.Polygon)
    {
      this.ArbritaryInit();
    }
    else
    {
      if (this.shape != MyCollider.Shape.Rectangle)
        return;
      this.baseCollider.InitializeRectangle();
    }
  }

  private void OnDestroy()
  {
    for (MyCollider.ParentNode parentNode = this.firstParent; parentNode != null; parentNode = parentNode.next)
      parentNode.node.RemoveCollider(this);
    this.firstParent = (MyCollider.ParentNode) null;
  }

  public void Enable(MyLocation x)
  {
    this.enabled = true;
    this.Move(x);
  }

  public void Enable()
  {
    this.enabled = true;
    this.Moved();
  }

  public void Move(MyLocation pos)
  {
    this.x = pos.x.ToInt() + this.offsetX;
    this.y = pos.y.ToInt() + this.offsetY;
    this.Moved();
  }

  public void Disable(bool setNull = true)
  {
    if (setNull)
    {
      this.spell?.SetNull();
      this.creature?.SetNull();
      this.effector?.SetNull();
      this.ghosting = (ZMap.GhostTerrain) null;
    }
    this.enabled = false;
    MyCollider.ParentNode parentNode = this.firstParent;
    this.firstParent = (MyCollider.ParentNode) null;
    for (; parentNode != null; parentNode = parentNode.next)
      parentNode.node.RemoveCollider(this);
  }

  public bool OverlapGhosting(int x, int y)
  {
    for (ZMap.GhostTerrain ghostTerrain = this.ghosting; ghostTerrain != null; ghostTerrain = ghostTerrain.next)
    {
      if ((x - ghostTerrain.x) * (x - ghostTerrain.x) + (y - ghostTerrain.y) * (y - ghostTerrain.y) <= ghostTerrain.radius2)
        return true;
    }
    return false;
  }

  public void Moved()
  {
    if (this.ghosted)
    {
      this.ghosting = (ZMap.GhostTerrain) null;
      this.ghosted = false;
    }
    if (!this.enabled)
      return;
    MyCollider.ParentNode parentNode = this.firstParent;
    this.firstParent = (MyCollider.ParentNode) null;
    for (; parentNode != null; parentNode = parentNode.next)
      parentNode.node.RemoveCollider(this);
    this.world?.AddCollider(this);
  }

  public bool OverlapPoint(int x, int y)
  {
    if (this.shape == MyCollider.Shape.Circle)
      return (x - this.x) * (x - this.x) + (y - this.y) * (y - this.y) <= this.radius * this.radius;
    if (this.shape == MyCollider.Shape.Polygon)
    {
      MyCollider.Bounds bounds = this.GetBounds();
      if (x < bounds.x1 || y < bounds.y1 || x > bounds.x2 || y > bounds.y2)
        return false;
      if (this.cachedPolygon == StaticPolygons.CachedPolygonEnums.None)
        return this.IsPointInPolygon(x, y);
      if (StaticPolygons.Inside(this.cachedPolygon, x - this.x - this.offsetX, y - this.y - this.offsetY))
        return true;
    }
    else if (this.shape == MyCollider.Shape.Rectangle && x > this.x + this.boundsMin.x && y > this.y + this.boundsMin.y && x < this.x + this.boundsMax.x)
      return y < this.y + this.boundsMax.y;
    return false;
  }

  public bool OverlapRectangle(MyCollider.Bounds rect)
  {
    if (this.shape == MyCollider.Shape.Circle)
      return this.CircleRectangleOverlap(rect.x1, rect.y1, rect.x2, rect.y2);
    return (this.shape == MyCollider.Shape.Polygon || this.shape == MyCollider.Shape.Rectangle) && this.RectanglesOverlap(rect, this.GetBounds());
  }

  private bool RectanglesOverlap(MyCollider.Bounds l, MyCollider.Bounds r)
  {
    return l.x1 < r.x2 && l.x2 > r.x1 && r.y2 > l.y1 && r.y1 < l.y2;
  }

  private bool CircleRectangleOverlap(int X1, int Y1, int X2, int Y2)
  {
    int num1 = Math.Max(X1, Math.Min(this.x, X2));
    int num2 = Math.Max(Y1, Math.Min(this.y, Y2));
    int x = this.x;
    int num3 = num1 - x;
    int num4 = num2 - this.y;
    return num3 * num3 + num4 * num4 <= this.radius * this.radius;
  }

  public bool OverlapPolygon(ZMyCollider polygon)
  {
    if (this.shape == MyCollider.Shape.Circle)
      return polygon.OverlapCircle(this.positionAsPoint, this.radius);
    return this.shape == MyCollider.Shape.Rectangle ? polygon.OverlapRectangle(this.GetBounds()) : this.PolygonCollision(polygon, this, new MyLocation(0, 0)).Intersect;
  }

  public bool OverlapCircle(Point p, int radius)
  {
    if (this.shape == MyCollider.Shape.Circle)
      return (this.x - p.x) * (this.x - p.x) + (this.y - p.y) * (this.y - p.y) <= (radius + this.radius) * (radius + this.radius);
    if (this.shape == MyCollider.Shape.Polygon)
    {
      MyCollider.Bounds bounds = this.GetBounds();
      if (p.x + radius < bounds.x1 || p.y + radius < bounds.y1 || p.x - radius > bounds.x2 || p.y - radius > bounds.y2)
        return false;
      if (this.cachedPolygon != StaticPolygons.CachedPolygonEnums.None)
      {
        int x = p.x - this.x - this.offsetX;
        int y = p.y - this.y - this.offsetY;
        if (StaticPolygons.Inside(this.cachedPolygon, x, y))
          return true;
        int num = radius * radius;
        for (int index = 0; index < this.polygon.Count; ++index)
        {
          MyCollider.Vertex vertex = this.polygon[index];
          if ((x - vertex.X).Squared() + (y - vertex.Y).Squared() < num)
            return true;
        }
        List<Coords> outlineArray = MapGenerator.getOutlineArray(radius);
        for (int index = 0; index < outlineArray.Count; index += 3)
        {
          if (StaticPolygons.Inside(this.cachedPolygon, x + outlineArray[index].x, y + outlineArray[index].y))
            return true;
        }
      }
      else
      {
        if (this.IsPointInPolygon(p.x, p.y))
          return true;
        int index1 = this.polygon.Count - 1;
        p.x -= this.x;
        p.y -= this.y;
        int num1 = radius * radius;
        for (int index2 = 0; index2 < this.polygon.Count; ++index2)
        {
          MyCollider.Vertex vertex1 = this.polygon[index2];
          if ((p.x - vertex1.X) * (p.x - vertex1.X) + (p.y - vertex1.Y) * (p.y - vertex1.Y) < num1)
            return true;
          MyCollider.Vertex vertex2 = this.polygon[index1];
          int num2 = (vertex2.X - vertex1.X) * (p.y - vertex1.Y) - (p.x - vertex1.X) * (vertex2.Y - vertex1.Y);
          if (num2 < 0)
            num2 = -num2;
          if (num2 / vertex1.LAB <= radius)
          {
            MyLocation myLocation = new MyLocation(vertex1.X - p.x, vertex1.Y - p.y);
            FixedInt dotD = vertex1.dotD;
            FixedInt ret1;
            MyLocation.Dot(myLocation, vertex1.d, out ret1);
            ret1 *= 2;
            FixedInt ret2;
            MyLocation.Dot(myLocation, myLocation, out ret2);
            ret2 -= radius * radius;
            FixedInt ret3 = ret1 * ret1 - FixedInt.Create(4) * dotD * ret2;
            if (ret3.BiggerThenOrEqualToZero())
            {
              Mathd.Sqrt(ret3.RawValue, out ret3);
              FixedInt fixedInt1 = (-ret1 - ret3) / (FixedInt.Create(2) * dotD);
              FixedInt fixedInt2 = (-ret1 + ret3) / (FixedInt.Create(2) * dotD);
              if (fixedInt1.BiggerThenOrEqualToZero() && fixedInt1.SmallerThenOrEqualToOne() || fixedInt2.BiggerThenOrEqualToZero() && fixedInt2.SmallerThenOrEqualToOne())
                return true;
            }
          }
          if (this.lineIntersects(p, radius, this.polygon[index2], this.polygon[index1]))
            return true;
          index1 = index2;
        }
      }
    }
    else if (this.shape == MyCollider.Shape.Rectangle)
    {
      MyCollider.Bounds bounds = this.GetBounds();
      return p.x + radius >= bounds.x1 && p.y + radius >= bounds.y1 && p.x - radius <= bounds.x2 && p.y - radius <= bounds.y2;
    }
    return false;
  }

  public bool IsPointInPolygon(int x, int y)
  {
    x -= this.x;
    y -= this.y;
    FixedInt fixedInt1 = (FixedInt) x;
    FixedInt fixedInt2 = (FixedInt) y;
    bool flag = false;
    int index1 = this.polygon.Count - 1;
    for (int index2 = 0; index2 < this.polygon.Count; ++index2)
    {
      if ((this.polygon[index2].fY < fixedInt2 && this.polygon[index1].fY >= fixedInt2 || this.polygon[index1].fY < fixedInt2 && this.polygon[index2].fY >= fixedInt2) && this.polygon[index2].fX + (fixedInt2 - this.polygon[index2].fY) / (this.polygon[index1].fY - this.polygon[index2].fY) * (this.polygon[index1].fX - this.polygon[index2].fX) < fixedInt1)
        flag = !flag;
      index1 = index2;
    }
    return flag;
  }

  private bool lineIntersects(Point p, int radius, MyCollider.Vertex a, MyCollider.Vertex b)
  {
    int num1 = (b.X - a.X) * (p.x - a.Y) - (p.x - a.X) * (b.Y - a.Y);
    if (num1 < 0)
      num1 = -num1;
    int num2 = (int) Mathd.IntSqrt((long) ((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y)));
    if (num1 / num2 >= radius)
      return false;
    MyLocation myLocation1 = new MyLocation(b.X - a.X, b.Y - a.Y);
    MyLocation myLocation2 = new MyLocation(a.X - p.x, a.Y - p.y);
    FixedInt ret1;
    MyLocation.Dot(myLocation1, myLocation1, out ret1);
    FixedInt ret2;
    MyLocation.Dot(myLocation2, myLocation1, out ret2);
    ret2 *= 2;
    FixedInt ret3;
    MyLocation.Dot(myLocation2, myLocation2, out ret3);
    FixedInt fixedInt1 = ret3 - radius * radius;
    FixedInt ret4 = ret2 * ret2 - FixedInt.Create(4) * ret1 * fixedInt1;
    if (ret4.BiggerThenOrEqualToZero())
    {
      Mathd.Sqrt(ret4.RawValue, out ret4);
      FixedInt fixedInt2 = (-ret2 - ret4) / (FixedInt.Create(2) * ret1);
      FixedInt fixedInt3 = (-ret2 + ret4) / (FixedInt.Create(2) * ret1);
      if (fixedInt2.BiggerThenOrEqualToZero() && fixedInt2.SmallerThenOrEqualToOne() || fixedInt3.BiggerThenOrEqualToZero() && fixedInt3.SmallerThenOrEqualToOne())
        return true;
    }
    return false;
  }

  public bool AddNode(ZMyWorld.Node c)
  {
    if (this.firstParent == null)
    {
      this.firstParent = new MyCollider.ParentNode()
      {
        node = c
      };
    }
    else
    {
      MyCollider.ParentNode parentNode1 = this.firstParent;
      MyCollider.ParentNode parentNode2 = parentNode1;
      for (; parentNode1 != null; parentNode1 = parentNode1.next)
      {
        if (parentNode1.node == c)
          return false;
        parentNode2 = parentNode1;
      }
      parentNode2.next = new MyCollider.ParentNode()
      {
        node = c
      };
    }
    return true;
  }

  public ZMyCollider.PolygonCollisionResult PolygonCollision(
    ZMyCollider polygonA,
    ZMyCollider polygonB,
    MyLocation velocity)
  {
    ZMyCollider.PolygonCollisionResult polygonCollisionResult = new ZMyCollider.PolygonCollisionResult();
    polygonCollisionResult.Intersect = true;
    polygonCollisionResult.WillIntersect = true;
    int count1 = polygonA.polygon.Count;
    int count2 = polygonB.polygon.Count;
    FixedInt fixedInt1 = FixedInt.MaxValue;
    MyLocation rhs = new MyLocation();
    for (int index = 0; index < count1 + count2; ++index)
    {
      MyCollider.Vertex vertex = index >= count1 ? polygonB.polygon[index - count1] : polygonA.polygon[index];
      MyLocation myLocation = index < count1 ? new MyLocation(-vertex.Y - polygonA.y, vertex.X + polygonA.x) : new MyLocation(-vertex.Y - polygonB.y, vertex.X + polygonB.x);
      myLocation.Normalize();
      FixedInt min1 = (FixedInt) 0;
      FixedInt min2 = (FixedInt) 0;
      FixedInt max1 = (FixedInt) 0;
      FixedInt max2 = (FixedInt) 0;
      this.ProjectPolygon(myLocation, polygonA, ref min1, ref max1);
      this.ProjectPolygon(myLocation, polygonB, ref min2, ref max2);
      if (this.IntervalDistance(min1, max1, min2, max2) > 0)
        polygonCollisionResult.Intersect = false;
      FixedInt ret1;
      MyLocation.Dot(myLocation, velocity, out ret1);
      if (ret1 < 0)
        min1 += ret1;
      else
        max1 += ret1;
      FixedInt x = this.IntervalDistance(min1, max1, min2, max2);
      if (x > 0)
        polygonCollisionResult.WillIntersect = false;
      if (polygonCollisionResult.Intersect || polygonCollisionResult.WillIntersect)
      {
        FixedInt fixedInt2 = FixedInt.Abs(x);
        if (fixedInt2 < fixedInt1)
        {
          fixedInt1 = fixedInt2;
          rhs = myLocation;
          FixedInt ret2;
          MyLocation.Dot(polygonA.Center - polygonB.Center, rhs, out ret2);
          if (ret2 < 0)
            rhs = -rhs;
        }
      }
      else
        break;
    }
    if (polygonCollisionResult.WillIntersect)
      polygonCollisionResult.MinimumTranslationVector = rhs * fixedInt1;
    return polygonCollisionResult;
  }

  public FixedInt IntervalDistance(FixedInt minA, FixedInt maxA, FixedInt minB, FixedInt maxB)
  {
    return minA < minB ? minB - maxA : minA - maxB;
  }

  public void ProjectPolygon(
    MyLocation axis,
    ZMyCollider polygon,
    ref FixedInt min,
    ref FixedInt max)
  {
    FixedInt ret;
    MyLocation.Dot(axis, new MyLocation(polygon.polygon[0].X + polygon.x, polygon.polygon[0].Y + polygon.y), out ret);
    min = ret;
    max = ret;
    for (int index = 0; index < polygon.polygon.Count; ++index)
    {
      MyLocation.Dot(new MyLocation(polygon.polygon[0].X + polygon.x, polygon.polygon[0].Y + polygon.y), axis, out ret);
      if (ret < min)
        min = ret;
      else if (ret > max)
        max = ret;
    }
  }

  public struct PolygonCollisionResult
  {
    public bool WillIntersect;
    public bool Intersect;
    public MyLocation MinimumTranslationVector;
  }
}
