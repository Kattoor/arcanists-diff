﻿// Decompiled with JetBrains decompiler
// Type: FollowBlizzard
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
