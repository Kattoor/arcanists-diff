

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
