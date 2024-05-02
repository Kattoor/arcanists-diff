// Decompiled with JetBrains decompiler
// Type: UIAchievement
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class UIAchievement : MonoBehaviour
{
  public RectTransform rect;
  public Image image;
  public TMP_Text text;
  private int _achievement;
  private int state;
  private float f;

  public static UIAchievement Instance { get; private set; }

  public static void Create()
  {
    if (Client.achievements.Count <= 0)
      return;
    Achievement achievement = Client.achievements[0];
    Client.achievements.RemoveAt(0);
    UIAchievement uiAchievement = Object.Instantiate<UIAchievement>(Controller.Instance.uIAchievement, Controller.Instance.transform);
    uiAchievement.rect.anchoredPosition = new Vector2(168f, 0.0f);
    uiAchievement.Init(achievement);
    ChatBox.Instance?.NewChatMsg("", "Achievement: " + Achievements.list[(int) achievement].name + " - " + Achievements.list[(int) achievement].description, (Color) ColorScheme.GetColor(Global.ColorNotification), "", ChatOrigination.System);
  }

  public void Init(Achievement e)
  {
    this._achievement = (int) e;
    this.image.sprite = ClientResources.Instance._achievementIcons[this._achievement];
    this.text.text = Achievements.list[this._achievement].name;
  }

  public void OnEnter() => MyToolTip.Show(Achievements.list[this._achievement].description);

  public void OnExit() => MyToolTip.Close();

  private void Awake()
  {
    UIAchievement.Instance = this;
    this.f = 0.0f;
  }

  private void OnDestroy()
  {
    if (!((Object) UIAchievement.Instance == (Object) this))
      return;
    UIAchievement.Instance = (UIAchievement) null;
  }

  private void Update()
  {
    this.transform.SetAsLastSibling();
    if (this.state == 0)
      this.state = 1;
    else if (this.state == 1)
    {
      this.f += Time.deltaTime;
      if ((double) this.f >= 1.0)
      {
        this.f = 1f;
        this.state = 2;
      }
      this.rect.anchoredPosition = new Vector2(this.rect.anchoredPosition.x, Mathf.Lerp(-172f, 0.0f, 1f - this.f));
    }
    else if (this.state == 2)
    {
      this.f -= Time.deltaTime / 3f;
      if ((double) this.f > 0.0)
        return;
      this.f = 0.0f;
      this.state = 3;
    }
    else
    {
      this.f += Time.deltaTime;
      if ((double) this.f > 1.0)
      {
        Object.Destroy((Object) this.gameObject);
        if (Client.achievements.Count <= 0)
          return;
        UIAchievement.Create();
      }
      else
        this.rect.anchoredPosition = new Vector2(this.rect.anchoredPosition.x, Mathf.Lerp(-172f, 0.0f, this.f));
    }
  }
}
