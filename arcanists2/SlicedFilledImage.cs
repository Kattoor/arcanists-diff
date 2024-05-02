// Decompiled with JetBrains decompiler
// Type: SlicedFilledImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.U2D;
using UnityEngine.UI;

#nullable disable
[RequireComponent(typeof (CanvasRenderer))]
[AddComponentMenu("UI/Sliced Filled Image", 11)]
public class SlicedFilledImage : 
  MaskableGraphic,
  ISerializationCallbackReceiver,
  ILayoutElement,
  ICanvasRaycastFilter
{
  private static readonly Vector3[] s_Vertices = new Vector3[4];
  private static readonly Vector2[] s_UVs = new Vector2[4];
  private static readonly Vector2[] s_SlicedVertices = new Vector2[4];
  private static readonly Vector2[] s_SlicedUVs = new Vector2[4];
  [SerializeField]
  private Sprite m_Sprite;
  [SerializeField]
  private SlicedFilledImage.FillDirection m_FillDirection;
  [Range(0.0f, 1f)]
  [SerializeField]
  private float m_FillAmount = 1f;
  [SerializeField]
  private bool m_FillCenter = true;
  [SerializeField]
  private bool m_PreserveBorder;
  [SerializeField]
  private float m_PixelsPerUnitMultiplier = 1f;
  [NonSerialized]
  private Sprite m_OverrideSprite;
  private bool m_Tracked;
  private static List<SlicedFilledImage> m_TrackedTexturelessImages = new List<SlicedFilledImage>();
  private static bool s_Initialized;

  public Sprite sprite
  {
    get => this.m_Sprite;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
        return;
      this.SetAllDirty();
      this.TrackImage();
    }
  }

  public SlicedFilledImage.FillDirection fillDirection
  {
    get => this.m_FillDirection;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetStruct<SlicedFilledImage.FillDirection>(ref this.m_FillDirection, value))
        return;
      this.SetVerticesDirty();
    }
  }

  public float fillAmount
  {
    get => this.m_FillAmount;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetStruct<float>(ref this.m_FillAmount, Mathf.Clamp01(value)))
        return;
      this.SetVerticesDirty();
    }
  }

  public bool fillCenter
  {
    get => this.m_FillCenter;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetStruct<bool>(ref this.m_FillCenter, value))
        return;
      this.SetVerticesDirty();
    }
  }

  public bool preserveBorder
  {
    get => this.m_PreserveBorder;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetStruct<bool>(ref this.m_PreserveBorder, value))
        return;
      this.SetVerticesDirty();
    }
  }

  public float pixelsPerUnitMultiplier
  {
    get => this.m_PixelsPerUnitMultiplier;
    set => this.m_PixelsPerUnitMultiplier = Mathf.Max(0.01f, value);
  }

  public float pixelsPerUnit
  {
    get
    {
      float num1 = 100f;
      if ((bool) (UnityEngine.Object) this.activeSprite)
        num1 = this.activeSprite.pixelsPerUnit;
      float num2 = 100f;
      if ((bool) (UnityEngine.Object) this.canvas)
        num2 = this.canvas.referencePixelsPerUnit;
      return this.m_PixelsPerUnitMultiplier * num1 / num2;
    }
  }

  public Sprite overrideSprite
  {
    get => this.activeSprite;
    set
    {
      if (!SlicedFilledImage.SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
        return;
      this.SetAllDirty();
      this.TrackImage();
    }
  }

  private Sprite activeSprite
  {
    get
    {
      return !((UnityEngine.Object) this.m_OverrideSprite != (UnityEngine.Object) null) ? this.m_Sprite : this.m_OverrideSprite;
    }
  }

  public override Texture mainTexture
  {
    get
    {
      if ((UnityEngine.Object) this.activeSprite != (UnityEngine.Object) null)
        return (Texture) this.activeSprite.texture;
      return !((UnityEngine.Object) this.material != (UnityEngine.Object) null) || !((UnityEngine.Object) this.material.mainTexture != (UnityEngine.Object) null) ? (Texture) Graphic.s_WhiteTexture : this.material.mainTexture;
    }
  }

  public bool hasBorder
  {
    get
    {
      return (UnityEngine.Object) this.activeSprite != (UnityEngine.Object) null && (double) this.activeSprite.border.sqrMagnitude > 0.0;
    }
  }

  public override Material material
  {
    get
    {
      if ((UnityEngine.Object) this.m_Material != (UnityEngine.Object) null)
        return this.m_Material;
      return (bool) (UnityEngine.Object) this.activeSprite && (UnityEngine.Object) this.activeSprite.associatedAlphaSplitTexture != (UnityEngine.Object) null ? Image.defaultETC1GraphicMaterial : this.defaultMaterial;
    }
    set => base.material = value;
  }

  public float alphaHitTestMinimumThreshold { get; set; }

  protected SlicedFilledImage() => this.useLegacyMeshGeneration = false;

  protected override void OnEnable()
  {
    base.OnEnable();
    this.TrackImage();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!this.m_Tracked)
      return;
    this.UnTrackImage();
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    if ((UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null)
      base.OnPopulateMesh(vh);
    else
      this.GenerateSlicedFilledSprite(vh);
  }

  protected override void UpdateMaterial()
  {
    base.UpdateMaterial();
    if ((UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null)
    {
      this.canvasRenderer.SetAlphaTexture((Texture) null);
    }
    else
    {
      Texture2D alphaSplitTexture = this.activeSprite.associatedAlphaSplitTexture;
      if (!((UnityEngine.Object) alphaSplitTexture != (UnityEngine.Object) null))
        return;
      this.canvasRenderer.SetAlphaTexture((Texture) alphaSplitTexture);
    }
  }

  private void GenerateSlicedFilledSprite(VertexHelper vh)
  {
    vh.Clear();
    if ((double) this.m_FillAmount < 1.0 / 1000.0)
      return;
    Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
    Vector4 outerUv = DataUtility.GetOuterUV(this.activeSprite);
    Vector4 padding = DataUtility.GetPadding(this.activeSprite);
    if (!this.hasBorder)
    {
      Vector2 size = this.activeSprite.rect.size;
      int num1 = Mathf.RoundToInt(size.x);
      int num2 = Mathf.RoundToInt(size.y);
      Vector4 vertices = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * (padding.x / (float) num1), pixelAdjustedRect.y + pixelAdjustedRect.height * (padding.y / (float) num2), pixelAdjustedRect.x + pixelAdjustedRect.width * (((float) num1 - padding.z) / (float) num1), pixelAdjustedRect.y + pixelAdjustedRect.height * (((float) num2 - padding.w) / (float) num2));
      this.GenerateFilledSprite(vh, vertices, outerUv, this.m_FillAmount);
    }
    else
    {
      Vector4 innerUv = DataUtility.GetInnerUV(this.activeSprite);
      Vector4 adjustedBorders = this.GetAdjustedBorders(this.activeSprite.border / this.pixelsPerUnit, pixelAdjustedRect);
      Vector4 vector4 = padding / this.pixelsPerUnit;
      SlicedFilledImage.s_SlicedVertices[0] = new Vector2(vector4.x, vector4.y);
      SlicedFilledImage.s_SlicedVertices[3] = new Vector2(pixelAdjustedRect.width - vector4.z, pixelAdjustedRect.height - vector4.w);
      SlicedFilledImage.s_SlicedVertices[1].x = adjustedBorders.x;
      SlicedFilledImage.s_SlicedVertices[1].y = adjustedBorders.y;
      SlicedFilledImage.s_SlicedVertices[2].x = pixelAdjustedRect.width - adjustedBorders.z;
      SlicedFilledImage.s_SlicedVertices[2].y = pixelAdjustedRect.height - adjustedBorders.w;
      if (this.m_PreserveBorder)
      {
        switch (this.m_FillDirection)
        {
          case SlicedFilledImage.FillDirection.Right:
            SlicedFilledImage.s_SlicedVertices[0].x = vector4.x;
            SlicedFilledImage.s_SlicedVertices[3].x = pixelAdjustedRect.width * this.m_FillAmount - vector4.z;
            if ((double) SlicedFilledImage.s_SlicedVertices[3].x - (double) SlicedFilledImage.s_SlicedVertices[0].x < (double) adjustedBorders.x + (double) adjustedBorders.z)
            {
              SlicedFilledImage.s_SlicedVertices[1].x = adjustedBorders.x;
              SlicedFilledImage.s_SlicedVertices[2].x = adjustedBorders.x;
              break;
            }
            SlicedFilledImage.s_SlicedVertices[1].x = adjustedBorders.x;
            SlicedFilledImage.s_SlicedVertices[2].x = pixelAdjustedRect.width * this.m_FillAmount - adjustedBorders.z;
            break;
          case SlicedFilledImage.FillDirection.Left:
            SlicedFilledImage.s_SlicedVertices[0].x = pixelAdjustedRect.width * (1f - this.m_FillAmount) + vector4.x;
            SlicedFilledImage.s_SlicedVertices[3].x = pixelAdjustedRect.width - vector4.z;
            if ((double) SlicedFilledImage.s_SlicedVertices[3].x - (double) SlicedFilledImage.s_SlicedVertices[0].x < (double) adjustedBorders.x + (double) adjustedBorders.z)
            {
              SlicedFilledImage.s_SlicedVertices[1].x = pixelAdjustedRect.width - adjustedBorders.z;
              SlicedFilledImage.s_SlicedVertices[2].x = pixelAdjustedRect.width - adjustedBorders.z;
              break;
            }
            SlicedFilledImage.s_SlicedVertices[1].x = pixelAdjustedRect.width * (1f - this.m_FillAmount) + adjustedBorders.x;
            SlicedFilledImage.s_SlicedVertices[2].x = pixelAdjustedRect.width - adjustedBorders.z;
            break;
          case SlicedFilledImage.FillDirection.Up:
            SlicedFilledImage.s_SlicedVertices[0].y = vector4.y;
            SlicedFilledImage.s_SlicedVertices[3].y = pixelAdjustedRect.height * this.m_FillAmount - vector4.w;
            if ((double) SlicedFilledImage.s_SlicedVertices[3].y - (double) SlicedFilledImage.s_SlicedVertices[0].y < (double) adjustedBorders.y + (double) adjustedBorders.w)
            {
              SlicedFilledImage.s_SlicedVertices[1].y = adjustedBorders.y;
              SlicedFilledImage.s_SlicedVertices[2].y = adjustedBorders.y;
              break;
            }
            SlicedFilledImage.s_SlicedVertices[1].y = adjustedBorders.y;
            SlicedFilledImage.s_SlicedVertices[2].y = pixelAdjustedRect.height * this.m_FillAmount - adjustedBorders.w;
            break;
          case SlicedFilledImage.FillDirection.Down:
            SlicedFilledImage.s_SlicedVertices[0].y = pixelAdjustedRect.height * (1f - this.m_FillAmount) + vector4.y;
            SlicedFilledImage.s_SlicedVertices[3].y = pixelAdjustedRect.height - vector4.w;
            if ((double) SlicedFilledImage.s_SlicedVertices[3].y - (double) SlicedFilledImage.s_SlicedVertices[0].y < (double) adjustedBorders.y + (double) adjustedBorders.w)
            {
              SlicedFilledImage.s_SlicedVertices[1].y = pixelAdjustedRect.height - adjustedBorders.w;
              SlicedFilledImage.s_SlicedVertices[2].y = pixelAdjustedRect.height - adjustedBorders.w;
              break;
            }
            SlicedFilledImage.s_SlicedVertices[1].y = pixelAdjustedRect.height * (1f - this.m_FillAmount) + adjustedBorders.y;
            SlicedFilledImage.s_SlicedVertices[2].y = pixelAdjustedRect.height - adjustedBorders.w;
            break;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        SlicedFilledImage.s_SlicedVertices[index].x += pixelAdjustedRect.x;
        SlicedFilledImage.s_SlicedVertices[index].y += pixelAdjustedRect.y;
      }
      SlicedFilledImage.s_SlicedUVs[0] = new Vector2(outerUv.x, outerUv.y);
      SlicedFilledImage.s_SlicedUVs[1] = new Vector2(innerUv.x, innerUv.y);
      SlicedFilledImage.s_SlicedUVs[2] = new Vector2(innerUv.z, innerUv.w);
      SlicedFilledImage.s_SlicedUVs[3] = new Vector2(outerUv.z, outerUv.w);
      float num3;
      float num4;
      if (this.m_FillDirection == SlicedFilledImage.FillDirection.Left || this.m_FillDirection == SlicedFilledImage.FillDirection.Right)
      {
        num3 = pixelAdjustedRect.x + vector4.x;
        float num5 = pixelAdjustedRect.width - vector4.x - vector4.z;
        num4 = (double) num5 > 0.0 ? 1f / num5 : 1f;
      }
      else
      {
        num3 = pixelAdjustedRect.y + vector4.y;
        float num6 = pixelAdjustedRect.height - vector4.y - vector4.w;
        num4 = (double) num6 > 0.0 ? 1f / num6 : 1f;
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = index1 + 1;
        for (int index3 = 0; index3 < 3; ++index3)
        {
          if (this.m_FillCenter || index1 != 1 || index3 != 1)
          {
            int index4 = index3 + 1;
            float num7;
            float num8;
            switch (this.m_FillDirection)
            {
              case SlicedFilledImage.FillDirection.Right:
                num7 = (SlicedFilledImage.s_SlicedVertices[index1].x - num3) * num4;
                num8 = (SlicedFilledImage.s_SlicedVertices[index2].x - num3) * num4;
                break;
              case SlicedFilledImage.FillDirection.Left:
                num7 = (float) (1.0 - ((double) SlicedFilledImage.s_SlicedVertices[index2].x - (double) num3) * (double) num4);
                num8 = (float) (1.0 - ((double) SlicedFilledImage.s_SlicedVertices[index1].x - (double) num3) * (double) num4);
                break;
              case SlicedFilledImage.FillDirection.Up:
                num7 = (SlicedFilledImage.s_SlicedVertices[index3].y - num3) * num4;
                num8 = (SlicedFilledImage.s_SlicedVertices[index4].y - num3) * num4;
                break;
              case SlicedFilledImage.FillDirection.Down:
                num7 = (float) (1.0 - ((double) SlicedFilledImage.s_SlicedVertices[index4].y - (double) num3) * (double) num4);
                num8 = (float) (1.0 - ((double) SlicedFilledImage.s_SlicedVertices[index3].y - (double) num3) * (double) num4);
                break;
              default:
                num7 = num8 = 0.0f;
                break;
            }
            if ((double) num7 < (double) this.m_FillAmount)
            {
              Vector4 vertices = new Vector4(SlicedFilledImage.s_SlicedVertices[index1].x, SlicedFilledImage.s_SlicedVertices[index3].y, SlicedFilledImage.s_SlicedVertices[index2].x, SlicedFilledImage.s_SlicedVertices[index4].y);
              Vector4 uvs = new Vector4(SlicedFilledImage.s_SlicedUVs[index1].x, SlicedFilledImage.s_SlicedUVs[index3].y, SlicedFilledImage.s_SlicedUVs[index2].x, SlicedFilledImage.s_SlicedUVs[index4].y);
              float fillAmount = (float) (((double) this.m_FillAmount - (double) num7) / ((double) num8 - (double) num7));
              this.GenerateFilledSprite(vh, vertices, uvs, fillAmount);
            }
          }
        }
      }
    }
  }

  private Vector4 GetAdjustedBorders(Vector4 border, Rect adjustedRect)
  {
    Rect rect = this.rectTransform.rect;
    for (int index = 0; index <= 1; ++index)
    {
      Vector2 size = rect.size;
      if ((double) size[index] != 0.0)
      {
        size = adjustedRect.size;
        double num1 = (double) size[index];
        size = rect.size;
        double num2 = (double) size[index];
        float num3 = (float) (num1 / num2);
        border[index] *= num3;
        border[index + 2] *= num3;
      }
      float num4 = border[index] + border[index + 2];
      size = adjustedRect.size;
      if ((double) size[index] < (double) num4 && (double) num4 != 0.0)
      {
        size = adjustedRect.size;
        float num5 = size[index] / num4;
        border[index] *= num5;
        border[index + 2] *= num5;
      }
    }
    return border;
  }

  private void GenerateFilledSprite(
    VertexHelper vh,
    Vector4 vertices,
    Vector4 uvs,
    float fillAmount)
  {
    if ((double) this.m_FillAmount < 1.0 / 1000.0)
      return;
    float x1 = uvs.x;
    float y1 = uvs.y;
    float x2 = uvs.z;
    float y2 = uvs.w;
    if ((double) fillAmount < 1.0)
    {
      if (this.m_FillDirection == SlicedFilledImage.FillDirection.Left || this.m_FillDirection == SlicedFilledImage.FillDirection.Right)
      {
        if (this.m_FillDirection == SlicedFilledImage.FillDirection.Left)
        {
          vertices.x = vertices.z - (vertices.z - vertices.x) * fillAmount;
          x1 = x2 - (x2 - x1) * fillAmount;
        }
        else
        {
          vertices.z = vertices.x + (vertices.z - vertices.x) * fillAmount;
          x2 = x1 + (x2 - x1) * fillAmount;
        }
      }
      else if (this.m_FillDirection == SlicedFilledImage.FillDirection.Down)
      {
        vertices.y = vertices.w - (vertices.w - vertices.y) * fillAmount;
        y1 = y2 - (y2 - y1) * fillAmount;
      }
      else
      {
        vertices.w = vertices.y + (vertices.w - vertices.y) * fillAmount;
        y2 = y1 + (y2 - y1) * fillAmount;
      }
    }
    SlicedFilledImage.s_Vertices[0] = new Vector3(vertices.x, vertices.y);
    SlicedFilledImage.s_Vertices[1] = new Vector3(vertices.x, vertices.w);
    SlicedFilledImage.s_Vertices[2] = new Vector3(vertices.z, vertices.w);
    SlicedFilledImage.s_Vertices[3] = new Vector3(vertices.z, vertices.y);
    SlicedFilledImage.s_UVs[0] = new Vector2(x1, y1);
    SlicedFilledImage.s_UVs[1] = new Vector2(x1, y2);
    SlicedFilledImage.s_UVs[2] = new Vector2(x2, y2);
    SlicedFilledImage.s_UVs[3] = new Vector2(x2, y1);
    int currentVertCount = vh.currentVertCount;
    for (int index = 0; index < 4; ++index)
      vh.AddVert(SlicedFilledImage.s_Vertices[index], (Color32) this.color, SlicedFilledImage.s_UVs[index]);
    vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
    vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
  }

  int ILayoutElement.layoutPriority => 0;

  float ILayoutElement.minWidth => 0.0f;

  float ILayoutElement.minHeight => 0.0f;

  float ILayoutElement.flexibleWidth => -1f;

  float ILayoutElement.flexibleHeight => -1f;

  float ILayoutElement.preferredWidth
  {
    get
    {
      return (UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null ? 0.0f : DataUtility.GetMinSize(this.activeSprite).x / this.pixelsPerUnit;
    }
  }

  float ILayoutElement.preferredHeight
  {
    get
    {
      return (UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null ? 0.0f : DataUtility.GetMinSize(this.activeSprite).y / this.pixelsPerUnit;
    }
  }

  void ILayoutElement.CalculateLayoutInputHorizontal()
  {
  }

  void ILayoutElement.CalculateLayoutInputVertical()
  {
  }

  bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
  {
    if ((double) this.alphaHitTestMinimumThreshold <= 0.0)
      return true;
    if ((double) this.alphaHitTestMinimumThreshold > 1.0)
      return false;
    if ((UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null)
      return true;
    Vector2 localPoint;
    if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, screenPoint, eventCamera, out localPoint))
      return false;
    Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
    localPoint.x += this.rectTransform.pivot.x * pixelAdjustedRect.width;
    localPoint.y += this.rectTransform.pivot.y * pixelAdjustedRect.height;
    Rect rect = this.activeSprite.rect;
    Vector4 border = this.activeSprite.border;
    Vector4 adjustedBorders = this.GetAdjustedBorders(border / this.pixelsPerUnit, pixelAdjustedRect);
    for (int index1 = 0; index1 < 2; ++index1)
    {
      if ((double) localPoint[index1] > (double) adjustedBorders[index1])
      {
        Vector2 size = pixelAdjustedRect.size;
        if ((double) size[index1] - (double) localPoint[index1] <= (double) adjustedBorders[index1 + 2])
        {
          ref Vector2 local1 = ref localPoint;
          int index2 = index1;
          ref Vector2 local2 = ref local1;
          int index3 = index2;
          double num1 = (double) local1[index2];
          size = pixelAdjustedRect.size;
          double num2 = (double) size[index1];
          size = rect.size;
          double num3 = (double) size[index1];
          double num4 = num2 - num3;
          double num5 = num1 - num4;
          local2[index3] = (float) num5;
        }
        else
        {
          double a1 = (double) adjustedBorders[index1];
          size = pixelAdjustedRect.size;
          double b1 = (double) size[index1] - (double) adjustedBorders[index1 + 2];
          double num6 = (double) localPoint[index1];
          float num7 = Mathf.InverseLerp((float) a1, (float) b1, (float) num6);
          ref Vector2 local = ref localPoint;
          int index4 = index1;
          double a2 = (double) border[index1];
          size = rect.size;
          double b2 = (double) size[index1] - (double) border[index1 + 2];
          double t = (double) num7;
          double num8 = (double) Mathf.Lerp((float) a2, (float) b2, (float) t);
          local[index4] = (float) num8;
        }
      }
    }
    Rect textureRect = this.activeSprite.textureRect;
    Vector2 vector2 = new Vector2(localPoint.x / textureRect.width, localPoint.y / textureRect.height);
    float u = Mathf.Lerp(textureRect.x, textureRect.xMax, vector2.x) / (float) this.activeSprite.texture.width;
    float v = Mathf.Lerp(textureRect.y, textureRect.yMax, vector2.y) / (float) this.activeSprite.texture.height;
    switch (this.m_FillDirection)
    {
      case SlicedFilledImage.FillDirection.Right:
        if ((double) u > (double) this.m_FillAmount)
          return false;
        break;
      case SlicedFilledImage.FillDirection.Left:
        if (1.0 - (double) u > (double) this.m_FillAmount)
          return false;
        break;
      case SlicedFilledImage.FillDirection.Up:
        if ((double) v > (double) this.m_FillAmount)
          return false;
        break;
      case SlicedFilledImage.FillDirection.Down:
        if (1.0 - (double) v > (double) this.m_FillAmount)
          return false;
        break;
    }
    try
    {
      return (double) this.activeSprite.texture.GetPixelBilinear(u, v).a >= (double) this.alphaHitTestMinimumThreshold;
    }
    catch (UnityException ex)
    {
      Debug.LogError((object) ("Using alphaHitTestMinimumThreshold greater than 0 on Image whose sprite texture cannot be read. " + ((Exception) ex).Message + " Also make sure to disable sprite packing for this sprite."), (UnityEngine.Object) this);
      return true;
    }
  }

  void ISerializationCallbackReceiver.OnBeforeSerialize()
  {
  }

  void ISerializationCallbackReceiver.OnAfterDeserialize()
  {
    this.m_FillAmount = Mathf.Clamp01(this.m_FillAmount);
  }

  private void TrackImage()
  {
    if (!((UnityEngine.Object) this.activeSprite != (UnityEngine.Object) null) || !((UnityEngine.Object) this.activeSprite.texture == (UnityEngine.Object) null))
      return;
    if (!SlicedFilledImage.s_Initialized)
    {
      SpriteAtlasManager.atlasRegistered += new Action<SpriteAtlas>(SlicedFilledImage.RebuildImage);
      SlicedFilledImage.s_Initialized = true;
    }
    SlicedFilledImage.m_TrackedTexturelessImages.Add(this);
    this.m_Tracked = true;
  }

  private void UnTrackImage()
  {
    SlicedFilledImage.m_TrackedTexturelessImages.Remove(this);
    this.m_Tracked = false;
  }

  private static void RebuildImage(SpriteAtlas spriteAtlas)
  {
    for (int index = SlicedFilledImage.m_TrackedTexturelessImages.Count - 1; index >= 0; --index)
    {
      SlicedFilledImage texturelessImage = SlicedFilledImage.m_TrackedTexturelessImages[index];
      if (spriteAtlas.CanBindTo(texturelessImage.activeSprite))
      {
        texturelessImage.SetAllDirty();
        SlicedFilledImage.m_TrackedTexturelessImages.RemoveAt(index);
      }
    }
  }

  private static class SetPropertyUtility
  {
    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
      if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
    {
      if ((object) currentValue == null && (object) newValue == null || (object) currentValue != null && currentValue.Equals((object) newValue))
        return false;
      currentValue = newValue;
      return true;
    }
  }

  public enum FillDirection
  {
    Right,
    Left,
    Up,
    Down,
  }
}
