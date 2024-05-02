

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace ChessConsole
{
  public abstract class Piece
  {
    public RectTransform image;
    private ChessBoard.Cell _parent;

    public PlayerColor Color { private set; get; }

    public bool Moved { protected set; get; }

    public bool SetMoved
    {
      set => this.Moved = value;
    }

    public abstract IEnumerable<ChessBoard.Cell> PossibleMoves { get; }

    public List<ChessBoard.Cell> LegalMoves { private set; get; }

    public ChessBoard.Cell Parent
    {
      private set
      {
        this._parent = value;
        if (!((Object) this.image != (Object) null) || this.Parent == null)
          return;
        this.image.anchoredPosition = ChessUI.Instance.GetAnchoredPositionAllowBig(this.Parent);
        float z1 = this.image.localPosition.z;
        float z2 = (float) (this.Parent.X + this.Parent.Y * 8);
        this.image.localPosition = new Vector3(this.image.localPosition.x, this.image.localPosition.y, z2);
        for (int index1 = 0; index1 < this.image.parent.childCount; ++index1)
        {
          if ((Object) this.image.parent.GetChild(index1) == (Object) this.image)
          {
            if ((double) z2 > (double) z1)
            {
              for (int index2 = index1 - 1; index2 >= 0 && (double) this.image.parent.GetChild(index2).localPosition.z < (double) z2; --index2)
                this.image.SetSiblingIndex(index2);
              break;
            }
            for (int index3 = index1 + 1; index3 < this.image.parent.childCount && (double) this.image.parent.GetChild(index3).localPosition.z > (double) z2; ++index3)
              this.image.SetSiblingIndex(index3);
            break;
          }
        }
      }
      get => this._parent;
    }

    public void Destroy()
    {
      if (!((Object) this.image != (Object) null))
        return;
      Object.Destroy((Object) this.image.gameObject);
    }

    public Piece(PlayerColor color)
    {
      this.Color = color;
      this.Moved = false;
      this.LegalMoves = new List<ChessBoard.Cell>();
    }

    public void OnPlace(ChessBoard.Cell cell) => this.Parent = cell;

    public void OnMove(ChessBoard.Cell cell)
    {
      this.Parent = cell;
      this.Moved = true;
    }

    public abstract void Recalculate();

    public abstract bool IsBlockedIfMove(
      ChessBoard.Cell from,
      ChessBoard.Cell to,
      ChessBoard.Cell blocked);

    public abstract ChessPiece Char { get; }

    protected virtual bool canHit(ChessBoard.Cell cell)
    {
      return cell != null && cell.Piece != null && cell.Piece.Color != this.Color;
    }
  }
}
