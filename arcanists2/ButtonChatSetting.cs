

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
