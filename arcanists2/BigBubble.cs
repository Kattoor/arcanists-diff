// Decompiled with JetBrains decompiler
// Type: BigBubble
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BigBubble : MonoBehaviour
{
  public ZCreature creature;
  private Vector3 _target;
  private Vector3 _start;
  private float cur = 1f;
  public float speed = 1f;

  private void Update()
  {
    if (!((ZComponent) this.creature == (object) null) && this.creature.parent.localTurn < 0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
