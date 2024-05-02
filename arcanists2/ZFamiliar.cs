// Decompiled with JetBrains decompiler
// Type: ZFamiliar
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ZFamiliar : ZComponent
{
  public Familiar baseFamiliar;
  public Familiar clientObj;
  public ZCreature creature;
  public ZSpellSoulJar soulJar;
  public ZEffector effector;
  public BookOf bookOf;

  public MyLocation position
  {
    get
    {
      if ((ZComponent) this.creature == (object) null)
      {
        Debug.LogError((object) ("Creature is null in Familiar " + (object) this.bookOf));
        return MyLocation.zero;
      }
      return (ZComponent) this.soulJar != (object) null ? this.soulJar.position : new MyLocation(0, 0);
    }
  }

  public void Serialize(myBinaryWriter writer)
  {
    writer.Write(this.baseFamiliar.name);
    writer.Write(this.position);
    writer.Write((ZComponent) this.effector != (object) null ? this.effector.variable : 0);
  }

  public static ZFamiliar Deserialize(myBinaryReader reader, ZCreature cre)
  {
    string n = reader.ReadString();
    MyLocation pos = reader.ReadMyLocation();
    int num = reader.ReadInt32();
    if ((ZComponent) cre == (object) null)
      return (ZFamiliar) null;
    Familiar component = ((IEnumerable<GameObject>) Inert.Instance.familiars).First<GameObject>((Func<GameObject, bool>) (q => string.Equals(n, q.name))).GetComponent<Familiar>();
    ZFamiliar zfamiliar = ZFamiliar.Create(cre, component);
    if ((ZComponent) zfamiliar.effector != (object) null)
    {
      zfamiliar.effector.position = pos;
      zfamiliar.effector.variable = num;
    }
    if ((ZComponent) zfamiliar.soulJar != (object) null)
    {
      zfamiliar.soulJar.position = pos;
      zfamiliar.soulJar.collider.Move(pos);
    }
    zfamiliar.clientObj.transform.position = (Vector3) pos.ToSinglePrecision();
    if (cre.parent.familiars == null)
      cre.parent.familiars = new List<ZFamiliar>();
    cre.parent.familiars.Add(zfamiliar);
    return zfamiliar;
  }

  public static ZFamiliar Create(ZCreature cre, Familiar f)
  {
    ZFamiliar zfamiliar = new ZFamiliar();
    zfamiliar.Copy(f);
    zfamiliar.creature = cre;
    zfamiliar.clientObj = UnityEngine.Object.Instantiate<Familiar>(f, (Vector3) cre.position.ToSinglePrecision(), Quaternion.identity, cre.game.GetMapTransform());
    zfamiliar.gameObject = zfamiliar.clientObj.gameObject;
    zfamiliar.transform = zfamiliar.clientObj.transform;
    zfamiliar.clientObj.serverObj = zfamiliar;
    if (f.bookOf == BookOf.Seasons && cre.game.isClient)
      zfamiliar.clientObj.GetComponent<DryadTextureChange>().creature = cre.clientObj;
    if (f.bookOf == BookOf.Underdark)
    {
      zfamiliar.soulJar = new ZSpellSoulJar();
      zfamiliar.soulJar.game = cre.game;
      zfamiliar.soulJar.Copy((Creature) f.GetComponent<SpellSoulJar>());
      zfamiliar.effector = zfamiliar.soulJar.auraOfDecay;
      zfamiliar.effector.collider = zfamiliar.soulJar.collider;
      zfamiliar.soulJar.auraOfDecay = (ZEffector) null;
      zfamiliar.soulJar.gameObject = zfamiliar.gameObject;
      zfamiliar.soulJar.transform = zfamiliar.transform;
      zfamiliar.soulJar.clientObj = (Creature) zfamiliar.clientObj.GetComponent<SpellSoulJar>();
    }
    return zfamiliar;
  }

  public static void CreateMinionMaster(ZPerson x)
  {
    if (!x.game.isClient)
      return;
    ZFamiliar zfamiliar = ZFamiliar.Create(x.controlled[0], ClientResources.Instance.minionMasterFamiliar.GetComponent<Familiar>());
    if (x.familiars == null)
      x.familiars = new List<ZFamiliar>();
    x.familiars.Add(zfamiliar);
  }

  public static void CreateBombMaster(ZPerson x)
  {
    if (!x.game.isClient)
      return;
    ZFamiliar zfamiliar = ZFamiliar.Create(x.controlled[0], ClientResources.Instance.bombMasterFamiliar.GetComponent<Familiar>());
    if (x.familiars == null)
      x.familiars = new List<ZFamiliar>();
    x.familiars.Add(zfamiliar);
    SpriteRenderer component = zfamiliar.clientObj.GetComponent<SpriteRenderer>();
    component.sprite = ConfigurePlayer.ApplyOutfit(component.sprite, x.settingsPlayer);
  }

  public void Copy(Familiar e)
  {
    this.baseFamiliar = e;
    this.bookOf = e.bookOf;
  }
}
