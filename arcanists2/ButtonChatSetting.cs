// Decompiled with JetBrains decompiler
// Type: ButtonChatSetting
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
