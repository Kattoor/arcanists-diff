

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
