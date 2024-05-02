// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.NumberGroupMatch
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace InGameCodeEditor.Lexer
{
  [Serializable]
  public sealed class NumberGroupMatch : MatchLexer
  {
    [NonSerialized]
    private string htmlColor;
    [Tooltip("Should numbers be highlighted")]
    public bool highlightNumbers = true;
    [Tooltip("The color that all numbers will be highlighted with")]
    public Color highlightColor = Color.black;

    public bool HasNumberHighlighting => this.highlightNumbers;

    public override string HTMLColor
    {
      get
      {
        if (this.htmlColor == null)
          this.htmlColor = "<#" + ColorUtility.ToHtmlStringRGB(this.highlightColor) + ">";
        return this.htmlColor;
      }
    }

    public override void Invalidate() => this.htmlColor = (string) null;

    public override bool IsImplicitMatch(ILexer lexer)
    {
      if (!this.highlightNumbers || !char.IsWhiteSpace(lexer.Previous) && !lexer.IsSpecialSymbol(lexer.Previous, SpecialCharacterPosition.End))
        return false;
      bool flag = false;
      while (!lexer.EndOfStream)
      {
        if (this.IsNumberOrDecimalPoint(lexer, lexer.ReadNext()))
        {
          flag = true;
          lexer.Commit();
        }
        else
        {
          lexer.Rollback();
          break;
        }
      }
      return flag;
    }

    private bool IsNumberOrDecimalPoint(ILexer lexer, char character)
    {
      return char.IsNumber(character) || character == '.';
    }
  }
}
