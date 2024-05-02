

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ClientCreature : MonoBehaviour
{
  public AudioClip clipDeath;
  public AudioClip clipHit;
  public AudioClip clipSelect;
  public GameObject deathExplosion;
  public Sprite undeadHead;
  public SpriteRenderer miniMapBg;
  public SpriteRenderer bg;
  public SpriteRenderer head;
  public SpriteRenderer body;
  public SpriteRenderer leftArm;
  public SpriteRenderer rightArm;
  public SpriteRenderer leftFoot;
  public SpriteRenderer rightFoot;
  public SpriteRenderer hat;
  public SpriteRenderer beard;
  public SpriteRenderer mouth;
  public TMP_Text txtHealth;
  public TMP_Text txtShield;
  public GameObject selectedSpellPanel;
  public Image selectedSpellIcon;
  [NonSerialized]
  public GameObject indicatorOfDecay;
}
