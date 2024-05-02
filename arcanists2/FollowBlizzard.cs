// Decompiled with JetBrains decompiler
// Type: FollowBlizzard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FollowBlizzard : MonoBehaviour
{
  private ZEffector e;
  private Vector3 v;

  private void Start()
  {
    this.e = this.gameObject.GetComponentInParent<Effector>().serverObj;
    this.v = new Vector3(0.0f, (float) (this.e.map.Height + 300));
  }

  private void Update()
  {
    if (!((ZComponent) this.e != (object) null) || ZComponent.IsNull((ZComponent) this.e.whoSummoned))
      return;
    this.v.x = this.e.whoSummoned.transform.position.x;
    this.transform.parent.position = this.v;
  }
}
