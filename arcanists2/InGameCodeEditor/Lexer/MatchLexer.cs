// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.Lexer.MatchLexer
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace InGameCodeEditor.Lexer
{
  public abstract class MatchLexer
  {
    public abstract string HTMLColor { get; }

    public virtual IEnumerable<char> SpecialStartCharacters
    {
      get
      {
        yield break;
      }
    }

    public virtual IEnumerable<char> SpecialEndCharacters
    {
      get
      {
        yield break;
      }
    }

    public abstract bool IsImplicitMatch(ILexer lexer);

    public virtual void Invalidate()
    {
    }

    public bool IsMatch(ILexer lexer)
    {
      int num = this.IsImplicitMatch(lexer) ? 1 : 0;
      if (num != 0)
      {
        lexer.Commit();
        return num != 0;
      }
      lexer.Rollback();
      return num != 0;
    }
  }
}
