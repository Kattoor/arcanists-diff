// Decompiled with JetBrains decompiler
// Type: CosmeticsMenuDev
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class CosmeticsMenuDev : MonoBehaviour
{
  public RectTransform pfabItem;
  public RectTransform container;
  public TMP_InputField input_name;
  public TMP_Text changed;
  public TMP_InputField inputField;
  private Cosmetics cosmetics;
  private Cosmetics validator;
  private int arrayIndex;

  public static CosmeticsMenuDev Instance { get; private set; }

  private void Update()
  {
    if (this.arrayIndex == 6)
    {
      if (Input.GetKeyDown(KeyCode.F1))
        this.ToggleSpells(0);
      else if (Input.GetKeyDown(KeyCode.F2))
        this.ToggleSpells(1);
      else if (Input.GetKeyDown(KeyCode.F3))
        this.ToggleSpells(2);
      else if (Input.GetKeyDown(KeyCode.F4))
        this.ToggleSpells(3);
      else if (Input.GetKeyDown(KeyCode.F5))
        this.ToggleSpells(4);
      else if (Input.GetKeyDown(KeyCode.F6))
        this.ToggleSpells(5);
      else if (Input.GetKeyDown(KeyCode.F7))
        this.ToggleSpells(6);
      else if (Input.GetKeyDown(KeyCode.F8))
        this.ToggleSpells(7);
      else if (Input.GetKeyDown(KeyCode.F9))
        this.ToggleSpells(8);
      else if (Input.GetKeyDown(KeyCode.F10))
        this.ToggleSpells(9);
      else if (Input.GetKeyDown(KeyCode.F11))
      {
        this.ToggleSpells(10);
      }
      else
      {
        if (!Input.GetKeyDown(KeyCode.F12))
          return;
        this.ToggleSpells(11);
      }
    }
    else
    {
      if (this.arrayIndex != 7 || !Input.GetKey(KeyCode.LeftAlt))
        return;
      for (int index = 0; index < 26; ++index)
      {
        if (Input.GetKeyDown((KeyCode) (97 + index)))
        {
          this.cosmetics.achievements[(int) byte.MaxValue - index] = !this.cosmetics.achievements[(int) byte.MaxValue - index];
          this.DisplayUpdated();
        }
      }
    }
  }

  private void ToggleSpells(int i)
  {
    if (Input.GetKey(KeyCode.LeftShift))
      i += 12;
    int num1 = i * 12;
    int num2 = (i + 1) * 12;
    for (i = num1; i < num2; ++i)
    {
      bool flag = !this.cosmetics.spells[i];
      this.cosmetics.spells[i] = flag;
      this.container.GetChild(i).GetComponent<UIOnHover>().AlwaysOn = !flag;
    }
    this.DisplayUpdated();
  }

  public static void Create(Cosmetics c, string n)
  {
    if ((Object) CosmeticsMenuDev.Instance != (Object) null)
    {
      CosmeticsMenuDev.Instance.transform.SetAsLastSibling();
    }
    else
    {
      CosmeticsMenuDev andApply = Controller.Instance.CreateAndApply<CosmeticsMenuDev>(Controller.Instance.cosmeticsMenu, Controller.Instance.transform);
      andApply.cosmetics = c;
      andApply.input_name.text = n;
      andApply.validator = new Cosmetics(c);
      andApply.ClickIndex(0);
    }
  }

  public void DisplayUpdated()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (byte index1 = 0; index1 < (byte) 8; ++index1)
    {
      for (int index2 = 0; index2 < 256; ++index2)
      {
        if (this.cosmetics.array[(int) index1][index2] != this.validator.array[(int) index1][index2])
          stringBuilder.Append((char) ((uint) index1 + 62U)).Append(":").Append(index2);
      }
    }
    this.inputField.text = stringBuilder.ToString();
  }

  public void ParseText(string s)
  {
    int num;
    for (int index1 = 0; index1 < s.Length; index1 = num + 1)
    {
      int index2 = (int) s[index1] - 62;
      if (index1 + 3 <= s.Length)
      {
        int startIndex = index1 + 2;
        num = s.IndexOf(':', index1 + 3) - 2;
        if (num < 0)
          num = s.Length - 1;
        if (index2 >= 0 && index2 <= 7)
        {
          string s1 = s.Substring(startIndex, num + 1 - startIndex);
          int index3 = 0;
          ref int local = ref index3;
          if (int.TryParse(s1, out local))
            this.cosmetics.array[index2][index3] = !this.validator.array[index2][index3];
        }
      }
      else
        break;
    }
    this.DisplayUpdated();
    this.ClickIndex(this.arrayIndex);
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
        myBinaryWriter.Write((byte) 73);
        myBinaryWriter.Write(this.input_name.text);
        List<CosmeticData> cosmeticDataList = new List<CosmeticData>();
        for (byte index1 = 0; index1 < (byte) 8; ++index1)
        {
          for (int index2 = 0; index2 < 256; ++index2)
          {
            if (this.cosmetics.array[(int) index1][index2] != this.validator.array[(int) index1][index2])
              cosmeticDataList.Add(new CosmeticData()
              {
                type = index1,
                index = (byte) index2,
                on = this.cosmetics.array[(int) index1][(int) (byte) index2]
              });
          }
        }
        myBinaryWriter.Write(cosmeticDataList.Count);
        for (int index = 0; index < cosmeticDataList.Count; ++index)
        {
          myBinaryWriter.Write(cosmeticDataList[index].type);
          myBinaryWriter.Write(cosmeticDataList[index].index);
          myBinaryWriter.Write(cosmeticDataList[index].on ? (byte) 1 : (byte) 0);
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
    Sprite[] sprites = (Sprite[]) null;
    int num = -1;
    bool spells = false;
    switch (e)
    {
      case 0:
        sprites = ClientResources.Instance._characterBody;
        break;
      case 1:
        sprites = ClientResources.Instance._characterHeads;
        break;
      case 2:
        sprites = ClientResources.Instance._characterLeftHand;
        break;
      case 3:
        sprites = ClientResources.Instance._characterRightHand;
        break;
      case 4:
        sprites = ClientResources.Instance._characterHats;
        break;
      case 5:
        sprites = ClientResources.Instance._characterBeards;
        break;
      case 6:
        sprites = ClientResources.Instance._spellicons;
        num = (int) (RandomExtensions.LastBook() + 1) * 12;
        spells = true;
        break;
      case 7:
        sprites = ClientResources.Instance._achievementIcons.ToArray();
        break;
      default:
        Debug.LogError((object) ("Invalid index " + (object) e));
        return;
    }
    if (num < 0)
      num = sprites.Length;
    for (int index = 0; index < num; ++index)
    {
      RectTransform rectTransform = Object.Instantiate<RectTransform>(this.pfabItem, (Transform) this.container);
      rectTransform.anchoredPosition = new Vector2((float) (index % 30 * 33 + 5), (float) (index / 30 * -33 - 5));
      rectTransform.GetComponent<Image>().sprite = sprites[index];
      rectTransform.gameObject.SetActive(true);
      UIOnHover u = rectTransform.GetComponent<UIOnHover>();
      byte y = (byte) index;
      u.onClick.AddListener((UnityAction) (() =>
      {
        bool flag = !this.cosmetics.array[this.arrayIndex][(int) y];
        this.cosmetics.array[this.arrayIndex][(int) y] = flag;
        u.AlwaysOn = spells ? !flag : flag;
        this.DisplayUpdated();
      }));
      u.onEnter.AddListener((UnityAction) (() => MyToolTip.Show(sprites[(int) y].name)));
      u.onExit.AddListener((UnityAction) (() => MyToolTip.Close()));
      u.AlwaysOn = spells ? !this.cosmetics.array[this.arrayIndex][(int) y] : this.cosmetics.array[this.arrayIndex][(int) y];
    }
  }

  private void Awake()
  {
    CosmeticsMenuDev.Instance = this;
    this.inputField.onSubmit.AddListener(new UnityAction<string>(this.ParseText));
  }

  private void OnDestroy()
  {
    if (!((Object) CosmeticsMenuDev.Instance == (Object) this))
      return;
    CosmeticsMenuDev.Instance = (CosmeticsMenuDev) null;
  }
}
