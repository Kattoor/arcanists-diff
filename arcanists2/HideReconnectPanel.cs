

using UnityEngine;

#nullable disable
public class HideReconnectPanel : MonoBehaviour
{
  private void Update()
  {
    if (!Client.isConnected)
      return;
    this.gameObject.SetActive(false);
  }
}
