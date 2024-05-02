

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
