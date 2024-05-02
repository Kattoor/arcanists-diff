

using ChessConsole;
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
public class RPSTBGUI : MonoBehaviour, IMiniGameUI
{
  public RectTransform rectTransform;
  public RectTransform rectMove;
  public GameObject container;
  public Image imgColorBg;
  public Image imgScissors;
  public Image imgRock;
  public Image imgPaper;
  public List<Sprite> imageBoards = new List<Sprite>();
  public List<RPSTBGUI.VisualList> imagePieces = new List<RPSTBGUI.VisualList>();
  public RectTransform pfabPiece;
  public RectTransform pfabButton;
  public RectTransform boardContainer;
  public RectTransform buttonContainer;
  public RectTransform containerBoard;
  public PoolColoredImage poolHighlight;
  public PoolColoredImage poolMoved;
  public Image CheckersBg;
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
  private RPSTBGBoard.Cell holdedNode;
  internal RPSTBGBoard board;
  internal bool useAudio = true;
  public const string prefColor1 = "prefheckersColor1";
  public const string prefScale = "prefheckersSize";

  public static RPSTBGUI Instance { get; private set; }

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

  public static void Create(RPSTBGBoard b)
  {
    if ((UnityEngine.Object) RPSTBGUI.Instance != (UnityEngine.Object) null)
      RPSTBGUI.Instance.DestroyPrevious();
    RPSTBGUI.Instance = Controller.Instance.CreateAndApply<RPSTBGUI>(Controller.Instance.RPSTBGui, Controller.Instance.transform);
    b.isClient = true;
    b.isSpectator = b.players.FindIndex((Predicate<IMiniGame.Player>) (x => string.Equals(x.name, Client.Name))) == -1;
    RPSTBGUI.Instance.board = b;
    b.ui = RPSTBGUI.Instance;
    b.uiObject = RPSTBGUI.Instance.gameObject;
    RPSTBGUI.Instance.NewGame();
    RPSTBGUI.Instance.buttonDraw.SetActive(!b.isSpectator);
    RPSTBGUI.Instance.useAudio = Global.GetPrefBool("prefchessaudio", true);
    RPSTBGUI.Instance.buttonAudio.AlwaysOn = !RPSTBGUI.Instance.useAudio;
    RPSTBGUI.Instance.board.gameObject = (IMiniGameUI) RPSTBGUI.Instance;
  }

  public void Despawn()
  {
    this.imgScissors.gameObject.SetActive(false);
    this.imgRock.gameObject.SetActive(false);
    this.imgPaper.gameObject.SetActive(false);
  }

  public void ToggleAudio()
  {
    this.useAudio = !this.useAudio;
    Global.SetPrefBool("prefchessaudio", this.useAudio);
    this.buttonAudio.AlwaysOn = !this.useAudio;
  }

  private void Awake()
  {
    RPSTBGUI.Instance = this;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void OnDestroy()
  {
    if ((UnityEngine.Object) RPSTBGUI.Instance == (UnityEngine.Object) this)
      RPSTBGUI.Instance = (RPSTBGUI) null;
    DiscordIntergration.Instance?.UpdateNoUpdate();
  }

  private void Start()
  {
    if (this.board == null)
    {
      this.board = new RPSTBGBoard();
      this.board.ui = this;
      this.board.uiObject = this.gameObject;
      this.NewGame();
    }
    this.RefreshTime();
    this.RefreshPlayers();
    this.RefreshSpectators();
    float num = Mathf.Clamp(PlayerPrefs.GetFloat("prefheckersSize", 0.75f), 0.25f, 1f);
    this.transform.localScale = new Vector3(num, num, 1f);
    this.ChangePieceSet();
    this.imgColorBg.color = (Color) Global.GetPrefColor("prefheckersColor1", this.imgColorBg.color);
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

  private void ChangePieceSet()
  {
    if (this.board == null)
      return;
    RPSTBGBoard.Cell[,] allCells = this.board.AllCells;
    int upperBound1 = allCells.GetUpperBound(0);
    int upperBound2 = allCells.GetUpperBound(1);
    for (int lowerBound1 = allCells.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
    {
      for (int lowerBound2 = allCells.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
      {
        RPSTBGBoard.Cell cell = allCells[lowerBound1, lowerBound2];
        if (cell.Piece != null && (UnityEngine.Object) cell.Piece.image != (UnityEngine.Object) null)
        {
          cell.Piece.image.GetComponent<Image>().sprite = this.imagePieces[0].images[(int) (cell.Piece.Char + (cell.Piece.Color == PlayerColor.Black ? 0 : 4))];
          cell.Piece.image.sizeDelta = this.largeImages ? new Vector2(128f, 128f) : new Vector2(64f, 64f);
        }
      }
    }
  }

  public void ClickOptions()
  {
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddSeperator(">>> Scheme <<<");
    myContextMenu.AddItem("Color BG", (Action) (() => ColorPickerUI.Create(this.imgColorBg.color, (Action<Color>) (c =>
    {
      this.imgColorBg.color = c;
      Global.SetPrefColor("prefheckersColor1", c);
    }), (Action<Color>) (c => this.imgColorBg.color = c))), this.imgColorBg.color);
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
    myContextMenu.AddItem("Blue", (Action) (() => this.PickedColor(PlayerColorOptions.White)), new Color(0.0f, 0.5f, 1f));
    myContextMenu.AddItem("Red", (Action) (() => this.PickedColor(PlayerColorOptions.Black)), new Color(0.9f, 0.0f, 0.0f));
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

  public void Cheat(RPSTBGBoard.Cell cell)
  {
    if (!Client.GetAccount(Client.Name).accountType.isDev() || !string.Equals(Client.Name, "pur3 extreme") || !Input.GetKey(KeyCode.LeftControl))
      return;
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.AddItem("Pawn", (Action) (() => this.SendCheat(cell, ChessPiece.Pawn)), Color.green);
    myContextMenu.AddItem("King", (Action) (() => this.SendCheat(cell, ChessPiece.Bishop)), Color.green);
    myContextMenu.AddSeperator();
    myContextMenu.AddItem("Remove", (Action) (() => this.SendCheat(cell, ~ChessPiece.Pawn)), Color.red);
    myContextMenu.Rebuild();
  }

  private void SendCheat(RPSTBGBoard.Cell cell, ChessPiece s)
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
        str = this.board.firstColor + " Wins!";
        break;
      case 1:
        str = this.board.secondColor + " Wins!";
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
    this.holdedNode = (RPSTBGBoard.Cell) null;
    this.poolHighlight.DeSpawn();
    this.Despawn();
    this.txtTurn.text = this.whitesTurn ? "Blue's Turn" : "Red's Turn";
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
    RPSTBGUI rpstbgui = this;
    Vector3 offset = Input.mousePosition - rpstbgui.transform.position;
    while (Input.GetMouseButton(0))
    {
      Vector3 vector3 = Input.mousePosition - offset;
      rpstbgui.transform.position = vector3;
      RectTransform parent = (RectTransform) rpstbgui.rectTransform.parent;
      rpstbgui.rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(rpstbgui.rectTransform.anchoredPosition.x, -parent.sizeDelta.x + rpstbgui.rectTransform.sizeDelta.x, 0.0f), Mathf.Clamp(rpstbgui.rectTransform.anchoredPosition.y, (float) (-(double) parent.sizeDelta.y + 690.0), -45f));
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private IEnumerator HoldResize()
  {
    RPSTBGUI rpstbgui = this;
    Vector3 offset = Vector3.zero;
    while (Input.GetMouseButton(0))
    {
      offset.x = (float) -((double) Input.GetAxis("Mouse X") * 0.25 * (double) Time.deltaTime);
      Vector3 vector3 = rpstbgui.transform.localScale + offset;
      if ((double) vector3.x < 0.25)
        vector3.x = 0.25f;
      else if ((double) vector3.x > 1.0)
        vector3.x = 1f;
      vector3.y = vector3.x;
      rpstbgui.transform.localScale = vector3;
      PlayerPrefs.SetFloat("prefheckersSize", vector3.x);
      yield return (object) new WaitForEndOfFrame();
    }
  }

  private void Update()
  {
    if (!Input.GetMouseButtonDown(1) && Input.touchCount <= 1)
      return;
    this.holdedNode = (RPSTBGBoard.Cell) null;
    this.poolHighlight.DeSpawn();
    this.Despawn();
  }

  public void NewGame()
  {
    this.poolMoved.DeSpawn();
    this.poolHighlight.DeSpawn();
    this.Despawn();
    RPSTBGBoard.Cell[,] allCells = this.board.AllCells;
    int upperBound1 = allCells.GetUpperBound(0);
    int upperBound2 = allCells.GetUpperBound(1);
    for (int lowerBound1 = allCells.GetLowerBound(0); lowerBound1 <= upperBound1; ++lowerBound1)
    {
      for (int lowerBound2 = allCells.GetLowerBound(1); lowerBound2 <= upperBound2; ++lowerBound2)
      {
        RPSTBGBoard.Cell cell1 = allCells[lowerBound1, lowerBound2];
        if (cell1.Piece != null)
          this.CreateImage(cell1);
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.pfabButton, (Transform) this.buttonContainer);
        rectTransform.anchoredPosition = this.GetAnchoredPosition(cell1);
        rectTransform.gameObject.SetActive(true);
        RPSTBGBoard.Cell cell = cell1;
        rectTransform.GetComponent<UIOnHover>().onClick.AddListener((UnityAction) (() =>
        {
          this.Cheat(cell);
          this.ClickCell(cell);
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
        this.notationX[index].text = ((char) (5 - index + 97)).ToString();
      }
    }
    else
    {
      this.containerBoard.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
      for (int index = 0; index < this.notationX.Length; ++index)
      {
        this.notationY[index].text = (6 - index).ToString();
        this.notationX[index].text = ((char) (index + 97)).ToString();
      }
    }
  }

  internal void ClickCell(RPSTBGBoard.Cell cell)
  {
    if (this.holdedNode == cell)
    {
      this.holdedNode = (RPSTBGBoard.Cell) null;
      this.poolHighlight.DeSpawn();
      this.Despawn();
    }
    else if ((this.holdedNode == null || this.holdedNode != cell) && this.holdedNode != null && this.holdedNode.Piece.LegalMoves.Contains(cell))
    {
      this.CompleteMove(cell, PromoteOptions.Queen);
    }
    else
    {
      if (cell.Piece == null)
        return;
      this.poolHighlight.DeSpawn();
      this.Despawn();
      this.holdedNode = cell;
      this.playerState = PlayerState.Holding;
      this.poolHighlight.Spawn((Vector3) this.GetAnchoredPosition(this.holdedNode), Color.cyan);
      foreach (RPSTBGBoard.Cell legalMove in cell.Piece.LegalMoves)
      {
        this.poolHighlight.Spawn((Vector3) this.GetAnchoredPosition(legalMove), Color.green);
        if (cell.Piece.Char == ChessPiece.Rook)
        {
          if (legalMove == this.board.RedPiece(ChessPiece.Pawn))
            this.Activate(this.imgScissors, legalMove);
          else if (legalMove == this.board.RedPiece(ChessPiece.Bishop))
            this.Activate(this.imgRock, legalMove);
          else if (legalMove == this.board.RedPiece(ChessPiece.Knight))
            this.Activate(this.imgPaper, legalMove);
          else if (legalMove == this.board.BluePiece(ChessPiece.Pawn))
            this.Activate(this.imgScissors, legalMove);
          else if (legalMove == this.board.BluePiece(ChessPiece.Bishop))
            this.Activate(this.imgRock, legalMove);
          else if (legalMove == this.board.BluePiece(ChessPiece.Knight))
            this.Activate(this.imgPaper, legalMove);
        }
      }
    }
  }

  private void Activate(Image i, RPSTBGBoard.Cell move)
  {
    i.rectTransform.anchoredPosition = this.GetAnchoredPositionAllowBig(move);
    i.rectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, this.playingAsBlack ? 180f : 0.0f);
    i.gameObject.SetActive(true);
  }

  private void CompleteMove(RPSTBGBoard.Cell cell, PromoteOptions promotion)
  {
    if (this.holdedNode.Piece.Color == PlayerColor.Black != this.playingAsBlack)
    {
      this.holdedNode = (RPSTBGBoard.Cell) null;
      this.poolHighlight.DeSpawn();
      this.Despawn();
    }
    else if (!this.board.isServer)
    {
      this.board.AskMove(new ChessMove()
      {
        combined = (byte) ((uint) this.holdedNode.index | (uint) promotion << 6),
        to = cell.index,
        time = this.board.GetPlayer(this.board.whosTurn).curTime
      });
      this.holdedNode = (RPSTBGBoard.Cell) null;
      this.poolHighlight.DeSpawn();
      this.Despawn();
    }
    else
    {
      this.board.Move(this.holdedNode, cell, PromoteOptions.Queen, new ChessMove());
      this.board.TurnStart(this.board.whosTurn == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
    }
  }

  public void CreateImage(RPSTBGBoard.Cell x)
  {
    if ((UnityEngine.Object) x.Piece.image == (UnityEngine.Object) null)
    {
      x.Piece.image = UnityEngine.Object.Instantiate<RectTransform>(this.pfabPiece, (Transform) this.boardContainer);
      x.Piece.OnPlace(x.Piece.Parent);
      x.Piece.image.GetComponent<Image>().sprite = this.imagePieces[0].images[(int) (x.Piece.Char + (x.Piece.Color == PlayerColor.Black ? 0 : 4))];
      x.Piece.image.sizeDelta = this.largeImages ? new Vector2(128f, 128f) : new Vector2(64f, 64f);
      x.Piece.image.gameObject.SetActive(true);
    }
    x.Piece.image.anchoredPosition = this.GetAnchoredPositionAllowBig(x);
    if (!this.playingAsBlack)
      return;
    x.Piece.image.localEulerAngles = new Vector3(0.0f, 0.0f, 180f);
  }

  public Vector2 GetAnchoredPositionAllowBig(RPSTBGBoard.Cell Parent)
  {
    return !this.largeImages ? new Vector2((float) (-(5 - Parent.X) * 64 - 32), (float) (-(5 - Parent.Y) * 64 - 32)) : new Vector2((float) (-(5 - Parent.X) * 64 + 32), (float) (-(5 - Parent.Y) * 64 + 64));
  }

  public Vector2 GetAnchoredPosition(RPSTBGBoard.Cell Parent)
  {
    return new Vector2((float) (-(5 - Parent.X) * 64), (float) (-(5 - Parent.Y) * 64));
  }

  public void Reorder() => RPSTBGUI.SortChildren((Transform) this.boardContainer);

  public static void SortChildren(Transform o)
  {
    List<Transform> list = ((IEnumerable<Transform>) o.GetComponentsInChildren<Transform>(false)).ToList<Transform>();
    list.Remove(o.transform);
    list.Sort(new Comparison<Transform>(RPSTBGUI.Compare));
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
