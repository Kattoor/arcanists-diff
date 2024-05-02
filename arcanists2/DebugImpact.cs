// Decompiled with JetBrains decompiler
// Type: DebugImpact
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
