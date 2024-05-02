// Decompiled with JetBrains decompiler
// Type: testrot
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class testrot : MonoBehaviour
{
  public EditorFixedInt ang = (EditorFixedInt) 0;
  public int width = 50;
  public int height = 20;
  public Transform t1;
  public Transform t2;
  public Transform t3;
  public Transform t4;

  private void Start()
  {
  }

  private void Update()
  {
  }

  [ContextMenu("Do")]
  private void dodo()
  {
    MyLocation centerPoint = new MyLocation(0, 0);
    FixedInt ang = (FixedInt) this.ang;
    MyLocation myLocation1 = RotateImage.RotatePoint(new MyLocation(0, 0), centerPoint, ang);
    MyLocation myLocation2 = RotateImage.RotatePoint(new MyLocation(this.width, 0), centerPoint, ang);
    MyLocation myLocation3 = RotateImage.RotatePoint(new MyLocation(0, this.height), centerPoint, ang);
    MyLocation myLocation4 = RotateImage.RotatePoint(new MyLocation(this.width, this.height), centerPoint, ang);
    this.t1.position = (Vector3) myLocation1.ToSinglePrecision();
    this.t2.position = (Vector3) myLocation2.ToSinglePrecision();
    this.t3.position = (Vector3) myLocation3.ToSinglePrecision();
    this.t4.position = (Vector3) myLocation4.ToSinglePrecision();
  }
}
