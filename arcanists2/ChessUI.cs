

using ChessConsole;
using ChessConsole.Pieces;
using Hazel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ChessUI : MonoBehaviour
{
  public RectTransform rectTransform;
  public RectTransform rectMove;
  public GameObject container;
  public int pieceSet;
  public int boardSet;
  public Image imgColorBg;
  public Image imgColorTan;
  public List<Sprite> imageBoards = new List<Sprite>();
  public List<ChessUI.VisualList> imagePieces = new List<ChessUI.VisualList>();
  public RectTransform pfabPiece;
  public RectTransform pfabButton;
  public RectTransform boardContainer;
  public RectTransform buttonContainer;
  public RectTransform containerBoard;
  public PoolColoredImage poolHighlight;
  public PoolColoredImage poolMoved;
  public Image chessboardBg;
  public GameObject buttonDraw;
  public TMP_Text txtTurn;
  public TMP_Text txtWhiteTime;
  public TMP_Text txtBlackTime;
  public TMP_Text txtWhiteDelay;
  public TMP_Text txtBlackDelay;
  public Image arrowPlayer1;
  public Image arrowPlayer2;
  public GameObject objWinner;
  public TMP_Text txtWinner;
  public TMP_Text txtSpectators;
  public RectTransform containerSpectators;
  public pfabName pfabname;
  public TMP_Text txtName1;
  public TMP_Text txtName2;
  public UIOnHover buttonAudio;
  public TMP_Text txtNotation;
  public GameObject panelNotation;
  public TMP_Text buttonTextNotation;
  public TMP_Text[] notationX;
  public TMP_Text[] notationY;
  [Header("Sounds")]
  public AudioClip chessMoveEnemy;
  public AudioClip chessMove;
  public AudioClip chessCheckmate;
  public AudioClip chessCheck;
  public AudioClip chessStart;
  public AudioClip chessInvalid;
  internal PlayerState playerState;
  private ChessBoard.Cell holdedNode;
  internal ChessBoard board;
  internal bool useAudio = true;
  public const string prefBoard = "prefchessBoard";
  public const string prefPieces = "prefchessPiece";
  public const string prefColor1 = "prefchessColor1";
  public const string prefColor2 = "prefchessColor2";
  public const string prefScale = "prefchessSize";

  public static ChessUI Instance { get; private set; }

  public bool largeImages => false;

  public IMiniGame.GameSettings gameSettings => this.board.gameSettings;

  private bool whitesTurn => this.board.whosTurn == PlayerColor.White;

  internal bool IsFirst => this.board.GetPlayerIndex(Client.Name) == 0;

  internal bool playingAsBlack
  {
    get
    {
      if (this.board.isSpectator)
        return false;
      return !this.IsFirst ? this.board.secondPlayer == PlayerColor.Black : this.board.firstPlayer == PlayerColor.Black;
    }
  }

  public static void Create(ChessBoard b)
  {
    if ((UnityEngine.Object) ChessUI.Instance != (UnityEngine.Object) null)
      ChessUI.Instance.DestroyPrevious();
    ChessUI.Instance = Controller.Instance.CreateAndApply<ChessUI>(Controller.Instance.chessui, Controller.Instance.transform);
    b.isClient = true;
    b.isSpectator = b.players.FindIndex((Predicate<IMiniGame.Player>) (x => string.Equals(x.name, Client.Name))) == -1;
    ChessUI.Instance.board = b;
    b.ui = ChessUI.Instance;
    b.uiObject = ChessUI.Instance.gameObject;
    ChessUI.Instance.NewGame();
    ChessUI.Instance.buttonDraw.SetActive(!b.isSpectator);
    ChessUI.Instance.useAudio = Global.GetPrefBool("prefchessaudio", true);
    ChessUI.Instance.buttonAudio.AlwaysOn = !ChessUI.Instance.useAudio;
  }

  public void ToggleAudio()
  {
    this.useAudio = !this.useAudio;
    Global.SetPrefBool("prefchessaudio", this.useAudio);
    this.buttonAudio.AlwaysOn = !this.useAudio;
  }

  public void ToggleNotation()
  {
    this.panelNotation.SetActive(!this.panelNotation.activeSelf);
    this.buttonTextNotation.text = this.panelNotation.activeSelf ? "Hide" : "Unhide";
  }

  private void Awake()
  {
    ChessUI.Instance = this;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) ChessUI.Instance == (UnityEngine.Object) this)
      ChessUI.Instance = (ChessUI) null;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void Start()
  {
    this.boardSet = Mathf.Clamp(PlayerPrefs.GetInt("prefchessBoard", 5), 0, this.imageBoards.Count - 1);
    this.pieceSet = Mathf.Clamp(PlayerPrefs.GetInt("prefchessPiece", 1), 0, this.imagePieces.Count - 1);
    this.chessboardBg.sprite = this.imageBoards[this.boardSet];
    if (this.board == null)
    {
      this.board = new ChessBoard();
      this.board.ui = this;
      this.board.uiObject = this.gameObject;
      this.NewGame();
    }
    this.RefreshTime();
    this.RefreshPlayers();
    this.RefreshSpectators();
    float num = Mathf.Clamp(PlayerPrefs.GetFloat("prefchessSize", 0.75f), 0.25f, 1f);
    this.transform.localScale = new Vector3(num, num, 1f);
    this.ChangePieceSet();
    this.imgColorBg.color = (Color) Global.GetPrefColor("prefchessColor1", this.imgColorBg.color);
    this.imgColorTan.color = (Color) Global.GetPrefColor("prefchessColor2", this.imgColorTan.color);
  }

  public void ClickStartGame()
  {
    if (!this.IsFirst)
      return;
    int whoFirst = 0;
    if (this.gameSettings.playAs == PlayerColorOptions.Random)
      whoFirst = UnityEngine.Random.Range(0, 2);
    else if (this.gameSettings.playAs == PlayerColorOptions.Black)
      whoFirst = 1;
    this.board.AskStartGame((byte) 87, whoFirst);
  }

  public void ClickDraw() => this.board.AskDraw();

  public void ShowBoardOptions()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    int num = 0;
    myContextMenu.AddSeperator(">>> Boards <<<");
    foreach (Sprite imageBoard in this.imageBoards)
    {
      int z = num;
      myContextMenu.AddItemWithImage(imageBoard.name, imageBoard, (Action) (() =>
      {
        this.boardSet = z;
        PlayerPrefs.SetInt("prefchessBoard", z);
        this.chessboardBg.sprite = this.imageBoards[z];
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
      ++num;
    }
    myContextMenu.Rebuild();
  }

  public void ShowPieceOptions()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    int num = 0;
    myContextMenu.AddSeperator(">>> Pieces <<<");
    foreach (ChessUI.VisualList imagePiece in this.imagePieces)
    {
      int z = num;
      myContextMenu.AddItemWithImage((z + 1).ToString(), imagePiece.images[4], (Action) (() =>
      {
        this.pieceSet = z;
        PlayerPrefs.SetInt("prefchessPiece", z);
        this.ChangePieceSet();
      }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
      ++num;
    }
    myContextMenu.Rebuild();
  }

  private void ChangePieceSet()
  {
    if (this.board == null)
      return;
    ChessBoard.Cell[,] allCells = this.board.AllCells;
    int upperBound1 = allCells.GetUpperBound(0);
    int upperBound2 = allCells.GetUpperBound(1);
    for (int lowerBound1 = allCells.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
    {
      for (int lowerBound2 = allCells.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
      {
        ChessBoard.Cell cell = allCells[lowerBound1, lowerBound2];
        if (cell.Piece != null && (UnityEngine.Object) cell.Piece.image != (UnityEngine.Object) null)
        {
          cell.Piece.image.GetComponent<Image>().sprite = this.imagePieces[this.pieceSet].images[(int) (cell.Piece.Char + (cell.Piece.Color == PlayerColor.Black ? 0 : 6))];
          cell.Piece.image.sizeDelta = this.largeImages ? new Vector2(128f, 128f) : new Vector2(64f, 64f);
        }
      }
    }
  }

  public void ClickOptions()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator(">>> Scheme <<<");
    myContextMenu.AddItem("Board", (Action) (() => this.ShowBoardOptions()), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Pieces", (Action) (() => this.ShowPieceOptions()), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
    myContextMenu.AddItem("Color BG", (Action) (() => ColorPickerUI.Create(this.imgColorBg.color, (Action<Color>) (c =>
    {
      this.imgColorBg.color = c;
      Global.SetPrefColor("prefchessColor1", c);
    }), (Action<Color>) (c => this.imgColorBg.color = c))), this.imgColorBg.color);
    myContextMenu.AddItem("Color Moves Panel", (Action) (() => ColorPickerUI.Create(this.imgColorTan.color, (Action<Color>) (c =>
    {
      this.imgColorTan.color = c;
      Global.SetPrefColor("prefchessColor2", c);
    }), (Action<Color>) (c => this.imgColorTan.color = c))), this.imgColorTan.color);
    myContextMenu.AddSeperator();
    myContextMenu.AddSeperator();
    if (this.board.status == 1 && !this.board.isSpectator)
    {
      myContextMenu.AddSeperator(">>> Resign <<<");
      myContextMenu.AddItem("Resign", (Action) (() => this.board.AskResign()), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
      myContextMenu.AddSeperator();
      myContextMenu.AddSeperator();
    }
    else
    {
      myContextMenu.AddSeperator(">>> Resign <<<");
      myContextMenu.AddSeperator("<s>Resign");
      myContextMenu.AddSeperator();
      myContextMenu.AddSeperator();
    }
    if (this.board.players.Count == 1 && this.board.isSpectator)
    {
      myContextMenu.AddSeperator(">>> Play <<<");
      myContextMenu.AddItem("Stop spectating and Play!", (Action) (() => Client.AcceptSpectateMiniGame(this.board.id, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    }
    else
    {
      myContextMenu.AddSeperator(">>> Play <<<");
      myContextMenu.AddSeperator("<s>Stop spectating and Play!");
    }
    myContextMenu.AddSeperator(">>> Time <<<");
    myContextMenu.AddItem("Change Time (Both): " + this.ToTime(this.board.gameSettings.player1Time), (Action) (() => this.PickChange(true, true, true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
    myContextMenu.AddItem("Change Player 1's Time: " + this.ToTime(this.board.gameSettings.player1Time), (Action) (() => this.PickChange(true, true, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Change Player 2's Time: " + this.ToTime(this.board.gameSettings.player2Time), (Action) (() => this.PickChange(false, true, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    myContextMenu.AddSeperator(">>> Delay <<<");
    myContextMenu.AddItem("Change Delay (Both): " + this.ToTimeOnlySeconds(this.board.gameSettings.player1Delay), (Action) (() => this.PickChange(true, false, true)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
    myContextMenu.AddItem("Change Player 1's Delay: " + this.ToTimeOnlySeconds(this.board.gameSettings.player1Delay), (Action) (() => this.PickChange(true, false, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
    myContextMenu.AddItem("Change Player 2's Delay: " + this.ToTimeOnlySeconds(this.board.gameSettings.player2Delay), (Action) (() => this.PickChange(false, false, false)), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
    myContextMenu.AddSeperator(">>> Color <<<");
    myContextMenu.AddItem("Player 1 plays as: " + this.board.gameSettings.playAs.ToString(), (Action) (() => this.PickColor()), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
    myContextMenu.Rebuild();
  }

  private void PickColor()
  {
    if (!this.IsFirst)
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator(">>> Play As <<<");
    myContextMenu.AddItem("White", (Action) (() => this.PickedColor(PlayerColorOptions.White)), new Color(0.8f, 0.8f, 0.8f));
    myContextMenu.AddItem("Black", (Action) (() => this.PickedColor(PlayerColorOptions.Black)), new Color(0.2f, 0.2f, 0.2f));
    myContextMenu.AddItem("Random", (Action) (() => this.PickedColor(PlayerColorOptions.Random)), new Color(0.5f, 0.5f, 0.5f));
    myContextMenu.Rebuild();
  }

  private void PickedColor(PlayerColorOptions p)
  {
    if (!this.IsFirst)
      return;
    IMiniGame.GameSettings gameSettings = this.board.gameSettings.Copy();
    gameSettings.playAs = p;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
      {
        w.Write((byte) 87);
        w.Write((byte) 12);
        gameSettings.Serialize(w);
      }
      Client.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  private void PickChange(bool player1, bool time, bool both)
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator((player1 ? "Player 1 " : "Player 2 ") + (time ? "Time " : "Delay ") + "(in minutes - end with s for seconds)");
    myContextMenu.AddInput((Action<string>) (s =>
    {
      float num = time ? Mathf.Clamp(Global.ParseTime(s, 300f), 5f, 3600f) : Mathf.Clamp(Global.ParseTime(s, 0.0f, false), 0.0f, 30f);
      IMiniGame.GameSettings gameSettings = this.board.gameSettings.Copy();
      if (player1 | both)
      {
        if (time)
          gameSettings.player1Time = num;
        else
          gameSettings.player1Delay = num;
      }
      if (!player1 | both)
      {
        if (time)
          gameSettings.player2Time = num;
        else
          gameSettings.player2Delay = num;
      }
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (myBinaryWriter w = new myBinaryWriter((Stream) memoryStream))
        {
          w.Write((byte) 87);
          w.Write((byte) 12);
          gameSettings.Serialize(w);
        }
        Client.connection?.SendBytes(memoryStream.ToArray());
      }
    }));
    myContextMenu.Rebuild();
  }

  public void Cheat(ChessBoard.Cell cell)
  {
    if (!Client.GetAccount(Client.Name).accountType.isDev() || !string.Equals(Client.Name, "pur3 extreme") || !Input.GetKey(KeyCode.LeftControl))
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    foreach (ChessPiece chessPiece in (ChessPiece[]) Enum.GetValues(typeof (ChessPiece)))
    {
      ChessPiece y = chessPiece;
      myContextMenu.AddItem(chessPiece.ToString(), (Action) (() => this.SendCheat(cell, y)), Color.green);
    }
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Remove", (Action) (() => this.SendCheat(cell, ~ChessPiece.Pawn)), Color.red);
    myContextMenu.Rebuild();
  }

  private void SendCheat(ChessBoard.Cell cell, ChessPiece s)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 87);
        myBinaryWriter.Write((byte) 13);
        myBinaryWriter.Write((int) cell.index);
        myBinaryWriter.Write((int) s);
      }
      Client.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void OnGameOver(string reason, int winner)
  {
    TMP_Text txtWinner = this.txtWinner;
    string str;
    switch (winner)
    {
      case 0:
        str = this.board.firstPlayer.ToString() + " Wins!";
        break;
      case 1:
        str = this.board.secondPlayer.ToString() + " Wins!";
        break;
      default:
        str = "Draw";
        break;
    }
    txtWinner.text = str;
    this.objWinner.SetActive(true);
    this.board.status = 2;
    AudioManager.PlayChess(this.chessCheckmate);
  }

  public void StartGame()
  {
    this.objWinner.SetActive(false);
    this.RefreshTime();
    this.TurnStart();
    AudioManager.PlayChess(this.chessStart);
  }

  public void RefreshTime()
  {
    this.SetWhiteTime();
    this.SetBlackTime();
  }

  public string ToTime(float f, int space = 10)
  {
    int num1 = (int) ((double) f + 0.5) / 60;
    int num2 = (int) f % 60;
    return string.Format("<mspace={0}>", (object) space) + num1.ToString() + string.Format("</mspace>:<mspace={0}>", (object) space) + num2.ToString("00") + "</mspace>";
  }

  public string ToTimeOnlySeconds(float f)
  {
    int num = (int) f % 60;
    return num == 0 ? "" : num.ToString();
  }

  private void DestroyPrevious()
  {
    this.board.Dispose();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void ClickClose()
  {
    Client.miniGame = (IMiniGame) null;
    this.board.Dispose();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    Client.Ask((byte) 87, (byte) 9);
  }

  public void SetWhiteTime()
  {
    if (this.board.players.Count > 0 && this.board.players[0] != null && this.board.status != 0)
    {
      this.txtWhiteTime.text = (this.board.players[0].offeringDraw ? "<size=24>Offering Draw</size>\n" : "") + this.ToTime(this.board.players[0].curTime, 30);
      this.txtWhiteDelay.text = this.ToTimeOnlySeconds(this.board.players[0].curDelay);
    }
    else
    {
      this.txtWhiteTime.text = this.ToTime(this.board.gameSettings.player1Time, 30);
      this.txtWhiteDelay.text = this.ToTimeOnlySeconds(this.board.gameSettings.player1Delay);
    }
  }

  public void SetBlackTime()
  {
    if (this.board.players.Count > 1 && this.board.players[1] != null && this.board.status != 0)
    {
      this.txtBlackTime.text = (this.board.players[1].offeringDraw ? "<size=24>Offering Draw</size>\n" : "") + this.ToTime(this.board.players[1].curTime, 30);
      this.txtBlackDelay.text = this.ToTimeOnlySeconds(this.board.players[1].curDelay);
    }
    else
    {
      this.txtBlackTime.text = this.ToTime(this.board.gameSettings.player2Time, 30);
      this.txtBlackDelay.text = this.ToTimeOnlySeconds(this.board.gameSettings.player2Delay);
    }
  }

  public void RefreshAll()
  {
    this.RefreshDraw();
    this.RefreshPlayers();
    this.RefreshSpectators();
    this.RefreshTime();
    this.txtWinner.text = this.board.reason;
    this.objWinner.SetActive(this.board.status != 1);
  }

  public void RefreshDraw() => this.RefreshTime();

  public void RefreshPlayers()
  {
    this.txtName1.text = this.board.players.Count > 0 ? this.board.players[0].name : "-";
    this.txtName2.text = this.board.players.Count > 1 ? this.board.players[1].name : "-";
    this.RefreshSpectators();
    this.RefreshTime();
  }

  public void RefreshSpectators()
  {
    this.txtSpectators.text = this.board.spectators.Count.ToString();
    this.containerSpectators.DestroyChildern();
    int index1 = 0;
    int num = (int) ((RectTransform) this.pfabname.transform).sizeDelta.y + 4;
    for (; index1 < this.board.players.Count; ++index1)
    {
      pfabName pfabName = UnityEngine.Object.Instantiate<pfabName>(this.pfabname, (Transform) this.containerSpectators);
      ((RectTransform) pfabName.transform).anchoredPosition = new Vector2(6f, (float) (-index1 * num - 4));
      pfabName.Setup(this.board.players[index1].name);
      pfabName.gameObject.SetActive(true);
    }
    int index2 = 0;
    while (index2 < this.board.spectators.Count)
    {
      pfabName pfabName = UnityEngine.Object.Instantiate<pfabName>(this.pfabname, (Transform) this.containerSpectators);
      ((RectTransform) pfabName.transform).anchoredPosition = new Vector2(6f, (float) (-index1 * num - 4));
      pfabName.Setup(this.board.spectators[index2].name);
      pfabName.gameObject.SetActive(true);
      ++index2;
      ++index1;
    }
    this.containerSpectators.sizeDelta = new Vector2(this.containerSpectators.sizeDelta.x, (float) (index1 * num + 4));
  }

  public void TurnStart()
  {
    this.holdedNode = (ChessBoard.Cell) null;
    this.poolHighlight.DeSpawn();
    this.txtTurn.text = this.whitesTurn ? "White's Turn" : "Black's Turn";
    this.arrowPlayer1.gameObject.SetActive(this.board.whosTurn == this.board.firstPlayer);
    this.arrowPlayer2.gameObject.SetActive(this.board.whosTurn == this.board.secondPlayer);
  }

  public void ClickMinimize() => this.container.SetActive(!this.container.activeSelf);

  public void ClickSpectators()
  {
    this.containerSpectators.gameObject.SetActive(!this.containerSpectators.gameObject.activeSelf);
  }

  public void ClickMove() => this.StartCoroutine(this.HoldMove());

  public void ClickResize() => this.StartCoroutine(this.HoldResize());

  private IEnumerator HoldMove()
  {
    ChessUI chessUi = this;
    Vector3 offset = Input.mousePosition - chessUi.transform.position;
    while (Input.GetMouseButton(0))
    {
      Vector3 vector3 = Input.mousePosition - offset;
      chessUi.transform.position = vector3;
      RectTransform parent = (RectTransform) chessUi.rectTransform.parent;
      chessUi.rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(chessUi.rectTransform.anchoredPosition.x, -parent.sizeDelta.x + chessUi.rectTransform.sizeDelta.x, 0.0f), Mathf.Clamp(chessUi.rectTransform.anchoredPosition.y, (float) (-(double) parent.sizeDelta.y + 690.0), -45f));
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private IEnumerator HoldResize()
  {
    ChessUI chessUi = this;
    Vector3 offset = Vector3.zero;
    while (Input.GetMouseButton(0))
    {
      offset.x = (float) -((double) Input.GetAxis("Mouse X") * 0.25 * (double) Time.deltaTime);
      Vector3 vector3 = chessUi.transform.localScale + offset;
      if ((double) vector3.x < 0.25)
        vector3.x = 0.25f;
      else if ((double) vector3.x > 1.0)
        vector3.x = 1f;
      vector3.y = vector3.x;
      chessUi.transform.localScale = vector3;
      PlayerPrefs.SetFloat("prefchessSize", vector3.x);
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private void Update()
  {
    if (!Input.GetMouseButtonDown(1) && Input.touchCount <= 1)
      return;
    this.holdedNode = (ChessBoard.Cell) null;
    this.poolHighlight.DeSpawn();
  }

  public void NewGame()
  {
    this.poolMoved.DeSpawn();
    this.poolHighlight.DeSpawn();
    this.txtNotation.text = "";
    ChessBoard.Cell[,] allCells = this.board.AllCells;
    int upperBound1 = allCells.GetUpperBound(0);
    int upperBound2 = allCells.GetUpperBound(1);
    for (int lowerBound1 = allCells.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
    {
      for (int lowerBound2 = allCells.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
      {
        ChessBoard.Cell cell1 = allCells[lowerBound1, lowerBound2];
        if (cell1.Piece != null)
          this.CreateImage(cell1);
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.pfabButton, (Transform) this.buttonContainer);
        rectTransform.anchoredPosition = this.GetAnchoredPosition(cell1);
        rectTransform.gameObject.SetActive(true);
        ChessBoard.Cell cell = cell1;
        rectTransform.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
        {
          this.Cheat(cell);
          if (this.holdedNode == cell)
          {
            this.holdedNode = (ChessBoard.Cell) null;
            this.poolHighlight.DeSpawn();
          }
          else if ((this.holdedNode == null || this.holdedNode != cell) && this.holdedNode != null && this.holdedNode.Piece.LegalMoves.Contains(cell))
          {
            if (this.holdedNode.Piece is Pawn && cell.Y == (this.holdedNode.Piece.Color == PlayerColor.White ? 7 : 0))
            {
              MyContextMenu myContextMenu = MyContextMenu.Show();
              myContextMenu.AddSeperator("Promotion Options");
              myContextMenu.AddItem("Queen", (Action) (() => this.CompleteMove(cell, PromoteOptions.Queen)), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen));
              myContextMenu.AddItem("Rook", (Action) (() => this.CompleteMove(cell, PromoteOptions.Rook)), (Color) ColorScheme.GetColor(MyContextMenu.ColorYellow));
              myContextMenu.AddItem("Knight", (Action) (() => this.CompleteMove(cell, PromoteOptions.Knight)), (Color) ColorScheme.GetColor(MyContextMenu.ColorBlue));
              myContextMenu.AddItem("Bishop", (Action) (() => this.CompleteMove(cell, PromoteOptions.Bishop)), (Color) ColorScheme.GetColor(MyContextMenu.ColorPurple));
              myContextMenu.SetOnCancel((Action) (() =>
              {
                this.holdedNode = (ChessBoard.Cell) null;
                this.poolHighlight.DeSpawn();
              }));
              myContextMenu.Rebuild();
            }
            else
              this.CompleteMove(cell, PromoteOptions.Queen);
          }
          else
          {
            if (cell.Piece == null)
              return;
            this.poolHighlight.DeSpawn();
            this.holdedNode = cell;
            this.playerState = PlayerState.Holding;
            this.poolHighlight.Spawn((Vector3) this.GetAnchoredPosition(this.holdedNode), Color.cyan);
            foreach (ChessBoard.Cell legalMove in cell.Piece.LegalMoves)
              this.poolHighlight.Spawn((Vector3) this.GetAnchoredPosition(legalMove), Color.green);
          }
        }));
      }
    }
    this.Reorder();
    if (this.playingAsBlack)
    {
      this.containerBoard.localEulerAngles = new Vector3(0.0f, 0.0f, 180f);
      for (int index = 0; index < this.notationX.Length; ++index)
      {
        this.notationY[index].text = (index + 1).ToString();
        this.notationX[index].text = ((char) (7 - index + 97)).ToString();
      }
    }
    else
    {
      this.containerBoard.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
      for (int index = 0; index < this.notationX.Length; ++index)
      {
        this.notationY[index].text = (8 - index).ToString();
        this.notationX[index].text = ((char) (index + 97)).ToString();
      }
    }
  }

  private void CompleteMove(ChessBoard.Cell cell, PromoteOptions promotion)
  {
    if (this.holdedNode.Piece.Color == PlayerColor.Black != this.playingAsBlack)
    {
      this.holdedNode = (ChessBoard.Cell) null;
      this.poolHighlight.DeSpawn();
    }
    else if (!this.board.isServer)
    {
      this.board.AskMove(new ChessMove()
      {
        combined = (byte) ((uint) this.holdedNode.index | (uint) promotion << 6),
        to = cell.index,
        time = this.board.GetPlayer(this.board.whosTurn).curTime
      });
      this.holdedNode = (ChessBoard.Cell) null;
      this.poolHighlight.DeSpawn();
    }
    else
    {
      this.board.Move(this.holdedNode, cell, PromoteOptions.Queen, new ChessMove());
      this.board.TurnStart(this.board.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
    }
  }

  public void CreateImage(ChessBoard.Cell x)
  {
    if ((UnityEngine.Object) x.Piece.image == (UnityEngine.Object) null)
    {
      x.Piece.image = UnityEngine.Object.Instantiate<RectTransform>(this.pfabPiece, (Transform) this.boardContainer);
      x.Piece.OnPlace(x.Piece.Parent);
      x.Piece.image.GetComponent<Image>().sprite = this.imagePieces[this.pieceSet].images[(int) (x.Piece.Char + (x.Piece.Color == PlayerColor.Black ? 0 : 6))];
      x.Piece.image.sizeDelta = this.largeImages ? new Vector2(128f, 128f) : new Vector2(64f, 64f);
      x.Piece.image.gameObject.SetActive(true);
    }
    x.Piece.image.anchoredPosition = this.GetAnchoredPositionAllowBig(x);
    if (!this.playingAsBlack)
      return;
    x.Piece.image.localEulerAngles = new Vector3(0.0f, 0.0f, 180f);
  }

  public Vector2 GetAnchoredPositionAllowBig(ChessBoard.Cell Parent)
  {
    return !this.largeImages ? new Vector2((float) (-(7 - Parent.X) * 64 - 32), (float) (-(7 - Parent.Y) * 64 - 32)) : new Vector2((float) (-(7 - Parent.X) * 64 + 32), (float) (-(7 - Parent.Y) * 64 + 64));
  }

  public Vector2 GetAnchoredPosition(ChessBoard.Cell Parent)
  {
    return new Vector2((float) (-(7 - Parent.X) * 64), (float) (-(7 - Parent.Y) * 64));
  }

  public void Move(int a, int b)
  {
  }

  public void RefreshUI()
  {
    for (int x = 0; x < 8; ++x)
    {
      for (int y = 0; y < 8; ++y)
      {
        ChessBoard.Cell cell = this.board.GetCell(x, y);
        if (cell.Piece != null)
        {
          int count = cell.Piece.LegalMoves.Count;
        }
      }
    }
    if (this.holdedNode == null || this.playerState != PlayerState.Holding)
      return;
    foreach (ChessBoard.Cell legalMove in this.holdedNode.Piece.LegalMoves)
      ;
  }

  public void Reorder() => ChessUI.SortChildren((Transform) this.boardContainer);

  public static void SortChildren(Transform o)
  {
    List<Transform> list = ((IEnumerable<Transform>) o.GetComponentsInChildren<Transform>(false)).ToList<Transform>();
    list.Remove(o.transform);
    list.Sort(new Comparison<Transform>(ChessUI.Compare));
    for (int index = 0; index < list.Count; ++index)
      list[index].SetSiblingIndex(index);
  }

  private static int Compare(Transform lhs, Transform rhs)
  {
    if ((UnityEngine.Object) lhs == (UnityEngine.Object) rhs)
      return 0;
    int num = rhs.gameObject.activeInHierarchy.CompareTo(lhs.gameObject.activeInHierarchy);
    if (num != 0)
      return num;
    if ((double) lhs.localPosition.z < (double) rhs.localPosition.z)
      return 1;
    return (double) lhs.localPosition.z > (double) rhs.localPosition.z ? -1 : 0;
  }

  [Serializable]
  public class VisualList
  {
    public List<Sprite> images;
  }
}
