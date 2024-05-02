// Decompiled with JetBrains decompiler
// Type: KeyBoard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class KeyBoard : MonoBehaviour
{
  public GameObject parentObj;
  public KeyboardLetter[] keyBoard;
  public InputFieldPlus input;
  private bool viewingSymbols;
  private bool symbols2;
  private bool upperCase;
  private bool forceCaps;
  public Image iconUppercase;
  public TMP_Text txtSymbolPage;
  public TMP_Text txtSymbols;
  private TouchScreenKeyboard mobileKeyboard;

  public static KeyBoard Instance { get; private set; }

  public static bool IsActive
  {
    get
    {
      if (!((UnityEngine.Object) KeyBoard.Instance != (UnityEngine.Object) null))
        return false;
      return KeyBoard.Instance.gameObject.activeInHierarchy || KeyBoard.Instance.mobileKeyboard != null;
    }
  }

  internal void Awake() => KeyBoard.Instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) KeyBoard.Instance == (UnityEngine.Object) this))
      return;
    KeyBoard.Instance = (KeyBoard) null;
  }

  public void Start()
  {
    for (int index = 0; index < this.keyBoard.Length; ++index)
    {
      char ch = this.keyBoard[index].txt.text[0];
      KeyboardLetter key = this.keyBoard[index];
      this.keyBoard[index].button.onClick.AddListener((UnityAction) (() =>
      {
        this.OnKey(this.GetChar(key));
        if (this.viewingSymbols || !this.upperCase || this.forceCaps)
          return;
        this.upperCase = false;
        this.SetChars();
      }));
      this.keyBoard[index].letter = ch;
    }
    this.SetChars();
  }

  private void Update()
  {
    if (!Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.Joystick1Button9))
      return;
    this.Close();
  }

  public void Open(Action<string> onEnd, bool forceClear = true, string txt = null)
  {
    if (Global.GetPrefBool("prefnativekeyboard", true))
    {
      this.OpenNativeKeyboard((Action<string>) (s =>
      {
        onEnd(s);
        this.ClickClose();
      }));
    }
    else
    {
      this.input.onEnd.RemoveAllListeners();
      if (forceClear)
        this.input.Clear();
      this.input.onEnd.AddListener((UnityAction<string>) (s =>
      {
        onEnd(s);
        this.ClickClose();
      }));
      this.parentObj.SetActive(true);
    }
  }

  public void Close()
  {
    this.input.onEnd.RemoveAllListeners();
    this.parentObj.SetActive(false);
    HUD.instance?.chatInput.gameObject.SetActive(false);
    ChatBox.Instance?.Init();
    HUD.instance?.chatInput?.Init(ColorScheme.GetColor(Global.ColorGameText));
  }

  public void ClickClose()
  {
    this.input.onEnd.RemoveAllListeners();
    this.input.Clear();
    this.parentObj.SetActive(false);
    HUD.instance?.chatInput.gameObject.SetActive(false);
    ChatBox.Instance?.Init();
    HUD.instance?.chatInput?.Init(ColorScheme.GetColor(Global.ColorGameText));
  }

  public void OnKey(char c) => this.input.OnScreenKeyboardProcessString(c.ToString());

  public void OnEnter() => this.input.OnScreenKeyboardProcessString("\n");

  public void OnBackspace() => this.input.OnScreenKeyboardProcessString("\b");

  public void OnSpace() => this.input.OnScreenKeyboardProcessString(" ");

  public void OnEmojiToggle()
  {
    this.input.emojiSelector.gameObject.SetActive(!this.input.emojiSelector.gameObject.activeSelf);
  }

  public void OnSymbolToggle()
  {
    this.viewingSymbols = !this.viewingSymbols;
    this.SetChars();
  }

  public void OnToggleCaps()
  {
    if (this.viewingSymbols)
    {
      this.symbols2 = !this.symbols2;
      this.SetChars();
    }
    else
    {
      if (!this.upperCase)
        this.upperCase = true;
      else if (!this.forceCaps)
      {
        this.forceCaps = true;
      }
      else
      {
        this.upperCase = false;
        this.forceCaps = false;
      }
      this.SetChars();
    }
  }

  private void SetChars()
  {
    foreach (KeyboardLetter x in this.keyBoard)
      x.txt.text = this.GetChar(x).ToString();
    this.iconUppercase.gameObject.SetActive(!this.viewingSymbols);
    this.iconUppercase.color = this.upperCase ? (this.forceCaps ? Color.red : Color.blue) : Color.white;
    this.txtSymbolPage.text = this.viewingSymbols ? (this.symbols2 ? "2/2" : "1/2") : "";
    this.txtSymbols.text = this.viewingSymbols ? "ABC" : "!#1";
  }

  private char GetChar(KeyboardLetter x)
  {
    return this.viewingSymbols ? (this.symbols2 ? x.symbol2Letter : x.symbolLetter) : (this.upperCase ? x.letter : x.alternateLetter);
  }

  [ContextMenu("do")]
  public void Editor()
  {
    foreach (KeyboardLetter componentsInChild in this.GetComponentsInChildren<KeyboardLetter>())
      componentsInChild.Editor();
  }

  [ContextMenu("do2")]
  public void Editor2()
  {
    string str = File.ReadAllText("txtSymbols.txt");
    int index = 0;
    foreach (KeyboardLetter keyboardLetter in this.keyBoard)
    {
      keyboardLetter.alternateLetter = keyboardLetter.txt.text.ToLower()[0];
      if (str.Length > index)
        keyboardLetter.symbolLetter = str[index];
      if (str.Length > index + 26)
        keyboardLetter.symbol2Letter = str[index + 26];
      ++index;
    }
  }

  public void OpenNativeKeyboard(Action<string> onEnd)
  {
  }

  private IEnumerator<float> MobileInputIE(Action<string> onEnd)
  {
    while (this.mobileKeyboard != null && this.mobileKeyboard.status == TouchScreenKeyboard.Status.Visible)
      yield return 0.0f;
    if (this.mobileKeyboard != null)
    {
      if (this.mobileKeyboard.status == TouchScreenKeyboard.Status.Done)
      {
        Action<string> action = onEnd;
        if (action != null)
          action(this.mobileKeyboard.text);
      }
      this.mobileKeyboard.text = "";
    }
    HUD.instance?.chatInput.gameObject.SetActive(false);
    ChatBox.Instance?.Init();
    HUD.instance?.chatInput?.Init(ColorScheme.GetColor(Global.ColorGameText));
    this.mobileKeyboard = (TouchScreenKeyboard) null;
  }

  public class OnClick<T> : UnityEvent<T>
  {
  }

  public class OnClick : UnityEvent
  {
  }
}
