

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class SystemUpdate : MonoBehaviour
{
  public static SystemUpdate Instance;
  public TMP_Text txt;
  public TMP_Text overTxt;
  public ContentSizeFitter contentSizeFitter;
  private float cur;
  private string t;
  private int lastS = -1;

  public static void Create(string txt, float time)
  {
    if ((Object) SystemUpdate.Instance != (Object) null)
    {
      SystemUpdate.Instance.Setup(txt, time);
    }
    else
    {
      SystemUpdate systemUpdate = Object.Instantiate<SystemUpdate>(Controller.Instance.systemUpdate, Controller.Instance.transform);
      ((RectTransform) systemUpdate.transform).anchoredPosition = new Vector2(-2f, 2f);
      systemUpdate.Setup(txt, time);
    }
  }

  private void Awake() => SystemUpdate.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) SystemUpdate.Instance == (Object) this))
      return;
    SystemUpdate.Instance = (SystemUpdate) null;
  }

  private void Setup(string txt, float time)
  {
    this.contentSizeFitter.enabled = true;
    this.t = txt;
    this.cur = time;
    this.lastS = -1;
    this.StartCoroutine(this.DelaySize());
  }

  private IEnumerator DelaySize()
  {
    yield return (object) 0;
    yield return (object) 0;
    this.contentSizeFitter.enabled = false;
  }

  private void Update()
  {
    this.cur -= Time.deltaTime;
    if ((double) this.cur < 0.0)
      this.cur = 0.0f;
    int s = (int) this.cur % 60;
    if ((Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (Object) this.transform)
      this.transform.SetAsLastSibling();
    if ((double) this.cur == 0.0 || !Client.isConnected)
    {
      Object.Destroy((Object) this.gameObject);
    }
    else
    {
      if (this.lastS == s)
        return;
      this.lastS = s;
      this.txt.text = this.t + " <mspace=.5em>" + Global.ToTime((int) this.cur / 60, s);
      this.overTxt.text = this.txt.text;
    }
  }
}
