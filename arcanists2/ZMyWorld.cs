

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZMyWorld
{
  private const int _block = 128;
  private int _numBlocksHorizontal;
  private int _numVertical;
  private List<ZMyWorld.Node> nodes = new List<ZMyWorld.Node>();
  public ZMap map;
  public ZMyWorld.ListPool listPool = new ZMyWorld.ListPool();

  public ZMyWorld.Node GetNode(int x, int y)
  {
    x += 7;
    return x < 0 || y < 0 || x >= this._numBlocksHorizontal || y >= this._numVertical ? (ZMyWorld.Node) null : this.nodes[x + y * this._numBlocksHorizontal];
  }

  public void AddCollider(ZMyCollider c)
  {
    int x1 = c.x >> 7;
    int y1 = c.y >> 7;
    if (c.shape == MyCollider.Shape.Circle)
    {
      if (c.radius > 128)
      {
        int num1 = c.x - c.radius >> 7;
        int num2 = c.y - c.radius >> 7;
        int num3 = c.x + c.radius >> 7;
        int num4 = c.y + c.radius >> 7;
        for (int x2 = num1; x2 <= num3; ++x2)
        {
          for (int y2 = num2; y2 <= num4; ++y2)
          {
            if (x2 >= 0 && x2 < this._numBlocksHorizontal && y2 >= 0 && y2 < this._numVertical)
              this.GetNode(x2, y2)?.AddCollider(c);
          }
        }
        return;
      }
      int x3 = c.x - c.radius >> 7;
      int y3 = c.y - c.radius >> 7;
      if (x3 != x1 && x3 >= 0)
      {
        this.GetNode(x3, y1)?.AddCollider(c);
        if (y3 != y1)
          this.GetNode(x3, y3)?.AddCollider(c);
        if (c.y + c.radius >> 7 != y1)
          this.GetNode(x3, c.y + c.radius >> 7)?.AddCollider(c);
      }
      int x4 = c.x + c.radius >> 7;
      if (x4 != x1 && x4 != x1 && x4 < this._numBlocksHorizontal)
      {
        this.GetNode(x4, y1)?.AddCollider(c);
        if (y3 != y1)
          this.GetNode(x4, y3)?.AddCollider(c);
        if (c.y + c.radius >> 7 != y1)
          this.GetNode(x4, c.y + c.radius >> 7)?.AddCollider(c);
      }
      this.GetNode(x1, y3)?.AddCollider(c);
      int y4 = c.y + c.radius >> 7;
      this.GetNode(x1, y4)?.AddCollider(c);
    }
    else
    {
      if (c.shape == MyCollider.Shape.Polygon)
      {
        MyCollider.Bounds bounds = c.GetBounds();
        bounds.x1 >>= 7;
        bounds.x2 >>= 7;
        bounds.y1 >>= 7;
        bounds.y2 >>= 7;
        for (int x1_1 = bounds.x1; x1_1 <= bounds.x2; ++x1_1)
        {
          for (int y1_1 = bounds.y1; y1_1 <= bounds.y2; ++y1_1)
            this.GetNode(x1_1, y1_1)?.AddCollider(c);
        }
        return;
      }
      if (c.shape == MyCollider.Shape.Rectangle)
      {
        int num5 = c.x + c.GetRectMinX() >> 7;
        int num6 = c.y + c.GetRectMinY() >> 7;
        int num7 = c.x + c.GetRectMaxX() >> 7;
        int num8 = c.y + c.GetRectMaxY() >> 7;
        for (int x5 = num5; x5 <= num7; ++x5)
        {
          for (int y5 = num6; y5 <= num8; ++y5)
          {
            if (x5 >= 0 && x5 < this._numBlocksHorizontal && y5 >= 0 && y5 < this._numVertical)
              this.GetNode(x5, y5)?.AddCollider(c);
          }
        }
        return;
      }
    }
    this.GetNode(x1, y1)?.AddCollider(c);
  }

  public void Clear() => this.nodes.Clear();

  public void Init(int x, int y, int count)
  {
    this.Clear();
    x += 14;
    y += 7;
    count = x * y;
    this._numBlocksHorizontal = x;
    this._numVertical = y;
    this.nodes = new List<ZMyWorld.Node>(count);
    int num = 896;
    for (int index1 = 0; index1 < y; ++index1)
    {
      for (int index2 = 0; index2 < x; ++index2)
      {
        int x1 = index2 * 128 - num;
        int y1 = index1 * 128;
        this.nodes.Add(new ZMyWorld.Node()
        {
          x = index2,
          y = index1,
          a = new Point(x1, y1),
          b = new Point(x1, y1 + 128),
          c = new Point(x1 + 128, y1 + 128),
          d = new Point(x1 + 128, y1)
        });
      }
    }
  }

  public ZMyCollider FindColliderAtPoint(int x, int y, int layer)
  {
    return this.GetNode(x >> 7, y >> 7)?.FindColliderAtPoint(x, y, layer) ?? (ZMyCollider) null;
  }

  public ZMyCollider FindColliderAtPoint(int x, int y, ZCreature notThis, int layer)
  {
    return this.GetNode(x >> 7, y >> 7)?.FindColliderAtPoint(x, y, notThis, layer) ?? (ZMyCollider) null;
  }

  public bool OverlapPoint(int x, int y, int layer)
  {
    ZMyWorld.Node node = this.GetNode(x >> 7, y >> 7);
    return node != null && node.OverlapPoint(x, y, layer);
  }

  public List<ZMyCollider> OverlapPointAll(int x, int y, int layer = -1)
  {
    int num = 0;
    List<ZMyCollider> list = this.listPool.Get();
    this.GetNode(x >> 7, y >> 7)?.OverlapPointAll(x, y, layer, list, ref num);
    return list;
  }

  public List<ZMyCollider> OverlapArbritaryPolygonAll(ZMyCollider poly, int layer = -1)
  {
    List<ZMyCollider> list = this.listPool.Get();
    if ((ZComponent) poly == (object) null)
    {
      Debug.LogError((object) "Null Collider in OverlapArbritaryPolygonAll");
      return list;
    }
    MyCollider.Bounds bounds = poly.GetBounds();
    bounds.x1 >>= 7;
    bounds.x2 >>= 7;
    bounds.y1 >>= 7;
    bounds.y2 >>= 7;
    for (int x1 = bounds.x1; x1 <= bounds.x2; ++x1)
    {
      for (int y1 = bounds.y1; y1 <= bounds.y2; ++y1)
        this.GetNode(x1, y1)?.OverlapPolygonAll(poly, layer, list);
    }
    return list;
  }

  public List<ZMyCollider> OverlapColliderAll(ZMyCollider poly, int layer = -1)
  {
    List<ZMyCollider> list = this.listPool.Get();
    if ((ZComponent) poly == (object) null)
    {
      Debug.LogError((object) "Null Collider in OverlapColliderAll");
      return list;
    }
    MyCollider.ParentNode parentNode = poly.firstParent;
    int num = 0;
    if (poly.shape == MyCollider.Shape.Circle)
    {
      for (; parentNode != null; parentNode = parentNode.next)
        parentNode.node.OverlapCircleAll(new Point(poly.x, poly.y), poly.radius, poly, layer, list, ref num);
    }
    else
    {
      for (; parentNode != null; parentNode = parentNode.next)
        parentNode.node.OverlapPolygonAll(poly, layer, list);
    }
    return list;
  }

  public int OverlapColliderAll(ZMyCollider poly, ZMyCollider[] list, int layer = -1)
  {
    if ((ZComponent) poly == (object) null)
    {
      Debug.LogError((object) "Null Collider in OverlapColliderAll");
      return 0;
    }
    MyCollider.ParentNode parentNode = poly.firstParent;
    int num = 0;
    if (poly.shape == MyCollider.Shape.Circle)
    {
      for (; parentNode != null; parentNode = parentNode.next)
        parentNode.node.OverlapCircleAll(new Point(poly.x, poly.y), poly.radius, poly, layer, list, ref num);
    }
    else
    {
      for (; parentNode != null; parentNode = parentNode.next)
        parentNode.node.OverlapPolygonAll(poly, layer, list, ref num);
    }
    return num;
  }

  public List<ZMyCollider> OverlapRectangleAll(
    MyCollider.Bounds bounds,
    ZMyCollider noCollision,
    int layer = -1)
  {
    List<ZMyCollider> list = this.listPool.Get();
    int num1 = 0;
    int num2 = bounds.x1 >> 7;
    int num3 = bounds.x2 >> 7;
    int num4 = bounds.y1 >> 7;
    int num5 = bounds.y2 >> 7;
    for (int x = num2; x <= num3; ++x)
    {
      for (int y = num4; y <= num5; ++y)
      {
        if (x >= 0 && x < this._numBlocksHorizontal && y >= 0 && y < this._numVertical)
          this.GetNode(x, y)?.OverlapRectangleAll(bounds, noCollision, layer, list, ref num1);
      }
    }
    return list;
  }

  public List<ZMyCollider> OverlapCircleAll(
    Point c,
    int radius,
    ZMyCollider noCollision,
    int layer = -1)
  {
    List<ZMyCollider> list = this.listPool.Get();
    int num1 = 0;
    int x1 = c.x >> 7;
    int y1 = c.y >> 7;
    if (radius > 128)
    {
      int num2 = c.x - radius >> 7;
      int num3 = c.y - radius >> 7;
      int num4 = c.x + radius >> 7;
      int num5 = c.y + radius >> 7;
      for (int x2 = num2; x2 <= num4; ++x2)
      {
        for (int y2 = num3; y2 <= num5; ++y2)
        {
          if (x2 >= 0 && x2 < this._numBlocksHorizontal && y2 >= 0 && y2 < this._numVertical)
            this.GetNode(x2, y2)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
        }
      }
      return list;
    }
    this.GetNode(x1, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    int x3 = c.x - radius >> 7;
    int y3 = c.y - radius >> 7;
    if (x3 != x1 && x3 >= 0)
    {
      this.GetNode(x3, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      if (y3 >= 0)
        this.GetNode(x3, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      this.GetNode(x3, c.y + radius >> 7)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    }
    int x4 = c.x + radius >> 7;
    if (x4 != x1 && x4 != x1 && x4 < this._numBlocksHorizontal)
    {
      this.GetNode(x4, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      if (y3 >= 0)
        this.GetNode(x4, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      this.GetNode(x4, c.y + radius >> 7)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    }
    if (y3 >= 0 && y3 != y1)
      this.GetNode(x1, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    int y4 = c.y + radius >> 7;
    if (y4 != y1)
      this.GetNode(x1, y4)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    return list;
  }

  public int OverlapCircleAll(
    Point c,
    int radius,
    ZMyCollider noCollision,
    ZMyCollider[] list,
    int layer = -1)
  {
    int num1 = 0;
    int x1 = c.x >> 7;
    int y1 = c.y >> 7;
    if (radius > 128)
    {
      int num2 = c.x - radius >> 7;
      int num3 = c.y - radius >> 7;
      int num4 = c.x + radius >> 7;
      int num5 = c.y + radius >> 7;
      for (int x2 = num2; x2 <= num4; ++x2)
      {
        for (int y2 = num3; y2 <= num5; ++y2)
        {
          if (x2 >= 0 && x2 < this._numBlocksHorizontal && y2 >= 0 && y2 < this._numVertical)
            this.GetNode(x2, y2)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
        }
      }
      return num1;
    }
    this.GetNode(x1, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    int x3 = c.x - radius >> 7;
    int y3 = c.y - radius >> 7;
    if (x3 != x1 && x3 >= 0)
    {
      this.GetNode(x3, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      if (y3 >= 0)
        this.GetNode(x3, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      this.GetNode(x3, c.y + radius >> 7)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    }
    int x4 = c.x + radius >> 7;
    if (x4 != x1 && x4 != x1 && x4 < this._numBlocksHorizontal)
    {
      this.GetNode(x4, y1)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      if (y3 >= 0)
        this.GetNode(x4, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
      this.GetNode(x4, c.y + radius >> 7)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    }
    if (y3 >= 0 && y3 != y1)
      this.GetNode(x1, y3)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    int y4 = c.y + radius >> 7;
    if (y4 != y1)
      this.GetNode(x1, y4)?.OverlapCircleAll(c, radius, noCollision, layer, list, ref num1);
    return num1;
  }

  public bool Overlaps(ZMyCollider a, MyLocation pos, int radius)
  {
    return a.OverlapCircle((Point) pos, radius);
  }

  public bool OverlapCircle(Point c, int radius, ZMyCollider noCollision, int layer)
  {
    int x1 = c.x >> 7;
    int y1 = c.y >> 7;
    if (radius > 128)
    {
      int num1 = c.x - radius >> 7;
      int num2 = c.y - radius >> 7;
      int num3 = c.x + radius >> 7;
      int num4 = c.y + radius >> 7;
      for (int x2 = num1; x2 <= num3; ++x2)
      {
        for (int y2 = num2; y2 <= num4; ++y2)
        {
          if (x2 >= 0 && x2 < this._numBlocksHorizontal && y2 >= 0 && y2 < this._numVertical)
          {
            ZMyWorld.Node node = this.GetNode(x2, y2);
            if ((node != null ? (node.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
              return true;
          }
        }
      }
      return false;
    }
    ZMyWorld.Node node1 = this.GetNode(x1, y1);
    if ((node1 != null ? (node1.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
      return true;
    int x3 = c.x - radius >> 7;
    int y3 = c.y - radius >> 7;
    if (x3 != x1 && x3 >= 0)
    {
      ZMyWorld.Node node2 = this.GetNode(x3, y1);
      if ((node2 != null ? (node2.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
      ZMyWorld.Node node3 = this.GetNode(x3, y3);
      if ((node3 != null ? (node3.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
      ZMyWorld.Node node4 = this.GetNode(x3, c.y + radius >> 7);
      if ((node4 != null ? (node4.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
    }
    int x4 = c.x + radius >> 7;
    if (x4 != x1 && x4 != x1 && x4 < this._numBlocksHorizontal)
    {
      ZMyWorld.Node node5 = this.GetNode(x4, y1);
      if ((node5 != null ? (node5.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
      ZMyWorld.Node node6 = this.GetNode(x4, y3);
      if ((node6 != null ? (node6.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
      ZMyWorld.Node node7 = this.GetNode(x4, c.y + radius >> 7);
      if ((node7 != null ? (node7.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
    }
    ZMyWorld.Node node8 = this.GetNode(x1, y3);
    if ((node8 != null ? (node8.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
      return true;
    int y4 = c.y + radius >> 7;
    if (y4 != y1)
    {
      ZMyWorld.Node node9 = this.GetNode(x1, y4);
      if ((node9 != null ? (node9.OverlapCircle(c, radius, noCollision, layer) ? 1 : 0) : 0) != 0)
        return true;
    }
    return false;
  }

  public class ListPool
  {
    public List<List<ZMyCollider>> list = new List<List<ZMyCollider>>();
    private int curIndex = -1;

    public List<ZMyCollider> Get()
    {
      ++this.curIndex;
      if (this.curIndex < this.list.Count)
        return this.list[this.curIndex];
      List<ZMyCollider> zmyColliderList = new List<ZMyCollider>();
      this.list.Add(zmyColliderList);
      return zmyColliderList;
    }

    public void Clear()
    {
      if (this.curIndex == -1)
        return;
      for (int curIndex = this.curIndex; curIndex >= 0; --curIndex)
        this.list[curIndex].Clear();
      this.curIndex = -1;
    }

    public void ReturnList(List<ZMyCollider> list)
    {
      if (list != this.list[this.curIndex])
        return;
      list.Clear();
      --this.curIndex;
    }
  }

  public class Node
  {
    public Point a;
    public Point b;
    public Point c;
    public Point d;
    public int x;
    public int y;
    public ZMyWorld.Item item;

    public override string ToString() => "pos: (" + (object) this.x + " " + (object) this.y + ")";

    public bool ContainsPoint(Point p)
    {
      return p.x >= this.a.x && p.x <= this.d.x && p.y >= this.a.y && p.y <= this.b.y;
    }

    public void AddCollider(ZMyCollider c)
    {
      if (!c.AddNode(this))
        return;
      if (this.item == null)
        this.item = new ZMyWorld.Item() { collider = c };
      else if (this.item.collider.y < c.y)
      {
        ZMyWorld.Item obj = new ZMyWorld.Item()
        {
          collider = this.item.collider,
          next = this.item.next
        };
        this.item = new ZMyWorld.Item()
        {
          collider = c,
          next = obj
        };
      }
      else
      {
        ZMyWorld.Item next;
        for (next = this.item; next.next != null; next = next.next)
        {
          if (next.next.collider.y < c.y)
          {
            ZMyWorld.Item obj = new ZMyWorld.Item()
            {
              collider = next.next.collider,
              next = next.next.next
            };
            next.next = new ZMyWorld.Item()
            {
              collider = c,
              next = obj
            };
            return;
          }
        }
        next.next = new ZMyWorld.Item() { collider = c };
      }
    }

    public void RemoveCollider(ZMyCollider c)
    {
      if ((ZComponent) this.item.collider == (object) c)
      {
        this.item = this.item.next;
      }
      else
      {
        ZMyWorld.Item obj = this.item;
        ZMyWorld.Item next;
        for (next = this.item; (ZComponent) next.collider != (object) c; next = next.next)
          obj = next;
        obj.next = next.next;
      }
    }

    public bool OverlapCircle(Point p, int radius, ZMyCollider noCollision, int layer)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapCircle(p, radius))
          return true;
      }
      return false;
    }

    public ZMyCollider FindColliderAtPoint(int x, int y, int layer)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapPoint(x, y))
          return next.collider;
      }
      return (ZMyCollider) null;
    }

    public ZMyCollider FindColliderAtPoint(int x, int y, ZCreature notThis, int layer)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((next.collider.layer & layer) != 0 && (ZComponent) notThis != (object) next.collider.creature && next.collider.enabled && next.collider.OverlapPoint(x, y))
          return next.collider;
      }
      return (ZMyCollider) null;
    }

    public bool OverlapPoint(int x, int y, int layer)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapPoint(x, y))
          return true;
      }
      return false;
    }

    public void OverlapPolygonAll(ZMyCollider noCollision, int layer, List<ZMyCollider> list)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && (next.collider.shape == MyCollider.Shape.Circle ? (noCollision.OverlapCircle((Point) next.collider.position, next.collider.radius) ? 1 : 0) : (noCollision.OverlapRectangle(next.collider.GetBounds()) ? 1 : 0)) != 0)
        {
          bool flag = true;
          for (int index = 0; index < list.Count; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
            list.Add(next.collider);
        }
      }
    }

    public void OverlapPolygonAll(
      ZMyCollider noCollision,
      int layer,
      ZMyCollider[] list,
      ref int num)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.shape == MyCollider.Shape.Circle && noCollision.OverlapCircle((Point) next.collider.position, next.collider.radius))
        {
          bool flag = true;
          for (int index = 0; index < num; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            list[num] = next.collider;
            ++num;
          }
        }
      }
    }

    public void OverlapRectangleAll(
      MyCollider.Bounds bounds,
      ZMyCollider noCollision,
      int layer,
      List<ZMyCollider> list,
      ref int num)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapRectangle(bounds))
        {
          bool flag = true;
          for (int index = 0; index < num; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            list.Add(next.collider);
            ++num;
          }
        }
      }
    }

    public void OverlapCircleAll(
      Point p,
      int radius,
      ZMyCollider noCollision,
      int layer,
      List<ZMyCollider> list,
      ref int num)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapCircle(p, radius))
        {
          bool flag = true;
          for (int index = 0; index < num; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            list.Add(next.collider);
            ++num;
          }
        }
      }
    }

    public void OverlapCircleAll(
      Point p,
      int radius,
      ZMyCollider noCollision,
      int layer,
      ZMyCollider[] list,
      ref int num)
    {
      for (ZMyWorld.Item next = this.item; next != null && num < list.Length; next = next.next)
      {
        if ((ZComponent) next.collider != (object) noCollision && (next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapCircle(p, radius))
        {
          bool flag = true;
          for (int index = 0; index < num; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            list[num] = next.collider;
            ++num;
          }
        }
      }
    }

    public void OverlapPointAll(int x, int y, int layer, List<ZMyCollider> list, ref int num)
    {
      for (ZMyWorld.Item next = this.item; next != null; next = next.next)
      {
        if ((next.collider.layer & layer) != 0 && next.collider.enabled && next.collider.OverlapPoint(x, y))
        {
          bool flag = true;
          for (int index = 0; index < num; ++index)
          {
            if ((ZComponent) list[index] == (object) next.collider)
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            list.Add(next.collider);
            ++num;
          }
        }
      }
    }
  }

  public class Item
  {
    public ZMyWorld.Item next;
    public ZMyCollider collider;
  }
}
