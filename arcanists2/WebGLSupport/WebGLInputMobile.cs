﻿// Decompiled with JetBrains decompiler
// Type: WebGLSupport.WebGLInputMobile
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace WebGLSupport
{
  public class WebGLInputMobile : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
  {
    private static Dictionary<int, WebGLInputMobile> instances = new Dictionary<int, WebGLInputMobile>();
    private int id = -1;

    private void Awake() => this.enabled = false;

    public void OnPointerDown(PointerEventData eventData)
    {
      if (this.id != -1)
        return;
      this.id = WebGLInputMobilePlugin.WebGLInputMobileRegister(new Action<int>(WebGLInputMobile.OnTouchEnd));
      WebGLInputMobile.instances[this.id] = this;
    }

    [MonoPInvokeCallback(typeof (Action<int>))]
    private static void OnTouchEnd(int id)
    {
      WebGLInputMobile instance = WebGLInputMobile.instances[id];
      instance.GetComponent<WebGLInput>().OnSelect();
      instance.StartCoroutine(WebGLInputMobile.RegisterOnFocusOut(id));
    }

    private static IEnumerator RegisterOnFocusOut(int id)
    {
      yield return (object) null;
      WebGLInputMobilePlugin.WebGLInputMobileOnFocusOut(id, new Action<int>(WebGLInputMobile.OnFocusOut));
    }

    [MonoPInvokeCallback(typeof (Action<int>))]
    private static void OnFocusOut(int id)
    {
      WebGLInputMobile.instances[id].StartCoroutine(WebGLInputMobile.ExecFocusOut(id));
    }

    private static IEnumerator ExecFocusOut(int id)
    {
      yield return (object) null;
      WebGLInputMobile instance = WebGLInputMobile.instances[id];
      instance.GetComponent<WebGLInput>().DeactivateInputField();
      instance.id = -1;
      WebGLInputMobile.instances.Remove(id);
    }
  }
}
