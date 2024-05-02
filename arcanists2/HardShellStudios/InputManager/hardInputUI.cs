

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
