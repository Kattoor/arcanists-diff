

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
