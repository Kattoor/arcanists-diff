// Decompiled with JetBrains decompiler
// Type: Pathfinder
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Pathfinder
{
  public Pathfinder.Node[,] Tiles;
  public int Width;
  public int Height;

  public void Init(int w, int h)
  {
    if (this.Tiles != null)
      return;
    this.Width = w;
    this.Height = h;
    this.Tiles = new Pathfinder.Node[this.Width, this.Height];
    for (int x = 0; x < this.Width; ++x)
    {
      for (int y = 0; y < this.Height; ++y)
        this.Tiles[x, y] = new Pathfinder.Node(new Pathfinder.GridTile(x, y));
    }
    for (int index1 = 0; index1 < this.Width; ++index1)
    {
      for (int index2 = 0; index2 < this.Height; ++index2)
      {
        if (index1 > 0)
          this.Tiles[index1, index2].edges.Add(new Pathfinder.Edge()
          {
            start = this.Tiles[index1, index2],
            end = this.Tiles[index1 - 1, index2]
          });
        if (index1 < this.Width - 1)
          this.Tiles[index1, index2].edges.Add(new Pathfinder.Edge()
          {
            start = this.Tiles[index1, index2],
            end = this.Tiles[index1 + 1, index2]
          });
        if (index2 > 0)
          this.Tiles[index1, index2].edges.Add(new Pathfinder.Edge()
          {
            start = this.Tiles[index1, index2],
            end = this.Tiles[index1, index2 - 1]
          });
        if (index2 < this.Height - 1)
          this.Tiles[index1, index2].edges.Add(new Pathfinder.Edge()
          {
            start = this.Tiles[index1, index2],
            end = this.Tiles[index1, index2 + 1]
          });
      }
    }
  }

  private static float EuclidianDistance(Pathfinder.Node node1, Pathfinder.Node node2)
  {
    return Pathfinder.EuclidianDistance(node1.pos, node2.pos);
  }

  private static float EuclidianDistance(Pathfinder.GridTile tile1, Pathfinder.GridTile tile2)
  {
    return (float) (Mathf.Abs(tile2.x - tile1.x) + Mathf.Abs(tile2.y - tile1.y));
  }

  public LinkedList<Pathfinder.Edge> FindPath(
    ZMyWorld map,
    ZCreature c,
    Pathfinder.Node start,
    Pathfinder.Node dest,
    Pathfinder.Boundaries boundaries = null,
    bool addEnd = true)
  {
    HashSet<Pathfinder.GridTile> gridTileSet = new HashSet<Pathfinder.GridTile>();
    Dictionary<Pathfinder.GridTile, Pathfinder.Edge> Parent = new Dictionary<Pathfinder.GridTile, Pathfinder.Edge>();
    Dictionary<Pathfinder.GridTile, float> dictionary = new Dictionary<Pathfinder.GridTile, float>();
    SimplePriorityQueue<Pathfinder.Node, float> simplePriorityQueue = new SimplePriorityQueue<Pathfinder.Node, float>();
    dictionary[start.pos] = 0.0f;
    simplePriorityQueue.Enqueue(start, Pathfinder.EuclidianDistance(start, dest));
    while (simplePriorityQueue.Count > 0)
    {
      Pathfinder.Node dest1 = simplePriorityQueue.Dequeue();
      if (dest1.pos.Equals((object) dest.pos))
        return Pathfinder.RebuildPath(map, Parent, dest1, dest, addEnd);
      gridTileSet.Add(dest1.pos);
      foreach (Pathfinder.Edge edge in dest1.edges)
      {
        if ((boundaries == null || !Pathfinder.IsOutOfGrid(edge.end.pos, boundaries)) && !gridTileSet.Contains(edge.end.pos) && (!edge.start.open(c) || !(edge.start.pos != dest.pos)))
        {
          float num1 = dictionary[dest1.pos] + edge.weight;
          float num2;
          if (!dictionary.TryGetValue(edge.end.pos, out num2) || (double) num1 < (double) num2)
          {
            Parent[edge.end.pos] = edge;
            dictionary[edge.end.pos] = num1;
            simplePriorityQueue.Enqueue(edge.end, num1 + Pathfinder.EuclidianDistance(edge.end, dest));
          }
        }
      }
    }
    return new LinkedList<Pathfinder.Edge>();
  }

  private static bool IsOutOfGrid(Pathfinder.GridTile pos, Pathfinder.Boundaries boundaries)
  {
    return pos.x < boundaries.Min.x || pos.x > boundaries.Max.x || pos.y < boundaries.Min.y || pos.y > boundaries.Max.y;
  }

  private static LinkedList<Pathfinder.Edge> RebuildPath(
    ZMyWorld map,
    Dictionary<Pathfinder.GridTile, Pathfinder.Edge> Parent,
    Pathfinder.Node dest,
    Pathfinder.Node end,
    bool addEnd)
  {
    LinkedList<Pathfinder.Edge> linkedList = new LinkedList<Pathfinder.Edge>();
    Pathfinder.GridTile pos = dest.pos;
    Pathfinder.Edge edge1 = (Pathfinder.Edge) null;
    if (addEnd)
    {
      foreach (Pathfinder.Edge edge2 in dest.edges)
      {
        if (edge2.start == end)
        {
          linkedList.AddFirst(edge2);
          break;
        }
      }
    }
    for (; Parent.TryGetValue(pos, out edge1); pos = edge1.start.pos)
    {
      if (linkedList.Count > 1 && linkedList.First.Next.Value.start.IsDiagonal(edge1.start))
      {
        int x1 = linkedList.First.Next.Value.start.pos.x;
        int x2 = edge1.start.pos.x;
        int y1 = linkedList.First.Next.Value.start.pos.y;
        int y2 = edge1.start.pos.y;
        if (true)
          linkedList.RemoveFirst();
      }
      linkedList.AddFirst(edge1);
    }
    if (linkedList != null && linkedList.Count > 1)
      linkedList.RemoveFirst();
    return linkedList;
  }

  public class Boundaries
  {
    public Pathfinder.GridTile Min { get; set; }

    public Pathfinder.GridTile Max { get; set; }
  }

  public class Edge
  {
    public Pathfinder.Node start;
    public Pathfinder.Node end;
    public Pathfinder.EdgeType type;
    public float weight;
    public LinkedList<Pathfinder.Edge> UnderlyingPath;
  }

  public enum EdgeType
  {
    INTRA,
    INTER,
  }

  public class GridTile
  {
    public int x;
    public int y;

    public Vector3 Position => new Vector3((float) this.x, 0.0f, (float) this.y);

    public Vector2Int PositionInt => new Vector2Int(this.x, this.y);

    public GridTile()
    {
    }

    public GridTile(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public GridTile(Vector3 position) => this.SetGridTile(position);

    public void SetGridTile(Vector3 position)
    {
      this.x = Mathf.FloorToInt(position.x);
      this.y = Mathf.FloorToInt(position.y);
    }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;
      Pathfinder.GridTile gridTile = obj as Pathfinder.GridTile;
      return this.x == gridTile.x && this.y == gridTile.y;
    }

    public override int GetHashCode()
    {
      return (17 * 29 + this.x.GetHashCode()) * 29 + this.y.GetHashCode();
    }

    public static bool operator !=(Pathfinder.GridTile o1, Pathfinder.GridTile o2)
    {
      return (object) o1 == null ? o2 != null : !o1.Equals((object) o2);
    }

    public static bool operator ==(Pathfinder.GridTile o1, Pathfinder.GridTile o2)
    {
      return (object) o1 == null ? (object) o2 == null : o1.Equals((object) o2);
    }
  }

  public class Node
  {
    public Pathfinder.GridTile pos;
    public List<Pathfinder.Edge> edges;

    public bool open(ZCreature c)
    {
      return !Client.map.CheckPosition(this.pos.x, this.pos.y, c, Inert.mask_entity_movement);
    }

    public bool IsDiagonal(Pathfinder.Node b)
    {
      return Mathf.Abs(this.pos.x - b.pos.x) == 1 && Mathf.Abs(this.pos.y - b.pos.y) == 1;
    }

    public int Distance(Pathfinder.Node b)
    {
      return Mathf.Abs(b.pos.x - this.pos.x) + Mathf.Abs(b.pos.y - this.pos.y);
    }

    public Node(Pathfinder.GridTile value)
    {
      this.pos = value;
      this.edges = new List<Pathfinder.Edge>();
    }
  }
}
