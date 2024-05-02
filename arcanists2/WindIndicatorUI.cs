// Decompiled with JetBrains decompiler
// Type: WindIndicatorUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class WindIndicatorUI : MonoBehaviour
{
  public RectTransform needle;
  public Image img_speed;
  private float targetRot;
  private float targetPow;
  private float startRot;
  private float startPow;

  public static WindIndicatorUI Instance { get; private set; }

  public void Refresh()
  {
    this.startRot = this.needle.localEulerAngles.z;
    this.startPow = this.img_speed.fillAmount;
    this.targetRot = Client.game.map.windDir.ToFloat();
    this.targetPow = Client.game.map.windSpeed.ToFloat() / ZMap.MaxWindSpeed.ToFloat();
    while ((double) this.targetRot > 360.0)
      this.targetRot -= 360f;
    while ((double) this.targetRot < 0.0)
      this.targetRot += 360f;
    if ((double) Mathf.Abs(this.targetRot - this.startRot) > 180.0)
    {
      if ((double) this.targetRot < (double) this.startRot)
        this.targetRot += 360f;
      else
        this.targetRot -= 360f;
    }
    this.StopAllCoroutines();
    if (this.gameObject.activeInHierarchy)
    {
      this.StartCoroutine(this.gogo());
    }
    else
    {
      this.needle.transform.localEulerAngles = new Vector3(0.0f, 0.0f, this.targetRot);
      this.img_speed.fillAmount = this.targetPow;
    }
  }

  private IEnumerator gogo()
  {
    float cur = 0.0f;
    while ((double) cur < 1.0)
    {
      cur += Time.deltaTime * 2f;
      this.needle.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.SmoothStep(this.startRot, this.targetRot, cur));
      this.img_speed.fillAmount = Mathf.SmoothStep(this.startPow, this.targetPow, cur);
      yield return (object) null;
    }
    this.needle.transform.localEulerAngles = new Vector3(0.0f, 0.0f, this.targetRot);
    this.img_speed.fillAmount = this.targetPow;
  }

  private void Awake() => WindIndicatorUI.Instance = this;

  private void OnDestroy()
  {
    if (!((Object) WindIndicatorUI.Instance == (Object) this))
      return;
    WindIndicatorUI.Instance = (WindIndicatorUI) null;
  }
}
