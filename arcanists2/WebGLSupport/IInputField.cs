// Decompiled with JetBrains decompiler
// Type: WebGLSupport.IInputField
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
