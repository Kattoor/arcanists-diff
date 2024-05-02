

using System;
using UnityEngine;

#nullable disable
public class IndirectRenderer
{
  private ComputeBuffer ArgsBuffer;
  private ComputeBuffer TransformBuffer;
  private ComputeBuffer ColorBuffer;
  public static Vector4[] TransformData;
  private Mesh Mesh;
  private Material Material;
  private uint[] args = new uint[5];

  public IndirectRenderer(int maxProjectiles, Material material, Mesh mesh)
  {
    QualitySettings.vSyncCount = 0;
    this.Mesh = mesh;
    this.Material = material;
    this.ArgsBuffer = new ComputeBuffer(1, this.args.Length * 4, ComputeBufferType.DrawIndirect);
    this.InitializeBuffers(maxProjectiles);
  }

  private void InitializeBuffers(int maxProjectiles)
  {
    this.ReleaseBuffers(false);
    this.TransformBuffer = new ComputeBuffer(maxProjectiles, 16);
    if (IndirectRenderer.TransformData == null || IndirectRenderer.TransformData.Length != maxProjectiles)
    {
      IndirectRenderer.TransformData = new Vector4[maxProjectiles];
      for (int index = 0; index < IndirectRenderer.TransformData.Length; ++index)
      {
        IndirectRenderer.TransformData[index].w = 1f;
        IndirectRenderer.TransformData[index].z = 0.0f;
      }
    }
    this.Material.SetBuffer("positionBuffer", this.TransformBuffer);
    this.args[0] = (UnityEngine.Object) this.Mesh != (UnityEngine.Object) null ? this.Mesh.GetIndexCount(0) : 0U;
    this.args[1] = (uint) maxProjectiles;
    this.ArgsBuffer.SetData((Array) this.args);
  }

  public void UpdateBufferData(int index, Vector3 data)
  {
    if (index < IndirectRenderer.TransformData.Length)
      IndirectRenderer.TransformData[index] = new Vector4(data.x, data.y, 1f, 0.0f);
    else
      Debug.Log((object) "Error: Initialized more projectiles than Projectile Type allows.");
  }

  public void Draw(int activeProjectileCount)
  {
    if (activeProjectileCount == 0)
      return;
    this.TransformBuffer.SetData((Array) IndirectRenderer.TransformData, 0, 0, activeProjectileCount);
    this.args[1] = (uint) activeProjectileCount;
    this.ArgsBuffer.SetData((Array) this.args);
    Graphics.DrawMeshInstancedIndirect(this.Mesh, 0, this.Material, new UnityEngine.Bounds(Vector3.zero, new Vector3(3000f, 3000f, 3000f)), this.ArgsBuffer);
  }

  public void ReleaseBuffers(bool releaseArgs)
  {
    if (this.TransformBuffer != null)
      this.TransformBuffer.Release();
    this.TransformBuffer = (ComputeBuffer) null;
    if (this.ColorBuffer != null)
      this.ColorBuffer.Release();
    this.ColorBuffer = (ComputeBuffer) null;
    if (!releaseArgs)
      return;
    if (this.ArgsBuffer != null)
      this.ArgsBuffer.Release();
    this.ArgsBuffer = (ComputeBuffer) null;
  }
}
