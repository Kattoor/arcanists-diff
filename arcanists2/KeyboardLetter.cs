// Decompiled with JetBrains decompiler
// Type: KeyboardLetter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class KeyboardLetter : MonoBehaviour
{
  public Image img;
  public UIOnHover button;
  public TMP_Text txt;
  public char alternateLetter;
  public char symbolLetter;
  public char symbol2Letter;
  internal char letter;

  [ContextMenu("do")]
  public void Editor()
  {
    this.img = this.GetComponentInChildren<Image>();
    this.button = this.GetComponentInChildren<UIOnHover>();
    this.txt = this.GetComponentInChildren<TMP_Text>();
  }
}
