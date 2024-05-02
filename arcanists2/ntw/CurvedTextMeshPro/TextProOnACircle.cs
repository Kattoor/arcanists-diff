// Decompiled with JetBrains decompiler
// Type: ntw.CurvedTextMeshPro.TextProOnACircle
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

#nullable disable
namespace ntw.CurvedTextMeshPro
{
  [ExecuteInEditMode]
  public class TextProOnACircle : TextProOnACurve
  {
    [SerializeField]
    [Tooltip("The radius of the text circle arc")]
    private float m_radius = 10f;
    [SerializeField]
    [Tooltip("How much degrees the text arc should span")]
    private float m_arcDegrees = 90f;
    [SerializeField]
    [Tooltip("The angular offset at which the arc should be centered, in degrees")]
    private float m_angularOffset = -90f;
    [SerializeField]
    [Tooltip("The maximum angular distance between letters, in degrees")]
    private int m_maxDegreesPerLetter = 360;
    private float m_oldRadius = float.MaxValue;
    private float m_oldArcDegrees = float.MaxValue;
    private float m_oldAngularOffset = float.MaxValue;
    private float m_oldMaxDegreesPerLetter = float.MaxValue;

    protected override bool ParametersHaveChanged()
    {
      int num = (double) this.m_radius != (double) this.m_oldRadius || (double) this.m_arcDegrees != (double) this.m_oldArcDegrees || (double) this.m_angularOffset != (double) this.m_oldAngularOffset ? 1 : ((double) this.m_oldMaxDegreesPerLetter != (double) this.m_maxDegreesPerLetter ? 1 : 0);
      this.m_oldRadius = this.m_radius;
      this.m_oldArcDegrees = this.m_arcDegrees;
      this.m_oldAngularOffset = this.m_angularOffset;
      this.m_oldMaxDegreesPerLetter = (float) this.m_maxDegreesPerLetter;
      return num != 0;
    }

    protected override Matrix4x4 ComputeTransformationMatrix(
      Vector3 charMidBaselinePos,
      float zeroToOnePos,
      TMP_TextInfo textInfo,
      int charIdx)
    {
      float num1 = Mathf.Min(this.m_arcDegrees, (float) (textInfo.characterCount / textInfo.lineCount * this.m_maxDegreesPerLetter));
      double f = (((double) zeroToOnePos - 0.5) * (double) num1 + (double) this.m_angularOffset) * (Math.PI / 180.0);
      float x = Mathf.Cos((float) f);
      float y = Mathf.Sin((float) f);
      float num2 = this.m_radius - textInfo.lineInfo[0].lineExtents.max.y * (float) textInfo.characterInfo[charIdx].lineNumber;
      Vector2 vector2 = new Vector2(x * num2, -y * num2);
      return Matrix4x4.TRS(new Vector3(vector2.x, vector2.y, 0.0f), Quaternion.AngleAxis((float) (-(double) Mathf.Atan2(y, x) * 57.295780181884766 - 90.0), Vector3.forward), Vector3.one);
    }
  }
}
