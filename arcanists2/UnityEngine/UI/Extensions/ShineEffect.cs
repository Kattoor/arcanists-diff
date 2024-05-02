// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.Extensions.ShineEffect
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace UnityEngine.UI.Extensions
{
  public class ShineEffect : MaskableGraphic
  {
    [SerializeField]
    private float yoffset = -1f;
    [SerializeField]
    private float width = 1f;

    public float Yoffset
    {
      get => this.yoffset;
      set
      {
        this.SetVerticesDirty();
        this.yoffset = value;
      }
    }

    public float Width
    {
      get => this.width;
      set
      {
        this.SetAllDirty();
        this.width = value;
      }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
      Vector4 vector4 = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
      float num = (float) (((double) vector4.w - (double) vector4.y) * 2.0);
      Color32 color = (Color32) this.color;
      vh.Clear();
      color.a = (byte) 0;
      vh.AddVert(new Vector3(vector4.x - 50f, (float) ((double) this.width * (double) vector4.y + (double) this.yoffset * (double) num)), color, new Vector2(0.0f, 0.0f));
      vh.AddVert(new Vector3(vector4.z + 50f, (float) ((double) this.width * (double) vector4.y + (double) this.yoffset * (double) num)), color, new Vector2(1f, 0.0f));
      color.a = (byte) ((double) this.color.a * (double) byte.MaxValue);
      vh.AddVert(new Vector3(vector4.x - 50f, (float) ((double) this.width * ((double) vector4.y / 4.0) + (double) this.yoffset * (double) num)), color, new Vector2(0.0f, 1f));
      vh.AddVert(new Vector3(vector4.z + 50f, (float) ((double) this.width * ((double) vector4.y / 4.0) + (double) this.yoffset * (double) num)), color, new Vector2(1f, 1f));
      color.a = (byte) ((double) this.color.a * (double) byte.MaxValue);
      vh.AddVert(new Vector3(vector4.x - 50f, (float) ((double) this.width * ((double) vector4.w / 4.0) + (double) this.yoffset * (double) num)), color, new Vector2(0.0f, 1f));
      vh.AddVert(new Vector3(vector4.z + 50f, (float) ((double) this.width * ((double) vector4.w / 4.0) + (double) this.yoffset * (double) num)), color, new Vector2(1f, 1f));
      color.a = (byte) ((double) this.color.a * (double) byte.MaxValue);
      color.a = (byte) 0;
      vh.AddVert(new Vector3(vector4.x - 50f, (float) ((double) this.width * (double) vector4.w + (double) this.yoffset * (double) num)), color, new Vector2(0.0f, 1f));
      vh.AddVert(new Vector3(vector4.z + 50f, (float) ((double) this.width * (double) vector4.w + (double) this.yoffset * (double) num)), color, new Vector2(1f, 1f));
      vh.AddTriangle(0, 1, 2);
      vh.AddTriangle(2, 3, 1);
      vh.AddTriangle(2, 3, 4);
      vh.AddTriangle(4, 5, 3);
      vh.AddTriangle(4, 5, 6);
      vh.AddTriangle(6, 7, 5);
    }

    public void Triangulate(VertexHelper vh)
    {
      int message = vh.currentVertCount - 2;
      Debug.Log((object) message);
      for (int idx0 = 0; idx0 <= message / 2 + 1; idx0 += 2)
      {
        vh.AddTriangle(idx0, idx0 + 1, idx0 + 2);
        vh.AddTriangle(idx0 + 2, idx0 + 3, idx0 + 1);
      }
    }
  }
}
