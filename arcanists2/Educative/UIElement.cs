// Decompiled with JetBrains decompiler
// Type: Educative.UIElement
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace Educative
{
  public class UIElement
  {
    public UIElement parent;
    [MoonSharpHidden]
    public LuaPanel container;
    [MoonSharpHidden]
    public Script script;
    private object _graphic = (object) 0;
    private string _onClick;
    private Anchor _anchor = Anchor.MiddleCenter;
    public object clickID;
    public bool clickDestroy = true;

    public bool isDead => (Object) this.container == (Object) null;

    public object graphic
    {
      get => this._graphic;
      set => this.SetGraphic(value);
    }

    public Anchor anchor
    {
      get => this._anchor;
      set
      {
        this._anchor = value;
        this.container.rectTransform.SetAnchor(this._anchor);
      }
    }

    public double width
    {
      get => (double) this.container.rectTransform.sizeDelta.x;
      set
      {
        this.container.rectTransform.sizeDelta = new Vector2((float) value, this.container.rectTransform.sizeDelta.y);
      }
    }

    public double height
    {
      get => (double) this.container.rectTransform.sizeDelta.y;
      set
      {
        this.container.rectTransform.sizeDelta = new Vector2(this.container.rectTransform.sizeDelta.x, (float) value);
      }
    }

    public Point size
    {
      get
      {
        return new Point((double) this.container.rectTransform.sizeDelta.x, (double) this.container.rectTransform.sizeDelta.y);
      }
      set => this.container.rectTransform.sizeDelta = new Vector2((float) value.x, (float) value.y);
    }

    public double x
    {
      get => (double) this.container.rectTransform.anchoredPosition.x;
      set
      {
        this.container.rectTransform.anchoredPosition = new Vector2((float) value, this.container.rectTransform.anchoredPosition.y);
      }
    }

    public double y
    {
      get => (double) this.container.rectTransform.anchoredPosition.y;
      set
      {
        this.container.rectTransform.anchoredPosition = new Vector2(this.container.rectTransform.anchoredPosition.x, (float) value);
      }
    }

    public Point position
    {
      get
      {
        return new Point((double) this.container.rectTransform.anchoredPosition.x, (double) this.container.rectTransform.anchoredPosition.y);
      }
      set
      {
        this.container.rectTransform.anchoredPosition = new Vector2((float) value.x, (float) value.y);
      }
    }

    public double pivot_x
    {
      get => (double) this.container.rectTransform.pivot.x;
      set
      {
        this.container.rectTransform.pivot = new Vector2((float) value, this.container.rectTransform.pivot.y);
      }
    }

    public double pivot_y
    {
      get => (double) this.container.rectTransform.pivot.y;
      set
      {
        this.container.rectTransform.pivot = new Vector2(this.container.rectTransform.pivot.x, (float) value);
      }
    }

    public Point pivot
    {
      get
      {
        return new Point((double) this.container.rectTransform.pivot.x, (double) this.container.rectTransform.pivot.y);
      }
      set => this.container.rectTransform.pivot = new Vector2((float) value.x, (float) value.y);
    }

    public double angle
    {
      get => (double) this.container.rectTransform.localEulerAngles.z;
      set => this.container.rectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) value);
    }

    public string onClick
    {
      get => this._onClick;
      set => this.SetCallback(value);
    }

    public string color
    {
      get => "#" + ColorUtility.ToHtmlStringRGBA(this.container.image.color);
      set
      {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(value, out color);
        this.container.image.color = color;
        this.container.button.StopAllCoroutines();
        this.container.button.NormalColor = color;
      }
    }

    public string text
    {
      get => this.container.input?.text ?? this.container.text.text ?? "";
      set
      {
        if ((Object) this.container.input != (Object) null)
          this.container.input.text = value;
        else
          this.container.text.text = value;
      }
    }

    public string textColor
    {
      get => "#" + ColorUtility.ToHtmlStringRGBA(this.container.text.color);
      set
      {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(value, out color);
        this.container.text.color = color;
      }
    }

    public int textStyle
    {
      get => (int) this.container.text.fontStyle;
      set => this.container.text.fontStyle = (FontStyles) value;
    }

    public double textSize
    {
      get => (double) this.container.text.fontSize;
      set => this.container.text.fontSize = (float) value;
    }

    public void activateInput()
    {
      if (!((Object) this.container.input != (Object) null))
        return;
      this.container.input.Select();
    }

    private void SetGraphic(object graphic)
    {
      if (graphic.GetType() == typeof (double))
      {
        int index = Mathf.Clamp((int) (double) graphic, 0, ClientResources.Instance.luaGraphic.Length - 1);
        this.container.image.sprite = ClientResources.Instance.luaGraphic[index];
        this._graphic = (object) index;
      }
      else
      {
        Sprite sprite;
        if (!(graphic.GetType() == typeof (string)) || !ClientResources.Instance.icons.TryGetValue((string) graphic, out sprite))
          return;
        this.container.image.sprite = sprite;
        this._graphic = graphic;
      }
    }

    private void SetCallback(string s)
    {
      this._onClick = s;
      if ((Object) this.container.input != (Object) null)
        return;
      this.container.button.onClick.RemoveAllListeners();
      if (!string.IsNullOrWhiteSpace(s))
      {
        Color color1 = this.container.image.color;
        this.container.button.onClick.AddListener(new UnityAction(this.OnClick));
        Color color2 = ColorHSV.LowerHigher(color1, 0.4f);
        this.container.button.HighlightedColor = color2;
        this.container.button.PressedColor = color2;
        this.container.image.raycastTarget = true;
        this.container.button.enabled = true;
      }
      else
      {
        this.container.button.enabled = false;
        this.container.image.raycastTarget = false;
      }
    }

    private void OnClick()
    {
      if (!string.IsNullOrEmpty(this._onClick))
        this.script.Call(this.script.Globals.Get(this._onClick), new object[2]
        {
          this.clickID,
          (object) this
        });
      if (!this.clickDestroy)
        return;
      this.destroyHierarchy();
    }

    private void OnInputEnd(string s)
    {
      if (!string.IsNullOrEmpty(this._onClick))
        this.script.Call(this.script.Globals.Get(this._onClick), new object[2]
        {
          (object) s,
          (object) this
        });
      if (!this.clickDestroy)
        return;
      this.destroyHierarchy();
    }

    public static UIElement createUI(Script script, UIElement parent = null)
    {
      return new UIElement()
      {
        script = script,
        parent = parent == null || !((Object) parent.container != (Object) null) ? (UIElement) null : parent,
        container = Object.Instantiate<LuaPanel>(ClientResources.Instance.luaPanel, parent == null || !((Object) parent.container != (Object) null) ? (Transform) HUD.instance.luaContainer : (Transform) parent.container.rectTransform)
      };
    }

    public static UIElement createInput(
      Script script,
      UIElement parent = null,
      string callback = null,
      bool onSubmit = true,
      bool active = true,
      bool forceFocus = true)
    {
      UIElement input = new UIElement();
      input.script = script;
      input.parent = parent == null || !((Object) parent.container != (Object) null) ? (UIElement) null : parent;
      RectTransform parent1 = parent == null || !((Object) parent.container != (Object) null) ? HUD.instance.luaContainer : parent.container.rectTransform;
      input.container = Object.Instantiate<LuaPanel>(ClientResources.Instance.luaPanel, (Transform) parent1);
      input.width = 260.0;
      input.height = 40.0;
      input.container.input = Object.Instantiate<TMP_InputField>(ClientResources.Instance.luaInput, input.container.transform);
      input.container.text = input.container.input.textComponent;
      input._onClick = callback;
      if (onSubmit)
        input.container.input.onSubmit.AddListener(new UnityAction<string>(input.OnInputEnd));
      else
        input.container.input.onEndEdit.AddListener(new UnityAction<string>(input.OnInputEnd));
      if (active)
        input.activateInput();
      if (!forceFocus)
        input.container.input.GetComponent<ForceInput>().enabled = false;
      return input;
    }

    public UIElement duplicate(Script script, UIElement parent = null)
    {
      return new UIElement()
      {
        script = script,
        parent = parent == null || !((Object) parent.container != (Object) null) ? (UIElement) null : parent,
        container = Object.Instantiate<LuaPanel>(this.container, parent == null || !((Object) parent.container != (Object) null) ? (Transform) HUD.instance.luaContainer : (Transform) parent.container.rectTransform)
      };
    }

    public void destroyHierarchy()
    {
      if ((Object) this.container == (Object) null)
        return;
      UIElement parent = this.parent;
      while (parent != null && (Object) parent.container != (Object) null && parent.parent != null && (Object) parent.parent.container != (Object) null)
        parent = parent.parent;
      if ((Object) parent?.container == (Object) null)
        this.destroy();
      else
        parent.destroy();
    }

    public void destroy()
    {
      if (!((Object) this.container != (Object) null))
        return;
      Object.Destroy((Object) this.container.gameObject);
      this.container = (LuaPanel) null;
    }
  }
}
