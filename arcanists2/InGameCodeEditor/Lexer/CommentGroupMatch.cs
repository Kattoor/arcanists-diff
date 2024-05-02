// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.CommentGroupMatch
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
  public sealed class CommentGroupMatch : MatchLexer
  {
    [NonSerialized]
    private string htmlColor;
    [Tooltip("The special string used to denote the start of a line comment")]
    public string lineCommentStart;
    [Tooltip("The special string used to denote the start of a block comment")]
    public string blockCommentStart;
    [Tooltip("The special string used to denote the end of a block comment")]
    public string blockCommentEnd;
    [Tooltip("The color that all comments will be highlighted with")]
    public Color highlightColor = Color.black;
    [Tooltip("Should line comments be matched before block comments")]
    public bool lineCommentHasPresedence = true;

    public bool HasCommentHighlighting
    {
      get
      {
        return !string.IsNullOrEmpty(this.lineCommentStart) || !string.IsNullOrEmpty(this.blockCommentStart);
      }
    }

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
        if (!string.IsNullOrEmpty(this.lineCommentStart))
          yield return this.lineCommentStart[0];
        if (!string.IsNullOrEmpty(this.blockCommentEnd))
          yield return this.blockCommentEnd[0];
      }
    }

    public override IEnumerable<char> SpecialEndCharacters
    {
      get
      {
        if (!string.IsNullOrEmpty(this.blockCommentEnd))
          yield return this.blockCommentEnd[this.blockCommentEnd.Length - 1];
      }
    }

    public override void Invalidate() => this.htmlColor = (string) null;

    public override bool IsImplicitMatch(ILexer lexer)
    {
      if (this.lineCommentHasPresedence)
      {
        if (this.IsLineCommentMatch(lexer) || this.IsBlockCommentMatch(lexer))
          return true;
      }
      else if (this.IsBlockCommentMatch(lexer) || this.IsLineCommentMatch(lexer))
        return true;
      return false;
    }

    private bool IsLineCommentMatch(ILexer lexer)
    {
      if (!string.IsNullOrEmpty(this.lineCommentStart))
      {
        lexer.Rollback();
        bool flag = true;
        for (int index = 0; index < this.lineCommentStart.Length; ++index)
        {
          if ((int) this.lineCommentStart[index] != (int) lexer.ReadNext())
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          do
            ;
          while (!this.IsEndLineOrEndFile(lexer, lexer.ReadNext()));
          return true;
        }
      }
      return false;
    }

    private bool IsBlockCommentMatch(ILexer lexer)
    {
      if (!string.IsNullOrEmpty(this.blockCommentStart))
      {
        lexer.Rollback();
        bool flag = true;
        for (int index = 0; index < this.blockCommentStart.Length; ++index)
        {
          if ((int) this.blockCommentStart[index] != (int) lexer.ReadNext())
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          do
            ;
          while (!this.IsEndLineOrString(lexer, this.blockCommentEnd));
          return true;
        }
      }
      return false;
    }

    private bool IsEndLineOrEndFile(ILexer lexer, char character)
    {
      return lexer.EndOfStream || character == '\n' || character == '\r';
    }

    private bool IsEndLineOrString(ILexer lexer, string endString)
    {
      for (int index = 0; index < endString.Length && !lexer.EndOfStream; ++index)
      {
        if ((int) endString[index] != (int) lexer.ReadNext())
          return false;
      }
      return true;
    }
  }
}
