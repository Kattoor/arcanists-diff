﻿// Decompiled with JetBrains decompiler
// Type: UIAlwaysOnTop
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIAlwaysOnTop : MonoBehaviour
{
  private void Update()
  {
    if (this.transform.GetSiblingIndex() == this.transform.parent.childCount - 1)
      return;
    this.transform.SetAsLastSibling();
  }
}