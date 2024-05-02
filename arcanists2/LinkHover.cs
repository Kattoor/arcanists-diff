// Decompiled with JetBrains decompiler
// Type: LinkHover
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class LinkHover : 
  MonoBehaviour,
  IPointerEnterHandler,
  IEventSystemHandler,
  IPointerExitHandler
{
  public TMP_Text pTextMeshPro;
  private int linkIndex = -1;

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.StartCoroutine(this.MouseMove());
    this.OnPointerMove(eventData);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.StopAllCoroutines();
    MyToolTip.Close();
    this.linkIndex = -1;
  }

  public void OnPointerMove(PointerEventData eventData)
  {
    int intersectingLink = TMP_TextUtilities.FindIntersectingLink(this.pTextMeshPro, Input.mousePosition, (Camera) null);
    if (intersectingLink != -1 && this.linkIndex != intersectingLink)
      MyToolTip.Show(this.pTextMeshPro.textInfo.linkInfo[intersectingLink].GetLinkID());
    else if (intersectingLink == -1)
      MyToolTip.Close();
    this.linkIndex = intersectingLink;
  }

  public IEnumerator MouseMove()
  {
    while (true)
    {
      this.OnPointerMove((PointerEventData) null);
      yield return (object) new WaitForEndOfFrame();
    }
  }
}
