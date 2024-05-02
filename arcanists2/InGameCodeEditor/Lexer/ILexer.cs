

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
