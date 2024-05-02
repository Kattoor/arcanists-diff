// Decompiled with JetBrains decompiler
// Type: EMPTY_AI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MovementEffects;
using System.Collections.Generic;

#nullable disable
public class EMPTY_AI : IAI
{
  public bool stillDoTurn;

  public override void DoTurn() => Timing.RunCoroutine(this.Loop());

  private IEnumerator<float> Loop()
  {
    EMPTY_AI emptyAi = this;
    yield return 0.0f;
    if (emptyAi.stillDoTurn)
    {
      while (emptyAi.parent.yourTurn)
        yield return 0.0f;
    }
    else
      emptyAi.game.NextTurn();
  }
}
