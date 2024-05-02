// Decompiled with JetBrains decompiler
// Type: FamiliarNature
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FamiliarNature : Familiar
{
  public float timeToFinish = 1f;
  public bool AddCreatureRadius;
  public Sprite[] sprites;
  private float curTime;
  private float timeBetweenFrames;
  private int index;
  private SpriteRenderer sp;

  private void Start()
  {
    this.sp = this.GetComponent<SpriteRenderer>();
    this.timeBetweenFrames = this.timeToFinish / (float) this.sprites.Length;
    if (Client.game.isClient && !ZComponent.IsNull((ZComponent) this.creature) && this.creature.parent != null)
    {
      int length = this.sprites.Length;
      for (int index = 0; index < length; ++index)
        this.sprites[index] = ConfigurePlayer.ApplyOutfit(this.sprites[index], this.creature.parent.settingsPlayer);
    }
    if (ZComponent.IsNull((ZComponent) this.creature))
      return;
    this.transform.position = (Vector3) this.creature.position.ToSinglePrecision();
  }

  private void OnDestroy()
  {
    foreach (Sprite sprite in this.sprites)
    {
      if ((Object) sprite != (Object) null)
        Global.DestroySprite(sprite);
    }
  }

  protected override void LateUpdate()
  {
    if (ZComponent.IsNull((ZComponent) this.creature))
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (!this.creature.game.isClient)
        return;
      this.curTime += Time.deltaTime;
      if ((double) this.curTime <= (double) this.timeBetweenFrames)
        return;
      this.curTime = 0.0f;
      ++this.index;
      if (this.index >= this.sprites.Length)
      {
        if (!this.creature.isMoving)
          this.UpdatePosition();
        this.index = 0;
      }
      this.sp.sprite = this.sprites[this.index];
    }
  }

  public void UpdatePosition()
  {
    if (this.AddCreatureRadius)
      this.transform.position = (Vector3) this.creature.position.ToSinglePrecision() + new Vector3(0.0f, (float) (45 + (this.creature.GetRadius() - 18)));
    else
      this.transform.position = (Vector3) this.creature.position.ToSinglePrecision() + new Vector3(0.0f, this.GetOffset().y - (float) this.creature.GetRadius());
  }
}
