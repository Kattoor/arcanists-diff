

using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class BadgeMenuDev : MonoBehaviour
{
  public RectTransform pfabItem;
  public RectTransform container;
  public TMP_InputField input_name;
  public TMP_Text changed;
  public TMP_InputField inputField;
  private BitBools cosmetics;
  private BitBools validator;
  private int arrayIndex;

  public static BadgeMenuDev Instance { get; private set; }

  private void ToggleSpells(int i)
  {
    if (Input.GetKey(KeyCode.LeftShift))
      i += 12;
    int num1 = i * 12;
    int num2 = (i + 1) * 12;
    for (i = num1; i < num2; ++i)
    {
      bool flag = !this.cosmetics[i];
      this.cosmetics[i] = flag;
      this.container.GetChild(i).GetComponent<UIOnHover>().AlwaysOn = !flag;
    }
    this.DisplayUpdated();
  }

  public static void Create(BitBools c, string n)
  {
    if ((Object) CosmeticsMenuDev.Instance != (Object) null)
    {
      CosmeticsMenuDev.Instance.transform.SetAsLastSibling();
    }
    else
    {
      BadgeMenuDev andApply = Controller.Instance.CreateAndApply<BadgeMenuDev>(Controller.Instance.badgeMenu, Controller.Instance.transform);
      andApply.cosmetics = c;
      andApply.input_name.text = n;
      andApply.validator = new BitBools();
      andApply.validator.Copy(c);
      andApply.ClickIndex(0);
    }
  }

  public void DisplayUpdated()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < 256; ++index)
    {
      if (this.cosmetics[index] != this.validator[index])
        stringBuilder.Append(index).Append(":").Append(this.cosmetics[index] ? "T  " : "F  ");
    }
    this.inputField.text = stringBuilder.ToString();
  }

  public void ClickApplyAndExit()
  {
    this.ClickApply();
    this.ClickClose();
  }

  public void ClickApply()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write((byte) 103);
        myBinaryWriter.Write(this.input_name.text);
        List<(int, bool)> valueTupleList = new List<(int, bool)>();
        for (int index = 0; index < 256; ++index)
        {
          if (this.cosmetics[index] != this.validator[index])
            valueTupleList.Add(((int) (byte) index, this.cosmetics[(int) (byte) index]));
        }
        myBinaryWriter.Write(valueTupleList.Count);
        for (int index = 0; index < valueTupleList.Count; ++index)
        {
          myBinaryWriter.Write((byte) valueTupleList[index].Item1);
          myBinaryWriter.Write(valueTupleList[index].Item2 ? (byte) 1 : (byte) 0);
        }
      }
      Client.connection?.SendBytes(memoryStream.ToArray());
    }
  }

  public void ClickClose() => Object.Destroy((Object) this.gameObject);

  public void ClickIndex(int e)
  {
    this.container.DestroyChildern();
    this.arrayIndex = e;
    int num = -1;
    bool spells = false;
    if (num < 0)
      num = ClientResources.Instance.badges.Count;
    for (int index = 0; index < num; ++index)
    {
      RectTransform rectTransform = Object.Instantiate<RectTransform>(this.pfabItem, (Transform) this.container);
      rectTransform.anchoredPosition = new Vector2((float) (index % 30 * 33 + 5), (float) (index / 30 * -33 - 5));
      rectTransform.GetComponent<Image>().sprite = ClientResources.Instance.badges[index];
      rectTransform.gameObject.SetActive(true);
      UIOnHover u = rectTransform.GetComponent<UIOnHover>();
      byte y = (byte) index;
      u.onClick.AddListener((UnityAction) (() =>
      {
        bool flag = !this.cosmetics[(int) y];
        this.cosmetics[(int) y] = flag;
        u.AlwaysOn = spells ? !flag : flag;
        this.DisplayUpdated();
      }));
      u.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(ClientResources.Instance.badges[(int) y].name)));
      u.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
      u.AlwaysOn = spells ? !this.cosmetics[(int) y] : this.cosmetics[(int) y];
    }
  }

  private void Awake() => BadgeMenuDev.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) BadgeMenuDev.Instance == (Object) this))
      return;
    BadgeMenuDev.Instance = (BadgeMenuDev) null;
  }
}
