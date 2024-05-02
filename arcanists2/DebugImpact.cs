// Decompiled with JetBrains decompiler
// Type: DebugImpact
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

#nullable disable
public class DebugImpact : MonoBehaviour
{
  public SpriteRenderer sprite;
  public TextMeshPro text;
  private static int index;

  public void Msg(string s) => this.text.text = s;

  public void Color(UnityEngine.Color c) => this.sprite.color = c;

  public static DebugImpact Create(Vector3 pos, string msg, UnityEngine.Color c)
  {
    return (DebugImpact) null;
  }
}
