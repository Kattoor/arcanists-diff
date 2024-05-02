

using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class KeyBindMenu : MonoBehaviour
{
  public UIOnHover toggleMouseX;
  public UIOnHover toggleMouseY;
  public UIOnHover toggleScrollWheel;
  public UIOnHover toggleMouseZoom;
  public UIOnHover toggleControls;
  public UIOnHover toggleSkipWarning;
  public UIOnHover toggleDetower;
  public GameObject panelHotkeys;

  public static KeyBindMenu Instance { get; private set; }

  public void ClickBack() => Object.Destroy((Object) this.gameObject);

  private void Awake() => KeyBindMenu.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) KeyBindMenu.Instance == (Object) this))
      return;
    KeyBindMenu.Instance = (KeyBindMenu) null;
  }

  private void Start()
  {
    this.toggleMouseX.AlwaysOn = Global.GetPrefBool("prefreversedx", false);
    this.toggleMouseY.AlwaysOn = Global.GetPrefBool("prefreversedy", false);
    this.toggleScrollWheel.AlwaysOn = Global.GetPrefBool("prefScrollWheel", true);
    this.toggleControls.onClick.AddListener(new UnityAction(this.ToggleControls));
    this.toggleControls.AlwaysOn = Global.GetPrefBool("prefControls", HUD.UseTouchControls);
    if ((Object) this.toggleMouseZoom != (Object) null)
    {
      this.toggleMouseZoom.onClick.AddListener(new UnityAction(this.MouseZoom));
      this.toggleMouseZoom.AlwaysOn = Global.GetPrefBool("prefZoomMouse", false);
    }
    this.toggleSkipWarning.onClick.AddListener((UnityAction) (() => HUD.ToggleSkipWarning(this.toggleSkipWarning, false)));
    this.toggleSkipWarning.AlwaysOn = !Global.GetPrefBool("prefskipwarning", false);
    this.toggleDetower.onClick.AddListener((UnityAction) (() =>
    {
      bool b = !Global.GetPrefBool("prefjumpdetower", false);
      Global.SetPrefBool("prefjumpdetower", b);
      if ((Object) Player.Instance != (Object) null)
        Player.Instance.jumpIsDetower = b;
      this.toggleDetower.AlwaysOn = b;
    }));
    this.toggleDetower.AlwaysOn = Global.GetPrefBool("prefjumpdetower", false);
  }

  public void ToggleHotkeys() => this.panelHotkeys.SetActive(!this.panelHotkeys.activeSelf);

  public void ToggleControls()
  {
    bool b = !Global.GetPrefBool("prefControls", HUD.UseTouchControls);
    Global.SetPrefBool("prefControls", b);
    this.toggleControls.AlwaysOn = b;
    HUD.UseTouchControls = b;
    HUD.instance?.panelControls.SetActive(b);
    HUD.instance?.buttonPing.gameObject.SetActive(b && !Client.game.isSandbox && Client.game.isTeam);
  }

  public void ResetControls() => hardInput.ResetAllBindings();

  public void ToggleMouseX()
  {
    bool b = !Global.GetPrefBool("prefreversedx", false);
    CameraMovement.reversedX = b;
    Global.SetPrefBool("prefreversedx", b);
    this.toggleMouseX.AlwaysOn = b;
  }

  public void ToggleMouseY()
  {
    bool b = !Global.GetPrefBool("prefreversedy", false);
    CameraMovement.reversedY = b;
    Global.SetPrefBool("prefreversedy", b);
    this.toggleMouseY.AlwaysOn = b;
  }

  public void ToggleScrollWheel()
  {
    bool b = !Global.GetPrefBool("prefScrollWheel", true);
    CameraMovement.allowscrollwheel = b;
    Global.SetPrefBool("prefScrollWheel", b);
    this.toggleScrollWheel.AlwaysOn = b;
  }

  public void MouseZoom()
  {
    if (!Global.GetPrefBool("prefZoomMouse", false))
    {
      Global.SetPrefBool("prefZoomMouse", true);
      this.toggleMouseZoom.AlwaysOn = true;
    }
    else
    {
      Global.SetPrefBool("prefZoomMouse", false);
      this.toggleMouseZoom.AlwaysOn = false;
    }
  }
}
