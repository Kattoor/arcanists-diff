// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.Extensions.UIPrimitiveBase
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace UnityEngine.UI.Extensions
{
  public class UIPrimitiveBase : MaskableGraphic, ILayoutElement, ICanvasRaycastFilter
  {
    protected static Material s_ETC1DefaultUI;
    [SerializeField]
    private Sprite m_Sprite;
    [NonSerialized]
    private Sprite m_OverrideSprite;
    internal float m_EventAlphaThreshold = 1f;
    [SerializeField]
    private ResolutionMode m_improveResolution;
    [SerializeField]
    protected float m_Resolution;
    [SerializeField]
    private bool m_useNativeSize;

    public Sprite sprite
    {
      get => this.m_Sprite;
      set
      {
        if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
          this.GeneratedUVs();
        this.SetAllDirty();
      }
    }

    public Sprite overrideSprite
    {
      get => this.activeSprite;
      set
      {
        if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
          this.GeneratedUVs();
        this.SetAllDirty();
      }
    }

    protected Sprite activeSprite
    {
      get
      {
        return !((UnityEngine.Object) this.m_OverrideSprite != (UnityEngine.Object) null) ? this.sprite : this.m_OverrideSprite;
      }
    }

    public float eventAlphaThreshold
    {
      get => this.m_EventAlphaThreshold;
      set => this.m_EventAlphaThreshold = value;
    }

    public ResolutionMode ImproveResolution
    {
      get => this.m_improveResolution;
      set
      {
        this.m_improveResolution = value;
        this.SetAllDirty();
      }
    }

    public float Resoloution
    {
      get => this.m_Resolution;
      set
      {
        this.m_Resolution = value;
        this.SetAllDirty();
      }
    }

    public bool UseNativeSize
    {
      get => this.m_useNativeSize;
      set
      {
        this.m_useNativeSize = value;
        this.SetAllDirty();
      }
    }

    protected UIPrimitiveBase() => this.useLegacyMeshGeneration = false;

    public static Material defaultETC1GraphicMaterial
    {
      get
      {
        if ((UnityEngine.Object) UIPrimitiveBase.s_ETC1DefaultUI == (UnityEngine.Object) null)
          UIPrimitiveBase.s_ETC1DefaultUI = Canvas.GetETC1SupportedCanvasMaterial();
        return UIPrimitiveBase.s_ETC1DefaultUI;
      }
    }

    public override Texture mainTexture
    {
      get
      {
        if (!((UnityEngine.Object) this.activeSprite == (UnityEngine.Object) null))
          return (Texture) this.activeSprite.texture;
        return (UnityEngine.Object) this.material != (UnityEngine.Object) null && (UnityEngine.Object) this.material.mainTexture != (UnityEngine.Object) null ? this.material.mainTexture : (Texture) Graphic.s_WhiteTexture;
      }
    }

    public bool hasBorder
    {
      get
      {
        return (UnityEngine.Object) this.activeSprite != (UnityEngine.Object) null && (double) this.activeSprite.border.sqrMagnitude > 0.0;
      }
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
        return num1 / num2;
      }
    }

    public override Material material
    {
      get
      {
        if ((UnityEngine.Object) this.m_Material != (UnityEngine.Object) null)
          return this.m_Material;
        return (bool) (UnityEngine.Object) this.activeSprite && (UnityEngine.Object) this.activeSprite.associatedAlphaSplitTexture != (UnityEngine.Object) null ? UIPrimitiveBase.defaultETC1GraphicMaterial : this.defaultMaterial;
      }
      set => base.material = value;
    }

    protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
    {
      UIVertex[] uiVertexArray = new UIVertex[4];
      for (int index = 0; index < vertices.Length; ++index)
      {
        UIVertex simpleVert = UIVertex.simpleVert with
        {
          color = (Color32) this.color,
          position = (Vector3) vertices[index],
          uv0 = uvs[index]
        };
        uiVertexArray[index] = simpleVert;
      }
      return uiVertexArray;
    }

    protected Vector2[] IncreaseResolution(Vector2[] input)
    {
      List<Vector2> vector2List = new List<Vector2>();
      switch (this.ImproveResolution)
      {
        case ResolutionMode.PerSegment:
          for (int index = 0; index < input.Length - 1; ++index)
          {
            Vector2 a = input[index];
            vector2List.Add(a);
            Vector2 b = input[index + 1];
            this.ResolutionToNativeSize(Vector2.Distance(a, b));
            float num1 = 1f / this.m_Resolution;
            for (float num2 = 1f; (double) num2 < (double) this.m_Resolution; ++num2)
              vector2List.Add(Vector2.Lerp(a, b, num1 * num2));
            vector2List.Add(b);
          }
          break;
        case ResolutionMode.PerLine:
          float distance = 0.0f;
          for (int index = 0; index < input.Length - 1; ++index)
            distance += Vector2.Distance(input[index], input[index + 1]);
          this.ResolutionToNativeSize(distance);
          float num3 = distance / this.m_Resolution;
          int num4 = 0;
          for (int index1 = 0; index1 < input.Length - 1; ++index1)
          {
            Vector2 a = input[index1];
            vector2List.Add(a);
            Vector2 b = input[index1 + 1];
            float num5 = Vector2.Distance(a, b) / num3;
            float num6 = 1f / num5;
            for (int index2 = 0; (double) index2 < (double) num5; ++index2)
            {
              vector2List.Add(Vector2.Lerp(a, b, (float) index2 * num6));
              ++num4;
            }
            vector2List.Add(b);
          }
          break;
      }
      return vector2List.ToArray();
    }

    protected virtual void GeneratedUVs()
    {
    }

    protected virtual void ResolutionToNativeSize(float distance)
    {
    }

    public virtual void CalculateLayoutInputHorizontal()
    {
    }

    public virtual void CalculateLayoutInputVertical()
    {
    }

    public virtual float minWidth => 0.0f;

    public virtual float preferredWidth
    {
      get
      {
        return (UnityEngine.Object) this.overrideSprite == (UnityEngine.Object) null ? 0.0f : this.overrideSprite.rect.size.x / this.pixelsPerUnit;
      }
    }

    public virtual float flexibleWidth => -1f;

    public virtual float minHeight => 0.0f;

    public virtual float preferredHeight
    {
      get
      {
        return (UnityEngine.Object) this.overrideSprite == (UnityEngine.Object) null ? 0.0f : this.overrideSprite.rect.size.y / this.pixelsPerUnit;
      }
    }

    public virtual float flexibleHeight => -1f;

    public virtual int layoutPriority => 0;

    public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
      if ((double) this.m_EventAlphaThreshold >= 1.0)
        return true;
      Sprite overrideSprite = this.overrideSprite;
      if ((UnityEngine.Object) overrideSprite == (UnityEngine.Object) null)
        return true;
      Vector2 localPoint;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, screenPoint, eventCamera, out localPoint);
      Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
      localPoint.x += this.rectTransform.pivot.x * pixelAdjustedRect.width;
      localPoint.y += this.rectTransform.pivot.y * pixelAdjustedRect.height;
      Vector2 vector2_1 = this.MapCoordinate(localPoint, pixelAdjustedRect);
      Rect textureRect = overrideSprite.textureRect;
      Vector2 vector2_2 = new Vector2(vector2_1.x / textureRect.width, vector2_1.y / textureRect.height);
      float u = Mathf.Lerp(textureRect.x, textureRect.xMax, vector2_2.x) / (float) overrideSprite.texture.width;
      float v = Mathf.Lerp(textureRect.y, textureRect.yMax, vector2_2.y) / (float) overrideSprite.texture.height;
      try
      {
        return (double) overrideSprite.texture.GetPixelBilinear(u, v).a >= (double) this.m_EventAlphaThreshold;
      }
      catch (UnityException ex)
      {
        Debug.LogError((object) ("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ((Exception) ex).Message + " Also make sure to disable sprite packing for this sprite."), (UnityEngine.Object) this);
        return true;
      }
    }

    private Vector2 MapCoordinate(Vector2 local, Rect rect)
    {
      Rect rect1 = this.sprite.rect;
      return new Vector2(local.x * rect.width, local.y * rect.height);
    }

    private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
    {
      for (int index = 0; index <= 1; ++index)
      {
        float num1 = border[index] + border[index + 2];
        if ((double) rect.size[index] < (double) num1 && (double) num1 != 0.0)
        {
          float num2 = rect.size[index] / num1;
          border[index] *= num2;
          border[index + 2] *= num2;
        }
      }
      return border;
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.SetAllDirty();
    }
  }
}
