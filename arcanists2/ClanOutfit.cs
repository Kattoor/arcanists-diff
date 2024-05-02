

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ClanOutfit
{
  private const byte Version = 1;
  public ClanOutfit.Meta[] outfits;

  public void Dispose()
  {
    if (this.outfits == null)
      return;
    for (int index = 0; index < this.outfits.Length; ++index)
    {
      if (this.outfits[index] != null && !((UnityEngine.Object) this.outfits[index].clientTexture == (UnityEngine.Object) null))
      {
        Global.DestroySprite(this.outfits[index].clientTexture);
        this.outfits[index].clientTexture = (Sprite) null;
      }
    }
  }

  public void ClientCreateSprites()
  {
    if (this.outfits == null)
      return;
    for (int index = 0; index < this.outfits.Length; ++index)
    {
      if (this.outfits[index] != null && this.outfits[index].png != null)
      {
        Texture2D texture2D = new Texture2D(2, 2);
        if (texture2D.LoadImage(this.outfits[index].png))
          this.outfits[index].clientTexture = Global.AddSprite(Sprite.Create(texture2D, new Rect(0.0f, 0.0f, (float) texture2D.width, (float) texture2D.height), (Vector2) this.outfits[index].pivot, 2f));
      }
    }
  }

  public bool OutfitNoMouth(int index)
  {
    return this.outfits != null && this.outfits.Length > 1 && (int) SettingsPlayer.clanOutfit[1] == index && this.outfits[1] != null && this.outfits[1].effect == (byte) 1;
  }

  public Sprite GetSprite(int index, Outfit o)
  {
    if (this.outfits == null || (Outfit) this.outfits.Length <= o || (int) SettingsPlayer.clanOutfit[(int) o] != index)
      return (Sprite) null;
    return this.outfits[(int) o]?.GetClientTexture();
  }

  public void Serialize(myBinaryWriter w)
  {
    w.Write((byte) 1);
    if (this.outfits != null)
    {
      w.Write((byte) 1);
      w.Write(this.outfits.Length);
      for (int index = 0; index < this.outfits.Length; ++index)
      {
        ClanOutfit.Meta outfit = this.outfits[index];
        if (outfit == null || outfit.png == null)
        {
          w.Write((byte) 0);
        }
        else
        {
          w.Write((byte) 1);
          w.Write(outfit.effect);
          w.Write((Vector2) outfit.pivot);
          w.Write(outfit.png);
        }
      }
    }
    else
      w.Write((byte) 0);
  }

  public static ClanOutfit Deserialize(myBinaryReader r)
  {
    ClanOutfit clanOutfit = new ClanOutfit();
    clanOutfit._Deserialize(r);
    return clanOutfit;
  }

  private void _Deserialize(myBinaryReader r)
  {
    int num = (int) r.ReadByte();
    if (r.ReadByte() != (byte) 1)
      return;
    int length = r.ReadInt32();
    this.outfits = new ClanOutfit.Meta[length];
    for (int index = 0; index < length; ++index)
    {
      if (r.ReadByte() == (byte) 1)
        this.outfits[index] = new ClanOutfit.Meta()
        {
          effect = r.ReadByte(),
          pivot = (MyVector2) r.ReadVector2(),
          png = r.ReadBytes()
        };
    }
  }

  [Serializable]
  public class Meta
  {
    public byte[] png;
    public byte effect;
    public MyVector2 pivot = MyVector2.zero;
    [NonSerialized]
    public Sprite clientTexture;

    public Sprite GetClientTexture()
    {
      if ((UnityEngine.Object) this.clientTexture == (UnityEngine.Object) null)
      {
        if (this.png == null || this.png.Length == 0)
          return (Sprite) null;
        Texture2D texture2D = new Texture2D(2, 2);
        if (texture2D.LoadImage(this.png))
          this.clientTexture = Global.AddSprite(Sprite.Create(texture2D, new Rect(0.0f, 0.0f, (float) texture2D.width, (float) texture2D.height), (Vector2) this.pivot, 2f));
      }
      return this.clientTexture;
    }

    public void Serialize(myBinaryWriter w)
    {
      w.Write(this.effect);
      w.Write((Vector2) this.pivot);
      w.Write(this.png);
    }

    public void Deserialize(myBinaryReader r)
    {
      this.effect = r.ReadByte();
      this.pivot = (MyVector2) r.ReadVector2();
      this.png = r.ReadBytes();
    }
  }
}
