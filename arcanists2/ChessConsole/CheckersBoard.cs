// Decompiled with JetBrains decompiler
// Type: ChessConsole.CheckersBoard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Hazel;
using Hazel.Tcp;
using MovementEffects;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
namespace ChessConsole
{
  public class CheckersBoard : IMiniGame
  {
    public string reason = "";
    public PlayerColor firstPlayer;
    public PlayerColor secondPlayer;
    public PlayerColor whosTurn;
    public List<ChessMove> previousMoves = new List<ChessMove>();
    public CheckersUI ui;
    public IMiniGame.GameSettings gameSettings = new IMiniGame.GameSettings();
    private List<CheckersBoard.CheckersPiece> pieces = new List<CheckersBoard.CheckersPiece>();
    private HashSet<CheckersBoard.CheckersPiece> whiteKills = new HashSet<CheckersBoard.CheckersPiece>();
    private HashSet<CheckersBoard.CheckersPiece> blackKills = new HashSet<CheckersBoard.CheckersPiece>();
    private bool inCheck;
    private IMiniGame.Player cur;
    private CheckersBoard.Cell[,] cells;

    public override string GetGameType() => "Checkers";

    public override IMiniGame.GameType gameType => IMiniGame.GameType.Checkers;

    public string firstColor => this.firstPlayer != PlayerColor.White ? "Black" : "Red";

    public string secondColor => this.secondPlayer != PlayerColor.White ? "Black" : "Red";

    public void AddKiller(CheckersBoard.CheckersPiece p)
    {
      if (p.Color == PlayerColor.White)
        this.whiteKills.Add(p);
      else
        this.blackKills.Add(p);
    }

    public bool IsKiller(CheckersBoard.CheckersPiece p)
    {
      return p.Color == PlayerColor.White ? this.whiteKills.Contains(p) : this.blackKills.Contains(p);
    }

    public CheckersBoard.Cell[,] AllCells => this.cells;

    public CheckersBoard.Cell FromIndex(int i) => this.cells[i / 8, i % 8];

    public CheckersBoard.Cell GetCell(int x, int y)
    {
      return x < 0 || this.cells.GetLength(0) <= x || y < 0 || this.cells.GetLength(1) <= y ? (CheckersBoard.Cell) null : this.cells[x, y];
    }

    public CheckersBoard() => this.Reset();

    public CheckersBoard(bool init)
    {
      if (!init)
        return;
      this.Reset();
    }

    public static void Create(Connection c)
    {
      if (c.miniGame != null && c.miniGame.First == c)
      {
        if (c.miniGame.players.Count > 1 && c.miniGame.players[1].connection.State == ConnectionState.Connected)
          c.miniGame.LogicPlayerLeft(c);
        else
          c.miniGame.Dispose();
      }
      c.miniGame = (IMiniGame) new CheckersBoard();
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
          w.Write((byte) 2);
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
          CheckersBoard.Cell from = this.FromIndex(chessMove.from);
          CheckersBoard.Cell cell = this.FromIndex((int) chessMove.to);
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
          if (this.Move(from, cell, chessMove.promotion, chessMove))
          {
            this.SendServerMove(chessMove);
            break;
          }
          int num = this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White) ? 1 : 0;
          this.GetPlayer(this.whosTurn).curDelay = this.whosTurn == this.firstPlayer ? this.gameSettings.player1Delay : this.gameSettings.player2Delay;
          this.SendServerMove(chessMove);
          if (num == 0)
          {
            this.SendGameOver(this.whosTurn == PlayerColor.White ? "Black Wins" : "Red Wins", this.whosTurn == this.firstPlayer ? 1 : 0);
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
      CheckersBoard.Cell cell = this.FromIndex(r.ReadInt32());
      int c = r.ReadInt32();
      if (c == -1)
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (CheckersBoard.CheckersPiece) null;
        }
      }
      else
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (CheckersBoard.CheckersPiece) null;
        }
        this.addPiece(cell, CheckersBoard.FromChar((ChessPiece) c, this.whosTurn, this));
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
          if (this.Move(this.FromIndex(chessMove.from), this.FromIndex((int) chessMove.to), chessMove.promotion, chessMove))
          {
            this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(this.FromIndex((int) chessMove.to)), new Color(1f, 0.0f, 1f, 0.5f));
            bool? playingAsBlack = CheckersUI.Instance?.playingAsBlack;
            CheckersBoard.CheckersPiece piece = this.FromIndex((int) chessMove.to).Piece;
            bool flag = piece != null && piece.Color == PlayerColor.Black;
            if (!(playingAsBlack.GetValueOrDefault() == flag & playingAsBlack.HasValue))
              break;
            CheckersUI.Instance?.ClickCell(this.FromIndex((int) chessMove.to));
            break;
          }
          AudioManager.PlayChess(this.ui.playingAsBlack == (this.whosTurn == PlayerColor.Black) ? this.ui.chessMove : this.ui.chessMoveEnemy);
          this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
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

    public void InitBoard()
    {
      if (this.pieces != null)
      {
        foreach (CheckersBoard.CheckersPiece piece in this.pieces)
          piece?.Destroy();
      }
      this.cells = new CheckersBoard.Cell[8, 8];
      for (int x = 0; x < 8; ++x)
      {
        for (int y = 0; y < 8; ++y)
          this.cells[x, y] = new CheckersBoard.Cell(this, x, y);
      }
      this.pieces.Clear();
    }

    public void Reset()
    {
      foreach (IMiniGame.Player player in this.players)
        player.offeringDraw = false;
      this.InitBoard();
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = index1 % 2; index2 < 8; index2 += 2)
          this.addPiece(this.cells[index2, index1], (CheckersBoard.CheckersPiece) new CheckersBoard.Pawn(PlayerColor.White, this));
      }
      for (int index3 = 7; index3 > 4; --index3)
      {
        for (int index4 = index3 % 2; index4 < 8; index4 += 2)
          this.addPiece(this.cells[index4, index3], (CheckersBoard.CheckersPiece) new CheckersBoard.Pawn(PlayerColor.Black, this));
      }
      this.whiteKills.Clear();
      this.blackKills.Clear();
      foreach (CheckersBoard.CheckersPiece piece in this.pieces)
        piece.Recalculate();
    }

    public void NewGame()
    {
      this.Reset();
      this.TurnStart(PlayerColor.Black);
    }

    public override void Start(bool newGame)
    {
      if (newGame)
      {
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
      CheckersBoard checkersBoard = this;
      while ((checkersBoard.status == 1 || checkersBoard.status == 3) && checkersBoard.players.Count == 2)
      {
        if (checkersBoard.status == 3)
        {
          yield return 0.0f;
        }
        else
        {
          checkersBoard.cur = checkersBoard.firstPlayer == checkersBoard.whosTurn ? checkersBoard.players[0] : checkersBoard.players[1];
          checkersBoard.TimePass();
          yield return 0.0f;
        }
      }
      if (checkersBoard.status == 1)
        checkersBoard.SendGameOver("Error", -1);
      checkersBoard.updateLoop = (IEnumerator<float>) null;
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
      foreach (CheckersBoard.CheckersPiece piece in this.pieces)
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
    }

    public static CheckersBoard Deserialize(myBinaryReader r)
    {
      CheckersBoard b = new CheckersBoard(false);
      b.id = r.ReadInt32();
      b.status = r.ReadInt32();
      b.whosTurn = (PlayerColor) r.ReadByte();
      b.firstPlayer = (PlayerColor) r.ReadByte();
      b.secondPlayer = b.firstPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
      IMiniGame.Player player1 = IMiniGame.Player.Deserialize(r);
      if (player1 != null)
        b.players.Add(player1);
      IMiniGame.Player player2 = IMiniGame.Player.Deserialize(r);
      if (player2 != null)
        b.players.Add(player2);
      b.gameSettings = IMiniGame.GameSettings.Desieralize(r);
      b.reason = r.ReadString();
      b.InitBoard();
      int num1 = r.ReadInt32();
      for (int index = 0; index < num1; ++index)
      {
        CheckersBoard.CheckersPiece piece = CheckersBoard.FromChar((ChessPiece) r.ReadByte(), (PlayerColor) r.ReadByte(), b);
        piece.SetMoved = r.ReadBoolean();
        b.addPiece(b.cells[(int) r.ReadByte(), (int) r.ReadByte()], piece);
      }
      int num2 = r.ReadInt32();
      for (int index = 0; index < num2; ++index)
        b.previousMoves.Add(ChessMove.Deserialize(r));
      int num3 = r.ReadInt32();
      for (int index = 0; index < num3; ++index)
      {
        TcpConnection tcpConnection = new TcpConnection();
        tcpConnection.player.account.name = r.ReadString();
        b.spectators.Add((Connection) tcpConnection);
      }
      return b;
    }

    public static CheckersBoard.CheckersPiece FromChar(
      ChessPiece c,
      PlayerColor p,
      CheckersBoard b)
    {
      if (c == ChessPiece.Pawn)
        return (CheckersBoard.CheckersPiece) new CheckersBoard.Pawn(p, b);
      if (c == ChessPiece.Bishop)
        return (CheckersBoard.CheckersPiece) new CheckersBoard.King(p, b);
      throw new Exception("Unknown piece: " + (object) c);
    }

    private void addPiece(CheckersBoard.Cell cell, CheckersBoard.CheckersPiece piece)
    {
      cell.Piece = piece;
      this.pieces.Add(piece);
      piece.OnPlace(cell);
      if (!this.isClient)
        return;
      CheckersUI.Instance?.CreateImage(cell);
    }

    public bool TurnStart(PlayerColor currentPlayer)
    {
      if (this.status != 0)
        this.status = 1;
      this.whosTurn = currentPlayer;
      this.GetPlayer(this.whosTurn)?.SetStartTime();
      bool flag = false;
      this.whiteKills.Clear();
      this.blackKills.Clear();
      foreach (CheckersBoard.CheckersPiece piece in this.pieces)
        piece.Recalculate();
      foreach (CheckersBoard.CheckersPiece piece in this.pieces)
      {
        piece.LegalMoves.Clear();
        foreach (CheckersBoard.Cell possibleMove in piece.PossibleMoves)
        {
          if (piece.Color == currentPlayer && this.isMoveLegal(piece, possibleMove))
          {
            piece.LegalMoves.Add(possibleMove);
            flag = true;
          }
        }
      }
      this.ui?.TurnStart();
      if (this.whosTurn == PlayerColor.White)
      {
        if (this.whiteKills.Count > 0)
        {
          foreach (CheckersBoard.CheckersPiece whiteKill in this.whiteKills)
            this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(whiteKill.Parent), new Color(1f, 0.0f, 1f, 0.5f));
        }
      }
      else if (this.blackKills.Count > 0)
      {
        foreach (CheckersBoard.CheckersPiece blackKill in this.blackKills)
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(blackKill.Parent), new Color(1f, 0.0f, 1f, 0.5f));
      }
      return flag;
    }

    public bool Move(
      CheckersBoard.Cell from,
      CheckersBoard.Cell to,
      PromoteOptions promoteOption,
      ChessMove chessMove,
      bool saveMove = true)
    {
      if (saveMove)
      {
        this.ui?.poolMoved.DeSpawn();
        if (this.isClient)
        {
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(from), new Color(1f, 1f, 0.0f, 0.3f));
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(to), new Color(1f, 1f, 0.0f, 0.3f));
        }
      }
      if (saveMove)
        this.previousMoves.Add(chessMove);
      bool flag = false;
      if (from.Distance(to) == 4)
      {
        CheckersBoard.Cell jump = from.GetJump(to);
        if (jump != null && jump.Piece != null)
        {
          flag = true;
          jump.Piece.Destroy();
          this.pieces.Remove(jump.Piece);
          jump.Piece = (CheckersBoard.CheckersPiece) null;
        }
      }
      to.Piece = from.Piece;
      from.Piece = (CheckersBoard.CheckersPiece) null;
      if (to.Piece is CheckersBoard.Pawn && to.Y == (to.Piece.Color == PlayerColor.White ? 7 : 0))
      {
        CheckersBoard.CheckersPiece checkersPiece = (CheckersBoard.CheckersPiece) new CheckersBoard.King(to.Piece, this);
        if (to.Piece != null)
        {
          to.Piece.Destroy();
          this.pieces.Remove(to.Piece);
        }
        to.Piece = checkersPiece;
        if (this.isClient)
          CheckersUI.Instance?.CreateImage(to);
        checkersPiece.OnPlace(to);
        this.pieces.Add(checkersPiece);
      }
      to.Piece.OnMove(to);
      this.whiteKills.Clear();
      this.blackKills.Clear();
      to.Piece.Recalculate();
      if (saveMove)
      {
        foreach (CheckersBoard.CheckersPiece piece in this.pieces)
        {
          if (piece.Color == to.Piece.Color)
          {
            piece.Recalculate();
            piece.LegalMoves.Clear();
            foreach (CheckersBoard.Cell possibleMove in piece.PossibleMoves)
            {
              if (this.isMoveLegal(piece, possibleMove))
                piece.LegalMoves.Add(possibleMove);
            }
          }
        }
      }
      if (!flag)
        return false;
      return to.Piece.Color != PlayerColor.White ? this.blackKills.Contains(to.Piece) : this.whiteKills.Contains(to.Piece);
    }

    private bool isMoveLegal(CheckersBoard.CheckersPiece piece, CheckersBoard.Cell move)
    {
      if (piece.Color != this.whosTurn || move == null || piece.Color == PlayerColor.White && this.whiteKills.Count > 0 && !this.whiteKills.Contains(piece) || piece.Color == PlayerColor.Black && this.blackKills.Count > 0 && !this.blackKills.Contains(piece))
        return false;
      foreach (CheckersBoard.Cell hit in piece.hits)
      {
        if (hit == move)
          return true;
      }
      return false;
    }

    public class Pawn : CheckersBoard.CheckersPiece
    {
      public Pawn(PlayerColor color, CheckersBoard b)
        : base(color)
      {
        this.hits = new CheckersBoard.Cell[4];
        this.board = b;
      }

      public override IEnumerable<CheckersBoard.Cell> PossibleMoves
      {
        get
        {
          CheckersBoard.Pawn pawn = this;
          if (pawn.canHit(pawn.hits[0]))
            yield return pawn.hits[0];
          if (pawn.canHit(pawn.hits[1]))
            yield return pawn.hits[1];
          for (int i = 2; i < pawn.hits.Length; ++i)
          {
            if (pawn.canHit(pawn.hits[i]))
            {
              yield return pawn.hits[i];
              foreach (CheckersBoard.Cell possibleMove in pawn.ContinueAttack(pawn.hits[i]))
                yield return possibleMove;
            }
          }
        }
      }

      public IEnumerable<CheckersBoard.Cell> ContinueAttack(CheckersBoard.Cell z)
      {
        CheckersBoard.Pawn pawn = this;
        CheckersBoard.Cell cell1 = z.Open(-1, pawn.Color == PlayerColor.White ? 1 : -1);
        CheckersBoard.Cell b = z.Open(1, pawn.Color == PlayerColor.White ? 1 : -1);
        if (cell1 != null && cell1.Piece != null && cell1.Piece.Color != pawn.Color)
        {
          CheckersBoard.Cell cell2 = cell1.Open(-1, pawn.Color == PlayerColor.White ? 1 : -1);
          if (cell2 != null && cell2.Piece == null)
            yield return cell2;
        }
        if (b != null && b.Piece != null && b.Piece.Color != pawn.Color)
        {
          CheckersBoard.Cell cell3 = b.Open(1, pawn.Color == PlayerColor.White ? 1 : -1);
          if (cell3 != null && cell3.Piece == null)
            yield return cell3;
        }
      }

      public override void Recalculate()
      {
        this.hits[0] = this.Parent.Open(-1, this.Color == PlayerColor.White ? 1 : -1);
        this.hits[1] = this.Parent.Open(1, this.Color == PlayerColor.White ? 1 : -1);
        if (this.hits[0] != null && this.hits[0].Piece != null && this.hits[0].Piece.Color != this.Color)
        {
          this.hits[2] = this.Parent.Open(-2, this.Color == PlayerColor.White ? 2 : -2);
          if (this.hits[2] != null && this.hits[2].Piece != null)
            this.hits[2] = (CheckersBoard.Cell) null;
          else if (this.hits[2] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[2] = (CheckersBoard.Cell) null;
        if (this.hits[0] != null && this.hits[0].Piece != null)
          this.hits[0] = (CheckersBoard.Cell) null;
        if (this.hits[1] != null && this.hits[1].Piece != null && this.hits[1].Piece.Color != this.Color)
        {
          this.hits[3] = this.Parent.Open(2, this.Color == PlayerColor.White ? 2 : -2);
          if (this.hits[3] != null && this.hits[3].Piece != null)
            this.hits[3] = (CheckersBoard.Cell) null;
          else if (this.hits[3] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[3] = (CheckersBoard.Cell) null;
        if (this.hits[1] != null && this.hits[1].Piece != null)
          this.hits[1] = (CheckersBoard.Cell) null;
        if (this.hits[2] != null)
          this.hits[1] = (CheckersBoard.Cell) null;
        if (this.hits[3] == null)
          return;
        this.hits[0] = (CheckersBoard.Cell) null;
      }

      public override ChessPiece Char => ChessPiece.Pawn;

      protected override bool canHit(CheckersBoard.Cell cell) => base.canHit(cell);
    }

    public class King : CheckersBoard.CheckersPiece
    {
      public King(PlayerColor color, CheckersBoard b)
        : base(color)
      {
        this.hits = new CheckersBoard.Cell[8];
        this.board = b;
      }

      public King(CheckersBoard.CheckersPiece p, CheckersBoard b)
        : this(p.Color, b)
      {
        this.Moved = true;
      }

      public override IEnumerable<CheckersBoard.Cell> PossibleMoves
      {
        get
        {
          CheckersBoard.King king = this;
          if (king.canHit(king.hits[0]))
            yield return king.hits[0];
          if (king.canHit(king.hits[1]))
            yield return king.hits[1];
          if (king.canHit(king.hits[4]))
            yield return king.hits[4];
          if (king.canHit(king.hits[5]))
            yield return king.hits[5];
          for (int i = 2; i < king.hits.Length; ++i)
          {
            if ((i < 4 || i > 5) && king.canHit(king.hits[i]))
            {
              yield return king.hits[i];
              foreach (CheckersBoard.Cell possibleMove in king.ContinueAttack(king.hits[i]))
                yield return possibleMove;
            }
          }
        }
      }

      public IEnumerable<CheckersBoard.Cell> ContinueAttack(CheckersBoard.Cell z)
      {
        CheckersBoard.King king = this;
        CheckersBoard.Cell cell1 = z.Open(-1, king.Color == PlayerColor.White ? 1 : -1);
        CheckersBoard.Cell b = z.Open(1, king.Color == PlayerColor.White ? 1 : -1);
        CheckersBoard.Cell c = z.Open(-1, king.Color != PlayerColor.White ? 1 : -1);
        CheckersBoard.Cell d = z.Open(1, king.Color != PlayerColor.White ? 1 : -1);
        if (cell1 != null && cell1.Piece != null && cell1.Piece.Color != king.Color)
        {
          CheckersBoard.Cell cell2 = cell1.Open(-1, king.Color == PlayerColor.White ? 1 : -1);
          if (cell2 != null && cell2.Piece == null)
            yield return cell2;
        }
        if (b != null && b.Piece != null && b.Piece.Color != king.Color)
        {
          CheckersBoard.Cell cell3 = b.Open(1, king.Color == PlayerColor.White ? 1 : -1);
          if (cell3 != null && cell3.Piece == null)
            yield return cell3;
        }
        if (c != null && c.Piece != null && c.Piece.Color != king.Color)
        {
          CheckersBoard.Cell cell4 = c.Open(-1, king.Color != PlayerColor.White ? 1 : -1);
          if (cell4 != null && cell4.Piece == null)
            yield return cell4;
        }
        if (d != null && d.Piece != null && d.Piece.Color != king.Color)
        {
          CheckersBoard.Cell cell5 = d.Open(1, king.Color != PlayerColor.White ? 1 : -1);
          if (cell5 != null && cell5.Piece == null)
            yield return cell5;
        }
      }

      public override void Recalculate()
      {
        this.hits[0] = this.Parent.Open(-1, this.Color == PlayerColor.White ? 1 : -1);
        this.hits[1] = this.Parent.Open(1, this.Color == PlayerColor.White ? 1 : -1);
        this.hits[4] = this.Parent.Open(-1, this.Color != PlayerColor.White ? 1 : -1);
        this.hits[5] = this.Parent.Open(1, this.Color != PlayerColor.White ? 1 : -1);
        if (this.hits[0] != null && this.hits[0].Piece != null && this.hits[0].Piece.Color != this.Color)
        {
          this.hits[2] = this.Parent.Open(-2, this.Color == PlayerColor.White ? 2 : -2);
          if (this.hits[2] != null && this.hits[2].Piece != null)
            this.hits[2] = (CheckersBoard.Cell) null;
          else if (this.hits[2] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[2] = (CheckersBoard.Cell) null;
        if (this.hits[0] != null && this.hits[0].Piece != null)
          this.hits[0] = (CheckersBoard.Cell) null;
        if (this.hits[1] != null && this.hits[1].Piece != null && this.hits[1].Piece.Color != this.Color)
        {
          this.hits[3] = this.Parent.Open(2, this.Color == PlayerColor.White ? 2 : -2);
          if (this.hits[3] != null && this.hits[3].Piece != null)
            this.hits[3] = (CheckersBoard.Cell) null;
          else if (this.hits[3] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[3] = (CheckersBoard.Cell) null;
        if (this.hits[1] != null && this.hits[1].Piece != null)
          this.hits[1] = (CheckersBoard.Cell) null;
        if (this.hits[4] != null && this.hits[4].Piece != null && this.hits[4].Piece.Color != this.Color)
        {
          this.hits[6] = this.Parent.Open(-2, this.Color != PlayerColor.White ? 2 : -2);
          if (this.hits[6] != null && this.hits[6].Piece != null)
            this.hits[6] = (CheckersBoard.Cell) null;
          else if (this.hits[6] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[6] = (CheckersBoard.Cell) null;
        if (this.hits[4] != null && this.hits[4].Piece != null)
          this.hits[4] = (CheckersBoard.Cell) null;
        if (this.hits[5] != null && this.hits[5].Piece != null && this.hits[5].Piece.Color != this.Color)
        {
          this.hits[7] = this.Parent.Open(2, this.Color != PlayerColor.White ? 2 : -2);
          if (this.hits[7] != null && this.hits[7].Piece != null)
            this.hits[7] = (CheckersBoard.Cell) null;
          else if (this.hits[7] != null)
            this.board.AddKiller((CheckersBoard.CheckersPiece) this);
        }
        else
          this.hits[7] = (CheckersBoard.Cell) null;
        if (this.hits[5] != null && this.hits[5].Piece != null)
          this.hits[5] = (CheckersBoard.Cell) null;
        if (!this.board.IsKiller((CheckersBoard.CheckersPiece) this))
          return;
        this.hits[0] = (CheckersBoard.Cell) null;
        this.hits[1] = (CheckersBoard.Cell) null;
        this.hits[4] = (CheckersBoard.Cell) null;
        this.hits[5] = (CheckersBoard.Cell) null;
      }

      public override ChessPiece Char => ChessPiece.Bishop;

      protected override bool canHit(CheckersBoard.Cell cell) => base.canHit(cell);
    }

    public class Cell
    {
      public int X;
      public int Y;
      public byte index;
      public CheckersBoard.CheckersPiece Piece;

      public CheckersBoard Parent { private set; get; }

      public Cell(CheckersBoard parent, int x, int y)
      {
        this.Parent = parent;
        this.X = x;
        this.Y = y;
        this.index = (byte) (x * 8 + y);
      }

      public CheckersBoard.Cell Open(int x, int y)
      {
        return this.Parent.GetCell(this.X + x, this.Y + y) ?? (CheckersBoard.Cell) null;
      }

      public int Distance(CheckersBoard.Cell b)
      {
        return Mathf.Abs(this.X - b.X) + Mathf.Abs(this.Y - b.Y);
      }

      public CheckersBoard.Cell GetJump(CheckersBoard.Cell b)
      {
        return this.Parent.GetCell(this.X - (this.X - b.X) / 2, this.Y - (this.Y - b.Y) / 2);
      }
    }

    public abstract class CheckersPiece
    {
      public RectTransform image;
      public CheckersBoard.Cell[] hits;
      public CheckersBoard board;
      private CheckersBoard.Cell _parent;

      public PlayerColor Color { private set; get; }

      public bool Moved { protected set; get; }

      public bool SetMoved
      {
        set => this.Moved = value;
      }

      public abstract IEnumerable<CheckersBoard.Cell> PossibleMoves { get; }

      public List<CheckersBoard.Cell> LegalMoves { private set; get; }

      public CheckersBoard.Cell Parent
      {
        private set
        {
          this._parent = value;
          if (!((UnityEngine.Object) this.image != (UnityEngine.Object) null) || this.Parent == null)
            return;
          this.image.anchoredPosition = CheckersUI.Instance.GetAnchoredPositionAllowBig(this.Parent);
          float z1 = this.image.localPosition.z;
          float z2 = (float) (this.Parent.X + this.Parent.Y * 8);
          this.image.localPosition = new Vector3(this.image.localPosition.x, this.image.localPosition.y, z2);
          for (int index1 = 0; index1 < this.image.parent.childCount; ++index1)
          {
            if ((UnityEngine.Object) this.image.parent.GetChild(index1) == (UnityEngine.Object) this.image)
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
        if (!((UnityEngine.Object) this.image != (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.image.gameObject);
      }

      public CheckersPiece(PlayerColor color)
      {
        this.Color = color;
        this.Moved = false;
        this.LegalMoves = new List<CheckersBoard.Cell>();
      }

      public void OnPlace(CheckersBoard.Cell cell) => this.Parent = cell;

      public void OnMove(CheckersBoard.Cell cell)
      {
        this.Parent = cell;
        this.Moved = true;
      }

      public abstract void Recalculate();

      public abstract ChessPiece Char { get; }

      protected virtual bool canHit(CheckersBoard.Cell cell) => cell != null;
    }
  }
}
