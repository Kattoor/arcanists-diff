// Decompiled with JetBrains decompiler
// Type: AStarWithGravity
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AStarWithGravity : MonoBehaviour
{
  public AStarWithGravity.Grid grid;
  public Vector2 unitSize;
  public Vector2 gravity;

  private void Start() => this.grid.SetGravity(this.gravity);

  public List<AStarWithGravity.Node> GetPath(Vector3 startPos, Vector3 targetPos)
  {
    List<AStarWithGravity.Node> nodeList = new List<AStarWithGravity.Node>();
    HashSet<AStarWithGravity.Node> nodeSet = new HashSet<AStarWithGravity.Node>();
    nodeList.Add(this.grid.NodeFromWorldPoint(startPos, this.unitSize));
    while (nodeList.Count > 0)
    {
      AStarWithGravity.Node node1 = nodeList[0];
      for (int index = 1; index < nodeList.Count; ++index)
      {
        if (nodeList[index].fCost < node1.fCost || nodeList[index].fCost == node1.fCost && nodeList[index].hCost < node1.hCost)
          node1 = nodeList[index];
      }
      nodeList.Remove(node1);
      nodeSet.Add(node1);
      if (node1 == this.grid.NodeFromWorldPoint(targetPos, this.unitSize))
        return this.RetracePath(startPos, targetPos);
      foreach (AStarWithGravity.Node node2 in this.grid.GetNeighboursWithGravity(node1, this.unitSize))
      {
        if (!nodeSet.Contains(node2))
        {
          int num = node1.gCost + this.GetDistance(node1, node2);
          if (num < node2.gCost || !nodeList.Contains(node2))
          {
            node2.gCost = num;
            node2.hCost = this.GetDistance(node2, this.grid.NodeFromWorldPoint(targetPos, this.unitSize));
            node2.parent = node1;
            if (!nodeList.Contains(node2))
              nodeList.Add(node2);
          }
        }
      }
    }
    return (List<AStarWithGravity.Node>) null;
  }

  private List<AStarWithGravity.Node> RetracePath(Vector3 startPos, Vector3 endPos)
  {
    List<AStarWithGravity.Node> nodeList = new List<AStarWithGravity.Node>();
    for (AStarWithGravity.Node node = this.grid.NodeFromWorldPoint(endPos, this.unitSize); node != this.grid.NodeFromWorldPoint(startPos, this.unitSize); node = node.parent)
      nodeList.Add(node);
    nodeList.Reverse();
    return nodeList;
  }

  private int GetDistance(AStarWithGravity.Node nodeA, AStarWithGravity.Node nodeB)
  {
    int num1 = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    int num2 = Mathf.Abs(nodeA.gridY - nodeB.gridY);
    return num1 > num2 ? 14 * num2 + 10 * (num1 - num2) : 14 * num1 + 10 * (num2 - num1);
  }

  public class Grid
  {
    public AStarWithGravity.Node[,] nodes;
    public Vector2 gravity;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    public void SetGravity(Vector2 v) => this.gravity = v;

    public int x => this.nodes.GetLength(0);

    public int y => this.nodes.GetLength(1);

    public AStarWithGravity.Node NodeFromWorldPoint(Vector3 worldPos, Vector2 unitSize)
    {
      double num1 = ((double) worldPos.x + (double) this.gridWorldSize.x / 2.0) / (double) this.gridWorldSize.x;
      float num2 = (worldPos.y + this.gridWorldSize.y / 2f) / this.gridWorldSize.y;
      double num3 = (double) Mathf.Clamp01((float) num1);
      float num4 = Mathf.Clamp01(num2);
      double num5 = (double) this.nodes.GetLength(0) - (double) unitSize.x / (double) this.nodeRadius;
      return this.nodes[(int) (num3 * num5), (int) ((double) num4 * ((double) this.nodes.GetLength(1) - (double) unitSize.y / (double) this.nodeRadius))];
    }

    public List<AStarWithGravity.Node> GetNeighbours(
      AStarWithGravity.Node currentNode,
      float jumpHeight)
    {
      List<AStarWithGravity.Node> neighbours = new List<AStarWithGravity.Node>();
      Vector3 worldPosition = (Vector3) currentNode.worldPosition;
      for (int index1 = -1; index1 <= 1; ++index1)
      {
        for (int index2 = -1; index2 <= 1; ++index2)
        {
          if (index1 != 0 || index2 != 0)
          {
            worldPosition.x = (float) (currentNode.gridX + index1);
            worldPosition.y = (float) (currentNode.gridY + index2);
            if ((double) worldPosition.x >= 0.0 && (double) worldPosition.x < (double) index1 && (double) worldPosition.y >= 0.0 && (double) worldPosition.y < (double) index2)
            {
              if ((bool) (Object) Physics2D.OverlapCircle((Vector2) worldPosition, this.nodeRadius))
              {
                AStarWithGravity.Node node = this.nodes[(int) worldPosition.x, (int) worldPosition.y];
                neighbours.Add(node);
              }
              else
              {
                worldPosition.y += jumpHeight;
                if ((bool) (Object) Physics2D.OverlapCircle((Vector2) worldPosition, this.nodeRadius))
                {
                  AStarWithGravity.Node node = this.nodes[(int) worldPosition.x, (int) worldPosition.y];
                  neighbours.Add(node);
                }
              }
            }
          }
        }
      }
      return neighbours;
    }

    public List<AStarWithGravity.Node> GetNeighboursWithGravity(
      AStarWithGravity.Node currentNode,
      Vector2 unitSize)
    {
      List<AStarWithGravity.Node> neighboursWithGravity = new List<AStarWithGravity.Node>();
      for (int index1 = -1; index1 <= 1; ++index1)
      {
        for (int index2 = -1; index2 <= 1; ++index2)
        {
          if (index1 != 0 || index2 != 0)
          {
            int index3 = currentNode.gridX + index1;
            int index4 = currentNode.gridY + index2;
            if (index3 >= 0 && (double) index3 < (double) this.gridWorldSize.x && index4 >= 0 && (double) index4 < (double) this.gridWorldSize.y && this.CanMoveToNode(currentNode, this.nodes[index3, index4], unitSize))
              neighboursWithGravity.Add(this.nodes[index3, index4]);
          }
        }
      }
      int x = (int) this.gravity.x;
      int y = (int) this.gravity.y;
      int index5 = currentNode.gridX + x;
      int index6 = currentNode.gridY + y;
      if (index5 >= 0 && (double) index5 < (double) this.gridWorldSize.x && index6 >= 0 && (double) index6 < (double) this.gridWorldSize.y && this.CanMoveToNode(currentNode, this.nodes[index5, index6], unitSize))
        neighboursWithGravity.Add(this.nodes[index5, index6]);
      return neighboursWithGravity;
    }

    private bool CanMoveToNode(
      AStarWithGravity.Node currentNode,
      AStarWithGravity.Node nextNode,
      Vector2 unitSize)
    {
      return nextNode.walkable && !this.IsBlockedBySize(currentNode, nextNode, unitSize);
    }

    private bool IsBlockedBySize(
      AStarWithGravity.Node currentNode,
      AStarWithGravity.Node nextNode,
      Vector2 unitSize)
    {
      Vector2 worldPosition = nextNode.worldPosition;
      for (float x = worldPosition.x - unitSize.x / 2f; (double) x <= (double) worldPosition.x + (double) unitSize.x / 2.0; x += this.nodeRadius)
      {
        for (float y = worldPosition.y - unitSize.y / 2f; (double) y <= (double) worldPosition.y + (double) unitSize.y / 2.0; y += this.nodeRadius)
        {
          if (!this.NodeFromWorldPoint(new Vector2(x, y)).walkable)
            return true;
        }
      }
      return false;
    }

    public AStarWithGravity.Node NodeFromWorldPoint(Vector2 pos)
    {
      return this.nodes[(int) pos.x, (int) pos.y];
    }
  }

  public class Node
  {
    public AStarWithGravity.Node parent;
    public int fCost;
    public int hCost;
    public int gCost;
    public int gridX;
    public int gridY;
    public bool walkable;

    public Vector2 worldPosition => new Vector2((float) this.gridX, (float) this.gridY);
  }

  public class AStar : MonoBehaviour
  {
    public LayerMask platformLayer;
    public float jumpHeight = 2f;
    public float moveSpeed = 2f;
    private AStarWithGravity.Grid grid;
    private Vector3 startPos;
    private Vector3 targetPos;

    private void Start() => this.grid = this.GetComponent<AStarWithGravity.Grid>();

    public List<AStarWithGravity.Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
      this.startPos = startPos;
      this.targetPos = targetPos;
      AStarWithGravity.Node startNode = this.grid.NodeFromWorldPoint((Vector2) startPos);
      AStarWithGravity.Node node1 = this.grid.NodeFromWorldPoint((Vector2) targetPos);
      List<AStarWithGravity.Node> nodeList = new List<AStarWithGravity.Node>();
      HashSet<AStarWithGravity.Node> nodeSet = new HashSet<AStarWithGravity.Node>();
      nodeList.Add(startNode);
      while (nodeList.Count > 0)
      {
        AStarWithGravity.Node node2 = nodeList[0];
        for (int index = 1; index < nodeList.Count; ++index)
        {
          if (nodeList[index].fCost < node2.fCost || nodeList[index].fCost == node2.fCost && nodeList[index].hCost < node2.hCost)
            node2 = nodeList[index];
        }
        nodeList.Remove(node2);
        nodeSet.Add(node2);
        if (node2 == node1)
          return this.RetracePath(startNode, node1);
        foreach (AStarWithGravity.Node neighbour in this.grid.GetNeighbours(node2, this.jumpHeight))
        {
          if (!nodeSet.Contains(neighbour))
          {
            int num = node2.gCost + this.GetDistance(node2, neighbour);
            if (num < neighbour.gCost || !nodeList.Contains(neighbour))
            {
              neighbour.gCost = num;
              neighbour.hCost = this.GetDistance(neighbour, node1);
              neighbour.parent = node2;
              if (!nodeList.Contains(neighbour))
                nodeList.Add(neighbour);
            }
          }
        }
      }
      return (List<AStarWithGravity.Node>) null;
    }

    private List<AStarWithGravity.Node> RetracePath(
      AStarWithGravity.Node startNode,
      AStarWithGravity.Node endNode)
    {
      List<AStarWithGravity.Node> nodeList = new List<AStarWithGravity.Node>();
      for (AStarWithGravity.Node node = endNode; node != startNode; node = node.parent)
        nodeList.Add(node);
      nodeList.Reverse();
      return nodeList;
    }

    private int GetDistance(AStarWithGravity.Node nodeA, AStarWithGravity.Node nodeB)
    {
      int num1 = Mathf.Abs(nodeA.gridX - nodeB.gridX);
      int num2 = Mathf.Abs(nodeA.gridY - nodeB.gridY);
      return num1 > num2 ? 14 * num2 + 10 * (num1 - num2) : 14 * num1 + 10 * (num2 - num1);
    }
  }
}
