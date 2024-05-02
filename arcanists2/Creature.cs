

using Educative;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class Creature : Entity
{
  public CreatureStats stats;
  [NonSerialized]
  internal CreatureStats runTimeStats;
  internal ZCreature serverObj;
  [Header("Effector")]
  public Effector auraOfDecay;
  public MyCollider collider;
  [Header("Audio")]
  public AudioClip clipDeath;
  public AudioClip clipHit;
  public AudioClip clipSelect;
  [Header("Effects")]
  public GameObject deathExplosion;
  public Sprite undeadHead;
  public Sprite undeadBody;
  public Sprite undeadLeftArm;
  public Sprite undeadRightArm;
  public Sprite undeadLeftFoot;
  public Sprite undeadRightFoot;
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
  public SpriteRenderer beard2;
  public SpriteRenderer beard3;
  public SpriteRenderer mouth;
  public GameObject christmasHat;
  internal TMP_Text txtHealth;
  internal TMP_Text txtShield;
  internal Image imgPrestige;
  internal GameObject selectedSpellPanel;
  internal Image selectedSpellIcon;
  [NonSerialized]
  public List<Creature.NotScaled> notScaled = new List<Creature.NotScaled>();
  public bool isPawn = true;
  [NonSerialized]
  public Canvas overheadCanvas;
  [NonSerialized]
  public OverheadEmoji overheadEmoji;
  [NonSerialized]
  public IAnimator animator;
  [NonSerialized]
  public PanelPlayer panelPlayer;
  internal ParticleDucks myducks;
  internal GameObject gravityObj;

  public MyLocation position => this.serverObj.position;

  public virtual ZCreature Get() => new ZCreature();

  public ZPerson parent => this.serverObj.parent;

  public ZPerson originalParent => this.serverObj.parent;

  public int radius => this.runTimeStats.radius;

  public bool affectedByGravity => this.runTimeStats.affectedByGravity;

  public int health => this.runTimeStats.health;

  public int maxHealth => this.runTimeStats.maxHealth;

  public FixedInt speed => this.runTimeStats.speed;

  public EditorFixedInt massMulti => this.runTimeStats.massMulti;

  public EditorFixedInt minAngle => this.runTimeStats.minAngle;

  public EditorFixedInt maxAngle => this.runTimeStats.maxAngle;

  public bool flying => this.runTimeStats.flying;

  public bool mountable => this.runTimeStats.mountable;

  public bool canMount => this.runTimeStats.canMount;

  public bool phantom => this.runTimeStats.phantom;

  public bool gliding => this.runTimeStats.gliding;

  public bool canMove => this.runTimeStats.canMove;

  public bool usingGlide => this.runTimeStats.usingGlide;

  public EditorVector2 LongJumpData => this.runTimeStats.LongJumpData;

  public EditorVector2 HighJumpData => this.runTimeStats.HighJumpData;

  public CreatureType type => this.runTimeStats.type;

  public CreatureRace race => this.runTimeStats.race;

  public bool waterWalking => this.runTimeStats.waterWalking;

  public bool frostWalking => this.runTimeStats.frostWalking;

  public List<SpellSlot> spells => this.runTimeStats.spells;

  internal int shield => this.serverObj.shield;

  public void OnEmoji(int index, bool spectator = false)
  {
    if (!Client.renderEmoji || !Client.renderEmojiSpectator & spectator || !((UnityEngine.Object) this.overheadCanvas != (UnityEngine.Object) null) || !((UnityEngine.Object) this.overheadEmoji == (UnityEngine.Object) null))
      return;
    this.overheadEmoji = UnityEngine.Object.Instantiate<OverheadEmoji>(ClientResources.Instance.overheadEmoji, this.overheadCanvas.transform.position + new Vector3(0.0f, spectator ? 20f : 40f), Quaternion.identity, this.transform);
    this.overheadEmoji.OnEmoji(index);
    if (!Client.emojiSound || spectator && !Spectator.isConnected)
      return;
    AudioManager.Play(AudioManager.instance.emojiShow);
  }

  private void OnDestroy() => this.Cleanup();

  public void Cleanup()
  {
    try
    {
      Global.DestroySprite((UnityEngine.Object) this.head != (UnityEngine.Object) null ? this.head.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.body != (UnityEngine.Object) null ? this.body.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.hat != (UnityEngine.Object) null ? this.hat.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.leftArm != (UnityEngine.Object) null ? this.leftArm.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.rightArm != (UnityEngine.Object) null ? this.rightArm.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.beard != (UnityEngine.Object) null ? this.beard.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.beard2 != (UnityEngine.Object) null ? this.beard2.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.beard3 != (UnityEngine.Object) null ? this.beard3.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.leftFoot != (UnityEngine.Object) null ? this.leftFoot.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.rightFoot != (UnityEngine.Object) null ? this.rightFoot.sprite : (Sprite) null);
      Global.DestroySprite((UnityEngine.Object) this.imgPrestige != (UnityEngine.Object) null ? this.imgPrestige.sprite : (Sprite) null);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public class NotScaled
  {
    public Transform transform;
    public Vector3 normalPositoin;
    public Vector3 leftPosition;
    public IAnimator animator;
    public Outfit outfit = Outfit.None;

    public NotScaled(IAnimator c, SpriteRenderer s, float offset, Outfit o)
    {
      this.transform = s.transform;
      this.normalPositoin = this.transform.localPosition;
      this.leftPosition = this.transform.localPosition + new Vector3(offset, 0.0f);
      this.animator = c;
      this.outfit = o;
    }

    public void Scale(Vector3 scale)
    {
      this.transform.localScale = scale;
      this.transform.localPosition = (double) scale.x < 0.0 ? this.leftPosition : this.normalPositoin;
      if (this.outfit == Outfit.LeftHand)
      {
        this.animator.ResetLeftHand();
      }
      else
      {
        if (this.outfit != Outfit.RightHand)
          return;
        this.animator.ResetRightHand();
      }
    }
  }
}
