// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.ILexer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
