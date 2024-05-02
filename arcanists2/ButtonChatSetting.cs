// Decompiled with JetBrains decompiler
// Type: ButtonChatSetting
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ButtonChatSetting : MonoBehaviour
{
  public Image icon;
  public TextMeshProUGUI text;
  public ChatSetting currentSetting;

  public void Set(ChatSetting x)
  {
    this.currentSetting = x;
    this.icon.sprite = ClientResources.Instance._iconsChatFilter[(int) x];
    this.text.text = x.ToString();
  }
}
