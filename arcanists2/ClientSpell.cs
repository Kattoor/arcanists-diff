

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
