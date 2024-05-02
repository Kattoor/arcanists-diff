

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ConfigurePlayer : MonoBehaviour
{
  public SpriteRenderer spriteHead;
  public SpriteRenderer spriteBeard;
  public SpriteRenderer spriteBeard2;
  public SpriteRenderer spriteBeard3;
  public SpriteRenderer spriteHat;
  public SpriteRenderer spriteBody;
  public SpriteRenderer spriteLeftFoot;
  public SpriteRenderer spriteRightFoot;
  public SpriteRenderer spriteLeftHand;
  public SpriteRenderer spriteRightHand;
  public SpriteRenderer spriteMouth;
  public SettingsPlayer _sp;

  public SettingsPlayer settingsPlayer => this._sp ?? Client.settingsPlayer;

  public SpriteRenderer Get(Outfit o)
  {
    switch (o)
    {
      case Outfit.Body:
        return this.spriteBody;
      case Outfit.Head:
        return this.spriteHead;
      case Outfit.LeftHand:
        return this.spriteLeftHand;
      case Outfit.RightHand:
        return this.spriteRightHand;
      case Outfit.Hair:
        return this.spriteHat;
      case Outfit.Beard:
        return this.spriteBeard;
      case Outfit.LeftFoot:
        return this.spriteLeftFoot;
      case Outfit.RightFoot:
        return this.spriteRightFoot;
      case Outfit.Beard2:
        return this.spriteBeard2;
      case Outfit.Beard3:
        return this.spriteBeard3;
      default:
        return (SpriteRenderer) null;
    }
  }

  public static void EquipAll(UIPlayerCharacter x, string name, SettingsPlayer settingsPlayer)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    if (settingsPlayer.custom == (byte) 0)
    {
      settingsPlayer.indexLeftFoot = settingsPlayer.FootIndex();
      settingsPlayer.indexRightFoot = settingsPlayer.indexLeftFoot;
    }
    else
    {
      x.mouth.sprite = settingsPlayer.textures?[8] ?? ClientResources.Instance._characterMouths[Mathf.Clamp((int) settingsPlayer.indexMouth, 0, ClientResources.Instance._characterMouths.Length - 1)];
      if (clanOutfit != null && clanOutfit.OutfitNoMouth((int) settingsPlayer.indexHead))
        x.mouth.sprite = ClientResources.Instance._characterMouths[ClientResources.Instance._characterMouths.Length - 1];
    }
    ConfigurePlayer.ApplyOutfit(x.head, settingsPlayer.indexHead, ClientResources.Instance._characterHeads, settingsPlayer, Outfit.Head, settingsPlayer.textures?[1] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHead, Outfit.Head));
    ConfigurePlayer.ApplyOutfit(x.body, settingsPlayer.indexBody, ClientResources.Instance._characterBody, settingsPlayer, Outfit.Body, settingsPlayer.textures?[0] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.Body));
    ConfigurePlayer.ApplyOutfit(x.hat, settingsPlayer.indexHat, ClientResources.Instance._characterHats, settingsPlayer, Outfit.Hair, settingsPlayer.textures?[4] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHat, Outfit.Hair));
    ConfigurePlayer.ApplyOutfit(x.leftArm, settingsPlayer.indexLeftHand, ClientResources.Instance._characterLeftHand, settingsPlayer, Outfit.LeftHand, settingsPlayer.textures?[2] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftHand, Outfit.LeftHand));
    ConfigurePlayer.ApplyOutfit(x.rightArm, settingsPlayer.indexRightHand, ClientResources.Instance._characterRightHand, settingsPlayer, Outfit.RightHand, settingsPlayer.textures?[3] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightHand, Outfit.RightHand));
    ConfigurePlayer.ApplyOutfit(x.beard, settingsPlayer.indexBeard, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard, settingsPlayer.textures?[5] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(x.beard2, settingsPlayer.indexBeard2, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard2, settingsPlayer.textures?[9] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard2, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(x.beard3, settingsPlayer.indexBeard3, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard3, settingsPlayer.textures?[10] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard3, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(x.leftFoot, settingsPlayer.indexLeftFoot, ClientResources.Instance._characterLeftFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[6] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.LeftFoot));
    ConfigurePlayer.ApplyOutfit(x.rightFoot, settingsPlayer.indexRightFoot, ClientResources.Instance._characterRightFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[7] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.RightFoot));
  }

  public void EquipAll(string name, SettingsPlayer settingsPlayer)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    if (settingsPlayer.custom == (byte) 0)
    {
      settingsPlayer.indexLeftFoot = settingsPlayer.FootIndex();
      settingsPlayer.indexRightFoot = settingsPlayer.indexLeftFoot;
    }
    else
    {
      this.spriteMouth.sprite = settingsPlayer.textures?[8] ?? ClientResources.Instance._characterMouths[Mathf.Clamp((int) settingsPlayer.indexMouth, 0, ClientResources.Instance._characterMouths.Length - 1)];
      if (clanOutfit != null && clanOutfit.OutfitNoMouth((int) settingsPlayer.indexHead))
        this.spriteMouth.sprite = ClientResources.Instance._characterMouths[ClientResources.Instance._characterMouths.Length - 1];
    }
    this.spriteLeftFoot.sortingOrder = settingsPlayer.indexLeftFoot != (byte) 2 ? -18 : -16;
    this.DestroyCharacterSprites();
    this.ApplyOutfit(this.spriteHead, settingsPlayer.indexHead, ClientResources.Instance._characterHeads, settingsPlayer, Outfit.Head, settingsPlayer.textures?[1] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHead, Outfit.Head));
    this.ApplyOutfit(this.spriteBody, settingsPlayer.indexBody, ClientResources.Instance._characterBody, settingsPlayer, Outfit.Body, settingsPlayer.textures?[0] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.Body));
    this.ApplyOutfit(this.spriteHat, settingsPlayer.indexHat, ClientResources.Instance._characterHats, settingsPlayer, Outfit.Hair, settingsPlayer.textures?[4] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHat, Outfit.Hair));
    this.ApplyOutfit(this.spriteLeftHand, settingsPlayer.indexLeftHand, ClientResources.Instance._characterLeftHand, settingsPlayer, Outfit.LeftHand, settingsPlayer.textures?[2] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftHand, Outfit.LeftHand));
    this.ApplyOutfit(this.spriteRightHand, settingsPlayer.indexRightHand, ClientResources.Instance._characterRightHand, settingsPlayer, Outfit.RightHand, settingsPlayer.textures?[3] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightHand, Outfit.RightHand));
    this.ApplyOutfit(this.spriteBeard, settingsPlayer.indexBeard, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard, settingsPlayer.textures?[5] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard, Outfit.Beard));
    this.ApplyOutfit(this.spriteBeard2, settingsPlayer.indexBeard2, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard2, settingsPlayer.textures?[9] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard2, Outfit.Beard));
    this.ApplyOutfit(this.spriteBeard3, settingsPlayer.indexBeard3, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard3, settingsPlayer.textures?[10] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard3, Outfit.Beard));
    this.ApplyOutfit(this.spriteLeftFoot, settingsPlayer.indexLeftFoot, ClientResources.Instance._characterLeftFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[6] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.LeftFoot));
    this.ApplyOutfit(this.spriteRightFoot, settingsPlayer.indexRightFoot, ClientResources.Instance._characterRightFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[7] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.RightFoot));
    this.CheckActive(name, settingsPlayer);
  }

  private void DestroyCharacterSprites()
  {
    Global.DestroySprite(this.spriteHead.sprite);
    Global.DestroySprite(this.spriteBody.sprite);
    Global.DestroySprite(this.spriteHat.sprite);
    Global.DestroySprite(this.spriteLeftHand.sprite);
    Global.DestroySprite(this.spriteRightHand.sprite);
    Global.DestroySprite(this.spriteBeard.sprite);
    Global.DestroySprite(this.spriteLeftFoot.sprite);
    Global.DestroySprite(this.spriteRightFoot.sprite);
  }

  public void ModEquipAll(
    string name,
    SettingsPlayer settingsPlayer,
    Color[] colors,
    bool destroy = true)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    settingsPlayer.indexLeftFoot = settingsPlayer.FootIndex();
    settingsPlayer.indexRightFoot = settingsPlayer.indexLeftFoot;
    this.spriteLeftFoot.sortingOrder = settingsPlayer.indexLeftFoot != (byte) 2 ? -18 : -16;
    if (clanOutfit != null && clanOutfit.OutfitNoMouth((int) settingsPlayer.indexHead))
      this.spriteMouth.sprite = ClientResources.Instance._characterMouths[ClientResources.Instance._characterMouths.Length - 1];
    this.DestroyCharacterSprites();
    this.ModApplyOutfit(destroy, this.spriteHead, settingsPlayer.indexHead, ClientResources.Instance._characterHeads, settingsPlayer, Outfit.Head, colors, settingsPlayer.textures?[1] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHead, Outfit.Head));
    this.ModApplyOutfit(destroy, this.spriteBody, settingsPlayer.indexBody, ClientResources.Instance._characterBody, settingsPlayer, Outfit.Body, colors, settingsPlayer.textures?[0] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.Body));
    this.ModApplyOutfit(destroy, this.spriteHat, settingsPlayer.indexHat, ClientResources.Instance._characterHats, settingsPlayer, Outfit.Hair, colors, settingsPlayer.textures?[4] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHat, Outfit.Hair));
    if ((UnityEngine.Object) this.spriteLeftHand != (UnityEngine.Object) null && (UnityEngine.Object) this.spriteLeftHand.sprite != (UnityEngine.Object) null && this.spriteLeftHand.sprite.texture.width > 2)
      this.ModApplyOutfit(destroy, this.spriteLeftHand, settingsPlayer.indexLeftHand, ClientResources.Instance._characterLeftHand, settingsPlayer, Outfit.LeftHand, colors, settingsPlayer.textures?[2] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftHand, Outfit.LeftHand));
    if ((UnityEngine.Object) this.spriteRightHand != (UnityEngine.Object) null && (UnityEngine.Object) this.spriteRightHand.sprite != (UnityEngine.Object) null && this.spriteRightHand.sprite.texture.width > 2)
      this.ModApplyOutfit(destroy, this.spriteRightHand, settingsPlayer.indexRightHand, ClientResources.Instance._characterRightHand, settingsPlayer, Outfit.RightHand, colors, settingsPlayer.textures?[3] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightHand, Outfit.RightHand));
    this.ModApplyOutfit(destroy, this.spriteBeard, settingsPlayer.indexBeard, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard, colors, settingsPlayer.textures?[5] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard, Outfit.Beard));
    this.ModApplyOutfit(destroy, this.spriteBeard2, settingsPlayer.indexBeard2, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard2, colors, settingsPlayer.textures?[9] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard2, Outfit.Beard));
    this.ModApplyOutfit(destroy, this.spriteBeard3, settingsPlayer.indexBeard3, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard3, colors, settingsPlayer.textures?[10] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard3, Outfit.Beard));
    this.ModApplyOutfit(destroy, this.spriteLeftFoot, settingsPlayer.indexLeftFoot, ClientResources.Instance._characterLeftFoot, settingsPlayer, Outfit.Body, colors, settingsPlayer.textures?[6] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftFoot, Outfit.LeftFoot));
    this.ModApplyOutfit(destroy, this.spriteRightFoot, settingsPlayer.indexRightFoot, ClientResources.Instance._characterRightFoot, settingsPlayer, Outfit.Body, colors, settingsPlayer.textures?[7] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightFoot, Outfit.RightFoot));
    this.CheckActive(name, settingsPlayer);
    List<GameObject> archMageStaffs = this.GetComponent<Creature>()?.parent?.archMageStaffs;
    if (archMageStaffs == null)
      return;
    foreach (GameObject gameObject in archMageStaffs)
    {
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        foreach (SpriteRenderer componentsInChild in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
          AnimateRepeat component1 = componentsInChild.GetComponent<AnimateRepeat>();
          if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) componentsInChild.sprite != (UnityEngine.Object) null && componentsInChild.sprite.texture.isReadable)
            {
              for (int index = 0; index < ClientResources.Instance.ArchmageStaffs.Length; ++index)
              {
                if (componentsInChild.name.StartsWith(ClientResources.Instance.ArchmageStaffs[index].name))
                {
                  SpriteRenderer componentInChildren = ClientResources.Instance.ArchmageStaffs[index].GetComponentInChildren<SpriteRenderer>();
                  componentsInChild.sprite = Recolor.Texture(componentInChildren.sprite, colors[0], colors[1], colors[2], colors[3]);
                  break;
                }
              }
            }
          }
          else
          {
            for (int index = 0; index < ClientResources.Instance.ArchmageStaffs.Length; ++index)
            {
              if (component1.name.StartsWith(ClientResources.Instance.ArchmageStaffs[index].name))
              {
                AnimateRepeat component2 = ClientResources.Instance.ArchmageStaffs[index].GetComponent<AnimateRepeat>();
                ClientResources.ChangeSprites(component1.GetSpriteRenderer, component1, component1.sprites, component2.sprites, colors);
                break;
              }
            }
          }
        }
      }
    }
  }

  public static bool BeardInFront(byte b) => b == (byte) 74 || b == (byte) 97;

  public static bool BeardInBack(byte b) => b == (byte) 98;

  public void CheckActive(string name, SettingsPlayer settingsPlayer)
  {
    if ((UnityEngine.Object) this.spriteMouth != (UnityEngine.Object) null)
      this.spriteMouth.enabled = !SettingsPlayer.HeadNoMouth(name, settingsPlayer);
    bool flag = (int) settingsPlayer.indexBody != SettingsPlayer.sno_body2;
    this.spriteHead.enabled = flag;
    this.spriteHat.enabled = flag;
    this.spriteLeftHand.enabled = flag;
    this.spriteRightHand.enabled = flag;
    this.spriteBeard.enabled = flag;
    this.spriteLeftFoot.enabled = flag;
    this.spriteRightFoot.enabled = flag;
    this.spriteBeard.sortingOrder = ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard) ? -10 : -13;
    this.spriteBeard2.sortingOrder = ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard2) ? -11 : -14;
    this.spriteBeard3.sortingOrder = ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard3) ? -12 : -15;
  }

  public static void EquipAll(string name, UIPlayerCharacter p, SettingsPlayer settingsPlayer)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    settingsPlayer.indexLeftFoot = settingsPlayer.FootIndex();
    settingsPlayer.indexRightFoot = settingsPlayer.indexLeftFoot;
    p.mouth.sprite = clanOutfit == null || !clanOutfit.OutfitNoMouth((int) settingsPlayer.indexHead) ? ClientResources.Instance._characterMouths[0] : ClientResources.Instance._characterMouths[ClientResources.Instance._characterMouths.Length - 1];
    p.Cleanup();
    p.disposable = true;
    ConfigurePlayer.ApplyOutfit(p.head, settingsPlayer.indexHead, ClientResources.Instance._characterHeads, settingsPlayer, Outfit.Head, settingsPlayer.textures?[1] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHead, Outfit.Head));
    ConfigurePlayer.ApplyOutfit(p.body, settingsPlayer.indexBody, ClientResources.Instance._characterBody, settingsPlayer, Outfit.Body, settingsPlayer.textures?[0] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.Body));
    ConfigurePlayer.ApplyOutfit(p.hat, settingsPlayer.indexHat, ClientResources.Instance._characterHats, settingsPlayer, Outfit.Hair, settingsPlayer.textures?[4] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHat, Outfit.Hair));
    ConfigurePlayer.ApplyOutfit(p.leftArm, settingsPlayer.indexLeftHand, ClientResources.Instance._characterLeftHand, settingsPlayer, Outfit.LeftHand, settingsPlayer.textures?[2] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexLeftHand, Outfit.LeftHand));
    ConfigurePlayer.ApplyOutfit(p.rightArm, settingsPlayer.indexRightHand, ClientResources.Instance._characterRightHand, settingsPlayer, Outfit.RightHand, settingsPlayer.textures?[3] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexRightHand, Outfit.RightHand));
    ConfigurePlayer.ApplyOutfit(p.beard, settingsPlayer.indexBeard, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard, settingsPlayer.textures?[5] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(p.beard2, settingsPlayer.indexBeard2, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard2, settingsPlayer.textures?[9] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard2, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(p.beard3, settingsPlayer.indexBeard3, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard3, settingsPlayer.textures?[10] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard3, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(p.leftFoot, settingsPlayer.indexLeftFoot, ClientResources.Instance._characterLeftFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[6] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.LeftFoot));
    ConfigurePlayer.ApplyOutfit(p.rightFoot, settingsPlayer.indexRightFoot, ClientResources.Instance._characterRightFoot, settingsPlayer, Outfit.Body, settingsPlayer.textures?[7] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBody, Outfit.RightFoot));
    if ((UnityEngine.Object) p.mouth != (UnityEngine.Object) null)
      p.mouth.enabled = !SettingsPlayer.HeadNoMouth(name, settingsPlayer);
    bool flag = (int) settingsPlayer.indexBody != SettingsPlayer.sno_body2;
    p.head.enabled = flag;
    p.hat.enabled = flag;
    p.leftArm.enabled = flag;
    p.rightArm.enabled = flag;
    p.beard.enabled = flag;
    p.beard2.enabled = flag;
    p.beard3.enabled = flag;
    p.rightFoot.enabled = flag;
    p.leftFoot.enabled = flag;
    p.Realign();
    p.beard.transform.SetSiblingIndex(4);
    p.beard2.transform.SetSiblingIndex(5);
    p.beard3.transform.SetSiblingIndex(6);
    p.beard.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard) ? 9 : (ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard) ? 0 : 4));
    int num1 = ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard) ? -1 : 0;
    int num2 = ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard) ? 1 : 0;
    p.beard2.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard2) ? 9 : (ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard2) ? num2 : 5 + num1));
    int num3 = num1 + (ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard2) ? -1 : 0);
    int num4 = ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard2) ? 1 : 0;
    p.beard3.transform.SetSiblingIndex(ConfigurePlayer.BeardInFront(settingsPlayer.indexBeard3) ? 9 : (ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard3) ? num4 : 6 + num3));
  }

  public static void DestroyOutfit(Sprite t)
  {
    try
    {
      Global.DestroySprite(t);
    }
    catch (Exception ex)
    {
    }
  }

  public static Sprite GetSprite(
    byte i,
    Sprite[] ggs,
    SettingsPlayer settingsPlayer,
    Outfit o,
    Sprite texOverride = null)
  {
    if ((UnityEngine.Object) texOverride != (UnityEngine.Object) null)
      return texOverride;
    if ((int) i >= ggs.Length)
      i = (byte) 0;
    return ggs[(int) i];
  }

  public void ApplyOutfit(
    SpriteRenderer r,
    byte i,
    Sprite[] ggs,
    SettingsPlayer settingsPlayer,
    Outfit o,
    Sprite texOverride = null)
  {
    if (r.gameObject.CompareTag("RawImage"))
      ConfigurePlayer.DestroyOutfit(r.sprite);
    if ((UnityEngine.Object) texOverride != (UnityEngine.Object) null)
    {
      r.sprite = settingsPlayer.coloring.Texture(texOverride, o);
      r.gameObject.tag = "RawImage";
      r.gameObject.layer = 20;
    }
    else
    {
      if ((int) i >= ggs.Length)
        i = (byte) 0;
      r.sprite = settingsPlayer.coloring.Texture(ggs[(int) i], o);
      r.gameObject.tag = "RawImage";
    }
  }

  public void ModApplyOutfit(
    bool destroy,
    SpriteRenderer r,
    byte i,
    Sprite[] ggs,
    SettingsPlayer settingsPlayer,
    Outfit o,
    Color[] colors,
    Sprite texOverride = null)
  {
    if (destroy && r.gameObject.CompareTag("RawImage"))
      ConfigurePlayer.DestroyOutfit(r.sprite);
    if ((UnityEngine.Object) texOverride != (UnityEngine.Object) null)
    {
      r.sprite = Recolor.Texture(texOverride, colors[0], colors[1], colors[2], colors[3]);
      r.gameObject.tag = "RawImage";
      r.gameObject.layer = 20;
    }
    else
    {
      if ((int) i >= ggs.Length)
        i = (byte) 0;
      r.sprite = Recolor.Texture(ggs[(int) i], colors[0], colors[1], colors[2], colors[3]);
      r.gameObject.tag = "RawImage";
    }
  }

  public static void ApplyOutfit(
    Image r,
    byte i,
    Sprite[] ggs,
    SettingsPlayer settingsPlayer,
    Outfit o,
    Sprite texOverride = null)
  {
    if (r.gameObject.CompareTag("RawImage"))
      ConfigurePlayer.DestroyOutfit(r.sprite);
    if ((UnityEngine.Object) texOverride != (UnityEngine.Object) null)
    {
      r.sprite = settingsPlayer.coloring.Texture(texOverride, o);
      r.gameObject.tag = "RawImage";
      r.gameObject.layer = 20;
    }
    else
    {
      if ((int) i >= ggs.Length)
        i = (byte) 0;
      r.sprite = settingsPlayer.coloring.Texture(OutfitButton.GetPreview(ggs[(int) i]), o);
      r.gameObject.tag = "RawImage";
    }
  }

  public static void PrestigeHat(Image r, byte i, Color c, bool destroy)
  {
    if ((int) i >= ClientResources.Instance._iconsPrestige.Length)
      i = (byte) 0;
    if (r.gameObject.CompareTag("RawImage") & destroy)
      ConfigurePlayer.DestroyOutfit(r.sprite);
    r.sprite = Recolor.PrestigeHat(ClientResources.Instance._iconsPrestige[(int) i], c);
    r.gameObject.tag = "RawImage";
  }

  public static void ApplyOutfit(PanelPlayer p, SettingsPlayer settingsPlayer, string name)
  {
    ClanOufit clanOutfit = ClientResources.Instance.GetClanOutfit(Client.GetAccount(name).clan);
    if (settingsPlayer.custom == (byte) 0)
    {
      settingsPlayer.indexLeftFoot = settingsPlayer.FootIndex();
      settingsPlayer.indexRightFoot = settingsPlayer.indexLeftFoot;
    }
    else
    {
      p.mouth.sprite = settingsPlayer.textures?[8] ?? ClientResources.Instance._characterMouths[Mathf.Clamp((int) settingsPlayer.indexMouth, 0, ClientResources.Instance._characterMouths.Length - 1)];
      if (clanOutfit != null && clanOutfit.OutfitNoMouth((int) settingsPlayer.indexHead))
        p.mouth.sprite = ClientResources.Instance._characterMouths[ClientResources.Instance._characterMouths.Length - 1];
    }
    Global.DestroySprite(p.head?.sprite);
    Global.DestroySprite(p.beard?.sprite);
    Global.DestroySprite(p.hat?.sprite);
    ConfigurePlayer.ApplyOutfit(p.head, settingsPlayer.indexHead, ClientResources.Instance._characterHeads, settingsPlayer, Outfit.Head, settingsPlayer.textures?[1] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHead, Outfit.Head));
    ConfigurePlayer.ApplyOutfit(p.hat, settingsPlayer.indexHat, ClientResources.Instance._characterHats, settingsPlayer, Outfit.Hair, settingsPlayer.textures?[4] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexHat, Outfit.Hair));
    ConfigurePlayer.ApplyOutfit(p.beard, ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard) ? (byte) 0 : settingsPlayer.indexBeard, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard, settingsPlayer.textures?[5] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(p.beard2, ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard2) ? (byte) 0 : settingsPlayer.indexBeard2, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard2, settingsPlayer.textures?[9] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard2, Outfit.Beard));
    ConfigurePlayer.ApplyOutfit(p.beard3, ConfigurePlayer.BeardInBack(settingsPlayer.indexBeard3) ? (byte) 0 : settingsPlayer.indexBeard3, ClientResources.Instance._characterBeards, settingsPlayer, Outfit.Beard3, settingsPlayer.textures?[10] ?? clanOutfit?.GetSprite((int) settingsPlayer.indexBeard3, Outfit.Beard));
    p.mouth.enabled = !SettingsPlayer.HeadNoMouth(name, settingsPlayer);
  }

  public static void ApplyOutfit(PanelPlayer p, SettingsPlayer settingsPlayer, Creature cre)
  {
    Global.DestroySprite(p.head?.sprite);
    Global.DestroySprite(p.beard?.sprite);
    Global.DestroySprite(p.beard2?.sprite);
    Global.DestroySprite(p.beard3?.sprite);
    Global.DestroySprite(p.hat?.sprite);
    p.head.sprite = !((UnityEngine.Object) cre.head != (UnityEngine.Object) null) ? (Sprite) null : Recolor.Copy(cre.head?.sprite);
    p.beard.sprite = !((UnityEngine.Object) cre.beard != (UnityEngine.Object) null) ? (Sprite) null : Recolor.Copy(cre.beard?.sprite);
    p.beard2.sprite = !((UnityEngine.Object) cre.beard2 != (UnityEngine.Object) null) ? (Sprite) null : Recolor.Copy(cre.beard2?.sprite);
    p.beard3.sprite = !((UnityEngine.Object) cre.beard3 != (UnityEngine.Object) null) ? (Sprite) null : Recolor.Copy(cre.beard3?.sprite);
    p.hat.sprite = !((UnityEngine.Object) cre.hat != (UnityEngine.Object) null) ? (Sprite) null : Recolor.Copy(cre.hat?.sprite);
    if (!((UnityEngine.Object) cre.mouth != (UnityEngine.Object) null))
      return;
    p.mouth.sprite = cre.mouth?.sprite;
  }

  public static void ApplyOutfit(SpriteRenderer r, SettingsPlayer settingsPlayer)
  {
    if (!((UnityEngine.Object) r.sprite != (UnityEngine.Object) null))
      return;
    r.gameObject.tag = "RawImage";
    r.sprite = settingsPlayer.coloring.Texture(r.sprite, Outfit.Body);
  }

  public static Sprite ApplyOutfit(Sprite r, SettingsPlayer settingsPlayer)
  {
    return settingsPlayer.coloring.Texture(r, Outfit.Body);
  }

  public static void ApplyOutfit(Image r, SettingsPlayer settingsPlayer)
  {
    if (!((UnityEngine.Object) r.sprite != (UnityEngine.Object) null))
      return;
    r.gameObject.tag = "RawImage";
    r.sprite = settingsPlayer.coloring.Texture(r.sprite, Outfit.Body);
  }
}
