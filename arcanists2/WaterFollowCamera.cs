// Decompiled with JetBrains decompiler
// Type: WaterFollowCamera
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WaterFollowCamera : MonoBehaviour
{
  private Transform _cam;
  private Vector3 pos = Vector3.zero;

  private void Start() => this._cam = Camera.main.transform;

  private void LateUpdate()
  {
    this.pos.x = this._cam.position.x;
    this.transform.position = this.pos;
  }
}
