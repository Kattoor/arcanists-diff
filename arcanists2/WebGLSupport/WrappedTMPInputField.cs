// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WrappedTMPInputField
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebGLSupport.Detail;

#nullable disable
namespace WebGLSupport
{
  internal class WrappedTMPInputField : IInputField
  {
    private TMP_InputField input;
    private RebuildChecker checker;
    private Coroutine delayedGraphicRebuild;

    public bool ReadOnly => this.input.readOnly;

    public string text
    {
      get => this.input.text;
      set => this.input.text = this.FixContentTypeByInputField(value);
    }

    private string FixContentTypeByInputField(string inText)
    {
      GameObject gameObject = new GameObject("FixContentTypeByInputField for WebGLInput");
      gameObject.SetActive(false);
      InputField inputField = gameObject.AddComponent<InputField>();
      inputField.contentType = (InputField.ContentType) Enum.Parse(typeof (InputField.ContentType), this.input.contentType.ToString());
      inputField.lineType = (InputField.LineType) Enum.Parse(typeof (InputField.LineType), this.input.lineType.ToString());
      inputField.inputType = (InputField.InputType) Enum.Parse(typeof (InputField.InputType), this.input.inputType.ToString());
      inputField.keyboardType = this.input.keyboardType;
      inputField.characterValidation = (InputField.CharacterValidation) Enum.Parse(typeof (InputField.CharacterValidation), this.input.characterValidation.ToString());
      inputField.characterLimit = this.input.characterLimit;
      inputField.text = inText;
      string text = inputField.text;
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      return text;
    }

    public string placeholder
    {
      get
      {
        if (!(bool) (UnityEngine.Object) this.input.placeholder)
          return "";
        TMP_Text component = this.input.placeholder.GetComponent<TMP_Text>();
        return !(bool) (UnityEngine.Object) component ? "" : component.text;
      }
    }

    public int fontSize => (int) this.input.textComponent.fontSize;

    public ContentType contentType => (ContentType) this.input.contentType;

    public LineType lineType => (LineType) this.input.lineType;

    public int characterLimit => this.input.characterLimit;

    public int caretPosition => this.input.caretPosition;

    public bool isFocused => this.input.isFocused;

    public int selectionFocusPosition
    {
      get => this.input.selectionStringFocusPosition;
      set => this.input.selectionStringFocusPosition = value;
    }

    public int selectionAnchorPosition
    {
      get => this.input.selectionStringAnchorPosition;
      set => this.input.selectionStringAnchorPosition = value;
    }

    public bool OnFocusSelectAll => this.input.onFocusSelectAll;

    public WrappedTMPInputField(TMP_InputField input)
    {
      this.input = input;
      this.checker = new RebuildChecker((IInputField) this);
    }

    public UnityEngine.RectTransform RectTransform() => this.input.GetComponent<UnityEngine.RectTransform>();

    public void ActivateInputField() => this.input.ActivateInputField();

    public void DeactivateInputField() => this.input.DeactivateInputField();

    public void Rebuild()
    {
      if (this.input.textComponent.enabled && this.checker.NeedRebuild())
      {
        UnityEngine.RectTransform component = this.input.textComponent.GetComponent<UnityEngine.RectTransform>();
        if ((double) this.input.textComponent.GetPreferredValues().x < (double) component.rect.xMax)
        {
          Vector2 anchoredPosition = component.anchoredPosition with
          {
            x = 0.0f
          };
          component.anchoredPosition = anchoredPosition;
          this.input.GetComponentInChildren<TMP_SelectionCaret>().GetComponent<UnityEngine.RectTransform>().anchoredPosition = component.anchoredPosition;
        }
        this.input.textComponent.enabled = this.rectOverlaps(this.input.textComponent.rectTransform, this.input.textViewport);
        this.input.textComponent.SetAllDirty();
        this.input.Rebuild(CanvasUpdate.LatePreRender);
      }
      else
        this.input.textComponent.enabled = true;
    }

    private bool rectOverlaps(UnityEngine.RectTransform rectTrans1, UnityEngine.RectTransform rectTrans2)
    {
      Rect rect1;
      ref Rect local1 = ref rect1;
      double x1 = (double) rectTrans1.localPosition.x;
      double y1 = (double) rectTrans1.localPosition.y;
      Rect rect2 = rectTrans1.rect;
      double width1 = (double) rect2.width;
      rect2 = rectTrans1.rect;
      double height1 = (double) rect2.height;
      local1 = new Rect((float) x1, (float) y1, (float) width1, (float) height1);
      Rect other;
      ref Rect local2 = ref other;
      double x2 = (double) rectTrans2.localPosition.x;
      double y2 = (double) rectTrans2.localPosition.y;
      Rect rect3 = rectTrans2.rect;
      double width2 = (double) rect3.width;
      rect3 = rectTrans2.rect;
      double height2 = (double) rect3.height;
      local2 = new Rect((float) x2, (float) y2, (float) width2, (float) height2);
      return rect1.Overlaps(other);
    }
  }
}
