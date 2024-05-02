

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Newsfeed : MonoBehaviour
{
  public NewsfeedItem pfab;
  public RectTransform container;
  private List<(NewsType type, NewsfeedItem item)> list = new List<(NewsType, NewsfeedItem)>();

  public static Newsfeed Instance { get; private set; }

  public static void Create()
  {
    if ((Object) Newsfeed.Instance != (Object) null)
      return;
    Newsfeed.Instance = Controller.Instance.CreateAndApply<Newsfeed>(Controller.Instance.newsfeed, Controller.Instance.transform);
  }

  public static void Add(NewsType t, string s) => Newsfeed.Create();

  public static void Remove(NewsType t) => Newsfeed.Instance._Remove(t);

  private void _Remove(NewsType t)
  {
    for (int index = 0; index < this.list.Count; ++index)
    {
      if (this.list[index].type == t)
      {
        Object.Destroy((Object) this.list[index].item.gameObject);
        this.list.RemoveAt(index);
        break;
      }
    }
  }

  private void OnDestroy()
  {
    if (!((Object) Newsfeed.Instance == (Object) this))
      return;
    Newsfeed.Instance = (Newsfeed) null;
  }
}
