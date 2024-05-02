// Decompiled with JetBrains decompiler
// Type: KnownServersList
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

  public static async Task<KnownServersList> GetServerList()
  {
    using (HttpClient client = new HttpClient())
    {
      string requestUri = "http://play.arcanists2.com/ServerList.json";
      try
      {
        using (HttpResponseMessage response = await client.GetAsync(requestUri))
        {
          response.EnsureSuccessStatusCode();
          return JsonUtility.FromJson<KnownServersList>(await response.Content.ReadAsStringAsync());
        }
      }
      catch (HttpRequestException ex)
      {
        Debug.LogError((object) ("Error getting ServerMesh Server List: " + ex.Message));
      }
    }
    return Inert.Instance.servers;
  }
}
