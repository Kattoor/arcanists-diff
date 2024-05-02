// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.ILexer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace InGameCodeEditor.Lexer
{
  public interface ILexer
  {
    bool EndOfStream { get; }

    char Previous { get; }

    char ReadNext();

    void Rollback(int amount = -1);

    void Commit();

    bool IsSpecialSymbol(char character, SpecialCharacterPosition position = SpecialCharacterPosition.Start);
  }
}
