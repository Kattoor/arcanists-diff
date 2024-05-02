// Decompiled with JetBrains decompiler
// Type: WebGLSupport.IInputField
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace WebGLSupport
{
  public interface IInputField
  {
    ContentType contentType { get; }

    LineType lineType { get; }

    int fontSize { get; }

    string text { get; set; }

    string placeholder { get; }

    int characterLimit { get; }

    int caretPosition { get; }

    bool isFocused { get; }

    int selectionFocusPosition { get; set; }

    int selectionAnchorPosition { get; set; }

    bool ReadOnly { get; }

    bool OnFocusSelectAll { get; }

    UnityEngine.RectTransform RectTransform();

    void ActivateInputField();

    void DeactivateInputField();

    void Rebuild();
  }
}
