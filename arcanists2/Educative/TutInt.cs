// Decompiled with JetBrains decompiler
// Type: Educative.TutInt
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using System;
using UnityEngine;

#nullable disable
namespace Educative
{
  [Serializable]
  public class TutInt
  {
    public int _number;
    public From which;
    public int entity = -2;
    private ZCreature creature;

    [JsonIgnore]
    public int value
    {
      get
      {
        switch (this.which)
        {
          case From.Number:
            return this._number;
          case From.Health:
            return (ZComponent) this.creature != (object) null ? this.creature.health : 0;
          case From.Shield:
            return (ZComponent) this.creature != (object) null ? this.creature.shield : 0;
          case From.Xpos:
            if ((ZComponent) this.creature != (object) null)
              return this.creature.position.x.ToInt();
            break;
          case From.Ypos:
            if ((ZComponent) this.creature != (object) null)
              return this.creature.position.y.ToInt();
            break;
          case From.MinionCount:
            if ((ZComponent) this.creature != (object) null)
              return this.creature.parent.controlled.Count - 1;
            break;
          case From.SpellUses:
            return (ZComponent) this.creature != (object) null && this.creature.spells.Count > 0 ? this.creature.spells[0].UsedUses : 0;
          case From.SpellMaxUses:
            return (ZComponent) this.creature != (object) null && this.creature.spells.Count > 0 ? this.creature.spells[0].MaxUses : 0;
          case From.SpellCooldown:
            return (ZComponent) this.creature != (object) null && this.creature.spells.Count > 0 ? this.creature.spells[0].RechargeTime : 0;
          case From.Weight:
            return (ZComponent) this.creature != (object) null ? (int) (this.creature.massMulti * 100) : 0;
          case From.Turn:
            return (ZComponent) this.creature == (object) null ? 0 : this.creature.parent.localTurn;
          case From.TimeLimit:
            return Client.game.sandboxTime;
          case From.Random10:
            return UnityEngine.Random.Range(0, 10);
          case From.Random1000000:
            return UnityEngine.Random.Range(0, 1000000);
          default:
            int num = 0;
            int key = (int) (this.which - 1000);
            Client._tutorial.namedVariable.TryGetValue(key, out num);
            return num;
        }
        Debug.LogError((object) ("Not Implemented: " + (object) this.which));
        return this._number;
      }
      set
      {
        switch (this.which)
        {
          case From.Number:
            this._number = value;
            break;
          case From.Health:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.health = value;
            this.creature.UpdateHealthTxt();
            if (this.creature.health > 0)
              break;
            this.creature.OnDeath(true);
            break;
          case From.Shield:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.CreateProtectionShield();
            this.creature.shield = value;
            this.creature.UpdateHealthTxt();
            break;
          case From.Xpos:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.MoveToPosition = new MyLocation((FixedInt) value, this.creature.position.y);
            this.creature.Demount();
            this.creature.Fall();
            break;
          case From.Ypos:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.MoveToPosition = new MyLocation(this.creature.position.x, (FixedInt) value);
            this.creature.Demount();
            this.creature.Fall();
            break;
          case From.MinionCount:
            Debug.LogError((object) ("Cannot set " + (object) this.which));
            break;
          case From.SpellUses:
            if ((ZComponent) this.creature == (object) null || this.creature.spells.Count == 0)
              break;
            this.creature.spells[0].SetUses = value;
            break;
          case From.SpellMaxUses:
            if ((ZComponent) this.creature == (object) null || this.creature.spells.Count == 0)
              break;
            this.creature.spells[0].MaxUses = value;
            break;
          case From.SpellCooldown:
            if ((ZComponent) this.creature == (object) null || this.creature.spells.Count == 0)
              break;
            this.creature.spells[0].RechargeTime = value;
            break;
          case From.Weight:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.massMulti = (FixedInt) (value > 100 || value < 0 ? 0.0f : (float) (100 - value) / 100f);
            break;
          case From.Turn:
            if ((ZComponent) this.creature == (object) null)
              break;
            this.creature.parent.localTurn = value;
            this.creature.game.turn = value;
            HUD.instance?.NextTurn(Client.game.players[0], true);
            break;
          case From.TimeLimit:
            Client.game.sandboxTime = value;
            Client.game.MaxTurnTime = value;
            Client.game.PlayersMaxTurnTime = (float) value;
            Client.game.serverState.turnTime = 0.0f;
            break;
          case From.Random10:
            break;
          case From.Random1000000:
            break;
          default:
            Client._tutorial.namedVariable[(int) (this.which - 1000)] = value;
            break;
        }
      }
    }

    public int Set(TutInt value)
    {
      this.value = value.value;
      return this.value;
    }

    public int Get() => this.value;

    public void SetEntity(Tutorial t) => this.creature = t.GetCreature(this.entity);

    public bool BiggerThen(TutInt other) => this.value > other.value;

    public bool BiggerThenOrEqual(TutInt other) => this.value >= other.value;

    public bool SmallerThen(TutInt other) => this.value < other.value;

    public bool SmallerThenOrEqual(TutInt other) => this.value <= other.value;

    public bool EqualTo(TutInt other) => this.value == other.value;

    public bool NotEqual(TutInt other) => this.value != other.value;

    public int Add(TutInt other) => this.value + other.value;

    public int Subract(TutInt other) => this.value - other.value;

    public int Multiply(TutInt other) => this.value * other.value;

    public int Divide(TutInt other) => this.value / other.value;

    public int Modules(TutInt other) => this.value % other.value;
  }
}
