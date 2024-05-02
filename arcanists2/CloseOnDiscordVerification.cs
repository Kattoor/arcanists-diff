// Decompiled with JetBrains decompiler
// Type: CloseOnDiscordVerification
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
