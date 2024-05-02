// Decompiled with JetBrains decompiler
// Type: AchievementButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class AchievementButton : MonoBehaviour
{
  public RectTransform rect;
  public Image image;
  public UIOnHover button;
  public Achievement achievement;

  public void OnClick() => AchievementsMenu.Instance.OnClick(this, this.achievement);

  public void OnHover() => AchievementsMenu.Instance.OnEnter(this.achievement);

  public void OnExit() => AchievementsMenu.Instance.OnExit();
}
