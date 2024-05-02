

using Junk;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class ZMap
{
  public FixedInt Gravity = (FixedInt) -838860L;
  public Action doneCallback;
  public MyLocation wind = new MyLocation(0, 0);
  public FixedInt windSpeed = (FixedInt) 0;
  public FixedInt windDir = (FixedInt) -90;
  public static FixedInt MaxWindSpeed = (FixedInt) 52428L;
  public static FixedInt MaxSpeed = FixedInt.Create(100);
  public static FixedInt SnowSpeed = FixedInt.Create(11);
  private int _height;
  private int _width;
  private int _numBlocksHorizontal;
  private const int BlockSize = 16384;
  private const int _blockHeight = 128;
  public ZMyWorld world;
  public ZGame game;
  internal List<PastBlits> _pastBits = new List<PastBlits>();
  private bool purple;
  private Sprite sprite;
  private List<ZMap.RawSprite> rawspriteColors;
  public Color32[] deserializedPixels;
  public byte[] serializedMap;
  public static Dictionary<Texture2D, Color32[]> cachedPixels = new Dictionary<Texture2D, Color32[]>();
  public static Dictionary<int, BufferedImage> cachedCutouts = new Dictionary<int, BufferedImage>();

  public int Height => this._height;

  public int Width => this._width;

  public void OnGhosted(int x, int y, int r, ZMyCollider colliderIgnore = null)
  {
    foreach (ZMyCollider z in this.world.OverlapCircleAll(new Point(x, y), r, colliderIgnore, Inert.mask_movement_NoEffector))
    {
      if (!ZComponent.IsNull((ZComponent) z) && ((ZComponent) z.creature != (object) null || (ZComponent) z.tower != (object) null))
      {
        ZMap.GhostTerrain ghostTerrain = z.ghosting;
        if (ghostTerrain == null)
        {
          z.ghosting = new ZMap.GhostTerrain()
          {
            x = x,
            y = y,
            radius2 = r * r
          };
        }
        else
        {
          int num = 0;
          while (ghostTerrain.next != null)
          {
            ghostTerrain = ghostTerrain.next;
            ++num;
            if (num >= 10)
              return;
          }
          ghostTerrain.next = new ZMap.GhostTerrain()
          {
            x = x,
            y = y,
            radius2 = r * r
          };
        }
        z.ghosted = true;
      }
    }
  }

  public void UpdateWind() => this.wind = Global.Velocity(this.windDir, this.windSpeed);

  public void RandomizeWind()
  {
    this.windDir = this.game.RandomFixedInt(0, 360);
    this.windSpeed = this.game.RandomFixedInt(0, 1) * ZMap.MaxWindSpeed;
    this.wind = Global.Velocity(this.windDir, this.windSpeed);
    if (!this.game.isClient)
      return;
    WindIndicatorUI.Instance?.Refresh();
  }

  public void ToggleWind(bool v)
  {
    this.game.isWindy = v;
    WindIndicatorUI.Instance?.gameObject.SetActive(v);
    if (!v)
      return;
    WindIndicatorUI.Instance?.Refresh();
  }

  public void SetMapSprite(ZGame game, Sprite s)
  {
    this.sprite = s;
    if ((UnityEngine.Object) s != (UnityEngine.Object) null)
      this.SetMapSprite(game, s.texture.GetPixels32(), s.texture.height, s.texture.width);
    else
      this.rawspriteColors = (List<ZMap.RawSprite>) null;
  }

  public void SetMapSprite(ZGame game, Color32[] pixels, int height, int width)
  {
    this._height = height;
    this._width = width;
    this._numBlocksHorizontal = this.Width / 128;
    if (this.Width % 128 != 0)
      ++this._numBlocksHorizontal;
    this.SetSprites(game, pixels);
    if (!game.isClient)
      return;
    CameraMovement objectOfType = UnityEngine.Object.FindObjectOfType<CameraMovement>();
    if ((UnityEngine.Object) objectOfType != (UnityEngine.Object) null)
      objectOfType.Start();
    if (!(bool) (UnityEngine.Object) MapObjects.Instance)
      return;
    MapObjects.Instance.SetDashes();
    MapObjects.Instance.SetWaves();
    MapObjects.Instance.ColorWaves();
  }

  public void CleanUp()
  {
    this.game = (ZGame) null;
    if (this.rawspriteColors == null)
      return;
    foreach (ZMap.RawSprite rawspriteColor in this.rawspriteColors)
    {
      if (rawspriteColor != null && (UnityEngine.Object) rawspriteColor.sprite != (UnityEngine.Object) null)
      {
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) rawspriteColor.sprite.texture);
        UnityEngine.Object.Destroy((UnityEngine.Object) rawspriteColor.sprite);
      }
    }
    this.rawspriteColors.Clear();
    Resources.UnloadUnusedAssets();
  }

  public void Purpalize()
  {
    if (this.purple)
      return;
    this.purple = true;
    foreach (ZMap.RawSprite rawspriteColor in this.rawspriteColors)
      rawspriteColor.Purpalize();
  }

  public Sprite GetMapSprite => this.sprite;

  public List<ZMap.RawSprite> GetRawSprites() => this.rawspriteColors;

  public void SetSprites(ZGame game, Color32[] x)
  {
    this.game = game;
    game.totalTurnsCombined = -1;
    this.rawspriteColors = new List<ZMap.RawSprite>();
    int x1 = this.Width / 128;
    int y = this.Height / 128;
    if (this.Width % 128 != 0)
      ++x1;
    if (this.Height % 128 != 0)
      ++y;
    Color white = Color.white;
    if (game.isClient)
    {
      float num = Mathf.Lerp(0.4f, 1f, Mathf.Clamp01(PlayerPrefs.GetFloat("prefblackFg", PlayerPrefs.GetFloat("prefblackBg", 1f))));
      white.r = num;
      white.g = num;
      white.b = num;
    }
    for (int index1 = 0; index1 < y; ++index1)
    {
      for (int index2 = 0; index2 < x1; ++index2)
      {
        Color32[] color32Array = new Color32[16384];
        for (int index3 = 0; index3 < 128; ++index3)
        {
          for (int index4 = 0; index4 < 128; ++index4)
          {
            int num1 = index2 * 128 + index4;
            int num2 = index1 * this.Width * 128 + index3 * this.Width;
            if (num1 < this.Width && num2 < x.Length)
              color32Array[index3 * 128 + index4] = x[num1 + num2];
            else
              break;
          }
        }
        if (!game.isClient)
        {
          this.rawspriteColors.Add(new ZMap.RawSprite((Sprite) null, (SpriteRenderer) null, color32Array));
        }
        else
        {
          GameObject gameObjectWithTag = GameObject.FindGameObjectWithTag("map");
          GameObject gameObject = new GameObject("map" + (object) index1);
          gameObject.layer = 12;
          gameObject.transform.SetParent(gameObjectWithTag.transform);
          gameObject.transform.position = new Vector3((float) (index2 * 128 + 64), (float) (index1 * 128) + 64f, 0.0f);
          Texture2D texture = new Texture2D(128, 128);
          texture.wrapMode = TextureWrapMode.Clamp;
          texture.SetPixels32(color32Array);
          texture.Apply(true);
          SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
          Sprite s = sr.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, 128f, 128f), new Vector2(0.5f, 0.5f), 1f, 1U, SpriteMeshType.FullRect);
          sr.sortingOrder = -20;
          sr.color = white;
          this.rawspriteColors.Add(new ZMap.RawSprite(s, sr, color32Array));
        }
      }
    }
    this.Apply();
    game.world.Init(x1, y, this.rawspriteColors.Count);
    if (!game.isClient)
    {
      if (this.serializedMap == null)
        this.serializedMap = this.CompressMap(x);
      game.OnMapGeneratedCompleted();
    }
    game.MAPCREATED = true;
    game.random = new IsaacCipher(new int[1]
    {
      game.gameFacts.seed
    });
    if (this.doneCallback == null)
      return;
    if (game.First_Turn_Teleport)
    {
      for (int index = 0; index < game.players.Count; ++index)
      {
        game.players[index].controlled[0].inWater = true;
        game.players[index].controlled[0].SetPosition(new MyLocation(this.Width / 2, -1000));
      }
    }
    else if (game.players.Count == 2)
    {
      int num3 = 256;
      int num4 = game.gameFacts.GetTimeInSeconds() <= 10 ? 256 : 448;
      int num5 = game.gameFacts.realMap == MapEnum.Jungle ? 300 : 0;
      for (int index5 = 0; index5 < game.players.Count; ++index5)
      {
        List<MyLocation> myLocationList = new List<MyLocation>();
        FixedInt fixedInt = (FixedInt) (this.Height - game.players[index5].controlled[0].radius);
        MyLocation startPosition1 = game.players[index5].controlled[0].GetStartPosition(new MyLocation((FixedInt) (index5 == 0 ? num3 : this.Width - num3), fixedInt - num5));
        myLocationList.Add(startPosition1);
        for (int index6 = 16; index6 <= num4; index6 += 16)
        {
          MyLocation startPosition2 = game.players[index5].controlled[0].GetStartPosition(new MyLocation((FixedInt) (index5 == 0 ? num3 + index6 : this.Width - num3 - index6), fixedInt - num5));
          myLocationList.Add(startPosition2);
        }
        myLocationList.Sort((Comparison<MyLocation>) ((aa, bb) => (int) aa.y - (int) bb.y));
        game.players[index5].controlled[0].SetPosition(myLocationList[1]);
      }
    }
    else
    {
      int num6 = (this.Width - 800 - (game.gameFacts.realMap == MapEnum.Wasteland ? 300 : 0)) / (game.players.Count == 1 ? 1 : game.players.Count - 1);
      int num7 = game.players.Count == 1 ? this.Width / 2 : 400;
      int num8 = game.gameFacts.realMap == MapEnum.Jungle ? 300 : 0;
      if (game.gameFacts.realMap == MapEnum.Wasteland)
        num7 += 300;
      for (int index = 0; index < game.players.Count; ++index)
      {
        FixedInt x2 = (FixedInt) (num6 * index + num7);
        FixedInt fixedInt = (FixedInt) (this.Height - game.players[index].controlled[0].radius);
        game.players[index].controlled[0].position = new MyLocation(x2, fixedInt - num8);
        game.players[index].controlled[0].SetStartPosition();
      }
    }
    if (game.isClient && game.isSpectator && (UnityEngine.Object) CameraMovement.Instance != (UnityEngine.Object) null)
      CameraMovement.Instance.GotoPosition(new Vector3((float) (this.Width / 2), (float) (this.Height / 2), 0.0f));
    game.OnSetup();
    this.doneCallback();
    this.doneCallback = (Action) null;
  }

  internal byte[] CompressMap(Color32[] x)
  {
    using (MemoryStream memoryStream1 = new MemoryStream())
    {
      using (myBinaryWriter myBinaryWriter1 = new myBinaryWriter((Stream) memoryStream1))
      {
        myBinaryWriter1.Write((byte) 16);
        myBinaryWriter1.Write(this.Width);
        myBinaryWriter1.Write(this.Height);
        myBinaryWriter1.Write(Server.CompressMap);
        if (Server.CompressMap)
        {
          using (MemoryStream memoryStream2 = new MemoryStream())
          {
            using (myBinaryWriter myBinaryWriter2 = new myBinaryWriter((Stream) memoryStream2))
            {
              myBinaryWriter2.Write(x.Length);
              for (int index = 0; index < x.Length; ++index)
                myBinaryWriter2.Write(x[index]);
            }
            byte[] buffer = CLZF2.Compress(memoryStream2.ToArray());
            myBinaryWriter1.Write(buffer);
          }
        }
        else
        {
          myBinaryWriter1.Write(x.Length);
          for (int index = 0; index < x.Length; ++index)
            myBinaryWriter1.Write(x[index]);
        }
      }
      return memoryStream1.ToArray();
    }
  }

  private void PixelCount(Color32[] g)
  {
    HashSet<int> intSet = new HashSet<int>();
    foreach (Color32 color32 in g)
      intSet.Add(((int) color32.r << 24) + ((int) color32.g << 16) + ((int) color32.b << 8));
    Debug.Log((object) ("Pixel Count: " + (object) intSet.Count));
  }

  public bool IsGround(int x, int y)
  {
    return this.InBounds(x, y) && this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a > (byte) 0;
  }

  public bool IsEmpty(int x, int y)
  {
    return !this.InBounds(x, y) || this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a > (byte) 0;
  }

  public void UpdatePixel(int x, int y, Color32 c)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return;
    this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].SetPixel((x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7), c);
  }

  public Color32 GetPixel(int x, int y)
  {
    return x < 0 || y < 0 || x >= this.Width || y >= this.Height ? (Color32) Color.white : this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)];
  }

  private bool PixelNotTransparent(int x, int y)
  {
    return x >= 0 && y >= 0 && x < this.Width && y < this.Height && this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a > (byte) 0;
  }

  public bool PixelNotAlpha(int x, int y, ZCreature creature, int mask, bool collideWithThorns = true)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return false;
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a != (byte) 0 || this.PhysicsCollidePoint(creature, x, y, mask, collideWithThorns);
  }

  public bool SpellPixelNotAlpha(int x, int y, ZCreature creature, int mask)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return false;
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a != (byte) 0 || this.SpellPhysicsCollidePoint(creature, x, y, mask);
  }

  public bool LeafPixelNotAlpha(int x, int y, ZCreature creature, int mask)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return false;
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a != (byte) 0 || this.LeafPhysicsCollidePoint(creature, x, y, mask);
  }

  public bool SpellPixelNotAlphaIgnoreSelf(int x, int y, ZMyCollider ignore, int mask)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return false;
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a != (byte) 0 || this.SpellPhysicsCollidePointIgnoreSelf(ignore, x, y, mask);
  }

  public bool PixelNotAlphaUndeadOnly(int x, int y, ZCreature creature, ZSpell spell, int mask)
  {
    if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
      return false;
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a != (byte) 0 || this.SpellPhysicsCollidePointUndeadOnly(creature, spell, x, y, mask);
  }

  public bool BitBltPixelNotAlpha(int x, int y)
  {
    return x >= 0 && y >= 0 && x < this.Width && y < this.Height && this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a > (byte) 0;
  }

  public Color32 BitBltPixelColor(int x, int y)
  {
    return x < 0 || y < 0 || x >= this.Width || y >= this.Height ? (Color32) Color.white : this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)];
  }

  public void SetPixelColor(int x, int y, Color32 p)
  {
    if (!this.InBounds(x, y))
      return;
    this.UpdatePixel(x, y, p);
  }

  public void DrawRectangle(int xx, int yy, int width, int height, Color32 p)
  {
    for (int index1 = 0; index1 < width; ++index1)
    {
      for (int index2 = 0; index2 < height; ++index2)
        this.UpdatePixel(xx + index1, yy + index2, p);
    }
  }

  public void DrawEllipse(int x, int y, int width, int height, Color32 color)
  {
    int f1 = width;
    int f2 = height;
    for (int x1 = x - Mathf.RoundToInt((float) f1); x1 <= x + Mathf.RoundToInt((float) f1); ++x1)
    {
      for (int y1 = y - Mathf.RoundToInt((float) f2); y1 <= y + Mathf.RoundToInt((float) f2); ++y1)
      {
        double num1 = (double) (x1 - x);
        float num2 = (float) (y1 - y);
        if (num1 * num1 / (double) (f1 * f1) + (double) num2 * (double) num2 / (double) (f2 * f2) < 1.0 && this.InBounds(x1, y1))
          this.UpdatePixel(x1, y1, color);
      }
    }
  }

  public void DrawEllipseOutline(
    int x,
    int y,
    int width,
    int height,
    int outlineRadius,
    Color32 color)
  {
    int f1 = width / 2;
    int f2 = height / 2;
    for (int x1 = x - Mathf.RoundToInt((float) f1); x1 <= x + Mathf.RoundToInt((float) f1); ++x1)
    {
      for (int y1 = y - Mathf.RoundToInt((float) f2); y1 <= y + Mathf.RoundToInt((float) f2); ++y1)
      {
        float num1 = (float) (x1 - x);
        float num2 = (float) (y1 - y);
        if ((double) num1 * (double) num1 / (double) (f1 * f1) + (double) num2 * (double) num2 / (double) (f2 * f2) < 1.0 && (double) num1 * (double) num1 / (double) ((f1 - outlineRadius) * (f1 - outlineRadius)) + (double) num2 * (double) num2 / (double) ((f2 - outlineRadius) * (f2 - outlineRadius)) >= 1.0 && this.InBounds(x1, y1))
          this.UpdatePixel(x1, y1, color);
      }
    }
  }

  public bool InBounds(int x, int y, int radius)
  {
    return x + radius < this.Width && x - radius >= 0 && y - radius >= 0 && y + radius < this.Height;
  }

  public bool InBounds(int x, int y) => x < this.Width && x >= 0 && y >= 0 && y < this.Height;

  public bool CheckPosition(int x, int y, ZCreature creature, int mask)
  {
    if (x < this.Width && x >= 0 && y >= 0 && y < this.Height)
      return !this.PixelNotAlpha(x, y, creature, mask);
    if (mask == Inert.mask_entity_movement)
      this.PhysicsCollidePoint(creature, x, y, 2560, false);
    return !((ZComponent) creature != (object) null) || !creature.flying || creature.entangledOrGravity || x < 0 || x >= this.Width || creature.race == CreatureRace.Effector || (x < this.Width && x >= 0 || !(creature.velocity.y == 0)) && (y >= 0 && y < this.Height || !(creature.velocity.x == 0));
  }

  public bool SpellCheckPosition(int x, int y, ZCreature creature, int mask)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height ? !this.SpellPhysicsCollidePoint(creature, x, y, mask) : !this.SpellPixelNotAlpha(x, y, creature, mask);
  }

  public bool LeafCheckPosition(int x, int y, ZCreature creature, int mask)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height ? !this.LeafPhysicsCollidePoint(creature, x, y, mask) : !this.LeafPixelNotAlpha(x, y, creature, mask);
  }

  public bool SpellCheckPositionIgnoreSelf(int x, int y, ZMyCollider ignore, int mask)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height ? !this.SpellPhysicsCollidePointIgnoreSelf(ignore, x, y, mask) : !this.SpellPixelNotAlphaIgnoreSelf(x, y, ignore, mask);
  }

  public bool SpellCheckPositionEntities(int x, int y, ZCreature creature, int mask)
  {
    return !this.SpellPhysicsCollidePoint(creature, x, y, mask);
  }

  public bool SpellCheckPositionUndeadOnly(
    int x,
    int y,
    ZCreature creature,
    ZSpell spell,
    int mask)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height ? !this.SpellPhysicsCollidePointUndeadOnly(creature, spell, x, y, mask) : !this.PixelNotAlphaUndeadOnly(x, y, creature, spell, mask);
  }

  public bool CheckPositionPhantom(int x, int y, ZCreature creature, int mask)
  {
    if (x < this.Width && x >= 0 && y >= 0 && y < this.Height)
      return !this.PhysicsCollidePoint(creature, x, y, mask);
    return !((ZComponent) creature != (object) null) || !creature.flying || creature.entangledOrGravity || x < 0 || x >= this.Width || creature.race == CreatureRace.Effector || (x < this.Width && x >= 0 || !(creature.velocity.y == 0)) && (y >= 0 && y < this.Height || !(creature.velocity.x == 0));
  }

  public bool CheckPositionOnlyEntities(int x, int y, ZCreature creature, int mask)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height || !this.PhysicsCollidePoint(creature, x, y, mask);
  }

  public bool CheckPositionOnlyMap(int x, int y)
  {
    return x >= this.Width || x < 0 || y < 0 || y >= this.Height || this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a == (byte) 0;
  }

  public bool CheckClampedPixel(int x, int y)
  {
    return this.rawspriteColors[(x >> 7) + (y >> 7) * this._numBlocksHorizontal].colors[(x & (int) sbyte.MaxValue) + ((y & (int) sbyte.MaxValue) << 7)].a == (byte) 0;
  }

  public void DoPastBlit(PastBlits p)
  {
    if (p.hue)
    {
      Color32[] pixels32;
      if (!ZMap.cachedPixels.TryGetValue(Inert.Instance.cutouts[p.index], out pixels32))
      {
        pixels32 = Inert.Instance.cutouts[p.index].GetPixels32();
        ZMap.cachedPixels.Add(Inert.Instance.cutouts[p.index], pixels32);
      }
      RotateImage.RenderHUE(this, new Surface(pixels32, Inert.Instance.cutouts[p.index].width, Inert.Instance.cutouts[p.index].height), p.x, p.y, 0.0f, false, p.brightness.ToFloat());
    }
    else if (p.rotate)
    {
      Color32[] pixels32;
      if (!ZMap.cachedPixels.TryGetValue(Inert.Instance.cutouts[p.index], out pixels32))
      {
        pixels32 = Inert.Instance.cutouts[p.index].GetPixels32();
        ZMap.cachedPixels.Add(Inert.Instance.cutouts[p.index], pixels32);
      }
      RotateImage.Render(this, new Surface(pixels32, Inert.Instance.cutouts[p.index].width, Inert.Instance.cutouts[p.index].height), p.x, p.y, p.brightness, 1, p.ignoreAlpha, true, (FixedInt) 1);
    }
    else if (p.brightness > 0)
      this.BitBltBrightness(Inert.Instance.cutouts[p.index], p.x, p.y, p.brightness.ToFloat(), p.ignoreAlpha);
    else
      this.BitBlt(Inert.Instance.cutouts[p.index], p.x, p.y, p.ignoreAlpha);
  }

  public void ServerBitBltRotate(
    int index,
    int x,
    int y,
    FixedInt Angle,
    bool infront,
    bool apply = true)
  {
    if (!this.game.AllowTerrainDestruction || index == 38)
      return;
    this._pastBits.Add(new PastBlits(x, y, index, infront, Angle, true));
    RotateImage.Render(this, new Surface(Inert.Instance.cutouts[index]), x, y, Angle, 1, infront, true, (FixedInt) 1, apply);
  }

  public void ServerBitBlt(int index, int x, int y, bool ignoreAlpha = true, bool apply = true)
  {
    if (!this.game.AllowTerrainDestruction || index == 38)
      return;
    this._pastBits.Add(new PastBlits(x, y, index, ignoreAlpha, (FixedInt) 0));
    this.BitBlt(Inert.Instance.cutouts[index], x, y, ignoreAlpha);
  }

  public void ServerBitBltHue(int index, int x, int y, float hue, bool ignoreAlpha = true, bool apply = true)
  {
    if (!this.game.AllowTerrainDestruction || index == 38)
      return;
    this._pastBits.Add(new PastBlits(x, y, index, ignoreAlpha, (FixedInt) hue, false, true));
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(Inert.Instance.cutouts[index], out pixels32))
    {
      pixels32 = Inert.Instance.cutouts[index].GetPixels32();
      ZMap.cachedPixels.Add(Inert.Instance.cutouts[index], pixels32);
    }
    RotateImage.RenderHUE(this, new Surface(pixels32, Inert.Instance.cutouts[index].width, Inert.Instance.cutouts[index].height), x, y, 0.0f, false, hue);
  }

  public void ServerBitBltBrightness(
    int index,
    int x,
    int y,
    FixedInt brightness,
    bool ignoreAlpha = true)
  {
    if (!this.game.AllowTerrainDestruction || index == 38)
      return;
    this._pastBits.Add(new PastBlits(x, y, index, ignoreAlpha, brightness));
    this.BitBlt(Inert.Instance.cutouts[index], x, y, ignoreAlpha);
  }

  public int ServerBitBltAndReturnIfChanged(int index, int x, int y, bool ignoreAlpha = true)
  {
    if (!this.game.AllowTerrainDestruction || index == 38)
      return 0;
    this._pastBits.Add(new PastBlits(x, y, index, ignoreAlpha, (FixedInt) -1));
    return this.BitBltAndReturnIfChanged(Inert.Instance.cutouts[index], x, y, ignoreAlpha);
  }

  public static Color32[] GetPixels32(Texture2D mask)
  {
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(mask, out pixels32))
    {
      pixels32 = mask.GetPixels32();
      ZMap.cachedPixels.Add(mask, pixels32);
    }
    return pixels32;
  }

  public void BitBlt(Color32 color, int x, int y)
  {
    if (this.BitBltPixelNotAlpha(x, y))
      return;
    this.UpdatePixel(x, y, color);
    this.Apply();
  }

  public void BitBltDelay(Color32 color, int x, int y)
  {
    if (this.BitBltPixelNotAlpha(x, y))
      return;
    this.UpdatePixel(x, y, color);
    this.Apply();
  }

  public void BitBlt(Texture2D mask, int x, int y, bool ignoreAlpha = true, bool apply = true)
  {
    int num1 = 0;
    int num2 = 0;
    int height = mask.height;
    int width = mask.width;
    x -= width / 2;
    y -= height / 2;
    if (x < 0)
    {
      num1 -= x;
      x = 0;
    }
    if (y < 0)
    {
      num2 -= y;
      y = 0;
    }
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(mask, out pixels32))
    {
      pixels32 = mask.GetPixels32();
      ZMap.cachedPixels.Add(mask, pixels32);
    }
    int num3 = num1;
    int num4 = x;
    if (ignoreAlpha)
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num5 = num3;
        for (x = num4; num5 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num5;
          if ((Color) pixels32[index] != Color.black && this.BitBltPixelNotAlpha(x, y))
            this.UpdatePixel(x, y, pixels32[index]);
          ++num5;
        }
        ++num2;
      }
    }
    else
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num6 = num3;
        for (x = num4; num6 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num6;
          if (pixels32[index].a != (byte) 0)
            this.UpdatePixel(x, y, pixels32[index]);
          ++num6;
        }
        ++num2;
      }
    }
    if (!apply)
      return;
    this.Apply();
  }

  public void BitBlt(int radius, int x, int y, bool ignoreAlpha = true, bool apply = true)
  {
    BufferedImage bufferedImage = (BufferedImage) null;
    if (!ZMap.cachedCutouts.TryGetValue(radius, out bufferedImage))
    {
      int num = radius * 2 + 1;
      bufferedImage = new BufferedImage(num, num);
      bufferedImage.circle(num / 2, num / 2, 3, radius);
      ZMap.cachedCutouts.Add(radius, bufferedImage);
    }
    int num1 = 0;
    int num2 = 0;
    int height = bufferedImage.height;
    int width = bufferedImage.width;
    x -= width / 2;
    y -= height / 2;
    if (x < 0)
    {
      num1 -= x;
      x = 0;
    }
    if (y < 0)
    {
      num2 -= y;
      y = 0;
    }
    int num3 = num1;
    int num4 = x;
    if (ignoreAlpha)
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num5 = num3;
        for (x = num4; num5 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num5;
          if ((Color) bufferedImage.pixels[index] != Color.black && this.BitBltPixelNotAlpha(x, y))
            this.UpdatePixel(x, y, bufferedImage.pixels[index]);
          ++num5;
        }
        ++num2;
      }
    }
    else
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num6 = num3;
        for (x = num4; num6 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num6;
          if (bufferedImage.pixels[index].a != (byte) 0)
            this.UpdatePixel(x, y, bufferedImage.pixels[index]);
          ++num6;
        }
        ++num2;
      }
    }
    if (!apply)
      return;
    this.Apply();
  }

  public void BitBltBrightness(Texture2D mask, int x, int y, float multiplier, bool ignoreAlpha = true)
  {
    int num1 = 0;
    int num2 = 0;
    int height = mask.height;
    int width = mask.width;
    x -= width / 2;
    y -= height / 2;
    if (x < 0)
    {
      num1 -= x;
      x = 0;
    }
    if (y < 0)
    {
      num2 -= y;
      y = 0;
    }
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(mask, out pixels32))
    {
      pixels32 = mask.GetPixels32();
      ZMap.cachedPixels.Add(mask, pixels32);
    }
    int num3 = num1;
    int num4 = x;
    if (ignoreAlpha)
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num5 = num3;
        for (x = num4; num5 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num5;
          if ((Color) pixels32[index] != Color.black && this.BitBltPixelNotAlpha(x, y))
            this.UpdatePixel(x, y, new Color32((byte) ((double) pixels32[index].r * (double) multiplier), (byte) ((double) pixels32[index].g * (double) multiplier), (byte) ((double) pixels32[index].b * (double) multiplier), pixels32[index].a));
          ++num5;
        }
        ++num2;
      }
    }
    else
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num6 = num3;
        for (x = num4; num6 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num6;
          if (pixels32[index].a != (byte) 0)
            this.UpdatePixel(x, y, new Color32((byte) ((double) pixels32[index].r * (double) multiplier), (byte) ((double) pixels32[index].g * (double) multiplier), (byte) ((double) pixels32[index].b * (double) multiplier), pixels32[index].a));
          ++num6;
        }
        ++num2;
      }
    }
    this.Apply();
  }

  public int BitBltAndReturnIfChanged(Texture2D mask, int x, int y, bool ignoreAlpha = true)
  {
    int num1 = 0;
    int num2 = 0;
    int height = mask.height;
    int width = mask.width;
    int num3 = 0;
    x -= width / 2;
    y -= height / 2;
    if (x < 0)
    {
      num1 -= x;
      x = 0;
    }
    if (y < 0)
    {
      num2 -= y;
      y = 0;
    }
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(mask, out pixels32))
    {
      pixels32 = mask.GetPixels32();
      ZMap.cachedPixels.Add(mask, pixels32);
    }
    int num4 = num1;
    int num5 = x;
    if (ignoreAlpha)
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num6 = num4;
        for (x = num5; num6 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num6;
          if ((Color) pixels32[index] != Color.black && this.BitBltPixelNotAlpha(x, y))
          {
            this.UpdatePixel(x, y, pixels32[index]);
            ++num3;
          }
          ++num6;
        }
        ++num2;
      }
    }
    else
    {
      for (; num2 < height && y < this.Height; ++y)
      {
        int num7 = num4;
        for (x = num5; num7 < width && x < this.Width; ++x)
        {
          int index = num2 * width + num7;
          if (pixels32[index].a != (byte) 0)
          {
            this.UpdatePixel(x, y, pixels32[index]);
            ++num3;
          }
          ++num7;
        }
        ++num2;
      }
    }
    this.Apply();
    return num3;
  }

  public void BitBltBlood(Texture2D mask, int x, int y)
  {
    int num1 = 0;
    int num2 = 0;
    int height = mask.height;
    int width = mask.width;
    x -= width / 2;
    y -= height / 2;
    if (x < 0)
    {
      num1 -= x;
      x = 0;
    }
    if (y < 0)
    {
      num2 -= y;
      y = 0;
    }
    Color32[] pixels32;
    if (!ZMap.cachedPixels.TryGetValue(mask, out pixels32))
    {
      pixels32 = mask.GetPixels32();
      ZMap.cachedPixels.Add(mask, pixels32);
    }
    int num3 = num1;
    int num4 = x;
    for (; num2 < height && y < this.Height; ++y)
    {
      int num5 = num3;
      for (x = num4; num5 < width && x < this.Width; ++x)
      {
        int index = num2 * width + num5;
        if (pixels32[index].a != (byte) 0)
        {
          Color32 color32 = this.BitBltPixelColor(x, y);
          if (color32.a > (byte) 0 && ((int) color32.r < (int) pixels32[index].r || color32.b > (byte) 0 || color32.g > (byte) 0))
            this.UpdatePixel(x, y, pixels32[index]);
        }
        ++num5;
      }
      ++num2;
    }
    this.Apply();
  }

  public void Apply()
  {
    if (!this.game.isClient || this.game._resyncing)
      return;
    for (int index = 0; index < this.rawspriteColors.Count; ++index)
      this.rawspriteColors[index].Apply();
  }

  private ZCreature FindCreatureAtPoint(ZCreature creature, int x, int y, int mask)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject)
          return list[index].gameObjectLayer == 13 ? list[index].tower.creature : list[index].creature;
      }
    }
    this.world.listPool.ReturnList(list);
    return (ZCreature) null;
  }

  private bool PhysicsCollidePoint(
    ZCreature creature,
    int x,
    int y,
    int mask,
    bool collideWithThorns = true)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) creature == (object) null)
        {
          if (list[index].gameObjectLayer != 11 && list[index].gameObjectLayer != 9)
          {
            this.world.listPool.ReturnList(list);
            return true;
          }
        }
        else if (!((ZComponent) list[index] == (object) creature.colliderGameObject) && (!list[index].ghosted || !list[index].OverlapGhosting(x, y)) && (ZComponent.IsNull((ZComponent) creature.rider) || !((ZComponent) creature.rider.collider == (object) list[index])))
        {
          if ((ZComponent) creature.mount != (object) null && (ZComponent) creature.mount.collider == (object) list[index])
          {
            if (creature.velocity.y > 0)
              creature.Demount();
            this.world.listPool.ReturnList(list);
            return false;
          }
          if (list[index].gameObjectLayer == 11)
            list[index].effector?.EffectCreature(creature);
          else if (list[index].gameObjectLayer == 9)
          {
            list[index].effector?.EffectCreature(creature);
          }
          else
          {
            if (list[index].gameObjectLayer == 13)
            {
              int num = (ZComponent) list[index] != (object) creature.colliderGameObject ? 1 : 0;
              this.world.listPool.ReturnList(list);
              return num != 0;
            }
            if (list[index].gameObjectLayer == 15)
            {
              list[index].effector?.EffectCreature(creature);
              if (!ZComponent.IsNull((ZComponent) creature.rider))
                list[index].effector?.EffectCreature(creature.rider);
            }
          }
          ZCreature creature1 = list[index].creature;
          if (!ZComponent.IsNull((ZComponent) creature1))
          {
            if (!((ZComponent) creature1.mount != (object) null) || !((ZComponent) creature != (object) null))
            {
              if (creature.team == creature1.team && (creature.mountable || creature1.mountable) && (creature.canMount || creature1.canMount) && (ZComponent) creature.tower == (object) null && (ZComponent) creature1.tower == (object) null)
              {
                if (!ZComponent.IsNull((ZComponent) creature.rider) || (ZComponent) creature.mount != (object) null || (ZComponent) creature1.rider != (object) null || (ZComponent) creature1.mount != (object) null)
                {
                  this.world.listPool.ReturnList(list);
                  return true;
                }
                if (creature.mountable)
                {
                  if (creature1.isPawn && creature1.radius + 10 >= creature.radius)
                  {
                    this.world.listPool.ReturnList(list);
                    return true;
                  }
                  MyLocation position = creature1.position;
                  creature1.OnMount(creature);
                  creature1.mount = creature;
                  creature.rider = creature1;
                  creature1.RiderMoveToPosition = creature.position + Global.GetMountOffset(creature.transformscale, creature.type);
                  creature1.SetScale(creature.transformscale);
                  creature1.game.CreatureMoveSurroundings(position, creature1.radius);
                }
                else
                {
                  if (creature.isPawn && creature.radius + 10 >= creature1.radius)
                  {
                    this.world.listPool.ReturnList(list);
                    return true;
                  }
                  if (creature.velocity.y > 0 || creature.position.y + creature.radius >= this.Height - 20)
                  {
                    this.world.listPool.ReturnList(list);
                    return false;
                  }
                  if (creature.velocity.y == 0 && !creature.flying)
                  {
                    this.world.listPool.ReturnList(list);
                    return true;
                  }
                  MyLocation position = creature.position;
                  creature.OnMount(creature1);
                  creature.RiderMoveToPosition = creature1.position + Global.GetMountOffset(creature1.transformscale, creature1.type);
                  creature.SetScale(creature1.transformscale);
                  creature.game.CreatureMoveSurroundings(position, creature.radius);
                }
              }
              if ((ZComponent) creature != (object) null && creature1.race != CreatureRace.Effector && !creature1.collider.ghosted && MyLocation.Distance(creature1.position, creature.position) < (creature1.radius > creature.radius ? creature.radius - 3 : creature1.radius - 3))
              {
                if ((ZComponent) creature1 == (object) creature.mount)
                {
                  this.world.listPool.ReturnList(list);
                  return true;
                }
              }
              else
              {
                if (!collideWithThorns && creature1.GetType() == typeof (ZCreatureThorn))
                {
                  this.world.listPool.ReturnList(list);
                  return false;
                }
                if (list[index].gameObjectLayer != 9)
                {
                  this.world.listPool.ReturnList(list);
                  return true;
                }
              }
            }
          }
          else if (list[index].gameObjectLayer == 8)
          {
            this.world.listPool.ReturnList(list);
            return true;
          }
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  public bool TryMount(Creature creature, ZCreature c, int team)
  {
    return !((ZComponent) c.mount != (object) null) && team == c.team && (creature.mountable || c.mountable) && (creature.canMount || c.canMount) && (ZComponent) c.tower == (object) null && !((ZComponent) c.rider != (object) null) && !((ZComponent) c.mount != (object) null) && creature.mountable && (!c.isPawn || c.radius + 10 < creature.radius);
  }

  public bool TryMount(ZCreature creature, ZCreature c, bool justReturn = false)
  {
    if ((ZComponent) c.mount != (object) null || (ZComponent) creature == (object) null || creature.team != c.team || !creature.mountable && !c.mountable || !creature.canMount && !c.canMount || !((ZComponent) creature.tower == (object) null) || !((ZComponent) c.tower == (object) null) || !ZComponent.IsNull((ZComponent) creature.rider) || (ZComponent) creature.mount != (object) null || (ZComponent) c.rider != (object) null || (ZComponent) c.mount != (object) null)
      return false;
    if (creature.mountable)
    {
      if (c.isPawn && c.radius + 10 >= creature.radius)
        return false;
      if (justReturn)
        return true;
      MyLocation position = c.position;
      c.OnMount(creature);
      c.mount = creature;
      creature.rider = c;
      c.RiderMoveToPosition = creature.position + Global.GetMountOffset(creature.transformscale, creature.type);
      c.SetScale(creature.transformscale);
      c.game.CreatureMoveSurroundings(position, c.radius);
    }
    else
    {
      if (creature.isPawn && creature.radius + 10 >= c.radius || creature.velocity.y > 0 || creature.position.y + creature.radius >= this.Height - 20 || creature.velocity.y == 0 && !creature.flying)
        return false;
      if (justReturn)
        return true;
      MyLocation position = creature.position;
      creature.OnMount(c);
      creature.RiderMoveToPosition = c.position + Global.GetMountOffset(c.transformscale, c.type);
      creature.SetScale(c.transformscale);
      creature.game.CreatureMoveSurroundings(position, creature.radius);
    }
    return false;
  }

  public bool SpellCheckEffectors(ZCreature creature, ZSpell spell, int x, int y)
  {
    if ((ZComponent) spell == (object) null)
      return false;
    List<ZMyCollider> list = this.world.OverlapCircleAll(new Point(x, y), spell.radius, spell.collider, 3072);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (list[index].gameObjectLayer == 10)
        {
          if (spell.spellEnum != SpellEnum.Flame_Wall)
          {
            ZEffector effector = list[index].effector;
            if (effector.type != EffectorType.Fire_Shield || !((ZComponent) creature == (object) effector.whoSummoned))
            {
              effector.EffectSpell(spell);
              if (effector.type == EffectorType.Static_Ball)
              {
                this.world.listPool.ReturnList(list);
                return true;
              }
            }
          }
        }
        else if (list[index].gameObjectLayer == 11 && spell.spellEnum != SpellEnum.Flame_Wall)
        {
          ZEffector effector = list[index].effector;
          if (effector.type != EffectorType.Fire_Shield || !((ZComponent) creature == (object) effector.whoSummoned))
            effector.EffectSpell(spell);
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  public bool CreatureCheckEffectors(ZCreature creature, int x, int y)
  {
    if ((ZComponent) creature == (object) null)
      return false;
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, 2560);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (list[index].gameObjectLayer == 9)
          list[index].effector.EffectCreature(creature);
        else if (list[index].gameObjectLayer == 11)
          list[index].effector.EffectEntity((ZEntity) creature);
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  private bool SpellPhysicsCollidePoint(ZCreature creature, int x, int y, int mask)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((!list[index].ghosted || !list[index].OverlapGhosting(x, y)) && ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject))
        {
          this.world.listPool.ReturnList(list);
          return true;
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  private bool LeafPhysicsCollidePoint(ZCreature creature, int x, int y, int mask)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((!list[index].ghosted || !list[index].OverlapGhosting(x, y)) && (list[index].layer != 512 || !((ZComponent) list[index].effector == (object) null) && list[index].effector.type == EffectorType.AutumnLeaves) && ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject))
        {
          this.world.listPool.ReturnList(list);
          return true;
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  private bool SpellPhysicsCollidePointIgnoreSelf(ZMyCollider ignore, int x, int y, int mask)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((!list[index].ghosted || !list[index].OverlapGhosting(x, y)) && (ZComponent) list[index] != (object) ignore)
        {
          this.world.listPool.ReturnList(list);
          return true;
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  private bool SpellPhysicsCollidePointUndeadOnly(
    ZCreature creature,
    ZSpell spell,
    int x,
    int y,
    int mask)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, mask);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((!list[index].ghosted || !list[index].OverlapGhosting(x, y)) && ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject))
        {
          ZCreature creature1 = list[index].creature;
          if ((ZComponent) creature1 == (object) null || (ZComponent) creature1.tower != (object) null)
          {
            this.world.listPool.ReturnList(list);
            return true;
          }
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return false;
  }

  public ZCreature PhysicsCollideCreatureCircle(
    ZCreature creature,
    int x,
    int y,
    int radius,
    int maskExtra = 0)
  {
    List<ZMyCollider> list = this.world.OverlapCircleAll(new Point(x, y), radius, creature?.collider, Inert.mask_movement_NoEffector | maskExtra);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject)
        {
          if (list[index].gameObjectLayer == 8 || list[index].gameObjectLayer == 16)
          {
            ZCreature creature1 = list[index].creature;
            if (!ZComponent.IsNull((ZComponent) creature1))
            {
              this.world.listPool.ReturnList(list);
              return creature1;
            }
          }
          else if (list[index].gameObjectLayer == 13)
          {
            ZTower tower = list[index].tower;
            if (!ZComponent.IsNull((ZComponent) tower))
            {
              this.world.listPool.ReturnList(list);
              return tower.creature;
            }
          }
          this.world.listPool.ReturnList(list);
          return (ZCreature) null;
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return (ZCreature) null;
  }

  public ZCreature PhysicsCollideCreature(ZCreature creature, int x, int y, int maskExtra = 0)
  {
    List<ZMyCollider> list = this.world.OverlapPointAll(x, y, Inert.mask_movement_NoEffector | maskExtra);
    if (list.Count > 0)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((ZComponent) creature == (object) null || (ZComponent) list[index] != (object) creature.colliderGameObject)
        {
          if (list[index].gameObjectLayer == 8 || list[index].gameObjectLayer == 16)
          {
            ZCreature creature1 = list[index].creature;
            if (!ZComponent.IsNull((ZComponent) creature1))
            {
              this.world.listPool.ReturnList(list);
              return creature1;
            }
          }
          else if (list[index].gameObjectLayer == 13)
          {
            ZTower tower = list[index].tower;
            if (!ZComponent.IsNull((ZComponent) tower))
            {
              this.world.listPool.ReturnList(list);
              return tower.creature;
            }
          }
          this.world.listPool.ReturnList(list);
          return (ZCreature) null;
        }
      }
    }
    this.world.listPool.ReturnList(list);
    return (ZCreature) null;
  }

  public bool CheckTexutureOnlyMap(int x0, int y0, Texture2D tex)
  {
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int index = 0;
    int num1 = x0 - tex.width / 2;
    int num2 = y0 - tex.height / 2;
    int num3 = num1 + tex.width;
    int num4 = num2 + tex.height;
    if (num1 < 0 || num2 < 0 || num3 > this.Width || num4 > this.Height)
      return false;
    for (int y = num2; y < num4; ++y)
    {
      for (int x = num1; x < num3; ++x)
      {
        if (pixels32[index].a != (byte) 0 && !this.CheckPositionOnlyMap(x, y))
          return false;
        ++index;
      }
    }
    return true;
  }

  public bool CheckTexuture(
    int x0,
    int y0,
    Texture2D tex,
    bool includeTerrain,
    ZCreature ceature,
    bool collideWithThorn = true,
    bool allowOutOfBounds = false)
  {
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int index = 0;
    int num1 = x0 - tex.width / 2;
    int num2 = y0 - tex.height / 2;
    int num3 = num1 + tex.width;
    int num4 = num2 + tex.height;
    if (!allowOutOfBounds)
    {
      if (num1 < 0 || num2 < 0 || num3 > this.Width || num4 > this.Height)
        return false;
    }
    else if (num2 < 0 || num4 > this.Height)
      return false;
    if (includeTerrain)
    {
      for (int y = num2; y < num4; ++y)
      {
        for (int x = num1; x < num3; ++x)
        {
          if (pixels32[index].a != (byte) 0 && this.PixelNotAlpha(x, y, ceature, Inert.mask_movement_NoEffector, collideWithThorn))
            return false;
          ++index;
        }
      }
    }
    else
    {
      for (int y = num2; y < num4; ++y)
      {
        for (int x = num1; x < num3; ++x)
        {
          if (pixels32[index].a != (byte) 0 && this.PhysicsCollidePoint(ceature, x, y, Inert.mask_movement_NoEffector, collideWithThorn))
            return false;
          ++index;
        }
      }
    }
    return true;
  }

  public bool CheckTexutureButterflyJar(
    int x0,
    int y0,
    Texture2D tex,
    bool includeTerrain,
    ZCreature ceature,
    bool collideWithThorn = true,
    bool allowOutOfBounds = false)
  {
    Color32[] pixels32 = ZMap.GetPixels32(tex);
    int index = 0;
    int num1 = 0;
    int num2 = 0;
    int num3 = x0 - tex.width / 2;
    int num4 = y0 - tex.height / 2;
    int num5 = num3 + tex.width;
    int num6 = num4 + tex.height;
    if (!allowOutOfBounds)
    {
      if (num3 < 0 || num4 < 0 || num5 > this.Width || num6 > this.Height)
        return false;
    }
    else if (num4 < 0 || num6 > this.Height)
      return false;
    if (includeTerrain)
    {
      int y = num4;
      while (y < num6)
      {
        int x = num3;
        while (x < num5)
        {
          if (pixels32[index].a != (byte) 0 && (num2 < 4 || num2 > 25 || num1 < 5 || num1 > 25) && this.PixelNotAlpha(x, y, ceature, Inert.mask_movement_NoEffector, collideWithThorn))
            return false;
          ++index;
          ++x;
          ++num1;
        }
        ++y;
        ++num2;
      }
    }
    else
    {
      int y = num4;
      while (y < num6)
      {
        int x = num3;
        while (x < num5)
        {
          if (pixels32[index].a != (byte) 0 && (num2 < 4 || num2 > 25 || num1 < 5 || num1 > 25) && this.PhysicsCollidePoint(ceature, x, y, Inert.mask_movement_NoEffector, collideWithThorn))
            return false;
          ++index;
          ++x;
          ++num1;
        }
        ++y;
        ++num2;
      }
    }
    return true;
  }

  public bool CheckCircleAllowOutOfBounds(
    int x0,
    int y0,
    int radius,
    ZCreature creature,
    int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x1 = x0 - num1;
      int y1 = y0 + num2;
      int num6 = x0 + num1;
      if (y1 >= 0 && y1 < this.Height)
      {
        for (; x1 <= num6; ++x1)
        {
          if (x1 >= 0 && x1 < this.Width && this.PixelNotAlpha(x1, y1, creature, mask))
            return false;
        }
      }
      int x2 = x0 - num2;
      int y2 = y0 + num1;
      int num7 = x0 + num2;
      if (y2 >= 0 && y2 < this.Height)
      {
        for (; x2 <= num7; ++x2)
        {
          if (x2 >= 0 && x2 < this.Width && this.PixelNotAlpha(x2, y2, creature, mask))
            return false;
        }
      }
      int x3 = x0 - num1;
      int y3 = y0 - num2;
      int num8 = x0 + num1;
      if (y3 >= 0 && y3 < this.Height)
      {
        for (; x3 <= num8; ++x3)
        {
          if (x3 >= 0 && x3 < this.Width && this.PixelNotAlpha(x3, y3, creature, mask))
            return false;
        }
      }
      int x4 = x0 - num2;
      int y4 = y0 - num1;
      int num9 = x0 + num2;
      if (y4 >= 0 && y4 < this.Height)
      {
        for (; x4 <= num9; ++x4)
        {
          if (x4 >= 0 && x4 < this.Width && this.PixelNotAlpha(x4, y4, creature, mask))
            return false;
        }
      }
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return true;
  }

  public bool CheckCircle(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x1 = x0 - num1;
      int y1 = y0 + num2;
      int num6 = x0 + num1;
      if (y1 < 0 || y1 >= this.Height)
        return false;
      for (; x1 <= num6; ++x1)
      {
        if (x1 < 0 || x1 >= this.Width || this.PixelNotAlpha(x1, y1, creature, mask))
          return false;
      }
      int x2 = x0 - num2;
      int y2 = y0 + num1;
      int num7 = x0 + num2;
      if (y2 < 0 || y2 >= this.Height)
        return false;
      for (; x2 <= num7; ++x2)
      {
        if (x2 < 0 || x2 >= this.Width || this.PixelNotAlpha(x2, y2, creature, mask))
          return false;
      }
      int x3 = x0 - num1;
      int y3 = y0 - num2;
      int num8 = x0 + num1;
      if (y3 < 0 || y3 >= this.Height)
        return false;
      for (; x3 <= num8; ++x3)
      {
        if (x3 < 0 || x3 >= this.Width || this.PixelNotAlpha(x3, y3, creature, mask))
          return false;
      }
      int x4 = x0 - num2;
      int y4 = y0 - num1;
      int num9 = x0 + num2;
      if (y4 < 0 || y4 >= this.Height)
        return false;
      for (; x4 <= num9; ++x4)
      {
        if (x4 < 0 || x4 >= this.Width || this.PixelNotAlpha(x4, y4, creature, mask))
          return false;
      }
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return true;
  }

  public void CallOfTheVoid(int x0, int y0, int radius)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x1 = x0 - num1;
      int y1 = y0 + num2;
      int num6 = x0 + num1;
      if (y1 >= 0 && y1 < this.Height)
      {
        for (; x1 <= num6; ++x1)
        {
          if (x1 >= 0 && x1 < this.Width && this.PixelNotTransparent(x1, y1))
            this.UpdatePixel(x1, y1, RotateImage.CallOfTheVoid(this.GetPixel(x1, y1)));
        }
      }
      int x2 = x0 - num2;
      int y2 = y0 + num1;
      int num7 = x0 + num2;
      if (y2 >= 0 && y2 < this.Height)
      {
        for (; x2 <= num7; ++x2)
        {
          if (x2 >= 0 && x2 < this.Width && this.PixelNotTransparent(x2, y2))
            this.UpdatePixel(x2, y2, RotateImage.CallOfTheVoid(this.GetPixel(x2, y2)));
        }
      }
      int x3 = x0 - num1;
      int y3 = y0 - num2;
      int num8 = x0 + num1;
      if (y3 >= 0 && y3 < this.Height)
      {
        for (; x3 <= num8; ++x3)
        {
          if (x3 >= 0 && x3 < this.Width && this.PixelNotTransparent(x3, y3))
            this.UpdatePixel(x3, y3, RotateImage.CallOfTheVoid(this.GetPixel(x3, y3)));
        }
      }
      int x4 = x0 - num2;
      int y4 = y0 - num1;
      int num9 = x0 + num2;
      if (y4 >= 0 && y4 < this.Height)
      {
        for (; x4 <= num9; ++x4)
        {
          if (x4 >= 0 && x4 < this.Width && this.PixelNotTransparent(x4, y4))
            this.UpdatePixel(x4, y4, RotateImage.CallOfTheVoid(this.GetPixel(x4, y4)));
        }
      }
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    this.Apply();
  }

  public Coords CheckTopRight(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 + num2;
      int y = y0 + num1;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckRightTop(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 + num1;
      int y = y0 + num2;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckRightBottom(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 + num1;
      int y = y0 - num2;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckBottomRight(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 + num2;
      int y = y0 - num1;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckRightBottom_FromTop(
    int x0,
    int y0,
    int radius,
    ZCreature creature,
    int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 + num1;
      int y = y0 - num2;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        --num1;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        ++num2;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckBottomLeft(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 - num2;
      int y = y0 - num1;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckBottomLeft_FromTop(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = 0;
    int num2 = radius - 1;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num2 >= num1)
    {
      int x = x0 - num2;
      int y = y0 - num1;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num1;
        num5 += num3;
        num3 += 2;
      }
      if (num5 > 0)
      {
        --num2;
        num4 += 2;
        num5 += (-radius << 1) + num4;
      }
    }
    return (Coords) null;
  }

  public Coords CheckLeftBottom(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 - num1;
      int y = y0 - num2;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckLeftTop(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 - num1;
      int y = y0 + num2;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords CheckTopLeft(int x0, int y0, int radius, ZCreature creature, int mask)
  {
    int num1 = radius - 1;
    int num2 = 0;
    int num3 = 1;
    int num4 = 1;
    int num5 = num3 - (radius << 1);
    while (num1 >= num2)
    {
      int x = x0 - num2;
      int y = y0 + num1;
      if (x >= this.Width || x < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      if (this.PixelNotAlpha(x, y, creature, mask))
        return new Coords(x, y);
      if (num5 <= 0)
      {
        ++num2;
        num5 += num4;
        num4 += 2;
      }
      if (num5 > 0)
      {
        --num1;
        num3 += 2;
        num5 += (-radius << 1) + num3;
      }
    }
    return (Coords) null;
  }

  public Coords downCastLightning(int x, int y, ZCreature c, ZSpell spell, int mask)
  {
    for (; y >= 0; --y)
    {
      if (x >= 0 && x < this.Width && y >= 0 && y < this.Height)
      {
        if (!this.CheckClampedPixel(x, y))
          return new Coords(x, y);
        ZCreature creatureAtPoint = this.FindCreatureAtPoint((ZCreature) null, x, y, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
        {
          spell.position = new MyLocation(x, y);
          spell.Damage(creatureAtPoint);
          if ((ZComponent) creatureAtPoint.tower == (object) null)
          {
            creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(x, y), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
            creatureAtPoint.StartMoving(false);
          }
          return new Coords(x, y);
        }
      }
    }
    return (Coords) null;
  }

  public ZCreature bresenhamsLineCastOnlyCreatures(
    Coords start,
    Coords end,
    ZCreature c,
    int mask)
  {
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      if (num7 == end.y)
      {
        int num16 = num8;
        int y1 = end.y;
        int x = num10;
        if (num16 >= this.Height || num16 < 0 || x < 0 || x >= this.Width)
          return (ZCreature) null;
        for (int y2 = num16; y2 > y1 && y2 >= 0; --y2)
        {
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, x, y2, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
            return creatureAtPoint;
          num14 -= num12;
          if (num14 < 0)
          {
            x -= num13;
            if (x >= 0 && x < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
      else
      {
        if (num7 >= this.Height || num7 < 0 || num15 < 0 || num15 >= this.Width)
          return (ZCreature) null;
        for (int y = num7; y <= num8 && y < this.Height; ++y)
        {
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, num15, y, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
            return creatureAtPoint;
          num14 -= num12;
          if (num14 < 0)
          {
            num15 += num13;
            if (num15 >= 0 && num15 < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
    }
    else if (num7 == end.x)
    {
      int num17 = num8;
      int x1 = end.x;
      int y = num10;
      if (num17 >= this.Width || num17 < 0 || y < 0 || y >= this.Height)
        return (ZCreature) null;
      for (int x2 = num17; x2 > x1 && x2 >= 0; --x2)
      {
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, x2, y, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
          return creatureAtPoint;
        num14 -= num12;
        if (num14 < 0)
        {
          y -= num13;
          if (y >= 0 && y < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    else
    {
      if (num7 >= this.Width || num7 < 0 || num15 < 0 || num15 >= this.Height)
        return (ZCreature) null;
      for (int x = num7; x <= num8 && x < this.Width; ++x)
      {
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, x, num15, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
          return creatureAtPoint;
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          if (num15 >= 0 && num15 < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    return (ZCreature) null;
  }

  public Coords bresenhamsLineCast(Coords start, Coords end, ZCreature c, ZSpell spell, int mask)
  {
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      if (num7 == end.y)
      {
        int num16 = num8;
        int y = end.y;
        int num17 = num10;
        if (num16 >= this.Height || num16 < 0 || num17 < 0 || num17 >= this.Width)
          return (Coords) null;
        for (int index = num16; index > y && index >= 0; --index)
        {
          if (!this.CheckClampedPixel(num17, index))
          {
            if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              spell.ApplyExplosionForce(new MyLocation(num17, index));
            return new Coords(num17, index);
          }
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, num17, index, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
          {
            if ((ZComponent) spell != (object) null)
            {
              spell.position = new MyLocation(num17, index);
              if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              {
                spell.ApplyExplosionForce(spell.position);
              }
              else
              {
                spell.Damage(creatureAtPoint);
                if ((ZComponent) creatureAtPoint.tower == (object) null)
                {
                  creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(num17, index), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                  creatureAtPoint.StartMoving(false);
                }
              }
            }
            return new Coords(num17, index);
          }
          num14 -= num12;
          if (num14 < 0)
          {
            num17 -= num13;
            if (num17 >= 0 && num17 < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
      else
      {
        if (num7 >= this.Height || num7 < 0 || num15 < 0 || num15 >= this.Width)
          return (Coords) null;
        for (int index = num7; index <= num8 && index < this.Height; ++index)
        {
          if (!this.CheckClampedPixel(num15, index))
          {
            if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              spell.ApplyExplosionForce(new MyLocation(num15, index));
            return new Coords(num15, index);
          }
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, num15, index, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
          {
            if ((ZComponent) spell != (object) null)
            {
              spell.position = new MyLocation(num15, index);
              if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              {
                spell.ApplyExplosionForce(spell.position);
              }
              else
              {
                spell.Damage(creatureAtPoint);
                if ((ZComponent) creatureAtPoint.tower == (object) null)
                {
                  creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(num15, index), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                  creatureAtPoint.StartMoving(false);
                }
              }
            }
            return new Coords(num15, index);
          }
          num14 -= num12;
          if (num14 < 0)
          {
            num15 += num13;
            if (num15 >= 0 && num15 < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
    }
    else if (num7 == end.x)
    {
      int num18 = num8;
      int x = end.x;
      int num19 = num10;
      if (num18 >= this.Width || num18 < 0 || num19 < 0 || num19 >= this.Height)
        return (Coords) null;
      for (int index = num18; index > x && index >= 0; --index)
      {
        if (!this.CheckClampedPixel(index, num19))
        {
          if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            spell.ApplyExplosionForce(new MyLocation(index, num19));
          return new Coords(index, num19);
        }
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, index, num19, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
        {
          if ((ZComponent) spell != (object) null)
          {
            spell.position = new MyLocation(index, num19);
            if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            {
              spell.ApplyExplosionForce(spell.position);
            }
            else
            {
              spell.Damage(creatureAtPoint);
              if ((ZComponent) creatureAtPoint.tower == (object) null)
              {
                creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(index, num19), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                creatureAtPoint.StartMoving(false);
              }
            }
          }
          return new Coords(index, num19);
        }
        num14 -= num12;
        if (num14 < 0)
        {
          num19 -= num13;
          if (num19 >= 0 && num19 < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    else
    {
      if (num7 >= this.Width || num7 < 0 || num15 < 0 || num15 >= this.Height)
        return (Coords) null;
      for (int index = num7; index <= num8 && index < this.Width; ++index)
      {
        if (!this.CheckClampedPixel(index, num15))
        {
          if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            spell.ApplyExplosionForce(new MyLocation(index, num15));
          return new Coords(index, num15);
        }
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, index, num15, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
        {
          if ((ZComponent) spell != (object) null)
          {
            spell.position = new MyLocation(index, num15);
            if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            {
              spell.ApplyExplosionForce(spell.position);
            }
            else
            {
              spell.Damage(creatureAtPoint);
              if ((ZComponent) creatureAtPoint.tower == (object) null)
              {
                creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(index, num15), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                creatureAtPoint.StartMoving(false);
              }
            }
          }
          return new Coords(index, num15);
        }
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          if (num15 >= 0 && num15 < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    return (Coords) null;
  }

  public Coords bresenhamsCircleCast(Coords start, Coords end, ZCreature c, int mask, int radius)
  {
    List<Coords> outlineArray = MapGenerator.getOutlineArray(radius);
    int num1 = ((FixedInt.Create(360) - (Inert.AngleOfVelocity(new MyLocation(end.x, end.y) - new MyLocation(start.x, start.y)) - FixedInt.Create(90))) * FixedInt.ThreeSixtyBy1 * outlineArray.Count - radius).ToInt();
    if (num1 < 0)
    {
      int num2 = num1 + outlineArray.Count;
    }
    int num3 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num4 = num3 != 0 ? start.y : start.x;
    int num5 = num3 != 0 ? start.x : start.y;
    int num6 = num3 != 0 ? end.y : end.x;
    int num7 = num3 != 0 ? end.x : end.y;
    int num8 = num4 > num6 ? 1 : 0;
    int num9 = num8 != 0 ? num6 : num4;
    int num10 = num8 != 0 ? num4 : num6;
    int num11 = num8 != 0 ? num7 : num5;
    int num12 = num8 != 0 ? num5 : num7;
    int num13 = num10 - num9;
    int num14 = Mathf.Abs(num12 - num11);
    int num15 = num11 < num12 ? 1 : -1;
    int num16 = num13 / 2;
    int num17 = num11;
    if (num3 != 0)
    {
      if (num9 == end.y)
      {
        int num18 = num10;
        int y = end.y;
        int num19 = num12;
        for (int index1 = num18; index1 > y && index1 >= 0; --index1)
        {
          for (int index2 = 0; index2 < radius * 2; ++index2)
          {
            if (!this.CheckPositionOnlyMap(num19 + outlineArray[index2].x, index1 + outlineArray[index2].y))
              return new Coords(num19 + outlineArray[index2].x, index1 + outlineArray[index2].y);
            if ((ZComponent) this.FindCreatureAtPoint(c, num19 + outlineArray[index2].x, index1 + outlineArray[index2].y, mask) != (object) null)
              return new Coords(num19 + outlineArray[index2].x, index1 + outlineArray[index2].y);
          }
          num16 -= num14;
          if (num16 < 0)
          {
            num19 -= num15;
            if (num19 >= 0 && num19 < this.Width)
              num16 += num13;
            else
              break;
          }
        }
      }
      else
      {
        for (int index3 = num9; index3 <= num10 && index3 < this.Height; ++index3)
        {
          for (int index4 = 0; index4 < radius * 2; ++index4)
          {
            if (!this.CheckPositionOnlyMap(num17 + outlineArray[index4].x, index3 + outlineArray[index4].y))
              return new Coords(num17 + outlineArray[index4].x, index3 + outlineArray[index4].y);
            if ((ZComponent) this.FindCreatureAtPoint(c, num17 + outlineArray[index4].x, index3 + outlineArray[index4].y, mask) != (object) null)
              return new Coords(num17 + outlineArray[index4].x, index3 + outlineArray[index4].y);
          }
          num16 -= num14;
          if (num16 < 0)
          {
            num17 += num15;
            if (num17 >= 0 && num17 < this.Width)
              num16 += num13;
            else
              break;
          }
        }
      }
    }
    else if (num9 == end.x)
    {
      int num20 = num10;
      int x = end.x;
      int num21 = num12;
      for (int index5 = num20; index5 > x && index5 >= 0; --index5)
      {
        for (int index6 = 0; index6 < radius * 2; ++index6)
        {
          if (!this.CheckPositionOnlyMap(index5 + outlineArray[index6].x, num21 + outlineArray[index6].y))
            return new Coords(index5 + outlineArray[index6].x, num21 + outlineArray[index6].y);
          if ((ZComponent) this.FindCreatureAtPoint(c, index5 + outlineArray[index6].x, num21 + outlineArray[index6].y, mask) != (object) null)
            return new Coords(index5 + outlineArray[index6].x, num21 + outlineArray[index6].y);
        }
        num16 -= num14;
        if (num16 < 0)
        {
          num21 -= num15;
          if (num21 >= 0 && num21 < this.Height)
            num16 += num13;
          else
            break;
        }
      }
    }
    else
    {
      for (int index7 = num9; index7 <= num10 && index7 < this.Width; ++index7)
      {
        for (int index8 = 0; index8 < radius * 2; ++index8)
        {
          if (!this.CheckPositionOnlyMap(index7 + outlineArray[index8].x, num17 + outlineArray[index8].y))
            return new Coords(index7 + outlineArray[index8].x, num17 + outlineArray[index8].y);
          if ((ZComponent) this.FindCreatureAtPoint(c, index7 + outlineArray[index8].x, num17 + outlineArray[index8].y, mask) != (object) null)
            return new Coords(index7 + outlineArray[index8].x, num17 + outlineArray[index8].y);
        }
        num16 -= num14;
        if (num16 < 0)
        {
          num17 += num15;
          if (num17 >= 0 && num17 < this.Height)
            num16 += num13;
          else
            break;
        }
      }
    }
    return (Coords) null;
  }

  public Coords GetLineByLength(Coords start, Coords end, int count)
  {
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      if (num7 == end.y)
      {
        int num16 = num8;
        int y1 = end.y;
        int x = num10;
        if (num16 >= this.Height || num16 < 0 || x < 0 || x >= this.Width)
          return (Coords) null;
        for (int y2 = num16; y2 > y1 && y2 >= 0; --y2)
        {
          num14 -= num12;
          if (num14 < 0)
          {
            x -= num13;
            if (x >= 0 && x < this.Width)
              num14 += num11;
            else
              break;
          }
          --count;
          if (count <= 0)
            return new Coords(x, y2);
        }
      }
      else
      {
        if (num7 >= this.Height || num7 < 0 || num15 < 0 || num15 >= this.Width)
          return (Coords) null;
        for (int y = num7; y <= num8 && y < this.Height; ++y)
        {
          num14 -= num12;
          if (num14 < 0)
          {
            num15 += num13;
            if (num15 >= 0 && num15 < this.Width)
              num14 += num11;
            else
              break;
          }
          --count;
          if (count <= 0)
            return new Coords(num15, y);
        }
      }
    }
    else if (num7 == end.x)
    {
      int num17 = num8;
      int x1 = end.x;
      int y = num10;
      if (num17 >= this.Width || num17 < 0 || y < 0 || y >= this.Height)
        return (Coords) null;
      for (int x2 = num17; x2 > x1 && x2 >= 0; --x2)
      {
        num14 -= num12;
        if (num14 < 0)
        {
          y -= num13;
          if (y >= 0 && y < this.Height)
            num14 += num11;
          else
            break;
        }
        --count;
        if (count <= 0)
          return new Coords(x2, y);
      }
    }
    else
    {
      if (num7 >= this.Width || num7 < 0 || num15 < 0 || num15 >= this.Height)
        return (Coords) null;
      for (int x = num7; x <= num8 && x < this.Width; ++x)
      {
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          if (num15 >= 0 && num15 < this.Height)
            num14 += num11;
          else
            break;
        }
        --count;
        if (count <= 0)
          return new Coords(x, num15);
      }
    }
    return (Coords) null;
  }

  public Coords bresenhamsLineCastLightningStrike(
    Coords start,
    Coords end,
    ZCreature c,
    ZSpell spell,
    int mask)
  {
    if (start.y >= this.Height)
      start.y = this.Height - 1;
    if (end.y >= this.Height)
      end.y = this.Height - 1;
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      if (num7 == end.y)
      {
        int num16 = num8;
        int y = end.y;
        int num17 = num10;
        if (num16 >= this.Height || num16 < 0 || num17 < 0 || num17 >= this.Width)
          return (Coords) null;
        for (int index = num16; index > y && index >= 0; --index)
        {
          if (!this.CheckClampedPixel(num17, index))
          {
            if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              spell.ApplyExplosionForce(new MyLocation(num17, index));
            return new Coords(num17, index);
          }
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, num17, index, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
          {
            if ((ZComponent) spell != (object) null)
            {
              spell.position = new MyLocation(num17, index);
              if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              {
                spell.ApplyExplosionForce(spell.position);
              }
              else
              {
                spell.Damage(creatureAtPoint);
                if ((ZComponent) creatureAtPoint.tower == (object) null)
                {
                  creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(num17, index), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                  creatureAtPoint.StartMoving(false);
                }
              }
            }
            return new Coords(num17, index);
          }
          num14 -= num12;
          if (num14 < 0)
          {
            num17 -= num13;
            if (num17 >= 0 && num17 < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
      else
      {
        if (num7 >= this.Height || num7 < 0 || num15 < 0 || num15 >= this.Width)
          return (Coords) null;
        for (int index = num7; index <= num8 && index < this.Height; ++index)
        {
          if (!this.CheckClampedPixel(num15, index))
          {
            if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              spell.ApplyExplosionForce(new MyLocation(num15, index));
            return new Coords(num15, index);
          }
          ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, num15, index, mask);
          if ((ZComponent) creatureAtPoint != (object) null)
          {
            if ((ZComponent) spell != (object) null)
            {
              spell.position = new MyLocation(num15, index);
              if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
              {
                spell.ApplyExplosionForce(spell.position);
              }
              else
              {
                spell.Damage(creatureAtPoint);
                if ((ZComponent) creatureAtPoint.tower == (object) null)
                {
                  creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(num15, index), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                  creatureAtPoint.StartMoving(false);
                }
              }
            }
            return new Coords(num15, index);
          }
          num14 -= num12;
          if (num14 < 0)
          {
            num15 += num13;
            if (num15 >= 0 && num15 < this.Width)
              num14 += num11;
            else
              break;
          }
        }
      }
    }
    else if (num7 == end.x)
    {
      int num18 = num8;
      int x = end.x;
      int num19 = num10;
      if (num18 >= this.Width || num18 < 0 || num19 < 0 || num19 >= this.Height)
        return (Coords) null;
      for (int index = num18; index > x && index >= 0; --index)
      {
        if (!this.CheckClampedPixel(index, num19))
        {
          if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            spell.ApplyExplosionForce(new MyLocation(index, num19));
          return new Coords(index, num19);
        }
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, index, num19, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
        {
          if ((ZComponent) spell != (object) null)
          {
            spell.position = new MyLocation(index, num19);
            if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            {
              spell.ApplyExplosionForce(spell.position);
            }
            else
            {
              spell.Damage(creatureAtPoint);
              if ((ZComponent) creatureAtPoint.tower == (object) null)
              {
                creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(index, num19), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                creatureAtPoint.StartMoving(false);
              }
            }
          }
          return new Coords(index, num19);
        }
        num14 -= num12;
        if (num14 < 0)
        {
          num19 -= num13;
          if (num19 >= 0 && num19 < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    else
    {
      if (num7 >= this.Width || num7 < 0 || num15 < 0 || num15 >= this.Height)
        return (Coords) null;
      for (int index = num7; index <= num8 && index < this.Width; ++index)
      {
        if (!this.CheckClampedPixel(index, num15))
        {
          if ((ZComponent) spell != (object) null && spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            spell.ApplyExplosionForce(new MyLocation(index, num15));
          return new Coords(index, num15);
        }
        ZCreature creatureAtPoint = this.FindCreatureAtPoint(c, index, num15, mask);
        if ((ZComponent) creatureAtPoint != (object) null)
        {
          if ((ZComponent) spell != (object) null)
          {
            spell.position = new MyLocation(index, num15);
            if (spell.spellEnum == SpellEnum.Storm_Dragon_Breath)
            {
              spell.ApplyExplosionForce(spell.position);
            }
            else
            {
              spell.Damage(creatureAtPoint);
              if ((ZComponent) creatureAtPoint.tower == (object) null)
              {
                creatureAtPoint.ApplyExplosionForce(spell.radius, new MyLocation(index, num15), spell.explisiveForce, spell.EXORADIUS, spell.forceOverDistance);
                creatureAtPoint.StartMoving(false);
              }
            }
          }
          return new Coords(index, num15);
        }
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          if (num15 >= 0 && num15 < this.Height)
            num14 += num11;
          else
            break;
        }
      }
    }
    return (Coords) null;
  }

  public Coords bresenhamsLineCastOnlyTerrain(Coords start, Coords end)
  {
    int num1 = Mathf.Abs(end.y - start.y) > Mathf.Abs(end.x - start.x) ? 1 : 0;
    int num2 = num1 != 0 ? start.y : start.x;
    int num3 = num1 != 0 ? start.x : start.y;
    int num4 = num1 != 0 ? end.y : end.x;
    int num5 = num1 != 0 ? end.x : end.y;
    int num6 = num2 > num4 ? 1 : 0;
    int num7 = num6 != 0 ? num4 : num2;
    int num8 = num6 != 0 ? num2 : num4;
    int num9 = num6 != 0 ? num5 : num3;
    int num10 = num6 != 0 ? num3 : num5;
    int num11 = num8 - num7;
    int num12 = Mathf.Abs(num10 - num9);
    int num13 = num9 < num10 ? 1 : -1;
    int num14 = num11 / 2;
    int num15 = num9;
    if (num1 != 0)
    {
      if (num7 == end.y)
      {
        int num16 = num8;
        int y1 = end.y;
        int x = num10;
        for (int y2 = num16; y2 > y1; --y2)
        {
          if (y2 < this.Height && y2 >= 0 && x >= 0 && x < this.Width && !this.CheckClampedPixel(x, y2))
            return new Coords(x, y2);
          num14 -= num12;
          if (num14 < 0)
          {
            x -= num13;
            num14 += num11;
          }
        }
      }
      else
      {
        if (num7 >= this.Height || num7 < 0 || num15 < 0 || num15 >= this.Width)
          return (Coords) null;
        for (int y = num7; y <= num8; ++y)
        {
          if (y < this.Height && y >= 0 && num15 >= 0 && num15 < this.Width && !this.CheckClampedPixel(num15, y))
            return new Coords(num15, y);
          num14 -= num12;
          if (num14 < 0)
          {
            num15 += num13;
            num14 += num11;
          }
        }
      }
    }
    else if (num7 == end.x)
    {
      int num17 = num8;
      int x1 = end.x;
      int y = num10;
      for (int x2 = num17; x2 > x1; --x2)
      {
        if (x2 < this.Width && x2 >= 0 && y >= 0 && y < this.Height && !this.CheckClampedPixel(x2, y))
          return new Coords(x2, y);
        num14 -= num12;
        if (num14 < 0)
        {
          y -= num13;
          num14 += num11;
        }
      }
    }
    else
    {
      for (int x = num7; x <= num8; ++x)
      {
        if (x < this.Width && x >= 0 && num15 >= 0 && num15 < this.Height && !this.CheckClampedPixel(x, num15))
          return new Coords(x, num15);
        num14 -= num12;
        if (num14 < 0)
        {
          num15 += num13;
          num14 += num11;
        }
      }
    }
    return (Coords) null;
  }

  public class GhostTerrain
  {
    public ZMap.GhostTerrain next;
    public int x;
    public int y;
    public int radius2;
  }

  public class RawSprite
  {
    public Sprite sprite;
    public SpriteRenderer sr;
    public Color32[] colors;
    public bool requireUpdate = true;

    public RawSprite(Sprite s, SpriteRenderer sr, Color32[] c)
    {
      this.sr = sr;
      this.sprite = s;
      this.colors = c;
    }

    public void SetPixel(int i, Color32 c)
    {
      this.colors[i] = c;
      this.requireUpdate = true;
    }

    public Color32 GetPixel(int i) => this.colors[i];

    public void Apply()
    {
      if (!this.requireUpdate)
        return;
      this.requireUpdate = false;
      this.sprite.texture.SetPixels32(this.colors);
      this.sprite.texture.Apply(true);
    }

    public void Purpalize()
    {
      for (int index = 0; index < this.colors.Length; ++index)
      {
        if (this.colors[index].a == byte.MaxValue)
          this.colors[index] = RotateImage.Purpalize(this.colors[index]);
      }
      this.sprite.texture.SetPixels32(this.colors);
      this.sprite.texture.Apply(true);
    }
  }
}
