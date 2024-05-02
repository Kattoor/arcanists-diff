// Decompiled with JetBrains decompiler
// Type: ChessConsole.Direction
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace ChessConsole
{
  public class Direction
  {
    private List<ChessBoard.Cell> possibleMoves;
    private bool updateHitGraph;

    public Piece Piece { private set; get; }

    public int X { private set; get; }

    public int Y { private set; get; }

    public IEnumerable<ChessBoard.Cell> GetPossibleMoves(bool enemyHittable = true)
    {
      if (this.possibleMoves.Count != 0)
      {
        for (int i = 0; i < this.possibleMoves.Count - 1; ++i)
          yield return this.possibleMoves[i];
        if (this.possibleMoves.Last<ChessBoard.Cell>().Piece == null)
          yield return this.possibleMoves.Last<ChessBoard.Cell>();
        else if (enemyHittable && this.possibleMoves.Last<ChessBoard.Cell>().Piece.Color != this.Piece.Color)
          yield return this.possibleMoves.Last<ChessBoard.Cell>();
      }
    }

    public int GetPossibleMoveCount(bool enemyHittable = true)
    {
      if (this.possibleMoves.Count == 0)
        return 0;
      return this.possibleMoves.Last<ChessBoard.Cell>().Piece == null || enemyHittable && this.possibleMoves.Last<ChessBoard.Cell>().Piece.Color != this.Piece.Color ? this.possibleMoves.Count : this.possibleMoves.Count - 1;
    }

    public int DesiredCount { private set; get; }

    public Direction(Piece piece, int x, int y, int desiredCount = 8, bool updateHitGraph = true)
    {
      this.Piece = piece;
      this.X = x;
      this.Y = y;
      this.DesiredCount = desiredCount;
      this.updateHitGraph = updateHitGraph;
      this.possibleMoves = new List<ChessBoard.Cell>();
      this.possibleMoves.AddRange(piece.Parent.OpenLineOfSight(piece, x, y, desiredCount));
      foreach (ChessBoard.Cell possibleMove in this.possibleMoves)
      {
        if (updateHitGraph)
          possibleMove.HitBy.Add(this.Piece);
      }
    }

    public bool IsBlockedIfMove(ChessBoard.Cell from, ChessBoard.Cell to, ChessBoard.Cell blocked)
    {
      if (this.possibleMoves.Contains(blocked) && !this.possibleMoves.Contains(to))
        return false;
      if (this.possibleMoves.Contains(from))
      {
        int num = this.possibleMoves.IndexOf(to);
        if (0 <= num && num < this.possibleMoves.Count - 1)
          return true;
        foreach (ChessBoard.Cell cell in from.OpenLineOfSight(from.Piece, this.X, this.Y, this.DesiredCount - this.possibleMoves.Count))
        {
          if (cell == to)
            return true;
          if (cell == blocked)
            return false;
        }
      }
      return true;
    }
  }
}
