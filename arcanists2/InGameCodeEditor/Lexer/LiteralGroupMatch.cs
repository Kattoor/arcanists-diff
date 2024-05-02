// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.LiteralGroupMatch
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace InGameCodeEditor.Lexer
{
  [Serializable]
  public sealed class LiteralGroupMatch : MatchLexer
  {
    [NonSerialized]
    private string htmlColor;
    [Tooltip("Should literals inside quotes be highlghted")]
    public bool highlightLiterals = true;
    [Tooltip("The color that all literal strings will be highglighted with")]
    public Color highlightColor = Color.black;

    public bool HasLiteralHighlighting => this.highlightLiterals;

    public override string HTMLColor
    {
      get
      {
        if (this.htmlColor == null)
          this.htmlColor = "<#" + ColorUtility.ToHtmlStringRGB(this.highlightColor) + ">";
        return this.htmlColor;
      }
    }

    public override IEnumerable<char> SpecialStartCharacters
    {
      get
      {
        yield return '"';
        yield return '\'';
      }
    }

    public override IEnumerable<char> SpecialEndCharacters
    {
      get
      {
        yield return '"';
        yield return '\'';
      }
    }

    public override void Invalidate() => this.htmlColor = (string) null;

    public override bool IsImplicitMatch(ILexer lexer)
    {
      if (!this.highlightLiterals || lexer.ReadNext() != '"')
        return false;
      do
        ;
      while (!this.IsClosingQuoteOrEndFile(lexer, lexer.ReadNext()));
      return true;
    }

    private bool IsClosingQuoteOrEndFile(ILexer lexer, char character)
    {
      return lexer.EndOfStream || character == '"';
    }
  }
}
