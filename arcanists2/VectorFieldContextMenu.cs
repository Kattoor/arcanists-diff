// Decompiled with JetBrains decompiler
// Type: VectorFieldContextMenu
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

#nullable disable
public class VectorFieldContextMenu : MonoBehaviour
{
  public TMP_InputField inputX;
  public TMP_InputField inputY;
  public UIOnHover buttonClose;
  private Action<Vector2> action;
  private Vector2 def;

  public void Init(Action<Vector2> a, Vector2 def)
  {
    this.def = def;
    this.action = a;
    this.inputX.text = def.x.ToString("0.##");
    this.inputY.text = def.y.ToString("0.##");
  }

  public void ClickOk()
  {
    this.ClickApply();
    MyContextMenu.CloseInstance();
  }

  public void ClickApply()
  {
    float result1;
    if (!float.TryParse(this.inputX.text, out result1))
      result1 = this.def.x;
    float result2;
    if (!float.TryParse(this.inputY.text, out result2))
      result2 = this.def.y;
    Action<Vector2> action = this.action;
    if (action == null)
      return;
    action(new Vector2(result1, result2));
  }
}
