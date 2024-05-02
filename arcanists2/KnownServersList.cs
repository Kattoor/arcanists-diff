// Decompiled with JetBrains decompiler
// Type: KnownServersList
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
[Serializable]
public class KnownServersList
{
  public List<KnownServers> servers = new List<KnownServers>();

  public static void Get(MonoBehaviour g, Action<KnownServersList> a)
  {
    g.StartCoroutine(KnownServersList._Get(a));
  }

  private static IEnumerator _Get(Action<KnownServersList> a)
  {
    using (UnityWebRequest webRequest = UnityWebRequest.Get("...url..."))
    {
      yield return (object) webRequest.SendWebRequest();
      if (webRequest.isHttpError || webRequest.isNetworkError)
        Debug.LogError((object) webRequest.error);
      Action<KnownServersList> action = a;
      if (action != null)
        action(JsonUtility.FromJson<KnownServersList>(webRequest.downloadHandler.text));
    }
  }

  public void ToJson()
  {
    Global.systemCopyBuffer = JsonUtility.ToJson((object) this);
    Debug.Log((object) ("Known Severs copied to clipboard: " + Global.systemCopyBuffer));
  }
}
