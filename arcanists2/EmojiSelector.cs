// Decompiled with JetBrains decompiler
// Type: EmojiSelector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
public class EmojiSelector : MonoBehaviour
{
  public InputFieldPlus targetInput;
  public int width = 6;
  public int height = 6;
  public TMP_Text pfabIcon;
  public TMP_InputField searchBox;
  public ScrollRect _chatScrollbar;
  public Scrollbar _bar;
  public RectTransform containerHidden;
  public UIOnHover buttonLocked;
  public UIOnHover buttonUnlocked;
  private List<TMP_Text> items = new List<TMP_Text>();
  private EmojiInfo.Cat favorites = new EmojiInfo.Cat();
  private bool initialized;
  private EmojiInfo.Cat active;
  private int first;
  private int MaxVisible;
  private bool searching;
  private int steps;
  private int curStep;
  private EmojiInfo.Category curCategory;
  private Action<string> onend;
  private bool destroyOnEnd = true;
  private bool isContextMenu;
  public const string prefLocked = "emojilocked";

  public static EmojiSelector Create(Action<string> action, Transform parent = null)
  {
    EmojiSelector andApply = Controller.Instance.CreateAndApply<EmojiSelector>(Controller.Instance.emojiSelector, (UnityEngine.Object) parent != (UnityEngine.Object) null ? parent : Controller.Instance.transform);
    andApply.onend = action;
    ((RectTransform) andApply.transform).anchoredPosition = Vector2.zero;
    andApply.buttonLocked.gameObject.SetActive(false);
    andApply.buttonUnlocked.gameObject.SetActive(false);
    andApply.gameObject.SetActive(true);
    andApply.isContextMenu = true;
    return andApply;
  }

  public void SetCustomOnEnd(Action<string> e)
  {
    this.onend = e;
    this.destroyOnEnd = false;
  }

  private void Awake()
  {
    this.LoadFavorites();
    this._bar.onValueChanged.AddListener(new UnityAction<float>(this.Scroll));
  }

  public void OnEnable()
  {
    if (!this.initialized)
    {
      this.initialized = true;
      this.MaxVisible = this.width * this.height;
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        for (int index2 = 0; index2 < this.width; ++index2)
          this.items.Add(UnityEngine.Object.Instantiate<TMP_Text>(this.pfabIcon, this.pfabIcon.transform.parent));
      }
      this.Display(this.favorites.list.Count > 0 ? EmojiInfo.Category.Favorites : EmojiInfo.Category.None);
    }
    this.SetLockedButtons(Global.GetPrefBool("emojilocked", false));
  }

  public void OnEditSearch(string n)
  {
    if (string.IsNullOrEmpty(n))
    {
      this.searching = false;
      this.SetSize(EmojiInfo.searchResults.list.Count);
      this.Render(0, true);
      this._chatScrollbar.normalizedPosition = new Vector2(0.0f, 1f);
    }
    else
    {
      EmojiInfo.Search(n);
      this.searching = true;
      this.SetSize(EmojiInfo.searchResults.list.Count);
      this.Render(0, true);
      this._chatScrollbar.normalizedPosition = new Vector2(0.0f, 1f);
    }
  }

  public void Display(int category)
  {
    if (this.searching)
    {
      this.searching = false;
      this.searchBox.SetTextWithoutNotify("");
    }
    this.Display((EmojiInfo.Category) category);
  }

  public void Display(EmojiInfo.Category category)
  {
    EmojiInfo.Cat cat;
    switch (category)
    {
      case EmojiInfo.Category.None:
        cat = EmojiInfo.allEmoji;
        break;
      case EmojiInfo.Category.Favorites:
        cat = this.favorites;
        break;
      default:
        cat = EmojiInfo.categories[(int) category];
        break;
    }
    this.active = cat;
    this.curCategory = category;
    this.SetSize();
    this.Render(0, true);
    this._chatScrollbar.normalizedPosition = new Vector2(0.0f, 1f);
  }

  private void SetSize(int count = -1)
  {
    if (count == -1)
      count = this.active.list.Count;
    this.steps = (count - this.MaxVisible + this.width) / this.width + ((count - this.MaxVisible + this.width) % this.width == 0 ? 0 : 1);
    this._bar.numberOfSteps = this.steps > 1 ? this.steps : 0;
    this._chatScrollbar.scrollSensitivity = this.steps < 4 ? (float) ((5 - this.steps) * 26) : 26f;
    this.containerHidden.sizeDelta = new Vector2(this.containerHidden.sizeDelta.x, (float) ((count + this.MaxVisible - 1) / this.MaxVisible * (this.height * 26)));
  }

  public void Render(int f, bool force = false)
  {
    EmojiInfo.Cat cat = this.searching ? EmojiInfo.searchResults : this.active;
    if (f >= (cat.list.Count + this.width - 1) / this.width * this.width - this.MaxVisible)
      f = (cat.list.Count + this.width - 1) / this.width * this.width - this.MaxVisible;
    if (f < 0)
      f = 0;
    if (this.first == f && !force)
      return;
    this.first = f;
    int index = 0;
    for (int first = this.first; first < cat.list.Count && index < this.items.Count; ++first)
    {
      this.items[index].text = "<sprite name=\"" + cat.list[first].realName + "\">";
      this.items[index].name = cat.list[first].name;
      this.items[index].gameObject.SetActive(true);
      ++index;
    }
    for (; index < this.items.Count; ++index)
      this.items[index].gameObject.SetActive(false);
  }

  public void Scroll(float v)
  {
    int num1 = (int) ((double) (this.active.list.Count - this.MaxVisible + this.width) * (1.0 - (double) v));
    if (num1 + this.MaxVisible >= this.active.list.Count)
    {
      num1 = this.active.list.Count - this.MaxVisible;
      if (num1 < 0)
        num1 = 0;
    }
    if (num1 % this.width != 0)
    {
      int num2 = num1 % this.width;
      if (num2 > 3 || (double) v < 0.25)
        num1 += this.width - num2;
      else
        num1 -= num2;
    }
    int f = Mathf.Clamp(num1, 0, this.active.list.Count - 1);
    this.curStep = f / this.width;
    this.Render(f);
  }

  public void ClickLock()
  {
    bool flag = !Global.GetPrefBool("emojilocked", false);
    Global.SetPrefBool("emojilocked", flag);
    Global.SetPrefBool("prefshowemoji", flag);
    this.SetLockedButtons(flag);
  }

  public void SetLockedButtons(bool v)
  {
    if (this.isContextMenu)
      return;
    this.buttonLocked.gameObject.SetActive(v);
    this.buttonUnlocked.gameObject.SetActive(!v);
  }

  public void RightClickEmoji(TMP_Text t) => this.AddToFavorites(t.name, t.text);

  public void ClickEmoji(TMP_Text t)
  {
    if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.touchCount > 1)
      this.AddToFavorites(t.name, t.text);
    else if (this.onend != null)
    {
      this.onend(t.text);
      if (!this.destroyOnEnd)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      this.targetInput.AddEmoji(t.text);
      if (Global.GetPrefBool("emojilocked", false) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        return;
      this.gameObject.SetActive(false);
    }
  }

  public void OnHover(TMP_Text t) => MyToolTip.Show(t.name);

  public void OnLeave() => MyToolTip.Close();

  public void AddToFavorites(string n, string emoji)
  {
    int index = this.favorites.list.FindIndex((Predicate<Emoji>) (z => string.Equals(z.name, n)));
    if (index >= 0)
    {
      this.favorites.list.RemoveAt(index);
      ChatBox.Instance?.NewChatMsg("Removed :" + n + ": (" + emoji + ") from favorites", (Color) ColorScheme.GetColor(Global.ColorSystem));
    }
    else
    {
      Emoji byName = EmojiInfo.FindByName(n);
      if (byName != null)
      {
        this.favorites.list.Add(byName);
        ChatBox.Instance?.NewChatMsg("Added :" + n + ": (" + emoji + ") to favorites", (Color) ColorScheme.GetColor(Global.ColorSystem));
      }
    }
    if (this.curCategory == EmojiInfo.Category.Favorites)
    {
      this.SetSize();
      this.Render(this.first, true);
    }
    this.SaveFavorites();
  }

  public void SaveFavorites()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter = new myBinaryWriter((Stream) memoryStream))
      {
        myBinaryWriter.Write(this.favorites.list.Count);
        foreach (Emoji emoji in this.favorites.list)
          myBinaryWriter.Write(emoji.name);
      }
      File.WriteAllBytes(Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "favoriteEmoji.txt", memoryStream.ToArray());
    }
  }

  public void ClickDestroy() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void LoadFavorites()
  {
    string path = Application.persistentDataPath + Path.DirectorySeparatorChar.ToString() + "favoriteEmoji.txt";
    if (!File.Exists(path))
      return;
    try
    {
      using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(path)))
      {
        using (myBinaryReader myBinaryReader = new myBinaryReader((Stream) memoryStream))
        {
          int num = myBinaryReader.ReadInt32();
          for (int index = 0; index < num; ++index)
          {
            Emoji byName = EmojiInfo.FindByName(myBinaryReader.ReadString());
            if (byName != null)
              this.favorites.list.Add(byName);
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
  }
}
