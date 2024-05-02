

using UnityEngine;

#nullable disable
public class SandPool : MonoBehaviour
{
  private IndirectRenderer rend;
  public SpriteRenderer spriteRenderer;
  public int instanceCount;
  public Mesh instanceMesh;
  public Material instanceMaterial;
  public int subMeshIndex;
  private int cachedInstanceCount = -1;
  private int cachedSubMeshIndex = -1;
  private ComputeBuffer positionBuffer;
  private ComputeBuffer argsBuffer;
  private uint[] args = new uint[5];
  private int killFrames = 100;
  private int curKillCount;
  public static bool NeedApply;
  private int delay;

  public static SandPool Instance { get; private set; }

  public void Reset() => this.instanceCount = 0;

  public static SandPool Create()
  {
    if ((Object) SandPool.Instance != (Object) null)
    {
      SandPool.Instance.curKillCount = 0;
      return SandPool.Instance;
    }
    SandPool.Instance = Object.Instantiate<SandPool>(ClientResources.Instance.sandPool);
    SandPool.Instance.CreateMesh();
    SandPool.Instance.rend = new IndirectRenderer(30000, SandPool.Instance.instanceMaterial, SandPool.Instance.instanceMesh);
    return SandPool.Instance;
  }

  public void ApplyBlit()
  {
    SandPool.NeedApply = false;
    Client.game.map.Apply();
    this.delay = 0;
  }

  public void Add(Vector3 pos)
  {
    if (this.instanceCount > 29999)
      return;
    IndirectRenderer.TransformData[this.instanceCount].x = pos.x;
    IndirectRenderer.TransformData[this.instanceCount].y = pos.y;
    ++this.instanceCount;
  }

  private void Update()
  {
    if (SandPool.NeedApply)
    {
      ++this.delay;
      if (this.delay > 1)
        this.ApplyBlit();
    }
    if (this.instanceCount == 0)
    {
      ++this.curKillCount;
      if (this.curKillCount < 100)
        return;
      SandPool.Instance = (SandPool) null;
      if (SandPool.NeedApply)
        this.ApplyBlit();
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (this.curKillCount > 0)
        this.curKillCount = 0;
      this.rend.Draw(this.instanceCount);
    }
  }

  private void CreateMesh()
  {
    Sprite sprite = this.spriteRenderer.sprite;
    Mesh mesh = new Mesh();
    UnityEngine.Bounds bounds = sprite.bounds;
    mesh.vertices = new Vector3[4]
    {
      new Vector3(bounds.min.x, bounds.min.y),
      new Vector3(bounds.min.x, bounds.max.y),
      new Vector3(bounds.max.x, bounds.max.y),
      new Vector3(bounds.max.x, bounds.min.y)
    };
    mesh.triangles = new int[6]{ 0, 1, 2, 2, 3, 0 };
    Vector2 scale = new Vector2(1f / (float) sprite.texture.width, 1f / (float) sprite.texture.height);
    Vector2 vector2_1;
    ref Vector2 local1 = ref vector2_1;
    Rect rect1 = sprite.rect;
    double x1 = (double) rect1.x;
    rect1 = sprite.rect;
    double yMax1 = (double) rect1.yMax;
    local1 = new Vector2((float) x1, (float) yMax1);
    Vector2 vector2_2;
    ref Vector2 local2 = ref vector2_2;
    Rect rect2 = sprite.rect;
    double x2 = (double) rect2.x;
    rect2 = sprite.rect;
    double y1 = (double) rect2.y;
    local2 = new Vector2((float) x2, (float) y1);
    Vector2 vector2_3;
    ref Vector2 local3 = ref vector2_3;
    Rect rect3 = sprite.rect;
    double xMax1 = (double) rect3.xMax;
    rect3 = sprite.rect;
    double y2 = (double) rect3.y;
    local3 = new Vector2((float) xMax1, (float) y2);
    Vector2 vector2_4;
    ref Vector2 local4 = ref vector2_4;
    Rect rect4 = sprite.rect;
    double xMax2 = (double) rect4.xMax;
    rect4 = sprite.rect;
    double yMax2 = (double) rect4.yMax;
    local4 = new Vector2((float) xMax2, (float) yMax2);
    vector2_1.Scale(scale);
    vector2_2.Scale(scale);
    vector2_3.Scale(scale);
    vector2_4.Scale(scale);
    mesh.uv = new Vector2[4]
    {
      vector2_1,
      vector2_2,
      vector2_3,
      vector2_4
    };
    mesh.colors = new Color[4]
    {
      Color.white,
      Color.white,
      Color.white,
      Color.white
    };
    this.spriteRenderer.GetPropertyBlock(new MaterialPropertyBlock());
    this.instanceMesh = mesh;
    this.instanceMaterial = this.spriteRenderer.material;
  }

  private void OnDestroy() => this.rend?.ReleaseBuffers(true);
}
