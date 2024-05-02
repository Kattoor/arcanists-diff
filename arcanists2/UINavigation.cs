

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
