

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
