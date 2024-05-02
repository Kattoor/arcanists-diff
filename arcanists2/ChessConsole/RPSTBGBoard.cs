

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
  public class RPSTBGBoard : IMiniGame
  {
    public string reason = "";
    public PlayerColor firstPlayer;
    public PlayerColor secondPlayer;
    public PlayerColor whosTurn;
    public List<ChessMove> previousMoves = new List<ChessMove>();
    public RPSTBGUI ui;
    public IMiniGame.GameSettings gameSettings = new IMiniGame.GameSettings();
    private List<RPSTBGBoard.CheckersPiece> pieces = new List<RPSTBGBoard.CheckersPiece>();
    private bool inCheck;
    private IMiniGame.Player cur;
    private RPSTBGBoard.Cell[,] cells;

    public override string GetGameType() => "RPS The Board Game";

    public override IMiniGame.GameType gameType => IMiniGame.GameType.RPSTBG;

    public string firstColor => this.firstPlayer != PlayerColor.White ? "Red" : "Blue";

    public string secondColor => this.secondPlayer != PlayerColor.White ? "Red" : "Blue";

    public RPSTBGBoard.Cell RedPiece(ChessPiece p)
    {
      switch (p)
      {
        case ChessPiece.Pawn:
          return this.cells[0, 4];
        case ChessPiece.Bishop:
          return this.cells[1, 5];
        case ChessPiece.Knight:
          return this.cells[1, 4];
        case ChessPiece.Rook:
          return this.cells[0, 5];
        default:
          return (RPSTBGBoard.Cell) null;
      }
    }

    public RPSTBGBoard.Cell BluePiece(ChessPiece p)
    {
      switch (p)
      {
        case ChessPiece.Pawn:
          return this.cells[5, 1];
        case ChessPiece.Bishop:
          return this.cells[4, 0];
        case ChessPiece.Knight:
          return this.cells[4, 1];
        case ChessPiece.Rook:
          return this.cells[5, 0];
        default:
          return (RPSTBGBoard.Cell) null;
      }
    }

    public RPSTBGBoard.Cell[,] AllCells => this.cells;

    public RPSTBGBoard.Cell FromIndex(int i) => this.cells[i / 6, i % 6];

    public RPSTBGBoard.Cell GetCell(int x, int y)
    {
      return x < 0 || this.cells.GetLength(0) <= x || y < 0 || this.cells.GetLength(1) <= y ? (RPSTBGBoard.Cell) null : this.cells[x, y];
    }

    public RPSTBGBoard() => this.Reset();

    public RPSTBGBoard(bool init)
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
      c.miniGame = (IMiniGame) new RPSTBGBoard();
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
          w.Write((byte) 4);
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
          RPSTBGBoard.Cell from = this.FromIndex(chessMove.from);
          RPSTBGBoard.Cell cell = this.FromIndex((int) chessMove.to);
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
            this.SendGameOver("Win", this.whosTurn == this.firstPlayer ? 0 : 1);
            break;
          }
          int num = this.TurnStart(this.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White) ? 1 : 0;
          this.GetPlayer(this.whosTurn).curDelay = this.whosTurn == this.firstPlayer ? this.gameSettings.player1Delay : this.gameSettings.player2Delay;
          this.SendServerMove(chessMove);
          if (num != 0)
            break;
          this.SendGameOver(this.whosTurn == PlayerColor.White ? "Blue Wins" : "Red Wins", this.whosTurn == this.firstPlayer ? 1 : 0);
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
      RPSTBGBoard.Cell cell = this.FromIndex(r.ReadInt32());
      int c = r.ReadInt32();
      if (c == -1)
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (RPSTBGBoard.CheckersPiece) null;
        }
      }
      else
      {
        if (cell.Piece != null)
        {
          cell.Piece.Destroy();
          this.pieces.Remove(cell.Piece);
          cell.Piece = (RPSTBGBoard.CheckersPiece) null;
        }
        this.addPiece(cell, RPSTBGBoard.FromChar((ChessPiece) c, this.whosTurn, this));
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
        foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
          piece?.Destroy();
      }
      this.cells = new RPSTBGBoard.Cell[6, 6];
      for (int x = 0; x < 6; ++x)
      {
        for (int y = 0; y < 6; ++y)
          this.cells[x, y] = new RPSTBGBoard.Cell(this, x, y);
      }
      this.pieces.Clear();
    }

    public void Reset()
    {
      foreach (IMiniGame.Player player in this.players)
        player.offeringDraw = false;
      this.InitBoard();
      this.addPiece(this.BluePiece(ChessPiece.Pawn), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Pawn(PlayerColor.White, this));
      this.addPiece(this.BluePiece(ChessPiece.Bishop), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Bishop(PlayerColor.White, this));
      this.addPiece(this.BluePiece(ChessPiece.Knight), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Knight(PlayerColor.White, this));
      this.addPiece(this.BluePiece(ChessPiece.Rook), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Rook(PlayerColor.White, this));
      this.addPiece(this.RedPiece(ChessPiece.Pawn), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Pawn(PlayerColor.Black, this));
      this.addPiece(this.RedPiece(ChessPiece.Bishop), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Bishop(PlayerColor.Black, this));
      this.addPiece(this.RedPiece(ChessPiece.Knight), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Knight(PlayerColor.Black, this));
      this.addPiece(this.RedPiece(ChessPiece.Rook), (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Rook(PlayerColor.Black, this));
      foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
        piece.Recalculate();
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
      RPSTBGBoard rpstbgBoard = this;
      while ((rpstbgBoard.status == 1 || rpstbgBoard.status == 3) && rpstbgBoard.players.Count == 2)
      {
        if (rpstbgBoard.status == 3)
        {
          yield return 0.0f;
        }
        else
        {
          rpstbgBoard.cur = rpstbgBoard.firstPlayer == rpstbgBoard.whosTurn ? rpstbgBoard.players[0] : rpstbgBoard.players[1];
          rpstbgBoard.TimePass();
          yield return 0.0f;
        }
      }
      if (rpstbgBoard.status == 1)
        rpstbgBoard.SendGameOver("Error", -1);
      rpstbgBoard.updateLoop = (IEnumerator<float>) null;
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
      foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
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

    public static RPSTBGBoard Deserialize(myBinaryReader r)
    {
      RPSTBGBoard b = new RPSTBGBoard(false);
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
        RPSTBGBoard.CheckersPiece piece = RPSTBGBoard.FromChar((ChessPiece) r.ReadByte(), (PlayerColor) r.ReadByte(), b);
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

    public static RPSTBGBoard.CheckersPiece FromChar(ChessPiece c, PlayerColor p, RPSTBGBoard b)
    {
      switch (c)
      {
        case ChessPiece.Pawn:
          return (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Pawn(p, b);
        case ChessPiece.Bishop:
          return (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Bishop(p, b);
        case ChessPiece.Knight:
          return (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Knight(p, b);
        case ChessPiece.Rook:
          return (RPSTBGBoard.CheckersPiece) new RPSTBGBoard.Rook(p, b);
        default:
          throw new Exception("Unknown piece: " + (object) c);
      }
    }

    private void addPiece(RPSTBGBoard.Cell cell, RPSTBGBoard.CheckersPiece piece)
    {
      cell.Piece = piece;
      this.pieces.Add(piece);
      piece.OnPlace(cell);
      if (!this.isClient)
        return;
      RPSTBGUI.Instance?.CreateImage(cell);
    }

    public bool TurnStart(PlayerColor currentPlayer)
    {
      if (this.status != 0)
        this.status = 1;
      this.whosTurn = currentPlayer;
      this.GetPlayer(this.whosTurn)?.SetStartTime();
      bool flag = false;
      foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
        piece.Recalculate();
      foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
      {
        piece.LegalMoves.Clear();
        foreach (RPSTBGBoard.Cell possibleMove in piece.PossibleMoves)
        {
          if (piece.Color == currentPlayer && this.isMoveLegal(piece, possibleMove))
          {
            piece.LegalMoves.Add(possibleMove);
            flag = true;
          }
        }
      }
      this.ui?.TurnStart();
      return flag;
    }

    public bool Move(
      RPSTBGBoard.Cell from,
      RPSTBGBoard.Cell to,
      PromoteOptions promoteOption,
      ChessMove chessMove,
      bool saveMove = true)
    {
      if (saveMove)
      {
        this.ui?.poolMoved.DeSpawn();
        this.ui?.Despawn();
        if (this.isClient)
        {
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(from), new Color(1f, 1f, 0.0f, 0.3f));
          this.ui?.poolMoved.SpawnBehind((Vector3) this.ui.GetAnchoredPosition(to), new Color(1f, 1f, 0.0f, 0.3f));
        }
      }
      if (saveMove)
        this.previousMoves.Add(chessMove);
      bool flag = false;
      if (to.Piece != null)
      {
        if (to.Piece.Char == ChessPiece.Rook)
          flag = true;
        to.Piece.Destroy();
        this.pieces.Remove(to.Piece);
        to.Piece = (RPSTBGBoard.CheckersPiece) null;
      }
      if (from.Piece != null && from.Piece.Char == ChessPiece.Rook)
      {
        ChessPiece c = ChessPiece.Queen;
        if (to == this.RedPiece(ChessPiece.Pawn))
          c = ChessPiece.Pawn;
        else if (to == this.RedPiece(ChessPiece.Bishop))
          c = ChessPiece.Bishop;
        else if (to == this.RedPiece(ChessPiece.Knight))
          c = ChessPiece.Knight;
        else if (to == this.BluePiece(ChessPiece.Pawn))
          c = ChessPiece.Pawn;
        else if (to == this.BluePiece(ChessPiece.Bishop))
          c = ChessPiece.Bishop;
        else if (to == this.BluePiece(ChessPiece.Knight))
          c = ChessPiece.Knight;
        RPSTBGBoard.CheckersPiece checkersPiece = RPSTBGBoard.FromChar(c, this.whosTurn, this);
        if (to.Piece != null)
        {
          to.Piece.Destroy();
          this.pieces.Remove(to.Piece);
        }
        to.Piece = checkersPiece;
        if (this.isClient)
          RPSTBGUI.Instance?.CreateImage(to);
        checkersPiece.OnPlace(to);
        this.pieces.Add(checkersPiece);
        return flag;
      }
      to.Piece = from.Piece;
      from.Piece = (RPSTBGBoard.CheckersPiece) null;
      to.Piece.OnMove(to);
      to.Piece.Recalculate();
      if (saveMove)
      {
        foreach (RPSTBGBoard.CheckersPiece piece in this.pieces)
        {
          if (piece.Color == to.Piece.Color)
          {
            piece.Recalculate();
            piece.LegalMoves.Clear();
            foreach (RPSTBGBoard.Cell possibleMove in piece.PossibleMoves)
            {
              if (this.isMoveLegal(piece, possibleMove))
                piece.LegalMoves.Add(possibleMove);
            }
          }
        }
      }
      return flag;
    }

    private bool isMoveLegal(RPSTBGBoard.CheckersPiece piece, RPSTBGBoard.Cell move)
    {
      return piece.Color == this.whosTurn && move != null;
    }

    public class Pawn : RPSTBGBoard.CheckersPiece
    {
      public Pawn(PlayerColor color, RPSTBGBoard b)
        : base(color)
      {
        this.hits = new RPSTBGBoard.Cell[4];
        this.board = b;
      }

      public override void Recalculate()
      {
      }

      public override ChessPiece Char => ChessPiece.Pawn;

      protected override bool canHit(RPSTBGBoard.Cell cell) => base.canHit(cell);
    }

    public class Bishop : RPSTBGBoard.CheckersPiece
    {
      public Bishop(PlayerColor color, RPSTBGBoard b)
        : base(color)
      {
        this.hits = new RPSTBGBoard.Cell[4];
        this.board = b;
      }

      public override void Recalculate()
      {
      }

      public override ChessPiece Char => ChessPiece.Bishop;

      protected override bool canHit(RPSTBGBoard.Cell cell) => base.canHit(cell);
    }

    public class Knight : RPSTBGBoard.CheckersPiece
    {
      public Knight(PlayerColor color, RPSTBGBoard b)
        : base(color)
      {
        this.hits = new RPSTBGBoard.Cell[4];
        this.board = b;
      }

      public override void Recalculate()
      {
      }

      public override ChessPiece Char => ChessPiece.Knight;

      protected override bool canHit(RPSTBGBoard.Cell cell) => base.canHit(cell);
    }

    public class Rook : RPSTBGBoard.CheckersPiece
    {
      public Rook(PlayerColor color, RPSTBGBoard b)
        : base(color)
      {
        this.hits = new RPSTBGBoard.Cell[4];
        this.board = b;
      }

      public override void Recalculate()
      {
      }

      public override ChessPiece Char => ChessPiece.Rook;

      protected override bool canHit(RPSTBGBoard.Cell cell) => base.canHit(cell);
    }

    public class Cell
    {
      public int X;
      public int Y;
      public byte index;
      public RPSTBGBoard.CheckersPiece Piece;

      public RPSTBGBoard Parent { private set; get; }

      public Cell(RPSTBGBoard parent, int x, int y)
      {
        this.Parent = parent;
        this.X = x;
        this.Y = y;
        this.index = (byte) (x * 6 + y);
      }

      public RPSTBGBoard.Cell Open(int x, int y)
      {
        return this.Parent.GetCell(this.X + x, this.Y + y) ?? (RPSTBGBoard.Cell) null;
      }

      public int Distance(RPSTBGBoard.Cell b) => Mathf.Abs(this.X - b.X) + Mathf.Abs(this.Y - b.Y);

      public RPSTBGBoard.Cell GetJump(RPSTBGBoard.Cell b)
      {
        return this.Parent.GetCell(this.X - (this.X - b.X) / 2, this.Y - (this.Y - b.Y) / 2);
      }
    }

    public abstract class CheckersPiece
    {
      public RectTransform image;
      public RPSTBGBoard.Cell[] hits;
      public RPSTBGBoard board;
      private RPSTBGBoard.Cell _parent;

      public PlayerColor Color { private set; get; }

      public bool Moved { protected set; get; }

      public bool SetMoved
      {
        set => this.Moved = value;
      }

      public IEnumerable<RPSTBGBoard.Cell> FloodFill(
        RPSTBGBoard.Cell c,
        int depth,
        HashSet<RPSTBGBoard.Cell> visited)
      {
        visited.Add(c);
        if (this.CanMove(c))
        {
          yield return c;
          if (c.Piece == null && depth != 3)
          {
            RPSTBGBoard.Cell cell1 = this.board.GetCell(c.X - 1, c.Y);
            if (cell1 != null && !visited.Contains(cell1))
            {
              foreach (RPSTBGBoard.Cell cell2 in this.FloodFill(cell1, depth + 1, visited))
                yield return cell2;
            }
            RPSTBGBoard.Cell cell3 = this.board.GetCell(c.X + 1, c.Y);
            if (cell3 != null && !visited.Contains(cell3))
            {
              foreach (RPSTBGBoard.Cell cell4 in this.FloodFill(cell3, depth + 1, visited))
                yield return cell4;
            }
            RPSTBGBoard.Cell cell5 = this.board.GetCell(c.X, c.Y + 1);
            if (cell5 != null && !visited.Contains(cell5))
            {
              foreach (RPSTBGBoard.Cell cell6 in this.FloodFill(cell5, depth + 1, visited))
                yield return cell6;
            }
            RPSTBGBoard.Cell cell7 = this.board.GetCell(c.X, c.Y - 1);
            if (cell7 != null && !visited.Contains(cell7))
            {
              foreach (RPSTBGBoard.Cell cell8 in this.FloodFill(cell7, depth + 1, visited))
                yield return cell8;
            }
          }
        }
      }

      public IEnumerable<RPSTBGBoard.Cell> PossibleMoves
      {
        get
        {
          if (this.Char == ChessPiece.Rook)
          {
            if (this.Color == PlayerColor.White)
            {
              RPSTBGBoard.Cell possibleMove1 = this.board.BluePiece(ChessPiece.Pawn);
              if (possibleMove1.Piece == null)
                yield return possibleMove1;
              RPSTBGBoard.Cell possibleMove2 = this.board.BluePiece(ChessPiece.Bishop);
              if (possibleMove2.Piece == null)
                yield return possibleMove2;
              RPSTBGBoard.Cell possibleMove3 = this.board.BluePiece(ChessPiece.Knight);
              if (possibleMove3.Piece == null)
                yield return possibleMove3;
            }
            else
            {
              RPSTBGBoard.Cell possibleMove4 = this.board.RedPiece(ChessPiece.Pawn);
              if (possibleMove4.Piece == null)
                yield return possibleMove4;
              RPSTBGBoard.Cell possibleMove5 = this.board.RedPiece(ChessPiece.Bishop);
              if (possibleMove5.Piece == null)
                yield return possibleMove5;
              RPSTBGBoard.Cell possibleMove6 = this.board.RedPiece(ChessPiece.Knight);
              if (possibleMove6.Piece == null)
                yield return possibleMove6;
            }
          }
          else
          {
            HashSet<RPSTBGBoard.Cell> visited = new HashSet<RPSTBGBoard.Cell>();
            HashSet<RPSTBGBoard.Cell> sent = new HashSet<RPSTBGBoard.Cell>();
            visited.Add(this.Parent);
            RPSTBGBoard.Cell cell1 = this.board.GetCell(this.Parent.X - 1, this.Parent.Y);
            if (cell1 != null && !visited.Contains(cell1))
            {
              foreach (RPSTBGBoard.Cell possibleMove in this.FloodFill(cell1, 1, visited))
              {
                if (sent.Add(possibleMove))
                  yield return possibleMove;
              }
            }
            visited.Clear();
            RPSTBGBoard.Cell cell2 = this.board.GetCell(this.Parent.X + 1, this.Parent.Y);
            if (cell2 != null && !visited.Contains(cell2))
            {
              foreach (RPSTBGBoard.Cell possibleMove in this.FloodFill(cell2, 1, visited))
              {
                if (sent.Add(possibleMove))
                  yield return possibleMove;
              }
            }
            visited.Clear();
            RPSTBGBoard.Cell cell3 = this.board.GetCell(this.Parent.X, this.Parent.Y + 1);
            if (cell3 != null && !visited.Contains(cell3))
            {
              foreach (RPSTBGBoard.Cell possibleMove in this.FloodFill(cell3, 1, visited))
              {
                if (sent.Add(possibleMove))
                  yield return possibleMove;
              }
            }
            visited.Clear();
            RPSTBGBoard.Cell cell4 = this.board.GetCell(this.Parent.X, this.Parent.Y - 1);
            if (cell4 != null && !visited.Contains(cell4))
            {
              foreach (RPSTBGBoard.Cell possibleMove in this.FloodFill(cell4, 1, visited))
              {
                if (sent.Add(possibleMove))
                  yield return possibleMove;
              }
            }
          }
        }
      }

      private bool CanMove(RPSTBGBoard.Cell l)
      {
        if (l == null)
          return false;
        if (l.Piece == null)
          return true;
        if (l.Piece.Color == this.Color)
          return false;
        return l.Piece.Char == this.CanKill || l.Piece.Char == ChessPiece.Rook;
      }

      public ChessPiece CanKill
      {
        get
        {
          if (this.Char == ChessPiece.Pawn)
            return ChessPiece.Knight;
          return this.Char != ChessPiece.Bishop ? ChessPiece.Bishop : ChessPiece.Pawn;
        }
      }

      public List<RPSTBGBoard.Cell> LegalMoves { private set; get; }

      public RPSTBGBoard.Cell Parent
      {
        private set
        {
          this._parent = value;
          if (!((UnityEngine.Object) this.image != (UnityEngine.Object) null) || this.Parent == null)
            return;
          this.image.anchoredPosition = RPSTBGUI.Instance.GetAnchoredPositionAllowBig(this.Parent);
          float z1 = this.image.localPosition.z;
          float z2 = (float) (this.Parent.X + this.Parent.Y * 6);
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
        this.LegalMoves = new List<RPSTBGBoard.Cell>();
      }

      public void OnPlace(RPSTBGBoard.Cell cell) => this.Parent = cell;

      public void OnMove(RPSTBGBoard.Cell cell)
      {
        this.Parent = cell;
        this.Moved = true;
      }

      public abstract void Recalculate();

      public abstract ChessPiece Char { get; }

      protected virtual bool canHit(RPSTBGBoard.Cell cell) => cell != null;
    }
  }
}
