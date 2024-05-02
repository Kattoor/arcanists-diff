

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class MyToolTip : MonoBehaviour
{
  public GameObject item;
  public RectTransform container;
  private bool shown;
  private bool calculateOnce;

  public static MyToolTip instance { get; private set; }

  private void Awake() => MyToolTip.instance = this;

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) MyToolTip.instance == (UnityEngine.Object) this))
      return;
    MyToolTip.instance = (MyToolTip) null;
  }

  public void Position(bool forceLeft = false)
  {
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.y += 25f;
    float x = (double) mousePosition.x > (double) (Screen.width / 2) | forceLeft ? 1f : 0.0f;
    if ((double) mousePosition.y > (double) (Screen.height / 2))
    {
      this.container.pivot = new Vector2(x, 1f);
      mousePosition.y -= 35f;
    }
    else
      this.container.pivot = new Vector2(x, 0.0f);
    this.container.position = mousePosition;
  }

  public TextMeshProUGUI AddItem(string n, Action a, Color c)
  {
    GameObject gameObject = this.item;
    TextMeshProUGUI component = gameObject.GetComponent<TextMeshProUGUI>();
    component.text = n;
    gameObject.GetComponent<UIOnHover>();
    gameObject.SetActive(true);
    return component;
  }

  public static void Show(string s, float time = -1f)
  {
    if ((UnityEngine.Object) MyToolTip.instance != (UnityEngine.Object) null)
      MyToolTip.instance._Close();
    MyToolTip andApply = Controller.Instance.CreateAndApply<MyToolTip>(Controller.Instance.MenuToolTip, Controller.Instance.transform);
    andApply.Position();
    andApply.AddItem(s, (Action) null, Color.white);
    andApply.Rebuild();
    if ((double) time <= 0.0)
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) andApply.gameObject, time);
  }

  public static void Show(string s, HorizontalAlignmentOptions align, float time = -1f)
  {
    if ((UnityEngine.Object) MyToolTip.instance != (UnityEngine.Object) null)
      MyToolTip.instance._Close();
    MyToolTip andApply = Controller.Instance.CreateAndApply<MyToolTip>(Controller.Instance.MenuToolTip, Controller.Instance.transform);
    andApply.Position();
    andApply.AddItem(s, (Action) null, Color.white).horizontalAlignment = align;
    andApply.Rebuild();
    if ((double) time <= 0.0)
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) andApply.gameObject, time);
  }

  public static void Show2(string s, bool forceLeft = true)
  {
    if ((UnityEngine.Object) MyToolTip.instance != (UnityEngine.Object) null)
      MyToolTip.instance._Close();
    MyToolTip andApply = Controller.Instance.CreateAndApply<MyToolTip>(Controller.Instance.MenuToolTip2, Controller.Instance.transform);
    andApply.Position(forceLeft);
    andApply.AddItem(s, (Action) null, Color.white);
    andApply.Rebuild();
  }

  public void Rebuild()
  {
    Canvas.ForceUpdateCanvases();
    VerticalLayoutGroup component = this.transform.GetChild(0).GetComponent<VerticalLayoutGroup>();
    component.enabled = false;
    component.enabled = true;
    RectTransform child1 = (RectTransform) this.transform.GetChild(0);
    float x = 0.0f;
    for (int index = 2; index < child1.childCount; ++index)
    {
      child1.GetChild(index).GetComponent<TextMeshProUGUI>().ForceMeshUpdate(false, false);
      float renderedWidth = child1.GetChild(index).GetComponent<TextMeshProUGUI>().renderedWidth;
      if ((double) renderedWidth > (double) x)
        x = renderedWidth;
      RectTransform child2 = (RectTransform) child1.GetChild(index);
      child2.sizeDelta = new Vector2(child1.sizeDelta.x + 20f, child2.sizeDelta.y);
    }
    for (int index = 2; index < child1.childCount; ++index)
    {
      child1.GetChild(index).GetComponent<ContentSizeFitter>().enabled = false;
      RectTransform child3 = (RectTransform) child1.GetChild(index);
      child3.sizeDelta = new Vector2(x, child3.sizeDelta.y);
    }
    component.enabled = false;
    component.enabled = true;
  }

  public static void Close() => MyToolTip.instance?._Close();

  public void _Close() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void Update()
  {
    if (!EventSystem.current.IsPointerOverGameObject())
    {
      this._Close();
    }
    else
    {
      float maxX = ((RectTransform) Controller.Instance.transform).sizeDelta.x * 0.5f;
      if ((double) ((RectTransform) this.container.GetChild(1)).sizeDelta.x > (double) maxX)
        this.CalculateBounds(maxX);
      if (this.shown)
        return;
      this.CalculateBounds(((RectTransform) this.container.GetChild(1)).sizeDelta.x + 1f);
      this.shown = true;
      TextMeshProUGUI component1 = this.container.GetChild(1).GetComponent<TextMeshProUGUI>();
      Color color = component1.color with { a = 1f };
      component1.color = color;
      Image component2 = this.container.GetComponent<Image>();
      color = component2.color with { a = 1f };
      component2.color = color;
      Image component3 = this.container.GetChild(0).GetComponent<Image>();
      color = component3.color with { a = 1f };
      component3.color = color;
    }
  }

  private void CalculateBounds(float maxX)
  {
    if (this.calculateOnce)
      return;
    this.calculateOnce = true;
    if ((double) this.container.sizeDelta.x < (double) maxX + 100.0)
    {
      this.container.GetChild(1).GetComponent<TextMeshProUGUI>().enableWordWrapping = false;
    }
    else
    {
      --maxX;
      float num1 = maxX;
      int num2 = 0;
      string text = this.container.GetChild(1).GetComponent<TextMeshProUGUI>().text;
      for (int index = 0; index < text.Length; ++index)
      {
        if (text[index] == '\n')
          ++num2;
      }
      int num3 = num2 + text.AllIndexesOf("<br>").Count;
      float num4 = ((RectTransform) this.container.GetChild(1)).sizeDelta.x / maxX;
      float y = (float) ((num3 > 0 || (double) num4 > 1.0 ? (double) num4 + (double) num3 + 2.0 : 1.0) * 28.430000305175781);
      this.container.GetChild(1).GetComponent<ContentSizeFitter>().enabled = false;
      this.container.GetComponent<ContentSizeFitter>().enabled = false;
      ((RectTransform) this.container.GetChild(1)).sizeDelta = new Vector2(num1 + 2f, y);
      this.container.sizeDelta = new Vector2(num1 + 20f, y);
    }
  }
}
