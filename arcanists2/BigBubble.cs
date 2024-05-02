// Decompiled with JetBrains decompiler
// Type: BigBubble
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
