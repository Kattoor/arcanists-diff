// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WrappedInputField
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;
using WebGLSupport.Detail;

#nullable disable
namespace WebGLSupport
{
  internal class WrappedInputField : IInputField
  {
    private InputField input;
    private RebuildChecker checker;

    public bool ReadOnly => this.input.readOnly;

    public string text
    {
      get => this.input.text;
      set => this.input.text = value;
    }

    public string placeholder
    {
      get
      {
        if (!(bool) (Object) this.input.placeholder)
          return "";
        Text component = this.input.placeholder.GetComponent<Text>();
        return !(bool) (Object) component ? "" : component.text;
      }
    }

    public int fontSize => this.input.textComponent.fontSize;

    public ContentType contentType => (ContentType) this.input.contentType;

    public LineType lineType => (LineType) this.input.lineType;

    public int characterLimit => this.input.characterLimit;

    public int caretPosition => this.input.caretPosition;

    public bool isFocused => this.input.isFocused;

    public int selectionFocusPosition
    {
      get => this.input.selectionFocusPosition;
      set => this.input.selectionFocusPosition = value;
    }

    public int selectionAnchorPosition
    {
      get => this.input.selectionAnchorPosition;
      set => this.input.selectionAnchorPosition = value;
    }

    public bool OnFocusSelectAll => true;

    public WrappedInputField(InputField input)
    {
      this.input = input;
      this.checker = new RebuildChecker((IInputField) this);
    }

    public UnityEngine.RectTransform RectTransform() => this.input.GetComponent<UnityEngine.RectTransform>();

    public void ActivateInputField() => this.input.ActivateInputField();

    public void DeactivateInputField() => this.input.DeactivateInputField();

    public void Rebuild()
    {
      if (!this.checker.NeedRebuild())
        return;
      this.input.textComponent.SetAllDirty();
      this.input.Rebuild(CanvasUpdate.LatePreRender);
    }
  }
}
