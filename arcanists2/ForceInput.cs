// Decompiled with JetBrains decompiler
// Type: ForceInput
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class ForceInput : MonoBehaviour
{
  public Selectable input;

  private void Update()
  {
    if (!((Object) EventSystem.current.currentSelectedGameObject != (Object) this.input.gameObject))
      return;
    this.input.Select();
  }
}
