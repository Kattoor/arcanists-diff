

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

#nullable disable
namespace InGameCodeEditor.Lexer
{
  [Serializable]
  public sealed class KeywordGroupMatch : MatchLexer
  {
    private static HashSet<string> shortlist = new HashSet<string>();
    private static Stack<string> removeList = new Stack<string>();
    [NonSerialized]
    private string[] keywordCache;
    [NonSerialized]
    private string htmlColor;
    [HideInInspector]
    public string group = "Keyword Group";
    [TextArea]
    [Tooltip("A number of keywords that will be highlighted. Word strings recommended (no numbers or symbols) separated by a space")]
    public string keywords;
    [Tooltip("The color that all keywords in this group will be highlighted with")]
    public Color highlightColor = Color.black;
    [Tooltip("Should the keyword match be case sensitive")]
    public bool caseSensitive = true;

    public bool HasKeywordHighlighting => !string.IsNullOrEmpty(this.keywords);

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
      this.BuildKeywordCache();
      if (!char.IsWhiteSpace(lexer.Previous) && !lexer.IsSpecialSymbol(lexer.Previous, SpecialCharacterPosition.End))
        return false;
      KeywordGroupMatch.shortlist.Clear();
      int num = 0;
      char b = lexer.ReadNext();
      for (int index = 0; index < this.keywordCache.Length; ++index)
      {
        if (this.CompareChar(this.keywordCache[index][0], b))
          KeywordGroupMatch.shortlist.Add(this.keywordCache[index]);
      }
      if (KeywordGroupMatch.shortlist.Count == 0)
        return false;
      while (!lexer.EndOfStream)
      {
        char ch = lexer.ReadNext();
        ++num;
        if (char.IsWhiteSpace(ch) || lexer.IsSpecialSymbol(ch))
        {
          this.RemoveLongStrings(num);
          lexer.Rollback(1);
          goto label_22;
        }
        else
        {
          foreach (string str in KeywordGroupMatch.shortlist)
          {
            if (num >= str.Length || !this.CompareChar(str[num], ch))
              KeywordGroupMatch.removeList.Push(str);
          }
          while (KeywordGroupMatch.removeList.Count > 0)
            KeywordGroupMatch.shortlist.Remove(KeywordGroupMatch.removeList.Pop());
          if (KeywordGroupMatch.shortlist.Count <= 0)
            goto label_22;
        }
      }
      this.RemoveLongStrings(num + 1);
label_22:
      return KeywordGroupMatch.shortlist.Count > 0;
    }

    private void RemoveLongStrings(int length)
    {
      foreach (string str in KeywordGroupMatch.shortlist)
      {
        if (str.Length > length)
          KeywordGroupMatch.removeList.Push(str);
      }
      while (KeywordGroupMatch.removeList.Count > 0)
        KeywordGroupMatch.shortlist.Remove(KeywordGroupMatch.removeList.Pop());
    }

    private void BuildKeywordCache()
    {
      if (this.keywordCache != null)
        return;
      this.keywordCache = this.keywords.Split(' ');
    }

    private bool CompareChar(char a, char b)
    {
      return (int) a == (int) b || !this.caseSensitive && (int) char.ToUpper(a, CultureInfo.CurrentCulture) == (int) char.ToUpper(b, CultureInfo.CurrentCulture);
    }
  }
}
