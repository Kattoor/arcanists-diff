// Decompiled with JetBrains decompiler
// Type: MyPollResults
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MyPolling;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TMPro;
using UnityEngine;

#nullable disable
public class MyPollResults : MonoBehaviour
{
  public GameObject panel;
  public TMP_Text txtSubmit;
  public TMP_Text txtHeader;
  public UIOnHover buttonBack;
  public UIOnHover buttonSubmit;
  public GameObject buttonExport;
  public RectTransform container;
  public MyPollBar pfab;
  public MyPollBar pfabUserData;
  public TMP_Text txtQuestion;
  internal MyPoll poll;
  private int curIndex;
  private const string path = "C:\\Users\\after\\source\\repos\\GooglePoll\\GooglePoll\\bin\\Debug\\";

  public static MyPollResults Instance { get; private set; }

  public static void Create(MyPoll poll)
  {
    if ((Object) MyPollResults.Instance != (Object) null)
      Object.Destroy((Object) MyPollResults.Instance.gameObject);
    MyPollResults andApply;
    MyPollResults.Instance = andApply = Controller.Instance.CreateAndApply<MyPollResults>(Controller.Instance.mypollResults, Controller.Instance.transform);
    andApply.poll = poll;
    andApply.Setup();
    andApply.CheckIfExportable();
  }

  private void OnDestroy()
  {
    if (!((Object) MyPollResults.Instance == (Object) this))
      return;
    MyPollResults.Instance = (MyPollResults) null;
  }

  private void Setup() => this.render(0);

  private void Update()
  {
    if (!((Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (Object) this.transform) || !((Object) MyToolTip.instance == (Object) null) || !((Object) MyContextMenu.instance == (Object) null))
      return;
    this.transform.SetAsLastSibling();
  }

  private void render(int index)
  {
    this.container.DestroyChildern();
    this.curIndex = index;
    if (this.poll.items[index].multipleSelection)
      this.txtQuestion.text = "<#7b7b7b>[Multiple Selection]</color><br>" + (object) (index + 1) + ") " + this.poll.items[index].question;
    else
      this.txtQuestion.text = (index + 1).ToString() + ") " + this.poll.items[index].question;
    List<string> stringList = new List<string>();
    List<int> intList = new List<int>();
    float num1 = 0.0f;
    int index1 = 0;
    foreach (int response in this.poll.items[index].responses)
    {
      if (this.poll.items[index].answers[index1].checkable)
      {
        stringList.Add(this.poll.items[index].answers[index1].text);
        intList.Add(response);
        num1 += (float) response;
        ++index1;
      }
    }
    float num2 = num1 / 100f;
    int num3;
    for (num3 = 0; num3 < intList.Count; ++num3)
    {
      MyPollBar myPollBar = Object.Instantiate<MyPollBar>(this.pfab, (Transform) this.container);
      myPollBar.rect.anchoredPosition = new Vector2(5f, (float) (-num3 * 26));
      myPollBar.Setup(num3, stringList[num3], (float) intList[num3] / num2, intList[num3]);
    }
    for (int index2 = 0; index2 < this.poll.items[index].userData.Count; ++index2)
    {
      MyPollBar myPollBar = Object.Instantiate<MyPollBar>(this.pfabUserData, (Transform) this.container);
      myPollBar.rect.anchoredPosition = new Vector2(5f, (float) (-num3 * 26));
      myPollBar.Setup(this.poll.items[index].userData[index2]);
      ++num3;
    }
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) (num3 * 26));
    this.txtHeader.text = this.poll.name + " (" + (object) (index + 1) + "/" + (object) this.poll.items.Count + ")";
    this.buttonBack.gameObject.SetActive(index > 0);
    this.buttonSubmit.gameObject.SetActive(index < this.poll.items.Count - 1);
  }

  private void CheckIfExportable()
  {
    this.buttonExport.SetActive(File.Exists("C:\\Users\\after\\source\\repos\\GooglePoll\\GooglePoll\\bin\\Debug\\GooglePoll.exe"));
  }

  public void ClickExport()
  {
    File.WriteAllBytes("C:\\Users\\after\\source\\repos\\GooglePoll\\GooglePoll\\bin\\Debug\\data/activepoll.poll", this.poll.ToBytesWithResponses());
    if (Input.GetKey(KeyCode.LeftControl))
      return;
    Process.Start("C:\\Users\\after\\source\\repos\\GooglePoll\\GooglePoll\\bin\\Debug\\GooglePoll.exe");
  }

  public void ClickSubmit()
  {
    if (this.curIndex >= this.poll.items.Count - 1)
      return;
    this.render(this.curIndex + 1);
  }

  public void ClickBack()
  {
    if (this.curIndex <= 0)
      return;
    this.render(this.curIndex - 1);
  }

  public void ClickMinimize() => this.panel.SetActive(!this.panel.activeSelf);

  public void ClickClose() => Object.Destroy((Object) this.gameObject);

  public void HoverOverflow(TMP_Text t)
  {
    t.color = new Color(0.6f, 0.6f, 0.6f);
    if (!t.isTextTruncated)
      return;
    MyToolTip.Show(t.text);
  }

  public void HideToolTip(TMP_Text t)
  {
    t.color = Color.white;
    MyToolTip.Close();
  }
}
