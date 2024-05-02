// Decompiled with JetBrains decompiler
// Type: KeyboardLetter
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
