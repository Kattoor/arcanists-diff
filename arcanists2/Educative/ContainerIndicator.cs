// Decompiled with JetBrains decompiler
// Type: Educative.ContainerIndicator
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using MoonSharp.Interpreter;
using System;
using UnityEngine;

#nullable disable
namespace Educative
{
  public class ContainerIndicator
  {
    [MoonSharpHidden]
    public GameObject indicator;
    [MoonSharpHidden]
    public IndicatorKind _kind;
    [MoonSharpHidden]
    private double _radius;
    [MoonSharpHidden]
    private Tutorial tutorial;

    [MoonSharpHidden]
    public ContainerIndicator(GameObject s) => throw new Exception("Notimplemented");

    [MoonSharpHidden]
    public ContainerIndicator(Tutorial t, GameObject s, IndicatorKind k, double radius)
    {
      this.indicator = s;
      this._kind = k;
      this._radius = radius;
      this.tutorial = t;
    }

    public bool isDead => (UnityEngine.Object) this.indicator == (UnityEngine.Object) null;

    public int x
    {
      get => (int) this.indicator.transform.position.x;
      set
      {
        this.indicator.transform.position = new Vector3((float) value, this.indicator.transform.position.y);
      }
    }

    public int y
    {
      get => (int) this.indicator.transform.position.y;
      set
      {
        this.indicator.transform.position = new Vector3(this.indicator.transform.position.x, (float) value);
      }
    }

    public double radius
    {
      get => this._radius;
      set
      {
        this._radius = value;
        if (this.kind != IndicatorKind.Area)
          return;
        this.indicator.transform.localScale = new Vector3((float) this._radius / 128f, (float) this._radius / 128f, 1f);
      }
    }

    public Point position
    {
      get
      {
        return new Point((double) (int) this.indicator.transform.position.x, (double) (int) this.indicator.transform.position.y);
      }
      set => this.indicator.transform.position = new Vector3((float) value.x, (float) value.y);
    }

    public double angle
    {
      get => (double) this.indicator.transform.localEulerAngles.z;
      set => this.indicator.transform.localEulerAngles = new Vector3(0.0f, 0.0f, (float) value);
    }

    public string color
    {
      get
      {
        return ColorUtility.ToHtmlStringRGB(this.indicator.GetComponentInChildren<SpriteRenderer>().color);
      }
      set
      {
        Color color;
        if (!ColorUtility.TryParseHtmlString(value, out color))
          return;
        this.indicator.GetComponentInChildren<SpriteRenderer>().color = color;
      }
    }

    public IndicatorKind kind => this._kind;

    public void Destroy()
    {
      this.tutorial.activeIndicators.Remove(this);
      UnityEngine.Object.Destroy((UnityEngine.Object) this.indicator);
      this.indicator = (GameObject) null;
    }
  }
}
