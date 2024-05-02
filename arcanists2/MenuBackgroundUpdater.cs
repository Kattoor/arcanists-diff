

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MenuBackgroundUpdater : MonoBehaviour
{
  public Image bgImage;
  public Image fgImage;

  private void Awake() => MenuBackgroundUpdater.SwitchBackgrounds(this.bgImage, this.fgImage);

  public static void SwitchBackgrounds(Image bgImage, Image fgImage)
  {
    if (!((Object) bgImage != (Object) null))
      return;
    int e = PlayerPrefs.GetInt("prefsandboxmaps", 0);
    if (e < 0 || e >= ClientResources.Instance._maps.Length)
      e = 0;
    while (e == 11)
      e = Random.Range(0, ClientResources.Instance._maps.Length);
    int mapIndex = GameFacts.GetMapIndex(GameFacts.MapFromIndex(e));
    fgImage.sprite = ClientResources.Instance._scaled_maps[e];
    bgImage.sprite = ClientResources.Instance._scaled_mapBgs[mapIndex];
    if (Client.colorScheme == null)
      return;
    fgImage.color = Client.colorScheme.backgroundColor;
    bgImage.color = Client.colorScheme.backgroundColor;
  }
}
