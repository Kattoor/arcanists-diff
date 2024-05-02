// Decompiled with JetBrains decompiler
// Type: ClientCreature
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

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
