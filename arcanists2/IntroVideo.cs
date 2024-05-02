// Decompiled with JetBrains decompiler
// Type: IntroVideo
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

#nullable disable
public class IntroVideo : MonoBehaviour
{
  public VideoPlayer video;
  private bool once;
  public AudioSource audio;
  private bool safe;

  private void Start()
  {
    this.audio.volume = PlayerPrefs.GetFloat("prefvolmusic", 1f);
    this.video.loopPointReached += new VideoPlayer.EventHandler(this.OnEnd);
    this.safe = Global.GetPrefBool("prefSkipIntro", false);
    Global.SetPrefBool("prefSkipIntro", true);
    PlayerPrefs.Save();
    try
    {
      this.video.Play();
      this.audio.Play();
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
      this.SkipOnce();
    }
  }

  public void OnEnd(VideoPlayer p) => this.SkipOnce();

  public void SkipOnce()
  {
    if (this.once)
      return;
    this.once = true;
    if (!this.safe)
      Global.SetPrefBool("prefSkipIntro", false);
    SceneManager.LoadScene("mainmenu");
  }

  private void Update()
  {
    if (this.once || !Input.anyKeyDown)
      return;
    this.SkipOnce();
  }
}
