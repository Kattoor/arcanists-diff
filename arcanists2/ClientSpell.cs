// Decompiled with JetBrains decompiler
// Type: ClientSpell
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ClientSpell : MonoBehaviour
{
  public AudioClip castClip;
  public AudioClip explosionClip;
  public GameObject explosion;
  [NonSerialized]
  private Renderer _renderer;

  public Renderer Renderer => this._renderer;

  public void FindRenderer()
  {
    if (!((UnityEngine.Object) this._renderer == (UnityEngine.Object) null))
      return;
    this._renderer = this.GetComponent<Renderer>();
  }
}
