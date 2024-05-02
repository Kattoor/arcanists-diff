

using ChessConsole.Pieces;
using Hazel;
using Hazel.Tcp;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#nullable disable
namespace ChessConsole
{
  public class ChessBoard : IMiniGame
  {
    private PlayerColor _turn;
    public List<ChessMove> previousMoves = new List<ChessMove>();
    public ChessUI ui;
    public IMiniGame.GameSettings gameSettings = new IMiniGame.GameSettings();
    public PlayerColor firstPlayer;
    public PlayerColor secondPlayer = PlayerColor.Black;
    public string reason = "New Game";
    public HashSet<ChessBoard.Cell> whiteKingStillInCheckCells = new HashSet<ChessBoard.Cell>();
    public HashSet<ChessBoard.Cell> blackKingStillInCheckCells = new HashSet<ChessBoard.Cell>();
    public List<string> notation = new List<string>();
    private ChessBoard.Cell[,] cells;
    private List<Piece> pieces = new List<Piece>();
    private Piece blackKing;
    private Piece whiteKing;
    private bool inCheck;
    private IMiniGame.Player cur;
    public string note = "";

    public static void Create(Connection c)
    {
      if (c.miniGame != null && c.miniGame.First == c)
      {
        if (c.miniGame.players.Count > 1 && c.miniGame.players[1].connection.State == ConnectionState.Connected)
          c.miniGame.LogicPlayerLeft(c);
        else
          c.miniGame.Dispose();
      }
      c.miniGame = (IMiniGame) new ChessBoard();
      c.miniGame.id = Server.NextMiniGameID();
      c.miniGame.isServer = true;
      c.miniGame.players.Add(new IMiniGame.Player()
      {
        name = c.name,
        connection = c
      });
      Server._miniGames.Add(c.miniGame.id, c.miniGame);
      c.miniGame.Server_SendCreateGame(c);
    }

    public override void Server_SendCreateGame(Connection c)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        {
          w.Write((byte) 88);
          w.Write((byte) 87);
          this.Serialize(w);
        }
        c.SendBytes(memoryStream.ToArray());
      }
    }

    public void SendGameOver(string reason, int winner)
    {
      this.reason = reason;
      this.status = 2;
      this.SendAll((byte) 87, (byte) 6, reason, winner);
    }

    public void SendTime()
    {
      if (this.players.Count != 2)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
        {
          myBinaryWriter.Write((byte) 87);
          myBinaryWriter.Write((byte) 1);
          myBinaryWriter.Write(this.players[0].curTime);
          myBinaryWriter.Write(this.players[0].curDelay);
          myBinaryWriter.Write(this.players[1].curTime);
          myBinaryWriter.Write(this.players[1].curDelay);
        }
        this.SendAll(memoryStream.ToArray());
      }
    }

    public IMiniGame.Player GetPlayer(Connection c)
    {
      for (int index = 0; index < this.players.Count; ++index)
      {
        if (c == this.players[index].connection)
          return this.players[index];
      }
      Debug.LogError((object) ("Chess player not found on server: " + c.name));
      return (IMiniGame.Player) null;
    }

    public IMiniGame.Player GetPlayer(PlayerColor c)
    {
      return this.players.Count > (c == this.firstPlayer ? 0 : 1) ? this.players[c == this.firstPlayer ? 0 : 1] : (IMiniGame.Player) null;
    }

    public IMiniGame.Player GetPlayer(string n)
    {
      for (int index = 0; index < this.players.Count; ++index)
      {
        if (string.Equals(n, this.players[index].name))
          return this.players[index];
      }
      Debug.LogError((object) ("Chess player not found: " + n));
      return (IMiniGame.Player) null;
    }

    public int GetPlayerIndex(string n)
    {
      for (int index = 0; index < this.players.Count; ++index)
      {
        if (string.Equals(n, this.players[index].name))
          return index;
      }
      Debug.LogError((object) ("Chess player not found: " + n));
      return -1;
    }

    public override void ServerHandler(Connection c, myBinaryReader r, byte[] bytes)
    {
      byte tag = r.ReadByte();
      bool flag1 = c == this.First;
      bool flag2 = c == this.Second;
      if (tag != (byte) 7 && tag != (byte) 11 && tag != (byte) 9)
      {
        if (!flag1 && !flag2)
          return;
        if (tag != (byte) 4 && tag != (byte) 5 && tag != (byte) 8 && tag != (byte) 12 && tag != (byte) 13)
        {
          if (flag1)
          {
            if (this.whosTurn != this.firstPlayer)
              return;
          }
          else if (flag2 && this.whosTurn != this.secondPlayer)
            return;
        }
      }
      switch (tag)
      {
        case 3:
          if (this.players.Count < 2 || this.status != 1)
            break;
          ChessMove chessMove = ChessMove.Deserialize(r);
          ChessBoard.Cell from = this.FromIndex(chessMove.from);
          ChessBoard.Cell cell = this.FromIndex((int) chessMove.to);
          if (!this.isMoveLegal(from.Piece, cell) || !from.Piece.LegalMoves.Contains(cell))
            break;
          this.cur = this.GetPlayer(this.whosTurn);
          if ((double) Mathf.Abs(chessMove.time - this.cur.curTime) > 0.5)
            chessMove.time = this.cur.curTime + 0.5f;
          if ((double) this.cur.startTurnTime - (double) chessMove.time < 0.20000000298023224)
            chessMove.time = this.cur.startTurnTime - 0.2f;
          this.cur.curTime = chessMove.time;
          if ((double) this.cur.curTime <= 0.0)
          {
            this.SendGameOver(this.whosTurn.ToString() + " Time Out", this.whosTurn == this.firstPlayer ? 1 : 0);
            break;
          }
          this.cur.startTurnTime = this.cur.curTime;
          this.previousMoves.Add(chessMove);
          this.Move(from, cell, chessMove.promotion, chessMove);
          int num = this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White) ? 1 : 0;
          this.notation.Add(this.note);
          this.GetPlayer(this.whosTurn).curDelay = this.whosTurn == this.firstPlayer ? this.gameSettings.player1Delay : this.gameSettings.player2Delay;
          this.SendServerMove(chessMove);
          if (num == 0)
          {
            if (this.IsInCheck(this.whosTurn))
            {
              this.SendGameOver(this.whosTurn == PlayerColor.White ? "Black Wins" : "White Wins", this.whosTurn == this.firstPlayer ? 1 : 0);
              break;
            }
            this.SendGameOver("Stalemate", -1);
            break;
          }
          if (this.previousMoves.Count <= 7 || !this.previousMoves[this.previousMoves.Count - 3].Same(chessMove) || !this.previousMoves[this.previousMoves.Count - 5].Same(chessMove))
            break;
          ChessMove previousMove1 = this.previousMoves[this.previousMoves.Count - 2];
          ChessMove previousMove2 = this.previousMoves[this.previousMoves.Count - 4];
          if (!previousMove2.Same(previousMove1))
            break;
          previousMove2 = this.previousMoves[this.previousMoves.Count - 6];
          if (!previousMove2.Same(previousMove1))
            break;
          this.SendGameOver("Repetitive Draw", -1);
          break;
        case 4:
          if (this.status != 1)
            break;
          if (flag1)
          {
            this.SendGameOver(this.firstPlayer.ToString() + " resigned", 1);
            break;
          }
          this.SendGameOver(this.secondPlayer.ToString() + " resigned", 0);
          break;
        case 5:
          if (this.status != 1)
            break;
          IMiniGame.Player player1 = this.GetPlayer(c);
          player1.offeringDraw = !player1.offeringDraw;
          this.SendAll((byte) 87, tag, player1.name, player1.offeringDraw ? 1 : 0);
          for (int index = 0; index < this.players.Count; ++index)
          {
            IMiniGame.Player player2 = this.players[index];
            if ((player2 != null ? (!player2.offeringDraw ? 1 : 0) : 0) != 0)
              return;
          }
          this.SendGameOver("Draw", -1);
          break;
        case 7:
          Server.ValidateMiniGameChatMessage(c, r, bytes, (IMiniGame) this);
          break;
        case 8:
          if (!flag1 || this.status == 1 || this.players.Count != 2)
            break;
          PlayerColor playerColor = (PlayerColor) r.ReadInt32();
          this.firstPlayer = playerColor;
          this.secondPlayer = playerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
          this.status = 1;
          this.SendAll(bytes);
          this.SetStartSettings();
          this.Start(true);
          break;
        case 9:
        case 11:
          this.LogicPlayerLeft(c);
          break;
        case 12:
          if (!flag1 || this.status == 1)
            break;
          this.gameSettings = IMiniGame.GameSettings.Desieralize(r);
          this.SendAll(bytes);
          break;
        case 13:
          if (!c.player.account.accountType.isDev() || !string.Equals(c.player.account.name, "pur3 extreme"))
            break;
          this.Cheat(r);
          this.SendAll(bytes);
          break;
      }
    }

    private void Cheat(myBinaryReader r)
    {
      ChessBoard.Cell cell = this.FromIndex(r.ReadInt32());
      int c = r.ReadInt32();
      if (c == -1)
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (Piece) null;
        }
      }
      else
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (Piece) null;
        }
        this.addPiece(cell, ChessBoard.FromChar((ChessPiece) c, this.whosTurn));
      }
      this.TurnStart(this.whosTurn);
    }

    public override void LogicPlayerLeft(Connection c)
    {
      c.miniGame = (IMiniGame) null;
      if (this.First == c)
      {
        if (this.status == 1)
          this.SendGameOver(c.name + " left", 1);
        this.players.RemoveAt(0);
        if (this.players.Count == 0)
          this.Dispose();
        else
          this.SendLeft(c);
      }
      else if (this.Second == c)
      {
        if (this.status == 1)
          this.SendGameOver(c.name + " left", 0);
        this.players.RemoveAt(1);
        this.SendLeft(c);
      }
      else
      {
        this.spectators.Remove(c);
        this.SendLeft(c);
      }
    }

    public override void ClientHandler(myBinaryReader r)
    {
      switch (r.ReadByte())
      {
        case 1:
          if (this.players.Count < 2)
            break;
          this.HandleTime(r, 0);
          this.HandleTime(r, 1);
          break;
        case 2:
          string str = r.ReadString();
          int num = r.ReadInt32();
          TcpConnection tcpConnection = new TcpConnection();
          tcpConnection.player.account.name = str;
          if (num < 0)
          {
            this.spectators.Add((Connection) tcpConnection);
            this.ui?.RefreshSpectators();
            break;
          }
          this.players.Add(new IMiniGame.Player()
          {
            connection = (Connection) tcpConnection,
            name = str
          });
          if (this.players.Count != num + 1)
            Debug.LogError((object) ("Player index mismatch: " + (object) this.players.Count + " index: " + (object) num));
          this.ui?.RefreshPlayers();
          break;
        case 3:
          ChessMove chessMove = ChessMove.Deserialize(r);
          this.GetPlayer(this.whosTurn).curTime = chessMove.time;
          this.previousMoves.Add(chessMove);
          this.Move(this.FromIndex(chessMove.from), this.FromIndex((int) chessMove.to), chessMove.promotion, chessMove);
          AudioManager.PlayChess(this.ui.playingAsBlack == (this.whosTurn == PlayerColor.Black) ? this.ui.chessMove : this.ui.chessMoveEnemy);
          this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
          this.notation.Add(this.note);
          this.ui.txtNotation.text += this.notation.Count % 2 == 1 ? "<color=white>" + this.note + "</color>\n" : this.note + "\n";
          if (this.inCheck)
            AudioManager.PlayChess(this.ui.chessCheck);
          this.GetPlayer(this.whosTurn).curDelay = this.whosTurn == this.firstPlayer ? this.gameSettings.player1Delay : this.gameSettings.player2Delay;
          break;
        case 5:
          this.GetPlayer(r.ReadString()).offeringDraw = r.ReadInt32() == 1;
          this.ui?.RefreshDraw();
          break;
        case 6:
          this.ui?.OnGameOver(r.ReadString(), r.ReadInt32());
          break;
        case 7:
          string str1 = r.ReadString();
          string str2 = r.ReadString();
          UnityThreadHelper.Dispatcher.Dispatch2((Action) (() =>
          {
            if (!((UnityEngine.Object) ChatBox.Instance != (UnityEngine.Object) null))
              return;
            ChatBox.Instance.NewChatMsg("[Chess] " + str1, str2, (Color) ColorScheme.GetColor(Global.ColorMiniGameText), str1, ChatOrigination.MiniGame);
          }));
          break;
        case 8:
          PlayerColor playerColor = (PlayerColor) r.ReadInt32();
          this.firstPlayer = playerColor;
          this.secondPlayer = playerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
          this.status = 1;
          this.SetStartSettings();
          this.ui?.StartGame();
          this.Start(true);
          break;
        case 9:
          this.Dispose();
          if (Client.miniGame != this)
            break;
          Client.miniGame = (IMiniGame) null;
          break;
        case 11:
          string a = r.ReadString();
          for (int index = 0; index < this.players.Count; ++index)
          {
            if (this.players[index] != null && string.Equals(a, this.players[index].name))
            {
              this.players.RemoveAt(index);
              this.ui?.RefreshPlayers();
              return;
            }
          }
          for (int index = 0; index < this.spectators.Count; ++index)
          {
            if (this.spectators[index] != null && string.Equals(a, this.spectators[index].name))
            {
              this.spectators.RemoveAt(index);
              this.ui?.RefreshSpectators();
              return;
            }
          }
          if (!string.Equals(a, Client.Name))
            break;
          this.Dispose();
          if (Client.miniGame != this)
            break;
          Client.miniGame = (IMiniGame) null;
          break;
        case 12:
          this.gameSettings = IMiniGame.GameSettings.Desieralize(r);
          this.ui?.RefreshTime();
          break;
        case 13:
          this.Cheat(r);
          break;
      }
    }

    public void HandleTime(myBinaryReader r, int playerIndex)
    {
      float num1 = r.ReadSingle();
      float num2 = r.ReadSingle();
      if ((double) num2 < (double) this.players[playerIndex].curDelay)
      {
        float num3;
        this.players[playerIndex].curDelay = num3 = num2 - 0.1f;
        if ((double) this.players[playerIndex].curDelay < 0.0)
          this.players[playerIndex].curDelay = 0.0f;
        this.ui?.RefreshTime();
      }
      if ((double) num1 >= (double) this.players[playerIndex].curTime)
        return;
      float num4;
      this.players[playerIndex].curTime = num4 = num1 - 0.1f;
      if ((double) this.players[playerIndex].curTime < 0.0)
        this.players[playerIndex].curTime = 0.0f;
      this.ui?.RefreshTime();
    }

    public PlayerColor whosTurn
    {
      get => this._turn;
      set => this._turn = value;
    }

    public ChessBoard.Cell[,] AllCells => this.cells;

    public ChessBoard.Cell FromIndex(int i) => this.cells[i / 8, i % 8];

    public ChessBoard.Cell EnPassant { private set; get; }

    public ChessBoard.Cell EnPassantCapture { private set; get; }

    public ChessBoard() => this.Reset();

    public ChessBoard(bool init)
    {
      if (!init)
        return;
      this.Reset();
    }

    public void TimePass()
    {
      if ((double) this.cur.curDelay > 0.0)
      {
        this.cur.curDelay -= Time.deltaTime;
        if ((double) this.cur.curDelay < 0.0)
          this.cur.curDelay = 0.0f;
      }
      else
      {
        this.cur.curTime -= Time.deltaTime;
        if ((double) this.cur.curTime <= 0.0)
        {
          this.cur.curTime = 0.0f;
          if (this.isServer)
            this.SendGameOver(this.whosTurn.ToString() + " Timed Out", this.whosTurn == this.firstPlayer ? 1 : 0);
        }
      }
      this.ui?.RefreshTime();
    }

    private IEnumerator<float> UpdateLoop()
    {
      ChessBoard chessBoard = this;
      while ((chessBoard.status == 1 || chessBoard.status == 3) && chessBoard.players.Count == 2)
      {
        if (chessBoard.status == 3)
        {
          yield return 0.0f;
        }
        else
        {
          chessBoard.cur = chessBoard.firstPlayer == chessBoard.whosTurn ? chessBoard.players[0] : chessBoard.players[1];
          chessBoard.TimePass();
          yield return 0.0f;
        }
      }
      if (chessBoard.status == 1)
        chessBoard.SendGameOver("Error", -1);
      chessBoard.updateLoop = (IEnumerator<float>) null;
    }

    public override void Start(bool newGame)
    {
      if (newGame)
      {
        this.notation.Clear();
        this.NewGame();
        this.ui?.NewGame();
      }
      else
      {
        this.TurnStart(this.whosTurn);
        this.ui?.RefreshAll();
      }
      this.KillLoop();
      this.updateLoop = Timing.RunCoroutine(this.UpdateLoop());
    }

    public void SetStartSettings()
    {
      if (this.players.Count > 0)
      {
        this.players[0].curTime = this.gameSettings.player1Time;
        this.players[0].curDelay = this.gameSettings.player1Delay;
        this.players[0].startTurnTime = this.gameSettings.player1Time;
      }
      if (this.players.Count <= 1)
        return;
      this.players[1].curTime = this.gameSettings.player2Time;
      this.players[1].curDelay = this.gameSettings.player2Delay;
      this.players[1].startTurnTime = this.gameSettings.player2Time;
    }

    public override void Serialize(myBinaryWriter w)
    {
      w.Write(this.id);
      w.Write(this.status);
      w.Write((byte) this.whosTurn);
      w.Write((byte) this.firstPlayer);
      if (this.players.Count < 1)
        w.Write((byte) 0);
      else
        this.players[0].Serialize(w);
      if (this.players.Count < 2)
        w.Write((byte) 0);
      else
        this.players[1].Serialize(w);
      this.gameSettings.Serialize(w);
      w.Write(this.reason);
      w.Write(this.pieces.Count);
      foreach (Piece piece in this.pieces)
      {
        w.Write((byte) piece.Char);
        w.Write((byte) piece.Color);
        w.Write(piece.Moved);
        w.Write((byte) piece.Parent.X);
        w.Write((byte) piece.Parent.Y);
      }
      w.Write(this.previousMoves.Count);
      foreach (ChessMove previousMove in this.previousMoves)
        previousMove.Serialize(w);
      w.Write(this.spectators.Count);
      foreach (Connection spectator in this.spectators)
        w.Write(spectator.name);
      w.Write(this.notation.Count);
      foreach (string str in this.notation)
        w.Write(str);
    }

    public static ChessBoard Deserialize(myBinaryReader r)
    {
      ChessBoard chessBoard = new ChessBoard(false);
      chessBoard.id = r.ReadInt32();
      chessBoard.status = r.ReadInt32();
      chessBoard.whosTurn = (PlayerColor) r.ReadByte();
      chessBoard.firstPlayer = (PlayerColor) r.ReadByte();
      chessBoard.secondPlayer = chessBoard.firstPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
      IMiniGame.Player player1 = IMiniGame.Player.Deserialize(r);
      if (player1 != null)
        chessBoard.players.Add(player1);
      IMiniGame.Player player2 = IMiniGame.Player.Deserialize(r);
      if (player2 != null)
        chessBoard.players.Add(player2);
      chessBoard.gameSettings = IMiniGame.GameSettings.Desieralize(r);
      chessBoard.reason = r.ReadString();
      chessBoard.InitBoard();
      int num1 = r.ReadInt32();
      for (int index = 0; index < num1; ++index)
      {
        Piece piece = ChessBoard.FromChar((ChessPiece) r.ReadByte(), (PlayerColor) r.ReadByte());
        piece.SetMoved = r.ReadBoolean();
        if (piece.Char == ChessPiece.King)
        {
          if (piece.Color == PlayerColor.White)
            chessBoard.whiteKing = piece;
          else
            chessBoard.blackKing = piece;
        }
        chessBoard.addPiece(chessBoard.cells[(int) r.ReadByte(), (int) r.ReadByte()], piece);
      }
      int num2 = r.ReadInt32();
      for (int index = 0; index < num2; ++index)
        chessBoard.previousMoves.Add(ChessMove.Deserialize(r));
      int num3 = r.ReadInt32();
      for (int index = 0; index < num3; ++index)
      {
        TcpConnection tcpConnection = new TcpConnection();
        tcpConnection.player.account.name = r.ReadString();
        chessBoard.spectators.Add((Connection) tcpConnection);
      }
      int num4 = r.ReadInt32();
      for (int index = 0; index < num4; ++index)
        chessBoard.notation.Add(r.ReadString());
      return chessBoard;
    }

    public ChessBoard.Cell GetCell(int x, int y)
    {
      return x < 0 || this.cells.GetLength(0) <= x || y < 0 || this.cells.GetLength(1) <= y ? (ChessBoard.Cell) null : this.cells[x, y];
    }

    private void addPiece(ChessBoard.Cell cell, Piece piece)
    {
      cell.Piece = piece;
      this.pieces.Add(piece);
      piece.OnPlace(cell);
      if (!this.isClient)
        return;
      ChessUI.Instance?.CreateImage(cell);
    }

    public void InitBoard()
    {
      if (this.pieces != null)
      {
        foreach (Piece piece in this.pieces)
          piece?.Destroy();
      }
      this.cells = new ChessBoard.Cell[8, 8];
      for (int x = 0; x < 8; ++x)
      {
        for (int y = 0; y < 8; ++y)
          this.cells[x, y] = new ChessBoard.Cell(this, x, y);
      }
      this.pieces.Clear();
      this.EnPassant = (ChessBoard.Cell) null;
      this.EnPassantCapture = (ChessBoard.Cell) null;
    }

    public void Reset()
    {
      foreach (IMiniGame.Player player in this.players)
        player.offeringDraw = false;
      this.InitBoard();
      this.addPiece(this.cells[0, 0], (Piece) new Rook(PlayerColor.White));
      this.addPiece(this.cells[1, 0], (Piece) new Knight(PlayerColor.White));
      this.addPiece(this.cells[2, 0], (Piece) new Bishop(PlayerColor.White));
      this.addPiece(this.cells[3, 0], (Piece) new Queen(PlayerColor.White));
      this.addPiece(this.cells[4, 0], this.whiteKing = (Piece) new King(PlayerColor.White));
      this.addPiece(this.cells[5, 0], (Piece) new Bishop(PlayerColor.White));
      this.addPiece(this.cells[6, 0], (Piece) new Knight(PlayerColor.White));
      this.addPiece(this.cells[7, 0], (Piece) new Rook(PlayerColor.White));
      this.addPiece(this.cells[0, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[1, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[2, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[3, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[4, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[5, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[6, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[7, 1], (Piece) new Pawn(PlayerColor.White));
      this.addPiece(this.cells[0, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[1, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[2, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[3, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[4, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[5, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[6, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[7, 6], (Piece) new Pawn(PlayerColor.Black));
      this.addPiece(this.cells[0, 7], (Piece) new Rook(PlayerColor.Black));
      this.addPiece(this.cells[1, 7], (Piece) new Knight(PlayerColor.Black));
      this.addPiece(this.cells[2, 7], (Piece) new Bishop(PlayerColor.Black));
      this.addPiece(this.cells[3, 7], (Piece) new Queen(PlayerColor.Black));
      this.addPiece(this.cells[4, 7], this.blackKing = (Piece) new King(PlayerColor.Black));
      this.addPiece(this.cells[5, 7], (Piece) new Bishop(PlayerColor.Black));
      this.addPiece(this.cells[6, 7], (Piece) new Knight(PlayerColor.Black));
      this.addPiece(this.cells[7, 7], (Piece) new Rook(PlayerColor.Black));
      foreach (Piece piece in this.pieces)
        piece.Recalculate();
    }

    public static Piece FromChar(ChessPiece c, PlayerColor p)
    {
      switch (c)
      {
        case ChessPiece.Pawn:
          return (Piece) new Pawn(p);
        case ChessPiece.Bishop:
          return (Piece) new Bishop(p);
        case ChessPiece.Knight:
          return (Piece) new Knight(p);
        case ChessPiece.Rook:
          return (Piece) new Rook(p);
        case ChessPiece.Queen:
          return (Piece) new Queen(p);
        case ChessPiece.King:
          return (Piece) new King(p);
        default:
          throw new Exception("Unknown piece: " + (object) c);
      }
    }

    public void NewGame()
    {
      this.Reset();
      this.TurnStart(PlayerColor.White);
    }

    public bool TurnStart(PlayerColor currentPlayer)
    {
      this.whiteKingStillInCheckCells.Clear();
      this.blackKingStillInCheckCells.Clear();
      if (this.status != 0)
        this.status = 1;
      this.whosTurn = currentPlayer;
      this.GetPlayer(this.whosTurn)?.SetStartTime();
      this.inCheck = this.IsInCheck(currentPlayer, false);
      bool flag = false;
      ChessBoard.Cell[,] cells = this.cells;
      int upperBound1 = cells.GetUpperBound(0);
      int upperBound2 = cells.GetUpperBound(1);
      for (int lowerBound1 = cells.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
      {
        for (int lowerBound2 = cells.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
          cells[lowerBound1, lowerBound2].HitBy.Clear();
      }
      foreach (Piece piece in this.pieces)
        piece.Recalculate();
      foreach (Piece piece in this.pieces)
      {
        piece.LegalMoves.Clear();
        foreach (ChessBoard.Cell possibleMove in piece.PossibleMoves)
        {
          if (piece.Color == currentPlayer && this.isMoveLegal(piece, possibleMove))
          {
            piece.LegalMoves.Add(possibleMove);
            flag = true;
          }
        }
      }
      this.ui?.TurnStart();
      if (this.inCheck)
        this.note += "+";
      return flag;
    }

    private bool isMoveLegal(Piece piece, ChessBoard.Cell move)
    {
      if (piece.Color != this.whosTurn)
        return false;
      Piece piece1 = piece.Color == PlayerColor.White ? this.whiteKing : this.blackKing;
      if (piece is King)
      {
        if (piece.Color == PlayerColor.White)
        {
          if (this.whiteKingStillInCheckCells.Contains(move))
            return false;
        }
        else if (this.blackKingStillInCheckCells.Contains(move))
          return false;
        foreach (Piece piece2 in move.HitBy)
        {
          if (piece2.Parent != move && piece2.Color != piece.Color)
            return false;
        }
        if (Math.Abs(move.X - piece.Parent.X) == 2)
        {
          if (this.inCheck)
            return false;
          foreach (Piece piece3 in this.GetCell(move.X > piece.Parent.X ? move.X - 1 : move.X + 1, move.Y).HitBy)
          {
            if (piece3.Color != piece.Color)
              return false;
          }
        }
      }
      else
      {
        if (this.inCheck)
        {
          foreach (Piece piece4 in piece1.Parent.HitBy)
          {
            if (piece4.Color != piece1.Color && piece4.Parent != move && !piece4.IsBlockedIfMove(piece.Parent, move, piece1.Parent))
              return false;
          }
        }
        foreach (Piece piece5 in piece.Parent.HitBy)
        {
          if (piece5.Color != piece1.Color && piece5.Parent != move && !piece5.IsBlockedIfMove(piece.Parent, move, piece1.Parent))
            return false;
        }
      }
      return true;
    }

    public bool IsInCheck(PlayerColor player, bool useCache = true)
    {
      if (useCache)
        return this.inCheck;
      return player == PlayerColor.White ? this.whiteKing.Parent.HitBy.Any<Piece>((Func<Piece, bool>) (hitter => hitter.Color != player)) : this.blackKing.Parent.HitBy.Any<Piece>((Func<Piece, bool>) (hitter => hitter.Color != player));
    }

    public bool WouldBeCheck(PlayerColor player, ChessBoard.Cell x)
    {
      return x.HitBy.Any<Piece>((Func<Piece, bool>) (hitter => hitter.Color != player));
    }

    public string GetNotation(ChessBoard.Cell from, ChessBoard.Cell to)
    {
      if (from.Piece is King && to.X - from.X == 2)
        return "O-O";
      if (from.Piece is King && to.X - from.X == -2)
        return "O-O-O";
      string str1 = this.PieceToChar(from.Piece.Char);
      foreach (Piece piece in this.pieces)
      {
        if (from.Piece != piece && piece.Color == from.Piece.Color && piece.Char == from.Piece.Char && piece.LegalMoves.Contains(to))
          str1 = piece.Parent.X == from.X ? str1 + (to.Y + 1).ToString() : str1 + ((char) (from.X + 97)).ToString();
      }
      if (to.Piece != null)
        str1 += "x";
      string str2 = this.StringMove(to);
      return str1 + str2;
    }

    private string StringMove(ChessBoard.Cell to)
    {
      return ((char) (to.X + 97)).ToString() + (to.Y + 1).ToString();
    }

    private string PieceToChar(ChessPiece p)
    {
      switch (p)
      {
        case ChessPiece.Pawn:
          return "";
        case ChessPiece.Bishop:
          return "B";
        case ChessPiece.Knight:
          return "N";
        case ChessPiece.Rook:
          return "R";
        case ChessPiece.Queen:
          return "Q";
        case ChessPiece.King:
          return "K";
        default:
          return "";
      }
    }

    public void Move(
      ChessBoard.Cell from,
      ChessBoard.Cell to,
      PromoteOptions promoteOption,
      ChessMove chessMove,
      bool saveMove = true)
    {
      this.note = this.GetNotation(from, to);
      if (saveMove)
      {
        this.ui?.poolMoved.DeSpawn();
        if (this.isClient)
        {
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(from), Color.red);
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(to), Color.red);
        }
      }
      if (saveMove)
        this.previousMoves.Add(chessMove);
      if (to.Piece != null)
      {
        to.Piece.Destroy();
        this.pieces.Remove(to.Piece);
      }
      to.Piece = from.Piece;
      from.Piece = (Piece) null;
      if (to == this.EnPassant && to.Piece is Pawn)
      {
        this.EnPassantCapture.Piece.Destroy();
        this.pieces.Remove(this.EnPassantCapture.Piece);
        this.EnPassantCapture.Piece = (Piece) null;
        this.note = ((char) (from.X + 97)).ToString() + "x" + this.StringMove(to);
      }
      if (to.Piece is King && to.X - from.X == 2)
        this.Move(this.GetCell(7, to.Y), this.GetCell(to.X - 1, to.Y), promoteOption, chessMove, false);
      if (to.Piece is King && to.X - from.X == -2)
        this.Move(this.GetCell(0, to.Y), this.GetCell(to.X + 1, to.Y), promoteOption, chessMove, false);
      if (to.Piece is Pawn && to.Y == (to.Piece.Color == PlayerColor.White ? 7 : 0))
      {
        Piece piece = (Piece) null;
        switch (promoteOption)
        {
          case PromoteOptions.Queen:
            piece = (Piece) new Queen(to.Piece);
            this.note = this.StringMove(to) + "=Q";
            break;
          case PromoteOptions.Rook:
            piece = (Piece) new Rook(to.Piece);
            this.note = this.StringMove(to) + "=R";
            break;
          case PromoteOptions.Bishop:
            piece = (Piece) new Bishop(to.Piece);
            this.note = this.StringMove(to) + "=B";
            break;
          case PromoteOptions.Knight:
            piece = (Piece) new Knight(to.Piece);
            this.note = this.StringMove(to) + "=K";
            break;
        }
        if (to.Piece != null)
        {
          to.Piece.Destroy();
          this.pieces.Remove(to.Piece);
        }
        to.Piece = piece;
        if (this.isClient)
          ChessUI.Instance?.CreateImage(to);
        piece.OnPlace(to);
        this.pieces.Add(piece);
      }
      to.Piece.OnMove(to);
      to.Piece.Recalculate();
      if (saveMove)
      {
        foreach (Piece piece in this.pieces)
        {
          if (piece.Color == to.Piece.Color)
          {
            piece.Recalculate();
            piece.LegalMoves.Clear();
            foreach (ChessBoard.Cell possibleMove in piece.PossibleMoves)
            {
              if (this.isMoveLegal(piece, possibleMove))
                piece.LegalMoves.Add(possibleMove);
            }
          }
        }
      }
      this.EnPassant = (ChessBoard.Cell) null;
      this.EnPassantCapture = (ChessBoard.Cell) null;
      if (!(to.Piece is Pawn) || Math.Abs(to.Y - from.Y) != 2)
        return;
      this.EnPassant = this.GetCell(to.X, from.Y > to.Y ? from.Y - 1 : from.Y + 1);
      this.EnPassantCapture = to;
    }

    public bool IsPromotable(ChessBoard.Cell from, ChessBoard.Cell to)
    {
      return from.Piece is Pawn && to.Y == (from.Piece.Color == PlayerColor.White ? 7 : 0);
    }

    public class Cell
    {
      public int X;
      public int Y;
      public byte index;
      public Piece Piece;
      public List<Piece> HitBy;

      public ChessBoard Parent { private set; get; }

      public Cell(ChessBoard parent, int x, int y)
      {
        this.Parent = parent;
        this.HitBy = new List<Piece>();
        this.X = x;
        this.Y = y;
        this.index = (byte) (x * 8 + y);
      }

      public IEnumerable<ChessBoard.Cell> OpenLineOfSight(
        Piece piece,
        int dirX,
        int dirY,
        int desiredCount = 1)
      {
        for (int i = 1; i <= desiredCount; ++i)
        {
          ChessBoard.Cell cell = this.Parent.GetCell(this.X + dirX * i, this.Y + dirY * i);
          if (cell == null)
            break;
          yield return cell;
          if (cell.Piece != null)
          {
            if (cell.Piece.Char != ChessPiece.King || cell.Piece.Color == piece.Color)
              break;
            if (piece.Color == PlayerColor.White)
              this.Parent.blackKingStillInCheckCells.Add(cell);
            else
              this.Parent.whiteKingStillInCheckCells.Add(cell);
            int num = i + 1;
            if (num > desiredCount)
              break;
            cell = this.Parent.GetCell(this.X + dirX * num, this.Y + dirY * num);
            if (cell == null || cell.Piece != null)
              break;
            if (piece.Color == PlayerColor.White)
            {
              this.Parent.blackKingStillInCheckCells.Add(cell);
              break;
            }
            this.Parent.whiteKingStillInCheckCells.Add(cell);
            break;
          }
          cell = (ChessBoard.Cell) null;
        }
      }

      public ChessBoard.Cell Open(int x, int y)
      {
        return this.Parent.GetCell(this.X + x, this.Y + y) ?? (ChessBoard.Cell) null;
      }
    }
  }
}
