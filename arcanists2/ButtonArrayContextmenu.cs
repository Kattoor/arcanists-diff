

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class ButtonArrayContextmenu : MonoBehaviour
{
  public UIOnHover pfabButton;
  public UIOnHover pfabImage;
  private int count;

  public int Count => this.count;

  public void AddItem(string n, Action a, string tooltip = null)
  {
    ++this.count;
    UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabButton, this.transform.GetChild(0));
    uiOnHover.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = n;
    uiOnHover.onClick.AddListener((UnityAction) (() =>
    {
      Action action = a;
      if (action == null)
        return;
      action();
    }));
    uiOnHover.onClick.AddListener((UnityAction) (() => MyContextMenu.CloseInstance()));
    if (!string.IsNullOrEmpty(tooltip))
    {
      uiOnHover.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(tooltip)));
      uiOnHover.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
    }
    uiOnHover.gameObject.SetActive(true);
    ((RectTransform) this.transform).sizeDelta = new Vector2((float) (this.count * 55 + 25), 55f);
  }

  public void AddImage(Sprite s, string tooltip, Action a)
  {
    ++this.count;
    UIOnHover uiOnHover = UnityEngine.Object.Instantiate<UIOnHover>(this.pfabImage, this.transform.GetChild(0));
    uiOnHover.transform.GetChild(0).GetComponent<Image>().sprite = s;
    if (!string.IsNullOrEmpty(tooltip))
    {
      uiOnHover.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(tooltip)));
      uiOnHover.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
    }
    uiOnHover.onClick.AddListener((UnityAction) (() =>
    {
      Action action = a;
      if (action == null)
        return;
      action();
    }));
    uiOnHover.onClick.AddListener((UnityAction) (() => MyContextMenu.CloseInstance()));
    uiOnHover.gameObject.SetActive(true);
    ((RectTransform) this.transform).sizeDelta = new Vector2((float) (this.count * 55 + 25), 55f);
  }
}
