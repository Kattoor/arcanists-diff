

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
