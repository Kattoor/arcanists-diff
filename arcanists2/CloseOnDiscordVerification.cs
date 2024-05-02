// Decompiled with JetBrains decompiler
// Type: CloseOnDiscordVerification
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
