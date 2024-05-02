

using System.Collections;
using UnityEngine;

#nullable disable
public class CloseOnDiscordVerification : MonoBehaviour
{
  private void Start() => this.StartCoroutine(this.Close());

  private IEnumerator Close()
  {
    CloseOnDiscordVerification discordVerification = this;
    do
    {
      yield return (object) new WaitForSecondsRealtime(5f);
    }
    while (Client.MyAccount.discord == 0UL);
    discordVerification.gameObject.SetActive(false);
  }
}
