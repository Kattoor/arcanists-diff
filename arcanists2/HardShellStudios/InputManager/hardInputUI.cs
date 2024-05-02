// Decompiled with JetBrains decompiler
// Type: HardShellStudios.InputManager.hardInputUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

#nullable disable
namespace HardShellStudios.InputManager
{
  public class hardInputUI : MonoBehaviour
  {
    public TMP_Text displayText;
    public string keyName;
    public bool useSecondary;
    [HideInInspector]
    public bool beingBound;
    public int buttonAction;

    public void EndBind() => this.beingBound = false;

    public void remapKey()
    {
      if (this.buttonAction == 0)
      {
        this.beingBound = true;
        hardInput.HardStartRebind(this.keyName, this.useSecondary, this.gameObject);
      }
      else if (this.buttonAction == 1)
      {
        hardInput.ResetBinding(this.keyName);
      }
      else
      {
        if (this.buttonAction != 2)
          return;
        hardInput.ResetAllBindings();
      }
    }

    private void OnGUI()
    {
      if (!((Object) this.displayText != (Object) null) || this.buttonAction != 0)
        return;
      if (!this.beingBound)
        this.displayText.text = hardInput.GetKeyName(this.keyName, this.useSecondary);
      else
        this.displayText.text = "PRESS A KEY";
    }
  }
}
