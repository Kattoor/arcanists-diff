// Decompiled with JetBrains decompiler
// Type: MyPopup
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MyPopup : MonoBehaviour
{
  public GameObject item;
  public RectTransform container;
  public Image icon;
  private bool shown;

  public void Position(Vector2 p)
  {
    Vector2 vector2 = p;
    float x = (double) vector2.x > (double) (Screen.width / 2) ? 1f : 0.0f;
    if ((double) vector2.y > (double) (Screen.height / 2))
    {
      this.container.pivot = new Vector2(x, 1f);
      vector2.y -= 35f;
    }
    else
      this.container.pivot = new Vector2(x, 0.0f);
    this.container.anchoredPosition = vector2;
  }

  public void AddItem(string n, Action a, Color c)
  {
    GameObject gameObject = this.item;
    gameObject.GetComponent<TextMeshProUGUI>().text = n;
    gameObject.GetComponent<UIOnHover>();
    gameObject.SetActive(true);
  }

  public static MyPopup Show(string s, Vector2 p, Sprite icon = null)
  {
    MyPopup andApply = Controller.Instance.CreateAndApply<MyPopup>(Controller.Instance.MyPopupItem, Controller.Instance.transform);
    andApply.transform.position = (Vector3) p;
    andApply.Position(p);
    andApply.AddItem(s, (Action) null, Color.white);
    andApply.Rebuild();
    andApply.icon.gameObject.SetActive((UnityEngine.Object) icon != (UnityEngine.Object) null);
    if ((UnityEngine.Object) icon != (UnityEngine.Object) null)
      andApply.icon.sprite = icon;
    return andApply;
  }

  public static Vector2 GetPosition(RectTransform t)
  {
    Vector2 anchoredPosition = t.anchoredPosition;
    while ((UnityEngine.Object) t != (UnityEngine.Object) null && (UnityEngine.Object) t.parent != (UnityEngine.Object) Controller.Instance.transform)
    {
      t = (RectTransform) t.parent;
      anchoredPosition += t.anchoredPosition;
    }
    return anchoredPosition;
  }

  public static MyPopup Show(string s, RectTransform p, Sprite icon = null)
  {
    MyPopup andApply = Controller.Instance.CreateAndApply<MyPopup>(Controller.Instance.MyPopupItem, Controller.Instance.transform);
    ((RectTransform) andApply.transform).anchoredPosition = MyPopup.GetPosition(p);
    andApply.AddItem(s, (Action) null, Color.white);
    andApply.Rebuild();
    andApply.container.anchoredPosition = new Vector2(((RectTransform) andApply.container.GetChild(1)).sizeDelta.x / 6f, ((RectTransform) andApply.container.GetChild(1)).sizeDelta.y / 6f);
    andApply.icon.gameObject.SetActive((UnityEngine.Object) icon != (UnityEngine.Object) null);
    if ((UnityEngine.Object) icon != (UnityEngine.Object) null)
      andApply.icon.sprite = icon;
    return andApply;
  }

  public static Rect RectTransformToScreenSpace(RectTransform transform)
  {
    Vector2 size = Vector2.Scale(transform.rect.size, (Vector2) transform.lossyScale);
    return new Rect((Vector2) transform.position - size * 0.5f, size);
  }

  public void Rebuild()
  {
    Canvas.ForceUpdateCanvases();
    VerticalLayoutGroup component1 = this.transform.GetChild(0).GetComponent<VerticalLayoutGroup>();
    component1.enabled = false;
    component1.enabled = true;
    RectTransform child1 = (RectTransform) this.transform.GetChild(0);
    float x = 0.0f;
    for (int index = 2; index < child1.childCount; ++index)
    {
      TextMeshProUGUI component2 = child1.GetChild(index).GetComponent<TextMeshProUGUI>();
      if (!((UnityEngine.Object) component2 == (UnityEngine.Object) null))
      {
        component2.ForceMeshUpdate(false, false);
        float renderedWidth = component2.renderedWidth;
        if ((double) renderedWidth > (double) x)
          x = renderedWidth;
        RectTransform child2 = (RectTransform) child1.GetChild(index);
        child2.sizeDelta = new Vector2(child1.sizeDelta.x + 20f, child2.sizeDelta.y);
      }
    }
    for (int index = 2; index < child1.childCount; ++index)
    {
      ContentSizeFitter component3 = child1.GetChild(index).GetComponent<ContentSizeFitter>();
      if (!((UnityEngine.Object) component3 == (UnityEngine.Object) null))
      {
        component3.enabled = false;
        RectTransform child3 = (RectTransform) child1.GetChild(index);
        child3.sizeDelta = new Vector2(x, child3.sizeDelta.y);
      }
    }
    component1.enabled = false;
    component1.enabled = true;
  }

  public void _Close() => UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);

  public void Update()
  {
    if (Input.anyKeyDown)
    {
      this._Close();
    }
    else
    {
      float num1 = ((RectTransform) Controller.Instance.transform).sizeDelta.x * 0.5f;
      if ((double) ((RectTransform) this.container.GetChild(1)).sizeDelta.x > (double) num1)
      {
        float num2 = num1 - 1f;
        float x = num2;
        int num3 = 0;
        foreach (char ch in this.container.GetChild(1).GetComponent<TextMeshProUGUI>().text)
        {
          if (ch == '\n')
            ++num3;
        }
        float y = (float) (((double) ((RectTransform) this.container.GetChild(1)).sizeDelta.x / (double) num2 + 2.0 + (double) num3) * 28.430000305175781);
        this.container.GetChild(1).GetComponent<ContentSizeFitter>().enabled = false;
        this.container.GetComponent<ContentSizeFitter>().enabled = false;
        ((RectTransform) this.container.GetChild(1)).sizeDelta = new Vector2(x, y);
        this.container.sizeDelta = new Vector2(x + 20f, y);
      }
      if (this.shown)
        return;
      this.shown = true;
      TextMeshProUGUI component1 = this.container.GetChild(1).GetComponent<TextMeshProUGUI>();
      component1.color = component1.color with { a = 1f };
      Image component2 = this.container.GetComponent<Image>();
      component2.color = component2.color with { a = 1f };
      Image component3 = this.container.GetChild(0).GetComponent<Image>();
      component3.color = component3.color with { a = 1f };
    }
  }
}
