

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class MinerMarket
{
  public bool[] bought;
  public int curHolesDug;
  public int count;
  public GameObject clientObj;

  public bool Has(MinerMarket.Types t) => this.bought[(int) t];

  public void Buy(MinerMarket.Types t)
  {
    ++this.count;
    this.bought[(int) t] = true;
  }

  public bool CanBuy(ZCreature c, MinerMarket.Types t)
  {
    return !this.Has(t) && c.health > Inert.Instance.minerMarket[(int) t].cost;
  }

  public MinerMarket() => this.bought = new bool[16];

  public void Serialize(myBinaryWriter writer)
  {
    writer.Write(this.bought.Length);
    writer.Write(this.curHolesDug);
    writer.Write(this.count);
    for (int index = 0; index < this.bought.Length; ++index)
      writer.Write(this.bought[index]);
  }

  public static void Deserialize(myBinaryReader reader, ZCreature c)
  {
    c.minerMarket = new MinerMarket();
    c.minerMarket.bought = new bool[reader.ReadInt32()];
    c.minerMarket.curHolesDug = reader.ReadInt32();
    c.minerMarket.count = reader.ReadInt32();
    for (int index = 0; index < c.minerMarket.bought.Length; ++index)
      c.minerMarket.bought[index] = reader.ReadBoolean();
    if ((UnityEngine.Object) c.minerMarket.clientObj == (UnityEngine.Object) null)
      c.minerMarket.clientObj = UnityEngine.Object.Instantiate<GameObject>(ClientResources.Instance.pfabMinerMarketIcons, c.transform.localPosition + new Vector3((float) (c.radius + 16), (float) (c.radius + 3)), Quaternion.identity, c.overheadCanvas.transform);
    for (int index = 0; index < c.minerMarket.bought.Length; ++index)
    {
      if (c.minerMarket.bought[index] && c.game.isClient)
      {
        Transform transform1 = c.minerMarket.clientObj.transform;
        Transform transform2 = UnityEngine.Object.Instantiate<Transform>(transform1.GetChild(0), transform1);
        transform2.GetComponent<SpriteRenderer>().sprite = Inert.Instance.minerMarket[index].icon;
        transform2.gameObject.SetActive(true);
      }
    }
  }

  public void IncreaseHolesDug(ZCreature c)
  {
    if (!this.Has(MinerMarket.Types.Scannox_5001) || ZComponent.IsNull((ZComponent) c) || c.isDead)
      return;
    ++this.curHolesDug;
    if (this.curHolesDug <= 100 || c.game.RandomInt(0, 100) <= 97)
      return;
    this.curHolesDug = 0;
    MinerMarket.Types y = this.RandomUnbought(c.game);
    if (y != MinerMarket.Types.None)
      ZSpell.FireMinerMarket((Spell) null, c, (int) y, false);
    else
      ZSpell.AddPresents((GameObject) null, c, 1);
    if (!c.game.isClient || c.game.resyncing || !((UnityEngine.Object) c.transform != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Instantiate<GameObject>(Inert.GetSpell(SpellEnum.Presents).explosion, c.transform.position, Quaternion.identity);
    AudioManager.Play(Inert.GetSpell(SpellEnum.Presents).explosionClip);
  }

  public static void ContextMenu(MinerMarket z)
  {
    if (z == null)
      z = new MinerMarket();
    MyContextMenu myContextMenu = MyContextMenu.Show();
    myContextMenu.SetOnCancel(new Action(Player.Instance.UnselectSpell));
    myContextMenu.AddSeperator(">>>>>>>>> Miner Market <<<<<<<<<");
    myContextMenu.AddSeperator("Nothing finer for the daily miner");
    for (int index = 1; index <= 15; ++index)
    {
      if (!z.bought[index])
      {
        int y = index;
        if (Player.Instance.selected.health <= Inert.Instance.minerMarket[y].cost)
          myContextMenu.AddItemWithImage("<s>(" + (object) Inert.Instance.minerMarket[y].cost + ")</s> " + Inert.Instance.minerMarket[y].name, Inert.Instance.minerMarket[y].icon, (Action) (() =>
          {
            Player.Instance.softLock = true;
            Player.Instance.UnselectSpell();
            ChatBox.Instance?.NewChatMsg("Not enough health to buy: " + Inert.Instance.minerMarket[y].name, (Color) ColorScheme.GetColor(MyContextMenu.ColorRed));
          }), (Color) ColorScheme.GetColor(MyContextMenu.ColorRed), Inert.Instance.minerMarket[y].description);
        else
          myContextMenu.AddItemWithImage("(" + (object) Inert.Instance.minerMarket[y].cost + ") " + Inert.Instance.minerMarket[y].name, Inert.Instance.minerMarket[y].icon, (Action) (() =>
          {
            Player.Instance.softLock = true;
            if ((UnityEngine.Object) Player.Instance.selectedSpell == (UnityEngine.Object) null)
              return;
            Player.Instance.spellTarget = new MyLocation(0, y);
            Player.Instance.SendFire();
          }), (Color) ColorScheme.GetColor(MyContextMenu.ColorGreen), Inert.Instance.minerMarket[y].description);
      }
      else
        myContextMenu.AddItemWithImage("<s>" + Inert.Instance.minerMarket[index].name + "</s>", Inert.Instance.minerMarket[index].icon, (Action) null, (Color) ColorScheme.GetColor(MyContextMenu.ColorGray));
    }
    myContextMenu.Rebuild();
  }

  public MinerMarket.Types RandomUnbought(ZGame game)
  {
    List<MinerMarket.Types> typesList = new List<MinerMarket.Types>();
    for (int index = 1; index <= 15; ++index)
    {
      if (!this.bought[index])
        typesList.Add((MinerMarket.Types) index);
    }
    if (typesList.Count == 0)
      return MinerMarket.Types.None;
    return typesList.Count == 1 ? typesList[0] : typesList[game.RandomInt(0, typesList.Count)];
  }

  [Serializable]
  public enum Types
  {
    None,
    Light,
    Hard_Hat,
    Dynamite,
    H2O,
    Gas_Mask,
    Scannox_5001,
    Punch,
    Depth_Charge,
    Drill,
    Rock_Blaster,
    Map,
    Platinum_Climbing_Hooks,
    Shaft,
    x2,
    x3,
  }

  [Serializable]
  public class Item
  {
    public string name;
    public MinerMarket.Types type;
    public int cost;
    public string description;
    public Sprite icon;
  }
}
