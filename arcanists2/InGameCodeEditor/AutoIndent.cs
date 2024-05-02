

using System;
using System.Text;

#nullable disable
namespace InGameCodeEditor
{
  [Serializable]
  public class AutoIndent
  {
    private static StringBuilder indentBuilder = new StringBuilder();
    [NonSerialized]
    private string indentDecreaseString;
    public AutoIndent.IndentMode autoIndentMode = AutoIndent.IndentMode.AutoTab;
    public bool allowAutoIndent = true;
    public char indentIncreaseCharacter;
    public char indentDecreaseCharacter;

    public string IndentDecreaseString
    {
      get
      {
        if (this.indentDecreaseString == null)
          this.indentDecreaseString = new string(this.indentDecreaseCharacter, 1);
        return this.indentDecreaseString;
      }
    }

    public string GetAutoIndentedFormattedString(
      string indentSection,
      int currentIndent,
      out int caretPosition)
    {
      int amount = currentIndent + 1;
      for (int index = 0; index < indentSection.Length; ++index)
      {
        if (indentSection[index] == '\n')
        {
          AutoIndent.indentBuilder.Append('\n');
          this.AppendIndentString(amount);
        }
        else if (indentSection[index] != '\t')
        {
          if ((int) indentSection[index] == (int) this.indentIncreaseCharacter)
          {
            AutoIndent.indentBuilder.Append(this.indentIncreaseCharacter);
            ++amount;
          }
          else if ((int) indentSection[index] == (int) this.indentDecreaseCharacter)
          {
            AutoIndent.indentBuilder.Append(this.indentDecreaseCharacter);
            --amount;
          }
          else
            AutoIndent.indentBuilder.Append(indentSection[index]);
        }
      }
      string indentedFormattedString = AutoIndent.indentBuilder.ToString();
      AutoIndent.indentBuilder.Length = 0;
      caretPosition = indentedFormattedString.Length - 1;
      for (int index = indentedFormattedString.Length - 1; index >= 0; --index)
      {
        if (indentedFormattedString[index] != '\n')
        {
          caretPosition = index;
          break;
        }
      }
      return indentedFormattedString;
    }

    public int GetAutoIndentLevel(string inputString, int startIndex, int endIndex)
    {
      int autoIndentLevel = 0;
      for (int index = startIndex; index < endIndex; ++index)
      {
        if (inputString[index] == '\t')
          ++autoIndentLevel;
        if (inputString[index] == '\n' || inputString[index] != ' ')
          break;
      }
      return autoIndentLevel;
    }

    private void AppendIndentString(int amount)
    {
      for (int index = 0; index < amount; ++index)
        AutoIndent.indentBuilder.Append("\t");
    }

    public enum IndentMode
    {
      None,
      AutoTab,
      AutoTabContextual,
    }
  }
}
