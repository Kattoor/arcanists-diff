

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class UIOnHoverChild : MonoBehaviour
{
  protected Image targetRenderer;
  protected Text text;
  protected TMP_Text tmpText;
  protected Outline outline;
  public float FadeDuration = 0.1f;
  public bool getImageComponetOverride;
  public bool useColorTint;
  public Color NormalColor = Color.white;
  public Color HighlightedColor = Color.white;
  public Color PressedColor = Color.white;
  public Color DisabledColor = new Color(0.576f, 0.576f, 0.576f, 1f);
  public bool useTMPTextColor;
  public bool useTextColor;
  public Color textNormalColor = Color.white;
  public Color textHighlightedColor = Color.white;
  public Color textPressedColor = Color.white;
  public Color textDisabledColor = new Color(0.376f, 0.376f, 0.376f, 1f);
  public bool useSpriteSwap;
  public Sprite NormalSprite;
  public Sprite HighlightedSprite;
  public Sprite PressedSprite;
  public Sprite DisabledSprite;
  public bool useResize;
  public float NormalSize = 1f;
  public float HighlightedSize = 1.2f;
  public float PressedSize = 1.2f;
  public float DisabledSize = 1f;
  public bool useTextResize;
  public float TextNormalSize = 1f;
  public float TextHighlightedSize = 1.2f;
  public float TextPressedSize = 1.2f;
  public float TextDisabledSize = 1f;
  public bool useActivate;
  public Behaviour[] activeBehaviours;
  public GameObject[] activeGameObjects;
  public bool NormalActive;
  public bool HighlightedActive = true;
  public bool PressedActive = true;
  public bool DisabledActive;
  private bool initialized;

  public Image GetImage => this.targetRenderer ?? this.GetComponent<Image>();

  public TMP_Text GetText => this.tmpText;

  public void ToggleInteractable(bool v, bool hovering, bool alwaysOn)
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    this.StopAllCoroutines();
    if (v)
    {
      if (hovering)
        this.OnEnter(alwaysOn);
      else
        this.OnExit(alwaysOn);
    }
    else
    {
      if (this.useColorTint)
        this.StartCoroutine(this.FadeColor(this.DisabledColor, false));
      if (this.useTextResize)
        this.StartCoroutine(this.TextSizeFade(this.TextDisabledSize));
      if (this.useSpriteSwap)
        this.SwapSprite(this.DisabledSprite);
      if (this.useTextColor)
        this.StartCoroutine(this.TextFadeColor(this.textDisabledColor));
      if (this.useTMPTextColor)
        this.StartCoroutine(this.TMP_TextFadeColor(this.textDisabledColor));
      if (this.useResize)
        this.StartCoroutine(this.SizeFade(this.DisabledSize));
      if (!this.useActivate)
        return;
      this.Activator(this.DisabledActive);
    }
  }

  public void Init()
  {
    if (this.initialized)
      return;
    if (this.useColorTint || this.useSpriteSwap || this.getImageComponetOverride)
      this.targetRenderer = this.GetComponent<Image>();
    if (this.useTextColor || this.useTextResize)
      this.text = this.GetComponent<Text>();
    if (this.useTMPTextColor)
      this.tmpText = this.GetComponent<TMP_Text>();
    this.initialized = true;
  }

  public void OnDown(bool alwaysOn = false)
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    this.StopAllCoroutines();
    if (this.useColorTint)
      this.StartCoroutine(this.FadeColor(this.PressedColor));
    if (this.useSpriteSwap)
      this.SwapSprite(this.PressedSprite);
    if (this.useTextColor)
      this.StartCoroutine(this.TextFadeColor(this.textPressedColor));
    if (this.useTMPTextColor)
      this.StartCoroutine(this.TMP_TextFadeColor(this.textPressedColor));
    if (this.useResize)
      this.StartCoroutine(this.SizeFade(this.PressedSize));
    if (this.useTextResize)
      this.StartCoroutine(this.TextSizeFade(this.TextPressedSize));
    if (!this.useActivate)
      return;
    this.Activator(this.PressedActive);
  }

  public void OnUp(bool alwaysOn = false)
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    this.StopAllCoroutines();
    this.OnEnter(alwaysOn);
  }

  public void OnEnter(bool alwaysOn = false)
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    if (alwaysOn)
    {
      this.OnDown(alwaysOn);
    }
    else
    {
      this.StopAllCoroutines();
      if (this.useColorTint)
        this.StartCoroutine(this.FadeColor(this.HighlightedColor));
      if (this.useSpriteSwap)
        this.SwapSprite(this.HighlightedSprite);
      if (this.useTextColor)
        this.StartCoroutine(this.TextFadeColor(this.textHighlightedColor));
      if (this.useTMPTextColor)
        this.StartCoroutine(this.TMP_TextFadeColor(this.textHighlightedColor));
      if (this.useResize)
        this.StartCoroutine(this.SizeFade(this.HighlightedSize));
      if (this.useTextResize)
        this.StartCoroutine(this.TextSizeFade(this.TextHighlightedSize));
      if (!this.useActivate)
        return;
      if (this.activeBehaviours.Length != 0)
        this.Activator(this.HighlightedActive);
      else
        this.Activator(alwaysOn ? this.HighlightedActive : this.NormalActive);
    }
  }

  public void OnExit(bool alwaysOn = false)
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    if (alwaysOn)
    {
      this.OnDown(alwaysOn);
    }
    else
    {
      this.StopAllCoroutines();
      if (this.useColorTint)
        this.StartCoroutine(this.FadeColor(this.NormalColor));
      if (this.useSpriteSwap)
        this.SwapSprite(this.NormalSprite);
      if (this.useTextColor)
        this.StartCoroutine(this.TextFadeColor(this.textNormalColor));
      if (this.useTMPTextColor)
        this.StartCoroutine(this.TMP_TextFadeColor(this.textNormalColor));
      if (this.useResize)
        this.StartCoroutine(this.SizeFade(this.NormalSize));
      if (this.useTextResize)
        this.StartCoroutine(this.TextSizeFade(this.TextNormalSize));
      if (!this.useActivate)
        return;
      this.Activator(this.NormalActive);
    }
  }

  private void Awake() => this.Init();

  private void Activator(bool v)
  {
    foreach (Behaviour activeBehaviour in this.activeBehaviours)
      activeBehaviour.enabled = v;
    foreach (GameObject activeGameObject in this.activeGameObjects)
      activeGameObject.SetActive(v);
  }

  private void SwapSprite(Sprite s)
  {
    if ((Object) s == (Object) null)
      return;
    if ((Object) this.targetRenderer == (Object) null)
    {
      this.targetRenderer = this.GetComponent<Image>();
      if ((Object) this.targetRenderer == (Object) null)
        return;
    }
    this.targetRenderer.sprite = s;
  }

  private IEnumerator FadeColor(Color c, bool useTime = true)
  {
    UIOnHoverChild uiOnHoverChild = this;
    if ((Object) uiOnHoverChild.targetRenderer == (Object) null)
    {
      uiOnHoverChild.targetRenderer = uiOnHoverChild.GetComponent<Image>();
      if ((Object) uiOnHoverChild.targetRenderer == (Object) null)
        yield break;
    }
    Color t = uiOnHoverChild.targetRenderer.color;
    float time = 0.0f;
    while ((double) time < 1.0 & useTime)
    {
      time += Time.deltaTime / uiOnHoverChild.FadeDuration;
      uiOnHoverChild.targetRenderer.color = Color.Lerp(t, c, time);
      yield return (object) new WaitForEndOfFrame();
    }
    uiOnHoverChild.targetRenderer.color = c;
  }

  private IEnumerator TextFadeColor(Color c)
  {
    UIOnHoverChild uiOnHoverChild = this;
    if ((Object) uiOnHoverChild.text == (Object) null)
    {
      uiOnHoverChild.text = uiOnHoverChild.GetComponent<Text>();
      if ((Object) uiOnHoverChild.text == (Object) null)
        yield break;
    }
    Color t = uiOnHoverChild.text.color;
    float time = 0.0f;
    while ((double) time < 1.0)
    {
      time += Time.deltaTime / uiOnHoverChild.FadeDuration;
      uiOnHoverChild.text.color = Color.Lerp(t, c, time);
      yield return (object) new WaitForEndOfFrame();
    }
    uiOnHoverChild.text.color = c;
  }

  private IEnumerator TMP_TextFadeColor(Color c)
  {
    UIOnHoverChild uiOnHoverChild = this;
    if ((Object) uiOnHoverChild.tmpText == (Object) null)
    {
      uiOnHoverChild.tmpText = uiOnHoverChild.GetComponent<TMP_Text>();
      if ((Object) uiOnHoverChild.tmpText == (Object) null)
        yield break;
    }
    Color t = uiOnHoverChild.tmpText.color;
    float time = 0.0f;
    while ((double) time < 1.0)
    {
      time += Time.deltaTime / uiOnHoverChild.FadeDuration;
      uiOnHoverChild.tmpText.color = Color.Lerp(t, c, time);
      yield return (object) new WaitForEndOfFrame();
    }
    uiOnHoverChild.tmpText.color = c;
  }

  private IEnumerator SizeFade(float f)
  {
    UIOnHoverChild uiOnHoverChild = this;
    float t = uiOnHoverChild.transform.localScale.x;
    float time = 0.0f;
    while ((double) time < 1.0)
    {
      time += Time.deltaTime / uiOnHoverChild.FadeDuration;
      float num = Mathf.Lerp(t, f, time);
      uiOnHoverChild.transform.localScale = new Vector3(num, num, num);
      yield return (object) new WaitForEndOfFrame();
    }
    uiOnHoverChild.transform.localScale = new Vector3(f, f, f);
  }

  private IEnumerator TextSizeFade(float f)
  {
    float t = (float) this.text.fontSize;
    float time = 0.0f;
    while ((double) time < 1.0)
    {
      time += Time.deltaTime / this.FadeDuration;
      this.text.fontSize = (int) Mathf.Lerp(t, f, time);
      yield return (object) new WaitForEndOfFrame();
    }
    this.text.fontSize = (int) f;
  }

  public void EditorDown()
  {
    if (this.useColorTint)
      this.RendererColor(this.PressedColor);
    if (this.useSpriteSwap)
      this.SwapSprite(this.PressedSprite);
    if (this.useTextColor)
      this.TextColor(this.textPressedColor);
    if (this.useTMPTextColor)
      this.TMP_TextColor(this.textPressedColor);
    if (this.useResize)
      this.Size(this.PressedSize);
    if (this.useTextResize)
      this.TextSize(this.TextPressedSize);
    if (!this.useActivate)
      return;
    this.Activator(this.PressedActive);
  }

  public void EditorEnter()
  {
    if (this.useColorTint)
      this.RendererColor(this.HighlightedColor);
    if (this.useSpriteSwap)
      this.SwapSprite(this.HighlightedSprite);
    if (this.useTextColor)
      this.TextColor(this.textHighlightedColor);
    if (this.useTMPTextColor)
      this.TMP_TextColor(this.textHighlightedColor);
    if (this.useResize)
      this.Size(this.HighlightedSize);
    if (this.useTextResize)
      this.TextSize(this.TextHighlightedSize);
    if (!this.useActivate)
      return;
    this.Activator(this.HighlightedActive);
  }

  public void EditorExit()
  {
    if (this.useColorTint)
      this.RendererColor(this.NormalColor);
    if (this.useSpriteSwap)
      this.SwapSprite(this.NormalSprite);
    if (this.useTextColor)
      this.TextColor(this.textNormalColor);
    if (this.useTMPTextColor)
      this.TMP_TextColor(this.textNormalColor);
    if (this.useResize)
      this.Size(this.NormalSize);
    if (this.useTextResize)
      this.TextSize(this.TextNormalSize);
    if (!this.useActivate)
      return;
    this.Activator(this.NormalActive);
  }

  public void EditorEnabled()
  {
    if (this.useColorTint)
      this.RendererColor(this.NormalColor);
    if (this.useSpriteSwap)
      this.SwapSprite(this.NormalSprite);
    if (this.useTextColor)
      this.TextColor(this.textNormalColor);
    if (this.useTMPTextColor)
      this.TMP_TextColor(this.textNormalColor);
    if (this.useResize)
      this.Size(this.NormalSize);
    if (this.useTextResize)
      this.TextSize(this.TextNormalSize);
    if (!this.useActivate)
      return;
    this.Activator(this.NormalActive);
  }

  public void EditorDisabled()
  {
    if (this.useColorTint)
      this.RendererColor(this.DisabledColor);
    if (this.useSpriteSwap)
      this.SwapSprite(this.DisabledSprite);
    if (this.useTextColor)
      this.TextColor(this.textDisabledColor);
    if (this.useTMPTextColor)
      this.TMP_TextColor(this.textDisabledColor);
    if (this.useResize)
      this.Size(this.DisabledSize);
    if (this.useTextResize)
      this.TextSize(this.TextDisabledSize);
    if (!this.useActivate)
      return;
    this.Activator(this.DisabledActive);
  }

  private void RendererColor(Color c)
  {
    if ((Object) this.targetRenderer == (Object) null)
    {
      this.targetRenderer = this.GetComponent<Image>();
      if ((Object) this.targetRenderer == (Object) null)
        return;
    }
    this.targetRenderer.color = c;
  }

  private void TextColor(Color c)
  {
    if ((Object) this.text == (Object) null)
    {
      this.text = this.GetComponent<Text>();
      if ((Object) this.text == (Object) null)
        return;
    }
    this.text.color = c;
  }

  private void TMP_TextColor(Color c)
  {
    if ((Object) this.tmpText == (Object) null)
    {
      this.tmpText = this.GetComponent<TMP_Text>();
      if ((Object) this.tmpText == (Object) null)
        return;
    }
    this.tmpText.color = c;
  }

  private void Size(float f) => this.transform.localScale = new Vector3(f, f, f);

  private void TextSize(float f) => this.text.fontSize = (int) f;
}
