

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class UIPlayerName : MonoBehaviour
{
  public Text _name;
  public Text _msg;
  public Button button;

  public void SetName(string s) => this._name.text = s;

  public void SetTxt(string s) => this._msg.text = s;

  public void Set(string n, string s)
  {
    this.SetName(n);
    this.SetTxt(s);
    this.button.onClick.AddListener((UnityAction) (() => Debug.Log((object) this._name.text)));
  }

  public void SetWithColor(string n, string s, Color c)
  {
    this.SetName(n);
    this.SetTxt(s);
    this._msg.color = c;
    this.button.onClick.AddListener((UnityAction) (() => Debug.Log((object) this._name.text)));
  }
}
