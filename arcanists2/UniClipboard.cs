

#nullable disable
public class UniClipboard
{
  private static IBoard _board;

  private static IBoard board
  {
    get
    {
      if (UniClipboard._board == null)
        UniClipboard._board = (IBoard) new StandardBoard();
      return UniClipboard._board;
    }
  }

  public static void SetText(string str) => UniClipboard.board.SetText(str);

  public static string GetText() => UniClipboard.board.GetText();
}
