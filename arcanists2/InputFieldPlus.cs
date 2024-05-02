// Decompiled with JetBrains decompiler
// Type: InputFieldPlus
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#nullable disable
public class InputFieldPlus : MonoBehaviour
{
  public TMP_Text inputText;
  public TMP_Text caretText;
  public bool IsReportInput;
  public bool NOCALLBACK;
  public string caretString = "|";
  public float blinkTime = 0.5f;
  public int maxChars = 20;
  public bool allowSpecialChars = true;
  public Color32 caretColor = (Color32) Color.cyan;
  public bool focused = true;
  public bool allowEmoji;
  internal bool suggestingEmoji;
  public EmojiSuggest emojiSuggest;
  public EmojiSelector emojiSelector;
  public InputFieldPlus.OnEnd onEnd;
  private int caretPos;
  private float curBlinkTime;
  private bool caretVisible;
  private string caretString2 = "";
  private static InputFieldPlus active;

  public int GetCaretPos => this.caretPos;

  public static bool Active
  {
    get
    {
      bool? nullable;
      if ((UnityEngine.Object) InputFieldPlus.active != (UnityEngine.Object) null)
      {
        InputFieldPlus active = InputFieldPlus.active;
        int num;
        if (active == null)
        {
          num = 0;
        }
        else
        {
          nullable = active.gameObject?.activeInHierarchy;
          bool flag = true;
          num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        if (num != 0)
          return true;
      }
      ChatBox instance = ChatBox.Instance;
      if (instance == null)
        return false;
      nullable = instance.chatInput?.ActiveInputField();
      bool flag1 = true;
      return nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
    }
  }

  private void Start()
  {
    this.Init(this.caretColor);
    this.emojiSelector?.gameObject.SetActive(Global.GetPrefBool("prefshowemoji", false));
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) InputFieldPlus.active == (UnityEngine.Object) this))
      return;
    InputFieldPlus.active = (InputFieldPlus) null;
  }

  public void Clear()
  {
    this.inputText.text = "";
    this.caretText.text = "";
    this.caretPos = 0;
    this.caretVisible = false;
  }

  public void SetText(string s)
  {
    this.inputText.text = s;
    this.caretPos = s.Length;
    this.caretText.text = "";
    this.caretVisible = false;
  }

  public void ClickOk() => this.onEnd?.Invoke(this.inputText.text);

  public void OnTouch() => ClientResources.Instance.OnTouch(this);

  public void Init(Color32 c)
  {
    this.caretString2 = "<color=" + InputFieldPlus.RGBtoHEX(c) + ">" + this.caretString + "</color>";
    this.caretText.text = "<color=#00000000>" + this.inputText.text.Substring(0, this.caretPos) + "</color>" + this.caretString2;
  }

  public void Deactivate()
  {
    this.focused = false;
    this.caretText.text = "";
    this.curBlinkTime = 0.0f;
    this.caretVisible = false;
    this.caretText.gameObject.SetActive(this.caretVisible);
  }

  public void SetAsActive(bool v = true)
  {
    if (v)
    {
      InputFieldPlus.active = this;
      this.focused = true;
    }
    else
      InputFieldPlus.active = (InputFieldPlus) null;
  }

  private bool ActiveInputField()
  {
    GameObject selectedGameObject = EventSystem.current?.currentSelectedGameObject;
    if ((UnityEngine.Object) selectedGameObject != (UnityEngine.Object) null)
    {
      TMP_InputField component = selectedGameObject.GetComponent<TMP_InputField>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        return component.isFocused;
    }
    return false;
  }

  public bool IsTheActiveInput => (UnityEngine.Object) InputFieldPlus.active == (UnityEngine.Object) this;

  public void ToggleEmoji()
  {
    this.emojiSelector.gameObject.SetActive(!this.emojiSelector.gameObject.activeSelf);
    Global.SetPrefBool("prefshowemoji", this.emojiSelector.gameObject.activeSelf && Global.GetPrefBool("emojilocked", false));
  }

  public void AddEmoji(string s)
  {
    if (this.inputText.text.Length + s.Length > this.maxChars)
      return;
    if (this.caretPos == this.inputText.text.Length)
    {
      this.inputText.text += s;
    }
    else
    {
      string text = this.inputText.text;
      this.inputText.text = text.Substring(0, this.caretPos) + s;
      this.inputText.text += text.Substring(this.caretPos, text.Length - this.caretPos);
    }
    this.caretPos += s.Length;
    this.RefreshCaret();
    this.suggestingEmoji = false;
    this.emojiSuggest.gameObject.SetActive(false);
  }

  public void ReplaceEmoji(string s)
  {
    int length = this.inputText.text.LastIndexOf(':', this.caretPos - 1);
    if (this.inputText.text.Length + s.Length - (this.caretPos - length) > this.maxChars)
      return;
    int num = this.inputText.text.Length - length;
    if (num + s.Length >= this.maxChars)
      return;
    this.inputText.text = this.caretPos != this.inputText.text.Length ? this.inputText.text.Substring(0, length) + s + this.inputText.text.Substring(this.caretPos) : this.inputText.text.Substring(0, length) + s;
    this.caretPos += s.Length - num;
    this.RefreshCaret();
    this.suggestingEmoji = false;
    this.emojiSuggest.gameObject.SetActive(false);
  }

  public void SearchEmoji()
  {
    int num = this.inputText.text.LastIndexOf(':', this.caretPos - 1);
    if (num < 0)
    {
      this.suggestingEmoji = false;
      this.emojiSuggest.gameObject.SetActive(false);
    }
    else
    {
      int length = this.inputText.text.Length - num - 1;
      if (length <= 0)
        this.emojiSuggest?.Search("");
      else
        this.emojiSuggest?.Search(this.inputText.text.Substring(num + 1, length));
    }
  }

  public bool ExactMatch()
  {
    if (this.caretPos < 3)
      return false;
    int num1 = this.inputText.text.LastIndexOf(':', this.caretPos - 2);
    if (num1 < 0)
      return false;
    int num2 = this.inputText.text.Length - num1 - 1;
    if (num2 <= 0)
      return false;
    string str = this.inputText.text.Substring(num1 + 1, num2 - 1);
    EmojiInfo.Search(str);
    for (int index = 0; index < EmojiInfo.searchResults.list.Count; ++index)
    {
      if (string.Equals(str, EmojiInfo.searchResults.list[index].name))
      {
        this.inputText.text = this.inputText.text.Substring(0, this.inputText.text.Length - 1);
        --this.caretPos;
        this.ReplaceEmoji("<sprite name=\"" + EmojiInfo.searchResults.list[index].realName + "\">");
        return true;
      }
    }
    return false;
  }

  private void Update()
  {
    if ((UnityEngine.Object) InputFieldPlus.active != (UnityEngine.Object) null && (UnityEngine.Object) InputFieldPlus.active != (UnityEngine.Object) this || this.ActiveInputField() || !this.focused || QuickchatUI.Active)
      return;
    if (this.inputText.text.Length < this.maxChars && this.allowSpecialChars && Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl) && !ChatBox.UsingMobileInput)
    {
      QuickchatUI.Create();
    }
    else
    {
      if (Global.InputString.Length > 0 && ((UnityEngine.Object) MyContextMenu.instance == (UnityEngine.Object) null && (UnityEngine.Object) ReportMenu.instance == (UnityEngine.Object) null || this.IsReportInput))
      {
        if (!((UnityEngine.Object) HUD.instance == (UnityEngine.Object) null) && !HUD.instance.Chatting())
        {
          ZGame game = Client.game;
          if ((game != null ? (game.isSandbox ? 1 : 0) : 0) == 0 && !((UnityEngine.Object) SpellLobbyChange.Instance != (UnityEngine.Object) null) && !((UnityEngine.Object) MyContextMenu.instance != (UnityEngine.Object) null) && !this.IsReportInput)
            goto label_12;
        }
        this.ProcessString(Global.InputString);
        if (this.suggestingEmoji)
          this.SearchEmoji();
        this.curBlinkTime = 0.0f;
        this.caretVisible = true;
        if (!this.caretText.gameObject.activeInHierarchy)
          this.caretText.gameObject.SetActive(this.caretVisible);
        this.caretText.text = "<color=#00000000>" + this.inputText.text.Substring(0, this.caretPos) + "</color>" + this.caretString2;
        return;
      }
label_12:
      if (Input.anyKeyDown && Input.GetKey(KeyCode.LeftControl))
      {
        if (Input.GetKeyDown(KeyCode.V))
        {
          this.inputText.text += Global.systemCopyBuffer;
          char[] charArray = this.inputText.text.ToCharArray();
          for (int index = 0; index < charArray.Length; ++index)
          {
            if (charArray[index] == '\n' || charArray[index] == '\r' || charArray[index] == '\b')
              charArray[index] = ' ';
          }
          string str = new string(charArray);
          if (str.Length > this.maxChars)
            str = str.Substring(0, this.maxChars);
          this.inputText.text = str;
          this.caretPos = this.inputText.text.Length;
          this.RefreshCaret();
        }
        else if (Input.GetKeyDown(KeyCode.C))
          Global.systemCopyBuffer = this.inputText.text;
        else if (Input.GetKeyDown(KeyCode.X))
        {
          Global.systemCopyBuffer = this.inputText.text;
          this.inputText.text = "";
          this.caretPos = 0;
          this.caretText.text = this.caretString2;
        }
        if (this.suggestingEmoji)
        {
          this.suggestingEmoji = false;
          this.emojiSuggest.gameObject.SetActive(false);
        }
      }
      this.curBlinkTime += Time.deltaTime;
      if ((double) this.curBlinkTime < (double) this.blinkTime)
        return;
      this.caretVisible = !this.caretVisible;
      this.caretText.gameObject.SetActive(this.caretVisible);
      this.curBlinkTime = 0.0f;
    }
  }

  internal void OnScreenKeyboardProcessString(string s)
  {
    this.ProcessString(s);
    if (this.suggestingEmoji)
      this.SearchEmoji();
    this.curBlinkTime = 0.0f;
    this.caretVisible = true;
    if (!this.caretText.gameObject.activeInHierarchy)
      this.caretText.gameObject.SetActive(this.caretVisible);
    this.caretText.text = "<color=#00000000>" + this.inputText.text.Substring(0, this.caretPos) + "</color>" + this.caretString2;
  }

  private void ProcessString(string s)
  {
    foreach (char ch in s)
    {
      switch (ch)
      {
        case '\b':
          if (this.inputText.text.Length != 0)
          {
            if (this.allowEmoji && this.inputText.text[this.inputText.text.Length - 1] == ':')
            {
              this.suggestingEmoji = false;
              this.emojiSuggest.gameObject.SetActive(false);
            }
            else if (this.allowEmoji && this.inputText.text[this.inputText.text.Length - 1] == '>' && this.caretPos > 5)
            {
              int num = this.inputText.text.LastIndexOf("<sprite name=", this.caretPos - 1);
              if (num > -1 && this.inputText.text.IndexOf('>', num) == this.inputText.text.Length - 1)
              {
                this.inputText.text = this.inputText.text.Substring(0, num);
                this.caretPos = this.inputText.text.Length;
                break;
              }
            }
            this.inputText.text = this.caretPos != this.inputText.text.Length ? this.inputText.text.Substring(0, this.caretPos - 1) + this.inputText.text.Substring(this.caretPos, this.inputText.text.Length - this.caretPos) : this.inputText.text.Substring(0, this.inputText.text.Length - 1);
            --this.caretPos;
            if (this.inputText.text.Length == 0)
            {
              this.curBlinkTime = this.blinkTime;
              break;
            }
            break;
          }
          break;
        case '\n':
        case '\r':
          if (this.suggestingEmoji && this.emojiSuggest.ActiveSelection || (UnityEngine.Object) ReportMenu.instance != (UnityEngine.Object) null || this.NOCALLBACK)
            return;
          if (this.onEnd != null)
            this.onEnd.Invoke(this.inputText.text.Trim());
          this.inputText.text = "";
          this.caretPos = 0;
          if (!this.suggestingEmoji)
            return;
          this.suggestingEmoji = false;
          this.emojiSuggest.gameObject.SetActive(false);
          return;
        default:
          if (this.inputText.text.Length < this.maxChars && (this.allowSpecialChars || ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch == ' ' || ch == '_'))
          {
            if (this.caretPos == this.inputText.text.Length)
            {
              this.inputText.text += ch.ToString();
            }
            else
            {
              string text = this.inputText.text;
              this.inputText.text = text.Substring(0, this.caretPos) + ch.ToString();
              this.inputText.text += text.Substring(this.caretPos, text.Length - this.caretPos);
            }
            ++this.caretPos;
            if (this.allowEmoji && ch == ':' && (this.inputText.text.Length == 0 || !this.inputText.text.StartsWith("?") || this.inputText.text.StartsWith("?msg:")) && (!this.suggestingEmoji || !this.ExactMatch()))
            {
              this.suggestingEmoji = true;
              break;
            }
            break;
          }
          break;
      }
    }
  }

  internal void RefreshCaret()
  {
    this.curBlinkTime = 0.0f;
    this.caretVisible = true;
    if (!this.caretText.gameObject.activeInHierarchy)
      this.caretText.gameObject.SetActive(this.caretVisible);
    this.caretText.text = "<color=#00000000>" + this.inputText.text.Substring(0, this.caretPos) + "</color>" + this.caretString2;
  }

  public static string RemoveSpecialCharacters(string str)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in str)
    {
      if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch == ' ' || ch == '_')
        stringBuilder.Append(ch);
    }
    return stringBuilder.ToString();
  }

  public static string RGBtoHEX(Color32 c)
  {
    return "#" + c.r.ToString("X2") + c.g.ToString("X2") + c.b.ToString("X2") + c.a.ToString("X2");
  }

  public void ProcessEvent(Event e) => this.KeyPressed(e);

  protected bool KeyPressed(Event evt)
  {
    EventModifiers modifiers = evt.modifiers;
    int num = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX ? ((modifiers & EventModifiers.Command) != 0 ? 1 : 0) : ((modifiers & EventModifiers.Control) != 0 ? 1 : 0);
    bool flag1 = (modifiers & EventModifiers.Shift) != 0;
    bool flag2 = (modifiers & EventModifiers.Alt) != 0;
    bool flag3 = num != 0 && !flag2 && !flag1;
    switch (evt.keyCode)
    {
      case KeyCode.C:
        if (flag3)
        {
          Global.systemCopyBuffer = this.inputText.text;
          return true;
        }
        break;
      case KeyCode.V:
        if (flag3)
        {
          this.inputText.text += Global.systemCopyBuffer;
          char[] charArray = this.inputText.text.ToCharArray();
          for (int index = 0; index < charArray.Length; ++index)
          {
            if (charArray[index] == '\n' || charArray[index] == '\r' || charArray[index] == '\b')
              charArray[index] = ' ';
          }
          string str = new string(charArray);
          if (str.Length > this.maxChars)
            str = str.Substring(0, this.maxChars);
          this.inputText.text = str;
          this.caretPos = this.inputText.text.Length;
          this.RefreshCaret();
          return true;
        }
        break;
      case KeyCode.X:
        if (flag3)
        {
          Global.systemCopyBuffer = this.inputText.text;
          this.inputText.text = "";
          this.caretPos = 0;
          this.caretText.text = this.caretString2;
          return true;
        }
        break;
    }
    return true;
  }

  protected virtual bool IsValidChar(char c)
  {
    if (c == char.MinValue || c == '\u007F')
      return false;
    if (c != '\t')
      ;
    return true;
  }

  [Serializable]
  public class OnEnd : UnityEvent<string>
  {
  }
}
