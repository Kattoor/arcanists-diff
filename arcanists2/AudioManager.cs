// Decompiled with JetBrains decompiler
// Type: AudioManager
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AudioManager : MonoBehaviour
{
  [Header("Per Spellbook")]
  public AudioClip[] clipSelectSpell;
  public AudioClip[] clipChargeFamiliar;
  public AudioClip[] clipChargeSpell;
  public AudioClip[] clipHarmony;
  [Header("Random")]
  public AudioClip createThornBallTerrain;
  public AudioClip spellBounce;
  public AudioClip spellBounceBones;
  public AudioClip flashEnd;
  public AudioClip pop;
  public AudioClip waterExplosion;
  public AudioClip waterBounce;
  public AudioClip trollMedium;
  public AudioClip trollBig;
  public AudioClip mechArrowExplode;
  public AudioClip mapPing;
  public AudioClip emojiShow;
  public AudioClip coins;
  [Header("Movement")]
  public AudioClip footSteps;
  public AudioClip runSteps;
  public AudioClip jumpLong;
  public AudioClip jumpHigh;
  public AudioClip land;
  public AudioClip click;
  public AudioClip splash;
  public AudioClip spellSlash;
  public AudioClip mineCharge;
  public AudioClip moneySteps;
  public AudioClip moneyJump;
  public AudioClip watchtowerDestroyed;
  public AudioClip[] turnStart;
  [Header("Music")]
  public AudioClip musicMainMenu;
  public AudioClip musicCredits;
  public AudioClip musicCharacterCreation;
  public AudioClip[] musicGame;
  public AudioClip[] additionalMusic;
  private float volumeSound = 0.5f;
  private float volumeMusic = 0.5f;
  private AudioClip clipPausedMusic;
  private float pausedTime;
  [Header("Cached Audio Sources")]
  public AudioSource[] sources;
  public AudioSource[] sourcesNapalm;
  public AudioSource sourceMusic;
  public AudioSource sourceChargeSpell;
  public AudioSource sourceCastSpell;
  public AudioSource sourcePickupMusic;
  private int curIndex;
  private int curIndexNapalm;

  public static AudioManager instance { get; private set; }

  public static void PlayTurnStart()
  {
    AudioManager.instance.InstancePlay(AudioManager.instance.turnStart[Mathf.Clamp(PlayerPrefs.GetInt("prefturnstartsound", 0), 0, AudioManager.instance.turnStart.Length - 1)], PlayerPrefs.GetFloat("prefturnstartvolume", 0.5f));
  }

  public static void PlayFromSource(AudioClip clip, AudioSource source)
  {
    source.clip = clip;
    source.volume = AudioManager.instance.volumeSound;
    source.time = 0.0f;
    source.Play();
  }

  public static void PlayChargeSpell(int i)
  {
    AudioManager.instance.sourceChargeSpell.clip = AudioManager.instance.clipChargeSpell[i];
    AudioManager.instance.sourceChargeSpell.volume = AudioManager.instance.volumeSound;
    AudioManager.instance.sourceChargeSpell.time = 0.0f;
    AudioManager.instance.sourceChargeSpell.Play();
  }

  public static void StopChargeSpell()
  {
    if (!AudioManager.instance.sourceChargeSpell.isPlaying)
      return;
    AudioManager.instance.sourceChargeSpell.Stop();
  }

  public static void PlayNapalm(AudioClip clip)
  {
    if (Client.game == null || !Client.game.isClient || Client.game.resyncing || !((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance.InstancePlayNapalm(clip);
  }

  public static void Play(AudioClip clip)
  {
    if (Client.game == null || !Client.game.isClient || Client.game.resyncing || !((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance.InstancePlay(clip);
  }

  public static void PlayChess(AudioClip clip)
  {
    if (!((Object) ChessUI.Instance != (Object) null) || !ChessUI.Instance.useAudio)
      return;
    AudioManager.instance.InstancePlay(clip, 1f);
  }

  public static void ResumeMusic()
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance._PlayMusic(AudioManager.instance.clipPausedMusic, AudioManager.instance.pausedTime);
    AudioManager.instance.clipPausedMusic = (AudioClip) null;
  }

  public static void PlayMusic(AudioClip music)
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance.clipPausedMusic = AudioManager.instance.sourceMusic.clip;
    AudioManager.instance.pausedTime = AudioManager.instance.sourceMusic.time;
    AudioManager.instance._PlayMusic(music);
  }

  public static void PlayMusicMainMenu()
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance._PlayMusic(AudioManager.instance.musicMainMenu);
  }

  public static void PlayMusicCharacterCreation()
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance._PlayMusic(AudioManager.instance.musicCharacterCreation);
  }

  public static void PlayGameMusic(int i)
  {
    if (!((Object) AudioManager.instance != (Object) null) || i < 0 || i >= AudioManager.instance.musicGame.Length)
      return;
    AudioManager.instance._PlayMusic(AudioManager.instance.musicGame[i]);
  }

  public void InstancePlay(AudioClip clip, float volume)
  {
    if ((Object) clip == (Object) null)
      return;
    this.sources[this.curIndex].clip = clip;
    this.sources[this.curIndex].time = 0.0f;
    this.sources[this.curIndex].volume = volume;
    this.sources[this.curIndex].Play();
    ++this.curIndex;
    if (this.curIndex < this.sources.Length)
      return;
    this.curIndex = 0;
  }

  private void InstancePlay(AudioClip clip)
  {
    if ((Object) clip == (Object) null)
      return;
    this.sources[this.curIndex].clip = clip;
    this.sources[this.curIndex].time = 0.0f;
    this.sources[this.curIndex].volume = this.volumeSound;
    this.sources[this.curIndex].Play();
    ++this.curIndex;
    if (this.curIndex < this.sources.Length)
      return;
    this.curIndex = 0;
  }

  private void InstancePlayNapalm(AudioClip clip)
  {
    if ((Object) clip == (Object) null)
      return;
    this.sourcesNapalm[this.curIndexNapalm].clip = clip;
    this.sourcesNapalm[this.curIndexNapalm].time = 0.0f;
    this.sourcesNapalm[this.curIndexNapalm].volume = this.volumeSound * 0.5f;
    this.sourcesNapalm[this.curIndexNapalm].Play();
    ++this.curIndexNapalm;
    if (this.curIndexNapalm < this.sourcesNapalm.Length)
      return;
    this.curIndexNapalm = 0;
  }

  private void _PlayMusic(AudioClip clip, float time = 0.0f)
  {
    if ((Object) clip == (Object) null || (Object) this.sourceMusic.clip == (Object) clip)
      return;
    this.sourceMusic.clip = clip;
    this.sourceMusic.time = time;
    this.sourceMusic.Play();
  }

  internal IEnumerator FadePickupMusic(bool stop)
  {
    float cur = this.sourcePickupMusic.volume;
    float volume = PlayerPrefs.GetFloat("prefvolmusic", 0.5f);
    if (stop)
    {
      while ((double) cur > 0.0)
      {
        cur -= Time.deltaTime;
        this.sourcePickupMusic.volume = cur;
        this.sourceMusic.volume = volume - cur;
        yield return (object) null;
      }
      this.sourceMusic.volume = volume;
      this.sourcePickupMusic.volume = 0.0f;
      this.sourcePickupMusic.Pause();
    }
    else
    {
      this.sourcePickupMusic.Play();
      while ((double) cur < (double) volume)
      {
        cur += Time.deltaTime;
        if ((double) cur >= (double) volume)
        {
          cur = volume;
          this.sourcePickupMusic.volume = cur;
          this.sourceMusic.volume = volume - cur;
          break;
        }
        this.sourceMusic.volume = 0.0f;
        this.sourcePickupMusic.volume = cur;
        yield return (object) null;
      }
    }
  }

  private void Awake()
  {
    AudioManager.instance = this;
    this.volumeSound = PlayerPrefs.GetFloat("prefvolsound", 0.5f);
    this.volumeMusic = PlayerPrefs.GetFloat("prefvolmusic", 0.5f);
    this.sourceMusic.volume = this.volumeMusic;
  }

  public static void UpdateVolumeSound(float f)
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance.volumeSound = f;
  }

  public static void UpdateVolumeMusic(float f)
  {
    if (!((Object) AudioManager.instance != (Object) null))
      return;
    AudioManager.instance.volumeMusic = f;
    AudioManager.instance.sourceMusic.volume = f;
  }

  private void OnDestroy() => AudioManager.instance = (AudioManager) null;
}
