// Decompiled with JetBrains decompiler
// Type: UniClipboard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
