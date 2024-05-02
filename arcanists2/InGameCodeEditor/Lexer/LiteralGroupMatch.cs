// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.LiteralGroupMatch
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
