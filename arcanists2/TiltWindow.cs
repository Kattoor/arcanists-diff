// Decompiled with JetBrains decompiler
// Type: TiltWindow
// Assembly: Assembly-CSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA7163A9-CD4F-457E-9379-B1755B6F3B01
// Assembly location: C:\Users\jaspe\Downloads\Arcanists6.8\Arcanists 2_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TiltWindow : MonoBehaviour
{
  public Vector2 range = new Vector2(5f, 3f);
  private Transform mTrans;
  private Quaternion mStart;
  private Vector2 mRot = Vector2.zero;

  private void Start()
  {
    this.mTrans = this.transform;
    this.mStart = this.mTrans.localRotation;
  }

  private void Update()
  {
    Vector3 mousePosition = Input.mousePosition;
    float num1 = (float) Screen.width * 0.5f;
    float num2 = (float) Screen.height * 0.5f;
    this.mRot = Vector2.Lerp(this.mRot, new Vector2(Mathf.Clamp((mousePosition.x - num1) / num1, -1f, 1f), Mathf.Clamp((mousePosition.y - num2) / num2, -1f, 1f)), Time.deltaTime * 5f);
    this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.range.y, this.mRot.x * this.range.x, 0.0f);
  }
}
