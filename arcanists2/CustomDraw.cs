

using UnityEngine;

#nullable disable
public class CustomDraw : MonoBehaviour
{
  private Texture2D texture;

  private ZMap map => Client.game.map;

  private void Start()
  {
    this.texture = Inert.GetSpell(SpellEnum.Forest_Seed).toSummon.GetComponent<SpriteRenderer>().sprite.texture;
  }

  private void Update()
  {
    if (!Input.GetMouseButton(0) || Client.game == null || (Object) HUD.instance == (Object) null || !Client.game.isSandbox)
      return;
    this.map.BitBlt(this.texture, (int) Player.Instance.mouseWorldPos.x, (int) Player.Instance.mouseWorldPos.y, false);
  }
}
