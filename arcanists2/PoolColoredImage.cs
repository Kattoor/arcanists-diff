// Decompiled with JetBrains decompiler
// Type: PoolColoredImage
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[Serializable]
public class PoolColoredImage
{
  public GameObject type;
  public RectTransform container;
  private List<Image> com = new List<Image>();
  private int index;

  public void SetColor(Color c)
  {
    for (int index = 0; index < this.index; ++index)
      this.com[index].color = c;
  }

  public void SetSprite(Sprite c)
  {
    for (int index = 0; index < this.index; ++index)
      this.com[index].sprite = c;
  }

  public void Clear()
  {
    int index = 0;
    for (int count = this.com.Count; index < count; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.com[index].gameObject);
    this.com = new List<Image>();
    this.index = 0;
  }

  public void DeSpawn()
  {
    for (int index = 0; index < this.index; ++index)
    {
      if (!this.com[index].gameObject.activeSelf)
        return;
      this.com[index].gameObject.SetActive(false);
    }
    this.index = 0;
  }

  public void SpawnBehind(Vector3 pos, Color c)
  {
    if (this.index < this.com.Count)
    {
      this.com[this.index].rectTransform.anchoredPosition = (Vector2) pos;
      this.com[this.index].color = c;
      this.com[this.index].gameObject.SetActive(true);
      this.com[this.index].transform.SetAsFirstSibling();
      ++this.index;
    }
    else
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.type, pos, this.container.rotation, (Transform) this.container);
      Image component = gameObject.GetComponent<Image>();
      component.rectTransform.anchoredPosition = (Vector2) pos;
      component.rectTransform.localScale = new Vector3(1f, 1f, 1f);
      component.color = c;
      this.com.Add(component);
      gameObject.SetActive(true);
      gameObject.transform.SetAsFirstSibling();
      ++this.index;
    }
  }

  public Image Spawn()
  {
    for (int index = 0; index < this.com.Count; ++index)
    {
      if (!this.com[index].gameObject.activeInHierarchy)
      {
        this.com[index].gameObject.SetActive(true);
        return this.com[index];
      }
    }
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.type, this.container.position, this.container.rotation, (Transform) this.container);
    Image component = gameObject.GetComponent<Image>();
    component.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    this.com.Add(component);
    gameObject.SetActive(true);
    return component;
  }

  public void Spawn(Vector3 pos, Color c)
  {
    if (this.index < this.com.Count)
    {
      this.com[this.index].rectTransform.anchoredPosition = (Vector2) pos;
      this.com[this.index].color = c;
      this.com[this.index].gameObject.SetActive(true);
      ++this.index;
    }
    else
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.type, pos, this.container.rotation, (Transform) this.container);
      Image component = gameObject.GetComponent<Image>();
      component.rectTransform.anchoredPosition = (Vector2) pos;
      component.rectTransform.localScale = new Vector3(1f, 1f, 1f);
      component.color = c;
      this.com.Add(component);
      gameObject.SetActive(true);
      ++this.index;
    }
  }

  public void Spawn(Vector3 pos, Vector3 scale, Color c)
  {
    if (this.index < this.com.Count)
    {
      this.com[this.index].rectTransform.anchoredPosition = (Vector2) pos;
      this.com[this.index].color = c;
      this.com[this.index].transform.localScale = scale;
      this.com[this.index].gameObject.SetActive(true);
      ++this.index;
    }
    else
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.type, pos, this.container.rotation, (Transform) this.container);
      Image component = gameObject.GetComponent<Image>();
      gameObject.transform.localScale = scale;
      component.rectTransform.anchoredPosition = (Vector2) pos;
      component.color = c;
      this.com.Add(component);
      gameObject.SetActive(true);
      ++this.index;
    }
  }

  public void SpawnColorScheme(Vector3 pos, Vector2 size, Color c)
  {
    if (this.index < this.com.Count)
    {
      this.com[this.index].rectTransform.anchoredPosition = (Vector2) pos;
      this.com[this.index].color = c;
      this.com[this.index].rectTransform.sizeDelta = size;
      this.com[this.index].gameObject.SetActive(true);
      ++this.index;
    }
    else
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.type, pos, this.container.rotation, (Transform) this.container);
      Image component = gameObject.GetComponent<Image>();
      component.rectTransform.anchoredPosition = (Vector2) pos;
      component.rectTransform.sizeDelta = size;
      component.color = c;
      this.com.Add(component);
      gameObject.SetActive(true);
      ++this.index;
    }
  }

  public PoolColoredImage()
  {
  }

  public PoolColoredImage(GameObject t)
  {
    if ((UnityEngine.Object) t == (UnityEngine.Object) null)
      Debug.LogError((object) "Pool GameObject is NULL");
    else
      this.type = t;
  }
}
