// Decompiled with JetBrains decompiler
// Type: ForceInput
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
