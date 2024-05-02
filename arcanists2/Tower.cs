// Decompiled with JetBrains decompiler
// Type: Tower
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Tower : MonoBehaviour
{
  internal ZTower serverObj;
  public Spell FromSpell;
  public TowerType type;
  public int radius;
  public int lowerRadius;
  public int MaxHealth = 75;
  public int Health = 75;
  public int colliderOffset;
  public ExplosionCutout cutout;
  [Tooltip("Height/2 - playerRadius")]
  public int playerOffsetY;
  public static readonly FixedInt AddedOffsetY = (FixedInt) -3250585L;
  [Tooltip("Check using Cutout in Hierarchy")]
  public int cutoutOffsetY;
  public Texture2D texture;
  public static readonly FixedInt CogTowerRadius = (FixedInt) 18874368L;
  private MyLocation _position;
  public MyCollider collider;
  internal MyCollider secondCollider;
  public Effector effector;
  public int firstChildYOffset;

  [ContextMenu("offset")]
  private void offset()
  {
    this.colliderOffset = (int) this.gameObject.transform.GetChild(0).transform.localPosition.y + 1;
  }
}
