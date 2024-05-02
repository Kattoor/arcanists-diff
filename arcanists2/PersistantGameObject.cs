// Decompiled with JetBrains decompiler
// Type: PersistantGameObject
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class PersistantGameObject : MonoBehaviour
{
  private static PersistantGameObject Instance { get; set; }

  public static GameObject Get()
  {
    if ((Object) PersistantGameObject.Instance == (Object) null)
      new GameObject(nameof (PersistantGameObject))
      {
        tag = nameof (PersistantGameObject)
      }.AddComponent<PersistantGameObject>();
    return PersistantGameObject.Instance.gameObject;
  }

  private void Awake()
  {
    if ((Object) PersistantGameObject.Instance != (Object) null)
      Debug.LogError((object) "Multiple Persistant GameObjects!!!!!!");
    PersistantGameObject.Instance = this;
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void OnApplicationQuit() => Client.OnApplicationQuit();

  private void OnDestroy()
  {
    if (!((Object) PersistantGameObject.Instance == (Object) this))
      return;
    PersistantGameObject.Instance = (PersistantGameObject) null;
  }
}
