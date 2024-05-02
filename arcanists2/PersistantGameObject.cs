// Decompiled with JetBrains decompiler
// Type: PersistantGameObject
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
