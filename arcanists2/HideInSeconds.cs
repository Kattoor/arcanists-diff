// Decompiled with JetBrains decompiler
// Type: HideInSeconds
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class HideInSeconds : MonoBehaviour
{
  public float seconds = 0.2f;
  private float cur;

  private void Update()
  {
    this.cur += Time.deltaTime;
    if ((double) this.cur <= (double) this.seconds)
      return;
    this.gameObject.SetActive(false);
    this.cur = 0.0f;
  }
}
