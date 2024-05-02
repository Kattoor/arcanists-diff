

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class NotificationMenu : MonoBehaviour
{
  public TMP_Text text;
  public Image glowBg;
  public Image parchment;
  public Image dispelBg;
  public TMP_Text txtDispel;
  public UIOnHover uionhover;
  public AudioClip clipOpen;
  public AudioClip clipClose;
  private Coroutine closing;

  public void Init(string s)
  {
    this.text.text = s;
    AudioManager.Play(this.clipOpen);
  }

  private void Update()
  {
    if (!((Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (Object) this.transform))
      return;
    this.transform.SetAsLastSibling();
  }

  private IEnumerator CloseSlow()
  {
    NotificationMenu notificationMenu = this;
    float scale = 1f;
    AudioManager.Play(notificationMenu.clipClose);
    while ((double) scale > 0.0)
    {
      scale -= Time.deltaTime;
      if ((double) scale < 0.0)
        scale = 0.0f;
      notificationMenu.transform.localScale = new Vector3(scale, scale, scale);
      yield return (object) new WaitForEndOfFrame();
    }
    Object.Destroy((Object) notificationMenu.gameObject);
  }

  public void Close()
  {
    this.uionhover.enabled = false;
    if (this.closing != null)
      return;
    this.closing = this.StartCoroutine(this.CloseSlow());
  }

  private void Alpha(Image x, float a)
  {
    Color color = x.color with { a = a };
    x.color = color;
  }

  private void Alpha(Outline x, float a)
  {
    Color effectColor = x.effectColor with { a = a };
    x.effectColor = effectColor;
  }

  private void Alpha(TMP_Text x, float a)
  {
    Color color = x.color with { a = a };
    x.color = color;
  }
}
