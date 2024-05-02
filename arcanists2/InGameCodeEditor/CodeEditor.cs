

using InGameCodeEditor.Lexer;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace InGameCodeEditor
{
  public class CodeEditor : MonoBehaviour
  {
    private static readonly KeyCode[] focusKeys = new KeyCode[6]
    {
      KeyCode.Return,
      KeyCode.Backspace,
      KeyCode.UpArrow,
      KeyCode.DownArrow,
      KeyCode.LeftArrow,
      KeyCode.RightArrow
    };
    private static StringBuilder highlightedBuilder = new StringBuilder(4096);
    private static StringBuilder lineBuilder = new StringBuilder();
    private static MethodInfo scrollBarUpdateFix = (MethodInfo) null;
    private InputStringLexer lexer = new InputStringLexer();
    private RectTransform inputTextTransform;
    private RectTransform lineHighlightTransform;
    private int lineCount;
    private int currentLine;
    private int currentColumn;
    private int currentIndent;
    private string lastText;
    private bool delayedRefresh;
    private float lastScrollValue;
    private bool lineHighlightLocked;
    [Header("Elements")]
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private TextMeshProUGUI inputText;
    [SerializeField]
    private TextMeshProUGUI inputHighlightText;
    [SerializeField]
    private TextMeshProUGUI lineText;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image lineHighlight;
    [SerializeField]
    private Image lineNumberBackground;
    [SerializeField]
    private Image scrollbar;
    [Header("Themes")]
    [SerializeField]
    private CodeEditorTheme editorTheme;
    [SerializeField]
    private CodeLanguageTheme languageTheme;
    [Header("Options")]
    [SerializeField]
    private bool lineNumbers = true;
    [SerializeField]
    private int lineNumbersSize = 20;
    [Header("TMP Compatibility")]
    [SerializeField]
    private bool applyLineOffsetFix;

    public CodeEditorTheme EditorTheme
    {
      get => this.editorTheme;
      set
      {
        this.editorTheme = value;
        this.ApplyTheme();
      }
    }

    public CodeLanguageTheme LanguageTheme
    {
      get => this.languageTheme;
      set
      {
        this.languageTheme = value;
        this.ApplyLanguage();
      }
    }

    public TMP_InputField InputField => this.inputField;

    public int LineCount => this.lineCount;

    public int CurrentLine => this.currentLine;

    public int CurrentColumn => this.currentColumn;

    public int CurrentIndent => this.currentIndent;

    public string Text
    {
      get => this.inputField.text;
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          string str = value.Replace("\r", "");
          this.inputField.text = str;
          this.inputHighlightText.text = str;
          try
          {
            if (CodeEditor.scrollBarUpdateFix == (MethodInfo) null)
              CodeEditor.scrollBarUpdateFix = typeof (TMP_InputField).GetMethod("UpdateScrollbar", BindingFlags.Instance | BindingFlags.NonPublic);
            CodeEditor.scrollBarUpdateFix.Invoke((object) this.inputField, (object[]) null);
          }
          catch
          {
          }
          this.delayedRefresh = true;
          this.inputText.ForceMeshUpdate(false, false);
        }
        else
        {
          this.inputField.text = string.Empty;
          this.inputHighlightText.text = string.Empty;
          this.inputText.ForceMeshUpdate(false, false);
        }
      }
    }

    public string HighlightedText => this.inputHighlightText.text;

    public bool LineNumbers
    {
      get => this.lineNumbers;
      set
      {
        this.lineNumbers = value;
        RectTransform transform1 = this.inputField.transform as RectTransform;
        RectTransform transform2 = this.lineNumberBackground.transform as RectTransform;
        if (this.lineNumbers)
        {
          this.lineNumberBackground.gameObject.SetActive(true);
          this.lineText.gameObject.SetActive(true);
          transform1.offsetMin = new Vector2((float) this.lineNumbersSize, transform1.offsetMin.y);
          transform2.sizeDelta = new Vector2((float) (this.lineNumbersSize + 15), transform2.sizeDelta.y);
        }
        else
        {
          this.lineNumberBackground.gameObject.SetActive(false);
          this.lineText.gameObject.SetActive(false);
          transform1.offsetMin = new Vector2(0.0f, transform1.offsetMin.y);
        }
      }
    }

    public int LineNumbersSize
    {
      get => this.lineNumbersSize;
      set
      {
        this.lineNumbersSize = value;
        this.LineNumbers = this.lineNumbers;
      }
    }

    public void Awake()
    {
      if (!this.AllReferencesAssigned())
      {
        this.enabled = false;
        throw new MissingReferenceException("One or more required references are missing. Make sure all references under the 'Elements' header are assigned");
      }
      this.inputTextTransform = this.inputText.GetComponent<RectTransform>();
      this.lineHighlightTransform = this.lineHighlight.GetComponent<RectTransform>();
    }

    public void Start()
    {
      if ((Object) this.editorTheme == (Object) null)
        this.editorTheme = CodeEditorTheme.DefaultTheme;
      this.ApplyTheme();
      this.ApplyLanguage();
    }

    public void LateUpdate()
    {
      if (Input.mouseScrollDelta != Vector2.zero || (double) this.inputField.verticalScrollbar.value != (double) this.lastScrollValue)
      {
        this.UpdateCurrentLineHighlight();
        this.lastScrollValue = this.inputField.verticalScrollbar.value;
      }
      if ((Object) this.languageTheme != (Object) null && this.languageTheme.autoIndent.autoIndentMode != AutoIndent.IndentMode.None)
      {
        if (Input.GetKeyDown(KeyCode.Return))
          this.AutoIndentCaret();
        else if (Input.anyKeyDown && Input.inputString.Contains(this.languageTheme.autoIndent.IndentDecreaseString))
          this.AutoIndentCaret(true);
      }
      if (!this.inputField.isFocused && !this.delayedRefresh)
        return;
      if (this.delayedRefresh)
      {
        this.delayedRefresh = false;
        this.Refresh(true, false);
      }
      if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        this.delayedRefresh = true;
      if (Input.anyKey)
        this.Refresh();
      bool flag = false;
      foreach (KeyCode focusKey in CodeEditor.focusKeys)
      {
        if (Input.GetKey(focusKey))
        {
          flag = true;
          break;
        }
      }
      if (!flag && !Input.GetMouseButton(0))
        return;
      this.UpdateCurrentLineHighlight();
    }

    public void Refresh(bool forceUpdate = false, bool updateLineOnly = true)
    {
      this.DisplayedContentChanged(this.inputField.text, forceUpdate, updateLineOnly);
    }

    public void SetLineHighlight(int lineNumber, bool lockLineHighlight)
    {
      if (!this.isActiveAndEnabled || lineNumber < 1 || lineNumber > this.LineCount)
        return;
      int num1 = 0;
      int num2 = lineNumber - 1;
      if (this.applyLineOffsetFix)
        ++num1;
      this.lineHighlightTransform.anchoredPosition = new Vector2(5f, (float) ((double) this.inputText.textInfo.lineInfo[this.inputText.textInfo.characterInfo[0].lineNumber].lineHeight * (double) -num2 + (double) num1 - 4.0) + this.inputTextTransform.anchoredPosition.y);
      if (lockLineHighlight)
        this.LockLineHighlight();
      else
        this.UnlockLineHighlight();
    }

    public void LockLineHighlight() => this.lineHighlightLocked = true;

    public void UnlockLineHighlight() => this.lineHighlightLocked = false;

    private void DisplayedContentChanged(string newText, bool forceUpdate, bool updateLineOnly)
    {
      this.UpdateCurrentLineColumnIndent();
      if (!forceUpdate && this.lastText == newText || string.IsNullOrEmpty(newText))
      {
        if (string.IsNullOrEmpty(newText))
          this.inputHighlightText.text = string.Empty;
        this.UpdateCurrentLineNumbers();
        this.UpdateCurrentLineHighlight();
      }
      else
      {
        this.inputHighlightText.text = this.SyntaxHighlightContent(newText);
        this.inputField.verticalScrollbar.gameObject.SetActive((double) this.inputField.verticalScrollbar.size < 1.0);
        this.UpdateCurrentLineNumbers();
        this.UpdateCurrentLineHighlight();
        this.lastText = newText;
      }
    }

    private void UpdateCurrentLineNumbers()
    {
      int lineCount = this.inputText.textInfo.lineCount;
      int num = 1;
      if (lineCount == this.lineCount)
        return;
      CodeEditor.lineBuilder.Length = 0;
      for (int index = 1; index < lineCount + 2; ++index)
      {
        if (index - 1 > 0 && index - 1 < lineCount - 1)
        {
          int firstCharacterIndex = this.inputText.textInfo.lineInfo[index - 1].firstCharacterIndex;
          int characterCount = this.inputText.textInfo.lineInfo[index - 1].characterCount;
          if (characterCount != 0 && !this.inputText.text.Substring(firstCharacterIndex, characterCount).Contains("\n"))
          {
            CodeEditor.lineBuilder.Append("\n");
            continue;
          }
        }
        CodeEditor.lineBuilder.Append(num);
        CodeEditor.lineBuilder.Append('\n');
        ++num;
        if (index - 1 == 0 && index - 1 < lineCount - 1)
        {
          int firstCharacterIndex = this.inputText.textInfo.lineInfo[index - 1].firstCharacterIndex;
          int characterCount = this.inputText.textInfo.lineInfo[index - 1].characterCount;
          if (characterCount != 0 && !this.inputText.text.Substring(firstCharacterIndex, characterCount).Contains("\n"))
            CodeEditor.lineBuilder.Append("\n");
        }
      }
      this.lineText.text = CodeEditor.lineBuilder.ToString();
      this.lineCount = lineCount;
    }

    private void UpdateCurrentLineColumnIndent()
    {
      this.currentLine = this.inputText.textInfo.characterInfo[this.InputField.caretPosition].lineNumber;
      int num1 = 0;
      for (int index = 0; index < this.currentLine; ++index)
        num1 += this.inputText.textInfo.lineInfo[index].characterCount;
      this.currentColumn = this.inputField.caretPosition - num1;
      this.currentIndent = 0;
      if (!((Object) this.languageTheme != (Object) null) || !this.languageTheme.autoIndent.allowAutoIndent)
        return;
      for (int index = 0; index < this.inputField.caretPosition && index < this.inputField.text.Length; ++index)
      {
        int num2 = (int) this.inputField.text[index];
        if (num2 == (int) this.languageTheme.autoIndent.indentIncreaseCharacter)
          ++this.currentIndent;
        if (num2 == (int) this.languageTheme.autoIndent.indentDecreaseCharacter)
          --this.currentIndent;
      }
      if (this.currentIndent >= 0)
        return;
      this.currentIndent = 0;
    }

    private void UpdateCurrentLineHighlight()
    {
      if (!this.isActiveAndEnabled || this.lineHighlightLocked)
        return;
      int num = 0;
      if (this.applyLineOffsetFix)
        ++num;
      this.lineHighlightTransform.anchoredPosition = new Vector2(5f, (float) ((double) this.inputText.textInfo.lineInfo[this.inputText.textInfo.characterInfo[0].lineNumber].lineHeight * (double) (-this.inputText.textInfo.characterInfo[this.inputField.caretPosition].lineNumber + num) - 4.0) + this.inputTextTransform.anchoredPosition.y);
    }

    private string SyntaxHighlightContent(string inputText)
    {
      if ((Object) this.languageTheme == (Object) null || (Object) this.editorTheme != (Object) null && !this.editorTheme.allowSyntaxHighlighting)
        return inputText;
      int num = 0;
      CodeEditor.highlightedBuilder.Length = 0;
      foreach (InputStringMatchInfo inputStringMatchInfo in this.lexer.LexInputString(inputText))
      {
        for (int index = num; index < inputStringMatchInfo.startIndex; ++index)
          CodeEditor.highlightedBuilder.Append(inputText[index]);
        CodeEditor.highlightedBuilder.Append(inputStringMatchInfo.htmlColor);
        for (int startIndex = inputStringMatchInfo.startIndex; startIndex < inputStringMatchInfo.endIndex; ++startIndex)
          CodeEditor.highlightedBuilder.Append(inputText[startIndex]);
        CodeEditor.highlightedBuilder.Append("</color>");
        num = inputStringMatchInfo.endIndex;
      }
      for (int index = num; index < inputText.Length; ++index)
        CodeEditor.highlightedBuilder.Append(inputText[index]);
      inputText = CodeEditor.highlightedBuilder.ToString();
      return inputText;
    }

    private void AutoIndentCaret(bool isClosingToken = false)
    {
      if (Input.GetKeyDown(KeyCode.Return))
      {
        this.UpdateCurrentLineColumnIndent();
        string str1 = string.Empty;
        if (this.inputField.caretPosition < this.inputField.text.Length)
        {
          int num1 = 0;
          int num2 = 0;
          for (int caretPosition = this.inputField.caretPosition; caretPosition >= 0; --caretPosition)
          {
            if (this.inputField.text[caretPosition] == '\t')
              ++num1;
            if ((this.inputField.text[caretPosition] == '\n' || this.languageTheme.autoIndent.autoIndentMode == AutoIndent.IndentMode.AutoTabContextual && this.inputField.text[caretPosition] != ' ') && caretPosition != this.inputField.caretPosition)
              break;
          }
          str1 = this.GetAutoIndentTab(this.languageTheme.autoIndent.autoIndentMode != AutoIndent.IndentMode.AutoTabContextual ? num1 : this.currentIndent - num2);
        }
        if (str1.Length > 0)
        {
          this.inputField.text = this.inputField.text.Insert(this.inputField.caretPosition + 1, str1);
          this.inputField.stringPosition += str1.Length;
        }
        bool flag = false;
        int count = -1;
        for (int index = this.inputField.caretPosition + 1; index < this.inputField.text.Length; ++index)
        {
          if ((int) this.inputField.text[index] == (int) this.languageTheme.autoIndent.indentDecreaseCharacter)
          {
            flag = true;
            count = index - (this.inputField.caretPosition + 1);
            break;
          }
          if (!char.IsWhiteSpace(this.inputField.text[index]) || this.inputField.text[index] == '\n')
            break;
        }
        if (flag)
        {
          this.inputField.text = this.inputField.text.Remove(this.inputField.caretPosition + 1, count);
          string str2 = string.IsNullOrEmpty(str1) ? string.Empty : str1.Remove(0, 1);
          this.inputField.text = this.inputField.text.Insert(this.inputField.caretPosition + 1, this.GetAutoIndentTab(this.currentIndent) + "\n" + str2);
          string.IsNullOrEmpty(str2);
          this.UpdateCurrentLineColumnIndent();
        }
      }
      if (isClosingToken && this.inputField.caretPosition > 0 && this.inputField.text[this.inputField.caretPosition - 1] == '\t')
      {
        this.inputField.text = this.inputField.text.Remove(this.inputField.caretPosition - 1, 1);
        --this.inputField.stringPosition;
      }
      this.inputText.text = this.inputField.text;
      this.inputText.SetText(this.inputField.text);
      this.inputText.Rebuild(CanvasUpdate.Prelayout);
      this.inputField.ForceLabelUpdate();
      this.inputField.Rebuild(CanvasUpdate.Prelayout);
      this.Refresh(true);
      this.delayedRefresh = true;
    }

    private string GetAutoIndentTab(int amount)
    {
      string empty = string.Empty;
      for (int index = 0; index < amount; ++index)
        empty += "\t";
      return empty;
    }

    private void ApplyTheme()
    {
      if (!this.AllReferencesAssigned())
        throw new MissingReferenceException("Cannot apply theme because one or more required component references are missing. Make sure all references under the 'Elements' header are assigned");
      bool flag = false;
      if ((Object) this.editorTheme == (Object) null)
      {
        this.editorTheme = CodeEditorTheme.DefaultTheme;
        flag = true;
      }
      this.inputField.caretColor = this.editorTheme.caretColor;
      this.inputText.color = this.editorTheme.textColor;
      this.inputHighlightText.color = this.editorTheme.textColor;
      this.background.color = this.editorTheme.backgroundColor;
      this.lineHighlight.color = this.editorTheme.lineHighlightColor;
      this.lineNumberBackground.color = this.editorTheme.lineNumberBackgroundColor;
      this.lineText.color = this.editorTheme.lineNumberTextColor;
      this.scrollbar.color = this.editorTheme.scrollbarColor;
      if (!flag)
        return;
      this.editorTheme = (CodeEditorTheme) null;
    }

    private void ApplyLanguage()
    {
      char[] delimiters = (char[]) null;
      MatchLexer[] matchers = (MatchLexer[]) null;
      if ((Object) this.languageTheme != (Object) null)
      {
        delimiters = this.languageTheme.DelimiterSymbols;
        matchers = this.languageTheme.Matchers;
      }
      this.lexer.UseMatchers(delimiters, matchers);
    }

    private bool AllReferencesAssigned()
    {
      return !((Object) this.inputField == (Object) null) && !((Object) this.inputText == (Object) null) && !((Object) this.inputHighlightText == (Object) null) && !((Object) this.lineText == (Object) null) && !((Object) this.background == (Object) null) && !((Object) this.lineHighlight == (Object) null) && !((Object) this.lineNumberBackground == (Object) null) && !((Object) this.scrollbar == (Object) null);
    }
  }
}
