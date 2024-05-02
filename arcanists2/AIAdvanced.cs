// Decompiled with JetBrains decompiler
// Type: AIAdvanced
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MovementEffects;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AIAdvanced : IAI
{
  public override void DoTurn() => Timing.RunCoroutine(this.Loop());

  public IEnumerator<float> Loop()
  {
    AIAdvanced aiAdvanced = this;
    ZCreature enemy = aiAdvanced.game.players[0].first();
    if (!((ZComponent) enemy == (object) null))
    {
      while (aiAdvanced.parent.yourTurn && aiAdvanced.game.serverState.busy != ServerState.Busy.Ended)
      {
        while (aiAdvanced.game.ongoing.Busy() || aiAdvanced.game.serverState.busy != ServerState.Busy.No)
          yield return 0.0f;
        if (!((ZComponent) aiAdvanced.creature == (object) null) && !aiAdvanced.creature.isDead)
        {
          int num = Random.Range(0, 3);
          if (num > 1 && aiAdvanced.creature.spells.Count > 0)
          {
            SpellSlot spell = aiAdvanced.creature.spells[0];
            MyLocation myLocation = enemy.GetPositionAI - aiAdvanced.creature.position;
            myLocation.Normalize();
            FixedInt rot_z = FixedInt.Atan2(myLocation.y, myLocation.x) * FixedInt.Rad2Deg;
            if (spell.spell.type == CastType.Power)
              rot_z += (FixedInt) Random.Range(-5f, 5f);
            if (rot_z > 90 || rot_z < -90)
            {
              if ((double) aiAdvanced.creature.transformscale > 0.0)
                aiAdvanced.creature.SetScale(-1f);
            }
            else if ((double) aiAdvanced.creature.transformscale < 0.0)
              aiAdvanced.creature.SetScale(1f);
            ZSpell.FireWhich(spell.spell, aiAdvanced.creature, aiAdvanced.creature.position, rot_z, (FixedInt) 1, enemy.GetPositionAI, NullMyLocation.Get());
            break;
          }
          if (Random.Range(0, 2) < 1)
          {
            if ((double) aiAdvanced.creature.transformscale > 0.0)
              aiAdvanced.creature.SetScale(-1f);
            else
              aiAdvanced.creature.SetScale(1f);
          }
          if (num == 1)
            aiAdvanced.creature.LongJump(0);
          else
            aiAdvanced.creature.HighJump(0);
          yield return 0.0f;
        }
        else
          break;
      }
      if (aiAdvanced.parent.yourTurn)
      {
        while (aiAdvanced.game.ongoing.Busy() || aiAdvanced.game.serverState.busy != ServerState.Busy.No)
          yield return 0.0f;
        if (aiAdvanced.parent.yourTurn)
          aiAdvanced.game.NextTurn();
      }
    }
  }
}
