// Decompiled with JetBrains decompiler
// Type: LED
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D266BEE2-E7E9-4299-9752-8BB93E4AAF85
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.9\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class LED : MonoBehaviour
{
  public int columnCount = 7;
  public int rowCount = 7;
  public RectTransform gridContainer;
  private Color colorOn = Color.green;
  private Color colorOff = Color.grey;
  private List<Image> array = new List<Image>();
  private char c = 'a';
  private StringBuilder b = new StringBuilder();

  private void Start() => this.GenerateGrid();

  private void GenerateGrid()
  {
    Rect rect = this.gridContainer.rect;
    double a = (double) rect.width / (double) this.columnCount;
    rect = this.gridContainer.rect;
    double b = (double) rect.height / (double) this.rowCount;
    float num1 = Mathf.Min((float) a, (float) b);
    GameObject original = new GameObject("ImagePrefab", new System.Type[1]
    {
      typeof (RectTransform)
    });
    original.AddComponent<Image>();
    for (int index1 = 0; index1 < this.rowCount; ++index1)
    {
      for (int index2 = 0; index2 < this.columnCount; ++index2)
      {
        int num2 = index1;
        int num3 = index2;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, (Transform) this.gridContainer);
        Image image = gameObject.GetComponent<Image>();
        image.rectTransform.sizeDelta = new Vector2(num1, num1);
        image.rectTransform.anchoredPosition = new Vector2((float) num3 * num1, (float) -num2 * num1);
        this.array.Add(image);
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerEnter;
        entry1.callback.AddListener((UnityAction<BaseEventData>) (data =>
        {
          if (!Input.GetMouseButton(0))
            return;
          image.color = this.IsOn(image) ? this.colorOff : this.colorOn;
        }));
        eventTrigger.triggers.Add(entry1);
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerDown;
        entry2.callback.AddListener((UnityAction<BaseEventData>) (data => image.color = this.IsOn(image) ? this.colorOff : this.colorOn));
        eventTrigger.triggers.Add(entry2);
      }
    }
    UnityEngine.Object.Destroy((UnityEngine.Object) original);
    this.ResetGrid();
  }

  public void ResetGrid()
  {
    foreach (Image componentsInChild in this.gridContainer.GetComponentsInChildren<Image>())
    {
      if ((UnityEngine.Object) componentsInChild.transform != (UnityEngine.Object) componentsInChild)
        componentsInChild.color = this.colorOff;
    }
  }

  public void Save()
  {
    this.b.Append(this.c).Append(" = {");
    for (int index1 = 0; index1 < 7; ++index1)
    {
      int num = 0;
      for (int index2 = 0; index2 < 7; ++index2)
        num |= this.IsOn(this.array[index1 * 7 + index2]) ? 1 << index2 : 0;
      this.b.Append(num);
      if (index1 < 6)
        this.b.Append(", ");
    }
    this.b.Append("},\n");
    Debug.Log((object) this.b);
    Global.systemCopyBuffer = this.b.ToString();
    ++this.c;
    Debug.Log((object) ("Now do " + this.c.ToString()));
  }

  private bool IsOn(Image i) => (double) i.color.r == 0.0;
}
