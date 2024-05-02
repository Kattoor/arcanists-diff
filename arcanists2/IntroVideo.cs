

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
