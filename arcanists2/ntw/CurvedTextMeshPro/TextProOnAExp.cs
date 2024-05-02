

using TMPro;
using UnityEngine;

#nullable disable
namespace ntw.CurvedTextMeshPro
{
  [ExecuteInEditMode]
  public class TextProOnAExp : TextProOnACurve
  {
    [SerializeField]
    [Tooltip("The base of the exponential curve")]
    private float m_expBase = 1.3f;
    private float m_oldExpBase = float.MaxValue;

    protected override bool ParametersHaveChanged()
    {
      int num = (double) this.m_expBase != (double) this.m_oldExpBase ? 1 : 0;
      this.m_oldExpBase = this.m_expBase;
      return num != 0;
    }

    protected override Matrix4x4 ComputeTransformationMatrix(
      Vector3 charMidBaselinePos,
      float zeroToOnePos,
      TMP_TextInfo textInfo,
      int charIdx)
    {
      float x = charMidBaselinePos.x;
      float num = Mathf.Pow(this.m_expBase, x);
      Vector2 vector2 = new Vector2(x, num - textInfo.lineInfo[0].lineExtents.max.y * (float) textInfo.characterInfo[charIdx].lineNumber);
      return Matrix4x4.TRS(new Vector3(vector2.x, vector2.y, 0.0f), Quaternion.AngleAxis(Mathf.Atan(Mathf.Log(this.m_expBase) * num) * 57.29578f, Vector3.forward), Vector3.one);
    }
  }
}
