// Decompiled with JetBrains decompiler
// Type: ChessConsole.Pieces.King
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace ChessConsole.Pieces
{
  public class King : Piece
  {
    private Direction[] directions = new Direction[8];
    private bool canCastleLeft;
    private bool canCastleRight;

    public King(PlayerColor color)
      : base(color)
    {
      for (int index = 0; index < 8; ++index)
        this.directions[index] = (Direction) null;
    }

    public override IEnumerable<ChessBoard.Cell> PossibleMoves
    {
      get
      {
        King king = this;
        Direction[] directionArray = king.directions;
        for (int index = 0; index < directionArray.Length; ++index)
        {
          foreach (ChessBoard.Cell possibleMove in directionArray[index].GetPossibleMoves())
            yield return possibleMove;
          if (king.canCastleLeft && !king.Moved)
            yield return king.Parent.Parent.GetCell(2, king.Color == PlayerColor.White ? 0 : 7);
          if (king.canCastleRight && !king.Moved)
            yield return king.Parent.Parent.GetCell(6, king.Color == PlayerColor.White ? 0 : 7);
        }
        directionArray = (Direction[]) null;
      }
    }

    public override void Recalculate()
    {
      if (!this.Moved)
      {
        this.canCastleLeft = true;
        ChessBoard.Cell cell1 = this.Parent.Parent.GetCell(0, this.Color == PlayerColor.White ? 0 : 7);
        if (cell1.Piece == null || !(cell1.Piece is Rook) || cell1.Piece.Color != this.Color || cell1.Piece.Moved)
        {
          this.canCastleLeft = false;
        }
        else
        {
          for (int x = 1; x <= 3; ++x)
          {
            if (this.Parent.Parent.GetCell(x, this.Color == PlayerColor.White ? 0 : 7).Piece != null || this.Parent.Parent.WouldBeCheck(this.Color, this.Parent.Parent.GetCell(x, this.Color == PlayerColor.White ? 0 : 7)))
              this.canCastleLeft = false;
          }
        }
        this.canCastleRight = true;
        ChessBoard.Cell cell2 = this.Parent.Parent.GetCell(7, this.Color == PlayerColor.White ? 0 : 7);
        if (cell2.Piece == null || !(cell2.Piece is Rook) || cell2.Piece.Color != this.Color || cell2.Piece.Moved)
        {
          this.canCastleRight = false;
        }
        else
        {
          for (int x = 5; x <= 6; ++x)
          {
            if (this.Parent.Parent.GetCell(x, this.Color == PlayerColor.White ? 0 : 7).Piece != null || this.Parent.Parent.WouldBeCheck(this.Color, this.Parent.Parent.GetCell(x, this.Color == PlayerColor.White ? 0 : 7)))
              this.canCastleRight = false;
          }
        }
      }
      else
      {
        this.canCastleLeft = false;
        this.canCastleRight = false;
      }
      this.directions[0] = new Direction((Piece) this, 0, 1, 1);
      this.directions[1] = new Direction((Piece) this, 0, -1, 1);
      this.directions[2] = new Direction((Piece) this, -1, 0, 1);
      this.directions[3] = new Direction((Piece) this, 1, 0, 1);
      this.directions[4] = new Direction((Piece) this, -1, 1, 1);
      this.directions[5] = new Direction((Piece) this, 1, 1, 1);
      this.directions[6] = new Direction((Piece) this, -1, -1, 1);
      this.directions[7] = new Direction((Piece) this, 1, -1, 1);
    }

    public override bool IsBlockedIfMove(
      ChessBoard.Cell from,
      ChessBoard.Cell to,
      ChessBoard.Cell blocked)
    {
      foreach (Direction direction in this.directions)
      {
        if (!direction.IsBlockedIfMove(from, to, blocked))
          return false;
      }
      return true;
    }

    public override ChessPiece Char => ChessPiece.King;
  }
}
