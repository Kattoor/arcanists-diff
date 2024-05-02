

using System.Collections;
using TMPro;
using UnityEngine;

#nullable disable
public class PfabServerButton : MonoBehaviour
{
  public TMP_Text txtName;
  public TMP_Text PingText;
  public UIOnHover uibutton;

  public void Ping(string isp) => this.StartCoroutine(this._Ping(isp));

  private IEnumerator _Ping(string isp)
  {
    yield return (object) 0;
    int pingErrors = 0;
    bool useColor = true;
    float waitTime = 2f;
    while ((Object) LocalServerConn.Instance != (Object) null)
    {
      UnityEngine.Ping ping = new UnityEngine.Ping(isp);
      float max;
      for (max = 0.0f; (double) max < 2.0 && !ping.isDone; max += Time.deltaTime)
        yield return (object) 0;
      if (ping.isDone)
      {
        pingErrors = 0;
        this.PingText.text = FpsToText.FpsStringMap[Mathf.Clamp(ping.time, 0, 2000)];
        if (useColor)
        {
          if (ping.time < 75)
            this.PingText.color = FpsToText.Good;
          else if (ping.time < 175)
            this.PingText.color = FpsToText.Okay;
          else
            this.PingText.color = FpsToText.Bad;
        }
      }
      else
      {
        ++pingErrors;
        if (pingErrors == 5)
        {
          this.PingText.text = "-";
          if (useColor)
            this.PingText.color = FpsToText.Bad;
        }
        ping.DestroyPing();
      }
      for (max = 0.0f; (double) max < (double) waitTime; max += Time.deltaTime)
        yield return (object) 0;
      ++waitTime;
      ping = (UnityEngine.Ping) null;
    }
  }
}
