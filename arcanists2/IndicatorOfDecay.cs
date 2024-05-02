// Decompiled with JetBrains decompiler
// Type: IndicatorOfDecay
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class IndicatorOfDecay : MonoBehaviour
{
  private ZCreature creature;
  private ZMyCollider col;
  private ZMyCollider colTower;
  private ZMyCollider fastCheck;
  public SpriteRenderer sp;
  private static ZMyCollider[] MyColliders = new ZMyCollider[20];
  private static int x;
  private static ContactFilter2D filter = IndicatorOfDecay.InitFilter();
  private int counter;

  public void Setup(ZCreature c, ZEffector e)
  {
    this.creature = c;
    this.fastCheck = e.collider;
    this.col = this.creature.collider;
    if ((ZComponent) this.creature.tower != (object) null)
      this.colTower = this.creature.tower.collider;
    if (e.damageType == DamageType.Arcane)
      this.sp.color = new Color(1f, 0.0f, 1f, 0.5f);
    else if (e.damageType == DamageType.Heal20)
      this.sp.color = new Color(1f, 0.0f, 0.0f, 0.5f);
    else if (e.damageType == DamageType.Infection)
      this.sp.color = new Color(1f, 1f, 0.0f, 0.5f);
    else if (e.damageType == DamageType.Sting)
      this.sp.color = new Color(1f, 0.5647f, 0.0f, 0.5f);
    else
      this.sp.color = new Color(0.0f, 1f, 0.0f, 0.5f);
  }

  private static ContactFilter2D InitFilter()
  {
    return new ContactFilter2D()
    {
      layerMask = (LayerMask) 512
    };
  }

  private void Update()
  {
    if ((ZComponent) this.creature != (object) null && this.creature.game != null && this.creature.game.world != null)
    {
      IndicatorOfDecay.x = !((ZComponent) this.colTower != (object) null) ? this.creature.game.world.OverlapColliderAll(this.col, IndicatorOfDecay.MyColliders, 512) : this.creature.game.world.OverlapColliderAll(this.colTower, IndicatorOfDecay.MyColliders, 512);
      for (int index = 0; index < IndicatorOfDecay.x; ++index)
      {
        ZEffector effector = IndicatorOfDecay.MyColliders[index].effector;
        if ((ZComponent) effector != (object) null)
        {
          if (effector.type == EffectorType.Aura_of_decay)
          {
            if (effector.damageType == DamageType.Arcane)
              this.sp.color = new Color(1f, 0.0f, 1f, 0.5f);
            else if (effector.damageType == DamageType.Heal20)
              this.sp.color = new Color(1f, 0.0f, 0.0f, 0.5f);
            else if (effector.damageType == DamageType.Infection)
              this.sp.color = new Color(1f, 1f, 0.0f, 0.5f);
            else if (effector.damageType == DamageType.Sting)
            {
              if (this.creature.type == CreatureType.Bee || this.creature.type == CreatureType.Beehive)
              {
                Object.Destroy((Object) this.gameObject);
                return;
              }
              this.sp.color = new Color(1f, 0.5647f, 0.0f, 0.5f);
            }
            else
              this.sp.color = new Color(0.0f, 1f, 0.0f, 0.5f);
            this.counter = 0;
            return;
          }
          if ((effector.type == EffectorType.Lich_Aura_of_decay || effector.type == EffectorType.Dragon_Aura_of_Decay) && (ZComponent) effector.whoSummoned != (object) this.creature)
          {
            if (effector.damageType == DamageType.Arcane)
              this.sp.color = new Color(1f, 0.0f, 1f, 0.5f);
            else if (effector.damageType == DamageType.Heal20)
              this.sp.color = new Color(1f, 0.0f, 0.0f, 0.5f);
            else if (effector.damageType == DamageType.Infection)
              this.sp.color = new Color(1f, 1f, 0.0f, 0.5f);
            else if (effector.damageType == DamageType.Sting)
            {
              if (this.creature.type == CreatureType.Bee || this.creature.type == CreatureType.Beehive)
              {
                Object.Destroy((Object) this.gameObject);
                return;
              }
              this.sp.color = new Color(1f, 0.5647f, 0.0f, 0.5f);
            }
            else
            {
              if (this.creature.type == CreatureType.Jar)
              {
                int? layer = effector.whoSummoned?.collider?.layer;
                int maskJar = Inert.mask_Jar;
                if (layer.GetValueOrDefault() == maskJar & layer.HasValue)
                {
                  Object.Destroy((Object) this.gameObject);
                  return;
                }
              }
              this.sp.color = new Color(0.0f, 1f, 0.0f, 0.5f);
            }
            this.counter = 0;
            return;
          }
        }
      }
      if (this.counter < 15 && this.creature.game.serverState.busy == ServerState.Busy.Moving)
      {
        ++this.counter;
        return;
      }
    }
    Object.Destroy((Object) this.gameObject);
  }
}
