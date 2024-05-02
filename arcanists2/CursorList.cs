// Decompiled with JetBrains decompiler
// Type: CursorList
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CursorList : MonoBehaviour
{
  public List<CursorList.CursorInfo> cursors;

  public void SetCursor(int i, CursorMode mode = CursorMode.Auto)
  {
  }

  public static CursorList Instance { get; private set; }

  private void Awake()
  {
    CursorList.Instance = this;
    this.SetCursor(0);
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) CursorList.Instance == (UnityEngine.Object) this))
      return;
    CursorList.Instance = (CursorList) null;
  }

  [Serializable]
  public class CursorInfo
  {
    public Texture2D cursorTex;
    public Vector2 hotspot;
  }
}
