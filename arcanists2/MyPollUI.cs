// Decompiled with JetBrains decompiler
// Type: MyPollUI
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using Educative;
using MyPolling;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable disable
public class MyPollUI : MonoBehaviour
{
  public GameObject minaPanel;
  public RectTransform container;
  public RectTransform scrollBox;
  public RectTransform panel;
  public TMP_Text txtSubmit;
  public TMP_Text txtHeader;
  public UIOnHover buttonBack;
  public UIOnHover buttonSubmit;
  public TMP_Text txtQuestion;
  public MyPollUIItem pfabItem;
  internal MyPoll poll;
  private int curIndex;
  public List<MyPollUIItem> items = new List<MyPollUIItem>();
  public float defaultHeight = 420f;
  public float defContainerHeight = 175f;
  public float maxBoxSixe = 500f;
  public float itemHeight = 80f;
  private bool testing;

  public static MyPollUI Instance { get; private set; }

  public static void Create(MyPoll poll, bool test = false)
  {
    if ((Object) MyPollUI.Instance != (Object) null)
      Object.Destroy((Object) MyPollUI.Instance.gameObject);
    MyPollUI andApply = Controller.Instance.CreateAndApply<MyPollUI>(Controller.Instance.mypoll, Controller.Instance.transform);
    andApply.testing = test;
    MyPollUI.Instance = andApply;
    andApply.poll = poll;
    andApply.Setup();
  }

  private void OnDestroy()
  {
    if (!((Object) MyPollUI.Instance == (Object) this))
      return;
    MyPollUI.Instance = (MyPollUI) null;
  }

  private void Setup() => this.render(0);

  private void Update()
  {
    if (!((Object) this.transform.parent.GetChild(this.transform.parent.childCount - 1) != (Object) this.transform))
      return;
    this.transform.SetAsLastSibling();
  }

  internal void SelectItem(int i)
  {
    this.poll.items[this.curIndex].answers[i].isChecked = !this.poll.items[this.curIndex].answers[i].isChecked;
    this.items[i].objCheck.SetActive(this.poll.items[this.curIndex].answers[i].isChecked);
    if (this.poll.items[this.curIndex].multipleSelection)
      return;
    for (int index = 0; index < this.poll.items[this.curIndex].answers.Count; ++index)
    {
      if (index != i)
      {
        this.poll.items[this.curIndex].answers[index].isChecked = false;
        this.items[index].objCheck.SetActive(false);
      }
    }
  }

  internal void OnEdit(int i, string s) => this.poll.items[this.curIndex].answers[i].userInput = s;

  private void render(int index)
  {
    foreach (Component component in this.items)
      Object.Destroy((Object) component.gameObject);
    this.items.Clear();
    this.curIndex = index;
    if (this.poll.items[index].multipleSelection)
      this.txtQuestion.text = "<#7b7b7b>[Multiple Selection]</color><br>" + (object) (index + 1) + ") " + this.poll.items[index].question;
    else
      this.txtQuestion.text = (index + 1).ToString() + ") " + this.poll.items[index].question;
    int index1 = 0;
    foreach (MyPoll.Answer answer in this.poll.items[index].answers)
    {
      MyPollUIItem myPollUiItem = Object.Instantiate<MyPollUIItem>(this.pfabItem, (Transform) this.container);
      myPollUiItem.rect.anchoredPosition = new Vector2(0.0f, (float) -index1 * this.itemHeight);
      myPollUiItem.Setup(index1, answer);
      this.items.Add(myPollUiItem);
      ++index1;
    }
    this.container.sizeDelta = new Vector2(this.container.sizeDelta.x, (float) index1 * this.itemHeight);
    this.txtHeader.text = this.poll.name + " (" + (object) (index + 1) + "/" + (object) this.poll.items.Count + ")";
    this.txtSubmit.text = index < this.poll.items.Count - 1 ? "Next" : "Submit";
    this.buttonBack.gameObject.SetActive(index > 0);
  }

  public void ClickSubmit()
  {
    if (this.curIndex >= this.poll.items.Count - 1)
    {
      int index = this.poll.Verify();
      if (index != -1)
      {
        this.render(index);
      }
      else
      {
        if (this.testing)
        {
          Tutorial.Print("Test complete");
          this.poll.Intitialize();
          this.poll.SanitizeResponses();
          this.poll.AddResponses(this.poll);
          MyPollResults.Create(this.poll);
        }
        else
          Client.PollSubmit(this.poll);
        this.ClickClose();
      }
    }
    else
      this.render(this.curIndex + 1);
  }

  public void ClickBack()
  {
    if (this.curIndex <= 0)
      return;
    this.render(this.curIndex - 1);
  }

  public void ClickMinimize() => this.minaPanel.SetActive(!this.minaPanel.activeSelf);

  public void ClickClose() => Object.Destroy((Object) this.gameObject);
}
