

using MyPolling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MyPollUIItem : MonoBehaviour
{
  public RectTransform rect;
  public Image checkbox;
  public UIOnHover toggle;
  public TMP_Text text;
  public TMP_InputField input;
  public GameObject objCheck;
  private int myIndex;

  public void Setup(int index, MyPoll.Answer item)
  {
    this.myIndex = index;
    this.text.text = item.text;
    this.input.text = item.userInput;
    this.objCheck.SetActive(item.isChecked);
    this.toggle.gameObject.SetActive(item.checkable);
    this.input.gameObject.SetActive(item.allowUserInput);
    this.gameObject.SetActive(true);
  }

  public void OnClick() => MyPollUI.Instance.SelectItem(this.myIndex);

  public void OnEdit(string s) => MyPollUI.Instance.OnEdit(this.myIndex, s);
}
