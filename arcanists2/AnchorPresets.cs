

using UnityEngine;

#nullable disable
public static class AnchorPresets
{
  private static AnchorPresets.Presets topLeft = new AnchorPresets.Presets(new Vector2(0.0f, 1f));
  private static AnchorPresets.Presets topCenter = new AnchorPresets.Presets(new Vector2(0.5f, 1f));
  private static AnchorPresets.Presets topRight = new AnchorPresets.Presets(new Vector2(1f, 1f));
  private static AnchorPresets.Presets middleLeft = new AnchorPresets.Presets(new Vector2(0.0f, 0.5f));
  private static AnchorPresets.Presets middleCenter = new AnchorPresets.Presets(new Vector2(0.5f, 0.5f));
  private static AnchorPresets.Presets middleRight = new AnchorPresets.Presets(new Vector2(1f, 0.5f));
  private static AnchorPresets.Presets bottomLeft = new AnchorPresets.Presets(new Vector2(0.0f, 0.0f));
  private static AnchorPresets.Presets bottomCenter = new AnchorPresets.Presets(new Vector2(0.5f, 0.0f));
  private static AnchorPresets.Presets bottomRight = new AnchorPresets.Presets(new Vector2(1f, 0.0f));
  private static AnchorPresets.Presets stretchLeft = new AnchorPresets.Presets(new Vector2(0.0f, 0.0f), new Vector2(0.0f, 1f));
  private static AnchorPresets.Presets stretchCenter = new AnchorPresets.Presets(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1f));
  private static AnchorPresets.Presets stretchRight = new AnchorPresets.Presets(new Vector2(1f, 0.0f), new Vector2(1f, 1f));
  private static AnchorPresets.Presets topStretch = new AnchorPresets.Presets(new Vector2(0.0f, 1f), new Vector2(1f, 1f));
  private static AnchorPresets.Presets middleStretch = new AnchorPresets.Presets(new Vector2(0.0f, 0.5f), new Vector2(1f, 0.5f));
  private static AnchorPresets.Presets bottomStretch = new AnchorPresets.Presets(new Vector2(0.0f, 0.0f), new Vector2(1f, 0.0f));
  private static AnchorPresets.Presets stretchStretch = new AnchorPresets.Presets(new Vector2(0.0f, 0.0f), new Vector2(1f, 1f));

  public static void SetAnchor(this RectTransform t, Anchor a)
  {
    AnchorPresets.Presets preset = AnchorPresets.getPreset(a);
    t.anchorMin = preset.min;
    t.anchorMax = preset.max;
  }

  public static AnchorPresets.Presets getPreset(Anchor a)
  {
    switch (a)
    {
      case Anchor.TopLeft:
        return AnchorPresets.topLeft;
      case Anchor.TopCenter:
        return AnchorPresets.topCenter;
      case Anchor.TopRight:
        return AnchorPresets.topRight;
      case Anchor.MiddleLeft:
        return AnchorPresets.middleLeft;
      case Anchor.MiddleCenter:
        return AnchorPresets.middleCenter;
      case Anchor.MiddleRight:
        return AnchorPresets.middleRight;
      case Anchor.BottomLeft:
        return AnchorPresets.bottomLeft;
      case Anchor.BottomCenter:
        return AnchorPresets.bottomCenter;
      case Anchor.BottomRight:
        return AnchorPresets.bottomRight;
      case Anchor.StretchLeft:
        return AnchorPresets.stretchLeft;
      case Anchor.StretchCenter:
        return AnchorPresets.stretchCenter;
      case Anchor.StretchRight:
        return AnchorPresets.stretchRight;
      case Anchor.TopStretch:
        return AnchorPresets.topStretch;
      case Anchor.MiddleStretch:
        return AnchorPresets.middleStretch;
      case Anchor.BottomStretch:
        return AnchorPresets.bottomStretch;
      case Anchor.StretchStretch:
        return AnchorPresets.stretchStretch;
      default:
        return (AnchorPresets.Presets) null;
    }
  }

  public class Presets
  {
    public Vector2 min;
    public Vector2 max;

    public Presets(Vector2 t, Vector2 b)
    {
      this.min = t;
      this.max = b;
    }

    public Presets(Vector2 t)
    {
      this.min = t;
      this.max = t;
    }
  }
}
