// Decompiled with JetBrains decompiler
// Type: InGameCodeEditor.CodeEditorTheme
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace InGameCodeEditor
{
  [CreateAssetMenu(fileName = "Code Editor Theme", menuName = "InGame Code Editor/Code Editor Theme")]
  [Serializable]
  public class CodeEditorTheme : ScriptableObject
  {
    public Color caretColor = (Color) new Color32((byte) 51, (byte) 51, (byte) 51, byte.MaxValue);
    public Color textColor = (Color) new Color32((byte) 51, (byte) 51, (byte) 51, byte.MaxValue);
    public Color backgroundColor = (Color) new Color32(byte.MaxValue, byte.MaxValue, (byte) 254, byte.MaxValue);
    public Color lineHighlightColor = (Color) new Color32((byte) 226, (byte) 225, (byte) 225, byte.MaxValue);
    public Color lineNumberBackgroundColor = (Color) new Color32(byte.MaxValue, byte.MaxValue, (byte) 254, byte.MaxValue);
    public Color lineNumberTextColor = (Color) new Color32((byte) 43, (byte) 145, (byte) 175, byte.MaxValue);
    public Color scrollbarColor = (Color) new Color32((byte) 193, (byte) 193, (byte) 192, byte.MaxValue);
    public bool allowSyntaxHighlighting = true;

    public static CodeEditorTheme DefaultTheme
    {
      get => ScriptableObject.CreateInstance<CodeEditorTheme>();
    }
  }
}
