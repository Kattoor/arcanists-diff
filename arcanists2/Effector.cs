// Decompiled with JetBrains decompiler
// Type: Effector
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Effector : MonoBehaviour
{
  internal ZEffector serverObj;
  public MyCollider collider;
  public AudioClip soundClip;
  public bool effectWhileInactive;
  public EffectorType type;
  public DamageType damageType;
  public GameObject explosion;
  public int MaxTurnsAlive = 5;
  public int delayDeath;
  public int variable;
}
