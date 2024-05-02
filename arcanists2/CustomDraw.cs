// Decompiled with JetBrains decompiler
// Type: CustomDraw
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
