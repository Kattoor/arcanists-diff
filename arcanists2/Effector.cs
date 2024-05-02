

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
