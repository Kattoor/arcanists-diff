// Decompiled with JetBrains decompiler
// Type: UniClipboard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
