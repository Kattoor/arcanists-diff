

using MovementEffects;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AI : IAI
{
  public override void DoTurn() => Timing.RunCoroutine(this.Loop());

  public IEnumerator<float> Loop()
  {
    AI ai = this;
    ZCreature enemy = ai.game.players[0].first();
    if (!((ZComponent) enemy == (object) null))
    {
      int count = 0;
      while (ai.parent.yourTurn && ai.game.serverState.busy != ServerState.Busy.Ended && count < 8)
      {
        while (ai.game.ongoing.Busy() || ai.game.serverState.busy != ServerState.Busy.No)
          yield return 0.0f;
        if (!((ZComponent) ai.creature == (object) null) && !ai.creature.isDead)
        {
          if (ai.creature.inWater)
          {
            ZSpell.FireWhich(Inert.GetSpell(SpellEnum.Arcane_Gate), ai.creature, enemy.GetPositionAI + new MyLocation(0, 100), (FixedInt) 0, (FixedInt) 1, enemy.GetPositionAI, NullMyLocation.Get());
            break;
          }
          int num = Random.Range(0, 3);
          if ((num > 1 || count >= 5) && ai.creature.spells.Count > 0)
          {
            SpellSlot spell = ai.creature.spells[0];
            if (ai.creature.spells.Count > 1)
              spell = ai.creature.spells[Random.Range(0, ai.creature.spells.Count)];
            MyLocation myLocation = enemy.GetPositionAI - ai.creature.position;
            myLocation.Normalize();
            FixedInt d = FixedInt.Abs(ai.creature.position.x - (FixedInt) (float) (int) enemy.position.x);
            FixedInt v = (FixedInt) spell.spell.speedMax;
            if (v <= 0)
              v = (FixedInt) 1000;
            bool smallAngle = !(myLocation.y < 0);
            FixedInt fixedInt = spell.spell.affectedByGravity ? ZSpell.AngleToGoDistance(spell.spell.spellType == SpellType.Ball || spell.spell.spellType == SpellType.Bomb || spell.spell.spellType == SpellType.Arrow ? v / 2 : v, -ai.creature.game.gravity, d, (FixedInt) (float) (int) enemy.position.y - ai.creature.position.y, smallAngle) : FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
            FixedInt power = (FixedInt) 524288L;
            if (d > 10 && FixedInt.InvalidAngle(fixedInt))
            {
              power = (FixedInt) 1;
              fixedInt = ZSpell.AngleToGoDistance(v, -ai.creature.game.gravity, d, (FixedInt) (float) (int) enemy.GetPositionAI.y - ai.creature.position.y, !smallAngle);
              if (FixedInt.InvalidAngle(fixedInt))
                fixedInt = (FixedInt) 47185920L;
            }
            if (spell.spell.affectedByGravity && enemy.position.x < ai.creature.position.x)
              fixedInt = (FixedInt) 180 - fixedInt;
            if (spell.spell.type == CastType.Power)
              fixedInt += (FixedInt) Random.Range(-5f, 5f);
            if (fixedInt > 90 || fixedInt < -90)
            {
              if ((double) ai.creature.transformscale > 0.0)
                ai.creature.SetScale(-1f);
            }
            else if ((double) ai.creature.transformscale < 0.0)
              ai.creature.SetScale(1f);
            ZSpell.FireWhich(spell.spell, ai.creature, ai.creature.position, fixedInt, power, enemy.GetPositionAI, NullMyLocation.Get());
            break;
          }
          ++count;
          if (!ai.creature.stunned && (ZComponent) ai.creature.tower == (object) null)
          {
            if (ai.creature.flying)
            {
              ai.Fly(Random.Range(0, 2) < 1);
            }
            else
            {
              if (Random.Range(0, 2) < 1)
              {
                if ((double) ai.creature.transformscale > 0.0)
                  ai.creature.SetScale(-1f);
                else
                  ai.creature.SetScale(1f);
              }
              if (num == 1)
                ai.creature.LongJump(0);
              else
                ai.creature.HighJump(0);
            }
          }
          yield return 0.0f;
        }
        else
          break;
      }
      if (ai.parent.yourTurn)
      {
        while (ai.game.ongoing.Busy() || ai.game.serverState.busy != ServerState.Busy.No)
          yield return 0.0f;
        if (ai.parent.yourTurn)
          ai.game.NextTurn();
      }
    }
  }

  public void Fly(bool left)
  {
    this.creature.velocity = left ? new MyLocation((FixedInt) ((double) this.creature.transformscale < 0.0 ? -11010048L : -6291456L), (FixedInt) 0) : new MyLocation((FixedInt) ((double) this.creature.transformscale > 0.0 ? 9437184L : 4194304L), (FixedInt) 0);
    this.creature.SetScale(left ? -1f : 1f);
    this.creature.moving = this.creature.game.ongoing.RunCoroutine(this.creature.FlyMove(true));
  }
}
