// Decompiled with JetBrains decompiler
// Type: UINavigation
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class UINavigation : MonoBehaviour
{
  public TMP_InputField previous_inputfield;
  public TMP_InputField next_inputfield;
  public bool hasSubmit;
  public UINavigation.OnClick onSubmit;

  public void OnBack()
  {
    if ((UnityEngine.Object) this.previous_inputfield != (UnityEngine.Object) null)
    {
      this.previous_inputfield.Select();
    }
    else
    {
      if (!((UnityEngine.Object) this.next_inputfield != (UnityEngine.Object) null))
        return;
      this.next_inputfield.Select();
    }
  }

  public void OnSelect()
  {
    if (!((UnityEngine.Object) this.next_inputfield != (UnityEngine.Object) null))
      return;
    this.next_inputfield.Select();
  }

  public void OnSubmit()
  {
    if (this.hasSubmit && this.onSubmit != null)
    {
      this.onSubmit.Invoke();
    }
    else
    {
      if (!((UnityEngine.Object) this.next_inputfield != (UnityEngine.Object) null))
        return;
      this.next_inputfield.Select();
    }
  }

  [Serializable]
  public class OnClick : UnityEvent
  {
  }
}
