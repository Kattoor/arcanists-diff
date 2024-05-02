// Decompiled with JetBrains decompiler
// Type: Hyperlink
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class Hyperlink : 
  MonoBehaviour,
  IPointerClickHandler,
  IEventSystemHandler,
  IPointerEnterHandler,
  IPointerExitHandler
{
  public TMP_Text pTextMeshPro;
  private int linkIndex = -1;
  private Color32 oldColors = (Color32) Color.blue;
  private bool showdTooltip;

  public void OnPointerClick(PointerEventData eventData)
  {
    if (this.linkIndex == -1 || eventData.button != PointerEventData.InputButton.Left)
      return;
    string path = this.pTextMeshPro.textInfo.linkInfo[this.linkIndex].GetLinkID();
    if (path.StartsWith("Copy: "))
    {
      Global.systemCopyBuffer = path.Substring("Copy: ".Length);
      MyToolTip.Show("Copied to Clipboard!");
    }
    else
    {
      if (!path.Contains("http"))
        path = "http://" + path;
      Global.OpenURL(path);
    }
  }

  private void Start() => this.StartCoroutine(this.delay());

  private IEnumerator delay()
  {
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    for (int linkIndex = 0; linkIndex < this.pTextMeshPro.textInfo.linkInfo.Length; ++linkIndex)
      this.SetLinkToColor(linkIndex, (Color32) Color.blue);
  }

  public IEnumerator MouseMove()
  {
    while (true)
    {
      this.OnPointerMove((PointerEventData) null);
      yield return (object) new WaitForEndOfFrame();
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    this.StartCoroutine(this.MouseMove());
    this.OnPointerMove(eventData);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    this.StopAllCoroutines();
    if (this.linkIndex != -1)
      this.SetLinkToColor(this.linkIndex, (Color32) Color.blue);
    this.linkIndex = -1;
    MyToolTip.Close();
  }

  public void OnPointerMove(PointerEventData eventData)
  {
    int intersectingLink = TMP_TextUtilities.FindIntersectingLink(this.pTextMeshPro, Input.mousePosition, (Camera) null);
    if (this.linkIndex != -1 && intersectingLink != this.linkIndex)
      this.SetLinkToColor(this.linkIndex, (Color32) Color.blue);
    if (intersectingLink != -1 && this.linkIndex != intersectingLink)
    {
      this.SetLinkToColor(intersectingLink, (Color32) Color.white);
      MyToolTip.Show(this.pTextMeshPro.textInfo.linkInfo[intersectingLink].GetLinkID());
      this.showdTooltip = true;
    }
    else if (intersectingLink == -1 && this.showdTooltip)
    {
      MyToolTip.Close();
      this.showdTooltip = false;
    }
    this.linkIndex = intersectingLink;
  }

  private void SetLinkToColor(int linkIndex, Color32 color)
  {
    TMP_LinkInfo tmpLinkInfo = this.pTextMeshPro.textInfo.linkInfo[linkIndex];
    Color32 blue = (Color32) Color.blue;
    for (int index = 0; index < tmpLinkInfo.linkTextLength; ++index)
    {
      TMP_CharacterInfo tmpCharacterInfo = this.pTextMeshPro.textInfo.characterInfo[tmpLinkInfo.linkTextfirstCharacterIndex + index];
      int materialReferenceIndex = tmpCharacterInfo.materialReferenceIndex;
      int vertexIndex = tmpCharacterInfo.vertexIndex;
      Color32[] colors32 = this.pTextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
      if (tmpCharacterInfo.isVisible)
      {
        colors32[vertexIndex] = color;
        colors32[vertexIndex + 1] = color;
        colors32[vertexIndex + 2] = color;
        colors32[vertexIndex + 3] = color;
      }
    }
    this.pTextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
  }
}
