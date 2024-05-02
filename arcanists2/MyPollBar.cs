// Decompiled with JetBrains decompiler
// Type: MyPollBar
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class MyPollBar : MonoBehaviour
{
  public RectTransform rect;
  public TMP_Text txtAnswer;
  public TMP_Text txtPercent;
  public TMP_Text txtAmount;
  public Image imgBar;
  public RectTransform rectBar;

  public void Setup(int i, string label, float percent, int amount)
  {
    this.imgBar.color = ClientResources.Instance.pollColors[i % ClientResources.Instance.pollColors.Length];
    this.txtAnswer.text = label;
    this.txtPercent.text = (double) percent >= 0.5 ? percent.ToString("0") + "%" : "";
    this.txtAmount.text = amount > 0 ? amount.ToString() : "-";
    this.rectBar.sizeDelta = new Vector2(Mathf.Lerp(0.0f, 700f, percent / 100f), this.rectBar.sizeDelta.y);
    this.gameObject.SetActive(true);
  }

  public void Setup(string userData)
  {
    this.txtAnswer.text = userData;
    this.gameObject.SetActive(true);
  }
}
