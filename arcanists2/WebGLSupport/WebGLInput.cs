

using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace WebGLSupport
{
  public class WebGLInput : MonoBehaviour, IComparable<WebGLInput>
  {
    private static Dictionary<int, WebGLInput> instances = new Dictionary<int, WebGLInput>();
    internal int id = -1;
    public IInputField input;
    private bool blurBlock;
    [Tooltip("show input element on canvas. this will make you select text by drag.")]
    public bool showHtmlElement;

    public static string CanvasId { get; set; }

    static WebGLInput()
    {
      WebGLInput.CanvasId = "unityContainer";
      WebGLInputPlugin.WebGLInputInit();
    }

    public int Id => this.id;

    private IInputField Setup()
    {
      if ((bool) (UnityEngine.Object) this.GetComponent<InputField>())
        return (IInputField) new WrappedInputField(this.GetComponent<InputField>());
      if ((bool) (UnityEngine.Object) this.GetComponent<TMP_InputField>())
        return (IInputField) new WrappedTMPInputField(this.GetComponent<TMP_InputField>());
      throw new Exception("Can not Setup WebGLInput!!");
    }

    private void Awake()
    {
      this.input = this.Setup();
      this.enabled = false;
      if (!Application.isMobilePlatform)
        return;
      this.gameObject.AddComponent<WebGLInputMobile>();
    }

    private RectInt GetElemetRect()
    {
      Rect screenCoordinates = this.GetScreenCoordinates(this.input.RectTransform());
      return this.showHtmlElement || Application.isMobilePlatform ? new RectInt((int) screenCoordinates.x, (int) ((double) Screen.height - ((double) screenCoordinates.y + (double) screenCoordinates.height)), (int) screenCoordinates.width, (int) screenCoordinates.height) : new RectInt((int) screenCoordinates.x, (int) ((double) Screen.height - (double) screenCoordinates.y), (int) screenCoordinates.width, 1);
    }

    public void OnSelect()
    {
      if (this.id != -1)
        throw new Exception("OnSelect : id != -1");
      RectInt elemetRect = this.GetElemetRect();
      bool isPassword = this.input.contentType == ContentType.Password;
      int fontsize = Mathf.Max(14, this.input.fontSize);
      bool isHidden = !this.showHtmlElement && !Application.isMobilePlatform;
      this.id = WebGLInputPlugin.WebGLInputCreate(WebGLInput.CanvasId, elemetRect.x, elemetRect.y, elemetRect.width, elemetRect.height, fontsize, this.input.text, this.input.placeholder, this.input.lineType != 0, isPassword, isHidden, Application.isMobilePlatform);
      WebGLInput.instances[this.id] = this;
      WebGLInputPlugin.WebGLInputEnterSubmit(this.id, this.input.lineType != LineType.MultiLineNewline);
      WebGLInputPlugin.WebGLInputOnFocus(this.id, new Action<int>(WebGLInput.OnFocus));
      WebGLInputPlugin.WebGLInputOnBlur(this.id, new Action<int>(WebGLInput.OnBlur));
      WebGLInputPlugin.WebGLInputOnValueChange(this.id, new Action<int, string>(WebGLInput.OnValueChange));
      WebGLInputPlugin.WebGLInputOnEditEnd(this.id, new Action<int, string>(WebGLInput.OnEditEnd));
      WebGLInputPlugin.WebGLInputTab(this.id, new Action<int, int>(WebGLInput.OnTab));
      WebGLInputPlugin.WebGLInputMaxLength(this.id, this.input.characterLimit > 0 ? this.input.characterLimit : 524288);
      WebGLInputPlugin.WebGLInputFocus(this.id);
      if (this.input.OnFocusSelectAll)
        WebGLInputPlugin.WebGLInputSetSelectionRange(this.id, 0, this.input.text.Length);
      WebGLWindow.OnBlurEvent += new Action(this.OnWindowBlur);
    }

    private void OnWindowBlur() => this.blurBlock = true;

    private Rect GetScreenCoordinates(RectTransform uiElement)
    {
      Vector3[] fourCornersArray = new Vector3[4];
      uiElement.GetWorldCorners(fourCornersArray);
      Canvas componentInParent = uiElement.GetComponentInParent<Canvas>();
      bool flag = componentInParent.renderMode != 0;
      if ((bool) (UnityEngine.Object) componentInParent & flag)
      {
        Camera camera = componentInParent.worldCamera;
        if (!(bool) (UnityEngine.Object) camera)
          camera = Camera.main;
        for (int index = 0; index < fourCornersArray.Length; ++index)
          fourCornersArray[index] = camera.WorldToScreenPoint(fourCornersArray[index]);
      }
      Vector3 vector3_1 = new Vector3(float.MaxValue, float.MaxValue);
      Vector3 vector3_2 = new Vector3(float.MinValue, float.MinValue);
      for (int index = 0; index < fourCornersArray.Length; ++index)
      {
        vector3_1.x = Mathf.Min(vector3_1.x, fourCornersArray[index].x);
        vector3_1.y = Mathf.Min(vector3_1.y, fourCornersArray[index].y);
        vector3_2.x = Mathf.Max(vector3_2.x, fourCornersArray[index].x);
        vector3_2.y = Mathf.Max(vector3_2.y, fourCornersArray[index].y);
      }
      return new Rect(vector3_1.x, vector3_1.y, vector3_2.x - vector3_1.x, vector3_2.y - vector3_1.y);
    }

    internal void DeactivateInputField()
    {
      if (!WebGLInput.instances.ContainsKey(this.id))
        return;
      WebGLInputPlugin.WebGLInputDelete(this.id);
      this.input.DeactivateInputField();
      WebGLInput.instances.Remove(this.id);
      this.id = -1;
      WebGLWindow.OnBlurEvent -= new Action(this.OnWindowBlur);
    }

    [MonoPInvokeCallback(typeof (Action<int>))]
    private static void OnFocus(int id)
    {
    }

    [MonoPInvokeCallback(typeof (Action<int>))]
    private static void OnBlur(int id)
    {
      WebGLInput.instances[id].StartCoroutine(WebGLInput.Blur(id));
    }

    private static IEnumerator Blur(int id)
    {
      yield return (object) null;
      if (WebGLInput.instances.ContainsKey(id))
      {
        int num = WebGLInput.instances[id].blurBlock ? 1 : 0;
        WebGLInput.instances[id].blurBlock = false;
        if (num == 0)
          WebGLInput.instances[id].DeactivateInputField();
      }
    }

    [MonoPInvokeCallback(typeof (Action<int, string>))]
    private static void OnValueChange(int id, string value)
    {
      if (!WebGLInput.instances.ContainsKey(id))
        return;
      WebGLInput instance = WebGLInput.instances[id];
      if (!instance.input.ReadOnly)
        instance.input.text = value;
      if (instance.input.contentType == ContentType.Name && string.Compare(instance.input.text, value, true) == 0)
        value = instance.input.text;
      if (!(value != instance.input.text))
        return;
      int num1 = WebGLInputPlugin.WebGLInputSelectionStart(id);
      int num2 = WebGLInputPlugin.WebGLInputSelectionEnd(id);
      int num3 = instance.input.text.Length - value.Length;
      WebGLInputPlugin.WebGLInputText(id, instance.input.text);
      WebGLInputPlugin.WebGLInputSetSelectionRange(id, num1 + num3, num2 + num3);
    }

    [MonoPInvokeCallback(typeof (Action<int, string>))]
    private static void OnEditEnd(int id, string value)
    {
      if (WebGLInput.instances[id].input.ReadOnly)
        return;
      WebGLInput.instances[id].input.text = value;
    }

    [MonoPInvokeCallback(typeof (Action<int, int>))]
    private static void OnTab(int id, int value)
    {
      WebGLInput.WebGLInputTabFocus.OnTab(WebGLInput.instances[id], value);
    }

    private void Update()
    {
      if (this.input == null || !this.input.isFocused)
      {
        this.CheckOutFocus();
      }
      else
      {
        if (!WebGLInput.instances.ContainsKey(this.id))
        {
          if (Application.isMobilePlatform)
            return;
          this.OnSelect();
        }
        else if (!WebGLInputPlugin.WebGLInputIsFocus(this.id))
        {
          if (Application.isMobilePlatform)
            return;
          WebGLInputPlugin.WebGLInputFocus(this.id);
        }
        int num1 = WebGLInputPlugin.WebGLInputSelectionStart(this.id);
        int num2 = WebGLInputPlugin.WebGLInputSelectionEnd(this.id);
        if (WebGLInputPlugin.WebGLInputSelectionDirection(this.id) == -1)
        {
          this.input.selectionFocusPosition = num1;
          this.input.selectionAnchorPosition = num2;
        }
        else
        {
          this.input.selectionFocusPosition = num2;
          this.input.selectionAnchorPosition = num1;
        }
        this.input.Rebuild();
      }
    }

    private void OnDestroy()
    {
      if (!WebGLInput.instances.ContainsKey(this.id))
        return;
      this.DeactivateInputField();
    }

    private void OnEnable() => WebGLInput.WebGLInputTabFocus.Add(this);

    private void OnDisable() => WebGLInput.WebGLInputTabFocus.Remove(this);

    public int CompareTo(WebGLInput other)
    {
      Rect screenCoordinates1 = this.GetScreenCoordinates(this.input.RectTransform());
      Rect screenCoordinates2 = this.GetScreenCoordinates(other.input.RectTransform());
      int num = screenCoordinates2.y.CompareTo(screenCoordinates1.y);
      if (num == 0)
        num = screenCoordinates1.x.CompareTo(screenCoordinates2.x);
      return num;
    }

    public void CheckOutFocus()
    {
      if (!Application.isMobilePlatform || !WebGLInput.instances.ContainsKey(this.id) || (UnityEngine.Object) EventSystem.current.currentSelectedGameObject != (UnityEngine.Object) null)
        return;
      WebGLInputPlugin.WebGLInputForceBlur(this.id);
    }

    private static class WebGLInputTabFocus
    {
      private static List<WebGLInput> inputs = new List<WebGLInput>();

      public static void Add(WebGLInput input)
      {
        WebGLInput.WebGLInputTabFocus.inputs.Add(input);
        WebGLInput.WebGLInputTabFocus.inputs.Sort();
      }

      public static void Remove(WebGLInput input)
      {
        WebGLInput.WebGLInputTabFocus.inputs.Remove(input);
      }

      public static void OnTab(WebGLInput input, int value)
      {
        if (WebGLInput.WebGLInputTabFocus.inputs.Count <= 1)
          return;
        int index = WebGLInput.WebGLInputTabFocus.inputs.IndexOf(input) + value;
        if (index < 0)
          index = WebGLInput.WebGLInputTabFocus.inputs.Count - 1;
        else if (index >= WebGLInput.WebGLInputTabFocus.inputs.Count)
          index = 0;
        WebGLInput.WebGLInputTabFocus.inputs[index].input.ActivateInputField();
      }
    }
  }
}
