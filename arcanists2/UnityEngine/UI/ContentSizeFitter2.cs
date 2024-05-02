// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.ContentSizeFitter2
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

#nullable disable
namespace UnityEngine.UI
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (RectTransform))]
  public class ContentSizeFitter2 : UIBehaviour, ILayoutSelfController, ILayoutController
  {
    [SerializeField]
    protected ContentSizeFitter.FitMode m_HorizontalFit;
    [SerializeField]
    protected ContentSizeFitter.FitMode m_VerticalFit;
    [NonSerialized]
    private RectTransform m_Rect;
    private DrivenRectTransformTracker m_Tracker;

    public ContentSizeFitter.FitMode horizontalFit
    {
      get => this.m_HorizontalFit;
      set
      {
        if (!ContentSizeFitter2.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
          return;
        this.SetDirty();
      }
    }

    public ContentSizeFitter.FitMode verticalFit
    {
      get => this.m_VerticalFit;
      set
      {
        if (!ContentSizeFitter2.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
          return;
        this.SetDirty();
      }
    }

    public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
    {
      if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
        return false;
      currentValue = newValue;
      return true;
    }

    private RectTransform rectTransform
    {
      get
      {
        if ((UnityEngine.Object) this.m_Rect == (UnityEngine.Object) null)
          this.m_Rect = this.GetComponent<RectTransform>();
        return this.m_Rect;
      }
    }

    protected ContentSizeFitter2()
    {
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.SetDirty();
    }

    protected override void OnDisable()
    {
      this.m_Tracker.Clear();
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
      base.OnDisable();
    }

    protected override void OnRectTransformDimensionsChange() => this.SetDirty();

    private void HandleSelfFittingAlongAxis(int axis)
    {
      ContentSizeFitter.FitMode fitMode = axis != 0 ? this.verticalFit : this.horizontalFit;
      if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
      {
        this.m_Tracker.Add((UnityEngine.Object) this, this.rectTransform, DrivenTransformProperties.None);
      }
      else
      {
        float num1 = 1f;
        float num2 = 1f;
        if (this.m_Rect.childCount > 0)
        {
          num1 = this.m_Rect.GetChild(0).localScale.x;
          num2 = this.m_Rect.GetChild(0).localScale.y;
        }
        this.m_Tracker.Add((UnityEngine.Object) this, this.rectTransform, axis != 0 ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
        if (fitMode == ContentSizeFitter.FitMode.MinSize)
          this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis, LayoutUtility.GetMinSize(this.m_Rect, axis) * (axis == 0 ? num1 : num2));
        else
          this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis) * (axis == 0 ? num1 : num2));
      }
    }

    public virtual void SetLayoutHorizontal()
    {
      this.m_Tracker.Clear();
      this.HandleSelfFittingAlongAxis(0);
    }

    public virtual void SetLayoutVertical() => this.HandleSelfFittingAlongAxis(1);

    protected void SetDirty()
    {
      if (!this.IsActive())
        return;
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
    }

    public void Rebuild()
    {
      base.OnEnable();
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
    }

    public enum FitMode
    {
      Unconstrained,
      MinSize,
      PreferredSize,
    }
  }
}
