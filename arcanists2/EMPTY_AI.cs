

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
