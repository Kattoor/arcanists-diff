

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZComponent
{
  internal bool isNull;
  public Transform _transform;
  public GameObject _gameObject;

  public static bool IsNull(ZComponent z) => (object) z == null || z.isNull;

  public void SetNull() => this.isNull = true;

  public static bool operator ==(ZComponent a, object b)
  {
    if (b != null)
      return (object) a == b;
    return (object) a == null || a.isNull;
  }

  public static bool operator !=(ZComponent a, object b)
  {
    if (b != null)
      return (object) a != b;
    return (object) a != null && !a.isNull;
  }

  public override bool Equals(object obj) => base.Equals(obj);

  public override int GetHashCode() => base.GetHashCode();

  public static T Instantiate<T>(T t, Vector3 l, Quaternion q, Transform p) where T : Object
  {
    return Object.Instantiate<T>(t, l, q, p);
  }

  public static T Instantiate<T>(T t, Vector3 l, Quaternion q) where T : Object
  {
    return Object.Instantiate<T>(t, l, q);
  }

  public static T Instantiate<T>(T t, Vector2 l, Quaternion q, Transform p) where T : Object
  {
    return Object.Instantiate<T>(t, (Vector3) l, q, p);
  }

  public static ZCreature Instantiate(ZCreature t, Vector2 l, Quaternion q, Transform p)
  {
    ZCreature z = new ZCreature();
    z.Clone(t);
    z.clientObj = ZComponent.Instantiate<Creature>(t.clientObj, l, q, p);
    return z;
  }

  public static T Instantiate<T>(T t, Vector2 l, Quaternion q) where T : Object
  {
    return Object.Instantiate<T>(t, (Vector3) l, q);
  }

  public static void Destroy<T>(T t) where T : Object
  {
    if (!((Object) t != (Object) null))
      return;
    Object.Destroy((Object) t);
  }

  public static void Destroy<T>(T t, float time) where T : Object
  {
    if (!((Object) t != (Object) null))
      return;
    Object.Destroy((Object) t, time);
  }

  public static void DestroyImmediate<T>(T t) where T : Object
  {
    if (!((Object) t != (Object) null))
      return;
    Object.DestroyImmediate((Object) t);
  }

  public void GetComponentsInChildren<T>(bool v, List<T> list)
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.gameObject.GetComponentsInChildren<T>(v, list);
  }

  public Transform transform
  {
    get => (Object) this._transform == (Object) null ? (Transform) null : this._transform;
    set => this._transform = value;
  }

  public GameObject gameObject
  {
    get => (Object) this._gameObject == (Object) null ? (GameObject) null : this._gameObject;
    set => this._gameObject = value;
  }
}
