// Decompiled with JetBrains decompiler
// Type: AIPathFinding
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using mattmc3.dotmore.Collections.Generic;
using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AIPathFinding
{
  private AIPathFinding.Tile[] tiles;
  internal int width;
  internal int height;
  internal int scale = 18;
  private SimplePriorityQueue<AIPathFinding.Tile, float> openSet = new SimplePriorityQueue<AIPathFinding.Tile, float>();
  private HashSet<AIPathFinding.Tile> closedSet = new HashSet<AIPathFinding.Tile>();
  private Dictionary<AIPathFinding.Tile, AIPathFinding.Tile> cameFrom = new Dictionary<AIPathFinding.Tile, AIPathFinding.Tile>();
  private SimplePriorityQueue<AIPathFinding.Tile, float> gScore = new SimplePriorityQueue<AIPathFinding.Tile, float>();

  public AIPathFinding.Tile GetClosestOpen(int x, int y)
  {
    x /= 18;
    y /= 18;
    if (x < 0)
      x = 0;
    if (y < 0)
      y = 0;
    if (x >= this.width)
      x = this.width - 1;
    if (y >= this.height)
      y = this.height - 1;
    AIPathFinding.Tile tile1 = this.tiles[y * this.width + x];
    if (!tile1.blocked)
      return tile1;
    int num1 = 0;
    while (num1 < 100)
    {
      ++num1;
      int num2 = Mathf.Min(this.width, x + num1);
      int num3 = Mathf.Min(this.height, y + num1);
      for (int index1 = Mathf.Max(0, y - num1); index1 < num3; ++index1)
      {
        for (int index2 = Mathf.Max(0, x - num1); index2 < num2; ++index2)
        {
          AIPathFinding.Tile tile2 = this.tiles[index1 * this.width + index2];
          if (!tile2.blocked)
            return tile2;
        }
      }
    }
    return (AIPathFinding.Tile) null;
  }

  public OrderedDictionary<AIPathFinding.Tile, float> A_Star(
    AIPathFinding.Tile start,
    AIPathFinding.Tile end)
  {
    this.openSet.Clear();
    this.closedSet.Clear();
    this.gScore.Clear();
    this.cameFrom.Clear();
    this.gScore.Enqueue(start, 0.0f);
    this.openSet.Enqueue(start, 0.0f);
    bool flag = false;
    while (this.openSet.Count > 0)
    {
      double priority1 = (double) this.openSet.GetPriority(this.openSet.First);
      AIPathFinding.Tile tile1 = this.openSet.Dequeue();
      if (tile1 == end)
      {
        if ((double) this.gScore.GetPriority(end) > 1000.0)
        {
          this.closedSet.Add(tile1);
          flag = true;
        }
        else
        {
          AIPathFinding.Tile key = end;
          OrderedDictionary<AIPathFinding.Tile, float> orderedDictionary = new OrderedDictionary<AIPathFinding.Tile, float>();
          orderedDictionary.Add(end, this.gScore.GetPriority(end));
          while (this.cameFrom.ContainsKey(key))
          {
            key = this.cameFrom[key];
            orderedDictionary.Add(key, this.gScore.GetPriority(key));
          }
          return orderedDictionary;
        }
      }
      else
      {
        this.closedSet.Add(tile1);
        int num1 = tile1.index % this.width;
        int num2 = tile1.index / this.width;
        for (int index = 0; index < 8; ++index)
        {
          AIPathFinding.Tile tile2 = (AIPathFinding.Tile) null;
          if (index == 0)
          {
            if (num1 + 1 < this.width)
              tile2 = this.tiles[tile1.index + 1];
          }
          else if (index == 1)
          {
            if (num1 - 1 >= 0)
              tile2 = this.tiles[tile1.index - 1];
          }
          else if (index == 2)
          {
            if (num2 + 1 < this.height)
              tile2 = this.tiles[tile1.index + this.width];
          }
          else if (index == 3)
          {
            if (num2 - 1 >= 0)
              tile2 = this.tiles[tile1.index - this.width];
          }
          else if (index == 4)
          {
            int num3 = num2 - 1;
            int num4 = num1 + 1;
            if (num3 >= 0 && num4 < this.width)
              tile2 = this.tiles[tile1.index - this.width + 1];
          }
          else if (index == 5)
          {
            int num5 = num1 + 1;
            int num6 = num2 + 1;
            int width = this.width;
            if (num5 < width && num6 < this.height)
              tile2 = this.tiles[tile1.index + 1 + this.width];
          }
          else if (index == 6)
          {
            int num7 = num1 - 1;
            int num8 = num2 - 1;
            if (num7 >= 0 && num8 >= 0)
              tile2 = this.tiles[tile1.index - 1 - this.width];
          }
          else
          {
            int num9 = num2 + 1;
            int num10 = num1 - 1;
            int height = this.height;
            if (num9 < height && num10 >= 0)
              tile2 = this.tiles[tile1.index + this.width - 1];
          }
          if (tile2 != null && !tile2.blocked)
          {
            float num11 = (float) AIPathFinding.distance(start, tile2);
            float priority2 = this.gScore.GetPriority(tile1) + num11;
            if (this.closedSet.Contains(tile2))
            {
              if ((double) priority2 < (double) this.gScore.GetPriority(tile2))
              {
                this.gScore.UpdatePriority(tile2, priority2);
                if (!this.openSet.Contains(tile2))
                  this.openSet.Enqueue(tile2, priority2);
                this.cameFrom[tile2] = tile1;
              }
            }
            else
            {
              if (!this.openSet.Contains(tile2))
                this.openSet.Enqueue(tile2, priority2);
              else if ((double) priority2 >= (double) this.gScore.GetPriority(tile2))
                continue;
              this.cameFrom[tile2] = tile1;
              if (this.gScore.Contains(tile2))
                this.gScore.UpdatePriority(tile2, priority2);
              else
                this.gScore.Enqueue(tile2, priority2);
            }
          }
        }
      }
    }
    if (!flag)
      return (OrderedDictionary<AIPathFinding.Tile, float>) null;
    AIPathFinding.Tile key1 = end;
    OrderedDictionary<AIPathFinding.Tile, float> orderedDictionary1 = new OrderedDictionary<AIPathFinding.Tile, float>();
    orderedDictionary1.Add(end, this.gScore.GetPriority(end));
    while (this.cameFrom.ContainsKey(key1))
    {
      key1 = this.cameFrom[key1];
      orderedDictionary1.Add(key1, this.gScore.GetPriority(key1));
    }
    return orderedDictionary1;
  }

  public static int distance(AIPathFinding.Tile a, AIPathFinding.Tile b)
  {
    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
  }

  public class Tile
  {
    public int index;
    public bool blocked;
    public int x;
    public int y;
    public int xPos;
    public int yPos;
  }
}
