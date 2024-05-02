

using System.Collections.Generic;

#nullable disable
namespace UnityEngine.UI.Extensions
{
  [AddComponentMenu("UI/Effects/Extensions/Nicer Outline")]
  public class NicerOutline : BaseMeshEffect
  {
    [SerializeField]
    private Color m_EffectColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    [SerializeField]
    private Vector2 m_EffectDistance = new Vector2(1f, -1f);
    [SerializeField]
    private bool m_UseGraphicAlpha = true;

    public Color effectColor
    {
      get => this.m_EffectColor;
      set
      {
        this.m_EffectColor = value;
        if (!((Object) this.graphic != (Object) null))
          return;
        this.graphic.SetVerticesDirty();
      }
    }

    public Vector2 effectDistance
    {
      get => this.m_EffectDistance;
      set
      {
        if ((double) value.x > 600.0)
          value.x = 600f;
        if ((double) value.x < -600.0)
          value.x = -600f;
        if ((double) value.y > 600.0)
          value.y = 600f;
        if ((double) value.y < -600.0)
          value.y = -600f;
        if (this.m_EffectDistance == value)
          return;
        this.m_EffectDistance = value;
        if (!((Object) this.graphic != (Object) null))
          return;
        this.graphic.SetVerticesDirty();
      }
    }

    public bool useGraphicAlpha
    {
      get => this.m_UseGraphicAlpha;
      set
      {
        this.m_UseGraphicAlpha = value;
        if (!((Object) this.graphic != (Object) null))
          return;
        this.graphic.SetVerticesDirty();
      }
    }

    protected void ApplyShadowZeroAlloc(
      List<UIVertex> verts,
      Color32 color,
      int start,
      int end,
      float x,
      float y)
    {
      int num = verts.Count * 2;
      if (verts.Capacity < num)
        verts.Capacity = num;
      for (int index = start; index < end; ++index)
      {
        UIVertex vert = verts[index];
        verts.Add(vert);
        Vector3 position = vert.position;
        position.x += x;
        position.y += y;
        vert.position = position;
        Color32 color32 = color;
        if (this.m_UseGraphicAlpha)
          color32.a = (byte) ((int) color32.a * (int) verts[index].color.a / (int) byte.MaxValue);
        vert.color = color32;
        verts[index] = vert;
      }
    }

    protected void ApplyShadow(
      List<UIVertex> verts,
      Color32 color,
      int start,
      int end,
      float x,
      float y)
    {
      int num = verts.Count * 2;
      if (verts.Capacity < num)
        verts.Capacity = num;
      this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
    }

    public override void ModifyMesh(VertexHelper vh)
    {
      if (!this.IsActive())
        return;
      List<UIVertex> uiVertexList = new List<UIVertex>();
      vh.GetUIVertexStream(uiVertexList);
      Text component = this.GetComponent<Text>();
      float num = 1f;
      if ((bool) (Object) component && component.resizeTextForBestFit)
        num = (float) component.cachedTextGenerator.fontSizeUsedForBestFit / (float) (component.resizeTextMaxSize - 1);
      float x = this.effectDistance.x * num;
      float y = this.effectDistance.y * num;
      int start1 = 0;
      int count1 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start1, uiVertexList.Count, x, y);
      int start2 = count1;
      int count2 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start2, uiVertexList.Count, x, -y);
      int start3 = count2;
      int count3 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start3, uiVertexList.Count, -x, y);
      int start4 = count3;
      int count4 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start4, uiVertexList.Count, -x, -y);
      int start5 = count4;
      int count5 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start5, uiVertexList.Count, x, 0.0f);
      int start6 = count5;
      int count6 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start6, uiVertexList.Count, -x, 0.0f);
      int start7 = count6;
      int count7 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start7, uiVertexList.Count, 0.0f, y);
      int start8 = count7;
      int count8 = uiVertexList.Count;
      this.ApplyShadow(uiVertexList, (Color32) this.effectColor, start8, uiVertexList.Count, 0.0f, -y);
      vh.Clear();
      vh.AddUIVertexTriangleStream(uiVertexList);
    }
  }
}
