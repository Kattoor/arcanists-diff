// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.SymbolGroupMatch
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
  public sealed class SymbolGroupMatch : MatchLexer
  {
    private static readonly List<string> shortlist = new List<string>();
    private static readonly Stack<string> removeList = new Stack<string>();
    [NonSerialized]
    private string[] symbolCache;
    [NonSerialized]
    private string htmlColor;
    [TextArea]
    [Tooltip("A number of symbols that will be highlighted. symbol strings of one or more characters recommended (no numbers or letters) separated by a space")]
    public string symbols;
    [Tooltip("The color that all symbols in this group will be highlighted with")]
    public Color highlightColor = Color.black;

    public bool HasSymbolHighlighting => this.symbols.Length > 0;

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
        this.BuildSymbolCache();
        string[] strArray = this.symbolCache;
        for (int index = 0; index < strArray.Length; ++index)
          yield return strArray[index][0];
        strArray = (string[]) null;
      }
    }

    public override IEnumerable<char> SpecialEndCharacters
    {
      get
      {
        this.BuildSymbolCache();
        string[] strArray = this.symbolCache;
        for (int index = 0; index < strArray.Length; ++index)
        {
          string str = strArray[index];
          yield return str[str.Length - 1];
        }
        strArray = (string[]) null;
      }
    }

    public override void Invalidate() => this.htmlColor = (string) null;

    public override bool IsImplicitMatch(ILexer lexer)
    {
      this.BuildSymbolCache();
      if (!char.IsWhiteSpace(lexer.Previous) && !char.IsLetter(lexer.Previous) && !char.IsDigit(lexer.Previous) && !lexer.IsSpecialSymbol(lexer.Previous, SpecialCharacterPosition.End))
        return false;
      SymbolGroupMatch.shortlist.Clear();
      int num = 0;
      char ch1 = lexer.ReadNext();
      for (int index = 0; index < this.symbolCache.Length; ++index)
      {
        if ((int) this.symbolCache[index][0] == (int) ch1)
          SymbolGroupMatch.shortlist.Add(this.symbolCache[index]);
      }
      if (SymbolGroupMatch.shortlist.Count == 0)
        return false;
      while (!lexer.EndOfStream)
      {
        char ch2 = lexer.ReadNext();
        ++num;
        if (char.IsWhiteSpace(ch2) || char.IsLetter(ch2) || char.IsDigit(ch2) || lexer.IsSpecialSymbol(ch2))
        {
          this.RemoveLongStrings(num);
          lexer.Rollback(1);
          goto label_22;
        }
        else
        {
          foreach (string str in SymbolGroupMatch.shortlist)
          {
            if (num >= str.Length || (int) str[num] != (int) ch2)
              SymbolGroupMatch.removeList.Push(str);
          }
          while (SymbolGroupMatch.removeList.Count > 0)
            SymbolGroupMatch.shortlist.Remove(SymbolGroupMatch.removeList.Pop());
          if (SymbolGroupMatch.shortlist.Count <= 0)
            goto label_22;
        }
      }
      this.RemoveLongStrings(num + 1);
label_22:
      return SymbolGroupMatch.shortlist.Count > 0;
    }

    private void RemoveLongStrings(int length)
    {
      foreach (string str in SymbolGroupMatch.shortlist)
      {
        if (str.Length > length)
          SymbolGroupMatch.removeList.Push(str);
      }
      while (SymbolGroupMatch.removeList.Count > 0)
        SymbolGroupMatch.shortlist.Remove(SymbolGroupMatch.removeList.Pop());
    }

    private void BuildSymbolCache()
    {
      if (this.symbolCache != null)
        return;
      if (string.IsNullOrEmpty(this.symbols))
        this.symbolCache = new string[0];
      else
        this.symbolCache = this.symbols.Split(' ');
    }
  }
}
