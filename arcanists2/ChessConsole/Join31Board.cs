

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
  public class Join31Board : IMiniGame
  {
    public string reason = "";
    public PlayerColor firstPlayer;
    public PlayerColor secondPlayer;
    public PlayerColor whosTurn;
    public List<ChessMove> previousMoves = new List<ChessMove>();
    public Join31UI ui;
    public IMiniGame.GameSettings gameSettings = new IMiniGame.GameSettings();
    private List<Join31Board.CheckersPiece> pieces = new List<Join31Board.CheckersPiece>();
    private bool inCheck;
    private IMiniGame.Player cur;
    private Join31Board.Cell[,] cells;

    public override string GetGameType() => "Join31";

    public override IMiniGame.GameType gameType => IMiniGame.GameType.Join31;

    public string firstColor => this.firstPlayer != PlayerColor.White ? "Blue" : "Red";

    public string secondColor => this.secondPlayer != PlayerColor.White ? "Blue" : "Red";

    public Join31Board.Cell[,] AllCells => this.cells;

    public Join31Board.Cell FromIndex(int i) => this.cells[i / 7, i % 7];

    public Join31Board.Cell GetCell(int x, int y)
    {
      return x < 0 || this.cells.GetLength(0) <= x || y < 0 || this.cells.GetLength(1) <= y ? (Join31Board.Cell) null : this.cells[x, y];
    }

    public Join31Board() => this.Reset();

    public Join31Board(bool init)
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
      c.miniGame = (IMiniGame) new Join31Board();
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
          w.Write((byte) 3);
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
          Join31Board.Cell cell = this.FromIndex(chessMove.from);
          if (!this.isMoveLegal(this.whosTurn, cell))
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
          if (this.CheckIfWon(this.Move(cell, chessMove.promotion, chessMove)))
          {
            this.SendServerMove(chessMove);
            this.SendGameOver(this.whosTurn == PlayerColor.White ? "Red Wins!" : "Blue Wins!", this.whosTurn == this.firstPlayer ? 1 : 0);
            break;
          }
          int num = this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White) ? 1 : 0;
          this.GetPlayer(this.whosTurn).curDelay = this.whosTurn == this.firstPlayer ? this.gameSettings.player1Delay : this.gameSettings.player2Delay;
          this.SendServerMove(chessMove);
          if (num != 0)
            break;
          this.SendGameOver("Draw", -1);
          break;
        case 4:
          if (this.status != 1)
            break;
          if (flag1)
          {
            this.SendGameOver(this.firstColor + " resigned", 1);
            break;
          }
          this.SendGameOver(this.secondColor + " resigned", 0);
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
      Join31Board.Cell cell = this.FromIndex(r.ReadInt32());
      int c = r.ReadInt32();
      if (c == -1)
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (Join31Board.CheckersPiece) null;
        }
      }
      else
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (Join31Board.CheckersPiece) null;
        }
        this.addPiece(cell, Join31Board.FromChar((ChessPiece) c, this.whosTurn, this));
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
          if (this.CheckIfWon(this.Move(this.FromIndex(chessMove.from), chessMove.promotion, chessMove)))
            break;
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
        foreach (Join31Board.CheckersPiece piece in this.pieces)
          piece?.Destroy();
      }
      this.cells = new Join31Board.Cell[7, 6];
      for (int x = 0; x < 7; ++x)
      {
        for (int y = 0; y < 6; ++y)
          this.cells[x, y] = new Join31Board.Cell(this, x, y);
      }
      this.pieces.Clear();
    }

    public void Reset()
    {
      foreach (IMiniGame.Player player in this.players)
        player.offeringDraw = false;
      this.InitBoard();
    }

    public void NewGame()
    {
      this.Reset();
      this.TurnStart(PlayerColor.White);
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
      Join31Board join31Board = this;
      while ((join31Board.status == 1 || join31Board.status == 3) && join31Board.players.Count == 2)
      {
        if (join31Board.status == 3)
        {
          yield return 0.0f;
        }
        else
        {
          join31Board.cur = join31Board.firstPlayer == join31Board.whosTurn ? join31Board.players[0] : join31Board.players[1];
          join31Board.TimePass();
          yield return 0.0f;
        }
      }
      if (join31Board.status == 1)
        join31Board.SendGameOver("Error", -1);
      join31Board.updateLoop = (IEnumerator<float>) null;
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
      foreach (Join31Board.CheckersPiece piece in this.pieces)
      {
        w.Write((byte) piece.Char);
        w.Write((byte) piece.Color);
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

    public static Join31Board Deserialize(myBinaryReader r)
    {
      Join31Board b = new Join31Board(false);
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
        Join31Board.CheckersPiece piece = Join31Board.FromChar((ChessPiece) r.ReadByte(), (PlayerColor) r.ReadByte(), b);
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

    public static Join31Board.CheckersPiece FromChar(ChessPiece c, PlayerColor p, Join31Board b)
    {
      if (c == ChessPiece.Pawn)
        return (Join31Board.CheckersPiece) new Join31Board.Pawn(p, b);
      throw new Exception("Unknown piece: " + (object) c);
    }

    private void addPiece(Join31Board.Cell cell, Join31Board.CheckersPiece piece)
    {
      cell.Piece = piece;
      this.pieces.Add(piece);
      piece.OnPlace(cell);
      if (!this.isClient)
        return;
      Join31UI.Instance?.CreateImage(cell);
    }

    public bool TurnStart(PlayerColor currentPlayer)
    {
      if (this.status != 0)
        this.status = 1;
      this.whosTurn = currentPlayer;
      this.GetPlayer(this.whosTurn)?.SetStartTime();
      bool flag = false;
      for (int x = 0; x < 7; ++x)
      {
        if (this.GetCell(x, 5).Piece == null)
        {
          flag = true;
          break;
        }
      }
      this.ui?.TurnStart();
      return flag;
    }

    public bool CheckIfWon(Join31Board.Cell from)
    {
      int num1 = 1;
      int x1 = from.X - 1;
      for (int y = from.Y - 1; y >= 0 && x1 >= 0; --y)
      {
        Join31Board.Cell cell = this.GetCell(x1, y);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
        {
          ++num1;
          --x1;
        }
        else
          break;
      }
      int x2 = from.X + 1;
      for (int y = from.Y + 1; y < 7 && x2 < 7; ++y)
      {
        Join31Board.Cell cell = this.GetCell(x2, y);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
        {
          ++num1;
          ++x2;
        }
        else
          break;
      }
      if (num1 >= 4)
        return true;
      int num2 = 1;
      int x3 = from.X - 1;
      for (int y = from.Y + 1; y < 7 && x3 >= 0; ++y)
      {
        Join31Board.Cell cell = this.GetCell(x3, y);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
        {
          ++num2;
          --x3;
        }
        else
          break;
      }
      int x4 = from.X + 1;
      for (int y = from.Y - 1; y >= 0 && x4 <= 7; --y)
      {
        Join31Board.Cell cell = this.GetCell(x4, y);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
        {
          ++num2;
          ++x4;
        }
        else
          break;
      }
      if (num2 >= 4)
        return true;
      int num3 = 1;
      int x5 = from.X;
      for (int y = from.Y - 1; y >= 0; --y)
      {
        Join31Board.Cell cell = this.GetCell(x5, y);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
          ++num3;
        else
          break;
      }
      if (num3 >= 4)
        return true;
      int num4 = 1;
      int x6 = from.X - 1;
      int y1 = from.Y;
      for (; x6 >= 0; --x6)
      {
        Join31Board.Cell cell = this.GetCell(x6, y1);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
          ++num4;
        else
          break;
      }
      int x7 = from.X + 1;
      int y2 = from.Y;
      for (; x7 < 7; ++x7)
      {
        Join31Board.Cell cell = this.GetCell(x7, y2);
        if (cell != null && cell.Piece != null && cell.Piece.Color == this.whosTurn)
          ++num4;
        else
          break;
      }
      return num4 >= 4;
    }

    public Join31Board.Cell Move(
      Join31Board.Cell from,
      PromoteOptions promoteOption,
      ChessMove chessMove,
      bool saveMove = true)
    {
      int x = from.X;
      int num = 5;
      Join31Board.Cell cell = from;
      for (; this.GetCell(x, num - 1) != null && this.GetCell(x, num - 1).Piece == null; --num)
        cell = this.GetCell(x, num - 1);
      if (saveMove)
        this.previousMoves.Add(chessMove);
      Join31Board.CheckersPiece checkersPiece = (Join31Board.CheckersPiece) new Join31Board.Pawn(this.whosTurn, this);
      cell.Piece = checkersPiece;
      if (this.isClient)
        Join31UI.Instance?.CreateImage(cell);
      checkersPiece.OnPlace(cell);
      this.pieces.Add(checkersPiece);
      cell.Piece.OnMove(cell);
      return cell;
    }

    private bool isMoveLegal(PlayerColor color, Join31Board.Cell move)
    {
      return color == this.whosTurn && move != null && move.Piece == null;
    }

    public class Pawn : Join31Board.CheckersPiece
    {
      public Pawn(PlayerColor color, Join31Board b)
        : base(color)
      {
        this.hits = new Join31Board.Cell[4];
        this.board = b;
      }

      public override ChessPiece Char => ChessPiece.Pawn;
    }

    public class Cell
    {
      public int X;
      public int Y;
      public byte index;
      public Join31Board.CheckersPiece Piece;

      public Join31Board Parent { private set; get; }

      public Cell(Join31Board parent, int x, int y)
      {
        this.Parent = parent;
        this.X = x;
        this.Y = y;
        this.index = (byte) (x * 7 + y);
      }

      public Join31Board.Cell Open(int x, int y)
      {
        return this.Parent.GetCell(this.X + x, this.Y + y) ?? (Join31Board.Cell) null;
      }
    }

    public abstract class CheckersPiece
    {
      public RectTransform image;
      public Join31Board.Cell[] hits;
      public Join31Board board;
      private Join31Board.Cell _parent;

      public PlayerColor Color { private set; get; }

      public Join31Board.Cell Parent
      {
        private set
        {
          this._parent = value;
          if (!((UnityEngine.Object) this.image != (UnityEngine.Object) null) || this.Parent == null)
            return;
          this.image.anchoredPosition = Join31UI.Instance.GetAnchoredPositionAllowBig(this.Parent);
          float z1 = this.image.localPosition.z;
          float z2 = (float) (this.Parent.X + this.Parent.Y * 7);
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

      public CheckersPiece(PlayerColor color) => this.Color = color;

      public void OnPlace(Join31Board.Cell cell) => this.Parent = cell;

      public void OnMove(Join31Board.Cell cell) => this.Parent = cell;

      public abstract ChessPiece Char { get; }
    }
  }
}
