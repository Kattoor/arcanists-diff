﻿// Decompiled with JetBrains decompiler
// Type: WebGLSupport.Detail.RebuildChecker
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace WebGLSupport.Detail
{
  public class RebuildChecker
  {
    private IInputField input;
    private string beforeString;
    private int beforeCaretPosition;
    private int beforeSelectionFocusPosition;
    private int beforeSelectionAnchorPosition;

    public RebuildChecker(IInputField input) => this.input = input;

    public bool NeedRebuild(bool debug = false)
    {
      bool flag = false;
      if (this.beforeString != this.input.text)
      {
        if (debug)
          Debug.Log((object) string.Format("beforeString : {0} != {1}", (object) this.beforeString, (object) this.input.text));
        this.beforeString = this.input.text;
        flag = true;
      }
      if (this.beforeCaretPosition != this.input.caretPosition)
      {
        if (debug)
          Debug.Log((object) string.Format("beforeCaretPosition : {0} != {1}", (object) this.beforeCaretPosition, (object) this.input.caretPosition));
        this.beforeCaretPosition = this.input.caretPosition;
        flag = true;
      }
      if (this.beforeSelectionFocusPosition != this.input.selectionFocusPosition)
      {
        if (debug)
          Debug.Log((object) string.Format("beforeSelectionFocusPosition : {0} != {1}", (object) this.beforeSelectionFocusPosition, (object) this.input.selectionFocusPosition));
        this.beforeSelectionFocusPosition = this.input.selectionFocusPosition;
        flag = true;
      }
      if (this.beforeSelectionAnchorPosition != this.input.selectionAnchorPosition)
      {
        if (debug)
          Debug.Log((object) string.Format("beforeSelectionAnchorPosition : {0} != {1}", (object) this.beforeSelectionAnchorPosition, (object) this.input.selectionAnchorPosition));
        this.beforeSelectionAnchorPosition = this.input.selectionAnchorPosition;
        flag = true;
      }
      return flag;
    }
  }
}
