// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.CodeLanguageTheme
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using InGameCodeEditor.Lexer;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
namespace InGameCodeEditor
{
  [CreateAssetMenu(fileName = "Code Language Theme", menuName = "InGame Code Editor/Code Language Theme")]
  [Serializable]
  public class CodeLanguageTheme : ScriptableObject
  {
    internal static readonly StringBuilder sharedBuilder = new StringBuilder();
    [NonSerialized]
    private char[] delimiterSymbolCache;
    [NonSerialized]
    private MatchLexer[] matchers;
    public string languageName;
    [TextArea]
    [Tooltip("Any special characters that can act as delimiters when they are immediatley before or after a keyword. Single characters only separated by a space")]
    public string delimiterSymbols;
    public KeywordGroupMatch[] keywordGroups;
    public SymbolGroupMatch symbolGroup;
    public NumberGroupMatch numberGroup;
    public CommentGroupMatch commentGroup;
    public LiteralGroupMatch literalGroup;
    public AutoIndent autoIndent;

    internal char[] DelimiterSymbols
    {
      get
      {
        if (this.delimiterSymbolCache == null)
        {
          string[] strArray = this.delimiterSymbols.Split(' ');
          int length = 0;
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (strArray[index].Length == 1)
              ++length;
          }
          this.delimiterSymbolCache = new char[length];
          int index1 = 0;
          int index2 = 0;
          for (; index1 < strArray.Length; ++index1)
          {
            if (strArray[index1].Length == 1)
            {
              this.delimiterSymbolCache[index2] = strArray[index1][0];
              ++index2;
            }
          }
        }
        return this.delimiterSymbolCache;
      }
    }

    internal MatchLexer[] Matchers
    {
      get
      {
        if (this.matchers == null)
        {
          List<MatchLexer> matchLexerList = new List<MatchLexer>();
          matchLexerList.Add((MatchLexer) this.commentGroup);
          matchLexerList.AddRange((IEnumerable<MatchLexer>) this.keywordGroups);
          matchLexerList.Add((MatchLexer) this.symbolGroup);
          matchLexerList.Add((MatchLexer) this.numberGroup);
          matchLexerList.Add((MatchLexer) this.literalGroup);
          this.matchers = matchLexerList.ToArray();
        }
        return this.matchers;
      }
    }

    internal void Invalidate()
    {
      foreach (MatchLexer keywordGroup in this.keywordGroups)
        keywordGroup.Invalidate();
      this.symbolGroup.Invalidate();
      this.commentGroup.Invalidate();
      this.numberGroup.Invalidate();
      this.literalGroup.Invalidate();
    }
  }
}
