

using System;

#nullable disable
namespace UnityEngine.UI.Extensions
{
  [Serializable]
  public class CableCurve
  {
    [SerializeField]
    private Vector2 m_start;
    [SerializeField]
    private Vector2 m_end;
    [SerializeField]
    private float m_slack;
    [SerializeField]
    private int m_steps;
    [SerializeField]
    private bool m_regen;
    private static Vector2[] emptyCurve = new Vector2[2]
    {
      new Vector2(0.0f, 0.0f),
      new Vector2(0.0f, 0.0f)
    };
    [SerializeField]
    private Vector2[] points;

    public bool regenPoints
    {
      get => this.m_regen;
      set => this.m_regen = value;
    }

    public Vector2 start
    {
      get => this.m_start;
      set
      {
        if (value != this.m_start)
          this.m_regen = true;
        this.m_start = value;
      }
    }

    public Vector2 end
    {
      get => this.m_end;
      set
      {
        if (value != this.m_end)
          this.m_regen = true;
        this.m_end = value;
      }
    }

    public float slack
    {
      get => this.m_slack;
      set
      {
        if ((double) value != (double) this.m_slack)
          this.m_regen = true;
        this.m_slack = Mathf.Max(0.0f, value);
      }
    }

    public int steps
    {
      get => this.m_steps;
      set
      {
        if (value != this.m_steps)
          this.m_regen = true;
        this.m_steps = Mathf.Max(2, value);
      }
    }

    public Vector2 midPoint
    {
      get
      {
        Vector2 midPoint = Vector2.zero;
        if (this.m_steps == 2)
          return (this.points[0] + this.points[1]) * 0.5f;
        if (this.m_steps > 2)
        {
          int index = this.m_steps / 2;
          midPoint = this.m_steps % 2 != 0 ? this.points[index] : (this.points[index] + this.points[index + 1]) * 0.5f;
        }
        return midPoint;
      }
    }

    public CableCurve()
    {
      this.points = CableCurve.emptyCurve;
      this.m_start = Vector2.up;
      this.m_end = Vector2.up + Vector2.right;
      this.m_slack = 0.5f;
      this.m_steps = 20;
      this.m_regen = true;
    }

    public CableCurve(Vector2[] inputPoints)
    {
      this.points = inputPoints;
      this.m_start = inputPoints[0];
      this.m_end = inputPoints[1];
      this.m_slack = 0.5f;
      this.m_steps = 20;
      this.m_regen = true;
    }

    public CableCurve(CableCurve v)
    {
      this.points = v.Points();
      this.m_start = v.start;
      this.m_end = v.end;
      this.m_slack = v.slack;
      this.m_steps = v.steps;
      this.m_regen = v.regenPoints;
    }

    public Vector2[] Points()
    {
      if (!this.m_regen)
        return this.points;
      if (this.m_steps < 2)
        return CableCurve.emptyCurve;
      double num1 = (double) Vector2.Distance(this.m_end, this.m_start);
      float num2 = Vector2.Distance(new Vector2(this.m_end.x, this.m_start.y), this.m_start);
      double num3 = (double) Mathf.Max(0.0001f, this.m_slack);
      float f1 = (float) (num1 + num3);
      float num4 = 0.0f;
      float y1 = this.m_start.y;
      float num5 = num2;
      float y2 = this.end.y;
      if ((double) num5 - (double) num4 == 0.0)
        return CableCurve.emptyCurve;
      float num6 = Mathf.Sqrt(Mathf.Pow(f1, 2f) - Mathf.Pow(y2 - y1, 2f)) / (num5 - num4);
      int num7 = 30;
      int num8 = 0;
      int num9 = num7 * 10;
      bool flag = false;
      float num10 = 0.0f;
      float num11 = 100f;
      for (int index1 = 0; index1 < num7; ++index1)
      {
        for (int index2 = 0; index2 < 10; ++index2)
        {
          ++num8;
          float num12 = num10 + num11;
          float f2 = (float) Math.Sinh((double) num12) / num12;
          if (!float.IsInfinity(f2))
          {
            if ((double) f2 == (double) num6)
            {
              flag = true;
              num10 = num12;
              break;
            }
            if ((double) f2 <= (double) num6)
            {
              num10 = num12;
              if (num8 > num9)
              {
                flag = true;
                break;
              }
            }
            else
              break;
          }
        }
        if (!flag)
          num11 *= 0.1f;
        else
          break;
      }
      float num13 = (float) (((double) num5 - (double) num4) / 2.0) / num10;
      float num14 = (float) (((double) num4 + (double) num5 - (double) num13 * (double) Mathf.Log((float) (((double) f1 + (double) y2 - (double) y1) / ((double) f1 - (double) y2 + (double) y1)))) / 2.0);
      float num15 = (float) (((double) y2 + (double) y1 - (double) f1 * Math.Cosh((double) num10) / Math.Sinh((double) num10)) / 2.0);
      this.points = new Vector2[this.m_steps];
      float num16 = (float) (this.m_steps - 1);
      for (int index = 0; index < this.m_steps; ++index)
      {
        float t = (float) index / num16;
        Vector2 zero = Vector2.zero with
        {
          x = Mathf.Lerp(this.start.x, this.end.x, t),
          y = num13 * (float) Math.Cosh(((double) t * (double) num2 - (double) num14) / (double) num13) + num15
        };
        this.points[index] = zero;
      }
      this.m_regen = false;
      return this.points;
    }
  }
}
