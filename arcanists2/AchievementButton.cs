// Decompiled with JetBrains decompiler
// Type: AchievementButton
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
