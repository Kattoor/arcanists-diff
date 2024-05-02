// Decompiled with JetBrains decompiler
// Type: ZEffectorStaticBall
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ZEffectorStaticBall : ZEffector
{
  internal List<ZEffectorStaticBall.CaughtSpell> caughtSpells = new List<ZEffectorStaticBall.CaughtSpell>();
  private int frame;
  private float f;
  private bool isactive = true;

  public override void EffectSpell(ZSpell s)
  {
    if ((ZComponent) s == (object) null || s.moving == null || this.game.ongoing.KillCoroutines(s.moving) <= 0)
      return;
    this.caughtSpells.Add(new ZEffectorStaticBall.CaughtSpell()
    {
      spell = s,
      offset = s.position - this.position
    });
    if (this.MaxTurnsAlive > 2 && (Object) s.transform != (Object) null)
      s.transform.SetParent(this.transform);
    if ((ZComponent) s.collider != (object) null)
      s.collider.Disable(false);
    s.FindRenderer();
    this.game.forceRysncPause = true;
  }

  public bool HasPlayersOceansFurry(ZCreature c)
  {
    for (int index = 0; index < this.caughtSpells.Count; ++index)
    {
      if (this.caughtSpells[index] != null && !((ZComponent) this.caughtSpells[index].spell == (object) null) && string.Equals("Ocean's Fury", this.caughtSpells[index].spell.name))
        return true;
    }
    return false;
  }

  public override void Die(int indexInParent, bool destroyable, bool global)
  {
    this.collider.Disable();
    this.ReleaseSpells();
    this.isNull = true;
    ZComponent.Destroy<GameObject>(this.gameObject);
  }

  public void ReleaseSpells()
  {
    this.isNull = true;
    this.active = false;
    this.game.forceRysncPause = true;
    if (this.caughtSpells.Count > 0 && this.game.serverState.busy == ServerState.Busy.No)
      this.game.serverState.busy = ServerState.Busy.Moving;
    for (int index = 0; index < this.caughtSpells.Count; ++index)
    {
      if (this.caughtSpells[index] != null && !((ZComponent) this.caughtSpells[index].spell == (object) null))
      {
        if ((Object) this.caughtSpells[index].Renderer != (Object) null)
          this.caughtSpells[index].Renderer.enabled = true;
        if ((ZComponent) this.caughtSpells[index].Collider != (object) null && this.caughtSpells[index].Collider.world != null && (this.caughtSpells[index].spell.moving == null || this.caughtSpells[index].spell.spellLogic != SpellLogic.Leaf && this.caughtSpells[index].spell.spellLogic != SpellLogic.Napalm && this.caughtSpells[index].spell.spellLogic != SpellLogic.ClockworkBomb))
          this.caughtSpells[index].Collider.Enable(this.caughtSpells[index].offset + this.position);
        this.caughtSpells[index].spell.position = this.caughtSpells[index].offset + this.position;
        if ((Object) this.caughtSpells[index].spell.transform == (Object) null)
          Debug.Log((object) (this.caughtSpells[index].spell.GetName + " " + (object) this.caughtSpells[index].spell.damage + " " + this.caughtSpells[index].spell.isNull.ToString() + " " + this.caughtSpells[index].spell.isDead.ToString() + " " + (object) this.caughtSpells[index].spell.curDuration));
        else if (this.MaxTurnsAlive > 2)
          this.caughtSpells[index].spell.transform.SetParent(this.caughtSpells[index].spell.game.GetMapTransform());
        this.game.ongoing.RunSpell(this.caughtSpells[index].spell.moving);
      }
    }
    this.caughtSpells.Clear();
  }

  public void Update()
  {
    this.f += Time.deltaTime;
    if ((double) this.f < 0.5)
      return;
    ++this.frame;
    this.f = 0.0f;
    if (this.caughtSpells.Count <= 0)
      return;
    this.isactive = !this.isactive;
    for (int index = 0; index < this.caughtSpells.Count; ++index)
    {
      if ((Object) this.caughtSpells[index].Renderer != (Object) null)
        this.caughtSpells[index].Renderer.enabled = this.isactive;
    }
  }

  internal class CaughtSpell
  {
    public ZSpell spell;
    public MyLocation offset;

    public Renderer Renderer => this.spell.Renderer;

    public ZMyCollider Collider => this.spell.collider;
  }
}
